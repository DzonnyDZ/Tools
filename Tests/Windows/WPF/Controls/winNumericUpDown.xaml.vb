Imports System.Windows


Namespace WindowsT.WPF.ControlsT
    ''' <summary>Tests for <see cref="Tools.WindowsT.WPF.ControlsT.NumericUpDown"/></summary>         
    Partial Public Class winNumericUpdown
        ''' <summary>Shows properties of tested <see cref="Tools.WindowsT.WPF.ControlsT.NumericUpDown"/></summary>
        Private WithEvents prgProperties As PropertyGrid
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Dim strm As New IO.MemoryStream
            Tools.ResourcesT.ToolsIcon.Save(strm)
            Me.Icon = New Media.Imaging.IconBitmapDecoder(strm, Media.Imaging.BitmapCreateOptions.None, Media.Imaging.BitmapCacheOption.Default).Frames(0)
            prgProperties = wfhPropertyGrid.Child
            prgProperties.SelectedObject = nudTested
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim win As New winNumericUpdown
            win.ShowDialog()
        End Sub
    End Class
End Namespace
