# Unite Dev Crawler
Data crawler and readers for local development.

## Build
To build the project [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) is required.

Run publishing script for corresponding platform:
- Linux x64: `sh publish-linux-x64.sh`
- OSX x64: `sh publish-osx-x64.sh`
- OSX ARM64: `sh publish-osx-arm64.sh`

Published binaries for corresponding paltform will be located in `publish` directory.  
Binaries are self contained and do not require any runtime to work.

## Crawler
Crawler requires standard directories structure to work:
```txt
├─root
  ├─genome #data feed
    ├─donor #donor id
      ├─sample #sample id
        ├─rnasc
          ├─mtx
            ├─matrix.mtx.gz
            ├─barcodes.tsv.gz
            ├─features.tsv.gz
            ├─meta.tsv
        ├─meth
          ├─idat
            ├─*Red.idat
            ├─*Grn.idat
            ├─meta.tsv
```

To make it explore the data of required type, specify the type and absolute path to the root directory:
```sh
./crawler rnasc-exp /path/to/root
```

### Supported types
- `meth` - Methylation array data in `idat` format.
- `rnasc-exp` - scRNASeq count matrix in `mtx` format.

## Readers
There is only one standard reader is available for any kind of data - `meta` reader.  
It reads metadata from `meta.tsv` file located in the directory of the data type.  
Metadata in the file should be in the format required by corresponding data feed api.

To read metadata of the file specify absolute path to the file:
```sh
./meta /path/to/meta.tsv
```
