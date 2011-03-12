'http://www.codeproject.com/KB/security/SimpleEncryption.aspx
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace SecurityT.CryptographyT.EncryptionT

    ''' <summary>Asymmetric encryption uses a pair of keys to encrypt and decrypt.</summary>
    ''' <remarks>
    ''' There is a "public" key which is used to encrypt. Decrypting, on the other hand, 
    ''' requires both the "public" key and an additional "private" key. The advantage is 
    ''' that people can send you encrypted messages without being able to decrypt them.
    ''' <note>The only provider supported is the <see cref="RSACryptoServiceProvider"/></note>
    ''' </remarks>
    Public Class Asymmetric

        Private _rsa As RSACryptoServiceProvider
        Private _UseMachineKeystore As Boolean = True
        Private _KeySize As Integer = 1024
#Region "Constatnts"
        Private Const ElementParent As String = "RSAKeyValue"
        Private Const ElementModulus As String = "Modulus"
        Private Const ElementExponent As String = "Exponent"
        Private Const ElementPrimeP As String = "P"
        Private Const ElementPrimeQ As String = "Q"
        Private Const ElementPrimeExponentP As String = "DP"
        Private Const ElementPrimeExponentQ As String = "DQ"
        Private Const ElementCoefficient As String = "InverseQ"
        Private Const ElementPrivateExponent As String = "D"


        ''' <summary>Identifies name of application setting key used to store default public key modulus in app.config or web.config file</summary>
        Public Const KeyModulus As String = "PublicKey.Modulus"
        ''' <summary>Identifies name of application setting key used to store default public key exponent in app.config or web.config file</summary>
        Public Const KeyExponent As String = "PublicKey.Exponent"
        ''' <summary>Identifies name of application setting key used to store default private key P parameter in app.config or web.config file</summary>
        Public Const KeyPrimeP As String = "PrivateKey.P"
        ''' <summary>Identifies name of application setting key used to store default private key Q parameter in app.config or web.config file</summary>
        Public Const KeyPrimeQ As String = "PrivateKey.Q"
        ''' <summary>Identifies name of application setting key used to store default private key DP parameter in app.config or web.config file</summary>
        Public Const KeyPrimeExponentP As String = "PrivateKey.DP"
        ''' <summary>Identifies name of application setting key used to store default private key DQ parameter in app.config or web.config file</summary>
        Public Const KeyPrimeExponentQ As String = "PrivateKey.DQ"
        ''' <summary>Identifies name of application setting key used to store default private key InverseQ parameter in app.config or web.config file</summary>
        Public Const KeyCoefficient As String = "PrivateKey.InverseQ"
        ''' <summary>Identifies name of application setting key used to store default private key D parameter in app.config or web.config file</summary>
        Public Const KeyPrivateExponent As String = "PrivateKey.D"
#End Region

#Region "PublicKey Class"
        ''' <summary>Represents a public encryption key. Intended to be shared, itcontains only the Modulus and Exponent.</summary>
        Public Class PublicKey
#Region "Properties"
            Private ReadOnly _exponent As String
            Private ReadOnly _modulus As String
            ''' <summary>Gets modulus of this key</summary>
            Public ReadOnly Property Modulus As String
                Get
                    Return _modulus
                End Get
            End Property
            ''' <summary>Gets exponent of this key</summary>
            Public ReadOnly Property Exponent As String
                Get
                    Return _exponent
                End Get
            End Property
#End Region

#Region "CTors"
            ''' <summary>Creates a new instance of the <see cref="PublicKey"/> class from modulus and exponent base64 values</summary>
            ''' <param name="modulus">Modulus value (base64-encoded)</param>
            ''' <param name="exponent">Exponent value (base64-encoded)</param>
            ''' <exception cref="ArgumentNullException"><paramref name="modulus"/> or <paramref name="exponent"/> is null</exception>
            ''' <exception cref="FormatException">
            ''' The length of <paramref name="modulus"/> or <paramref name="exponent"/>, ignoring white-space characters, is not zero or a multiple of 4. -or-
            ''' The format of <paramref name="modulus"/> or <paramref name="exponent"/> is invalid. <paramref name="modulus"/> or <paramref name="exponent"/> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.
            ''' </exception>
            Public Sub New(modulus$, exponent$)
                Convert.FromBase64String(modulus)
                Convert.FromBase64String(exponent)
                _modulus = modulus
                _exponent = exponent
            End Sub

            ''' <summary>Creates a new instance ot the <see cref="PublicKey"/> from modulus and exponent binary values</summary>
            ''' <param name="modulus">Modulus value</param>
            ''' <param name="exponent">Exponent value</param>
            ''' <exception cref="ArgumentNullException"><paramref name="modulus"/> or <paramref name="exponent"/> is null</exception>
            Public Sub New(modulus As Byte(), exponent As Byte())
                _modulus = Convert.ToBase64String(modulus)
                _exponent = Convert.ToBase64String(exponent)
            End Sub

            ''' <summary>CTor - creates a new instance of the <see cref="PublicKey"/> class from <see cref="RSAParameters"/></summary>
            ''' <param name="parameters">A <see cref="RSAParameters"/> object</param>
            Public Sub New(parameters As RSAParameters)
                Me.New(Convert.ToBase64String(parameters.Exponent), Convert.ToBase64String(parameters.Modulus))
            End Sub
            ''' <summary>CTor - creates a new instance of the <see cref="PublicKey"/> class from string representing its XML representation</summary>
            ''' <param name="keyXml">A string containing XML representation of public key</param>
            Public Sub New(ByVal keyXml As String)
                Me.New(EncryptionUtilities.GetXmlElement(keyXml, ElementModulus), EncryptionUtilities.GetXmlElement(keyXml, ElementExponent))
            End Sub
