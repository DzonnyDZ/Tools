Imports System.Windows.Forms

''' <summary>Implements <see cref="IListerUI"/> for windows forms</summary>
Public Class ListerPluginUIWinForms
    Inherits Control
    Implements IListerUI


    Private ReadOnly Property IListerUI_Handle As System.IntPtr Implements IListerUI.Handle
        Get
            Return Handle
        End Get
    End Property

    <MethodNotSupported()>
    Public Overridable Function LoadNext(e As ListerPluginReInitEventArgs) As Boolean Implements IListerUI.LoadNext
        Throw New NotSupportedException(My.Resources.MethodNotSupported)
    End Function

    Public Overridable Sub OnBeforeClose() Implements IListerUI.OnBeforeClose
        'Do nothing
    End Sub

    <MethodNotSupported()>
    Public Overridable Function OnCommand(e As ListerCommandEventArgs) As Boolean Implements IListerUI.OnCommand
        Throw New NotSupportedException(My.Resources.MethodNotSupported)
    End Function

    <MethodNotSupported()>
    Public Overridable Sub OnNotificationReceived(e As MessageEventArgs) Implements IListerUI.OnNotificationReceived
        Throw New NotSupportedException(My.Resources.MethodNotSupported)
    End Sub

    <MethodNotSupported()>
    Public Overridable Function Print(e As PrintEventArgs) As Boolean Implements IListerUI.Print
        Throw New NotSupportedException(My.Resources.MethodNotSupported)
    End Function

    <MethodNotSupported()>
    Public Overridable Function SearchText(e As TextSearchEventArgs) As Boolean Implements IListerUI.SearchText
        Throw New NotSupportedException(My.Resources.MethodNotSupported)
    End Function

    <MethodNotSupported()>
    Public Overridable Function ShowSearchDialog(e As SearchDialogEventArgs) As Boolean Implements IListerUI.ShowSearchDialog
        Throw New NotSupportedException(My.Resources.MethodNotSupported)
    End Function


    Public ReadOnly Property FileName As String Implements IListerUIInfo.FileName
        Get

        End Get
    End Property


    Public ReadOnly Property Options As ListerShowFlags Implements IListerUIInfo.Options
        Get

        End Get
    End Property

    Public ReadOnly Property ParentWindowHandle As System.IntPtr Implements IListerUIInfo.ParentWindowHandle
        Get

        End Get
    End Property

    Private ReadOnly Property PluginWindowHandle As System.IntPtr Implements IListerUIInfo.PluginWindowHandle
        Get
            Return Handle
        End Get
    End Property
End Class
