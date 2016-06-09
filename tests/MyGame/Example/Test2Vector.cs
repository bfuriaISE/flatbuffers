// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct Test2Vector : IFieldGroupVector<Test2Struct> {
  private VectorAccessor _vectorAccessor;

  public Test2Vector(BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }
  public Test2Vector(ref BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }

  public VectorAccessor VectorAccessor{ get { return _vectorAccessor; } }
  public int Length { get { return _vectorAccessor.VectorDataLength; } }
  public ArraySegment<byte> GetAsArraySegment() { return _vectorAccessor.GetVectorAsArraySegment(); }
  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { _vectorAccessor.GetVectorAsArraySegment(out arraySegment); }
  public ByteBufferSegment GetAsByteBufferSegment() { return _vectorAccessor.GetVectorAsByteBufferSegment(); }
  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment); }
  public FieldGroupVectorEnumerator<Test2Struct, Test2Vector> GetEnumerator() { return new FieldGroupVectorEnumerator<Test2Struct, Test2Vector>(this); }
  System.Collections.Generic.IEnumerator<Test2Struct> System.Collections.Generic.IEnumerable<Test2Struct>.GetEnumerator() { return GetEnumerator(); }
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

  public void GetItem(int index, out Test2Struct item) {
    BufferPosition itemPosition;
    _vectorAccessor.GetStructItem(index, 1, out itemPosition);
    item = new Test2Struct(ref itemPosition);
  }

  public Test2Struct this[int index] {
    get { 
      Test2Struct item;
      GetItem(index, out item);
      return item;
    }
    set { throw new NotSupportedException(); }
  }
}


}
