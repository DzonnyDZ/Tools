Imports System.Windows.Forms, System.Windows.Forms.VisualStyles, System.Drawing
Imports Tools.DrawingT, Tools.ExtensionsT
Imports System.Runtime.CompilerServices, ps = System.Windows.Forms.VisualStyles.PushButtonState
Imports Tools.ResourcesT, System.Linq, Tools.LinqT

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.FormsT.VisualStylesT
    '#Region "V1"
    '    ''' <summary>Abstract base class for visual style object renderers</summary>
    '    ''' <remarks>This class is intended as simplified encapsulation of <see cref="VisualStyleRenderer"/>, which can be replaced by another derived class.
    '    ''' <see cref="VisualStyleRenderer"/> is encapsulated by <see cref="UxThemeObject"/> class.</remarks>
    '    Public MustInherit Class VisualStyleObject
    '        ''' <summary>When overriden in derived class, draws portion of element background</summary>
    '        ''' <param name="g">raphics to draw element background on</param>
    '        ''' <param name="rect">Rectangle identifiing position and size of visual style element object</param>
    '        ''' <param name="clip">Postion of <paramref name="g"/> that was invalidated and must be redrawn</param>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
    '        Public MustOverride Sub DrawBackground(ByVal g As Graphics, ByVal rect As Rectangle, ByVal clip As Rectangle)
    '        ''' <summary>Draws whole element background to area specified by coordinates and size</summary>
    '        ''' <param name="g">Graphics to draw background on</param>
    '        ''' <param name="x">Horizontal postion of left edge of element, in pixels</param>
    '        ''' <param name="y">Vertical postioon of top edge of element, in pixels</param>
    '        ''' <param name="Width">With of element, in pixels</param>
    '        ''' <param name="Height">Height of element, in pixels</param>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
    '        Public Sub DrawBackground(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%)
    '            DrawBackground(g, New Rectangle(x, y, Width, Height))
    '        End Sub
    '        ''' <summary>Draws whole element bacground to area specified by given <see cref="Rectangle"/></summary>
    '        ''' <param name="g">Graphics to draw background on</param>
    '        ''' <param name="rect">Rectangle identifiing position and size of visual style element object</param>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
    '        Public Sub DrawBackground(ByVal g As Graphics, ByVal rect As Rectangle)
    '            DrawBackground(g, rect, rect)
    '        End Sub
    '        ''' <summary>When overiden in derived class, gets rectangle representing area of visual element reserved for its content</summary>
    '        ''' <param name="g">Graphic to use, when necesary</param>
    '        ''' <param name="rect">Size of whole visual style element</param>
    '        ''' <returns>Area of element reserved for its content</returns>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and derived class needs it</exception>
    '        Public MustOverride Function GetContentRectangle(ByVal g As Graphics, ByVal rect As Rectangle) As Rectangle
    '        ''' <summary>Gets rectangle representing area of visual element reserved for its content</summary>
    '        ''' <param name="g">Graphic to use, when necesary</param>
    '        ''' <param name="x">Horizontal postion of left edge of element, in pixels</param>
    '        ''' <param name="y">Vertical postioon of top edge of element, in pixels</param>
    '        ''' <param name="Width">With of element, in pixels</param>
    '        ''' <param name="Height">Height of element, in pixels</param>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and derived class needs it</exception>
    '        Public Function GetContentRectangle(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%) As Rectangle
    '            Return GetContentRectangle(g, New Rectangle(x, y, Width, Height))
    '        End Function
    '        ''' <summary>Computes padding from content rectangle</summary>
    '        ''' <param name="g">Graphic to use, when necesary</param>
    '        ''' <param name="rect">Size of whole visual style element</param>
    '        ''' <returns>Padding of element (thicknesses of its borders)</returns>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and derived class needs it</exception>
    '        Public Function GetPadding(ByVal g As Graphics, ByVal rect As Rectangle) As Padding
    '            With GetContentRectangle(g, rect)
    '                Return New Padding(.Left - rect.Left, .Top - rect.Top, rect.Right - .Right, rect.Bottom - .Bottom)
    '            End With
    '        End Function
    '        ''' <summary>Computes padding from content rectangle</summary>
    '        ''' <param name="g">Graphic to use, when necesary</param>
    '        ''' <param name="x">Horizontal postion of left edge of element, in pixels</param>
    '        ''' <param name="y">Vertical postioon of top edge of element, in pixels</param>
    '        ''' <param name="Width">With of element, in pixels</param>
    '        ''' <param name="Height">Height of element, in pixels</param>
    '        ''' <returns>Padding of element (thicknesses of its borders)</returns>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and derived class needs it</exception>
    '        Public Function GetPadding(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%) As Padding
    '            Return GetPadding(g, New Rectangle(x, x, Width, Height))
    '        End Function
    '        ''' <summary>When overriden in derived class, returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
    '        ''' <param name="g">The <see cref="System.Drawing.Graphics" /> this operation will use.</param>
    '        ''' <param name="Rect">A <see cref="System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
    '        ''' <param name="type">One of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
    '        ''' <returns>A <see cref="System.Drawing.Size" /> that contains the size specified by the type parameter for the current visual style part.</returns>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="g"/> is null and drived class needs it.</exception>
    '        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException"><paramref name="Type"/> is not one of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
    '        Public MustOverride Function GetSize(ByVal g As Graphics, ByVal Type As ThemeSizeType, ByVal Rect As Rectangle) As Size
    '        ''' <summary>Draws focus rectangle</summary>
    '        ''' <param name="g">Graphics to draw focus rectangle on</param>
    '        ''' <param name="Rect">Rectangle representign size and position of either control rectangle or control content rectangle depending on <paramref name="ComputeContentRectangle"/></param>
    '        ''' <param name="ComputeContentRectangle">True to call <see cref="GetContentRectangle"/> and use its result to draw focus rectangle; false to draw focus rectange using <paramref name="Rect"/> directly</param>
    '        Public Overridable Sub DrawFocusRectangle(ByVal g As Graphics, ByVal Rect As Rectangle, Optional ByVal ComputeContentRectangle As Boolean = True)
    '            ControlPaint.DrawFocusRectangle(g, If(ComputeContentRectangle, GetContentRectangle(g, Rect), Rect))
    '        End Sub
    '        ''' <summary>Draws focus rectangle</summary>
    '        ''' <param name="g">Graphics to draw focus rectangle on</param>
    '        ''' <param name="x">X position of either control rectangle or focus rectangle depending on <paramref name="ComputeContentRectangle"/></param>
    '        ''' <param name="y">Y position of either control rectangle or focus rectangle depending on <paramref name="ComputeContentRectangle"/></param>
    '        ''' <param name="Width">Width of either control rectangle or focus rectangle depending on <paramref name="ComputeContentRectangle"/></param>
    '        ''' <param name="Height">Height position of either control rectangle or focus rectangle depending on <paramref name="ComputeContentRectangle"/></param>
    '        ''' <param name="ComputeContentRectangle">True to call <see cref="GetContentRectangle"/> and use its result to draw focus rectangle; false to draw focus rectange using passed dimensions directly</param>
    '        Public Sub DrawFocusRectangle(ByVal g As Graphics, ByVal x%, ByVal y%, ByVal Width%, ByVal Height%, Optional ByVal ComputeContentRectangle As Boolean = True)
    '            Me.DrawFocusRectangle(g, New Rectangle(x, y, Width, Height), ComputeContentRectangle)
    '        End Sub
    '    End Class
    '    ''' <summary>Renderer of visual style object implemented either by <see cref="UxThemeObject"/> when visual styles are supported and requested object can be drawn; or by custom renderer</summary>
    '    ''' <remarks>This class simply encapsulates another <see cref="VisualStyleObject"/>.</remarks>
    '    Public Class VisualStyleSafeObject
    '        Inherits VisualStyleObject
    '#Region "Encapsulation"
    '        ''' <summary>Contains value of the <see cref="InternalObject"/> property</summary>
    '        Private _InternalObject As VisualStyleObject
    '        ''' <summary>CTor from internal implementation</summary>
    '        ''' <param name="Internal"><see cref="VisualStyleObject"/> to be encapsulated</param>
    '        ''' <exception cref="ArgumentNullException"><paramref name="Internal"/> is null</exception>
    '        Protected Sub New(ByVal Internal As UxThemeObject)
    '            If Internal Is Nothing Then Throw New ArgumentNullException("Internal")
    '            _InternalObject = Internal
    '        End Sub
    '        ''' <summary>CTor from internal implementation</summary>
    '        ''' <param name="Internal"><see cref="VisualStyleObject"/> to be encapsulated</param>
    '        ''' <exception cref="ArgumentNullException"><paramref name="Internal"/> is null</exception>
    '        Protected Sub New(ByVal Internal As NonThemedVisualStyleObject)
    '            If Internal Is Nothing Then Throw New ArgumentNullException("Internal")
    '            _InternalObject = Internal
    '        End Sub
    '        ''' <summary>Gets value indicationg is this instance uses visual styles</summary>
    '        ''' <returns>True if <see cref="InternalObject"/> is <see cref="UxThemeObject"/></returns>
    '        Public ReadOnly Property UseVisualStyle() As Boolean
    '            Get
    '                Return TypeOf InternalObject Is UxThemeObject
    '            End Get
    '        End Property
    '        ''' <summary>Gets encapsulated <see cref="VisualStyleObject"/></summary>
    '        ''' <returns>Encapsulated <see cref="VisualStyleObject"/></returns>
    '        ''' <value>New internal visual style objet</value>
    '        ''' <exception cref="TypeMismatchException">Value being set is neither <see cref="UxThemeObject"/> nor <see cref="NonThemedVisualStyleObject"/></exception>
    '        Protected Friend Overridable Property InternalObject() As VisualStyleObject
    '            Get
    '                Return _InternalObject
    '            End Get
    '            Protected Set(ByVal value As VisualStyleObject)
    '                If Not TypeOf value Is UxThemeObject AndAlso Not TypeOf value Is NonThemedVisualStyleObject Then
    '                    Throw New TypeMismatchException("value", value, GetType(VisualStyleObject), Exceptions.InternalObjectMustBeEitherUxThemeObjectOrNonThemedVisualStyleObject)
    '                End If
    '                _InternalObject = value
    '            End Set
    '        End Property
    '        ''' <summary>Draws portion of element background</summary>
    '        ''' <param name="g">Graphics to draw element background on</param>
    '        ''' <param name="rect">Rectangle identifiing position and size of visual style element object</param>
    '        ''' <param name="clip">Postion of <paramref name="g"/> that was invalidated and must be redrawn</param>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
    '        Public Overloads Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle, ByVal clip As System.Drawing.Rectangle)
    '            InternalObject.DrawBackground(g, rect, clip)
    '        End Sub
    '        ''' <summary>Gets rectangle representing area of visual element reserved for its content</summary>
    '        ''' <param name="g">Graphic to use, when necesary</param>
    '        ''' <param name="rect">Size of whole visual style element</param>
    '        ''' <returns>Area of element reserved for its content</returns>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null and encapsulated instance class needs it</exception>
    '        Public Overloads Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle) As System.Drawing.Rectangle
    '            Return InternalObject.GetContentRectangle(g, rect)
    '        End Function
    '        ''' <summary>Returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
    '        ''' <param name="g">The <see cref="System.Drawing.Graphics" /> this operation will use.</param>
    '        ''' <param name="Rect">A <see cref="System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
    '        ''' <param name="type">One of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
    '        ''' <returns>A <see cref="System.Drawing.Size" /> that contains the size specified by the type parameter for the current visual style part.</returns>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="g"/> is null and encapsulated instance needs it.</exception>
    '        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException"><paramref name="Type"/> is not one of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
    '        Public Overrides Function GetSize(ByVal g As System.Drawing.Graphics, ByVal Type As System.Windows.Forms.VisualStyles.ThemeSizeType, ByVal Rect As System.Drawing.Rectangle) As System.Drawing.Size
    '            Return InternalObject.GetSize(g, Type, Rect)
    '        End Function
    '#End Region
    '#Region "Implementations"
    '        ''' <summary>Abstract base class of non-themed contrl rendereres</summary>
    '        Public MustInherit Class NonThemedVisualStyleObject
    '            Inherits VisualStyleObject
    '        End Class
    '#Region "Button"
    '        ''' <summary>Gets visual style object capable of rendiring with or without visual styles depending on if visual style is available</summary>
    '        ''' <param name="State">State of button</param>
    '        ''' <returns><see cref="VisualStyleSafeObject"/> capable of rendering of button</returns>
    '        ''' <exception cref="InvalidEnumArgumentException"><paramref name="State"/> is not member of <see cref="PushButtonState"/> and visual styles are not supported</exception>
    '        Public Shared Function CreateButton(ByVal State As PushButtonState) As SafeButtonRenderer
    '            If VisualStyleRenderer.IsSupported Then
    '                If UxThemeObject.IsSupported(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, State) Then
    '                    Return New SafeButtonRenderer(New UxThemeObject(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, State))
    '                Else
    '                    Select Case State
    '                        Case PushButtonState.Default, PushButtonState.Hot, PushButtonState.Pressed, PushButtonState.Disabled : Return CreateButton(PushButtonState.Normal)
    '                        Case Else : Return New SafeButtonRenderer(New OldButtonRenderer(State))
    '                    End Select
    '                End If
    '            Else
    '                Return New SafeButtonRenderer(New OldButtonRenderer(State))
    '            End If
    '        End Function
    '        ''' <summary>Implements theme-aware theme-independent renderer of <see cref="Button"/> control</summary>
    '        Public Class SafeButtonRenderer
    '            Inherits VisualStyleSafeObject
    '            ''' <summary>CTor from theme-unaware renderer</summary>
    '            ''' <param name="Renderer">Renderer to encapsulate</param>
    '            ''' <exception cref="ArgumentNullException"><paramref name="Renderer"/> is null</exception>
    '            Public Sub New(ByVal Renderer As OldButtonRenderer)
    '                MyBase.New(Renderer)
    '            End Sub
    '            ''' <summary>CTor from theme-aware renderer</summary>
    '            ''' <param name="Renderer">Renderer to encapsulate</param>
    '            ''' <exception cref="ArgumentNullException"><paramref name="Renderer"/> is null</exception>
    '            ''' <exception cref="ArgumentException"><paramref name="Renderer"/> does not render <see cref="VisualStyleElement.Button.PushButton"/>.</exception>
    '            Public Sub New(ByVal Renderer As UxThemeObject)
    '                MyBase.New(Renderer)
    '            End Sub
    '            ''' <summary>Gets or sets state of button being rendered</summary>
    '            ''' <returns>State of button being rendered</returns>
    '            ''' <value>New state of button being rendered</value>
    '            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="PushButtonState"/></exception>
    '            ''' <remarks>Changing this property may leed to change of theme-aware to theme unaware renderer if <see cref="VisualStyleElement.Button.PushButton"/> is no longer supported (due to theme change).
    '            ''' If theme does not support certain state, <see cref="PushButtonState.Normal"/> is used instead.</remarks>
    '            Public Property State() As PushButtonState
    '                Get
    '                    If TypeOf InternalObject Is UxThemeObject Then
    '                        Return DirectCast(InternalObject, UxThemeObject).State
    '                    Else
    '                        Return DirectCast(InternalObject, OldButtonRenderer).State
    '                    End If
    '                End Get
    '                Set(ByVal value As PushButtonState)
    '                    If TypeOf InternalObject Is OldButtonRenderer Then
    '                        DirectCast(InternalObject, OldButtonRenderer).State = value
    '                    Else
    '                        With DirectCast(InternalObject, UxThemeObject)
    '                            If UxThemeObject.IsSupported(.Renderer.Class, .Renderer.Part, value) Then
    '                                .State = value
    '                            ElseIf UxThemeObject.IsSupported(.Renderer.Class, .Renderer.Part, PushButtonState.Normal) Then
    '                                .State = PushButtonState.Normal
    '                            Else
    '                                InternalObject = New OldButtonRenderer(value)
    '                            End If
    '                        End With
    '                    End If
    '                End Set
    '            End Property
    '            ''' <summary>Gets or sets value indicationg is this instance uses visual styles</summary>
    '            ''' <returns>True if <see cref="InternalObject"/> is <see cref="UxThemeObject"/></returns>
    '            ''' <value>True to use visual styls, false not to use visual styles. If change form false to true faild, no exception is thrown and value of property remains unchanged.</value>
    '            Public Shadows Property UseVisualStyle() As Boolean
    '                Get
    '                    Return MyBase.UseVisualStyle
    '                End Get
    '                Set(ByVal value As Boolean)
    '                    If value <> UseVisualStyle Then
    '                        If value Then
    '                            If UxThemeObject.IsSupported(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, Me.State) Then
    '                                InternalObject = New UxThemeObject(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, State)
    '                            ElseIf UxThemeObject.IsSupported(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, PushButtonState.Normal) Then
    '                                InternalObject = New UxThemeObject(UxThemeObject.VisualStyleClass.Button, UxThemeObject.ButtonPart.Button, PushButtonState.Normal)
    '                            End If
    '                        Else
    '                            InternalObject = New OldButtonRenderer(Me.State)
    '                        End If
    '                    End If
    '                End Set
    '            End Property
    '            ''' <summary>Gets encapsulated <see cref="VisualStyleObject"/></summary>
    '            ''' <returns>Encapsulated <see cref="VisualStyleObject"/></returns>
    '            ''' <value>New internal visual style objet</value>
    '            ''' <exception cref="TypeMismatchException">Value being set is neither <see cref="UxThemeObject"/> nor <see cref="NonThemedVisualStyleObject"/></exception>
    '            ''' <exception cref="ArgumentException">Value being set is <see cref="UxThemeObject"/> but it does not represent <see cref="VisualStyleElement.Button.PushButton"/>.</exception>
    '            Protected Friend Overrides Property InternalObject() As VisualStyleObject
    '                Get
    '                    Return MyBase.InternalObject
    '                End Get
    '                Protected Set(ByVal value As VisualStyleObject)
    '                    If TypeOf value Is UxThemeObject Then
    '                        With DirectCast(value, UxThemeObject)
    '                            If .ClassName <> UxThemeObject.VisualStyleClass.Button.ClassName OrElse .Part <> UxThemeObject.ButtonPart.Button Then _
    '                                Throw New ArgumentException(Exceptions.RendererMustRender0.f("PushButton"))
    '                        End With
    '                    End If
    '                    MyBase.InternalObject = value
    '                End Set
    '            End Property
    '        End Class
    '        ''' <summary>Implements renderer for <see cref="Button"/> control without visual styles</summary>
    '        Public Class OldButtonRenderer
    '            Inherits NonThemedVisualStyleObject
    '            ''' <summary>CTor</summary>
    '            ''' <param name="State">State of button</param>
    '            ''' <exception cref="InvalidEnumArgumentException"><paramref name="State"/> is not member of <see cref="PushButtonState"/></exception>
    '            Public Sub New(ByVal State As PushButtonState)
    '                Me.State = State
    '            End Sub
    '            ''' <summary>Contains value of the <see cref="State"/> property</summary>
    '            Private _State As PushButtonState
    '            ''' <summary>Gets or sets state to be rendered</summary>
    '            ''' <returns>Current state of button rendered</returns>
    '            ''' <value>State of button to be rendered</value>
    '            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="PushButtonState"/></exception>
    '            Public Property State() As PushButtonState
    '                Get
    '                    Return _State
    '                End Get
    '                Set(ByVal value As PushButtonState)
    '                    If Not value.IsDefined Then Throw New InvalidEnumArgumentException(value, "value", value.GetType)
    '                    _State = value
    '                End Set
    '            End Property
    '            ''' <summary>Gets state to be rendered as <see cref="Windows.Forms.ButtonState"/></summary>
    '            ''' <returns>If <see cref="State"/> is <see cref="PushButtonState.Pressed"/> returns <see cref="ButtonState.Pushed"/>.
    '            ''' If <see cref="State"/> is <see cref="PushButtonState.Disabled"/> return <see cref="ButtonState.Inactive"/>.
    '            ''' Otherwise returns <see cref="ButtonState.Normal"/></returns>
    '            Public ReadOnly Property ButtonState() As ButtonState
    '                Get
    '                    Select Case State
    '                        Case PushButtonState.Pressed : Return Windows.Forms.ButtonState.Pushed
    '                        Case PushButtonState.Disabled : Return Windows.Forms.ButtonState.Inactive
    '                        Case Else : Return Windows.Forms.ButtonState.Normal
    '                    End Select
    '                End Get
    '            End Property
    '            ''' <summary>Draws portion of button without visual styles</summary>
    '            ''' <param name="g">Graphics to draw button background on</param>
    '            ''' <param name="rect">Rectangle identifiing position and size of button</param>
    '            ''' <param name="clip">Postion of <paramref name="g"/> that was invalidated and must be redrawn</param>
    '            ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
    '            Public Overloads Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle, ByVal clip As System.Drawing.Rectangle)
    '                Dim oc = g.Clip
    '                g.IntersectClip(clip)
    '                Try
    '                    ControlPaint.DrawButton(g, rect, ButtonState)
    '                Finally
    '                    g.Clip = oc
    '                End Try
    '            End Sub

    '            ''' <summary>Gets rectangle representing area of button reserved for its content</summary>
    '            ''' <param name="g">Ignored</param>
    '            ''' <param name="rect">Size of whole button element</param>
    '            ''' <returns>Area of button reserved for its content. Actually it's area with pading 3 on each side</returns>
    '            Public Overloads Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle) As System.Drawing.Rectangle
    '                Return New Rectangle(rect.X + 3, rect.Y + 3, rect.Width - 6, rect.Height - 6)
    '            End Function

    '            ''' <summary>Returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
    '            ''' <param name="g">Ignored</param>
    '            ''' <param name="Rect">A <see cref="System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
    '            ''' <param name="type">One of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
    '            ''' <returns>A <see cref="System.Drawing.Size" /> that contains the size specified by the type parameter for the current visual style part. For <paramref name="Type"/> <see cref="ThemeSizeType.Draw"/> and <see cref="ThemeSizeType.[True]"/> returns <paramref name="Rect"/>, for <see cref="ThemeSizeType.Minimum"/> returns square of size 7.</returns>
    '            ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException"><paramref name="Type"/> is not one of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
    '            Public Overrides Function GetSize(ByVal g As System.Drawing.Graphics, ByVal Type As System.Windows.Forms.VisualStyles.ThemeSizeType, ByVal Rect As System.Drawing.Rectangle) As System.Drawing.Size
    '                Select Case Type
    '                    Case ThemeSizeType.Draw, ThemeSizeType.True : Return Rect.Size
    '                    Case ThemeSizeType.Minimum : Return New Size(7, 7)
    '                    Case Else : Throw New InvalidEnumArgumentException("Type", Type, Type.GetType)
    '                End Select
    '            End Function
    '        End Class
    '#End Region
    '#End Region
    '        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="VisualStyleSafeObject" />.</summary>
    '        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="VisualStyleSafeObject" />; otherwise, false.</returns>
    '        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="VisualStyleSafeObject" />. </param>
    '        ''' <remarks>Object is considered eqzual to <see cref="VisualStyleSafeObject"/> if it is either <see cref="VisualStyleSafeObject"/> with same <see cref="InternalObject"/> or it is <see cref="VisualStyleObject"/> equal to <see cref="InternalObject"/>.</remarks>
    '        Public Overrides Function Equals(ByVal obj As Object) As Boolean
    '            If obj Is Nothing Then Return False
    '            If TypeOf obj Is VisualStyleSafeObject Then
    '                Return Me.InternalObject.Equals(DirectCast(obj, VisualStyleSafeObject).InternalObject)
    '            ElseIf TypeOf obj Is VisualStyleObject Then
    '                Return Me.InternalObject.Equals(obj)
    '            End If
    '            Return MyBase.Equals(obj)
    '        End Function
    '    End Class
    '    ''' <summary>Encalsulates <see cref="VisualStyleRenderer"/> to simplified form</summary>
    '    Public Class UxThemeObject
    '        Inherits VisualStyleObject
    '        Implements IEquatable(Of VisualStyleElement), IEquatable(Of VisualStyleRenderer), IEquatable(Of VisualStyleSafeObject), IEquatable(Of UxThemeObject)
    '        ''' <summary>Test if current visual style supports element identified by class name, part number and state</summary>
    '        ''' <param name="Class">String identifiying class of visual element</param>
    '        ''' <param name="Part">Part of visual element</param>
    '        ''' <param name="State">State of visual element</param>
    '        ''' <returns>True when visual style are supported and current visual style supportes element identified by parameters; otherwise false</returns>
    '        Public Shared Function IsSupported(ByVal Class$, ByVal Part%, ByVal State%) As Boolean
    '            Return VisualStyleRenderer.IsSupported AndAlso VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement([Class], Part, State))
    '        End Function
    '        ''' <summary>Test if current visual style supports element identified by class name, part number and state</summary>
    '        ''' <param name="Class">Value identifiying class of visual element</param>
    '        ''' <param name="Part">Part of visual element</param>
    '        ''' <param name="State">State of visual element</param>
    '        ''' <returns>True when visual style are supported and current visual style supportes element identified by parameters; otherwise false</returns>
    '        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Class"/> is neither zero nor member of <see cref="VisualStyleClass"/> enumeration</exception>
    '        Public Shared Function IsSupported(ByVal [Class] As VisualStyleClass, ByVal Part%, ByVal State%) As Boolean
    '            Return VisualStyleRenderer.IsSupported AndAlso VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement([Class].ClassName, Part, State))
    '        End Function

    '        ''' <summary>CTor from class, part and state</summary>
    '        ''' <param name="ClassName">String identifiying class of visual element</param>
    '        ''' <param name="Part">Part of visual element</param>
    '        ''' <param name="State">State of visual element</param>
    '        ''' <exception cref="InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
    '        ''' <exception cref="ArgumentException">The combination of <paramref name="className"/>, <paramref name="part"/>, and <paramref name="state"/> is not defined by the current visual style.</exception>
    '        ''' <seelaso cref="VisualStyleRenderer"/>
    '        Public Sub New(ByVal ClassName$, ByVal Part%, ByVal State%)
    '            _renderer = New VisualStyleRenderer(ClassName, Part, State)
    '        End Sub
    '        ''' <summary>CTor from class, part and state</summary>
    '        ''' <param name="CLass">Value identifiying class of visual element</param>
    '        ''' <param name="Part">Part of visual element</param>
    '        ''' <param name="State">State of visual element</param>
    '        ''' <exception cref="InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
    '        ''' <exception cref="ArgumentException">The combination of <paramref name="className"/>, <paramref name="part"/>, and <paramref name="state"/> is not defined by the current visual style.</exception>
    '        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Class"/> is neither zero nor member of <see cref="VisualStyleClass"/>. Note: When <paramref name="Class"/> is zero, <see cref="ArgumentException"/> is thrown.</exception>
    '        ''' <seelaso cref="VisualStyleRenderer"/>
    '        Public Sub New(ByVal [Class] As VisualStyleClass, ByVal Part%, ByVal State%)
    '            _renderer = New VisualStyleRenderer([Class].ClassName, Part, State)
    '        End Sub
    '        ''' <summary>CTor form <see cref="VisualStyleElement"/></summary>
    '        ''' <param name="Element">Visual element to encapsulate</param>
    '        ''' <exception cref="InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
    '        ''' <exception cref="ArgumentException"><paramref name="element"/> is not defined by the current visual style.</exception>
    '        ''' <seelaso cref="VisualStyleRenderer"/>
    '        Public Sub New(ByVal Element As VisualStyleElement)
    '            _renderer = New VisualStyleRenderer(Element)
    '        End Sub
    '        ''' <summary>CTor from <see cref="VisualStyleRenderer"/></summary>
    '        ''' <param name="Renderer">A <see cref="VisualStyleRenderer"/> to encapsulate</param>
    '        Public Sub New(ByVal Renderer As VisualStyleRenderer)
    '            _renderer = Renderer
    '        End Sub
    '        ''' <summary>Gets the class name of the current visual style element.</summary>
    '        ''' <returns>A string that identifies the class of the current visual style element.</returns>
    '        Public ReadOnly Property ClassName$()
    '            Get
    '                Return Renderer.Class
    '            End Get
    '        End Property
    '        ''' <summary>Gets the part of the current visual style element.</summary>
    '        ''' <returns>A value that specifies the part of the current visual style element.</returns>
    '        Public ReadOnly Property Part%()
    '            Get
    '                Return Renderer.Part
    '            End Get
    '        End Property
    '        ''' <summary>Gets the state of the current visual style element.</summary>
    '        ''' <returns>A value that identifies the state of the current visual style element.</returns>
    '        Public Property State%()
    '            Get
    '                Return Renderer.State
    '            End Get
    '            Set(ByVal value%)
    '                _renderer = New VisualStyleRenderer(ClassName, Part, value)
    '            End Set
    '        End Property
    '        ''' <summary>Contains value of the <see cref="Renderer"/> property</summary>
    '        Private _renderer As VisualStyleRenderer
    '        ''' <summary>Gets <see cref="VisualStyleRenderer"/> encapsulated by this instance</summary>
    '        ''' <returns><see cref="VisualStyleRenderer"/> encapsulated by this instance</returns>
    '        Public ReadOnly Property Renderer() As VisualStyleRenderer
    '            Get
    '                Return _renderer
    '            End Get
    '        End Property
    '        ''' <summary>Gets <see cref="VisualStyleElement"/> encapsulated by this instance</summary>
    '        ''' <returns><see cref="VisualStyleElement"/> encapsulated by this instance</returns>
    '        Public Function GetElement() As VisualStyleElement
    '            Return VisualStyleElement.CreateElement(ClassName, Part, State)
    '        End Function
    '        ''' <summary>Draws background part using current <see cref="VisualStyleRenderer"/></summary>
    '        ''' <param name="g">Graphisc to draw on</param>
    '        ''' <param name="rect">Rectangle representing size and position of element</param>
    '        ''' <param name="clip">Part of element to be drawn</param>
    '        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
    '        ''' <seelaso cref="VisualStyleRenderer.DrawBackground"/>
    '        Public Overloads Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle, ByVal clip As Rectangle)
    '            Renderer.DrawBackground(g, rect, clip)
    '        End Sub
    '        ''' <summary>Returns the content area for the background of the current visual style element.</summary>
    '        ''' <param name="g">The <see cref="System.Drawing.Graphics" /> this operation will use.</param>
    '        ''' <param name="rect">A <see cref="System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</param>
    '        ''' <returns>A <see cref="System.Drawing.Rectangle" /> that contains the content area for the background of the current visual style element.</returns>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="g"/> is null.</exception>
    '        ''' <seelaso cref="VisualStyleRenderer.GetBackgroundContentRectangle"/>
    '        Public Overloads Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal rect As System.Drawing.Rectangle) As System.Drawing.Rectangle
    '            Return Renderer.GetBackgroundContentRectangle(g, rect)
    '        End Function
    '        ''' <summary>Returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
    '        ''' <param name="g">The <see cref="System.Drawing.Graphics" /> this operation will use.</param>
    '        ''' <param name="Rect">A <see cref="System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
    '        ''' <param name="type">One of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
    '        ''' <returns>A <see cref="System.Drawing.Size" /> that contains the size specified by the type parameter for the current visual style part.</returns>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="g"/> is null.</exception>
    '        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException"><paramref name="Type"/> is not one of the <see cref="System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
    '        ''' <seelaso cref="VisualStyleRenderer.GetPartSize"/>
    '        Public Overrides Function GetSize(ByVal g As System.Drawing.Graphics, ByVal Type As ThemeSizeType, ByVal Rect As System.Drawing.Rectangle) As Size
    '            Renderer.GetPartSize(g, Rect, Type)
    '        End Function
    '        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="UxThemeObject" />.</summary>
    '        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
    '        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="UxThemeObject" />. </param>
    '        ''' <remarks>Object is considered equal to <see cref="UxThemeObject"/> when it is either <see cref="UxThemeObject"/>, <see cref="VisualStyleElement"/>, <see cref="VisualStyleRenderer"/> or <see cref="VisualStyleSafeObject"/> and represents same <see cref="ClassName"/>, <see cref="Part"/> and <see cref="State"/>.</remarks>
    '        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
    '            If obj Is Nothing Then Return False
    '            If obj Is Me Then Return True
    '            If TypeOf obj Is UxThemeObject Then
    '                Return Me.Equals(DirectCast(obj, UxThemeObject))
    '            ElseIf TypeOf obj Is VisualStyleElement Then
    '                Return Me.Equals(DirectCast(obj, VisualStyleElement))
    '            ElseIf TypeOf obj Is VisualStyleRenderer Then
    '                Return Me.Equals(DirectCast(obj, VisualStyleRenderer))
    '            ElseIf TypeOf obj Is VisualStyleSafeObject Then
    '                Return Me.Equals(DirectCast(obj, VisualStyleSafeObject))
    '            End If
    '            Return MyBase.Equals(obj)
    '        End Function
    '        ''' <summary>Determines whether the specified <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement" /> is equal to the current <see cref="UxThemeObject" />.</summary>
    '        ''' <returns>true if the specified <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
    '        ''' <param name="other">The <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement" /> to compare with the current <see cref="UxThemeObject" />. </param>
    '        Public Overloads Function Equals(ByVal other As System.Windows.Forms.VisualStyles.VisualStyleElement) As Boolean Implements System.IEquatable(Of System.Windows.Forms.VisualStyles.VisualStyleElement).Equals
    '            If other Is Nothing Then Return False
    '            Return Me.ClassName = other.ClassName AndAlso Me.Part = other.Part AndAlso Me.State = other.State
    '        End Function
    '        ''' <summary>Determines whether the specified <see cref="System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> is equal to the current <see cref="UxThemeObject" />.</summary>
    '        ''' <returns>true if the specified <see cref="System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
    '        ''' <param name="other">The <see cref="System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> to compare with the current <see cref="UxThemeObject" />. </param>
    '        Public Overloads Function Equals(ByVal other As System.Windows.Forms.VisualStyles.VisualStyleRenderer) As Boolean Implements System.IEquatable(Of System.Windows.Forms.VisualStyles.VisualStyleRenderer).Equals
    '            If other Is Nothing Then Return False
    '            Return Me.ClassName = other.Class AndAlso Me.Part = other.Part AndAlso Me.State = other.State
    '        End Function
    '        ''' <summary>Determines whether the specified <see cref="UxThemeObject" /> is equal to the current <see cref="UxThemeObject" />.</summary>
    '        ''' <returns>true if the specified <see cref="UxThemeObject" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
    '        ''' <param name="other">The <see cref="UxThemeObject" /> to compare with the current <see cref="UxThemeObject" />. </param>
    '        Public Overloads Function Equals(ByVal other As UxThemeObject) As Boolean Implements System.IEquatable(Of UxThemeObject).Equals
    '            If other Is Nothing Then Return False
    '            If other Is Me Then Return True
    '            Return Me.ClassName = other.ClassName AndAlso Me.Part = other.Part AndAlso Me.State = other.State
    '        End Function
    '        ''' <summary>Determines whether the specified <see cref="VisualStyleSafeObject" /> is equal to the current <see cref="UxThemeObject" />.</summary>
    '        ''' <returns>true if the specified <see cref="VisualStyleSafeObject" /> is equal to the current <see cref="UxThemeObject" />; otherwise, false.</returns>
    '        ''' <param name="other">The <see cref="VisualStyleSafeObject" /> to compare with the current <see cref="UxThemeObject" />. </param>
    '        Public Overloads Function Equals(ByVal other As VisualStyleSafeObject) As Boolean Implements System.IEquatable(Of VisualStyleSafeObject).Equals
    '            If other Is Nothing Then Return False
    '            Return Me.Equals(other.InternalObject)
    '        End Function
    '#Region "Enums"
    '        ''' <summary>Enumerates classes used with visual styles</summary>
    '        ''' <seelaso cref="VisualStyleElement"/>
    '        Public Enum VisualStyleClass
    '            ''' <summary><see cref="Button"/>, <see cref="CheckBox"/>, <see cref="RadioButton"/> and <see cref="GroupBox"/> controls</summary>
    '            ''' <seelaso cref="VisualStyleElement.Button"/>
    '            Button = 1
    '            ''' <summary><see cref="ComboBox"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.ComboBox"/>
    '            ComboBox = 2
    '            ''' <summary>Exprorer bar control</summary>
    '            ''' <seelaso cref="VisualStyleElement.ExplorerBar"/>
    '            ExplorerBar = 3
    '            ''' <summary><see cref="ColumnHeader"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Header"/>
    '            Header = 4
    '            ''' <summary><see cref="ListView"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.ListView"/>
    '            ListView = 5
    '            ''' <summary><see cref="Menu"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Menu"/>
    '            Menu = 6
    '            ''' <summary>Menu band control</summary>
    '            ''' <seelaso cref="VisualStyleElement.MenuBand"/>
    '            MenuBand = 7
    '            ''' <summary>Page control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Page"/>
    '            Page = 8
    '            ''' <summary><see cref="ProgressBar"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.ProgressBar"/>
    '            Progress = 9
    '            ''' <summary>Rebar control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Rebar"/>
    '            Rebar = 10
    '            ''' <summary><see cref="ScrollBar"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.ScrollBar"/>
    '            ScrollBar = 11
    '            ''' <summary>Spin control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Spin"/>
    '            Spin = 12
    '            ''' <summary>Componnts of start menu</summary>
    '            ''' <seelaso cref="VisualStyleElement.StartPanel"/>
    '            StartPanel = 13
    '            ''' <summary><see cref="StatusBar"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Status"/>
    '            Status = 14
    '            ''' <summary><see cref="TabControl"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Tab"/>
    '            Tab = 15
    '            ''' <summary>Task band control (on taskbar)</summary>
    '            ''' <seelaso cref="VisualStyleElement.TaskBand"/>
    '            TaskBand = 16
    '            ''' <summary>System taskbar</summary>
    '            ''' <seelaso cref="VisualStyleElement.Taskbar"/>
    '            TaskBar = 17
    '            ''' <summary>Taskbar clock</summary>
    '            ''' <seelaso cref="VisualStyleElement.TaskbarClock"/>
    '            Clock = 18
    '            ''' <summary><see cref="TextBox"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.TextBox"/>
    '            Edit = 19
    '            ''' <summary><see cref="ToolBar"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.ToolBar"/>
    '            Toolbar = 20
    '            ''' <summary><see cref="ToolTip"/></summary>
    '            ''' <seelaso cref="VisualStyleElement.ToolTip"/>
    '            Tooltip = 21
    '            ''' <summary><see cref="TrackBar"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.TrackBar"/>
    '            Trackbar = 22
    '            ''' <summary><see cref="NotifyIcon"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.TrayNotify"/>
    '            TrayNotify = 23
    '            ''' <summary><see cref="TreeView"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.TreeView"/>
    '            TreeView = 24
    '            ''' <summary><see cref="Form"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Window"/>
    '            Window = 25
    '        End Enum
    '        ''' <summary>Parts of <see cref="VisualStyleElement.Button"/></summary>
    '        Public Enum ButtonPart
    '            ''' <summary><see cref="GroupBox"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Button.GroupBox"/>
    '            GroupBox = 4
    '            ''' <summary><see cref="CheckBox"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Button.CheckBox"/>
    '            CheckBox = 3
    '            ''' <summary><see cref="Button"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Button.PushButton"/>
    '            Button = 1
    '            ''' <summary><see cref="RadioButton"/> control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Button.RadioButton"/>
    '            RadioButton = 2
    '            ''' <summary>User button control</summary>
    '            ''' <seelaso cref="VisualStyleElement.Button.UserButton"/>
    '            UserButton = 5
    '        End Enum
    '#End Region
    '    End Class
    '#End Region
    ''' <summary>Provides extension mehods related to <see cref="VisualStyles"/></summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Module VisualStyleExtensions
        '''' <summary>Gets name of class from <see cref="UxThemeObject.VisualStyleClass"/> value</summary>
        '''' <param name="Class">Visual style class enumerated value</param>
        '''' <returns>Class name, its actualy uppercase name of enumerated value; <see cref="System.String.Empty"/> when <paramref name="Class"/> is zero</returns>
        '''' <exception cref="InvalidEnumArgumentException"><paramref name="Class"/> is neither zero nor member of <see cref="UxThemeObject.VisualStyleClass"/></exception>
        '<Extension()> _
        'Public Function ClassName(ByVal [Class] As UxThemeObject.VisualStyleClass) As String
        '    If [Class] = 0 Then Return ""
        '    If Not [Class].IsDefined Then Throw New InvalidEnumArgumentException("Class", [Class], [Class].GetType)
        '    Return [Class].GetName.ToUpper
        'End Function
        ''' <summary>Determines whether the specified visual style element is defined by the current visual style.</summary>
        ''' <param name="element">A <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement" /> whose class and part combination will be verified.</param>
        ''' <returns>true if the combination of the <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" /> and <see cref="System.Windows.Forms.VisualStyles.VisualStyleElement.Part" /> properties of element are defined; otherwise, false. Returns false when vidsual styles are not supported.</returns>
        <Extension()> Function IsDefined(ByVal Element As VisualStyleElement) As Boolean
            Return VisualStyleRenderer.IsSupported AndAlso VisualStyleRenderer.IsElementDefined(Element)
        End Function
    End Module

