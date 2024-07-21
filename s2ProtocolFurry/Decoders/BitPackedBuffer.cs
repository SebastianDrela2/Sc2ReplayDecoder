namespace s2ProtocolFurry.Decoder
{
    public class BitPackedBuffer
    {
        private readonly byte[] _data;
        private int _used;      // Index of the next byte to be read
        private int _nextBits; // Number of bits remaining in the current byte
        private byte _next;    // Current byte being read
        private readonly bool _bigEndian;

        public BitPackedBuffer(byte[] data, string endian = "big")
        {
            _data = data ?? Array.Empty<byte>();
            _used = 0;
            _nextBits = 0;
            _next = 0;
            _bigEndian = endian == "big";
        }

        public override string ToString()
        {
            var s = _used < _data.Length ? _data[_used].ToString("X2") : "--";
            return $"buffer({(_nextBits > 0 ? _next : 0):X2}/{_nextBits},[{_used}]={s})";
        }

        public bool Done()
        {
            return _nextBits == 0 && _used >= _data.Length;
        }

        public int UsedBits()
        {
            return _used * 8 - _nextBits;
        }

        public void ByteAlign()
        {
            _nextBits = 0;
        }

        public byte[] ReadAlignedBytes(int count)
        {
            ByteAlign();
            if (_used + count > _data.Length)
            {
                throw new TruncatedError();
            }
            var result = _data.Skip(_used).Take(count).ToArray();
            _used += count;
            return result;
        }

        public int ReadBits(int bits)
        {
            int result = 0;
            int resultbits = 0;

            while (resultbits != bits)
            {
                if (_nextBits == 0)
                {
                    if (Done())
                    {
                        throw new TruncatedError();
                    }

                    _next = _data[_used];
                    _used += 1;
                    _nextBits = 8;
                }
                int copybits = Math.Min(bits - resultbits, _nextBits);
                int copy = (_next & ((1 << copybits) - 1));
                if (_bigEndian)
                {
                    result |= copy << (bits - resultbits - copybits);
                }
                else
                {
                    result |= copy << resultbits;
                }
                _next >>= copybits;
                _nextBits -= copybits;
                resultbits += copybits;
            }

            return result;
        }

        public byte[] ReadUnalignedBytes(int count)
        {
            var result = new List<byte>();

            for (int i = 0; i < count; i++)
            {
                result.Add((byte)ReadBits(8));
            }

            return result.ToArray();
        }
    }

    public class TruncatedError : Exception
    {
        public TruncatedError() : base("Data was truncated.")
        {
        }
    }
}
