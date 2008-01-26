Imports System.ComponentModel

#If Framework >= 3.5 Then
Namespace WindowsT.FormsT
    'ASAP:  Wiki, 
    ''' <summary>Provides common base for implementing Windows screensawers</summary>
    <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ScreenSaverBase))> _
    Public MustInherit Class ScreenSaverBase
        ''' <summary>Runs a screensaver</summary>
        ''' <param name="cmd">Application command line arguments (excluding executable name)</param>
        ''' <remarks>Use this method as only think called by application. Implemented screen saver will take care about everything else. Alternatively you can use <see cref="Run(Of T)"/> method.</remarks>
        Public Sub Run(ByVal ParamArray cmd As String())
            If cmd Is Nothing OrElse cmd.Length = 0 OrElse (cmd.Length >= 1 AndAlso cmd(0) = "/s") Then
                RunScreenSaver()
            ElseIf cmd.Length >= 1 AndAlso cmd(0).StartsWith("/c") Then
                Dim handle As Integer = 0
                If cmd(0).Length > 2 Then
                    Try : handle = cmd(0).Substring(2) : Catch : End Try
                End If
                If handle <> 0 Then
                    SettingsForm.ShowDialog()
                Else
                    SettingsForm.ShowDialog(New W32(handle)) 'TODO: DOes not work
                End If
            ElseIf cmd.Length >= 2 AndAlso cmd(0) = "/p" Then
                Try : RunPreviewLow(cmd(1)) : Catch : End Try
            End If
        End Sub
        ''' <summary>Simply implements <see cref="IWin32Window"/></summary>
        Private Class W32 : Implements IWin32Window
            ''' <summary>Contains value of the <see cref="Handle"/> property</summary>
            Private ReadOnly hWnd As IntPtr
            ''' <summary>CTor</summary>
            ''' <param name="hWnd">Handle</param>
            Public Sub New(ByVal hWnd As IntPtr)
                Me.hWnd = hWnd
            End Sub
            ''' <summary>Gets the handle to the window represented by the implementer.</summary>
            ''' <returns>A handle to the window represented by the implementer.</returns>
            Public ReadOnly Property Handle() As System.IntPtr Implements System.Windows.Forms.IWin32Window.Handle
                Get
                    Return hWnd
                End Get
            End Property
        End Class
        ''' <summary>Creates new instance of ScreenSaver implementation and runs it</summary>
        ''' <param name="cmd">Application command line arguments (excluding executable name)</param>
        ''' <typeparam name="T">Implementation of ScreenSaverBase</typeparam>
        ''' <remarks>Use this method as only think called by application. Implemented screen saver will take care about everything else. Alternatively you can use <see cref="Run"/> method of new instance of screen saver implementation.</remarks>
        Public Shared Sub Run(Of T As {New, ScreenSaverBase})(ByVal ParamArray cmd As String())
            Dim instance As New T
            instance.Run(cmd)
        End Sub
