using Unite.Crawler.Models;

namespace Unite.Crawler.Crawlers.Meth;

public class IdatExplorer
{
    // Explorer should receive a path to the root directory
    // ../root/genome/meth/donor/sample/idat/*Grn.idat
    // ../root/genome/meth/donor/sample/idat/*Red.idat
    public static IEnumerable<FileMetadata> Explore(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory '{path}' not found.");

        var analysisDirectoryPath = Path.Combine(path, "genome", "meth");
        if (!Directory.Exists(analysisDirectoryPath))
            yield break;

        var analysisDirectory = new DirectoryInfo(analysisDirectoryPath);

        foreach (var donorDirectory in analysisDirectory.EnumerateDirectories())
        {
            foreach (var sampleDirectory in donorDirectory.EnumerateDirectories())
            {
                foreach (var formatDirectory in sampleDirectory.EnumerateDirectories())
                {
                    var redIdatFile = formatDirectory.EnumerateFiles("*Red.idat").FirstOrDefault();
                    var grnIdatFile = formatDirectory.EnumerateFiles("*Grn.idat").FirstOrDefault();

                    if (redIdatFile != null && grnIdatFile != null)
                    {
                        yield return new FileMetadata
                        {
                            Name = redIdatFile.Name,
                            Reader = "cmd/meta",
                            Format = "idat",
                            Archive = "none",
                            Path = redIdatFile.FullName
                        };

                        yield return new FileMetadata
                        {
                            Name = grnIdatFile.Name,
                            Reader = "cmd/meta",
                            Format = "idat",
                            Archive = "none",
                            Path = grnIdatFile.FullName
                        };
                    }
                }
            }
        }
    }
}
