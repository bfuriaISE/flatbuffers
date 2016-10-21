using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatBuffers {
  public struct VectorEnumerator<TItem, TVector> : IEnumerator<TItem> 
      where TVector : IVector<TItem> {

    public VectorEnumerator(TVector vector) {
      m_vector = vector;
      m_index = 0;
      m_length = -1;
      m_current = default(TItem);
    }

    public VectorEnumerator(ref TVector vector) {
      m_vector = vector;
      m_index = 0;
      m_length = -1;
      m_current = default(TItem);
    }

    public TItem Current {
      get { return m_current; }
    }

    public void Dispose() {
      
    }

    object System.Collections.IEnumerator.Current {
      get { return Current; }
    }

    public bool MoveNext() {
      if (m_index >= m_length && (m_length != -1 || (m_length = m_vector.Length) == 0)) {
        m_current = default(TItem);
        return false;
      }
      m_current = m_vector[m_index++];
      return true;
    }

    public void Reset() {
      m_index = 0;
      m_current = default(TItem);
    }


    private TVector m_vector;
    private int m_index;
    private int m_length;
    private TItem m_current;
  }


  public struct FieldGroupVectorEnumerator<TItem, TVector> : IEnumerator<TItem>
      where TVector : IFieldGroupVector<TItem> {

    public FieldGroupVectorEnumerator(TVector vector) {
      m_vector = vector;
      m_index = 0;
      m_length = -1;
      m_current = default(TItem);
    }

    public FieldGroupVectorEnumerator(ref TVector vector) {
      m_vector = vector;
      m_index = 0;
      m_length = -1;
      m_current = default(TItem);
    }

    public TItem Current {
      get { return m_current; }
    }

    public void Dispose() {

    }

    object System.Collections.IEnumerator.Current {
      get { return Current; }
    }

    public bool MoveNext() {
      if (m_index >= m_length && (m_length != -1 || (m_length = m_vector.Length) == 0)) {
        m_current = default(TItem);
        return false;
      }
      m_vector.GetItem(m_index++, out m_current);
      return true;
    }

    public void Reset() {
      m_index = 0;
      m_current = default(TItem);
    }


    private TVector m_vector;
    private int m_index;
    private int m_length;
    private TItem m_current;
  }
}
