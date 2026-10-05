# Graphical installer source

`Installer.cs` is the Windows Forms source for the unsigned community installer. It locates Steam libraries through registry paths and `libraryfolders.vdf`, validates the selected game directory, backs up files, applies a binary delta and verifies hashes. It does not download files or perform account authentication.

## Build on Windows

Requirements: the .NET Framework C# compiler and framework references listed below. Build from a separate folder, not a game installation.

1. Extract `Dressmaker_PTBR_Installer.zip` and run this PowerShell command to export the embedded payload from the existing installer for a rebuild:

```powershell
$assembly = [Reflection.Assembly]::LoadFrom((Resolve-Path '.\Dressmaker-PTBR-Setup.exe'))
$source = $assembly.GetManifestResourceStream('payload.zip')
$target = [IO.File]::Create((Join-Path (Get-Location) 'payload.zip'))
try { $source.CopyTo($target) } finally { $target.Dispose(); $source.Dispose() }
```

This reads the embedded data; it does not invoke the installer entry point. Place `Installer.cs` alongside `payload.zip` and compile:

```powershell
& "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /nologo /target:winexe /optimize+ /reference:System.Windows.Forms.dll /reference:System.Drawing.dll /reference:System.Web.Extensions.dll /reference:System.IO.Compression.dll /reference:System.IO.Compression.FileSystem.dll /resource:payload.zip,payload.zip /out:Dressmaker-PTBR-Setup-Rebuilt.exe Installer.cs
```

Use Windows PowerShell 5.1 for the assembly-export command. If your framework is installed elsewhere, adjust the compiler path. The rebuilt binary may not be byte-identical because compiler versions and build metadata differ; verify behavior and payload rather than claiming reproducible builds. Rebuilding without a signing certificate does not remove Windows security warnings.

The resource delta accepts only the original SHA-256 in its manifest, or an already-translated target. It cannot merge arbitrary mods. Do not change compatibility hashes to bypass validation. Translation-authoring and delta-generation sources remain in the private development workspace; contact Bianca for integration assistance.
