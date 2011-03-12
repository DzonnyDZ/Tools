'http://www.codeproject.com/KB/security/SimpleEncryption.aspx
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration
Imports System.ComponentModel

Namespace SecurityT.CryptographyT.EncryptionT
    ''' <summary>Represents Hex, Byte, Base64, or String data to encrypt/decrypt</summary>
    ''' <remarks>
    ''' <para>Use the <see cref="EncryptionData.Text"/> property to set/get a string representation.</para>
    ''' <para>Use the <see cref="EncryptionData.Hex"/> property to set/get a string-based Hexadecimal representation.</para>
    ''' <para>Use the <see cref="EncryptionData.Base64"/> to set/get a string-based Base64 representation.</para>
    ''' </remarks>
    Public Class EncryptionData
        Private _bytes As Byte()
        Private _maxBytes As Integer = 0
        Private _minBytes As Integer = 0
        Private _stepBytes As Integer = 0

        Private _encoding As System.Text.Encoding = System.Text.Encoding.UTF8
        ''' <summary>Gets or sets the default text encoding for this Data instance</summary>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        Public Property Encoding() As System.Text.Encoding
            Get
                Return _encoding
            End Get
            Set(value As System.Text.Encoding)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _encoding = value
            End Set
        End Property

        ''' <summary>Default CTor - creates new, empty encryption data</summary>
        Public Sub New()
        End Sub

        ''' <summary>CTor - creates new encryption data with the specified byte array</summary>
        ''' <param name="data">A byte array containing encryption data</param>
        Public Sub New(ByVal data As Byte())
            _bytes = data
        End Sub

        ''' <summary> CTor - creates new encryption data with the specified string; will be converted to byte array using UTF-8 encoding</summary>
        ''' <param name="s">A string. The string will be converted to byte array using UTF-8 encoding</param>
        Public Sub New(ByVal s As String)
            Me.Text = s
        End Sub

        ''' <summary>CTor - creates new encryption data using the specified string and the specified encoding to convert the string to a byte array.</summary>
        ''' <param name="s">A string. The string will be converted to byte array using <paramref name="encoding"/></param>
        ''' <param name="encoding">An encoding to be used with this instance</param>
        ''' <exception cref="ArgumentNullException"><paramref name="encoding"/> is null</exception>
        Public Sub New(ByVal s As String, ByVal encoding As System.Text.Encoding)
            Me.Encoding = encoding
            Me.Text = s
        End Sub

        ''' <summary>Returns true if no data is present</summary>
        Public ReadOnly Property IsEmpty() As Boolean
            Get
                Return Bytes Is Nothing OrElse Bytes.Length = 0
            End Get
        End Property

        ''' <summary>Gets or sets allowed step interval, in bytes, for this data; if 0, no limit</summary>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is less than zero</exception>
        Public Property StepBytes() As Integer
            Get
                Return _StepBytes
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then Throw New ArgumentOutOfRangeException("value")
                _stepBytes = Value
            End Set
        End Property

        ''' <summary>Gets or sets allowed step interval, in bits, for this data; if 0, no limit</summary>
        ''' <remarks>Value being set is truncated to whole bytes</remarks>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is less than zero</exception>
        Public Property StepBits() As Integer
            Get
                Return _stepBytes * 8
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then Throw New ArgumentOutOfRangeException("value")
                _stepBytes = Value \ 8
            End Set
        End Property

        ''' <summary>Gets or sets minimum number of bytes allowed for this data; if 0, no limit</summary>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is less than zero</exception>
        Public Property MinBytes() As Integer
            Get
                Return _minBytes
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then Throw New ArgumentOutOfRangeException("value")
                _minBytes = Value
            End Set
        End Property

        ''' <summary>Gets or sets minimum number of bits allowed for this data; if 0, no limit</summary>
        ''' <remarks>Value being set is truncated to whole bytes</remarks>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is less than zero</exception>
        Public Property MinBits() As Integer
            Get
                Return _minBytes * 8
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then Throw New ArgumentOutOfRangeException("value")
                _minBytes = Value \ 8
            End Set
        End Property

        ''' <summary>Gets or sets maximum number of bytes allowed for this data; if 0, no limit</summary>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is less than zero</exception>
        Public Property MaxBytes() As Integer
            Get
                Return _maxBytes
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then Throw New ArgumentOutOfRangeException("value")
                _maxBytes = Value
            End Set
        End Property

        ''' <summary>Gets or sets maximum number of bits allowed for this data; if 0, no limit</summary>
        ''' <remarks>Value being set is truncated to whole bytes</remarks>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is less than zero</exception>
        Public Property MaxBits() As Integer
            Get
                Return _maxBytes * 8
            End Get
            Set(ByVal Value As Integer)
                If Value < 0 Then Throw New ArgumentOutOfRangeException("value")
                _maxBytes = Value \ 8
            End Set
        End Property

        ''' <summary>Gets or sets the byte representation of the data</summary>
        ''' <remarks>This will be padded to <see cref="MinBytes"/> and trimmed to <see cref="MaxBytes"/> as necessary!</remarks>
        Public Property Bytes() As Byte()
            Get
                If _MaxBytes > 0 Then
                    If _bytes.Length > _MaxBytes Then
                        Dim b(_MaxBytes - 1) As Byte
                        Array.Copy(_bytes, b, b.Length)
                        _bytes = b
                    End If
                End If
                If _MinBytes > 0 Then
                    If _bytes.Length < _MinBytes Then
                        Dim b(_MinBytes - 1) As Byte
                        Array.Copy(_bytes, b, _bytes.Length)
                        _bytes = b
                    End If
                End If
                Return _bytes
            End Get
            Set(ByVal Value As Byte())
                _bytes = Value
            End Set
        End Property

        ''' <summary>Gets or sets text representation of bytes using <see cref="Encoding"/></summary>
        Public Property Text() As String
            Get
                If _bytes Is Nothing Then
                    Return ""
                Else
                    '-- need to handle nulls here; oddly, C# will happily convert
                    '-- nulls into the string whereas VB stops converting at the
                    '-- first null!
                    Dim i As Integer = Array.IndexOf(_bytes, CType(0, Byte))
                    If i >= 0 Then
                        Return Me.Encoding.GetString(_bytes, 0, i)
                    Else
                        Return Me.Encoding.GetString(_bytes)
                    End If
                End If
            End Get
            Set(ByVal Value As String)
                _bytes = Me.Encoding.GetBytes(Value)
            End Set
        End Property

        ''' <summary>Gets or sets hex string representation of this data</summary>
        Public Property Hex() As String
            Get
                Return EncryptionUtilities.ToHex(_bytes)
            End Get
            Set(ByVal Value As String)
                _bytes = EncryptionUtilities.FromHex(Value)
            End Set
        End Property

        ''' <summary>Gets or sets Base64 string representation of this data</summary>
        Public Property Base64() As String
            Get
                Return EncryptionUtilities.ToBase64(_bytes)
            End Get
            Set(ByVal Value As String)
                _bytes = EncryptionUtilities.FromBase64(Value)
            End Set
        End Property

        ''' <summary>Returns text representation of bytes using <see cref="Encoding"/></summary>
        Public Shadows Function ToString() As String
            Return Me.Text
        End Function

        ''' <summary>Returns Base64 string representation of this data</summary>
        Public Function ToBase64() As String
            Return Me.Base64
        End Function

        ''' <summary>Returns Hex string representation of this data</summary>
        Public Function ToHex() As String
            Return Me.Hex
        End Function

        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is EncryptionData Then
                Dim o2 As EncryptionData = obj
                If Me.Bytes Is Nothing AndAlso o2.Bytes Is Nothing Then Return True
                If Me.Bytes Is Nothing OrElse o2.Bytes Is Nothing Then Return False
                If Me.Bytes.Length <> o2.Bytes.Length Then Return False
                For i = 0 To Me.Bytes.Length - 1
                    If Me.Bytes(i) <> o2.Bytes(i) Then Return False
                Next
                Return True
            End If
            Return MyBase.Equals(obj)
        End Function

        ''' <summary>Converts byte array to <see cref="EncryptionData"/></summary>
        ''' <param name="a">A byte array</param>
        ''' <returns>A new instance of <see cref="EncryptionData"/> initialized to <paramref name="a"/>; null if <paramref name="a"/> is null</returns>
        Public Shared Widening Operator CType(a As Byte()) As EncryptionData
            If a Is Nothing Then Return Nothing
            Return New EncryptionData(a)
        End Operator

        ''' <summary>Converts <see cref="EncryptionData"/> to a byte array</summary>
        ''' <param name="a">An <see cref="EncryptionData"/></param>
        ''' <remarks><paramref name="a"/>.<see cref="Bytes">Bytes</see>; null if <paramref name="a"/> is null</remarks>
        Public Shared Widening Operator CType(a As EncryptionData) As Byte()
            If a Is Nothing Then Return Nothing
            Return a.Bytes
        End Operator

    End Class
End Namespace
