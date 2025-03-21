// "dna", "dna-ssm", "dna-cnv", "dna-sv", "meth", "meth-lvl", "rna", "rna-exp", "rnasc", "rnasc-exp",
using Unite.Essentials.Tsv;

var types = new string[] { "meth", "rnasc-exp" };

if (args.Length != 2)
{
    Console.Error.WriteLine($"Requires 2 arguments: <type({string.Join('|', types)})> <path>.");
    return;
}

var type = args[0]?.Trim().ToLower();
if (!types.Contains(type))
{
    Console.Error.WriteLine($"Type '{type}' is not supported.");
    return;
}

var path = args[1];
if (!Directory.Exists(path))
{
    Console.Error.WriteLine($"Directory '{path}' not found.");
    return;
}


if (type.Equals(types[0]))
{
    // meth
    var files = Unite.Crawler.Crawlers.Meth.IdatExplorer.Explore(path).ToArray();
    var tsv = TsvWriter.Write(files);
    Console.Write(tsv);
}
else if (type.Equals(types[1]))
{
    // rnasc-exp
    var files = Unite.Crawler.Crawlers.Rnasc.MtxExplorer.Explore(path).ToArray();
    var tsv = TsvWriter.Write(files);
    Console.Write(tsv);
}
