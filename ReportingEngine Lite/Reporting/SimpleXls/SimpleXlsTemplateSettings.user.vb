Imports System.ComponentModel
Imports res = Tools.ReportingT.ReportingEngineLite.ReportingResources
Imports System.Drawing.Design
Imports System.ComponentModel.Design
Imports Tools.ComponentModelT

Namespace ReportingT.ReportingEngineLite
    ''' <summary>Settings of <see cref="SimpleXlsTemplate"/></summary>
    <TypeDescriptionProvider(GetType(SingleTypeDescriptionProvider(Of SimpleXlsSettings, SimpleXlsSettings.SimpleXlsTypeDescriptor)))> _
    Partial Public Class SimpleXlsSettings : Implements ICloneable
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function Clone() As SimpleXlsSettings
            Dim s As New Xml.Serialization.XmlSerializer(Me.GetType)
            Dim str As New IO.MemoryStream
            s.Serialize(str, Me)
            str.Position = 0
            Return s.Deserialize(str)
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function
#Region "Changing"
        ''' <summary>Regular expression for <see cref="AutoWidth"/> testing</summary>
        Private AutoWidthRegEx As New System.Text.RegularExpressions.Regex("^(\*)|(\d+(-\d+)?(,\d+(-\d+)?)*(,\d+-)?)|(\d+-)$", System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.CultureInvariant)
        ''' <summary>Regukar expression for <see cref="PrintArea"/> testing</summary>
        Private PrintAreaRegEx As New System.Text.RegularExpressions.Regex("^[+-]?\d+;[+-]?\d+-[+-]?\d;+[+-]?\d+$", System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.CultureInvariant)

        Private Sub SimpleXlsSettings_AutoWidthChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.AutoWidthChanging
            If e.ProposedNewValue = "" Then Return
            If Not AutoWidthRegEx.IsMatch(e.ProposedNewValue) Then Throw New FormatException("AutoWidth má neplatný formát." & vbCrLf & "Zadejte *, nebo seznam rozsahů sloupců určených jejich indexy (1-based). Například ""7,8,10-15,20,22,53-"" (bez uvozovek)")
        End Sub

        Private Sub SimpleXlsSettings_Col1Changing(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.Col1Changing
            If e.ProposedNewValue < 1 Then Throw New ArgumentOutOfRangeException("Col1 musí být větší než 0")
        End Sub

        Private Sub SimpleXlsSettings_ColumnNameRowChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.ColumnNameRowChanging
            If e.ProposedNewValue < 0 Then Throw New ArgumentOutOfRangeException("ColumnNameRow musí být větší než nebo rovno 0")
        End Sub

        Private Sub SimpleXlsSettings_ListChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.ListChanging
            Dim Value As Integer
            If Integer.TryParse(e.ProposedNewValue, Globalization.NumberStyles.Integer, Globalization.CultureInfo.InvariantCulture, Value) AndAlso Value <= 0 Then Throw _
                New ArgumentOutOfRangeException("Pokud je sloupec zadán jako číslo, musí toto číslo být větší než 0.")
        End Sub

        Private Sub SimpleXlsSettings_PrintAreaChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.PrintAreaChanging
            If e.ProposedNewValue = "" Then Return
            If Not PrintAreaRegEx.IsMatch(e.ProposedNewValue) Then Throw New FormatException("PrintArea má neplatný formát." & vbCrLf & "Zadejte jako x1;y1-x2;y2 každé číslo ve formátu +a, -a, 0 nebo a.")
        End Sub

        Private Sub SimpleXlsSettings_Row1Changing(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.Row1Changing
            If e.ProposedNewValue < 1 Then Throw New ArgumentOutOfRangeException("Row1 musí být větší než nula")
        End Sub
#End Region
        ''' <summary>Gets indexes (1-based) of all columns to set width of</summary>
        ''' <param name="FilledColumns">Indexes (1-based) of al columns data were entered to</param>
        ''' <returns>1-based indexs of columns to set width of</returns>
        Public Function GetAutoWidthColumns(ByVal FilledColumns As Integer()) As Integer()
            If Me.AutoWidth = "" Then Return New Integer() {}
            If Me.AutoWidth = "*" Then Return FilledColumns
            Dim Parts As String() = Me.AutoWidth.Split(","c)
            Dim ret As New List(Of Integer)
            For Each part As String In Parts
                If part.EndsWith("-") Then
                    Dim coln As Integer = Integer.Parse(part.Substring(0, part.Length - 1), Globalization.CultureInfo.InvariantCulture)
                    For i As Integer = coln To Tools.MathT.Max(FilledColumns)
                        If Array.IndexOf(FilledColumns, i >= 0) Then ret.Add(i)
                    Next
                ElseIf part.Contains("-") Then
                    Dim col1 As Integer = Integer.Parse(part.Substring(0, part.IndexOf("-"c)), Globalization.CultureInfo.InvariantCulture)
                    Dim col2 As Integer = Integer.Parse(part.Substring(part.IndexOf("-"c) + 1), Globalization.CultureInfo.InvariantCulture)
                    For i As Integer = col1 To col2
                        ret.Add(i)
                    Next
                Else
                    Dim coln As Integer = Integer.Parse(part, Globalization.CultureInfo.InvariantCulture)
                    ret.Add(coln)
                End If
            Next
            Return ret.ToArray
        End Function

        ''' <summary>Determines if a columns with given number is selected for autowidth</summary>
        ''' <param name="index">Column index (1-based)</param>
        ''' <returns>True if <paramref name="index"/> is in one of rages specified in <see cref="AutoWidth"/></returns>
        Public Function IsAutoWidth(ByVal index As Integer) As Boolean
            If Me.AutoWidth = "" Then Return False
            Dim Parts As String() = Me.AutoWidth.Split(","c)
            For Each part As String In Parts
                If part.EndsWith("-") Then
                    Dim coln As Integer = Integer.Parse(part.Substring(0, part.Length - 1), Globalization.CultureInfo.InvariantCulture)
                    If index >= coln Then Return True
                ElseIf part.Contains("-") Then
                    Dim col1 As Integer = Integer.Parse(part.Substring(0, part.IndexOf("-"c)), Globalization.CultureInfo.InvariantCulture)
                    Dim col2 As Integer = Integer.Parse(part.Substring(part.IndexOf("-"c) + 1), Globalization.CultureInfo.InvariantCulture)
                    If index >= col1 AndAlso index <= col2 Then Return True
                Else
                    Dim coln As Integer = Integer.Parse(part, Globalization.CultureInfo.InvariantCulture)
                    If index = coln Then Return True
                End If
            Next
        End Function
        ''' <summary>Regular expression fo print area parsing</summary>
        Private PrintAreaParsingRegEx As New System.Text.RegularExpressions.Regex("^(?<x1>[+-]?\d+);(?<y1>[+-]?\d+)-(?<x2>[+-]?\d+);(?<y2>[+-]?\d+)$", System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
        ''' <summary>Gets print area as <see cref="Rectangle"/></summary>
        ''' <param name="x1">1st column written (1-based)</param>
        ''' <param name="x2">Last column written (1-based)</param>
        ''' <param name="y1">1st row written (1-based)</param>
        ''' <param name="y2">Last row written (1-based)</param>
        ''' <returns>Null if <see cref="PrintArea"/> is not set; print areas indicated in 1-based index otherwise</returns>
        Public Function GetPrintArea(ByVal x1%, ByVal x2%, ByVal y1%, ByVal y2%) As Nullable(Of Rectangle)
            If Me.PrintArea = "" Then Return Nothing
            Dim res As System.Text.RegularExpressions.Match = PrintAreaParsingRegEx.Match(Me.PrintArea)
            Dim pax1 As RelativeNumber = RelativeNumber.Parse(res.Groups!x1.Value)
            Dim pax2 As RelativeNumber = RelativeNumber.Parse(res.Groups!x2.Value)
            Dim pay1 As RelativeNumber = RelativeNumber.Parse(res.Groups!y1.Value)
            Dim pay2 As RelativeNumber = RelativeNumber.Parse(res.Groups!y2.Value)
            Dim ret As Rectangle
            ret.X = pax1.Combine(x1)
            ret.Y = pay1.Combine(y1)
            ret.Width = Math.Max(0, pax2.Combine(x2) - ret.X)
            ret.Height = Math.Max(0, pay2.Combine(y2) - ret.Y)
            Return ret
        End Function

        ''' <summary>Represents relative of absolute number</summary>
        <DebuggerDisplay("{ToString}")> _
        Private Structure RelativeNumber
            Private _relativity As Relativity
            ''' <summary>Gets or sets relative direction</summary>
            Public Property Relativity() As Relativity
                Get
                    Return _relativity
                End Get
                Set(ByVal value As Relativity)
                    _relativity = value
                End Set
            End Property

            Private _Value As Integer
            ''' <summary>Gets or sets value of this instance</summary>
            Public Property Value() As Integer
                Get
                    Return _Value
                End Get
                Set(ByVal value As Integer)
                    _Value = value
                End Set
            End Property
            ''' <summary>Parses <see cref="RelativeNumber"/> value form string like [+-]?\d+</summary>
            ''' <param name="value">Value in format like [+-]?\d+</param>
            ''' <exception cref="FormatException">Vallue format is invalid</exception>
            ''' <exception cref="OverflowException">Value is out of range of type <see cref="Integer"/></exception>
            Public Shared Function Parse(ByVal value As String) As RelativeNumber
                Dim ret As RelativeNumber
                If value = "0" Then
                    ret._relativity = Relativity.Plus
                    ret._Value = 0
                    Return ret
                Else
                    Select Case value(0)
                        Case "+"c : ret._relativity = Relativity.Plus
                        Case "-"c : ret._relativity = Relativity.Minus
                        Case Else
                            ret._Value = Integer.Parse(value, Globalization.CultureInfo.InvariantCulture)
                            ret._relativity = Relativity.Absolute
                            Return ret
                    End Select
                    ret._Value = Integer.Parse(value.Substring(1), Globalization.CultureInfo.InvariantCulture)
                    Return ret
                End If
            End Function
            ''' <summary>Kombines a relative and normal number</summary>
            ''' <param name="value">A number to combine</param>
            ''' <returns>Combined number - <paramref name="value"/> ± <see cref="Value"/> if <see cref="Relativity"/> is not <see cref="Relativity.Absolute"/>; <see cref="Value"/> if <see cref="Relativity"/> is <see cref="Relativity.Absolute"/></returns>
            Public Function Combine(ByVal value As Integer) As Integer
                If Relativity = SimpleXlsSettings.Relativity.Absolute Then Return Me.Value
                Return value + Me.Relativity * Me.Value
            End Function
            ''' <summary>Gets string representation of current instance</summary>
            ''' <returns>String in format [+-]\d+</returns>
            Public Overrides Function ToString() As String
                Select Case Relativity
                    Case SimpleXlsSettings.Relativity.Minus
                        If Value < 0 Then : Return "+" & (-Value).ToString(Globalization.CultureInfo.InvariantCulture)
                        ElseIf Value = 0 Then : Return "0"
                        Else : Return "-" & Value.ToString(Globalization.CultureInfo.InvariantCulture)
                        End If
                    Case SimpleXlsSettings.Relativity.Plus
                        If Value < 0 Then : Return "-" & (-Value).ToString(Globalization.CultureInfo.InvariantCulture)
                        ElseIf Value = 0 Then : Return "0"
                        Else : Return "+" & Value.ToString(Globalization.CultureInfo.InvariantCulture)
                        End If
                    Case Else
                        Return Value.ToString(Globalization.CultureInfo.InvariantCulture)
                End Select
            End Function
        End Structure

        ''' <summary>Relative directions</summary>
        Private Enum Relativity
            ''' <summary>Absolute</summary>
            Absolute = 0
            ''' <summary>Plus (right/down)</summary>
            Plus = 1
            ''' <summary>Minus (left/up)</summary>
            Minus = -1
        End Enum

        ''' <summary>Gets names of columns to skip</summary>
        ''' <returns>Names of columns to skip</returns>
        Public Function GetSkipColumns() As List(Of String)
            If SkipColumns = "" Then Return New List(Of String)
            Dim colnames As String() = SkipColumns.Split(",")
            Dim ret As New List(Of String)
            For Each col As String In colnames
                Dim NewStr As New System.Text.StringBuilder
                Dim State% = 0
                For Each ch As Char In col
                    Select Case State
                        Case 0
                            Select Case ch
                                Case "|"c : State = 1
                                Case Else : NewStr.Append(ch)
                            End Select
                        Case 1
                            Select Case ch
                                Case "|" : NewStr.Append("|"c) : State = 0
                                Case Else : NewStr.Append("," & ch) : State = 0
                            End Select
                    End Select
                Next
                ret.Add(NewStr.ToString)
            Next
            Return ret
        End Function


#Region "TypeDescriptor"

        ''' <summary>ImplemnentImplements <see cref="TypeDescriptor"/> for <see cref="SimpleXlsTemplate"/></summary>
        Friend Class SimpleXlsTypeDescriptor
            Inherits CustomTypeDescriptor
            Private ReadOnly instance As SimpleXlsSettings
            ''' <summary>CTor  - creates a new instance of the <see cref="SimpleXlsTypeDescriptor"/> class</summary>
            ''' <param name="instance">Instance to describe</param>
            ''' <param name="Parent">Original type descriptor</param>
            Public Sub New(ByVal instance As SimpleXlsSettings, ByVal parent As ICustomTypeDescriptor)
                MyBase.New(parent)
                Me.instance = instance
            End Sub
            ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
            ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
            Public Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
                Return ChangeProperties(MyBase.GetProperties())
            End Function

            ''' <summary>Alters property attributes</summary>
            ''' <param name="properties">Properties to change attributes of</param>
            ''' <returns><paramref name="properties"/> with attributes changed</returns>
            Protected Overridable Function ChangeProperties(ByVal properties As PropertyDescriptorCollection) As PropertyDescriptorCollection
                Dim ret As New List(Of PropertyDescriptor)
                For Each prp As PropertyDescriptor In properties
                    Select Case prp.Name
                        Case "AutoWidth"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_AutoWidtn),
                                New DescriptionAttribute(res.AutoWidth_d),
                                New CategoryAttribute(My.Resources.cat_Formatting)}))
                        Case "Col1"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_Col1),
                                New DescriptionAttribute(res.Col1_d),
                                New CategoryAttribute(My.Resources.cat_Location)}))
                        Case "ColumnNameRow"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_ColumnNameRow),
                                New DescriptionAttribute(res.ColumnNameRow_d),
                                New CategoryAttribute(My.Resources.cat_Location)}))
                        Case "InsertRows"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_InsertRows),
                                New DescriptionAttribute(res.InsertRows_d),
                                New CategoryAttribute(My.Resources.cat_Filling)}))
                        Case "List"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_List),
                                New DescriptionAttribute(res.List_d),
                                New CategoryAttribute(My.Resources.cat_Location)}))
                        Case "PrintArea"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_PrintArea),
                                New DescriptionAttribute(res.PrintArea_d),
                                New CategoryAttribute(My.Resources.cat_Formatting)}))
                        Case "Row1"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_Row1),
                                New DescriptionAttribute(res.Row1_d),
                                New CategoryAttribute(My.Resources.cat_Location)}))
                        Case "SkipFilled"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_SkipFilled),
                                New DescriptionAttribute(res.SkipFilled_d),
                                New CategoryAttribute(My.Resources.cat_Filling)}))
                        Case "SkipFilledNames"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_SkipFilledName),
                                New DescriptionAttribute(res.SkipFilledNames_d),
                                New CategoryAttribute(My.Resources.cat_Filling)}))
                        Case "CopyColumnsFrom"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_CopyColumnsFrom),
                                New DescriptionAttribute(My.Resources.d_CopyColumnsFrom),
                                New CategoryAttribute(My.Resources.cat_Filling)}))
                        Case "SkipColumns"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_SkipColumns),
                                New DescriptionAttribute(My.Resources.d_SkipColumns),
                                New CategoryAttribute(My.Resources.cat_Filling)}))
                        Case "NameColumn"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_NameColumns),
                                New DescriptionAttribute(My.Resources.d_NameColumn),
                                New CategoryAttribute(My.Resources.cat_Formatting)}))
                        Case "NameFormat"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_NameFormat),
                                New DescriptionAttribute(My.Resources.d_NameFormat),
                                New CategoryAttribute(My.Resources.cat_Formatting)}))
                        Case "SelectList"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_SelectList),
                                New DescriptionAttribute(My.Resources.d_SelectList),
                                New CategoryAttribute(My.Resources.cat_PostProcessing)}))
                        Case "SuspendRecalculations"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_SuspendRecalculations),
                                New DescriptionAttribute(My.Resources.d_SuspendRecalculations),
                                New CategoryAttribute(My.Resources.cat_Filling)}))
                        Case "RunMacroAfter"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_RunMacroAfter),
                                New DescriptionAttribute(My.Resources.d_RunMacroAfter),
                                New CategoryAttribute(My.Resources.cat_PostProcessing)}))
                        Case "PostProcessingCode"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_PostProcessingCode),
                                New DescriptionAttribute(My.Resources.d_PostProcessingCode & vbCrLf & "Vizte dokumentaci na WiKi!"),
                                New CategoryAttribute(My.Resources.cat_PostProcessing),
                                New EditorAttribute(GetType(MultilineStringEditor), GetType(UITypeEditor)),
                                New TypeConverterAttribute(GetType(MultilineStringConverter))}))
                        Case Else : ret.Add(prp)
                    End Select
                Next
                Return New PropertyDescriptorCollection(ret.ToArray)
            End Function
            ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
            ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
            Public Overrides Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
                Return ChangeProperties(MyBase.GetProperties(attributes))
            End Function
            ''' <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
            ''' <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
            Public Overrides Function GetAttributes() As System.ComponentModel.AttributeCollection
                Dim ret As New List(Of Attribute)
                For Each attr As Attribute In MyBase.GetAttributes()
                    ret.Add(attr)
                Next
                ret.Add(New DefaultPropertyAttribute("Name"))
                Return New AttributeCollection(ret.ToArray)
            End Function
        End Class
#End Region
    End Class

    ''' <summary>Settings of <see cref="RepeatXlsTemplate"/></summary>
    <TypeDescriptionProvider(GetType(SingleTypeDescriptionProvider(Of RepeatedXlsSettings, RepeatedXlsSettings.RepeatedXlsTypeDescriptor)))> _
    Partial Class RepeatedXlsSettings
#Region "TypeDescriptor"

        ''' <summary>A <see cref="TypeDescriptor"/> for <see cref="RepeatedXlsSettings"/></summary>
        Friend Class RepeatedXlsTypeDescriptor
            Inherits SimpleXlsSettings.SimpleXlsTypeDescriptor
            ''' <summary>CTor - creates a new instance of the <see cref="RepeatedXlsTypeDescriptor"/> class</summary>
            ''' <param name="instance">An instance to describe</param>
            ''' <param name="Parent">Original type descriptor</param>
            Public Sub New(ByVal instance As SimpleXlsSettings, ByVal parent As ICustomTypeDescriptor)
                MyBase.New(instance, parent)
            End Sub
            ''' <summary>Alters property attributes</summary>
            ''' <param name="properties">Properties to change attributes of</param>
            ''' <returns><paramref name="properties"/> with attributes changed</returns>
            Protected Overrides Function ChangeProperties(ByVal properties As PropertyDescriptorCollection) As PropertyDescriptorCollection
                Dim ret As New List(Of PropertyDescriptor)
                For Each prp As PropertyDescriptor In MyBase.ChangeProperties(properties)
                    Select Case prp.Name
                        Case "BreakColumn"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_BreakColumn),
                                New DescriptionAttribute(My.Resources.d_BreakColumn),
                                New CategoryAttribute(My.Resources.cat_Repeat)}))
                        Case "NameColumn"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_NameColumn),
                                New DescriptionAttribute(My.Resources.d_NameColumnXlsRepeat),
                                New CategoryAttribute(My.Resources.cat_Repeat)}))
                        Case "NameFormat"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_NameFormat),
                                New DescriptionAttribute(My.Resources.d_NameFormat),
                                New CategoryAttribute(My.Resources.cat_Repeat)}))
                        Case "WriteBreak"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_WriteBreak),
                                New DescriptionAttribute(My.Resources.d_WriteBreak),
                                New CategoryAttribute(My.Resources.cat_Repeat)}))
                        Case "WriteName"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_WriteName),
                                New DescriptionAttribute(My.Resources.d_WriteName),
                                New CategoryAttribute(My.Resources.cat_Repeat)}))
                        Case Else : ret.Add(prp)
                    End Select
                Next
                Return New PropertyDescriptorCollection(ret.ToArray)
            End Function
        End Class
#End Region
    End Class
End Namespace