#Region "Default implementation - ScreenSaver"
        '''' <summary>For each display at system gets one form</summary>
        '''' <returns>Form for each screen</returns>
        '''' <remarks>
        '''' As many forms as computer has displays.
        '''' This method cannot be overriden, but you can override methods called by this one.
        '''' <see cref="GetScreenForm"/>, <see cref="FormatScreenForm"/> and <see cref="HookFormEvents"/>
        '''' Methods are called in following order:
        '''' <see cref="GetScreenForm"/>,
        '''' <see cref="M:Tools.WindowsT.FormsT.FormatScreenForm(System.Windows.Forms.Form)"/>,
        '''' <see cref="M:Tools.WindowsT.FormsT.FormatScreenForm(System.Windows.Forms.Form,System.Windows.Forms.Screen)"/>,
        '''' <see cref="HookFormEvents"/>
        '''' </remarks>
        'Protected Function GetFormForEachScreen() As Form()
        '    Dim Forms(Screen.AllScreens.Length - 1) As Form
        '    For i = 0 To Screen.AllScreens.Length - 1
        '        Forms(i) = GetScreenForm(Screen.AllScreens(i))
        '        FormatScreenForm(Forms(i))
        '        FormatScreenForm(Forms(i), Screen.AllScreens(i))
        '        HookFormEvents(Forms(i))
        '    Next i
        '    Return Forms
        'End Function
        ''' <summary>Creates instance of form for one screen. Called by default implementation of <see cref="RunScreenSaver"/> as 1st method.</summary>
        ''' <param name="Screen">Screen form is created for. Can be null if form is created for preview mode.</param>
        ''' <returns>Default implementation returns new unmodified instance of <see cref="Form"/></returns>
        ''' <remarks>
        ''' You should override this method if you do not <see cref="RunScreenSaver"/>.
        ''' The purpose of this method is to create instance of form for one screen. You can completely ignore the <paramref name="Screen"/> parameter. You should not use the <paramref name="Screen"/> parameter for sizing and positioning your form - it is done in <see cref="M:Tools.WindowsT.FormsT.FormatScreenForm(System.Windows.Forms.Form,System.Windows.Forms.Screen)"/>.
        ''' </remarks>
        Protected Overridable Function GetScreenForm(ByVal Screen As Screen) As Form
            Return New Form
        End Function
        ''' <summary>Perform screen-independent setup of form.  Called by default implementation of <see cref="RunScreenSaver"/> as 2nd method.</summary>
        ''' <param name="Form">Form to setup.</param>
        ''' <remarks>
        ''' If you override <see cref="GetScreenForm"/> you can perform such setup also there. In this case this method should be overriden with do-nothing method.
        ''' Default implementation sets <see cref="Form.BackColor"/> to <see cref="Color.Black"/>,
        ''' <see cref="Form.FormBorderStyle"/> to <see cref="FormBorderStyle.None"/>,
        ''' <see cref="Form.TopMost"/> to <c>True</c> and 
        ''' <see cref="Form.ShowInTaskbar"/> to <c>False</c>.
        ''' </remarks>
        Protected Overridable Sub FormatScreenForm(ByVal Form As Form)
            With Form
                .BackColor = Color.Black
                .FormBorderStyle = FormBorderStyle.None
                .TopMost = True
                .ShowInTaskbar = False
            End With
        End Sub
        ''' <summary>Performs screen-aware setup of form. Called by default implementation of <see cref="RunScreenSaver"/> as 3rd method.</summary>
        ''' <param name="Form">Form to perform setup on</param>
        ''' <param name="Screen">Screen to perform setup for</param>
        ''' <remarks>Default implementation makes <paramref name="Form"/> completely covering <paramref name="Screen"/>.</remarks>
        Protected Overridable Sub FormatScreenForm(ByVal Form As Form, ByVal Screen As Screen)
            With Form
                .Width = Screen.Bounds.Width
                .Height = Screen.Bounds.Height
                .Top = Screen.Bounds.Top
                .Left = Screen.Bounds.Left
            End With
        End Sub
        ''' <summary>Hooks events for form created by <see cref="GetScreenForm"/>. Called by default implementation of <see cref="RunScreenSaver"/> as 4th method.</summary>
        ''' <param name="Form">Form to hook eventf for</param>
        ''' <remarks>Default implementation hooks <see cref="OnAcion"/> to <see cref="Form.MouseDown"/> and <see cref="Form.KeyDown"/></remarks>
        Protected Overridable Sub HookFormEvents(ByVal Form As Form)
            AddHandler Form.MouseDown, AddressOf OnAcion
            AddHandler Form.KeyDown, AddressOf OnAcion
        End Sub
        ''' <summary>In case you use default implementation fo <see cref="HookFormEvents"/> this method is called for <see cref="Form.MouseDown"/> and <see cref="Form.KeyDown"/> of each screen form.</summary>
        ''' <param name="sender">For that causes the event</param>
        ''' <param name="e">Event parameters</param>
        ''' <remarks>Default implementation closes all forms containded in <see cref="RunningForms"/> (in thread-safe way)</remarks>
        Protected Overridable Sub OnAcion(ByVal sender As Form, ByVal e As EventArgs)
            For Each f In RunningForms
                f.Invoke(New Action(AddressOf f.Close))
            Next f
        End Sub
        ''' <summary>Contains value of the <see cref="RunningForms"/> property</summary>
        Private ReadOnly _RunningForms As New List(Of Form)
        ''' <summary>When screen-saver is running should contain all the forms that performs screen-saving</summary>
        Protected ReadOnly Property RunningForms() As List(Of Form)
            Get
                Return _RunningForms
            End Get
        End Property
        ''' <summary>Runs the screen saver</summary>
        ''' <remarks>Default implementation calls <see cref="RunScreenSaverOnScreen"/> for each screen in single thread.</remarks>
        Protected Overridable Sub RunScreenSaver()
            Dim Threads As New List(Of Threading.Thread)
            For Each Screen As Screen In Screen.AllScreens
                Dim t As New Threading.Thread(AddressOf RunScreenSaverOnScreen)
                t.Name = "Screen thread"
                t.TrySetApartmentState(ThreadingApartment)
                Threads.Add(t)
                t.Start(Screen)
            Next Screen
            For Each t In Threads
                t.Join()
            Next t
        End Sub
        ''' <summary>Gets <see cref="Threading.ApartmentState"/> used by screen threads</summary>
        ''' <returns>Default implementation returns <see cref="Threading.ApartmentState.MTA"/></returns>
        ''' <remarks>Override this property if you want to work with COM objects and return <see cref="Threading.ApartmentState.STA"/></remarks>
        Protected Overridable ReadOnly Property ThreadingApartment() As Threading.ApartmentState
            Get
                Return Threading.ApartmentState.MTA
            End Get
        End Property
        Protected Overridable Sub RunScreenSaverOnScreen(ByVal Screen As Screen)
            Dim F As Form
            SyncLock RunningForms
                F = GetScreenForm(Screen)
                FormatScreenForm(F)
                FormatScreenForm(F, Screen)
                HookFormEvents(F)
                RunningForms.Add(F)
            End SyncLock
            Application.Run(F)
        End Sub

