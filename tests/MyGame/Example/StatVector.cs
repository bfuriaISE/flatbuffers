// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct StatVector : IFieldGroupVector<StatStruct> {
  private VectorAccessor _vectorAccessor;

  public StatVector(BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }
  public StatVector(ref BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }

  public VectorAccessor VectorAccessor { get { return _vectorAccessor; } }
  public int Length { get { return _vectorAccessor.VectorDataLength; } }
  public ArraySegment<byte> GetAsArraySegment() { return _vectorAccessor.GetVectorAsArraySegment(); }
  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { _vectorAccessor.GetVectorAsArraySegment(out arraySegment); }
  public ByteBufferSegment GetAsByteBufferSegment() { return _vectorAccessor.GetVectorAsByteBufferSegment(); }
  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment); }
  public FieldGroupVectorEnumerator<StatStruct, StatVector> GetEnumerator() { return new FieldGroupVectorEnumerator<StatStruct, StatVector>(ref this); }
  System.Collections.Generic.IEnumerator<StatStruct> System.Collections.Generic.IEnumerable<StatStruct>.GetEnumerator() { return GetEnumerator(); }
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

  public void GetItem(int index, out StatStruct item) {
    BufferPosition itemPosition;
    _vectorAccessor.GetTableItem(index, out itemPosition);
    item = new StatStruct(ref itemPosition);
  }

  public StatStruct this[int index] {
    get { 
      StatStruct item;
      GetItem(index, out item);
      return item;
    }
    set { throw new NotSupportedException(); }
  }
}


}
