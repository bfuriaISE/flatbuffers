/*
 * Copyright 2014 Google Inc. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#define UNSAFE_BYTEBUFFER     // uncomment this line to use faster ByteBuffer
#define ASSUME_LITTLE_ENDIAN  // uncomment this line to disable endianness check
#define PINNED_BYTEBUFFER     // uncomment this line to enable buffer pinning and working with unmanaged memory

#if ASSUME_LITTLE_ENDIAN
#pragma warning disable 162, 429 // disables unreachable code warnings CS0162, CS0429
#endif

using System;
using System.Text;

#if PINNED_BYTEBUFFER
using System.Runtime.InteropServices;
using System.Security;
#endif


namespace FlatBuffers
{
    /// <summary>
    /// Class to mimic Java's ByteBuffer which is used heavily in Flatbuffers.
    /// If your execution environment allows unsafe code, you should enable
    /// unsafe code in your project and #define UNSAFE_BYTEBUFFER to use a
    /// MUCH faster version of ByteBuffer.
    /// </summary>
    public class ByteBuffer : IDisposable
    {
#if PINNED_BYTEBUFFER
        // Need to P/Invoke memmove to handle case when dealing with two ByteBuffers that point
        // to unmanaged memory.
        [DllImport("msvcrt.dll", SetLastError=false)]
        [SuppressUnmanagedCodeSecurity]
        static extern IntPtr Memmove(IntPtr dest, IntPtr src, int count);

        private readonly IntPtr _bufferPtr;
        private readonly int _bufferLength;
        private GCHandle _pinningHandle;
#endif
        private bool _isDisposed;
        private readonly byte[] _buffer;
        private int _pos;  // Must track start of the buffer.

        public int Length 
        {
            get 
            {
                return
#if PINNED_BYTEBUFFER
                    _bufferLength;
#else
                    _buffer.Length;
#endif
            }
        }
        
        /// <summary>
        /// Returns the internal managed byte array. Note: this may be null if PINNED_BYTEBUFFER is 
        /// defined and the internal buffer is an unmanaged byte array.
        /// </summary>
        public byte[] Data { get { return _buffer; } }

#if PINNED_BYTEBUFFER
        /// <summary>
        /// Returns a pointer to the internal buffer.
        /// </summary>
        public IntPtr DataPtr { get { return _bufferPtr; } }
#endif

        public ByteBuffer(byte[] buffer) : this(buffer, 0) { }

        public ByteBuffer(byte[] buffer, int pos)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if ((uint)pos > buffer.Length)
                throw new ArgumentOutOfRangeException("pos");
            _buffer = buffer;
            _pos = pos;
#if PINNED_BYTEBUFFER
            _bufferLength = buffer.Length;
            _pinningHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
            _bufferPtr = _pinningHandle.AddrOfPinnedObject();
#endif
        }

#if PINNED_BYTEBUFFER
        public ByteBuffer(IntPtr bufferPtr, int pos, int bufferLength) 
        {
            if (bufferPtr == IntPtr.Zero)
                throw new ArgumentNullException("bufferPtr");
            if (bufferLength < 0)
                throw new ArgumentOutOfRangeException("bufferLength");
            if ((uint)pos > bufferLength)
                throw new ArgumentOutOfRangeException("pos");
            _bufferPtr = bufferPtr;
            _pos = pos;
            _bufferLength = bufferLength;
            GC.SuppressFinalize(this);
        }

        public ByteBuffer(IntPtr bufferPtr, int bufferLength) : this(bufferPtr, 0, bufferLength)
        {

        }

        ~ByteBuffer() 
        {
            Dispose(false);
        }
#endif
        public void Dispose() 
        {
            Dispose(true);
#if PINNED_BYTEBUFFER
            GC.SuppressFinalize(this);
#endif
        }

        protected void Dispose(bool disposing) 
        {
            if (!_isDisposed) 
            {
                _isDisposed = true;

#if PINNED_BYTEBUFFER
                if (_buffer != null) 
                {
                    _pinningHandle.Free();
                }
#endif
            }
        }

#if ASSUME_LITTLE_ENDIAN
        static ByteBuffer() 
        {
            if (!BitConverter.IsLittleEndian) 
            {
                throw new NotSupportedException(
                    "ByteBuffer compiled to only support little endian architectures. " + 
                      "Re-compile without defining ASSUME_LITTLE_ENDIAN.");
            }
        }
#endif

        public bool IsDisposed 
        {
            get { return _isDisposed; }
        }

        public int Position {
            get { return _pos; }
            set { _pos = value; }
        }

        public void Reset()
        {
            _pos = 0;
        }

        // Pre-allocated helper arrays for convertion.
        private float[] floathelper = new[] { 0.0f };
        private int[] inthelper = new[] { 0 };
        private double[] doublehelper = new[] { 0.0 };
        private ulong[] ulonghelper = new[] { 0UL };

        // Helper functions for the unsafe version.
        static public ushort ReverseBytes(ushort input)
        {
            return (ushort)(((input & 0x00FFU) << 8) |
                            ((input & 0xFF00U) >> 8));
        }
        static public uint ReverseBytes(uint input)
        {
            return ((input & 0x000000FFU) << 24) |
                   ((input & 0x0000FF00U) <<  8) |
                   ((input & 0x00FF0000U) >>  8) |
                   ((input & 0xFF000000U) >> 24);
        }
        static public ulong ReverseBytes(ulong input)
        {
            return (((input & 0x00000000000000FFUL) << 56) |
                    ((input & 0x000000000000FF00UL) << 40) |
                    ((input & 0x0000000000FF0000UL) << 24) |
                    ((input & 0x00000000FF000000UL) <<  8) |
                    ((input & 0x000000FF00000000UL) >>  8) |
                    ((input & 0x0000FF0000000000UL) >> 24) |
                    ((input & 0x00FF000000000000UL) >> 40) |
                    ((input & 0xFF00000000000000UL) >> 56));
        }

#if !UNSAFE_BYTEBUFFER && !PINNED_BYTEBUFFER
        // Helper functions for the safe (but slower) version.
        protected void WriteLittleEndian(int offset, int count, ulong data)
        {
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
                for (int i = 0; i < count; i++)
                {
                    _buffer[offset + i] = (byte)(data >> i * 8);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    _buffer[offset + count - 1 - i] = (byte)(data >> i * 8);
                }
            }
        }

        protected ulong ReadLittleEndian(int offset, int count)
        {
            AssertOffsetAndLength(offset, count);
            ulong r = 0;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
                for (int i = 0; i < count; i++)
                {
                  r |= (ulong)_buffer[offset + i] << i * 8;
                }
            }
            else
            {
              for (int i = 0; i < count; i++)
              {
                r |= (ulong)_buffer[offset + count - 1 - i] << i * 8;
              }
            }
            return r;
        }
#endif // !UNSAFE_BYTEBUFFER && !PINNED_BYTEBUFFER

        private static void ThrowOffsetAndLengthAssertionFailed() 
        {
            throw new ArgumentOutOfRangeException();
        }

        private void AssertOffsetAndLength(int offset, int length) 
        {
            int bufferLength =
#if PINNED_BYTEBUFFER
                _bufferLength;
#else
                _buffer.Length;
#endif
            if (_isDisposed ||
                (uint)offset > bufferLength ||
                (uint)length > bufferLength - offset)
            { 
                ThrowOffsetAndLengthAssertionFailed();
            }
        }

        private static void AssertOffsetAndLength<T>(T[] array, int offset, int count) 
        {
            if ((uint)offset > array.Length ||
                (uint)count > array.Length - offset) 
            { 
                ThrowOffsetAndLengthAssertionFailed();
            }
        }

#if PINNED_BYTEBUFFER
        public unsafe void PutSbyte(int offset, sbyte value)
#else
        public void PutSbyte(int offset, sbyte value)
#endif
        {
            AssertOffsetAndLength(offset, sizeof(sbyte));
#if PINNED_BYTEBUFFER
            *((sbyte*)_bufferPtr.ToPointer() + offset) = value;
#else
            _buffer[offset] = (byte)value;
#endif
        }

#if PINNED_BYTEBUFFER
        public unsafe void PutByte(int offset, byte value)
#else
        public void PutByte(int offset, byte value)
#endif
        {
            AssertOffsetAndLength(offset, sizeof(byte));
#if PINNED_BYTEBUFFER
            *((byte*)_bufferPtr.ToPointer() + offset) = value;
#else
            _buffer[offset] = (byte)value;
#endif
        }

#if PINNED_BYTEBUFFER
        public unsafe void PutByte(int offset, byte value, int count)
#else
        public void PutByte(int offset, byte value, int count)
#endif
        {
            AssertOffsetAndLength(offset, sizeof(byte) * count);
            for (int endOffset = offset + count; offset < endOffset; ++offset) 
            { 
#if PINNED_BYTEBUFFER
                *((byte*)_bufferPtr.ToPointer() + offset) = value;
#else
                _buffer[offset] = value;
#endif
            }
        }

        // this method exists in order to conform with Java ByteBuffer standards
        public void Put(int offset, byte value)
        {
            PutByte(offset, value);
        }

#if UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER
        // Unsafe but more efficient versions of Put*.
        public void PutShort(int offset, short value)
        {
            PutUshort(offset, (ushort)value);
        }

        public unsafe void PutUshort(int offset, ushort value)
        {
            AssertOffsetAndLength(offset, sizeof(ushort));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif
            {
                *(ushort*)(ptr + offset) =
#if ASSUME_LITTLE_ENDIAN
                    true
#else
                    BitConverter.IsLittleEndian
#endif
                        ? value
                        : ReverseBytes(value);
            }
        }

        public void PutInt(int offset, int value)
        {
            PutUint(offset, (uint)value);
        }

        public unsafe void PutUint(int offset, uint value)
        {
            AssertOffsetAndLength(offset, sizeof(uint));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif 
            {
                *(uint*)(ptr + offset) =
#if ASSUME_LITTLE_ENDIAN
                    true
#else
                    BitConverter.IsLittleEndian
#endif
                        ? value
                        : ReverseBytes(value);
            }
        }

        public unsafe void PutLong(int offset, long value)
        {
            PutUlong(offset, (ulong)value);
        }

        public unsafe void PutUlong(int offset, ulong value)
        {
            AssertOffsetAndLength(offset, sizeof(ulong));

#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif 
            {
                *(ulong*)(ptr + offset) =
#if ASSUME_LITTLE_ENDIAN
                    true
#else
                    BitConverter.IsLittleEndian
#endif
                        ? value
                        : ReverseBytes(value);
            }
        }

        public unsafe void PutFloat(int offset, float value)
        {
            AssertOffsetAndLength(offset, sizeof(float));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif
            {
#if ASSUME_LITTLE_ENDIAN
                if (true)
#else
                if (BitConverter.IsLittleEndian)
#endif
                {
                    *(float*)(ptr + offset) = value;
                }
                else
                {
                    *(uint*)(ptr + offset) = ReverseBytes(*(uint*)(&value));
                }
            }
        }

        public unsafe void PutDouble(int offset, double value)
        {
            AssertOffsetAndLength(offset, sizeof(double));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif
            {
#if ASSUME_LITTLE_ENDIAN
                if (true)
#else
                if (BitConverter.IsLittleEndian)
#endif
                {
                    *(double*)(ptr + offset) = value;
                }
                else
                {
                    *(ulong*)(ptr + offset) = ReverseBytes(*(ulong*)(ptr + offset));
                }
            }
        }

#else // !UNSAFE_BYTEBUFFER && !PINNED_BYTEBUFFER
        // Slower versions of Put* for when unsafe code is not allowed.
        public void PutShort(int offset, short value)
        {
            AssertOffsetAndLength(offset, sizeof(short));
            WriteLittleEndian(offset, sizeof(short), (ulong)value);
        }

        public void PutUshort(int offset, ushort value)
        {
            AssertOffsetAndLength(offset, sizeof(ushort));
            WriteLittleEndian(offset, sizeof(ushort), (ulong)value);
        }

        public void PutInt(int offset, int value)
        {
            AssertOffsetAndLength(offset, sizeof(int));
            WriteLittleEndian(offset, sizeof(int), (ulong)value);
        }

        public void PutUint(int offset, uint value)
        {
            AssertOffsetAndLength(offset, sizeof(uint));
            WriteLittleEndian(offset, sizeof(uint), (ulong)value);
        }

        public void PutLong(int offset, long value)
        {
            AssertOffsetAndLength(offset, sizeof(long));
            WriteLittleEndian(offset, sizeof(long), (ulong)value);
        }

        public void PutUlong(int offset, ulong value)
        {
            AssertOffsetAndLength(offset, sizeof(ulong));
            WriteLittleEndian(offset, sizeof(ulong), value);
        }

        public void PutFloat(int offset, float value)
        {
            AssertOffsetAndLength(offset, sizeof(float));
            floathelper[0] = value;
            Buffer.BlockCopy(floathelper, 0, inthelper, 0, sizeof(float));
            WriteLittleEndian(offset, sizeof(float), (ulong)inthelper[0]);
        }

        public void PutDouble(int offset, double value)
        {
            AssertOffsetAndLength(offset, sizeof(double));
            doublehelper[0] = value;
            Buffer.BlockCopy(doublehelper, 0, ulonghelper, 0, sizeof(double));
            WriteLittleEndian(offset, sizeof(double), ulonghelper[0]);
        }

#endif // UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER

#if UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER
        public unsafe void PutStringUtf8(int offset, string value, int? byteLength = null)
#else
        public void PutStringUtf8(int offset, string value, int? byteLength = null)
#endif
        {
            if (byteLength == null) 
            {
                byteLength = Encoding.UTF8.GetByteCount(value);
            }
            AssertOffsetAndLength(offset, byteLength.GetValueOrDefault());
#if UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER

#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed(byte* ptr = _buffer)
#endif // PINNED_BYTEBUFFER
            {
                fixed (char* charPtr = value) 
                {
                    Encoding.UTF8.GetBytes(charPtr, value.Length, ptr + offset, byteLength.GetValueOrDefault());
                }
            }

#else // !UNSAFE_BYTEBUFFER && !PINNED_BYTEBUFFER
            Encoding.UTF8.GetBytes(value, 0, value.Length, _buffer, offset);
#endif // UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER
        }

#if PINNED_BYTEBUFFER
        public unsafe sbyte GetSbyte(int index)
#else
        public sbyte GetSbyte(int index)
#endif
        {
            AssertOffsetAndLength(index, sizeof(sbyte));
            return
#if PINNED_BYTEBUFFER
                *((sbyte*)_bufferPtr.ToPointer() + index);
#else
                (sbyte)_buffer[index];
#endif
        }

#if PINNED_BYTEBUFFER
        public unsafe byte Get(int index)
#else
        public byte Get(int index)
#endif
        {
            AssertOffsetAndLength(index, sizeof(byte));
            return
#if PINNED_BYTEBUFFER
                *((byte*)_bufferPtr.ToPointer() + index);
#else
                (byte)_buffer[index];
#endif
        }

#if UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER
        // Unsafe but more efficient versions of Get*.
        public short GetShort(int offset)
        {
            return (short)GetUshort(offset);
        }

        public unsafe ushort GetUshort(int offset)
        {
            AssertOffsetAndLength(offset, sizeof(ushort));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif 
            {
                return
#if ASSUME_LITTLE_ENDIAN
                    true
#else
                    BitConverter.IsLittleEndian
#endif
                      ? *(ushort*)(ptr + offset)
                      : ReverseBytes(*(ushort*)(ptr + offset));
            }
        }

        public int GetInt(int offset)
        {
            return (int)GetUint(offset);
        }

        public unsafe uint GetUint(int offset)
        {
            AssertOffsetAndLength(offset, sizeof(uint));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif 
            {
                return
#if ASSUME_LITTLE_ENDIAN
                    true
#else
                    BitConverter.IsLittleEndian
#endif
                        ? *(uint*)(ptr + offset)
                        : ReverseBytes(*(uint*)(ptr + offset));
            }
        }

        public long GetLong(int offset)
        {
            return (long)GetUlong(offset);
        }

        public unsafe ulong GetUlong(int offset)
        {
            AssertOffsetAndLength(offset, sizeof(ulong));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif 
            {
                return
#if ASSUME_LITTLE_ENDIAN
                    true
#else
                    BitConverter.IsLittleEndian
#endif
                        ? *(ulong*)(ptr + offset)
                        : ReverseBytes(*(ulong*)(ptr + offset));
            }
        }

        public unsafe float GetFloat(int offset)
        {
            AssertOffsetAndLength(offset, sizeof(float));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif
            {
#if ASSUME_LITTLE_ENDIAN
                if (true)
#else
                if (BitConverter.IsLittleEndian)
#endif
                {
                    return *(float*)(ptr + offset);
                }
                else
                {
                    uint uvalue = ReverseBytes(*(uint*)(ptr + offset));
                    return *(float*)(&uvalue);
                }
            }
        }

        public unsafe double GetDouble(int offset)
        {
            AssertOffsetAndLength(offset, sizeof(double));
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#else
            fixed (byte* ptr = _buffer)
#endif
            {
#if ASSUME_LITTLE_ENDIAN
                if (true)
#else
                if (BitConverter.IsLittleEndian)
#endif
                {
                    return *(double*)(ptr + offset);
                }
                else
                {
                    ulong uvalue = ReverseBytes(*(ulong*)(ptr + offset));
                    return *(double*)(&uvalue);
                }
            }
        }
#else // !UNSAFE_BYTEBUFFER && !PINNED_BYTEBUFFER
        // Slower versions of Get* for when unsafe code is not allowed.
        public short GetShort(int index)
        {
            return (short)ReadLittleEndian(index, sizeof(short));
        }

        public ushort GetUshort(int index)
        {
            return (ushort)ReadLittleEndian(index, sizeof(ushort));
        }

        public int GetInt(int index)
        {
            return (int)ReadLittleEndian(index, sizeof(int));
        }

        public uint GetUint(int index)
        {
            return (uint)ReadLittleEndian(index, sizeof(uint));
        }

        public long GetLong(int index)
        {
           return (long)ReadLittleEndian(index, sizeof(long));
        }

        public ulong GetUlong(int index)
        {
            return ReadLittleEndian(index, sizeof(ulong));
        }

        public float GetFloat(int index)
        {
            int i = (int)ReadLittleEndian(index, sizeof(float));
            inthelper[0] = i;
            Buffer.BlockCopy(inthelper, 0, floathelper, 0, sizeof(float));
            return floathelper[0];
        }

        public double GetDouble(int index)
        {
            ulong i = ReadLittleEndian(index, sizeof(double));
            // There's Int64BitsToDouble but it uses unsafe code internally.
            ulonghelper[0] = i;
            Buffer.BlockCopy(ulonghelper, 0, doublehelper, 0, sizeof(double));
            return doublehelper[0];
        }
#endif // UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER

#if PINNED_BYTEBUFFER
        public unsafe string GetStringUtf8(int offset, int length)
#else
        public string GetStringUtf8(int offset, int length) 
#endif
        {
            AssertOffsetAndLength(offset, length);
            return
#if PINNED_BYTEBUFFER
                new string((sbyte*)_bufferPtr.ToPointer(), offset, length, Encoding.UTF8);
#else
                Encoding.UTF8.GetString(_buffer, offset, length);
#endif
        }

        public ArraySegment<byte> GetArraySegment(int offset, int length) 
        {
            ArraySegment<byte> arraySegment;
            GetArraySegment(offset, length, out arraySegment);
            return arraySegment;
        }

        public void GetArraySegment(int offset, int length, out ArraySegment<byte> arraySegment) {
          AssertOffsetAndLength(offset, length);
          byte[] buffer = _buffer;
#if PINNED_BYTEBUFFER
          if (buffer == null) {
            buffer = new byte[length];
            CopyTo(offset, buffer, 0, length);
          }
#endif
          arraySegment = new ArraySegment<byte>(buffer, offset, length);
        }

        #region CopyFrom Overloads

        public void CopyFrom(byte[] src, int srcOffset, int dstOffset, int count) 
        {
            int length = sizeof(byte) * count;
#if PINNED_BYTEBUFFER
            AssertOffsetAndLength(dstOffset, length);
            Marshal.Copy(src, srcOffset, _bufferPtr + dstOffset, count);
#else
            Buffer.BlockCopy(src, srcOffset, _buffer, dstOffset, length);
#endif
        }

        public void CopyFrom(sbyte[] src, int srcOffset, int dstOffset, int count) 
        {
            CopyFrom((byte[])(Array)src, srcOffset, dstOffset, count);
        }

#if UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER
        public unsafe void CopyFrom(bool[] src, int srcOffset, int dstOffset, int count)
#else
        public void CopyFrom(bool[] src, int srcOffset, int dstOffset, int count)
#endif
        {
            AssertOffsetAndLength(dstOffset, count);
            AssertOffsetAndLength(src, srcOffset, count);
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#elif UNSAFE_BYTEBUFFER
            fixed (byte* ptr = _buffer)
#endif
            {

                for (int i = 0; i < count; i++, srcOffset++, dstOffset++)
                {
#if UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER
                    *(ptr + dstOffset) 
#else
                    _buffer[dstOffset]
#endif
                        = (byte)(src[srcOffset] ? 1 : 0);
                }
            }
        }

        public void CopyFrom(short[] src, int srcOffset, int dstOffset, int count)
        {
            int length = sizeof(short) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            { 
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(dstOffset, length);
                Marshal.Copy(src, srcOffset, _bufferPtr + dstOffset, count);
#else
                Buffer.BlockCopy(src, srcOffset, _buffer, dstOffset, length);
#endif
            }
            else 
            {
                AssertOffsetAndLength(dstOffset, length);
                AssertOffsetAndLength(src, srcOffset, count);
                for (int i = 0; i < count; i++, srcOffset++, dstOffset += sizeof(short)) 
                {
                    PutShort(dstOffset, src[srcOffset]);
                }
            }
        }

        public void CopyFrom(ushort[] src, int srcOffset, int dstOffset, int count)
        {
            CopyFrom((short[])(Array)src, srcOffset, dstOffset, count);
        }

        public void CopyFrom(int[] src, int srcOffset, int dstOffset, int count)
        {
            int length = sizeof(int) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(dstOffset, length);
                Marshal.Copy(src, srcOffset, _bufferPtr + dstOffset, count);
#else
                Buffer.BlockCopy(src, srcOffset, _buffer, dstOffset, length);
#endif
            } 
            else 
            {
                AssertOffsetAndLength(dstOffset, length);
                AssertOffsetAndLength(src, srcOffset, count);
                for (int i = 0; i < count; i++, srcOffset++, dstOffset += sizeof(int)) 
                {
                    PutInt(dstOffset, src[srcOffset]);
                }
            }
        }

        public void CopyFrom(uint[] src, int srcOffset, int dstOffset, int count)
        {
            CopyFrom((int[])(Array)src, srcOffset, dstOffset, count);
        }

        public void CopyFrom(long[] src, int srcOffset, int dstOffset, int count)
        {
            int length = sizeof(long) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(dstOffset, length);
                Marshal.Copy(src, srcOffset, _bufferPtr + dstOffset, count);
#else
                Buffer.BlockCopy(src, srcOffset, _buffer, dstOffset, length);
#endif
            } 
            else 
            {
                AssertOffsetAndLength(dstOffset, length);
                AssertOffsetAndLength(src, srcOffset, count);
                for (int i = 0; i < count; i++, srcOffset++, dstOffset += sizeof(long)) 
                {
                    PutLong(dstOffset, src[srcOffset]);
                }
            }
        }

        public void CopyFrom(ulong[] src, int srcOffset, int dstOffset, int count) 
        {
            CopyFrom((long[])(Array)src, srcOffset, dstOffset, count);
        }

        public void CopyFrom(float[] src, int srcOffset, int dstOffset, int count)
        {
            int length = sizeof(float) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(dstOffset, length);
                Marshal.Copy(src, srcOffset, _bufferPtr + dstOffset, count);
#else
                Buffer.BlockCopy(src, srcOffset, _buffer, dstOffset, length);
#endif
            }
            else 
            {
                AssertOffsetAndLength(dstOffset, length);
                AssertOffsetAndLength(src, srcOffset, count);
                for (int i = 0; i < count; i++, srcOffset++, dstOffset += sizeof(float))
                {
                    PutFloat(dstOffset, src[srcOffset]);
                }
            }
        }

        public void CopyFrom(double[] src, int srcOffset, int dstOffset, int count)
        {
            int length = sizeof(double) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(dstOffset, length);
                Marshal.Copy(src, srcOffset, _bufferPtr + dstOffset, count);
#else
                Buffer.BlockCopy(src, srcOffset, _buffer, dstOffset, length);
#endif
            } 
            else 
            {
                AssertOffsetAndLength(dstOffset, length);
                AssertOffsetAndLength(src, srcOffset, count);
                for (int i = 0; i < count; i++, srcOffset++, dstOffset += sizeof(double)) 
                {
                    PutDouble(dstOffset, src[srcOffset]);
                }
            }
        }

        public void CopyFrom(ByteBuffer src, int srcOffset, int dstOffset, int count) 
        {
#if PINNED_BYTEBUFFER
            if (_buffer == null || src._buffer == null) 
            {
                if (_buffer != null)
                {
                    src.AssertOffsetAndLength(srcOffset, count);
                    Marshal.Copy(src._bufferPtr + srcOffset, _buffer, dstOffset, count);
                }
                else if (src._buffer != null)
                {
                    AssertOffsetAndLength(dstOffset, count);
                    Marshal.Copy(src._buffer, srcOffset, _bufferPtr + dstOffset, count);
                }
                else 
                {
                    AssertOffsetAndLength(dstOffset, count);
                    src.AssertOffsetAndLength(srcOffset, count);
                    Memmove(_bufferPtr + dstOffset, src._bufferPtr + srcOffset, count);
                }
                return;
            }
#endif
            Buffer.BlockCopy(src._buffer, srcOffset, _buffer, dstOffset, count);
        }

        #endregion

        #region CopyTo Overloads

        public void CopyTo(int srcOffset, byte[] dst, int dstOffset, int count) 
        {
            int length = sizeof(byte) * count;
#if PINNED_BYTEBUFFER
            AssertOffsetAndLength(srcOffset, length);
            Marshal.Copy(_bufferPtr + srcOffset, dst, dstOffset, count);
#else
            Buffer.BlockCopy(_buffer, srcOffset, dst, dstOffset, length);
#endif
        }

        public void CopyTo(int srcOffset, sbyte[] dst, int dstOffset, int count)
        {
            CopyTo(srcOffset, (byte[])(Array)dst, dstOffset, count);
        }

#if UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER
        public unsafe void CopyTo(int srcOffset, bool[] dst, int dstOffset, int count)
#else
        public void CopyTo(int srcOffset, bool[] dst, int dstOffset, int count)
#endif
        {
            AssertOffsetAndLength(srcOffset, count);
            AssertOffsetAndLength(dst, dstOffset, count);
#if PINNED_BYTEBUFFER
            byte* ptr = (byte*)_bufferPtr.ToPointer();
#elif UNSAFE_BYTEBUFFER
            fixed (byte* ptr = _buffer)
#endif
            {

                for (int i = 0; i < count; i++, srcOffset++, dstOffset++)
                {
                    dst[dstOffset] =
#if UNSAFE_BYTEBUFFER || PINNED_BYTEBUFFER
                        *(ptr + srcOffset)
#else
                        _buffer[srcOffset]
#endif
                            > 0;
                }
            }
        }

        public void CopyTo(int srcOffset, short[] dst, int dstOffset, int count)
        {
            int length = sizeof(short) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            { 
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(srcOffset, length);
                Marshal.Copy(_bufferPtr + srcOffset, dst, dstOffset, count);
#else
                Buffer.BlockCopy(_buffer, srcOffset, dst, dstOffset, length);
#endif
            }
            else 
            {
                AssertOffsetAndLength(srcOffset, length);
                AssertOffsetAndLength(dst, dstOffset, count);
                for (int i = 0; i < count; i++, srcOffset += sizeof(short), dstOffset++) 
                {
                    dst[dstOffset] = GetShort(srcOffset);
                }
            }
        }

        public void CopyTo(int srcOffset, ushort[] dst, int dstOffset, int count)
        {
            CopyTo(srcOffset, (short[])(Array)dst, dstOffset, count);
        }

        public void CopyTo(int srcOffset, int[] dst, int dstOffset, int count)
        {
            int length = sizeof(int) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(srcOffset, length);
                Marshal.Copy(_bufferPtr + srcOffset, dst, dstOffset, count);
#else
                Buffer.BlockCopy(_buffer, srcOffset, dst, dstOffset, length);
#endif
            } 
            else 
            {
                AssertOffsetAndLength(srcOffset, length);
                AssertOffsetAndLength(dst, dstOffset, count);
                for (int i = 0; i < count; i++, srcOffset += sizeof(int), dstOffset++) 
                {
                    dst[dstOffset] = GetInt(srcOffset);
                }
            }
        }

        public void CopyTo(int srcOffset, uint[] dst, int dstOffset, int count)
        {
            CopyTo(srcOffset, (int[])(Array)dst, dstOffset, count);
        }

        public void CopyTo(int srcOffset, long[] dst, int dstOffset, int count)
        {
            int length = sizeof(long) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(srcOffset, length);
                Marshal.Copy(_bufferPtr + srcOffset, dst, dstOffset, count);
#else
                Buffer.BlockCopy(_buffer, srcOffset, dst, dstOffset, length);
#endif
            } 
            else 
            {
                AssertOffsetAndLength(srcOffset, length);
                AssertOffsetAndLength(dst, dstOffset, count);
                for (int i = 0; i < count; i++, srcOffset += sizeof(long), dstOffset++) 
                {
                    dst[dstOffset] = GetLong(srcOffset);
                }
            }
        }

        public void CopyTo(int srcOffset, ulong[] dst, int dstOffset, int count)
        {
            CopyTo(srcOffset, (long[])(Array)dst, dstOffset, count);
        }

        public void CopyTo(int srcOffset, float[] dst, int dstOffset, int count)
        {
            int length = sizeof(float) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(srcOffset, length);
                Marshal.Copy(_bufferPtr + srcOffset, dst, dstOffset, count);
#else
                Buffer.BlockCopy(_buffer, srcOffset, dst, dstOffset, length);
#endif
            } 
            else 
            {
                AssertOffsetAndLength(srcOffset, length);
                AssertOffsetAndLength(dst, dstOffset, count);
                for (int i = 0; i < count; i++, srcOffset += sizeof(float), dstOffset++) 
                {
                    dst[dstOffset] = GetFloat(srcOffset);
                }
            }
        }

        public void CopyTo(int srcOffset, double[] dst, int dstOffset, int count)
        {
            int length = sizeof(double) * count;
#if ASSUME_LITTLE_ENDIAN
            if (true)
#else
            if (BitConverter.IsLittleEndian)
#endif
            {
#if PINNED_BYTEBUFFER
                AssertOffsetAndLength(srcOffset, length);
                Marshal.Copy(_bufferPtr + srcOffset, dst, dstOffset, count);
#else
                Buffer.BlockCopy(_buffer, srcOffset, dst, dstOffset, length);
#endif
            } 
            else 
            {
                AssertOffsetAndLength(srcOffset, length);
                AssertOffsetAndLength(dst, dstOffset, count);
                for (int i = 0; i < count; i++, srcOffset += sizeof(double), dstOffset++) 
                {
                    dst[dstOffset] = GetDouble(srcOffset);
                }
            }
        }

        public void CopyTo(int srcOffset, ByteBuffer dst, int dstOffset, int count) 
        {
#if PINNED_BYTEBUFFER
            if (_buffer == null || dst._buffer == null) 
            {
                if (_buffer != null) 
                {
                    dst.AssertOffsetAndLength(dstOffset, count);
                    Marshal.Copy(_buffer, srcOffset, dst._bufferPtr + dstOffset, count);
                }
                else if (dst._buffer != null)
                {
                    AssertOffsetAndLength(srcOffset, count);
                    Marshal.Copy(_bufferPtr + srcOffset, dst._buffer, dstOffset, count);
                }
                else
                {
                    AssertOffsetAndLength(srcOffset, count);
                    dst.AssertOffsetAndLength(dstOffset, count);
                    Memmove(dst._bufferPtr + dstOffset, _bufferPtr + srcOffset, count);
                }
                return;
            }
#endif
            Buffer.BlockCopy(_buffer, srcOffset, dst._buffer, dstOffset, count);
        }

        #endregion
    }
}