#Region "V2"
    ''' <summary>Base class for themed and non-themed object rendereres</summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public MustInherit Class ObjectRenderer
        ''' <summary>When overriden in derived class draws element background</summary>
        ''' <param name="g">rahics to draw onto</param>
        ''' <param name="BackgroundRectangle">Rectangle of element area</param>
        ''' <param name="ClipRectangle">Rectangle that was invalidate and needs to be repainted</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        Public MustOverride Sub DrawBackground(ByVal g As Graphics, ByVal BackgroundRectangle As Rectangle, ByVal ClipRectangle As Rectangle)
        ''' <summary>Draws focus rectangle</summary>
        ''' <param name="g">Graphics to draw onto</param>
        ''' <param name="Rectangle">Rectangle of focus rectangle</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception> 
        Public Overridable Sub DrawFocusRectangle(ByVal g As Graphics, ByVal Rectangle As Rectangle)
            If g Is Nothing Then Throw New ArgumentNullException("g")
            ControlPaint.DrawFocusRectangle(g, Rectangle)
        End Sub
        ''' <summary>When overiden in derived class gets content rectangle of control being rendered</summary>
        ''' <param name="g">Used for reference</param>
        ''' <param name="ControlRectangle">Rectange of full control including edges</param>
        ''' <returns>Rectangle aloocated for control content</returns>
        ''' <remarks>This implementation always returns <paramref name="ControlRectangle"/> (unles an exception is thrown)</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception> 
        Public Overridable Function GetContentRectangle(ByVal g As Graphics, ByVal ControlRectangle As Rectangle) As Rectangle
            If g Is Nothing Then Throw New ArgumentNullException("g")
            Return ControlRectangle
        End Function
        ''' <summary>Draws focusr rectangle using <see cref="DrawFocusRectangle"/> at bounds obtained from <see cref="GetContentRectangle"/>.</summary>
        ''' <param name="g">Graphics to draw onto</param>
        ''' <param name="ControlRectangle">Rectange of full control including edges</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception> 
        Public Sub DrawContentFocusRectangle(ByVal g As Graphics, ByVal ControlRectangle As Rectangle)
            DrawFocusRectangle(g, GetContentRectangle(g, ControlRectangle))
        End Sub
    End Class
    ''' <summary>Safe renderer containing both, themed and non-themed renderer</summary>
    ''' <completionlist cref="SafeObjectRenderer"/>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class SafeObjectRenderer
        Inherits ObjectRenderer
        ''' <summary>Contains value of the <see cref="Themed"/> property</summary>
        Private _Themed As ThemedObjectRenderer
        ''' <summary>Holds themed renderer. May be null.</summary>
        Protected ReadOnly Property Themed() As ThemedObjectRenderer
            Get
                Return _Themed
            End Get
        End Property
        ''' <summary>Honlds non-themed renderer. Cannot be null.</summary>
        Protected ReadOnly Property NonThemed() As NonThemedObejctRenderer
            Get
                Return _NonThemed
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="NonThemed"/> proprty</summary>
        Private _NonThemed As NonThemedObejctRenderer
        ''' <summary>CTor</summary>
        ''' <param name="Themed">Themed renderer. Can be null.</param>
        ''' <param name="NonThemed">Non-themed renderer</param>
        ''' <exception cref="ArgumentNullException"><paramref name="NonThemed"/> is null</exception>
        Public Sub New(ByVal Themed As ThemedObjectRenderer, ByVal NonThemed As NonThemedObejctRenderer)
            If NonThemed Is Nothing Then Throw New ArgumentNullException("NonThemed")
            Me._Themed = Themed
            Me._NonThemed = NonThemed
        End Sub
        ''' <summary>Contains value of the <see cref="UseThemes"/> property</summary>
        Private _UseThemes As Boolean = True
        ''' <summary>Gets or sets value indicatiing if themes hsould be used</summary>
        ''' <remarks>True when themes are likely to be used</remarks>
        ''' <value>True to use themes if possible; false to never use themes; default value is true</value>
        <DefaultValue(True)> _
        Public Property UseThemes() As Boolean
            Get
                Return _UseThemes
            End Get
            Set(ByVal value As Boolean)
                _UseThemes = value
            End Set
        End Property
#Region "Forks"
        ''' <summary>Draws element background using either themed or untehemed renderer</summary>
        ''' <param name="g">Grahics to draw onto</param>
        ''' <param name="BackgroundRectangle">Rectangle of element area</param>
        ''' <param name="ClipRectangle">Rectangle that was invalidate and needs to be repainted</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        Public Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal BackgroundRectangle As System.Drawing.Rectangle, ByVal ClipRectangle As System.Drawing.Rectangle)
            If UseThemes AndAlso Themed IsNot Nothing AndAlso Themed.Supported Then
                Themed.DrawBackground(g, BackgroundRectangle, ClipRectangle)
            Else
                NonThemed.DrawBackground(g, BackgroundRectangle, ClipRectangle)
            End If
        End Sub
        ''' <summary>Gets content rectangle of control being rendered</summary>
        ''' <param name="g">Used for reference</param>
        ''' <param name="ControlRectangle">Rectange of full control including edges</param>
        ''' <returns>Rectangle aloocated for control content</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        Public Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal ControlRectangle As System.Drawing.Rectangle) As System.Drawing.Rectangle
            If UseThemes AndAlso Themed IsNot Nothing AndAlso Themed.Supported Then
                Return Themed.GetContentRectangle(g, ControlRectangle)
            Else
                Return NonThemed.GetContentRectangle(g, ControlRectangle)
            End If
        End Function
        ''' <summary>Draws focus rectangle</summary>
        ''' <param name="g">Graphics to draw onto</param>
        ''' <param name="Rectangle">Rectangle of focus rectangle</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        Public Overrides Sub DrawFocusRectangle(ByVal g As System.Drawing.Graphics, ByVal Rectangle As System.Drawing.Rectangle)
            If UseThemes AndAlso Themed IsNot Nothing AndAlso Themed.Supported Then
                Themed.DrawFocusRectangle(g, Rectangle)
            Else
                NonThemed.DrawFocusRectangle(g, Rectangle)
            End If
        End Sub
