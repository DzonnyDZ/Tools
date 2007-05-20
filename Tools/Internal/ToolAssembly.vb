
Imports System.Reflection, Microsoft.VisualBasic.CompilerServices
Imports Tools.Collections.Generic
Namespace Internal
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Represents assembly of <see cref="Tools"/> project</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ToolAssembly), LastChMMDDYYYY:="05/18/2007")> _
    <Tool(GetType(Tool), FirstVerMMDDYYYY:="05/18/2007")> _
    Public Class ToolAssembly
        ''' <summary>Contains value of the <see cref="Assembly"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Assembly As Assembly
        ''' <summary>CTor from assembly</summary>
        ''' <param name="Assembly">Assembly thhis tool will work with</param>
        Public Sub New(ByVal Assembly As Assembly)
            Me.Assembly = Assembly
        End Sub
        ''' <summary>CTor from type</summary>
        ''' <param name="Type">Any type from assembly this tool will work with</param>
        Public Sub New(ByVal Type As Type)
            Me.Assembly = Type.Assembly
        End Sub
        ''' <summary>Assembly this tool works with</summary>
        Public Property Assembly() As Assembly
            Get
                Return _Assembly
            End Get
            Private Set(ByVal value As Assembly)
                _Assembly = value
            End Set
        End Property
        ''' <summary>Indicates if assembly itself is decorated as tool (see <seealso cref="Tool.IsDecoratedAsTool"/></summary>
        ''' <returns>True if assembly is decorated with <see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/>. It doesn't necesaarrylly mean that assembly is valid tool (as defined by <see cref="Tool.IsValidTool"/>)</returns>
        Public ReadOnly Property IsTool() As Boolean
            Get
                Return Tool.MayBeTool(Assembly)
            End Get
        End Property
        ''' <summary>Returns value indicating if assembly is decorated with <see cref="DoNotApplyAuthorAndVersionAttributesAttribute"/></summary>
        Public ReadOnly Property IsNotTool() As Boolean
            Get
                Return Tool.ShouldNotBeTool(Assembly)
            End Get
        End Property
        ''' <summary><see cref="Tool"/> that represents current <see cref="ToolAssembly"/></summary>
        ''' <returns>New <see cref="Tool"/> initialized with <see cref="Assembly"/>. Note: This tool can be invalid.</returns>
        Public Function MeAsTool() As Tool
            Return Tool.InitInvalid(Assembly)
        End Function
        ''' <summary>Gets list of all items that are decorated with either <see cref="AuthorAttribute"/> or <see cref="VersionAttribute"/> within assembly</summary>
        ''' <remarks>Returned tools can be invalid. Methods and fierlds declared directly in portable executable module are ignored.</remarks>
        Public Function GetAllTools() As IReadOnlyIndexable(Of Tool)
            Dim ret As New List(Of Tool)
            Dim ShouldBeTools As New List(Of Tool)
            If Me.IsTool Then ret.Add(Tool.InitInvalid(Assembly))
            If Not Me.IsTool AndAlso Not Me.IsNotTool Then ShouldBeTools.Add(Tool.InitInvalid(Assembly))
            For Each m As [Module] In Assembly.GetModules
                If Tool.MayBeTool(m) Then ret.Add(Tool.InitInvalid(m))
                For Each t As Type In m.GetTypes()
                    If Tool.MayBeTool(t) Then
                        ret.Add(Tool.InitInvalid(t))
                    ElseIf Tool.ShouldNotBeTool(t) AndAlso Not Tool.IsNestedInTool(t) AndAlso Tool.IsTypePublicOrProtected(t) Then
                        ShouldBeTools.Add(Tool.InitInvalid(t))
                    End If
                    For Each i As MemberInfo In t.GetMembers()
                        If i.MemberType <> MemberTypes.TypeInfo AndAlso i.MemberType <> MemberTypes.NestedType AndAlso Tool.MayBeTool(i) Then
                            ret.Add(Tool.InitInvalid(i))
                        ElseIf i.MemberType <> MemberTypes.TypeInfo AndAlso i.MemberType <> MemberTypes.NestedType AndAlso Tool.IsTypePublicOrProtected(i.DeclaringType) AndAlso Tool.IsMemberPublicOrProtected(i) AndAlso Not Tool.IsNestedInTool(i) AndAlso Tool.IsTypePublicOrProtected(i.DeclaringType) Then
                            ShouldBeTools.Add(Tool.InitInvalid(i))
                        End If
                    Next i
                Next t
            Next m
            _ShouldBeTools = New ReadOnlyListAdapter(Of Tool)(ShouldBeTools)
            Return New ReadOnlyListAdapter(Of Tool)(ret)
        End Function
        ''' <summary>Contains value of the <see cref="ShouldBeTools"/> property</summary>
        Private _ShouldBeTools As IReadOnlyList(Of Tool)
        ''' <summary>Contains emelents that are neither nested within another tool nor are decorated with <see cref="DoNotApplyAuthorAndVersionAttributesAttribute"/> and are public or protected, so it seems that somebody forgot to mark tem as tools or mark them as not tools</summary>
        ''' <remarks>All returned tools are invalid</remarks>
        Public ReadOnly Property ShouldBeTools() As IReadOnlyList(Of Tool)
            Get
                If _ShouldBeTools Is Nothing Then GetAllTools()
                Return _ShouldBeTools
            End Get
        End Property
        ''' <summary>Compares two <see cref="ToolAssembly"/>s</summary>
        ''' <param name="a">A <see cref="ToolAssembly"/></param>
        ''' <param name="b">A <see cref="ToolAssembly"/></param>
        ''' <returns>True if <see cref="ToolAssembly.Assembly"/> of <paramref name="a"/> and <paramref name="b"/> are equivalent</returns>
        Public Shared Operator =(ByVal a As ToolAssembly, ByVal b As ToolAssembly) As Boolean
            Return a.Assembly.Equals(b.Assembly)
        End Operator
        ''' <summary>Compares two <see cref="ToolAssembly"/>s</summary>
        ''' <param name="a">A <see cref="ToolAssembly"/></param>
        ''' <param name="b">A <see cref="ToolAssembly"/></param>
        ''' <returns>False if <see cref="ToolAssembly.Assembly"/> of <paramref name="a"/> and <paramref name="b"/> are equivalent</returns>
        Public Shared Operator <>(ByVal a As ToolAssembly, ByVal b As ToolAssembly) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="ToolAssembly"/>.</summary>
        ''' <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="ToolAssembly"/>.</param>
        ''' <returns>true if the specified <see cref="System.Object"/> is equal to the current <see cref="ToolAssembly"/>; otherwise, false.</returns>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is ToolAssembly Then
                Return Me = obj
            ElseIf TypeOf obj Is Assembly Then
                Return Me.Assembly.Equals(obj)
            Else
                Return MyBase.Equals(obj)
            End If
        End Function
    End Class
    ''' <summary>Represents one tool</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Tool), LastChMMDDYYYY:="05/18/2007")> _
    <MainTool(FirstVerMMDDYYYY:="05/18/2007")> _
    Public Class Tool
        Implements IEquatable(Of Tool)
#Region "Enums"
        ''' <summary>Main type of tool</summary>
        Public Enum MainToolTypes
            ''' <summary>The tool is whole assembly</summary>
            Assembly = 10
            ''' <summary>The tool is type</summary>
            ''' <remarks>That is Class, Structure, Delegate, Interface, Enum, Module (Visual Basic standard module)</remarks>
            Type = 30
            ''' <summary>The tool is method</summary>
            ''' <remarks>That is Sub, Function (including Declare) or Operator (CTor cannot be tool). Getters or setters of properties should not be marked as tools.</remarks>
            Method = 50
            ''' <summary>The tool is property</summary>
            [Property] = 40
            ''' <summary>The tool is event</summary>
            [Event] = 45
            ''' <summary>The tool is module</summary>
            ''' <remarks>Note: PEModule refers to a portable executable file (.dll or.exe) and not a Visual Basic standard module.</remarks>
            PEModule = 20
        End Enum
        ''' <summary>Detailed type of tool</summary>
        Public Enum DetailedToolTypes
            ''' <summary>The tool is whole assembly</summary>
            Assembly = MainToolTypes.Assembly
            ''' <summary>The tool is class</summary>
            [Class] = MainToolTypes.Type + 1
            ''' <summary>The tool is structure</summary>
            [Structure] = MainToolTypes.Type + 2
            ''' <summary>The tools is delegate</summary>
            [Delegate] = MainToolTypes.Type + 3
            ''' <summary>The tool is interface</summary>
            [Interface] = MainToolTypes.Type + 4
            ''' <summary>The tool is enum</summary>
            [Enum] = MainToolTypes.Type + 5
            ''' <summary>The tool is Visual Basic standard module</summary>
            [Module] = MainToolTypes.Type + 5
            ''' <summary>The tool is sub (method which returns void)</summary>
            ''' <remarks>This includes Declare Sub ([DllImport]), but such subs should not bee tools and should be friend or private</remarks>
            [Sub] = MainToolTypes.Method + 1
            ''' <summary>The tools is function (method wich returns something)</summary>
            ''' <remarks>This includes Declare Function ([DllImport]), but such functions should not bee tools and should be friend or private</remarks>
            [Function] = MainToolTypes.Method + 2
            ''' <summary>The tool is operator</summary>
            [Operator] = MainToolTypes.Method + 3
            ''' <summary>The tool is property</summary>
            [Property] = MainToolTypes.Property
            ''' <summary>The tools is module</summary>
            ''' <remarks>Note: PEModule refers to a portable executable file (.dll or.exe) and not a Visual Basic standard module.</remarks>
            PEModule = MainToolTypes.PEModule
            ''' <summary>The tool is event</summary>
            [Event] = MainToolTypes.Event
        End Enum
