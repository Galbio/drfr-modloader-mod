void AddCommonAssets(List<string> sources, string assetFolder, string importType, char chapitre)
{
	string importFolder = Path.Combine(assetFolder, importType);

	foreach (string file in Directory.EnumerateFiles(importFolder, "*.*", SearchOption.AllDirectories))
	{
		if (!RespectImportSuffix(file, importType))
			continue;

		if (!RespectImportConditions(file, importType))
			continue;

		sources.Add(file);
	}
}

void AddChapterAssets(List<string> sources, string assetFolder, string importType)
{
	string importFolder = Path.Combine(assetFolder, importType);
	
	if (!Directory.Exists(importFolder))
		return;

	string[] dirFiles = Directory.GetFiles(importFolder);
	foreach (string file in dirFiles) {
		if (!RespectImportSuffix(file, importType))
			continue;

		sources.Add(file);
	}
}

List<string> FetchAssets(string importType, int chap)
{
	List<string> sources = new();
	char chapitre = (char)(chap + '0');

	string commonDir = PatcherUtils.getFilePath("assets");
	string chapterDir = PatcherUtils.getFilePath($"chapitre-{chapitre}");

	AddCommonAssets(sources, commonDir, importType, chapitre);
	AddChapterAssets(sources, chapterDir, importType);

	Console.WriteLine($"[{importType}] - Fetched {sources.Count} elements");
	return (sources);
}
