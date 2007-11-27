Imports System.Drawing
Imports System.Runtime.CompilerServices

Namespace DrawingT
    'ASAP: Mark, WiKi, COmment, Forum
    Public Module ImageTools
#If VBC_VER >= 9.0 Then
        <Extension()> _
        Public Function ThumbSize(ByVal ImgSize As Size, ByVal ThumbBounds As Size) As Size
#Else
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
#If VBC_VER >= 9.0 Then
        <Extension()> _
        Public Function ThumbSize(ByVal Image As Image, ByVal ThumbBounds As Size) As Size
            Return Image.Size.ThumbSize(ThumbBounds)
        End Function
#End If
    End Module
End Namespace