Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath
Imports System.Reflection
Imports System.ComponentModel

#If Config <= Alpha Then
Namespace XmlT.XPathT
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
    ''' <item><term>enumerable</term><description>If context object of current node is <see cref="IEnumerable"/> and it is not of <see cref="Tools.XmlT.XPathT.XPathObjectNavigator.IsSupportedType">supported type</see> contains true; otherwise it is not present.</description></item>
    ''' <item><term>circle-level</term><description>If same (reference equal) object as context object of current node is context object of node somewhere at parent axis of current node this pseudo-attribute contains number of levels upward (on parent axis) to such (at level closest to root) object; otherwise this pseudo-attribute is not present.</description></item>
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
    ''' <para>Definitions:</para>
    ''' <list>
    ''' <item><term>Current object</term><description>Object contained in <see cref="F:Tools.XmlT.XPathT.XPathObjectNavigator.[Step].[Object]"/> field of current step</description></item>
    ''' <item><term>Current step</term><description>Last item of <see cref="XPathObjectNavigator.Location"/> collection</description></item>
    ''' <item><term>Context object</term><description>Object produced or used by current step. Value depends on type of step
    ''' <list><listheader><term>Type of step</term><description>Context object</description></listheader>
    ''' <item><term><see cref="Tools.XmlT.XPathT.XPathObjectNavigator.PropertyStep"/></term><description>Object returned by getter of current property</description></item>
    ''' <item><term><see cref="Tools.XmlT.XPathT.XPathObjectNavigator.EnumerableStep"/></term><description>Object returned by enumerator at its current position</description></item>
    ''' <item><term>Any other defined in <see cref="XPathObjectNavigator"/></term><description>Same as current object</description></item>
    ''' </list>
    ''' </description></item>
    ''' <item><term>Parent step of another step</term><description>Step that lies on preceding index of <see cref="Tools.XmlT.XPathT.XPathObjectNavigator.Location"/> collection then step which's parent it is.</description></item>
    ''' <item><term>Current property</term><description>For property steps (<seealso cref="Tools.XmlT.XPathT.XPathObjectNavigator.PropertyStep"/>) it is <see cref="F:Tools.XmlT.XPathT.XPathObjectNavigator.PropertyStep.Property"/> otherwise it is null</description></item>
    ''' <item><term>Current enumerator</term><description>For enumerable steps (<seealso cref="Tools.XmlT.XPathT.XPathObjectNavigator.EnumerableStep"/>) it is <see cref="P:Tools.XmlT.XPathT.XPathObjectNavigator.EnumerableStep.Enumerator"/> otherwise it is null</description></item>
    ''' <item><term>Context value</term><description>Same as context object with exception when current step is <see cref="Tools.XmlT.XPathT.XPathObjectNavigator.SpecialStep"/> (then it is value of pseudo-attribute)</description></item>
    ''' </list>
    ''' </remarks>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(XPathObjectNavigator), LastChMMDDYYYY:="09/17/2007")> _
    <StandAloneTool(FirstVerMMDDYYYY:="09/17/2007")> _
    Public Class XPathObjectNavigator : Inherits XPathNavigator : Implements ICloneable(Of XPathNavigator)
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
                ''' <summary>Contains number of steps upward (in parent axis) needed to reach same context object as is actual <see cref="ContextObject"/>. Present only if non-zero.</summary>
                CircleLevel
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
        ''' <summary>Contains value of the <see cref="Location"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Location As New List(Of [Step])
        ''' <summary>Sequence of steps alwas starting with <see cref="RootStep"/> and always ahving at least 1 item. This sequence determines current position of <see cref="XPathObjectNavigator"/>.</summary>
        ''' <returns>Steps in child or attribute axes from root needed to reproduce navigation</returns>
        ''' <remarks>Avoind clearing this collection and puting anything other than <see cref="RootStep"/> at first index. Consider carefully changing value of items.</remarks>
        Protected ReadOnly Property Location() As List(Of [Step])
            Get
                Return _Location
            End Get
        End Property
        Protected Const ns$ = ""
        Protected Const atrName$ = "name"
        Protected Const atrTypeName$ = "type-name"
        Protected Const atrFullName$ = "full-name"
        Protected Const atrEnumerable$ = "enumerable"
        Protected Const atrCircleLevel$ = "circle-level"
        Protected Const nodItemOf$ = "item-of"
        ''' <summary>Private CTor dhat does common construction steps</summary>
        ''' <param name="AllowCircles">Valus for the <see cref="AllowCircles"/> property</param>
        Private Sub New(Optional ByVal AllowCircles As Boolean = False)
            _AllowCircles = AllowCircles
            NameTable.Add(ns)
            NameTable.Add(atrName)
            NameTable.Add(atrTypeName)
            NameTable.Add(atrFullName)
            NameTable.Add(atrEnumerable)
            NameTable.Add(atrCircleLevel)
            NameTable.Add(nodItemOf)
        End Sub
        ''' <summary>CTor from any <see cref="Object"/></summary>
        ''' <param name="Object">Root for new instance</param>
        ''' <param name="AllowCircles">Indicates if newly created instance will support infinite-depth trees (circle references). See <seealso cref="AllowCircles"/> for more details.</param>
        Public Sub New(ByVal [Object] As Object, Optional ByVal AllowCircles As Boolean = False)
            Me.New(AllowCircles)
            Location.Add(New RootStep([Object]))
        End Sub
        ''' <summary>Copy CTor</summary>
        ''' <param name="Other">Instance which's location new insatnce will point to</param>
        Private Sub New(ByVal Other As XPathObjectNavigator)
            Me.New(Other.AllowCircles)
            Me._Location = New List(Of [Step])(Other.CloneLocation)
        End Sub
        ''' <summary>When overridden in a derived class, gets the base URI for the current node.</summary>
        ''' <returns>For <see cref="XPathObjectNavigator"/> this property always returns <see cref="String.Empty"/></returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides ReadOnly Property BaseURI() As String
            Get
                Return String.Empty
            End Get
        End Property
        ''' <summary>When overridden in a derived class, creates a new <see cref="XPathObjectNavigator"/> positioned at the same node as this <see cref="XPathObjectNavigator"/>.</summary>
        ''' <returns>A new <see cref="XPathObjectNavigator"></see> positioned at the same node as this <see cref="XPathObjectNavigator"/>.</returns>
        Public Overrides Function Clone() As System.Xml.XPath.XPathNavigator Implements ICloneable(Of System.Xml.XPath.XPathNavigator).Clone
            Return New XPathObjectNavigator(Me)
        End Function
        ''' <summary>Gets a value indicating whether the current node is an empty element without an end element tag.</summary>
        ''' <returns>True if context object has no public readable properties without mandatory arguments and it is not <see cref="IEnumerable"/> or it is <see cref="IEnumerable"/> but with no items inside.</returns>
        Public Overrides ReadOnly Property IsEmptyElement() As Boolean
            Get
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Enumerable, [Step].StepClasses.Root, [Step].StepClasses.Property
                        Dim clone As XPathObjectNavigator = Me.Clone
                        Return clone.MoveToFirstChild
                    Case Else : Return False
                End Select
            End Get
        End Property
        ''' <summary>Determines whether the current <see cref="T:System.Xml.XPath.XPathNavigator"></see> is at the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator"></see>.</summary>
        ''' <returns>Returns true if <paramref name="other"/> is <see cref="XPathObjectNavigator"/>, has same count of items in its <see cref="Location"/> as current <see cref="XPathObjectNavigator"/> and all those steps equals to steps in current <see cref="XPathObjectNavigator"/></returns>
        ''' <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator"></see> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator"/>.</param>
        Public Overrides Function IsSamePosition(ByVal other As System.Xml.XPath.XPathNavigator) As Boolean
            If TypeOf other Is XPathObjectNavigator Then
                With DirectCast(other, XPathObjectNavigator)
                    If .Location.Count = Me.Location.Count Then
                        For i As Integer = 0 To Location.Count - 1
                            If Not Location(i).Equals(.Location(i)) Then Return False
                        Next i
                        Return True
                    End If
                End With
            End If
            Return False
        End Function
        ''' <summary>Gets the <see cref="P:System.Xml.XPath.XPathNavigator.Name"></see> of the current node without any namespace prefix.</summary>
        ''' <returns>A <see cref="T:System.String"></see> that contains the local name of the current node, or <see cref="F:System.String.Empty"></see> if the current node does not have a name (for example, text or comment nodes).</returns>
        ''' <remarks><list><listheader><term>Current step</term><description>Returned value</description></listheader>
        ''' <item><term><see cref="EnumerableStep"/></term><description>"item-of"</description></item>
        ''' <item><term><see cref="PropertyStep"/></term><description><see cref="PropertyInfo.Name"/> of current property</description></item>
        ''' <item><term><see cref="SpecialStep"/> <see cref="SpecialStep.Type">Type</see> = <see cref="SpecialStep.StepType.Enumerable"/></term><description>"enumerable"</description></item>
        ''' <item><term><see cref="SpecialStep"/> <see cref="SpecialStep.Type">Type</see> = <see cref="SpecialStep.StepType.FullName"/></term><description>"full-name"</description></item>
        ''' <item><term><see cref="SpecialStep"/> <see cref="SpecialStep.Type">Type</see> = <see cref="SpecialStep.StepType.Name"/></term><description>"name"</description></item>
        ''' <item><term><see cref="SpecialStep"/> <see cref="SpecialStep.Type">Type</see> = <see cref="SpecialStep.StepType.TypeName"/></term><description>"type-name"</description></item>
        ''' <item><term><see cref="SpecialStep"/> <see cref="SpecialStep.Type">Type</see> = <see cref="SpecialStep.StepType.CircleLevel"/></term><description>"circle-level"</description></item>
        ''' <item><term>Any other</term><description><see cref="String.empty"/></description></item>
        ''' </list>
        ''' <para>See <seealso cref="XPathObjectNavigator"/> for definition of some terms.</para>
        ''' </remarks>
        Public Overrides ReadOnly Property LocalName() As String
            Get
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Enumerable : Return NameTable.Get(nodItemOf)
                    Case [Step].StepClasses.Property : Return NameTable.Add(DirectCast(CurrentStep, PropertyStep).Property.Name)
                    Case [Step].StepClasses.Special
                        Select Case DirectCast(CurrentStep, SpecialStep).Type
                            Case SpecialStep.StepType.Enumerable : Return NameTable.Get(atrEnumerable)
                            Case SpecialStep.StepType.FullName : Return NameTable.Get(atrFullName)
                            Case SpecialStep.StepType.Name : Return NameTable.Get(atrName)
                            Case SpecialStep.StepType.TypeName : Return NameTable.Get(atrTypeName)
                            Case SpecialStep.StepType.CircleLevel : Return NameTable.Get(atrCircleLevel)
                        End Select
                End Select
                Return NameTable.Get(String.Empty)
            End Get
        End Property
        ''' <summary>Clones <see cref="Location"/> by clonig all steps in it</summary>
        ''' <returns>Indepemdent copy of actual <see cref="Location"/></returns>
        Protected Function CloneLocation() As List(Of [Step])
            Dim NewLoc As New List(Of [Step])(Me.Location.Count)
            For Each item As [Step] In Me.Location
                NewLoc.Add(item.Clone)
            Next item
            Return NewLoc
        End Function
        ''' <summary>Moves the <see cref="XPathObjectNavigator"/> to the same position as the specified <see cref="XPathObjectNavigator"/>.</summary>
        ''' <returns>Returns true if the <see cref="XPathObjectNavigator"/> is successful moving to the same position as the specified <see cref="XPathObjectNavigator"/>; otherwise, false. If false, the position of the <see cref="XPathObjectNavigator"/> is unchanged.</returns>
        ''' <param name="other">The <see cref="XPathNavigator"/> positioned on the node that you want to move to. </param>
        ''' <remarks>Changing position succeds only if <paramref name="other"/> is <see cref="XPathObjectNavigator"/></remarks>
        Public Overrides Function MoveTo(ByVal other As System.Xml.XPath.XPathNavigator) As Boolean
            If TypeOf other Is XPathObjectNavigator Then
                Me._Location = DirectCast(other, XPathObjectNavigator).CloneLocation
                Return True
            End If
            Return False
        End Function

        ''' <summary>When overridden in a derived class, moves the <see cref="XPathObjectNavigator"/> to the first attribute of the current node.</summary>
        ''' <returns>Returns true if the <see cref="XPathObjectNavigator"/> is successful moving to the first attribute of the current node; otherwise, false. If false, the position of the <see cref="XPathObjectNavigator"/> is unchanged.</returns>
        ''' <remarks>This method is succesfull if current step is <see cref="RootStep"/> or <see cref="PropertyStep"/> or <see cref="EnumerableStep"/>. Firts attribute is always <see cref="SpecialStep.StepType.TypeName"/> and context object becomes current object</remarks>
        Public Overrides Function MoveToFirstAttribute() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable, [Step].StepClasses.Property, [Step].StepClasses.Root
                    Location.Add(New SpecialStep(ContextObject, SpecialStep.StepType.TypeName))
                    Return True
            End Select
            Return False
        End Function
        ''' <summary>Finds first property of specified object lying after specified property in specified direction</summary>
        ''' <param name="Obj">Object to be sought for property. If ommited <see cref="ContextObject"/> is used.</param>
        ''' <param name="After">Property after which the search should start. If ommited first property is returned</param>
        ''' <param name="Reverse">If true the property is being searched from last to first instead of from first to last</param>
        ''' <returns>First usable property of spacified object laying after specified property in specified direction. Usable properties are public instance properties (and not <see cref="PropertyInfo.IsSpecialName"/>) with public get accessor which is callable without parameters. If no such property is found null is returned.</returns>
        Protected Function GetFirstProperty(Optional ByVal Obj As Object = Nothing, Optional ByVal After As PropertyInfo = Nothing, Optional ByVal Reverse As Boolean = False) As PropertyInfo
            If Obj Is Nothing Then Obj = ContextObject
            If Obj Is Nothing Then Return Nothing
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
        ''' <summary>Helper method for <see cref="MoveToFirstChild"/> and <see cref="MoveToNext"/> (when current step is <see cref="PropertyStep"/>). Moves <see cref="XPathObjectNavigator"/> to first (or next) property or first <see cref="IEnumerable"/> item.</summary>
        ''' <param name="After">Property after which start search. If ommited or null first property is used.</param>
        ''' <returns>True if moving was successfull</returns>
        ''' <remarks>This function uses <see cref="GetFirstProperty"/> to get first (or next) property in forward direction. If there is no such property and context object is <see cref="IEnumerable"/> which has at least one item the <see cref="XPathObjectNavigator"/> is muved to this first item.</remarks>
        ''' <param name="Obj">Object which's properties should be examined. If not specified or null the <see cref="ContextObject"/> is used.</param>
        ''' <param name="Replace">Determines if new step is appended after current (false) or replaces current (true)</param>
        Protected Overridable Function MoveToFirstPropertyOrItem(Optional ByVal After As PropertyInfo = Nothing, Optional ByVal Obj As Object = Nothing, Optional ByVal Replace As Boolean = False) As Boolean
            If Obj Is Nothing Then Obj = ContextObject
            Dim fp As PropertyInfo = GetFirstProperty(Obj:=Obj, After:=After)
            If fp IsNot Nothing Then
                Dim NewStep As XPathObjectNavigator.PropertyStep = New PropertyStep(Obj, fp)
                If Replace Then
                    CurrentStep = NewStep
                Else
                    Location.Add(NewStep)
                End If
                Return True
            End If
            If TypeOf Obj Is IEnumerable Then
                Dim E As IEnumerator = DirectCast(Obj, IEnumerable).GetEnumerator
                If E.MoveNext Then
                    Dim NewStep As XPathObjectNavigator.EnumerableStep = New EnumerableStep(Obj, 0)
                    If Replace Then
                        CurrentStep = NewStep
                    Else
                        Location.Add(NewStep)
                    End If
                    Return True
                End If
            End If
            Return False
        End Function

        ''' <summary>When overridden in a derived class, moves the <see cref="XPathObjectNavigator"></see> to the first child node of the current node.</summary>
        ''' <returns>Returns true if the <see cref="XPathObjectNavigator"></see> is successful moving to the first child node of the current node; otherwise, false. If false, the position of the <see cref="XPathObjectNavigator"></see> is unchanged.</returns>
        ''' <remarks>
        ''' <para>Only <see cref="RootStep"/>, <see cref="PropertyStep"/> and <see cref="EnumerableStep"/> can have children.
        ''' In order step-that-can-have-children to have children its context object must not be of supported type (see <seealso cref="IsSupportedType"/>) and it must at least one public instance property with public getter or it must be <see cref="IEnumerable"/> with at least one item to enumerate.</para>
        ''' <para>This function uses <see cref="MoveToFirstPropertyOrItem"/> for unsupported types.</para>
        ''' <para>If type of context object is supported then it has only text child taht contains value of supported type.</para>
        ''' <para>Is <see cref="IsCircleReferenced"/> returns true than this method retruns false if <see cref="AllowCircles"/> is set to false</para>
        ''' </remarks>
        Public Overrides Function MoveToFirstChild() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable, [Step].StepClasses.Root, [Step].StepClasses.Property
                    If Not AllowCircles AndAlso IsCircleReferenced Then Return False
                    If ContextObject Is Nothing Then Return False
                    If IsSupportedType(ContextObject.GetType) Then
                        Location.Add(New SelfStep(ContextObject))
                        Return True
                    End If
                    Return MoveToFirstPropertyOrItem()
                Case Else : Return False
            End Select
        End Function

        ''' <summary>Gets value indicating if cpecified type is supported for in-line (text node) representation.</summary>
        ''' <param name="T">Type to be verified</param>
        ''' <returns>Currently following types are supported:
        ''' <see cref="String"/>, <see cref="Char"/>, <see cref="SByte"/>, <see cref="Byte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/>, <see cref="ULong"/>, <see cref="Decimal"/>, <see cref="Double"/>, <see cref="Single"/>, <see cref="Boolean"/>, <see cref="Date"/>, <see cref="TimeSpan"/>, <see cref="TimeSpanFormattable"/>, <see cref="Uri"/>, <see cref="System.Text.StringBuilder"/> and any type that has <see cref="Type.IsEnum"/> true
        ''' </returns>
        ''' <remarks>Note for inheritors: Supported types are sometimes treated specially sometimes is only <see cref="System.Object.ToString"/> used. In order to control this behavoir overrides <see cref="SupportedTypeValue"/>
        ''' <para>See <seealso cref="SupportedTypeValue"/> for an example.</para>
        ''' </remarks>
        Protected Overridable Function IsSupportedType(ByVal T As Type) As Boolean
            Return _
                T.Equals(GetType(String)) OrElse _
                T.Equals(GetType(Char)) OrElse _
                T.Equals(GetType(Byte)) OrElse _
                T.Equals(GetType(SByte)) OrElse _
                T.Equals(GetType(Short)) OrElse _
                T.Equals(GetType(UShort)) OrElse _
                T.Equals(GetType(Long)) OrElse _
                T.Equals(GetType(ULong)) OrElse _
                T.Equals(GetType(Integer)) OrElse _
                T.Equals(GetType(UInteger)) OrElse _
                T.Equals(GetType(Decimal)) OrElse _
                T.Equals(GetType(Double)) OrElse _
                T.Equals(GetType(Single)) OrElse _
                T.Equals(GetType(Date)) OrElse _
                T.Equals(GetType(TimeSpan)) OrElse _
                T.Equals(GetType(TimeSpanFormattable)) OrElse _
                T.Equals(GetType(Uri)) OrElse _
                T.Equals(GetType(System.Text.StringBuilder)) OrElse _
                T.Equals(GetType(Boolean)) OrElse _
                T.IsEnum
        End Function
        ''' <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to first namespace node of the current node.</summary>
        ''' <returns>Not implemented. always retruns false.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overloads Overrides Function MoveToFirstNamespace(ByVal namespaceScope As System.Xml.XPath.XPathNamespaceScope) As Boolean
            Return False
        End Function
        ''' <summary>Moves to the node that has an attribute of type ID whose value matches the specified <see cref="T:System.String"></see>.</summary>
        ''' <returns>Not implemeted. always returns false.</returns>
        ''' <param name="id">A <see cref="T:System.String"></see> representing the ID value of the node to which you want to move.</param>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Function MoveToId(ByVal id As String) As Boolean
            Return False
        End Function
        ''' <summary>Moves the <see cref="XPathObjectNavigator"></see> to the next sibling node of the current node.</summary>
        ''' <returns>true if the <see cref="XPathObjectNavigator"/> is successful moving to the next sibling node; otherwise, false if there are no more siblings or if the <see cref="XPathObjectNavigator"></see> is currently positioned on an attribute node. If false, the position of the <see cref="XPathObjectNavigator"/> is unchanged.</returns>
        ''' <remarks>For <see cref="PropertyStep"/> the <see cref="MoveToFirstPropertyOrItem"/> is invoked. For <see cref="EnumerableStep"/> an attempt to invoke <see cref="System.Collections.IEnumerator.MoveNext"/> is done.</remarks>
        Public Overloads Overrides Function MoveToNext() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable
                    If CurrentEnumerator.MoveNext Then
                        DirectCast(CurrentStep, EnumerableStep).Index += 1
                        Return True
                    End If
                    Return False
                Case [Step].StepClasses.Property
                    Return MoveToFirstPropertyOrItem(Obj:=CurrentObject, After:=CurrentProperty, Replace:=True)
                Case Else : Return False
            End Select
        End Function
        ''' <summary>Moves the <see cref="XPathObjectNavigator"></see> to the next attribute.</summary>
        ''' <returns>Returns true if the <see cref="XPathObjectNavigator"></see> is successful moving to the next attribute; false if there are no more attributes. If false, the position of the <see cref="XPathObjectNavigator"></see> is unchanged.</returns>
        ''' <remarks>Only <see cref="RootStep"/>, <see cref="PropertyStep"/> and <see cref="EnumerableStep"/> have attributes. Attributes are ordered type-name, full-name, name, enumerable, circle-level. The enumerable attribute is present only when context object is <see cref="IEnumerable"/> (in such case it has value "true"). The name attribute is present always but for <see cref="RootStep"/> it has value <see cref="String.Empty"/>. The circle-level attribute is present only if it's value is non-zero. Attributes as menitoned are provided also for so-called supported types (<seealso cref="IsSupportedType"/>).</remarks>
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
                        If TypeOf ContextObject Is IEnumerable AndAlso Not IsSupportedType(ContextObject.GetType) Then
                            CurrentStep = New SpecialStep(CurrentStep.Object, SpecialStep.StepType.Enumerable)
                            Return True
                        End If
                        GoTo CircleLevel
                    Case SpecialStep.StepType.Enumerable
