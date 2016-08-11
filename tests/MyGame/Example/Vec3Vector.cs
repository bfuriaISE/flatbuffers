// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct Vec3Vector : IFieldGroupVector<Vec3Struct> {
  private VectorAccessor _vectorAccessor;

  public Vec3Vector(BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }
  public Vec3Vector(ref BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }

  public VectorAccessor VectorAccessor { get { return _vectorAccessor; } }
  public int Length { get { return _vectorAccessor.VectorDataLength; } }
  public ArraySegment<byte> GetAsArraySegment() { return _vectorAccessor.GetVectorAsArraySegment(); }
  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { _vectorAccessor.GetVectorAsArraySegment(out arraySegment); }
  public ByteBufferSegment GetAsByteBufferSegment() { return _vectorAccessor.GetVectorAsByteBufferSegment(); }
  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment); }
  public FieldGroupVectorEnumerator<Vec3Struct, Vec3Vector> GetEnumerator() { return new FieldGroupVectorEnumerator<Vec3Struct, Vec3Vector>(ref this); }
  System.Collections.Generic.IEnumerator<Vec3Struct> System.Collections.Generic.IEnumerable<Vec3Struct>.GetEnumerator() { return GetEnumerator(); }
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

  public void GetItem(int index, out Vec3Struct item) {
    BufferPosition itemPosition;
    _vectorAccessor.GetStructItem(index, 32, out itemPosition);
    item = new Vec3Struct(ref itemPosition);
  }

  public Vec3Struct this[int index] {
    get { 
      Vec3Struct item;
      GetItem(index, out item);
      return item;
    }
    set { throw new NotSupportedException(); }
  }
}


}
