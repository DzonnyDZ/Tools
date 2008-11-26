'Attributes used mainly internally by ĐTools are defined here
#If Config <= Release Then
Imports System.Reflection
Namespace InternalT
    ''' <summary>Marks person defined by instance of this attribute as author of marked part of code.</summary>
    ''' <remarks><para>This attribute is obsolete. Use XML documentation tag &lt;author> instead.</para>
    ''' Use this attribute to mark yourself as author of code you have written.</remarks>
    ''' <author>Đonny</author>
    ''' <version version="1.5.2" stage="Release">Marked as obsolete; use &lt;author> XML Doc tag instead, <see cref="AuthorAttribute"/>, <see cref="VersionAttribute"/> and <see cref="FirstVersionAttribute"/> removed</version>
    <AttributeUsage(AuthorAndVersionAttributesUsage, AllowMultiple:=True, Inherited:=False)> _
    <Obsolete("Use <author> XML comment instead")> _
    Public Class AuthorAttribute : Inherits Attribute
        ''' <summary>Contains value of the <see cref="Name"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Name As String
        ''' <summary>Contains value of the <see cref="eMail"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _eMail As String
        ''' <summary>Contains value of the <see cref="WWW"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _WWW As String
        ''' <summary>CTor</summary>
        ''' <param name="Name">Name or nick of the author</param>
        ''' <param name="eMail">@-mail to the author</param>
        ''' <param name="WWW">Web pages of the author</param>
        Public Sub New(ByVal Name As String, Optional ByVal eMail As String = "", Optional ByVal WWW As String = "")
            Me.Name = Name
            Me.eMail = eMail
            Me.WWW = WWW
        End Sub
        ''' <summary>Name or nick of the author</summary>
        Public Property Name() As String
            <DebuggerStepThrough()> Get
                Return _Name
            End Get
            <DebuggerStepThrough()> Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        ''' <summary>@-mail to the author</summary>
        Public Property eMail() As String
            <DebuggerStepThrough()> Get
                Return _eMail
            End Get
            <DebuggerStepThrough()> Set(ByVal value As String)
                _eMail = value
            End Set
        End Property
        ''' <summary>Web pages of the author</summary>
        Public Property WWW() As String
            <DebuggerStepThrough()> Get
                Return _WWW
            End Get
            <DebuggerStepThrough()> Set(ByVal value As String)
                _WWW = value
            End Set
        End Property
    End Class

    ''' <summary>Defines a version of component of code</summary>
    ''' <remarks>This tag is obsolete. Use XML docummentation rag &lt;version> instead</remarks>
    ''' <version version="1.5.2">Marked as obsolete - use XML doc tag &lt;version> instead, <see cref="AuthorAttribute"/>, <see cref="VersionAttribute"/> and <see cref="FirstVersionAttribute"/> removed</version>
    ''' <author>Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    <AttributeUsage(AuthorAndVersionAttributesUsage, AllowMultiple:=False, Inherited:=False)> _
        <Obsolete("Use <version> XML tag instead")> _
    Public Class VersionAttribute : Inherits Attribute
        ''' <summary>Contains value of the <see cref="Major"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Major As Integer = 0
        ''' <summary>Contains value of the <see cref="Minor"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Minor As Integer = 0
        ''' <summary>Contains value of the <see cref="Build"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Build As Integer = 0
        ''' <summary>Contains value of the <see cref="Revision"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Revision As Integer = 0
        ''' <summary>Contains value of the <see cref="Note"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Note As String
