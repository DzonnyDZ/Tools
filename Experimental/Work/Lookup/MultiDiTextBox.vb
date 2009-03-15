Imports Tools.ComponentModelT, System.ComponentModel, Tools
Imports Tools.CollectionsT.GenericT
Imports System.Windows.Forms

''' <summary>Control consisiting of multiple <see cref="DiTextBox">DiTextBoxes</see></summary>
Public Class MultiDiTextBox
    Inherits MultiControl(Of MultiDiTextBoxItem)
#Region "Group properties"
    ''' <summary>Text návěští 1. text boxu všech položek</summary>
    ''' <returns>Text návěští 1. text boxu všech položek; null pokud se liší.</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Text návěští 1. text boxu všech položek")> _
    Public Property Label1$()
        Get
            Dim value As String = Nothing
            Dim Got As Boolean
            For Each item As MultiDiTextBoxItem In Me.Items
                If Not Got Then
                    value = item.LabelText1
                    Got = True
                ElseIf item.LabelText1 <> value Then
                    Return Nothing
                End If
            Next
            Return value
        End Get
        Set(ByVal value$)
            For Each item As MultiDiTextBoxItem In Me.Items
                item.LabelText1 = value
            Next
        End Set
    End Property
    ''' <summary>Text návěští 2. text boxu všech položek</summary>
    ''' <returns>Text návěští 2. text boxu všech položek; null pokud se liší.</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Text návěští 2. text boxu všech položek")> _
    Public Property Label2$()
        Get
            Dim value As String = Nothing
            Dim Got As Boolean
            For Each item As MultiDiTextBoxItem In Me.Items
                If Not Got Then
                    value = item.LabelText2
                    Got = True
                ElseIf item.LabelText2 <> value Then
                    Return Nothing
                End If
            Next
            Return value
        End Get
        Set(ByVal value$)
            For Each item As MultiDiTextBoxItem In Me.Items
                item.LabelText2 = value
            Next
        End Set
    End Property
#End Region
#Region "CollectionEvents"
    Protected Overrides Sub OnItemAdded(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of MultiDiTextBoxItem).ItemIndexEventArgs)
        MyBase.OnItemAdded(e)
        SetLabelsByDefault(e.Item)
    End Sub
    ''' <summary>Nsastaví výchozí texty labelů, pokud je všechny dosavadní mají stejné</summary>
    Private Sub SetLabelsByDefault(ByVal TargetItem As MultiDiTextBoxItem)
        If Me.Items.Count > 1 Then
            Dim same1 As Boolean = True, same2 As Boolean = True
            Dim value1$ = Nothing, value2$ = Nothing
            Dim i%
            For Each item As MultiDiTextBoxItem In Me.Items
                If item IsNot TargetItem Then
                    If i = 0 Then value1 = item.LabelText1 : value2 = item.LabelText2 _
                    Else same1 = item.LabelText1 = value1 : same2 = item.LabelText2 = value2
                    If Not same1 AndAlso Not same2 Then Exit For
                    i += 1
                End If
            Next
            If same1 Then TargetItem.LabelText1 = value1
            If same2 Then TargetItem.LabelText1 = value2
        End If
    End Sub
    Protected Overrides Sub OnItemChanged(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of MultiDiTextBoxItem).OldNewItemEventArgs)
        MyBase.OnItemChanged(e)
        If e.OldItem IsNot e.Item Then SetLabelsByDefault(e.Item)
    End Sub
#End Region
#Region "Events"
    Protected Overrides Sub Handlers(ByVal Add As Boolean, ByVal Item As MultiDiTextBoxItem)
        MyBase.Handlers(Add, Item)
        If Add Then
            AddHandler Item.TextBox1.TextChanged, AddressOf TextBox_TextChanged
            AddHandler Item.TextBox2.TextChanged, AddressOf TextBox_TextChanged
        Else
            RemoveHandler Item.TextBox1.TextChanged, AddressOf TextBox_TextChanged
            RemoveHandler Item.TextBox2.TextChanged, AddressOf TextBox_TextChanged
        End If
    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim i% = 0
        For Each item As MultiDiTextBoxItem In Me.Items
            If item.TextBox1 Is sender Then
                OnTextBox1TextChanged(New ListWithEvents(Of MultiDiTextBoxItem).ItemIndexEventArgs(item, i))
                Exit Sub
            End If
            If item.TextBox2 Is sender Then
                OnTextBox2TextChanged(New ListWithEvents(Of MultiDiTextBoxItem).ItemIndexEventArgs(item, i))
                Exit Sub
            End If
            i += 1
        Next
    End Sub
    ''' <summary>Raises the <see cref="TextBox1TextChanged"/> event</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnTextBox1TextChanged(ByVal e As ListWithEvents(Of MultiDiTextBoxItem).ItemIndexEventArgs)
        RaiseEvent TextBox1TextChanged(Me, e)
    End Sub
    ''' <summary>Raises the <see cref="TextBox2TextChanged"/> event</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnTextBox2TextChanged(ByVal e As ListWithEvents(Of MultiDiTextBoxItem).ItemIndexEventArgs)
        RaiseEvent TextBox2TextChanged(Me, e)
    End Sub
    ''' <summary>Raised when text of any textbox1 changes</summary>
    <Description("Vyvolána, když se změní text libovolného textboxu 1")> _
    <Category("Property Changed")> _
    Public Event TextBox1TextChanged As EventHandler(Of ListWithEvents(Of MultiDiTextBoxItem).ItemIndexEventArgs)
    ''' <summary>Raised when text of any textbox2 changes</summary>
    <Description("Vyvolána, když se změní text libovolného textboxu 2")> _
    <Category("Property Changed")> _
    Public Event TextBox2TextChanged As EventHandler(Of ListWithEvents(Of MultiDiTextBoxItem).ItemIndexEventArgs)
#End Region
    ''' <summary>Item of <see cref="MultiDiTextBox"/> control</summary>
    Public Class MultiDiTextBoxItem : Inherits MultiControlItemWithOwner(Of DiTextBox, MultiDiTextBox)
#Region "Quick properties"
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <Description("Popisek textového pole 1")> _
        <DefaultValue("Label1")> _
        Public Property LabelText1$()
            Get
                Return Label1.Text
            End Get
            Set(ByVal value$)
                Label1.Text = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <Description("Popisek textového pole 2")> _
        <DefaultValue("Label2")> _
        Public Property LabelText2$()
            Get
                Return Label2.Text
            End Get
            Set(ByVal value$)
                Label2.Text = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <Description("Text textového pole 1")> _
        <DefaultValue("")> _
        Public Property Text1$()
            Get
                Return TextBox1.Text
            End Get
            Set(ByVal value$)
                TextBox1.Text = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <Description("Text textového pole 2")> _
        <DefaultValue("")> _
        Public Property Text2$()
            Get
                Return TextBox2.Text
            End Get
            Set(ByVal value$)
                TextBox2.Text = value
            End Set
        End Property
#End Region
#Region "Controls"
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        Public ReadOnly Property DiTextBox() As DiTextBox
            Get
                Return Control
            End Get
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        Public ReadOnly Property Label1() As Windows.Forms.Label
            Get
                Return DiTextBox.Label1
            End Get
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        Public ReadOnly Property Label2() As Windows.Forms.Label
            Get
                Return DiTextBox.Label2
            End Get
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        Public ReadOnly Property TextBox1() As TextBox
            Get
                Return DiTextBox.TextBox1
            End Get
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        Public ReadOnly Property TextBox2() As TextBox
            Get
                Return DiTextBox.TextBox2
            End Get
        End Property
#End Region
    End Class
End Class