#End Region
#Region "CTors"
        ''' <summary>Initializes new tool of <see cref="MainToolTypes.Assembly"/> type</summary>
        ''' <param name="Assembly">Assembly that the tool is represented by</param>
        ''' <exception cref="ArgumentException"><paramref name="Assembly"/> does not represent a valid tool. (See <seealso cref="IsValidTool"/>)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
        Public Sub New(ByVal Assembly As Assembly)
            Me.Assembly = Assembly
            If Not IsValidTool Then Throw New ArgumentException("Item passed is not valid tool")
        End Sub
        ''' <summary>Initializes new tool of <see cref="MainToolTypes.PEModule"/> type</summary>
        ''' <param name="Module">Protable Exacutable Module that the tool is represented by</param>
        ''' <exception cref="ArgumentException"><paramref name="Module"/> does not represent a valid tool. (See <seealso cref="IsValidTool"/>)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> is null</exception>
        Public Sub New(ByVal [Module] As [Module])
            Me.Module = [Module]
            If Not IsValidTool Then Throw New ArgumentException("Item passed is not valid tool")
        End Sub
        ''' <summary>Initializes new tools of <see cref="MainToolTypes.Type"/> type</summary>
        ''' <param name="Type">Type the tool is realized by</param>
        ''' <exception cref="ArgumentException"><paramref name="Type"/> does not represent a valid tool. (See <seealso cref="IsValidTool"/>)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        Public Sub New(ByVal Type As Type)
            Me.Type = Type
            If Not IsValidTool Then Throw New ArgumentException("Item passed is not valid tool")
        End Sub
        ''' <summary>Initializes new tools of <see cref="MainToolTypes.Method"/> type</summary>
        ''' <param name="Method">Method the tool is realized by</param>
        ''' <exception cref="ArgumentException"><paramref name="Method"/> does not represent a valid tool. (See <seealso cref="IsValidTool"/>)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        Public Sub New(ByVal Method As MethodInfo)
            Me.Member = Method
            If Not IsValidTool Then Throw New ArgumentException("Item passed is not valid tool")
        End Sub
        ''' <summary>Initializes new tools of <see cref="MainToolTypes.Event"/></summary>
        ''' <param name="Event">Event the tool is realized by</param>
        ''' <exception cref="ArgumentException"><paramref name="Event"/> does not represent a valid tool. (See <seealso cref="IsValidTool"/>)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Event"/> is null</exception>
        Public Sub New(ByVal [Event] As EventInfo)
            Me.Member = [Event]
            If Not IsValidTool Then Throw New ArgumentException("Item passed is not valid tool")
        End Sub
        ''' <summary>Initializes new tools of <see cref="MainToolTypes.Property"/></summary>
        ''' <param name="Property">Property the tool is realized by</param>
        ''' <exception cref="ArgumentException"><paramref name="Property"/> does not represent a valid tool. (See <seealso cref="IsValidTool"/>)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Property"/> is null</exception>
        Public Sub New(ByVal [Property] As PropertyInfo)
            Me.Member = [Property]
            If Not IsValidTool Then Throw New ArgumentException("Item passed is not valid tool")
        End Sub
