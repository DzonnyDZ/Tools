'#If Config <= RC Then Stage is set in vbproj
Imports Tools.MathT
''' <summary>Tests <see cref="Tools.WindowsT.FormsT.LinkLabel"/></summary>
Friend Class frmLEBE
    ''' <summary>Show test form</summary>
    Public Shared Sub Test()
        Dim frm As New frmLEBE
        frm.ShowDialog()
    End Sub
    ''' <summary>CTor</summary>
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = Tools.ResourcesT.ToolsIcon
    End Sub


    Private Sub cmdS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdS.Click
        Dim N As Short = txtNumber.Text
        Dim N2 As Short = LEBE(N)
        View(N, N2, Int16.MaxValue.ToString("D").Length, Int16.MaxValue.ToString("X").Length)
    End Sub
    Private Sub View(ByVal N As IFormattable, ByVal N2 As IFormattable, ByVal D As Byte, ByVal H As Byte)
        Dim DecF As String = String.Format(" {0};{0}", New String("0"c, D))
        lblSrcDec.Text = N.ToString(DecF, Nothing)
        lblSrcHex.Text = N.ToString("X", Nothing)
        lblResDec.Text = N2.ToString(DecF, Nothing)
        lblResHex.Text = N2.ToString("X", Nothing)
        lblSrcHex.Text = New String("0"c, H - lblSrcHex.Text.Length) & lblSrcHex.Text
        lblResHex.Text = New String("0"c, H - lblResHex.Text.Length) & lblResHex.Text
    End Sub

    Private Sub cmdUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUS.Click
        Dim N As UShort = txtNumber.Text
        Dim N2 As UShort = LEBE(N)
        View(N, N2, UInt16.MaxValue.ToString("D").Length, UInt16.MaxValue.ToString("X").Length)
    End Sub

    Private Sub cmdI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdI.Click
        Dim N As Integer = txtNumber.Text
        Dim N2 As Integer = LEBE(N)
        View(N, N2, Int32.MaxValue.ToString("D").Length, Int32.MaxValue.ToString("X").Length)
    End Sub

    Private Sub cmdUI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUI.Click
        Dim N As UInteger = txtNumber.Text
        Dim N2 As UInteger = LEBE(N)
        View(N, N2, UInt32.MaxValue.ToString("D").Length, UInt32.MaxValue.ToString("X").Length)
    End Sub

    Private Sub cmdL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdL.Click
        Dim N As Long = txtNumber.Text
        Dim N2 As Long = LEBE(N)
        View(N, N2, Int64.MaxValue.ToString("D").Length, Int64.MaxValue.ToString("X").Length)
    End Sub

    Private Sub cmdUL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUL.Click
        Dim N As ULong = txtNumber.Text
        Dim N2 As ULong = LEBE(N)
        View(N, N2, UInt64.MaxValue.ToString("D").Length, UInt64.MaxValue.ToString("X").Length)
    End Sub
End Class
'#End If