#End Region

#Region "Shared conversions"
            ''' <summary>Loads public key from current web.config or app.config file using default names of settings</summary>
            ''' <returns>A new instance of the <see cref="PublicKey"/> representing default public key stored in current app.config or web.config file</returns>
            Public Shared Function FromConfig() As PublicKey
                Return FromConfig(KeyModulus, KeyExponent)
            End Function
            ''' <summary>Loads public key from current web.config or app.config file using custom names of settings</summary>
            ''' <param name="keyModulus">Key of app.config or web.config application settting stroring public key's modulus</param>
            ''' <param name="keyExponent">Key of app.config or web.config application settting stroring public key's exponent</param>
            ''' <returns>A new instance of the <see cref="PublicKey"/> representing public key stored in current app.config or web.config file</returns>
            Public Shared Function FromConfig(keyModulus$, keyExponent$) As PublicKey
                Return New PublicKey(EncryptionUtilities.GetConfigString(keyModulus), EncryptionUtilities.GetConfigString(keyExponent))
            End Function

            ''' <summary>Loads public key from string containing it's XML representation</summary>
            ''' <param name="xml">String containing XML representation of public key</param>
            ''' <returns>A ne winstance of the <see cref="PublicKey"/> class</returns>
            Public Shared Function FromXmlString(xml As String) As PublicKey
                Return New PublicKey(xml)
            End Function
#End Region

#Region "Conversions"
            ''' <summary>Returns app.config or web.config file XML section representing this public key (to be stored under default names of application settings)</summary>
            ''' <returns>A string for app.config or web.config file</returns>
            ''' <seelaso cref="KeyModulus"/><seelaso cref="KeyExponent"/>
            Public Overridable Function ToConfigSection() As String
                Return ToConfigSection(KeyModulus, KeyExponent)
            End Function

            ''' <summary>Returns app.config or web.config file XML section representing this public key  (to be stored under custom names of application settings)</summary>
            ''' <param name="keyModulus">Name of application setting to store modulus in</param>
            ''' <param name="keyExponent">Name of application setting to store exponent in</param>
            ''' <returns>A string for app.config or web.config file</returns>
            Public Overridable Function ToConfigSection(keyModulus$, keyExponent$) As String
                Dim sb As New StringBuilder
                With sb
                    .Append(EncryptionUtilities.WriteConfigKey(keyModulus, Me.Modulus))
                    .Append(EncryptionUtilities.WriteConfigKey(keyExponent, Me.Exponent))
                End With
                Return sb.ToString
            End Function

            ''' <summary>Writes the app.config or web.config file representation of this public key to a file (as default key)</summary>
            ''' <param name="filePath">Path of configuration file to write key to</param>
            Public Overridable Sub ExportToConfigFile(ByVal filePath As String)
                ExportToConfigFile(filePath, KeyModulus, KeyExponent)
            End Sub

            ''' <summary>Writes the app.config or web.config file representation of this public key to a file (under custom settings names)</summary>
            ''' <param name="keyModulus">Name of application setting to store modulus in</param>
            ''' <param name="keyExponent">Name of application setting to store exponent in</param>
            ''' <param name="filePath">Path of configuration file to write key to</param>
            Public Overridable Sub ExportToConfigFile(ByVal filePath As String, keyModulus$, keyExponent$)
                Dim sw As New StreamWriter(filePath, False)
                sw.Write(Me.ToConfigSection(keyModulus, keyExponent))
                sw.Close()
            End Sub

            ''' <summary>Converts this public key to its XML string representation</summary>
            ''' <returns>A string containing XML representation of this key</returns>
            Public Overridable Function ToXml() As String
                Dim sb As New StringBuilder
                With sb
                    .Append(EncryptionUtilities.WriteXmlNode(ElementParent))
                    .Append(EncryptionUtilities.WriteXmlElement(ElementModulus, Me.Modulus))
                    .Append(EncryptionUtilities.WriteXmlElement(ElementExponent, Me.Exponent))
                    .Append(EncryptionUtilities.WriteXmlNode(ElementParent, True))
                End With
                Return sb.ToString
            End Function

            ''' <summary>Writes the Xml representation of this public key to a file</summary>
            ''' <param name="filePath">A path of a XML file</param>
            Public Sub ExportToXmlFile(ByVal filePath As String)
                Dim sw As New StreamWriter(filePath, False)
                sw.Write(Me.ToXml)
                sw.Close()
            End Sub
#End Region

            ''' <summary>Converts this public key to an RSAParameters object</summary>
            ''' <returns>A <see cref="RSAParameters"/> object</returns>
            Public Overridable Function ToParameters() As RSAParameters
                Dim r As New RSAParameters
                r.Modulus = Convert.FromBase64String(Me.Modulus)
                r.Exponent = Convert.FromBase64String(Me.Exponent)
                Return r
            End Function
        End Class
#End Region

