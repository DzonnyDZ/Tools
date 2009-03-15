Imports Tools.ComponentModelT, System.ComponentModel, Tools
Imports Tools.CollectionsT.GenericT
Imports System.Windows.Forms

''' <summary>Společná báze pro <see cref="MultiControl(Of T)"/></summary>
Public MustInherit Class MultiControlBase
    ''' <summary>CTor</summary>
    Friend Sub New()
        InitializeComponent()
        Me.ItemsCount = 3
    End Sub
#Region "Properties"
    ''' <summary>Panel s ovládacími prvky</summary>
    Protected ReadOnly Property Panel() As FlowLayoutPanel
        Get
            Return flpMain
        End Get
    End Property
    Protected ReadOnly Property AddButton() As Button
        Get
            Return cmdAdd
        End Get
    End Property
    ''' <summary>Obsahuje hodnotu proměnné <see cref="NumberFormat"/></summary>
    Private _NumberFormat$ = "{0}"
    ''' <summary>Formát čísla položky, když <see cref="AutomaticLabel"/> je true.</summary>
    ''' <remarks>Zadejte ve formátu pro String.Format; {0} je nahrazena číslem položky.</remarks>
    ''' <exception cref="ArgumentNullException">Nastavovaná hodnota je null</exception>
    ''' <exception cref="FormatException">The format item in format is invalid.  -or- The number indicating an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <DefaultValue("{0}")> _
    <Description("Formát čísla položky, když AutomaticLabel je true. Zadejte ve formátu pro String.Format; {0} je nahrazena číslem položky.")> _
    Public Property NumberFormat$()
        Get
            Return _NumberFormat
        End Get
        Set(ByVal value$)
            String.Format(value, 0)
            _NumberFormat = value
        End Set
    End Property
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="AutomaticLabel"/></summary>
    Private _AutomaticLabel As Boolean = True
    ''' <summary>Určuje jestli text labelů je generován automaticky podle <see cref="NumberFormat"/></summary>
    <Description("Určuje jestli text labelů je generován automaticky podle NumberFormat")> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
    <DefaultValue(True)> _
    Public Property AutomaticLabel() As Boolean
        'ATTR: <DebuggerStepThrough > 
        Get
            Return _AutomaticLabel
        End Get
        Set(ByVal value As Boolean)
            Dim Changed As Boolean = value <> AutomaticLabel
            _AutomaticLabel = value
            If Changed AndAlso AutomaticLabel Then
                Dim i%
                For Each item As MultiControlItem In Me.MultiControlItems
                    item.LabelText = String.Format(Me.NumberFormat, i + 1)
                    i += 1
                Next
            End If
        End Set
    End Property
    ''' <summary>Získá nebo nastaví směr toku ovládacích prvků</summary>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Layout)> _
    <Description("Získá nebo nastaví směr toku ovládacích prvků")> _
    <DefaultValue(GetType(FlowDirection), "LeftToRight")> _
    Public Property FlowDirection() As FlowDirection
        <DebuggerStepThrough()> Get
            Return flpMain.FlowDirection
        End Get
        <DebuggerStepThrough()> Set(ByVal value As FlowDirection)
            flpMain.FlowDirection = value
        End Set
    End Property

    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="AddState"/></summary>
    Private _AddState As ControlState = ControlState.Enabled
    ''' <summary>Stav tlačítka přidat</summary>
    ''' <exception cref="InvalidEnumArgumentException">Nastavovaná hodnota není členem <see cref="ControlState"/></exception>
    <DefaultValue(GetType(ControlState), "Enabled")> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Stav tlačítka přidat")> _
    Public Property AddState() As ControlState
        <DebuggerStepThrough()> Get
            Return _AddState
        End Get
        Set(ByVal value As ControlState)
            If Not Tools.TypeTools.InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
            _AddState = value
            Select Case AddState
                Case ControlState.Enabled : cmdAdd.Visible = True : cmdAdd.Enabled = True
                Case ControlState.Disabled : cmdAdd.Visible = True : cmdAdd.Enabled = False
                Case ControlState.Hidden : cmdAdd.Visible = False
            End Select
        End Set
    End Property
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="MinimumItems"/></summary>
    Private _MinimumItems% = 3
    ''' <summary>Minimální počet položek, na kolik jej může snížit uživatel</summary>
    ''' <remarks>Programově lze nastavit i menší počet položek</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
    <DefaultValue(3I)> _
    <Description("Minimální počet položek, na kolik jej může snížit uživatel")> _
    Public Property MinimumItems%()
        <DebuggerStepThrough()> Get
            Return _MinimumItems
        End Get
        <DebuggerStepThrough()> Set(ByVal value%)
            _MinimumItems = value
        End Set
    End Property
    ''' <summary>Stav odebících tlačítek všech prvků</summary>
    ''' <returns>Stav odebíracích tlačítek všech prvků, pokud je schodný. Null pokud není schodný nebo je prvků nula.</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Stav odebících tlačítek všech prvků")> _
    <DefaultValue(GetType(Nullable(Of ControlState)), "Enabled")> _
    Public Property AllRemoveState() As Nullable(Of ControlState)
        Get
            Dim Value As Nullable(Of ControlState)
            Dim i%
            For Each item As MultiControlItem In Me.MultiControlItems
                If i = 0 Then
                    Value = item.RemoveButtonState
                ElseIf Value.Value <> item.RemoveButtonState Then
                    Return Nothing
                End If
            Next
            Return Value
        End Get
        Set(ByVal value As Nullable(Of ControlState))
            If value.HasValue Then
                For Each item As MultiControlItem In Me.MultiControlItems
                    item.RemoveButtonState = value.Value
                Next
            End If
        End Set
    End Property
