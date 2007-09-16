Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath
Imports System.Reflection
Imports System.ComponentModel

#If Config <= Nightly Then
Namespace XmlT.XPathT
    'ASAP:Mark, Wiki, Comment, Forum
    ''' <summary>Implements <see cref="XPathNavigator"/> over any object structure</summary>
    ''' <remarks>
    ''' <para>Pass any object to CTor of this class and peudo-XML tree structure that can be navigated using XPath will be created.</para>
    ''' <para>
    ''' The structure alwasy consists of root node and sequence of other nodes.
    ''' For some spcially supported types (like <see cref="Int32"/> or <see cref="String"/>; see <seealso cref="XPathObjectNavigator.Value"/> for information) the text node is created and nothing else.
    ''' For other types if created element node with three or four attributes and elements named as properties of such type and element named value-of for items of <see cref="IEnumerable"/>.
    ''' </para>
    ''' <para>The attributes are:</para>
    ''' <list type="table"><listheader><term>Attribute name</term><description>Description</description></listheader>
    ''' <item><term>type-name</term><description>Short name of type represented by node (see <seealso cref="Type.Name"/>)</description></item>
    ''' <item><term>full-name</term><description>Full name of type represented by node (see <seealso cref="Type.FullName"/>)</description></item>
    ''' <item><term>name</term><description>Name of property through which the object have been obtained. For rooth node contains <see cref="String.Empty"/></description></item>
    ''' <item><term>enumerable</term><description>If object of current node is <see cref="IEnumerable"/> contains true; otherwise it is not present.</description></item>
    ''' </list>
    ''' <para>Example for <see cref="List(Of String)"/></para>
    ''' <example>
    ''' <![CDATA[
    ''' < type-name="List`1" full-name="System.Collections.Generic.List`1[[System.String, mscorlib, Versionb=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]" name="" enumerable="true">
    '''     <Capacity type-name="Int32" full-name="System.Int32" name="Capacity">3</Capacity>
    '''     <Count type-name="Int32" full-name="System.Int32" name="Count">3</Count>
    '''     <item-of type-name="String" full-name="System.String" name="GetEnumerator">Item 1</item>
    '''     <item-of type-name="String" full-name="System.String" name="GetEnumerator">Item 2</item>
    '''     <item-of type-name="String" full-name="System.String" name="GetEnumerator">Item 3</item>
    ''' </ >
    ''' ]]>
    ''' </example>
    ''' Note: Root node is unnamed. Properties can be of complex types (stored as sub-trees). Order of occurence of attributes is as shown. Properties are alwas before enum items. name from enum items is always GetEnumerator. Name of node representing enum item - item-of can never be in conflict with name of property because it contains hyppen.
    ''' </remarks>
    Public Class XPathObjectNavigator : Inherits XPathNavigator

        'Private Class X
        '    Public ReadOnly Property A() As String
        '    Public ReadOnly Property B() As Long
        '    Public ReadOnly Property C() As List(Of String)
        '    Public ReadOnly Property D() As List(Of Y)
        '    End Property
        '    Public ReadOnly Property E() As Y
        'End Class
        'Private Class Y : Implements IEnumerable(Of Long)
        '    Public ReadOnly Property Count() As Integer
        'End Class

        'instance of X
        ' type-name="X" full-name="A.X"  name=""
        '<A type-name="String" full-name="System.String" name="A"></A>
        '<B></B>
        '<C ... enumerable="true">
        '    <item-of></item-of>
        '    <item-of></item-of>
        '</C>
        '<D>
        '    <item-of>
        '        <count></count>
        '        <item-of></item-of>
        '        <item-of></item-of>
        '    </item-of>
        '    <item-of>
        '        <count></count>
        '        <item-of></item-of>
        '        <item-of></item-of>
        '    </item-of>
        '</D>
        '<E>
        '    <count></count>
        '    <item-of></item-of>
        '    <item-of></item-of>
        '</E>

        'Attributes type-name, full-name, name, [enumerable]

#Region "Steps"
        ''' <summary>Common base for step class. Represents one step (level) in pseudo-XML structure exposed by <see cref="XPathObjectNavigator"/></summary>
        ''' <remarks>You should not create own derived classes from <see cref="[Step]"/> unless you are going to create own <see cref="XPathObjectNavigator"/>-derived class</remarks>
        <DebuggerDisplay("{ToString}")> _
        Protected MustInherit Class [Step] : Implements ICloneable(Of [Step])
            ''' <summary>Reprecents object associated with this step</summary>
            ''' <remarks><list type="table"><listheader><term>Step type</term><description>Content of this field</description></listheader>
            ''' <item><term><see cref="RootStep"/></term><description>Object value for this step</description></item>
            ''' <item><term><see cref="PropertyStep"/></term><description>Object on which the property getter will be invoked</description></item>
            ''' <item><term><see cref="EnumerableStep"/></term><description><see cref="IEnumerable"/> on which the <see cref="System.Collections.Generic.IEnumerable.GetEnumerator">GetEnumerator</see> will be invoked in order to get <see cref="IEnumerator"/></description></item>
            ''' <item><term><see cref="SpecialStep"/></term><description>Object which's pseudo-propertties will be get</description></item>
            ''' <item><term><see cref="SelfStep"/></term><description>Object which's value will be returned</description></item>
            ''' </list></remarks>
            Public ReadOnly [Object] As Object
            ''' <summary>Helper enumeration that allows quicker identification of steps. Contains one value of each class inherited from <see cref="[Step]"/></summary>
            Public Enum StepClasses
                ''' <summary><see cref="RootStep"/></summary>
                Root
                ''' <summary><see cref="PropertyStep"/></summary>
                [Property]
                ''' <summary><see cref="EnumerableStep"/></summary>
                Enumerable
                ''' <summary><see cref="SpecialStep"/></summary>
                Special
                ''' <summary><see cref="SelfStep"/></summary>
                Self
            End Enum
            ''' <summary>Returns one of <see cref="StepClasses"/> values according to type of current step</summary>
            Public MustOverride ReadOnly Property StepClass() As StepClasses
            ''' <summary>CTor</summary>
            ''' <param name="Object">Value for the <see cref="[Object]"/> field</param>
            <DebuggerStepThrough()> _
            Public Sub New(ByVal [Object] As Object)
                Me.Object = [Object]
            End Sub
            ''' <summary>Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="[Step]"/>.</summary>
            ''' <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="[Step]"/>.</param>
            ''' <returns>true if the specified <see cref="System.Object"/> is equal to the current <see cref="[Step]"/>; otherwise, false. This function always returns false when type of <paramref name="obj"/> is not <see cref="[Step]"/> and is not same as type of current instance.</returns>
            ''' <remarks>This function cannot be overriden. Override overloaded function instead.</remarks>
            Public Overrides Function Equals(ByVal obj As Object) As Boolean
                If TypeOf obj Is [Step] Then
                    If Me.GetType.Equals(obj.GetType) Then
                        Return Me.Equals(DirectCast(obj, [Step]))
                    Else
                        Return False
                    End If
                End If
                Return MyBase.Equals(obj)
            End Function
            ''' <summary>Determines whether the specified <see cref="[Step]"/> is equal to the current <see cref="[Step]"/>.</summary>
            ''' <param name="other">The <see cref="[Step]"/> to compare with the current <see cref="[Step]"/>.</param>
            ''' <returns>true if the specified <see cref="[Step]"/> is equal to the current <see cref="[Step]"/>. This function should always return false if the current <see cref="[Step]"/> is not of the same type as <paramref name="other"/></returns>
            Public MustOverride Overloads Function Equals(ByVal other As [Step]) As Boolean
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public MustOverride Function Clone() As [Step] Implements ICloneable(Of [Step]).Clone
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            ''' <remarks>Use type-safe <see cref="Clone"/> instead</remarks>
            <Obsolete("Use type-safe Clone instead"), EditorBrowsable(EditorBrowsableState.Never)> _
            Private Function Clone1() As Object Implements System.ICloneable.Clone
                Return Me.Clone
            End Function
            ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="[Step]"/>.</summary>
            ''' <returns>A <see cref="System.String"/> that represents the current <see cref="[Step]"/></returns>
            Public Overrides Function ToString() As String
                Return String.Format("Object ""{0}"" type {1}", Me.Object, Me.Object.GetType.Name)
            End Function
        End Class
        ''' <summary>Represents root step of pesudo-XML structure. This step can occure only as first step of sequence.</summary>
        Protected NotInheritable Class RootStep : Inherits [Step]
            ''' <summary>CTor</summary>
            ''' <param name="Object">Context object fro new instance</param>
            Public Sub New(ByVal [Object] As Object)
                MyBase.New([Object])
            End Sub
            ''' <summary>Determines whether the specified <see cref="[Step]"/> is equal to the current <see cref="[RootStep]"/>.</summary>
            ''' <param name="other">The <see cref="[Step]"/> to compare with the current <see cref="[RootStep]"/>.</param>
            ''' <returns>true if the specified <see cref="[Step]"/> is <see cref="RootStep"/> and <see cref="[Step].[Object]"/>-s of both <see cref="[Step]">Steps</see> are the same instance.</returns>
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is RootStep AndAlso Me.Object Is other.Object
            End Function
            ''' <summary>Type of this instance</summary>
            ''' <returns><see cref="StepClasses.Root"/></returns>
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                <DebuggerStepThrough()> Get
                    Return StepClasses.Root
                End Get
            End Property
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public Overrides Function Clone() As [Step]
                Return New RootStep(Me.Object)
            End Function
        End Class
        ''' <summary>Represents step that represents property of an object</summary>
        Protected NotInheritable Class PropertyStep : Inherits [Step]
            ''' <summary>Property represented by this step</summary>
            Public ReadOnly [Property] As PropertyInfo
            ''' <summary>CTor</summary>
            ''' <param name="Object">Object the property is invoked on</param>
            ''' <param name="Property">Identification of property to be represented by a new instance</param>
            <DebuggerStepThrough()> _
            Public Sub New(ByVal [Object] As Object, ByVal [Property] As PropertyInfo)
                MyBase.New([Object])
                Me.Property = [Property]
            End Sub
            ''' <summary>Determines whether the specified <see cref="[Step]"/> is equal to the current <see cref="[RootStep]"/>.</summary>
            ''' <param name="other">The <see cref="[Step]"/> to compare with the current <see cref="[RootStep]"/>.</param>
            ''' <returns>true if type of <paramref name="other"/> is <see cref="PropertyStep"/> and both, current an specified, <see cref="[Step]">Steps</see> has same value of the <see cref="[Object]">Object</see> (reference equals) and <see cref="[Property]">Property</see> (same <see cref="PropertyInfo.Name"/>) fields.</returns>
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is PropertyStep AndAlso Me.Object Is other.Object AndAlso Me.Property.Name = DirectCast(other, PropertyStep).Property.Name
            End Function
            ''' <summary>Type of this instance</summary>
            ''' <returns><see cref="StepClasses.[Property]"/></returns>
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Property
                End Get
            End Property
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public Overrides Function Clone() As [Step]
                Return New PropertyStep(Me.Object, Me.Property)
            End Function
            ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="[PropertyStep]"/>.</summary>
            ''' <returns>A <see cref="System.String"/> that represents the current <see cref="[PropertyStep]"/></returns>
            Public Overrides Function ToString() As String
                Return MyBase.ToString() & String.Format(" Property {0}", Me.Property.Name)
            End Function
        End Class
        ''' <summary>Represents step that points to item of <see cref="IEnumerable"/></summary>
        Protected NotInheritable Class EnumerableStep : Inherits [Step]
            ''' <summary>Position of pointed object in <see cref="IEnumerable"/></summary>
            ''' <remarks>This field must be kept in sinc with real position of <see cref="Enumerator"/> manually! Do not change it if you haven't (or are not going to) move <see cref="Enumerator"/> into the same position as <see cref="Index"/> points to</remarks>
            Public Index As Integer
            ''' <summary>Contains value of the <see cref="Enumerator"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Enumerator As IEnumerator
            ''' <summary><see cref="IEnumerator"/> that iterrates through <see cref="IEnumerable"/> contained in <see cref="[Object]"/></summary>
            ''' <remarks>If you use <see cref="Enumerator">Enumerator</see>.<see cref="System.Collections.Generic.IEnumerator.MoveNext">MoveNext</see> (or <see cref="System.Collections.Generic.IEnumerator.Reset">Reset</see>) set <see cref="Index"/> to actual position!</remarks>
            Public ReadOnly Property Enumerator() As IEnumerator
                Get
                    Return _Enumerator
                End Get
            End Property
            ''' <summary>CTor</summary>
            ''' <param name="Object">Instance of <see cref="IEnumerable"/> to iterrate through</param>
            ''' <param name="index">Index to move <see cref="Enumerator"/> initially to</param>
            ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero - or <paramref name="index"/> points to position which exceeds number of items in <paramref name="Object"/></exception>
            Public Sub New(ByVal [Object] As IEnumerable, Optional ByVal [index] As Integer = 0)
                MyBase.New([Object])
                Me.Index = index
                If index < 0 Then Throw New ArgumentOutOfRangeException("index", "index mus be greater than or equal to zero")
                _Enumerator = Me.Object.GetEnumerator
                For i As Integer = 0 To Me.Index
                    If Not Enumerator.MoveNext() Then Throw New ArgumentOutOfRangeException("index", "There are not enought items in IEnumerable")
                Next i
            End Sub
            ''' <summary>Shadows <see cref="[Step].[Object]"/> by returning it casted to <see cref="IEnumerable"/></summary>
            Private Shadows ReadOnly Property [Object]() As IEnumerable
                Get
                    Return MyBase.Object
                End Get
            End Property
            ''' <summary>Determines whether the specified <see cref="[Step]"/> is equal to the current <see cref="[EnumerableStep]"/>.</summary>
            ''' <param name="other">The <see cref="[Step]"/> to compare with the current <see cref="[EnumerableStep]"/>.</param>
            ''' <returns>true if type of <paramref name="other"/> is <see cref="EnumerableStep"/>, both (current and specified) <see cref="[Step]">Steps</see> points to the same object (reference equals) and has same <see cref="Index"/></returns>
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is EnumerableStep AndAlso Me.Object Is other.Object AndAlso Me.Index = DirectCast(other, EnumerableStep).Index
            End Function
            ''' <summary>Type of this instance</summary>
            ''' <returns><see cref="StepClasses.Enumerable"/></returns>
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Enumerable
                End Get
            End Property
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public Overrides Function Clone() As [Step]
                Return New EnumerableStep(Me.Object, Me.Index)
            End Function
            ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="[EnumerableStep]"/>.</summary>
            ''' <returns>A <see cref="System.String"/> that represents the current <see cref="[EnumerableStep]"/></returns>
            Public Overrides Function ToString() As String
                Return MyBase.ToString() & String.Format(" index {0}", Index)
            End Function
        End Class
        ''' <summary>Step that points to pseudo-CData content of pseudo-node</summary>
        Protected NotInheritable Class SelfStep : Inherits [Step]
            ''' <summary>CTor</summary>
            ''' <param name="Object">Object to point to. This object should be of supported type. See <see cref="Value"/> for list of supported types for pseudo-text pseudo-nodes.</param>
            Public Sub New(ByVal [Object] As Object)
                MyBase.New([Object])
            End Sub
            ''' <summary>Determines whether the specified <see cref="[Step]"/> is equal to the current <see cref="[SelfStep]"/>.</summary>
            ''' <param name="other">The <see cref="[Step]"/> to compare with the current <see cref="[SelfStep]"/>.</param>
            ''' <returns>true if type of <paramref name="other"/> is <see cref="SelfStep"/> and <see cref="[Step].[Object]"/> of both (current and specified) <see cref="[Step]">Steps</see> equals (is same instance)</returns>
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is SelfStep AndAlso Me.Object Is other.Object
            End Function
            ''' <summary>Type of thsi step</summary>
            ''' <returns><see cref="StepClasses.Self"/></returns>
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Self
                End Get
            End Property
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public Overrides Function Clone() As [Step]
                Return New SelfStep(Me.Object)
            End Function
        End Class
        ''' <summary>Represents special step that points to especially supported property of object</summary>
        Protected NotInheritable Class SpecialStep : Inherits [Step]
            ''' <summary>Type of especially supported properties</summary>
            Public Enum StepType
                ''' <summary>Short name of type of context object (<seealso cref="Type.Name"/>)</summary>
                TypeName
                ''' <summary>Full name of type of context object (<see cref="Type.FullName"/>)</summary>
                FullName
                ''' <summary>Name of property, current stape will be obtained throught. Supported also for root but returns <see cref="String.Empty"/></summary>
                Name
                ''' <summary>Contains true for enumerable objects, otherwise is not present</summary>
                Enumerable
            End Enum
            ''' <summary>Sub-type of this step</summary>
            Public Type As StepType
            ''' <summary>CTor</summary>
            ''' <param name="Object">Object to get information from</param>
            ''' <param name="Type">Type of information to be got</param>
            Friend Sub New(ByVal [Object] As Object, ByVal Type As StepType)
                MyBase.New([Object])
                Me.Type = Type
            End Sub
            ''' <summary>Determines whether the specified <see cref="[Step]"/> is equal to the current <see cref="[SelfStep]"/>.</summary>
            ''' <param name="other">The <see cref="[Step]"/> to compare with the current <see cref="[SelfStep]"/>.</param>
            ''' <returns>True if <paramref name="other"/> is <see cref="SpecialStep"/> and both (current and specified) <see cref="[Step]">Steps</see> have same instance in their <see cref="[Object]"/> field and are of same sub-type (see also <seealso cref="Type"/>)</returns>
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is SpecialStep AndAlso other.Object Is Me.Object AndAlso Me.Type = DirectCast(other, SpecialStep).Type
            End Function
            ''' <summary>Type of this instance</summary>
            ''' <returns><see cref="StepClasses.Special"/></returns>
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Special
                End Get
            End Property
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public Overrides Function Clone() As [Step]
                Return New SpecialStep(Me.Object, Me.Type)
            End Function
            ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="[SpecialStep]"/>.</summary>
            ''' <returns>A <see cref="System.String"/> that represents the current <see cref="[SpecialStep]"/></returns>
            Public Overrides Function ToString() As String
                Return MyBase.ToString() & String.Format(" type {0}", Type)
            End Function
        End Class