#Region "PrivateKey Class"

        ''' <summary>Represents a private encryption key. Not intended to be shared, as it contains all the elements that make up the key.</summary>
        Public Class PrivateKey : Inherits PublicKey
#Region "Properties"
            Private ReadOnly _primeP As String
            Private ReadOnly _primeQ As String
            Private ReadOnly _primeExponentP As String
            Private ReadOnly _primeExponentQ As String
            Private ReadOnly _coefficient As String
            Private ReadOnly _privateExponent As String
            ''' <summary>Gets the P parameter</summary>
            Public ReadOnly Property PrimeP() As String
                Get
                    Return _primeP
                End Get
            End Property
            ''' <summary>Gets the Q parameter</summary>
            Public ReadOnly Property PrimeQ() As String
                Get
                    Return _primeQ
                End Get
            End Property
            ''' <summary>Gets the prime exponent P</summary>
            Public ReadOnly Property PrimeExponentP() As String
                Get
                    Return _primeExponentP
                End Get
            End Property
            ''' <summary>Gets the prime exponent Q</summary>
            Public ReadOnly Property PrimeExponentQ() As String
                Get
                    Return _primeExponentQ
                End Get
            End Property
            ''' <summary>Gets the coeficient</summary>
            Public ReadOnly Property Coefficient() As String
                Get
                    Return _coefficient
                End Get
            End Property
            ''' <summary>Gets the private exponent</summary>
            Public ReadOnly Property PrivateExponent() As String
                Get
                    Return _privateExponent
                End Get
            End Property
#End Region

#Region "CTors"
            ''' <summary>Creates a new instance of the <see cref="PublicKey"/> class from base64-encoded values</summary>
            ''' <param name="modulus">Modulus value (base64-encoded)</param>
            ''' <param name="exponent">Exponent value (base64-encoded)</param>
            ''' <param name="coefficient">A coeficient (base64-encoded)</param>
            ''' <param name="primeExponentP">A prime exponent P (base64-encoded)</param>
            ''' <param name="primeExponentQ">A prime exponent Q (base64-encoded)</param>
            ''' <param name="primeP">A prime P (base64-encoded)</param>
            ''' <param name="primeQ">A prime Q (base64-encoded)</param>
            ''' <param name="privateExponent">A private exponent (base64-encoded)</param>
            ''' <exception cref="ArgumentNullException"><paramref name="modulus"/> or <paramref name="exponent"/> is null</exception>
            ''' <exception cref="FormatException">
            ''' The length of an argument, ignoring white-space characters, is not zero or a multiple of 4. -or-
            ''' The format of an argument is invalid. An argument contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.
            ''' </exception>
            Public Sub New(modulus$, exponent$, primeP$, primeQ$, primeExponentP$, primeExponentQ$, coefficient$, privateExponent$)
                MyBase.New(modulus, exponent)
                Convert.FromBase64String(primeP)
                Convert.FromBase64String(primeQ)
                Convert.FromBase64String(primeExponentP)
                Convert.FromBase64String(primeExponentQ)
                Convert.FromBase64String(coefficient)
                Convert.FromBase64String(exponent)
                _primeP = primeP
                _primeQ = primeQ
                _primeExponentP = primeExponentP
                _primeExponentQ = primeExponentQ
                _coefficient = coefficient$
                _privateExponent = privateExponent
            End Sub

            ''' <summary>Creates a new instance ot the <see cref="PublicKey"/> from binary values</summary>
            ''' <param name="modulus">Modulus value</param>
            ''' <param name="exponent">Exponent value</param>
            ''' <param name="coefficient">A coeficient</param>
            ''' <param name="primeExponentP">A prime exponent P</param>
            ''' <param name="primeExponentQ">A prime exponent Q</param>
            ''' <param name="primeP">A prime P</param>
            ''' <param name="primeQ">A prime Q</param>
            ''' <param name="privateExponent">A private exponent</param>
            ''' <exception cref="ArgumentNullException">Any arguments is null</exception>
            Public Sub New(modulus As Byte(), exponent As Byte(), primeP As Byte(), primeQ As Byte(), primeExponentP As Byte(), primeExponentQ As Byte(), coefficient As Byte(), privateExponent As Byte())
                MyBase.New(modulus, exponent)
                _primeP = Convert.ToBase64String(primeP)
                _primeP = Convert.ToBase64String(primeQ)
                _primeExponentP = Convert.ToBase64String(primeExponentP)
                _primeExponentQ = Convert.ToBase64String(primeExponentQ)
                _coefficient = Convert.ToBase64String(coefficient)
                _privateExponent = Convert.ToBase64String(privateExponent)
            End Sub

            ''' <summary>CTor - creates a new instance of the <see cref="PublicKey"/> class from <see cref="RSAParameters"/></summary>
            ''' <param name="parameters">A <see cref="RSAParameters"/> object</param>
            Public Sub New(parameters As RSAParameters)
                Me.New(parameters.Exponent, parameters.Modulus, parameters.P, parameters.Q, parameters.DP, parameters.DQ, parameters.InverseQ, parameters.D)
            End Sub
            ''' <summary>CTor - creates a new instance of the <see cref="PublicKey"/> class from string representing its XML representation</summary>
            ''' <param name="keyXml">A string containing XML representation of public key</param>
            Public Sub New(ByVal keyXml As String)
                Me.New(EncryptionUtilities.GetXmlElement(keyXml, ElementModulus),
                       EncryptionUtilities.GetXmlElement(keyXml, ElementExponent),
                       EncryptionUtilities.GetXmlElement(keyXml, ElementPrimeP),
                       EncryptionUtilities.GetXmlElement(keyXml, ElementPrimeQ),
                       EncryptionUtilities.GetXmlElement(keyXml, ElementPrimeExponentP),
                       EncryptionUtilities.GetXmlElement(keyXml, ElementPrimeExponentQ),
                       EncryptionUtilities.GetXmlElement(keyXml, ElementCoefficient),
                       EncryptionUtilities.GetXmlElement(keyXml, ElementPrivateExponent)
                      )
            End Sub
#End Region

#Region "Shared conversions"
            ''' <summary>Loads public key from current web.config or app.config file using default names of settings</summary>
            ''' <returns>A new instance of the <see cref="PublicKey"/> representing default public key stored in current app.config or web.config file</returns>
            Public Shared Shadows Function FromConfig() As PrivateKey
                Return FromConfig(KeyModulus, KeyExponent, KeyPrimeP$, KeyPrimeQ$, KeyPrimeExponentP$, KeyPrimeExponentQ$, KeyCoefficient$, KeyPrivateExponent$)
            End Function
            ''' <summary>Loads public key from current web.config or app.config file using custom names of settings</summary>
            ''' <param name="keyModulus">Key of app.config or web.config application settting stroring public key's modulus</param>
            ''' <param name="keyExponent">Key of app.config or web.config application settting stroring public key's exponent</param>
            ''' <returns>A new instance of the <see cref="PublicKey"/> representing public key stored in current app.config or web.config file</returns>
            Public Shared Shadows Function FromConfig(keyModulus$, keyExponent$, KeyPrimeP$, KeyPrimeQ$, KeyPrimeExponentP$, KeyPrimeExponentQ$, KeyCoefficient$, KeyPrivateExponent$) As PrivateKey
                Return New PrivateKey(
                    EncryptionUtilities.GetConfigString(keyModulus),
                    EncryptionUtilities.GetConfigString(keyExponent),
                    EncryptionUtilities.GetConfigString(KeyPrimeP$),
                    EncryptionUtilities.GetConfigString(KeyPrimeQ$),
                    EncryptionUtilities.GetConfigString(KeyPrimeExponentP$),
                    EncryptionUtilities.GetConfigString(KeyPrimeExponentQ$),
                    EncryptionUtilities.GetConfigString(KeyCoefficient$),
                    EncryptionUtilities.GetConfigString(KeyPrivateExponent$)
                   )
            End Function

            ''' <summary>Loads public key from string containing it's XML representation</summary>
            ''' <param name="xml">String containing XML representation of public key</param>
            ''' <returns>A ne winstance of the <see cref="PublicKey"/> class</returns>
            Public Shared Shadows Function FromXmlString(xml As String) As PrivateKey
                Return New PrivateKey(xml)
            End Function
