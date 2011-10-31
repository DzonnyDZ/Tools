Imports System.Xml.Serialization, Tools.ExtensionsT
Imports System.Globalization.CultureInfo
Imports Tools.ComponentModelT

Namespace TextT.UnicodeT
    ''' <summary>Represents a CSUR character block and provides additional CSUR properties</summary>
    ''' <remarks><note>XML serialization attributes used to decorate properties of this class are not intended for XML serialization, they are rather intended as machine-readable documentation where the property originates from in UCD XML.</note></remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class CsurBlock
        Inherits UnicodeBlock

        ''' <summary>CTor - creates a new instance of the <see cref="CsurBlock"/> class</summary>
        ''' <param name="element">An XML element that represents the block. The element should come from a document that specifies extension CSUR properties.</param>
        ''' <param name="mainUcdDocument">A XML document that represents the main Unicode Character Database (not CSUR extension database)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;block> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement, mainUcdDocument As XDocument)
            MyBase.New(element)
            If mainUcdDocument Is Nothing Then Throw New ArgumentNullException("mainUcdDocument")
        End Sub

        Private mainUcdDocument As XDocument
        ''' <summary>Gets a XML document that represents whole Unicode Character Database</summary>
        ''' <remarks>In case of <see cref="CsurBlock"/> this document is typically different from document <see cref="Element"/> belongs to</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), XmlIgnore()>
        Protected Overrides ReadOnly Property Document As System.Xml.Linq.XDocument
            Get
                Return mainUcdDocument
            End Get
        End Property


        ''' <summary>Gets status of this block in ConScript Unicode Registry (CSUR)</summary>
        ''' <returns>Status of this block in CSUR. <see cref="CsurStatus.NotInCsur"/> if value is not provided.</returns>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attributte cannot be parsed as <see cref="CsurStatus"/>.</exception>
        <XmlAttribute("status", namespace:=CsurExtensions.XmlNamespace)>
        <LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR"), LDisplayName(GetType(UnicodeResources), "d_CSUR_Status")>
        Public ReadOnly Property Status As CsurStatus
            Get
                Dim val = Element.@status
                If val = "" Then Return CsurStatus.NotInCsur
                Try
                    Return EnumCore.Parse(Of CsurStatus)(val)
                Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is OverflowException
                    Throw New InvalidOperationException(UnicodeResources.ex_ValueCannotBeParsed.f(val, GetType(CsurStatus).Name), ex)
                End Try
            End Get
        End Property

        ''' <summary>Gets date of latest revision of the block registration in CSUR</summary>
        ''' <returns>Date of last revision of block registration or proposal in CSUR. Null if the value was not specified.</returns>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute cannot be parsed as date in XML format yyyy-MM-dd in invariant culture.</exception>
        <XmlAttribute("last-revision", Namespace:=CsurExtensions.XmlNamespace)>
        <LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR"), LDisplayName(GetType(UnicodeResources), "d_CSUR_LastRevision")>
        Public ReadOnly Property LastRevision As DateTime?
            Get
                Dim val = Element.@<last-revision>
                If val = "" Then Return Nothing
                Try
                    Return Date.ParseExact(val, "yyyy-MM-dd", InvariantCulture)
                Catch ex As FormatException
                    Throw New InvalidOperationException(UnicodeResources.ex_ValueCannotBeParsed.f(val, GetType(Date).Name), ex)
                End Try
            End Get
        End Property

        ''' <summary>Gets name of creator of script/block</summary>
        ''' <returns>Name of creator of script or block as recorded in CSUR. Null if the vaue is not provided</returns>
        <XmlAttribute("creator", namespace:=CsurExtensions.XmlNamespace)>
        <LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR"), LDisplayName(GetType(UnicodeResources), "d_CSUR_Creator")>
        Public ReadOnly Property Creator As String
            Get
                Return Element.@creator
            End Get
        End Property

    End Class
End Namespace
