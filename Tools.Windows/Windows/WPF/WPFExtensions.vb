Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Windows.Media

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF
    ''' <summary>Contains varios extenstion functions related to Windows Presentation Foundation</summary>
    ''' <version version="1.5.2">Module introduced</version>
    Public Module WpfExtensions
        ''' <summary>Gets parent of given <see cref="DependencyObject"/></summary>
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
        ''' <summary>Looks for ancestor of givenm <see cref="DependencyObject"/> of given type</summary>
        ''' <param name="obj">A <see cref="DependencyObject"/> to get ancestor of</param>
        ''' <typeparam name="TParent">Type of ancestor to get</typeparam>
        ''' <returns>Nearest ancestor of <paramref name="obj"/> which of type <typeparamref name="TParent"/>; null when such ancestor cannot be found.</returns>
        ''' <remarks>This function uses <see cref="GetParent"/> to get parents of investigated <see cref="DependencyObject"´>DependencyObjects</see>.</remarks>
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

        ''' <summary>Finds first visual child of given <see cref="DependencyObject"/> which conforms to given condition</summary>
        ''' <param name="parent">A <see cref="DependencyObject"/> to search for visual child of</param>
        ''' <param name="condition">Function evaluated for each visual child of <paramref name="parent"/> until conforming child is found</param>
        ''' <returns>First visual child of <paramref name="parent"/> for which <paramref name="condition"/> returns true; null when no such child can be found</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="parent"/> is null -or- <paramref name="condition"/> is null</exception>
        ''' <version version="1.3.5" stage="Nightly">This function is new in version 1.3.5</version>
        <Extension()> Public Function FindVisualChild(ByVal parent As DependencyObject, ByVal condition As Func(Of DependencyObject, Boolean)) As DependencyObject
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
        <Extension()> Public Function FindVisualChild(Of T As DependencyObject)(ByVal parent As DependencyObject) As T
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
    End Module
End Namespace
#End If