#Region "Invalid tools initializers"
        ''' <summary>CTor of empty tools</summary>
        ''' <remarks>You SHOULD initialize tool immedialelly aster using this CTor</remarks>
        Private Sub New()
        End Sub
        ''' <summary>Initializes new tool of <see cref="MainToolTypes.Assembly"/> type</summary>
        ''' <param name="Assembly">Assembly that the tool is represented by</param>
        ''' <remarks>Unlike constructor, this method doesn't throw an exception when tool is not valid and allows you to create instance of invalid tool and work with it</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
        Public Shared Function InitInvalid(ByVal Assembly As Assembly) As Tool
            Dim ret As New Tool
            ret.Assembly = Assembly
            Return ret
        End Function
        ''' <summary>Initializes new tool of <see cref="MainToolTypes.[Event]"/> or <see cref="MainToolTypes.[Property]"/> or <see cref="MainToolTypes.Method"/> or <see cref="MainToolTypes.Type"/> type</summary>
        ''' <param name="Member">Assembly that the tool is represented by</param>
        ''' <remarks>Unlike constructor, this method doesn't throw an exception when tool is not valid and allows you to create instance of invalid tool and work with it. This overload allows you also to pass fields and constructors that are not valid member types and thus leed too invalid tool</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
        Public Shared Function InitInvalid(ByVal Member As MemberInfo) As Tool
            If TypeOf Member Is PropertyInfo Then
                Return InitInvalid(DirectCast(Member, PropertyInfo))
            ElseIf TypeOf Member Is EventInfo Then
                Return InitInvalid(DirectCast(Member, EventInfo))
            ElseIf TypeOf Member Is MethodInfo Then
                Return InitInvalid(DirectCast(Member, MethodInfo))
            ElseIf TypeOf Member Is Type Then
                Return InitInvalid(DirectCast(Member, Type))
            Else
                Dim ret As New Tool
                ret.Member = Member
                Return ret
            End If
        End Function
        ''' <summary>Initializes new tool of <see cref="MainToolTypes.PEModule"/> type</summary>
        ''' <param name="Module">Protable Exacutable Module that the tool is represented by</param>
        ''' <remarks>Unlike constructor, this method doesn't throw an exception when tool is not valid and allows you to create instance of invalid tool and work with it</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> is null</exception>
        Public Shared Function InitInvalid(ByVal [Module] As [Module]) As Tool
            Dim ret As New Tool
            ret.Module = [Module]
            Return ret
        End Function
        ''' <summary>Initializes new tools of <see cref="MainToolTypes.Type"/> type</summary>
        ''' <param name="Type">Type the tool is realized by</param>
        ''' <remarks>Unlike constructor, this method doesn't throw an exception when tool is not valid and allows you to create instance of invalid tool and work with it</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        Public Shared Function InitInvalid(ByVal Type As Type) As Tool
            Dim ret As New Tool
            ret.Type = Type
            Return ret
        End Function
        ''' <summary>Initializes new tools of <see cref="MainToolTypes.Method"/> type</summary>
        ''' <param name="Method">Method the tool is realized by</param>
        ''' <remarks>Unlike constructor, this method doesn't throw an exception when tool is not valid and allows you to create instance of invalid tool and work with it</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        Public Shared Function InitInvalid(ByVal Method As MethodInfo) As Tool
            Dim ret As New Tool
            ret.Member = Method
            Return ret
        End Function
        ''' <summary>Initializes new tools of <see cref="MainToolTypes.Property"/></summary>
        ''' <param name="Property">Property the tool is realized by</param>
        ''' <remarks>Unlike constructor, this method doesn't throw an exception when tool is not valid and allows you to create instance of invalid tool and work with it</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Property"/> is null</exception>
        Public Shared Function InitInvalid(ByVal [Property] As PropertyInfo) As Tool
            Dim ret As New Tool
            ret.Member = [Property]
            Return ret
        End Function
        ''' <summary>Initializes new tools of <see cref="MainToolTypes.Event"/></summary>
        ''' <param name="Event">Event the tool is realized by</param>
        ''' <remarks>Unlike constructor, this method doesn't throw an exception when tool is not valid and allows you to create instance of invalid tool and work with it</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Event"/> is null</exception>
        Public Shared Function InitInvalid(ByVal [Event] As EventInfo) As Tool
            Dim ret As New Tool
            ret.Member = [Event]
            Return ret
        End Function
#End Region
#End Region
#Region "Tool identification"
        ''' <summary>Contains value of the <see cref="[Assembly]"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Assembly As Assembly
        ''' <summary>Contains value of the <see cref="[Type]"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Type As Type
        ''' <summary>Contains value of the <see cref="[Member]"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Member As MemberInfo
        ''' <summary>Contains value of the <see cref="[Module]"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Module As [Module]
        ''' <summary>Assembly in which the tool is contained</summary>
        ''' <remarks>For tools of type <see cref="MainToolTypes.Assembly"/> this is the tool itself</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        Public Property Assembly() As Assembly
            Get
                Return _Assembly
            End Get
            Private Set(ByVal value As Assembly)
                If value Is Nothing Then Throw New ArgumentNullException("value", "value cannot be null")
                _Assembly = value
            End Set
        End Property
        ''' <summary>Module in which the tool is contained</summary>
        ''' <remarks>Forr tools of type <see cref="MainToolTypes.PEModule"/> this is the tool itself</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        Public Property [Module]() As [Module]
            Get
                Return _Module
            End Get
            Private Set(ByVal value As [Module])
                If value Is Nothing Then Throw New ArgumentNullException("value", "value cannot be null")
                _Module = value
                Assembly = value.Assembly
            End Set
        End Property
        ''' <summary>Type that represents the tool</summary>
        ''' <remarks>
        ''' For tools of type <see cref="MainToolTypes.Assembly"/> and <see cref="MainToolTypes.PEModule"/> this is null.
        ''' For tools of type <see cref="MainToolTypes.Method"/>, <see cref="MainToolTypes.[Property]"/> or <see cref="MainToolTypes.[Event]"/> this is the type that contains the tool.
        ''' </remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        Public Property Type() As Type
            Get
                Return _Type
            End Get
            Private Set(ByVal value As Type)
                If value Is Nothing Then Throw New ArgumentNullException("value", "value cannot be null")
                _Type = value
                [Module] = value.Module
            End Set
        End Property
        ''' <summary>Member thet represents the tool</summary>
        ''' <remarks>For tools of type <see cref="Assembly"/>, <see cref="MainToolTypes.PEModule"/> and <see cref="Type"/> this is null.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        Public Property Member() As MemberInfo
            Get
                Return _Member
            End Get
            Private Set(ByVal value As MemberInfo)
                If value Is Nothing Then Throw New ArgumentNullException("value", "value cannot be null")
                _Member = value
                Type = value.DeclaringType
            End Set
        End Property
