using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlatBuffers {
  public struct BufferPosition {
    public BufferPosition(ByteBuffer byteBuffer, int offset) {
#if DEBUG
      if (byteBuffer == null)
        throw new ArgumentNullException("byteBuffer");
      if ((uint)offset > byteBuffer.Length)
        throw new ArgumentOutOfRangeException("offset");
#endif
      _byteBuffer = byteBuffer;
      _offset = offset;
    }

    public ByteBuffer ByteBuffer {
      get { return _byteBuffer; }
    }

    public int Offset {
      get { return _offset; }
    }

    public byte GetByte(int relOffset = 0) {
      return _byteBuffer.Get(_offset + relOffset);
    }

    public sbyte GetSbyte(int relOffset = 0) {
      return _byteBuffer.GetSbyte(_offset + relOffset);
    }

    public bool GetBool(int relOffset = 0) {
      return GetByte(relOffset) != 0;
    }

    public short GetShort(int relOffset = 0) {
      return _byteBuffer.GetShort(_offset + relOffset);
    }

    public ushort GetUshort(int relOffset = 0) {
      return _byteBuffer.GetUshort(_offset + relOffset);
    }

    public int GetInt(int relOffset = 0) {
      return _byteBuffer.GetInt(_offset + relOffset);
    }

    public uint GetUint(int relOffset = 0) {
      return _byteBuffer.GetUint(_offset + relOffset);
    }

    public long GetLong(int relOffset = 0) {
      return _byteBuffer.GetLong(_offset + relOffset);
    }

    public ulong GetUlong(int relOffset = 0) {
      return _byteBuffer.GetUlong(_offset + relOffset);
    }

    public float GetFloat(int relOffset = 0) {
      return _byteBuffer.GetFloat(_offset + relOffset);
    }

    public double GetDouble(int relOffset = 0) {
      return _byteBuffer.GetDouble(_offset + relOffset);
    }

    public string GetString(int relOffset, int length) {
      return _byteBuffer.GetStringUtf8(_offset + relOffset, length);
    }

    public string GetString(int length) {
      return GetString(0, length);
    }

    public int GetOffset(int relOffset = 0) {
      return _byteBuffer.GetInt(_offset + relOffset);
    }

    public void CreateFromOffset(int relOffset, out BufferPosition bufferPosition) {
      bufferPosition = new BufferPosition(_byteBuffer, GetAbsoluteOffset(_byteBuffer, _offset, relOffset));
    }

    public void CreateFromOffset(out BufferPosition bufferPosition) {
      CreateFromOffset(0, out bufferPosition);
    }

    public BufferPosition CreateFromOffset(int relOffset = 0) {
      return new BufferPosition(_byteBuffer, GetAbsoluteOffset(_byteBuffer, _offset, relOffset));
    }

    public void Create(int relOffset, out BufferPosition bufferPosition) {
      bufferPosition = new BufferPosition(_byteBuffer, _offset + relOffset);
    }

    public void Create(out BufferPosition bufferPosition) {
      Create(0, out bufferPosition);
    }

    public BufferPosition Create(int relOffset = 0) {
      return new BufferPosition(_byteBuffer, _offset + relOffset);
    }

    public ArraySegment<byte> GetArraySegment(int relOffset, int length) {
      return _byteBuffer.GetArraySegment(_offset + relOffset, length);
    }

    public void GetArraySegment(int relOffset, int length, out ArraySegment<byte> arraySegment) {
      _byteBuffer.GetArraySegment(_offset + relOffset, length, out arraySegment);
    }

    public ArraySegment<byte> GetArraySegment(int length) {
      return GetArraySegment(0, length);
    }

    public void GetArraySegment(int length, out ArraySegment<byte> arraySegment) {
      GetArraySegment(0, length, out arraySegment);
    }

    public ByteBufferSegment GetByteBufferSegment(int relOffset, int length) {
      return new ByteBufferSegment(_byteBuffer, _offset + relOffset, length);
    }

    public void GetByteBufferSegment(int relOffset,
                                     int length,
                                     out ByteBufferSegment byteBufferSegment) {
      byteBufferSegment = new ByteBufferSegment(_byteBuffer, _offset + relOffset, length);
    }

    public ByteBufferSegment GetByteBufferSegment(int length) {
      return GetByteBufferSegment(0, length);
    }

    public void GetByteBufferSegment(int length, out ByteBufferSegment byteBufferSegment) {
      GetByteBufferSegment(0, length, out byteBufferSegment);
    }


    public void PutByte(int relOffset, byte value) {
      _byteBuffer.PutByte(_offset + relOffset, value);
    }

    public void PutByte(byte value) {
      PutByte(0, value);
    }

    public void PutSbyte(int relOffset, sbyte value) {
      _byteBuffer.PutSbyte(_offset + relOffset, value);
    }

    public void PutSbyte(sbyte value) {
      PutSbyte(0, value);
    }

    public void PutBool(int relOffset, bool value) {
      PutByte(relOffset, (byte)(value ? 1 : 0));
    }

    public void PutBool(bool value) {
      PutBool(0, value);
    }

    public void PutShort(int relOffset, short value) {
      _byteBuffer.PutShort(_offset + relOffset, value);
    }

    public void PutShort(short value) {
      PutShort(0, value);
    }

    public void PutUshort(int relOffset, ushort value) {
      _byteBuffer.PutUshort(_offset + relOffset, value);
    }

    public void PutUshort(ushort value) {
      PutUshort(0, value);
    }

    public void PutInt(int relOffset, int value) {
      _byteBuffer.PutInt(_offset + relOffset, value);
    }

    public void PutInt(int value) {
      PutInt(0, value);
    }

    public void PutUint(int relOffset, uint value) {
      _byteBuffer.PutUint(_offset + relOffset, value);
    }

    public void PutUint(uint value) {
      PutUint(0, value);
    }

    public void PutLong(int relOffset, long value) {
      _byteBuffer.PutLong(_offset + relOffset, value);
    }

    public void PutLong(long value) {
      PutLong(0, value);
    }

    public void PutUlong(int relOffset, ulong value) {
      _byteBuffer.PutUlong(_offset + relOffset, value);
    }

    public void PutUlong(ulong value) {
      PutUlong(0, value);
    }

    public void PutFloat(int relOffset, float value) {
      _byteBuffer.PutFloat(_offset + relOffset, value);
    }

    public void PutFloat(float value) {
      PutFloat(0, value);
    }

    public void PutDouble(int relOffset, double value) {
      _byteBuffer.PutDouble(_offset + relOffset, value);
    }

    public void PutDouble(double value) {
      PutDouble(0, value);
    }


    public static void CreateFromOffset(ByteBuffer byteBuffer,
                                        int offsetOffset,
                                        out BufferPosition bufferPosition) {
      bufferPosition = new BufferPosition(byteBuffer, GetAbsoluteOffset(byteBuffer, offsetOffset, 0));
    }

    public static void CreateFromOffset(ByteBuffer byteBuffer,
                                        out BufferPosition bufferPosition) {
      bufferPosition = new BufferPosition(byteBuffer, GetAbsoluteOffset(byteBuffer, byteBuffer.Position, 0));
    }

    public static void CreateFromOffset(ref ByteBufferSegment segment, 
                                        out BufferPosition bufferPosition) {
      bufferPosition = 
        new BufferPosition(segment.ByteBuffer, GetAbsoluteOffset(segment.ByteBuffer, segment.Offset, 0));
    }


    private static int GetAbsoluteOffset(ByteBuffer byteBuffer, int offset, int relOffset) {
      int absOffset = relOffset + offset;
      return absOffset + byteBuffer.GetInt(absOffset);
    }


    private ByteBuffer _byteBuffer;
    private int _offset;
  }
}