#End Region
#Region "Shared"
        ''' <summary>Gets renderer of push button</summary>
        Public Shared ReadOnly Property PushButton() As SpecializedSafeRenderers.PushButton
            Get
                Return New SpecializedSafeRenderers.PushButton
            End Get
        End Property
#End Region
    End Class
    Namespace SpecializedSafeRenderers
        'TODO: Safe renderers for controls
        'TODO: Introduce common base class as in themed
        ''' <summary>Renders pus button</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public Class PushButton : Inherits SafeObjectRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New(UxThemeObjectRenderer.PushButton, NonThemedObejctRenderer.PushButton)
            End Sub
            ''' <summary>Gets or sets state of button to be rendered</summary>
            Public Property State() As PushButtonState
                Get
                    Return DirectCast(Me.Themed, SpecializedThemedRenderers.PushButton).State
                End Get
                Set(ByVal value As PushButtonState)
                    DirectCast(Me.Themed, SpecializedThemedRenderers.PushButton).State = value
                    With DirectCast(NonThemed, SpecializedNonThemedRenderers.PushButton)
                        Select Case value
                            Case PushButtonState.Default : .State = ButtonState.Normal
                            Case PushButtonState.Disabled : .State = ButtonState.Inactive
                            Case PushButtonState.Hot : .State = ButtonState.Normal
                            Case PushButtonState.Normal : .State = ButtonState.Normal
                            Case PushButtonState.Pressed : .State = ButtonState.Pushed
                            Case Else : .State = ButtonState.Normal
                        End Select
                    End With
                End Set
            End Property
        End Class
    End Namespace
#Region "Themed"
    ''' <summary>Base class for themed renderers</summary>
    ''' <completionlist cref="UxThemeObjectRenderer"/>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public MustInherit Class ThemedObjectRenderer
        Inherits ObjectRenderer
        ''' <summary>Gets value indicatioing if this renderer is supported now</summary>
        ''' <returns>True if renderer can render its object; false if non-themed rendere must be used. This implementation returns <see cref="VisualStyleRenderer.IsSupported"/>.</returns>
        ''' <remarks>Override this property to refine indication</remarks>
        Public Overridable ReadOnly Property Supported() As Boolean
            Get
                Return VisualStyleRenderer.IsSupported
            End Get
        End Property
    End Class
    ''' <summary>Base class for themed renderers base on UxThemes</summary>
    ''' <completionlist cref="UxThemeObjectRenderer"/>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public MustInherit Class UxThemeObjectRenderer
        Inherits ThemedObjectRenderer
