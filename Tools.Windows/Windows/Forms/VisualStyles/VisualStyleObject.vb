Imports System.Windows.Forms, System.Windows.Forms.VisualStyles, System.Drawing
Imports Tools.DrawingT, Tools.ExtensionsT
Imports System.Runtime.CompilerServices
Imports Tools.ResourcesT

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.FormsT.VisualStylesT
    ''' <summary>Abstract base class for visual style object renderers</summary>
    ''' <remarks>This class is intended as simplified encapsulation of <see cref="VisualStyleRenderer"/>, which can be replaced by another derived class.
    ''' <see cref="VisualStyleRenderer"/> is encapsulated by <see cref="UxThemeObject"/> class.</remarks>
    Public MustInherit Class VisualStyleObject
        ''' <summary>When overriden in derived class, draws portion of element background</summary>
        ''' <param name="g">raphics to draw element background on</param>
        ''' <param name="rect">Rectangle identifiing position and size of visual style element object</param>
        ''' <param name="clip">Postion of <paramref name="g"/> that was invalidated and must be redrawn</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        Public MustOverride Sub DrawBackground(ByVal g As Graphics, ByVal rect As Rectangle, ByVal clip As Rectangle)
        ''' <summary>Draws whole element background to area specified by coordinates and size</summary>
        ''' <param name="g">Graphics to draw background on</param>
        ''' <param name="x">Horizontal postion of left edge of element, in pixels</param>
        ''' <param name="y">Vertical postioon of top edge of element, in pixels</param>
        ''' <param name="Width">With of element, in pixels</param>
        ''' <param name="Height">Height of element, in pixels</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        Public Sub DrawBackground(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%)
            DrawBackground(g, New Rectangle(x, y, Width, Height))
        End Sub
        ''' <summary>Draws whole element bacground to area specified by given <see cref="Rectangle"/></summary>
        ''' <param name="g">Graphics to draw background on</param>
        ''' <param name="rect">Rectangle identifiing position and size of visual style element object</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        Public Sub DrawBackground(ByVal g As Graphics, ByVal rect As Rectangle)
            DrawBackground(g, rect, rect)
        End Sub
        ''' <summary>When overiden in derived class, gets rectangle representing area of visual element reserved for its content</summary>
        ''' <param name="g">Graphic to use, when necesary</param>
        ''' <param name="rect">Size of whole visual style element</param>
        ''' <returns>Area of element reserved for its content</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and derived class needs it</exception>
        Public MustOverride Function GetContentRectangle(ByVal g As Graphics, ByVal rect As Rectangle) As Rectangle
        ''' <summary>Gets rectangle representing area of visual element reserved for its content</summary>
        ''' <param name="g">Graphic to use, when necesary</param>
        ''' <param name="x">Horizontal postion of left edge of element, in pixels</param>
        ''' <param name="y">Vertical postioon of top edge of element, in pixels</param>
        ''' <param name="Width">With of element, in pixels</param>
        ''' <param name="Height">Height of element, in pixels</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and derived class needs it</exception>
        Public Function GetContentRectangle(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%) As Rectangle
            Return GetContentRectangle(g, New Rectangle(x, y, Width, Height))
        End Function
        ''' <summary>Computes padding from content rectangle</summary>
        ''' <param name="g">Graphic to use, when necesary</param>
        ''' <param name="rect">Size of whole visual style element</param>
        ''' <returns>Padding of element (thicknesses of its borders)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and derived class needs it</exception>
        Public Function GetPadding(ByVal g As Graphics, ByVal rect As Rectangle) As Padding
            With GetContentRectangle(g, rect)
                Return New Padding(.Left - rect.Left, .Top - rect.Top, rect.Right - .Right, rect.Bottom - .Bottom)
            End With
        End Function
        ''' <summary>Computes padding from content rectangle</summary>
        ''' <param name="g">Graphic to use, when necesary</param>
        ''' <param name="x">Horizontal postion of left edge of element, in pixels</param>
        ''' <param name="y">Vertical postioon of top edge of element, in pixels</param>
        ''' <param name="Width">With of element, in pixels</param>
        ''' <param name="Height">Height of element, in pixels</param>
        ''' <returns>Padding of element (thicknesses of its borders)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and derived class needs it</exception>
        Public Function GetPadding(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%) As Padding
            Return GetPadding(g, New Rectangle(x, x, Width, Height))
        End Function
        ''' <summary>When overriden in derived class, returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
        ''' <param name="g">The <see cref="System.Drawing.Graphics" /> this operation will use.</param>
        ''' <param name="Rect">A <see cref="System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
        ''' <param name="type">One of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
        ''' <returns>A <see cref="System.Drawing.Size" /> that contains the size specified by the type parameter for the current visual style part.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="g"/> is null and drived class needs it.</exception>
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException"><paramref name="Type"/> is not one of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
        Public MustOverride Function GetSize(ByVal g As Graphics, ByVal Type As ThemeSizeType, ByVal Rect As Rectangle) As Size
        ''' <summary>Draws focus rectangle</summary>
        ''' <param name="g">Graphics to draw focus rectangle on</param>
        ''' <param name="Rect">Rectangle representign size and position of either control rectangle or control content rectangle depending on <paramref name="ComputeContentRectangle"/></param>
        ''' <param name="ComputeContentRectangle">True to call <see cref="GetContentRectangle"/> and use its result to draw focus rectangle; false to draw focus rectange using <paramref name="Rect"/> directly</param>
        Public Overridable Sub DrawFocusRectangle(ByVal g As Graphics, ByVal Rect As Rectangle, Optional ByVal ComputeContentRectangle As Boolean = True)
            ControlPaint.DrawFocusRectangle(g, If(ComputeContentRectangle, GetContentRectangle(g, Rect), Rect))
        End Sub
        ''' <summary>Draws focus rectangle</summary>
        ''' <param name="g">Graphics to draw focus rectangle on</param>
        ''' <param name="x">X position of either control rectangle or focus rectangle depending on <paramref name="ComputeContentRectangle"/></param>
        ''' <param name="y">Y position of either control rectangle or focus rectangle depending on <paramref name="ComputeContentRectangle"/></param>
        ''' <param name="Width">Width of either control rectangle or focus rectangle depending on <paramref name="ComputeContentRectangle"/></param>
        ''' <param name="Height">Height position of either control rectangle or focus rectangle depending on <paramref name="ComputeContentRectangle"/></param>
        ''' <param name="ComputeContentRectangle">True to call <see cref="GetContentRectangle"/> and use its result to draw focus rectangle; false to draw focus rectange using passed dimensions directly</param>
        Public Sub DrawFocusRectangle(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%, Optional ByVal ComputeContentRectangle As Boolean = True)
            Me.DrawFocusRectangle(g, New Rectangle(x, y, Width, Height), ComputeContentRectangle)
        End Sub
    End Class
    ''' <summary>Renderer of visual style object implemented either by <see cref="UxThemeObject"/> when visual styles are supported and requested object can be drawn; or by custom renderer</summary>
    ''' <remarks>This class simply encapsulates another <see cref="VisualStyleObject"/>.</remarks>
    Public Class VisualStyleSafeObject
        Inherits VisualStyleObject
#Region "Encapsulation"
        ''' <summary>Contains value of the <see cref="InternalObject"/> property</summary>
        Private _InternalObject As VisualStyleObject
        ''' <summary>CTor from internal implementation</summary>
        ''' <param name="Internal"><see cref="VisualStyleObject"/> to be encapsulated</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Internal"/> is null</exception>
        Protected Sub New(ByVal Internal As UxThemeObject)
            If Internal Is Nothing Then Throw New ArgumentNullException("Internal")
            _InternalObject = Internal
        End Sub
        ''' <summary>CTor from internal implementation</summary>
        ''' <param name="Internal"><see cref="VisualStyleObject"/> to be encapsulated</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Internal"/> is null</exception>
        Protected Sub New(ByVal Internal As NonThemedVisualStyleObject)
            If Internal Is Nothing Then Throw New ArgumentNullException("Internal")
            _InternalObject = Internal
        End Sub
        ''' <summary>Gets value indicationg is this instance uses visual styles</summary>
        ''' <returns>True if <see cref="InternalObject"/> is <see cref="UxThemeObject"/></returns>
        Public ReadOnly Property UseVisualStyle() As Boolean
            Get
                Return TypeOf InternalObject Is UxThemeObject
            End Get
        End Property
        ''' <summary>Gets encapsulated <see cref="VisualStyleObject"/></summary>
        ''' <returns>Encapsulated <see cref="VisualStyleObject"/></returns>
        ''' <value>New internal visual style objet</value>
        ''' <exception cref="TypeMismatchException">Value being set is neither <see cref="UxThemeObject"/> nor <see cref="NonThemedVisualStyleObject"/></exception>
        Protected Friend Overridable Property InternalObject() As VisualStyleObject
            Get
                Return _InternalObject
            End Get
            Protected Set(ByVal value As VisualStyleObject)
                If Not TypeOf value Is UxThemeObject AndAlso Not TypeOf value Is NonThemedVisualStyleObject Then
                    Throw New TypeMismatchException("value", value, GetType(VisualStyleObject), Exceptions.InternalObjectMustBeEitherUxThemeObjectOrNonThemedVisualStyleObject)
                End If
                _InternalObject = value
            End Set
        End Property
        ''' <summary>Draws portion of element background</summary>
        ''' <param name="g">Graphics to draw element background on</param>
        ''' <param name="rect">Rectangle identifiing position and size of visual style element object</param>
        ''' <param name="clip">Postion of <paramref name="g"/> that was invalidated and must be redrawn</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        Public Overloads Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle, ByVal clip As System.Drawing.Rectangle)
            InternalObject.DrawBackground(g, rect, clip)
        End Sub
        ''' <summary>Gets rectangle representing area of visual element reserved for its content</summary>
        ''' <param name="g">Graphic to use, when necesary</param>
        ''' <param name="rect">Size of whole visual style element</param>
        ''' <returns>Area of element reserved for its content</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and encapsulated instance class needs it</exception>
        Public Overloads Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle) As System.Drawing.Rectangle
            Return InternalObject.GetContentRectangle(g, rect)
        End Function
        ''' <summary>Returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
        ''' <param name="g">The <see cref="System.Drawing.Graphics" /> this operation will use.</param>
        ''' <param name="Rect">A <see cref="System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
        ''' <param name="type">One of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
        ''' <returns>A <see cref="System.Drawing.Size" /> that contains the size specified by the type parameter for the current visual style part.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="g"/> is null and encapsulated instance needs it.</exception>
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException"><paramref name="Type"/> is not one of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
        Public Overrides Function GetSize(ByVal g As System.Drawing.Graphics, ByVal Type As System.Windows.Forms.VisualStyles.ThemeSizeType, ByVal Rect As System.Drawing.Rectangle) As System.Drawing.Size
            Return InternalObject.GetSize(g, Type, Rect)
        End Function
#End Region
#Region "Implementations"
        ''' <summary>Abstract base class of non-themed contrl rendereres</summary>
        Public MustInherit Class NonThemedVisualStyleObject
            Inherits VisualStyleObject
        End Class
