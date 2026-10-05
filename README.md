# Dressmaker — Tradução para Português (Brasil)

**Português (Brasil)** | [English](README.en.md)

Tradução comunitária não oficial de **Dressmaker** para português brasileiro, revisada e adaptada por **Bianca**. Inclui diálogos, interface, tutoriais, nomes de tecidos e peças de moldes.

## Escolha como instalar

Use **uma** das opções; não precisa instalar as duas. Ambas resultam nos mesmos arquivos traduzidos.

| Opção | Download | Para quem prefere |
| --- | --- | --- |
| Instalação manual | [Dressmaker_PTBR_Manual.zip](packages/manual/Dressmaker_PTBR_Manual.zip) | Conferir e copiar os arquivos sem executar um instalador. |
| Instalador com interface | [Dressmaker_PTBR_Installer.zip](packages/installer/Dressmaker_PTBR_Installer.zip) | Localização automática do jogo, backup e verificações de compatibilidade. |

No GitHub, abra o ZIP desejado e clique em **Download raw file** (baixar arquivo). Não é necessário baixar o repositório inteiro. Depois de extrair um dos pacotes, comece pelo `README.md`, em português; `README.en.md` contém as instruções em inglês. Os links para pastas do repositório funcionam no GitHub, não na pasta extraída.

O instalador já contém os dados necessários: **não precisa baixar o pacote manual junto**.

## Requisitos e compatibilidade

Você precisa do **Dressmaker para Windows instalado pela Steam**. Os pacotes foram conferidos com os arquivos instalados em **4 de outubro de 2026**. O instalador verifica internamente a versão do arquivo de recursos antes de aplicar as alterações. Não foi confirmada compatibilidade com atualizações futuras, outras plataformas ou outros mods.

**Faça backup dos cinco arquivos antes de substituí-los.** O pacote manual substitui o `resources.assets` completo e pode sobrescrever alterações de outros mods. Não use numa versão incompatível. O instalador recusa um arquivo incompatível ou modificado, em vez de tentar combinar alterações. Ele requer .NET Framework 4.5 ou posterior, normalmente disponível nas instalações modernas do Windows.

## Opção A — Instalação manual, sem executar programas

1. Feche o Dressmaker.
2. Na Steam, clique com o botão direito no jogo → **Gerenciar → Explorar arquivos locais**. Assim você encontra a pasta correta mesmo se a Steam estiver em outro disco. Confirme que ela contém **Dressmaker.exe** e **Dressmaker_Data**.
3. Faça uma cópia de segurança dos cinco arquivos listados em **Arquivos alterados**, mantendo a estrutura das pastas. Guarde o backup fora dos arquivos que serão substituídos.
4. Extraia **Dressmaker_PTBR_Manual.zip** primeiro numa pasta separada. Dentro haverá `Dressmaker_Data` e as instruções nos dois idiomas.
5. Copie a pasta **Dressmaker_Data** extraída para a pasta do jogo que contém `Dressmaker.exe`. **Mescle as pastas** e confirme a substituição dos cinco arquivos. Não apague a pasta `Dressmaker_Data` existente e não coloque uma segunda `Dressmaker_Data` dentro dela.
6. Abra o jogo e escolha **Português (Brasil)** nas opções de idioma. Pode aparecer como **pt**.

A estrutura final deve ficar assim:

```text
Sua biblioteca da Steam/.../Dressmaker/
├── Dressmaker.exe                    (já existe; não acompanha a tradução)
└── Dressmaker_Data/
    ├── resources.assets
    └── StreamingAssets/aa/
        ├── catalog.bin
        └── StandaloneWindows64/
            ├── localization-asset-tables-english(en)_assets_all.bundle
            ├── localization-locales_assets_all.bundle
            └── localization-string-tables-english(en)_assets_all.bundle
```

O método manual **não verifica a compatibilidade nem cria backup automaticamente**. Ele inclui o arquivo de recursos modificado completo porque copiar apenas um pequeno patch não aplicaria as alterações.

## Opção B — Instalador com interface

1. Feche o jogo e extraia **Dressmaker_PTBR_Installer.zip** numa pasta separada, não na pasta do jogo.
2. Abra **Dressmaker-PTBR-Setup.exe** com dois cliques. Não são necessários comandos de PowerShell, Python ou ferramentas adicionais. A interface está em português brasileiro.
3. O instalador procura a instalação da Steam e suas bibliotecas, inclusive em outros discos, e mostra a pasta encontrada. **Confira o caminho**, especialmente se houver mais de uma instalação.
4. Se não encontrar o jogo ou mostrar a pasta errada, clique em **Selecionar...**. Use **Gerenciar → Explorar arquivos locais** na Steam e selecione a pasta que contém **Dressmaker.exe** e **Dressmaker_Data**, não a própria `Dressmaker_Data` nem a pasta inteira da Steam.
5. Clique em **Instalar tradução**, confirme o destino e espere terminar. Não abra o jogo nem desligue o computador durante a instalação.
6. Abra o jogo e selecione **Português (Brasil)** ou **pt**.

