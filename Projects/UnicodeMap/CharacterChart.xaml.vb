Imports Tools.ExtensionsT, Tools.LinqT
Imports Tools

Public Class CharacterChart
    Private Const MaxChar As UInteger = &H10FFFFUI

    Public Sub New()
        InitializeComponent()
        SetDataSource()
        itmHeader.ItemsSource = New ForLoopCollection(Of String)(0, &HF, Function(i) i.ToString("X"))
    End Sub


#Region "FirstCharacter"
    ''' <summary>Gets or sets first character shown in this control</summary>
    Public Property FirstCharacter() As UInteger
        <DebuggerStepThrough()> Get
            Return GetValue(FirstCharacterProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As UInteger)
            SetValue(FirstCharacterProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="FirstCharacter"/> dependency property</summary>
    Public Shared ReadOnly FirstCharacterProperty As DependencyProperty =
                           DependencyProperty.Register("FirstCharacter", GetType(UInteger), GetType(CharacterChart),
                           New FrameworkPropertyMetadata(0UI, AddressOf OnFirstCharacterChanged, AddressOf CoerceFirstCharacterValue))
    ''' <summary>Called when value of the <see cref="FirstCharacter"/> property changes for any <see cref="characterchart"/></summary>
    ''' <param name="d">A <see cref="characterchart"/> <see cref="FirstCharacter"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="characterchart"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnFirstCharacterChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharacterChart Then Throw New TypeMismatchException("d", d, GetType(CharacterChart))
        DirectCast(d, CharacterChart).OnFirstCharacterChanged(e)
    End Sub
    ''' <summary>Called whan value of the <see cref="FirstCharacter"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnFirstCharacterChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        SetDataSource()
    End Sub
    ''' <summary>Called whenever a value of the <see cref="FirstCharacter"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="d">The object that the property exists on. When the callback is invoked, the property system passes this value.</param>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not of type <see cref="characterchart"/> -or- <paramref name="baseValue"/> is not of type <see cref="uinteger"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    Private Shared Function CoerceFirstCharacterValue(ByVal d As System.Windows.DependencyObject, ByVal baseValue As Object) As Object
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharacterChart Then Throw New TypeMismatchException("d", d, GetType(CharacterChart))
        If Not TypeOf baseValue Is UInteger AndAlso Not baseValue Is Nothing Then Throw New TypeMismatchException("baseValue", baseValue, GetType(UInteger))
        Return DirectCast(d, CharacterChart).CoerceFirstCharacterValue(baseValue)
    End Function
    ''' <summary>Called whenever a value of the <see cref="FirstCharacter"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt, but ensured to be of correct type.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    Protected Overridable Function CoerceFirstCharacterValue(ByVal baseValue As UInteger) As UInteger
        Return Math.Max(baseValue, LastCharacter)
    End Function
#End Region

#Region "LastCharacter"
    ''' <summary>Gets or sets last character shown in this control</summary>
    Public Property LastCharacter() As UInteger
        <DebuggerStepThrough()> Get
            Return GetValue(LastCharacterProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As UInteger)
            SetValue(LastCharacterProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="LastCharacter"/> dependency property</summary>
    Public Shared ReadOnly LastCharacterProperty As DependencyProperty =
                           DependencyProperty.Register("LastCharacter", GetType(UInteger), GetType(CharacterChart),
                           New FrameworkPropertyMetadata(MaxChar, AddressOf OnLastCharacterChanged, AddressOf CoerceLastCharacterValue))
    ''' <summary>Called when value of the <see cref="LastCharacter"/> property changes for any <see cref="characterchart"/></summary>
    ''' <param name="d">A <see cref="characterchart"/> <see cref="LastCharacter"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="characterchart"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnLastCharacterChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharacterChart Then Throw New TypeMismatchException("d", d, GetType(CharacterChart))
        DirectCast(d, CharacterChart).OnLastCharacterChanged(e)
    End Sub
    ''' <summary>Called whan value of the <see cref="LastCharacter"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnLastCharacterChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        SetDataSource()
    End Sub
    ''' <summary>Called whenever a value of the <see cref="LastCharacter"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="d">The object that the property exists on. When the callback is invoked, the property system passes this value.</param>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not of type <see cref="characterchart"/> -or- <paramref name="baseValue"/> is not of type <see cref="uinteger"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    Private Shared Function CoerceLastCharacterValue(ByVal d As System.Windows.DependencyObject, ByVal baseValue As Object) As Object
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharacterChart Then Throw New TypeMismatchException("d", d, GetType(CharacterChart))
        If Not TypeOf baseValue Is UInteger AndAlso Not baseValue Is Nothing Then Throw New TypeMismatchException("baseValue", baseValue, GetType(UInteger))
        Return DirectCast(d, CharacterChart).CoerceLastCharacterValue(baseValue)
    End Function
    ''' <summary>Called whenever a value of the <see cref="LastCharacter"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt, but ensured to be of correct type.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    Protected Overridable Function CoerceLastCharacterValue(ByVal baseValue As UInteger) As UInteger
        Return Math.Min(Math.Min(baseValue, FirstCharacter), MaxChar)
    End Function