#End Region
#Region "Tool properties"
#Region "Deep reflection"
        ''' <summary>Gets value indicating if tool is visible from ouside of its assembly (thats if in its nesting hierarchy are only public and protected types and tool itself is public or protected)</summary>
        ''' <returns>True for valid tools, always true form <see cref="MainToolTypes.Assembly"/> and <see cref="MainToolTypes.PEModule"/> tools.</returns>
        ''' <exception cref="InvalidOperationException">The tool is of member type but <see cref="Member"/> is neither <see cref="PropertyInfo"/> nor <see cref="MethodInfo"/> nor <see cref="EventInfo"/></exception>
        ''' <remarks>
        ''' To be true for <see cref="MainToolTypes.[Property]"/> the property must declare at least one of set and get accessors as public or protected (family).
        ''' To be true for <see cref="MainToolTypes.[Event]"/> the event must declare bothe add and remove acessors as public or protected (family)
        ''' See also: <seealso cref="MethodInfo.IsPublic"/>, <seealso cref="MethodInfo.IsFamily"/>, <see cref="Type.IsPublic"/>, <see cref="Type.IsNestedFamily"/>
        ''' </remarks>
        Public ReadOnly Property IsPublicOrProtected() As Boolean
            Get
                Select Case MainType
                    Case MainToolTypes.Assembly, MainToolTypes.PEModule : Return True
                    Case MainToolTypes.Type
                        Return IsTypePublicOrProtected(Type)
                    Case MainToolTypes.Method
                        With DirectCast(Member, MethodInfo)
                            Return (.IsPublic OrElse .IsFamily) AndAlso IsTypePublicOrProtected(.DeclaringType)
                        End With
                    Case MainToolTypes.Property
                        Return IsPropertyPublicOrProtected(DirectCast(Member, PropertyInfo)) AndAlso IsTypePublicOrProtected(DirectCast(Member, PropertyInfo).DeclaringType)
                    Case MainToolTypes.Event
                        Return IsEventPublicOrProtected(DirectCast(Member, EventInfo)) AndAlso IsTypePublicOrProtected((DirectCast(Member, EventInfo)).DeclaringType)
                End Select
            End Get
        End Property
        ''' <summary>Gets information if event is public or protected (family) in its context or not </summary>
        ''' <param name="Event">Event to check</param>
        Public Shared Function IsEventPublicOrProtected(ByVal [Event] As EventInfo) As Boolean
            Return [Event].GetAddMethod IsNot Nothing AndAlso ([Event].GetAddMethod.IsPublic OrElse [Event].GetAddMethod.IsFamily) AndAlso [Event].GetRemoveMethod IsNot Nothing AndAlso ([Event].GetRemoveMethod.IsPublic OrElse [Event].GetRemoveMethod.IsFamily)
        End Function
        ''' <summary>Gets information if property is public or protected (family) in its context or not </summary>
        ''' <param name="Property">Property to check</param>
        Public Shared Function IsPropertyPublicOrProtected(ByVal [Property] As PropertyInfo) As Boolean
            Return ([Property].GetGetMethod IsNot Nothing AndAlso ([Property].GetGetMethod.IsPublic OrElse [Property].GetGetMethod.IsFamily)) OrElse ([Property].GetSetMethod IsNot Nothing AndAlso ([Property].GetSetMethod.IsPublic OrElse [Property].GetSetMethod.IsFamily))
        End Function
        ''' <summary>Checks if geiven type is visible from outside of its declaring assembly (in Nesting hierarchy are only public and protected types)</summary>
        ''' <param name="Type">Type to be checked</param>
        ''' <returns>True if in nesting hierarchy of <paramref name="Type"/> are only public and protected (family) types</returns>
        Public Shared Function IsTypePublicOrProtected(ByVal Type As Type) As Boolean
            If Not Type.IsNested Then Return Type.IsPublic
            Dim CPar As Type = Type.DeclaringType
            While CPar.IsNested
                If Not CPar.IsPublic AndAlso Not CPar.IsNestedFamily Then Return False
            End While
            Return CPar.IsPublic
        End Function
        ''' <summary>Checks if given meber is public or protected in its declaring context or not</summary>
        ''' <param name="member">Member to check</param>
        ''' <returns>True if member is public of protected (family). False otherwise. Also returns false if member is neither <see cref="MemberTypes.Constructor"/> nor <see cref="MemberTypes.[Event]"/> nor <see cref="MemberTypes.Field"/> nor <see cref="MemberTypes.Method"/> nor <see cref="MemberTypes.NestedType"/> nor <see cref="MemberTypes.[Property]"/> nor <see cref="MemberTypes.TypeInfo"/></returns>
        Public Shared Function IsMemberPublicOrProtected(ByVal member As MemberInfo) As Boolean
            Select Case member.MemberType
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsPublic OrElse DirectCast(member, MethodBase).IsFamily
                Case MemberTypes.Event
                    Return IsEventPublicOrProtected(member)
                Case MemberTypes.Property
                    Return IsPropertyPublicOrProtected(member)
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    Return DirectCast(member, Type).IsPublic OrElse DirectCast(member, Type).IsNestedFamily
                Case MemberTypes.Field
                    Return DirectCast(member, FieldInfo).IsPublic OrElse DirectCast(member, FieldInfo).IsFamily
                Case Else
                    Return False
            End Select
        End Function


        ''' <summary>Gets value indicating if tool is valid</summary>
        ''' <returns>Always true for valid tools</returns>
        ''' <remarks>Tool is valid whan it is <see cref="IsDecoratedAsTool"/>, <see cref="IsPublicOrProtected"/> and it's <see cref="DetailedType"/> is known</remarks>
        Public ReadOnly Property IsValidTool() As Boolean
            Get
                Try
                    Dim __ As DetailedToolTypes = DetailedType
                    Return IsPublicOrProtected AndAlso IsDecoratedAsTool AndAlso NotToolMarker Is Nothing
                Catch ex As InvalidOperationException
                    Return False
                End Try
            End Get
        End Property
        ''' <summary>Main type of tool</summary>
        ''' <exception cref="InvalidOperationException">The tool is of member type but <see cref="Member"/> is neither <see cref="PropertyInfo"/> nor <see cref="MethodInfo"/> nor <see cref="EventInfo"/></exception>
        Public ReadOnly Property MainType() As MainToolTypes
            Get
                If [Module] Is Nothing Then
                    Return MainToolTypes.Assembly
                ElseIf Type Is Nothing Then
                    Return MainToolTypes.PEModule
                ElseIf Member Is Nothing Then
                    Return MainToolTypes.Type
                Else
                    If TypeOf Member Is MethodInfo Then
                        Return MainToolTypes.Method
                    ElseIf TypeOf Member Is PropertyInfo Then
                        Return MainToolTypes.Property
                    ElseIf TypeOf Member Is EventInfo Then
                        Return MainToolTypes.Event
                    Else
                        Throw New InvalidOperationException("The tools is invalid")
                    End If
                End If
            End Get
        End Property
        ''' <summary>Gets detailed tool type</summary>
        ''' <exception cref="InvalidOperationException">Tool is invalid because:
        ''' The tool is of member type but <see cref="Member"/> is neither <see cref="PropertyInfo"/> nor <see cref="MethodInfo"/> nor <see cref="EventInfo"/> -or-
        ''' The tool is applyed on method that is part of Property or Event -or-
        ''' The tool is type type but type of type is not known
        ''' </exception>
        Public ReadOnly Property DetailedType() As DetailedToolTypes
            Get
                Select Case MainType
                    Case MainToolTypes.Assembly : Return DetailedToolTypes.Assembly
                    Case MainToolTypes.PEModule : Return DetailedToolTypes.PEModule
                    Case MainToolTypes.Property : Return DetailedToolTypes.Property
                    Case MainToolTypes.Event : Return DetailedToolTypes.Event
                    Case MainToolTypes.Method
                        With DirectCast(Member, MethodInfo)
                            If .IsSpecialName Then
                                'It can be Operator, Getter, Setter or Event method
                                Dim IsSpecialForbidden As Boolean = False
                                Dim Properties As PropertyInfo() = Type.GetProperties
                                Dim Events As EventInfo() = Type.GetEvents
                                If Properties IsNot Nothing Then
                                    'Look for getters and setters
                                    For Each Prop As PropertyInfo In Properties
                                        If Prop.GetAccessors(True) IsNot Nothing Then
                                            For Each Accessor As MethodInfo In Prop.GetAccessors(True)
                                                If Accessor Is Member Then
                                                    IsSpecialForbidden = True
                                                    Exit For
                                                End If
                                            Next Accessor
                                            If IsSpecialForbidden Then Exit For
                                        End If
                                    Next Prop
                                End If
                                If Not IsSpecialForbidden AndAlso Events IsNot Nothing Then
                                    'Look for event methods
                                    For Each Evnt As EventInfo In Events
                                        If Evnt.GetRaiseMethod(True) Is Member OrElse Evnt.GetAddMethod Is Member OrElse Evnt.GetRemoveMethod Is Member Then
                                            IsSpecialForbidden = True
                                            Exit For
                                        End If
                                        If Evnt.GetOtherMethods(True) IsNot Nothing Then
                                            For Each OtherMethod As MethodInfo In Evnt.GetOtherMethods(True)
                                                If OtherMethod Is Member Then
                                                    IsSpecialForbidden = True
                                                    Exit For
                                                End If
                                            Next OtherMethod
                                            If IsSpecialForbidden Then Exit For
                                        End If
                                    Next Evnt
                                End If
                                If IsSpecialForbidden Then
                                    Throw New InvalidOperationException("Tool is applyed on type that it cannot be applyed on")
                                End If
                                'If no method found hope that it is Operator
                                Return DetailedToolTypes.Operator
                            Else
                                If .ReturnType.Equals(GetType(Void)) Then
                                    Return DetailedToolTypes.Sub
                                Else
                                    Return DetailedToolTypes.Function
                                End If
                            End If
                        End With
                    Case MainToolTypes.Type
                        If Type.IsEnum Then
                            Return DetailedToolTypes.Enum
                        ElseIf Type.IsInterface Then
                            Return DetailedToolTypes.Interface
                        ElseIf Type.IsSubclassOf(GetType([Delegate])) Then
                            Return DetailedToolTypes.Delegate
                        ElseIf Type.IsValueType Then
                            Return DetailedToolTypes.Structure
                        ElseIf Type.IsClass Then
                            If GetAttribute(Of StandardModuleAttribute)(Type) IsNot Nothing Then
                                Return DetailedToolTypes.Module
                            Else
                                Return DetailedToolTypes.Class
                            End If
                        Else
                            Throw New InvalidOperationException("Tool is applyed on unknown type type")
                        End If
                End Select
            End Get
        End Property
        ''' <summary>Flags that describes why the tool is not valid</summary>
        Public Enum InvalidReasons
            ''' <summary>The tool is valid</summary>
            Valid = 0
            ''' <summary><see cref="MainType"/> caused <see cref="InvalidOperationException"/>. This implyes <see cref="InvalidReasons.UnknownDetailedType"/> set and <see cref="InvalidReasons.NotPublic"/> not set</summary>
            UnknownMainType = 1
            ''' <summary><see cref="DetailedType"/> caused <see cref="InvalidOperationException"/></summary>
            UnknownDetailedType = 2
            ''' <summary><see cref="IsPublicOrProtected"/> returned false. This flag is never set when <see cref="InvalidReasons.UnknownMainType"/> is set.</summary>
            NotPublic = 4
            ''' <summary><see cref="IsDecoratedAsTool"/> returned false</summary>
            NotDecorated = 8
            ''' <summary><see cref="NotToolMarker"/> didn't return null</summary>
            DecoratedAsNotTool = 16
        End Enum
        ''' <summary>Gets reasons why the tools is invalid</summary>
        ''' <returns>Or-ed reasons why the tools is invalid or <see cref="InvalidReasons.Valid"/> if tool is valid</returns>
        Public ReadOnly Property InvalidReason() As InvalidReasons
            Get
                Dim ret As InvalidReasons = InvalidReasons.Valid
                Try
                    Dim __ As MainToolTypes = MainType
                Catch ex As InvalidOperationException
                    ret = ret Or InvalidReasons.UnknownMainType
                End Try
                Try
                    Dim __ As DetailedToolTypes = DetailedType
                Catch ex As InvalidOperationException
                    ret = ret Or InvalidReasons.UnknownDetailedType
                End Try
                Try
                    If Not IsPublicOrProtected Then ret = ret Or InvalidReasons.NotPublic
                Catch ex As InvalidOperationException : End Try
                If Not IsDecoratedAsTool Then ret = ret Or InvalidReasons.NotDecorated
                If NotToolMarker IsNot Nothing Then ret = ret Or InvalidReasons.DecoratedAsNotTool
                Return ret
            End Get
        End Property