#End Region

#Region "Conversions"
            ''' <summary>Returns app.config or web.config file XML section representing this public key (to be stored under default names of application settings)</summary>
            ''' <returns>A string for app.config or web.config file</returns>
            ''' <seelaso cref="KeyModulus"/><seelaso cref="KeyExponent"/>
            ''' <seealso cref="KeyPrimeP"/><seealso cref="KeyPrimeQ"/><seealso cref="KeyPrimeExponentP"/><seealso cref="KeyPrimeExponentQ"/><seealso cref="KeyCoefficient"/><seealso cref="KeyPrivateExponent"/>
            Public Overrides Function ToConfigSection() As String
                Return ToConfigSection(KeyModulus, KeyExponent, KeyPrimeP$, KeyPrimeQ$, KeyPrimeExponentP$, KeyPrimeExponentQ$, KeyCoefficient$, KeyPrivateExponent$)
            End Function

            ''' <summary>Returns app.config or web.config file XML section representing this public part of this key (to be stored under custom names of application settings)</summary>
            ''' <param name="keyExponent">Name of application setting to store <see cref="Exponent"/> in</param>
            ''' <param name="keyModulus">Name of application setting to store <see cref="Modulus"/> in</param>
            ''' <param name="keyPrimeP">Name of application setting to store <see cref="PrimeP"/> in</param>
            ''' <param name="keyPrimeQ">Name of application setting to store <see cref="PrimeQ"/> in</param>
            ''' <param name="keyPrimeExponentP">Name of application setting to store <see cref="PrimeExponentP"/> in</param>
            ''' <param name="KeyPrimeExponentQ">Name of application setting to store <see cref="PrimeExponentQ"/> in</param>
            ''' <param name="keyCoefficient">Name of application setting to store <see cref="Coefficient"/> in</param>
            ''' <param name="keyPrivateExponent">Name of application setting to store <see cref="PrivateExponent"/> in</param>
            ''' <returns>A string for app.config or web.config file</returns>
            Public Overridable Overloads Function ToConfigSection(keyModulus$, keyExponent$, keyPrimeP$, keyPrimeQ$, keyPrimeExponentP$, KeyPrimeExponentQ$, keyCoefficient$, keyPrivateExponent$) As String
                Dim sb As New StringBuilder
                With sb
                    .Append(EncryptionUtilities.WriteConfigKey(keyModulus, Me.Modulus))
                    .Append(EncryptionUtilities.WriteConfigKey(keyExponent, Me.Exponent))
                    .Append(EncryptionUtilities.WriteConfigKey(keyPrimeP, Me.PrimeP))
                    .Append(EncryptionUtilities.WriteConfigKey(keyPrimeQ, Me.PrimeQ))
                    .Append(EncryptionUtilities.WriteConfigKey(keyPrimeExponentP, Me.PrimeExponentP))
                    .Append(EncryptionUtilities.WriteConfigKey(KeyPrimeExponentQ, Me.PrimeExponentQ))
                    .Append(EncryptionUtilities.WriteConfigKey(keyCoefficient, Me.Coefficient))
                    .Append(EncryptionUtilities.WriteConfigKey(keyPrivateExponent, Me.PrivateExponent))
                End With
                Return sb.ToString
            End Function

            ''' <summary>Returns app.config or web.config file XML section representing this public part of this key (to be stored under custom names of application settings)</summary>
            ''' <param name="keyModulus">Name of application setting to store modulus in</param>
            ''' <param name="keyExponent">Name of application setting to store exponent in</param>
            ''' <returns>A string for app.config or web.config file</returns>
            <EditorBrowsable(EditorBrowsableState.Never)>
            Public Overrides Function ToConfigSection(keyModulus$, keyExponent$) As String
                Return MyBase.ToConfigSection(keyModulus, keyExponent)
            End Function

            ''' <summary>Writes the app.config or web.config file representation of this public key to a file (as default key)</summary>
            ''' <param name="filePath">Path of configuration file to write key to</param>
            Public Overrides Sub ExportToConfigFile(ByVal filePath As String)
                ExportToConfigFile(filePath, KeyModulus, KeyExponent, KeyPrimeP$, KeyPrimeQ$, KeyPrimeExponentP$, KeyPrimeExponentQ$, KeyCoefficient$, KeyPrivateExponent$)
            End Sub

            ''' <summary>Writes the app.config or web.config file representation of this public key to a file (under custom settings names)</summary>
            ''' <param name="keyExponent">Name of application setting to store <see cref="Exponent"/> in</param>
            ''' <param name="keyModulus">Name of application setting to store <see cref="Modulus"/> in</param>
            ''' <param name="keyPrimeP">Name of application setting to store <see cref="PrimeP"/> in</param>
            ''' <param name="keyPrimeQ">Name of application setting to store <see cref="PrimeQ"/> in</param>
            ''' <param name="keyPrimeExponentP">Name of application setting to store <see cref="PrimeExponentP"/> in</param>
            ''' <param name="KeyPrimeExponentQ">Name of application setting to store <see cref="PrimeExponentQ"/> in</param>
            ''' <param name="keyCoefficient">Name of application setting to store <see cref="Coefficient"/> in</param>
            ''' <param name="keyPrivateExponent">Name of application setting to store <see cref="PrivateExponent"/> in</param>
            ''' <param name="filePath">Path of configuration file to write key to</param>
            Public Overridable Overloads Sub ExportToConfigFile(ByVal filePath As String, keyModulus$, keyExponent$, keyPrimeP$, keyPrimeQ$, keyPrimeExponentP$, KeyPrimeExponentQ$, keyCoefficient$, keyPrivateExponent$)
                Dim sw As New StreamWriter(filePath, False)
                sw.Write(Me.ToConfigSection(keyModulus, keyExponent, keyPrimeP$, keyPrimeQ$, keyPrimeExponentP$, KeyPrimeExponentQ$, keyCoefficient$, keyPrivateExponent$))
                sw.Close()
            End Sub

            ''' <summary>Writes the app.config or web.config file representation of this public part of this key to a file (under custom settings names)</summary>
            ''' <param name="keyModulus">Name of application setting to store modulus in</param>
            ''' <param name="keyExponent">Name of application setting to store exponent in</param>
            ''' <param name="filePath">Path of configuration file to write key to</param>
            <EditorBrowsable(EditorBrowsableState.Never)>
            Public Overrides Sub ExportToConfigFile(ByVal filePath As String, keyModulus$, keyExponent$)
                MyBase.ExportToConfigFile(filePath, keyModulus, keyExponent)
            End Sub
