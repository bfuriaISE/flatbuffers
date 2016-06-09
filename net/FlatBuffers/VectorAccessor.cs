
#define CACHE_VECTOR_DATA_LENGTH

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlatBuffers {
  public struct VectorAccessor {
    public VectorAccessor(ByteBuffer byteBuffer, int offset) {
      _bufferPosition = new BufferPosition(byteBuffer, offset);

#if CACHE_VECTOR_DATA_LENGTH
      _vectorDataLength = GetVectorDataLength(ref _bufferPosition);
#endif
    }

    public VectorAccessor(BufferPosition bufferPosition) {
#if DEBUG
      if (bufferPosition.ByteBuffer == null)
        throw new ArgumentException("invalid buffer position");
#endif

      _bufferPosition = bufferPosition;

#if CACHE_VECTOR_DATA_LENGTH
      _vectorDataLength = GetVectorDataLength(ref _bufferPosition);
#endif
    }

    public VectorAccessor(ref BufferPosition bufferPosition) {
#if DEBUG
      if (bufferPosition.ByteBuffer == null)
        throw new ArgumentException("invalid buffer position");
#endif

      _bufferPosition = bufferPosition;

#if CACHE_VECTOR_DATA_LENGTH
      _vectorDataLength = GetVectorDataLength(ref _bufferPosition);
#endif
    }

    public BufferPosition BufferPosition {
      get { return _bufferPosition; }
    }

    public int VectorDataLength {
      get {
#if CACHE_VECTOR_DATA_LENGTH
        return _vectorDataLength;
#else
        return GetVectorDataLength(ref _bufferPosition);
#endif
      }
    }

    public int VectorDataOffset {
      get { return GetVectorDataOffset(_bufferPosition.Offset); }
    }

    public byte GetByteItem(int vectorItemOffset) {
      return _bufferPosition.GetByte(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(byte)));
    }

    public sbyte GetSbyteItem(int vectorItemOffset) {
      return _bufferPosition.GetSbyte(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(sbyte)));
    }

    public bool GetBoolItem(int vectorItemOffset) {
      return _bufferPosition.GetBool(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(byte)));
    }

    public short GetShortItem(int vectorItemOffset) {
      return _bufferPosition.GetShort(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(short)));
    }

    public ushort GetUshortItem(int vectorItemOffset) {
      return _bufferPosition.GetUshort(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(ushort)));
    }

    public int GetIntItem(int vectorItemOffset) {
      return _bufferPosition.GetInt(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(int)));
    }

    public uint GetUintItem(int vectorItemOffset) {
      return _bufferPosition.GetUint(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(uint)));
    }

    public long GetLongItem(int vectorItemOffset) {
      return _bufferPosition.GetLong(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(long)));
    }

    public ulong GetUlongItem(int vectorItemOffset) {
      return _bufferPosition.GetUlong(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(ulong)));
    }

    public float GetFloatItem(int vectorItemOffset) {
      return _bufferPosition.GetFloat(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(float)));
    }

    public double GetDoubleItem(int vectorItemOffset) {
      return _bufferPosition.GetDouble(GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(double)));
    }

    public string GetStringItem(int vectorItemOffset) {
      VectorAccessor strChrVectorPos;
      GetStringItemVectorAccessor(vectorItemOffset, out strChrVectorPos);
      return strChrVectorPos.GetVectorAsString();
    }

    public void GetStructItem(int vectorItemOffset, int stride, out BufferPosition structPosition) {
      _bufferPosition.Create(GetItemVecBufPosRelOffset(vectorItemOffset, stride), out structPosition);
    }

    public void GetTableItem(int vectorItemOffset, out BufferPosition tablePosition) {
      GetItemBufferPositionFromOffset(vectorItemOffset, out tablePosition);
    }

    public ArraySegment<byte> GetStringItemAsArraySegment(int vectorItemOffset) {
      VectorAccessor strChrVectorPos;
      GetStringItemVectorAccessor(vectorItemOffset, out strChrVectorPos);
      return strChrVectorPos.GetVectorAsArraySegment();
    }

    public void GetStringItemAsArraySegment(int vectorItemOffset, out ArraySegment<byte> arraySegment) {
      VectorAccessor strChrVectorPos;
      GetStringItemVectorAccessor(vectorItemOffset, out strChrVectorPos);
      strChrVectorPos.GetVectorAsArraySegment(out arraySegment);
    }

    public ByteBufferSegment GetStringItemAsByteBufferSegment(int vectorItemOffset) {
      VectorAccessor strChrVectorPos;
      GetStringItemVectorAccessor(vectorItemOffset, out strChrVectorPos);
      return strChrVectorPos.GetVectorAsByteBufferSegment();
    }

    public void GetStringItemAsByteBufferSegment(int vectorItemOffset, out ByteBufferSegment byteBufferSegment) {
      VectorAccessor strChrVectorPos;
      GetStringItemVectorAccessor(vectorItemOffset, out strChrVectorPos);
      strChrVectorPos.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }


    public void PutByteItem(int vectorItemOffset, byte value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(byte));
      _bufferPosition.PutByte(relOffset, value);
    }

    public void PutSbyteItem(int vectorItemOffset, sbyte value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(sbyte));
      _bufferPosition.PutSbyte(relOffset, value);
    }

    public void PutBoolItem(int vectorItemOffset, bool value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(byte));
      _bufferPosition.PutBool(relOffset, value);
    }

    public void PutShortItem(int vectorItemOffset, short value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(short));
      _bufferPosition.PutShort(relOffset, value);
    }

    public void PutUshortItem(int vectorItemOffset, ushort value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(ushort));
      _bufferPosition.PutUshort(relOffset, value);
    }

    public void PutIntItem(int vectorItemOffset, int value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(int));
      _bufferPosition.PutInt(relOffset, value);
    }

    public void PutUintItem(int vectorItemOffset, uint value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(uint));
      _bufferPosition.PutUint(relOffset, value);
    }

    public void PutLongItem(int vectorItemOffset, long value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(long));
      _bufferPosition.PutLong(relOffset, value);
    }

    public void PutUlongItem(int vectorItemOffset, ulong value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(ulong));
      _bufferPosition.PutUlong(relOffset, value);
    }

    public void PutFloatItem(int vectorItemOffset, float value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(float));
      _bufferPosition.PutFloat(relOffset, value);
    }

    public void PutDoubleItem(int vectorItemOffset, double value) {
      int relOffset = GetItemVecBufPosRelOffset(vectorItemOffset, sizeof(double));
      _bufferPosition.PutDouble(relOffset, value);
    }


    public string GetVectorAsString() {
      return _bufferPosition.GetString(DataRelOffset, VectorDataLength);
    }

    public ArraySegment<byte> GetVectorAsArraySegment() {
      return _bufferPosition.GetArraySegment(DataRelOffset, VectorDataLength);
    }

    public void GetVectorAsArraySegment(out ArraySegment<byte> arraySegment) {
      _bufferPosition.GetArraySegment(DataRelOffset, VectorDataLength, out arraySegment);
    }

    public ByteBufferSegment GetVectorAsByteBufferSegment() {
      return _bufferPosition.GetByteBufferSegment(DataRelOffset, VectorDataLength);
    }

    public void GetVectorAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _bufferPosition.GetByteBufferSegment(DataRelOffset, VectorDataLength, out byteBufferSegment);
    }

    public void GetNestedFlatBufferTable(out BufferPosition tablePosition) {
      GetNestedFlatBufferTable(ref _bufferPosition, out tablePosition);
    }


    public static int GetVectorDataOffset(int vectorOffset) {
      return vectorOffset + DataRelOffset;
    }

    public static int GetVectorDataLength(ref BufferPosition vectorBufferPosition) {
      return vectorBufferPosition.GetInt();
    }

    public static void GetNestedFlatBufferTable(ref BufferPosition vectorBufferPosition,
                                                out BufferPosition tablePosition) {
      vectorBufferPosition.CreateFromOffset(DataRelOffset, out tablePosition);
    }


    private void GetItemBufferPositionFromOffset(int vectorItemOffset,
                                                 out BufferPosition bufferPosition) {
      int itemBufPosRelOffset = GetItemVecBufPosRelOffset(vectorItemOffset, OffsetStride);
      _bufferPosition.CreateFromOffset(itemBufPosRelOffset, out bufferPosition);
    }

    private void GetStringItemVectorAccessor(int vectorItemOffset,
                                             out VectorAccessor stringItemVectorAccessor) {
      BufferPosition stringItemVectorPosition;
      GetItemBufferPositionFromOffset(vectorItemOffset, out stringItemVectorPosition);
      stringItemVectorAccessor = new VectorAccessor(ref stringItemVectorPosition);
    }


    private static int GetItemVecBufPosRelOffset(int vectorItemOffset, int stride) {
      return DataRelOffset + vectorItemOffset * stride;
    }

    private const int DataRelOffset = sizeof(int);
    private const int OffsetStride = sizeof(int);

    private BufferPosition _bufferPosition;

#if CACHE_VECTOR_DATA_LENGTH
    private int _vectorDataLength;
#endif
  }
}
