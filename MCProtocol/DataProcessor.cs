using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class DataProcessor
    {
        public bool LittleEndian { get; set; }

        public Stream BaseStream { get; }

        public DataProcessor(Stream baseStream)
        {
            this.LittleEndian = BitConverter.IsLittleEndian;
            this.BaseStream = baseStream;
        }

        public virtual void Write(byte[] bytes, int offset, int count)
        {
            this.BaseStream.Write(bytes, offset, count);
        }

        public virtual void WriteByte(byte value)
        {
            this.BaseStream.WriteByte(value);
        }

        public virtual void WriteBytes(byte[] value)
        {
            this.Write(value, 0, value.Length);
        }
        public virtual void WriteSByte(sbyte value)
        {
            this.WriteByte((byte)value);
        }

        public virtual void WriteShort(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteUShort(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteChar(char value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteInt(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteUInt(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteLong(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteULong(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteFloat(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteDouble(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual int Read(byte[] bytes, int offset, int count)
        {
            return this.BaseStream.Read(bytes, offset, count);
        }

        public virtual byte ReadByte()
        {
            int data = this.BaseStream.ReadByte();

            if (data == -1)
            {
                throw new IOException();
            }

            return (byte)data;
        }

        public virtual byte[] ReadBytes(int length)
        {
            byte[] bytes = new byte[length];
            this.ReadBytes(bytes);

            return bytes;
        }

        public virtual void ReadBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = this.ReadByte();
            }

        }

        public virtual sbyte ReadSByte()
        {
            return (sbyte)this.ReadByte();
        }

        public virtual short ReadShort()
        {
            byte[] bytes = this.ReadBytes(2);
            this.FlipCheck(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        public virtual ushort ReadUShort()
        {
            byte[] bytes = this.ReadBytes(2);
            this.FlipCheck(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public virtual int ReadInt()
        {
            byte[] bytes = this.ReadBytes(4);
            this.FlipCheck(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public virtual uint ReadUInt()
        {
            byte[] bytes = this.ReadBytes(4);
            this.FlipCheck(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public virtual long ReadLong()
        {
            byte[] bytes = this.ReadBytes(8);
            this.FlipCheck(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        public virtual ulong ReadULong()
        {
            byte[] bytes = this.ReadBytes(8);
            this.FlipCheck(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public virtual float ReadFloat()
        {
            byte[] bytes = this.ReadBytes(4);
            this.FlipCheck(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        public virtual double ReadDouble()
        {
            byte[] bytes = this.ReadBytes(8);
            this.FlipCheck(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        public virtual void FlipCheck(byte[] bytes)
        {
            if (this.LittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

        }

    }

}