#End Region

            ''' <summary>Converts this private key to an <see cref="RSAParameters"/> object</summary>
            ''' <returns>A <see cref="RSAParameters"/> object</returns>
            Public Overrides Function ToParameters() As RSAParameters
                Dim r As New RSAParameters
                r.Modulus = Convert.FromBase64String(Me.Modulus)
                r.Exponent = Convert.FromBase64String(Me.Exponent)
                r.P = Convert.FromBase64String(Me.PrimeP)
                r.Q = Convert.FromBase64String(Me.PrimeQ)
                r.DP = Convert.FromBase64String(Me.PrimeExponentP)
                r.DQ = Convert.FromBase64String(Me.PrimeExponentQ)
                r.InverseQ = Convert.FromBase64String(Me.Coefficient)
                r.D = Convert.FromBase64String(Me.PrivateExponent)
                Return r
            End Function
        End Class

#End Region

#Region "CTors"
        ''' <summary>CTor - instantiates a new asymmetric encryption session using the default key size; this is usally 1024 bits</summary>
        Public Sub New()
            _rsa = GetRSAProvider()
        End Sub

        ''' <summary>Instantiates a new asymmetric encryption session using a specific key size</summary>
        Public Sub New(ByVal keySize As Integer)
            _KeySize = keySize
            _rsa = GetRSAProvider()
        End Sub
#End Region

