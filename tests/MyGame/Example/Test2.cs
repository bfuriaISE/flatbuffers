// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public sealed class Test2 : Struct {
  public Test2 __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public sbyte B { get { return bb.GetSbyte(bb_pos + 0); } }
  public void MutateB(sbyte b) { bb.PutSbyte(bb_pos + 0, b); }

  public static Offset<Test2> CreateTest2(FlatBufferBuilder builder, sbyte B) {
    builder.Prep(1, 1);
    builder.PutSbyte(B);
    return new Offset<Test2>(builder.Offset);
  }
};


}
