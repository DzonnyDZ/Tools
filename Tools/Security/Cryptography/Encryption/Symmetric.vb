'http://www.codeproject.com/KB/security/SimpleEncryption.aspx
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration
Imports System.ComponentModel

Namespace SecurityT.CryptographyT.EncryptionT
    ''' <summary>Symmetric encryption uses a single key to encrypt and decrypt.</summary>
    ''' <remarks>Both parties (encryptor and decryptor) must share the same secret key.</remarks>
    Public Class Symmetric
        ''' <summary>Default initialization vector</summary>
        Private Const DefaultIntializationVector As String = "%1Az=-@qT"
        Private Const BufferSize As Integer = 2048

        ''' <summary>Symetric encryption providers</summary>
        Public Enum SymmetricProvider
            ''' <summary>The Data Encryption Standard provider supports a 64 bit key only</summary>
            DES
            ''' <summary>The Rivest Cipher 2 provider supports keys ranging from 40 to 128 bits, default is 128 bits</summary>
            RC2
            ''' <summary>The Rijndael (also known as AES) provider supports keys of 128, 192, or 256 bits with a default of 256 bits</summary>
            Rijndael
            ''' <summary>The TripleDES provider (also known as 3DES) supports keys of 128 or 192 bits with a default of 192 bits</summary>
            TripleDES
        End Enum

        Private _data As EncryptionData
        Private _key As EncryptionData
        Private _intializationVector As EncryptionData
        Private ReadOnly _algnoritm As SymmetricAlgorithm
        Private _encryptedBytes As Byte()

        ''' <summary>Instantiates a new symmetric encryption object using the specified provider.</summary>
        ''' <param name="provider">Identifies symmetric cryptography alghoritm</param>
        ''' <param name="useDefaultInitializationVector">True to use default initialization vector, false to use random one</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="provider"/> is not one of <see cref="SymmetricProvider"/> values</exception>
        Public Sub New(ByVal provider As SymmetricProvider, Optional ByVal useDefaultInitializationVector As Boolean = True)
            Me.New(GetAlghoritm(provider))
        End Sub
        ''' <summary>Instantiates a new symmetric encryption object using the specified provider.</summary>
        ''' <param name="alghoritm">A symmetric cryptography alghoritm</param>
        ''' <param name="useDefaultInitializationVector">True to use default initialization vector, false to use random one</param>
        ''' <exception cref="ArgumentNullException"><paramref name="alghoritm"/> is null</exception>
        Public Sub New(alghoritm As SymmetricAlgorithm, Optional useDefaultInitializationVector As Boolean = True)
            If alghoritm Is Nothing Then Throw New ArgumentNullException("alghoritm")
            _algnoritm = alghoritm
            '-- make sure key and IV are always set, no matter what
            Me.Key = RandomKey()
            If useDefaultInitializationVector Then
                Me.IntializationVector = New EncryptionData(DefaultIntializationVector)
            Else
                Me.IntializationVector = RandomInitializationVector()
            End If
        End Sub

        ''' <summary>Converts <see cref="SymmetricProvider"/> to <see cref="SymmetricAlgorithm"/></summary>
        ''' <param name="provider">Identifies symmetric cryptography provider</param>
        ''' <returns>A new instance of <see cref="SymmetricAlgorithm"/>-derived class</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="provider"/> is not one of <see cref="SymmetricProvider"/> values</exception>
        Public Shared Function GetAlghoritm(ByVal provider As SymmetricProvider) As SymmetricAlgorithm
            Select Case provider
                Case SymmetricProvider.DES : Return New DESCryptoServiceProvider
                Case SymmetricProvider.RC2 : Return New RC2CryptoServiceProvider
                Case SymmetricProvider.Rijndael : Return New RijndaelManaged
                Case SymmetricProvider.TripleDES : Return New TripleDESCryptoServiceProvider
                Case Else : Throw New InvalidEnumArgumentException("provider", provider, provider.GetType)
            End Select
        End Function

        ''' <summary>Gets a cryptography alghoritm used by this instance</summary>
        Public ReadOnly Property Algnoritm() As SymmetricAlgorithm
            Get
                Return _algnoritm
            End Get
        End Property
        ''' <summary>Gets or sets key size in bytes.</summary>
        ''' <remarks>Default key size is used for any given provider; if you want to force a specific key size, set this property</remarks>
        ''' <seelaso cref="KeySizeBits"/>
        Public Property KeySizeBytes() As Integer
            Get
                Return Algnoritm.KeySize \ 8
            End Get
            Set(ByVal Value As Integer)
                Algnoritm.KeySize = Value * 8
                _key.MaxBytes = Value
            End Set
        End Property

        ''' <summary>Gets or sets key size in bits.</summary>
        ''' <remarks>Default key size is used for any given provider; if you want to force a specific key size, set this property</remarks>
        ''' <seelaso cref="KeySizeBytes"/>
        Public Property KeySizeBits() As Integer
            Get
                Return Algnoritm.KeySize
            End Get
            Set(ByVal Value As Integer)
                Algnoritm.KeySize = Value
                _key.MaxBits = Value
            End Set
        End Property

        ''' <summary>Gets or sets the key used to encrypt/decrypt data</summary>
        Public Property Key() As EncryptionData
            Get
                Return _key
            End Get
            Set(ByVal Value As EncryptionData)
                _key = Value
                _key.MaxBytes = Algnoritm.LegalKeySizes(0).MaxSize \ 8
                _key.MinBytes = Algnoritm.LegalKeySizes(0).MinSize \ 8
                _key.StepBytes = Algnoritm.LegalKeySizes(0).SkipSize \ 8
            End Set
        End Property

        ''' <summary>Gets or sets initialization vector used by this instance</summary>
        ''' <remarks>
        ''' Using the default Cipher Block Chaining (CBC) mode, all data blocks are processed using
        ''' the value derived from the previous block; the first data block has no previous data block
        ''' to use, so it needs an InitializationVector to feed the first block
        ''' </remarks>
        Public Property IntializationVector() As EncryptionData
            Get
                Return _intializationVector
            End Get
            Set(ByVal Value As EncryptionData)
                _intializationVector = Value
                _intializationVector.MaxBytes = Algnoritm.BlockSize \ 8
                _intializationVector.MinBytes = Algnoritm.BlockSize \ 8
            End Set
        End Property

        ''' <summary>Generates a random Initialization Vector</summary>
        ''' <remarks>An ititialization vector in form of <see cref="EncryptionData"/></remarks>
        Public Function RandomInitializationVector() As EncryptionData
            Algnoritm.GenerateIV()
            Dim d As New EncryptionData(Algnoritm.IV)
            Return d
        End Function

        ''' <summary>Generates a random Key</summary>
        ''' <returns>An encryption key in form of <see cref="EncryptionData"/></returns>
        Public Function RandomKey() As EncryptionData
            Algnoritm.GenerateKey()
            Dim d As New EncryptionData(Algnoritm.Key)
            Return d
        End Function

        ''' <summary>Ensures that _crypto object has valid Key and IV prior to any attempt to encrypt/decrypt anything</summary>
        Private Sub ValidateKeyAndIv(ByVal isEncrypting As Boolean)
            If _key.IsEmpty Then
                If isEncrypting Then
                    _key = RandomKey()
                Else
                    Throw New CryptographicException(ResourcesT.Exceptions.DecryptKeyMissing)
                End If
            End If
            If _intializationVector.IsEmpty Then
                If isEncrypting Then
                    _intializationVector = RandomInitializationVector()
                Else
                    Throw New CryptographicException(ResourcesT.Exceptions.DecryptInitializationVectorMissing)
                End If
            End If
            Algnoritm.Key = _key.Bytes
            Algnoritm.IV = _intializationVector.Bytes
        End Sub

#Region "Encrypt"
        ''' <summary>Encrypts the specified Data using provided key</summary>
        ''' <param name="data">A data to be encrypted</param>
        ''' <param name="key">An encryption key; ignored if null</param>
        ''' <returns>Encrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        Public Function Encrypt(ByVal data As EncryptionData, ByVal key As EncryptionData) As EncryptionData
            Dim oldkey = Me.Key
            Try
                If key IsNot Nothing Then Me.Key = key
                Return Encrypt(data)
            Finally
                Me.Key = oldkey
            End Try
        End Function

        ''' <summary>Encrypts the specified Data using preset key and preset initialization vector</summary>
        ''' <param name="data">A data to be encrypted</param>
        ''' <returns>Encryptred data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        Public Function Encrypt(ByVal data As EncryptionData) As EncryptionData
            If data Is Nothing Then Throw New ArgumentNullException("data")
            Dim ms As New IO.MemoryStream

            ValidateKeyAndIv(True)

            Dim cs As New CryptoStream(ms, Algnoritm.CreateEncryptor(), CryptoStreamMode.Write)
            cs.Write(data.Bytes, 0, data.Bytes.Length)
            cs.Close()
            ms.Close()

            Return New EncryptionData(ms.ToArray) With {.Encoding = data.Encoding}
        End Function

        ''' <summary>Encrypts the stream to memory using provided key and provided initialization vector</summary>
        ''' <param name="stream">A stream to encrypt</param>
        ''' <param name="key">An encryption key; ignored if null</param>
        ''' <param name="iv">Initialization vector; ignored if nulll</param>
        ''' <returns>Encrypted data</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="stream"/> is null</exception>
        Public Function Encrypt(ByVal stream As Stream, ByVal key As EncryptionData, ByVal iv As EncryptionData) As EncryptionData
            Dim oldIv = Me.RandomInitializationVector
            Dim oldKey = Me.Key
            Try
                If iv IsNot Nothing Then Me.IntializationVector = iv
                If key IsNot Nothing Then Me.Key = key
                Return Encrypt(stream)
            Finally
                Me.IntializationVector = oldIv
                Me.Key = oldKey
            End Try
        End Function

        ''' <summary>Encrypts the stream to memory using specified key</summary>
        ''' <param name="stream">A stream to encrypt</param>
        ''' <param name="key">An encryption key; ignored if null</param>
        ''' <returns>Encrypted data</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="stream"/> is null</exception>
        Public Function Encrypt(ByVal stream As Stream, ByVal key As EncryptionData) As EncryptionData
            Dim oldKey = Me.Key
            Try
                If key IsNot Nothing Then Me.Key = key
                Return Encrypt(stream)
            Finally
                Me.Key = oldKey
            End Try
        End Function

        ''' <summary>Encrypts the specified stream to memory using preset key and preset initialization vector</summary>
        ''' <param name="stream">A stream to encrypt</param>
        ''' <returns>Encrypted data</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="stream"/> is null</exception>
        Public Function Encrypt(ByVal stream As Stream) As EncryptionData
            If stream Is Nothing Then Throw New ArgumentNullException("stream")
            Dim ms As New IO.MemoryStream
            Dim b(BufferSize) As Byte
            Dim i As Integer

            ValidateKeyAndIv(True)

            Dim cs As New CryptoStream(ms, Algnoritm.CreateEncryptor(), CryptoStreamMode.Write)
            i = stream.Read(b, 0, BufferSize)
            Do While i > 0
                cs.Write(b, 0, i)
                i = stream.Read(b, 0, BufferSize)
            Loop

            cs.Close()
            ms.Close()

            Return New EncryptionData(ms.ToArray)
        End Function