#Region "Properties"
        ''' <summary>Gets or sets the name of the key container used to store this key on disk.</summary>
        ''' <remarks>
        ''' This is an unavoidable side effect (bug) of the underlying Microsoft CryptoAPI. See
        ''' http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/q322/3/71.asp&amp;NoWebContent=1
        ''' </remarks>
        Public Property KeyContainerName() As String = GetType(Asymmetric).FullName + "DefaultContainerName"

        ''' <summary>Gets the current key size, in bits</summary>
        Public ReadOnly Property KeySizeBits() As Integer
            Get
                Return _rsa.KeySize
            End Get
        End Property

        ''' <summary>Gets the maximum supported key size, in bits</summary>
        Public ReadOnly Property KeySizeMaxBits() As Integer
            Get
                Return _rsa.LegalKeySizes(0).MaxSize
            End Get
        End Property

        ''' <summary>Gets the minimum supported key size, in bits</summary>
        Public ReadOnly Property KeySizeMinBits() As Integer
            Get
                Return _rsa.LegalKeySizes(0).MinSize
            End Get
        End Property

        ''' <summary>Returns valid key step sizes, in bits</summary>
        Public ReadOnly Property KeySizeStepBits() As Integer
            Get
                Return _rsa.LegalKeySizes(0).SkipSize
            End Get
        End Property

        ''' <summary>Returns the default public key as stored in the *.config file</summary>
        Public ReadOnly Property DefaultPublicKey() As PublicKey
            Get
                Return PublicKey.FromConfig()
            End Get
        End Property

        ''' <summary>Returns the default private key as stored in the *.config file</summary>
        Public ReadOnly Property DefaultPrivateKey() As PrivateKey
            Get
                Return PrivateKey.FromConfig
            End Get
        End Property

        ''' <summary>Generates a new public/private key pair as objects</summary>
        ''' <param name="publicKey">Returns a public key</param>
        ''' <param name="privateKey">Raturns a private key</param>
        Public Sub GenerateNewKeyset(<Out()> ByRef publicKey As PublicKey, <Out()> ByRef privateKey As PrivateKey)
            Dim PublicKeyXML As String = Nothing
            Dim PrivateKeyXML As String = Nothing
            GenerateNewKeyset(PublicKeyXML, PrivateKeyXML)
            publicKey = New PublicKey(PublicKeyXML)
            privateKey = New PrivateKey(PrivateKeyXML)
        End Sub

        ''' <summary>Generates a new public/private key pair as XML strings</summary>
        ''' <param name="privateKeyXML">Returns a XML representation of public key</param>
        ''' <param name="publicKeyXML">Returns a XML representation of private key</param>
        Public Sub GenerateNewKeyset(<Out()> ByRef publicKeyXml As String, <Out()> ByRef privateKeyXml As String)
            Dim rsa As RSA = RSACryptoServiceProvider.Create
            publicKeyXML = rsa.ToXmlString(False)
            privateKeyXML = rsa.ToXmlString(True)
        End Sub
#End Region

#Region "Encrypt"
        ''' <summary>Encrypts data using the default public key</summary>
        ''' <param name="data">Data to be encrypted</param>
        ''' <returns>Encrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        Public Function Encrypt(ByVal data As EncryptionData) As EncryptionData
            Dim PublicKey As PublicKey = DefaultPublicKey
            Return Encrypt(data, PublicKey)
        End Function

        ''' <summary>Encrypts data using the provided public key</summary>
        ''' <param name="data">Data to be encrypted</param>
        ''' <param name="publicKey">A public key used for data encryption</param>
        ''' <returns>Encrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="publicKey"/> is null</exception>
        Public Function Encrypt(ByVal data As EncryptionData, ByVal publicKey As PublicKey) As EncryptionData
            If publicKey Is Nothing Then Throw New ArgumentNullException("publicKey")
            _rsa.ImportParameters(publicKey.ToParameters)
            Return EncryptPrivate(data)
        End Function

        ''' <summary>Encrypts data using the provided public key as XML</summary>
        ''' <param name="data">Data to be encrypted</param>
        ''' <param name="publicKeyXML">XML representation of public key used to encrypt data</param>
        ''' <returns>Encrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        Public Function Encrypt(ByVal data As EncryptionData, ByVal publicKeyXml As String) As EncryptionData
            LoadKeyXml(publicKeyXML, False)
            Return EncryptPrivate(data)
        End Function

        ''' <summary>Encrypts data using current public key</summary>
        ''' <param name="data">A data to be encrypred</param>
        ''' <returns>Encrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        Private Function EncryptPrivate(ByVal data As EncryptionData) As EncryptionData
            If data Is Nothing Then Throw New ArgumentNullException("data")
            Return New EncryptionData(_rsa.Encrypt(data.Bytes, False)) With {.Encoding = data.Encoding}
        End Function
#End Region

#Region "Decrypt"
        ''' <summary>Decrypts data using the default private key</summary>
        ''' <param name="encryptedData">An encrypted data to be decrypted</param>
        ''' <returns>Decrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="encryptedData"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="encryptedData"/> is null</exception>
        Public Function Decrypt(ByVal encryptedData As EncryptionData) As EncryptionData
            Dim pk = PrivateKey.FromConfig
            Return Decrypt(encryptedData, pk)
        End Function

        ''' <summary>Decrypts data using the provided private key</summary>
        ''' <param name="encryptedData">An encrypted data to be decrypted</param>
        ''' <param name="PrivateKey">A private key to use to decrypt data</param>
        ''' <returns>Decrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="encryptedData"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="encryptedData"/> or <paramref name="PrivateKey"/> is null</exception>
        Public Function Decrypt(ByVal encryptedData As EncryptionData, ByVal privateKey As PrivateKey) As EncryptionData
            If privateKey Is Nothing Then Throw New ArgumentNullException("privateKey")
            _rsa.ImportParameters(privateKey.ToParameters)
            Return DecryptPrivate(encryptedData)
        End Function

        ''' <summary>Decrypts data using the provided private key as XML</summary>
        ''' <param name="encryptedData">An encrypted data to be decrypted</param>
        ''' <param name="PrivateKeyXML">XMl representation of private key to use to decrypt data</param>
        ''' <returns>Decrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="encryptedData"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="encryptedData"/> is null</exception>
        Public Function Decrypt(ByVal encryptedData As EncryptionData, ByVal privateKeyXml As String) As EncryptionData
            LoadKeyXml(PrivateKeyXml, True)
            Return DecryptPrivate(encryptedData)
        End Function

        ''' <summary>Decrypts data using current private key</summary>
        ''' <param name="encryptedData">An encrypted data to be decrypted</param>
        ''' <returns>Decrypted data; <see cref="EncryptionData.Encoding"/> is set to <paramref name="encryptedData"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="encryptedData"/> is null</exception>
        Private Function DecryptPrivate(ByVal encryptedData As EncryptionData) As EncryptionData
            Return New EncryptionData(_rsa.Decrypt(encryptedData.Bytes, False))
        End Function
#End Region

#Region "SignData"
        ''' <summary>Signs a hash of data using the default private key</summary>
        ''' <param name="data">A data to sign hash of</param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <returns>Signed hash of <paramref name="data"/>; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="hashAlghoritm"/> is not one of <see cref="Hash.HashProvider"/> values</exception>
        ''' <remarks>Signing computes hash of <paramref name="data"/> and then encrypts the hash using default private key.</remarks>
        Public Function SignData(ByVal data As EncryptionData, hashAlghoritm As Hash.HashProvider) As EncryptionData
            Dim privateKey As PrivateKey = DefaultPrivateKey
            Return SignData(data, privateKey, hashAlghoritm)
        End Function

        ''' <summary>Signs a hash of data using the provided private key</summary>
        ''' <param name="data">A data to sign hash of</param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <param name="privateKey">A private key to sign data with</param>
        ''' <returns>Signed hash of <paramref name="data"/>; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="hashAlghoritm"/> is not one of <see cref="Hash.HashProvider"/> values</exception>
        ''' <remarks>Signing computes hash of <paramref name="data"/> and then encrypts the hash using given private key.</remarks>
        Public Function SignData(ByVal data As EncryptionData, ByVal privateKey As PrivateKey, hashalghoritm As Hash.HashProvider) As EncryptionData
            _rsa.ImportParameters(privateKey.ToParameters)
            Return SignDataInternal(data, Hash.GetAlghoritm(hashalghoritm))
        End Function

        ''' <summary>Signs a hash of data using the provided private key as XML</summary>
        ''' <param name="data">A data to sign hash of</param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <param name="privateKeyXml">A XMl representation of private key to sign data with</param>
        ''' <returns>Signed hash of <paramref name="data"/>; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> is null</exception>
        ''' <remarks>Signing computes hash of <paramref name="data"/> and then encrypts the hash using given private key.</remarks>
        Public Function SignData(ByVal data As EncryptionData, ByVal privateKeyXml As String, hashAlghoritm As Hash.HashProvider) As EncryptionData
            LoadKeyXml(privateKeyXml, True)
            Return SignDataInternal(data, Hash.GetAlghoritm(hashAlghoritm))
        End Function

        ''' <summary>Signs a hash of data using the provided key and hash alghoritm</summary>
        ''' <param name="data">A data to sign hash of</param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <param name="privateKey">A private key to sign data with</param>
        ''' <returns>Signed hash of <paramref name="data"/>; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="hashAlghoritm"/> is null</exception>
        ''' <remarks>Signing computes hash of <paramref name="data"/> and then encrypts the hash using given private key.</remarks>
        Public Function SignData(data As EncryptionData, privateKey As PrivateKey, hashAlghoritm As HashAlgorithm) As EncryptionData
            _rsa.ImportParameters(privateKey.ToParameters)
            Return SignDataInternal(data, hashAlghoritm)
        End Function

        ''' <summary>Signs a hash of data using preset private key and given hash alghoritm</summary>
        ''' <param name="data">A data to sign hash of</param>
        ''' <returns>Signed hash of <paramref name="data"/>; <see cref="EncryptionData.Encoding"/> is set to <paramref name="data"/>.<see cref="EncryptionData.Encoding">Encoding</see></returns>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="hashAlghoritm"/> is null</exception>
        ''' <remarks>Signing computes hash of <paramref name="data"/> and then encrypts the hash using current private key.</remarks>
        Private Function SignDataInternal(ByVal data As EncryptionData, hashAlghoritm As HashAlgorithm) As EncryptionData
            If data Is Nothing Then Throw New ArgumentNullException("data")
            Return New EncryptionData(_rsa.SignData(data.Bytes, hashAlghoritm)) With {.Encoding = data.Encoding}
        End Function