#Region "CTors"
        ''' <summary>CTor - initializes version as <paramref name="Major"/>.<paramref name="MajorRevision"/>.<paramref name="Minor"/>.<paramref name="MinorRevision"/></summary>
        ''' <param name="Major">Major version number</param>
        ''' <param name="Minor">Minor version number</param>
        ''' <param name="Build">Build number</param>
        ''' <param name="Revision">Revision number</param>
        ''' <exception cref="ArgumentOutOfRangeException">Version component is less than zero</exception> 
        Public Sub New(ByVal Major As Integer, Optional ByVal Minor As Integer = 0, Optional ByVal Build As Integer = 0, Optional ByVal Revision As Integer = 0)
            With Me
                .Major = Major
                .Minor = Minor
                .Build = Build
                .Revision = Revision
            End With
        End Sub
        ''' <summary>CTor - initializes new instance of version with each parameter either defined by <see cref="Int32"/> value or inherited from assembly where passed <see cref="System.Type"/> is present.</summary>
        ''' <param name="Major">Value to initialize <see cref="Major"/> property</param>
        ''' <param name="Minor">Value to initialize <see cref="Minor"/> property</param>
        ''' <param name="Build">Value to initialize <see cref="Build"/> property</param>
        ''' <param name="Revision">Value to initialize <see cref="Revision"/> property</param>
        Public Sub New(ByVal Major As TypeOrInt32, ByVal Minor As TypeOrInt32, ByVal Build As TypeOrInt32, ByVal Revision As TypeOrInt32)
            If Major.Int32 IsNot Nothing Then
                Me.Major = Major
            Else
                Me.Major = New ApplicationServices.AssemblyInfo(Major.Type.Item.Assembly).Version.Major
            End If
            If Minor.Int32 IsNot Nothing Then
                Me.Minor = Minor
            Else
                Me.Minor = New ApplicationServices.AssemblyInfo(Minor.Type.Item.Assembly).Version.Minor
            End If
            If Build.Int32 IsNot Nothing Then
                Me.Build = Build
            Else
                Me.Build = New ApplicationServices.AssemblyInfo(Build.Type.Item.Assembly).Version.Build
            End If
            If Revision.Int32 IsNot Nothing Then
                Me.Revision = Revision
            Else
                Me.Revision = New ApplicationServices.AssemblyInfo(Revision.Type.Item.Assembly).Version.Revision
            End If
        End Sub
        ''' <summary>CTor - initializes new instance with version fully inherited from assembly where passed <see cref="System.Type"/> is present</summary>
        ''' <param name="TypeFromAssembly">Type form which's assembly inherit the version</param>
        Public Sub New(ByVal TypeFromAssembly As Type)
            Me.New(TypeFromAssembly, TypeFromAssembly, TypeFromAssembly, TypeFromAssembly)
        End Sub
        ''' <summary>CTor - initializes new instance with concrete <see cref="Major"/> and <see cref="Minor"/> numbers and inherits <see cref="Build"/> and <see cref="Revision"/> from assembly of passed <see cref="System.Type"/></summary>
        ''' <param name="Major">Value to initialize <see cref="Major"/> property</param>
        ''' <param name="Minor">Value to initialize <see cref="Minor"/> property</param>
        ''' <param name="BuildAndRevision">Type from which's assembly the <see cref="Build"/> and <see cref="Revision"/> properties will be inherited</param>
        Public Sub New(ByVal Major As Integer, ByVal Minor As Integer, ByVal BuildAndRevision As Type)
            Me.New(Major, Minor, BuildAndRevision, BuildAndRevision)
        End Sub
        ''' <summary>CTor - initializes new instance with concrete <see cref="Build"/> and <see cref="Revision"/> numbers and inherits <see cref="Major"/> and <see cref="Minor"/> from assembly of passed <see cref="System.Type"/></summary>
        ''' <param name="MajorAndMinor">Type from which's assembly the <see cref="Major"/> and <see cref="Minor"/> properties will be inherited</param>
        ''' <param name="Build">Value to initialize <see cref="Build"/> property</param>
        ''' <param name="Revision">Value to initialize <see cref="Revision"/> property</param>
        Public Sub New(ByVal MajorAndMinor As Type, ByVal Build As Integer, ByVal Revision As Integer)
            Me.New(MajorAndMinor, MajorAndMinor, Build, Revision)
        End Sub
        ''' <summary>Represents something tha can be either <see cref="System.Type"/> or <see cref="System.Int32"/></summary>
        ''' <remarks>This class is designed to be used only with <see cref="VersionAttribute"/>. If you are looking for general-purpose class try <see cref="Tools.DataStructuresT.GenericT.T1orT2(Of T1, T2)"/> instead.</remarks>
        Public Class TypeOrInt32
            ''' <summary>Value when <see cref="TypeOrInt32"/> is <see cref="System.Type"/></summary>
            Friend ReadOnly Type As Box(Of Type) = Nothing
            ''' <summary>Value when <see cref="TypeOrInt32"/> is <see cref="System.Int32"/></summary>
            Friend ReadOnly Int32 As Box(Of Int32) = Nothing
            ''' <summary>CTor - initializes new <see cref="TypeOrInt32"/> to represent <see cref="System.Type"/></summary>
            ''' <param name="Type"><see cref="System.Type"/>To be stored in new instance of <see cref="TypeOrInt32"/></param>
            Private Sub New(ByVal Type As Type)
                Me.Type = Type
            End Sub
            ''' <summary>CTor - initializes new <see cref="TypeOrInt32"/> to represent <see cref="System.Int32"/></summary>
            ''' <param name="Int32"><see cref="System.Int32"/>To be stored in new instance of <see cref="TypeOrInt32"/></param>
            Private Sub New(ByVal Int32 As Int32)
                Me.Int32 = Int32
            End Sub
            ''' <summary>Converts <see cref="System.Int32"/> to new instance of <see cref="TypeOrInt32"/> containing it</summary>
            ''' <param name="a"><see cref="System.Int32"/> to be contained in newly created instance</param>
            ''' <returns>New instance of <see cref="TypeOrInt32"/> initialized with <paramref name="a"/></returns>
            Public Shared Widening Operator CType(ByVal a As Int32) As TypeOrInt32
                Return New TypeOrInt32(a)
            End Operator
            ''' <summary>Converts <see cref="System.Type"/> to new instance of <see cref="TypeOrInt32"/> containing it</summary>
            ''' <param name="a"><see cref="System.Type"/> to be contained in newly created instance</param>
            ''' <returns>New instance of <see cref="TypeOrInt32"/> initialized with <paramref name="a"/></returns>
            Public Shared Widening Operator CType(ByVal a As Type) As TypeOrInt32
                Return New TypeOrInt32(a)
            End Operator
            ''' <summary>If <paramref name="a"/> contains <see cref="System.Type"/> unwrap it</summary>
            ''' <param name="a">Instance that contains value to be unwraped</param>
            ''' <returns>Value of <see cref="System.Type"/> stored in <paramref name="a"/> if <paramref name="a"/> contains it</returns>
            ''' <exception cref="InvalidOperationException"><paramref name="a"/> doesn't contain value of type <see cref="System.Type"/></exception>
            Public Shared Narrowing Operator CType(ByVal a As TypeOrInt32) As Type
                If a.Type Is Nothing Then
                    Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.This0CannotBeConvertedTo1BecauseItDoesnTContain1, "TypeOrInt32", "Type"))
                Else
                    Return a.Type
                End If
            End Operator
            ''' <summary>If <paramref name="a"/> contains <see cref="System.Int32"/> unwrap it</summary>
            ''' <param name="a">Instance that contains value to be unwraped</param>
            ''' <returns>Value of <see cref="System.Int32"/> stored in <paramref name="a"/> if <paramref name="a"/> contains it</returns>
            ''' <exception cref="InvalidOperationException"><paramref name="a"/> doesn't contain value of type <see cref="System.Int32"/></exception>
            Public Shared Narrowing Operator CType(ByVal a As TypeOrInt32) As Int32
                If a.Int32 Is Nothing Then
                    Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.This0CannotBeConvertedTo1BecauseItDoesnTContain1, "TypeOrInt32", "Int32"))
                Else
                    Return a.Int32
                End If
            End Operator
        End Class

        '''' <summary>CTor - initializes version from version string</summary>
        '''' <param name="Version">Version string in form <see cref="Major"/>.<see cref="Minor"/>.<see cref="Build"/>.<see cref="Revision"/>. Only the first (Major) version component is compulsory.</param>
        '''' <exception cref="ArgumentException"><paramref name="Version"/> is an empty string or it contains more then 4 dot-separated parts.</exception>
        'Private Sub New(ByVal Version As String)
        '    Dim vParts As String() = Version.Split("."c)
        '    If vParts.Length <= 0 Then Throw New ArgumentException("Version must contain at least one part.", "Version")
        '    Major = vParts(0)
        '    If vParts.Length >= 2 Then
        '        Minor = vParts(1)
        '        If vParts.Length >= 3 Then
        '            Build = vParts(2)
        '            If vParts.Length >= 4 Then
        '                Revision = vParts(3)
        '                If vParts.Length >= 5 Then _
        '                    Throw New ArgumentException("Version string must have maximally 4 parts.", "Version")
        '            End If
        '        End If
        '    End If
        'End Sub
        '''' <summary>CTor - initializes <see cref="VersionAttribute"/> from given <see cref="System.Version"/></summary>
        '''' <param name="Version"><see cref="System.Version"/> to initialize new instance with</param>
        'Private Sub New(ByVal Version As Version)
        '    Major = Version.Major
        '    Minor = Version.Minor
        '    Build = Version.Build
        '    Revision = Version.Revision
        'End Sub