CircleLevel:            If CircleLevel < Location.Count - 2 Then '2 because 1 for difference between Count and highest index and 1 because last index is not important for me
                            CurrentStep = New SpecialStep(CurrentStep.Object, SpecialStep.StepType.CircleLevel)
                            Return True
                        End If
                End Select
            End If
            Return False
        End Function
        ''' <summary>Moves the <see cref="XPathObjectNavigator"></see> to the next namespace node.</summary>
        ''' <returns>Always false. Not implemented</returns>
        ''' <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope"></see> value describing the namespace scope. </param>
        Public Overloads Overrides Function MoveToNextNamespace(ByVal namespaceScope As System.Xml.XPath.XPathNamespaceScope) As Boolean
            Return False
        End Function
        ''' <summary>Moves the <see cref="XPathObjectNavigator"></see> to the parent node of the current node.</summary>
        ''' <returns>Returns true if the <see cref="XPathObjectNavigator"></see> is successful moving to the parent node of the current node; otherwise, false. If false, the position of the <see cref="XPathObjectNavigator"></see> is unchanged.</returns>
        ''' <remarks>Works for all type of steps expect <see cref="RootStep"/>. Removes current spet, so parent of current step becomes current step.</remarks>
        Public Overrides Function MoveToParent() As Boolean
            If Location.Count > 1 Then
                Location.RemoveAt(Location.Count - 1)
                Return True
            End If
            Return False
        End Function
        ''' <summary>When overridden in a derived class, moves the <see cref="XPathObjectNavigator"></see> to the previous sibling node of the current node.</summary>
        ''' <returns>Returns true if the <see cref="XPathObjectNavigator"></see> is successful moving to the previous sibling node; otherwise, false if there is no previous sibling node or if the <see cref="XPathObjectNavigator"></see> is currently positioned on an attribute node. If false, the position of the <see cref="XPathObjectNavigator"></see> is unchanged.</returns>
        ''' <remarks>This is valid for <see cref="PropertyStep"/> and <see cref="EnumerableStep"/>.
        ''' For <see cref="PropertyStep"/> moves to previous usable property (if any) using <see cref="GetFirstProperty"/>.
        ''' For <see cref="EnumerableStep"/> re-iterates <see cref="IEnumerator"/> to position less by 1 than actual position (if actual position is greater than zero; see <see cref="EnumerableStep.Index"/>). If it is zero that last property of current object become surrent step (if there is any usable property).
        ''' </remarks>
        Public Overrides Function MoveToPrevious() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable
                    With DirectCast(CurrentStep, EnumerableStep)
                        If .Index > 0 Then
                            CurrentStep = New EnumerableStep(CurrentStep.Object, .Index - 1)
                            Return True
                        End If
                        Dim prp As PropertyInfo = GetFirstProperty(CurrentObject, Reverse:=True)
                        If prp IsNot Nothing Then
                            CurrentStep = New PropertyStep(CurrentObject, prp)
                            Return True
                        End If
                        Return False
                    End With
                Case [Step].StepClasses.Property
                    Dim prp As PropertyInfo = GetFirstProperty(CurrentObject, CurrentProperty, True)
                    If prp IsNot Nothing Then
                        CurrentStep = New PropertyStep(CurrentObject, prp)
                        Return True
                    End If
                    Return False
                Case Else : Return False
            End Select
        End Function
        ''' <summary>When overridden in a derived class, gets the qualified name of the current node.</summary>
        ''' <remarks><see cref="LocalName"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides ReadOnly Property Name() As String
            Get
                Return LocalName
            End Get
        End Property
        ''' <summary>When overridden in a derived class, gets the namespace URI of the current node.</summary>
        ''' <returns>Not supported. Gets value from <see cref="NameTable"/> using <see cref="String.Empty"/> as key</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides ReadOnly Property NamespaceURI() As String
            Get
                Return NameTable.Get(String.Empty)
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="NameTable"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _NameTable As New NameTable
        ''' <summary>Gets the <see cref="T:System.Xml.XmlNameTable"></see> of the <see cref="XPathObjectNavigator"></see>.</summary>
        ''' <returns><see cref="NameTable"/> that contains only item with <see cref="String.Empty"/> as key</returns>
        Public Overrides ReadOnly Property NameTable() As System.Xml.XmlNameTable
            Get
                Return _NameTable
            End Get
        End Property
        ''' <summary>Gets the <see cref="T:System.Xml.XPath.XPathNodeType"/> of the current node.</summary>
        ''' <returns>
        ''' <para>One of the <see cref="T:System.Xml.XPath.XPathNodeType"/> values representing the current node.</para>
        ''' <list type="table"><listheader><term>Type of current step</term><description>Returned value</description></listheader>
        ''' <item><term><see cref="RootStep"/></term><description><see cref="XPathNodeType.Root"/></description></item>
        ''' <item><term><see cref="EnumerableStep"/> or <see cref="PropertyStep"/></term><term><see cref="XPathNodeType.Element"/></term></item>
        ''' <item><term><see cref="SpecialStep"/></term><description><see cref="XPathNodeType.Attribute"/></description></item>
        ''' <item><term><see cref="SelfStep"/></term><description><see cref="XPathNodeType.Text"/></description></item>
        ''' <item><description>other</description><description>Throws <see cref="InvalidOperationException"/></description></item>
        ''' </list>
        ''' </returns>
        ''' <exception cref="InvalidOperationException"><see cref="CurrentStep"/>.<see cref="[Step].StepClass">StepClass</see> is not member of <see cref="[Step].StepClasses"/>. This can happne only in derived class when inheritor have created own types of steps buth naven't overrided this property.</exception>
        Public Overrides ReadOnly Property NodeType() As System.Xml.XPath.XPathNodeType
            Get
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Root : Return XPathNodeType.Root
                    Case [Step].StepClasses.Enumerable, [Step].StepClasses.Property
                        Return XPathNodeType.Element
                    Case [Step].StepClasses.Special : Return XPathNodeType.Attribute
                    Case [Step].StepClasses.Self : Return XPathNodeType.Text
                    Case Else : Throw New InvalidOperationException("Unknown type of node.")
                End Select
            End Get
        End Property
        ''' <summary>When overridden in a derived class, gets the namespace prefix associated with the current node.</summary>
        ''' <returns>Not supported. Returns item from <see cref="NameTable"/> got by key <see cref="String.Empty"/></returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides ReadOnly Property Prefix() As String
            Get
                Return NameTable.Get(String.Empty)
            End Get
        End Property

        ''' <summary>Called by <see cref="Value"/> for steps which context object is of type for which <see cref="IsSupportedType"/> returns true</summary>
        ''' <param name="obj">Object to get <see cref="String"/> value for</param>
        ''' <remarks>This implementation treats supported types in same way as <see cref="Value"/> with exception for following types:
        ''' <list type="table"><listheader><term>Type</term><description>Treatement</description></listheader>
        ''' <item><term><see cref="Date"/></term><description>Returned in format YYY-MMddHH:mm:ss.fffzzz (fff part is ommited when <see cref="DateTime.Millisecond"/> is zero; zzz part is ommited when <see cref="DateTime.Kind"/> is neither <see cref="DateTimeKind.Local"/> nor <see cref="DateTimeKind.Utc"/> and replaced wizh 'Z' when it is <see cref="DateTimeKind.Utc"/>)</description></item>
        ''' <item><term><see cref="TimeSpan"/></term><description>Converted to <see cref="TimeSpanFormattable"/> and <see cref="SupportedTypeValue"/> is called again</description></item>
        ''' <item><term><see cref="TimeSpanFormattable"/></term><description>Returned ifn format h(0):mm:ss.lll (lll part is ommited when <see cref="TimeSpanFormattable.Milliseconds"/> is zero).</description></item>
        ''' <item><term><see cref="Boolean"/></term><description>"true" or "false" depending on value </description></item>
        ''' <item><term>Derived from <see cref="[Enum]"/></term><description>Returned result of <see cref="[Enum].ToString"/></description></item>
        ''' </list>
        ''' <para>Note for inheritors: You should consider implementing this method for any type added to <see cref="IsSupportedType"/> if you don't want default behavior for such type.</para>
        ''' <example>
        ''' <para>Following example shows preffered way of adding own supported type. Of course you can use any other way - change <see cref="SupportedTypeValue"/>'s behavior for in-this-implementation supported types or make supported type unsuported by returnnig false for it from <see cref="IsSupportedType"/></para>
        ''' <code>
        ''' <![CDATA[
        ''' Protected Overrides Function IsSupportedType(ByVal T As Type) As Boolean
        '''     Return T.Equals(GetType(MySupportedType)) OrElse T.IsSubclassOf(GetType(MySupportedType)) OrElse MyBase.IsSupportedType(T)
        ''' End Function
        ''' Protected Overrides Function SupportedTypeValue(ByVal obj As Object) As String
        '''     If TypeOf obj Is MySupportedType Then
        '''          Return DirectCast(obj, MySupportedType).GetValueThatIWantToBeGot()
        '''     Else
        '''         Return MyBase.SupportedTypeValue(obj)
        '''     End If
        ''' End Function
        ''' ]]>
        ''' </code>
        ''' </example>
        ''' Note: This function is also called for steps of <see cref="SpecialStep"/> type not depending on if type of pseudo-attribute has <see cref="IsSupportedType"/> true for itself. So, this function must behave correctly for all types of pseudo-attributes (in this implementation it is only <see cref="String"/> and <see cref="Boolean"/>).
        ''' </remarks>
        Protected Overridable Function SupportedTypeValue(ByVal obj As Object) As String
            If obj Is Nothing Then : Return String.Empty
            ElseIf TypeOf obj Is Date Then
                With DirectCast(obj, Date)
                    Dim fff$
                    If .Millisecond <> 0 Then
                        fff = ".fff"
                    Else
                        fff = ""
                    End If
                    Dim zzz$
                    Select Case .Kind
                        Case DateTimeKind.Local : zzz = "zzz"
                        Case DateTimeKind.Utc : zzz = "'Z'"
                        Case Else : zzz = ""
                    End Select
                    Return .ToString("YYYY-MM-ddHH:mm:ss" & fff & zzz, System.Globalization.CultureInfo.InvariantCulture)
                End With
            ElseIf TypeOf obj Is TimeSpan Then
                Return SupportedTypeValue(New TimeSpanFormattable(DirectCast(obj, TimeSpan)))
            ElseIf TypeOf obj Is TimeSpanFormattable Then
                With DirectCast(obj, TimeSpanFormattable)
                    Dim lll$
                    If .Milliseconds <> 0 Then
                        lll = ".lll"
                    Else
                        lll = ""
                    End If
                    Return .ToString("h(0):mm:ss" & lll, System.Globalization.CultureInfo.InvariantCulture)
                End With
                If TypeOf obj Is Boolean Then
                    Return Tools.VisualBasicT.iif(DirectCast(obj, Boolean), "true", "false")
                End If
            ElseIf obj.GetType.IsEnum Then
                Return DirectCast(obj, System.Enum).ToString()
            ElseIf TypeOf obj Is IFormattable Then
                Return DirectCast(obj, IFormattable).ToString("", System.Globalization.CultureInfo.InvariantCulture)
            Else
                Return obj.ToString
            End If
        End Function
        ''' <summary>Gets the string value of the item.</summary>
        ''' <returns>The string value of the item.</returns>
        ''' <remarks>If <see cref="CurrentStep"/> is <see cref="SpecialStep"/> then value of peudo-property depending on <see cref="SpecialStep.Type"/> is returned (using <see cref="SupportedTypeValue"/>) otherwise if context object is <see cref="IFormattable"/> its value is obtained via <see cref="IFormattable.ToString"/> with <see cref="String.Empty"/> as format parameter and <see cref="System.Globalization.CultureInfo.InvariantCulture"/> as culture if it is not <see cref="IFormattable"/> value is obtained via <see cref="System.Object.ToString"/>. But vhen <see cref="IsSupportedType"/> returns true for type of context object <see cref="SupportedTypeValue"/> is used instead.</remarks>
        Public Overrides ReadOnly Property Value() As String
            Get
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Special
                        If ContextValue Is Nothing Then Return String.Empty
                        Return SupportedTypeValue(ContextValue)
                    Case Else
                        If ContextObject Is Nothing Then Return String.Empty
                        If IsSupportedType(ContextObject.GetType) Then
                            Return SupportedTypeValue(ContextObject)
                        Else
                            If TypeOf ContextObject Is IFormattable Then
                                Return DirectCast(ContextObject, IFormattable).ToString("", System.Globalization.CultureInfo.InvariantCulture)
                            Else
                                Return ContextObject.ToString
                            End If
                        End If
                End Select
            End Get
        End Property
        ''' <summary>Gets the current node's value as an <see cref="T:System.Boolean"></see>.</summary>
        ''' <returns>The current node's value as an <see cref="T:System.Boolean"></see>. If <see cref="ContextValue"/> is <see cref="IConvertible"/> than <see cref="IConvertible.ToBoolean"/> is used, otherwise <see cref="XPathObjectNavigator.ValueAsLong"/> is called.</returns>
        ''' <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Boolean"></see>.</exception>
        ''' <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean"></see> is not valid.</exception>
        Public Overrides ReadOnly Property ValueAsBoolean() As Boolean
            Get
                If ContextValue Is Nothing Then Return False
                If TypeOf ContextValue Is IConvertible Then
                    With DirectCast(ContextValue, IConvertible)
                        Return .ToBoolean(System.Globalization.CultureInfo.InvariantCulture)
                    End With
                End If
                Return MyBase.ValueAsBoolean
            End Get
        End Property
        ''' <summary>Gets the current node's value as an <see cref="T:System.Double"></see>.</summary>
        ''' <returns>The current node's value as an <see cref="T:System.Double"></see>. If <see cref="ContextValue"/> is <see cref="IConvertible"/> than <see cref="IConvertible.ToDouble"/> is used, otherwise <see cref="XPathObjectNavigator.ValueAsLong"/> is called.</returns>
        ''' <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Double"></see>.</exception>
        ''' <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double"></see> is not valid.</exception>
        Public Overrides ReadOnly Property ValueAsDouble() As Double
            Get
                If ContextValue Is Nothing Then Return 0.0
                If TypeOf ContextValue Is IConvertible Then
                    With DirectCast(ContextValue, IConvertible)
                        Return .ToDouble(System.Globalization.CultureInfo.InvariantCulture)
                    End With
                End If
                Return MyBase.ValueAsDouble
            End Get
        End Property
        ''' <summary>Gets the current node's value as an <see cref="T:System.DateTime"></see>.</summary>
        ''' <returns>The current node's value as an <see cref="T:System.DateTime"></see>. If <see cref="ContextValue"/> is <see cref="IConvertible"/> than <see cref="IConvertible.ToDateTime"/> is used, otherwise <see cref="XPathObjectNavigator.ValueAsLong"/> is called.</returns>
        ''' <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.DateTime"></see>.</exception>
        ''' <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime"></see> is not valid.</exception>
        Public Overrides ReadOnly Property ValueAsDateTime() As Date
            Get
                If ContextValue Is Nothing Then Return DateTime.MinValue
                If TypeOf ContextValue Is IConvertible Then
                    With DirectCast(ContextValue, IConvertible)
                        Return .ToDateTime(System.Globalization.CultureInfo.InvariantCulture)
                    End With
                End If
                Return MyBase.ValueAsDateTime
            End Get
        End Property
        ''' <summary>Gets the current node's value as an <see cref="T:System.Int32"></see>.</summary>
        ''' <returns>The current node's value as an <see cref="T:System.Int32"></see>. If <see cref="ContextValue"/> is <see cref="IConvertible"/> than <see cref="IConvertible.ToInt32"/> is used, otherwise <see cref="XPathObjectNavigator.ValueAsLong"/> is called.</returns>
        ''' <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int32"></see>.</exception>
        ''' <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32"></see> is not valid.</exception>
        Public Overrides ReadOnly Property ValueAsInt() As Integer
            Get
                If ContextValue Is Nothing Then Return 0I
                If TypeOf ContextValue Is IConvertible Then
                    With DirectCast(ContextValue, IConvertible)
                        Return .ToInt32(System.Globalization.CultureInfo.InvariantCulture)
                    End With
                End If
                Return MyBase.ValueAsInt
            End Get
        End Property
        ''' <summary>Gets the current node's value as an <see cref="T:System.Int64"></see>.</summary>
        ''' <returns>The current node's value as an <see cref="T:System.Int64"></see>. If <see cref="ContextValue"/> is <see cref="IConvertible"/> than <see cref="IConvertible.ToInt64"/> is used, otherwise <see cref="XPathObjectNavigator.ValueAsLong"/> is called.</returns>
        ''' <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int64"></see>.</exception>
        ''' <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64"></see> is not valid.</exception>
        Public Overrides ReadOnly Property ValueAsLong() As Long
            Get
                If ContextValue Is Nothing Then Return 0L
                If TypeOf ContextValue Is IConvertible Then
                    With DirectCast(ContextValue, IConvertible)
                        Return .ToInt64(System.Globalization.CultureInfo.InvariantCulture)
                    End With
                End If
                Return MyBase.ValueAsLong
            End Get
        End Property
        ''' <summary>Gets the .NET Framework <see cref="T:System.Type"></see> of the current node.</summary>
        ''' <returns>The .NET Framework <see cref="T:System.Type"></see> of the current node. The default value is <see cref="T:System.String"></see>.</returns>
        ''' <remarks>Returned value depends on actual type of actual context value (see <seealso cref="ContextValue"/>). If <see cref="ContextValue"/> is null this returns <see cref="DBNull"/></remarks>
        Public Overrides ReadOnly Property ValueType() As System.Type
            Get
                If ContextValue Is Nothing Then Return GetType(DBNull)
                Return ContextValue.GetType
            End Get
        End Property
        ''' <summary>Gets the current node's value as the <see cref="T:System.Type"></see> specified, using the <see cref="T:System.Xml.IXmlNamespaceResolver"></see> object specified to resolve namespace prefixes.</summary>
        ''' <returns>The value of the current node as the <see cref="T:System.Type"></see> requested. If <see cref="ValueType"/> is subclass of (or is itself) or implements <paramref name="returnType"/> then <see cref="ContextValue"/> is returned. Otherwise <see cref="XPathNavigator.ValueAs"/> is called</returns>
        ''' <param name="returnType">The <see cref="T:System.Type"></see> to return the current node's value as.</param>
        ''' <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver"></see> object used to resolve namespace prefixes.</param>
        ''' <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
        ''' <exception cref="T:System.FormatException">The current node's value is not in the correct format for the target type.</exception>
        Public Overrides Function ValueAs(ByVal returnType As System.Type, ByVal nsResolver As System.Xml.IXmlNamespaceResolver) As Object
            If ValueType.Equals(returnType) OrElse ValueType.IsSubclassOf(returnType) OrElse Array.IndexOf(ValueType.GetInterfaces, returnType) >= 0 Then Return ContextValue
            Return MyBase.ValueAs(returnType, nsResolver)
        End Function

