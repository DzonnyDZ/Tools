#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.InteropT
    ''' <summary>Wraps <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> as clas derived from <see cref="EventArgs"/></summary>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version stage="Nightly" version="1.5.2">Class introduced</version>
    Public Class DependencyPropertyChangedEventArgsEventArgs
        Inherits EventArgs
        Implements IEquatable(Of DependencyPropertyChangedEventArgsEventArgs), IEquatable(Of System.Windows.DependencyPropertyChangedEventArgs)
        ''' <summary>CTor</summary>
        ''' <param name="DependencyPropertyChangedEventArgs">Instance to be wraped</param>
        Public Sub New(ByVal DependencyPropertyChangedEventArgs As System.Windows.DependencyPropertyChangedEventArgs)
            Me.e = DependencyPropertyChangedEventArgs
        End Sub
        ''' <summary>Instance being wrapped</summary>
        Public ReadOnly e As System.Windows.DependencyPropertyChangedEventArgs
        ''' <summary>Converts <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> to <see cref="DependencyPropertyChangedEventArgsEventArgs"/></summary>
        ''' <param name="a">A <see cref="System.Windows.DependencyPropertyChangedEventArgs"/></param> 
        ''' <returns>New instance of <see cref="DependencyPropertyChangedEventArgsEventArgs"/> wrapping <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As System.Windows.DependencyPropertyChangedEventArgs) As DependencyPropertyChangedEventArgsEventArgs
            Return New DependencyPropertyChangedEventArgsEventArgs(a)
        End Operator
        ''' <summary>Converts <see cref="DependencyPropertyChangedEventArgsEventArgs"/> to <see cref="System.Windows.DependencyPropertyChangedEventArgs"/></summary>
        ''' <param name="a">A <see cref="DependencyPropertyChangedEventArgsEventArgs"/></param>
        ''' <returns><see cref="System.Windows.DependencyPropertyChangedEventArgs"/> wraped by <paramref name="a"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
        Public Shared Widening Operator CType(ByVal a As DependencyPropertyChangedEventArgsEventArgs) As System.Windows.DependencyPropertyChangedEventArgs
            If a Is Nothing Then Throw New ArgumentNullException("a")
            Return a.e
        End Operator
