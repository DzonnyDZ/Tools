Imports CultureInfo = System.Globalization.CultureInfo
Imports CultureTypes = System.Globalization.CultureTypes
Imports Tools.MetadataT

''' <summary>Configures export in CSV format</summary>
''' <version version="2.0.5">This class is new in version 2.0.5</version>
Public Class CsvFormatConfigurator
    Inherits UserControl
    Implements IExportFormatConfigurator

    ''' <summary>CTor - creates a new instance of the <see cref="CsvFormatConfigurator"/> class</summary>
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        cmbNewLine.DataSource = {
            New With {.Name = "CrLf (Windows)", .NewLine = vbCrLf},
               New With {.Name = "Lf (Unix)", .NewLine = vbLf},
          New With {.Name = "Cr (Mac)", .NewLine = vbCr}}
        'cmbNewLine.DisplayMember = "Name"
        'cmbNewLine.ValueMember = "NewLine"
        cmbNewLine.SelectedIndex = 0

        For Each enc In System.Text.Encoding.GetEncodings
            cmbEncoding.Items.Add(enc.GetEncoding)
        Next
        Dim i% = 0
        For Each item As System.Text.Encoding In cmbEncoding.Items
            If item.WebName = "utf-8" Then
                cmbEncoding.SelectedIndex = i
                Exit For
            End If
            i += 1
        Next

        cmbCulture.DataSource = CultureInfo.GetCultures(CultureTypes.AllCultures)
        i = 0
        For Each item As CultureInfo In cmbCulture.Items
            If item.Name = System.Globalization.CultureInfo.CurrentCulture.Name Then
                cmbCulture.SelectedIndex = i
                Exit For
            End If
            i += 1
        Next
    End Sub

#Region "IExportFormatConfigurator"
    ''' <summary>Gets filter for <see cref="OpenFileDialog"/>.</summary>
    ''' <seelaso cref="OpenFileDialog.Filter"/>
    Public ReadOnly Property FileFilter As String Implements IExportFormatConfigurator.FileFilter
        Get
            Return My.Resources.CSV_Filter
        End Get
    End Property

    ''' <summary>Gets name of format to export data in</summary>
    Public ReadOnly Property FormatName As String Implements IExportFormatConfigurator.FormatName
        Get
            Return My.Resources.CSV
        End Get
    End Property
#End Region


#Region "Properties"
    ''' <summary>Gets or sets encoding used for writing CSV file</summary>
    Public Property Encoding As System.Text.Encoding
        Get
            Return cmbEncoding.SelectedItem
        End Get
        Set(ByVal value As System.Text.Encoding)
            cmbEncoding.SelectedIndex = cmbEncoding.Items.IndexOf(value)
        End Set
    End Property
    ''' <summary>Gets culture used for converting objects to string</summary>
    Public Property Culture As System.Globalization.CultureInfo
        Get
            Return cmbCulture.SelectedItem
        End Get
        Set(ByVal value As System.Globalization.CultureInfo)
            cmbCulture.SelectedIndex = cmbCulture.Items.IndexOf(value)
        End Set
    End Property
    ''' <summary>Gets or sets CSV field separator</summary>
    Public Property Separator As Char
        Get
            Return txtSeparator.Text
        End Get
        Set(ByVal value As Char)
            txtSeparator.Text = value
        End Set
    End Property
    ''' <summary>Gets or sets character used to enclose field value to</summary>
    Public Property TextQualifier As Char
        Get
            Return txtQualifier.Text
        End Get
        Set(ByVal value As Char)
            txtQualifier.Text = value
        End Set
    End Property
    ''' <summary>Gets or sets value indicating if text qualifier is used always or only when needed</summary>
    Public Property AlwaysUseTextQualifier As Boolean
        Get
            Return Not chkQualifierUsage.Checked
        End Get
        Set(ByVal value As Boolean)
            chkQualifierUsage.Checked = Not value
        End Set
    End Property
    ''' <summary>Gets or sets string used as line (record) separator</summary>
    Public Property NewLine As String
        Get
            Return cmbNewLine.SelectedValue
        End Get
        Set(ByVal value As String)
            cmbNewLine.SelectedValue = value
        End Set
    End Property
    ''' <summary>Possible modes of string escaping</summary>
    Public Enum Escaping
        ''' <summary>Special characters are doubled</summary>
        [Double]
        ''' <summary>Special characters are prepended with backslash (\)</summary>
        BackSlash
    End Enum
    ''' <summary>Gets or sets value indicating how text qualifier is escaped</summary>
    Public Property QualifierEspaing As Escaping
        Get
            Return If(optEscapeDouble.Checked, Escaping.Double, Escaping.BackSlash)
        End Get
        Set(ByVal value As Escaping)
            optEscapeDouble.Checked = value = Escaping.Double
            optEscapeBackSlash.Checked = value = Escaping.BackSlash
        End Set
    End Property
