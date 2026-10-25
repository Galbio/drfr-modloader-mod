string fr_file = "strings_fr.txt";
string en_file = "strings_en.txt";
string file_dir = $"chapitre-{currentChapter}";

string[] fr_lines = File.ReadAllLines(PatcherUtils.getFilePath($"{file_dir}/{fr_file}"));
string[] en_lines = File.ReadAllLines(PatcherUtils.getFilePath($"{file_dir}/{en_file}"));

Hashtable trad_table = new Hashtable();
for (int i = 0; i < fr_lines.Length; i++) {
    en_lines[i] = en_lines[i].Replace("[RETOUR A LA LIGNE]", "\n");
    fr_lines[i] = fr_lines[i].Replace("[RETOUR A LA LIGNE]", "\n");
    trad_table.Add(en_lines[i], fr_lines[i]);
}

foreach (UndertaleString str in Data.Strings) {
    if (trad_table.ContainsKey(str.Content)) {
        str.Content = trad_table[str.Content].ToString();
    }
}

FontPatcher.importFontList(FetchAssets("Fonts", currentChapter));
AudioPatcher.importSoundList(FetchAssets("Sounds", currentChapter));
TilesetPatcher.importTilesetList(FetchAssets("Textures/Tilesets", currentChapter));
SpritePatcher.importSpriteList(FetchAssets("Textures/Sprites", currentChapter));
Console.WriteLine("\n");
