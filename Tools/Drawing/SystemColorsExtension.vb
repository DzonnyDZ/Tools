Imports System.Drawing, System.Windows.Forms
Namespace DrawingT
#If True
    ''' <summary>Gives access to aditional system-defined colors</summary>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public NotInheritable Class SystemColorsExtension
        ''' <summary>Color of non visited non hovered hyperlink in web browser</summary>
        Public Shared ReadOnly Property BrowserLink() As Color
            Get
                Return New ToolStripLabel().LinkColor
            End Get
        End Property
        ''' <summary>Color of visited hyperlink in web browser</summary>
        Public Shared ReadOnly Property BrowserVisitedLink() As Color
            Get
                Return New ToolStripLabel().VisitedLinkColor
            End Get
        End Property
        ''' <summary>Color as active (hovered) hyperlink in web browser</summary>
        Public Shared ReadOnly Property BrowserActiveLink() As Color
            Get
                Return New ToolStripLabel().ActiveLinkColor
            End Get
        End Property
    End Class
#End If
End Namespace