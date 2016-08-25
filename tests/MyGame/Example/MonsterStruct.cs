// automatically generated, do not modify

namespace MyGame.Example
{

using System;
using FlatBuffers;

/// an example documentation comment: monster object
public struct MonsterStruct {
  private TableAccessor _tableAccessor;

  public MonsterStruct(BufferPosition bufferPosition) { _tableAccessor = new TableAccessor(ref bufferPosition); }
  public MonsterStruct(ref BufferPosition bufferPosition) { _tableAccessor = new TableAccessor(ref bufferPosition); }

  private MonsterStruct(ByteBuffer buffer) { TableAccessor.CreateFromOffset(buffer, out _tableAccessor); }
  private MonsterStruct(ref ByteBufferSegment segment) { TableAccessor.CreateFromOffset(ref segment, out _tableAccessor); }

  public static MonsterStruct GetRootAsMonster(ByteBuffer buffer) { return new MonsterStruct(buffer); }
  public static void GetRootAsMonster(ByteBuffer buffer, out MonsterStruct monster) { monster = new MonsterStruct(buffer); }
  public static MonsterStruct GetRootAsMonster(ByteBufferSegment segment) { return new MonsterStruct(ref segment); }
  public static void GetRootAsMonster(ByteBufferSegment segment, out MonsterStruct monster){ monster = new MonsterStruct(ref segment); }
  public static MonsterStruct GetRootAsMonster(ref ByteBufferSegment segment) { return new MonsterStruct(ref segment); }
  public static void GetRootAsMonster(ref ByteBufferSegment segment, out MonsterStruct monster) { monster = new MonsterStruct(ref segment); }

  public static bool MonsterBufferHasIdentifier(ByteBuffer buffer) { return TableAccessor.HasIdentifier(buffer, "MONS"); }

  public TableAccessor GetTableAccessor() { return _tableAccessor; }

  public Vec3Struct? Pos {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetStructFieldValue(4, out position) ? new Vec3Struct(ref position): (Vec3Struct?)null;
    }
  }

  public bool TryGetPos(out Vec3Struct pos) {
    BufferPosition position;
    if (_tableAccessor.TryGetStructFieldValue(4, out position)) {
      pos = new Vec3Struct(ref position);
      return true;
    }
    pos = default(Vec3Struct);
    return false;
  }

