namespace s2ProtocolFurry.Decoder
{
    public class BitPackedBuffer
    {
        private readonly byte[] _data;
        private int _bitPosition;
        private int _bytePosition;

        public BitPackedBuffer(byte[] data)
        {
            _data = data;
            _bitPosition = 0;
            _bytePosition = 0;
        }

        public void ByteAlign()
        {
            _bitPosition = ((_bitPosition + 7) / 8) * 8;
            _bytePosition = _bitPosition / 8;
        }

        public bool Done() => _bytePosition >= _data.Length;

        public int UsedBits() => _bitPosition;

        public int ReadBits(int count)
        {
            if (count > 32 || count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            int result = 0;

            while (count > 0)
            {
                int availableBitsInByte = Math.Min(8 - (_bitPosition % 8), count);
                byte currentByte = _data[_bytePosition];
                int bitsToRead = (currentByte >> (8 - availableBitsInByte)) & ((1 << availableBitsInByte) - 1);
                result = (result << availableBitsInByte) | bitsToRead;
                _bitPosition += availableBitsInByte;
                count -= availableBitsInByte;
                _bytePosition = _bitPosition / 8;
            }

            return result;
        }

        public byte[] ReadAlignedBytes(int count)
        {
            ByteAlign();
            byte[] bytes = _data.Skip(_bytePosition).Take(count).ToArray();
            _bytePosition += count;
            _bitPosition = _bytePosition * 8;
            return bytes;
        }
    }
}
