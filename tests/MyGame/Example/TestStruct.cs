// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct TestStruct {
  private BufferPosition _bufferPosition;

  public TestStruct(BufferPosition bufferPosition) { _bufferPosition = bufferPosition; }
  public TestStruct(ref BufferPosition bufferPosition) { _bufferPosition = bufferPosition; }

  public BufferPosition GetBufferPosition() { return _bufferPosition; }

  public short A { get { return _bufferPosition.GetShort(0); } }
  public void MutateA(short a) { _bufferPosition.PutShort(0, a); }
  public sbyte B { get { return _bufferPosition.GetSbyte(2); } }
  public void MutateB(sbyte b) { _bufferPosition.PutSbyte(2, b); }

}


}