#Region "Standard Control propeties"
    ''' <summary>Gets or sets the border style of the tree view control.</summary>
    ''' <returns>One of the <see cref="System.Windows.Forms.BorderStyle"/> values. The default is <see cref="System.Windows.Forms.BorderStyle.Fixed3D"/>.</returns>
    ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="System.Windows.Forms.BorderStyle"/> values.</exception>
    <ResourcesT.SRCategory("CatAppearance"), DefaultValue(2), ResourcesT.SRDescription("UserControlBorderStyleDescr")> _
    Shadows Property BorderStyle() As BorderStyle
        <DebuggerStepThrough()> Get
            Return MyBase.BorderStyle
        End Get
        <DebuggerStepThrough()> Set(ByVal value As BorderStyle)
            MyBase.BorderStyle = value
        End Set
    End Property

#End Region
#End Region
#Region "Controls"
    ''' <summary>Počet položek v kolekci Items</summary>
    ''' <remarks>Vlastnost musí fungovat dřív než proběhne CTor!</remarks>
    ''' <exception cref="ArgumentOutOfRangeException">Nastavovaná hodnota je menší než 0</exception>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <Description("Počet položek v kolekci Items")> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <DefaultValue(3I)> _
    Public MustOverride Property ItemsCount%()
    ''' <summary>Creates new intem</summary>
    ''' <returns>New item</returns>
    Protected MustOverride Function NewItem() As MultiControlItem
    ''' <summary>Gets items of this control in type-semi-safe way</summary>
    Public MustOverride ReadOnly Property MultiControlItems() As IList(Of MultiControlItem)
    ''' <summary>Gets or sets item at specified index</summary>
    ''' <param name="index">Index to get or set item at (may be in range 0 to <see cref="ItemsCount"/> -1 (for get) <see cref="ItemsCount"/> (for set))</param>
    ''' <remarks>When setting and <paramref name="index"/> is <see cref="ItemsCount"/> item is added</remarks>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than <see cref="ItemsCount"/></exception>
    ''' <exception cref="TypeMismatchException">Value being set is not of type expected type</exception>
    ''' <exception cref="OperationCanceledException">Value being set is null</exception>
    Public MustOverride Property Item(ByVal index%) As MultiControlItem

    ''' <summary>Prřidává a odebírá ovladače událostí položek</summary>
    ''' <param name="Add">True pro přidání, false pro odebrání</param>
    ''' <param name="Item">Položka</param>
    ''' <remarks>tato implementace nedělá nic</remarks>
    Protected Overridable Sub Handlers(ByVal Add As Boolean, ByVal Item As MultiControlItem)
    End Sub
#Region "FindItemByControl"
    ''' <summary>Nalezne položku kolekce <see cref="MultiControlItems"/> podle <see cref="P:EOS.Dohledávač.MultiControlItem.Control"/></summary>
    ''' <param name="Control"><see cref="Control"/> k hledání</param>
    ''' <returns>Naelezená položka nebo null</returns>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Function FindItemByControl(ByVal Control As Control) As MultiControlItem
        For Each item As MultiControlItem In MultiControlItems
            If item.Control Is Control Then Return item
        Next
        Return Nothing
    End Function
    ''' <summary>Nalezne položku kolekce <see cref="MultiControlItems"/> podle <see cref="P:EOS.Dohledávač.MultiControlItem.Label "/></summary>
    ''' <param name="Label"><see cref="Label"/> k hledání</param>
    ''' <returns>Naelezená položka nebo null</returns>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Function FindItemByControl(ByVal Label As Label) As MultiControlItem
        For Each item As MultiControlItem In MultiControlItems
            If item.Label Is Label Then Return item
        Next
        Return Nothing
    End Function
    ''' <summary>Nalezne položku kolekce <see cref="MultiControlItems"/> podle <see cref="P:EOS.Dohledávač.MultiControlItem.Panel"/></summary>
    ''' <param name="Panel"><see cref="TableLayoutPanel"/> k hledání</param>
    ''' <returns>Naelezená položka nebo null</returns>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Function FindItemByControl(ByVal Panel As TableLayoutPanel) As MultiControlItem
        For Each item As MultiControlItem In MultiControlItems
            If item.Panel Is Panel Then Return item
        Next
        Return Nothing
    End Function
    ''' <summary>Nalezne položku kolekce <see cref="MultiControlItems"/> podle <see cref="P:EOS.Dohledávač.MultiControlItem.RemoveButton"/></summary>
    ''' <param name="RemoveButton"><see cref="TableLayoutPanel"/> k hledání</param>
    ''' <returns>Naelezená položka nebo null</returns>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Function FindItemByControl(ByVal RemoveButton As Button) As MultiControlItem
        For Each item As MultiControlItem In MultiControlItems
            If item.RemoveButton Is RemoveButton Then Return item
        Next
        Return Nothing
    End Function
#End Region
#End Region
#Region "Event handlers"
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            MultiControlItems.Add(NewItem)
        Catch ex As OperationCanceledException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Přidat")
            Exit Sub
        End Try
        MultiControlItems(MultiControlItems.Count - 1).Control.Select()
    End Sub
#End Region
End Class

''' <summary>RTeprezentuje položku MultiControlu</summary>
Public MustInherit Class MultiControlItem
    ''' <summary>CTor</summary>
    Public Sub New()
        _Control = CreateControl()
        _Label = New Label
        _RemoveButton = New Button
        _Panel = New TableLayoutPanel
        With Label
            .TabIndex = 0
            .AutoSize = True
            .Anchor = AnchorStyles.Right
            .Margin = New Padding(0, 0, 0, 0)
        End With
        With Control
            .TabIndex = 1
            .Anchor = AnchorStyles.Left Or AnchorStyles.Right
            .Margin = New Padding(0, 0, 0, 0)
        End With
        With RemoveButton
            .Text = ""
            .Image = My.Resources.DeleteHS
            .AutoSize = True
            .Dock = DockStyle.Right
            .AutoSizeMode = AutoSizeMode.GrowAndShrink
            .Anchor = AnchorStyles.None
        End With
        With Panel
            .RowCount = 1
            .ColumnCount = 3
            .Margin = New Padding(0)
            .Controls.Add(Label, 0, 0)
            .Controls.Add(Control, 1, 0)
            .Controls.Add(RemoveButton, 2, 0)
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            .ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            .RowStyles.Add(New RowStyle(SizeType.AutoSize))
            .AutoSize = True
            .AutoSizeMode = AutoSizeMode.GrowAndShrink
            .Anchor = AnchorStyles.None
            .BorderStyle = BorderStyle.FixedSingle
        End With
    End Sub
    ''' <summary>Vytvoří ovládací prvek</summary>
    Protected MustOverride Function CreateControl() As Control
