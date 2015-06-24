Imports System.Xml
Imports Tools.XmlT.LinqT
Imports Tools.XmlT
Imports System.Globalization.CultureInfo
Imports System.Linq
Imports System.Text
Imports <xmlns:cf="http://schemas.microsoft.com/winfx/2006/xaml/composite-font">
Imports System.Xml.Xsl
Imports Microsoft.Win32
Imports Tools
Imports Tools.TextT.UnicodeT

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

            If MsgBox("Wanna optimize the composite font now?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
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
            Dim compositeRanges = From r In ParseRange(compositeMap.@Unicode) Where r.Item1 <= r.Item2 Order By r.Item1
            Dim targetRanges As IEnumerable(Of Tuple(Of UInteger, UInteger)) = EmptyArray(Of Tuple(Of UInteger, UInteger)).value
            For Each targetMap In ff.FamilyMaps
                targetRanges = targetRanges.Concat(ParseRange(targetMap.Unicode))
            Next
            Dim tre = (From r In targetRanges Where r.Item1 <= r.Item2 Order By r.Item1).GetEnumerator
            Dim outputRanges As New List(Of Tuple(Of UInteger, UInteger))
            Dim targetRange As Tuple(Of UInteger, UInteger)
            Dim moveTarget = Function()
                                 If tre.MoveNext Then
                                     targetRange = tre.Current
                                     Return True
                                 Else
                                     targetRange = Nothing
                                     Return False
                                 End If
                             End Function
            If moveTarget() Then
                For Each compositeRange As Tuple(Of UInteger, UInteger) In compositeRanges
                    While targetRange.Item1 < compositeRange.Item2
                        If Not moveTarget() Then Exit For
                        outputRanges.Add(Tuple.Create(Math.Max(compositeRange.Item1, targetRange.Item1), Math.Min(compositeRange.Item2, targetRange.Item2)))
                    End While
                Next
            End If
            If outputRanges.Count = 0 Then
                toRemove.Add(compositeMap)
            Else
                tre = outputRanges.GetEnumerator
                outputRanges = New List(Of Tuple(Of UInteger, UInteger))(outputRanges.Count)
                While tre.MoveNext
                    Dim l = tre.Current.Item1
                    Dim u = tre.Current.Item2
                    While tre.MoveNext AndAlso tre.Current.Item1 = u
                        u = tre.Current.Item1
                    End While
                    outputRanges.Add(Tuple.Create(l, u))
                End While

                Dim b As New StringBuilder
                For Each orng In outputRanges
                    If b.Length > 0 Then b.Append(",")
                    If orng.Item1 = orng.Item2 Then b.Append(orng.Item2.ToString("X", InvariantCulture)) _
                        Else b.AppendFormat(InvariantCulture, "{0:X}-{1:X}", orng.Item1, orng.Item2)
                Next
                compositeMap.@Unicode = b.ToString
            End If
        Next
        For Each mtr In toRemove
            mtr.Remove()
        Next
    End Sub

    Private Sub Hyperlink_Click(sender As Hyperlink, e As RoutedEventArgs)
        Process.Start(sender.NavigateUri.ToString)
        e.Handled = True
    End Sub
End Class
