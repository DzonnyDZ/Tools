Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports Tools.WindowsT.InteropT
Imports System.Windows.Markup


Namespace WindowsT.WPF
    ''' <summary>Test fro system theme</summary>
    Partial Public Class winSystemTheme
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Dim strm As New IO.MemoryStream
            Tools.ResourcesT.ToolsIcon.Save(strm)
            Me.Icon = New Media.Imaging.IconBitmapDecoder(strm, Media.Imaging.BitmapCreateOptions.None, Media.Imaging.BitmapCacheOption.Default).Frames(0)
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim win As New winSystemTheme
            win.ShowDialog()
        End Sub
    End Class
    <System.ComponentModel.TypeConverterAttribute(GetType(SystemButtonBackgroundSourceConverter))> _
    Public Class SystemButtonBackgroundSource
        Public Sub New()
        End Sub
        Friend Sub New(ByVal Bitmap As Bitmap)
            Me.Cache = Bitmap
        End Sub
        Private _Width As Integer
        Public Shadows Property Width() As Integer
            Get
                Return _Width
            End Get
            Set(ByVal value As Integer)
                Dim Changed As Integer = value <> Width
                If value < 0 Then Throw New ArgumentOutOfRangeException("value")
                _Width = value
                If Changed Then Cache = Nothing
            End Set
        End Property
        Private _Height As Integer
        Public Shadows Property Height() As Integer
            Get
                Return _Height
            End Get
            Set(ByVal value As Integer)
                Dim Changed As Integer = value <> Height
                If value < 0 Then Throw New ArgumentOutOfRangeException("value")
                _Height = value
                If Changed Then Cache = Nothing
            End Set
        End Property
        Private Cache As Bitmap
        Public Function GetImage() As System.Drawing.Bitmap
            If Cache IsNot Nothing Then Return Cache
            Dim bmp As New System.Drawing.Bitmap(Width, Height)
            Dim g = Graphics.FromImage(bmp)
            Dim r As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Button.PushButton.Normal)
            r.DrawBackground(g, New Rectangle(0, 0, bmp.Width, bmp.Height))
            g.Flush(Drawing2D.FlushIntention.Sync)
            Cache = bmp
            Return bmp
        End Function

    End Class
    Public Class SystemButtonBackgroundSourceConverter
        Inherits Tools.WindowsT.WPF.ConvertersT.StronglyTypedConverter(Of SystemButtonBackgroundSource, BitmapSource)


        'Public Overrides Function Convert(ByVal value As SystemButtonBackgroundSource, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As System.Windows.Interop.BitmapSource
        '    Return Interop.Imaging.CreateBitmapSourceFromHBitmap(value.GetImage.GetHbitmap)
        'End Function

        'Public Overrides Function ConvertBack(ByVal value As System.Windows.Interop.InteropBitmap, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As SystemButtonBackgroundSource

        'End Function

        Public Overrides Function Convert(ByVal value As SystemButtonBackgroundSource, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As System.Windows.Media.Imaging.BitmapSource
            Return value.GetImage.ToBitmapSource
        End Function

        Public Overrides Function ConvertBack(ByVal value As System.Windows.Media.Imaging.BitmapSource, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As SystemButtonBackgroundSource
            Return New SystemButtonBackgroundSource(value.ToBitmap)
        End Function
    End Class


    Public Class Convert
        Inherits Markup.MarkupExtension

        Public Sub New(ByVal Value As Object)

        End Sub

        Public Sub New()

        End Sub

        Public Property Value() As Object
            Get

            End Get
            Set(ByVal value As Object)

            End Set
        End Property

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Dim ipv As IProvideValueTarget = serviceProvider.GetService(GetType(IProvideValueTarget))


        End Function
    End Class

End Namespace