#Region "Implementation"
        ''' <summary>Contains valkue of the <see cref="ClassName"/> property</summary>
        Private ReadOnly _ClassName$
        ''' <summary>Contains value of the <see cref="Part"/> property</summary>
        Private _Part As Integer
        ''' <summary>Gets class name this themed renderer renders</summary>
        ''' <seelaso cref="VisualStyleElement.ClassName"/>
        Public ReadOnly Property ClassName$()
            Get
                Return _ClassName
            End Get
        End Property
        ''' <summary>Gets part number this themed renderer renders</summary>
        ''' <seelaso cref="VisualStyleElement.Part"/>
        Public Property Part() As Integer
            Get
                Return _Part
            End Get
            Protected Set(ByVal value As Integer)
                _Part = value
            End Set
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="ClassName">Class name to render</param>
        ''' <param name="Part">Part number to render</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ClassName"/> is null</exception>
        Public Sub New(ByVal ClassName$, ByVal Part%)
            If ClassName Is Nothing Then Throw New ArgumentNullException("ClassName")
            _ClassName = ClassName
            _Part = Part
        End Sub
        ''' <summary>Contains value of the <see cref="State"/> property</summary>
        Private _State As Integer
        ''' <summary>Gets or sets state to be rendered</summary>
        ''' <returns>Satate of object to be rendered</returns>
        ''' <remarks>Use <see cref="GetStates"/> to get possible values of this property. When state cannot be rendered default state is used instead (<see cref="GetDefaultState"/>).</remarks>
        Protected Property State() As Integer
            Get
                Return _State
            End Get
            Set(ByVal value As Integer)
                _State = value
            End Set
        End Property
        ''' <summary>When overriden in derived class gets lis of possible values of <see cref="State"/> property</summary>
        ''' <remarks>All values of the <see cref="State"/> property. USage of another value may leed to fallback.</remarks>
        Protected MustOverride Function GetStates() As IEnumerable(Of Integer)

        ''' <summary>Gets value indicatioing if this renderer is supported now</summary>
        ''' <returns>True if renderer can render its object; false if non-themed renderer must be used. This implementation returns <see cref="VisualStyleRenderer.IsSupported"/> and <see cref="VisualStyleRenderer.IsElementDefined"/> forelement of state <see cref="State"/> or default state.</returns>
        Public Overrides ReadOnly Property Supported() As Boolean
            Get
                Return MyBase.Supported AndAlso (IsCurrentDefined OrElse GetFallback1.IsDefined OrElse GetFallBack2.IsDefined OrElse GetFallBack3.IsDefined)
            End Get
        End Property
        ''' <summary>Gets value indicating if current state of this instance is defined by current theme</summary>
        ''' <returns>True if current visual style object identified by <see cref="ClassName"/>, <see cref="Part"/> and <see cref="State"/> is defined by current theme; false otherwise</returns>
        Public ReadOnly Property IsCurrentDefined() As Boolean
            Get
                Return VisualStyleRenderer.IsSupported AndAlso VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(ClassName, Part, State))
            End Get
        End Property
        '''' <summary>When overriden in derived class gets default state for goven state</summary>
        '''' <param name="State">State to get default state of</param>
        '''' <returns>Default state for state <paramref name="State"/></returns>
        'Protected MustOverride Function GetDefaultState(ByVal State As Integer) As Integer
        ''' <summary>When overriden in derived class gets 1st chance fallback state for current state identified by <see cref="VisualStyleElement"/> object</summary>
        ''' <returns>1st change fallback state identified by <see cref="VisualStyleElement"/>. <see cref="VisualStyleElement.ClassName"/> must be same as <see cref="ClassName"/>. When no 1st chance fallback is defined returns current state.</returns>
        Protected MustOverride Function GetFallback1() As VisualStyleElement
        ''' <summary>When overriden in derived class gets 2nd chance fallback state for current state identified by <see cref="VisualStyleElement"/> object</summary>
        ''' <returns>2nd change fallback state identified by <see cref="VisualStyleElement"/>. <see cref="VisualStyleElement.ClassName"/> must be same as <see cref="ClassName"/>. When no 2nd chance fallback is defined returns <see cref="GetFallback1"/>.</returns>
        Protected MustOverride Function GetFallBack2() As VisualStyleElement
        ''' <summary>When overriden in derived class gets 3rd chance fallback state for current state identified by <see cref="VisualStyleElement"/> object</summary>
        ''' <returns>3rd change fallback state identified by <see cref="VisualStyleElement"/>. <see cref="VisualStyleElement.ClassName"/> must be same as <see cref="ClassName"/>. When no 3rd chance fallback is defined returns <see cref="GetFallback2"/>.</returns>
        Protected MustOverride Function GetFallBack3() As VisualStyleElement
        ''' <summary>Draws element background using <see cref="State"/> or default state if current state is not supported</summary>
        ''' <param name="g">Grahics to draw onto</param>
        ''' <param name="BackgroundRectangle">Rectangle of element area</param>
        ''' <param name="ClipRectangle">Rectangle that was invalidate and needs to be repainted</param>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Supported"/> is false</exception>
        Public Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal BackgroundRectangle As System.Drawing.Rectangle, ByVal ClipRectangle As System.Drawing.Rectangle)
            If g Is Nothing Then Throw New ArgumentNullException("g")
            Dim r = GetRenderer()
            r.DrawBackground(g, BackgroundRectangle, ClipRectangle)
        End Sub
        ''' <summary>Gets renderer for current state or one of its fallback states</summary>
        ''' <exception cref="InvalidOperationException"><see cref="Supported"/> is false</exception>
        Protected Function GetRenderer() As VisualStyleRenderer
            Dim r As VisualStyleRenderer
            If IsCurrentDefined Then
                r = New VisualStyleRenderer(ClassName, Part, State)
            ElseIf GetFallback1.IsDefined Then
                r = New VisualStyleRenderer(GetFallback1)
            ElseIf GetFallBack2.IsDefined Then
                r = New VisualStyleRenderer(GetFallBack2)
            ElseIf GetFallBack3.IsDefined Then
                r = New VisualStyleRenderer(GetFallBack3)
            Else
                Throw New InvalidOperationException("This visual style element is not supported")
            End If
            Return r
        End Function
        ''' <summary>Gets content rectangle of control being rendered</summary>
        ''' <param name="g">Used for reference</param>
        ''' <param name="ControlRectangle">Rectange of full control including edges</param>
        ''' <returns>Rectangle allocated for control content</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Supported"/> is false</exception>
        Public Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal ControlRectangle As System.Drawing.Rectangle) As System.Drawing.Rectangle
            If g Is Nothing Then Throw New ArgumentNullException("g")
            Dim r = GetRenderer()
            Return r.GetBackgroundContentRectangle(g, ControlRectangle)
        End Function
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0} {1} {2}", ClassName, Part, State)
        End Function