#End Region
        ''' <summary>The elment the tool is represented by</summary>
        Public ReadOnly Property Tool() As ICustomAttributeProvider
            Get
                If [Module] Is Nothing Then
                    Return Assembly
                ElseIf Type Is Nothing Then
                    Return [Module]
                ElseIf Member Is Nothing Then
                    Return Type
                Else
                    Return Member
                End If
            End Get
        End Property

        ''' <summary>Gets value indicating if this tool is well-decorated as tool</summary>
        ''' <returns>For valid tools always True</returns>
        Public ReadOnly Property IsDecoratedAsTool() As Boolean
            Get
                Return Author IsNot Nothing AndAlso Version IsNot Nothing
            End Get
        End Property
        ''' <summary><see cref="AuthorAttribute"/> applyed on the tool</summary>
        ''' <returns>For valid tool always returns nonnull value</returns>
        Public ReadOnly Property Author() As AuthorAttribute
            Get
                Return GetAttribute(Of AuthorAttribute)(Tool)
            End Get
        End Property
        ''' <summary><see cref="VersionAttribute"/> applyed on the tool</summary>
        ''' <returns>For valid tool always returns nonnull value</returns>
        Public ReadOnly Property Version() As VersionAttribute
            Get
                Return GetAttribute(Of VersionAttribute)(Tool)
            End Get
        End Property
        ''' <summary><see cref="ToolAttribute"/> applyed on the tool (if any)</summary>
        Public ReadOnly Property ToolInfo() As ToolAttribute
            Get
                Return GetAttribute(Of ToolAttribute)(Tool)
            End Get
        End Property
        ''' <summary>Gets first custom attribute</summary>
        ''' <param name="From">Element to get attribute from</param>
        ''' <typeparam name="T">Type of attribute to be got</typeparam>
        ''' <returns>Attribute applyed on <paramref name="From"/> if any attribute of type <paramref name="Type"/> is present, null otherwise.</returns>
        Private Shared Function GetAttribute(Of T As Attribute)(ByVal From As ICustomAttributeProvider) As T
            Dim Attrs() As Object = From.GetCustomAttributes(GetType(T), False)
            If Attrs IsNot Nothing AndAlso Attrs.Length > 0 Then
                Return Attrs(0)
            Else
                Return Nothing
            End If
        End Function
        ''' <summary>Gets attribute that marks current declaration as not tool</summary>
        ''' <returns><see cref="DoNotApplyAuthorAndVersionAttributesAttribute"/> applyed or null is it is not applyed. For valid tools always returns null.</returns>
        Public ReadOnly Property NotToolMarker() As DoNotApplyAuthorAndVersionAttributesAttribute
            Get
                Return GetAttribute(Of DoNotApplyAuthorAndVersionAttributesAttribute)(Tool)
            End Get
        End Property
        ''' <summary>Gets value indivating if tool is decorated with <see cref="MainToolAttribute"/></summary>
        Public ReadOnly Property IsMainTool() As Boolean
            Get
                Return GetAttribute(Of MainToolAttribute)(Tool) IsNot Nothing
            End Get
        End Property
        ''' <summary>Gets value indivating if tool is stand-alone</summary>
        ''' <remarks>Tool is considered to be stand-alone when there is no <see cref="ToolAttribute"/> (or derived) applyed or when <see cref="StandAloneToolAttribute"/> is applyed. Main tools are not considered to be stand-alone.</remarks>
        Public ReadOnly Property IsStandAloneTool() As Boolean
            Get
                Return GetAttribute(Of StandAloneToolAttribute)(Tool) IsNot Nothing OrElse GetAttribute(Of ToolAttribute)(Tool) Is Nothing
            End Get
        End Property
        ''' <summary>Returns tool this current tool is related to (if any)</summary>
        ''' <returns>For main tools returns related main tool in 1st level of same group, for dependent tools returns parent tools. Related tool can be only of <see cref="MainToolTypes.Type"/> type. Returns null if no related tool is specified. If null is returned for main tool it doesn't necessarryly mean that the tool is not related with another. It can be also the situation when only one of related tools of 1st elevel is decorated with <see cref="MainToolAttribute"/> defining related tool.</returns>
        Public ReadOnly Property RelatedTool() As Tool
            Get
                If Me.ToolInfo IsNot Nothing AndAlso Me.ToolInfo.Group IsNot Nothing Then
                    Return InitInvalid(Me.ToolInfo.Group)
                Else : Return Nothing
                End If
            End Get
        End Property
#End Region
#Region "Shared tools"
        ''' <summary>Indicates if given element may be tool</summary>
        ''' <param name="Elem">An element to check</param>
        ''' <returns>True if <paramref name="Elem"/> is decorated either with <see cref="AuthorAttribute"/> or <see cref="VersionAttribute"/></returns>
        Public Shared Function MayBeTool(ByVal Elem As ICustomAttributeProvider) As Boolean
            Return GetAttribute(Of VersionAttribute)(Elem) IsNot Nothing OrElse GetAttribute(Of AuthorAttribute)(Elem) IsNot Nothing
        End Function
        ''' <summary>Indicates if given element should not be tool</summary>
        ''' <param name="elem">An element to check</param>
        ''' <returns>True if <paramref name="Elem"/> is decorated with <see cref="DoNotApplyAuthorAndVersionAttributesAttribute"/></returns>
        Public Shared Function ShouldNotBeTool(ByVal Elem As ICustomAttributeProvider) As Boolean
            Return GetAttribute(Of DoNotApplyAuthorAndVersionAttributesAttribute)(Elem) IsNot Nothing
        End Function
        ''' <summary>Gets information if given member is nested within tool</summary>
        ''' <param name="Member">A member to check</param>
        ''' <returns>True if any type in nesting hierarchy of meber or module or assembly makes <see cref="MayBeTool"/> to return true</returns>
        Public Shared Function IsNestedInTool(ByVal Member As MemberInfo) As Boolean
            Return MayBeTool(Member.Module) OrElse MayBeTool(Member.Module.Assembly) OrElse (Member.DeclaringType IsNot Nothing AndAlso MayBeTool(Member.DeclaringType) OrElse IsNestedInTool(Member.DeclaringType))
        End Function
        ''' <summary>Compares two <see cref="Tool"/>s</summary>
        ''' <param name="a">A <see cref="Tool"/></param>
        ''' <param name="b">A <see cref="Tool"/></param>
        ''' <returns>True if <paramref name="a"/> equals to <paramref name="b"/></returns>
        Public Shared Operator =(ByVal a As Tool, ByVal b As Tool) As Boolean
            Return CObj(a.Tool).Equals(b.Tool)
        End Operator
        ''' <summary>Compares two <see cref="Tool"/>s</summary>
        ''' <param name="a">A <see cref="Tool"/></param>
        ''' <param name="b">A <see cref="Tool"/></param>
        ''' <returns>False if <paramref name="a"/> equals to <paramref name="b"/></returns>
        Public Shared Operator <>(ByVal a As Tool, ByVal b As Tool) As Boolean
            Return Not a = b
        End Operator
