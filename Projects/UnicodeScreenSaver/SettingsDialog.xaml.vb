Imports System.Globalization.CultureInfo
Imports System.Text
Imports System.Xml
Imports System.Xml.Xsl
Imports Microsoft.Win32
Imports Tools.TextT.UnicodeT
Imports <xmlns:cf="http://schemas.microsoft.com/winfx/2006/xaml/composite-font">

''' <summary>UI for setting up the screensaver</summary>
Public Class SettingsDialog

    Private Sub OK_Click(sender As Object, e As RoutedEventArgs)
        My.Settings.Save()
        Close()
    End Sub

    Private Sub Cancel_Click(sender As Object, e As RoutedEventArgs)
        My.Settings.Reload()
        Close()
    End Sub

    Private Sub SettingsDialog_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DataContext = My.Settings
        BindFonts()
    End Sub

    ''' <summary>Binds system fonts or fonts form selected location to fonts dropdown</summary>
    Private Sub BindFonts()
        If String.IsNullOrEmpty(txtFontPath.Text) Then
            cmbFonts.ItemsSource = Fonts.SystemFontFamilies
        Else
            Try
                cmbFonts.ItemsSource = Fonts.GetFontFamilies(txtFontPath.Text)
            Catch
                cmbFonts.ItemsSource = Nothing
            End Try
        End If
    End Sub

    Private Sub txtFontPath_TextChanged(sender As Object, e As TextChangedEventArgs)
        BindFonts()
    End Sub

    Private Sub btnFontBrowse_Click(sender As Object, e As RoutedEventArgs)
        Dim dialog As New OpenFileDialog With {
            .Title = "Select font",
            .Filter = "All fonts (*.ttf, *.oft, *.ttc, *.CompositeFont)|*.ttf;*.otf;*.ttc;*.CompositeFont|Classic fonts (*.ttf, *.otf, *.ttc)|*.ttf;*.otf;*.ttc|Composite fonts (*.CompositeFont)|*.CompositeFont|TrueType fonts (*.ttf)|*.ttf|OpenType fonts (*.otf)|*.otf|TrueType font collections (*.ttc)|*.ttc|All files (*.*)|*.*"
        }
        If dialog.ShowDialog(Me) Then txtFontPath.Text = dialog.FileName
    End Sub

    Private Sub btnConvertCompositeFont_Click(sender As Object, e As RoutedEventArgs)
        Dim openDialog As New OpenFileDialog With {.Title = "Select BabelMap composite font", .Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"}
        Dim saveDialog As New SaveFileDialog With {
            .Title = "Save .NET/WPF Composite font",
            .Filter = "Composite fonts (*.CompositeFont)|*.CompositeFont",
            .DefaultExt = "CompositeFont",
            .AddExtension = True
        }

        If openDialog.ShowDialog(Me) AndAlso saveDialog.ShowDialog(Me) Then

            Dim debug As Boolean
#If DEBUG Then
            debug = True
#Else
            debug=false
#End If


            Dim transform As New XslCompiledTransform(debug)
            Try
                Using rs = GetType(SettingsDialog).Assembly.GetManifestResourceStream("Dzonny.UnicodeScreenSaver.Babel 2 WPF composite font.xslt")
                    Using xmlr = XmlReader.Create(rs)
                        transform.Load(xmlr)
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Failed to load embedded XSL transformation: " & ex.Message, MsgBoxStyle.Critical)
                Return
            End Try

            Try
                Dim args As New XsltArgumentList()
                Dim ucddoc As New XmlDocument
                Using ms As New IO.MemoryStream
                    Dim ucdxdoc = UnicodeCharacterDatabase.GetXml()
                    Using xw = XmlWriter.Create(ms)
                        ucdxdoc.WriteTo(xw)
                        xw.Flush()
                    End Using
                    ms.Seek(0, IO.SeekOrigin.Begin)
                    ucddoc.Load(ms)
                End Using
                args.AddParam("UCDXML", "", ucddoc)
                Using sfs = IO.File.Open(saveDialog.FileName, IO.FileMode.Truncate, IO.FileAccess.Write, IO.FileShare.Read)
                    transform.Transform(openDialog.FileName, args, sfs)
                End Using
            Catch ex As Exception
                MsgBox("Failed to transform the file " & ex.Message, MsgBoxStyle.Critical)
            End Try

            If MsgBox("Wanna optimize the composite font now?" & vbCrLf & "Optimization will remove characters that composite mapping indicated to be present in the fonts but in fact they are not.", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    OptimizeCompositeFont(saveDialog.FileName)
                Catch ex As Exception
                    MsgBox("Failed to optimize composite font " & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            End If
        End If
    End Sub

    ''' <summary>Optimizes composite font by removing ranges that are not supported by target fonts of individual map components</summary>
    ''' <param name="fileName">Name of file where the composite font is stored</param>
    Private Sub OptimizeCompositeFont(fileName As String)
        Dim compositeXml = XDocument.Load(fileName)
        OptimizeCompositeFont(compositeXml)
        compositeXml.Save(fileName)
    End Sub

    ''' <summary>Optimizes composite font by removing ranges that are not supported by target fonts of individual map components</summary>
    ''' <param name="compositeXml">Composite font as XML document</param>
    Private Sub OptimizeCompositeFont(compositeXml As XDocument)

        Dim toRemove As New List(Of XElement)
        For Each compositeMap In compositeXml.<cf:FontFamily>.<cf:FontFamily.FamilyMaps>.<cf:FontFamilyMap>
            Dim ff = New FontFamily(compositeMap.@Target)
            Dim faces = ff.GetTypefaces
            Dim glyphFaces As New List(Of GlyphTypeface)(faces.Count)
            For Each f In faces
                Dim gf As GlyphTypeface
                If f.TryGetGlyphTypeface(gf) Then glyphFaces.Add(gf)
            Next

            Dim ranges As New List(Of Tuple(Of UInteger, UInteger))
            For Each range In ParseRange(compositeMap.@Unicode)
                Dim min As UInteger?, max As UInteger?
                For character = range.Item1 To range.Item2
                    If FontFacesContainCodePoint(glyphFaces, character) Then
                        If min Is Nothing Then min = character
                        max = character
                    ElseIf min.HasValue Then
                        ranges.Add(Tuple.Create(min.Value, max.Value))
                        min = Nothing
                        max = Nothing
                    End If
                Next
                If min.HasValue Then ranges.Add(Tuple.Create(min.Value, max.Value))
            Next
            If ranges.Count > 0 Then
                Dim b As New StringBuilder
                For Each range In ranges
                    If b.Length > 0 Then b.Append(",")
                    If range.Item1 = range.Item2 Then b.Append(range.Item1.ToString("X", InvariantCulture)) _
                    Else b.AppendFormat(InvariantCulture, "{0:X}-{1:X}", range.Item1, range.Item2)
                Next
                compositeMap.@Unicode = b.ToString
            Else
                toRemove.Add(compositeMap)
            End If
        Next
        For Each tr In toRemove
            tr.Remove()
        Next
    End Sub

    Private Function FontFacesContainCodePoint(glyphFaces As List(Of GlyphTypeface), character As UInteger) As Boolean

        Dim found As Boolean = False
        For Each face In glyphFaces
            Dim glyph As UShort
            If face.CharacterToGlyphMap.TryGetValue(character, glyph) Then
                found = True
                Exit For
            End If
        Next
        Return found
    End Function

    Private Sub Hyperlink_Click(sender As Hyperlink, e As RoutedEventArgs)
        Process.Start(sender.NavigateUri.ToString)
        e.Handled = True
    End Sub
    Private Sub btnFontDefault_Click(sender As Object, e As RoutedEventArgs)
        txtFontPath.Text = "pack://application:,,,/"
        cmbFonts.Text = "Unicode composite font"
    End Sub
End Class
