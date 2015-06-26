Namespace TextT.UnicodeT

    ''' <summary>Interface of source that can provide names of characters</summary>
    ''' <version version="1.5.4">This interface is new in version 1.5.4</version>
    Public Interface ICharNameProvider
        ''' <summary>Gets name of a character</summary>
        ''' <param name="codePoint">A Unicode (UTF-32) code-point</param>
        ''' <returns>Name of the character, null of the source is not capable of providing character name</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is less than zero or greater than <see cref="UnicodeCharacterDatabase.MaxCodePoint"/>.</exception>
        Function GetName(codePoint%) As String
    End Interface

    ''' <summary>Simplistic implementation of <see cref="ICharNameProvider"/></summary>
    ''' <remarks>
    ''' This is singleton class. Use the <see cref="SimpleCharNameProvider.Instance"/> property to obtain singleton instance.
    ''' You need instance of this class only if you want to pass it as <see cref="ICharNameProvider"/>, otherwise use static function <see cref="SimpleCharNameProvider.GetName"/>.
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class SimpleCharNameProvider
        Implements ICharNameProvider
        ''' <summary>Singleton class private CTor</summary>
        Private Sub New()
        End Sub
        ''' <summary>Gets name of a character</summary>
        ''' <param name="codePoint">A Unicode (UTF-32) code-point</param>
        ''' <returns>Name of the character. This implementation returns for all characters character code (<paramref name="codePoint"/>) in format <c>U+{0:X4}</c>.</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is less than zero or greater than <see cref="UnicodeCharacterDatabase.MaxCodePoint"/>.</exception>
        Public Shared Function GetName(codePoint%) As String
            If codePoint < 0 OrElse codePoint > UnicodeCharacterDatabase.MaxCodePoint Then Throw New ArgumentOutOfRangeException("codePoint")
            Return String.Format("U+{0:X4}")
        End Function
       ''' <summary>Gets name of a character</summary>
        ''' <param name="codePoint">A Unicode (UTF-32) code-point</param>
        ''' <returns>Name of the character. This implementation returns for all characters character code (<paramref name="codePoint"/>) in format <c>U+{0:X4}</c>.</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is less than zero or greater than <see cref="UnicodeCharacterDatabase.MaxCodePoint"/>.</exception>
        Private Function ICharNameProvider_GetName(codePoint As Integer) As String Implements ICharNameProvider.GetName
            Return GetName(codePoint)
        End Function

        Private Shared _instance As SimpleCharNameProvider = New SimpleCharNameProvider

        ''' <summary>Gets instance of this singleton class</summary>
        ''' <remarks>You only need instance of this class if you want to use it as <see cref="ICharNameProvider"/>, otherwise use static method <see cref="GetName"/>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared ReadOnly Property Instance As SimpleCharNameProvider
            Get
                Return _instance
            End Get
        End Property   
    End Class          
End Namespace