#End Region


#Region "SignData"
        ''' <summary>Verifies data against given signature using default public key</summary>
        ''' <param name="data">A data to verify</param>
        ''' <param name="signature">Signature originaly generated from <paramref name="data"/></param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <returns>True if signature is signature originally generated from data using <see cref="SignDataInternal"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="hashAlghoritm"/> or <paramref name="signature"/> is null</exception>
        Public Function VerifyData(ByVal data As EncryptionData, hashAlghoritm As Hash.HashProvider, signature As EncryptionData) As Boolean
            Dim publicKey = DefaultPublicKey
            Return VerifyData(data, publicKey, hashAlghoritm, signature)
        End Function

        ''' <summary>Verifies data against given signature using given public key</summary>
        ''' <param name="data">A data to verify</param>
        ''' <param name="signature">Signature originaly generated from <paramref name="data"/></param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <param name="publicKey">A public key appropriate to private key data was originally signed with</param>
        ''' <returns>True if signature is signature originally generated from data using <see cref="SignDataInternal"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="hashAlghoritm"/> or <paramref name="signature"/> is null</exception>
        Public Function VerifyData(ByVal data As EncryptionData, ByVal publicKey As PublicKey, hashalghoritm As Hash.HashProvider, signature As EncryptionData) As Boolean
            _rsa.ImportParameters(publicKey.ToParameters)
            Return VerifyDataInternal(data, Hash.GetAlghoritm(hashalghoritm), signature)
        End Function

        ''' <summary>Verifies data against given signature using public key given as XML</summary>
        ''' <param name="data">A data to verify</param>
        ''' <param name="signature">Signature originaly generated from <paramref name="data"/></param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <param name="publicKeyXML">A XML representation of public key originally used to sign data</param>
        ''' <returns>True if signature is signature originally generated from data using <see cref="SignDataInternal"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="hashAlghoritm"/> or <paramref name="signature"/> is null</exception>
        Public Function VerifyData(ByVal data As EncryptionData, ByVal publicKeyXml As String, hashAlghoritm As Hash.HashProvider, signature As EncryptionData) As Boolean
            LoadKeyXml(publicKeyXML, False)
            Return VerifyDataInternal(data, Hash.GetAlghoritm(hashAlghoritm), signature)
        End Function

        ''' <summary>Verifies data against given signature uisng igven public key and hash alghoritm</summary>
        ''' <param name="data">A data to verify</param>
        ''' <param name="signature">Signature originaly generated from <paramref name="data"/></param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <param name="publicKey">A public key appropriate to private key data was originally signed with</param>
        ''' <returns>True if signature is signature originally generated from data using <see cref="SignDataInternal"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="hashAlghoritm"/> or <paramref name="signature"/> is null</exception>
        Public Function VerifyData(data As EncryptionData, publicKey As PublicKey, hashAlghoritm As HashAlgorithm, signature As EncryptionData) As Boolean
            _rsa.ImportParameters(publicKey.ToParameters)
            Return VerifyDataInternal(data, hashAlghoritm, signature)
        End Function

        ''' <summary>Verifies data against given signature</summary>
        ''' <param name="data">A data to verify</param>
        ''' <param name="signature">Signature originaly generated from <paramref name="data"/></param>
        ''' <param name="hashAlghoritm">A hash alghoritm to use to compute hash of <paramref name="data"/></param>
        ''' <returns>True if signature is signature originally generated from data using <see cref="SignDataInternal"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="hashAlghoritm"/> or <paramref name="signature"/> is null</exception>
        Private Function VerifyDataInternal(ByVal data As EncryptionData, hashAlghoritm As HashAlgorithm, signature As EncryptionData) As Boolean
            If data Is Nothing Then Throw New ArgumentNullException("data")
            If signature Is Nothing Then Throw New ArgumentNullException("signature")
            Return _rsa.VerifyData(data.Bytes, hashAlghoritm, signature.Bytes)
        End Function
