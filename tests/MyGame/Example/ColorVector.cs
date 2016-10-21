// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct ColorVector : IVector<Color> {
  private VectorAccessor _vectorAccessor;

  public ColorVector(BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }
  public ColorVector(ref BufferPosition vectorPosition) { _vectorAccessor = new VectorAccessor(ref vectorPosition); }

  public VectorAccessor VectorAccessor { get { return _vectorAccessor; } }
  public int Length { get { return _vectorAccessor.VectorDataLength; } }
  public ArraySegment<byte> GetAsArraySegment() { return _vectorAccessor.GetVectorAsArraySegment(); }
  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { _vectorAccessor.GetVectorAsArraySegment(out arraySegment); }
  public ByteBufferSegment GetAsByteBufferSegment() { return _vectorAccessor.GetVectorAsByteBufferSegment(); }
  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { _vectorAccessor.GetVectorAsByteBufferSegment(out byteBufferSegment); }
  public VectorEnumerator<Color, ColorVector> GetEnumerator() { return new VectorEnumerator<Color, ColorVector>(ref this); }
  System.Collections.Generic.IEnumerator<Color> System.Collections.Generic.IEnumerable<Color>.GetEnumerator() { return GetEnumerator(); }
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

  public Color this[int index] {
    get { return (Color)_vectorAccessor.GetSbyteItem(index); }
    set { _vectorAccessor.PutSbyteItem(index, (sbyte)value); }
  }
}


}
