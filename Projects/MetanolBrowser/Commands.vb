Imports System.Windows.Markup
Imports Tools.WindowsT.WPF.InputT

''' <summary>Defines commands for file operations</summary>
Public NotInheritable Class FileCommands
    ''' <summary>Private constructor not present - this is static class</summary>
    Partial Private Sub New()
    End Sub
    Private Shared _fileCopy As New RoutedUICommand("Copy", "FileCopy", GetType(FileCommands), New InputGestureCollection() From {New KeyGesture(Key.F5)})
    ''' <summary>Gets a command for cyping file</summary>
    Public Shared ReadOnly Property FileCopy As RoutedUICommand
        Get
            Return _fileCopy
        End Get
    End Property

    Private Shared _fileLink As New RoutedUICommand("Create link", "FileLink", GetType(FileCommands), New InputGestureCollection() From {New KeyGesture(Key.F12)})
    ''' <summary>Gets a command for creating a link (shortcut)</summary>
    Public Shared ReadOnly Property FileLink As RoutedUICommand
        Get
            Return _fileLink
        End Get
    End Property
End Class

''' <summary>Defines command for displaying</summary>
Public NotInheritable Class DisplayCommands
    ''' <summary>Private constructor not present - this is static class</summary>
    Partial Private Sub New()
    End Sub
    Private Shared _toggleFullscreen As New RoutedUICommand("Toggle Fullscreen", "ToggleFullscreen", GetType(FileCommands), New InputGestureCollection() From {New KeyGesture(Key.Return, ModifierKeys.Alt)})
    ''' <summary>Gets a command for toggling a fullscreen view</summary>
    Public Shared ReadOnly Property ToggleFullscreen As RoutedUICommand
        Get
            Return _toggleFullscreen
        End Get
    End Property
End Class


''' <summary>Defines MetanolBrowser-specific commands</summary>
Public NotInheritable Class MetanolBrowserCommands
    ''' <summary>Private constructor not present - this is static class</summary>
    Partial Private Sub New()
    End Sub
    Private Shared _editIptc As New RoutedUICommand("Edit IPTC", "EditIptc", GetType(MetanolBrowserCommands), New InputGestureCollection() From {New FreeKeyGesture(Key.I)})
    ''' <summary>Gets a command for editing IPTC data</summary>
    Public Shared ReadOnly Property EditIptc As RoutedUICommand
        Get
            Return _editIptc
        End Get
    End Property

    Private Shared _editRating As New RoutedUICommand("Edit rating", "EditRating", GetType(MetanolBrowserCommands), New InputGestureCollection() From {New FreeKeyGesture(Key.Multiply), New KeyGesture(Key.R, ModifierKeys.Control)})
    ''' <summary>Gets a command for editing rating</summary>
    Public Shared ReadOnly Property EditRating As RoutedUICommand
        Get
            Return _editRating
        End Get
    End Property
End Class