#End Region
#Region "Default implementation - Preview"
        ''' <summary>Low-level implementation of preview</summary>
        ''' <param name="hWnd">Handle of control to display preview in</param>
        ''' <remarks>Default implementation creates new panel and places it into window with handle <paramref name="hWnd"/>. If it succedes calls <see cref="RunPreview"/>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Sub RunPreviewLow(ByVal hWnd As Integer)
            Dim RECT As API.RECT
            If API.GetWindowRect(hWnd, RECT) Then
                Dim P As New Panel
                If API.SetParent(P.Handle, hWnd) Then
                    P.Left = 0 : P.Top = 0
                    P.Height = RECT.Bottom - RECT.Top
                    P.Width = RECT.Right - RECT.Left
                    P.BackColor = Color.Black
                    RunPreview(P)
                End If
            End If
        End Sub
        ''' <summary>Runs preview of screen saver</summary>
        ''' <param name="Target">Control to display preview in</param>
        ''' <remarks>
        ''' Default implementation creates screen form using <see cref="GetScreenForm"/> (with null argument), 
        ''' performs it's screen-independent setup using <see cref="M:Tools.WindowsT.FormsT.ScreenSaverBase.FormatScreenForm(System.Windows.Forms.Form)"/>,
        ''' sets <see cref="Form.TopLevel"/> to <c>False</c> and <see cref="Form.Parent"/> to <paramref name="Target"/>,
        ''' performs forms's screen-aware setup using <see cref="FormatPreviewForm"/>,
        ''' calls <see cref="HookPreviewFormEvents"/>
        ''' adds form into <see cref="RunningForms"/>, 
        ''' and calls <see cref="Application.Run"/> on that form 
        ''' </remarks>
        Protected Overridable Sub RunPreview(ByVal Target As Control)
            Dim Form = GetScreenForm(Nothing)
            FormatScreenForm(Form)
            Form.TopLevel = False
            Form.Parent = Target
            FormatPreviewForm(Form)
            HookPreviewFormEvents(Form)
            RunningForms.Add(Form)
            Application.Run(Form)
        End Sub
        ''' <summary>Performs specific seeting of screen form for displaying it in preview mode</summary>
        ''' <param name="Form">Form to setup. It is already placed in container.</param>
        ''' <remarks>Default implementation sets size and position fo form to fit in its parent.
        ''' To get control in which form is placed, see <see cref="Form.Parent"/>.</remarks>
        Protected Overridable Sub FormatPreviewForm(ByVal Form As Form)
            With Form
                .Size = .Parent.ClientSize
                .Left = 0
                .Top = 0
            End With
        End Sub
        ''' <summary>Allows to hook for events of screen form used for preview</summary>
        ''' <param name="Form">Form to hook on</param>
        ''' <remarks>Default implementation does nothing</remarks>
        Protected Overridable Sub HookPreviewFormEvents(ByVal Form As Form)
        End Sub
#End Region
        ''' <summary>If implemented in derived class gets instance of form used to setup the screensaver</summary>
        Protected MustOverride ReadOnly Property SettingsForm() As Form
    End Class
End Namespace
#End If
