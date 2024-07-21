using System.Runtime.InteropServices;

namespace MPQArchive.MPQ.Hashing
{
    public class Encryption
    {
        private readonly uint[] _encryptionTable;
        private readonly Dictionary<string, int> _hashTypes = new Dictionary<string, int>()
    {
        { "TABLE_OFFSET", 0 },
        { "HASH_A", 1 },
        { "HASH_B", 2 },
        { "TABLE", 3 }
    };

        public Encryption(uint[] encryptionTable)
        {
            _encryptionTable = encryptionTable;
        }

        public uint Hash(string str, string hashType)
        {            
            uint seed1 = 0x7FED7FEDu;
            uint seed2 = 0xEEEEEEEEu;
            
            if (!_hashTypes.TryGetValue(hashType, out int hashTypeId))
            {
                throw new ArgumentException("Invalid hash type", nameof(hashType));
            }

            hashTypeId <<= 8;
            
            foreach (var c in str.ToUpper())
            {
                uint charValue = (uint)c;
                
                if (hashTypeId + charValue >= _encryptionTable.Length)
                {
                    throw new IndexOutOfRangeException("Index exceeds encryption table bounds.");
                }

                var value = _encryptionTable[hashTypeId + charValue];
                seed1 = (value ^ (seed1 + seed2)) & 0xFFFFFFFFu;
                seed2 = (charValue + seed1 + seed2 + (seed2 << 5) + 3) & 0xFFFFFFFFu;
            }

            return seed1;
        }

        public byte[] DecryptBlock(byte[] block, uint key)
        {
            uint seed1 = key;
            uint seed2 = 0xEEEEEEEEu;
            int numElements = block.Length / 4;
           
            if (block.Length % 4 != 0)
            {
                throw new ArgumentException("Block length is not a multiple of 4 bytes.");
            }

            using (var result = new MemoryStream())
            {
                var u32Data = MemoryMarshal.Cast<byte, uint>(block);

                for (int i = 0; i < numElements; i++)
                {
                    int idx = 0x400 + (int)(seed1 & 0xFF);
                    
                    if (idx >= _encryptionTable.Length)
                    {
                        throw new IndexOutOfRangeException("Index exceeds encryption table bounds.");
                    }

                    seed2 += _encryptionTable[idx];
                    seed2 &= 0xFFFFFFFFu;

                    uint value = u32Data[i];
                    value = (value ^ (seed1 + seed2)) & 0xFFFFFFFFu;

                    seed1 = ((~seed1 << 0x15) + 0x11111111u) | (seed1 >> 0x0B);
                    seed1 &= 0xFFFFFFFFu;

                    seed2 = (value + seed2 + (seed2 << 5) + 3) & 0xFFFFFFFFu;

                    result.Write(BitConverter.GetBytes(value), 0, 4);
                }

                return result.ToArray();
            }
        }
    }
    /*
     *  //while(length-- > 0)
            //{ 
            //    seed += stormBuffer[0x400 + (key & 0xFF)];
            //    ch = *castBlock ^ (key + seed);

            //    key = ((~key << 0x15) + 0x11111111) | (key >> 0x0B);
            //    seed = ch + seed + (seed << 5) + 3;
            //    *castBlock++ = ch; 
            //} 

     *  def _decrypt(self, data, key):
        """Decrypt hash or block table or a sector."""
        seed1 = key
        seed2 = 0xEEEEEEEE
        result = BytesIO()

        for i in range(len(data) // 4):
            seed2 += self.encryption_table[0x400 + (seed1 & 0xFF)]
            seed2 &= 0xFFFFFFFF
            value = struct.unpack("<I", data[i*4:i*4+4])[0]
            value = (value ^ (seed1 + seed2)) & 0xFFFFFFFF

            seed1 = ((~seed1 << 0x15) + 0x11111111) | (seed1 >> 0x0B)
            seed1 &= 0xFFFFFFFF
            seed2 = value + seed2 + (seed2 << 5) + 3 & 0xFFFFFFFF

            result.write(struct.pack("<I", value))

        return result.getvalue()

     * def _hash(self, string, hash_type):
        """Hash a string using MPQ's hash function."""
        hash_types = {
            'TABLE_OFFSET': 0,
            'HASH_A': 1,
            'HASH_B': 2,
            'TABLE': 3
        }
        seed1 = 0x7FED7FED
        seed2 = 0xEEEEEEEE

        for ch in string.upper():
            if not isinstance(ch, int): ch = ord(ch)
            value = self.encryption_table[(hash_types[hash_type] << 8) + ch]
            seed1 = (value ^ (seed1 + seed2)) & 0xFFFFFFFF
            seed2 = ch + seed1 + seed2 + (seed2 << 5) + 3 & 0xFFFFFFFF

        return seed1
     */
}
