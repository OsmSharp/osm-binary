using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OsmSharp.IO.Binary.Test")]
namespace OsmSharp.IO.Binary
{
    internal static class BitCoder
    {
        private const byte Mask = 128 - 1;

        public static long WriteVarUInt32(this Stream stream, uint value)
        {
            var d0 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                return 1;
            }

            d0 += 128;
            var d1 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                return 2;
            }

            d1 += 128;
            var d2 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                return 3;
            }

            d2 += 128;
            var d3 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                stream.WriteByte(d3);
                return 4;
            }

            d3 += 128;
            var d4 = (byte) (value & Mask);
            stream.WriteByte(d0);
            stream.WriteByte(d1);
            stream.WriteByte(d2);
            stream.WriteByte(d3);
            stream.WriteByte(d4);
            return 5;
        }

        public static long WriteVarUInt64(this Stream stream, ulong value)
        {
            var d0 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                return 1;
            }

            d0 += 128;
            var d1 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                return 2;
            }

            d1 += 128;
            var d2 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                return 3;
            }

            d2 += 128;
            var d3 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                stream.WriteByte(d3);
                return 4;
            }

            d3 += 128;
            var d4 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                stream.WriteByte(d3);
                stream.WriteByte(d4);
                return 5;
            }

            d4 += 128;
            var d5 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                stream.WriteByte(d3);
                stream.WriteByte(d4);
                stream.WriteByte(d5);
                return 6;
            }

            d5 += 128;
            var d6 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                stream.WriteByte(d3);
                stream.WriteByte(d4);
                stream.WriteByte(d5);
                stream.WriteByte(d6);
                return 7;
            }

            d6 += 128;
            var d7 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                stream.WriteByte(d3);
                stream.WriteByte(d4);
                stream.WriteByte(d5);
                stream.WriteByte(d6);
                stream.WriteByte(d7);
                return 8;
            }

            d7 += 128;
            var d8 = (byte) (value & Mask);
            value >>= 7;
            if (value == 0)
            {
                stream.WriteByte(d0);
                stream.WriteByte(d1);
                stream.WriteByte(d2);
                stream.WriteByte(d3);
                stream.WriteByte(d4);
                stream.WriteByte(d5);
                stream.WriteByte(d6);
                stream.WriteByte(d7);
                stream.WriteByte(d8);
                return 9;
            }

            d8 += 128;
            var d9 = (byte) (value & Mask);
            stream.WriteByte(d0);
            stream.WriteByte(d1);
            stream.WriteByte(d2);
            stream.WriteByte(d3);
            stream.WriteByte(d4);
            stream.WriteByte(d5);
            stream.WriteByte(d6);
            stream.WriteByte(d7);
            stream.WriteByte(d8);
            stream.WriteByte(d9);
            return 10;
        }

        public static long ReadVarUInt32(this Stream stream, out uint value)
        {
            var d = stream.ReadByte();
            if (d < 128)
            {
                value = (uint)d;
                return 1;
            }
            value = (uint)d - 128;
            d = stream.ReadByte();
            if (d < 128)
            {
                value += ((uint)d << 7);
                return 2;
            }
            d -= 128;
            value += ((uint)d << 7);
            d = stream.ReadByte();
            if (d < 128)
            {
                value += ((uint)d << 14);
                return 3;
            }
            d -= 128;
            value += ((uint)d << 14);
            d = stream.ReadByte();
            if (d < 128)
            {
                value += ((uint)d << 21);
                return 4;
            }
            d -= 128;
            value += ((uint)d << 21);
            d =stream.ReadByte();
            value += ((uint) d << 28);
            return 5;
        }

        public static long ReadVarUInt64(this Stream stream, out ulong value)
        {
            var d = stream.ReadByte();
            if (d < 128)
            {
                value = (ulong)d;
                return 1;
            }

            value = (ulong) d - 128;
            d = stream.ReadByte();;
            if (d < 128)
            {
                value += ((uint) d << 7);
                return 2;
            }

            d -= 128;
            value += ((ulong) d << 7);
            d = stream.ReadByte();;
            if (d < 128)
            {
                value += ((uint) d << 14);
                return 3;
            }

            d -= 128;
            value += ((ulong) d << 14);
            d = stream.ReadByte();;
            if (d < 128)
            {
                value += ((ulong) d << 21);
                return 4;
            }

            d -= 128;
            value += ((ulong) d << 21);
            d = stream.ReadByte();;
            if (d < 128)
            {
                value += ((ulong) d << 28);
                return 5;
            }

            d -= 128;
            value += ((ulong) d << 28);
            d = stream.ReadByte();;
            if (d < 128)
            {
                value += ((ulong) d << 35);
                return 6;
            }

            d -= 128;
            value += ((ulong) d << 35);
            d = stream.ReadByte();;
            if (d < 128)
            {
                value += ((ulong) d << 42);
                return 7;
            }

            d -= 128;
            value += ((ulong) d << 42);
            d = stream.ReadByte();;
            if (d < 128)
            {
                value += ((ulong) d << 49);
                return 8;
            }

            d -= 128;
            value += ((ulong) d << 49);
            d = stream.ReadByte();;
            if (d < 128)
            {
                value += ((ulong) d << 56);
                return 9;
            }

            d -= 128;
            value += ((ulong) d << 56);
            d = stream.ReadByte();;
            value += ((ulong) d << 63);
            return 10;
        }

        private static ulong ToUnsigned(long value)
        {
            var unsigned = (uint) value;
            if (value < 0) unsigned = (uint) -value;

            unsigned <<= 1;
            if (value < 0)
            {
                unsigned += 1;
            }

            return unsigned;
        }

        private static uint ToUnsigned(int value)
        {
            var unsigned = (uint) value;
            if (value < 0) unsigned = (uint) -value;

            unsigned <<= 1;
            if (value < 0)
            {
                unsigned += 1;
            }

            return unsigned;
        }

        private static long FromUnsigned(ulong unsigned)
        {
            var sign = unsigned & (uint)1;

            var value = (int)(unsigned >> 1);
            if (sign == 1)
            {
                value = -value;
            }

            return value;
        }

        private static int FromUnsigned(uint unsigned)
        {
            var sign = unsigned & (uint)1;

            var value = (int)(unsigned >> 1);
            if (sign == 1)
            {
                value = -value;
            }

            return value;
        }
        
        public static long WriteVarInt32(this Stream data, int value)
        {
            return data.WriteVarUInt32(ToUnsigned(value));
        }

        public static long ReadVarInt32(this Stream data, out int value)
        {
            var c = data.ReadVarUInt32(out var unsigned);
            value = FromUnsigned(unsigned);
            return c;
        }
        
        public static long WriteVarInt64(this Stream data, long value)
        {
            return data.WriteVarUInt64(ToUnsigned(value));
        }

        public static long ReadVarInt64(this Stream data, out long value)
        {
            var c = data.ReadVarUInt64(out var unsigned);
            value = FromUnsigned(unsigned);
            return c;
        }

        public static void WriteInt64(this Stream stream, long value)
        {
            for (var b = 0; b < 8; b++)
            {
                stream.WriteByte((byte)(value & byte.MaxValue));
                value >>= 8;
            }
        }

        public static long ReadInt64(this Stream stream)
        {
            var value = 0L;
            for (var b = 0; b < 8; b++)
            {
                value += ((long)stream.ReadByte() << (b * 8));
            }

            return value;
        }

        public static void WriteUInt64(this Stream stream, ulong value)
        {
            for (var b = 0; b < 8; b++)
            {
                stream.WriteByte((byte)(value & byte.MaxValue));
                value >>= 8;
            }
        }

        public static ulong ReadUInt64(this Stream stream)
        {
            var value = 0UL;
            for (var b = 0; b < 8; b++)
            {
                value += ((ulong)stream.ReadByte() << (b * 8));
            }

            return value;
        }

        public static void WriteInt32(this Stream stream, int value)
        {
            for (var b = 0; b < 4; b++)
            {
                stream.WriteByte((byte)(value & byte.MaxValue));
                value >>= 8;
            }
        }

        public static int ReadInt32(this Stream stream)
        {
            var value = 0;
            for (var b = 0; b < 4; b++)
            {
                value += (stream.ReadByte() << (b * 8));
            }

            return value;
        }
    }
}