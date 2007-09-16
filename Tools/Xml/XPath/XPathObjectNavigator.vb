Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath
Imports System.Reflection
Imports System.ComponentModel

#If Config <= Nightly Then
Namespace XmlT.XPathT
    'ASAP:Mark, Wiki, Comment, Forum
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
        <DebuggerDisplay("{ToString}")> _
        Private MustInherit Class [Step] : Implements ICloneable
            Friend ReadOnly [Object] As Object
            Public Enum StepClasses
                Root
                [Property]
                Enumerable
                Special
                Self
            End Enum
            Public MustOverride ReadOnly Property StepClass() As StepClasses
            Friend Sub New(ByVal [Object] As Object)
                Me.Object = [Object]
            End Sub
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
            Public MustOverride Overloads Function Equals(ByVal other As [Step]) As Boolean
            Public MustOverride Function Clone() As [Step]
            Private Function Clone1() As Object Implements System.ICloneable.Clone
                Return Me.Clone
            End Function
            Public Overrides Function ToString() As String
                Return String.Format("Object ""{0}"" type {1}", Me.Object, Me.Object.GetType.Name)
            End Function
        End Class
        Private Class RootStep : Inherits [Step]
            Friend Sub New(ByVal [Object] As Object)
                MyBase.New([Object])
            End Sub
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is RootStep AndAlso Me.Object Is other.Object
            End Function
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Root
                End Get
            End Property
            Public Overrides Function Clone() As [Step]
                Return New RootStep(Me.Object)
            End Function
        End Class
        Private Class PropertyStep : Inherits [Step]
            Friend ReadOnly [Property] As PropertyInfo
            Friend Sub New(ByVal [Object] As Object, ByVal [Property] As PropertyInfo)
                MyBase.New([Object])
                Me.Property = [Property]
            End Sub
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is PropertyStep AndAlso Me.Object Is other.Object AndAlso Me.Property.Name = DirectCast(other, PropertyStep).Property.Name
            End Function
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Property
                End Get
            End Property
            Public Overrides Function Clone() As [Step]
                Return New PropertyStep(Me.Object, Me.Property)
            End Function
            Public Overrides Function ToString() As String
                Return MyBase.ToString() & String.Format(" Property {0}", Me.Property.Name)
            End Function
        End Class
        Private Class EnumerableStep : Inherits [Step]
            Friend Index As Integer
            Private _Enumerator As IEnumerator
            Public ReadOnly Property Enumerator() As IEnumerator
                Get
                    Return _Enumerator
                End Get
            End Property
            Friend Sub New(ByVal [Object] As Object, ByVal [index] As Integer)
                MyBase.New([Object])
                Me.Index = index
                _Enumerator = Me.Object.GetEnumerator
                For i As Integer = 0 To Me.Index
                    Enumerator.MoveNext()
                Next i
            End Sub
            Private Shadows ReadOnly Property [Object]() As IEnumerable
                Get
                    Return MyBase.Object
                End Get
            End Property
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is EnumerableStep AndAlso Me.Object Is other.Object AndAlso Me.Index = DirectCast(other, EnumerableStep).Index
            End Function
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Enumerable
                End Get
            End Property
            Public Overrides Function Clone() As [Step]
                Return New EnumerableStep(Me.Object, Me.Index)
            End Function
            Public Overrides Function ToString() As String
                Return MyBase.ToString() & String.Format(" index {0}", Index)
            End Function
        End Class
        Private Class SelfStep : Inherits [Step]
            Friend Sub New(ByVal [Object] As Object)
                MyBase.New([Object])
            End Sub
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is SelfStep AndAlso Me.Object Is other.Object
            End Function
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Self
                End Get
            End Property
            Public Overrides Function Clone() As [Step]
                Return New SelfStep(Me.Object)
            End Function
        End Class
        Private Class SpecialStep : Inherits [Step]
            Friend Enum StepType
                TypeName
                FullName
                Name
                Enumerable
            End Enum
            Friend Type As StepType
            Friend Sub New(ByVal [Object] As Object, ByVal Type As StepType)
                MyBase.New([Object])
                Me.Type = Type
            End Sub
            Public Overrides Function Equals(ByVal other As [Step]) As Boolean
                Return TypeOf other Is SpecialStep AndAlso Me.Type = DirectCast(other, SpecialStep).Type
            End Function
            Public Overrides ReadOnly Property StepClass() As [Step].StepClasses
                Get
                    Return StepClasses.Special
                End Get
            End Property
            Public Overrides Function Clone() As [Step]
                Return New SpecialStep(Me.Object, Me.Type)
            End Function
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

        Public Overrides Function MoveToFirstAttribute() As Boolean
            Select Case CurrentStep.StepClass
                Case [Step].StepClasses.Enumerable, [Step].StepClasses.Property, [Step].StepClasses.Root
                    Location.Add(New SpecialStep(Location(Location.Count - 1).Object, SpecialStep.StepType.TypeName))
                    Return True
            End Select
            Return False
        End Function

        Private Property CurrentStep() As [Step]
            Get
                Return Location(Location.Count - 1)
            End Get
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
            If Reverse Then [To] = 0 Else [To] = Properties.Length
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
                    Case Else : Location.Add(New PropertyStep(fp.GetValue(Me.CurrentStep.Object, Nothing), fp))
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
                    If CurrentStep.StepClass = [Step].StepClasses.Enumerable Then
                        obj = CurrentEnumerator.Current
                    Else
                        obj = CurrentStep.Object
                    End If
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
                        With CurrentStep
                            If TypeOf .Object Is Date Then
                                Dim fff As String = Tools.VisualBasicT.iif(DirectCast(.Object, Date).Millisecond = 0, "", ".fff")
                                Return DirectCast(.Object, Date).ToString("yyyy-MM-DDHH:mm:ss" & fff, System.Globalization.CultureInfo.InvariantCulture)
                            ElseIf TypeOf .Object Is TimeSpan Then
                                Dim lll As String = Tools.VisualBasicT.iif(DirectCast(.Object, TimeSpan).Milliseconds = 0, "", ".lll")
                                Return CType(DirectCast(.Object, TimeSpan), Tools.TimeSpanFormattable).ToString("h(0):mm:ss" & lll, System.Globalization.CultureInfo.InvariantCulture)
                            ElseIf TypeOf .Object Is Tools.TimeSpanFormattable Then
                                Dim lll As String = Tools.VisualBasicT.iif(DirectCast(.Object, Tools.TimeSpanFormattable).Milliseconds = 0, "", ".lll")
                                Return DirectCast(.Object, Tools.TimeSpanFormattable).ToString("h(0):mm:ss" & lll, System.Globalization.CultureInfo.InvariantCulture)
                            ElseIf TypeOf .Object Is Boolean Then
                                Return Tools.VisualBasicT.iif(.Object, "true", "false")
                            ElseIf TypeOf .Object Is IFormattable Then
                                Return DirectCast(.Object, IFormattable).ToString("", System.Globalization.CultureInfo.InvariantCulture)
                            Else
                                Return .Object.ToString
                            End If
                        End With
                End Select
                Return ""
            End Get
        End Property
    End Class
End Namespace
#End If