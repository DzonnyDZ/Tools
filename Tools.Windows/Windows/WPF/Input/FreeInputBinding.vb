Imports System.Windows.Input
Imports System.Windows

Namespace WindowsT.WPF.InputT
    ''' <summary>An implementation of <see cref="InputBinding"/> than can bind to any <see cref="InputGesture"/></summary>
    ''' <remarks>
    ''' <see cref="KeyBinding"/> is limited to <see cref="KeyGesture">KeyGestures</see>, <see cref="MouseBinding"/> is limited to <see cref="MouseGesture">MouseGestures</see>, <see cref="InputBinding"/> itself cannot be instantiated in XAML.
    ''' <see cref="FreeInputBinding"/> acccepts any <see cref="InputGesture"/> and can be instantiated in XAML.
    ''' <para>
    ''' When used in XAML unlike <see cref="KeyBinding"/> or <see cref="MouseBinding"/> the <see cref="FreeInputBinding.Gesture"/> property cannot be set as attribute (unless you use markup extension such as <c>{<see cref="StaticResourceExtension">StaticResource</see>}</c>.
    ''' You must set it as inner element instead.
    ''' <example>
    ''' This example show how to use <see cref="FreeInputBinding"/> to instantiate <see cref="FreeKeyGesture"/> in XAML.
    ''' It binds the <see cref="ApplicationCommands.Open"/> to key G.
    ''' <code lang="XAML"><![CDATA[<Window.InputBindings xmlns:ti="clr-namespace:Tools.WindowsT.WPF.InputT;assembly=Tools.Windows">
    '''     <ti:FreeInputBinding Command="Open">
    '''         <ti:FreeInputBinding.Gesture>
    '''             <ti:FreeKeyGesture>G</ti:FreeKeyGesture>
    '''         </ti:FreeInputBinding.Gesture>
    '''     </ti:FreeInputBinding>
    ''' </Window.InputBindings>]]></code>
    ''' </example>
    ''' </para>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class FreeInputBinding
        Inherits InputBinding

        ''' <summary>Default CTor - creates a new empty instance of the <see cref="FreeInputBinding"/> class</summary>
        Public Sub New()
        End Sub

        ''' <summary>CTor from <see cref="InputGesture"/></summary>
        ''' <param name="command">The command to associate with <paramref name="gesture"/></param>
        ''' <param name="gesture">The <see cref="InputGesture"/> to associate with <paramref name="command"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="gesture"/> is null</exception>
        Public Sub New(command As ICommand, gesture As InputGesture)
            MyBase.New(command, gesture)
        End Sub

        ''' <summary>CTor which creates <see cref="FreeKeyGesture"/></summary>
        ''' <param name="command">The command to associate with gesture</param>
        ''' <param name="key">The key associated with the gesture.</param>
        ''' <param name="modifiers">The modifier keys associated with the gesture.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="modifiers"/> is not valid <see cref="ModifierKeys"/> value</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="command"/> is null</exception>
        ''' <seelaso cref="FreeKeyGesture"/>
        Public Sub New(command As ICommand, key As Key, Optional modifiers As ModifierKeys = ModifierKeys.None)
            MyBase.New(command, New FreeKeyGesture(key, modifiers))
        End Sub

        ''' <summary>CTor which creates <see cref="MouseGesture"/></summary>
        ''' <param name="command">The command to associate with gesture</param>
        ''' <param name="mouseAction">The action associated with <see cref="MouseGesture"/>.</param>
        ''' <param name="modifiers">The modifiers associated with <see cref="MouseGesture"/>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="command"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="mouseAction"/> is not a valid <see cref="MouseAction"/> value -or- <paramref name="modifiers"/> is not a valid <see cref="ModifierKeys"/> value.</exception>
        ''' <seelaso cref="MouseGesture"/>
        Public Sub New(command As ICommand, mouseAction As MouseAction, Optional modifiers As ModifierKeys = ModifierKeys.None)
            MyBase.New(command, New MouseGesture(mouseAction, modifiers))
        End Sub

        ''' <summary>CTor which creates <see cref="MouseWheelGesture"/></summary>
        ''' <param name="command">The command to associate with gesture</param>
        ''' <param name="direction">Direction of wheel movement</param>
        ''' <param name="delta">Number of wheel clicks (0 means any)</param>
        ''' <param name="modifiers">Modifier keys</param>
        ''' <exception cref="ArgumentNullException"><paramref name="command"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="modifiers"/> is not valid <see cref="ModifierKeys"/> value</exception>
        ''' <seelaso cref="MouseWheelGesture"/>
        Public Sub New(command As ICommand, direction As MouseWheelDirection, Optional delta% = 0, Optional modifiers As ModifierKeys = ModifierKeys.None)
            MyBase.New(command, New MouseWheelGesture(direction, delta, modifiers))
        End Sub

        ''' <summary>Creates an instance of an <see cref="FreeInputBinding" />.</summary>
        ''' <returns>The new object.</returns>
        Protected Overrides Function CreateInstanceCore() As System.Windows.Freezable
            Return New FreeInputBinding()
        End Function
    End Class
End Namespace
