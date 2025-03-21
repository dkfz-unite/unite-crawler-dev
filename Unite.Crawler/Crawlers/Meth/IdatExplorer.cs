using Unite.Crawler.Models;

namespace Unite.Crawler.Crawlers.Meth;

public class IdatExplorer
{
    // Explorer should receive a path to the root directory
    // ../root/genome/donor/sample/meth/*Grn.idat
    // ../root/genome/donor/sample/meth/*Red.idat
    public static IEnumerable<FileMetadata> Explore(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory '{path}' not found.");

        var genomeDirectoryPath = Path.Combine(path, "genome");
        if (!Directory.Exists(genomeDirectoryPath))
            yield break;

        var genomeDirectory = new DirectoryInfo(genomeDirectoryPath);
        foreach (var donorDirectory in genomeDirectory.EnumerateDirectories())
        {
            foreach (var sampleDirectory in donorDirectory.EnumerateDirectories())
            {
                var dataDirectoryPath = Path.Combine(sampleDirectory.FullName, "meth");
                if (!Directory.Exists(dataDirectoryPath))
                    continue;

                var dataDirectory = new DirectoryInfo(dataDirectoryPath);
                var redIdatFile = dataDirectory.EnumerateFiles("*Red.idat").FirstOrDefault();
                var grnIdatFile = dataDirectory.EnumerateFiles("*Grn.idat").FirstOrDefault();

                if (redIdatFile != null && grnIdatFile != null)
                {
                    yield return new FileMetadata
                    {
                        Name = redIdatFile.Name,
                        Reader = "cmd/meta",
                        Format = "idat",
                        Path = redIdatFile.FullName
                    };

                    yield return new FileMetadata
                    {
                        Name = grnIdatFile.Name,
                        Reader = "cmd/meta",
                        Format = "idat",
                        Path = grnIdatFile.FullName
                    };
                }
            }
        }
    }
}
