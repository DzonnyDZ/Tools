Imports System.Drawing, System.Windows.Forms
Namespace DrawingT
#If Config <= Release Then
    ''' <summary>Gives access to aditional system-defined colors</summary>
    <Author("�onny", "dzonny@dzonny.cz", "http://dzony.cz")> _
    <Version(1, 0, GetType(SystemColorsExtension), LastChange:="05/10/2007")> _
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