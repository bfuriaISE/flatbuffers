// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct TestVector : IFieldGroupVector<TestStruct> {
  private VectorAccessor _vectorAccessor;

  public TestVector(BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }
  public TestVector(ref BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }

  public VectorAccessor VectorAccessor { get { return _vectorAccessor; } }
  public int Length { get { return _vectorAccessor.VectorDataLength; } }
  public ArraySegment<byte> GetAsArraySegment() { return _vectorAccessor.GetVectorAsArraySegment(); }
  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { _vectorAccessor.GetVectorAsArraySegment(out arraySegment); }
  public ByteBufferSegment GetAsByteBufferSegment() { return _vectorAccessor.GetVectorAsByteBufferSegment(); }
  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment); }
  public FieldGroupVectorEnumerator<TestStruct, TestVector> GetEnumerator() { return new FieldGroupVectorEnumerator<TestStruct, TestVector>(ref this); }
  System.Collections.Generic.IEnumerator<TestStruct> System.Collections.Generic.IEnumerable<TestStruct>.GetEnumerator() { return GetEnumerator(); }
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

  public void GetItem(int index, out TestStruct item) {
    BufferPosition itemPosition;
    _vectorAccessor.GetStructItem(index, 4, out itemPosition);
    item = new TestStruct(ref itemPosition);
  }

  public TestStruct this[int index] {
    get { 
      TestStruct item;
      GetItem(index, out item);
      return item;
    }
    set { throw new NotSupportedException(); }
  }
}


}
