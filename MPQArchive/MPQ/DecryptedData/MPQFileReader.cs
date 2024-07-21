using MPQArchive.MPQ.Constants;
using MPQArchive.MPQ.Utils;
using ICSharpCode.SharpZipLib.BZip2;
using System.IO.Compression;

namespace MPQArchive.MPQ.DecryptedData
{
    public class MPQFileReader(
        BinaryReader reader,
        MPQHashTableReader mpqHashTableReader, 
        MPQHeader1 mpqHeader,        
        CompositeTable compositeTable,
        long headerBaseOffset)
    {
        public byte[] ReadFile(string fileName, bool forceDecompress = false)
        {
            var mpqHashEntry = mpqHashTableReader.GetHashTableEntry(fileName);

            if (mpqHashEntry is null)
            {
                throw new InvalidDataException("File not found in hash table.");
            }

            var mpqBlockEntry = compositeTable.MPQBlockTableEntries[mpqHashEntry.BlockIndex];

            if ((mpqBlockEntry.Flags & MPQFileConstant.MPQ_FILE_EXISTS) == 0)
            {
                throw new InvalidDataException("File does not exist.");
            }

            var offset = mpqBlockEntry.FilePosition + headerBaseOffset;

            reader.GoTo(offset);
            var fileData = reader.ReadBytes((int)mpqBlockEntry.CompressedSize);

            if ((mpqBlockEntry.Flags & MPQFileConstant.MPQ_FILE_ENCRYPTED) != 0)
            {
                throw new NotSupportedException("Encryption is not supported.");
            }

            if ((mpqBlockEntry.Flags & MPQFileConstant.MPQ_FILE_SINGLE_UNIT) == 0 
                && (mpqBlockEntry.CompressedSize is not 0 
                || mpqBlockEntry.UncompressedSize is not 0))
            {
                var sectorSize = 512 << mpqHeader.SectorSizeShift;
                var sectors = (int)(mpqBlockEntry.UncompressedSize / sectorSize) + 1;

                bool crc = false;

                if ((mpqBlockEntry.Flags & MPQFileConstant.MPQ_FILE_SECTOR_CRC) == 0)
                {
                    crc = true;
                    sectors += 1;
                }

                var positions = UnpackPositions(fileData, sectors);
                fileData = ProcessSectors(fileData, mpqBlockEntry, positions, crc, forceDecompress);
            }
            else if ((mpqBlockEntry.Flags & MPQFileConstant.MPQ_FILE_COMPRESS) != 0
                && (forceDecompress || mpqBlockEntry.UncompressedSize > mpqBlockEntry.CompressedSize))
            {
                fileData = Decompress(fileData);
            }
            
            return fileData;
        }

        public static byte[] ProcessSectors(byte[] fileData, MPQBlockTableEntry blockEntry, uint[] positions, bool crc, bool forceDecompress)
        {
            var result = new MemoryStream();
            int sectorBytesLeft = (int)blockEntry.UncompressedSize;

            for (int i = 0; i < positions.Length - (crc ? 2 : 1); i++)
            {
                int start = (int)positions[i];
                int end = (int)positions[i + 1];
                byte[] sector = fileData.Skip(start).Take(end - start).ToArray();

                if ((blockEntry.Flags & MPQFileConstant.MPQ_FILE_COMPRESS) is not 0 &&
                    (forceDecompress || sectorBytesLeft > sector.Length))
                {
                    sector = Decompress(sector);
                }

                sectorBytesLeft -= sector.Length;
                result.Write(sector, 0, sector.Length);
            }
           
            result.Position = 0;

            return result.ToArray();
        }

        private static byte[] Decompress(byte[] data)
        {
            if (data.Length == 0)
            {
                return data;
            } 
            
            var compressionType = data[0];
            
            using (var outputStream = new MemoryStream())
            {
                using (var inputStream = new MemoryStream(data, 1, data.Length - 1))
                {
                    switch (compressionType)
                    {
                        case 0:                        
                            inputStream.CopyTo(outputStream);
                            break;

                        case 2:                           
                            using (var decompressionStream = new DeflateStream(inputStream, CompressionMode.Decompress))
                            {
                                decompressionStream.CopyTo(outputStream);
                            }
                            break;

                        case 16:                            
                            using (var decompressionStream = new BZip2InputStream(inputStream))
                            {
                                decompressionStream.CopyTo(outputStream);
                            }
                            break;

                        default:
                            throw new InvalidOperationException("Unsupported compression type.");
                    }
                }

                return outputStream.ToArray();
            }
        }

        private uint[] UnpackPositions(byte[] fileData, int sectors)
        {
            int count = sectors + 1;
            uint[] positions = new uint[count];

            using (var stream = new MemoryStream(fileData, 0, 4 * count))
            using (var reader = new BinaryReader(stream))
            {
                for (int i = 0; i < count; i++)
                {
                    positions[i] = reader.ReadUInt32();
                }
            }

            return positions;
        }
    }
}
