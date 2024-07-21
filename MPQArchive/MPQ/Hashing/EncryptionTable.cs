using System;

namespace MPQArchive.MPQ.Hashing
{
    public static class EncryptionTable
    {
        public static uint[] CreateNew()
        {
            var seed = 0x00100001;
            var encryptionTableSize = 256 * 5;
            var encryptionTable = new uint[encryptionTableSize];

            for (var i = 0; i < 256 ; i++)
            {
                var index = i;
                for (var j = 0; j < 5; j++)
                {
                    seed = (seed * 125 + 3) % 0x2AAAAB;
                    var temp1 = (uint)(seed & 0xFFFF) << 0x10;

                    seed = (seed * 125 + 3) % 0x2AAAAB;
                    var temp2 = (uint)(seed & 0xFFFF);

                    encryptionTable[index] = (temp1 | temp2);

                    index += 256;
                }
            }

            return encryptionTable;
        }
    }

    /*
     def _prepare_encryption_table():
        """Prepare encryption table for MPQ hash function."""
        seed = 0x00100001
        crypt_table = {}

        for i in range(256):
            index = i
            for j in range(5):
                seed = (seed * 125 + 3) % 0x2AAAAB
                temp1 = (seed & 0xFFFF) << 0x10

                seed = (seed * 125 + 3) % 0x2AAAAB
                temp2 = (seed & 0xFFFF)

                crypt_table[index] = (temp1 | temp2)

                index += 0x100

        return crypt_table

    encryption_table = _prepare_encryption_table()
     */
}