#Region "Button"
        ''' <summary>Gets visual style object capable of rendiring with or without visual styles depending on if visual style is available</summary>
        ''' <param name="State">State of button</param>
        ''' <returns><see cref="VisualStyleSafeObject"/> capable of rendering of button</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="State"/> is not member of <see cref="PushButtonState"/> and visual styles are not supported</exception>
        Public Shared Function CreateButton(ByVal State As PushButtonState) As SafeButtonRenderer
            If VisualStyleRenderer.IsSupported Then
                If UxThemeObject.IsSupported(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, State) Then
                    Return New SafeButtonRenderer(New UxThemeObject(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, State))
                Else
                    Select Case State
                        Case PushButtonState.Default, PushButtonState.Hot, PushButtonState.Pressed, PushButtonState.Disabled : Return CreateButton(PushButtonState.Normal)
                        Case Else : Return New SafeButtonRenderer(New OldButtonRenderer(State))
                    End Select
                End If
            Else
                Return New SafeButtonRenderer(New OldButtonRenderer(State))
            End If
        End Function
        ''' <summary>Implements theme-aware theme-independent renderer of <see cref="Button"/> control</summary>
        Public Class SafeButtonRenderer
            Inherits VisualStyleSafeObject
            ''' <summary>CTor from theme-unaware renderer</summary>
            ''' <param name="Renderer">Renderer to encapsulate</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Renderer"/> is null</exception>
            Public Sub New(ByVal Renderer As OldButtonRenderer)
                MyBase.New(Renderer)
            End Sub
            ''' <summary>CTor from theme-aware renderer</summary>
            ''' <param name="Renderer">Renderer to encapsulate</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Renderer"/> is null</exception>
            ''' <exception cref="ArgumentException"><paramref name="Renderer"/> does not render <see cref="VisualStyleElement.Button.PushButton"/>.</exception>
            Public Sub New(ByVal Renderer As UxThemeObject)
                MyBase.New(Renderer)
            End Sub
            ''' <summary>Gets or sets state of button being rendered</summary>
            ''' <returns>State of button being rendered</returns>
            ''' <value>New state of button being rendered</value>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="PushButtonState"/></exception>
            ''' <remarks>Changing this property may leed to change of theme-aware to theme unaware renderer if <see cref="VisualStyleElement.Button.PushButton"/> is no longer supported (due to theme change).
            ''' If theme does not support certain state, <see cref="PushButtonState.Normal"/> is used instead.</remarks>
            Public Property State() As PushButtonState
                Get
                    If TypeOf InternalObject Is UxThemeObject Then
                        Return DirectCast(InternalObject, UxThemeObject).State
                    Else
                        Return DirectCast(InternalObject, OldButtonRenderer).State
                    End If
                End Get
                Set(ByVal value As PushButtonState)
                    If TypeOf InternalObject Is OldButtonRenderer Then
                        DirectCast(InternalObject, OldButtonRenderer).State = value
                    Else
                        With DirectCast(InternalObject, UxThemeObject)
                            If UxThemeObject.IsSupported(.Renderer.Class, .Renderer.Part, value) Then
                                .State = value
                            ElseIf UxThemeObject.IsSupported(.Renderer.Class, .Renderer.Part, PushButtonState.Normal) Then
                                .State = PushButtonState.Normal
                            Else
                                InternalObject = New OldButtonRenderer(value)
                            End If
                        End With
                    End If
                End Set
            End Property
            ''' <summary>Gets or sets value indicationg is this instance uses visual styles</summary>
            ''' <returns>True if <see cref="InternalObject"/> is <see cref="UxThemeObject"/></returns>
            ''' <value>True to use visual styls, false not to use visual styles. If change form false to true faild, no exception is thrown and value of property remains unchanged.</value>
            Public Shadows Property UseVisualStyle() As Boolean
                Get
                    Return MyBase.UseVisualStyle
                End Get
                Set(ByVal value As Boolean)
                    If value <> UseVisualStyle Then
                        If value Then
                            If UxThemeObject.IsSupported(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, Me.State) Then
                                InternalObject = New UxThemeObject(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, State)
                            ElseIf UxThemeObject.IsSupported(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, PushButtonState.Normal) Then
                                InternalObject = New UxThemeObject(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, PushButtonState.Normal)
                            End If
                        Else
                            InternalObject = New OldButtonRenderer(Me.State)
                        End If
                    End If
                End Set
            End Property
            ''' <summary>Gets encapsulated <see cref="VisualStyleObject"/></summary>
            ''' <returns>Encapsulated <see cref="VisualStyleObject"/></returns>
            ''' <value>New internal visual style objet</value>
            ''' <exception cref="TypeMismatchException">Value being set is neither <see cref="UxThemeObject"/> nor <see cref="NonThemedVisualStyleObject"/></exception>
            ''' <exception cref="ArgumentException">Value being set is <see cref="UxThemeObject"/> but it does not represent <see cref="VisualStyleElement.Button.PushButton"/>.</exception>
            Protected Friend Overrides Property InternalObject() As VisualStyleObject
                Get
                    Return MyBase.InternalObject
                End Get
                Protected Set(ByVal value As VisualStyleObject)
                    If TypeOf value Is UxThemeObject Then
                        With DirectCast(value, UxThemeObject)
                            If .ClassName <> UxThemeObject.VisualStyleClass.Button.ClassName OrElse .Part <> UxThemeObject.ButtonPart.Button Then _
                                Throw New ArgumentException(Exceptions.RendererMustRender0.f("PushButton"))
                        End With
                    End If
                    MyBase.InternalObject = value
                End Set
            End Property
        End Class
        ''' <summary>Implements renderer for <see cref="Button"/> control without visual styles</summary>
        Public Class OldButtonRenderer
            Inherits NonThemedVisualStyleObject
            ''' <summary>CTor</summary>
            ''' <param name="State">State of button</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="State"/> is not member of <see cref="PushButtonState"/></exception>
            Public Sub New(ByVal State As PushButtonState)
                Me.State = State
            End Sub
            ''' <summary>Contains value of the <see cref="State"/> property</summary>
            Private _State As PushButtonState
            ''' <summary>Gets or sets state to be rendered</summary>
            ''' <returns>Current state of button rendered</returns>
            ''' <value>State of button to be rendered</value>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="PushButtonState"/></exception>
            Public Property State() As PushButtonState
                Get
                    Return _State
                End Get
                Set(ByVal value As PushButtonState)
                    If Not value.IsDefined Then Throw New InvalidEnumArgumentException(value, "value", value.GetType)
                    _State = value
                End Set
            End Property
            ''' <summary>Gets state to be rendered as <see cref="Windows.Forms.ButtonState"/></summary>
            ''' <returns>If <see cref="State"/> is <see cref="PushButtonState.Pressed"/> returns <see cref="ButtonState.Pushed"/>.
            ''' If <see cref="State"/> is <see cref="PushButtonState.Disabled"/> return <see cref="ButtonState.Inactive"/>.
            ''' Otherwise returns <see cref="ButtonState.Normal"/></returns>
            Public ReadOnly Property ButtonState() As ButtonState
                Get
                    Select Case State
                        Case PushButtonState.Pressed : Return Windows.Forms.ButtonState.Pushed
                        Case PushButtonState.Disabled : Return Windows.Forms.ButtonState.Inactive
                        Case Else : Return Windows.Forms.ButtonState.Normal
                    End Select
                End Get
            End Property
            ''' <summary>Draws portion of button without visual styles</summary>
            ''' <param name="g">Graphics to draw button background on</param>
            ''' <param name="rect">Rectangle identifiing position and size of button</param>
            ''' <param name="clip">Postion of <paramref name="g"/> that was invalidated and must be redrawn</param>
            ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
            Public Overloads Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle, ByVal clip As System.Drawing.Rectangle)
                Dim oc = g.Clip
                g.IntersectClip(clip)
                Try
                    ControlPaint.DrawButton(g, rect, ButtonState)
                Finally
                    g.Clip = oc
                End Try
            End Sub

            ''' <summary>Gets rectangle representing area of button reserved for its content</summary>
            ''' <param name="g">Ignored</param>
            ''' <param name="rect">Size of whole button element</param>
            ''' <returns>Area of button reserved for its content. Actually it's area with pading 3 on each side</returns>
            Public Overloads Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle) As System.Drawing.Rectangle
                Return New Rectangle(rect.X + 3, rect.Y + 3, rect.Width - 6, rect.Height - 6)
            End Function

            ''' <summary>Returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
            ''' <param name="g">Ignored</param>
            ''' <param name="Rect">A <see cref="System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
            ''' <param name="type">One of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
            ''' <returns>A <see cref="System.Drawing.Size" /> that contains the size specified by the type parameter for the current visual style part. For <paramref name="Type"/> <see cref="ThemeSizeType.Draw"/> and <see cref="ThemeSizeType.[True]"/> returns <paramref name="Rect"/>, for <see cref="ThemeSizeType.Minimum"/> returns square of size 7.</returns>
            ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException"><paramref name="Type"/> is not one of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
            Public Overrides Function GetSize(ByVal g As System.Drawing.Graphics, ByVal Type As System.Windows.Forms.VisualStyles.ThemeSizeType, ByVal Rect As System.Drawing.Rectangle) As System.Drawing.Size
                Select Case Type
                    Case ThemeSizeType.Draw, ThemeSizeType.True : Return Rect.Size
                    Case ThemeSizeType.Minimum : Return New Size(7, 7)
                    Case Else : Throw New InvalidEnumArgumentException("Type", Type, Type.GetType)
                End Select
            End Function
        End Class
