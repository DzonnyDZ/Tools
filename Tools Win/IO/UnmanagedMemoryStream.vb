Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace IOt
    ''' <summary>Read-only stream to unmanaged memory</summary>
    ''' <remarks><note type="inheritinfo">Derived class can provide write capabilities. Just override write-related methods.</note></remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class UnmanagedMemoryStream
        Inherits IO.Stream

        ''' <summary>Used to release the memory</summary>
        Private release As Action
        ''' <summary>Pointer to memory</summary>
        Private pointer As IntPtr
        ''' <summary>Size of memory area</summary>
        Private size As Integer

        ''' <summary>CTor - creates a new instance of the <see cref="UnmanagedMemoryStream"/> class that own a handle (custom release of unmanaged memory)</summary>
        ''' <param name="pointer">Pointer to unmanaged memory where the stream starts</param>
        ''' <param name="size">Length of unmanaged memory where the stream is located in bytes</param>
        ''' <param name="release">Method to release the unmanaged memory. This method will be called form <see cref="Dispose"/> to deallocate the unmanaged memory.</param>
        ''' <remarks>Use this CTor if unmanaged memory should be deallocated when newly created instance is disposed and deallocation logic is not based on handle (<see cref="IntPtr"/>).</remarks>
        Public Sub New(pointer As IntPtr, size As Integer, release As Action)
            Me.New(pointer, size)
            If release Is Nothing Then Throw New ArgumentNullException("release")
            Me.release = release
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="UnmanagedMemoryStream"/> class that own a handle</summary>
        ''' <param name="pointer">Pointer to unmanaged memory where the stream starts</param>
        ''' <param name="size">Length of unmanaged memory where the stream is located in bytes</param>
        ''' <param name="handle">Handle that was used to obtain <paramref name="pointer"/>. The handle keeps the unmanaged memory allocated.</param>
        ''' <param name="release">Method to release the <paramref name="handle"/>. This method will be called form <see cref="Dispose"/> to deallocate the unmanaged memory.</param>
        ''' <remarks>Use this CTor if this instance is exclusive owhen of <paramref name="handle"/>.</remarks>
        Public Sub New(pointer As IntPtr, size As Integer, handle As IntPtr, release As Action(Of IntPtr))
            Me.New(pointer, size, Tools.ExtensionsT.ThrowIfNull(release, Sub() release(handle), "release"))
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="UnmanagedMemoryStream"/> class that does not own a handle</summary>
        ''' <param name="pointer">Pointer to unmanaged memory where the stream starts</param>
        ''' <param name="size">Length of unmanaged memory where the stream is located in bytes</param>
        ''' <remarks>
        ''' Use this constructor only if you know what you are doing!
        ''' This constructor does not take care of releasing the unmanaged memory.
        ''' Caller is responsible for releasing the unmanaged memory.
        ''' Unmanaged memory should be release when it is no longer used.
        ''' Unmanaged memory should not be released untill instance created by this CTor is disposed.
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Sub New(pointer As IntPtr, size As Integer)
            If pointer = IntPtr.Zero Then Throw New ArgumentNullException("pointer")
            If size < 0 Then Throw New ArgumentOutOfRangeException("size")
            Me.pointer = pointer
            Me.size = size
        End Sub


        ''' <summary>Gets a value indicating whether the current stream supports reading.</summary>
        ''' <returns>true if the stream supports reading; otherwise, false. This implementation always returns true.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Overrides ReadOnly Property CanRead As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>Gets a value indicating whether the current stream supports seeking.</summary>
        ''' <returns>true if the stream supports seeking; otherwise, false. This implementation always returns true.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Overrides ReadOnly Property CanSeek As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>When overridden in a derived class, gets a value indicating whether the current stream supports writing.</summary>
        ''' <returns>true if the stream supports writing; otherwise, false. This implementation always returns false.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Overrides ReadOnly Property CanWrite As Boolean
            Get
                Return False
            End Get
        End Property

        ''' <summary>When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs. (not thrown by this implementation)</exception>
        ''' <exception cref="NotSupportedException">This implementation always throws this exception.</exception>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Sub Flush()
            Throw New NotSupportedException
        End Sub

        ''' <summary>Gets the length in bytes of the stream.</summary>
        ''' <returns>A long value representing the length of the stream in bytes.</returns>
        ''' <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. (not thrown by this implementation)</exception>
        ''' <filterpriority>1</filterpriority>
        Public Overrides ReadOnly Property Length As Long
            Get
                Return size
            End Get
        End Property

        Private _position As Integer = 0

        ''' <summary>Gets or sets the position within the current stream.</summary>
        ''' <returns>The current position within the stream.</returns>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.NotSupportedException">The stream does not support seeking. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. (not thrown by this implementation)</exception>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Property Position As Long
            Get
                Return _position
            End Get
            Set(value As Long)
                _position = value
            End Set
        End Property

        ''' <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
        ''' <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        ''' <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source. </param>
        ''' <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream. </param>
        ''' <param name="count">The maximum number of bytes to be read from the current stream. </param>
        ''' <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length. </exception>
        ''' <exception cref="ArgumentNullException"><paramref name="buffer" /> is null. </exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        ''' <exception cref="T:System.NotSupportedException">The stream does not support reading. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
            If buffer Is Nothing Then Throw New ArgumentNullException("buffer")
            If offset + count > buffer.Length Then Throw New ArgumentException("Not enough space in target buffer")
            If offset < 0 Then Throw New ArgumentOutOfRangeException("offset")
            If count < 0 Then Throw New ArgumentOutOfRangeException("count")
            If pointer = IntPtr.Zero Then Throw New ObjectDisposedException([GetType].Name)
            If count = 0 Then Return 0
            If Position >= Length Then Return 0
            Dim bytesToRead = Math.Min(count, size - Position)
            'If offset = 0 Then
            Marshal.Copy(pointer + Position, buffer, offset, bytesToRead)
            'Else
            'Dim buff(0 To bytesToRead - 1) As Byte
            'Marshal.Copy(pointer, buff, Position, bytesToRead)
            'Array.Copy(buff, 0, buffer, offset, bytesToRead)
            'Erase buff
            'End If
            Position += bytesToRead
            Return bytesToRead
        End Function

        ''' <summary>When overridden in a derived class, sets the position within the current stream.</summary>
        ''' <param name="offset">A byte offset relative to the 
        ''' <paramref name="origin" /> parameter. </param>
        ''' <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position. </param>
        ''' <returns>The new position within the current stream.</returns>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. (not thrown by this implementation)</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="origin"/> is not one of <see cref="IO.SeekOrigin"/> values.</exception>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Function Seek(offset As Long, origin As IO.SeekOrigin) As Long
            Select Case origin
                Case IO.SeekOrigin.Begin : Position = offset
                Case IO.SeekOrigin.Current : Position += offset
                Case IO.SeekOrigin.End : Position = size - offset
                Case Else : Throw New InvalidEnumArgumentException("origin", origin, origin.GetType)
            End Select
            Return Position
        End Function

        ''' <summary>When overridden in a derived class, sets the length of the current stream.</summary>
        ''' <param name="value">The desired length of the current stream in bytes. </param>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. This implementation always throws this exception.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. (not thrown by this implementation)</exception>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Sub SetLength(value As Long)
            Throw New NotSupportedException
        End Sub

        ''' <summary>When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
        ''' <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream. </param>
        ''' <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream. </param>
        ''' <param name="count">The number of bytes to be written to the current stream. </param>
        ''' <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length. (not thrown by this implementation)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="buffer" /> is null. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs. (not thrown by this implementation)</exception>
        ''' <exception cref="T:System.NotSupportedException">The stream does not support writing. This implementation always throws this exception.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. (not thrown by this implementation)</exception>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
            Throw New NotSupportedException
        End Sub

        ''' <summary>Releases all resources used by the <see cref="T:System.IO.Stream" />.</summary>
        ''' <param name="disposing">True when disposing, false when finalizing</param>
        Protected Overrides Sub Dispose(disposing As Boolean)
            MyBase.Dispose(disposing)
            If release IsNot Nothing Then release() : release = Nothing
            pointer = IntPtr.Zero
        End Sub
    End Class
End Namespace