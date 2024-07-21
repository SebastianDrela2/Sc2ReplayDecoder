using MPQArchive.MPQ.DecryptedData;
using MPQArchive.MPQ.Hashing;
using MPQArchive.MPQ.ReceivedData;
using MPQArchive.MPQ.Utils;
using System.Text;

namespace MPQArchive.MPQ
{
    public class MPQReader(FileStream stream) : IDisposable
    {
        private const uint MagicHeader = 0x1A_51_50_4D;
        private const uint MagicShunt = 0x1B_51_50_4D;

        private readonly BinaryReader _reader = new BinaryReader(stream, Encoding.UTF8, true);

        private long Position 
        { 
            get => _reader.BaseStream.Position;
            set => _reader.BaseStream.Position = value; 
        }

        public ReceivedData.MPQArchive Read()
        {
            MPQHeader1? mpqHeader = null;
            MPQUserData mpqUserData = null;
            long headerOffset = 0;

            for (long baseOffset = 0; baseOffset < _reader.BaseStream.Length; Position += 0x200)
            {              
                switch (_reader.ReadUInt32())
                {
                    case MagicHeader:
                        mpqHeader = ReadHeader();
                        headerOffset = baseOffset;                      
                        goto label;

                    case MagicShunt:
                        mpqUserData = ReadUserData(ref baseOffset);
                        goto case MagicHeader;                  
                }
            }
            label:

            var encryptionTable = EncryptionTable.CreateNew();
            var encryption = new Encryption(encryptionTable);
            
            var decryptedTableReader = new DecryptedTableReader(encryption, _reader, mpqHeader!);
            var compositeTable = new CompositeTable(decryptedTableReader, headerOffset);

            var mpqHashTableReader = new MPQHashTableReader(encryption, compositeTable.MPQHashTableEntries);
            var mpqFileReader = new MPQFileReader
                (_reader, mpqHashTableReader, mpqHeader!, compositeTable, headerOffset);

            var listingFiles = new ListingFilesReader(mpqFileReader).Read();

            return new ReceivedData.MPQArchive(mpqHeader!, mpqUserData!, listingFiles);
        }

        private MPQHeader1 ReadHeader()
        {
            var mpqHeader = new MPQHeader1()
            {
                HeaderSize = _reader.ReadUInt32(),
                ArchiveSize = _reader.ReadUInt32(),
                FormatVersion = _reader.ReadUInt16(),
                SectorSizeShift = _reader.ReadUInt16(),
                HastTableOffset = _reader.ReadUInt32(),
                BlockTableOffset = _reader.ReadUInt32(),
                HashTableCount = _reader.ReadUInt32(),
                BlockTableCount = _reader.ReadUInt32(),
            };

            if (mpqHeader.FormatVersion >= 1)
            {
                var mpqHeaderExtended = new MPQHeader2()
                {
                    ExtBlockTablePos = _reader.ReadUInt64(),
                    HashTablePosHigh = _reader.ReadUInt16(),
                    BlockTablePosHigh = _reader.ReadUInt16(),
                };

                if (mpqHeaderExtended.BlockTablePosHigh is not 0)
                {
                    throw new NotImplementedException();
                }
            }

            return mpqHeader;
        }

        private MPQUserData ReadUserData(ref long baseOffset)
        {
            // Read the initial header data
            var userDataSize = _reader.ReadUInt32();
            var headerPosition = _reader.ReadUInt32();

            // Create an MPQUserData object with the read values
            var mpqUserData = new MPQUserData
            {
                UserDataSize = userDataSize,
                HeaderPosition = headerPosition
            };

            byte[] content = _reader.ReadBytes((int)userDataSize);

            mpqUserData.Content = content;

            var mpqHeaderPosition = baseOffset + mpqUserData.HeaderPosition;
            _reader.GoTo(mpqHeaderPosition);
            baseOffset = mpqHeaderPosition;
           
            var mpqHeaderMagic = _reader.ReadUInt32();

            if (mpqHeaderMagic != MagicHeader)
            {
                throw new InvalidDataException("Invalid MPQ header magic number.");
            }
            
            return mpqUserData;
        }


        public void Dispose()
        {
            _reader.Dispose();
        }

    }
}
