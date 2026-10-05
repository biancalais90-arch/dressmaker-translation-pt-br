# Dressmaker — Brazilian Portuguese Translation

[Português (Brasil)](README.md) | **English**

Unofficial Brazilian Portuguese (PT-BR) translation for the Windows Steam version of **Dressmaker**, reviewed and adapted by **Bianca**. Includes dialogue, interface text, tutorials, fabric names and sewing-pattern labels.

## Choose how to install

Use **one** method, not both. Both packages produce the same translated files.

| Option | Download | Who it is for |
| --- | --- | --- |
| Manual installation | [Dressmaker_PTBR_Manual.zip](packages/manual/Dressmaker_PTBR_Manual.zip) | People who prefer inspecting and copying files without running an installer. |
| Graphical installer | [Dressmaker_PTBR_Installer.zip](packages/installer/Dressmaker_PTBR_Installer.zip) | People who prefer automatic Steam-library detection, backups and compatibility checks. |

On GitHub, open the chosen ZIP and use **Download raw file**. Do not download the whole repository just to install. The installer ZIP contains an executable and instructions; the manual ZIP contains the ready-to-copy game files and instructions. The installer embeds its payload, so it does not need the manual ZIP alongside it.

## Requirements and compatibility

You need a **Windows Steam installation of Dressmaker**. These packages were checked against the locally installed files on **October 4, 2026**. The installer checks the resource revision internally before applying changes. Compatibility with future updates, other platforms or other mods has not been verified.

**Back up all five files before replacing them.** The manual package replaces the complete `resources.assets`: this can overwrite other mods or modifications. Do not use it with an incompatible game revision. The installer is more conservative: it refuses an independently modified or incompatible resource instead of attempting to merge it. The installer requires .NET Framework 4.5 or later, normally available on modern Windows installations.

## Option A — Manual installation (no executable)

1. Close Dressmaker.
2. In Steam, right-click Dressmaker → **Manage → Browse local files**. This opens the correct game directory regardless of the drive or Steam-library location. Confirm it contains **Dressmaker.exe** and **Dressmaker_Data**.
3. Before installing, make a backup of the five files listed under **Files changed** below, preserving their paths. Keep the backup outside the files being replaced.
4. Extract **Dressmaker_PTBR_Manual.zip** to a separate folder first. Inside, you will find `Dressmaker_Data` and this README.
5. Copy the extracted **Dressmaker_Data** folder into the game directory containing `Dressmaker.exe`. **Merge the folders** and confirm replacement of the five files. Do not delete your existing `Dressmaker_Data`, and do not place another `Dressmaker_Data` folder inside it.
6. Open Dressmaker and select **Português (Brasil)**, which may appear as **pt**, in the language settings.

Correct final layout:

```text
Your chosen Steam library/.../Dressmaker/
├── Dressmaker.exe                    (already present; not supplied)
└── Dressmaker_Data/
    ├── resources.assets
    └── StreamingAssets/aa/
        ├── catalog.bin
        └── StandaloneWindows64/
            ├── localization-asset-tables-english(en)_assets_all.bundle
            ├── localization-locales_assets_all.bundle
            └── localization-string-tables-english(en)_assets_all.bundle
```

The manual method does not run a compatibility check or create a backup automatically. It includes the full modified `resources.assets` because copying a small delta file alone cannot apply the changes.

## Option B — Graphical installer

1. Close Dressmaker and extract **Dressmaker_PTBR_Installer.zip** into a separate folder, not the game directory.
2. Double-click **Dressmaker-PTBR-Setup.exe**. No PowerShell commands, Python or additional patching utilities are needed. The interface is in Brazilian Portuguese.
3. The installer searches Steam's registered installation and `libraryfolders.vdf`, including libraries on other drives, and displays a discovered game folder. **Confirm the path**, especially if you have several installations.
4. If detection fails or the path is wrong, click **Selecionar...**. Use Steam's **Manage → Browse local files** to locate the directory containing **Dressmaker.exe** and **Dressmaker_Data**. Select that directory, not `Dressmaker_Data` and not the whole Steam folder.
5. Click **Instalar tradução**, confirm the destination and wait for completion. Do not launch the game or shut down the computer during installation.
6. Open the game and select **Português (Brasil)** or **pt**.