#End Region
        Private Location As New List(Of [Step])
        Private Sub New()
            _NameTable.Add(String.Empty)
        End Sub
        Public Sub New(ByVal [Object] As Object)
            Me.New()
            Location.Add(New RootStep([Object]))
        End Sub
        Private Sub New(ByVal Other As XPathObjectNavigator)
            Me.New()
            Me.Location = New List(Of [Step])(Other.CloneLocation)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides ReadOnly Property BaseURI() As String
            Get
                Return String.Empty
            End Get
        End Property

        Public Overrides Function Clone() As System.Xml.XPath.XPathNavigator
            Return New XPathObjectNavigator(Me)
        End Function

        Public Overrides ReadOnly Property IsEmptyElement() As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function IsSamePosition(ByVal other As System.Xml.XPath.XPathNavigator) As Boolean
            If TypeOf other Is XPathObjectNavigator Then
                With DirectCast(other, XPathObjectNavigator)
                    If .Location.Count = Me.Location.Count Then
                        For i As Integer = 0 To Location.Count
                            If Not Location(i).Equals(.Location(i)) Then Return False
                        Next i
                        Return True
                    End If
                End With
            End If
            Return False
        End Function

        Public Overrides ReadOnly Property LocalName() As String
            Get
                Select Case Location(Location.Count - 1).StepClass
                    Case [Step].StepClasses.Enumerable : Return "item-of"
                    Case [Step].StepClasses.Property : Return DirectCast(Location(Location.Count - 1), PropertyStep).Property.Name
                    Case [Step].StepClasses.Special
                        Select Case DirectCast(Location(Location.Count - 1), SpecialStep).Type
                            Case SpecialStep.StepType.Enumerable : Return "enumerable"
                            Case SpecialStep.StepType.FullName : Return "full-name"
                            Case SpecialStep.StepType.Name : Return "name"
                            Case SpecialStep.StepType.TypeName : Return "type-name"
                            Case Else : Return ""
                        End Select
                    Case Else : Return ""
                End Select
            End Get
        End Property

        Private Function CloneLocation() As List(Of [Step])
            Dim NewLoc As New List(Of [Step])(Me.Location.Count)
            For Each item As [Step] In Me.Location
                NewLoc.Add(item.Clone)
            Next item
            Return NewLoc
        End Function

        Public Overrides Function MoveTo(ByVal other As System.Xml.XPath.XPathNavigator) As Boolean
            If TypeOf other Is XPathObjectNavigator Then
                Me.Location = DirectCast(other, XPathObjectNavigator).CloneLocation
                Return True
            End If
            Return False
        End Function

        Private ReadOnly Property ContextObject() As Object
            Get
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Enumerable
                        Return DirectCast(CurrentStep, EnumerableStep).Enumerator.Current
                    Case [Step].StepClasses.Property
                        Return DirectCast(CurrentStep, PropertyStep).Property.GetValue(CurrentStep.Object, Nothing)
                    Case Else
                        Return CurrentStep.Object
                End Select
            End Get
        End Property

        Public Overrides Function MoveToFirstAttribute() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable, [Step].StepClasses.Property, [Step].StepClasses.Root
                    Location.Add(New SpecialStep(ContextObject, SpecialStep.StepType.TypeName))
                    Return True
            End Select
            Return False
        End Function

        Private Property CurrentStep() As [Step]
            <DebuggerStepThrough()> _
            Get
                Return Location(Location.Count - 1)
            End Get
            <DebuggerStepThrough()> _
            Set(ByVal value As [Step])
                Location(Location.Count - 1) = value
            End Set
        End Property

        Private Function GetFirstProperty(Optional ByVal Obj As Object = Nothing, Optional ByVal After As PropertyInfo = Nothing, Optional ByVal Reverse As Boolean = False) As PropertyInfo
            If Obj Is Nothing Then Obj = CurrentStep.Object
            Dim IsAfter As Boolean = After Is Nothing
            Dim Properties As PropertyInfo() = Obj.GetType.GetProperties(BindingFlags.GetProperty Or BindingFlags.Instance Or BindingFlags.Public)
            Dim From As Integer
            If Reverse Then From = Properties.Length - 1 Else From = 0
            Dim [To] As Integer
            If Reverse Then [To] = 0 Else [To] = Properties.Length - 1
            Dim [Step] As SByte
            If Reverse Then [Step] = -1 Else [Step] = 1
            For i As Integer = From To [To] Step [Step]
                Dim GoodMethod As Boolean = True
                If Properties(i).CanRead Then
                    Dim params As ParameterInfo() = Properties(i).GetGetMethod.GetParameters
                    If params IsNot Nothing Then
                        For Each param As ParameterInfo In params
                            If Not param.IsOptional Then GoodMethod = False : Exit For
                        Next
                    End If
                Else
                    GoodMethod = False
                End If
                If GoodMethod Then
                    If IsAfter Then Return Properties(i) Else IsAfter = Properties(i).Name = After.Name
                End If
            Next i
            Return Nothing
        End Function

        Private ReadOnly Property CurrentEnumerator() As IEnumerator
            Get
                If CurrentStep.StepClass = [Step].StepClasses.Enumerable Then
                    Return DirectCast(CurrentStep, EnumerableStep).Enumerator
                End If
                Throw New InvalidOperationException("CurrentEnumerator is valid only when location is EnumeratorStep")
            End Get
        End Property

        Private Function MoveToFirstPropertyOrItem(Optional ByVal After As PropertyInfo = Nothing) As Boolean
            Dim fp As PropertyInfo
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable
                    fp = GetFirstProperty(CurrentEnumerator.Current, After)
                Case [Step].StepClasses.Root, [Step].StepClasses.Property
                    fp = GetFirstProperty(, After)
                Case Else
                    Return False
            End Select
            If fp IsNot Nothing Then
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Enumerable
                        Location.Add(New PropertyStep(fp.GetValue(Me.CurrentEnumerator.Current, Nothing), fp))
                    Case Else : Location.Add(New PropertyStep(Me.CurrentStep.Object, fp))
                End Select
                Return True
            End If
            Dim CurrObj As Object
            If CurrentStep.StepClass = [Step].StepClasses.Enumerable Then
                CurrObj = CurrentEnumerator.Current
            Else
                CurrObj = CurrentStep.Object
            End If
            If TypeOf CurrObj Is IEnumerable Then
                Dim E As IEnumerator = DirectCast(CurrObj, IEnumerable).GetEnumerator
                If E.MoveNext Then
                    Location.Add(New EnumerableStep(CurrObj, 0))
                    Return True
                End If
            End If
            Return False
        End Function


        Public Overrides Function MoveToFirstChild() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable, [Step].StepClasses.Root, [Step].StepClasses.Property
                    Dim obj As Object
                    Select Case CurrentStep.StepClass
                        Case [Step].StepClasses.Enumerable
                            obj = CurrentEnumerator.Current
                        Case [Step].StepClasses.Property
                            obj = DirectCast(CurrentStep, PropertyStep).Property.GetValue(CurrentStep.Object, Nothing)
                        Case Else
                            obj = CurrentStep.Object
                    End Select
                    If TypeOf obj Is String OrElse TypeOf obj Is Char OrElse TypeOf obj Is Byte OrElse TypeOf obj Is SByte OrElse TypeOf obj Is Short OrElse TypeOf obj Is UShort OrElse TypeOf obj Is Long OrElse TypeOf obj Is ULong OrElse TypeOf obj Is Integer OrElse TypeOf obj Is UInt16 OrElse TypeOf obj Is Single OrElse TypeOf obj Is Double OrElse TypeOf obj Is Decimal OrElse TypeOf obj Is Date OrElse TypeOf obj Is Boolean OrElse TypeOf obj Is TimeSpan OrElse TypeOf obj Is Tools.TimeSpanFormattable OrElse TypeOf obj Is Uri OrElse TypeOf obj Is System.Text.StringBuilder Then
                        Location.Add(New SelfStep(CurrentStep.Object))
                        Return True
                    End If
                    Return MoveToFirstPropertyOrItem()
                Case Else : Return False
            End Select
        End Function

        Public Overloads Overrides Function MoveToFirstNamespace(ByVal namespaceScope As System.Xml.XPath.XPathNamespaceScope) As Boolean
            Return False
        End Function

        Public Overrides Function MoveToId(ByVal id As String) As Boolean
            Return False
        End Function

        Public Overloads Overrides Function MoveToNext() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable
                    If CurrentEnumerator.MoveNext Then
                        DirectCast(CurrentStep, EnumerableStep).Index += 1
                        Return True
                    End If
                    Return False
                Case [Step].StepClasses.Property
                    Return MoveToFirstPropertyOrItem(DirectCast(CurrentStep, PropertyStep).Property)
                Case Else : Return False
            End Select
        End Function

        Public Overrides Function MoveToNextAttribute() As Boolean
            If CurrentStep.StepClass = [Step].StepClasses.Special Then
                Select Case DirectCast(CurrentStep, SpecialStep).Type
                    Case SpecialStep.StepType.TypeName
                        CurrentStep = New SpecialStep(CurrentStep.Object, SpecialStep.StepType.FullName)
                        Return True
                    Case SpecialStep.StepType.FullName
                        CurrentStep = New SpecialStep(CurrentStep.Object, SpecialStep.StepType.Name)
                        Return True
                    Case SpecialStep.StepType.Name
                        If Location(Location.Count - 2).StepClass = [Step].StepClasses.Enumerable Then
                            CurrentStep = New SpecialStep(CurrentStep.Object, SpecialStep.StepType.Enumerable)
                            Return True
                        Else
                            Return False
                        End If
                    Case Else : Return False
                End Select
            End If
            Return False
        End Function

        Public Overloads Overrides Function MoveToNextNamespace(ByVal namespaceScope As System.Xml.XPath.XPathNamespaceScope) As Boolean
            Return False
        End Function

        Public Overrides Function MoveToParent() As Boolean
            If Location.Count > 1 Then
                Location.RemoveAt(Location.Count - 1)
                Return False
            End If
            Return True
        End Function

        Public Overrides Function MoveToPrevious() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable
                    With DirectCast(CurrentStep, EnumerableStep)
                        If .Index > 0 Then
                            CurrentStep = New EnumerableStep(CurrentStep.Object, .Index - 1)
                            Return True
                        End If
                        Dim prp As PropertyInfo = GetFirstProperty(Reverse:=True)
                        If prp IsNot Nothing Then
                            CurrentStep = New PropertyStep(CurrentStep.Object, prp)
                            Return True
                        End If
                        Return False
                    End With
                Case [Step].StepClasses.Property
                    Dim prp As PropertyInfo = GetFirstProperty(, DirectCast(CurrentStep, PropertyStep).Property, True)
                    If prp IsNot Nothing Then
                        CurrentStep = New PropertyStep(CurrentStep.Object, prp)
                        Return True
                    End If
                    Return False
                Case Else : Return False
            End Select
        End Function

        Public Overrides ReadOnly Property Name() As String
            Get
                Return LocalName
            End Get
        End Property

        Public Overrides ReadOnly Property NamespaceURI() As String
            Get
                Return NameTable.Get(String.Empty)
            End Get
        End Property
        Private _NameTable As New NameTable
        Public Overrides ReadOnly Property NameTable() As System.Xml.XmlNameTable
            Get
                Return _NameTable
            End Get
        End Property

        Public Overrides ReadOnly Property NodeType() As System.Xml.XPath.XPathNodeType
            Get
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Root : Return XPathNodeType.Root
                    Case [Step].StepClasses.Enumerable, [Step].StepClasses.Property
                        Return XPathNodeType.Element
                    Case [Step].StepClasses.Special : Return XPathNodeType.Attribute
                    Case [Step].StepClasses.Self : Return XPathNodeType.Text
                End Select
            End Get
        End Property

        Public Overrides ReadOnly Property Prefix() As String
            Get
                Return NameTable.Get(String.Empty)
            End Get
        End Property

        Public Overrides ReadOnly Property Value() As String
            Get
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Special
                        Select Case DirectCast(CurrentStep, SpecialStep).Type
                            Case SpecialStep.StepType.Enumerable : Return Tools.VisualBasicT.iif(TypeOf CurrentStep.Object Is IEnumerable, "true", "false")
                            Case SpecialStep.StepType.FullName : Return CurrentStep.Object.GetType.FullName
                            Case SpecialStep.StepType.TypeName : Return CurrentStep.Object.GetType.Name
                            Case SpecialStep.StepType.Name
                                If Location(Location.Count - 2).StepClass = [Step].StepClasses.Property Then
                                    Return DirectCast(Location(Location.Count - 2), PropertyStep).Property.Name
                                ElseIf Location(Location.Count - 2).StepClass = [Step].StepClasses.Enumerable Then
                                    Return "GetEnumerator"
                                Else
                                    Return ""
                                End If
                        End Select
                    Case Else
                        Dim Obj As Object
                        If CurrentStep.StepClass = [Step].StepClasses.Property Then
                            Obj = Location(Location.Count - 2).Object
                        Else
                            Obj = CurrentStep.Object
                        End If
                        If TypeOf Obj Is Date Then
                            Dim fff As String = Tools.VisualBasicT.iif(DirectCast(Obj, Date).Millisecond = 0, "", ".fff")
                            Return DirectCast(Obj, Date).ToString("yyyy-MM-DDHH:mm:ss" & fff, System.Globalization.CultureInfo.InvariantCulture)
                        ElseIf TypeOf Obj Is TimeSpan Then
                            Dim lll As String = Tools.VisualBasicT.iif(DirectCast(Obj, TimeSpan).Milliseconds = 0, "", ".lll")
                            Return CType(DirectCast(Obj, TimeSpan), Tools.TimeSpanFormattable).ToString("h(0):mm:ss" & lll, System.Globalization.CultureInfo.InvariantCulture)
                        ElseIf TypeOf Obj Is Tools.TimeSpanFormattable Then
                            Dim lll As String = Tools.VisualBasicT.iif(DirectCast(Obj, Tools.TimeSpanFormattable).Milliseconds = 0, "", ".lll")
                            Return DirectCast(Obj, Tools.TimeSpanFormattable).ToString("h(0):mm:ss" & lll, System.Globalization.CultureInfo.InvariantCulture)
                        ElseIf TypeOf Obj Is Boolean Then
                            Return Tools.VisualBasicT.iif(Obj, "true", "false")
                        ElseIf TypeOf Obj Is IFormattable Then
                            Return DirectCast(Obj, IFormattable).ToString("", System.Globalization.CultureInfo.InvariantCulture)
                        Else
                            Return Obj.ToString
                        End If
                End Select
                Return ""
            End Get
        End Property
    End Class
End Namespace
#End If