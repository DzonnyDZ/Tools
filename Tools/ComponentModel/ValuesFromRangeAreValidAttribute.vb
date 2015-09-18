Imports CultureInfo = System.Globalization.CultureInfo
Imports Tools.ExtensionsT

#If True
Namespace ComponentModelT
    ''' <summary>For enumeration this attribute is applied onto indicates which additional values that are not member of enumeration are valid enumeration values</summary>
    ''' <remarks>This attribute can be applied multiple times
    ''' <para>Note: This attribute can be nohow enforced. It depends on target where enumeration is passed if it utilizes it.</para>
    ''' <para>This attribute canot be used for big values of type <see cref="ULong"/>.</para></remarks>
    <AttributeUsage(AttributeTargets.Enum, AllowMultiple:=True)> _
    Public Class ValuesFromRangeAreValidAttribute
        Inherits Attribute
        ''' <summary>CTor - defines range of possible values</summary>
        ''' <param name="Min">Minimum range value</param>
        ''' <param name="Max">Maximum range value</param>
        ''' <exception cref="ArgumentException"><paramref name="Max"/> is smaller than <see cref="Min"/></exception>
        Public Sub New(ByVal Min As Long, ByVal Max As Long)
            If Max < Min Then Throw New ArgumentException(ResourcesT.Exceptions.MustBeGreaterThanOrEqualTo1.f("Max", "Min"))
            _Min = Min
            _Max = Max
        End Sub
        ''' <summary>Contains value of the <see cref="Min"/> property</summary>
        Private ReadOnly _Min As Long
        ''' <summary>Contains value of the <see cref="Max"/> property</summary>
        Private ReadOnly _Max As Long
        ''' <summary>Gets minimum value of range of additional values</summary>
        Public ReadOnly Property Min() As Long
            Get
                Return _Min
            End Get
        End Property
        ''' <summary>Gets maximum value of range of additional values</summary>
        Public ReadOnly Property Max() As Long
            Get
                Return _Max
            End Get
        End Property

        ''' <summary>Gets all possible values for enumeration with <see cref="ValuesFromRangeAreValidAttribute"/> possibly aplied</summary>
        ''' <param name="Enumeration">Enumeration to get values for</param>
        ''' <returns>Array containing all the values possible for <paramref name="Enumeration"/> with respect to all applied <see cref="ValuesFromRangeAreValidAttribute"/> attributes.</returns>
        ''' <remarks>For overlapping attributes and values specified via attribute as well as in enumeration, such values are present only once</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Enumeration"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Enumeration"/> does not represent enumeration</exception>
        Public Shared Function GetAllValues(ByVal Enumeration As Type) As [Enum]()
            If Enumeration Is Nothing Then Throw New ArgumentNullException("Type")
            If Not Enumeration.IsEnum Then Throw New ArgumentException(ResourcesT.Exceptions.MustBeEnumeration.f("Enumeration"), "Enumeration")
            Dim KnownValues = [Enum].GetValues(Enumeration)
            Dim AllValues As New List(Of [Enum])
            For Each value In KnownValues
                AllValues.Add(value)
            Next
            Dim Attributes = Enumeration.GetAttributes(Of ValuesFromRangeAreValidAttribute)()
            If Attributes Is Nothing Then Return AllValues.ToArray
            For Each Attribute In Attributes
                For i As Long = Attribute.min To Attribute.max
                    Dim value As [Enum] = GetEnumValue(Enumeration, i)
                    If Not AllValues.Contains(value) Then AllValues.Add(value)
                Next
            Next
            Return AllValues.ToArray
        End Function
        ''' <summary>Gets value indicating if given value is valid value of its enumeration type according to all possibly applied <see cref="ValuesFromRangeAreValidAttribute"/>s</summary>
        ''' <param name="Value">Value to observe</param>
        ''' <returns>True if value is defined for its enumeration type or it is in range of any of applied <see cref="ValuesFromRangeAreValidAttribute"/>s; otherwise false</returns>
        ''' <remarks>If <paramref name="Value"/> is greater than <see cref="Int64.MaxValue"/> attribute testing is skipped.</remarks>
        Public Shared Function IsValidValue(ByVal Value As [Enum]) As Boolean
            If Value.IsDefined Then Return True
            If [Enum].GetUnderlyingType(Value.GetType).Equals(GetType(ULong)) AndAlso Value.GetValue.ToUInt64(CultureInfo.InvariantCulture) > CULng(Long.MaxValue) Then Return False
            Dim Attributes = Value.GetType.GetAttributes(Of ValuesFromRangeAreValidAttribute)()
            If Attributes Is Nothing Then Return False
            For Each Attribute In Attributes
                If Value.GetValue.ToInt64(CultureInfo.InstalledUICulture) >= Attribute.Min AndAlso Value.GetValue.ToInt64(CultureInfo.InstalledUICulture) <= Attribute.Max Then Return True
            Next
            Return False
        End Function
    End Class
End Namespace
#End If

