Imports System.Reflection
#If Config <= Nightly Then
Namespace Internal
    Public Class ToolAssembly
        Shared Sub Test()
            Dim [Class] As Type = GetType([Class])
            Dim NotInheritableClass As Type = GetType(NotInheritableClass)
            Dim MustInheritClass As Type = GetType(MustInheritClass)
            Dim [Structure] As Type = GetType([Structure])
            Dim [Module] As Type = GetType([Module])
            Dim [Delegate] As Type = GetType([Delegate])
            Dim [Enum] As Type = GetType([Enum])
            Dim [Interface] As Type = GetType([Interface])

            Dim Test As Type = GetType(Test)
            Dim M As MemberInfo() = Test.GetMembers
        End Sub
    End Class
#Region "TestClasses"
    Class [Class]
        Public A As Long
    End Class
    NotInheritable Class NotInheritableClass
    End Class
    MustInherit Class MustInheritClass
    End Class
    Structure [Structure]
        Public A As Long
    End Structure
    Module [Module]
        Public A As Long
    End Module
    Delegate Sub [Delegate](ByVal A As Long)
    Enum [Enum] As Long
        A
    End Enum
    Interface [Interface]
        Property A() As Long
    End Interface

    Class Test
        Public Sub [Sub](<Runtime.InteropServices.Out()> ByRef X As Long)
        End Sub
        Public Function [Function]() As Long
        End Function
        Public Property [Property]() As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Public Event [Event] As EventHandler
        Public Event [Event2]()
        Public Custom Event [Event3] As EventHandler(Of MouseEventArgs)
            AddHandler(ByVal value As EventHandler(Of MouseEventArgs))
            End AddHandler
            RemoveHandler(ByVal value As EventHandler(Of MouseEventArgs))
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            End RaiseEvent
        End Event
        Public [Dim] As Long
        Public Shared Widening Operator CType(ByVal a As String) As Test
            Return Nothing
        End Operator
        Public Shared Widening Operator CType(ByVal a As Test) As String
            Return ""
        End Operator
        Public Shared Narrowing Operator CType(ByVal a As Long) As Test
            Return Nothing
        End Operator
        Public Shared Narrowing Operator CType(ByVal a As Test) As Long
        End Operator
        Public Shared Operator +(ByVal a As Test) As Test
            Return a
        End Operator
        Public Sub New()
        End Sub
    End Class
#End Region
End Namespace
#End If