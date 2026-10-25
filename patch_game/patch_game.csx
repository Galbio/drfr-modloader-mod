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

List<string>fnt_list = FetchAssets("Fonts", currentChapter);
if (!import_asset_type("IMPORT_JAFONTS")) {
	for (int i = 0; i < fnt_list.Count; i++) {
		string fnt_name = fnt_list[i];

		if (!fnt_name.Contains("fnt_ja_"))
			continue;

		fnt_list.RemoveAt(i--);
	}
}
FontPatcher.importFontList(fnt_list);

if (import_asset_type("IMPORT_SOUNDS"))
	AudioPatcher.importSoundList(FetchAssets("Sounds", currentChapter));

if (import_asset_type("IMPORT_TEXTURES")) {
	TilesetPatcher.importTilesetList(FetchAssets("Textures/Tilesets", currentChapter));
	SpritePatcher.importSpriteList(FetchAssets("Textures/Sprites", currentChapter));
}

Console.WriteLine("\n");

bool import_asset_type(string key)
{
	string val = Environment.GetEnvironmentVariable(key);

	return (val != "0");
}