#Region "Contrtols"
    ''' <summary>Contains value of the <see cref="Control"/> property</summary>
    Private WithEvents _Control As Control
    ''' <summary>Contains value of the <see cref="Label"/> property</summary>
    Private WithEvents _Label As Label
    ''' <summary>Contains value of the <see cref="Panel"/> property</summary>
    Private WithEvents _Panel As TableLayoutPanel
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RemoveButton"/></summary>
    Private WithEvents _RemoveButton As Button
    ''' <summary>Tlačítko pro odstranění položky</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Protected Friend ReadOnly Property RemoveButton() As Button
        <DebuggerStepThrough()> Get
            Return _RemoveButton
        End Get
    End Property
    ''' <summary>Control that represents current instance</summary>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    Public ReadOnly Property Control() As Control
        <DebuggerStepThrough()> Get
            Return _Control
        End Get
    End Property
    ''' <summary>Lable serving as title of current instance</summary>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    Public ReadOnly Property Label() As Label
        <DebuggerStepThrough()> Get
            Return _Label
        End Get
    End Property
    ''' <summary>Panel holding controls for current instance</summary>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    Public ReadOnly Property Panel() As TableLayoutPanel
        <DebuggerStepThrough()> Get
            Return _Panel
        End Get
    End Property

#End Region
#Region "Properties"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RemoveButton"/></summary>
    Private _RemoveButtonState As ControlState = ControlState.Enabled
    ''' <summary>Stav tlačítka odebrat</summary>
    ''' <exception cref="InvalidEnumArgumentException">Nastavovaná hodnota není členem <see cref="ControlState"/>.</exception>
    ''' <remarks>Poznámka pro dědice: Vždy nastavte bázovou vlasnost.</remarks>
    <DefaultValue(GetType(ControlState), "Enabled")> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Stav tlačítka odebrat")> _
    Public Property RemoveButtonState() As ControlState
        <DebuggerStepThrough()> Get
            Return _RemoveButtonState
        End Get
        Set(ByVal value As ControlState)
            If Not TypeTools.InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
            _RemoveButtonState = value
            OnRemoveButtonStateChanged()
        End Set
    End Property
    ''' <summary>efektivní stav tlačítka <see cref="RemoveButton"/></summary>
    Protected ReadOnly Property RemoveButtonStateEffective() As ControlState
        Get
            Select Case RemoveButtonState
                Case ControlState.Disabled
                    Return ControlState.Disabled
                Case ControlState.Enabled
                    Select Case RemoveButtonStateOverride
                        Case ControlState.Disabled, ControlState.Hidden : Return ControlState.Disabled
                        Case ControlState.Enabled : Return ControlState.Enabled
                    End Select
                Case ControlState.Hidden
                    Return ControlState.Hidden
            End Select

        End Get
    End Property
    ''' <summary>Volána, když se hodnota vlastnosti <see cref="RemoveButtonStateEffective"/> změní</summary>
    Protected Overridable Sub OnRemoveButtonStateChanged()
        Select Case RemoveButtonStateEffective
            Case ControlState.Disabled : RemoveButton.Enabled = False : RemoveButton.Visible = True
            Case ControlState.Enabled : RemoveButton.Enabled = True : RemoveButton.Visible = True
            Case ControlState.Hidden : RemoveButton.Visible = False
        End Select
    End Sub

    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RemoveButton"/></summary>
    Friend _RemoveButtonInternal As ControlState = ControlState.Enabled
    ''' <summary>Stav tlačítka odebrat (interní)</summary>
    ''' <exception cref="InvalidEnumArgumentException">Nastavovaná hodnota není členem <see cref="ControlState"/>.</exception>
    ''' <remarks>Poznámka pro dědice: Vždy nastavte bázovou vlasnost.</remarks>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
    Friend Property RemoveButtonStateOverride() As ControlState
        <DebuggerStepThrough()> Get
            Return _RemoveButtonInternal
        End Get
        Set(ByVal value As ControlState)
            If Not TypeTools.InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
            _RemoveButtonInternal = value
            OnRemoveButtonStateChanged()
        End Set
    End Property

    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="GenericOwner"/></summary>
    Private WithEvents _GenericOwner As MultiControlBase
    ''' <summary>Vlastník položky</summary>
    ''' <returns>Vlastník položky, nebo null</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <Browsable(False)> _
    Public Overridable Property GenericOwner() As MultiControlBase
        <DebuggerStepThrough()> Get
            Return _GenericOwner
        End Get
        <DebuggerStepThrough()> Set(ByVal value As MultiControlBase)
            Dim Old As MultiControlBase = GenericOwner
            _GenericOwner = value
            If value IsNot Old Then OnOwnerChanged(Old)
        End Set
    End Property
    ''' <summary>Válána po změně vlastnosti <see cref="GenericOwner"/></summary>
    ''' <param name="OldOwner">Původní vlastník; může být null</param>
    Protected Overridable Sub OnOwnerChanged(ByVal OldOwner As MultiControlBase)
        RemoveButton.Enabled = GenericOwner IsNot Nothing AndAlso RemoveButtonState = ControlState.Enabled
    End Sub
    ''' <summary>Text návěští</summary>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Text návěští")> _
    Public Property LabelText$()
        <DebuggerStepThrough()> Get
            Return Label.Text
        End Get
        <DebuggerStepThrough()> Set(ByVal value$)
            Label.Text = value
        End Set
    End Property
    ''' <summary>Vrací hodnotu určující jestli hodnota vlastnosti <see cref="LabelText"/> má být serializována</summary>
    ''' <returns>True pokud text není generován automaticky a je různý od ""</returns>
    Private Function ShouldSerializeLabeltext() As Boolean
        If GenericOwner Is Nothing Then
            Return LabelText <> ""
        ElseIf Not GenericOwner.AutomaticLabel Then
            Return LabelText <> String.Format(GenericOwner.NumberFormat, Index)
        Else
            Return False
        End If
    End Function
    ''' <summary>Nastaví hodnotu vlastnosti <see cref="LabelText"/> na výchozí</summary>
    ''' <remarks>Pokud hodnota textu není generována automaticky, nastaví ji na "", jinak nedělá nic.</remarks>
    Private Sub ResetLabelText()
        If GenericOwner Is Nothing Then
            LabelText = ""
        ElseIf Not GenericOwner.AutomaticLabel Then
            LabelText = String.Format(GenericOwner.NumberFormat, Index)
        End If
    End Sub
    ''' <summary>Vrací index této položky vrámci svého vlastníka</summary>
    ''' <returns>Index položky vrámci vlasníka; -1 pokud <see cref="GenericOwner"/> je null</returns>
    <Browsable(False)> _
    Public ReadOnly Property Index() As Integer
        Get
            If Me.GenericOwner Is Nothing Then Return -1
            Return GenericOwner.MultiControlItems.IndexOf(Me)
        End Get
    End Property