#End Region
#Region "Shared"
        ''' <summary>Renderer of normal button</summary>
        Public Shared ReadOnly Property PushButton() As SpecializedThemedRenderers.PushButton
            Get
                Return New SpecializedThemedRenderers.PushButton
            End Get
        End Property
        ''' <summary>Renderer of radio (option) button</summary>
        Public Shared ReadOnly Property RadioButton() As SpecializedThemedRenderers.RadioButton
            Get
                Return New SpecializedThemedRenderers.RadioButton
            End Get
        End Property
        ''' <summary>Renderer of check box</summary>
        Public Shared ReadOnly Property CheckBox() As SpecializedThemedRenderers.CheckBox
            Get
                Return New SpecializedThemedRenderers.CheckBox
            End Get
        End Property
        ''' <summary>Renderer of combo box drop down button</summary>
        Public Shared ReadOnly Property ComboBoxDropDownButton() As SpecializedThemedRenderers.ComboBoxDropDownButton
            Get
                Return New SpecializedThemedRenderers.ComboBoxDropDownButton
            End Get
        End Property
        ''' <summary>Renderer of spin button</summary>
        Public Shared ReadOnly Property Spin() As SpecializedThemedRenderers.Spin
            Get
                Return New SpecializedThemedRenderers.Spin
            End Get
        End Property
        ''' <summary>Renderer of scroll bar arrow button</summary>
        Public Shared ReadOnly Property ScrollBarArrow() As SpecializedThemedRenderers.ScrollBarArrow
            Get
                Return New SpecializedThemedRenderers.ScrollBarArrow
            End Get
        End Property
        ''' <summary>Renderer of scroll bar thumb button</summary>
        Public Shared ReadOnly Property ScrollBarThumbButton() As SpecializedThemedRenderers.ScrollBarThumbButton
            Get
                Return New SpecializedThemedRenderers.ScrollBarThumbButton
            End Get
        End Property
        ''' <summary>Renderer of scroll bar track</summary>
        Public Shared ReadOnly Property ScrollBarTrack() As SpecializedThemedRenderers.ScrollBarTrack
            Get
                Return New SpecializedThemedRenderers.ScrollBarTrack
            End Get
        End Property
        ''' <summary>Renderer of scroll bar gripper</summary>
        Public Shared ReadOnly Property ScrollBarGripper() As SpecializedThemedRenderers.ScrollBarGripper
            Get
                Return New SpecializedThemedRenderers.ScrollBarGripper
            End Get
        End Property
        ''' <summary>Renderer of scroll bar size box</summary>
        Public Shared ReadOnly Property ScrollBarSizeBox() As SpecializedThemedRenderers.ScrollBarSizeBox
            Get
                Return New SpecializedThemedRenderers.ScrollBarSizeBox
            End Get
        End Property
        ''' <summary>Renderer of scroll tab item</summary>
        Public Shared ReadOnly Property TabItem() As SpecializedThemedRenderers.TabItem
            Get
                Return New SpecializedThemedRenderers.TabItem
            End Get
        End Property
        ''' <summary>Renderer of explorer bar header close</summary>
        Public Shared ReadOnly Property ExplorerHeaderClose() As SpecializedThemedRenderers.ExplorerHeaderClose
            Get
                Return New SpecializedThemedRenderers.ExplorerHeaderClose
            End Get
        End Property
        ''' <summary>Renderer of explorer bar header pin</summary>
        Public Shared ReadOnly Property ExplorerPin() As SpecializedThemedRenderers.ExplorerPin
            Get
                Return New SpecializedThemedRenderers.ExplorerPin
            End Get
        End Property
        ''' <summary>Renderer of explorer bar IE bar menu</summary>
        Public Shared ReadOnly Property ExplorerDoubleArrow() As SpecializedThemedRenderers.ExplorerIEBarMenu
            Get
                Return New SpecializedThemedRenderers.ExplorerIEBarMenu
            End Get
        End Property
        ''' <summary>Renderer of explorer bar IE bar menu</summary>
        Public Shared ReadOnly Property ExplorerGroup() As SpecializedThemedRenderers.ExplorerGroup
            Get
                Return New SpecializedThemedRenderers.ExplorerGroup
            End Get
        End Property
        ''' <summary>Renders header item</summary>
        Public Shared ReadOnly Property HeaderItem() As SpecializedThemedRenderers.HeaderItem
            Get
                Return New SpecializedThemedRenderers.HeaderItem
            End Get
        End Property
#End Region
    End Class
    Namespace SpecializedThemedRenderers
#Region "BUTTON"
        ''' <summary>Renders normal button</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class PushButton
            Inherits UxSingleGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("BUTTON", 1, 1, 2, 3, 5, 4)
            End Sub
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("PushButton {0}", State)
            End Function
        End Class
        ''' <summary>Renders radio (option) button</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class RadioButton
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("BUTTON", 2, New Integer() {True, False}, _
                    New Integer() {PushButtonState.Normal, 1, PushButtonState.Hot, 2, PushButtonState.Pressed, 3, PushButtonState.Disabled, 4}, _
                    New Integer() {PushButtonState.Normal, 5, PushButtonState.Hot, 6, PushButtonState.Pressed, 7, PushButtonState.Disabled, 8})
            End Sub
            ''' <summary>Gets state group of this option button</summary>
            ''' <returns>True for checked state group false for unchecked</returns>
            ''' <value>True for checked state false for uncheckd</value>
            Public Shadows Property StateGroup() As Boolean
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As Boolean)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("RadioButton {0} {1}", StateGroup, State)
            End Function
        End Class
        ''' <summary>Renders check box</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class CheckBox
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("BUTTON", 3, New Integer() {CheckState.Unchecked, CheckState.Checked, CheckState.Indeterminate}, _
                            New Integer() {ps.Normal, 1, ps.Hot, 2, ps.Pressed, 3, ps.Disabled, 4}, _
                            New Integer() {ps.Normal, 5, ps.Hot, 6, ps.Pressed, 7, ps.Disabled, 8}, _
                            New Integer() {ps.Normal, 9, ps.Hot, 10, ps.Pressed, 11, ps.Disabled, 12})
            End Sub
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Public Shadows Property StateGroup() As CheckState
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As CheckState)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("CheckBox {0} {1}", StateGroup, State)
            End Function
        End Class
#End Region
#Region "COMBOBOX"
        ''' <summary>Renders combo box drop down button</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ComboBoxDropDownButton
            Inherits UxSingleGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("COMBOBOX", 1, 1, 2, 3, , 4)
            End Sub
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ComboBoxDropDownButton {0}", State)
            End Function
        End Class
#End Region
#Region "SPIN"
        ''' <summary>Renders spin button</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class Spin
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("SPIN", 1, New Integer() {ArrowDirection.Up, ArrowDirection.Down, ArrowDirection.Right, ArrowDirection.Left}, _
                            New Integer() {ps.Normal, 1, ps.Hot, 2, ps.Pressed, 3, ps.Disabled, 4}, _
                            New Integer() {ps.Normal, 1, ps.Hot, 2, ps.Pressed, 3, ps.Disabled, 4}, _
                            New Integer() {ps.Normal, 1, ps.Hot, 2, ps.Pressed, 3, ps.Disabled, 4})
                Me.SetPartChanges(ArrangeDirection.Up, 1, ArrowDirection.Down, 2, ArrowDirection.Right, 3, ArrowDirection.Left, 4)
            End Sub
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Public Shadows Property StateGroup() As ArrowDirection
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As ArrowDirection)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("Spin {0} {1}", StateGroup, State)
            End Function
        End Class
