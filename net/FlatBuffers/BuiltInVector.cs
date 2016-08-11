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