#End Region


        Private Sub LoadKeyXml(ByVal keyXml As String, ByVal isPrivate As Boolean)
            Try
                _rsa.FromXmlString(keyXml)
            Catch ex As Security.XmlSyntaxException
                Dim s As String
                If isPrivate Then
                    s = "private"
                Else
                    s = "public"
                End If
                Throw New Security.XmlSyntaxException( _
                    String.Format("The provided {0} encryption key XML does not appear to be valid.", s), ex)
            End Try
        End Sub
        ''' <summary>
        ''' gets the default RSA provider using the specified key size; 
        ''' note that Microsoft's CryptoAPI has an underlying file system dependency that is unavoidable
        ''' </summary>
        ''' <remarks>
        ''' http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/q322/3/71.asp&amp;NoWebContent=1
        ''' </remarks>
        Private Function GetRSAProvider() As RSACryptoServiceProvider
            Dim rsa As RSACryptoServiceProvider = Nothing
            Dim csp As CspParameters = Nothing
            Try
                csp = New CspParameters
                csp.KeyContainerName = _KeyContainerName
                rsa = New RSACryptoServiceProvider(_KeySize, csp)
                rsa.PersistKeyInCsp = False
                RSACryptoServiceProvider.UseMachineKeyStore = True
                Return rsa
            Catch ex As System.Security.Cryptography.CryptographicException
                If ex.Message.ToLower.IndexOf("csp for this implementation could not be acquired") > -1 Then
                    Throw New Exception("Unable to obtain Cryptographic Service Provider. " & _
                        "Either the permissions are incorrect on the " & _
                        "'C:\Documents and Settings\All Users\Application Data\Microsoft\Crypto\RSA\MachineKeys' " & _
                        "folder, or the current security context '" & Security.Principal.WindowsIdentity.GetCurrent.Name & "'" & _
                        " does not have access to this folder.", ex)
                Else
                    Throw
                End If
            Finally
                If Not rsa Is Nothing Then
                    rsa = Nothing
                End If
                If Not csp Is Nothing Then
                    csp = Nothing
                End If
            End Try
        End Function

    End Class

End Namespace
