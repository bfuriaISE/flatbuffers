// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public partial struct TestSimpleTableWithEnumStruct {
  private TableAccessor _tableAccessor;

  public TestSimpleTableWithEnumStruct(BufferPosition bufferPosition) { _tableAccessor = new TableAccessor(ref bufferPosition); }
  public TestSimpleTableWithEnumStruct(ref BufferPosition bufferPosition) { _tableAccessor = new TableAccessor(ref bufferPosition); }

  private TestSimpleTableWithEnumStruct(ByteBuffer buffer) { TableAccessor.CreateFromOffset(buffer, out _tableAccessor); }
  private TestSimpleTableWithEnumStruct(ref ByteBufferSegment segment) { TableAccessor.CreateFromOffset(ref segment, out _tableAccessor); }

  public static TestSimpleTableWithEnumStruct GetRootAsTestSimpleTableWithEnum(ByteBuffer buffer) { return new TestSimpleTableWithEnumStruct(buffer); }
  public static void GetRootAsTestSimpleTableWithEnum(ByteBuffer buffer, out TestSimpleTableWithEnumStruct testSimpleTableWithEnum) { testSimpleTableWithEnum = new TestSimpleTableWithEnumStruct(buffer); }
  public static TestSimpleTableWithEnumStruct GetRootAsTestSimpleTableWithEnum(ByteBufferSegment segment) { return new TestSimpleTableWithEnumStruct(ref segment); }
  public static void GetRootAsTestSimpleTableWithEnum(ByteBufferSegment segment, out TestSimpleTableWithEnumStruct testSimpleTableWithEnum){ testSimpleTableWithEnum = new TestSimpleTableWithEnumStruct(ref segment); }
  public static TestSimpleTableWithEnumStruct GetRootAsTestSimpleTableWithEnum(ref ByteBufferSegment segment) { return new TestSimpleTableWithEnumStruct(ref segment); }
  public static void GetRootAsTestSimpleTableWithEnum(ref ByteBufferSegment segment, out TestSimpleTableWithEnumStruct testSimpleTableWithEnum) { testSimpleTableWithEnum = new TestSimpleTableWithEnumStruct(ref segment); }

  public TableAccessor GetTableAccessor() { return _tableAccessor; }

  public Color Color { get { return (Color)_tableAccessor.GetSbyteFieldValue(4, 2); } }
  public bool MutateColor(Color color) { return _tableAccessor.MutateSbyteFieldValue(4, (sbyte)color); }
  public bool IsColorSpecified { get { return _tableAccessor.CheckField(4); } }

}


}
