// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using System.Collections.Generic;
using FlatBuffers;

public partial class TestSimpleTableWithEnum : Table {
  public static TestSimpleTableWithEnum GetRootAsTestSimpleTableWithEnum(ByteBuffer _bb) { return GetRootAsTestSimpleTableWithEnum(_bb, new TestSimpleTableWithEnum()); }
  public static TestSimpleTableWithEnum GetRootAsTestSimpleTableWithEnum(ByteBuffer _bb, TestSimpleTableWithEnum obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public static TestSimpleTableWithEnum GetRootAsTestSimpleTableWithEnum(ByteBufferSegment bbs) { return GetRootAsTestSimpleTableWithEnum(bbs, new TestSimpleTableWithEnum()); }
  public static TestSimpleTableWithEnum GetRootAsTestSimpleTableWithEnum(ByteBufferSegment bbs, TestSimpleTableWithEnum obj) { return (obj.__init(bbs.ByteBuffer.GetInt(bbs.Offset) + bbs.Offset, bbs.ByteBuffer)); }
  public TestSimpleTableWithEnum __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Color Color { get { int o = __offset(4); return o != 0 ? (Color)bb.GetSbyte(o + bb_pos) : Color.Green; } }
  public bool MutateColor(Color color) { int o = __offset(4); if (o != 0) { bb.PutSbyte(o + bb_pos, (sbyte)color); return true; } else { return false; } }

  public static Offset<TestSimpleTableWithEnum> CreateTestSimpleTableWithEnum(FlatBufferBuilder builder,
      Color color = Color.Green,
      bool enableVtableReuse = true) {
    builder.StartObject(1);
    TestSimpleTableWithEnum.AddColor(builder, color);
    return TestSimpleTableWithEnum.EndTestSimpleTableWithEnum(builder, enableVtableReuse);
  }

  public static void StartTestSimpleTableWithEnum(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddColor(FlatBufferBuilder builder, Color color) { builder.AddSbyte(0, (sbyte)color, 2); }
  public static Offset<TestSimpleTableWithEnum> EndTestSimpleTableWithEnum(FlatBufferBuilder builder, bool enableVtableReuse = true) {
    int o = builder.EndObject(enableVtableReuse);
    return new Offset<TestSimpleTableWithEnum>(o);
  }
};


}
