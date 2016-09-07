using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatBuffers {

  public struct ByteVector : IVector<byte> {
    public ByteVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public ByteVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public byte this[int index] {
      get { return _vectorAccessor.GetByteItem(index); }
      set { _vectorAccessor.PutByteItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(byte[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, byte[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(byte[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(byte[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public byte[] ToArray() {
      byte[] array = new byte[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<byte> ToList() {
      int length = Length;
      List<byte> list = new List<byte>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    } 

    public VectorEnumerator<byte, ByteVector> GetEnumerator() {
      return new VectorEnumerator<byte, ByteVector>(ref this);
    } 
    
    IEnumerator<byte> IEnumerable<byte>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }
  

  public struct SbyteVector : IVector<sbyte> {
    public SbyteVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public SbyteVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public sbyte this[int index] {
      get { return _vectorAccessor.GetSbyteItem(index); }
      set { _vectorAccessor.PutSbyteItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(sbyte[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, sbyte[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(sbyte[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(sbyte[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public sbyte[] ToArray() {
      sbyte[] array = new sbyte[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<sbyte> ToList() {
      int length = Length;
      List<sbyte> list = new List<sbyte>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<sbyte, SbyteVector> GetEnumerator() {
      return new VectorEnumerator<sbyte, SbyteVector>(ref this);
    }

    IEnumerator<sbyte> IEnumerable<sbyte>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct BoolVector : IVector<bool> {
    public BoolVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public BoolVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public bool this[int index] {
      get { return _vectorAccessor.GetBoolItem(index); }
      set { _vectorAccessor.PutBoolItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(bool[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, bool[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(bool[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(bool[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public bool[] ToArray() {
      bool[] array = new bool[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<bool> ToList() {
      int length = Length;
      List<bool> list = new List<bool>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<bool, BoolVector> GetEnumerator() {
      return new VectorEnumerator<bool, BoolVector>(ref this);
    }

    IEnumerator<bool> IEnumerable<bool>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct ShortVector : IVector<short> {
    public ShortVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public ShortVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public short this[int index] {
      get { return _vectorAccessor.GetShortItem(index); }
      set { _vectorAccessor.PutShortItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(short[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, short[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(short[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(short[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public short[] ToArray() {
      short[] array = new short[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<short> ToList() {
      int length = Length;
      List<short> list = new List<short>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<short, ShortVector> GetEnumerator() {
      return new VectorEnumerator<short, ShortVector>(ref this);
    }

    IEnumerator<short> IEnumerable<short>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct UshortVector : IVector<ushort> {
    public UshortVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public UshortVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public ushort this[int index] {
      get { return _vectorAccessor.GetUshortItem(index); }
      set { _vectorAccessor.PutUshortItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(ushort[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, ushort[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(ushort[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(ushort[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public ushort[] ToArray() {
      ushort[] array = new ushort[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<ushort> ToList() {
      int length = Length;
      List<ushort> list = new List<ushort>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }
    
    public VectorEnumerator<ushort, UshortVector> GetEnumerator() {
      return new VectorEnumerator<ushort, UshortVector>(ref this);
    }

    IEnumerator<ushort> IEnumerable<ushort>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct IntVector : IVector<int> {
    public IntVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public IntVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public int this[int index] {
      get { return _vectorAccessor.GetIntItem(index); }
      set { _vectorAccessor.PutIntItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(int[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, int[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(int[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(int[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public int[] ToArray() {
      int[] array = new int[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<int> ToList() {
      int length = Length;
      List<int> list = new List<int>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<int, IntVector> GetEnumerator() {
      return new VectorEnumerator<int, IntVector>(ref this);
    }

    IEnumerator<int> IEnumerable<int>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct UintVector : IVector<uint> {
    public UintVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public UintVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public uint this[int index] {
      get { return _vectorAccessor.GetUintItem(index); }
      set { _vectorAccessor.PutUintItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(uint[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, uint[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(uint[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(uint[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public uint[] ToArray() {
      uint[] array = new uint[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<uint> ToList() {
      int length = Length;
      List<uint> list = new List<uint>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<uint, UintVector> GetEnumerator() {
      return new VectorEnumerator<uint, UintVector>(ref this);
    }

    IEnumerator<uint> IEnumerable<uint>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct LongVector : IVector<long> {
    public LongVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public LongVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public long this[int index] {
      get { return _vectorAccessor.GetLongItem(index); }
      set { _vectorAccessor.PutLongItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(long[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, long[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(long[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(long[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public long[] ToArray() {
      long[] array = new long[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<long> ToList() {
      int length = Length;
      List<long> list = new List<long>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<long, LongVector> GetEnumerator() {
      return new VectorEnumerator<long, LongVector>(ref this);
    }

    IEnumerator<long> IEnumerable<long>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct UlongVector : IVector<ulong> {
    public UlongVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public UlongVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public ulong this[int index] {
      get { return _vectorAccessor.GetUlongItem(index); }
      set { _vectorAccessor.PutUlongItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(ulong[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, ulong[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(ulong[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(ulong[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public ulong[] ToArray() {
      ulong[] array = new ulong[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<ulong> ToList() {
      int length = Length;
      List<ulong> list = new List<ulong>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<ulong, UlongVector> GetEnumerator() {
      return new VectorEnumerator<ulong, UlongVector>(ref this);
    }

    IEnumerator<ulong> IEnumerable<ulong>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct FloatVector : IVector<float> {
    public FloatVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public FloatVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public float this[int index] {
      get { return _vectorAccessor.GetFloatItem(index); }
      set { _vectorAccessor.PutFloatItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public VectorEnumerator<float, FloatVector> GetEnumerator() {
      return new VectorEnumerator<float, FloatVector>(ref this);
    }

    public void CopyTo(float[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, float[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(float[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(float[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public float[] ToArray() {
      float[] array = new float[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<float> ToList() {
      int length = Length;
      List<float> list = new List<float>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    IEnumerator<float> IEnumerable<float>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct DoubleVector : IVector<double> {
    public DoubleVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public DoubleVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public double this[int index] {
      get { return _vectorAccessor.GetDoubleItem(index); }
      set { _vectorAccessor.PutDoubleItem(index, value); }
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(double[] array, int offset) {
      _vectorAccessor.CopyTo(array, offset);
    }

    public void CopyTo(int srcOffset, double[] dstArray, int dstOffset, int count) {
      _vectorAccessor.CopyTo(srcOffset, dstArray, dstOffset, count);
    }

    public void CopyFrom(double[] srcArray, int srcOffset) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset);
    }

    public void CopyFrom(double[] srcArray, int srcOffset, int dstOffset, int count) {
      _vectorAccessor.CopyFrom(srcArray, srcOffset, dstOffset, count);
    }

    public double[] ToArray() {
      double[] array = new double[Length];
      _vectorAccessor.CopyTo(array, 0);
      return array;
    }

    public List<double> ToList() {
      int length = Length;
      List<double> list = new List<double>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<double, DoubleVector> GetEnumerator() {
      return new VectorEnumerator<double, DoubleVector>(ref this);
    }

    IEnumerator<double> IEnumerable<double>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }


    private VectorAccessor _vectorAccessor;
  }


  public struct StringVector : IVector<string> {
    public StringVector(BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(vectorPosition);
    }

    public StringVector(ref BufferPosition vectorPosition) {
      _vectorAccessor = new VectorAccessor(ref vectorPosition);
    }

    public VectorAccessor VectorAccessor {
      get { return _vectorAccessor; }
    }

    public int Length {
      get { return _vectorAccessor.VectorDataLength; }
    }

    public string this[int index] {
      get { return _vectorAccessor.GetStringItem(index); }
      set { throw new NotSupportedException(); }
    }

    public void GetItemAsArraySegment(int index, out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetStringItemAsArraySegment(index, out arraySegment);
    }

    public ArraySegment<byte> GetItemAsArraySegment(int index) {
      ArraySegment<byte> arraySegment;
      GetItemAsArraySegment(index, out arraySegment);
      return arraySegment;
    }

    public void GetItemAsByteBufferSegment(int index, out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetStringItemAsByteBufferSegment(index, out byteBufferSegment);
    }

    public ByteBufferSegment GetItemAsByteBufferSegment(int index) {
      ByteBufferSegment byteBufferSegment;
      GetItemAsByteBufferSegment(index, out byteBufferSegment);
      return byteBufferSegment;
    }

    public void GetAsArraySegment(out ArraySegment<byte> arraySegment) {
      _vectorAccessor.GetVectorAsArraySegment(out arraySegment);
    }

    public ArraySegment<byte> GetAsArraySegment() {
      ArraySegment<byte> arraySegment;
      GetAsArraySegment(out arraySegment);
      return arraySegment;
    }

    public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) {
      _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment);
    }

    public ByteBufferSegment GetAsByteBufferSegment() {
      ByteBufferSegment byteBufferSegment;
      GetAsByteBufferSegment(out byteBufferSegment);
      return byteBufferSegment;
    }

    public void CopyTo(string[] array, int offset) {
      CopyTo(0, array, offset, Length);
    }

    public void CopyTo(int srcOffset, string[] dstArray, int dstOffset, int count) {
      int length = Length;
      if ((uint)srcOffset > length ||
          (uint)count > length - srcOffset ||
          (uint)dstOffset > dstArray.Length || 
          (uint)count > dstArray.Length - dstOffset) {
        throw new IndexOutOfRangeException();
      }

      for (int i = 0; i < count; i++) {
        dstArray[dstOffset + i] = this[srcOffset + i];
      }
    }

    public string[] ToArray() {
      int length = Length;
      string[] array = new string[length];
      for (int i = 0; i < length; i++) {
        array[i] = this[i];
      }
      return array;
    }

    public List<string> ToList() {
      int length = Length;
      List<string> list = new List<string>(length);
      for (int i = 0; i < length; i++) {
        list.Add(this[i]);
      }
      return list;
    }

    public VectorEnumerator<string, StringVector> GetEnumerator() {
      return new VectorEnumerator<string, StringVector>(ref this);
    }

    IEnumerator<string> IEnumerable<string>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

    private VectorAccessor _vectorAccessor;
  }
}
