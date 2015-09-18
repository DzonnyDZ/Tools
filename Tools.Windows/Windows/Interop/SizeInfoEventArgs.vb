Imports System.Windows, Tools.ExtensionsT

#If True
Namespace WindowsT.InteropT
    ''' <summary>Wraps <see cref="SizeChangedInfo"/> as clas derived from <see cref="EventArgs"/></summary>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2">Class introduced</version>
    Public NotInheritable Class SizeChangedInfoEventArgs
        Inherits EventArgs
        Implements IEquatable(Of SizeChangedInfo), IEquatable(Of SizeChangedInfoEventArgs), IEquatable(Of SizeChangedEventArgs)
        ''' <summary>CTor from <see cref="SizeChangedInfo"/></summary>
        ''' <param name="SizeChangedInfo">A <see cref="SizeChangedInfo"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="SizeChangedInfo"/> is null</exception>
        Public Sub New(ByVal SizeChangedInfo As SizeChangedInfo)
            If SizeChangedInfo Is Nothing Then Throw New ArgumentNullException("SizeChangedInfo")
            _Value1 = SizeChangedInfo
        End Sub
        ''' <summary>CTor from <see cref="SizeChangedEventArgs"/></summary>
        ''' <param name="SizeChangedEventArgs">A <see cref="SizeChangedEventArgs"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="SizeChangedEventArgs"/> is null</exception>
        Private Sub New(ByVal SizeChangedEventArgs As SizeChangedEventArgs)
            If SizeChangedEventArgs Is Nothing Then Throw New ArgumentNullException("SizeChangedEventArgs")
            _Value2 = SizeChangedEventArgs
        End Sub
        ''' <summary>Contains value of the <see cref="Value"/> property. Nul when this instace was created from <see cref="SizeChangedEventArgs"/> and <see cref="Value"/> have not been got yet.</summary>
        Private _Value1 As SizeChangedInfo
        ''' <summary>If this instance was created from <see cref="SizeChangedEventArgs"/>, contains them; otherwise null</summary>
        Private ReadOnly _Value2 As SizeChangedEventArgs
        ''' <summary>Gets <see cref="SizeChangedInfo"/> this instance wraps</summary>
        ''' <returns>Instance this instance wraps when it wraps <see cref="SizeChangedInfo"/> or appropriate instance when this instance wraps <see cref="SizeChangedEventArgs"/>.</returns>
        ''' <exception cref="InvalidOperationException">This instance wraps <see cref="SizeChangedEventArgs"/> and its <see cref="SizeChangedEventArgs.OriginalSource"/> is not <see cref="UIElement"/>.</exception>
        ''' <remarks>When this instance encaplsulates <see cref="SizeChangedEventArgs"/> subsequent calls to getter of this property returns same instance of <see cref="SizeChangedInfo"/>.</remarks>
        Public ReadOnly Property Value() As SizeChangedInfo
            Get
                If _Value1 IsNot Nothing Then Return _Value1
                Try
                    _Value1 = New SizeChangedInfo(_Value2.OriginalSource, _Value2.PreviousSize, _Value2.WidthChanged, _Value2.HeightChanged)
                Catch ex As InvalidCastException
                    Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceWasCreatedFromSizeChangedEventArgsWhich, ex)
                End Try
                Return _Value1
            End Get
        End Property
#Region "Equals"
        ''' <summary>Indicates whether the current object is equal to another <see cref="SizeChangedEventArgs"/>.</summary>
        ''' <returns>true if the current object is equal to the 
        ''' <paramref name="other" /> parameter; otherwise, false.</returns>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <remarks>Instances are compared by their HeightChanged, NewSize, PreviousSize and WidthChanged properties</remarks>
        Public Overloads Function Equals(ByVal other As System.Windows.SizeChangedEventArgs) As Boolean Implements System.IEquatable(Of System.Windows.SizeChangedEventArgs).Equals
            If other Is Nothing Then Return False
            Return other.HeightChanged = HeightChanged AndAlso other.NewSize = NewSize AndAlso other.PreviousSize = PreviousSize AndAlso other.WidthChanged = WidthChanged
        End Function
        ''' <summary>Indicates whether the current object is equal to another <see cref="SizeChangedInfo"/>.</summary>
        ''' <returns>true if the current object is equal to the 
        ''' <paramref name="other" /> parameter; otherwise, false.</returns>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <remarks>Instances are compared by their HeightChanged, NewSize, PreviousSize and WidthChanged properties</remarks>
        Public Overloads Function Equals(ByVal other As System.Windows.SizeChangedInfo) As Boolean Implements System.IEquatable(Of System.Windows.SizeChangedInfo).Equals
            If other Is Nothing Then Return False
            Return other.HeightChanged = HeightChanged AndAlso other.NewSize = NewSize AndAlso other.PreviousSize = PreviousSize AndAlso other.WidthChanged = WidthChanged
        End Function
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <exception cref="T:System.NullReferenceException">The 
        ''' <paramref name="obj" /> parameter is null.</exception>
        ''' <remarks>This instance can be compared to objects of type <see cref="SizeChangedInfo"/>, <see cref="SizeChangedInfoEventArgs"/> and <see cref="SizeChangedEventArgs"/>; otherwise returns false.</remarks>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is SizeChangedInfo Then Return Equals(DirectCast(obj, SizeChangedInfo))
            If TypeOf obj Is SizeChangedInfoEventArgs Then Return Equals(DirectCast(obj, SizeChangedInfoEventArgs))
            If TypeOf obj Is SizeChangedEventArgs Then Return Equals(DirectCast(obj, SizeChangedEventArgs))
            Return MyBase.Equals(obj)
        End Function
        ''' <summary>Indicates whether the current object is equal to another <see cref="SizeChangedInfoEventArgs"/>.</summary>
        ''' <returns>true if the current object is equal to the 
        ''' <paramref name="other" /> parameter; otherwise, false.</returns>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <remarks>Instances are compared by their <see cref="HeightChanged"/>, <see cref="NewSize"/>, <see cref="PreviousSize"/> and <see cref="WidthChanged"/> properties</remarks>
        Public Overloads Function Equals(ByVal other As SizeChangedInfoEventArgs) As Boolean Implements System.IEquatable(Of SizeChangedInfoEventArgs).Equals
            If other Is Nothing Then Return False
            Return other.HeightChanged = HeightChanged AndAlso other.NewSize = NewSize AndAlso other.PreviousSize = PreviousSize AndAlso other.WidthChanged = WidthChanged
        End Function
