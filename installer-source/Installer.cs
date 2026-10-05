using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.Win32;

public static class Installer {
    static ZipArchive Payload() { return new ZipArchive(Assembly.GetExecutingAssembly().GetManifestResourceStream("payload.zip"), ZipArchiveMode.Read); }
    static string Hash(string path) { using(var s=File.OpenRead(path)) using(var h=SHA256.Create()) return BitConverter.ToString(h.ComputeHash(s)).Replace("-", "").ToLowerInvariant(); }
    static string Hash(byte[] b) { using(var h=SHA256.Create()) return BitConverter.ToString(h.ComputeHash(b)).Replace("-", "").ToLowerInvariant(); }
    static byte[] Read(ZipArchive z, string name) { var e=z.GetEntry(name); if(e==null) throw new Exception("Pacote incompleto: "+name); using(var s=e.Open()) using(var m=new MemoryStream()) { s.CopyTo(m); return m.ToArray(); } }
    public static bool ValidFolder(string path) { return File.Exists(Path.Combine(path,"Dressmaker.exe")) && File.Exists(Path.Combine(path,"Dressmaker_Data","resources.assets")); }
    public static List<string> Detect() {
        var roots=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach(var key in new[]{@"HKEY_CURRENT_USER\Software\Valve\Steam",@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam"}) {
            foreach(var value in new[]{"SteamPath","InstallPath"}) { var p=Registry.GetValue(key,value,null) as string; if(!String.IsNullOrEmpty(p)) roots.Add(p); }
        }
        roots.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),"Steam"));
        foreach(var root in new List<string>(roots)) {
            try {
                var file=Path.Combine(root,"steamapps","libraryfolders.vdf");
                if(File.Exists(file)) foreach(Match m in Regex.Matches(File.ReadAllText(file), "\"(?:path|[0-9]+)\"\\s+\"([^\"]+)\"")) {
                    var p=m.Groups[1].Value.Replace(@"\\",@"\"); if(Path.IsPathRooted(p)) roots.Add(p);
                }
            } catch { }
        }
        var result=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach(var root in roots) {
            var steamapps=Path.Combine(root,"steamapps");
            var common=Path.Combine(steamapps,"common");
            var conventional=Path.Combine(common,"Dressmaker");
            if(ValidFolder(conventional)) result.Add(conventional);
            try {
                if(!Directory.Exists(steamapps)) continue;
                foreach(var file in Directory.GetFiles(steamapps,"appmanifest_*.acf")) {
                    var text=File.ReadAllText(file);
                    if(!Regex.IsMatch(text,"\"name\"\\s+\"Dressmaker\"",RegexOptions.IgnoreCase)) continue;
                    var m=Regex.Match(text,"\"installdir\"\\s+\"([^\"]+)\"");
                    if(m.Success) { var p=Path.Combine(common,m.Groups[1].Value); if(ValidFolder(p)) result.Add(p); }
                }
            } catch { }
        }
        return new List<string>(result);
    }
    public static string Install(string game, Action<string> progress) {
        if(Process.GetProcessesByName("Dressmaker").Length>0) throw new Exception("Feche o Dressmaker antes de instalar.");
        game=Path.GetFullPath(game);
        if(!ValidFolder(game)) throw new Exception("Selecione a pasta que contém Dressmaker.exe e Dressmaker_Data, não a pasta Dressmaker_Data.");
        Action<string> say=progress??delegate(string s){};
        var data=Path.Combine(game,"Dressmaker_Data");
        using(var z=Payload()) {
            var m=new JavaScriptSerializer().Deserialize<Dictionary<string,object>>(Encoding.UTF8.GetString(Read(z,"manifest.json")));
            var files=(Dictionary<string,object>)m["files"];
            var resource=Path.Combine(data,"resources.assets");
            say("Verificando compatibilidade e integridade...");
            var originalHash=Hash(resource);
            var expected=(string)m["resourcesTargetSha256"];
            if(originalHash!=(string)m["resourcesBaseSha256"] && originalHash!=expected) throw new Exception("Esta versão do resources.assets é incompatível ou foi modificada. Nenhum arquivo foi alterado. Use a versão compatível; não tente ignorar esta verificação.");
            var patch=Read(z,"patches/resources.delta");
            if(Hash(patch)!=(string)m["patchSha256"]) throw new Exception("Patch corrompido.");
            foreach(var f in files) {
                var destination=Path.Combine(data,f.Key.Replace('/',Path.DirectorySeparatorChar));
                if(!File.Exists(destination)) throw new Exception("Arquivo do jogo ausente: "+f.Key);
                if(f.Key!="resources.assets" && Hash(Read(z,f.Key.Substring("StreamingAssets/".Length)))!=(string)f.Value) throw new Exception("Pacote corrompido: "+f.Key);
            }
            var backup=Path.Combine(game,"PTBR_Backup_"+DateTime.Now.ToString("yyyyMMdd_HHmmss")+"_"+Guid.NewGuid().ToString("N").Substring(0,8));
            Directory.CreateDirectory(backup);
            say("Criando cópia de segurança...");
            foreach(var f in files) { var saved=Path.Combine(backup,f.Key.Replace('/',Path.DirectorySeparatorChar)); Directory.CreateDirectory(Path.GetDirectoryName(saved)); File.Copy(Path.Combine(data,f.Key.Replace('/',Path.DirectorySeparatorChar)),saved); }
            var temp=Path.Combine(backup,"resources.assets.patched.tmp");
            try {
                if(originalHash!=expected) {
                    say("Aplicando tradução aos nomes das peças...");
                    using(var input=File.OpenRead(resource)) using(var reader=new BinaryReader(new MemoryStream(patch))) using(var output=File.Create(temp)) {
                        if(Encoding.ASCII.GetString(reader.ReadBytes(8))!="DMPTBR01") throw new Exception("Cabeçalho do patch inválido.");
                        var buffer=new byte[1048576];
                        while(reader.BaseStream.Position<reader.BaseStream.Length) {
                            var op=reader.ReadByte(); Stream source; long remaining;
                            if(op==0) { var offset=reader.ReadInt64(); remaining=reader.ReadInt64(); if(offset<0 || remaining<0 || offset>input.Length-remaining) throw new Exception("Patch inválido."); input.Position=offset; source=input; }
                            else if(op==1) { remaining=reader.ReadInt64(); if(remaining<0 || remaining>reader.BaseStream.Length-reader.BaseStream.Position) throw new Exception("Patch inválido."); source=reader.BaseStream; }
                            else throw new Exception("Operação inválida.");
                            while(remaining>0) { var n=source.Read(buffer,0,(int)Math.Min(remaining,buffer.Length)); if(n<=0) throw new Exception("Patch incompleto."); output.Write(buffer,0,n); remaining-=n; }
                        }
                    }
                    if(Hash(temp)!=expected) throw new Exception("Falha ao verificar o arquivo reconstruído.");
                    File.Copy(temp,resource,true); File.Delete(temp);
                }
                say("Instalando e verificando os arquivos de tradução...");
                foreach(var f in files) if(f.Key!="resources.assets") File.WriteAllBytes(Path.Combine(data,f.Key.Replace('/',Path.DirectorySeparatorChar)),Read(z,f.Key.Substring("StreamingAssets/".Length)));
                foreach(var f in files) if(Hash(Path.Combine(data,f.Key.Replace('/',Path.DirectorySeparatorChar)))!=(string)f.Value) throw new Exception("Falha de verificação: "+f.Key);
            } catch(Exception installError) {
                try { foreach(var f in files) File.Copy(Path.Combine(backup,f.Key.Replace('/',Path.DirectorySeparatorChar)),Path.Combine(data,f.Key.Replace('/',Path.DirectorySeparatorChar)),true); }
                catch(Exception restoreError) { throw new Exception("Falha na instalação e na restauração automática. Guarde o backup em "+backup+". "+installError.Message+" / "+restoreError.Message); }
                throw new Exception(installError.Message+" Os arquivos anteriores foram restaurados. Backup: "+backup);
            }
            return backup;
        }
    }
    [STAThread] public static void Main() { Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false); Application.Run(new InstallerWindow()); }
}

