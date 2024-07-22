using System.Text;

namespace s2ProtocolFurry.Decoder
{
    public class BitPackedBuffer
    {
        private readonly byte[] _data;
        private int _used;
        private int _next;
        private int _nextBits;
        private readonly bool _bigEndian;

        public required DebugLogger DebugOutput { get; init; }
        public string DebugData
        {
            get
            {
                StringBuilder sb = new();
                sb.AppendLine($"[{' ',8}] |{0,8}|{1,8}|{2,8}|{3,8}|{4,8}|{5,8}|{6,8}|{7,8}|");
                for (int i = 0; ;)
                {
                    sb.Append($"[{i,8}]  |");
                    for (int x = 0; ; )
                    {
                        if (i >= _data.Length)
                        {
                            sb.AppendLine();
                            return sb.ToString();
                        }
                        sb.Append($"{Convert.ToString(_data[i], 2).PadLeft(8, '0')}");
                        ++i;
                        ++x;
                        if (x >= 8) break;
                        sb.Append(" ");
                    }
                    sb.Append("|");
                    sb.AppendLine();
                }
            }
        }

        public BitPackedBuffer(byte[] data, string endian = "big")
        {
            _data = data ?? Array.Empty<byte>();
            _used = 0;
            _next = 0;
            _nextBits = 0;
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
            DebugOutput.AppendLine($"ByteAlign ({_nextBits} > 0)");
            _nextBits = 0;
        }

        public byte[] ReadAlignedBytes(int bytes)
        {
            DebugOutput.AppendLine($"{nameof(ReadAlignedBytes)}({nameof(bytes)}: {bytes})");
            using var guard = DebugOutput.PushIndent();

            ByteAlign();
            var data = _data.AsSpan(_used, bytes);
            _used += bytes;

            if (data.Length != bytes)
            {
                throw new TruncatedError();
            }
            return data.ToArray();
        }

        public int ReadBits(int bits)
        {
            DebugOutput.Append($"{nameof(ReadBits)}({nameof(bits)}: {bits})");
            using var guard = DebugOutput.PushIndent();

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
                int copy = _next & ((1 << copybits) - 1);
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

            DebugOutput.AppendLine($" = {result} // {nameof(_used)} = {_used}, {nameof(_next)} = {_next}, {nameof(_nextBits)} = {_nextBits}");
            return result;
        }

        public byte[] ReadUnalignedBytes(int count)
        {
            DebugOutput.AppendLine($"{nameof(ReadUnalignedBytes)}({nameof(count)}: {count})");
            using var guard = DebugOutput.PushIndent();

            var result = new byte[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = (byte)ReadBits(8);
            }

            return result;
        }
    }

    public class TruncatedError : Exception
    {
        public TruncatedError() : base("Data was truncated.")
        {
        }
    }
}
