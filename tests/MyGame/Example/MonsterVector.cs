// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct MonsterVector : IFieldGroupVector<MonsterStruct> {
  private VectorAccessor _vectorAccessor;

  public MonsterVector(BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }
  public MonsterVector(ref BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }

  public VectorAccessor VectorAccessor{ get { return _vectorAccessor; } }
  public int Length { get { return _vectorAccessor.VectorDataLength; } }
  public ArraySegment<byte> GetAsArraySegment() { return _vectorAccessor.GetVectorAsArraySegment(); }
  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { _vectorAccessor.GetVectorAsArraySegment(out arraySegment); }
  public ByteBufferSegment GetAsByteBufferSegment() { return _vectorAccessor.GetVectorAsByteBufferSegment(); }
  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment); }
  public FieldGroupVectorEnumerator<MonsterStruct, MonsterVector> GetEnumerator() { return new FieldGroupVectorEnumerator<MonsterStruct, MonsterVector>(this); }
  System.Collections.Generic.IEnumerator<MonsterStruct> System.Collections.Generic.IEnumerable<MonsterStruct>.GetEnumerator() { return GetEnumerator(); }
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

  public void GetItem(int index, out MonsterStruct item) {
    BufferPosition itemPosition;
    _vectorAccessor.GetTableItem(index, out itemPosition);
    item = new MonsterStruct(ref itemPosition);
  }

  public MonsterStruct this[int index] {
    get { 
      MonsterStruct item;
      GetItem(index, out item);
      return item;
    }
    set { throw new NotSupportedException(); }
  }
}


}
