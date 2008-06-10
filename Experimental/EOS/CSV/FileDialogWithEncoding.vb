Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Runtime.InteropServices
Imports Tools.WindowsT.FormsT

''' <summary>Dialog pro výbìr souboru a kódování, rozšiøitelný</summary>
Public Class FileDialogWithEncoding : Inherits Component
    Private m_LabelHandle As Integer = 0
    Private m_ComboHandle As Integer = 0
    Private m_ButtonHandle As Integer = 0
    Private m_Filter As String = ""
    Private m_DefaultExt As String = ""
    Private m_FileName As String = ""
    Private m_ActiveScreen As Screen
    Private m_DialogType As DialogTypes = DialogTypes.Open
    Private EncS As New EncodingSelector
    Private WithEvents Button As New Button
    Public Property DialogType() As DialogTypes
        Get
            Return m_DialogType
        End Get
        Set(ByVal value As DialogTypes)
            m_DialogType = value
        End Set
    End Property
    Public Enum DialogTypes
        Open
        Save
    End Enum

#Region "API const"
    Private Const stc2 As Integer = 1089
    Private Const cmb1 As Integer = 1136
    ''' <summary>ID PlacesBaru</summary>
    ''' <remarks>Zjištìno pøes Winspectory Spy (http://www.windows-spy.com/)</remarks>
    Private Const PlacesBar As Integer = 1184
    Private Const OFN_ENABLEHOOK As Integer = 32
    Private Const OFN_EXPLORER As Integer = 524288
    Private Const OFN_FILEMUSTEXIST As Integer = 4096
    Private Const OFN_HIDEREADONLY As Integer = 4
    Private Const OFN_CREATEPROMPT As Integer = 8192
    Private Const OFN_NOTESTFILECREATE As Integer = 65536
    Private Const OFN_OVERWRITEPROMPT As Integer = 2
    Private Const OFN_PATHMUSTEXIST As Integer = 2048
    Private Const SWP_NOSIZE As Integer = 1
    Private Const SWP_NOMOVE As Integer = 2
    Private Const SWP_NOZORDER As Integer = 4
    Private Const WM_INITDIALOG As Integer = 272
    Private Const WM_DESTROY As Integer = 2
    Private Const WM_SETFONT As Integer = 48
    Private Const WM_GETFONT As Integer = 49
    Private Const CBS_DROPDOWNLIST As Integer = 3
    Private Const CBS_HASSTRINGS As Integer = 512
    Private Const CB_ADDSTRING As Integer = 323
    Private Const CB_SETCURSEL As Integer = 334
    Private Const CB_GETCURSEL As Integer = 327
    Private Const WS_VISIBLE As UInteger = 268435456
    Private Const WS_CHILD As UInteger = 1073741824
    Private Const WS_TABSTOP As UInteger = 65536
    Private Const WM_NOTIFY As Integer = 78
    Private Const DWL_MSGRESULT As Int32 = 0
    Private Const CDN_FILEOK As Int32 = (CDN_FIRST - &H5)
    Private Const CDN_FIRST As Int32 = (-601)
    Private Const CDN_FOLDERCHANGE As Int32 = (CDN_FIRST - &H2)
    Private Const CDN_HELP As Int32 = (CDN_FIRST - &H4)
    Private Const CDN_INCLUDEITEM As Int32 = (CDN_FIRST - &H7)
    Private Const CDN_INITDONE As Int32 = (CDN_FIRST - &H0)
    Private Const CDN_LAST As Int32 = (-699)
    Private Const CDN_SELCHANGE As Int32 = (CDN_FIRST - &H1)
    Private Const CDN_SHAREVIOLATION As Int32 = (CDN_FIRST - &H3)
    Private Const CDN_TYPECHANGE As Int32 = (CDN_FIRST - &H6)
#End Region

    Private _Additional As Control = Nothing
    Public Property Additional() As Control
        Get
            Return _Additional
        End Get
        Set(ByVal value As Control)
            _Additional = value
        End Set
    End Property
    Public Property DefaultExt() As String
        Get
            Return m_DefaultExt
        End Get
        Set(ByVal value As String)
            m_DefaultExt = value
        End Set
    End Property
    Public Property Filter() As String
        Get
            Return m_Filter
        End Get
        Set(ByVal value As String)
            m_Filter = value
        End Set
    End Property
    Public Property FileName() As String
        Get
            Return m_FileName
        End Get
        Set(ByVal value As String)
            m_FileName = value
        End Set
    End Property
    Public Sub New()
        Try : EncS.SelectedName = System.Text.Encoding.UTF8.WebName : Catch : End Try
        EncS.Style = EncodingSelector.EncodingSelectorStyle.ComboBox
    End Sub
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property Encoding() As System.Text.EncodingInfo
        Get
            Return EncS.SelectedEncoding
        End Get
        Set(ByVal value As System.Text.EncodingInfo)
            EncS.SelectedEncoding = value
        End Set
    End Property
    Private Class MyTC : Inherits Tools.ComponentModelT.TypeConverter(Of String)
        Public Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            Return True
        End Function
        Public Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            Return False
        End Function
        Public Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
            Dim ret As New List(Of String)
            For Each ei As System.Text.EncodingInfo In System.Text.Encoding.GetEncodings
                ret.Add(ei.Name)
            Next ei
            Return New System.ComponentModel.TypeConverter.StandardValuesCollection(ret)
        End Function
    End Class
    <TypeConverter(GetType(MyTC))> _
    Public Property EncodingName() As String
        Get
            Return EncS.SelectedName
        End Get
        Set(ByVal value As String)
            EncS.SelectedName = value
        End Set
    End Property
#Region "API"
    Private Declare Auto Function GetSaveFileName Lib "Comdlg32.dll" (ByRef lpofn As OPENFILENAME) As Boolean
    Private Declare Auto Function GetOpenFileName Lib "comdlg32.dll" (ByRef lpofn As OPENFILENAME) As Boolean
    Private Declare Function CommDlgExtendedError Lib "Comdlg32.dll" () As Integer
    Private Declare Function SetWindowPos Lib "user32.dll" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Boolean
    Private Declare Function GetWindowRect Lib "user32.dll" (ByVal hWnd As Integer, ByRef lpRect As RECT) As Boolean
    Private Declare Function GetParent Lib "user32.dll" (ByVal hWnd As Integer) As Integer
    Private Declare Auto Function SetWindowText Lib "user32.dll" (ByVal hWnd As Integer, ByVal lpString As String) As Boolean
    Private Overloads Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As Integer, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Private Overloads Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As Integer, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer
    Private Declare Function DestroyWindow Lib "user32.dll" (ByVal hwnd As Integer) As Boolean
    Private Declare Auto Function GetDlgItem Lib "user32.dll" (ByVal hDlg As Integer, ByVal nIDDlgItem As Integer) As Integer
    'Private Declare Auto Function CreateWindowEx Lib "user32.dll" (ByVal dwExStyle As Integer, ByVal lpClassName As String, ByVal lpWindowName As String, ByVal dwStyle As UInteger, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hWndParent As Integer, ByVal hMenu As Integer, ByVal hInstance As Integer, ByVal lpParam As Integer) As Integer
    Private Declare Function ScreenToClient Lib "user32.dll" (ByVal hWnd As Integer, ByRef lpPoint As POINT) As Boolean
    Private Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Int32
    Private Declare Function MoveWindow Lib "user32.dll" (ByVal hwnd As Int32, ByVal x As Int32, ByVal y As Int32, ByVal nWidth As Int32, ByVal nHeight As Int32, ByVal bRepaint As Int32) As Int32
    Private Declare Auto Function SetWindowLong Lib "user32.dll" (ByVal hwnd As Int32, ByVal nIndex As Int32, ByVal dwNewLong As Int32) As Int32
    Private Declare Function EndDialog Lib "user32.dll" (ByVal hDlg As Int32, ByVal nResult As Int32) As Int32
    Private Declare Function BringWindowToTop Lib "user32.dll" (ByVal hwnd As Int32) As Int32
    Private Declare Function SetActiveWindow Lib "user32.dll" (ByVal hwnd As Int32) As Int32
    Private Declare Function SetFocus Lib "user32.dll" (ByVal hwnd As Int32) As Int32
#End Region
    Private dlgHandle As Integer
    Private OldParent As Integer
    Private OldVisible As Integer
    Private Function HookProc(ByVal hdlg As Integer, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
        Select Case (msg)
            Case WM_INITDIALOG
                dlgHandle = hdlg
                'Center dialog
                Dim sr As Rectangle = m_ActiveScreen.Bounds
                Dim cr As RECT = New RECT
                Dim parent As Integer = GetParent(hdlg)
                GetWindowRect(parent, cr)
                Dim x As Integer = ((sr.Right + (sr.Left - (cr.Right - cr.Left))) / 2)
                Dim y As Integer = ((sr.Bottom + (sr.Top - (cr.Bottom - cr.Top))) / 2)
                SetWindowPos(parent, 0, x, y, (cr.Right - cr.Left), ((cr.Bottom - cr.Top) + 32), SWP_NOZORDER)
                'Position of encoding label
                Dim fileTypeWindow As Integer = GetDlgItem(parent, stc2)
                Dim aboveRect As RECT = New RECT
                GetWindowRect(fileTypeWindow, aboveRect)
                Dim point As POINT = New POINT
                point.X = aboveRect.Left
                point.Y = aboveRect.Bottom
                ScreenToClient(parent, point)
                'Label for encoding
                Dim Label As New Label
                Label.Text = "&Kódování:"
                Label.Name = "mylabel"
                Dim labelHandle As Integer = Label.Handle
                SetParent(labelHandle, parent)
                Label.Left = point.X
                Label.Top = point.Y + 12
                Label.Width = 200
                SetWindowText(labelHandle, "&Encoding:")
                Dim fontHandle As Integer = SendMessage(fileTypeWindow, WM_GETFONT, 0, 0)
                SendMessage(labelHandle, WM_SETFONT, fontHandle, 0)
                'Position of encoding combo
                Dim fileComboWindow As Integer = GetDlgItem(parent, cmb1)
                aboveRect = New RECT
                GetWindowRect(fileComboWindow, aboveRect)
                point = New POINT
                point.X = aboveRect.Left
                point.Y = aboveRect.Bottom
                ScreenToClient(parent, point)
                Dim rightPoint As POINT = New POINT
                rightPoint.X = aboveRect.Right
                rightPoint.Y = aboveRect.Top
                ScreenToClient(parent, rightPoint)
                'Encoding combo
                Dim comboHandle As Integer = EncS.Handle
                SetParent(comboHandle, parent)
                EncS.Left = point.X
                EncS.Top = point.Y + 8
                EncS.Width = rightPoint.X - point.X
                FilterEncodings(EncS)
                SendMessage(comboHandle, WM_SETFONT, fontHandle, 0)
                'Encoding button
                Me.m_ButtonHandle = Button.Handle
                Button.AutoSize = True
                Button.Text = "všechna"
                Button.AutoSizeMode = AutoSizeMode.GrowAndShrink
                Button.PerformLayout()
                SetParent(m_ButtonHandle, parent)
                SendMessage(m_ButtonHandle, WM_SETFONT, fontHandle, 0)
                Button.Left = EncS.Left + EncS.Width
                Button.Top = (EncS.Top + EncS.Height + EncS.Top) / 2 - Button.Height / 2
                'Additional control
                If Additional IsNot Nothing Then
                    OldParent = GetParent(Additional.Handle)
                    OldVisible = Additional.Visible
                    SetParent(Additional.Handle, parent)
                    Additional.Visible = True
                    Additional.Left = Label.Left
                    Additional.Top = EncS.Top + EncS.Height
                    Additional.Width = EncS.Right - Additional.Left
                    Dim DlgRect As RECT
                    GetWindowRect(parent, DlgRect)
                    MoveWindow(parent, DlgRect.Left, DlgRect.Top, DlgRect.Right - DlgRect.Left, DlgRect.Bottom - DlgRect.Top + Additional.Height, True)
                End If
                'Enlarge PlacesBar
                Dim PlBar As Integer = GetDlgItem(parent, PlacesBar)
                If PlBar <> 0 Then
                    GetWindowRect(parent, cr)
                    Dim PRect As RECT
                    GetWindowRect(PlBar, PRect)
                    Dim PrectTL As POINT
                    PrectTL.Y = PRect.Top
                    PrectTL.X = PRect.Left
                    Dim PrectBR As POINT
                    PrectBR.Y = PRect.Bottom
                    PrectBR.X = PRect.Right
                    ScreenToClient(parent, PrectTL)
                    ScreenToClient(parent, PrectBR)
                    Dim NewHeight As Integer = (cr.Bottom - cr.Top) - PrectTL.Y
                    If Additional IsNot Nothing Then
                        NewHeight -= (cr.Bottom - cr.Top) - Additional.Bottom
                    Else
                        NewHeight -= (cr.Bottom - cr.Top) - EncS.Bottom
                    End If
                    MoveWindow(PlBar, PrectTL.X, PrectTL.Y, PrectBR.X - PrectTL.X, NewHeight, True)
                End If

                'remember the handles of the controls we have created so we can destroy them after
                m_LabelHandle = labelHandle
                m_ComboHandle = comboHandle
            Case WM_DESTROY
                'destroy the handles we have created
                If (m_ComboHandle <> 0) Then _
                    DestroyWindow(m_ComboHandle)
                If (m_LabelHandle <> 0) Then _
                    DestroyWindow(m_LabelHandle)
                If (m_ButtonHandle <> 0) Then _
                    DestroyWindow(m_ButtonHandle)
                Additional.Visible = OldVisible
                SetParent(Additional.Handle, OldParent)
            Case WM_NOTIFY
                Dim nmhdr As NMHDR = CType(Marshal.PtrToStructure(New IntPtr(lParam), GetType(NMHDR)), NMHDR)
                Select Case nmhdr.Code
                    Case CDN_FILEOK '-606
                        If Me.ignoreSecondFileOkNotification Then
                            If (Me.okNotificationCount <> 0) Then
                                Exit Select
                            End If
                            Me.okNotificationCount = 1
                        End If

                        Dim ea As New CancelEventArgs(False)
                        RaiseEvent OK(Me, ea)
                        If ea.Cancel Then
                            SetWindowLong(hdlg, DWL_MSGRESULT, 1L)
                            Return 1
                        Else
                            SetWindowLong(hdlg, DWL_MSGRESULT, 0L)
                        End If
                    Case CDN_SHAREVIOLATION '-604
                        Me.ignoreSecondFileOkNotification = True
                        Me.okNotificationCount = 0
                    Case CDN_SELCHANGE '-602
                        Me.ignoreSecondFileOkNotification = False
                    Case CDN_INITDONE '-601
                End Select
        End Select
        Return 0
    End Function
    Private ignoreSecondFileOkNotification As Boolean = False
    Private okNotificationCount As Integer = 0

    Public Event OK As CancelEventHandler

    Friend Shared Sub FilterEncodings(ByVal EncS As EncodingSelector)
        EncS.RefreshEncodings()
        'Dim Elist As New List(Of System.Text.EncodingInfo)(EncS.Encodings)
        'For Each Encoding As System.Text.EncodingInfo In Elist
        '    Dim Found As Boolean = False
        '    For Each PreName As String In My.Settings.PreferredEncodings
        '        If Encoding.Name = PreName Then Found = True : Exit For
        '    Next PreName
        '    If Not Found AndAlso EncS.SelectedCodepage <> Encoding.CodePage Then _
        '        EncS.RemoveEncoding(Encoding)
        'Next Encoding
    End Sub
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="DialogTitle"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private _DialoGtitle$ = "Otevøít"
    ''' <summary>Nadpis dialogu</summary>
    <DefaultValue("Otevøít"), Tools.ComponentModelT.KnownCategory(Tools.ComponentModelT.KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Dialog title")> _
    Public Property DialogTitle$()
        Get
            Return Me._DialoGtitle
        End Get
        Set(ByVal value$)
            Me._DialoGtitle = value
        End Set
    End Property

    Public Function ShowDialog(Optional ByVal Owner As Form = Nothing) As DialogResult
        If Owner Is Nothing Then Owner = Form.ActiveForm
        'set up the struct and populate it
        Dim ofn As OPENFILENAME = New OPENFILENAME
        ofn.lStructSize = Marshal.SizeOf(ofn)
        ofn.lpstrFilter = (m_Filter.Replace("|"c, vbNullChar) + vbNullChar)
        ofn.lpstrFile = (m_FileName + New String(" "c, 512))
        ofn.nMaxFile = ofn.lpstrFile.Length
        ofn.lpstrFileTitle = (System.IO.Path.GetFileName(m_FileName) + New String(" "c, 512))
        ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length
        ofn.lpstrTitle = Me.DialogTitle ' "Otevøít"
        ofn.lpstrDefExt = m_DefaultExt
        'position the dialog above the active window
        ofn.hwndOwner = Owner.Handle
        'we need to find out the active screen so the dialog box is
        'centred on the correct display
        m_ActiveScreen = Screen.FromControl(Owner)
        'set up some sensible flags
        If DialogType = DialogTypes.Save Then
            ofn.Flags = OFN_EXPLORER _
                        Or OFN_PATHMUSTEXIST _
                        Or OFN_NOTESTFILECREATE _
                        Or OFN_ENABLEHOOK _
                        Or OFN_HIDEREADONLY Or OFN_OVERWRITEPROMPT _
                        Or OFN_HIDEREADONLY
        Else 'Open
            ofn.Flags = OFN_EXPLORER _
                        Or OFN_PATHMUSTEXIST _
                        Or OFN_FILEMUSTEXIST _
                        Or OFN_ENABLEHOOK Or _
                        OFN_HIDEREADONLY
        End If
        'this is where the hook is set. Note that we can use a C# delegate in place of a C function pointer
        ofn.lpfnHook = New OFNHookProcDelegate(AddressOf HookProc)
        'if we're running on Windows 98/ME then the struct is smaller
        If (System.Environment.OSVersion.Platform <> PlatformID.Win32NT) Then
            ofn.lStructSize = (ofn.lStructSize - 12)
        End If
        'show the dialog

        Dim dFunc As dDialogFunction
        If DialogType = DialogTypes.Save Then
            dFunc = AddressOf GetSaveFileName
        Else
            dFunc = AddressOf GetOpenFileName
        End If

        Try
            If Not dFunc.Invoke(ofn) Then
                Dim ret As Integer = CommDlgExtendedError
                If (ret <> 0) Then
                    Throw New ApplicationException(("Couldn't show file open dialog - " + ret.ToString))
                End If
                Return DialogResult.Cancel
            End If
            m_FileName = ofn.lpstrFile
            Return DialogResult.OK
        Finally
            If Owner IsNot Nothing Then Owner.Activate()
        End Try
    End Function

    Private Delegate Function dDialogFunction(ByRef ofn As OPENFILENAME) As Boolean

    Public Delegate Function OFNHookProcDelegate(ByVal hdlg As Integer, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

#Region "Structures"

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Private Structure OPENFILENAME
        Public lStructSize As Integer
        Public hwndOwner As IntPtr
        Public hInstance As Integer
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpstrFilter As String
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpstrCustomFilter As String
        Public nMaxCustFilter As Integer
        Public nFilterIndex As Integer
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpstrFile As String
        Public nMaxFile As Integer
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpstrFileTitle As String
        Public nMaxFileTitle As Integer
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpstrInitialDir As String
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpstrTitle As String
        Public Flags As Integer
        Public nFileOffset As Short
        Public nFileExtension As Short
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpstrDefExt As String
        Public lCustData As Integer
        Public lpfnHook As OFNHookProcDelegate
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpTemplateName As String
        'only if on nt 5.0 or higher
        Public pvReserved As Integer
        Public dwReserved As Integer
        Public FlagsEx As Integer
    End Structure

    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    Private Structure POINT
        Public X As Integer
        Public Y As Integer
    End Structure

    Private Structure NMHDR
        Public HwndFrom As Integer
        Public IdFrom As Integer
        Public Code As Integer
    End Structure
#End Region

    Private Sub Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button.Click
        EncS.RefreshEncodings()
        Button.Visible = False
    End Sub
End Class