O instalador verifica o pacote e a compatibilidade, cria um backup `PTBR_Backup_*` ao lado de `Dressmaker.exe`, reconstrói o arquivo traduzido a partir do original compatível e confere os cinco arquivos instalados. Se a instalação falhar, tenta restaurar o backup e informa se a restauração também falhar. Não usa sua conta Steam, não acessa seus saves e não se conecta à internet. Não solicita permissão de administrador automaticamente; pastas protegidas podem exigir permissões adequadas.

### Avisos de segurança do Windows

É um **instalador comunitário sem assinatura digital**, não um instalador oficial da Steam ou do Dressmaker. O SmartScreen ou o antivírus podem exibir um aviso. Baixe apenas de uma fonte em que confia; não desative o antivírus nem ignore avisos que não entende. Uma interface gráfica não é garantia de segurança. O código-fonte e as instruções de compilação estão em [installer-source](installer-source/) para quem quiser conferir.

## Por que os pacotes têm tamanhos tão diferentes?

O pacote manual contém o `resources.assets` modificado inteiro. O instalador contém um pequeno patch e os quatro arquivos de localização. Esse patch reconstrói o recurso **completo**, incluindo as modificações de tradução já feitas. O resultado foi comparado byte por byte com o arquivo instalado e com o pacote manual anterior; os outros quatro arquivos também são idênticos. O pacote menor não descarta essas alterações.

## Arquivos alterados

Apenas estes cinco arquivos dentro de `Dressmaker_Data` são substituídos:

- `resources.assets`
- `StreamingAssets/aa/catalog.bin`
- `StreamingAssets/aa/StandaloneWindows64/localization-asset-tables-english(en)_assets_all.bundle`
- `StreamingAssets/aa/StandaloneWindows64/localization-locales_assets_all.bundle`
- `StreamingAssets/aa/StandaloneWindows64/localization-string-tables-english(en)_assets_all.bundle`

Os nomes em inglês são necessários para a estrutura do jogo; **não renomeie os arquivos**. Nenhum dos métodos modifica seus saves. Os pacotes não incluem o jogo completo, executáveis do jogo, credenciais ou backups de desenvolvimento.

## Desinstalar ou restaurar

Feche o jogo e restaure os cinco arquivos do backup para `Dressmaker_Data`, preservando a estrutura das pastas. O backup do instalador já usa essa estrutura. Guarde o **primeiro** backup: uma reinstalação pode criar um backup de arquivos que já estavam traduzidos.

Outra opção é **Verificar integridade dos arquivos** pela Steam, que restaura os arquivos oficiais atuais. Isso também pode remover outros mods, então faça backup deles antes. Atualizações do jogo podem sobrescrever a tradução ou torná-la incompatível; reinstale apenas um pacote compatível.

## Problemas e sugestões

- **Jogo não encontrado:** use Explorar arquivos locais na Steam e escolha a pasta que contém `Dressmaker.exe`.
- **Arquivo incompatível:** não ignore a verificação nem force a instalação manual para contorná-la. Confira se a versão do jogo é compatível com o pacote.
- **Acesso negado:** confira as permissões da pasta; execute com privilégios elevados apenas se necessário e se confiar no pacote.
- **Problema na tradução:** envie uma captura e o contexto da conversa, principalmente para texto sem tradução, gênero incorreto, frase estranha ou texto fora da caixa.

Os testes verificaram integridade dos pacotes, reconstrução do recurso, reinstalação, rejeição de pasta incorreta e de recurso incompatível numa cópia separada. Isso não significa que todas as falas, configurações do Windows, antivírus ou bibliotecas da Steam foram testados.

## Organização e textos editáveis

```text
README.md                     — instruções e apresentação em português
README.en.md                  — versão em inglês
packages/manual/              — ZIP para copiar os arquivos manualmente
packages/installer/           — ZIP com instalador gráfico
installer-source/             — código-fonte e instruções de compilação
translation-source/           — textos e nomes de peças editáveis
```

Os textos editáveis estão em [translation-source](translation-source/). Todos podem baixar, usar, modificar e compartilhar nossas contribuições de tradução, sugerir melhorias ou adaptá-las para outro processo de tradução. Os desenvolvedores do jogo também são bem-vindos a usar e adaptar essas contribuições para integração oficial, **sem precisar pedir autorização à Bianca por elas**.

Os JSONs mostram os textos usados nos pacotes, identificados pelos IDs de localização, e os nomes de peças armazenados fora dessas tabelas. Editar o JSON sozinho não atualiza os arquivos compilados: é necessário importar as alterações num processo de localização e compilação compatível. Veja as orientações na pasta de fontes.

## Projeto comunitário e agradecimentos

Esta tradução é compartilhada abertamente com a comunidade do Dressmaker. Revisada e adaptada por **Bianca**, com melhorias e contribuições de todos bem-vindas. Não é necessário entrar em contato com Bianca para baixar, usar, modificar ou compartilhar as contribuições de tradução disponibilizadas aqui.

**Dressmaker, seus diálogos originais e seus recursos pertencem aos criadores do jogo e aos respectivos titulares. Não reivindicamos propriedade do jogo nem do conteúdo original.** Este projeto é comunitário e não oficial; não há confirmação de apoio oficial ou de incorporação ao jogo. A liberdade de reutilizar nossas contribuições de tradução não pretende conceder direitos sobre o jogo original. Ficaremos felizes se os desenvolvedores aproveitarem essas contribuições no jogo.
