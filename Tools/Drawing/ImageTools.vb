Imports System.Drawing
Imports System.Runtime.CompilerServices

Namespace DrawingT
    'ASAP: Mark, WiKi, COmment, Forum
    Public Module ImageTools
#If Framework >= 3.5 Then
        ''' <summary>Gets thumbnail size that best fits into given size</summary>
        ''' <param name="ImgSize">Size of original image</param>
        ''' <param name="ThumbBounds">Size that represents maximal bounds of thumbnail</param>
        ''' <returns>Size that does not exceed <paramref name="ThumbBounds"/> and has same proportions as <paramref name="ImgSize"/></returns>
        <Extension()> _
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <FirstVersion(2008, 1, 26)> _
        Public Function ThumbSize(ByVal ImgSize As Size, ByVal ThumbBounds As Size) As Size
#Else
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <FirstVersion(2008, 1, 26)> _
        Public Function ThumbSize(ByVal ImgSize As Size, ByVal ThumbBounds As Size) As Size
#End If
            Dim NewS As Size
            If ImgSize.Width <= ThumbBounds.Width AndAlso ImgSize.Height <= ThumbBounds.Width Then
                Return ImgSize
            ElseIf ImgSize.Height > ThumbBounds.Height Then
                NewS = New Size(ThumbBounds.Width, ImgSize.Height / (ImgSize.Width / ThumbBounds.Width))
            Else
                NewS = New Size(ImgSize.Width / (ImgSize.Height / ThumbBounds.Height), ThumbBounds.Height)
            End If
            If NewS.Width > ThumbBounds.Width Then
                Return New Size(ThumbBounds.Width, NewS.Height / (NewS.Width / ThumbBounds.Width))
            ElseIf NewS.Height > ImgSize.Height Then
                Return New Size(NewS.Width / (NewS.Height / ThumbBounds.Height), ThumbBounds.Height)
            End If
            Return NewS
        End Function
#If Framework >= 3.5 Then
        ''' <summary>Gets size of image that best fits into given size and has same proportins</summary>
        ''' <param name="Image">Original image (only <see cref="Image.Size"/> of this image is used).</param>
        ''' <param name="ThumbBounds">Size that represents maximal bounds of thumbnail</param>
        ''' <returns>Size that does not exceed <paramref name="ThumbBounds"/> and has same proportions as <paramref name="Image"/>.<see cref="Image.Size">Size</see>.</returns>
        <Extension()> _
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <FirstVersion(2008, 1, 26)> _
        Public Function ThumbSize(ByVal Image As Image, ByVal ThumbBounds As Size) As Size
            Return Image.Size.ThumbSize(ThumbBounds)
        End Function
#End If
        ''' <summary>Combines two images by overlaying them</summary>
        ''' <param name="Background">Image to serve as background</param>
        ''' <param name="OverlayImage">Image to draw over <paramref name="Background"/></param>
        ''' <param name="Position">Position of overlay image</param>
        ''' <returns>New instance of <see cref="Image"/> with <paramref name="Background"/> as backround and <paramref name="OverlayImage"/> drawn ovwe it.</returns>
#If Framework >= 3.5 Then
        <Extension()> _
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <FirstVersion(2008, 1, 26)> _
        Public Function Overlay(ByVal Background As Image, ByVal OverlayImage As Image, ByVal Position As ContentAlignment) As Image
#Else
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <FirstVersion(2008, 1, 26)> _
        Public Function Overlay(ByVal Background As Image, ByVal OverlayImage As Image, ByVal Position As ContentAlignment) As Image
#End If
            If Background Is Nothing Then Throw New ArgumentNullException("Background")
            Dim ret As New Bitmap(Background.Width, Background.Height)
            Dim g = Graphics.FromImage(ret)
            g.DrawImage(Background, 0, 0)
            If OverlayImage IsNot Nothing Then
                Dim op As Point
                Select Case Position
                    Case ContentAlignment.BottomCenter, ContentAlignment.BottomLeft, ContentAlignment.BottomRight
                        op.Y = Background.Height - OverlayImage.Height
                    Case ContentAlignment.MiddleCenter, ContentAlignment.MiddleLeft, ContentAlignment.MiddleRight
                        op.Y = Background.Height / 2 - OverlayImage.Height / 2
                    Case ContentAlignment.TopCenter, ContentAlignment.TopLeft, ContentAlignment.TopRight
                        op.Y = 0
                    Case Else
                        Throw New InvalidEnumArgumentException("Position", Position, GetType(ContentAlignment))
                End Select
                Select Case Position
                    Case ContentAlignment.BottomCenter, ContentAlignment.MiddleCenter, ContentAlignment.TopCenter
                        op.X = Background.Width / 2 - OverlayImage.Width / 2
                    Case ContentAlignment.BottomLeft, ContentAlignment.MiddleLeft, ContentAlignment.TopLeft
                        op.X = 0
                    Case ContentAlignment.BottomRight, ContentAlignment.MiddleRight, ContentAlignment.TopRight
                        op.X = Background.Width - OverlayImage.Width
                End Select
                g.DrawImage(OverlayImage, op)
            End If
            g.Flush(Drawing2D.FlushIntention.Flush)
            Return ret
        End Function
    End Module
End Namespace