#Region "Lifting"
        ''' <summary>Gets the value of the property before the change.</summary>
        ''' <returns>The property value before the change.</returns>
        ''' <seelaso cref="System.Windows.DependencyPropertyChangedEventArgs.OldValue"/>
        Public ReadOnly Property OldValue() As Object
            Get
                Return e.OldValue
            End Get
        End Property
        ''' <summary>Gets the value of the property before the change.</summary>
        ''' <returns>The property value before the change.</returns>
        ''' <seelaso cref="System.Windows.DependencyPropertyChangedEventArgs.NewValue"/>
        Public ReadOnly Property NewValue() As Object
            Get
                Return e.NewValue
            End Get
        End Property
        ''' <summary>Gets the identifier for the dependency property where the value change occurred.</summary>
        ''' <returns>The identifier field of the dependency property where the value change occurred.</returns>
        ''' <seelaso cref="System.Windows.DependencyPropertyChangedEventArgs.[Property]"/>
        Public ReadOnly Property [Property]() As System.Windows.DependencyProperty
            Get
                Return e.Property
            End Get
        End Property
#End Region
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="DependencyPropertyChangedEventArgsEventArgs" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="DependencyPropertyChangedEventArgsEventArgs" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="DependencyPropertyChangedEventArgsEventArgs" />. </param>
        ''' <remarks><see cref="DependencyPropertyChangedEventArgsEventArgs"/> can be compared to <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> and to <see cref="DependencyPropertyChangedEventArgsEventArgs"/>. Comparison with other types returns false.</remarks>
        ''' <filterpriority>2</filterpriority>
        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is System.Windows.DependencyPropertyChangedEventArgs Then Return Me.Equals(DirectCast(obj, System.Windows.DependencyPropertyChangedEventArgs))
            If TypeOf obj Is DependencyPropertyChangedEventArgsEventArgs Then Return Me.Equals(DirectCast(obj, DependencyPropertyChangedEventArgsEventArgs))
            Return MyBase.Equals(obj)
        End Function
        ''' <summary>Indicates whether the current object is equal to another object of the type <see cref="System.Windows.DependencyPropertyChangedEventArgs"/>.</summary>
        ''' <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false. It true if <paramref name="other"/> equals to <see cref="e"/>.</returns>
        ''' <param name="other">An object to compare with this object.</param>
        Public Overloads Function Equals(ByVal other As System.Windows.DependencyPropertyChangedEventArgs) As Boolean Implements System.IEquatable(Of System.Windows.DependencyPropertyChangedEventArgs).Equals
            Return other = e
        End Function

        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false. It is true if <paramref name="other"/>.<see cref="e">e</see> equals to <see cref="e"/>.</returns>
        ''' <param name="other">An object to compare with this object.</param>
        Public Overloads Function Equals(ByVal other As DependencyPropertyChangedEventArgsEventArgs) As Boolean Implements System.IEquatable(Of DependencyPropertyChangedEventArgsEventArgs).Equals
            Return other.e = e
        End Function
        ''' <summary>Compares <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> and <see cref="DependencyPropertyChangedEventArgsEventArgs"/> for equality</summary>
        ''' <returns>true when <paramref name="a"/> equals to <paramref name="b"/></returns>
        ''' <param name="a">A System.Windows.DependencyPropertyChangedEventArgs</param>
        ''' <param name="b">A DependencyPropertyChangedEventArgsEventArgs</param>
        ''' <seelaso cref="Equals"/>
        Public Shared Operator =(ByVal a As System.Windows.DependencyPropertyChangedEventArgs, ByVal b As DependencyPropertyChangedEventArgsEventArgs) As Boolean
            If b Is Nothing Then Return False
            Return b.Equals(a)
        End Operator
        ''' <summary>Compares <see cref="DependencyPropertyChangedEventArgsEventArgs"/> and <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> for equality</summary>
        ''' <returns>true when <paramref name="b"/> equals to <paramref name="a"/></returns>
        ''' <param name="a">A DependencyPropertyChangedEventArgsEventArgs</param>
        ''' <param name="b">A System.Windows.DependencyPropertyChangedEventArgs</param>
        ''' <seelaso cref="Equals"/>
        Public Shared Operator =(ByVal a As DependencyPropertyChangedEventArgsEventArgs, ByVal b As System.Windows.DependencyPropertyChangedEventArgs) As Boolean
            If a Is Nothing Then Return False
            Return a.Equals(b)
        End Operator
        ''' <summary>Compares two <see cref="DependencyPropertyChangedEventArgsEventArgs"/> objects for equality</summary>
        ''' <returns>true when <paramref name="a"/> equals to <paramref name="b"/></returns>
        ''' <param name="a">A <see cref="DependencyPropertyChangedEventArgsEventArgs"/></param>
        ''' <param name="b">A <see cref="DependencyPropertyChangedEventArgsEventArgs"/></param>
        ''' <seelaso cref="Equals"/>
        Public Shared Operator =(ByVal a As DependencyPropertyChangedEventArgsEventArgs, ByVal b As DependencyPropertyChangedEventArgsEventArgs) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing Xor b Is Nothing Then Return False
            Return a.Equals(b)
        End Operator
        ''' <summary>Compares <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> and <see cref="DependencyPropertyChangedEventArgsEventArgs"/> for inequality</summary>
        ''' <returns>false when <paramref name="a"/> equals to <paramref name="b"/></returns>
        ''' <param name="a">A System.Windows.DependencyPropertyChangedEventArgs</param>
        ''' <param name="b">A DependencyPropertyChangedEventArgsEventArgs</param>
        ''' <seelaso cref="Equals"/>
        Public Shared Operator <>(ByVal a As System.Windows.DependencyPropertyChangedEventArgs, ByVal b As DependencyPropertyChangedEventArgsEventArgs) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Compares <see cref="DependencyPropertyChangedEventArgsEventArgs"/> and <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> for inequality</summary>
        ''' <returns>false when <paramref name="b"/> equals to <paramref name="a"/></returns>
        ''' <param name="a">A DependencyPropertyChangedEventArgsEventArgs</param>
        ''' <param name="b">A System.Windows.DependencyPropertyChangedEventArgs</param>
        ''' <seelaso cref="Equals"/>
        Public Shared Operator <>(ByVal a As DependencyPropertyChangedEventArgsEventArgs, ByVal b As System.Windows.DependencyPropertyChangedEventArgs) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Compares two <see cref="DependencyPropertyChangedEventArgsEventArgs"/> objects for inequality</summary>
        ''' <returns>false when <paramref name="a"/> equals to <paramref name="b"/></returns>
        ''' <param name="a">A <see cref="DependencyPropertyChangedEventArgsEventArgs"/></param>
        ''' <param name="b">A <see cref="DependencyPropertyChangedEventArgsEventArgs"/></param>
        ''' <seelaso cref="Equals"/>
        Public Shared Operator <>(ByVal a As DependencyPropertyChangedEventArgsEventArgs, ByVal b As DependencyPropertyChangedEventArgsEventArgs) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Gets a hash code for <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> being wrapped.</summary>
        ''' <returns>A signed 32-bit integer hash code.</returns>
        Public Overrides Function GetHashCode() As Integer
            Return e.GetHashCode()
        End Function
    End Class
End Namespace
#End If