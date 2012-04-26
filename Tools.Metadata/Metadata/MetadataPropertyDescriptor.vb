Namespace MetadataT
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Descripbes a metadata property</summary>
    ''' <remarks>This is a helper class used internally by some simplier <see cref="IMetadata"/> implementations</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class MetadataPropertyDescriptor(Of TOwner As IMetadata)
        Private _valueGetter As Func(Of TOwner, Object)
        Public Property ValueGetter As Func(Of TOwner, Object)
            Get
                Return _valueGetter
            End Get
            Set(value As Func(Of TOwner, Object))
                SetValueGetterImpl(value)
            End Set
        End Property
        Protected Overridable Sub SetValueGetterImpl(value As Func(Of TOwner, Object))
            _valueGetter = value
        End Sub
        Public Property Key As String
        Public Property HumanNameGetter As Func(Of String)
        Public Property DescriptionGetter As Func(Of String)

        Public Sub New(key$, valueGetter As Func(Of TOwner, Object), Optional humanNameGetter As Func(Of String) = Nothing, Optional descriptionGetter As Func(Of String) = Nothing)
            Me.Key = key
            Me.ValueGetter = valueGetter
            Me.HumanNameGetter = humanNameGetter
            Me.DescriptionGetter = descriptionGetter
        End Sub

        Public Function GetValue(owner As TOwner) As Object
            If ValueGetter Is Nothing Then Return Nothing
            Return ValueGetter(owner)
        End Function
        Public ReadOnly Property HumanName As String
            Get
                If HumanNameGetter Is Nothing Then Return Nothing
                Return HumanNameGetter()()
            End Get
        End Property
        Public ReadOnly Property Description As String
            Get
                If DescriptionGetter Is Nothing Then Return Nothing
                Return DescriptionGetter()()
            End Get
        End Property
    End Class

    ''' <summary>Descripbes a type-safe metadata property</summary>
    ''' <remarks>This is a helper class used internally by some simplier <see cref="IMetadata"/> implementations</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class MetadataPropertyDescriptor(Of TOwner As IMetadata, TValue)
        Inherits MetadataPropertyDescriptor(Of TOwner)
        Public Sub New(key$, valueGetter As Func(Of TOwner, TValue), Optional humanNameGetter As Func(Of String) = Nothing, Optional descriptionGetter As Func(Of String) = Nothing)
            MyBase.New(key, If(valueGetter Is Nothing, Nothing, Function(owner As TOwner) valueGetter(owner)), humanNameGetter, descriptionGetter)
        End Sub

        Protected Overrides Sub SetValueGetterImpl(value As System.Func(Of TOwner, Object))
            If value IsNot Nothing Then
                If Not GetType(Func(Of TOwner, TValue)).IsAssignableFrom(value.GetType) Then Throw New TypeMismatchException(value, "value", GetType(Func(Of TOwner, TValue)))
            End If
            MyBase.SetValueGetterImpl(value)
        End Sub

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

        Public Shadows Function GetValue(owner As TOwner) As TValue
            If ValueGetter Is Nothing Then Return Nothing
            Return ValueGetter(owner)
        End Function
    End Class

#End If
End Namespace