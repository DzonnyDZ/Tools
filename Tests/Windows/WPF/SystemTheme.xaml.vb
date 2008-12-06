Imports System.Windows, System.Linq, Tools.LinqT
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports Tools.WindowsT.InteropT, Tools.ExtensionsT, Tools.ComponentModelT
Imports System.Windows.Markup
Imports System.ComponentModel
Imports System.Windows.Controls


Namespace WindowsT.WPF
    ''' <summary>Test fro system theme</summary>
    Partial Public Class winSystemTheme
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Dim strm As New IO.MemoryStream
            Tools.ResourcesT.ToolsIcon.Save(strm)
            Me.Icon = New Media.Imaging.IconBitmapDecoder(strm, Media.Imaging.BitmapCreateOptions.None, Media.Imaging.BitmapCacheOption.Default).Frames(0)
            'sbb.Width = Button1.Width
            'sbb.Height = Button1.Height
            'Button1.Background = New VisualBrush(sbb)
        End Sub
        'Dim sbb As New SystemButtonBorder
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim win As New winSystemTheme
            win.ShowDialog()
        End Sub

        'Private Sub Button1_SizeChanged(ByVal sender As System.Object, ByVal e As System.Windows.SizeChangedEventArgs)
        '    sbb.Width = e.NewSize.Width
        '    sbb.Height = e.NewSize.Height
        '    Button1.Background = New VisualBrush(sbb)
        'End Sub

  
        Private Sub Label1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Label1.MouseDoubleClick
            Dim frm As New Form With {.Width = 300, .Height = 300}
            frm.Controls.Add(New Windows.Forms.Button With {.Width = frm.ClientSize.Width - 20, .Height = frm.ClientSize.Height - 20, .Left = 10, .Top = 20, .Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom, .Text = "Ahoj;-)"})
            frm.Show()
        End Sub
    End Class
    '<TypeConverterAttribute(GetType(SystemButtonBackgroundSourceConverter))> _
    'Public Class SystemButtonBackgroundSource
    '    Inherits DependencyObject

    '    Public Sub New()
    '    End Sub
    '    Friend Sub New(ByVal Bitmap As Bitmap)
    '        Me.Cache = Bitmap
    '    End Sub


    '    Public Property Width() As Integer
    '        Get
    '            Return GetValue(WidthProperty)
    '        End Get

    '        Set(ByVal value As Integer)
    '            SetValue(WidthProperty, value)
    '        End Set
    '    End Property

    '    Public Shared ReadOnly WidthProperty As DependencyProperty = _
    '                           DependencyProperty.Register("Width", _
    '                           GetType(Integer), GetType(SystemButtonBackgroundSource), _
    '                           New FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure Or FrameworkPropertyMetadataOptions.AffectsRender))

    '    Public Property Height() As Integer
    '        Get
    '            Return GetValue(HeightProperty)
    '        End Get

    '        Set(ByVal value As Integer)
    '            SetValue(HeightProperty, value)
    '        End Set
    '    End Property

    '    Public Shared ReadOnly HeightProperty As DependencyProperty = _
    '                           DependencyProperty.Register("Height", _
    '                           GetType(Integer), GetType(SystemButtonBackgroundSource), _
    '                           New FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure Or FrameworkPropertyMetadataOptions.AffectsRender))

    '    Protected Overrides Sub OnPropertyChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
    '        If e.Property.Equals(HeightProperty) OrElse e.Property.Equals(WidthProperty) Then Cache = Nothing
    '        MyBase.OnPropertyChanged(e)
    '    End Sub

    '    Private Cache As Bitmap
    '    Public Function GetImage() As System.Drawing.Bitmap
    '        If Cache IsNot Nothing Then Return Cache
    '        If Width = 0 OrElse Height = 0 Then Return Nothing
    '        Dim bmp As New System.Drawing.Bitmap(Width, Height)
    '        Dim g = Graphics.FromImage(bmp)
    '        Dim r As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Button.PushButton.Normal)
    '        r.DrawBackground(g, New Rectangle(0, 0, bmp.Width, bmp.Height))
    '        g.Flush(Drawing2D.FlushIntention.Sync)
    '        Cache = bmp
    '        ' bmp.Save("d:\temp\test.png", System.Drawing.Imaging.ImageFormat.Png)
    '        Return bmp
    '    End Function

    'End Class
    'Public Class SystemButtonBackgroundSourceConverter
    '    Inherits TypeConverter(Of SystemButtonBackgroundSource, BitmapSource)
    '    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As System.Windows.Media.Imaging.BitmapSource) As SystemButtonBackgroundSource
    '        Return New SystemButtonBackgroundSource(value.ToBitmap)
    '    End Function
    '    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As SystemButtonBackgroundSource) As System.Windows.Media.Imaging.BitmapSource
    '        Using img = value.GetImage
    '            Return img.ToBitmapSource()
    '        End Using
    '    End Function
    'End Class

    '''' <summary>Markup extension that can convert value of any type to value of target dependency property using <see cref="TypeConverter"/></summary>
    '''' <remarks>Either type of target dependency property or type of value being converted must have <see cref="TypeConverterAttribute"/> providing <see cref="TypeConverter"/> for conversion from type of value being converted to type of target dependency property.
    '''' See <see cref="ConvertExtension.ProvideValue"/> for details.</remarks>
    '''' <seelaso cref="TypeConverter"/>
    '''' <seelaso cref="TypeConverterAttribute"/>
    '''' <seelaso cref="ConvertExtension.ProvideValue"/>
    'Public Class ConvertExtension
    '    Inherits Markup.MarkupExtension
    '    ''' <summary>Contains value of the <see cref="Value"/> property</summary>
    '    Private _Value As Object
    '    ''' <summary>CTor with value</summary>
    '    ''' <param name="Value">Value to be converted</param>
    '    ''' <remarks>This CTor is used when this markup is used with attribute syntax.</remarks>
    '    Public Sub New(ByVal Value As Object)
    '        _Value = Value
    '    End Sub
    '    ''' <summary>Default constructor</summary>
    '    ''' <remarks>This constructor is ued when this markup extension is used with verbose (element) syntax.</remarks>
    '    Public Sub New()
    '    End Sub
    '    ''' <summary>Gets or sets value to be converted</summary>
    '    ''' <value>Value to be converted</value>
    '    ''' <returns>Value being converted</returns>
    '    ''' <remarks>This property is used when this merkup extension is used with verbose (element) syntax.</remarks>
    '    Public Property Value() As Object
    '        Get
    '            Return _Value
    '        End Get
    '        Set(ByVal value As Object)
    '            _Value = value
    '        End Set
    '    End Property
    '    ''' <summary>Returns an object that is set as the value of the target property for this markup extension - attempts to convert <see cref="Value"/> to type of target property.</summary>
    '    ''' <returns>The object value to set on the property where the extension is applied.</returns>
    '    ''' <param name="serviceProvider">Object that can provide services for the markup extension.</param>
    '    ''' <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> is null and <see cref="Value"/> is not null.</exception>
    '    ''' <exception cref="ArgumentException"><see cref="Value"/> is not null and any of following conditions is true:
    '    ''' <paramref name="serviceProvider"/> does not provide service of type <see cref="IProvideValueTarget"/> -or-
    '    ''' <see cref="IProvideValueTarget.TargetProperty"/> obtained from <paramref name="serviceProvider"/> is null -or-
    '    ''' <see cref="IProvideValueTarget.TargetProperty"/> obtained from <paramref name="serviceProvider"/> is not <see cref="DependencyProperty"/>
    '    ''' </exception>
    '    ''' <exception cref="TypeMismatchException">Appropriate <see cref="TypeConverter"/> is found, but it provides value of unexpected type.</exception>
    '    ''' <exception cref="InvalidOperationException">No type converter found from type of <see cref="Value"/> to type of target <see cref="DependencyProperty"/>.</exception>
    '    ''' <remarks>
    '    ''' This mehod attempts to convert <see cref="Value"/> to type of target <see cref="DependencyProperty"/> using <see cref="TypeConverter"/> obtained via <see cref="TypeConverterAttribute"/> from type of <see cref="Value"/> (at first) or from type of target <see cref="DependencyProperty"/> (at second).
    '    ''' <see cref="TypeConverter"/> obtained from type of <see cref="Value"/> is considered appropriate if its <see cref="TypeConverter.CanConvertTo"/> returns true for type of target <see cref="DependencyProperty"/>.
    '    ''' <see cref="TypeConverter"/> obtained from type of target <see cref="DependencyProperty"/> is considered approprite if its <see cref="TypeConverter.CanConvertFrom"/> returns true for type of <see cref="Value"/>.
    '    ''' <para>If <see cref="Value"/> is <see langword="null"/>, <see langword="null"/> is returned without any conversion.</para>
    '    ''' <para>When type of <see cref="Value"/> can be assigned to type of target <see cref="DependencyProperty"/>, no conversion is done.</para>
    '    ''' </remarks>
    '    ''' <seelaso cref="Type.IsAssignableFrom"/>
    '    Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
    '        Dim value = Me.Value
    '        If value Is Nothing Then Return Nothing
    '        If serviceProvider Is Nothing Then Throw New ArgumentNullException("serviceProvider")
    '        Dim ipv As IProvideValueTarget = serviceProvider.GetService(GetType(IProvideValueTarget))
    '        If ipv Is Nothing Then Throw New ArgumentException("serviceProvider does not provide IProvideValueTarget")
    '        If ipv.TargetProperty Is Nothing Then Throw New ArgumentException("IProvideValueTarget does not provide TargetProperty")
    '        If Not TypeOf ipv.TargetProperty Is DependencyProperty Then Throw New ArgumentException("IProvideValueTarget provides TargetProperty which is not DependencyProperty")
    '        Dim tp As DependencyProperty = ipv.TargetProperty
    '        If Not TypeOf value Is Expression Then
    '            Return Convert(value, tp.PropertyType)
    '        Else
    '            Dim MEx As MarkupExtension = Convert(value, GetType(MarkupExtension))
    '            If MEx Is Nothing Then Return Nothing
    '            value = MEx.ProvideValue(Nothing)
    '            If value Is Nothing Then Return Nothing
    '            Return Convert(value, tp.PropertyType)
    '        End If
    '    End Function

    '    Private Shared Function GetSourceConverters(ByVal SourceType As Type, ByVal TargteType As Type) As IEnumerable(Of TypeConverter)
    '        Dim ValueConverters = SourceType.GetAttributes(Of TypeConverterAttribute)()
    '        If ValueConverters IsNot Nothing AndAlso ValueConverters.Length > 0 Then
    '            Return From tc In ValueConverters _
    '                                      Let ConverterType = Type.GetType(tc.ConverterTypeName) _
    '                                      Where ConverterType IsNot Nothing AndAlso ConverterType.CanAutomaticallyCreateInstance AndAlso GetType(TypeConverter).IsAssignableFrom(ConverterType) _
    '                                      Select Converter = DirectCast(Activator.CreateInstance(ConverterType), TypeConverter) _
    '                                      Where Converter IsNot Nothing AndAlso Converter.CanConvertTo(TargteType)
    '        Else
    '            Return New TypeConverter() {}
    '        End If
    '    End Function
    '    Private Shared Function GetTargetConverters(ByVal SourceType As Type, ByVal TargteType As Type) As IEnumerable(Of TypeConverter)
    '        Dim ValueConverters = SourceType.GetAttributes(Of TypeConverterAttribute)()
    '        If ValueConverters IsNot Nothing AndAlso ValueConverters.Length > 0 Then
    '            Return From tc In ValueConverters _
    '                                  Let ConverterType = Type.GetType(tc.ConverterTypeName) _
    '                                  Where ConverterType IsNot Nothing AndAlso ConverterType.CanAutomaticallyCreateInstance AndAlso GetType(TypeConverter).IsAssignableFrom(ConverterType) _
    '                                  Select Converter = DirectCast(Activator.CreateInstance(ConverterType), TypeConverter) _
    '                                  Where Converter IsNot Nothing AndAlso Converter.CanConvertFrom(SourceType)
    '        Else
    '            Return New TypeConverter() {}
    '        End If
    '    End Function

    '    Private Function CanConvert(ByVal SourceType As Type, ByVal TargetType As Type) As Boolean
    '        Return Not GetSourceConverters(SourceType, TargetType).IsEmpty OrElse Not GetTargetConverters(SourceType, TargetType).IsEmpty
    '    End Function

    '    Private Function Convert(ByVal Value As Object, ByVal TargetType As Type) As Object
    '        If Value Is Nothing Then Return Nothing
    '        If TargetType.IsAssignableFrom(Value.GetType) Then Return Value
    '        Dim conv = GetSourceConverters(Value.GetType, TargetType).FirstOrDefault
    '        If conv IsNot Nothing Then
    '            Dim ret = conv.ConvertTo(Value, TargetType)
    '            If ret Is Nothing Then Return ret
    '            If Not TargetType.IsAssignableFrom(ret.GetType) Then Throw New TypeMismatchException(ret, TargetType, "Type converter {0} of {1} returned value of unecpected type {2}.".f(conv.GetType.FullName, Value.GetType.FullName, ret.GetType.FullName))
    '            Return ret
    '        End If
    '        conv = GetTargetConverters(Value.GetType, TargetType).FirstOrDefault
    '        If conv IsNot Nothing Then
    '            Dim ret = conv.ConvertFrom(Value)
    '            If ret Is Nothing Then Return ret
    '            If Not TargetType.IsAssignableFrom(ret.GetType) Then Throw New TypeMismatchException(ret, TargetType, "Type converter {0} of {1} returned value of unecpected type {2}.".f(conv.GetType.FullName, TargetType.FullName, ret.GetType.FullName))
    '            Return ret
    '        End If
    '        Throw New InvalidOperationException("Unable to convert type {0} to {1}. No appropriate converter found.".f(Value.GetType.FullName, TargetType.FullName))
    '    End Function
    'End Class

    'Public Class SystemButtonBorder
    '    Inherits Border
    '    Private sbc As New SystemButtonBackgroundSourceConverter
    '    Private sbs As New SystemButtonBackgroundSource




    '    Private Sub SystemButtonBorder_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
    '        sbs.Height = e.NewSize.Height
    '        sbs.Width = e.NewSize.Width
    '        Me.Background = New ImageBrush(sbs.GetImage.ToBitmapSource)
    '    End Sub


    'End Class

    Public MustInherit Class VisualStyleDrawer
        Inherits DependencyObject
        Public MustOverride Sub DrawBackground(ByVal g As Graphics, ByVal rect As Rectangle)
        Public Sub DrawBackground(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%)
            DrawBackground(g, New Rectangle(x, y, Width, Height))
        End Sub
        Public MustOverride Function GetContentRectangle(ByVal g As Graphics, ByVal rect As Rectangle) As Rectangle
        Public Function GetContentRectangle(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%) As Rectangle
            Return GetContentRectangle(g, New Rectangle(x, y, Width, Height))
        End Function
        Public Function GetPadding(ByVal g As Graphics, ByVal rect As Rectangle) As Padding
            With GetContentRectangle(g, rect)
                Return New Padding(.Left - rect.Left, .Top - rect.Top, rect.Right - .Right, rect.Bottom - .Bottom)
            End With
        End Function
        Public Function GetPadding(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%) As Padding
            Return GetPadding(g, New Rectangle(x, x, Width, Height))
        End Function
        Public Function GetPaddingWpf(ByVal g As Graphics, ByVal rect As Rectangle) As Thickness
            Dim Padding = GetPadding(g, rect)
            Return New Thickness(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom)
        End Function
        Public Function GetPaddingWpf(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%) As Thickness
            Return GetPaddingWpf(g, New Rectangle(x, y, Width, Height))
        End Function
        Public MustOverride ReadOnly Property CanHaveContent() As Boolean
    End Class


    Public Class VisualStyleButton
        Inherits VisualStyleDrawer


        Private Renderer As VisualStyles.VisualStyleRenderer
        Public Sub New()
            'If VisualStyles.VisualStyleRenderer.IsSupported AndAlso VisualStyles.VisualStyleRenderer.IsElementDefined(VisualStyles.VisualStyleElement.Button.PushButton.Default) Then _
            Renderer = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Button.PushButton.Default)
        End Sub

        Public Property State() As VisualStyles.PushButtonState
            Get
                Return GetValue(StateProperty)
            End Get

            Set(ByVal value As VisualStyles.PushButtonState)
                SetValue(StateProperty, value)
            End Set
        End Property

        Public Shared ReadOnly StateProperty As DependencyProperty = _
                               DependencyProperty.Register("State", _
                               GetType(VisualStyles.PushButtonState), GetType(VisualStyleButton), _
                               New FrameworkPropertyMetadata(VisualStyles.PushButtonState.Default))
        'Protected Overrides Sub OnPropertyChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        '    MyBase.OnPropertyChanged(e)
        '    If e.Property.Equals(StateProperty) Then
        '        Renderer = New VisualStyles.VisualStyleRenderer("BUTTON",
        '    End If
        'End Sub



        Public Overrides ReadOnly Property CanHaveContent() As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overloads Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle)

        End Sub

        Public Overloads Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle) As System.Drawing.Rectangle

        End Function
    End Class



    Public Class VisualStyleBorder
        Inherits Border
        Private Drawer As VisualStyleDrawer

        Private Sub VisualStyleBorder_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
            Using img As New Bitmap(CInt(e.NewSize.Width), CInt(e.NewSize.Height))
                Using g As Graphics = Graphics.FromImage(img)
                    Drawer.DrawBackground(g, 0, 0, img.Width, img.Height)
                    g.Flush(Drawing2D.FlushIntention.Sync)
                    Me.Padding = Drawer.GetPaddingWpf(g, 0, 0, img.Width, img.Height)
                End Using
                Me.Background = New ImageBrush(img.ToBitmapSource)
            End Using
        End Sub



        'Public Property Padding() As Thickness
        '    Get
        '        Return GetValue(PaddingProperty)
        '    End Get

        '    Set(ByVal value As Thickness)
        '        SetValue(PaddingProperty, value)
        '    End Set
        'End Property

        'Public Shared ReadOnly PaddingProperty As DependencyProperty = _
        '                       DependencyProperty.Register("Padding", _
        '                       GetType(Thickness), GetType(VisualStyleBorder), _
        '                       New FrameworkPropertyMetadata(New Thickness(0)))




    End Class


End Namespace
