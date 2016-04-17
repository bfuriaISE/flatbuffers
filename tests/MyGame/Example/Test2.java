// automatically generated, do not modify

package MyGame.Example;

import java.nio.*;
import java.lang.*;
import java.util.*;
import com.google.flatbuffers.*;

@SuppressWarnings("unused")
public final class Test2 extends Struct {
  public Test2 __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public byte b() { return bb.get(bb_pos + 0); }
  public void mutateB(byte b) { bb.put(bb_pos + 0, b); }

  public static int createTest2(FlatBufferBuilder builder, byte b) {
    builder.prep(1, 1);
    builder.putByte(b);
    return builder.offset();
  }
};

