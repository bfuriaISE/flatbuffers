// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct AnyVector : IVector<Any> {
  private VectorAccessor _vectorAccessor;

  public AnyVector(BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }
  public AnyVector(ref BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }

  public VectorAccessor VectorAccessor{ get { return _vectorAccessor; } }
  public int Length { get { return _vectorAccessor.VectorDataLength; } }
  public ArraySegment<byte> GetAsArraySegment() { return _vectorAccessor.GetVectorAsArraySegment(); }
  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { _vectorAccessor.GetVectorAsArraySegment(out arraySegment); }
  public ByteBufferSegment GetAsByteBufferSegment() { return _vectorAccessor.GetVectorAsByteBufferSegment(); }
  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment); }
  public VectorEnumerator<Any, AnyVector> GetEnumerator() { return new VectorEnumerator<Any, AnyVector>(this); }
  System.Collections.Generic.IEnumerator<Any> System.Collections.Generic.IEnumerable<Any>.GetEnumerator() { return GetEnumerator(); }
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

  public Any this[int index] {
    get { return (Any)_vectorAccessor.GetByteItem(index); }
    set { _vectorAccessor.PutByteItem(index, (byte)value); }
  }
}


}
