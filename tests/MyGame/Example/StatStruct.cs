// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

public struct StatStruct {
  private TableAccessor _tableAccessor;

  public StatStruct(BufferPosition bufferPosition) { _tableAccessor = new TableAccessor(ref bufferPosition); }
  public StatStruct(ref BufferPosition bufferPosition) { _tableAccessor = new TableAccessor(ref bufferPosition); }

  private StatStruct(ByteBuffer buffer) { TableAccessor.CreateFromOffset(buffer, out _tableAccessor); }
  private StatStruct(ref ByteBufferSegment segment) { TableAccessor.CreateFromOffset(ref segment, out _tableAccessor); }

  public static StatStruct GetRootAsStat(ByteBuffer buffer) { return new StatStruct(buffer); }
  public static void GetRootAsStat(ByteBuffer buffer, out StatStruct stat) { stat = new StatStruct(buffer); }
  public static StatStruct GetRootAsStat(ByteBufferSegment segment) { return new StatStruct(ref segment); }
  public static void GetRootAsStat(ByteBufferSegment segment, out StatStruct stat){ stat = new StatStruct(ref segment); }
  public static StatStruct GetRootAsStat(ref ByteBufferSegment segment) { return new StatStruct(ref segment); }
  public static void GetRootAsStat(ref ByteBufferSegment segment, out StatStruct stat) { stat = new StatStruct(ref segment); }

  public TableAccessor GetTableAccessor() { return _tableAccessor; }

  public string Id { get { return _tableAccessor.GetStringFieldValue(4); } }
  public ArraySegment<byte>? GetIdBytes() { return _tableAccessor.GetStringFieldValueAsArraySegment(4); }
  public ByteBufferSegment? GetIdBufferSegment() { return _tableAccessor.GetStringFieldValueAsByteBufferSegment(4); }
  public long Val { get { return _tableAccessor.GetLongFieldValue(6, 0); } }
  public bool MutateVal(long val) { return _tableAccessor.MutateLongFieldValue(6, val); }
  public bool IsValSpecified { get { return _tableAccessor.CheckField(6); } }
  public ushort Count { get { return _tableAccessor.GetUshortFieldValue(8, 0); } }
  public bool MutateCount(ushort count) { return _tableAccessor.MutateUshortFieldValue(8, count); }
  public bool IsCountSpecified { get { return _tableAccessor.CheckField(8); } }

}


}
