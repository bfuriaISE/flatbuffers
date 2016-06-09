// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct Test2Struct {
  private BufferPosition _bufferPosition;

  public Test2Struct(BufferPosition bufferPosition) { _bufferPosition = bufferPosition; }
  public Test2Struct(ref BufferPosition bufferPosition) { _bufferPosition = bufferPosition; }

  public BufferPosition GetBufferPosition() { return _bufferPosition; }

  public sbyte B { get { return _bufferPosition.GetSbyte(0); } }
  public void MutateB(sbyte b) { _bufferPosition.PutSbyte(0, b); }

}


}
