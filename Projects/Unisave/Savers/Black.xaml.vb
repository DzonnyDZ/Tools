Imports <xmlns:black="clr-namespace:Tools.Unisave;assembly=Unisave#BlackSaver">
Imports System.Globalization.CultureInfo
Imports System.Windows.Forms
Imports Tools.WindowsT.InteropT

''' <summary>Single color saver part</summary>
Public Class BlackSaver
    Inherits SaverBase

#Region "BackgroundColor"
    ''' <summary>Gets or sets saver background color</summary>      
    Public Property BackgroundColor As Color
        Get
            Return GetValue(BackgroundColorProperty)
        End Get
        Set(ByVal value As Color)
            SetValue(BackgroundColorProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="BackgroundColor"/> dependency property</summary>                                                   
    Public Shared ReadOnly BackgroundColorProperty As DependencyProperty = DependencyProperty.Register(
        "BackgroundColor", GetType(Color), GetType(BlackSaver), New FrameworkPropertyMetadata(Colors.Black))
#End Region


    ''' <summary>Saves current settings off a component as XML element</summary>
    ''' <returns>An XML element containing saved serttings of the component</returns>
    Public Overrides Function GetSettings() As System.Xml.Linq.XElement
        Return <black:settings black:background-color=<%= (BackgroundColor.ToArgb).ToString("X", InvariantCulture) %>/>
    End Function

    ''' <summary>Loads settings of this component form an XML element</summary>
    ''' <param name="settings">An XML element to load settings from</param>
    Public Overrides Sub LoadSettings(settings As System.Xml.Linq.XElement)
        If settings.@<black:background-color> IsNot Nothing Then
            BackgroundColor = ColorFromArgb(Integer.Parse(settings.@<black:background-color>, Globalization.NumberStyles.HexNumber, InvariantCulture))
        End If
    End Sub

    ''' <summary>Shows settings dialog for user.</summary>
    Public Overrides Sub ShowSettings()
        Dim dlg As ColorDialog = New ColorDialog() With {.Color = BackgroundColor.ToColor, .ShowHelp = False}
        If SSaverContext.ShowDialog(dlg) = DialogResult.OK Then
            BackgroundColor = dlg.Color.ToColor
        End If
    End Sub
End Class
