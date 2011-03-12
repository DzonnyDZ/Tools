'http://www.codeproject.com/KB/security/SimpleEncryption.aspx
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration
Imports System.ComponentModel

Namespace SecurityT.CryptographyT.EncryptionT
    ''' <summary>Provides functions for calculating hash values using different alghoritms</summary>
    ''' <remarks>
    ''' Hash functions are fundamental to modern cryptography. These functions map binary 
    ''' strings of an arbitrary length to small binary strings of a fixed length, known as 
    ''' hash values. A cryptographic hash function has the property that it is computationally
    ''' infeasible to find two distinct inputs that hash to the same value. Hash functions 
    ''' are commonly used with digital signatures and for data integrity.
    ''' </remarks>
    Public Class Hash
        Implements IDisposable
        ''' <summary>Type of hash; some are security oriented, others are fast and simple</summary>
        Public Enum HashProvider
            ''' <summary>Cyclic Redundancy Check provider, 32-bit</summary>
            ''' <seelaso cref="CRC32"/>
            CRC32
            ''' <summary>Secure Hashing Algorithm provider, SHA-1 variant, 160-bit</summary>
            ''' <seelaso cref="SHA1"/>
            SHA1
            ''' <summary>Secure Hashing Algorithm provider, SHA-2 variant, 256-bit</summary>
            ''' <seelaso cref="SHA256"/>
            SHA256
            ''' <summary>Secure Hashing Algorithm provider, SHA-2 variant, 384-bit</summary>
            ''' <seelaso cref="SHA384"/>
            SHA384
            ''' <summary>Secure Hashing Algorithm provider, SHA-2 variant, 512-bit</summary>
            ''' <seelaso cref="SHA512"/>
            SHA512
            ''' <summary>Message Digest algorithm 5, 128-bit</summary>
            ''' <seelaso cref="MD5"/>
            MD5
        End Enum

        Private ReadOnly _alghoritm As HashAlgorithm
        ''' <summary>Gets a hash alghoritm</summary>
        Public ReadOnly Property Alghoritm As HashAlgorithm
            Get
                Return _alghoritm
            End Get
        End Property

        ''' <summary>CTor - instantiate a new hash of the specified type</summary>
        ''' <param name="provider">Identifies the hash alghoritm to use</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="provider"/> is not one of <see cref="HashProvider"/> values</exception>
        Public Sub New(ByVal provider As HashProvider)
            _alghoritm = GetAlghoritm(provider)
        End Sub
        ''' <summary>CTor- instantiates a new hash based on given <see cref="HashAlgorithm"/></summary>
        ''' <param name="alghoritm">A hash alghoritm provider</param>
        ''' <exception cref="ArgumentNullException"><paramref name="alghoritm"/> is null</exception>
        Public Sub New(alghoritm As HashAlgorithm)
            If alghoritm Is Nothing Then Throw New ArgumentNullException("alghoritm")
            _alghoritm = alghoritm
        End Sub

        ''' <summary>Converts <see cref="HashProvider"/> value to instance of <see cref="HashAlgorithm"/>-derived class</summary>
        ''' <param name="provider">Identifies the hash alghoritm</param>
        ''' <returns>An instance of <see cref="HashAlgorithm"/>-derived class</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="provider"/> is not one of <see cref="HashProvider"/> values</exception>
        Public Shared Function GetAlghoritm(ByVal provider As HashProvider) As HashAlgorithm
            Select Case provider
                Case HashProvider.CRC32 : Return New CRC32
                Case HashProvider.MD5 : Return New MD5CryptoServiceProvider
                Case HashProvider.SHA1 : Return New SHA1Managed
                Case HashProvider.SHA256 : Return New SHA256Managed
                Case HashProvider.SHA384 : Return New SHA384Managed
                Case HashProvider.SHA512 : Return New SHA512Managed
                Case Else : Throw New InvalidEnumArgumentException("provider", provider, provider.GetType)
            End Select
        End Function

        ''' <summary>Calculates hash on a stream of arbitrary length</summary>
        ''' <param name="stream">A stream to calculate hash for</param>
        ''' <remarks>Calculated hash</remarks>
        ''' <exception cref="ObjectDisposedException"><see cref="Alghoritm"/> is disposed</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="stream"/> is null</exception>
        Public Function Calculate(ByRef stream As System.IO.Stream) As EncryptionData
            If stream Is Nothing Then Throw New ArgumentNullException("stream")
            Return New EncryptionData(Alghoritm.ComputeHash(stream))
        End Function

        ''' <summary>Calculates hash for fixed length <see cref="Data"/></summary>
        ''' <param name="data">A data to calculate hash for</param>
        ''' <returns>Calculated hash</returns>
        ''' <exception cref="ObjectDisposedException"><see cref="Alghoritm"/> is disposed</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        ''' <remarks><see cref="EncryptionData.Encoding"/> of value being returned is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see>.</remarks>
        Public Function Calculate(ByVal data As EncryptionData) As EncryptionData
            If data Is Nothing Then Throw New ArgumentNullException("data")
            Dim ret = Calculate(data.Bytes)
            ret.Encoding = data.Encoding
            Return ret
        End Function

        ''' <summary>Calculates hash for a string with a prefixed salt value.</summary>
        ''' <param name="data">A data to claculate hash for</param>
        ''' <param name="salt">A "salt" is random data prefixed to every hashed value to prevent common dictionary attacks.</param>
        ''' <exception cref="ObjectDisposedException"><see cref="Alghoritm"/> is disposed</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="salt"/> is null</exception>
        ''' <remarks><see cref="EncryptionData.Encoding"/> of value being returned is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see>.</remarks>
        Public Function Calculate(ByVal data As EncryptionData, ByVal salt As EncryptionData) As EncryptionData
            If data Is Nothing Then Throw New ArgumentNullException("data")
            If salt Is Nothing Then Throw New ArgumentNullException("salt")
            Dim nb(data.Bytes.Length + salt.Bytes.Length - 1) As Byte
            salt.Bytes.CopyTo(nb, 0)
            data.Bytes.CopyTo(nb, salt.Bytes.Length)
            Dim ret = Calculate(nb)
            ret.Encoding = data.Encoding
            Return ret
        End Function

        ''' <summary>Calculates hash for an array of bytes</summary>
        ''' <param name="data">A data to calculate hash for</param>
        ''' <returns>Calculated hash</returns>
        ''' <exception cref="ObjectDisposedException"><see cref="Alghoritm"/> is disposed</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        Public Function Calculate(ByVal data() As Byte) As EncryptionData
            If data Is Nothing Then Throw New ArgumentNullException("data")
            Return New EncryptionData(Alghoritm.ComputeHash(data))
        End Function

