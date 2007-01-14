Imports System.Globalization, System.Threading
Imports Tools.VisualBasic
Imports System.ComponentModel
''' <summary>Hlavní okno aplikace</summary>
Public Class frmMain
    ''' <summary>Hodnota názvu pro soubor bez názvu</summary>
    Const BezNázvuFileName As String = "(bez názvu)"
    ''' <summary>Název XML tagu equas</summary>
    Const XMLEquas As String = "equas"
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="Changed"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Changed As Boolean = False
    ''' <summary>Sezma aktuálně načtených rovnic</summary>
    Private Equas As New List(Of Equation)
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="CurrentEqua"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _CurrentFile As String = ""
    Private CurrentEqua As ToolStripButton

    ''' <summary>Nastavuje/zjišˇuje cestu k aktuálnímu souboru</summary>
    ''' <remarks>Může být "" pro soubor bez názvu</remarks>
    Public Property CurrentFile() As String
        <DebuggerStepThrough()> Get
            Return _CurrentFile
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
            _CurrentFile = value
            _Changed = False
            ShowFileName()
        End Set
    End Property
    ''' <summary>Zjišťuje / nastavuje jestli byl aktuální soubor změněn</summary>
    Public Property Changed() As Boolean
        <DebuggerStepThrough()> Get
            Return _Changed
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Boolean)
            _Changed = value
            ShowFileName()
        End Set
    End Property
    ''' <summary>CTor</summary>
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        tosRovnice.Items.Clear()
        ShowFileName()
        Me.Text = My.Application.Info.Title & " " & My.Application.Info.Version.ToString
    End Sub
    ''' <summary>Zobrazí název souboru v titulku okna</summary>
    Private Sub ShowFileName()
        If CurrentFile = "" Then
            Me.Text = my.Application.Info.Title & " "& BezNázvuFileName
        Else
            Me.Text = My.Application.Info.Title & " " & IO.Path.GetFileName(CurrentFile)
        End If
        If Changed Then Me.Text &= "*"
    End Sub

    Private Sub tmiKonec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiKonec.Click
        Me.Close()
    End Sub

    Private Sub equa_Click(ByVal sender As Object, ByVal e As [EventArgs])
        CurrentEqua = sender
        For Each itm As ToolStripButton In tosRovnice.Items
            If itm Is sender Then
                itm.Checked = True
            Else
                itm.Checked = False
            End If
        Next itm
        ShowEqua()
    End Sub
    ''' <summary>Zobrazí informace o vybrané rovnici</summary>
    Private Sub ShowEqua()
        RCEEnabled = True
        With CType(CurrentEqua.Tag, Equation)
            tstNázev.Text = .Name
            tstDx.Text = .Dx
            tstDy.Text = .Dy
            tstPPx.Text = .StartX
            tstPPy.Text = .StartY
            tstXmax.Text = .MaxX
            tstYmax.Text = .MaxY
            tstYmin.Text = .MinY
            tstXmin.Text = .MinX
            tstTmin.Text = .Tmin
            tstTmax.Text = .Tmax
            tstΔt.Text = .Δt
        End With
    End Sub
    ''' <summary>Zobrazí seznam rovnic</summary>
    Private Sub ShowAllEquas()
        tosRovnice.Items.Clear()
        For Each rce As Equation In Equas
            tosRovnice.Items(tosRovnice.Items.Add( _
                    New ToolStripButton(rce.ToString, Nothing, AddressOf equa_Click) _
                    )).Tag = rce
        Next rce
    End Sub

    Private Sub tsbAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbAdd.Click
        Dim equa As New Equation
        equa.Name = "Equa " & (tosRovnice.Items.Count + 1)
        Equas.Add(equa)
        Changed = True
        Dim itm As New ToolStripButton(equa.ToString, Nothing, AddressOf equa_Click)
        itm.Tag = equa
        tosRovnice.Items.Add(itm)
        equa_Click(itm, New EventArgs())
    End Sub
    ''' <summary>Nastavuje vlastnost Enabled prvků u nichž se tato vlastnost mění pokud je/není vybrána rovnice</summary>
    Private WriteOnly Property RCEEnabled() As Boolean
        Set(ByVal value As Boolean)
            tsbDel.Enabled = value
            tmiVykreslit.Enabled = value
            tosMěřítko.Enabled = value
            tosPodmínky.Enabled = value
            tosZadání.Enabled = value
            picMain.Enabled = value
        End Set
    End Property
    ''' <summary>Uloží soubor</summary>
    ''' <returns>True pokud uživatel operaci nestornoval</returns>
    Private Function Save() As Boolean
        If CurrentFile = "" Then
            Return SaveAs()
        Else
            If Save(CurrentFile) Then
                Changed = False
                Return True
            Else
                Return SaveAs()
            End If
        End If
    End Function
    ''' <summary>Uloží sobor pod zvoleným názvem</summary>
    ''' <returns>True pokud uživatel operaci nestornoval</returns>
    Private Function SaveAs() As Boolean
        If sfdSave.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Save(sfdSave.FileName) Then
                CurrentFile = sfdSave.FileName
                Return True
            Else
                Return SaveAs
            End If
        Else
            Return False
        End If
    End Function
    ''' <summary>Uloží soubor pod daným názvel</summary>
    ''' <param name="FileName">Cesta k souboru</param>
    ''' <returns>True pokud se uložení podařilo</returns>
    Private Function Save(ByVal FileName As String) As Boolean
        Dim doc As New Xml.XmlDocument
        doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", ""))
        Dim EquasNode As Xml.XmlElement = doc.AppendChild(doc.CreateElement(XMLEquas))
        For Each equa As Equation In Equas
            EquasNode.AppendChild(equa.ToXml(doc))
        Next equa
        Try
            doc.Save(FileName)
        Catch ex As Exception
            MsgBox("Soubor " & FileName & "se nepodařilo uložit:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Chyba")
            Return False
        End Try
        Changed = False
        Return True
    End Function
    ''' <summary>Zeptá se jestli chce uživatel uložit změny (pokud byly nějaké provedeny)</summary>
    ''' <returns>True, pokud uživatel operaci nestornoval</returns>
    Private Function AskChanged() As Boolean
        If Not Changed Then
            Return True
        Else
            Select Case MsgBox("Obsah souboru " & iif(CurrentFile = "", BezNázvuFileName, IO.Path.GetFileName(CurrentFile)) & " byl změněn. Chcete jej nyní uložit?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, "Uložit změny")
                Case MsgBoxResult.Yes
                    Return Save()
                Case MsgBoxResult.No
                    Return True
                Case MsgBoxResult.Cancel
                    Return False
                Case Else
                    Return False
            End Select
        End If
    End Function

    Private Sub mniOtevřít_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniOtevřít.Click
        If AskChanged() Then
            If ofdOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim lst As New List(Of Equation)
                Try
                    Dim doc As New Xml.XmlDocument
                    doc.Load(ofdOpen.FileName)
                    For Each node As Xml.XmlNode In CType(doc.GetElementsByTagName(XMLEquas)(0), Xml.XmlElement).GetElementsByTagName(Equation.XMLEqua)
                        lst.Add(New Equation(node))
                    Next node
                Catch ex As Exception
                    MsgBox("Chyba při otevírání souboru " & ofdOpen.FileName & ":" & ex.Message, MsgBoxStyle.Critical, "Chyba")
                    Return
                End Try
                Equas = lst
                CurrentFile = ofdOpen.FileName
                RCEEnabled = False
                ShowAllEquas()
            End If
        End If
    End Sub

    Private Sub mniUložit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniUložit.Click
        Save()
    End Sub

    Private Sub mniUložitJako_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniUložitJako.Click
        SaveAs()
    End Sub

    Private Sub mniNový_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniNový.Click
        If AskChanged() Then
            Equas = New List(Of Equation)
            CurrentFile = ""
            ShowAllEquas()
        End If
    End Sub

    Private Sub tsbDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbDel.Click
        For Each itm As ToolStripButton In tosRovnice.Items
            If itm.Checked Then
                tosRovnice.Items.Remove(itm)
                Equas.Remove(itm.Tag)
                Changed = True
                RCEEnabled = False
                Exit For
            End If
        Next itm
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not AskChanged()
    End Sub

    Private Sub tstNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles tstXmin.TextChanged, tstPPy.TextChanged, tstXmax.TextChanged, tstYmin.TextChanged, tstYmax.TextChanged, tstPPx.TextChanged, _
             tstNázev.TextChanged, tstDx.TextChanged, tstDy.TextChanged
        With CType(sender, ToolStripTextBox)
            .Tag = enmChState.Changed
        End With
    End Sub

    Private Sub tstNumber_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles tstXmin.Validating, tstPPy.Validating, tstXmax.Validating, tstYmin.Validating, tstYmax.Validating, tstPPx.Validating
        With CType(sender, ToolStripTextBox)
            Try
                Dim x As Single = CDbl(.Text)
            Catch ex As Exception
                e.Cancel = True
                MsgBox("Zadaná hodnota musí být číslo", MsgBoxStyle.Critical, "Chyba")
                .Tag = enmChState.NotValidated
                Return
            End Try
        End With
    End Sub

    Private Sub tstNumber_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles tstXmin.GotFocus, tstPPy.GotFocus, tstXmax.GotFocus, tstYmin.GotFocus, tstYmax.GotFocus, tstPPx.GotFocus, _
             tstNázev.GotFocus, tstDx.GotFocus, tstDy.GotFocus
        With CType(sender, ToolStripTextBox)
            If .Tag Is Nothing OrElse (TypeOf .Tag Is enmChState AndAlso CType(.Tag, enmChState) = enmChState.Changed) Then
                .Tag = enmChState.NotChanged
            ElseIf TypeOf .Tag Is enmChState AndAlso CType(.Tag, enmChState) = enmChState.NotValidated Then
                .Tag = enmChState.Changed
            Else
                .Tag = enmChState.Changed
            End If
        End With
    End Sub

    Private Sub tstNumber_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles tstXmin.Validated, tstPPy.Validated, tstXmax.Validated, tstYmin.Validated, tstYmax.Validated, tstPPx.Validated, _
             tstNázev.Validated, tstDx.Validated, tstDy.Validated
        With CType(sender, ToolStripTextBox)
            If (TypeOf .Tag Is enmChState AndAlso CType(.Tag, enmChState) = enmChState.Changed) OrElse Not TypeOf .Tag Is enmChState Then
                StoreTextBoxes()
            End If
        End With
    End Sub
    ''' <summary>Možné stavy textového pole</summary>
    Private Enum enmChState
        ''' <summary>Nedošlo ke změně</summary>
        NotChanged
        ''' <summary>Hodnota v pole nebyla validována</summary>
        NotValidated
        ''' <summary>Došlo ke změně</summary>
        Changed
    End Enum

    Private Sub tstString_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles tstNázev.Validating, tstDx.Validating, tstDy.Validating
        With CType(sender, ToolStripTextBox)
            If .Text = "" Then
                e.Cancel = True
                MsgBox("Text musí být zadán", MsgBoxStyle.Critical, "Chyba")
                .Tag = enmChState.NotValidated
            End If
        End With
    End Sub
    ''' <summary>Uloží hodnoty s textboxů k rovnici</summary>
    Private Sub StoreTextBoxes()
        Changed = True
        With CType(CurrentEqua.Tag, Equation)
            .Name = tstNázev.Text
            .Dx = tstDx.Text
            .Dy = tstDy.Text
            .StartX = tstPPx.Text
            .StartY = tstPPy.Text
            .MaxX = tstXmax.Text
            .MaxY = tstYmax.Text
            .MinY = tstYmin.Text
            .MinX = tstXmin.Text
            .Tmax = tstTmax.Text
            .Tmin = tstTmin.Text
            .Δt = tstΔt.Text
        End With
    End Sub

    Private Sub tmiAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiAbout.Click
        frmAbout.ShowDialog(Me)
    End Sub

    Private Sub tmiVykreslit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiVykreslit.Click
        Dim minX As Single = tstXmin.Text
        Dim miny As Single = tstYmin.Text
        Dim maxX As Single = tstXmax.Text
        Dim maxY As Single = tstYmax.Text
        Dim r As RectangleF
        r.X = minX
        r.Y = maxY
        r.Width = maxX - minX
        r.Height = -(maxY - miny)

        Dim dr As New Drawer( _
                r, _
                New PointF(tstPPx.Text, tstPPy.Text), _
                New SyntaktickyAnalyzator.Analyzer(tstDx.Text, New String() {"x", "y", "t"}), _
                New SyntaktickyAnalyzator.Analyzer(tstDy.Text, New String() {"x", "y", "t"}), _
                picMain.ClientSize, tstTmax.Text, tstΔt.Text, tstTmin.Text)
        picMain.Image = dr.Draw
    End Sub

End Class