#End Region
    Private Sub Label_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Label.TextChanged
        If GenericOwner IsNot Nothing AndAlso GenericOwner.AutomaticLabel Then
            Dim text$ = String.Format(GenericOwner.NumberFormat, Index + 1)
            If LabelText <> text Then LabelText = text
        End If
    End Sub
    ''' <summary>Volána před tím než se uživatel pokusí odebrat tento ovládací prvek</summary>
    Public Event Removing As CancelEventHandler
    ''' <summary>Vyvolává událost <see cref="Removing"/></summary>
    Protected Overridable Sub OnRemoving(ByVal e As CancelEventArgs)
        If Me.GenericOwner Is Nothing Then
            e.Cancel = True
        ElseIf Me.GenericOwner.ItemsCount <= Me.GenericOwner.MinimumItems Then
            e.Cancel = True
        End If
        RaiseEvent Removing(Me, e)
    End Sub
    ''' <summary>Odstraní ovládací prvek z jeho rodiče <see cref="GenericOwner"/></summary>
    ''' <remarks>Tuto akci nelze stornovat pomocí <see cref="OnRemoving"/> ani <see cref="Removing"/></remarks>
    ''' <exception cref="InvalidOperationException"><see cref="GenericOwner"/> is null</exception>
    Public Sub Remove()
        If Me.GenericOwner Is Nothing Then Throw New InvalidOperationException("Nemohu odstranit ovládací prvek, protože není znám jeho rodič")
        Me.GenericOwner.MultiControlItems.Remove(Me)
    End Sub
    Private Sub RemoveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _RemoveButton.Click
        Dim e2 As New CancelEventArgs
        If Me.GenericOwner IsNot Nothing Then
            OnRemoving(e2)
            If Not e2.Cancel Then
                Remove()
            End If
        End If
    End Sub
End Class