#End Region
#End Region
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="VisualStyleSafeObject" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="VisualStyleSafeObject" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="VisualStyleSafeObject" />. </param>
        ''' <remarks>Object is considered eqzual to <see cref="VisualStyleSafeObject"/> if it is either <see cref="VisualStyleSafeObject"/> with same <see cref="InternalObject"/> or it is <see cref="VisualStyleObject"/> equal to <see cref="InternalObject"/>.</remarks>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is VisualStyleSafeObject Then
                Return Me.InternalObject.Equals(DirectCast(obj, VisualStyleSafeObject).InternalObject)
            ElseIf TypeOf obj Is VisualStyleObject Then
                Return Me.InternalObject.Equals(obj)
            End If
            Return MyBase.Equals(obj)
        End Function
    End Class
    ''' <summary>Encalsulates <see cref="VisualStyleRenderer"/> to simplified form</summary>
    Public Class UxThemeObject
        Inherits VisualStyleObject
        Implements IEquatable(Of VisualStyleElement), IEquatable(Of VisualStyleRenderer), IEquatable(Of VisualStyleSafeObject), IEquatable(Of UxThemeObject)
        ''' <summary>Test if current visual style supports element identified by class name, part number and state</summary>
        ''' <param name="Class">String identifiying class of visual element</param>
        ''' <param name="Part">Part of visual element</param>
        ''' <param name="State">State of visual element</param>
        ''' <returns>True when visual style are supported and current visual style supportes element identified by parameters; otherwise false</returns>
        Public Shared Function IsSupported(ByVal Class$, ByVal Part%, ByVal State%) As Boolean
            Return VisualStyleRenderer.IsSupported AndAlso VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement([Class], Part, State))
        End Function
        ''' <summary>Test if current visual style supports element identified by class name, part number and state</summary>
        ''' <param name="Class">Value identifiying class of visual element</param>
        ''' <param name="Part">Part of visual element</param>
        ''' <param name="State">State of visual element</param>
        ''' <returns>True when visual style are supported and current visual style supportes element identified by parameters; otherwise false</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Class"/> is neither zero nor member of <see cref="VisualStyleClass"/> enumeration</exception>
        Public Shared Function IsSupported(ByVal [Class] As VisualStyleClass, ByVal Part%, ByVal State%) As Boolean
            Return VisualStyleRenderer.IsSupported AndAlso VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement([Class].ClassName, Part, State))
        End Function

        ''' <summary>CTor from class, part and state</summary>
        ''' <param name="ClassName">String identifiying class of visual element</param>
        ''' <param name="Part">Part of visual element</param>
        ''' <param name="State">State of visual element</param>
        ''' <exception cref="InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
        ''' <exception cref="ArgumentException">The combination of <paramref name="className"/>, <paramref name="part"/>, and <paramref name="state"/> is not defined by the current visual style.</exception>
        ''' <seelaso cref="VisualStyleRenderer"/>
        Public Sub New(ByVal ClassName$, ByVal Part%, ByVal State%)
            _renderer = New VisualStyleRenderer(ClassName, Part, State)
        End Sub
        ''' <summary>CTor from class, part and state</summary>
        ''' <param name="CLass">Value identifiying class of visual element</param>
        ''' <param name="Part">Part of visual element</param>
        ''' <param name="State">State of visual element</param>
        ''' <exception cref="InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
        ''' <exception cref="ArgumentException">The combination of <paramref name="className"/>, <paramref name="part"/>, and <paramref name="state"/> is not defined by the current visual style.</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Class"/> is neither zero nor member of <see cref="VisualStyleClass"/>. Note: When <paramref name="Class"/> is zero, <see cref="ArgumentException"/> is thrown.</exception>
        ''' <seelaso cref="VisualStyleRenderer"/>
        Public Sub New(ByVal [Class] As VisualStyleClass, ByVal Part%, ByVal State%)
            _renderer = New VisualStyleRenderer([Class].ClassName, Part, State)
        End Sub
        ''' <summary>CTor form <see cref="VisualStyleElement"/></summary>
        ''' <param name="Element">Visual element to encapsulate</param>
        ''' <exception cref="InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not defined by the current visual style.</exception>
        ''' <seelaso cref="VisualStyleRenderer"/>
        Public Sub New(ByVal Element As VisualStyleElement)
            _renderer = New VisualStyleRenderer(Element)
        End Sub
        ''' <summary>CTor from <see cref="VisualStyleRenderer"/></summary>
        ''' <param name="Renderer">A <see cref="VisualStyleRenderer"/> to encapsulate</param>
        Public Sub New(ByVal Renderer As VisualStyleRenderer)
            _renderer = Renderer
        End Sub
        ''' <summary>Gets the class name of the current visual style element.</summary>
        ''' <returns>A string that identifies the class of the current visual style element.</returns>
        Public ReadOnly Property ClassName$()
            Get
                Return Renderer.Class
            End Get
        End Property
        ''' <summary>Gets the part of the current visual style element.</summary>
        ''' <returns>A value that specifies the part of the current visual style element.</returns>
        Public ReadOnly Property Part%()
            Get
                Return Renderer.Part
            End Get
        End Property
        ''' <summary>Gets the state of the current visual style element.</summary>
        ''' <returns>A value that identifies the state of the current visual style element.</returns>
        Public Property State%()
            Get
                Return Renderer.State
            End Get
            Set(ByVal value%)
                _renderer = New VisualStyleRenderer(ClassName, Part, value)
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Renderer"/> property</summary>
        Private _renderer As VisualStyleRenderer
        ''' <summary>Gets <see cref="VisualStyleRenderer"/> encapsulated by this instance</summary>
        ''' <returns><see cref="VisualStyleRenderer"/> encapsulated by this instance</returns>
        Public ReadOnly Property Renderer() As VisualStyleRenderer
            Get
                Return _renderer
            End Get
        End Property
        ''' <summary>Gets <see cref="VisualStyleElement"/> encapsulated by this instance</summary>
        ''' <returns><see cref="VisualStyleElement"/> encapsulated by this instance</returns>
        Public Function GetElement() As VisualStyleElement
            Return VisualStyleElement.CreateElement(ClassName, Part, State)
        End Function
        ''' <summary>Draws background part using current <see cref="VisualStyleRenderer"/></summary>
        ''' <param name="g">Graphisc to draw on</param>
        ''' <param name="rect">Rectangle representing size and position of element</param>
        ''' <param name="clip">Part of element to be drawn</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        ''' <seelaso cref="VisualStyleRenderer.DrawBackground"/>
        Public Overloads Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle, ByVal clip As Rectangle)
            Renderer.DrawBackground(g, rect, clip)
        End Sub
        ''' <summary>Returns the content area for the background of the current visual style element.</summary>
        ''' <param name="g">The <see cref="System.Drawing.Graphics" /> this operation will use.</param>
        ''' <param name="rect">A <see cref="System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</param>
        ''' <returns>A <see cref="System.Drawing.Rectangle" /> that contains the content area for the background of the current visual style element.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="g"/> is null.</exception>
        ''' <seelaso cref="VisualStyleRenderer.GetBackgroundContentRectangle"/>
        Public Overloads Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle) As System.Drawing.Rectangle
            Return Renderer.GetBackgroundContentRectangle(g, rect)
        End Function
        ''' <summary>Returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
        ''' <param name="g">The <see cref="System.Drawing.Graphics" /> this operation will use.</param>
        ''' <param name="Rect">A <see cref="System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
        ''' <param name="type">One of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
        ''' <returns>A <see cref="System.Drawing.Size" /> that contains the size specified by the type parameter for the current visual style part.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="g"/> is null.</exception>
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException"><paramref name="Type"/> is not one of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
        ''' <seelaso cref="VisualStyleRenderer.GetPartSize"/>
        Public Overrides Function GetSize(ByVal g As System.Drawing.Graphics, ByVal Type As ThemeSizeType, ByVal Rect As System.Drawing.Rectangle) As Size
            Renderer.GetPartSize(g, Rect, Type)
        End Function
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="UxThemeObject" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="UxThemeObject" />. </param>
        ''' <remarks>Object is considered equal to <see cref="UxThemeObject"/> when it is either <see cref="UxThemeObject"/>, <see cref="VisualStyleElement"/>, <see cref="VisualStyleRenderer"/> or <see cref="VisualStyleSafeObject"/> and represents same <see cref="ClassName"/>, <see cref="Part"/> and <see cref="State"/>.</remarks>
        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If obj Is Me Then Return True
            If TypeOf obj Is UxThemeObject Then
                Return Me.Equals(DirectCast(obj, UxThemeObject))
            ElseIf TypeOf obj Is VisualStyleElement Then
                Return Me.Equals(DirectCast(obj, VisualStyleElement))
            ElseIf TypeOf obj Is VisualStyleRenderer Then
                Return Me.Equals(DirectCast(obj, VisualStyleRenderer))
            ElseIf TypeOf obj Is VisualStyleSafeObject Then
                Return Me.Equals(DirectCast(obj, VisualStyleSafeObject))
            End If
            Return MyBase.Equals(obj)
        End Function
        ''' <summary>Determines whether the specified <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement" /> is equal to the current <see cref="UxThemeObject" />.</summary>
        ''' <returns>true if the specified <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
        ''' <param name="other">The <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement" /> to compare with the current <see cref="UxThemeObject" />. </param>
        Public Overloads Function Equals(ByVal other As System.Windows.Forms.VisualStyles.VisualStyleElement) As Boolean Implements System.IEquatable(Of System.Windows.Forms.VisualStyles.VisualStyleElement).Equals
            If other Is Nothing Then Return False
            Return Me.ClassName = other.ClassName AndAlso Me.Part = other.Part AndAlso Me.State = other.State
        End Function
        ''' <summary>Determines whether the specified <see cref="System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> is equal to the current <see cref="UxThemeObject" />.</summary>
        ''' <returns>true if the specified <see cref="System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
        ''' <param name="other">The <see cref="System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> to compare with the current <see cref="UxThemeObject" />. </param>
        Public Overloads Function Equals(ByVal other As System.Windows.Forms.VisualStyles.VisualStyleRenderer) As Boolean Implements System.IEquatable(Of System.Windows.Forms.VisualStyles.VisualStyleRenderer).Equals
            If other Is Nothing Then Return False
            Return Me.ClassName = other.Class AndAlso Me.Part = other.Part AndAlso Me.State = other.State
        End Function
        ''' <summary>Determines whether the specified <see cref="UxThemeObject" /> is equal to the current <see cref="UxThemeObject" />.</summary>
        ''' <returns>true if the specified <see cref="UxThemeObject" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
        ''' <param name="other">The <see cref="UxThemeObject" /> to compare with the current <see cref="UxThemeObject" />. </param>
        Public Overloads Function Equals(ByVal other As UxThemeObject) As Boolean Implements System.IEquatable(Of UxThemeObject).Equals
            If other Is Nothing Then Return False
            If other Is Me Then Return True
            Return Me.ClassName = other.ClassName AndAlso Me.Part = other.Part AndAlso Me.State = other.State
        End Function
        ''' <summary>Determines whether the specified <see cref="VisualStyleSafeObject" /> is equal to the current <see cref="UxThemeObject" />.</summary>
        ''' <returns>true if the specified <see cref="VisualStyleSafeObject" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
        ''' <param name="other">The <see cref="VisualStyleSafeObject" /> to compare with the current <see cref="UxThemeObject" />. </param>
        Public Overloads Function Equals(ByVal other As VisualStyleSafeObject) As Boolean Implements System.IEquatable(Of VisualStyleSafeObject).Equals
            If other Is Nothing Then Return False
            Return Me.Equals(other.InternalObject)
        End Function
#Region "Enums"
        ''' <summary>Enumerates classes used with visual styles</summary>
        ''' <seelaso cref="VisualStyleElement"/>
        Public Enum VisualStyleClass
            ''' <summary><see cref="Button"/>, <see cref="CheckBox"/>, <see cref="RadioButton"/> and <see cref="GroupBox"/> controls</summary>
            ''' <seelaso cref="VisualStyleElement.Button"/>
            Button = 1
            ''' <summary><see cref="ComboBox"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.ComboBox"/>
            ComboBox = 2
            ''' <summary>Exprorer bar control</summary>
            ''' <seelaso cref="VisualStyleElement.ExplorerBar"/>
            ExplorerBar = 3
            ''' <summary><see cref="ColumnHeader"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Header"/>
            Header = 4
            ''' <summary><see cref="ListView"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.ListView"/>
            ListView = 5
            ''' <summary><see cref="Menu"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Menu"/>
            Menu = 6
            ''' <summary>Menu band control</summary>
            ''' <seelaso cref="VisualStyleElement.MenuBand"/>
            MenuBand = 7
            ''' <summary>Page control</summary>
            ''' <seelaso cref="VisualStyleElement.Page"/>
            Page = 8
            ''' <summary><see cref="ProgressBar"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.ProgressBar"/>
            Progress = 9
            ''' <summary>Rebar control</summary>
            ''' <seelaso cref="VisualStyleElement.Rebar"/>
            Rebar = 10
            ''' <summary><see cref="ScrollBar"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.ScrollBar"/>
            ScrollBar = 11
            ''' <summary>Spin control</summary>
            ''' <seelaso cref="VisualStyleElement.Spin"/>
            Spin = 12
            ''' <summary>Componnts of start menu</summary>
            ''' <seelaso cref="VisualStyleElement.StartPanel"/>
            StartPanel = 13
            ''' <summary><see cref="StatusBar"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Status"/>
            Status = 14
            ''' <summary><see cref="TabControl"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Tab"/>
            Tab = 15
            ''' <summary>Task band control (on taskbar)</summary>
            ''' <seelaso cref="VisualStyleElement.TaskBand"/>
            TaskBand = 16
            ''' <summary>System taskbar</summary>
            ''' <seelaso cref="VisualStyleElement.Taskbar"/>
            TaskBar = 17
            ''' <summary>Taskbar clock</summary>
            ''' <seelaso cref="VisualStyleElement.TaskbarClock"/>
            Clock = 18
            ''' <summary><see cref="TextBox"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.TextBox"/>
            Edit = 19
            ''' <summary><see cref="ToolBar"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.ToolBar"/>
            Toolbar = 20
            ''' <summary><see cref="ToolTip"/></summary>
            ''' <seelaso cref="VisualStyleElement.ToolTip"/>
            Tooltip = 21
            ''' <summary><see cref="TrackBar"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.TrackBar"/>
            Trackbar = 22
            ''' <summary><see cref="NotifyIcon"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.TrayNotify"/>
            TrayNotify = 23
            ''' <summary><see cref="TreeView"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.TreeView"/>
            TreeView = 24
            ''' <summary><see cref="Form"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Window"/>
            Window = 25
        End Enum
        ''' <summary>Parts of <see cref="VisualStyleElement.Button"/></summary>
        Public Enum ButtonPart
            ''' <summary><see cref="GroupBox"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Button.GroupBox"/>
            GroupBox = 4
            ''' <summary><see cref="CheckBox"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Button.CheckBox"/>
            CheckBox = 3
            ''' <summary><see cref="Button"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Button.PushButton"/>
            Button = 1
            ''' <summary><see cref="RadioButton"/> control</summary>
            ''' <seelaso cref="VisualStyleElement.Button.RadioButton"/>
            RadioButton = 2
            ''' <summary>User button control</summary>
            ''' <seelaso cref="VisualStyleElement.Button.UserButton"/>
            UserButton = 5
        End Enum
#End Region
    End Class
    ''' <summary>Provides extension mehods related to <see cref="VisualStyles"/></summary>
    Public Module VisualStyleExtensions
        ''' <summary>Gets name of class from <see cref="UxThemeObject.VisualStyleClass"/> value</summary>
        ''' <param name="Class">Visual style class enumerated value</param>
        ''' <returns>Class name, its actualy uppercase name of enumerated value; <see cref="System.String.Empty"/> when <paramref name="Class"/> is zero</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Class"/> is neither zero nor member of <see cref="UxThemeObject.VisualStyleClass"/></exception>
        <Extension()> _
        Public Function ClassName(ByVal [Class] As UxThemeObject.VisualStyleClass) As String
            If [Class] = 0 Then Return ""
            If Not [Class].IsDefined Then Throw New InvalidEnumArgumentException("Class", [Class], [Class].GetType)
            Return [Class].GetName.ToUpper
        End Function
        ''' <summary>Determines whether the specified visual style element is defined by the current visual style.</summary>
        ''' <param name="element">A <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement" /> whose class and part combination will be verified.</param>
        ''' <returns>true if the combination of the <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" /> and <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement.Part" /> properties of element are defined; otherwise, false.</returns>
        ''' <exception cref="System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual" styles are not applied to the client area of application windows.</exception>
        <Extension()> Function IsDefined(ByVal Element As VisualStyleElement) As Boolean
            Return VisualStyleRenderer.IsElementDefined(Element)
        End Function
    End Module
End Namespace
#End If