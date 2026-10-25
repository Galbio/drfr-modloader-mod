bool RespectSpriteImportCond(string assetname)
{
	int frame_index = assetname.LastIndexOf("_");

	string framepart = assetname.Substring(frame_index + 1);
	if (!Int32.TryParse(framepart, out int frame))
		return (Data.Sprites.ByName(assetname) != null);

	string sprname = assetname.Substring(0, frame_index);

	UndertaleSprite trySprite = Data.Sprites.ByName(sprname);
	if (trySprite == null)
		return (false);

	bool res = (trySprite.Textures.Count > frame);
	return (res);
}

readonly string SoundFRSuffix = "_fr";
bool RespectSoundImportCond(string soundname)
{
	if (Data.Sounds.ByName(soundname) != null)
		return (true);

	if (!soundname.EndsWith(SoundFRSuffix))
		return (false);

	soundname = soundname.Remove(soundname.Length - SoundFRSuffix.Length, SoundFRSuffix.Length);
	return (Data.Sounds.ByName(soundname) != null);
}

bool RespectFontImportCond(string fontname)
{
	return (Data.Fonts.ByName(fontname) != null);
}

bool RespectBackgroundImportCond(string setname)
{
	return (Data.Backgrounds.ByName(setname) != null);
}

bool RespectImportSuffix(string filepath, string importType)
{
	string searchPattern = "*.png";

	if (importType == "Sounds")
		searchPattern = "*.(?:wav|ogg)";

	searchPattern = "." + searchPattern + "$";
	return (Regex.IsMatch(filepath, searchPattern));
}

bool RespectImportConditions(string filepath, string importType)
{
	if (!RespectImportSuffix(filepath, importType))
		return (false);

	string assetname = Path.GetFileNameWithoutExtension(filepath);
	bool isValid = false;

	switch (importType)
	{
		case "Textures/Sprites":
			isValid = RespectSpriteImportCond(assetname);
			break;

		case "Sounds":
			isValid = RespectSoundImportCond(assetname);
			break;

		case "Fonts":
			isValid = RespectFontImportCond(assetname);
			break;

		case "Textures/Tilesets":
			isValid = RespectBackgroundImportCond(assetname);
			break;

		default:
			Console.WriteLine($"Unknown importType `{importType}'");
			break;
	}

	return (isValid);
}
