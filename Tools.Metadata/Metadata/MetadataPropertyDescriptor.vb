Namespace MetadataT
#If True
    ''' <summary>Descripbes a metadata property</summary>
    ''' <typeparam name="TOwner">Type of <see cref="IMetadata"/> that constructed type provides metadata for</typeparam>
    ''' <remarks>This is a helper class used internally by some simplier <see cref="IMetadata"/> implementations</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class MetadataPropertyDescriptor(Of TOwner As {IMetadata, Class})
        Private _valueGetter As Func(Of TOwner, Object)
        ''' <summary>Gets or sets a method that gets metadata value</summary>
        ''' <remarks>Argument of the method is metadata instance. Return value of the method is metadata value.</remarks>                       
        ''' <exception cref="Exception">Delegate being set is rejected by derived class (see <see cref="SetValueGetterImpl"/>)</exception>
        Public Property ValueGetter As Func(Of TOwner, Object)
            Get
                Return _valueGetter
            End Get
            Set(value As Func(Of TOwner, Object))
                SetValueGetterImpl(value)
            End Set
        End Property
        ''' <summary>When overriden in derived class validates that delegate passed to setter of the <see cref="ValueGetter"/> property is acceptable by derived class.</summary>
        ''' <param name="value">Proposed new value of the <see cref="ValueGetter"/> property</param>
        ''' <remarks>
        ''' If validation fails throw an exception. If validation succeeds call base class method.
        ''' <para>This implementation does no validation. It accepts any value and just stores it in backing field for the <see cref="ValueGetter"/> property</para>
        ''' </remarks>
        ''' <exception cref="Exception">When overriden by derived class: <paramref name="value"/> is regected by derived class</exception>
        Protected Overridable Sub SetValueGetterImpl(value As Func(Of TOwner, Object))
            _valueGetter = value
        End Sub
        ''' <summary>Gets or sets key of this metadata item</summary>
        Public Property Key As String
        ''' <summary>Gets or sets delagate that returns human-friendly name of this property</summary>
        Public Property HumanNameGetter As Func(Of String)
        ''' <summary>Gets or sets delegate that returns human-readable description of this property</summary>
        Public Property DescriptionGetter As Func(Of String)

        ''' <summary>CTor - creates a new instance of the <see cref="MetadataPropertyDescriptor(Of TOwner)"/> class</summary>
        ''' <param name="key">Key of metadata property</param>
        ''' <param name="valueGetter">Delegate for getting value of metadata property</param>
        ''' <param name="humanNameGetter">Delegate for getting human-friendly name of the property</param>
        ''' <param name="descriptionGetter">Delegate for getting human-readable description of the property</param>
        Public Sub New(key$, valueGetter As Func(Of TOwner, Object), Optional humanNameGetter As Func(Of String) = Nothing, Optional descriptionGetter As Func(Of String) = Nothing)
            Me.Key = key
            Me.ValueGetter = valueGetter
            Me.HumanNameGetter = humanNameGetter
            Me.DescriptionGetter = descriptionGetter
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="MetadataPropertyDescriptor(Of TOwner)"/> class without passing <see cref="ValueGetter"/> delegate</summary>
        ''' <param name="key">Key of metadata property</param>
        ''' <param name="humanNameGetter">Delegate for getting human-friendly name of the property</param>
        ''' <param name="descriptionGetter">Delegate for getting human-readable description of the property</param>
        ''' <remarks>The <see cref="ValueGetter"/> proeprty of newly created instance should be set ASAP after creating a new instance using this CTor - ideally in derived class CTor.</remarks>
        Protected Sub New(key$, humanNameGetter As Func(Of String), descriptionGetter As Func(Of String))
            Me.Key = key
            Me.HumanNameGetter = humanNameGetter
            Me.DescriptionGetter = descriptionGetter
        End Sub

        ''' <summary>Invokes <see cref="ValueGetter"/> to get property value</summary>
        ''' <param name="owner"><see cref="IMetadata"/> instance to get value for</param>
        ''' <returns>Metadata value for property identified by <see cref="Key"/> of <paramref name="owner"/>. Null if <see cref="ValueGetter"/> is null</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="owner"/> is null</exception>
        Public Function GetValue(owner As TOwner) As Object
            If owner Is Nothing Then Throw New ArgumentNullException("owner")
            If ValueGetter Is Nothing Then Return Nothing
            Return ValueGetter(owner)
        End Function

        ''' <summary>Invokes <see cref="HumanNameGetter"/> to get human-friendly name of this property</summary>
        ''' <returns>Human-friendly name of property identified by <see cref="Key"/>. Null if <see cref="HumanNameGetter"/> isnull.</returns>
        Public ReadOnly Property HumanName As String
            Get
                If HumanNameGetter Is Nothing Then Return Nothing
                Return HumanNameGetter()()
            End Get
        End Property

        ''' <summary>Invokes <see cref="DescriptionGetter"/> to get human-readable description of this property</summary>
        ''' <returns>Human-readable description of property identified by <see cref="Key"/>. Null if <see cref="DescriptionGetter"/> isnull.</returns>
        Public ReadOnly Property Description As String
            Get
                If DescriptionGetter Is Nothing Then Return Nothing
                Return DescriptionGetter()()
            End Get
        End Property
    End Class

    ''' <summary>Describes a type-safe metadata property</summary>
    ''' <typeparam name="TOwner">Type of <see cref="IMetadata"/> that constructed type provides metadata for</typeparam>
    ''' <typeparam name="TValue">Type of property value</typeparam>
    ''' <remarks>This is a helper class used internally by some simplier <see cref="IMetadata"/> implementations</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class MetadataPropertyDescriptor(Of TOwner As {IMetadata, Class}, TValue)
        Inherits MetadataPropertyDescriptor(Of TOwner)

        ''' <summary>CTor - creates a new instance of the <see cref="MetadataPropertyDescriptor(Of TOwner)"/> class</summary>
        ''' <param name="key">Key of metadata property</param>
        ''' <param name="valueGetter">Delegate for getting value of metadata property</param>
        ''' <param name="humanNameGetter">Delegate for getting human-friendly name of the property</param>
        ''' <param name="descriptionGetter">Delegate for getting human-readable description of the property</param>
        Public Sub New(key$, valueGetter As Func(Of TOwner, TValue), Optional humanNameGetter As Func(Of String) = Nothing, Optional descriptionGetter As Func(Of String) = Nothing)
            MyBase.New(key, humanNameGetter, descriptionGetter)
            Me.ValueGetter = valueGetter
        End Sub

        ''' <summary>Validates that delegate passed to setter of the <see cref="ValueGetter"/> property is acceptable by this class.</summary>
        ''' <param name="value">Proposed new value of the <see cref="ValueGetter"/> property</param>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is not acceptable as <see cref="Func(Of TOwner, TValue)"/>[<typeparamref name="TOwner"/>, <typeparamref name="TValue"/>]</exception>
        Protected Overrides Sub SetValueGetterImpl(value As System.Func(Of TOwner, Object))
            If value IsNot Nothing Then
                If Not GetType(Func(Of TOwner, TValue)).IsAssignableFrom(value.GetType) Then Throw New TypeMismatchException(value, "value", GetType(Func(Of TOwner, TValue)))
            End If
            MyBase.SetValueGetterImpl(value)
        End Sub

        ''' <summary>Gets or sets a method that gets metadata value</summary>
        ''' <remarks>Argument of the method is metadata instance. Return value of the method is metadata value.</remarks>
        Public Shadows Property ValueGetter As Func(Of TOwner, TValue)
            Get
                If MyBase.ValueGetter Is Nothing Then Return Nothing
                Return Function(owner) MyBase.ValueGetter(owner)
            End Get
            Set(value As Func(Of TOwner, TValue))
                If value Is Nothing Then
                    MyBase.SetValueGetterImpl(Nothing)
                Else
                    MyBase.SetValueGetterImpl(Function(owner) value(owner))
                End If
            End Set
        End Property

        ''' <summary>Invokes <see cref="ValueGetter"/> to get property value</summary>
        ''' <param name="owner"><see cref="IMetadata"/> instance to get value for</param>
        ''' <returns>Metadata value for property identified by <see cref="Key"/> of <paramref name="owner"/>. Null if <see cref="ValueGetter"/> is null</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="owner"/> is null</exception>
        Public Shadows Function GetValue(owner As TOwner) As TValue
            If owner Is Nothing Then Throw New ArgumentNullException("owner")
            If ValueGetter Is Nothing Then Return Nothing
            Return ValueGetter(owner)
        End Function
    End Class

#End If
End Namespace