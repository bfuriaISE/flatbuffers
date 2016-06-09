using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatBuffers {
  public interface IVector {
    VectorAccessor VectorAccessor { get; }
    int Length { get; }
    ArraySegment<byte> GetAsArraySegment();
    void GetAsArraySegment(out ArraySegment<byte> arraySegment);
    ByteBufferSegment GetAsByteBufferSegment();
    void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment);
  }

  public interface IVector<TItem> : IVector, IEnumerable<TItem> {
    TItem this[int index] { get; set; }
  }

  public interface IFieldGroupVector<TItem> : IVector<TItem> {
    void GetItem(int index, out TItem item);
  }
}
