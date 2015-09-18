Imports System.ComponentModel, Tools.ExtensionsT
Imports System.Runtime.InteropServices

#If True
Namespace API.Messages.Notifications
    ''' <summary>Contains information about a notification message.</summary>
    ''' <remarks>You can use the <see cref="WindowsT.NativeT.Win32Window.SendNotification"/> function to send a notification without dealing with structure marshaling.</remarks>
    ''' <seelaso cref="WindowMessages.WM_NOTIFY"/><seelaso cref="WindowsT.NativeT.Win32Window.SendNotification"/>
    ''' <version version="1.5.3">This structure is new in version 1.5.3</version>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure NMHDR
        ''' <summary>CTor - initializes a new instance of the <see cref="NMHDR"/> structure</summary>
        ''' <param name="hwndFrom">A window handle to the control sending the message.</param>
        ''' <param name="idFrom">An identifier of the control sending the message.</param>
        ''' <param name="code">A notification code. This member can be one of the common notification codes (see Notifications under General Control Reference - http://msdn.microsoft.com/en-us/library/bb775497.aspx), or it can be a control-specific notification code.</param>
        Public Sub New(ByVal hwndFrom As IntPtr, ByVal idFrom As IntPtr, ByVal code As Notification)
            Me.hwndFrom = hwndFrom
            Me.idFrom = idFrom
            Me.code = code
        End Sub
        ''' <summary>A window handle to the control sending the message.</summary>
        Public hwndFrom As IntPtr
        ''' <summary>An identifier of the control sending the message.</summary>
        Public idFrom As IntPtr
        ''' <summary>A notification code. This member can be one of the common notification codes (see Notifications under General Control Reference - http://msdn.microsoft.com/en-us/library/bb775497.aspx), or it can be a control-specific notification code.</summary>
        Public code As Notification
    End Structure

    ''' <summary>Contains known notification codes</summary>
    ''' <seelaso cref="WindowMessages.WM_NOTIFY"/>
    ''' <version version="1.5.3" stage="Nightly">This enumeration is new in version 1.5.3. As of version 1.5.3 this enumeration is incomplete.</version>
    Public Enum Notification As Integer
        ''' <summary>Notifies a tab control's parent window that the currently selected tab is about to change. This notification code is sent in the form of a <see cref="WindowMessages.WM_NOTIFY"/> message.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>lParam</term>
        ''' <description>Pointer to an <see cref="NMHDR"/> structure that contains additional information about this notification. </description></item>
        ''' <item><term>Return Value</term>
        ''' <description>Returns TRUE to prevent the selection from changing, or FALSE to allow the selection to change.</description></item></list>
        ''' <seealso>http://msdn.microsoft.com/en-us/library/bb760571.aspx</seealso></remarks>
        TCN_SELCHANGING = -552
        ''' <summary>Notifies a tab control's parent window that the currently selected tab has changed. This notification code is sent in the form of a <see cref="WindowMessages.WM_NOTIFY"/> message.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>lParam</term>
        ''' <description>Pointer to an <see cref="NMHDR"/> structure that contains additional information about this notification. </description></item>
        ''' <item><term>Return Value</term>
        ''' <description>No return value.</description></item></list>
        ''' <seealso>http://msdn.microsoft.com/en-us/library/bb760571.aspx</seealso></remarks>
        TCN_SELCHANGE = -551
    End Enum
End Namespace
#End If
