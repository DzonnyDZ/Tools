Imports Screen = System.Windows.Forms.Screen
Imports System.Linq
Imports Tools.WindowsT.WPF
Imports System.Windows.Controls.Primitives

''' <summary>A windows for configuring a screen saver</summary>
Friend Class PropertiesWindow


    Private Sub PropertiesWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim screens = Screen.AllScreens
        Dim minx = (From s In screens Select s.Bounds.Left).Min
        Dim miny = (From s In screens Select s.Bounds.Top).Min
        Dim maxx = (From s In screens Select s.Bounds.Right).Max
        Dim maxy = (From s In screens Select s.Bounds.Bottom).Max
        icScreens.ItemsSource = From s In screens
                                Select s.DeviceName, s.Primary, s.Bounds.Width, s.Bounds.Height,
                                       Left = s.Bounds.Left - minx, Top = s.Bounds.Top - miny
        icScreens.Width = maxx - minx
        icScreens.Height = maxy - miny
    End Sub

    Private Sub ZoomIn_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        Dim newValue = Math.Min(sldZoom.Maximum, sldZoom.Value + 0.1)
        sldZoom.Value = newValue
    End Sub
    Private Sub ZoomOut_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        Dim newValue = Math.Max(sldZoom.Minimum, sldZoom.Value - 0.1)
        sldZoom.Value = newValue
    End Sub
End Class
