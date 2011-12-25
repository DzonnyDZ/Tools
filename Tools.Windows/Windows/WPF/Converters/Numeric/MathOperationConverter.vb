Imports System.Windows.Data, Tools.ExtensionsT

Namespace WindowsT.WPF.ConvertersT
    Public Class MathOperationConverter
        Implements IValueConverter

        Private _operation As Char = "+"c
        Public Overridable Property Operation As Char
            Get
                Return _operation
            End Get
            Set(value As Char)
                Select Case value
                    Case "+"c, "-"c, "*"c, "/"c, "^"c, "\"c, "&"c, "%"c
                    Case Else : Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_UnsupportedOperation.f(value), "value")
                End Select
            End Set
        End Property

        Protected Overridable Function GetOperation([operator] As Char, type As Type) As [Delegate]
            Select Case [operator]
                Case "+"c
                    Select Case type
                        Case GetType(Byte)
                        Case GetType(Byte)      'TODO
                    End Select
                Case "-"c
                Case "*"c
                Case "/"c
                Case "^"c
                Case "\"c
                Case "&"c
                Case "%"c
            End Select

    End Class
End Namespace
