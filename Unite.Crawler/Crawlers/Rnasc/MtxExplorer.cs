using Unite.Crawler.Models;

namespace Unite.Crawler.Crawlers.Rnasc;

public class MtxExplorer
{
    // Explorer should receive a path to the root directory
    // ../root/genome/donor/sample/rnasc/mtx/matrix.mtx.gz
    // ../root/genome/donor/sample/rnasc/mtx/barcodes.tsv.gz
    // ../root/genome/donor/sample/rnasc/mtx/features.tsv.gz
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
                var dataDirectoryPath = Path.Combine(sampleDirectory.FullName, "rnasc", "mtx");
                if (!Directory.Exists(dataDirectoryPath))
                    continue;

                var dataDirectory = new DirectoryInfo(dataDirectoryPath);
                var matrixFile = dataDirectory.EnumerateFiles("matrix.mtx.gz").FirstOrDefault();
                var barcodesFile = dataDirectory.EnumerateFiles("barcodes.tsv.gz").FirstOrDefault();
                var featuresFile = dataDirectory.EnumerateFiles("features.tsv.gz").FirstOrDefault();

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