#Region "Helper properties"
        ''' <summary>Gets current context value</summary>
        ''' <remarks>See <seealso cref="XPathObjectNavigator"/> for definition of context value</remarks>
        Public ReadOnly Property ContextValue() As Object
            Get
                Select Case CurrentStep.StepClass
                    Case [Step].StepClasses.Special
                        Select Case DirectCast(CurrentStep, SpecialStep).Type
                            Case SpecialStep.StepType.Enumerable : Return TypeOf ContextObject Is IEnumerable AndAlso Not IsSupportedType(ContextObject.GetType)
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
                            Case SpecialStep.StepType.CircleLevel : Return Location.Count - CircleLevel - 2 '2 because 1 for difference between highest index and Count and 1 because we are at level of attribute
                            Case Else : Return ""
                        End Select
                    Case Else : Return ContextObject
                End Select
            End Get
        End Property
        ''' <summary>Gets or sets actual current step</summary>
        ''' <value>Setting value of <see cref="CurrentStep"/> replaces actual current step by another one - so, it changes position of <see cref="XPathObjectNavigator"/> inside parent of current step</value>
        ''' <remarks>See <seealso cref="XPathObjectNavigator"/> for definition of current step</remarks>
        Protected Property CurrentStep() As [Step]
            <DebuggerStepThrough()> Get
                Return Location(Location.Count - 1)
            End Get
            <DebuggerStepThrough()> Set(ByVal value As [Step])
                Location(Location.Count - 1) = value
            End Set
        End Property
        ''' <summary>Gets actual current property</summary>
        ''' <remarks>See <seealso cref="XPathObjectNavigator"/> for definition of current property</remarks>
        Protected ReadOnly Property CurrentProperty() As PropertyInfo
            <DebuggerStepThrough()> Get
                If CurrentStep.StepClass = [Step].StepClasses.Property Then Return DirectCast(CurrentStep, PropertyStep).Property Else Return Nothing
            End Get
        End Property
        ''' <summary>Gets actual current object</summary>
        ''' <remarks>See <seealso cref="XPathObjectNavigator"/> for definition of current object</remarks>
        Protected ReadOnly Property CurrentObject() As Object
            Get
                Return CurrentStep.Object
            End Get
        End Property
        ''' <summary>Gets actual current enumerator</summary>
        ''' <remarks>See <see cref="XPathObjectNavigator"/> for definition of current enumerator</remarks>
        Private ReadOnly Property CurrentEnumerator() As IEnumerator
            Get
                If CurrentStep.StepClass = [Step].StepClasses.Enumerable Then
                    Return DirectCast(CurrentStep, EnumerableStep).Enumerator
                Else
                    Return Nothing
                End If
            End Get
        End Property
        ''' <summary>Gets actual context object</summary>
        ''' <remarks>See <seealso cref="XPathObjectNavigator"/> for definition of context object</remarks>
        Protected ReadOnly Property ContextObject() As Object
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
        ''' <summary>Gets information if current <see cref="ContextObject"/> is context object of any of parent nodes.</summary>
        ''' <returns>True if <see cref="CircleLevel"/> &lt; <see cref="Location">Location</see>.<see cref="List(Of [Step]).Count">Count</see> - 1</returns>
        Public ReadOnly Property IsCircleReferenced() As Boolean
            Get
                Return CircleLevel < Location.Count - 1
            End Get
        End Property
        ''' <summary>Detects circle references. Gets index into the <see cref="Location"/> collection where is the first occurence of actual <see cref="ContextObject"/> as context object.</summary>
        ''' <returns>If no reference equal context object in location is found returns index of current step (this is <see cref="Location">Location</see>.<see cref="List(Of [Step]).Count">Count</see> - 1).
        ''' If <see cref="CurrentStep"/> is <see cref="SpecialStep"/> and returned value should be <see cref="Location">Location</see>.<see cref="List(Of [Step]).Count">Count</see> - 1 then it is <see cref="Location">Location</see>.<see cref="List(Of [Step]).Count">Count</see> - 2 (as if it is in context of pseudo-attribute parent instead of pseudo-attribute itself).
        ''' </returns>
        ''' <remarks>If current step is <see cref="SpecialStep"/> <see cref="CurrentObject"/> is used instead of <see cref="ContextObject"/></remarks>
        Public Overridable ReadOnly Property CircleLevel() As Integer
            Get
                Try
                    Dim co As Object = ContextObject
                    If CurrentStep.StepClass = [Step].StepClasses.Special Then co = CurrentObject
                    Dim i As Integer = 0
                    For Each item As [Step] In Location
                        Dim Obj As Object
                        Select Case item.StepClass
                            Case [Step].StepClasses.Root
                                Obj = item.Object
                            Case [Step].StepClasses.Property
                                Obj = DirectCast(item, PropertyStep).Property.GetValue(item.Object, Nothing)
                            Case [Step].StepClasses.Enumerable
                                Obj = DirectCast(item, EnumerableStep).Enumerator.Current
                            Case Else : Return Location.Count - 1
                        End Select
                        If Obj IsNot Nothing AndAlso Obj Is co Then
                            Return i
                        End If
                        i += 1
                    Next item
                    Return Location.Count - 1
                Finally
                    If CurrentStep.StepClass = [Step].StepClasses.Special AndAlso CircleLevel = Location.Count - 1 Then CircleLevel = Location.Count - 2
                End Try
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="AllowCircles"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private ReadOnly _AllowCircles As Boolean
        ''' <summary>Gets value indicating if circles in references of objects are allowed.</summary>
        ''' <remarks>This property can be set only via CTor
        ''' <para>Circle is detected when <see cref="MoveToFirstChild"/> is invoked and <see cref="ContextObject"/> reference equals to context object of any of steps in <see cref="Location"/>.
        ''' If <see cref="AllowCircles"/> is False in such situation than <see cref="MoveToFirstChild"/> returns false. In XPath you can detect circle references by testing the circle-level pseudo-attribute.
        ''' The circle-level attribute contains number of steps upwards (in parent axis) to reach same context object as is current <see cref="ContextObject"/>.
        ''' </para>
        ''' </remarks>
        Public ReadOnly Property AllowCircles() As Boolean
            Get
                Return _AllowCircles
            End Get
        End Property

#End Region
    End Class
End Namespace
#End If