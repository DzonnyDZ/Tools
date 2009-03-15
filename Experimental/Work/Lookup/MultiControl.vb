Imports Tools.ComponentModelT, System.ComponentModel, Tools
Imports Tools.CollectionsT.GenericT
Imports System.Windows.Forms

''' <summary>Ovládací prvek skládající se z ovládacích prvků</summary>
Public Class MultiControl(Of TItem As {New, MultiControlItem})
    Inherits MultiControlBase
#Region "Controls"
    ''' <summary>Prřidává a odebírá ovladače událostí položek</summary>
    ''' <param name="Add">True pro přidání, false pro odebrání</param>
    ''' <param name="Item">Položka</param>
    ''' <remarks>tato implementace nedělá nic</remarks>
    ''' <exception cref="TypeMismatchException"><paramref name="Item"/> is not <typeparamref name="TItem"/></exception>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Protected NotOverridable Overloads Overrides Sub Handlers(ByVal Add As Boolean, ByVal Item As MultiControlItem)
        If Not TypeOf Item Is TItem Then Throw New TypeMismatchException(Item, GetType(TItem), "item")
        Handlers(Add, DirectCast(Item, TItem))
    End Sub
    ''' <summary>Prřidává a odebírá ovladače událostí položek</summary>
    ''' <param name="Add">True pro přidání, false pro odebrání</param>
    ''' <param name="Item">Položka</param>
    ''' <remarks>tato implementace nedělá nic</remarks>
    Protected Overridable Overloads Sub Handlers(ByVal Add As Boolean, ByVal item As TItem)
        MyBase.Handlers(Add, item)
    End Sub

    ''' <summary>Creates new item for the <see cref="Items"/> collection</summary>
    ''' <returns>New instance of type <typeparamref name="TItem"/></returns>
    ''' <remarks>Use and override <see cref="GetNewItem"/> instead</remarks>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Protected NotOverridable Overrides Function NewItem() As MultiControlItem
        Return New TItem
    End Function
    ''' <summary>Creates new item for the <see cref="Items"/> collection</summary>
    ''' <returns>New instance of type <typeparamref name="TItem"/></returns>
    Public Overridable Overloads Function GetNewItem() As TItem
        Return New TItem
    End Function

    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Items"/></summary>
    Private WithEvents _Items As ListWithEvents(Of TItem)
    ''' <summary>Položky obsažené v MultiControlu</summary>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Položky obsažené v MultiControlu")> _
    Public ReadOnly Property Items() As ListWithEvents(Of TItem)
        <DebuggerStepThrough()> Get
            If _Items Is Nothing Then _Items = New ListWithEvents(Of TItem)
            Return _Items
        End Get
    End Property
    ''' <summary>Gets or sets item at specified index</summary>
    ''' <param name="index">Index to get or set item at (may be in range 0 to <see cref="ItemsCount"/> -1 (for get) <see cref="ItemsCount"/> (for set))</param>
    ''' <remarks>When setting and <paramref name="index"/> is <see cref="ItemsCount"/> item is added</remarks>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than <see cref="ItemsCount"/></exception>
    ''' <exception cref="TypeMismatchException">Value being set is not of type <typeparamref name="TItem"/></exception>
    ''' <exception cref="OperationCanceledException">Value being set is null</exception>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public Overrides Property Item(ByVal index As Integer) As MultiControlItem
        Get
            Return Items(index)
        End Get
        Set(ByVal value As MultiControlItem)
            If Not TypeOf value Is TItem Then Throw New TypeMismatchException(value, GetType(TItem), "value")
            If index = ItemsCount Then
                Items.Add(DirectCast(value, TItem))
            Else
                Items(index) = value
            End If
        End Set
    End Property
#Region "Items events"
    Private Sub Items_Adding(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).CancelableItemIndexEventArgs) Handles _Items.Adding
        OnItemAdding(e)
    End Sub
    Protected Overridable Sub OnItemAdding(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).CancelableItemIndexEventArgs)
        If e.Item Is Nothing Then
            e.Cancel = True
            e.CancelMessage = "Do kolekce Items nelze přidat hodnoty null."
        ElseIf e.Item.GenericOwner IsNot Nothing Then
            e.Cancel = True
            e.CancelMessage = "Do kolekce Items nelze přidat položku, které již je v jiné kolekci"
        End If
    End Sub
    Private Sub Items_Added(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemIndexEventArgs) Handles _Items.Added
        OnItemAdded(e)
    End Sub
    Protected Overridable Sub OnItemAdded(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemIndexEventArgs)
        Panel.Controls.Add(e.Item.Panel)
        Panel.Controls.SetChildIndex(e.Item.Panel, e.Index)
        e.Item.GenericOwner = Me
        If Me.AutomaticLabel Then e.Item.LabelText = String.Format(Me.NumberFormat, e.Index + 1)
        Handlers(True, e.Item)
        CheckNumberOfItems()
        RaiseEvent ItemAdded(Me, e)
    End Sub
    ''' <summary>Raised when an item is added to the <see cref="Items"/> collection</summary>
    <Description("Vyvolána když je přidána položka do kolekce Items.")> _
    <Category("Action")> _
    Public Event ItemAdded As EventHandler(Of Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemIndexEventArgs)
    Private Sub _Items_Clearing(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem), ByVal e As Tools.ComponentModelT.CancelMessageEventArgs) Handles _Items.Clearing
        OnItemsClearing(e)
    End Sub
    Protected Overridable Sub OnItemsClearing(ByVal e As Tools.ComponentModelT.CancelMessageEventArgs)
    End Sub
    Private Sub Items_Cleared(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemsEventArgs) Handles _Items.Cleared
        OnItemsCleared(e)
    End Sub
    Protected Overridable Sub OnItemsCleared(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemsEventArgs)
        Panel.Controls.Clear()
        Panel.Controls.Add(AddButton)
        For Each item As TItem In e.Items
            Handlers(False, item)
        Next
        CheckNumberOfItems()
        RaiseEvent ItemsCleared(Me, e)
    End Sub
    ''' <summary>Raised when the <see cref="Items"/> collection is cleared</summary>
    <Description("Vyvolána když je kolekce Items vyčištěna")> _
    <Category("Action")> _
    Public Event ItemsCleared As EventHandler(Of Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemsEventArgs)
    Private Sub Items_ItemChanging(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).CancelableItemIndexEventArgs) Handles _Items.ItemChanging
        OnItemChanging(e)
    End Sub
    Protected Overridable Sub OnItemChanging(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).CancelableItemIndexEventArgs)
        If e.Item Is Nothing Then
            e.Cancel = True
            e.CancelMessage = "Do kolekce Items nelze přidat hodnoty null."
        ElseIf e.Item.GenericOwner IsNot Nothing AndAlso (e.Item.GenericOwner IsNot Me OrElse e.NewIndex <> e.Item.Index) Then
            e.Cancel = True
            e.CancelMessage = "Do kolekce Items nelze přidat položku, které již je v jiné kolekci"
        End If
    End Sub
    Private Sub Items_ItemChanged(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).OldNewItemEventArgs) Handles _Items.ItemChanged
        OnItemChanged(e)
    End Sub
    Protected Overridable Sub OnItemChanged(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).OldNewItemEventArgs)
        Dim OldIndex% = Panel.Controls.IndexOf(e.OldItem.Panel)
        Panel.Controls.Remove(e.OldItem.Panel)
        Panel.Controls.Add(e.Item.Panel)
        Panel.Controls.SetChildIndex(e.Item.Panel, OldIndex)
        e.OldItem.GenericOwner = Nothing
        e.Item.GenericOwner = Me
        If Me.AutomaticLabel Then e.Item.LabelText = String.Format(Me.NumberFormat, e.Index + 1)
        Handlers(False, e.OldItem)
        Handlers(True, e.Item)
        RaiseEvent ItemReplaced(Me, e)
    End Sub
    ''' <summary>Raised when item in the <see cref="Items"/> collection is replaced by another one</summary>
    <Description("Vyvolána pokud je položka v kolekci Items nahrazena jinou.")> _
    <Category("Action")> _
    Public Event ItemReplaced As EventHandler(Of Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).OldNewItemEventArgs)
    Private Sub Items_Removing(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).CancelableItemIndexEventArgs) Handles _Items.Removing
        OnItemRemoving(e)
    End Sub
    Protected Overridable Sub OnItemRemoving(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).CancelableItemIndexEventArgs)
    End Sub
    Private Sub Items_Removed(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemIndexEventArgs) Handles _Items.Removed
        OnItemRemoved(e)
    End Sub
    Protected Overridable Sub OnItemRemoved(ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemIndexEventArgs)
        Panel.Controls.Remove(e.Item.Panel)
        e.Item.GenericOwner = Nothing
        Handlers(False, e.Item)
        CheckNumberOfItems()
        RaiseEvent ItemRemoved(Me, e)
    End Sub
    ''' <summary>Raised when an item is removed from the <see cref="Items"/> collection</summary>
    <Description("Vyvolána, když je z kolekce Items odstraněna položka.")> _
    <Category("Action")> _
    Public Event ItemRemoved As EventHandler(Of Tools.CollectionsT.GenericT.ListWithEvents(Of TItem).ItemIndexEventArgs)
    Private Sub CheckNumberOfItems()
        If Me.ItemsCount <= Me.MinimumItems Then
            For Each Item As MultiControlItem In Me.Items
                Item.RemoveButtonStateOverride = ControlState.Disabled
            Next
        Else
            For Each Item As MultiControlItem In Me.Items
                Item.RemoveButtonStateOverride = Nothing
            Next
        End If
    End Sub
#End Region

    ''' <summary>Počet položek v kolekci Items</summary>
    ''' <remarks>Vlastnost musí fungovat dřív než proběhne CTor!</remarks>
    ''' <exception cref="ArgumentOutOfRangeException">Nastavovaná hodnota je menší než 0</exception>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <Description("Počet položek v kolekci Items")> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <DefaultValue(3I)> _
    Public Overrides Property ItemsCount%()
        <DebuggerStepThrough()> Get
            Return Items.Count
        End Get
        Set(ByVal value%)
            Select Case value
                Case Is < 0 : Throw New ArgumentOutOfRangeException("Negative number of items is not allowed")
                Case Is > Items.Count
                    While value > Items.Count
                        Items.Add(GetNewItem)
                    End While
                Case Is < Items.Count
                    While value < Items.Count
                        Items.RemoveAt(Items.Count - 1)
                    End While
            End Select
        End Set
    End Property
    ''' <summary>Gets <see cref="Items"/> in type-semi-safe way</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public Overrides ReadOnly Property MultiControlItems() As IList(Of MultiControlItem)
        Get
            Return New CollectionsT.GenericT.ListWrapper(Of MultiControlItem)(Items)
        End Get
    End Property
#End Region
End Class

''' <summary>Stavy ovládacího prvku</summary>
Public Enum ControlState
    ''' <summary>Povolený</summary>
    Enabled
    ''' <summary>Zakázaný</summary>
    Disabled
    ''' <summary>neviditelný</summary>
    Hidden
End Enum

#Region "Generic items"
''' <summary><see cref="MultiControlItem"/> s určeným ovládacím prkem, který ho tvoří</summary>
''' <typeparam name="TControl">Typ ovládacího prvku</typeparam>
Public Class MultiControlItem(Of TControl As {New, Control})
    Inherits MultiControlItem
    ''' <summary>Vytvoří instanci ovládacího prvku</summary>
    ''' <returns>Nová instance <typeparamref name="TControl"/></returns>
    Protected Overrides Function CreateControl() As System.Windows.Forms.Control
        Return New TControl
    End Function
    ''' <summary>Ovládací prvek tvořící tuto instanci</summary>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    Public Shadows ReadOnly Property Control() As TControl
        Get
            Return MyBase.Control
        End Get
    End Property
