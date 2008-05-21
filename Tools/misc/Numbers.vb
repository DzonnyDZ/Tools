Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage:Nightly
''' <summary>Contains extension methods for working with numbers of basic data types</summary>
Module Numbers
    'ASAP:Mark, comments
    <Extension()> Function IsNaN(ByVal n As Single) As Boolean
        Return Single.IsNaN(n)
    End Function
    <Extension()> Function IsNaN(ByVal n As Double) As Boolean
        Return Double.IsNaN(n)
    End Function
    <Extension()> Function IsInfinity(ByVal n As Single) As Boolean
        Return Single.IsInfinity(n)
    End Function
    <Extension()> Function IsInfinity(ByVal n As Double) As Boolean
        Return Double.IsInfinity(n)
    End Function
    <Extension()> Function IsNegativeInfinity(ByVal n As Single) As Boolean
        Return Single.IsNegativeInfinity(n)
    End Function
    <ExtenderProvidedProperty()> Function IsPositiveInfinity(ByVal n As Single) As Boolean
        Return Single.IsPositiveInfinity(n)
    End Function
    <Extension()> Function IsNegativeInfinity(ByVal n As Double) As Boolean
        Return Double.IsNegativeInfinity(n)
    End Function
    <Extension()> Function IsPositiveInfinity(ByVal n As Double) As Boolean
        Return Double.IsPositiveInfinity(n)
    End Function
    <Extension()> Function GetBits(ByVal n As Decimal) As Integer()
        Return Decimal.GetBits(n)
    End Function
End Module
#End If