#End Region
#Region "SCROLLBAR"
        ''' <summary>Renders scroll bar arrow button</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ScrollBarArrow
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("SCROLLBAR", 1, New Integer() {ArrowDirection.Up, ArrowDirection.Down, ArrowDirection.Left, ArrowDirection.Right}, _
                            New Integer() {ps.Normal, 1, ps.Hot, 2, ps.Pressed, 3, ps.Disabled, 4}, _
                            New Integer() {ps.Normal, 5, ps.Hot, 6, ps.Pressed, 7, ps.Disabled, 8}, _
                            New Integer() {ps.Normal, 9, ps.Hot, 10, ps.Pressed, 11, ps.Disabled, 12}, _
                            New Integer() {ps.Normal, 13, ps.Hot, 14, ps.Pressed, 15, ps.Disabled, 16})
            End Sub
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Public Shadows Property StateGroup() As ArrowDirection
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As ArrowDirection)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ScrollBbarArrow {0} {1}", StateGroup, State)
            End Function
        End Class
        ''' <summary>Renders scroll bar thumb button</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ScrollBarThumbButton
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("SCROLLBAR", 2, New Integer() {Orientation.Horizontal, Orientation.Vertical}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4})
                Me.SetPartChanges(Orientation.Horizontal, 2, Orientation.Vertical, 3)
            End Sub
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Public Shadows Property StateGroup() As Orientation
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As Orientation)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ScrollBarThumbButton {0} {1}", StateGroup, State)
            End Function
        End Class
        ''' <summary>Renders scroll bar track</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ScrollBarTrack
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("SCROLLBAR", 2, New Integer() {ArrowDirection.Right, ArrowDirection.Left, ArrowDirection.Down, ArrowDirection.Up}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4})
                Me.SetPartChanges(ArrowDirection.Right, 4, ArrowDirection.Left, 5, ArrowDirection.Down, 6, ArrowDirection.Up, 7)
            End Sub
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Public Shadows Property StateGroup() As ArrowDirection
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As ArrowDirection)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ScrollBarTrack {0} {1}", StateGroup, State)
            End Function
        End Class
        ''' <summary>Renders scroll bar gropper</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ScrollBarGripper
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("SCROLLBAR", 2, New Integer() {Orientation.Horizontal, Orientation.Vertical}, _
                           New Integer() {ps.Normal = 0}, _
                           New Integer() {ps.Normal = 0})
                Me.SetPartChanges(Orientation.Horizontal, 8, Orientation.Vertical, 9)
            End Sub
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Public Shadows Property StateGroup() As Orientation
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As Orientation)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>This class recognizes only the <see cref="PushButtonState.Normal"/> state</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Public Shadows Property State() As PushButtonState
                Get
                    Return MyBase.State
                End Get
                Set(ByVal value As PushButtonState)
                    MyBase.State = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ScrollBarGripper {0}", StateGroup)
            End Function
        End Class
        ''' <summary>Renders scroll bar size box</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ScrollBarSizeBox
            Inherits UxSingleGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("SCROLLBAR", 10, New Integer() {LeftRightAlignment.Left, 2, LeftRightAlignment.Right, 1})
            End Sub
            ''' <summary>Gets or sets current state within group <see cref="StateGroup"/></summary>
            ''' <value>You can set state to any value, event when such value is not member of <see cref="GetStates"/> for curent group. BUt using undefined state is likely to casuse fallabsck to <see cref="PushButtonState.Normal"/> when rendering.</value>
            Public Shadows Property State() As LeftRightAlignment
                Get
                    Return MyBase.State
                End Get
                Set(ByVal value As LeftRightAlignment)
                    MyBase.State = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ScrollBarSizeBox {0}", State)
            End Function
        End Class
#End Region
#Region "TAB"
        ''' <summary>Renders tab item</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class TabItem
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("TAB", 4, New Integer() {TabItemType.Normal, TabItemType.LeftEdge, TabItemType.RightEdge, TabItemType.BothEdges, TabItemType.Top, TabItemType.TopLeftEdge, TabItemType.TopRightEdge, TabItemType.TopBothEdges}, _
                            New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                            New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                            New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                            New Integer() {ps.Normal = 1}, _
                            New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                            New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                            New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3, ps.Disabled = 4}, _
                            New Integer() {ps.Normal = 1})
                Me.SetPartChanges(TabItemType.Normal, TabItemType.Normal, _
                                   TabItemType.LeftEdge, TabItemType.LeftEdge, _
                                   TabItemType.RightEdge, TabItemType.RightEdge, _
                                   TabItemType.BothEdges, TabItemType.BothEdges, _
                                   TabItemType.Top, TabItemType.Top, _
                                   TabItemType.TopLeftEdge, TabItemType.TopLeftEdge, _
                                   TabItemType.TopRightEdge, TabItemType.TopRightEdge, _
                                   TabItemType.TopBothEdges, TabItemType.TopBothEdges)
            End Sub
            ''' <summary>Gets falback group for given group</summary>
            ''' <returns>Group number of fallback group for given group</returns>
            ''' <param name="Group">Group to get fallback group of</param>
            ''' <exception cref="ArgumentException"><paramref name="Group"/> is not defined</exception>
            ''' <remarks>Returns <see cref="TabItemType.Normal"/>. For <see cref="TabItemType.TopBothEdges"/>, <see cref="TabItemType.TopLeftEdge"/> and <see cref="TabItemType.TopRightEdge"/> returns <see cref="TabItemType.Top"/> is curent visual style suports it; <see cref="TabItemType.Normal"/> otherwise.</remarks>
            Protected Overrides Function GetFallBackGroup(ByVal Group As Integer) As Integer
                Select Case CType(Group, TabItemType)
                    Case TabItemType.Normal, TabItemType.BothEdges, TabItemType.LeftEdge, TabItemType.RightEdge, TabItemType.Top : Return TabItemType.Normal
                    Case TabItemType.TopBothEdges, TabItemType.TopLeftEdge, TabItemType.TopRightEdge
                        If VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(ClassName, TabItemType.Top, 1)) Then Return TabItemType.Top Else Return TabItemType.Normal
                    Case Else : Return MyBase.GetFallBackGroup(Group)
                End Select
            End Function
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Public Shadows Property StateGroup() As TabItemType
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As TabItemType)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Tab item type</summary>
            Public Enum TabItemType
                ''' <summary>Default</summary>
                Normal = 1
                ''' <summary>Left edge</summary>
                LeftEdge = 2
                ''' <summary>Right edge</summary>
                RightEdge = 3
                ''' <summary>Both edges</summary>
                BothEdges = 4
                ''' <summary>Top</summary>
                Top = 5
                ''' <summary>Top, left edge</summary>
                TopLeftEdge = 6
                ''' <summary>Top, right edge</summary>
                TopRightEdge = 7
                ''' <summary>Top, both edges</summary>
                TopBothEdges = 8
            End Enum
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ScrollBbarArrow {0} {1}", StateGroup, State)
            End Function
        End Class
#End Region
#Region "EXPLORERBAR"
        ''' <summary>Renders explorer bar header close</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ExplorerHeaderClose
            Inherits UxSingleGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("EXPLORERBAR", 2, 1, 2, 3)
            End Sub
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ExplorerHeaderClose {0}", State)
            End Function
        End Class
        ''' <summary>Renders explorer bar header pin</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ExplorerPin
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("EXPLORERBAR", 3, New Integer() {True, False}, _
                    New Integer() {PushButtonState.Normal, 1, PushButtonState.Hot, 2, PushButtonState.Pressed, 3}, _
                    New Integer() {PushButtonState.Normal, 4, PushButtonState.Hot, 5, PushButtonState.Pressed, 6})
            End Sub
            ''' <summary>Gets state group of this option button</summary>
            ''' <returns>True for checked state group false for unchecked</returns>
            ''' <value>True for checked state false for uncheckd</value>
            Public Shadows Property StateGroup() As Boolean
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As Boolean)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ExplorerPin {0} {1}", StateGroup, State)
            End Function
        End Class
        ''' <summary>Renders explorer bar IE bar menu</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ExplorerIEBarMenu
            Inherits UxSingleGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("EXPLORERBAR", 4, 1, 2, 3)
            End Sub
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ExplorerDoubleArrow {0}", State)
            End Function
        End Class
        ''' <summary>Renders explorer bar header pin</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public NotInheritable Class ExplorerGroup
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("EXPLORERBAR", 6, New Integer() {GroupType.NormalCollapse, GroupType.NormalExpand, GroupType.SpecialCollapse, GroupType.SpecialExpand}, _
                    New Integer() {PushButtonState.Normal, 1, PushButtonState.Hot, 2, PushButtonState.Pressed, 3}, _
                    New Integer() {PushButtonState.Normal, 1, PushButtonState.Hot, 2, PushButtonState.Pressed, 3}, _
                    New Integer() {PushButtonState.Normal, 1, PushButtonState.Hot, 2, PushButtonState.Pressed, 3}, _
                    New Integer() {PushButtonState.Normal, 1, PushButtonState.Hot, 2, PushButtonState.Pressed, 3})
                Me.SetPartChanges(GroupType.NormalCollapse, GroupType.NormalCollapse, GroupType.NormalExpand, GroupType.NormalExpand, GroupType.SpecialCollapse, GroupType.SpecialCollapse, GroupType.SpecialExpand, GroupType.SpecialExpand)
            End Sub
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Public Shadows Property StateGroup() As GroupType
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As GroupType)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
            ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
            Public Overrides Function ToString() As String
                Return String.Format("ExplorerPin {0} {1}", StateGroup, State)
            End Function
            ''' <summary>Group types</summary>
            Public Enum GroupType
                ''' <summary>Norlam collapse</summary>
                NormalCollapse = 6
                ''' <summary>Normal expand</summary>
                NormalExpand = 7
                ''' <summary>Special collapse</summary>
                SpecialCollapse = 10
                ''' <summary>Special expand</summary>
                SpecialExpand = 11
            End Enum
            ''' <summary>Gets falback group for given group</summary>
            ''' <returns>Group number of fallback group for given group</returns>
            ''' <param name="Group">Group to get fallback group of</param>
            ''' <exception cref="ArgumentException"><paramref name="Group"/> is not defined</exception>
            ''' <remarks>For <see cref="GroupType.SpecialCollapse"/> returns <see cref="GroupType.NormalExpand"/>; for <see cref="GroupType.SpecialExpand"/> returns <see cref="GroupType.NormalExpand"/>; <paramref name="Group"/> otherwise.</remarks>
            Protected Overrides Function GetFallBackGroup(ByVal Group As Integer) As Integer
                Select Case CType(Group, GroupType)
                    Case GroupType.SpecialCollapse : Return GroupType.NormalCollapse
                    Case GroupType.SpecialExpand : Return GroupType.NormalExpand
                End Select
                Return MyBase.GetFallBackGroup(Group)
            End Function
        End Class
#End Region
#Region "HEADER"
        ''' <summary>Renders header item</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public Class HeaderItem
            Inherits UxMultiGroupMultiStateRenderer
            ''' <summary>CTor</summary>
            Public Sub New()
                MyBase.New("HEADER", 1, New Integer() {Types.Normal, Types.Left, Types.Right}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3}, _
                           New Integer() {ps.Normal = 1, ps.Hot = 2, ps.Pressed = 3})
                Me.SetPartChanges(Types.Normal, Types.Normal, Types.Left, Types.Left, Types.Right, Types.Right)
            End Sub
            ''' <summary>Header item types</summary>
            Public Enum Types
                ''' <summary>Normal</summary>
                Normal = 1
                ''' <summary>Left (not supported by most themes)</summary>
                Left = 2
                ''' <summary>Right (not supported by most themes)</summary>
                Right = 3
            End Enum
            ''' <summary>Gets or sets group of states state is being selected from</summary>
            ''' <returns>Current group of states state is slecdted from</returns>
            ''' <value>Changes group of states</value>
            ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
            Shadows Property StateGroup() As Types
                Get
                    Return MyBase.StateGroup
                End Get
                Set(ByVal value As Types)
                    MyBase.StateGroup = value
                End Set
            End Property
            ''' <summary>Gets falback group for given group</summary>
            ''' <returns>Group number of fallback group for given group</returns>
            ''' <param name="Group">Group to get fallback group of</param>
            ''' <exception cref="ArgumentException"><paramref name="Group"/> is not defined</exception>
            ''' <remarks>This implementation always returns <see cref="Types.Normal"/> (unless it throws an exception)</remarks>
            Protected Overrides Function GetFallBackGroup(ByVal Group As Integer) As Integer
                Select Case CType(Group, Types)
                    Case Types.Left, Types.Right, Types.Normal : Return Types.Normal
                    Case Else : Return MyBase.GetFallBackGroup(Group)
                End Select
            End Function
        End Class
#End Region
    End Namespace