#End Region
#Region "Lifting"
        ''' <summary>Gets a value indicating whether this <see cref="System.Windows.SizeChangedInfo" /> reports a size change that includes a significant change to the Height component.</summary>
        ''' <returns>true if there is a significant Height component change; otherwise, false.</returns>
        ''' <seelaso cref="SizeChangedInfo.HeightChanged"/><seelaso cref="SizeChangedEventArgs.HeightChanged"/>
        Public ReadOnly Property HeightChanged() As Boolean
            Get
                If _Value1 Is Nothing Then Return _Value2.HeightChanged
                Return Value.HeightChanged
            End Get
        End Property
        ''' <summary>Gets a value that declares whether the Width component of the size changed.</summary>
        ''' <seelaso cref="SizeChangedInfo.WidthChanged"/><seelaso cref="SizeChangedEventArgs.WidthChanged"/>
        Public ReadOnly Property WidthChanged() As Boolean
            Get
                If _Value1 Is Nothing Then Return _Value2.WidthChanged
                Return Value.WidthChanged
            End Get
        End Property
        ''' <summary>Gets the new size being reported.</summary>
        ''' <returns>The new size.</returns>
        ''' <seelaso cref="SizeChangedInfo.NewSize"/><seelaso cref="SizeChangedEventArgs.NewSize"/>
        Public ReadOnly Property NewSize() As Size
            Get
                If _Value1 Is Nothing Then Return _Value2.NewSize
                Return Value.NewSize
            End Get
        End Property
        ''' <summary>Gets the previous size of the size-related value being reported as changed.</summary>
        ''' <returns>The previous size.</returns>
        ''' <seelaso cref="SizeChangedInfo.PreviousSize"/><seelaso cref="SizeChangedEventArgs.PreviousSize"/>
        Public ReadOnly Property PreviousSize() As Size
            Get
                If _Value1 Is Nothing Then Return _Value2.PreviousSize
                Return Value.PreviousSize
            End Get
        End Property
#End Region
#Region "CType"
        ''' <summary>Converts <see cref="SizeChangedInfo"/> to <see cref="SizeChangedInfoEventArgs"/></summary>
        ''' <param name="a">A <see cref="SizeChangedInfo"/></param>
        ''' <returns>Instance encapsulating <paramref name="a"/>. Null when <paramref name="a"/> is null.</returns>
        Public Shared Widening Operator CType(ByVal a As SizeChangedInfo) As SizeChangedInfoEventArgs
            If a Is Nothing Then Return Nothing
            Return New SizeChangedInfoEventArgs(a)
        End Operator
        ''' <summary>Converts <see cref="SizeChangedInfoEventArgs"/> to <see cref="SizeChangedInfo"/></summary>
        ''' <param name="a">A <see cref="SizeChangedInfoEventArgs"/></param>
        ''' <returns>If <paramref name="a"/> encapsulates <see cref="SizeChangedInfo"/> returns it; otherwise (<paramref name="a"/> encapsulets <see cref="SizeChangedEventArgs"/>), new instance of <see cref="SizeChangedInfo"/> is created from it or precreated instance is returned. Null when <paramref name="a"/> is null.</returns>
        ''' <exception cref="InvalidOperationException"><paramref name="a"/> encapsulates <see cref="SizeChangedInfoEventArgs"/> and its <see cref="SizeChangedEventArgs.OriginalSource"/> is not <see cref="UIElement"/></exception>
        ''' <seelaso cref="Value"/>
        Public Shared Narrowing Operator CType(ByVal a As SizeChangedInfoEventArgs) As SizeChangedInfo
            If a Is Nothing Then Return Nothing
            Return a.Value
        End Operator
        ''' <summary>Converts <see cref="SizeChangedInfoEventArgs"/> to <see cref="SizeChangedInfoEventArgs"/></summary>
        ''' <param name="a">A <see cref="SizeChangedInfoEventArgs"/></param>
        ''' <returns>Instance encapsulating <paramref name="a"/>.  Null when <paramref name="a"/> is null.</returns>
        Public Shared Widening Operator CType(ByVal a As SizeChangedEventArgs) As SizeChangedInfoEventArgs
            If a Is Nothing Then Return Nothing
            Return New SizeChangedInfoEventArgs(a)
        End Operator
        ''' <summary>Atemts to convert <see cref="SizeChangedInfoEventArgs"/> to <see cref="SizeChangedEventArgs"/></summary>
        ''' <param name="a">A <see cref="SizeChangedInfoEventArgs"/></param>
        ''' <returns><see cref="SizeChangedEventArgs"/> encapsulated by <paramref name="a"/>.  Null when <paramref name="a"/> is null.</returns>
        ''' <exception cref="InvalidOperationException"><paramref name="a"/> does not encapsulate <see cref="SizeChangedEventArgs"/>.</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Narrowing Operator CType(ByVal a As SizeChangedInfoEventArgs) As SizeChangedEventArgs
            If a Is Nothing Then Return Nothing
            If a._Value2 IsNot Nothing Then Return a._Value2
            Throw New InvalidOperationException(ResourcesT.Exceptions.GivenInstanceWasNotCreatedFrom0.f("SizeChangedEventArgs"))
        End Operator
#End Region
    End Class
End Namespace
#End If