#End Region
    ''' <summary>Saves data in current format to given file</summary>
    ''' <param name="stream">Stream to export metadata to</param>
    ''' <param name="selectedFields">Denotes which fields are selected for metadata</param>
    ''' <param name="data">Metadata to be saved. One item per file.</param>
    ''' <param name="mCulture">Culture to use to format data</param>
    ''' <version version="2.0.7">Added parameter <paramref name="mCulture"/></version>
    Public Sub Save(ByVal stream As IO.Stream, ByVal selectedFields As IDictionary(Of String, List(Of String)), ByVal data As IEnumerable(Of IMetadataProvider), mCulture As IFormatProvider) Implements IExportFormatConfigurator.Save
        Using w As New IO.StreamWriter(stream, Encoding)
            'Header
            Dim hCol% = 0
            For Each category In selectedFields
                For Each field In category.Value
                    If hCol > 0 Then w.Write(Separator)
                    Dim name = field
                    Dim useQualifier = AlwaysUseTextQualifier OrElse name.Contains(Separator) OrElse name.StartsWith(TextQualifier) OrElse name.Contains(vbCr) OrElse name.Contains(vbLf)
                    If useQualifier AndAlso name.Contains(TextQualifier) Then
                        Select Case QualifierEspaing
                            Case Escaping.Double : name = name.Replace(TextQualifier, TextQualifier & TextQualifier)
                            Case Else : name = name.Replace("\", "\\").Replace(TextQualifier, "\" & TextQualifier)
                        End Select
                    End If
                    If useQualifier Then w.Write(TextQualifier)
                    w.Write(name)
                    If useQualifier Then w.Write(TextQualifier)
                    hCol += 1
                Next
            Next

            'Data
            Dim line% = 0
            For Each file In data
                w.Write(NewLine)
                Dim column% = 0
                For Each category In selectedFields
                    Dim metadata As IMetadata = Nothing
                    Try
                        If file.Contains(category.Key) Then metadata = file.Metadata(category.Key)
                        For Each field In category.Value
                            Dim value$ = Nothing
                            If metadata IsNot Nothing AndAlso metadata.ContainsKey(field) Then value = metadata.GetStringValue(field, mCulture)
                            'Dim strValue = ""
                            'If value IsNot Nothing Then
                            '    If TypeOf value Is IEnumerable AndAlso Not TypeOf value Is String Then
                            '        Dim b As New System.Text.StringBuilder
                            '        For Each valueItem In DirectCast(value, IEnumerable)
                            '            If b.Length > 0 Then b.Append(Separator)
                            '            b.AppendFormat(Culture, "{0}", valueItem)
                            '        Next valueItem
                            '        strValue = b.ToString
                            '    Else : strValue = String.Format(Culture, "{0}", value)
                            '    End If
                            'End If
                            If value IsNot Nothing Then value = value.Replace(vbNullChar, "") Else value = ""
                            Dim useQualifier = AlwaysUseTextQualifier OrElse value.Contains(Separator) OrElse value.StartsWith(TextQualifier) OrElse value.Contains(vbCr) OrElse value.Contains(vbLf)
                            If useQualifier AndAlso value.Contains(TextQualifier) Then
                                Select Case QualifierEspaing
                                    Case Escaping.Double : value = value.Replace(TextQualifier, TextQualifier & TextQualifier)
                                    Case Else : value = value.Replace("\", "\\").Replace(TextQualifier, "\" & TextQualifier)
                                End Select
                            End If
                            If column > 0 Then w.Write(Separator)
                            If useQualifier Then w.Write(TextQualifier)
                            w.Write(value)
                            If useQualifier Then w.Write(TextQualifier)
                            column += 1
                        Next field
                    Finally
                        If metadata IsNot Nothing AndAlso TypeOf metadata Is IDisposable Then DirectCast(metadata, IDisposable).Dispose()
                    End Try
                Next category
                line += 1
            Next file
        End Using
    End Sub

    Private Sub cmdTab_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTab.Click
        txtSeparator.Text = vbTab
    End Sub

    Private Sub cmdExcelFriendly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelFriendly.Click
        Separator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
        TextQualifier = """"c
        AlwaysUseTextQualifier = False
        NewLine = vbCrLf
        Encoding = System.Text.Encoding.UTF8
        Culture = System.Globalization.CultureInfo.CurrentCulture
        QualifierEspaing = Escaping.Double
    End Sub
End Class
