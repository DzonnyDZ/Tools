Imports System.Drawing
Imports System.Runtime.CompilerServices

Namespace DrawingT
    'ASAP: Mark, WiKi, COmment, Forum
    ''' <summary>Contains extension methods for working with images</summary>
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
            'Dim NewS As Size
            If ImgSize.Width <= ThumbBounds.Width AndAlso ImgSize.Height <= ThumbBounds.Width Then
                Return ImgSize
            ElseIf ImgSize.Height / ThumbBounds.Height > ImgSize.Width / ThumbBounds.Width Then  'ImgSize.Height > ThumbBounds.Height AndAlso ImgSize.Width <= ThumbBounds.Width Then
                'NewS = New Size(ThumbBounds.Width, ImgSize.Height / (ImgSize.Width / ThumbBounds.Width))
                Return New Size(ImgSize.Width / (ImgSize.Height / ThumbBounds.Height), ThumbBounds.Height)
            Else 'ImgSize.Width > ThumbBounds.Width AndAlso ImgSize.Height <= ThumbBounds.Height Then
                Return New Size(ThumbBounds.Width, ImgSize.Height / (ImgSize.Width / ThumbBounds.Width))
                'Elseif 
                'NewS = New Size(ImgSize.Width / (ImgSize.Height / ThumbBounds.Height), ThumbBounds.Height)
            End If
            'If NewS.Width > ThumbBounds.Width Then
            '    Return New Size(ThumbBounds.Width, NewS.Height / (NewS.Width / ThumbBounds.Width))
            'ElseIf NewS.Height > ImgSize.Height Then
            '    Return New Size(NewS.Width / (NewS.Height / ThumbBounds.Height), ThumbBounds.Height)
            'End If
            'Return NewS
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
            g.DrawImageInPixels(Background, New Point(0, 0))
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
                g.DrawImageInPixels(OverlayImage, op)
            End If
            g.Flush(Drawing2D.FlushIntention.Sync)
            Return ret
        End Function

        ''' <summary>Draws image at given position in its pixel size (unscaled)</summary>
        ''' <param name="g">Graphic to draw the image</param>
        ''' <param name="img">Image to be drawn</param>
        ''' <param name="Position">Position (in pixels) to draw the image</param>
        <Extension()> Public Sub DrawImageInPixels(ByVal g As Graphics, ByVal img As Image, ByVal Position As Point)
            Dim oldu = g.PageUnit
            g.PageUnit = GraphicsUnit.Pixel
            Try
                g.DrawImage(img, Position.X, Position.Y, img.Size.Width, img.Size.Height)
            Finally
                g.PageUnit = oldu
            End Try
        End Sub
        ''' <summary>Draws image at given position in its pixel size (unscaled)</summary>
        ''' <param name="g">Graphic to draw the image</param>
        ''' <param name="img">Image to be drawn</param>
        ''' <param name="x">X part of position of image</param>
        ''' <param name="y">Y part of position of image</param>
        <Extension()> Public Sub DrawImageInPixels(ByVal g As Graphics, ByVal img As Image, ByVal x As Integer, ByVal y As Integer)
            g.DrawImageInPixels(img, New Point(x, y))
        End Sub


        ''' <summary>Gets thumbnail for given image</summary>
        ''' <param name="img">Image to get thumbnail of</param>
        ''' <param name="Bounds">Maximum size of thumbnail</param>
        ''' <param name="CancelFunction">Optional function that can be used to cancel long-lasting thumbmail creation</param>
        ''' <returns>Image that is guaranted to fit in <paramref name="Bounds"/> and has the same proportions as original image</returns>
        ''' <remarks>Uses <see cref="Image.GetThumbnailImage"/>.</remarks>
        ''' <seelaso cref="Image.GetThumbnailImage"/>
        ''' <exception cref="ArgumentNullException"><paramref name="img"/> is null</exception>
        <Extension()> _
        Public Function GetThumbnail(ByVal img As Image, ByVal Bounds As Size, Optional ByVal CancelFunction As Image.GetThumbnailImageAbort = Nothing) As Image
            If img Is Nothing Then Throw New ArgumentNullException("img")
            Dim thSize = img.ThumbSize(Bounds)
            If CancelFunction Is Nothing Then CancelFunction = Function() False
            Dim th = img.GetThumbnailImage(thSize.Width, thSize.Height, CancelFunction, IntPtr.Zero)
            Return th
        End Function
        ''' <summary>Gets thumbnail for given image drawn on specified background</summary>
        ''' <param name="img">Image to get thumbnail of</param>
        ''' <param name="bounds">Size of restangle to place image on</param>
        ''' <param name="Background">Background color for rectangle</param>
        ''' <param name="CancelFunction">Optional function that can be used to cancel long-lasting thumbnail creation</param>
        ''' <returns>Image that consists of rectangle of color <paramref name="Background"/> and image <paramref name="img"/> proportionally scaled to fit to <paramref name="bounds"/> and centered within <paramref name="bounds"/>.</returns>
        ''' <remarks>Uses <see cref="Graphics.DrawImage"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="img"/> is null</exception>
        <Extension()> _
        Public Function GetThumbnail(ByVal img As Image, ByVal bounds As Size, ByVal Background As Color, Optional ByVal CancelFunction As Graphics.DrawImageAbort = Nothing) As Image
            If img Is Nothing Then Throw New ArgumentNullException("img")
            Dim thsize = img.ThumbSize(bounds)
            Dim ret As New Bitmap(bounds.Width, bounds.Height)
            Dim g = Graphics.FromImage(ret)
            g.FillRectangle(New SolidBrush(Background), 0, 0, ret.Width, ret.Height)
            If CancelFunction Is Nothing Then CancelFunction = Function() False
            g.DrawImage(img, New Rectangle(New Point((bounds.Width - thsize.Width) / 2, (bounds.Height - thsize.Height) / 2), thsize), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, New Imaging.ImageAttributes, CancelFunction)
            g.Flush()
            Return ret
        End Function
    End Module
End Namespace