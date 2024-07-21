using MPQArchive.MPQ.Hashing;
using MPQArchive.MPQ.Utils;
using System.Runtime.InteropServices;

namespace MPQArchive.MPQ.DecryptedData
{
    public class DecryptedTableReader
    {
        private readonly Encryption _encryption;
        private readonly BinaryReader _reader;
        private readonly MPQHeader1 _mpqHeader;

        public DecryptedTableReader(Encryption encryption, BinaryReader reader, MPQHeader1 mpqHeader)
        {
           _encryption = encryption;
           _reader = reader;
           _mpqHeader = mpqHeader;
        }

        public MPQHashTableEntry[] ReadHashTable(long headerOffset)
        {
            var key = _encryption.Hash("(hash table)", "TABLE");
            var data = ReadAndDecryptTable(_mpqHeader.HastTableOffset, _mpqHeader.HashTableCount, key, headerOffset);
            
            var src = MemoryMarshal.Cast<byte, uint>(data);
            var items = new MPQHashTableEntry[_mpqHeader.HashTableCount];

            for (var i = 0; i < _mpqHeader.HashTableCount; i++)
            {
                items[i] = new MPQHashTableEntry
                {
                    HashA = src[i * 4 + 0],    
                    HashB = src[i * 4 + 1],
                    lcLocale = (ushort)(src[i * 4 + 2] & 0xFFFF),
                    Platform = (ushort)(src[i * 4 + 2] >> 16),
                    BlockIndex = src[i * 4 + 3]
                };
            }

            return items;
        }

        public MPQBlockTableEntry[] ReadBlockTable(long headerOffset)
        {
            var key = _encryption.Hash("(block table)", "TABLE");
            var data = ReadAndDecryptTable(_mpqHeader.BlockTableOffset, _mpqHeader.BlockTableCount, key, headerOffset);
           
            var src = MemoryMarshal.Cast<byte, uint>(data);
            var items = new MPQBlockTableEntry[_mpqHeader.BlockTableCount];

            for (var i = 0; i < _mpqHeader.BlockTableCount; i++)
            {
                items[i] = new MPQBlockTableEntry()
                {
                    FilePosition = src[i * 4 + 0],
                    CompressedSize = src[i * 4 + 1],
                    UncompressedSize = src[i * 4 + 2],
                    Flags = src[i * 4 + 3]
                };
            }

            return items;
        }

        private byte[] ReadAndDecryptTable(long tablePosition, uint tableCount, uint key, long headerOffset)
        {
            _reader.GoTo(tablePosition + headerOffset);
            var data = _reader.ReadBytes((int)(tableCount * 16));
            return _encryption.DecryptBlock(data, key);
        }
    }
}