The installer validates the package and the resource revision, creates a timestamped `PTBR_Backup_*` directory next to `Dressmaker.exe`, reconstructs the translated resource from your compatible original file, and verifies the five installed files. On installation failure it attempts to restore the backup and reports a restoration failure if one occurs. It does not use your Steam account, access your saves or connect to the internet. No administrator privileges are requested automatically; protected folders may require you to run it with appropriate permissions.

### Windows security notices

The executable is an **unsigned community installer**, not an official Steam or Dressmaker installer. Windows SmartScreen or antivirus software may show a warning. Download only from a source you trust; do not disable antivirus or bypass warnings you do not understand. A graphical interface is not a security guarantee. The source is available under [installer-source](installer-source/) for inspection; build instructions are provided there.

## Why are the package sizes different?

The manual package contains the entire modified `resources.assets`. The installer instead embeds a small binary delta plus the four localization files. The delta reconstructs the **complete** resource, including existing translation modifications. Reconstruction was compared byte-for-byte with the installed resource and previous manual package; the four localization files are also identical. The smaller package does not omit these modifications.

## Files changed

Only these five paths beneath `Dressmaker_Data` are replaced:

- `resources.assets`
- `StreamingAssets/aa/catalog.bin`
- `StreamingAssets/aa/StandaloneWindows64/localization-asset-tables-english(en)_assets_all.bundle`
- `StreamingAssets/aa/StandaloneWindows64/localization-locales_assets_all.bundle`
- `StreamingAssets/aa/StandaloneWindows64/localization-string-tables-english(en)_assets_all.bundle`

English-named asset paths are intentional; do not rename them. Neither method modifies saves. Packages do not contain a complete game installation, game executables, credentials or development backups.

## Uninstall / restore

Close Dressmaker and restore all five backed-up files into `Dressmaker_Data`, preserving the directory structure. Installer backups already have this layout. Keep your **first** backup: repeat installations may back up an already-translated version.

Alternatively, use Steam's **Verify integrity of game files** to restore current official files. This can remove other mods too; back them up first. Game updates may overwrite the translation or make it incompatible. Reinstall only a compatible package.

## Troubleshooting and feedback

- **Game not found:** use Steam's Browse local files and select the folder containing `Dressmaker.exe`.
- **Incompatible resource:** do not bypass the installer check or force-copy the manual package to work around it. Confirm that your game revision matches this release.
- **Access denied:** check game-folder permissions; use elevated privileges only when necessary and only if you trust the package.
- **Translation issue:** report a screenshot and surrounding dialogue for untranslated text, incorrect gender, awkward phrasing or overflowing text.

Checks cover package integrity, resource reconstruction, repeat installation, incorrect-folder rejection and incompatible-resource rejection on a separate test copy. They do not establish testing of every gameplay path, Windows configuration, antivirus or Steam-library layout.

## Repository layout and developer integration

```text
README.md                     — Portuguese installation instructions and project information
README.en.md                  — English installation instructions and project information
packages/manual/              — ready-to-copy manual ZIP
packages/installer/           — graphical-installer ZIP
installer-source/             — installer source and build instructions
translation-source/           — editable translation text and sewing-pattern labels
```

The editable texts are available in [translation-source](translation-source/). Anyone is welcome to download, use, modify and share the community translation, contribute improvements or adapt it for another translation workflow. The game's developers are welcome to use and adapt the translation for official integration without needing to request permission from Bianca for this project's translation contributions.

The JSON files expose the texts currently shipped in the packages, keyed by localization IDs, plus sewing-pattern labels stored outside the localization tables. Editing JSON alone does not update the compiled game files; it must be imported into a compatible localization/build workflow. See the source-folder README for details.

## Community project and acknowledgements

This translation is shared openly for the Dressmaker community. Reviewed and adapted by **Bianca**, with improvements and contributions welcome from everyone. You do not need to contact Bianca to download, use, modify or share the translation contributions offered here.

**Dressmaker, its original dialogue and assets belong to the game's creators and respective rights holders. No ownership of the game or its original content is claimed.** This is an unofficial community project; official endorsement or incorporation into the game has not been confirmed. The permission to reuse this project's translation contributions does not purport to grant rights over the original game. We would be happy to see the developers adapt these contributions for the game.