#Region "Themed renderers"
    ''' <summary>Renderer of button-like control which state is specified by state group and sub-state</summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class UxMultiGroupMultiStateRenderer
        Inherits UxThemeObjectRenderer
        ''' <summary>CTor</summary>
        ''' <param name="ClassName">CLass name to render</param>
        ''' <param name="Part">Part number to render</param>
        ''' <param name="GroupMap">Group map. Key contains numbers of groups, values contains group definitions. In group definition, keys are logical states (any integral value can be used but <see cref="PushButtonState.Normal"/> must be always present) and values are physical styles for renderer.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ClassName"/> or <paramref name="GroupMap"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="GroupMap"/> is empty -or- Group does not contain deifinition for <see cref="PushButtonState.Normal"/>.</exception>
        Public Sub New(ByVal ClassName$, ByVal Part%, ByVal GroupMap As IDictionary(Of Integer, IDictionary(Of PushButtonState, Integer)))
            MyBase.New(ClassName, Part)
            If GroupMap Is Nothing Then Throw New ArgumentNullException("GroupMap")
            If GroupMap.Count = 0 Then Throw New ArgumentException("Group map cannot be empty.")
            Me._GroupMap = New Dictionary(Of Integer, Dictionary(Of PushButtonState, Integer))
            For Each group In GroupMap
                If Not group.Value.ContainsKey(PushButtonState.Normal) Then Throw New ArgumentNullException("Each group must contain definition for Normal state")
                Me.GroupMap.Add(group.Key, New Dictionary(Of PushButtonState, Integer)(group.Value))
            Next
            StateGroup = Me.GroupMap.First.Key
            State = PushButtonState.Normal
        End Sub
        ''' <summary>CTor from group codes and groups in separate arrays</summary>
        ''' <param name="ClassName">CLass name to render</param>
        ''' <param name="Part">Part number to render</param>
        ''' <param name="GroupCodes">Codes of groups. Must have same lenght as <paramref name="GroupContent"/></param>
        ''' <param name="GroupContent">Group definitions. Must have same lenght as <paramref name="GroupCodes"/>. Must have even lengh. First item is logical group code (preferrably one of <see cref="PushButtonState"/> values), second value is physical grou code for renderer etc. Each grooup must contain definition for <see cref="PushButtonState.Normal"/>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ClassName"/>, <paramref name="GroupCodes"/> or <paramref name="GroupContent"/> is null</exception>
        ''' <exception cref="ArgumentException">Length of <paramref name="GroupContent"/> is zero or length of <paramref name="GroupCodes"/> is zero or length of <paramref name="GroupCodes"/> difers from length of <paramref name="GroupContent"/>.
        ''' -or- Group is empty or null -or- Group does not have even number of elements
        ''' -or- Group does not contain deifinition for <see cref="PushButtonState.Normal"/>.</exception>
        Public Sub New(ByVal ClassName$, ByVal Part%, ByVal GroupCodes%(), ByVal ParamArray GroupContent%()())
            Me.New(ClassName, Part, GetGroupMap(GroupCodes, GroupContent))
        End Sub
        ''' <summary>Converts group sepcification in arrays to group map dictionary.</summary>
        ''' <param name="GroupCodes">Codes of groups. Must have same lenght as <paramref name="GroupContent"/></param>
        ''' <param name="GroupContent">Group definitions. Must have same lenght as <paramref name="GroupCodes"/>. Must have even lengh. First item is logical group code (preferrably one of <see cref="PushButtonState"/> values), second value is physical grou code for renderer etc. Each grooup must contain definition for <see cref="PushButtonState.Normal"/>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="GroupCodes"/> or <paramref name="GroupContent"/> is null</exception>
        ''' <exception cref="ArgumentException">Length of <paramref name="GroupContent"/> is zero or length of <paramref name="GroupCodes"/> is zero or length of <paramref name="GroupCodes"/> difers from length of <paramref name="GroupContent"/>.
        ''' -or- Group is empty or null -or- Group does not have even number of elements</exception>
        Private Shared Function GetGroupMap(ByVal GroupCodes%(), ByVal GroupContent%()()) As Dictionary(Of Integer, Dictionary(Of PushButtonState, Integer))
            If GroupCodes Is Nothing Then Throw New ArgumentNullException("GroupCodes")
            If GroupContent Is Nothing Then Throw New ArgumentNullException("GroupContent")
            If GroupCodes.Length <> GroupCodes.Length Then Throw New ArgumentException("Lengths of GroupCOdes and GroupContent arrays must be same")
            If GroupCodes.Length = 0 Then Throw New ArgumentException("At least one group must be specified")
            Dim ret As New Dictionary(Of Integer, Dictionary(Of PushButtonState, Integer))
            For i As Integer = 0 To GroupCodes.Length - 1
                If GroupContent(i) Is Nothing OrElse GroupContent(i).Length = 0 Then Throw New ArgumentException("Group cannot be empty")
                If GroupCodes(i) Mod 2 <> 0 Then Throw New ArgumentException("Group content must have even number of elemnts")
                Dim idic As New Dictionary(Of PushButtonState, Integer)
                For j As Integer = 0 To GroupContent(i).Length - 1 Step 2
                    idic.Add(GroupContent(i)(j), GroupContent(i)(j + 1))
                Next
                ret.Add(GroupCodes(i), idic)
            Next
            Return ret
        End Function
        ''' <summary>Contains value of the <see cref="GroupMap"/> property</summary>
        Private ReadOnly _GroupMap As Dictionary(Of Integer, Dictionary(Of PushButtonState, Integer))
        ''' <summary>Gets group map</summary>
        ''' <remarks>Do not change content of group map</remarks>
        Protected ReadOnly Property GroupMap() As Dictionary(Of Integer, Dictionary(Of PushButtonState, Integer))
            Get
                Return _GroupMap
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="StateGroup"/> property</summary>
        Private _StateGroup As Integer
        ''' <summary>Gets or sets group of states state is being selected from</summary>
        ''' <returns>Current group of states state is slecdted from</returns>
        ''' <value>Changes group of states</value>
        ''' <exception cref="ArgumentException">Value being set is not one of <see cref="GetGroups"/> values</exception>
        Public Overridable Property StateGroup() As Integer
            Get
                Return _StateGroup
            End Get
            Set(ByVal value As Integer)
                If Not GroupMap.ContainsKey(value) Then Throw New ArgumentException("Given group is not defined")
                _StateGroup = value
                SetState()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="PartChanges"/> property</summary>
        Private _StateGroupPartChange As Dictionary(Of Integer, Integer)
        ''' <summary>Gets or sets map of <see cref="StateGroup"/> value to <see cref="Part"/></summary>
        ''' <value>Dictionary, keys are value of the <see cref="StateGroup"/> property; values are value for <see cref="Part"/> property.</value>
        ''' <remarks>When this property is set and <see cref="StateGroup"/> changes, it changes <see cref="Part"/> to corresponding value</remarks>
        ''' <exception cref="InvalidOperationException">Attemt to change value of this property when it was already et to non-null.</exception>
        Protected Property PartChanges() As Dictionary(Of Integer, Integer)
            Get
                Return _StateGroupPartChange
            End Get
            Set(ByVal value As Dictionary(Of Integer, Integer))
                If value Is _StateGroupPartChange Then Exit Property
                If _StateGroupPartChange IsNot Nothing Then Throw New InvalidOperationException("Property was already set")
                _StateGroupPartChange = value
            End Set
        End Property
        ''' <summary>Sets value of the <see cref="PartChanges"/> property from array</summary>
        ''' <param name="Values">Map of <see cref="StateGroup"/> to <see cref="Part"/>. 1st, 2nd, etc. are keys (<see cref="StateGroup"/> values), 2nd, 4th etc. are values (correctponding <see cref="Part"/> values).</param>
        ''' <exception cref="ArgumentException"><paramref name="Values"/> does not contain even number of items</exception>
        ''' <exception cref="InvalidOperationException">Value of the <see cref="PartChanges"/> property not null and <paramref name="Values"/> is not null.</exception>
        Public Sub SetPartChanges(ByVal ParamArray Values As Integer())
            If Values Is Nothing Then PartChanges = Nothing
            If Values.Length Mod 2 <> 0 Then Throw New ArgumentException("Values must have even  number of items")
            Dim dic As New Dictionary(Of Integer, Integer)
            For i As Integer = 0 To Values.Length - 1 Step 2
                dic.Add(Values(i), Values(i + 1))
            Next
            PartChanges = dic
        End Sub

        ''' <summary>Contains value of <see cref="State"/> property</summary>
        Private _State As PushButtonState
        ''' <summary>Gets or sets current state within group <see cref="StateGroup"/></summary>
        ''' <value>You can set state to any value, event when such value is not member of <see cref="GetStates"/> for curent group. BUt using undefined state is likely to casuse fallabsck to <see cref="PushButtonState.Normal"/> when rendering.</value>
        Public Shadows Property State() As PushButtonState
            Get
                Return _State
            End Get
            Set(ByVal value As PushButtonState)
                _State = value
                If PartChanges IsNot Nothing AndAlso PartChanges.ContainsKey(value) Then Part = PartChanges(value)
                SetState()
            End Set
        End Property
        ''' <summary>Gets or sets value of the <see cref="UxThemeObjectRenderer.State"/> property</summary>
        ''' <remarks>Use this property only when you need to manipulate physical state rather than logical stored in <see cref="State"/></remarks>
        Protected Property PhysicalState() As Integer
            Get
                Return MyBase.State
            End Get
            Set(ByVal value As Integer)
                MyBase.State = value
            End Set
        End Property
        ''' <summary>Applies changes in <see cref="StateGroup"/> and <see cref="State"/> properties</summary>
        Private Sub SetState()
            Dim group = GroupMap(StateGroup)
            If group.ContainsKey(State) Then MyBase.State = group(State) Else MyBase.State = group(PushButtonState.Normal)
        End Sub
#Region "Fallback"
        '''' <summary>Gets default state for given state</summary>
        '''' <param name="State">State to get default state of</param>
        '''' <returns>Default state for state <paramref name="State"/>. If <paramref name="Group"/> is defined group, returns <see cref="PushButtonState.Normal"/> for that group; otherwise returns <see cref="PushButtonState.Normal"/> of current group.</returns>
        'Protected Overridable Overloads Function GetDefaultState(ByVal State As Integer, ByVal Group%) As Integer
        '    Return GroupMap(If(GroupMap.ContainsKey(Group), Group, StateGroup))(PushButtonState.Normal)
        'End Function
        ''' <summary>Gets 1st chance fallback state for current state</summary>
        ''' <returns>Fallback state inside current group</returns>
        ''' <remarks>This function cannot be overriden. Override <see cref="GetFallBackState"/> instead.</remarks>
        Protected NotOverridable Overrides Function GetFallback1() As System.Windows.Forms.VisualStyles.VisualStyleElement
            Return VisualStyleElement.CreateElement(ClassName, Part, GroupMap(StateGroup)(GetFallBackState))
        End Function
        ''' <summary>Gets fallback state inside current group</summary>
        ''' <returns>Logical number of fallback state for current <see cref="State"/> in current <see cref="StateGroup"/>.</returns>
        ''' <remarks>Calls parametrized overload</remarks>
        Protected Function GetFallBackState() As PushButtonState
            Return GetFallBackState(StateGroup, State)
        End Function
        ''' <summary>Gets fallback state for given state in given group</summary>
        ''' <param name="Group">Grou to get fallback state for</param>
        ''' <param name="State">State to get fallback state for (this is logical state inside <paramref name="Group"/>)</param>
        ''' <returns>Logical state number of state in group <paramref name="Group"/> which is fallback state for state <paramref name="State"/>. Returned state must be defined in group <paramref name="Group"/>.</returns>
        ''' <remarks>This implementation always returns <see cref="PushButtonState.Normal"/></remarks>
        Protected Overridable Function GetFallBackState(ByVal Group As Integer, ByVal State As PushButtonState) As PushButtonState
            Return PushButtonState.Normal
        End Function
        ''' <summary>Gets 2nd chance fallback state for current state</summary>
        ''' <returns>Same state (if exists; fallback otherwise) in fallback group for this group</returns>
        ''' <remarks>This function cannot be overriden, override <see cref="GetFallBackGroup"/> instead</remarks>
        Protected NotOverridable Overrides Function GetFallBack2() As System.Windows.Forms.VisualStyles.VisualStyleElement
            Dim fbgCode As Object = GetFallBackGroup()
            Dim fbg = GroupMap(fbgCode)
            Dim state = If(fbg.ContainsKey(Me.State), Me.State, GetFallBackState(fbgCode, Me.State))
            If PartChanges IsNot Nothing AndAlso PartChanges.ContainsKey(fbgCode) Then Return VisualStyleElement.CreateElement(ClassName, PartChanges(fbgCode), state)
            Return VisualStyleElement.CreateElement(ClassName, Part, state)
        End Function
        ''' <summary>Gets fallback group for current <see cref="StateGroup"/></summary>
        ''' <returns>Fallback grou for current <see cref="StateGroup"/></returns>
        ''' <remarks>Calls parametrized overload</remarks>
        Protected Function GetFallBackGroup() As Integer
            Return GetFallBackGroup(StateGroup)
        End Function
        ''' <summary>Gets falback group for given group</summary>
        ''' <returns>Group number of fallback group for given group</returns>
        ''' <param name="Group">Group to get fallback group of</param>
        ''' <exception cref="ArgumentException"><paramref name="Group"/> is not defined</exception>
        ''' <remarks>This implementation always returns <paramref name="Group"/> (unless it throws an exception)</remarks>
        Protected Overridable Function GetFallBackGroup(ByVal Group As Integer) As Integer
            If Not GroupMap.ContainsKey(Group) Then Throw New ArgumentException("Group {0} is not defined.".f(Group)) 'Localize: Exception
            Return Group
        End Function
        ''' <summary>Gets 3rd fallback state for current state</summary>
        ''' <returns>Default state in default group of current group</returns>
        ''' <remarks>This method cannot be overriden. Override <see cref="GetFallBackGroup"/> and <see cref="GetFallBackState"/> instead</remarks>
        Protected NotOverridable Overrides Function GetFallBack3() As System.Windows.Forms.VisualStyles.VisualStyleElement
            Dim Group As Integer = GetFallBackGroup()
            Dim state = GetFallBackState(Group, Me.State)
            Return VisualStyleElement.CreateElement(ClassName, _
                    If(PartChanges IsNot Nothing AndAlso PartChanges.ContainsKey(Group), PartChanges(Group), Part), _
                    state)
        End Function
#End Region

        '''' <summary>When overriden in derived class gets default group fro given group</summary>
        '''' <param name="Group">Logical group code (as in <see cref="StateGroup"/>)</param>
        '''' <returns>Logical group code (as in <see cref="StateGroup"/>) for given <paramref name="Group"/>. <paramref name="Group"/> when no defualt group is defined for given group.</returns>
        '''' <remarks>This implementation always returns <paramref name="Group"/>.</remarks>
        'Protected Overridable Function GetDefaultGroup(ByVal Group As Integer) As Integer
        '    Return Group
        'End Function
        ''' <summary>Gets possible value of <see cref="StateGroup"/> property</summary>
        ''' <remarks>Possible value for <see cref="StateGroup"/> property</remarks>
        Public Overridable Function GetGroups() As IEnumerable(Of Integer)
            Return GroupMap.Keys
        End Function
        ''' <summary>Gets possible values of <see cref="State"/> property for given group</summary>
        ''' <param name="Group">Group to get values for</param>
        ''' <returns>Possible value of <see cref="State"/> property when <see cref="StateGroup"/> is <paramref name="Group"/></returns>
        ''' <exception cref="KeyNotFoundException"><paramref name="Group"/> is not defined</exception>
        Public Overridable Overloads Function GetStates(ByVal Group%) As IEnumerable(Of PushButtonState)
            Return GroupMap(Group).Keys
        End Function
        ''' <summary>Gest possible value of the <see cref="UxThemeObjectRenderer.State"/> property</summary>
        Protected Overloads Overrides Function GetStates() As System.Collections.Generic.IEnumerable(Of Integer)
            Return (From g In GroupMap Select gv = g.Value Select From s In gv Select s.Value).UnionAll.Distinct
        End Function
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0} {1} {2} {3}", ClassName, Part, StateGroup, State)
        End Function
    End Class
    ''' <summary>Renderer that can render any button-like control based on UxThemes</summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class UxSingleGroupMultiStateRenderer
        Inherits UxMultiGroupMultiStateRenderer
        ''' <summary>CTor</summary>
        ''' <param name="ClassName">CLass name to render</param>
        ''' <param name="Part">Part number to render</param>
        ''' <param name="Map">State map. Key contains logical states (<see cref="PushButtonState.Normal"/> must be defined; any other integers can be used); values contains physical states for renderer. This is map of single group for <see cref="UxMultiGroupMultiStateRenderer"/>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ClassName"/> is null or <paramref name="Map"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Map"/> does not contain definition for <see cref="PushButtonState.Normal"/></exception>
        Public Sub New(ByVal ClassName$, ByVal Part%, ByVal Map As IDictionary(Of PushButtonState, Integer))
            MyBase.New(ClassName, Part, GetDefaultGroupMap(Map))
            State = PushButtonState.Normal
        End Sub
        ''' <summary>Gets or sets group of states state is being selected from</summary>
        ''' <returns>0</returns>
        ''' <value>Can be only 0</value>
        ''' <exception cref="ArgumentException">Value being set is not 0.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property StateGroup() As Integer
            Get
                Return MyBase.StateGroup
            End Get
            Set(ByVal value As Integer)
                MyBase.StateGroup = value
            End Set
        End Property
        ''' <summary>Gets group map from state map</summary>
        ''' <param name="Map">Group map</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Map"/> is null</exception>
        ''' <returns>Dictionary containing <paramref name="Map"/> at key 0.</returns>
        Private Shared Function GetDefaultGroupMap(ByVal Map As IDictionary(Of PushButtonState, Integer)) As IDictionary(Of Integer, IDictionary(Of PushButtonState, Integer))
            If Map Is Nothing Then Throw New ArgumentNullException("Map")
            Dim ret As New Dictionary(Of Integer, IDictionary(Of PushButtonState, Integer))
            ret.Add(0, Map)
            Return ret
        End Function
        ''' <summary>CTor with <see cref="PushButtonState"/> values mapping</summary>
        ''' <param name="ClassName">CLass name to render</param>
        ''' <param name="Part">Part number to render</param>
        ''' <param name="Normal">Mapping for <see cref="PushButtonState.Normal"/>. Cannot be ommitted</param>
        ''' <param name="Hot">Mapping for <see cref="PushButtonState.Hot"/>. -1 to ommit</param>
        ''' <param name="Pressed">Mapping for <see cref="PushButtonState.Pressed"/>. -1 to ommit</param>
        ''' <param name="Default">Mapping for <see cref="PushButtonState.[Default]"/>. -1 to ommit</param>
        ''' <param name="Disabled">Mapping for <see cref="PushButtonState.Disabled"/>. -1 to ommit</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ClassName"/> is null</exception>
        Public Sub New(ByVal ClassName$, ByVal Part%, ByVal Normal%, Optional ByVal Hot% = -1, Optional ByVal Pressed% = -1, Optional ByVal Default% = -1, Optional ByVal Disabled% = -1)
            Me.New(ClassName, Part, GetDictionary(0, PushButtonState.Normal, Normal, PushButtonState.Hot, Hot, PushButtonState.Pressed, Pressed, PushButtonState.Default, [Default], PushButtonState.Disabled, Disabled))
        End Sub
        ''' <summary>CTor from map in array</summary>
        ''' <param name="ClassName">CLass name to render</param>
        ''' <param name="Part">Part number to render</param>
        ''' <param name="Map">Map. Keys are first, third, fifth etc. Values are second, fourth, sixth, etc. Must have even number of items. <see cref="PushButtonState.Normal"/> must be one of keys, other keys can be any integers, but <see cref="PushButtonState"/> prefferably. When value is -1 it is skipped.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ClassName"/> or <paramref name="Map"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Map"/> has not even number of items, is empty (or contains only -1 values) or does not contain <see cref="PushButtonState.Normal"/> as one of keys (or corresponding value is -1).</exception>
        Public Sub New(ByVal ClassName$, ByVal Part%, ByVal ParamArray Map%())
            Me.New(ClassName, Part, GetDictionary(-1, Map))
        End Sub
        ''' <summary>Gets dictionary map from map in array</summary>
        ''' <param name="NonIgnoreIndex">Index of key in array to inclue even if following value is -1</param>
        ''' <param name="Map">Map in array. Odd indexes are kays, even are values</param>
        ''' <returns>Map as dictionary. For value which's key index is <paramref name="NonIgnoreIndex"/> it is included even when value is -1; otherwise -1s are skiped.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Map"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Map"/> has odd number of items</exception>
        Private Shared Function GetDictionary(ByVal NonIgnoreIndex%, ByVal ParamArray Map%()) As IDictionary(Of PushButtonState, Integer)
            If Map Is Nothing Then Throw New ArgumentNullException("Map")
            Dim ret As New Dictionary(Of PushButtonState, Integer)
            If Map.Length Mod 2 <> 0 Then Throw New ArgumentException("Even number of items must be passed")
            For i As Integer = 0 To Map.Length / 2 - 1
                If i = NonIgnoreIndex OrElse Map(i * 2 + 1) <> -1 Then ret.Add(Map(i * 2), Map(i * 2 + 1))
            Next
            Return ret
        End Function
        ''' <summary>Gets state map</summary>
        ''' <returns>Group map with key 0</returns>
        Private ReadOnly Property StateMap() As Dictionary(Of PushButtonState, Integer)
            Get
                Return GroupMap(0)
            End Get
        End Property

        ''' <summary>Gest possible values of the <see cref="UxThemeObjectRenderer.State"/> property</summary>
        Protected NotOverridable Overrides Function GetStates() As System.Collections.Generic.IEnumerable(Of Integer)
            Return StateMap.Values
        End Function
        ''' <summary>Gets possible value of <see cref="StateGroup"/> property</summary>
        ''' <returns>aray containing only 0</returns>
        Public NotOverridable Overrides Function GetGroups() As System.Collections.Generic.IEnumerable(Of Integer)
            Return New Integer() {0}
        End Function
        ''' <summary>Gets possible values of <see cref="State"/> property for given group</summary>
        ''' <param name="Group">Ignored. Should be 0.</param>
        ''' <returns><see cref="GetPossibleStates"/></returns>
        ''' <exception cref="KeyNotFoundException"><paramref name="Group"/> is not defined</exception>
        Public NotOverridable Overrides Function GetStates(ByVal Group As Integer) As System.Collections.Generic.IEnumerable(Of System.Windows.Forms.VisualStyles.PushButtonState)
            Return GetPossibleStates()
        End Function
        ''' <summary>Gets possible value of the <see cref="State"/> property</summary>
        ''' <returns>Possible value of the <see cref="State"/> property</returns>
        Public Function GetPossibleStates() As IEnumerable(Of PushButtonState)
            Return StateMap.Keys
        End Function
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0} {1} {3}", ClassName, Part, State)
        End Function
    End Class
#End Region
#End Region
#Region "Non-themed"
    ''' <summary>Base class for non-themed renderers</summary>
    ''' <completionlist cref="NonThemedObejctRenderer"/>
    Public MustInherit Class NonThemedObejctRenderer
        Inherits ObjectRenderer
        'TODO: Non-themed renderers for controls
        'TODO: Introduce common base class as in themed
#Region "Static"
        ''' <summary>Gets renderer of push button</summary>
        Public Shared ReadOnly Property PushButton() As SpecializedNonThemedRenderers.PushButton
            Get
                Return New SpecializedNonThemedRenderers.PushButton
            End Get
        End Property
