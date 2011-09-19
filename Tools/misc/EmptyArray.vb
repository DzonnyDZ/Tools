Imports System.Runtime.CompilerServices

''' <summary>Provides static instances of empty arrays of any type</summary>
''' <typeparam name="T">Type of array to get</typeparam>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
Public Class EmptyArray(Of T)
    ''' <summary>There is not CTor, this is static class</summary>
    Partial Private Sub New()
    End Sub
    ''' <summary>Gets static empty array of given type</summary>
    ''' <remarks>For type <see cref="Type"/> gets <see cref="Type.EmptyTypes"/></remarks>
    Public Shared ReadOnly value As T() = If(GetType(T).Equals(GetType(Type)), DirectCast(CObj(Type.EmptyTypes), T()), New T() {})
End Class

''' <summary>Provides static instances of empty arrays of common .NET types</summary>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
Public Class EmptyArray
    ''' <summary>There is not CTor, this is static class</summary>
    Partial Private Sub New()
    End Sub

    ''' <summary>Gets an empty array of type <see cref="System.[Byte]"/></summary>
    Public Shared ReadOnly Property [Byte] As Byte()
        Get
            Return EmptyArray(Of Byte).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.[SByte]"/></summary>
    <CLSCompliant(False)>
    Public Shared ReadOnly Property [SByte] As SByte()
        Get
            Return EmptyArray(Of SByte).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Int16"/></summary>
    Public Shared ReadOnly Property Int16 As Int16()
        Get
            Return EmptyArray(Of Int16).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Int32"/></summary>
    Public Shared ReadOnly Property Int32 As Int32()
        Get
            Return EmptyArray(Of Int32).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Int64"/></summary>
    Public Shared ReadOnly Property Int64 As Int64()
        Get
            Return EmptyArray(Of Int64).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.UInt16"/></summary>
    <CLSCompliant(False)>
    Public Shared ReadOnly Property UInt16 As UInt16()
        Get
            Return EmptyArray(Of UInt16).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.UInt32"/></summary>
    <CLSCompliant(False)>
    Public Shared ReadOnly Property UInt32 As UInt32()
        Get
            Return EmptyArray(Of UInt32).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.UInt64"/></summary>
    <CLSCompliant(False)>
    Public Shared ReadOnly Property UInt64 As UInt64()
        Get
            Return EmptyArray(Of UInt64).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Single"/></summary>
    Public Shared ReadOnly Property [Single] As Single()
        Get
            Return EmptyArray(Of Single).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Double"/></summary>
    Public Shared ReadOnly Property [Double] As Double()
        Get
            Return EmptyArray(Of Double).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Boolean"/></summary>
    Public Shared ReadOnly Property [Boolean] As Boolean()
        Get
            Return EmptyArray(Of Boolean).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Char"/></summary>
    Public Shared ReadOnly Property [Char] As Char()
        Get
            Return EmptyArray(Of Char).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Decimal"/></summary>
    Public Shared ReadOnly Property [Decimal] As Decimal()
        Get
            Return EmptyArray(Of Decimal).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.String"/></summary>
    Public Shared ReadOnly Property [String] As String()
        Get
            Return EmptyArray(Of String).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Type"/></summary>
    Public Shared ReadOnly Property Type As Type()
        Get
            Return EmptyArray(Of Type).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.DateTime"/></summary>
    Public Shared ReadOnly Property DateTime As DateTime()
        Get
            Return EmptyArray(Of DateTime).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.TimeSpan"/></summary>
    Public Shared ReadOnly Property TimeSpan As TimeSpan()
        Get
            Return EmptyArray(Of TimeSpan).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.DateTimeOffset"/></summary>
    Public Shared ReadOnly Property DateTimeOffset As DateTimeOffset()
        Get
            Return EmptyArray(Of DateTimeOffset).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Object"/></summary>
    Public Shared ReadOnly Property [Object] As Object()
        Get
            Return EmptyArray(Of Object).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Guid"/></summary>
    Public Shared ReadOnly Property Guid As Guid()
        Get
            Return EmptyArray(Of Guid).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Enum"/></summary>
    Public Shared ReadOnly Property [Enum] As [Enum]()
        Get
            Return EmptyArray(Of [Enum]).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Delegate"/></summary>
    Public Shared ReadOnly Property [Delegate] As [Delegate]()
        Get
            Return EmptyArray(Of [Delegate]).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Attribute"/></summary>
    Public Shared ReadOnly Property Attribute As Attribute()
        Get
            Return EmptyArray(Of Attribute).value
        End Get
    End Property

    ''' <summary>Gets an empty array of type <see cref="System.Exception"/></summary>
    Public Shared ReadOnly Property Exception As Exception()
        Get
            Return EmptyArray(Of Exception).value
        End Get
    End Property
End Class