#Region "CRC32 HashAlgorithm"
        ''' <summary>Implements CRC32 hash alghoritm</summary>
        Public Class CRC32
            Inherits HashAlgorithm

            Private result As Integer = &HFFFFFFFF

            ''' <summary>Routes data written to the object into the hash algorithm for computing the hash.</summary>
            ''' <param name="array">The input to compute the hash code for. </param>
            ''' <param name="ibStart">The offset into the byte array from which to begin using data. </param>
            ''' <param name="cbSize">The number of bytes in the byte array to use as data. </param>
            Protected Overrides Sub HashCore(ByVal array() As Byte, ByVal ibStart As Integer, ByVal cbSize As Integer)
                Dim lookup As Integer
                For i As Integer = ibStart To cbSize - 1
                    lookup = (result And &HFF) Xor array(i)
                    result = ((result And &HFFFFFF00) \ &H100) And &HFFFFFF
                    result = result Xor crcLookup(lookup)
                Next i
            End Sub

            ''' <summary>Finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
            ''' <returns>The computed hash code.</returns>
            Protected Overrides Function HashFinal() As Byte()
                Dim b() As Byte = BitConverter.GetBytes(Not result)
                Array.Reverse(b)
                Return b
            End Function

            ''' <summary>Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
            Public Overrides Sub Initialize()
                result = &HFFFFFFFF
            End Sub

            Private Shared crcLookup() As Integer = { _
                &H0, &H77073096, &HEE0E612C, &H990951BA, _
                &H76DC419, &H706AF48F, &HE963A535, &H9E6495A3, _
                &HEDB8832, &H79DCB8A4, &HE0D5E91E, &H97D2D988, _
                &H9B64C2B, &H7EB17CBD, &HE7B82D07, &H90BF1D91, _
                &H1DB71064, &H6AB020F2, &HF3B97148, &H84BE41DE, _
                &H1ADAD47D, &H6DDDE4EB, &HF4D4B551, &H83D385C7, _
                &H136C9856, &H646BA8C0, &HFD62F97A, &H8A65C9EC, _
                &H14015C4F, &H63066CD9, &HFA0F3D63, &H8D080DF5, _
                &H3B6E20C8, &H4C69105E, &HD56041E4, &HA2677172, _
                &H3C03E4D1, &H4B04D447, &HD20D85FD, &HA50AB56B, _
                &H35B5A8FA, &H42B2986C, &HDBBBC9D6, &HACBCF940, _
                &H32D86CE3, &H45DF5C75, &HDCD60DCF, &HABD13D59, _
                &H26D930AC, &H51DE003A, &HC8D75180, &HBFD06116, _
                &H21B4F4B5, &H56B3C423, &HCFBA9599, &HB8BDA50F, _
                &H2802B89E, &H5F058808, &HC60CD9B2, &HB10BE924, _
                &H2F6F7C87, &H58684C11, &HC1611DAB, &HB6662D3D, _
                &H76DC4190, &H1DB7106, &H98D220BC, &HEFD5102A, _
                &H71B18589, &H6B6B51F, &H9FBFE4A5, &HE8B8D433, _
                &H7807C9A2, &HF00F934, &H9609A88E, &HE10E9818, _
                &H7F6A0DBB, &H86D3D2D, &H91646C97, &HE6635C01, _
                &H6B6B51F4, &H1C6C6162, &H856530D8, &HF262004E, _
                &H6C0695ED, &H1B01A57B, &H8208F4C1, &HF50FC457, _
                &H65B0D9C6, &H12B7E950, &H8BBEB8EA, &HFCB9887C, _
                &H62DD1DDF, &H15DA2D49, &H8CD37CF3, &HFBD44C65, _
                &H4DB26158, &H3AB551CE, &HA3BC0074, &HD4BB30E2, _
                &H4ADFA541, &H3DD895D7, &HA4D1C46D, &HD3D6F4FB, _
                &H4369E96A, &H346ED9FC, &HAD678846, &HDA60B8D0, _
                &H44042D73, &H33031DE5, &HAA0A4C5F, &HDD0D7CC9, _
                &H5005713C, &H270241AA, &HBE0B1010, &HC90C2086, _
                &H5768B525, &H206F85B3, &HB966D409, &HCE61E49F, _
                &H5EDEF90E, &H29D9C998, &HB0D09822, &HC7D7A8B4, _
                &H59B33D17, &H2EB40D81, &HB7BD5C3B, &HC0BA6CAD, _
                &HEDB88320, &H9ABFB3B6, &H3B6E20C, &H74B1D29A, _
                &HEAD54739, &H9DD277AF, &H4DB2615, &H73DC1683, _
                &HE3630B12, &H94643B84, &HD6D6A3E, &H7A6A5AA8, _
                &HE40ECF0B, &H9309FF9D, &HA00AE27, &H7D079EB1, _
                &HF00F9344, &H8708A3D2, &H1E01F268, &H6906C2FE, _
                &HF762575D, &H806567CB, &H196C3671, &H6E6B06E7, _
                &HFED41B76, &H89D32BE0, &H10DA7A5A, &H67DD4ACC, _
                &HF9B9DF6F, &H8EBEEFF9, &H17B7BE43, &H60B08ED5, _
                &HD6D6A3E8, &HA1D1937E, &H38D8C2C4, &H4FDFF252, _
                &HD1BB67F1, &HA6BC5767, &H3FB506DD, &H48B2364B, _
                &HD80D2BDA, &HAF0A1B4C, &H36034AF6, &H41047A60, _
                &HDF60EFC3, &HA867DF55, &H316E8EEF, &H4669BE79, _
                &HCB61B38C, &HBC66831A, &H256FD2A0, &H5268E236, _
                &HCC0C7795, &HBB0B4703, &H220216B9, &H5505262F, _
                &HC5BA3BBE, &HB2BD0B28, &H2BB45A92, &H5CB36A04, _
                &HC2D7FFA7, &HB5D0CF31, &H2CD99E8B, &H5BDEAE1D, _
                &H9B64C2B0, &HEC63F226, &H756AA39C, &H26D930A, _
                &H9C0906A9, &HEB0E363F, &H72076785, &H5005713, _
                &H95BF4A82, &HE2B87A14, &H7BB12BAE, &HCB61B38, _
                &H92D28E9B, &HE5D5BE0D, &H7CDCEFB7, &HBDBDF21, _
                &H86D3D2D4, &HF1D4E242, &H68DDB3F8, &H1FDA836E, _
                &H81BE16CD, &HF6B9265B, &H6FB077E1, &H18B74777, _
                &H88085AE6, &HFF0F6A70, &H66063BCA, &H11010B5C, _
                &H8F659EFF, &HF862AE69, &H616BFFD3, &H166CCF45, _
                &HA00AE278, &HD70DD2EE, &H4E048354, &H3903B3C2, _
                &HA7672661, &HD06016F7, &H4969474D, &H3E6E77DB, _
                &HAED16A4A, &HD9D65ADC, &H40DF0B66, &H37D83BF0, _
                &HA9BCAE53, &HDEBB9EC5, &H47B2CF7F, &H30B5FFE9, _
                &HBDBDF21C, &HCABAC28A, &H53B39330, &H24B4A3A6, _
                &HBAD03605, &HCDD70693, &H54DE5729, &H23D967BF, _
                &HB3667A2E, &HC4614AB8, &H5D681B02, &H2A6F2B94, _
                &HB40BBE37, &HC30C8EA1, &H5A05DF1B, &H2D02EF8D}

            ''' <summary>Gets the value of the computed hash code.</summary>
            ''' <returns>The current value of the computed hash code.</returns>
            ''' <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
            Public Overrides ReadOnly Property Hash() As Byte()
                Get
                    If isDisposed Then Throw New ObjectDisposedException([GetType]().Name)
                    Dim b() As Byte = BitConverter.GetBytes(Not result)
                    Array.Reverse(b)
                    Return b
                End Get
            End Property
            ''' <summary>True when this instance was already disposed</summary>
            Private isDisposed As Boolean
            ''' <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
            ''' <param name="disposing">Is disposing</param>
            Protected Overrides Sub Dispose(disposing As Boolean)
                MyBase.Dispose(disposing)
                isDisposed = True
            End Sub

        End Class

#End Region

#Region "IDisposable Support"
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean

        ''' <summary>Implements <see cref="IDisposable.Dispose"/></summary>
        ''' <param name="disposing">True if disposing</param>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Alghoritm.Dispose()
                End If
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <filterpriority>2</filterpriority>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
