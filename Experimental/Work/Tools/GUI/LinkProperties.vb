'http://www.codeplex.com/DTools/SourceControl/FileView.aspx?itemId=305155&changeSetId=34459

Imports Microsoft.Win32
Imports System.Security.Permissions, System.Security
Imports System.Globalization
Imports System.Drawing
Imports System.Windows.Forms

Namespace GUI
    ''' <summary>Gives acces of hyperlink settings of Internet Explorer</summary>
    ''' <remarks>See also <seealso cref="Tools.DrawingT.SystemColorsExtension"/></remarks>
    Public NotInheritable Class LinkProperties
        ''' <summary>There is no CTor</summary>
        Private Sub New()
        End Sub
        ''' <summary>Color of not visited not hovered hyperlink</summary>
        ''' <remarks>This system value can be also accesed via <see cref="System.Windows.Forms.ToolstripLabel.LinkColor"/></remarks>
        Public Shared ReadOnly Property Color() As Color
            Get
                Return LinkUtilities.IELinkColor
            End Get
        End Property
        ''' <summary>Color of visited hyperlink</summary>
        ''' <remarks>This system value can be also accesed via <see cref="System.Windows.Forms.ToolstripLabel.VisitedLinkColor"/></remarks>
        Public Shared ReadOnly Property VisitedColor() As Color
            Get
                Return LinkUtilities.IEVisitedLinkColor
            End Get
        End Property
        ''' <summary>Color of hovered (active) hyperlink</summary>
        ''' <remarks>This system value can be also accesed via <see cref="System.Windows.Forms.ToolstripLabel.ActiveLinkColor"/></remarks>
        Public Shared ReadOnly Property HoveredColor() As Color
            Get
                Return LinkUtilities.IEActiveLinkColor
            End Get
        End Property
        ''' <summary>System defined link behavior (this is actual setting of Internet Explorer)</summary>
        Public Shared ReadOnly Property Behavior() As LinkBehavior
            Get
                Return LinkUtilities.GetIELinkBehavior
            End Get
        End Property
        ''' <summary>Decorates given <see cref="Font"/> according to given <see cref="LinkBehavior"/> applyed on non-hovered link</summary>
        ''' <param name="Base">Base font to be decorated</param>
        ''' <param name="Behavior">Link behavior used to decorate <paramref name="Base"/>. If ommited system setting is used</param>
        ''' <returns><paramref name="Base"/> underlined or not according to <paramref name="Behavior"/> or system settings</returns>
        Public Shared Function LinkFontDecoration(ByVal Base As Font, Optional ByVal Behavior As LinkBehavior = LinkBehavior.SystemDefault) As Font
            Dim f As Font = Nothing
            LinkUtilities.EnsureLinkFonts(Base, Behavior, f, Nothing)
            Return f
        End Function
        ''' <summary>Decorates given <see cref="Font"/> according to given <see cref="LinkBehavior"/> applyed on hovered link</summary>
        ''' <param name="Base">Base font to be decorated</param>
        ''' <param name="Behavior">Link behavior used to decorate <paramref name="Base"/>. If ommited system setting is used</param>
        ''' <returns><paramref name="Base"/> underlined or not according to <paramref name="Behavior"/> or system settings</returns>
        Public Shared Function HoverLinkFontDecoration(ByVal Base As Font, Optional ByVal Behavior As LinkBehavior = LinkBehavior.SystemDefault) As Font
            Dim f As Font = Nothing
            LinkUtilities.EnsureLinkFonts(Base, Behavior, Nothing, f)
            Return f
        End Function
    End Class
    ''' <summary>Copy of Micforoft's internal System.Windows.Forms.LinkUtilities class that gives access to link setting of Internet Explorer</summary>
    ''' <remarks>This is 1:1 copy of <see cref="T:System.Windows.Forms.LinkUtilities"/> got by Reflector</remarks>
    Friend NotInheritable Class LinkUtilities
        ''' <summary>There is no CTor</summary>
        Private Sub New()
        End Sub
        ''' <summary>Gets link fonts</summary>
        ''' <param name="baseFont">Default font</param>
        ''' <param name="link">Custom link behavior, pass <see cref="LinkBehavior.SystemDefault"/> to use system default</param>
        ''' <param name="linkFont">Out. Link font</param>
        ''' <param name="hoverLinkFont">Out. Hovered link font</param>
        Public Shared Sub EnsureLinkFonts(ByVal baseFont As Font, ByVal link As LinkBehavior, ByRef linkFont As Font, ByRef hoverLinkFont As Font)
            If ((linkFont Is Nothing) OrElse (hoverLinkFont Is Nothing)) Then
                Dim flag As Boolean = True
                Dim flag2 As Boolean = True
                If (link = LinkBehavior.SystemDefault) Then
                    link = LinkUtilities.GetIELinkBehavior
                End If
                Select Case link
                    Case LinkBehavior.AlwaysUnderline
                        flag = True
                        flag2 = True
                        Exit Select
                    Case LinkBehavior.HoverUnderline
                        flag = False
                        flag2 = True
                        Exit Select
                    Case LinkBehavior.NeverUnderline
                        flag = False
                        flag2 = False
                        Exit Select
                End Select
                Dim prototype As Font = baseFont
                If (flag2 = flag) Then
                    Dim newStyle As FontStyle = prototype.Style
                    If flag2 Then
                        newStyle = (newStyle Or FontStyle.Underline)
                    Else
                        newStyle = (newStyle And Not FontStyle.Underline)
                    End If
                    hoverLinkFont = New Font(prototype, newStyle)
                    linkFont = hoverLinkFont
                Else
                    Dim style As FontStyle = prototype.Style
                    If flag2 Then
                        style = (style Or FontStyle.Underline)
                    Else
                        style = (style And Not FontStyle.Underline)
                    End If
                    hoverLinkFont = New Font(prototype, style)
                    Dim style3 As FontStyle = prototype.Style
                    If flag Then
                        style3 = (style3 Or FontStyle.Underline)
                    Else
                        style3 = (style3 And Not FontStyle.Underline)
                    End If
                    linkFont = New Font(prototype, style3)
                End If
            End If
        End Sub
        ''' <summary>Gets IE color by its name</summary>
        ''' <param name="name">Name of color</param>
        ''' <returns>Value of color with name <paramref name="name"/></returns>
        ''' <permission cref="RegistryPermission"/>
        Private Shared Function GetIEColor(ByVal name As String) As Color
            Dim red As Color
            Dim rpm As RegistryPermission = New RegistryPermission(PermissionState.Unrestricted)
            rpm.Assert()
            Try
                Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Internet Explorer\Settings")
                If (Not key Is Nothing) Then
                    Dim text As String = CStr(key.GetValue(name))
                    If (Not [text] Is Nothing) Then
                        Dim textArray As String() = [text].Split(New Char() {","c})
                        Dim numArray As Integer() = New Integer(3 - 1) {}
                        Dim num As Integer = Math.Min(numArray.Length, textArray.Length)
                        Dim i As Integer
                        For i = 0 To num - 1
                            Integer.TryParse(textArray(i), numArray(i))
                        Next i
                        Return Color.FromArgb(numArray(0), numArray(1), numArray(2))
                    End If
                End If
                If String.Equals(name, IEAnchorColor, StringComparison.OrdinalIgnoreCase) Then
                    Return Color.Blue
                End If
                If String.Equals(name, IEAnchorColorVisited, StringComparison.OrdinalIgnoreCase) Then
                    Return Color.Purple
                End If
                If String.Equals(name, IEAnchorColorHover, StringComparison.OrdinalIgnoreCase) Then
                    Return Color.Red
                End If
                red = Color.Red
            Finally
                CodeAccessPermission.RevertAssert()
            End Try
            Return red
        End Function
        ''' <summary>Gets system-defined default link behavior</summary>
        ''' <returns>System-defined default link behavior</returns>
        ''' <permission cref="RegistryPermission"/>
        Public Shared Function GetIELinkBehavior() As LinkBehavior
            Dim rpm As RegistryPermission = New RegistryPermission(PermissionState.Unrestricted)
            rpm.Assert()
            Try
                Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Internet Explorer\Main")
                If (Not key Is Nothing) Then
                    Dim strA As String = CStr(key.GetValue("Anchor Underline"))
                    If ((Not strA Is Nothing) AndAlso (String.Compare(strA, "no", True, CultureInfo.InvariantCulture) = 0)) Then
                        Return LinkBehavior.NeverUnderline
                    End If
                    If ((Not strA Is Nothing) AndAlso (String.Compare(strA, "hover", True, CultureInfo.InvariantCulture) = 0)) Then
                        Return LinkBehavior.HoverUnderline
                    End If
                    Return LinkBehavior.AlwaysUnderline
                End If
            Finally
                CodeAccessPermission.RevertAssert()
            End Try
            Return LinkBehavior.AlwaysUnderline
        End Function
        ' Properties
        ''' <summary>Color of active hyperlink</summary>
        Public Shared ReadOnly Property IEActiveLinkColor() As Color
            Get
                If _IEActiveLinkColor.IsEmpty Then
                    _IEActiveLinkColor = LinkUtilities.GetIEColor(IEAnchorColorHover)
                End If
                Return _IEActiveLinkColor
            End Get
        End Property
        ''' <summary>Color of hyperlink</summary>
        Public Shared ReadOnly Property IELinkColor() As Color
            Get
                If _IELinkColor.IsEmpty Then
                    _IELinkColor = LinkUtilities.GetIEColor(IEAnchorColor)
                End If
                Return _IELinkColor
            End Get
        End Property
        ''' <summary>Color of visited hyperlink</summary>
        Public Shared ReadOnly Property IEVisitedLinkColor() As Color
            Get
                If _IEVisitedLinkColor.IsEmpty Then
                    _IEVisitedLinkColor = LinkUtilities.GetIEColor(IEAnchorColorVisited)
                End If
                Return _IEVisitedLinkColor
            End Get
        End Property
        ' Fields
        ''' <summary>Contains value of the <see cref="IEActiveLinkColor"/> property</summary>
        Private Shared _IEActiveLinkColor As Color = Color.Empty
        ''' <summary>Name of property that stores hyperlink color</summary>
        Private Const IEAnchorColor As String = "Anchor Color"
        ''' <summary>Name of property that stores hovered hyperlink color</summary>
        Private Const IEAnchorColorHover As String = "Anchor Color Hover"
        ''' <summary>Name of property that stores visited hyperlink color</summary>
        Private Const IEAnchorColorVisited As String = "Anchor Color Visited"
        ''' <summary>Contains value of the <see cref="IELinkColor"/> property</summary>
        Private Shared _IELinkColor As Color = Color.Empty
        ''' <summary>Path of main IE regsitry key</summary>
        Public Const IEMainRegPath As String = "Software\Microsoft\Internet Explorer\Main"
        ''' <summary>Path of settings IE registry key</summary>
        Private Const IESettingsRegPath As String = "Software\Microsoft\Internet Explorer\Settings"
        ''' <summary>Contains value of the <see cref="IEVisitedLinkColor"/> property</summary>
        Private Shared _IEVisitedLinkColor As Color = Color.Empty
    End Class
End Namespace