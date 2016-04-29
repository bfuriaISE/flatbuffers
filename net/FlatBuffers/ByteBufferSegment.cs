using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatBuffers {
  public struct ByteBufferSegment {
    public ByteBufferSegment(ByteBuffer byteBuffer, int offset, int count) {
      if (byteBuffer == null)
        throw new ArgumentNullException("byteBuffer");
      if ((uint)offset > byteBuffer.Length)
        throw new ArgumentOutOfRangeException("offset");
      if ((uint)count > byteBuffer.Length - offset)
        throw new ArgumentOutOfRangeException("count");

      _byteBuffer = byteBuffer;
      _offset = offset;
      _count = count;
    }

    public ByteBufferSegment(ByteBuffer byteBuffer) 
        : this(byteBuffer, byteBuffer.Position, byteBuffer.Length) {
      
    }

    public ByteBuffer ByteBuffer {
      get { return _byteBuffer; }
    }

    public int Offset {
      get { return _offset; }
    }

    public int Count {
      get { return _count; }
    }


    private readonly ByteBuffer _byteBuffer;
    private readonly int _offset;
    private readonly int _count;
  }
}