#End Region

#Region "Decrypt"
        ''' <summary>Decrypts the specified data using provided key and preset initialization vector</summary>
        ''' <param name="encryptedData">Encrypted data to decrypt</param>
        ''' <param name="key">An encryption key; ignored if null</param>
        ''' <returns>Decrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="encryptedData"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="encryptedData"/> is null</exception>
        ''' <exception cref="CryptographicException">Cannot decrypt data. The key may be invalid.</exception>
        Public Function Decrypt(ByVal encryptedData As EncryptionData, ByVal key As EncryptionData) As EncryptionData
            Dim oldkey = Me.Key
            Try
                If key IsNot Nothing Then Me.Key = key
                Return Decrypt(encryptedData)
            Finally
                Me.Key = oldkey
            End Try
        End Function

        ''' <summary>Decrypts the specified stream using provided key and preset initialization vector</summary>
        ''' <param name="encryptedStream">A stream to decrypt</param>
        ''' <param name="key">An encryption key; ignored if null</param>
        ''' <exception cref="ArgumentNullException"><paramref name="encryptedStream"/> is null</exception>
        ''' <exception cref="CryptographicException">Cannot decrypt data. The key may be invalid.</exception>
        Public Function Decrypt(ByVal encryptedStream As Stream, ByVal key As EncryptionData) As EncryptionData
            Dim oldkey = Me.Key
            Try
                If key IsNot Nothing Then Me.Key = key
                Return Decrypt(encryptedStream)
            Finally
                Me.Key = oldkey
            End Try
        End Function

        ''' <summary>Decrypts the specified stream using preset key and preset initialization vector</summary>
        ''' <param name="encryptedStream">A stream to decrypt</param>
        ''' <returns>Decrypted data</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="encryptedStream"/> is null</exception>
        ''' <exception cref="CryptographicException">Cannot decrypt data. The key may be invalid.</exception>
        Public Function Decrypt(ByVal encryptedStream As Stream) As EncryptionData
            If encryptedStream Is Nothing Then Throw New ArgumentNullException("encryptedStream")
            Dim ms As New System.IO.MemoryStream
            Dim b(BufferSize) As Byte

            ValidateKeyAndIv(False)
            Dim cs As New CryptoStream(encryptedStream, _
                Algnoritm.CreateDecryptor(), CryptoStreamMode.Read)

            Dim i As Integer
            i = cs.Read(b, 0, BufferSize)

            Do While i > 0
                ms.Write(b, 0, i)
                i = cs.Read(b, 0, BufferSize)
            Loop
            cs.Close()
            ms.Close()

            Return New EncryptionData(ms.ToArray)
        End Function

        ''' <summary>Decrypts the specified data using preset key and preset initialization vector</summary>
        ''' <param name="encryptedData">A data to decrypt</param>
        ''' <returns>Decrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="encryptedData"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="encryptedData"/> is null</exception>
        ''' <exception cref="CryptographicException">Cannot decrypt data. The key may be invalid.</exception>
        Public Function Decrypt(ByVal encryptedData As EncryptionData) As EncryptionData
            If encryptedData Is Nothing Then Throw New ArgumentNullException("encryptedData")
            Dim ms As New System.IO.MemoryStream(encryptedData.Bytes, 0, encryptedData.Bytes.Length)
            Dim b() As Byte = New Byte(encryptedData.Bytes.Length - 1) {}

            ValidateKeyAndIv(False)
            Dim cs As New CryptoStream(ms, Algnoritm.CreateDecryptor(), CryptoStreamMode.Read)

            Try
                cs.Read(b, 0, encryptedData.Bytes.Length - 1)
            Finally
                cs.Close()
            End Try
            Return New EncryptionData(b) With {.Encoding = encryptedData.Encoding}
        End Function
#End Region
    End Class

End Namespace
