# Editable community translation

These UTF-8 JSON files expose the texts in the current downloadable translation packages. They are provided openly for community corrections, reuse, sharing and adaptation, including integration by the game's developers. No permission from Bianca is required to reuse this project's translation contributions. No ownership of Dressmaker or its original text and assets is claimed.

- `dialogue.json`: dialogue strings, keyed by localization ID.
- `ui.json`: interface strings, keyed by localization ID.
- `tutorial.json`: tutorial strings, keyed by localization ID.
- `content.json`: fabrics, garment components and other content strings, keyed by localization ID.
- `pattern-labels.json`: component labels embedded in garment definitions, grouped by garment name and class, with variant/panel indices and asset IDs.

These files are exported from the actual release assets, so they include the final resolved text rather than historical backups or duplicate-key draft mappings. Some retained names or strings may remain in English. Preserve formatting tags, escapes and dynamic placeholders when editing; JSON keys are identifiers, not text to translate.

**Editing these JSON files alone does not update the ZIPs or the installed game.** Import changes into the game's localization authoring/build workflow, or an appropriate compatible asset-patching workflow, then rebuild and verify the packages. Pattern labels are stored separately from localization tables and must be handled too. The original authoring tools and original game source are not supplied by this repository.

Community members can propose corrections through issues or pull requests, or adapt the files independently. Developers may map the localization IDs to their original project and incorporate the translation contributions without contacting Bianca for approval. The original game and its content remain the work of their respective creators; this project's reuse invitation concerns its translation contributions, not ownership of the game.