#End Region
    End Class
    Namespace SpecializedNonThemedRenderers
        ''' <summary>Renders pus button</summary>
        ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
        Public Class PushButton : Inherits NonThemedObejctRenderer
            Public Overrides Sub DrawBackground(ByVal g As System.Drawing.Graphics, ByVal BackgroundRectangle As System.Drawing.Rectangle, ByVal ClipRectangle As System.Drawing.Rectangle)
                ControlPaint.DrawButton(g, BackgroundRectangle, State)
            End Sub
            ''' <summary>Contains value of the state property</summary>
            Private _State As ButtonState
            ''' <summary>State of button</summary>
            Public Property State() As ButtonState
                Get
                    Return _State
                End Get
                Set(ByVal value As ButtonState)
                    _State = value
                End Set
            End Property
            ''' <summary>When overiden in derived class gets content rectangle of control being rendered</summary>
            ''' <param name="g">Used for reference</param>
            ''' <param name="ControlRectangle">Rectange of full control including edges</param>
            ''' <returns>Rectangle aloocated for control content</returns>
            ''' <remarks>This implementation always returns <paramref name="ControlRectangle"/> (unles an exception is thrown)</remarks>
            ''' <exception cref="ArgumentNullException"><paramref name="g"/> is null</exception>
            Public Overrides Function GetContentRectangle(ByVal g As System.Drawing.Graphics, ByVal ControlRectangle As System.Drawing.Rectangle) As System.Drawing.Rectangle
                Dim sbs = SystemInformation.Border3DSize
                Return New Rectangle(ControlRectangle.X + sbs.Width, ControlRectangle.Y + sbs.Height, Math.Max(0, ControlRectangle.Width - 2 * sbs.Width), Math.Max(0, ControlRectangle.Height - 2 * sbs.Height))
            End Function
        End Class
    End Namespace
#End Region
#End Region
End Namespace
#End If