  public short Mana { get { return _tableAccessor.GetShortFieldValue(6, 150); } }
  public bool MutateMana(short mana) { return _tableAccessor.MutateShortFieldValue(6, mana); }
  public bool IsManaSpecified { get { return _tableAccessor.CheckField(6); } }
  public short Hp { get { return _tableAccessor.GetShortFieldValue(8, 100); } }
  public bool MutateHp(short hp) { return _tableAccessor.MutateShortFieldValue(8, hp); }
  public bool IsHpSpecified { get { return _tableAccessor.CheckField(8); } }
  public string Name { get { return _tableAccessor.GetStringFieldValue(10); } }
  public ArraySegment<byte>? GetNameBytes() { return _tableAccessor.GetStringFieldValueAsArraySegment(10); }
  public ByteBufferSegment? GetNameBufferSegment() { return _tableAccessor.GetStringFieldValueAsByteBufferSegment(10); }
  public ByteVector? Inventory {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(14, out position) ? new ByteVector(ref position): (ByteVector?)null;
    }
  }

  public bool TryGetInventory(out ByteVector inventory) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(14, out position)) {
      inventory = new ByteVector(ref position);
      return true;
    }
    inventory = default(ByteVector);
    return false;
  }

  public Color Color { get { return (Color)_tableAccessor.GetSbyteFieldValue(16, 8); } }
  public bool MutateColor(Color color) { return _tableAccessor.MutateSbyteFieldValue(16, (sbyte)color); }
  public bool IsColorSpecified { get { return _tableAccessor.CheckField(16); } }
  public Any TestType { get { return (Any)_tableAccessor.GetByteFieldValue(18, 0); } }
  public bool MutateTestType(Any testType) { return _tableAccessor.MutateByteFieldValue(18, (byte)testType); }
  public bool IsTestTypeSpecified { get { return _tableAccessor.CheckField(18); } }
  public MonsterStruct? TestAsMonster {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetTableFieldValue(20, out position) ? new MonsterStruct(ref position): (MonsterStruct?)null;
    }
  }

  public bool TryGetTestAsMonster(out MonsterStruct testAsMonster) {
    BufferPosition position;
    if (_tableAccessor.TryGetTableFieldValue(20, out position)) {
      testAsMonster = new MonsterStruct(ref position);
      return true;
    }
    testAsMonster = default(MonsterStruct);
    return false;
  }

  public TestSimpleTableWithEnumStruct? TestAsTestSimpleTableWithEnum {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetTableFieldValue(20, out position) ? new TestSimpleTableWithEnumStruct(ref position): (TestSimpleTableWithEnumStruct?)null;
    }
  }

  public bool TryGetTestAsTestSimpleTableWithEnum(out TestSimpleTableWithEnumStruct testAsTestSimpleTableWithEnum) {
    BufferPosition position;
    if (_tableAccessor.TryGetTableFieldValue(20, out position)) {
      testAsTestSimpleTableWithEnum = new TestSimpleTableWithEnumStruct(ref position);
      return true;
    }
    testAsTestSimpleTableWithEnum = default(TestSimpleTableWithEnumStruct);
    return false;
  }

  public TestVector? Test4 {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(22, out position) ? new TestVector(ref position): (TestVector?)null;
    }
  }

  public bool TryGetTest4(out TestVector test4) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(22, out position)) {
      test4 = new TestVector(ref position);
      return true;
    }
    test4 = default(TestVector);
    return false;
  }

  public StringVector? Testarrayofstring {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(24, out position) ? new StringVector(ref position): (StringVector?)null;
    }
  }

  public bool TryGetTestarrayofstring(out StringVector testarrayofstring) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(24, out position)) {
      testarrayofstring = new StringVector(ref position);
      return true;
    }
    testarrayofstring = default(StringVector);
    return false;
  }

  /// an example documentation comment: this will end up in the generated code
  /// multiline too
  public MonsterVector? Testarrayoftables {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(26, out position) ? new MonsterVector(ref position): (MonsterVector?)null;
    }
  }

  public bool TryGetTestarrayoftables(out MonsterVector testarrayoftables) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(26, out position)) {
      testarrayoftables = new MonsterVector(ref position);
      return true;
    }
    testarrayoftables = default(MonsterVector);
    return false;
  }

  public MonsterStruct? Enemy {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetTableFieldValue(28, out position) ? new MonsterStruct(ref position): (MonsterStruct?)null;
    }
  }

  public bool TryGetEnemy(out MonsterStruct enemy) {
    BufferPosition position;
    if (_tableAccessor.TryGetTableFieldValue(28, out position)) {
      enemy = new MonsterStruct(ref position);
      return true;
    }
    enemy = default(MonsterStruct);
    return false;
  }

  public ByteVector? Testnestedflatbuffer {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(30, out position) ? new ByteVector(ref position): (ByteVector?)null;
    }
  }

  public bool TryGetTestnestedflatbuffer(out ByteVector testnestedflatbuffer) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(30, out position)) {
      testnestedflatbuffer = new ByteVector(ref position);
      return true;
    }
    testnestedflatbuffer = default(ByteVector);
    return false;
  }

  public MonsterStruct? TestnestedflatbufferAsMonster {
    get {
      MonsterStruct testnestedflatbuffer;
      return TryGetTestnestedflatbufferAsMonster(out testnestedflatbuffer) ? testnestedflatbuffer : (MonsterStruct?)null;
    }
  }

  public bool TryGetTestnestedflatbufferAsMonster(out MonsterStruct testnestedflatbuffer) {
    BufferPosition vectorPosition;
    if (_tableAccessor.TryGetVectorFieldValue(30, out vectorPosition)) {
      BufferPosition tablePosition;
      VectorAccessor.GetNestedFlatBufferTable(ref vectorPosition, out tablePosition);
      testnestedflatbuffer = new MonsterStruct(ref tablePosition);
      return true;
    }
    testnestedflatbuffer = default(MonsterStruct);
    return false;
  }

  public StatStruct? Testempty {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetTableFieldValue(32, out position) ? new StatStruct(ref position): (StatStruct?)null;
    }
  }

  public bool TryGetTestempty(out StatStruct testempty) {
    BufferPosition position;
    if (_tableAccessor.TryGetTableFieldValue(32, out position)) {
      testempty = new StatStruct(ref position);
      return true;
    }
    testempty = default(StatStruct);
    return false;
  }

  public bool Testbool { get { return _tableAccessor.GetBoolFieldValue(34, false); } }
  public bool MutateTestbool(bool testbool) { return _tableAccessor.MutateBoolFieldValue(34, testbool); }
  public bool IsTestboolSpecified { get { return _tableAccessor.CheckField(34); } }
  public int Testhashs32Fnv1 { get { return _tableAccessor.GetIntFieldValue(36, 0); } }
  public bool MutateTesthashs32Fnv1(int testhashs32Fnv1) { return _tableAccessor.MutateIntFieldValue(36, testhashs32Fnv1); }
  public bool IsTesthashs32Fnv1Specified { get { return _tableAccessor.CheckField(36); } }
  public uint Testhashu32Fnv1 { get { return _tableAccessor.GetUintFieldValue(38, 0); } }
  public bool MutateTesthashu32Fnv1(uint testhashu32Fnv1) { return _tableAccessor.MutateUintFieldValue(38, testhashu32Fnv1); }
  public bool IsTesthashu32Fnv1Specified { get { return _tableAccessor.CheckField(38); } }
  public long Testhashs64Fnv1 { get { return _tableAccessor.GetLongFieldValue(40, 0); } }
  public bool MutateTesthashs64Fnv1(long testhashs64Fnv1) { return _tableAccessor.MutateLongFieldValue(40, testhashs64Fnv1); }
  public bool IsTesthashs64Fnv1Specified { get { return _tableAccessor.CheckField(40); } }
  public ulong Testhashu64Fnv1 { get { return _tableAccessor.GetUlongFieldValue(42, 0); } }
  public bool MutateTesthashu64Fnv1(ulong testhashu64Fnv1) { return _tableAccessor.MutateUlongFieldValue(42, testhashu64Fnv1); }
  public bool IsTesthashu64Fnv1Specified { get { return _tableAccessor.CheckField(42); } }
  public int Testhashs32Fnv1a { get { return _tableAccessor.GetIntFieldValue(44, 0); } }
  public bool MutateTesthashs32Fnv1a(int testhashs32Fnv1a) { return _tableAccessor.MutateIntFieldValue(44, testhashs32Fnv1a); }
  public bool IsTesthashs32Fnv1aSpecified { get { return _tableAccessor.CheckField(44); } }
  public uint Testhashu32Fnv1a { get { return _tableAccessor.GetUintFieldValue(46, 0); } }
  public bool MutateTesthashu32Fnv1a(uint testhashu32Fnv1a) { return _tableAccessor.MutateUintFieldValue(46, testhashu32Fnv1a); }
  public bool IsTesthashu32Fnv1aSpecified { get { return _tableAccessor.CheckField(46); } }
  public long Testhashs64Fnv1a { get { return _tableAccessor.GetLongFieldValue(48, 0); } }
  public bool MutateTesthashs64Fnv1a(long testhashs64Fnv1a) { return _tableAccessor.MutateLongFieldValue(48, testhashs64Fnv1a); }
  public bool IsTesthashs64Fnv1aSpecified { get { return _tableAccessor.CheckField(48); } }
  public ulong Testhashu64Fnv1a { get { return _tableAccessor.GetUlongFieldValue(50, 0); } }
  public bool MutateTesthashu64Fnv1a(ulong testhashu64Fnv1a) { return _tableAccessor.MutateUlongFieldValue(50, testhashu64Fnv1a); }
  public bool IsTesthashu64Fnv1aSpecified { get { return _tableAccessor.CheckField(50); } }
  public BoolVector? Testarrayofbools {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(52, out position) ? new BoolVector(ref position): (BoolVector?)null;
    }
  }

  public bool TryGetTestarrayofbools(out BoolVector testarrayofbools) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(52, out position)) {
      testarrayofbools = new BoolVector(ref position);
      return true;
    }
    testarrayofbools = default(BoolVector);
    return false;
  }

  public float Testf { get { return _tableAccessor.GetFloatFieldValue(54, 3.14159f); } }
  public bool MutateTestf(float testf) { return _tableAccessor.MutateFloatFieldValue(54, testf); }
  public bool IsTestfSpecified { get { return _tableAccessor.CheckField(54); } }
  public ByteVector? Testarrayofbytes {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(56, out position) ? new ByteVector(ref position): (ByteVector?)null;
    }
  }

  public bool TryGetTestarrayofbytes(out ByteVector testarrayofbytes) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(56, out position)) {
      testarrayofbytes = new ByteVector(ref position);
      return true;
    }
    testarrayofbytes = default(ByteVector);
    return false;
  }

  public BoolVector? Testarrayofbools1 {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(58, out position) ? new BoolVector(ref position): (BoolVector?)null;
    }
  }

  public bool TryGetTestarrayofbools1(out BoolVector testarrayofbools1) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(58, out position)) {
      testarrayofbools1 = new BoolVector(ref position);
      return true;
    }
    testarrayofbools1 = default(BoolVector);
    return false;
  }

  public ShortVector? Testarrayofshorts {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(60, out position) ? new ShortVector(ref position): (ShortVector?)null;
    }
  }

  public bool TryGetTestarrayofshorts(out ShortVector testarrayofshorts) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(60, out position)) {
      testarrayofshorts = new ShortVector(ref position);
      return true;
    }
    testarrayofshorts = default(ShortVector);
    return false;
  }

  public IntVector? Testarrayofints {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(62, out position) ? new IntVector(ref position): (IntVector?)null;
    }
  }

  public bool TryGetTestarrayofints(out IntVector testarrayofints) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(62, out position)) {
      testarrayofints = new IntVector(ref position);
      return true;
    }
    testarrayofints = default(IntVector);
    return false;
  }

  public LongVector? Testarrayoflongs {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(64, out position) ? new LongVector(ref position): (LongVector?)null;
    }
  }

  public bool TryGetTestarrayoflongs(out LongVector testarrayoflongs) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(64, out position)) {
      testarrayoflongs = new LongVector(ref position);
      return true;
    }
    testarrayoflongs = default(LongVector);
    return false;
  }

  public FloatVector? Testarrayoffloats {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(66, out position) ? new FloatVector(ref position): (FloatVector?)null;
    }
  }

  public bool TryGetTestarrayoffloats(out FloatVector testarrayoffloats) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(66, out position)) {
      testarrayoffloats = new FloatVector(ref position);
      return true;
    }
    testarrayoffloats = default(FloatVector);
    return false;
  }

  public DoubleVector? Testarrayofdoubles {
    get {
      BufferPosition position;
      return _tableAccessor.TryGetVectorFieldValue(68, out position) ? new DoubleVector(ref position): (DoubleVector?)null;
    }
  }

  public bool TryGetTestarrayofdoubles(out DoubleVector testarrayofdoubles) {
    BufferPosition position;
    if (_tableAccessor.TryGetVectorFieldValue(68, out position)) {
      testarrayofdoubles = new DoubleVector(ref position);
      return true;
    }
    testarrayofdoubles = default(DoubleVector);
    return false;
  }


}


}
