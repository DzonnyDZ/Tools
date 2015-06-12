Imports System.Windows.Input, Tools.ExtensionsT, Tools
Imports System.Globalization
Imports System.Windows.Markup
Imports System.Text.RegularExpressions
Imports System.Globalization.CultureInfo
Imports Tools.WindowsT.WPF.MarkupT
Imports System.Windows.Controls

Namespace WindowsT.WPF.InputT

    ''' <summary>An implementation of <see cref="MouseGesture"/> class which matches to specific mouse wheel events</summary>
    ''' <remarks>
    ''' You can use this class with <see cref="InputBinding"/> in XAML as shown i example. But it causes problems in Visual Studio designer.
    ''' <example>
    ''' This exaple shows how to use <see cref="MouseWheelGesture"/> with <see cref="InputBinding"/>.
    ''' <note>
    ''' Code shown in this example is valid XAML code and works perfectly in runtime, however there is a bug in Visual Studio designer that causes that designer stops working.
    ''' If you want to use Visual Studio designer you must use an alternative approach.
    ''' </note>
    ''' <code lang="XAML"><![CDATA[<Window.InputBindings>
    '''     <MouseBinding Command="NextPage">
    '''         <MouseBinding.Gesture>
    '''             <ti:MouseWheelGesture xmlns:ti="clr-namespace:Tools.WindowsT.WPF.InputT;assembly=Tools.Windows">Minus</ti:MouseWheelGesture>
    '''         </MouseBinding.Gesture>
    '''     </MouseBinding>
    ''' </Window.InputBindings>]]></code>
    ''' </example>
    ''' An alternative way that satisfies both - runtime and Visual Studio designer is to instantiate <see cref="MouseWheelGesture"/> in resources.
    ''' <example>
    ''' This example shows alternative way of instantiating and assigning of <see cref="MouseWheelGesture"/> using resource dictionary.
    ''' This way Visual Studio designer does not stop working.
    ''' <code lang="XAML"><![CDATA[
    ''' <Window.Resources>
    '''     <ti:MouseWheelGesture x:Key="MouseWheelMinus" xmlns:ti="clr-namespace:Tools.WindowsT.WPF.InputT;assembly=Tools.Windows">Minus</ti:MouseWheelGesture>
    ''' </Window.Resources>
    ''' <Window.InputBindings>
    '''     <MouseBinding Command="NextPage" Gesture="{StaticResource MouseWheelMinus}"/>
    ''' </Window.InputBindings>
    ''' ]]></code>
    ''' </example>
    ''' Another workaround is to use <see cref="FreeInputBinding"/> instead.
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <TypeConverter(GetType(MouseWheelGestureConverter))>
    <ValueSerializer(GetType(DefaultTypeConverterBasedValueSerializer(Of MouseWheelGesture)))>
    Public Class MouseWheelGesture
        Inherits MouseGesture
        ''' <summary>CTor - creates a new instance of the <see cref="MouseWheelGesture"/> class with default settings</summary>
        Public Sub New()
            MouseAction = System.Windows.Input.MouseAction.WheelClick
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="MouseWheelGesture"/> class setting its properties</summary>
        ''' <param name="direction">Direction of wheel movement</param>
        ''' <param name="delta">Number of wheel clicks (0 means any)</param>
        ''' <param name="modifiers">Modifier keys</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="modifiers"/> is not valid <see cref="ModifierKeys"/> value</exception>
        Public Sub New(direction As MouseWheelDirection, Optional delta% = 0, Optional modifiers As ModifierKeys = ModifierKeys.None)
            Me.New()
            Me.Modifiers = modifiers
            Me.Direction = direction
            Me.Delta = delta
        End Sub

        ''' <summary>Gets or sets the <see cref="System.Windows.Input.MouseAction"/> associated with this gesture.</summary>
        ''' <value>The mouse action associated with this gesture. The default and only allowed value is <see cref="System.Windows.Input.MouseAction.WheelClick"/>.</value>
        ''' <remarks>Do not use this property. Do not use base class <see cref="MouseGesture.MouseAction"/> property on this class. If value of this property is different than <see cref="MouseAction.WheelClick"/> this gesture never matches.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Property MouseAction As MouseAction
            Get
                Return MyBase.MouseAction
            End Get
            Set(value As MouseAction)
                If value <> System.Windows.Input.MouseAction.WheelClick Then Throw New NotSupportedException(ResourcesT.Exceptions.ValueMustBeFor.f("MouseAction", MouseAction.WheelClick, GetType(MouseWheelGesture).Name))
                MyBase.MouseAction = value
            End Set
        End Property

        Private Shared ReadOnly regex As New Regex("^\s*(?<Direction>(Plus)|(Minus)|(Any))?\s*(?<Delta>\d+)?\s*(?<Orientation>(Horizontal)|(Vertical))?\s*(\+(?<Modifier>(Alt)|(Control)|(Ctrl)|(Shift)|(Win(dows)?)|(None)))*\s*$", RegexOptions.Compiled Or RegexOptions.CultureInvariant Or RegexOptions.IgnoreCase Or RegexOptions.ExplicitCapture)

        '(Plus|Minus)?\s*(Value)?\s*(+Modifiers)?
        ''' <summary>CTor - creates a new instance of the <see cref="MouseWheelGesture"/> class from it's string representation</summary>
        ''' <param name="description">String representation of <see cref="MouseWheelGesture"/> in recognized format. Can also be null or an empty string.</param>
        ''' <exception cref="FormatException"><paramref name="description"/> is not valid string description of <see cref="MouseWheelGesture"/></exception>
        ''' <exception cref="OverflowException">Delta component of <see cref="MouseWheelGesture"/> described in <paramref name="description"/> does not fit to type <see cref="Int32"/>.</exception>
        ''' <remarks>
        ''' The format of string is as follows:
        ''' The string is composed of 3 parts in fixed order: Direction Delta Modifier. All parts are optional. Parts can be separated by whitespaces. Also whitespaces at the beginning and at the end of string are ignored.
        ''' The string is case-insensitive.
        ''' <list type="table">
        ''' <listheader><term>Part</term><description>Format</description></listheader>
        ''' <item><term>Direction</term><description>One of following values: Plus, Minus, Any. If not presend Any is assumed. Converts to appropriate value of <see cref="MouseWheelDirection"/> enumeration.</description></item>
        ''' <item><term>Delta</term><description>Non-negative integer number. Value of the <see cref="Delta"/> property. If not present 0 is assumed.</description></item>
        ''' <item><term>Modifier</term><description>Any number of modifier values for <see cref="Modifiers"/> property. Each value must be preceded with the "+" sign. Whitespaces after the "+" and before the "+" (except first one) are NOT allowed. Supported values are: +Alt, +Control, +Ctrl, +Shift, +Win, +Windows, +None. Repeated uses of the same value are ignored. If not present None is assumed.</description></item>
        ''' </list>
        ''' </remarks>
        Public Sub New(description As String)
            Me.New()
            If description <> "" Then
                Dim result = regex.Match(description)
                If Not result.Success Then Throw New FormatException(String.Format(ResourcesT.Exceptions.StringIsNotValidDescription, description, GetType(MouseWheelDirection).Name))
                If result.Groups!Direction.Captures.Count > 0 Then
                    Direction = [Enum].Parse(GetType(MouseWheelDirection), result.Groups!Direction.Captures(0).Value, True)
                End If
                If result.Groups!Delta.Captures.Count > 0 Then
                    Delta = Integer.Parse(result.Groups!Delta.Captures(0).Value, InvariantCulture)
                End If
                If result.Groups!Orientation.Captures.Count > 0 Then
                    Orientation = [Enum].Parse(GetType(Orientation), result.Groups!Orientation.Captures(0).Value, True)
                End If
                For Each capt As Capture In result.Groups!Modifier.Captures
                    Select Case capt.Value.ToLowerInvariant
                        Case "alt" : Modifiers = Modifiers Or ModifierKeys.Alt
                        Case "control", "ctrl" : Modifiers = Modifiers Or ModifierKeys.Control
                        Case "shift" : Modifiers = Modifiers Or ModifierKeys.Shift
                        Case "win", "windows" = Modifiers = Modifiers Or ModifierKeys.Windows
                    End Select
                Next
            End If
        End Sub

        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />. Value returned by this function is suibale for <see cref="M:Tools.WindowsT.WPF.InputT.MouseWheelGesture.#ctor(System.String)"/>.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            Dim ret As String = ""
            If Direction <> MouseWheelDirection.Any Then ret &= Direction.ToString & " "
            If Delta <> 0 Then ret &= Delta.ToString(InvariantCulture) & " "
            ret &= Orientation.ToString & " "
            If Modifiers.HasFlag(ModifierKeys.Control) Then ret &= "+Win"
            If Modifiers.HasFlag(ModifierKeys.Control) Then ret &= "+Ctrl"
            If Modifiers.HasFlag(ModifierKeys.Alt) Then ret &= "+Alt"
            If Modifiers.HasFlag(ModifierKeys.Shift) Then ret &= "+Shift"
            ret = ret.Trim
            If ret = "" Then ret = "Any"
            Return ret
        End Function
        ' ''' <summary>Gets or sets modifier keys</summary>
        ' ''' <remarks>The gesture matches only if these keys are pressed on keyboard during wheel event</remarks>
        '<DefaultValue(ModifierKeys.None)>
        'Public Property Modifiers As ModifierKeys

        ''' <summary>Gets or sets value indicating which directions of mouse wheel movement will match</summary>
        <DefaultValue(MouseWheelDirection.Any)>
        Public Property Direction As MouseWheelDirection = MouseWheelDirection.Any

        ''' <summary>Gets or sets value indicating wheather horizontal or vertical mouse wheel action will match.</summary>
        <DefaultValue(Orientation.Vertical)>
        Public Property Orientation As Orientation = Orientation.Vertical

        ''' <summary>Gets or sets number of wheel clicks this operation will match.</summary>
        ''' <value>0 means any</value>
        <DefaultValue(0I)>
        Public Property Delta%

        ''' <summary>When overridden in a derived class, determines whether the specified <see cref="T:System.Windows.Input.InputGesture" /> matches the input associated with the specified <see cref="T:System.Windows.Input.InputEventArgs" /> object.</summary>
        ''' <param name="targetElement">The target of the command.</param>
        ''' <param name="inputEventArgs">The input event data to compare this gesture to.</param>
        ''' <returns>true if the gesture matches the input; otherwise, false.</returns>
        Public Overrides Function Matches(targetElement As Object, inputEventArgs As InputEventArgs) As Boolean
            Dim mwea = TryCast(inputEventArgs, MouseWheelEventArgs)
            If mwea Is Nothing Then Return False
            If MouseAction <> System.Windows.Input.MouseAction.WheelClick Then Return False
            If mwea.Delta = 0 Then Return False
            If Modifiers <> Keyboard.Modifiers Then Return False
            Select Case Direction
                Case MouseWheelDirection.Plus : If mwea.Delta < 0 Then Return False
                Case MouseWheelDirection.Minus : If mwea.Delta > 0 Then Return False
            End Select
            Select Case Orientation
                Case Orientation.Horizontal : If Not TypeOf mwea Is MouseWheelEventArgsEx OrElse DirectCast(mwea, MouseWheelEventArgsEx).Orientation <> System.Windows.Controls.Orientation.Horizontal Then Return False
                Case Orientation.Vertical : If TypeOf mwea Is MouseWheelEventArgsEx AndAlso DirectCast(mwea, MouseWheelEventArgsEx).Orientation <> System.Windows.Controls.Orientation.Vertical Then Return False
            End Select
            If Delta = 0 Then Return True
            Return Math.Abs(Delta) = Math.Abs(mwea.Delta)
        End Function
    End Class

    ''' <summary>Mose wheel directions</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum MouseWheelDirection
        ''' <summary>Any direction (plus or minus)</summary>
        Any = 0
        ''' <summary>Direction plus. <see cref="MouseWheelEventArgs.Delta"/> > 0</summary>
        Plus = 1
        ''' <summary>Direction minus. <see cref="MouseWheelEventArgs.Delta"/> &lt; 0</summary>
        Minus = -1
    End Enum

    ''' <summary>Converts a <see cref="MouseWheelGesture"/> object to and from other types.</summary>
    ''' <seelaso cref="MouseWheelGesture"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class MouseWheelGestureConverter
        Inherits TypeConverter
        ''' <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        ''' <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
        ''' <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
            Return (sourceType Is GetType(String))
        End Function

        ''' <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        ''' <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
        ''' <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
            If destinationType.Equals(GetType(String)) Then
                If context Is Nothing Then Return True
                Return TypeOf context.Instance Is MouseWheelGesture
            End If
            Return False
        End Function

        ''' <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        ''' <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
        ''' <param name="source">The <see cref="T:System.Object" /> to convert. </param>
        ''' <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        ''' <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        ''' <exception cref="FormatException"><paramref name="source"/> is string in invalid format</exception>
        ''' <exception cref="OverflowAction"><paramref name="source"/> is string describing <see cref="MouseWheelGesture.Delta"/> value that does not fit to <see cref="Int32"/> type.</exception>
        Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal source As Object) As Object
            If source IsNot Nothing AndAlso TypeOf source Is String Then
                Return New MouseWheelGesture(DirectCast(source, String))
            End If
            Throw MyBase.GetConvertFromException(source)
        End Function

        ''' <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        ''' <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
        ''' <param name="value">The <see cref="T:System.Object" /> to convert. </param>
        ''' <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
        ''' <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
        ''' <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
            If (destinationType Is Nothing) Then
                Throw New ArgumentNullException("destinationType")
            End If
            If (destinationType Is GetType(String)) Then
                If (value Is Nothing) Then
                    Return String.Empty
                End If
                Dim gesture As MouseWheelGesture = TryCast(value, MouseWheelGesture)
                If (Not gesture Is Nothing) Then
                    Return gesture.ToString
                End If
            End If
            Throw MyBase.GetConvertToException(value, destinationType)
        End Function
    End Class
End Namespace