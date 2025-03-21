using Unite.Crawler.Models;

namespace Unite.Crawler.Crawlers.Rnasc;

public class MtxExplorer
{
    // Explorer should receive a path to the root directory
    // ../root/genome/rnasc/donor/sample/mtx/matrix.mtx.gz
    // ../root/genome/rnasc/donor/sample/mtx/barcodes.tsv.gz
    // ../root/genome/rnasc/donor/sample/mtx/features.tsv.gz
    public static IEnumerable<FileMetadata> Explore(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory '{path}' not found.");

        var dataDirectoryPath = Path.Combine(path, "genome", "rnasc");
        if (!Directory.Exists(dataDirectoryPath))
            yield break;

        var dataDirectory = new DirectoryInfo(dataDirectoryPath);

        foreach (var donorDirectory in dataDirectory.EnumerateDirectories())
        {
            foreach (var sampleDirectory in donorDirectory.EnumerateDirectories())
            {
                foreach (var formatDirectory in sampleDirectory.EnumerateDirectories())
                {
                    var matrixFile = formatDirectory.EnumerateFiles("matrix.mtx.gz").FirstOrDefault();
                    var barcodesFile = formatDirectory.EnumerateFiles("barcodes.tsv.gz").FirstOrDefault();
                    var featuresFile = formatDirectory.EnumerateFiles("features.tsv.gz").FirstOrDefault();

                    if (matrixFile != null && barcodesFile != null && featuresFile != null)
                    {
                        yield return new FileMetadata
                        {
                            Name = matrixFile.Name,
                            Reader = "cmd/meta",
                            Format = "mtx",
                            Archive = "gz",
                            Path = matrixFile.FullName
                        };

                        yield return new FileMetadata
                        {
                            Name = barcodesFile.Name,
                            Reader = "cmd/meta",
                            Format = "tsv",
                            Archive = "gz",
                            Path = barcodesFile.FullName
                        };

                        yield return new FileMetadata
                        {
                            Name = featuresFile.Name,
                            Reader = "cmd/meta",
                            Format = "tsv",
                            Archive = "gz",
                            Path = featuresFile.FullName
                        };
                    }
                }
            }
        }
    }
}