#End Region
#Region "Implementation"
        ''' <summary>Indicates whether the current <see cref="Tool"/> is equal to another <see cref="Tool"/>.</summary>
        ''' <param name="other">A <see cref="Tool"/> to compare with this <see cref="Tool"/>.</param>
        ''' <returns>true if the current <see cref="Tool"/> is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        Public Overloads Function Equals(ByVal other As Tool) As Boolean Implements System.IEquatable(Of Tool).Equals
            Return Me = other
        End Function
        ''' <summary>Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Tool"/>.</summary>
        ''' <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Tool"/>.</param>
        ''' <returns>true if the specified <see cref="System.Object"/> is equal to the current <see cref="Tool"/>; otherwise, false.</returns>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is Tool Then
                Return Me = obj
            Else
                Return MyBase.Equals(obj)
            End If
        End Function
        ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="System.Object"/>.</summary>
        ''' <returns>A <see cref="System.String"/> that represents the current <see cref="System.Object"/></returns>
        Public Overrides Function ToString() As String
            Return CObj(Tool).ToString
        End Function
#End Region
        ''' <summary>Contains value of the <see cref="SubTools"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _SubTools As New List(Of Tool)
        ''' <summary>Returns all <see cref="Tool"/>s which's <see cref="RelatedTool"/> point to thes tool and <see cref="ToolAttribute"/> applyed is not <see cref="MainToolAttribute"/></summary>
        ''' <returns>An empty <see cref="List(Of Tool)"/> unless the <see cref="ToolAssembly"/> this tool belongs too is parsed by <see cref="ToolAssemblyStructure"/>'s ctor</returns>
        Public ReadOnly Property SubTools() As List(Of Tool)
            Get
                Return _SubTools
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Group"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Group As ToolAssemblyStructure.ToolGroup
        ''' <summary>Group this tool belongs too</summary>
        ''' <returns>Null unless the <see cref="ToolAssembly"/> this tool belongs too is parsed by <see cref="ToolAssemblyStructure"/>'s ctor</returns>
        Public Property Group() As ToolAssemblyStructure.ToolGroup
            Get
                Return _Group
            End Get
            Set(ByVal value As ToolAssemblyStructure.ToolGroup)
                _Group = value
            End Set
        End Property

    End Class
    ''' <summary>Represents parsed structure of assembly of <see cref="Tools"/>. This class allow to read relations between tools</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ToolAssemblyStructure), LastChMMDDYYYY:="05/18/2007")> _
    <Tool(GetType(Tool), FirstVerMMDDYYYY:="05/18/2007")> _
    Public Class ToolAssemblyStructure
        ''' <summary>CTor from <see cref="ToolAssembly"/></summary>
        ''' <param name="Asm"><see cref="ToolAssembly"/> that should be parsed</param>
        Public Sub New(ByVal Asm As ToolAssembly)
            Dim SubTools As New List(Of Tool)
            For Each Tool As Tool In Asm.GetAllTools
                If Tool.IsStandAloneTool Then
                    StandAloneTools.Add(Tool)
                ElseIf Tool.IsMainTool Then
                    FindGroup(Tool)
                Else
                    SubTools.Add(Tool)
                End If
            Next Tool
            For Each SubTool As Tool In SubTools
                SubTool.Group = FindSubToolGroup(SubTool)
            Next SubTool
            _NotToolsShouldBeTools = New List(Of Tool)(Asm.ShouldBeTools)
        End Sub
        ''' <summary>Searches for <see cref="ToolGroup"/> that <see cref="Tool"/> belongs to</summary>
        ''' <param name="Tool"><see cref="Tool"/> which's <see cref="ToolGroup"/> should be found</param>
        ''' <returns><see cref="ToolGroup"/> that <paramref name="Tool"/> belongs to</returns>
        ''' <remarks>If no group is found new group is founded</remarks>
        Private Function FindGroup(ByVal Tool As Tool) As ToolGroup
            If Tool.Group IsNot Nothing Then Return Tool.Group
            If Tool.RelatedTool Is Nothing Then
                Dim G As New ToolGroup
                Groups.Add(G)
                G.MainTools.Add(Tool)
                Tool.Group = G
            Else
                Tool.Group = FindGroup(Tool.RelatedTool)
            End If
            Return Tool.Group
        End Function
        ''' <summary>Finds <see cref="ToolGroup"/> that <see cref="Tool"/> belongs to</summary>
        ''' <param name="SubTool"><see cref="Tool"/> which's <see cref="ToolGroup"/> should be found</param>
        ''' <returns><see cref="ToolGroup"/> that <paramref name="Tool"/> belongs to or null is no <see cref="ToolGroup"/> is found</returns>
        Private Function FindSubToolGroup(ByVal SubTool As Tool) As ToolGroup
            If SubTool.Group IsNot Nothing Then
                Return SubTool.Group
            ElseIf SubTool.RelatedTool Is Nothing Then
                Return Nothing
            ElseIf SubTool.RelatedTool.Group IsNot Nothing Then
                Return SubTool.RelatedTool.Group
            Else
                Return FindSubToolGroup(SubTool.RelatedTool)
            End If
        End Function
        ''' <summary>All stand-alone tools in assembly</summary>
        Public ReadOnly Property StandAloneTools() As List(Of Tool)
            Get
                Return _StandAloneTools
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="StandAloneTools"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _StandAloneTools As List(Of Tool)
        ''' <summary>All toolgroups in assembly</summary>
        Public ReadOnly Property Groups() As List(Of ToolGroup)
            Get
                Return _Groups
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Groups"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Groups As List(Of ToolGroup)
        ''' <summary>Represents group of tools</summary>
        Public Class ToolGroup
            ''' <summary>Contains value of the <see cref="MainTools"/> property</summary>
            Private _MainTools As New List(Of Tool)
            ''' <summary>Retuns main tools of group</summary>
            Public ReadOnly Property MainTools() As List(Of Tool)
                Get
                    Return _MainTools
                End Get
            End Property
            ''' <summary>Name of group</summary>
            ''' <returns>Name fo group if exactly one distinct <see cref="MainToolAttribute.GroupName"/> is found in <see cref="MainTools"/>. In case no name is found returns an empty <see cref="String"/>. In case more names are specified throws an <see cref="InvalidOperationException"/></returns>
            ''' <exception cref="InvalidOperationException">There are more distinc names of group specified</exception>
            Public ReadOnly Property Name() As String
                Get
                    Dim GName As String = ""
                    For Each t As Tool In MainTools
                        If t.ToolInfo IsNot Nothing AndAlso TypeOf t.ToolInfo Is MainToolAttribute AndAlso DirectCast(t.ToolInfo, MainToolAttribute).GroupName <> "" Then
                            With DirectCast(t.ToolInfo, MainToolAttribute)
                                If GName = "" Then : GName = .GroupName
                                Else : Throw New InvalidOperationException("More than one group names specified") : End If
                            End With
                        End If
                    Next t
                    Return GName
                End Get
            End Property
        End Class
        ''' <summary>Contains value of the <see cref="NotToolsShouldBeTools"/> property</summary>
        Private _NotToolsShouldBeTools As List(Of Tool)
        ''' <summary>Retuns same value as <see cref="ToolAssembly.ShouldBeTools"/> (element that are not marked as tools but are not nested within tools and are not marked with <see cref="DoNotApplyAuthorAndVersionAttributesAttribute"/></summary>
        Public ReadOnly Property NotToolsShouldBeTools() As List(Of Tool)
            Get
                Return _NotToolsShouldBeTools
            End Get
        End Property
    End Class
#End If
End Namespace