//#define CACHE_VTABLE_INFO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlatBuffers {
  public struct TableAccessor {

    public TableAccessor(ByteBuffer byteBuffer, int offset) {
      _bufferPosition = new BufferPosition(byteBuffer, offset);

#if CACHE_VTABLE_INFO
      CacheVtableInfo(ref _bufferPosition, out _vtableRelOffset, out _vtableSize);
#endif
    }

    public TableAccessor(BufferPosition bufferPosition) {
#if DEBUG
      if (bufferPosition.ByteBuffer == null)
        throw new ArgumentException("invalid buffer position");
#endif

      _bufferPosition = bufferPosition;

#if CACHE_VTABLE_INFO
      CacheVtableInfo(ref _bufferPosition, out _vtableRelOffset, out _vtableSize);
#endif
    }

    public TableAccessor(ref BufferPosition bufferPosition) {
#if DEBUG
      if (bufferPosition.ByteBuffer == null)
        throw new ArgumentException("invalid buffer position");
#endif

      _bufferPosition = bufferPosition;

#if CACHE_VTABLE_INFO
      CacheVtableInfo(ref _bufferPosition, out _vtableRelOffset, out _vtableSize);
#endif
    }

    public BufferPosition BufferPosition {
      get { return _bufferPosition; }
    }

    public bool CheckField(int fieldVtableOffset) {
      return GetFieldRelOffset(fieldVtableOffset) != 0;
    }

    public byte GetByteFieldValue(int fieldVtableOffset, byte defaultValue = 0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetByte(fieldOffset) : defaultValue;
    }

    public sbyte GetSbyteFieldValue(int fieldVtableOffset, sbyte defaultValue = 0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetSbyte(fieldOffset) : defaultValue;
    }

    public bool GetBoolFieldValue(int fieldVtableOffset, bool defaultValue = false) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetBool(fieldOffset) : defaultValue;
    }

    public short GetShortFieldValue(int fieldVtableOffset, short defaultValue = 0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetShort(fieldOffset) : defaultValue;
    }

    public ushort GetUshortFieldValue(int fieldVtableOffset, ushort defaultValue = 0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetUshort(fieldOffset) : defaultValue;
    }

    public int GetIntFieldValue(int fieldVtableOffset, int defaultValue = 0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetInt(fieldOffset) : defaultValue;
    }

    public uint GetUintFieldValue(int fieldVtableOffset, uint defaultValue = 0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetUint(fieldOffset) : defaultValue;
    }

    public long GetLongFieldValue(int fieldVtableOffset, long defaultValue = 0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetLong(fieldOffset) : defaultValue;
    }

    public ulong GetUlongFieldValue(int fieldVtableOffset, ulong defaultValue = 0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetUlong(fieldOffset) : defaultValue;
    }

    public float GetFloatFieldValue(int fieldVtableOffset, float defaultValue = 0.0F) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetFloat(fieldOffset) : defaultValue;
    }

    public double GetDoubleFieldValue(int fieldVtableOffset, double defaultValue = 0.0) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      return fieldOffset != 0 ? _bufferPosition.GetDouble(fieldOffset) : defaultValue;
    }

    public string GetStringFieldValue(int fieldVtableOffset) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset == 0)
        return null;
      VectorAccessor strCharVectorAccessor;
      GetVectorAccessor(fieldOffset, out strCharVectorAccessor);
      return strCharVectorAccessor.GetVectorAsString();
    }

    public ArraySegment<byte>? GetStringFieldValueAsArraySegment(int fieldVtableOffset) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset == 0)
        return null;
      VectorAccessor charVectorAccessor;
      GetVectorAccessor(fieldOffset, out charVectorAccessor);
      return charVectorAccessor.GetVectorAsArraySegment();
    }

    public bool TryGetStringFieldValueAsArraySegment(int fieldVtableOffset,
                                                     out ArraySegment<byte> arrSeg) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset == 0) {
        arrSeg = default(ArraySegment<byte>);
        return false;
      }
      VectorAccessor charVectorAccessor;
      GetVectorAccessor(fieldOffset, out charVectorAccessor);
      charVectorAccessor.GetVectorAsArraySegment(out arrSeg);
      return true;
    }

    public ByteBufferSegment? GetStringFieldValueAsByteBufferSegment(int fieldVtableOffset) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset == 0)
        return null;
      VectorAccessor charVectorAccessor;
      GetVectorAccessor(fieldOffset, out charVectorAccessor);
      return charVectorAccessor.GetVectorAsByteBufferSegment();
    }

    public bool TryGetStringFieldValueAsByteBufferSegment(int fieldVtableOffset,
                                                     out ByteBufferSegment bbSeg) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset == 0) {
        bbSeg = default(ByteBufferSegment);
        return false;
      }
      VectorAccessor charVectorAccessor;
      GetVectorAccessor(fieldOffset, out charVectorAccessor);
      charVectorAccessor.GetVectorAsByteBufferSegment(out bbSeg);
      return true;
    }

    public bool TryGetVectorFieldValue(int fieldVtableOffset, out BufferPosition vectorPosition) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset == 0) {
        vectorPosition = default(BufferPosition);
        return false;
      }
      GetVectorPosition(fieldOffset, out vectorPosition);
      return true;
    }

    public bool TryGetStructFieldValue(int fieldVtableOffset,
                                       out BufferPosition structPosition) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset == 0) {
        structPosition = default(BufferPosition);
        return false;
      }
      GetStructPosition(fieldOffset, out structPosition);
      return true;
    }

    public bool TryGetTableFieldValue(int fieldVtableOffset, out BufferPosition tablePosition) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset == 0) {
        tablePosition = default(BufferPosition);
        return false;
      }
      GetTablePosition(fieldOffset, out tablePosition);
      return true;
    }


    public bool MutateByteFieldValue(int fieldVtableOffset, byte value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutByte(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateSbyteFieldValue(int fieldVtableOffset, sbyte value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutSbyte(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateBoolFieldValue(int fieldVtableOffset, bool value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutBool(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateShortFieldValue(int fieldVtableOffset, short value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutShort(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateUshortFieldValue(int fieldVtableOffset, ushort value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutUshort(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateIntFieldValue(int fieldVtableOffset, int value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutInt(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateUintFieldValue(int fieldVtableOffset, uint value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutUint(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateLongFieldValue(int fieldVtableOffset, long value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutLong(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateUlongFieldValue(int fieldVtableOffset, ulong value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutUlong(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateFloatFieldValue(int fieldVtableOffset, float value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutFloat(fieldOffset, value);
        return true;
      }
      return false;
    }

    public bool MutateDoubleFieldValue(int fieldVtableOffset, double value) {
      int fieldOffset = GetFieldRelOffset(fieldVtableOffset);
      if (fieldOffset != 0) {
        _bufferPosition.PutDouble(fieldOffset, value);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Returns <c>true</c> if the identifier is present in the byte buffer, <c>false</c> otherwise.
    /// </summary>
    public static bool HasIdentifier(ByteBuffer bb, string ident) {
      return HasIdentifier(new BufferPosition(bb, bb.Position), ident);
    }

    /// <summary>
    /// Returns <c>true</c> if the identifier is present in the byte buffer, <c>false</c> otherwise.
    /// </summary>
    public static bool HasIdentifier(BufferPosition bufferPosition, string ident) {
      if (ident.Length != FlatBufferConstants.FileIdentifierLength)
        throw new ArgumentException("FlatBuffers: file identifier must be length " + FlatBufferConstants.FileIdentifierLength, "ident");

      for (var i = 0; i < FlatBufferConstants.FileIdentifierLength; i++) {
        if (ident[i] != (char)bufferPosition.GetByte(sizeof(int) + i)) return false;
      }

      return true;
    }

    public static void CreateFromOffset(ByteBuffer byteBuffer,
                                        int offsetOffset,
                                        out TableAccessor tableAccessor) {
      tableAccessor = new TableAccessor(byteBuffer, offsetOffset, default(GetAsRootTag));
    }

    public static void CreateFromOffset(ByteBuffer byteBuffer,
                                        out TableAccessor tableAccessor) {
      CreateFromOffset(byteBuffer, byteBuffer.Position, out tableAccessor);
    }

    public static void CreateFromOffset(ref ByteBufferSegment segment,
                                        out TableAccessor tableAccessor) {
      CreateFromOffset(segment.ByteBuffer, segment.Offset, out tableAccessor);
    }


    private int GetFieldRelOffset(int fieldVtableOffset) {
      int vtableOffset = GetVtableRelOffset();

      return
        fieldVtableOffset < GetVtableSize(vtableOffset)
          ? (int)_bufferPosition.GetShort(vtableOffset + fieldVtableOffset)
          : 0;
    }

    private int GetVtableRelOffset() {
#if CACHE_VTABLE_INFO
      return _vtableRelOffset;
#else
      return GetVtableRelOffset(ref _bufferPosition);
#endif
    }

    private int GetVtableSize(int vtableRelOffset) {
#if CACHE_VTABLE_INFO
      return _vtableSize;
#else
      return GetVtableSize(ref _bufferPosition, vtableRelOffset);
#endif
    }

    private void GetVectorPosition(int fieldRelOffset, out BufferPosition vectorPosition) {
      _bufferPosition.CreateFromOffset(fieldRelOffset, out vectorPosition);
    }

    private void GetVectorAccessor(int fieldRelOffset, out VectorAccessor vectorAccessor) {
      BufferPosition vectorPosition;
      GetVectorPosition(fieldRelOffset, out vectorPosition);
      vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    private void GetStructPosition(int fieldRelOffset, out BufferPosition structPosition) {
      _bufferPosition.Create(fieldRelOffset, out structPosition);
    }

    private void GetTablePosition(int fieldRelOffset, out BufferPosition tablePosition) {
      _bufferPosition.CreateFromOffset(fieldRelOffset, out tablePosition);
    }


    private static int GetVtableRelOffset(ref BufferPosition tablePosition) {
      return -tablePosition.GetOffset();
    }

    private static short GetVtableSize(ref BufferPosition tablePosition, int vtableRelOffset) {
      return tablePosition.GetShort(vtableRelOffset);
    }

#if CACHE_VTABLE_INFO
    private static void CacheVtableInfo(ref BufferPosition tablePosition,
                                        out int vtableRelOffset,
                                        out short vtableSize) {
      vtableRelOffset = GetVtableRelOffset(ref tablePosition);
      vtableSize = GetVtableSize(ref tablePosition, vtableRelOffset);
    }
#endif


    private TableAccessor(ByteBuffer byteBuffer, int offset, GetAsRootTag tag) {
      BufferPosition.CreateFromOffset(byteBuffer, offset, out _bufferPosition);

#if CACHE_VTABLE_INFO
      CacheVtableInfo(ref _bufferPosition, out _vtableRelOffset, out _vtableSize);
#endif
    }


    private struct GetAsRootTag { }

    private BufferPosition _bufferPosition;

#if CACHE_VTABLE_INFO
    private int _vtableRelOffset;
    private short _vtableSize;
#endif
  }
}
