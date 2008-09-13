Imports System.Xml.Linq, System.Resources, System.Xml.Schema, System.Linq, Tools.ExtensionsT, Tools.XmlT.LinqT, Tools.LinqT
Imports <xmlns:e="http://codeplex.com/DTools/IS2022">
Imports System.Runtime.InteropServices
'TODO:Test
#If Config <= Nightly Then 'Stage=Nightly
Namespace TextT.EncodingT
    ''' <summary>Provides runtime access to list of text encodings registered by ISO-IR 2022 (also known as ISO/IEC 2022 or ECMA-35)</summary>
    ''' <remarks>This class provides access to information about such encodings and possibly gives their names as registered by IANA and possibly gives instances of the <see cref="Text.Encoding"/> class to manipulate with text stored in this encoding. Not all ISO-2022 encodings are registered with IANA and not all ISO-2022 encodings are supported by .NET framework. This class does not provide more implementations of the <see cref="Text.Encoding"/> class to deal with all ISO-2022 registered encodings neither this class provides generic ISO-2022 reader/writer. The aim of this class is to provide possibility of identifiying ISO 2022 encoding by its escape sequence, not to deal with it.
    ''' <para>For further reference see <a href="http://www.itscj.ipsj.or.jp/ISO-IR/">International Register of Coded Character Sets To Be Used With Escape Sequences</a>.</para></remarks>
    Public NotInheritable Class ISO2022
        ''' <summary>XML namespace which contains XML-Schema definition of format for storing information about ISO-2022 encodings</summary>
        Public Const xmlns$ = "http://codeplex.com/DTools/IS2022"
        ''' <summary>represents ASCII Escape character often used with ISO-2022 encodings</summary>
        Public Const AsciiEscape As Byte = &H1B
        ''' <summary>Contains value of the <see cref="DefaultInstance"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Shared _DefaultInstance As ISO2022
        ''' <summary>Gets default instance of the <see cref="ISO2022"/> class initialized with default values.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared ReadOnly Property DefaultInstance() As ISO2022
            Get
                If _DefaultInstance Is Nothing Then _DefaultInstance = New ISO2022
                Return _DefaultInstance
            End Get
        End Property
        ''' <summary>Default constructor. Returns new instance which operates with built-in definition of ISO-2022 encodings.</summary>
        Public Sub New()
            Me.New(XDocument.Load(New IO.StreamReader(GetType(ISO2022).Assembly.GetManifestResourceStream("Tools.Text.Encoding.ISO2022"))))
        End Sub
        ''' <summary>Constructor which allows to load definitions of encodings from custom <see cref="XDocument"/></summary>
        ''' <param name="EncodingDefinitions">The <see cref="XDocument"/> containing encoding definitions</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EncodingDefinitions"/> is null</exception>
        ''' <exception cref="System.Xml.XmlException">Given <paramref name="EncodingDefinitions"/> does not validate to XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="EncodingDefinitions"/> is not http://codeplex.com/DTools/IS2022:encodings</exception>
        ''' <remarks>The XML-Schema for the http://codeplex.com/DTools/IS2022 namespace is specified in file Text/Encoding/IOS2022.xsd which is included in source code of the Tools project. Actual schema can be also obtained by reading embdeded resource Tools.Text.Encoding.ISO2022Schema from this assembly.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Sub New(ByVal EncodingDefinitions As XDocument)
            If EncodingDefinitions Is Nothing Then Throw New ArgumentNullException("EncodingDefinitions")
            Dim ss As New XmlSchemaSet
            ss.Add(XmlSchema.Read(GetType(ISO2022).Assembly.GetManifestResourceStream("Tools.Text.Encoding.ISO2022Schema"), AddressOf veh))
            EncodingDefinitions.Validate(ss, AddressOf veh)
            If Not EncodingDefinitions.Root.HasSameName(<e:encodings/>) Then _
                Throw New ArgumentException(String.Format(ResourcesT.Exceptions.RootElementOf0MustBe1, "EncodingDeifinitions", "<encodings>"), "EncodingDeifinitions")
            root = EncodingDefinitions.Root
        End Sub
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>True if <paramref name="obj"/> is <see cref="ISO2022"/> and was initialized with the same xml element.</returns>
        ''' <exception cref="T:System.NullReferenceException">The 
        ''' <paramref name="obj" /> parameter is null.</exception>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is ISO2022 Then Return DirectCast(obj, ISO2022).root Is Me.root
            Return MyBase.Equals(obj)
        End Function
        ''' <summary><see cref="ValidationEventHandler"/> to handle and raise XML-Errors</summary>
        Private Sub veh(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If e.Severity = XmlSeverityType.Error Then Throw e.Exception
        End Sub
        ''' <summary>Root element of encoding definitions in XML</summary>
        Private root As XElement
        ''' <summary>Gets all encoding types recognized by ISO-2022</summary>
        ''' <returns>Array of <see cref="ISO2022EncodingType"/> class instance. One for each of types defined in the <see cref="ISO2022EncodingType.EncodingTypes"/> enumeration.</returns>
        Public Function GetEncodingTypes() As ISO2022EncodingType()
            Return (From type In root.<e:type> Select New ISO2022EncodingType(type)).ToArray
        End Function
        ''' <summary>Gets all encodings recognized by ISO-2022 and registered by IPSJ/ITSCJ</summary>
        ''' <remarks>Array of <see cref="ISO2022Encoding"/> to represent all the encodings</remarks>
        Public Function GetEncodings() As ISO2022Encoding()
            Static encodings As ISO2022Encoding() = _
                (From encoding In root.<e:encoding> Select New ISO2022Encoding(encoding)).ToArray
            Return encodings.Clone
        End Function
        ''' <summary>Attempts to detect ISO-2022 encoding of <see cref="io.Stream"/></summary>
        ''' <param name="stream">Stream to detect encoding of. Current position of stream must be at place where the encoding escape sequence starts, where encoding should be detected.</param>
        ''' <param name="WorkingSet">Specifies current working set of stream - it defines which set of escape sequences will be used. If you are at the beginning of an unknown stream leave default value <see cref="ISO20200Sets.G0"/></param>
        ''' <param name="BytesRead">Optional output parameter which returns all the bytes that were read from <paramref name="stream"/> when attempting to detect encoding. This parameter may be useful when trying to detect encoding of stream where you cannot seek in case encoding is not detected.</param>
        ''' <returns>Detected encoding or null if encoding was not detected</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="stream"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="WorkingSet"/> is not member of <see cref="ISO20200Sets"/>.</exception>
        ''' <exception cref="ArgumentException"><paramref name="stream"/> does not support reading</exception>
        ''' <remarks>If return value is null the encoding was not detected. But actual position in <paramref name="stream"/> have been changed. If <paramref name="stream"/> represents ISO-2022 encoded string you want to decode you should seek back (if <paramref name="stream"/> supports seeking) and reinterpred bytes as characters in somehow determined default encoding.
        ''' <para>This method is intended to be used especially only at start of string or in special cases when encoding type information is stored as ISO-2022 escape sequnce apart from encoded string itself. It is because it is not efficient to call this method for each byte in stream in order to test if it represents start of escape sequnce. You should use some finite-state-automaton-based alghoritm instead (which is not provided by this class).</para></remarks>
        Public Function DetectEncoding(ByVal stream As IO.Stream, Optional ByVal WorkingSet As ISO20200Sets = ISO20200Sets.G0, <Out()> Optional ByRef BytesRead As Byte() = Nothing) As ISO2022Encoding
            If stream Is Nothing Then Throw New ArgumentNullException("stream")
            If Not stream.CanRead Then Throw New ArgumentException(ResourcesT.Exceptions.GivenStreamDoesNotSupportReading)
            Dim Encodings = From enc In GetEncodings() Select Encoding = enc, Escape = enc.EscapeSequence(WorkingSet) Where Escape IsNot Nothing AndAlso Escape.Length > 0
            Dim i As Integer = 0
            Dim bytes As New List(Of Byte)
            Try
                Do While Encodings.Count > 0
                    Dim b = stream.ReadByte
                    If b = -1 Then
                        If i > 0 AndAlso Encodings.Count = 1 AndAlso Encodings.First.Escape.Length = i Then Return Encodings.First.Encoding
                        Return Nothing
                    End If
                    bytes.Add(b)
                    Dim ByteMatch = From enc In Encodings Where enc.Escape.Length > i AndAlso enc.Escape(i) = b Select enc
                    Dim TooShort = From enc In Encodings Where enc.Escape.Length <= i Select enc
                    If ByteMatch.Count = 0 Then
                        If i = 0 Then Return Nothing
                        If TooShort.Count = 1 Then Return TooShort.First.Encoding
                        Return Nothing
                    End If
                    Encodings = ByteMatch
                    i += 1
                Loop
                Return Nothing
            Finally
                BytesRead = bytes.ToArray
            End Try
        End Function

        ''' <summary>Attempts to detect ISO-2022 encoding of array of bytes</summary>
        ''' <param name="bytes">Byte array to detect encoding of. This array should start with valid ISO-2022 escape sequence for some encoding in given <paramref name="WorkingSet"/>. Otherwise encoding is not bdetected.</param>
        ''' <param name="WorkingSet">Specifies current working set of stream - it defines which set of escape sequences will be used. If you are at the beginning of an unknown stream leave default value <see cref="ISO20200Sets.G0"/></param>
        ''' <returns>Detected encoding or null if encoding was not detected</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="stream"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="WorkingSet"/> is not member of <see cref="ISO20200Sets"/>.</exception>
        Public Function DetectEncoding(ByVal bytes As Byte(), Optional ByVal WorkingSet As ISO20200Sets = ISO20200Sets.G0) As ISO2022Encoding
            If bytes Is Nothing Then Throw New ArgumentNullException("stream")
            If bytes.Length = 0 Then Return Nothing
            Dim ms As New IO.MemoryStream(bytes, False)
            Return DetectEncoding(ms, WorkingSet)
        End Function


        ''' <summary>Defines working sets used by IS-2022</summary>
        Public Enum ISO20200Sets
            ''' <summary>C0 set (ASCII and derivatives)</summary>
            C0
            ''' <summary>C1 set (ISO 8859 and Unicode)</summary>
            C1
            ''' <summary>G0 set</summary>
            G0
            ''' <summary>G1 set</summary>
            G1
            ''' <summary>G2 set</summary>
            G2
            ''' <summary>G3 set</summary>
            G3
        End Enum
        ''' <summary>Parses Escape sequence as stored in Xml to array of bytes</summary>
        ''' <param name="XmlEscapeSequence">String to be parsed</param>
        ''' <remarks>Byte array representation of <paramref name="XmlEscapeSequence"/></remarks>
        ''' <exception cref="FormatException"><paramref name="XmlEscapeSequence"/> contains invalid byte. Each byte is specified as either "ESC" string or two numbers from range 0÷127 separated by "/". Leading zeros are allowd. Bytes are separated, preceded and succeded by any number of whitespaces.</exception>
        Friend Shared Function ParseXmlEscapeSequence(ByVal XmlEscapeSequence As String) As Byte()
            Static wh As New System.Text.RegularExpressions.Regex("\s+", Text.RegularExpressions.RegexOptions.Compiled Or Text.RegularExpressions.RegexOptions.CultureInvariant)
            Static regex As New System.Text.RegularExpressions.Regex("^(((?<High>(0*((1(([01]?[0-9])|(2[0-7]))?)|([2-9]?[0-9])))|0)/(?<Low>(0*((1(([01]?[0-9])|(2[0-7]))?)|([2-9]?[0-9])))|0))|(?<ESC>ESC))(\s+(?<Byte>((?<Low>(0*((1(([01]?[0-9])|(2[0-7]))?)|([2-9]?[0-9])))|0)/(?<High>(0*((1(([01]?[0-9])|(2[0-7]))?)|([2-9]?[0-9])))|0))|(?<ESC>ESC)))$", Text.RegularExpressions.RegexOptions.Compiled Or Text.RegularExpressions.RegexOptions.CultureInvariant Or Text.RegularExpressions.RegexOptions.ExplicitCapture)
            Dim ret As New List(Of Byte)
            For Each b In wh.Split(XmlEscapeSequence)
                If b = "" Then Continue For
                Dim m = regex.Match(b)
                If Not m.Success Then Throw New FormatException(ResourcesT.Exceptions.EscapeSequenceStringIsInInvalidFormat)
                If m.Groups!ESC.Success Then
                    ret.Add(AsciiEscape)
                Else
                    Dim High = Byte.Parse(m.Groups!High.Value, Globalization.CultureInfo.InvariantCulture)
                    Dim Low = Byte.Parse(m.Groups!Low.Value, Globalization.CultureInfo.InvariantCulture)
                    ret.Add(High << 4 Or Low)
                End If
            Next
            Return ret.ToArray
        End Function
    End Class
    ''' <summary>Represents single ISO-2022 encoding and provides infornmation about it.</summary>
    ''' <seealso cref="ISO2022"/>
    Public NotInheritable Class ISO2022Encoding : Implements IEquatable(Of ISO2022Encoding)
        ''' <summary><see cref="XElement"/> encoding info is stored in</summary>
        Private element As XElement
        ''' <summary>Creates new instance of the <see cref="ISO2022Encoding"/> class from its definition in <see cref="XElement"/></summary>
        ''' <param name="encoding"><see cref="XElement"/> encoding is stored in</param>
        ''' <exception cref="ArgumentNullException"><paramref name="encoding"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="encoding"/> does not represent &lt;encoding> element.</exception>
        Friend Sub New(ByVal encoding As XElement)
            If encoding Is Nothing Then Throw New ArgumentNullException("encoding")
            If Not encoding.HasSameName(<e:encoding/>) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustRepresentXMLElement1, "encoding", "encoding"), "encoding")
            Me.element = element
        End Sub
        ''' <summary>Gets official encoding name under which it is registered by IPSJ/ITSCJ</summary>
        ''' <returns>Name of encoding in ISO-2022 encoding register.</returns>
        ''' <remarks>This property is never localized.</remarks>
        Public ReadOnly Property Name$()
            Get
                Return element.@name
            End Get
        End Property
        ''' <summary>Gets number under which the encoding is registered by IPSJ/ITSCJ</summary>
        ''' <returns>Number of encoding in ISO-2022 encoding register.</returns>
        ''' <remarks>In case the encoding is not assigned single number but two numbers separated by hyphen, this property returns such number as decimal number.</remarks>
        ''' <seealso cref="NumberOriginal"/>
        Public ReadOnly Property Number() As Decimal
            Get
                If element.@number.Contains("-"c) Then
                    Return Decimal.Parse(element.@number.Split("-"c).Join(Globalization.CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), Globalization.CultureInfo.InvariantCulture)
                Else
                    Return Integer.Parse(element.@number, Globalization.CultureInfo.InvariantCulture)
                End If
            End Get
        End Property
        ''' <summary>Gets number under which the encoding is registered by IPSJ/ITSCJ</summary>
        ''' <returns>Number as string in same form it was stored in xml element which initialized this instance. It means no assumptions about leading and trailing whitespaces can be done, as well as about leading zeros. Some numbers may contain hyphen.</returns>
        ''' <seealso cref="Number"/>
        Public ReadOnly Property NumberOriginal() As String
            Get
                Return element.@number
            End Get
        End Property
        ''' <summary>Gets type of this encoding</summary>
        ''' <returns>If called repeatedly for the same instance returns the same instance of the <see cref="ISO2022EncodingType"/>, but it is not the same instance (in terms of <see cref="System.Object.ReferenceEquals"/>) as one of those obtained by <see cref="ISO2022.[GetEncodingTypes]"/>.</returns>
        ''' <remarks>Type of this encoding assording to IPSJ/ITSCJ registr</remarks>
        Public ReadOnly Property Type() As ISO2022EncodingType
            Get
                Static iType As ISO2022EncodingType = New ISO2022EncodingType((From t In element.Parent.<e:type> Where t.@name = element.@name)(0))
                Return iType
            End Get
        End Property
        ''' <summary>Gets name of encoding as registered by IANA (if exists).</summary>
        ''' <returns>Primary (preffered) name of encoding as registered by IANA. Or null if this encoding is not registered by IANA.</returns>
        ''' <remarks>IANA organisation maintains list of encoding names registered for use on the Internet. Such encoding names are used eg. in XML encoding specification or in HTTP headers. Also <see cref="System.Text.EncodingInfo.Name"/> and <see cref="System.Text.Encoding.WebName"/> uses IANA registred names. For more information see <a href="http://www.iana.org">www.iana.org</a> or <a href="http://www.iana.org/assignments/character-sets">character sets registry</a>.</remarks>
        ''' <seealso cref="System.Text.Encoding.WebName"/><seealso cref="System.Text.EncodingInfo.Name"/>
        ''' <seealso cref="IanaAlias"/>
        Public ReadOnly Property IanaName$()
            Get
                Return If(element.@IANAName <> "", element.@IANAName, Nothing)
            End Get
        End Property
        ''' <summary>Return alternative name for encoding as registered by IANA in format "iso-ir-0" (if exists).</summary>
        ''' <returns>Alias name of encoding which starts with "iso-ir-" and then contains <see cref="NumberOriginal"/> without leading zeros. If such alias does not exist returns null.</returns>
        ''' <remarks>IANA registeres several aliases for each encoding. Most of ISO-2022 encoding registered with IANA have alias "iso-ir-0" where 0 is <see cref="NumberOriginal"/> (without leading zeros).</remarks>
        ''' <seealso cref="IanaName"/>
        Public ReadOnly Property IanaAlias$()
            Get
                Return If(element.@IANAAlias <> "", element.@IANAAlias, Nothing)
            End Get
        End Property
        ''' <summary>Gets <see cref="System.Text.Encoding"/> which implements ISO-2022 encoding represented by this instance</summary>
        ''' <returns>Appropriate <see cref="Text.Encoding"/> or null if either <see cref="IanaName"/> is null or it is not supported by the .NET framework (the <see cref="M:System.Text.Encoding.GetEncoding(System.String)">Encoding.GetEncoding</see> method.)</returns>
        ''' <remarks>Actual ability of .NET framework to support particular encoding depends on which encodings are supported by (installed in) operating system.</remarks>
        ''' <seealso cref="Text.Encoding"/><seeaslo cref="M:System.Text.Encoding.GetEncoding(System.String)"/><seealso cref="IanaName"/>
        Public Function GetEncoding() As Text.Encoding
            If Me.IanaName Is Nothing Then Return Nothing
            Try
                Return Text.Encoding.GetEncoding(Me.IanaName)
            Catch ex As ArgumentException
                Return Nothing
            End Try
        End Function
        ''' <summary>Gets escape sequence used to switch to encoding represented by this instance in given working set</summary>
        ''' <param name="WorkingSet">Working set to obtain escape sequence for</param>
        ''' <returns>Escape sequance as array of bytes. Null if escape sequence for given working set does not exist.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="WorkingSet"/> is not member of <see cref="ISO2022.ISO20200Sets"/>.</exception>
        Public ReadOnly Property EscapeSequence(ByVal WorkingSet As ISO2022.ISO20200Sets) As Byte()
            Get
                If element.<e:escape>.Count > 0 Then
                    Dim TypeEscapeSequence = Me.Type.EscapeSequencePrefix(WorkingSet)
                    If TypeEscapeSequence Is Nothing Then Return Nothing
                    Dim MyEscapeSequence = ISO2022.ParseXmlEscapeSequence(element.<e:escape>.Value)
                    Dim ret(TypeEscapeSequence.Length + MyEscapeSequence.Length - 1) As Byte
                    Array.Copy(TypeEscapeSequence, ret, TypeEscapeSequence.Length)
                    Array.Copy(MyEscapeSequence, 0, ret, TypeEscapeSequence.Length, MyEscapeSequence.Length)
                    Return ret
                ElseIf element.<e:extended-escape>.Count > 0 Then
                    Dim strEsc$
                    Select Case WorkingSet
                        Case ISO2022.ISO20200Sets.C0 : strEsc = element.<e:extended-escape>(0).@C0
                        Case ISO2022.ISO20200Sets.C1 : strEsc = element.<e:extended-escape>(0).@C1
                        Case ISO2022.ISO20200Sets.G0 : strEsc = element.<e:extended-escape>(0).@G0
                        Case ISO2022.ISO20200Sets.G1 : strEsc = element.<e:extended-escape>(0).@G1
                        Case ISO2022.ISO20200Sets.G2 : strEsc = element.<e:extended-escape>(0).@G2
                        Case ISO2022.ISO20200Sets.G3 : strEsc = element.<e:extended-escape>(0).@G3
                        Case Else : Throw New InvalidEnumArgumentException("WorkingSet", WorkingSet, WorkingSet.GetType)
                    End Select
                    If strEsc = "" Then Return Nothing
                    Return ISO2022.ParseXmlEscapeSequence(strEsc)
                Else
                    Return Nothing
                End If
            End Get
        End Property
        ''' <summary>Gets escape sequence used to switch to encoding represented by this instance in C0 working set.</summary>
        ''' <remarks>Escape sequence as array of bytes or null if the encoding has no escape sequence for C0 working set</remarks>
        Public ReadOnly Property C0EscapeSequence() As Byte()
            Get
                Return EscapeSequence(ISO2022.ISO20200Sets.C0)
            End Get
        End Property
        ''' <summary>Gets escape sequence used to switch to encoding represented by this instance in C1 working set.</summary>
        ''' <remarks>Escape sequence as array of bytes or null if the encoding has no escape sequence for C1 working set</remarks>
        Public ReadOnly Property C1EscapeSequence() As Byte()
            Get
                Return EscapeSequence(ISO2022.ISO20200Sets.C1)
            End Get
        End Property
        ''' <summary>Gets escape sequence used to switch to encoding represented by this instance in G0 working set.</summary>
        ''' <remarks>Escape sequence as array of bytes or null if the encoding has no escape sequence for G0 working set</remarks>
        Public ReadOnly Property G0EscapeSequence() As Byte()
            Get
                Return EscapeSequence(ISO2022.ISO20200Sets.G0)
            End Get
        End Property
        ''' <summary>Gets escape sequence used to switch to encoding represented by this instance in G1 working set.</summary>
        ''' <remarks>Escape sequence as array of bytes or null if the encoding has no escape sequence for G1 working set</remarks>
        Public ReadOnly Property G1EscapeSequence() As Byte()
            Get
                Return EscapeSequence(ISO2022.ISO20200Sets.G1)
            End Get
        End Property
        ''' <summary>Gets escape sequence used to switch to encoding represented by this instance in G2 working set.</summary>
        ''' <remarks>Escape sequence as array of bytes or null if the encoding has no escape sequence for G2 working set</remarks>
        Public ReadOnly Property G2EscapeSequence() As Byte()
            Get
                Return EscapeSequence(ISO2022.ISO20200Sets.G2)
            End Get
        End Property
        ''' <summary>Gets escape sequence used to switch to encoding represented by this instance in G3 working set.</summary>
        ''' <remarks>Escape sequence as array of bytes or null if the encoding has no escape sequence for G3 working set</remarks>
        Public ReadOnly Property G3EscapeSequence() As Byte()
            Get
                Return EscapeSequence(ISO2022.ISO20200Sets.G3)
            End Get
        End Property
        ''' <summary>Gets value idicating if this <see cref="EscapeSequence"/> for this encoding starts wit same prefix sa <see cref="EscapeSequence"/> of all other encodings of same type that use common prefix too.</summary>
        ''' <returns>True if <see cref="EscapeSequence"/> of this encoding starts with <see cref="Type"/>.<see cref="ISO2022EncodingType.EscapeSequencePrefix">EscapeSequencePrefix</see> for all working sets.</returns>
        ''' <remarks>This is only hint property and is not mentioned in the ISO-2022 standard.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Property UsesCommonPrefix() As Boolean
            Get
                Return element.<e:extended-escape>.Count = 0
            End Get
        End Property
        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <returns>true if <paramref name="other"/> was initialized with the same xml elemet as current instance</returns>
        ''' <param name="other">An object to compare with this object.</param>
        Public Overloads Function Equals(ByVal other As ISO2022Encoding) As Boolean Implements System.IEquatable(Of ISO2022Encoding).Equals
            Return other.element Is Me.element
        End Function
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />. </summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is <see cref="ISO2022Encoding"/> and it was initialized by the same xml element.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
        ''' <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception>
        ''' <filterpriority>2</filterpriority>
        ''' <remarks>Use type-safe overload <see cref="M:Tools.TextT.EncodingT.ISO2022EncodingType.Equals(Tools.TextT.EncodingT.ISO2022EncodingType)"/> instead</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is ISO2022EncodingType Then Return DirectCast(obj, ISO2022Encoding).element Is Me.element
            Return MyBase.Equals(obj)
        End Function
    End Class
    ''' <summary>Represents and provides information about one type of encoding as recognized by ISO-2022</summary>
    ''' <remarks>You can obtain all encoding type information by <see cref="ISO2022.GetEncodingTypes"/>. All existing encoding types are defined in the <see cref="ISO2022EncodingType.EncodingTypes"/> enumeration.
    ''' <para>This class has no public constructor.</para></remarks>
    ''' <seealso cref="ISO2022.GetEncodingTypes"/><seealso cref="ISO2022EncodingType.EncodingTypes"/>
    ''' <seealso cref="ISO2022"/>
    Public NotInheritable Class ISO2022EncodingType : Implements IEquatable(Of ISO2022EncodingType)
        ''' <summary>Constructs a new instance from given <see cref="XElement"/></summary>
        ''' <param name="type"><see cref="XElement"/> &lt;type> which represents ISO-20200 encoding type to be constructed.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="type"/> dioes not represent element &lt;type></exception>
        Friend Sub New(ByVal type As XElement)
            If type Is Nothing Then Throw New ArgumentNullException("type")
            If Not type.HasSameName(<e:type/>) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustRepresentXMLElement1, "type", "type"), "type")
            Me.element = type
        End Sub
        ''' <summary>The <see cref="XElement"/> which stores information for this instance</summary>
        Private element As XElement
        ''' <summary>Defines all encoding types recognized by ISO-2022</summary>
        Public Enum EncodingTypes
            ''' <summary>The C0 type (C0-Control character sets)</summary>
            C0
            ''' <summary>The 94/1 type (94-Character graphic character sets with one Intermediate byte)</summary>
            t_94_1
            ''' <summary>The S type (Single control functions)</summary>
            S
            ''' <summary>The C1 type (C1 Control character sets)</summary>
            C1
            ''' <summary>The M type (Multiple byte graphic character sets)</summary>
            M
            ''' <summary>The wSR type (Coding systems with Standard return)</summary>
            wSR
            ''' <summary>The w/oSR type (Coding Systems without Standard return)</summary>
            woSR
            ''' <summary>The 96 type (96-Character graphic character set)</summary>
            t_96
            ''' <summary>The 94-2 type (94-Character graphic character set with second Intermediate byte)</summary>
            t_94_2
        End Enum
        ''' <summary>Returns one of <see cref="EncodingTypes"/> values which identifies type of encoding recognized by ISO-2022.</summary>
        Public ReadOnly Property Type() As EncodingTypes
            Get
                Select Case Me.element.Value
                    Case "C0" : Return EncodingTypes.C0
                    Case "94/1" : Return EncodingTypes.t_94_1
                    Case "S" : Return EncodingTypes.S
                    Case "C1" : Return EncodingTypes.C1
                    Case "M" : Return EncodingTypes.M
                    Case "wSR" : Return EncodingTypes.wSR
                    Case "w/oSR" : Return EncodingTypes.woSR
                    Case "96" : Return EncodingTypes.t_96
                    Case "94/2" : Return EncodingTypes.t_94_2
                    Case Else : Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.UnknownISO2022EncodingType0, Me.element.Value))
                End Select
            End Get
        End Property
        ''' <summary>Gets default escape sequence prefix used by encodings of type represented by current instance in given working set</summary>
        ''' <returns>Array of bytes used as prefix of escape sequence. Null if encodings of current type usualy does not use given working set.</returns>
        ''' <param name="WorkingSet">Working set to get escape sequence prefix for</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="WorkingSet"/> is not member of <see cref="ISO2022.ISO20200Sets"/>.</exception>
        Public ReadOnly Property EscapeSequencePrefix(ByVal WorkingSet As ISO2022.ISO20200Sets) As Byte()
            Get
                Select Case WorkingSet
                    Case ISO2022.ISO20200Sets.C0 : Return C0EscapeSequencePrefix
                    Case ISO2022.ISO20200Sets.C1 : Return C1EscapeSequencePrefix
                    Case ISO2022.ISO20200Sets.G0 : Return G0EscapeSequencePrefix
                    Case ISO2022.ISO20200Sets.G1 : Return G1EscapeSequencePrefix
                    Case ISO2022.ISO20200Sets.G2 : Return G2EscapeSequencePrefix
                    Case ISO2022.ISO20200Sets.G3 : Return G3EscapeSequencePrefix
                    Case Else : Throw New InvalidEnumArgumentException("workingSet", WorkingSet, WorkingSet.GetType)
                End Select
            End Get
        End Property
        ''' <summary>Gets default prefix of escape sequence used by encodign of current type in working set C0.</summary>
        ''' <returns>Escape sequence as array of bytes or null if current encoding type does not usually have escape sequence in the C0 working set.</returns>
        ''' <seealso cref="EscapeSequencePrefix"/>
        Public ReadOnly Property C0EscapeSequencePrefix() As Byte()
            Get
                If element.@C0 = "" Then Return Nothing
                Return ISO2022.ParseXmlEscapeSequence(element.@C0)
            End Get
        End Property
        ''' <summary>Gets default prefix of escape sequence used by encodign of current type in working set C1.</summary>
        ''' <returns>Escape sequence as array of bytes or null if current encoding type does not usually have escape sequence in the C1 working set.</returns>
        ''' <seealso cref="EscapeSequencePrefix"/>
        Public ReadOnly Property C1EscapeSequencePrefix() As Byte()
            Get
                If element.@C1 = "" Then Return Nothing
                Return ISO2022.ParseXmlEscapeSequence(element.@C1)
            End Get
        End Property
        ''' <summary>Gets default prefix of escape sequence used by encodign of current type in working set G0.</summary>
        ''' <returns>Escape sequence as array of bytes or null if current encoding type does not usually have escape sequence in the G0 working set.</returns>
        ''' <seealso cref="EscapeSequencePrefix"/>
        Public ReadOnly Property G0EscapeSequencePrefix() As Byte()
            Get
                If element.@G0 = "" Then Return Nothing
                Return ISO2022.ParseXmlEscapeSequence(element.@G0)
            End Get
        End Property
        ''' <summary>Gets default prefix of escape sequence used by encodign of current type in working set G1.</summary>
        ''' <returns>Escape sequence as array of bytes or null if current encoding type does not usually have escape sequence in the G1 working set.</returns>
        ''' <seealso cref="EscapeSequencePrefix"/>
        Public ReadOnly Property G1EscapeSequencePrefix() As Byte()
            Get
                If element.@G1 = "" Then Return Nothing
                Return ISO2022.ParseXmlEscapeSequence(element.@G1)
            End Get
        End Property
        ''' <summary>Gets default prefix of escape sequence used by encodign of current type in working set G2.</summary>
        ''' <returns>Escape sequence as array of bytes or null if current encoding type does not usually have escape sequence in the G2 working set.</returns>
        ''' <seealso cref="EscapeSequencePrefix"/>
        Public ReadOnly Property G2EscapeSequencePrefix() As Byte()
            Get
                If element.@G2 = "" Then Return Nothing
                Return ISO2022.ParseXmlEscapeSequence(element.@G2)
            End Get
        End Property
        ''' <summary>Gets default prefix of escape sequence used by encodign of current type in working set G3.</summary>
        ''' <returns>Escape sequence as array of bytes or null if current encoding type does not usually have escape sequence in the G3 working set.</returns>
        ''' <seealso cref="EscapeSequencePrefix"/>
        Public ReadOnly Property G3EscapeSequencePrefix() As Byte()
            Get
                If element.@G3 = "" Then Return Nothing
                Return ISO2022.ParseXmlEscapeSequence(element.@G3)
            End Get
        End Property

        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <returns>true if <paramref name="other"/> was initialized with the same xml elemet as current instance</returns>
        ''' <param name="other">An object to compare with this object.</param>
        Public Overloads Function Equals(ByVal other As ISO2022EncodingType) As Boolean Implements System.IEquatable(Of ISO2022EncodingType).Equals
            Return other.element Is Me.element
        End Function
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />. </summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is <see cref="ISO2022EncodingType"/> and it was initialized by the same xml element.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
        ''' <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception>
        ''' <filterpriority>2</filterpriority>
        ''' <remarks>Use type-safe overload <see cref="M:Tools.TextT.EncodingT.ISO2022EncodingType.Equals(Tools.TextT.EncodingT.ISO2022EncodingType)"/> instead</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is ISO2022EncodingType Then Return DirectCast(obj, ISO2022EncodingType).element Is Me.element
            Return MyBase.Equals(obj)
        End Function
    End Class
End Namespace
#End If