#End Region
        ''' <summary>First part of version number</summary>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than zero</exception>
        <DefaultValue(0I)> _
        Public Property Major() As Integer
            Get
                Return _Major
            End Get
            Set(ByVal value As Integer)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", ResourcesT.Exceptions.VersionComponentMustBeGreaterThanOrEqualToZero)
                _Major = value
            End Set
        End Property
        ''' <summary>Second part of version number</summary>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than zero</exception>
        <DefaultValue(0I)> _
        Public Property Minor() As Integer
            Get
                Return _Minor
            End Get
            Set(ByVal value As Integer)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", ResourcesT.Exceptions.VersionComponentMustBeGreaterThanOrEqualToZero)
                _Minor = value
            End Set
        End Property
        ''' <summary>Third part of version number</summary>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than zero</exception>
        <DefaultValue(0I)> _
        Public Property Build() As Integer
            Get
                Return _Build
            End Get
            Set(ByVal value As Integer)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", ResourcesT.Exceptions.VersionComponentMustBeGreaterThanOrEqualToZero)
                _Build = value
            End Set
        End Property
        ''' <summary>Fourth part of version number</summary>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than zero</exception>
        <DefaultValue(0I)> _
        Public Property Revision() As Integer
            Get
                Return _Revision
            End Get
            Set(ByVal value As Integer)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", ResourcesT.Exceptions.VersionComponentMustBeGreaterThanOrEqualToZero)
                _Revision = value
            End Set
        End Property
        ''' <summary>Version-related note</summary>
        Public Property Note() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                _Note = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="LastChangeDate"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _LastChange As Date
        ''' <summary>Date of last modification (date format in invariant culture: MM/DD/YYYY)</summary>
        ''' <remarks>Be carefull when setting this attribute property. If you make mistake if will cause exception when reading it at runtime.</remarks>
        ''' <exception cref="System.FormatException">Value being set does not contain a valid string representation of a date and time.</exception>
        Public Property LastChange() As String
            Get
                Return _LastChange.ToString("d", Globalization.CultureInfo.InvariantCulture)
            End Get
            Set(ByVal value As String)
                _LastChange = Date.Parse(value, Globalization.CultureInfo.InvariantCulture)
            End Set
        End Property
        ''' <summary>Date of last modification</summary>
        Public Property LastChangeDate() As Date
            Get
                Return _LastChange
            End Get
            Set(ByVal value As Date)
                _LastChange = value
            End Set
        End Property
    End Class

    ''' <summary>Declares things that are spacific for attributes declared in <see cref="Tools.InternalT"/> namespace</summary>
    ''' <remarks>As <see cref="AuthorAttribute"/> and <see cref="VersionAttribute"/> are obsolete, this module is also obsolete</remarks>
    ''' <author>Đonny</author>
    ''' <version stage="Release" version="1.5.2">Marked as obsolete, removed <see cref="AuthorAttribute"/>, <see cref="VersionAttribute"/> and <see cref="FirstVersionAttribute"/></version>
    <Obsolete("AuthorAttribute and VersionAttribute are obsolete")> _
    Friend Module AttributesSpecificDeclarations 'Original 12/20/2006
        ''' <summary>Defines value for <see cref="AttributeUsageAttribute"/> applyed on <see cref="AuthorAttribute"/>, <see cref="VersionAttribute"/> and realetd attributes.</summary>
        ''' <remarks>DO NOT remove ored constants from this declaration. Add constants only when you really need it. (since version 1.1 there is no need to add constants.</remarks>
        Public Const AuthorAndVersionAttributesUsage As AttributeTargets = AttributeTargets.All
    End Module

    ''' <summary>Defines date when item was introduced</summary>
    ''' <remarks>This attribute is obsolete, use &lt;version> XML doc tag instead.</remarks>
    ''' <author>Đonny</author>
    ''' <version stage="Release" version="1.5.2">Marked as obsolete, <see cref="AuthorAttribute"/>, <see cref="VersionAttribute"/> and <see cref="FirstVersionAttribute"/> removed, renamed to <see cref="FirstVersionAttribute"/> (form FirstVersion).</version>
    <AttributeUsage(AuthorAndVersionAttributesUsage, AllowMultiple:=False, Inherited:=False)> _
    <Obsolete("Use <version> XML doc tag instead")> _
    Public Class FirstVersionAttribute : Inherits Attribute
        ''' <summary>CTor from date</summary>
        ''' <param name="Date">Date when item was first introduced</param>
        Public Sub New(ByVal [Date] As Date)
            Me.FirstVersionDate = [Date]
        End Sub
        ''' <summary>CTor from string that represents date in invariant culture format MM/DD/YYYY</summary>
        ''' <param name="InvariantDateStr">Invariant culture string representation of date when tool was first introduced (MM/DD/YYYY)</param>
        ''' <remarks>Be carefull when setting this attribute property. If you make mistake if will cause exception when reading it at runtime.</remarks>
        ''' <exception cref="System.FormatException"><paramref name="InvariantDateStr"/> not contain a valid string representation of a date and time.</exception>
        Public Sub New(ByVal InvariantDateStr As String)
            Me.FirstVerStr = InvariantDateStr
        End Sub
        ''' <summary>CTor from parts of date</summary>
        ''' <param name="Day">Day (number of day in <paramref name="Month"/>, 1-based)</param>
        ''' <param name="Month">Month (number of month in <paramref name="Year"/>, 1-based)</param>
        ''' <param name="Year">Number of eyar</param>
        ''' <remarks>Arguments valid for <see cref="Date"/> constructor are acceptable.</remarks>
        ''' <exception cref="System.ArgumentOutOfRangeException">year is less than 1 or greater than 9999.-or- month is less than 1 or greater than 12.-or- day is less than 1 or greater than the number of days in month.</exception>
        ''' <exception cref="System.ArgumentException">The specified parameters evaluate to less than <see cref="System.DateTime.MinValue" /> or more than <see cref="System.DateTime.MaxValue" />.</exception>
        <CLSCompliant(False)> _
        Public Sub New(ByVal Year As UShort, ByVal Month As Byte, ByVal Day As Byte)
            Me.New(New Date(Year, Month, Day))
        End Sub
        ''' <summary>Contains value of the <see cref="FirstVersionDate"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _FirstVersion As Date
        ''' <summary>Date when first version the item was created</summary>
        Public Property FirstVersionDate() As Date
            Get
                Return _FirstVersion
            End Get
            Private Set(ByVal value As Date)
                _FirstVersion = value
            End Set
        End Property
        ''' <summary>Date when first version of item was created (date format in invariant culture: MM/DD/YYYY)</summary>
        ''' <exception cref="System.FormatException">Value being set does not contain a valid string representation of a date and time.</exception>
        Public Property FirstVerStr() As String
            Get
                Dim c As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
                Return FirstVersionDate.ToString(c.DateTimeFormat)
            End Get
            Private Set(ByVal value As String)
                FirstVersionDate = Date.Parse(value, System.Globalization.CultureInfo.InvariantCulture)
            End Set
        End Property
    End Class

    ''' <summary>Identifies in which stage of development life-cycle current build was done</summary>
    ''' <version stage="Release" version="1.5.2">Version documentation added</version>
    <AttributeUsage(AttributeTargets.Assembly, AllowMultiple:=False)> _
    Public Class AssemblyBuildStageAttribute : Inherits Attribute
        ''' <summary>Contains value of the <see cref="State"/> property</summary>
        Private _State As BuildStates
        ''' <summary>Identifies stage of life-cycle</summary>
        Public ReadOnly Property State() As BuildStates
            Get
                Return _State
            End Get
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="State">Identifies stage of life-cycle</param>
        Public Sub New(ByVal State As BuildStates)
            Me._State = State
        End Sub
    End Class
    ''' <summary>Represents possible stages of life-cycle of assembly used by ĐTools project</summary>
    ''' <version stage="Release" version="1.5.2">Version documentation added</version>
    ''' <remarks>Note for tools developper: Use name of those constants for values of &lt;vesrsion stage=""/> XML doc attribute.</remarks>
    Public Enum BuildStates
        ''' <summary>Debug build, usually done by developer to debug and test</summary>
        Nightly = 1
        ''' <summary>Early stagte of development posled to public</summary>
        Alpha = 2
        ''' <summary>More tested and debuged statge of development, but still not final</summary>
        Beta = 3
        ''' <summary>Near-final stage of development</summary>
        RC = 4
        ''' <summary>Production release</summary>
        Release = 5
    End Enum
End Namespace
#End If