End Class
''' <summary><see cref="MultiControlItem"/> s určeným typem rodiče</summary>
''' <typeparam name="TOwner">Typ rodiče</typeparam>
Public MustInherit Class MultiControlItemWithOwner(Of TOwner As MultiControlBase)
    Inherits MultiControlItem
    ''' <summary>Vlastník položky</summary>
    ''' <returns>Vlastník položky, nebo null</returns>
    ''' <exception cref="TypeMismatchException">Nastavovaná hodnota není typu <typeparamref name="TOwner"/></exception>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <Browsable(False)> _
    Public Overrides Property GenericOwner() As MultiControlBase
        Get
            Return MyBase.GenericOwner
        End Get
        Set(ByVal value As MultiControlBase)
            If value IsNot Nothing AndAlso Not TypeOf value Is TOwner Then Throw New TypeMismatchException(value, GetType(TOwner), "value")
            MyBase.GenericOwner = value
        End Set
    End Property
    ''' <summary>Vlastník položky</summary>
    ''' <returns>Vlastník položky, nebo null</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <Browsable(False)> _
    Public Property Owner() As TOwner
        Get
            Return GenericOwner
        End Get
        Set(ByVal value As TOwner)
            GenericOwner = value
        End Set
    End Property
End Class
''' <summary><see cref="MultiControlItem"/> s určeným ovládacím prkem, který ho tvoří, a typem rodiče</summary>
''' <typeparam name="TControl">Typ ovládacího prvku</typeparam>
''' <typeparam name="TOwner">Typ rodiče</typeparam>
Public MustInherit Class MultiControlItemWithOwner(Of TControl As {New, Control}, TOwner As MultiControlBase)
    Inherits MultiControlItemWithOwner(Of TOwner)
    ''' <summary>Vytvoří instanci ovládacího prvku</summary>
    ''' <returns>Nová instance <typeparamref name="TControl"/></returns>
    Protected Overrides Function CreateControl() As System.Windows.Forms.Control
        Return New TControl
    End Function
    ''' <summary>Ovládací prvek tvořící tuto instanci</summary>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    Public Shadows ReadOnly Property Control() As TControl
        Get
            Return MyBase.Control
        End Get
    End Property
End Class
#End Region