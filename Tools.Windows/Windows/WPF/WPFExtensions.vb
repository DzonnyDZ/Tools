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
    End Module
End Namespace
#End If