#End Region

#Region "CharSize"
    ''' <summary>Gets or sets size of characters</summary>
    Public Property CharSize As Double
        Get
            Return GetValue(CharSizeProperty)
        End Get
        Set(ByVal value As Double)
            SetValue(CharSizeProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="Size"/> dependency property</summary>
    Public Shared ReadOnly CharSizeProperty As DependencyProperty =
        DependencyProperty.Register("CharSize", GetType(Double), GetType(CharacterChart), New FrameworkPropertyMetadata(32.0#))
#End Region

#Region "CharFontFamily"
    ''' <summary>Gets or sets Font Family used for characters in grid</summary>      
    Public Property CharFontFamily As FontFamily
        Get
            Return GetValue(CharFontFamilyProperty)
        End Get
        Set(ByVal value As FontFamily)
            SetValue(CharFontFamilyProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="CharFontFamily"/> dependency property</summary>                                                   
    Public Shared ReadOnly CharFontFamilyProperty As DependencyProperty =
        DependencyProperty.Register("CharFontFamily", GetType(FontFamily), GetType(CharacterChart), New FrameworkPropertyMetadata(Nothing))
#End Region

#Region "CharFontStretch"
    ''' <summary>Gets or sets font stretch used for characters in grid</summary>      
    Public Property CharFontStretch As FontStretch
        Get
            Return GetValue(CharFontStretchProperty)
        End Get
        Set(ByVal value As FontStretch)
            SetValue(CharFontStretchProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="CharFontStretch"/> dependency property</summary>                                                   
    Public Shared ReadOnly CharFontStretchProperty As DependencyProperty =
        DependencyProperty.Register("CharFontStretch", GetType(FontStretch), GetType(CharacterChart), New FrameworkPropertyMetadata(Nothing))
#End Region

#Region "CharFontStyle"
    ''' <summary>Gets or sets font style used for characters in grid</summary>      
    Public Property CharFontStyle As FontStyle
        Get
            Return GetValue(CharFontStyleProperty)
        End Get
        Set(ByVal value As FontStyle)
            SetValue(CharFontStyleProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="CharFontStyle"/> dependency property</summary>                                                   
    Public Shared ReadOnly CharFontStyleProperty As DependencyProperty =
        DependencyProperty.Register("CharFontStyle", GetType(FontStyle), GetType(CharacterChart), New FrameworkPropertyMetadata(Nothing))
#End Region

#Region "CharFontWeight"
    ''' <summary>Gets or sets fint weight used for characters in grid</summary>      
    Public Property CharFontWeight As FontWeight
        Get
            Return GetValue(CharFontWeightProperty)
        End Get
        Set(ByVal value As FontWeight)
            SetValue(CharFontWeightProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="CharFontWeight"/> dependency property</summary>                                                   
    Public Shared ReadOnly CharFontWeightProperty As DependencyProperty =
        DependencyProperty.Register("CharFontWeight", GetType(FontWeight), GetType(CharacterChart), New FrameworkPropertyMetadata(Nothing))
#End Region


    ''' <summary>Sets data source of list of rows</summary>
    Private Sub SetDataSource()
        itmContent.ItemsSource = New RowsDataSource(FirstCharacter, LastCharacter)
    End Sub


#Region "Helper classes"

    ''' <summary>Virtual collection of all rows shown in control</summary>
    Private NotInheritable Class RowsDataSource
        Implements ICollection(Of RowInfo)

        ''' <summary>CTor - creates a new instance of the <see cref="RowsDataSource"/> class</summary>
        ''' <param name="firstCharacter">First character in range</param>
        ''' <param name="lastCharacter">Last character in range</param>
        ''' <exception cref="ArgumentException"><paramref name="lastCharacter"/> is less than <paramref name="firstCharacter"/></exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="lastCharacter"/> is greater than 0x10FFFF</exception>
        Public Sub New(firstCharacter As UInteger, lastCharacter As UInteger)
            If lastCharacter < firstCharacter Then Throw New ArgumentException("Last character must be greater than or equal to first character.")
            If lastCharacter > MaxChar Then Throw New ArgumentOutOfRangeException("lastCharacter")
            _firstCharacter = firstCharacter
            _lastCharacter = lastCharacter
        End Sub

        Private ReadOnly _firstCharacter As UInteger
        Private ReadOnly _lastCharacter As UInteger

        ''' <summary>Gets first character in range</summary>
        Public ReadOnly Property FirstCharacter() As UInteger
            Get
                Return _firstCharacter
            End Get
        End Property

        ''' <summary>Gets last character in range</summary>
        Public ReadOnly Property LastCharacter() As UInteger
            Get
                Return _lastCharacter
            End Get
        End Property

        ''' <summary>Gets first character in first row of range (that is <see cref="FirstCharacter"/> rounded down to 16)</summary>
        Public ReadOnly Property ActualFirstCharacter As UInteger
            Get
                Return FirstCharacter \ 16UI * 16UI
            End Get
        End Property
        ''' <summary>Gest last character in last row of range (that is <see cref="LastCharacter"/> rounded down to 16 plus 15)</summary>
        Public ReadOnly Property ActualLastCharacter As UInteger
            Get
                Return LastCharacter \ 16UI * 16UI + 15UI
            End Get
        End Property

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Function GetEnumerator() As IEnumerator(Of RowInfo) Implements IEnumerable(Of RowInfo).GetEnumerator
            Return New RowsDataSourceEnumerator(Me)
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function


        ''' <summary>Gets virtual number of characters contained in the <see cref="RowsDataSource" />.</summary>
        Public ReadOnly Property Count As Integer Implements ICollection(Of RowInfo).Count
            Get
                Return (ActualLastCharacter - ActualFirstCharacter) / 16UI
            End Get
        End Property

#Region "Not suppoprted"
        Private Sub Add(item As RowInfo) Implements ICollection(Of RowInfo).Add
            Throw New NotSupportedException("The collection is read-only.")
        End Sub

        Private Sub Clear() Implements ICollection(Of RowInfo).Clear
            Throw New NotSupportedException("The collection is read-only.")
        End Sub
        Private Function Remove(item As RowInfo) As Boolean Implements ICollection(Of RowInfo).Remove
            Throw New NotSupportedException("The collection is read-only.")
        End Function
        Private ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of RowInfo).IsReadOnly
            Get
                Return True
            End Get
        End Property
        Private Function Contains(item As RowInfo) As Boolean Implements ICollection(Of RowInfo).Contains
            Throw New NotSupportedException
        End Function

        Private Sub CopyTo(array() As RowInfo, arrayIndex As Integer) Implements ICollection(Of RowInfo).CopyTo
            Throw New NotSupportedException
        End Sub
#End Region
    End Class

    ''' <summary>Enumerator over <see cref="RowsDataSource"/></summary>
    Private NotInheritable Class RowsDataSourceEnumerator
        Implements IEnumerator(Of RowInfo)
        ''' <summary><see cref="RowsDataSource"/> this instance enumerates over</summary>
        Private owner As RowsDataSource
        ''' <summary>First character in current row</summary>
        Private row As UInteger?
        ''' <summary>CTor - creates a new instance of the <see cref="RowsDataSourceEnumerator"/> class</summary>
        ''' <param name="owner">A <see cref="RowsDataSource"/> to enumerate over</param>
        ''' <exception cref="ArgumentNullException"><paramref name="owner"/> is nulll</exception>
        Public Sub New(owner As RowsDataSource)
            If owner Is Nothing Then Throw New ArgumentNullException("owner")
            Me.owner = owner
        End Sub
        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        ''' <exception cref="ObjectDisposedException">This instance was disposed</exception>
        Public ReadOnly Property Current As RowInfo Implements IEnumerator(Of RowInfo).Current
            Get
                If owner Is Nothing Then Throw New ObjectDisposedException([GetType].Name)
                If row Is Nothing Then Throw New InvalidOperationException("Iteration didn't started yet.")
                If row > owner.LastCharacter Then Throw New InvalidOperationException("Iteration has already ended.")
                Return New RowInfo(row, owner)
            End Get
        End Property

        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        ''' <exception cref="ObjectDisposedException">This instance was disposed</exception>
        Private ReadOnly Property IEnumerator_Current As Object Implements IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        ''' <summary>Advances the enumerator to the next element of the collection.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        ''' <exception cref="ObjectDisposedException">This instance was disposed</exception>
        ''' <filterpriority>2</filterpriority>
        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            If owner Is Nothing Then Throw New ObjectDisposedException([GetType].Name)
            If row Is Nothing Then
                row = owner.FirstCharacter
            Else
                row += 16UI
            End If
            Return row <= owner.LastCharacter
        End Function

        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        ''' <exception cref="ObjectDisposedException">This instance was disposed</exception>
        ''' <filterpriority>2</filterpriority>
        Public Sub Reset() Implements IEnumerator.Reset
            If owner Is Nothing Then Throw New ObjectDisposedException([GetType].Name)
            row = Nothing
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <filterpriority>2</filterpriority>
        Public Sub Dispose() Implements IDisposable.Dispose
            owner = Nothing
        End Sub
    End Class

    ''' <summary>Provides data for single row in grid</summary>
    Private NotInheritable Class RowInfo
        Private ReadOnly _row As UInteger
        ''' <summary>Gets first character in row</summary>
        ''' <remarks>This value is always multiple of 16</remarks>
        Public ReadOnly Property Row() As UInteger
            Get
                Return _row
            End Get
        End Property
        ''' <summary>A <see cref="RowsDataSource"/> this instance was generated for</summary>
        Private ReadOnly owner As RowsDataSource
        ''' <summary>CTor - creates a new instance of the <see cref="RowInfo"/> class</summary>
        ''' <param name="row">First character in row. Must be multiple of 16</param>
        ''' <param name="owner">An owning <see cref="RowsDataSource"/></param>
        ''' <exception cref="ArgumentException"><paramref name="row"/> is not multiple of 16</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="row"/> is greater than 0x10FFFF</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="owner"/> is null</exception>
        Public Sub New(row As UInteger, owner As RowsDataSource)
            If row Mod 16UI <> 0 Then Throw New ArgumentException("Value must be mupltipe of 16.", "row")
            If owner Is Nothing Then Throw New ArgumentNullException("owner")
            If row > MaxChar Then Throw New ArgumentOutOfRangeException("row")
            Me._row = row
            Me.owner = owner
            _chars = New ForLoopCollection(Of CharInfo, UInteger)(Function() row, Function(i) i < row + 16UI, Function(i) i + 1UI, Function(i) New CharInfo(i, owner)).ToArray
        End Sub
        Private _chars As CharInfo()
        ''' <summary>Gets characters in current wor</summary>
        Public ReadOnly Property Chars As CharInfo()
            Get
                Return _chars
            End Get
        End Property
        ''' <summary>Gets <see cref="Row"/> in hex</summary>
        Public ReadOnly Property RowHex As String
            Get
                Return Row.ToString("X4")
            End Get
        End Property
    End Class

    ''' <summary>Provided data for single character</summary>
    Private NotInheritable Class CharInfo
        ''' <summary>An owning <see cref="RowsDataSource"/> this instance was generated for</summary>
        Private ReadOnly owner As RowsDataSource
        Private ReadOnly _char As UInteger
        ''' <summary>CTor - creates a new instance of the <see cref="CharInfo"/> class</summary>
        ''' <param name="char">The character</param>
        ''' <param name="owner">Oa owning <see cref="RowsDataSource"/></param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="char"/> is greater than 0x10FFFF</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="owner"/> is null</exception>
        Public Sub New([char] As UInteger, owner As RowsDataSource)
            If [char] > MaxChar Then Throw New ArgumentOutOfRangeException("char")
            If owner Is Nothing Then Throw New ArgumentNullException("owner")
            Me._char = [char]
            Me.owner = owner
        End Sub
        ''' <summary>Gets Unicode code of the character</summary>
        Public ReadOnly Property [Char] As UInteger
            Get
                Return [_char]
            End Get
        End Property
        ''' <summary>Gets Unicod code of the character in Hex</summary>
        Public ReadOnly Property CharHex$
            Get
                Return [Char].ToString("X4")
            End Get
        End Property
        ''' <summary>Gets value indicating if current character is in range of characters selected for <see cref="RowsDataSource"/>.</summary>
        Public ReadOnly Property IsInRange As Boolean
            Get
                Return [Char] >= owner.FirstCharacter AndAlso [Char] <= owner.LastCharacter
            End Get
        End Property

        ''' <summary>Gets string representing current character</summary>
        Public ReadOnly Property Text$
            Get
                If [Char] > &HD800UI AndAlso [Char] < &HDFFFUI Then Return Nothing 'Surrogates
                Return Char.ConvertFromUtf32([Char].BitwiseSame)
            End Get
        End Property
    End Class
#End Region

    Private Sub CharButton_Click(sender As Button, e As System.Windows.RoutedEventArgs)
 
    End Sub
End Class
