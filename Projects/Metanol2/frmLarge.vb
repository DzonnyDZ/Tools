''' <summary>Big preview form</summary>
Public Class frmLarge
    '''' <summary>Shows image form given folder</summary>
    'Public Sub LoadImage(ByVal Path As String)
    '    Try
    '        Me.BackgroundImage = New Bitmap(Path)
    '    Catch ex As Exception
    '        Me.BackgroundImage = Nothing
    '    End Try
    'End Sub

    Private Sub frmLarge_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.LargePosition = Me.Location
        My.Settings.LargeSize = Me.Size
        My.Settings.LargeState = Me.WindowState
        My.Settings.LargeFullScreen = FullScreen
    End Sub

    Private Sub frmLarge_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyData
            Case Keys.Left : frmMain.GoPrevious()
            Case Keys.Right : frmMain.GoNext()
            Case Keys.Enter : FullScreen = Not FullScreen
            Case Keys.Escape : Me.Close() : Me.frmMain.Activate()
            Case Keys.L : RotateLeft()
            Case Keys.R : RotateRight()
        End Select
    End Sub
    ''' <summary>Rotates image left 90°</summary>
    Public Sub RotateLeft()
        Me.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone)
        Me.Invalidate()
        RaiseEvent ImageAltered(Me)
    End Sub
    ''' <summary>Rotates image right 90°</summary>
    Public Sub RotateRight()
        Me.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Me.Invalidate()
        RaiseEvent ImageAltered(Me)
    End Sub
    ''' <summary>Raised when image was altered (i.e. rotated)</summary>
    ''' <param name="sender">Source of the event</param>
    Public Event ImageAltered(ByVal sender As frmLarge)


    Private Sub frmLarge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = My.Settings.LargePosition
        Me.Size = My.Settings.LargeSize
        Me.WindowState = My.Settings.LargeState
        Me.FullScreen = My.Settings.LargeFullScreen
        If Me.Owner IsNot Nothing Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
            Me.MinimizeBox = False
            Me.MaximizeBox = False
            Me.ShowInTaskbar = False
            Me.ShowIcon = False
        End If
    End Sub
    ''' <summary>Contains value of the <see cref="FullScreen"/> property</summary>
    Private _FullScreen As Boolean = False
    Public Property FullScreen() As Boolean
        Get
            Return _FullScreen
        End Get
        Set(ByVal value As Boolean)
            If value <> FullScreen Then
                Dim MyScreen = Screen.FromControl(Me)
                If value Then
                    Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                    Me.WindowState = FormWindowState.Maximized
                    Using Me32 As New WindowsT.NativeT.Win32Window(Me)
                        Me32.Area = MyScreen.Bounds 'TODO: Why I'm unable to do this via managed code???
                    End Using
                Else
                    Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                    Me.WindowState = FormWindowState.Maximized
                End If
            End If
            _FullScreen = value
        End Set
    End Property
    ''' <summary>Instance of <see cref="frmMain"/> which owns this instance</summary>
    Private WithEvents frmMain As frmMain
    ''' <summary>CTor</summary>
    ''' <param name="MainForm">Instance of <see cref="frmMain"/> which will own this instance - this is not owner for <see cref="Show"/>!</param>
    ''' <exception cref="ArgumentNullException"><paramref name="MainForm"/> is null</exception>
    Friend Sub New(ByVal MainForm As frmMain)
        If MainForm Is Nothing Then Throw New ArgumentNullException("MainForm")
        InitializeComponent()
        Me.frmMain = MainForm
    End Sub
    ''' <summary>Sets path of current image displayed to user</summary>
    ''' <remarks>When value being set is null or <see cref="System.String.Empty"/> default text is displayed. When value being set is a valid file path only file name is used, otherwise whole value is displayed.</remarks>
    Public WriteOnly Property ImagePath$()
        Set(ByVal value$)
            If value = "" Then
                Me.Text = My.Resources.Preview
            Else
                Try
                    Me.Text = IO.Path.GetFileName(value)
                Catch
                    Me.Text = value
                End Try
            End If
        End Set
    End Property

    Private Sub tmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiClose.Click
        Me.Close()
    End Sub

    Private Sub tmiFullScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiFullScreen.Click
        Me.FullScreen = Not Me.FullScreen
    End Sub

    Private Sub tmiPreviousImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiPreviousImage.Click
        Me.frmMain.GoPrevious()
    End Sub

    Private Sub tmiNextImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiNextImage.Click
        Me.frmMain.GoNext()
    End Sub
    ''' <summary>Gets or sets currently displayed image</summary>
    ''' <remarks>Use this property instead of <see cref="BackgroundImage"/> because way of displaying images may change in the future.</remarks>
    Public Property Image() As Image
        Get
            Return Me.BackgroundImage
        End Get
        Set(ByVal value As Image)
            Me.BackgroundImage = value
        End Set
    End Property

    Private Sub tmiRoL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiRoL.Click
        RotateLeft()
    End Sub

    Private Sub rmiRoR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiRoR.Click
        RotateRight()
    End Sub
End Class