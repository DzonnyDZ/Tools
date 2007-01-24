#If Config <= Release Then
Namespace ComponentModel
    ''' <summary>Inform programmer that he should name instances of class market wiht this attribute with names beginning with prefix</summary>
    ''' <remarks>This is ONLY recomendation. This attribute is here to allow control author to select prefix. Of course users of controls can either use own prefix or use no prefix.</remarks>
    <AttributeUsage(AttributeTargets.Class, AllowMultiple:=False, Inherited:=False)> _
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, lastchange:="1/24/2007")> _
    Public Class PrefixAttribute : Inherits Attribute
        ''' <summary>CTor</summary>
        ''' <param name="Prefix">Prefix associated with control</param>
        Public Sub New(ByVal Prefix As String)
            Me.Prefix = Prefix
        End Sub
        ''' <summary>Contains value of the <see cref="Prefix"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Prefix As String
        ''' <summary>Gets or sets prefix associated with control</summary>
        Public Property Prefix() As String
            Get
                Return _Prefix
            End Get
            Set(ByVal value As String)
                _Prefix = value
            End Set
        End Property
    End Class
End Namespace
#End If