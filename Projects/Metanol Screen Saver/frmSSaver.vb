Imports Tools.DrawingT.ImageTools, Tools.DrawingT.MetadataT
Partial Friend Class frmSSaver
    Private Shared Files As Files
    Private Shared FilesLock As New Object
    Shared Sub New()
        SyncLock FilesLock
            'If Files Is Nothing Then
            '    Dim root = My.Settings.Folder
            '    If root = "" Then root = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            '    Dim Masks = My.Settings.Mask
            '    If Masks.Count = 0 Then Masks.AddRange(New String() {"*.jpg", "*.jpeg"})
            '    Files = New Files(root, New Tools.CollectionsT.GenericT.Wrapper(Of String)(Masks), My.Settings.Alghoritm)
            'End If
        End SyncLock
    End Sub
    Private Enumerator As IEnumerator(Of String)
    Private Sub frmSSaver_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.BackColor = My.Settings.BgColor
        'picMain.BackColor = My.Settings.BgColor
        'tlbInfo.BackColor = My.Settings.InfoBgColor
        'tlbInfo.ForeColor = My.Settings.FgColor
        'tlbInfo.Text = My.Settings.InfoAlign
        'Try : tlbInfo.Font = My.Settings.Font : Catch : End Try

        'Enumerator = Files.GetEnumerator
        'If Enumerator.MoveNext() Then
        '    tmrImg.Interval = My.Settings.Timer * 1000
        '    tmrImg.Enabled = True
        '    LoadImage()
        'End If
        'Cursor.Hide()
    End Sub
    Private Enum ParseStates
        Text
        Lt1
        [Namespace]
        Name
        Format
    End Enum
    Friend Shared Function ParseText(ByVal input As String, ByVal Exif As Exif.IFDExif, ByVal IPTC As IPTC, ByVal SysInfo As SysInfo) As String
        Dim ret As New System.Text.StringBuilder
        Dim State = ParseStates.Text
        Dim PrpName As System.Text.StringBuilder = Nothing
        Dim PrpNamespace As System.Text.StringBuilder = Nothing
        Dim PrpFormat As System.Text.StringBuilder = Nothing
        Dim i = -1I
        For Each ch As Char In input : i += 1
            Select Case State
                Case ParseStates.Text
                    Select Case ch
                        Case "<"c : State = ParseStates.Lt1
                        Case Else : ret.Append(ch)
                    End Select
                Case ParseStates.Lt1
                    Select Case ch
                        Case "<"c : State = ParseStates.Text : ret.Append("<"c)
                        Case "A"c To "Z"c, "a"c To "z"c, "_"c
                            PrpNamespace = New System.Text.StringBuilder
                            PrpNamespace.Append(ch)
                            State = ParseStates.Namespace
                        Case Else : Throw New TextSyntaxErrorException(i, String.Format("Unexpected character {0}, expected characters 'A'-'Z', 'a'-'z', '_'.", ch))
                    End Select
                Case ParseStates.Namespace
                    Select Case ch
                        Case "A"c To "Z"c, "a"c To "z"c, "0"c To "9"c, "_"c
                            PrpNamespace.Append(ch)
                        Case ":"c
                            PrpName = New System.Text.StringBuilder
                            State = ParseStates.Name
                        Case Else : Throw New TextSyntaxErrorException(i, String.Format("Unexpected character {0}, expected characters 'A'-'Z', 'a'-'z', '0'-'9', '_', ':'.", ch))
                    End Select
                Case ParseStates.Name
                    Select Case ch
                        Case "A"c To "Z"c, "a"c To "z"c, "0"c To "9"c, "_"c
                            PrpName.Append(ch)
                        Case ":"c
                            PrpFormat = New System.Text.StringBuilder
                            State = ParseStates.Format
                        Case Else : Throw New TextSyntaxErrorException(i, String.Format("Unexpected character {0}, expected characters 'A'-'Z', 'a'-'z', '0'-'9', '_', ':'.", ch))
                    End Select
                Case ParseStates.Format
                    Select Case ch
                        Case ">"c
                            Dim Obj As Object
                            Select Case PrpNamespace.ToString
                                Case "Exif"
                                    Obj = Exif
                                Case "IPTC"
                                    Obj = IPTC
                                Case "Sys"
                                    Obj = SysInfo
                                Case Else : Throw New TextSyntaxErrorException(i - PrpFormat.ToString.Length - PrpName.ToString.Length - PrpNamespace.ToString.Length, String.Format("Unknown property namespace {0}. Expected 'IPTC', 'Exif' or 'Sys'", PrpNamespace.ToString))
                            End Select
                            If Obj IsNot Nothing Then
                                Dim t = Obj.GetType
                                Dim prp = t.GetProperty(PrpName.ToString)
                                If prp IsNot Nothing Then
                                    Try
                                        Dim PVal As Object = prp.GetValue(Obj, Nothing)
                                        If TypeOf PVal Is String Then
                                            PVal = CStr(PVal).Trim(" "c, vbTab, vbCr, vbLf, ChrW(0))
                                        End If
                                        If PVal IsNot Nothing Then
                                            Try
                                                ret.Append(String.Format(PrpFormat.ToString, PVal))
                                            Catch ex As FormatException
                                                ret.Append(String.Format("<ERROR in format {0}: {1}", PrpFormat.ToString, ex.Message))
                                            End Try
                                        End If
                                    Catch : End Try
                                End If
                            End If
                            State = ParseStates.Text
                        Case Else : PrpFormat.Append(ch)
                    End Select
            End Select
        Next ch
        Return ret.ToString
    End Function
    Friend Class TextSyntaxErrorException : Inherits SyntaxErrorException
        Public ReadOnly Position As Integer
        Public Sub New(ByVal Position As Integer, Optional ByVal Message$ = "", Optional ByVal InnerException As Exception = Nothing)
            MyBase.New(Message, InnerException)
            Me.Position = Position
        End Sub
    End Class
    Private Sub LoadImage()
        Dim path = Enumerator.Current
        If path <> "" Then
            Try : picMain.Load(path) : Catch : End Try
            Try
                Using JPEG As New Tools.DrawingT.IO.JPEG.JPEGReader(path, False)
                    Dim IPTC As IPTC = Nothing
                    Try : IPTC = New IPTC(JPEG) : Catch : End Try
                    Dim Exif As Exif.IFDExif = Nothing
                    Try
                        Exif = New Exif.IFDExif(New Tools.DrawingT.MetadataT.ExifReader(JPEG).ExifSubIFD)
                    Catch : End Try
                    tlbInfo.AutoSize = True
                    'tlbInfo.Text = ParseText(My.Settings.InfoText, Exif, IPTC, New SysInfo(picMain.Image, path))
                End Using
            Catch : End Try
        End If
        tmrImg.Enabled = Enumerator.MoveNext
    End Sub
    Private Sub SetEl(ByVal El As HtmlElement, ByVal Value As Object)
        If El IsNot Nothing Then
            Dim empty As Boolean = Not (Value Is Nothing OrElse _
                (TypeOf Value Is String AndAlso CStr(Value) = "") OrElse _
                (Value.GetType.IsGenericType AndAlso Value.GetType.GetGenericTypeDefinition.Equals(GetType(Nullable(Of ))) AndAlso CBool(Value.GetType.GetProperty("HasValue").GetValue(Value, Nothing)) = False) _
            )
            If empty Then : El.InnerText = "" : Else
                Dim fstr = El.GetAttribute("f")
                If fstr = "" Then fstr = "{0}"
                El.InnerText = String.Format(fstr, Value)
            End If
        End If
    End Sub

    Private Sub tmrImg_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrImg.Tick
        LoadImage()
    End Sub

    Private Sub item_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles picMain.MouseDown, tlbInfo.MouseDown
        Me.OnMouseDown(e)
    End Sub

    Private Sub tlbInfo_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbInfo.SizeChanged
        'Select Case My.Settings.InfoAlign
        '    Case ContentAlignment.BottomCenter, ContentAlignment.BottomLeft, ContentAlignment.BottomRight
        '        tlbInfo.Top = Me.ClientSize.Height - tlbInfo.Height
        '    Case ContentAlignment.MiddleCenter, ContentAlignment.MiddleLeft, ContentAlignment.MiddleRight
        '        tlbInfo.Top = Me.ClientSize.Height / 2 - tlbInfo.Height / 2
        '    Case ContentAlignment.TopCenter, ContentAlignment.TopLeft, ContentAlignment.TopRight
        '        tlbInfo.Top = 0
        'End Select
        'Select Case My.Settings.InfoAlign
        '    Case ContentAlignment.BottomCenter, ContentAlignment.MiddleCenter, ContentAlignment.TopCenter
        '        tlbInfo.Left = Me.ClientSize.Width / 2 - tlbInfo.Width / 2
        '    Case ContentAlignment.BottomLeft, ContentAlignment.MiddleLeft, ContentAlignment.TopLeft
        '        tlbInfo.Left = 0
        '    Case ContentAlignment.BottomRight, ContentAlignment.MiddleRight, ContentAlignment.TopRight
        '        tlbInfo.Left = Me.ClientSize.Width - tlbInfo.Width
        'End Select
    End Sub
End Class

