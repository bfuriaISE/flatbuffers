// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct TestSimpleTableWithEnumVector : IFieldGroupVector<TestSimpleTableWithEnumStruct> {
  private VectorAccessor _vectorAccessor;

  public TestSimpleTableWithEnumVector(BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }
  public TestSimpleTableWithEnumVector(ref BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }

  public VectorAccessor VectorAccessor { get { return _vectorAccessor; } }
  public int Length { get { return _vectorAccessor.VectorDataLength; } }
  public ArraySegment<byte> GetAsArraySegment() { return _vectorAccessor.GetVectorAsArraySegment(); }
  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { _vectorAccessor.GetVectorAsArraySegment(out arraySegment); }
  public ByteBufferSegment GetAsByteBufferSegment() { return _vectorAccessor.GetVectorAsByteBufferSegment(); }
  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment); }
  public FieldGroupVectorEnumerator<TestSimpleTableWithEnumStruct, TestSimpleTableWithEnumVector> GetEnumerator() { return new FieldGroupVectorEnumerator<TestSimpleTableWithEnumStruct, TestSimpleTableWithEnumVector>(ref this); }
  System.Collections.Generic.IEnumerator<TestSimpleTableWithEnumStruct> System.Collections.Generic.IEnumerable<TestSimpleTableWithEnumStruct>.GetEnumerator() { return GetEnumerator(); }
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

  public void GetItem(int index, out TestSimpleTableWithEnumStruct item) {
    BufferPosition itemPosition;
    _vectorAccessor.GetTableItem(index, out itemPosition);
    item = new TestSimpleTableWithEnumStruct(ref itemPosition);
  }

  public TestSimpleTableWithEnumStruct this[int index] {
    get { 
      TestSimpleTableWithEnumStruct item;
      GetItem(index, out item);
      return item;
    }
    set { throw new NotSupportedException(); }
  }
}


}
