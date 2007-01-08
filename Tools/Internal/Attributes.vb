'Attributes used mainly internally by ĐTools are defined here
#If Config <= Release Then
Imports System.Reflection
Namespace Internal
    ''' <summary>Marks person defined by instance of this attribute as author of marked part of code.</summary>
    ''' <remarks>
    ''' Use this attribute to mark yourself as author of code you have written.
    ''' This attribute should not be applyed on items where <see cref="DoNotApplyAuthorAndVersionAttributesAttribute"/> is used.
    ''' This attribute should not be applyed on items nested in items decorated with this attribute (if not necessary).
    ''' If you want to determine author of something and it is not decorated with this attribute try serarching for this attribute applyed on item which your item is nested in. If attribute is not found, go 1 nesting level up, if not found go again etc. If no information is found at assembly level search for <see cref="AssemblyCompanyAttribute"/>
    ''' </remarks>
    <AttributeUsage(AuthorAndVersionAttributesUsage, AllowMultiple:=True, Inherited:=False)> _
    <Author("Đonny", "dzony.dz@gmail.com"), Version(1, 0, GetType(AuthorAttribute), LastChange:="12/20/2006")> _
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
    ''' <remarks>
    ''' This attribute should not be applyed on items where <see cref="DoNotApplyAuthorAndVersionAttributesAttribute"/> is used.
    ''' This attribute should not be applyed on items nested in items decorated with this attribute (if not necessary).
    ''' If you want to determine version of something and it is not decorated with this attribute try serarching for this attribute applyed on item which your item is nested in. If attribute is not found, go 1 nesting level up, if not found go again etc. If no information is found at assembly level search for <see cref="AssemblyVersionAttribute"/>
    ''' </remarks>
    <AttributeUsage(AuthorAndVersionAttributesUsage, AllowMultiple:=False, Inherited:=False)> _
    <Author("Đonny", "dzony.dz@gmail.com"), Version(1, 0, GetType(VersionAttribute), LastChange:="12/21/2006")> _
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
        ''' <remarks>This class is designed to be used only with <see cref="VersionAttribute"/>. If you are looking for general-purpose class try <see cref="Tools.DataStructures.Generic.T1orT2(Of T1, T2)"/> instead.</remarks>
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
                    Throw New InvalidOperationException("This TypeOrInt32 cannot be converted to Type because it doesn't contain Type.")
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
                    Throw New InvalidOperationException("This TypeOrInt32 cannot be converted to Int32 because it doesn't contain Int32.")
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
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", "Version component must be greater than or equal to zero")
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
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", "Version component must be greater than or equal to zero")
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
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", "Version component must be greater than or equal to zero")
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
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", "Version component must be greater than or equal to zero")
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
        ''' <summary>Contains value of the <see cref="LastChange"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _LastChange As Date
        ''' <summary>Date of last modification (date format in invariant culture: MM/DD/YYYY)</summary>
        Public Property LastChange() As String
            Get
                Dim c As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
                Return _LastChange.ToString(c.DateTimeFormat)
            End Get
            Set(ByVal value As String)
                Dim c As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
                Dim oldc As Globalization.CultureInfo = Threading.Thread.CurrentThread.CurrentCulture
                Threading.Thread.CurrentThread.CurrentCulture = c
                _LastChange = value
                Threading.Thread.CurrentThread.CurrentCulture = oldc
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

    ''' <summary>By applying this attribute you informs other programmers that tehy shouldn't apply <see cref="AuthorAttribute"/> and <see cref="VersionAttribute"/> at this declaration level. They should apply them to nested code elements.</summary>
    <AttributeUsage(AuthorAndVersionAttributesUsage, AllowMultiple:=False, Inherited:=False)> _
    <Author("Đonny", "dzony.dz@gmail.com"), Version(1, 0, GetType(DoNotApplyAuthorAndVersionAttributesAttribute), LastChange:="12/20/2006")> _
    Public Class DoNotApplyAuthorAndVersionAttributesAttribute : Inherits Attribute
    End Class

    ''' <summary>Declares things that are spacific for attributes declared in <see cref="Tools.Internal"/> namespace</summary>
    <Author("Đonny", "dzony.dz@gmail.com"), Version(1, 0, GetType(AttributesSpecificDeclarations), LastChange:="12/20/2006")> _
    Friend Module AttributesSpecificDeclarations
        ''' <summary>Defines value for <see cref="AttributeUsageAttribute"/> applyed on <see cref="AuthorAttribute"/>, <see cref="VersionAttribute"/> and realetd attributes.</summary>
        ''' <remarks>DO NOT remove ored constants from this declaration. Add constants only when you really need it.</remarks>
        Public Const AuthorAndVersionAttributesUsage As AttributeTargets = AttributeTargets.Assembly Or AttributeTargets.Class Or AttributeTargets.Delegate Or AttributeTargets.Enum Or AttributeTargets.Interface Or AttributeTargets.Method Or AttributeTargets.Module Or AttributeTargets.Struct
    End Module
End Namespace
#End If