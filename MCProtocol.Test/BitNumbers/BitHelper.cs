using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test.BitNumbers
{
    public static class BitHelper
    {
        public static long MergeInteger2(int i1, int i2, bool isBigEndian)
        {
            long value = 0L;

            if (isBigEndian)
            {
                value |= ((long)i1 << 0x20);
                value |= ((long)i2 << 0x00);
            }
            else
            {
                value |= ((long)i1 << 0x00);
                value |= ((long)i2 << 0x20);
            }

            return value;
        }

        public static int[] SplitInteger2(long value, bool isBigEndian)
        {
            int[] values = new int[2];

            if (isBigEndian)
            {
                values[0] = (int)((value >> 0x20) & 0xFFFFFFFF);
                values[1] = (int)((value >> 0x00) & 0xFFFFFFFF);
            }
            else
            {
                values[0] = (int)((value >> 0x00) & 0xFFFFFFFF);
                values[1] = (int)((value >> 0x20) & 0xFFFFFFFF);
            }

            return values;
        }

        public static byte[] SplitByes2(int value, bool isBigEndian)
        {
            byte[] bytes = new byte[2];

            if (isBigEndian)
            {
                bytes[0] = (byte)((value >> 08) & 0xFF);
                bytes[1] = (byte)((value >> 00) & 0xFF);
            }
            else
            {
                bytes[0] = (byte)((value >> 00) & 0xFF);
                bytes[1] = (byte)((value >> 08) & 0xFF);
            }

            return bytes;
        }

        public static short MergeByte2(byte b1, byte b2, bool isBigEndian)
        {
            int value = 0;

            if (isBigEndian)
            {
                value |= b1 << 08;
                value |= b2 << 00;
            }
            else
            {
                value |= b1 << 00;
                value |= b2 << 08;
            }

            return (short)value;
        }

        public static byte[] SplitByte4(int value, bool isBigEndian)
        {
            byte[] bytes = new byte[4];

            if (isBigEndian)
            {
                bytes[0] = (byte)((value >> 24) & 0xFF);
                bytes[1] = (byte)((value >> 16) & 0xFF);
                bytes[2] = (byte)((value >> 08) & 0xFF);
                bytes[3] = (byte)((value >> 00) & 0xFF);
            }
            else
            {
                bytes[0] = (byte)((value >> 00) & 0xFF);
                bytes[1] = (byte)((value >> 08) & 0xFF);
                bytes[2] = (byte)((value >> 16) & 0xFF);
                bytes[3] = (byte)((value >> 24) & 0xFF);
            }

            return bytes;
        }

        public static int MergeByte4(byte[] bytes, bool isBigEndian)
        {
            return MergeByte4(bytes[0], bytes[1], bytes[2], bytes[3], isBigEndian);
        }

        public static int MergeByte4(byte b1, byte b2, byte b3, byte b4, bool isBigEndian)
        {
            int value = 0;

            if (isBigEndian)
            {
                value |= b1 << 24;
                value |= b2 << 16;
                value |= b3 << 08;
                value |= b4 << 00;
            }
            else
            {
                value |= b1 << 00;
                value |= b2 << 08;
                value |= b3 << 16;
                value |= b4 << 24;
            }

            return value;
        }

        public static int ToInt32(bool[] bools)
        {
            int value = 0;

            for (int i = 0; i < bools.Length; ++i)
            {
                value |= bools[i] ? (int)(1 << i) : 0;
            }

            return value;
        }

        public static long ToInt64(bool[] bools)
        {
            long value = 0;

            for (int i = 0; i < bools.Length; ++i)
            {
                value |= bools[i] ? (long)(1 << i) : 0;
            }

            return value;
        }

        public static uint ToUInt32(bool[] bools)
        {
            uint value = 0;

            for (int i = 0; i < bools.Length; ++i)
            {
                value |= bools[i] ? (uint)(1 << i) : 0;
            }

            return value;
        }

        public static ulong ToUInt64(bool[] bools)
        {
            ulong value = 0;

            for (int i = 0; i < bools.Length; ++i)
            {
                value |= bools[i] ? (ulong)(1 << i) : 0;
            }

            return value;
        }

    }

}
