// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct Vec3Struct {
  private BufferPosition _bufferPosition;

  public Vec3Struct(BufferPosition bufferPosition) { _bufferPosition = bufferPosition; }
  public Vec3Struct(ref BufferPosition bufferPosition) { _bufferPosition = bufferPosition; }

  public BufferPosition GetBufferPosition() { return _bufferPosition; }

  public float X { get { return _bufferPosition.GetFloat(0); } }
  public void MutateX(float x) { _bufferPosition.PutFloat(0, x); }
  public float Y { get { return _bufferPosition.GetFloat(4); } }
  public void MutateY(float y) { _bufferPosition.PutFloat(4, y); }
  public float Z { get { return _bufferPosition.GetFloat(8); } }
  public void MutateZ(float z) { _bufferPosition.PutFloat(8, z); }
  public double Test1 { get { return _bufferPosition.GetDouble(16); } }
  public void MutateTest1(double test1) { _bufferPosition.PutDouble(16, test1); }
  public Color Test2 { get { return (Color)_bufferPosition.GetSbyte(24); } }
  public void MutateTest2(Color test2) { _bufferPosition.PutSbyte(24, (sbyte)test2); }
  public TestStruct Test3 { get { return new TestStruct(_bufferPosition.Create(26)); } }

  public void GetTest3(out TestStruct test3) {
    BufferPosition position;
    _bufferPosition.Create(26, out position);
    test3 = new TestStruct(ref position);
  }


}


}
