#If Config <= Nightly Then 'Stage=Nightly
Namespace TextT.EncodingT
    'TODO:ISO2022
    ''' <summary>Provides runtime access to list of text encodings registered by ISO-IR 2002</summary>
    ''' <remarks>This class provides access to information about such encodings and possibly gives their names as registered by IANA and possibly gives instances of the <see cref="Text.Encoding"/> class to manipulate with text stored in this encoding. Not all ISO-2022 encodings are registered with IANA and not all ISO-2022 encodings are supported by .NET framework. This class does not provide more implementations of the <see cref="Text.Encoding"/> class to deal with all ISO-2022 registered encodings neither this class provides generic ISO-2022 reader/writer. The aim of this class is to provide possibility of identifiying ISO 2022 encoding by its escape sequence, not to deal with it.</remarks>
    Public Class ISO2022
        ''' <summary>Contains value of the <see cref="DefaultInstance"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Shared _DefaultInstance As ISO2022
        ''' <summary>Gets default instance of the <see cref="ISO2022"/> class initialized with default values.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared ReadOnly Property DefaultInstance() As ISO2022
            Get
                If _DefaultInstance Is Nothing Then _DefaultInstance = New ISO2022
                Return _DefaultInstance
            End Get
        End Property

    End Class
    Public Class ISO2022Encoding

    End Class
    Public NotInheritable Class ISO2022EncodingType

    End Class
End Namespace
#End If