public class InstallerWindow : Form {
    TextBox folder=new TextBox(); Button browse=new Button(); Button install=new Button(); Label status=new Label(); bool busy;
    public InstallerWindow() {
        Text="Dressmaker — Tradução Português (Brasil)"; ClientSize=new Size(640,320); FormBorderStyle=FormBorderStyle.FixedDialog; MaximizeBox=false; StartPosition=FormStartPosition.CenterScreen;
        var title=new Label {Text="Instalar tradução PT-BR",Font=new Font("Segoe UI",15,FontStyle.Bold),Location=new Point(20,18),Size=new Size(590,35)};
        var instructions=new Label {Text="Feche o jogo. Confirme abaixo a pasta que contém Dressmaker.exe e Dressmaker_Data.\nNão selecione Dressmaker_Data nem a pasta da Steam inteira.\nSteam: botão direito no jogo → Gerenciar → Explorar arquivos locais.",Location=new Point(20,65),Size=new Size(600,65)};
        folder.SetBounds(20,145,480,25); browse.Text="Selecionar..."; browse.SetBounds(510,143,110,28);
        var note=new Label {Text="A instalação verifica a versão do jogo e guarda uma cópia de segurança.\nNão altera seus saves. Não baixa arquivos nem usa sua conta Steam.",Location=new Point(20,185),Size=new Size(600,42)};
        status.SetBounds(20,235,600,30); install.Text="Instalar tradução"; install.SetBounds(430,275,190,32);
        Controls.AddRange(new Control[]{title,instructions,folder,browse,note,status,install});
        browse.Click+=delegate { using(var dialog=new FolderBrowserDialog {Description="Selecione a pasta do jogo que contém Dressmaker.exe e Dressmaker_Data.",ShowNewFolderButton=false}) { if(Directory.Exists(folder.Text)) dialog.SelectedPath=folder.Text; if(dialog.ShowDialog()==DialogResult.OK) folder.Text=dialog.SelectedPath; } };
        install.Click+=async delegate {
            if(!Installer.ValidFolder(folder.Text)) { MessageBox.Show(this,"Selecione a pasta que contém Dressmaker.exe e Dressmaker_Data. Na Steam, use Gerenciar → Explorar arquivos locais.","Pasta incorreta",MessageBoxButtons.OK,MessageBoxIcon.Warning); return; }
            if(MessageBox.Show(this,"Instalar a tradução nesta pasta?\n\n"+folder.Text+"\n\nSerá criado um backup antes de substituir arquivos.","Confirmar instalação",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
            busy=true; install.Enabled=browse.Enabled=folder.Enabled=false;
            try { var path=folder.Text; var backup=await Task.Run(()=>Installer.Install(path,s=>BeginInvoke(new Action(()=>status.Text=s)))); status.Text="Instalação concluída e verificada."; MessageBox.Show(this,"Tradução instalada! Abra o jogo e selecione Português (Brasil), ou pt.\n\nBackup: "+backup,"Concluído",MessageBoxButtons.OK,MessageBoxIcon.Information); }
            catch(Exception e) { status.Text="Não foi possível concluir."; MessageBox.Show(this,e.Message,"Instalação não concluída",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            finally { busy=false; install.Enabled=browse.Enabled=folder.Enabled=true; }
        };
        FormClosing+=delegate(object s,FormClosingEventArgs e) { if(busy) { e.Cancel=true; MessageBox.Show(this,"Aguarde a instalação terminar para fechar."); } };
        Shown+=delegate { var found=Installer.Detect(); if(found.Count>0) { folder.Text=found[0]; status.Text=found.Count==1?"Jogo localizado. Confira a pasta antes de instalar.":"Mais de uma instalação encontrada. Confira ou selecione a pasta desejada."; } else status.Text="Jogo não localizado. Clique em Selecionar para indicar a pasta."; };
    }
}
