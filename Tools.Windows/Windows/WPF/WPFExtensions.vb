Imports System.Runtime.CompilerServices
Imports System.Windows, System.Linq
Imports System.Windows.Media
Imports Tools.WindowsT.FormsT.UtilitiesT.WinFormsExtensions
Imports System.Windows.Markup
Imports System.Xaml, Tools.ExtensionsT
Imports System.Windows.Controls
Imports Tools.LinqT

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF
    ''' <summary>Contains varios extenstion functions related to Windows Presentation Foundation</summary>
    ''' <version version="1.5.2">Module introduced</version>
    Public Module WpfExtensions
#Region "Visual Tree"
        ''' <summary>Gets parent (in visual tree) of given <see cref="DependencyObject"/></summary>
        ''' <param name="obj"><see cref="DependencyObject"/> to get parent of</param>
        ''' <returns>Parent of given dependency object; null when <paramref name="obj"/> is null or no parent can be found</returns>
        ''' <remarks>Parent is obtained either via <see cref="ContentOperations.GetParent"/> for <see cref="ContentElement">ContentElement</see> or via <see cref="VisualTreeHelper.GetParent"/>.</remarks>
        ''' <author www="http://code.logos.com/blog/2008/02/finding_ancestor_elements_in_w.html">Ed Ball</author>
        <Extension()> Public Function GetParent(ByVal obj As DependencyObject) As DependencyObject
            If obj Is Nothing Then Return Nothing
            Dim ce As ContentElement = TryCast(obj, ContentElement)
            If ce IsNot Nothing Then
                Dim parent As DependencyObject = ContentOperations.GetParent(ce)
                If parent IsNot Nothing Then Return parent
                Dim fce As FrameworkContentElement = TryCast(ce, FrameworkContentElement)
                Return If(fce IsNot Nothing, fce.Parent, Nothing)
            End If
            Return VisualTreeHelper.GetParent(obj)
        End Function
        ''' <summary>Looks for visual tree ancestor of given <see cref="DependencyObject"/> of given type</summary>
        ''' <param name="obj">A <see cref="DependencyObject"/> to get ancestor of</param>
        ''' <typeparam name="TParent">Type of ancestor to get</typeparam>
        ''' <returns>Nearest ancestor of <paramref name="obj"/> which of type <typeparamref name="TParent"/>; null when such ancestor cannot be found.</returns>
        ''' <remarks>This function uses <see cref="GetParent"/> to get parents of investigated <see cref="DependencyObject">DependencyObjects</see>.</remarks>
        ''' <seelaso cref="GetParent"/>
        ''' <version stage="Nightly" version="1.5.3">This function is new in version 1.5.3</version> 
        <Extension()> Public Function GetParent(Of TParent As DependencyObject)(ByVal obj As DependencyObject) As TParent
            Dim p As DependencyObject = obj.GetParent
            Do
                If p Is Nothing Then Return Nothing
                If TypeOf p Is TParent Then Return p
                p = p.GetParent
            Loop
        End Function
        ''' <summary>Sets window owner and opens it and returns only when the newly opened window is closed.</summary>
        ''' <returns>A <see cref="System.Nullable(Of T)" /> value of type <see cref="System.Boolean" /> that signifies how a window was closed by the user.</returns>
        ''' <exception cref="System.InvalidOperationException"><see cref="System.Windows.Window.ShowDialog" /> is called on a <see cref="System.Windows.Window" /> that is visible -or- <see cref="System.Windows.Window.ShowDialog" /> is called on a visible <see cref="System.Windows.Window" /> that was opened by calling <see cref="System.Windows.Window.ShowDialog" />. -or-
        ''' <see cref="System.Windows.Window.ShowDialog" /> is called on a window that is closing (<see cref="System.Windows.Window.Closing" />) or has been closed (<see cref="System.Windows.Window.Closed" />).</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <param name="Window">Window to be shown</param>
        ''' <param name="Owner">Owner <see cref="Window"/> or element in <see cref="Window"/> to show <paramref name="Window"/> modally against.</param>
        ''' <remarks>If <paramref name="Owner"/> is null or <paramref name="Owner"/> is not of type <see cref="Window"/> and function <see cref="GetParent(Of Window)"/> cannot find parent <see cref="Window"/> of <paramref name="Owner"/>, <paramref name="Owner"/> is ignored and original <see cref="Window.Owner"/> is kept.</remarks>
        ''' <version stage="Nightly" version="1.5.3">This function is new in version 1.5.3</version> 
        <Extension()> Public Function ShowDialog(ByVal Window As Window, ByVal Owner As UIElement) As Boolean?
            If Window Is Nothing Then Throw New ArgumentNullException("Window")
            If Owner IsNot Nothing Then
                If TypeOf Owner Is Window Then
                    Window.Owner = Owner
                Else
                    Dim newOwner = Owner.GetParent(Of Window)()
                    If newOwner IsNot Nothing Then Window.Owner = newOwner
                End If
            End If
            Return Window.ShowDialog()
        End Function

        ''' <summary>Enumerates all the visual children of given <see cref="DependencyObject"/> which conform to given condition</summary>
        ''' <param name="parent">A <see cref="DependencyObject"/> to look for children in</param>
        ''' <param name="condition">Condition to be used to filer children objects</param>
        ''' <param name="onlyFirstLevel">True not to look inside objects fulfilling the condition, false to enumerate children of these objects recursivelly.</param>
        ''' <returns>Visual children of <paramref name="parent"/> fulfilling <paramref name="condition"/>. If <paramref name="onlyFirstLevel"/> is false search is done inside these objects as well and objects are returned in in-order order.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="parent"/> is null or <paramref name="condition"/> is null.</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function FindVisualChildren(ByVal parent As DependencyObject, ByVal condition As Func(Of DependencyObject, Boolean), ByVal onlyFirstLevel As Boolean) As IEnumerable(Of DependencyObject)
            'Static level% = 0
            If parent Is Nothing Then Throw New ArgumentNullException("parent")
            If condition Is Nothing Then Throw New ArgumentNullException("condition")
            Dim ret As New List(Of DependencyObject)
            For i = 0 To VisualTreeHelper.GetChildrenCount(parent) - 1
                Dim added As Boolean = False
                Dim child As DependencyObject = VisualTreeHelper.GetChild(parent, i)
                'Debug.Print("{0}{1} {2}", New String(Tab, level), child.GetType.Name, If(TypeOf child Is FrameworkElement, DirectCast(child, FrameworkElement).Name, ""))
                If condition(child) Then
                    ret.Add(child)
                    added = True
                End If
                If Not added OrElse Not onlyFirstLevel Then
                    'level += 1
                    'Try
                    ret.AddRange(child.FindVisualChildren(condition, onlyFirstLevel))
                    'Finally
                    '    level -= 1
                    'End Try
                End If
            Next
            Return ret
        End Function
        ''' <summary>Enumerates all the visual children of given <see cref="DependencyObject"/> of given type</summary>
        ''' <typeparam name="T">Type (derived from <see cref="DependencyObject"/>) to to serach for children of</typeparam>
        ''' <param name="parent">A <see cref="DependencyObject"/> to look for children in</param>
        ''' <param name="onlyFirstLevel">True not to look inside objects of type <typeparamref name="T"/>, false to enumerate children of these objects recursivelly.</param>
        ''' <returns>Visual children of <paramref name="parent"/> of type <typeparamref name="T"/>. If <paramref name="onlyFirstLevel"/> is false search is done inside these objects as well and objects are returned in in-order order.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="parent"/> is null.</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function FindVisualChildren(Of T As DependencyObject)(ByVal parent As DependencyObject, ByVal onlyFirstLevel As Boolean) As IEnumerable(Of T)
            Return FindVisualChildren(parent, Function([do]) TypeOf [do] Is T, onlyFirstLevel).OfType(Of T)()
        End Function

        ''' <summary>Finds first visual child of given <see cref="DependencyObject"/> which conforms to given condition</summary>
        ''' <param name="parent">A <see cref="DependencyObject"/> to search for visual child of</param>
        ''' <param name="condition">Function evaluated for each visual child of <paramref name="parent"/> until conforming child is found</param>
        ''' <returns>First visual child of <paramref name="parent"/> for which <paramref name="condition"/> returns true; null when no such child can be found</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="parent"/> is null -or- <paramref name="condition"/> is null</exception>
        ''' <version version="1.3.5" stage="Nightly">This function is new in version 1.3.5</version>
        <Extension()>
        Public Function FindVisualChild(ByVal parent As DependencyObject, ByVal condition As Func(Of DependencyObject, Boolean)) As DependencyObject
            If parent Is Nothing Then Throw New ArgumentNullException("parent")
            If condition Is Nothing Then Throw New ArgumentNullException("condition")
            For i = 0 To VisualTreeHelper.GetChildrenCount(parent) - 1
                If condition(VisualTreeHelper.GetChild(parent, i)) Then Return VisualTreeHelper.GetChild(parent, i)
            Next
            For i = 0 To VisualTreeHelper.GetChildrenCount(parent) - 1
                Dim ret = VisualTreeHelper.GetChild(parent, i).FindVisualChild(condition)
                If ret IsNot Nothing Then Return ret
            Next
            Return Nothing
        End Function
        ''' <summary>Finds first visual child of given <see cref="DependencyObject"/> of specific type</summary>
        ''' <param name="parent">A <see cref="DependencyObject"/> to search for visual child of</param>
        ''' <typeparam name="T">Type of child to search for</typeparam>
        ''' <returns>First visual child of <paramref name="parent"/> which is of type <typeparamref name="T"/>; null when no such child can be found</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="parent"/> is null</exception>
        ''' <version version="1.3.5" stage="Nightly">This function is new in version 1.3.5</version>
        <Extension()>
        Public Function FindVisualChild(Of T As DependencyObject)(ByVal parent As DependencyObject) As T
            Return parent.FindVisualChild(Function(a) TypeOf a Is T)
        End Function
        ''' <summary>Finds first visual child of given <see cref="DependencyObject"/> of specific type which conforms to given condition</summary>
        ''' <param name="parent">A <see cref="DependencyObject"/> to search for visual child of</param>
        ''' <param name="condition">Function evaluated for each visual child of <paramref name="parent"/> which is of type <typeparamref name="T"/> until conforming child is found</param>
        ''' <typeparam name="T">Type of child to search for</typeparam>
        ''' <returns>First visual child of <paramref name="parent"/> which is of type <typeparamref name="T"/> and which conforms to <paramref name="condition"/>; null when no such child can be found</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="parent"/> is null -or- <paramref name="condition"/> is null</exception>
        ''' <version version="1.3.5" stage="Nightly">This function is new in version 1.3.5</version>
        <Extension()> Public Function FindVisualChild(Of T As DependencyObject)(ByVal parent As DependencyObject, ByVal condition As Func(Of T, Boolean)) As T
            Return parent.FindVisualChild(Function(a) If(TypeOf a Is T, condition(a), False))
        End Function
#End Region

#Region "Logical Tree"
        ''' <summary>Searches for ancestor of given WPF object of given type in logical tree</summary>
        ''' <param name="obj">Object to find ancestor of</param>
        ''' <typeparam name="TAncestor">Type of ancestor to find. This type must be or derive from <see cref="DependencyObject"/>.</typeparam>
        ''' <returns>The closest ancestor of object <paramref name="obj"/> which's type is <typeparamref name="TAncestor"/>. Null where there is no such ancestor.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="obj"/> is null</exception>
        ''' <remarks>
        ''' This function uses <see cref="ContentOperations.GetParent"/> to walk visual tree upwards from <paramref name="obj"/>.
        ''' <para>If <typeparamref name="TAncestor"/> is <see cref="Window"/> (not type derived from <see cref="Window"/>) <see cref="Window.GetWindow"/> is used instead.</para>
        ''' </remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function FindAncestor(Of TAncestor As DependencyObject)(ByVal obj As DependencyObject) As TAncestor
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            If GetType(TAncestor).Equals(GetType(Window)) Then Return CObj(Window.GetWindow(obj))
            Dim currobj As DependencyObject = obj
            Do
                currobj = LogicalTreeHelper.GetParent(currobj)
                If currobj Is Nothing Then Return Nothing
                If TypeOf currobj Is TAncestor Then Return currobj
            Loop
            Return Nothing
        End Function
#End Region

#Region "Windows"
        ''' <summary>Sets windows position and size. Prevents window from leaking out of screen.</summary>
        ''' <param name="Window">Window to set position of</param>
        ''' <param name="Position">Proposed position and size of <paramref name="Window"/> in window client coordinates</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <remarks>If <paramref name="Position"/> <see cref="System.Drawing.Rectangle.IsEmpty">is empty</see>, neither window position nor size is set, but off-screen prevention alghoritm is run for window current position.</remarks>
        <Extension()>
        Public Sub SetWindowPosition(ByVal Window As Window, ByVal Position As System.Drawing.Rectangle)
            If Window Is Nothing Then Throw New ArgumentNullException("Window")
            If Not Position.IsEmpty Then
                Window.Left = Position.Left
                Window.Top = Position.Top
                Window.Height = Position.Height
                Window.Width = Position.Width
            End If

            Dim winTopLeft = PresentationSource.FromVisual(Window).CompositionTarget.TransformToDevice.Transform(New Point(Window.Left, Window.Top))
            Dim winBottomRight = PresentationSource.FromVisual(Window).CompositionTarget.TransformToDevice.Transform(New Point(Window.Left + Window.ActualWidth, Window.Top + Window.ActualHeight))
            Dim winRect = System.Drawing.Rectangle.FromLTRB(winTopLeft.X, winTopLeft.Y, winBottomRight.X, winBottomRight.Y)

            Dim winScreen = Forms.Screen.FromRectangle(winRect)

            If winScreen.WorkingArea.Contains(winRect) Then Exit Sub

            Dim Left = winScreen.GetNeighbourScreen(Direction.Left)
            Dim Right = winScreen.GetNeighbourScreen(Direction.Right)
            Dim Top = winScreen.GetNeighbourScreen(Direction.Top)
            Dim Bottom = winScreen.GetNeighbourScreen(Direction.Bottom)

            Dim MoveRight = 0%
            Dim MoveDown = 0%

            If winRect.Left < winScreen.WorkingArea.Left AndAlso Left Is Nothing Then MoveRight = winScreen.WorkingArea.Left - winRect.Left
            If winRect.Top < winScreen.WorkingArea.Top AndAlso Top Is Nothing Then MoveDown = winScreen.WorkingArea.Top - winRect.Top
            If winRect.Bottom > winScreen.WorkingArea.Bottom AndAlso Bottom Is Nothing Then MoveDown = winScreen.WorkingArea.Bottom - winRect.Bottom
            If winRect.Right > winScreen.WorkingArea.Right AndAlso Right Is Nothing Then MoveRight = winScreen.WorkingArea.Right - winRect.Right

            If MoveRight <> 0 OrElse MoveDown <> 0 Then
                Dim newloc = PresentationSource.FromVisual(Window).CompositionTarget.TransformFromDevice.Transform(New Point(winRect.Left + MoveRight, winRect.Top + MoveDown))
                Window.Left = newloc.X
                Window.Top = newloc.Y
            End If
        End Sub
        ''' <summary>Gets size and location of the <see cref="Window"/></summary>
        ''' <param name="Window">A <see cref="Window"/> to get size and location of</param>
        ''' <returns>Rectangle of <paramref name="Window"/> in window coordinates</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        <Extension()> Public Function GetWindowPosition(ByVal Window As Window) As System.Drawing.Rectangle
            If Window Is Nothing Then Throw New ArgumentNullException("Window")
            Return New System.Drawing.Rectangle(Window.Left, Window.Top, Window.ActualWidth, Window.ActualHeight)
        End Function

#End Region

        ''' <summary>Gets one of predefined well-known XAML services usually provided by <see cref="IServiceProvider"/> in XAML context such as <see cref="MarkupExtension.ProvideValue"/>.</summary>
        ''' <param name="provider">An <see cref="IServiceProvider"/> to get service from.</param>
        ''' <param name="service">Specifies service to get</param>
        ''' <returns>Service requested in <paramref name="service"/> parameter; nulll when <paramref name="provider"/> is nulll or does not provide service requested.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="service"/> is not one of <see cref="XamlService"/> values</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function GetXamlService(ByVal provider As IServiceProvider, ByVal service As XamlService) As Object
            Return GetXamlService(provider, service, False)
        End Function
        ''' <summary>Gets one of predefined well-known XAML services usually provided by <see cref="IServiceProvider"/> in XAML context such as <see cref="MarkupExtension.ProvideValue"/>.</summary>
        ''' <param name="provider">An <see cref="IServiceProvider"/> to get service from.</param>
        ''' <param name="service">Specifies service to get</param>
        ''' <param name="throwException">True to throw exception when <paramref name="provider"/> is null or does not provide service requested.</param>
        ''' <returns>Service requested in <paramref name="service"/> parameter; nulll when <paramref name="provider"/> is nulll or does not provide service requested.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="service"/> is not one of <see cref="XamlService"/> values. <note>This exception is thrown even when <paramref name="throwException"/> is false.</note></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="throwException"/> is true and <paramref name="provider"/> is null.</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="provider"/> does not provide service requested and <paramref name="throwException"/> is true.</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function GetXamlService(ByVal provider As IServiceProvider, ByVal service As XamlService, ByVal throwException As Boolean) As Object
            Dim ret As Object
            If provider Is Nothing AndAlso throwException Then Throw New ArgumentNullException("provider")
            If provider Is Nothing Then Return Nothing
            Select Case service
                Case XamlService.ProvideValueTarget : ret = provider.GetService(GetType(IProvideValueTarget))
                Case XamlService.XamlTypeResolver : ret = provider.GetService(GetType(IXamlTypeResolver))
                Case XamlService.XamlSchemaContextProvider : ret = provider.GetService(GetType(IXamlSchemaContextProvider))
                Case XamlService.UriContext : ret = provider.GetService(GetType(IUriContext))
                Case XamlService.AmbientProvider : ret = provider.GetService(GetType(IAmbientProvider))
                Case XamlService.DestinationTypeProvider : ret = provider.GetService(GetType(IDestinationTypeProvider))
                Case XamlService.RootObjectProvider : ret = provider.GetService(GetType(IRootObjectProvider))
                Case XamlService.XamlNameResolver : ret = provider.GetService(GetType(IXamlNameResolver))
                Case XamlService.XamlNamespaceResolver : ret = provider.GetService(GetType(IXamlNamespaceResolver))
                Case Else : Throw New InvalidEnumArgumentException("service", service, GetType(XamlService))
            End Select
            If ret Is Nothing AndAlso throwException Then Throw New InvalidOperationException(ResourcesT.Exceptions.ProviderDoesNotProviderService.f(service))
            Return ret
        End Function

        ''' <summary>Gets a value that indicates whether any binding on given lement or any of its logical children has a <see cref="ValidationError"/>.</summary>
        ''' <param name="parent">The element to check recursivelly for validation errors</param>
        ''' <returns>True if the <see cref="Validation.HasErrorProperty"/> attached property is true for <paramref name="parent"/> or any of its logical children</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="parent"/> is null</exception>
        ''' <seelaso cref="Validation.HasErrorProperty"/><seelaso cref="LogicalTreeHelper.GetChildren"/>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function HasErrors(ByVal parent As DependencyObject) As Boolean
            If parent Is Nothing Then Throw New ArgumentNullException("parent")
            If Validation.GetHasError(parent) Then Return True
            For i = 0 To VisualTreeHelper.GetChildrenCount(parent) - 1
                If VisualTreeHelper.GetChild(parent, i).HasErrors Then Return True
            Next
            Return False
        End Function
        ''' <summary>Recursivelly gets validation errors of given elemnent and all its logical children</summary>
        ''' <param name="parent">The element to check recursivelly for validation errors</param>
        ''' <returns>Collection of validation errors for <paramref name="parent"/> and all its logical children</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="parent"/> is null</exception>
        ''' <seelaso cref="Validation.ErrorsProperty"/><seelaso cref="LogicalTreeHelper.GetChildren"/>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function GetErrors(ByVal parent As DependencyObject) As IEnumerable(Of ValidationError)
            Return FlatAllDeffered(New IEnumerable(Of ValidationError)() {Validation.GetErrors(parent), FlatAllDeffered(
                                       New ForLoopCollection(Of IEnumerable(Of ValidationError))(0, VisualTreeHelper.GetChildrenCount(parent) - 1,
                                           Sub(ByRef i, ByRef yield) yield = VisualTreeHelper.GetChild(parent, i).GetErrors()
                                      ))})
        End Function
    End Module

    ''' <summary>Enumeration of well-known services that are often used with XAML (especially by <c>serviceProvider</c> parameters of <see cref="MarkupExtension.ProvideValue"/>)</summary>
    ''' <seelaso cref="MarkupExtension.ProvideValue"/>
    ''' <version stage="1.5.3">This enumeration is new in version 1.5.3</version>
    Public Enum XamlService
        ''' <summary><see cref="IProvideValueTarget"/> (Reports situational object-property relationships for markup extension evaluation.)</summary>
        ProvideValueTarget
        ''' <summary><see cref="IXamlTypeResolver"/> (Resolves from named elements in XAML markup to the appropriate CLR type.)</summary>
        XamlTypeResolver
        ''' <summary><see cref="IXamlSchemaContextProvider"/> (Provides XAML schema context information to type converters and markup extensions.)</summary>
        XamlSchemaContextProvider
        ''' <summary><see cref="IUriContext"/> (Can use application context to resolve a provided relative URI to an absolute URI.)</summary>
        UriContext
        ''' <summary><see cref="IAmbientProvider"/> (Can return information items of ambient properties or ambient types to type converters and markup extensions.)</summary>
        AmbientProvider
        ''' <summary><see cref="IDestinationTypeProvider"/> (Can return a type system identifier for the destination type. The destination type is relevant for cases where there is an indirect reporting of destination type for a set operation based on reflection or other mechanisms.)</summary>
        DestinationTypeProvider
        ''' <summary><see cref="IRootObjectProvider"/> (Can return the root object of markup being parsed.)</summary>
        RootObjectProvider
        ''' <summary><see cref="IXamlNameResolver"/> (Can return objects specified by name, or alternatively returns a token. The service can also return an enumerable set of all named objects that are in the XAML namescope.)</summary>
        XamlNameResolver
        ''' <summary><see cref="IXamlNamespaceResolver"/> (Can return a XAML namespace based on its prefix as mapped in XAML markup.)</summary>
        XamlNamespaceResolver
    End Enum
End Namespace
#End If