Imports System.Windows.Input, Tools.ExtensionsT, Tools
Imports System.Globalization
Imports System.Windows.Markup
Imports Tools.WindowsT.WPF.MarkupT

Namespace WindowsT.WPF.InputT

    ''' <summary>An alternative implementation of <see cref="KeyGesture"/> class which allows non-modified key to be matched</summary>
    ''' <remarks>
    ''' In XAML <see cref="FreeKeyGesture"/> cannot be used with <see cref="KeyBinding"/>. Use <see cref="FreeInputBinding"/> instead.
    ''' <example>
    ''' This example show how to instantiate <see cref="FreeKeyGesture"/> using <see cref="FreeInputBinding"/> in XAML.
    ''' The code below binds command <see cref="ApplicationCommands.Open"/> go key G.
    ''' <code lang="XAML"><![CDATA[<Window.InputBindings xmlns:ti="clr-namespace:Tools.WindowsT.WPF.InputT;assembly=Tools.Windows">
    '''     <ti:FreeInputBinding Command="Open">
    '''         <ti:FreeInputBinding.Gesture>
    '''             <ti:FreeKeyGesture>G</ti:FreeKeyGesture>
    '''         </ti:FreeInputBinding.Gesture>
    '''     </ti:FreeInputBinding>
    ''' </Window.InputBindings>]]></code>
    ''' </example>
    ''' </remarks>
    ''' <seelaso cref="KeyGesture"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <TypeConverter(GetType(FreeKeyGestureConverter))>
    <ValueSerializer(GetType(DefaultTypeConverterBasedValueSerializer(Of FreeKeyGesture)))>
    Public Class FreeKeyGesture
        Inherits InputGesture

        ''' <summary>Holds instance of <see cref="FreeKeyGestureConverter"/></summary>
        Private Shared ReadOnly converter As New FreeKeyGestureConverter

        ''' <summary>Initializes a new instance of the <see cref="FreeKeyGesture"/> class with the specified <see cref="Key"/>. </summary>
        ''' <param name="key">The key associated with this gesture.</param>
        Public Sub New(ByVal key As Key)
            Me.New(key, ModifierKeys.None)
        End Sub

        ''' <summary>Initializes a new instance of the <see cref="FreeKeyGesture"/> class with the specified <see cref="Key"/> and <see cref="ModifierKeys"/>.</summary>
        ''' <param name="key">The key associated with the gesture.</param>
        ''' <param name="modifiers">The modifier keys associated with the gesture.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="modifiers"/> is not valid <see cref="ModifierKeys"/> value</exception>
        Public Sub New(ByVal key As Key, ByVal modifiers As ModifierKeys)
            Me.New(key, modifiers, String.Empty)
        End Sub


        ''' <summary>Initializes a new instance of the <see cref="FreeKeyGesture"/> class with the specified <see cref="Key"/>, <see cref="ModifierKeys"/>, and display string.</summary>
        ''' <param name="key">The key associated with the gesture.</param>
        ''' <param name="modifiers">The modifier keys associated with the gesture.</param>
        ''' <param name="displayString">A string representation of the <see cref="FreeKeyGesture"/>.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="modifiers"/> is not valid <see cref="ModifierKeys"/> value</exception>
        Public Sub New(ByVal key As Key, ByVal modifiers As ModifierKeys, ByVal displayString As String)
            If Not ModifierKeysConverter.IsDefinedModifierKeys(modifiers) Then
                Throw New InvalidEnumArgumentException("modifiers", CInt(modifiers), GetType(ModifierKeys))
            End If

            If (displayString Is Nothing) Then Throw New ArgumentNullException("displayString")

            Me._modifiers = modifiers
            Me._key = key
            Me._displayString = displayString
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="FreeKeyGesture"/> class from an instance of <see cref="KeyGesture"/> class</summary>
        ''' <param name="gesture">A <see cref="KeyGesture"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="gesture"/> is null</exception>
        Public Sub New(gesture As KeyGesture)
            Me.New(gesture.ThrowIfNull("gesture").Key, gesture.Modifiers, gesture.DisplayString)
        End Sub


        ''' <summary>Determines whether this <see cref="FreeKeyGesture"/> matches the input associated with the specified <see cref="InputEventArgs"/> object.</summary>
        ''' <param name="targetElement">The target of the command.</param>
        ''' <param name="inputEventArgs">The input event data to compare this gesture to.</param>
        ''' <returns>true if the gesture matches the input; otherwise, false.</returns>
        Public Overrides Function Matches(targetElement As Object, inputEventArgs As System.Windows.Input.InputEventArgs) As Boolean
            Dim args As KeyEventArgs = TryCast(inputEventArgs, KeyEventArgs)
            If args Is Nothing Then Return False
            Return Me.Key = If(args.Key = Key.System, args.SystemKey, args.Key) AndAlso Me.Modifiers = Keyboard.Modifiers
        End Function

        ''' <summary>Returns a string that can be used to display the <see cref="FreeKeyGesture"/>.</summary>
        ''' <param name="culture">The culture specific information.</param>
        ''' <returns>The string to display </returns>
        ''' <remarks>If the display string was set by the constructor, that string is returned; otherwise, a string is created from the <see cref="Key"/> and <see cref="Modifiers"/> with any necessary conversions being governed by the specified <see cref="CultureInfo"/> object.</remarks>
        Public Function GetDisplayStringForCulture(ByVal culture As CultureInfo) As String
            If Not String.IsNullOrEmpty(Me._displayString) Then
                Return Me._displayString
            End If
            Return CStr(converter.ConvertTo(Nothing, culture, Me, GetType(String)))
        End Function

        Private ReadOnly _displayString$
        ''' <summary>Gets a string representation of this <see cref="FreeKeyGesture"/>.</summary>
        ''' <value>The display string for this <see cref="FreeKeyGesture"/>. The default value is <see cref="String.Empty"/>.</value>
        ''' <remarks>If a display string was not set in the constructor, an empty string is returned.
        ''' <para>If this property is empty, the <see cref="GetDisplayStringForCulture"/> method returns a string created from the <see cref="Key"/> and <see cref="Modifiers"/>.</para></remarks>
        Public ReadOnly Property DisplayString$
            Get
                Return _displayString
            End Get
        End Property

        Private ReadOnly _key As Key
        ''' <summary>Gets the key associated with this <see cref="FreeKeyGesture"/>.</summary>
        ''' <value>The key associated with the gesture. The default value is <see cref="Key.None"/>.</value>
        ''' <remarks>Unlike with <see cref="KeyGesture"/> <see cref="Modifiers"/> can be <see cref="ModifierKeys.None"/> for any <see cref="Key"/>.</remarks>
        Public ReadOnly Property Key As Key
            Get
                Return _key
            End Get
        End Property

        Private ReadOnly _modifiers As ModifierKeys
        ''' <summary>Gets the modifier keys associated with this <see cref="FreeKeyGesture"/>.</summary>
        ''' <value>The modifier keys associated with the gesture. The default value is <see cref="ModifierKeys.None"/>.</value>
        ''' <remarks>Unlike with <see cref="KeyGesture"/> this property can be <see cref="ModifierKeys.None"/> for any <see cref="Key"/>.</remarks>
        Public ReadOnly Property Modifiers As ModifierKeys
            Get
                Return _modifiers
            End Get
        End Property

        ''' <summary>Converts <see cref="KeyGesture"/> to <see cref="FreeKeyGesture"/></summary>
        ''' <param name="a">A <see cref="KeyGesture"/></param>
        ''' <returns>A new instance of <see cref="FreeKeyGesture"/> class initialized from <paramref name="a"/>; null if <paramref name="a"/> is null.</returns>
        Public Shared Widening Operator CType(a As KeyGesture) As FreeKeyGesture
            If a Is Nothing Then Return Nothing
            Return New FreeKeyGesture(a)
        End Operator

        ''' <summary>Converts <see cref="FreeKeyGesture"/> to <see cref="KeyGesture"/></summary>
        ''' <param name="a">A <see cref="FreeKeyGesture"/></param>
        ''' <returns>A new instance of <see cref="KeyGesture"/> initialized from <paramref name="a"/>; null if <paramref name="a"/> is null.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="a"/>.<see cref="Key">Key</see> is not a valid <see cref="System.Windows.Input.Key"/>.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="a"/>.<see cref="Key">Key</see> and <paramref name="a"/>.<see cref="Modifiers">Modifiers</see> do not form a valid <see cref="KeyGesture"/>.</exception>
        Public Shared Narrowing Operator CType(a As FreeKeyGesture) As KeyGesture
            If a Is Nothing Then Return Nothing
            Return New KeyGesture(a.Key, a.Modifiers, a.DisplayString)
        End Operator
    End Class

    ''' <summary>Converts a <see cref="FreeKeyGesture"/> object to and from other types.</summary>
    ''' <seelaso cref="KeyGestureConverter"/><seelaso cref="FreeKeyGesture"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class FreeKeyGestureConverter
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
            If (((destinationType Is GetType(String)) AndAlso (Not context Is Nothing)) AndAlso (Not context.Instance Is Nothing)) Then
                Dim instance As FreeKeyGesture = TryCast(context.Instance, FreeKeyGesture)
                If (Not instance Is Nothing) Then
                    Return modifierKeysConverter.IsDefinedModifierKeys(instance.Modifiers) 'AndAlso FreeKeyGestureConverter.IsDefinedKey(instance.Key)
                End If
            End If
            Return False
        End Function

        ''' <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        ''' <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
        ''' <param name="source">The <see cref="T:System.Object" /> to convert. </param>
        ''' <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        ''' <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal source As Object) As Object
            If ((Not source Is Nothing) AndAlso TypeOf source Is String) Then
                Dim str2 As String
                Dim str3 As String
                Dim str4 As String
                Dim str As String = CStr(source).Trim
                If (str = String.Empty) Then
                    Return New FreeKeyGesture(Key.None)
                End If
                Dim index As Integer = str.IndexOf(","c)
                If (index >= 0) Then
                    str4 = str.Substring((index + 1)).Trim
                    str = str.Substring(0, index).Trim
                Else
                    str4 = String.Empty
                End If
                index = str.LastIndexOf("+"c)
                If (index >= 0) Then
                    str3 = str.Substring(0, index)
                    str2 = str.Substring((index + 1))
                Else
                    str3 = String.Empty
                    str2 = str
                End If
                Dim none As ModifierKeys = ModifierKeys.None
                Dim obj3 As Object = FreeKeyGestureConverter.keyConverter.ConvertFrom(context, culture, str2)
                If (Not obj3 Is Nothing) Then
                    Dim obj2 As Object = FreeKeyGestureConverter.modifierKeysConverter.ConvertFrom(context, culture, str3)
                    If (Not obj2 Is Nothing) Then
                        none = DirectCast(obj2, ModifierKeys)
                    End If
                    Return New FreeKeyGesture(DirectCast(obj3, Key), none, str4)
                End If
            End If
            Throw MyBase.GetConvertFromException(source)
        End Function

        ''' <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        ''' <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
        ''' <param name="value">The <see cref="T:System.Object" /> to convert. </param>
        ''' <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
        ''' <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        ''' <exception cref="ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
        ''' <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
            If (destinationType Is Nothing) Then
                Throw New ArgumentNullException("destinationType")
            End If
            If (destinationType Is GetType(String)) Then
                If (value Is Nothing) Then
                    Return String.Empty
                End If
                Dim gesture As FreeKeyGesture = TryCast(value, FreeKeyGesture)
                If (Not gesture Is Nothing) Then
                    If (gesture.Key = Key.None) Then
                        Return String.Empty
                    End If
                    Dim str As String = ""
                    Dim str2 As String = CStr(FreeKeyGestureConverter.keyConverter.ConvertTo(context, culture, gesture.Key, destinationType))
                    If (str2 <> String.Empty) Then
                        str = (str & TryCast(FreeKeyGestureConverter.modifierKeysConverter.ConvertTo(context, culture, gesture.Modifiers, destinationType), String))
                        If (str <> String.Empty) Then
                            str = (str & "+"c)
                        End If
                        str = (str & str2)
                        If Not String.IsNullOrEmpty(gesture.DisplayString) Then
                            str = (str & ","c & gesture.DisplayString)
                        End If
                    End If
                    Return str
                End If
            End If
            Throw MyBase.GetConvertToException(value, destinationType)
        End Function


        ' Fields
        Friend Const DisplayStringSeparator As Char = ","c
        Private Shared ReadOnly keyConverter As KeyConverter = New KeyConverter
        Private Shared ReadOnly modifierKeysConverter As ModifierKeysConverter = New ModifierKeysConverter
        Private Const ModifiersDelimiter As Char = "+"c
    End Class

End Namespace
