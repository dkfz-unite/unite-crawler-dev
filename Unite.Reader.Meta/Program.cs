if (args.Length != 1)
{
    Console.Error.WriteLine($"Requires 1 argument: <path>.");
    return;
}

var path = args[0];
if (!File.Exists(path))
{
    Console.Error.WriteLine($"File '{path}' not found.");
    return;
}

var folderPath = Path.GetDirectoryName(path);
var metaPath = Path.Combine(folderPath, "meta.tsv");
var meta = File.ReadAllText(metaPath);

Console.WriteLine(meta);
