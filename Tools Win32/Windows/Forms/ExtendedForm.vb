Imports Tools.API
Imports System.ComponentModel
#If Config <= Nightly Then
Namespace Windows.Forms
    ''' <summary><see cref="System.Windows.Forms.Form"/> with additional functionality based on Win32 API</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz"), Version(1, 0, GetType(ExtendedForm), LastChange:="10/4/2007")> _
    Public Class ExtendedForm : Inherits System.Windows.Forms.Form
        ''' <summary>Items of form's system menu accesible via <see cref="SystemMenuItemEnabled"/></summary>
        Public Enum SystemMenuItem As Integer
            'TODO:Comments
            Close = SystemMenuItems.SC_CLOSE
            Move = SystemMenuItems.SC_MOVE
            Maximize = SystemMenuItems.SC_MAXIMIZE
            Minimize = SystemMenuItems.SC_MINIMIZE
            Size = SystemMenuItems.SC_SIZE
            Restore = SystemMenuItems.SC_RESTORE
        End Enum
        ''' <summary>Possible states of system menu</summary>
        Public Enum SystemMenuState
            ''' <summary>Indicates that the menu item is disabled, but not grayed, so it cannot be selected.</summary>
            Disabled = enmEnableMenuItemStatus.MF_DISABLED
            ''' <summary>Indicates that the menu item is enabled and restored from a grayed state so that it can be selected.</summary>
            Enabled = enmEnableMenuItemStatus.MF_ENABLED
            ''' <summary>Indicates that the menu item is disabled and grayed so that it cannot be selected.</summary>
            Grayed = enmEnableMenuItemStatus.MF_GRAYED
        End Enum
        'TODO:Comments
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        Public Property SystemMenuItemEnabled(ByVal MenuItem As SystemMenuItem) As SystemMenuState
            Get
                Dim Prev As enmPreviousMenuItemStatus = EnableMenuItem(GetSystemMenu(Me.Handle.ToInt32, 0), MenuItem, enmEnableMenuItemStatus.MF_BYCOMMAND Or enmEnableMenuItemStatus.MF_ENABLED)
                If Prev = enmPreviousMenuItemStatus.DoesNotExist Then Throw New ArgumentException("Menu item doesn't exist", "MenuItem")
                SystemMenuItemEnabled = Prev
                Me.SystemMenuItemEnabled(MenuItem) = Prev
            End Get
            Set(ByVal value As SystemMenuState)
                Select Case EnableMenuItem(GetSystemMenu(Me.Handle.ToInt32, 0), MenuItem, enmEnableMenuItemStatus.MF_BYCOMMAND Or value)
                    Case enmPreviousMenuItemStatus.DoesNotExist
                        Throw New ArgumentException("Menu item doesn't exist", "MenuItem")
                    Case Else
                End Select
            End Set
        End Property
        <DefaultValue(GetType(SystemMenuState), "Enabled")> _
        Public Property CloseButtonEnabled() As SystemMenuState
            Get
                Return SystemMenuItemEnabled(SystemMenuItem.Close)
            End Get
            Set(ByVal value As SystemMenuState)
                SystemMenuItemEnabled(SystemMenuItem.Close) = value
            End Set
        End Property
        'Public Property MinimizeButtonEnabled() As SystemMenuState
        '    Get

        '    End Get
        '    Set(ByVal value As SystemMenuState)

        '    End Set
        'End Property
        'Public Property MaximizeButtonEnabled() As SystemMenuState
        '    Get

        '    End Get
        '    Set(ByVal value As SystemMenuState)

        '    End Set
        'End Property
        'Public Property RestoreButtonEnabled() As SystemMenuState
        '    Get

        '    End Get
        '    Set(ByVal value As SystemMenuState)

        '    End Set
        'End Property
    End Class
End Namespace
#End If