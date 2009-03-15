Imports System.Drawing.Design, System.ComponentModel, System.Windows.Forms.Design
Imports System.Windows.Forms
Imports System.Data

Partial Public Class DayView
    ''' <summary>Poskytuje spoleènou bázi pro editory seznamu datových èlenù</summary>
    ''' <remarks>Tato tøída nemùže být použita pro <see cref="EditorAttribute"/> pøímo, protože nemá CTor bez parametrù. Oddìïtì od této tøídy a pøedejte <see cref="EditorAttribute">EditorAttributu</see> zdìdìnou tøídu.</remarks>
    Private Class DataMemberEditor : Inherits MemberEditorBase
        ''' <summary>Název vlastnosti objektu pro nìhož je editorem, která obsahuje instanci datového zdroje, jehož vlasntosti mejí být vylistovány.</summary>
        Private [For] As String
        ''' <summary>CTor</summary>
        ''' <param name="For">Název vlastnosti objektu pro nìhož je editorem, která obsahuje instanci datového zdroje, jehož vlasntosti mejí být vylistovány.</param>
        Public Sub New(ByVal [For] As String)
            Me.For = [For]
        End Sub
        ''' <summary>gets object to show properties of</summary>
        ''' <param name="value">The object to edit.</param>
        ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
        Protected Overrides Function GetObjectProperties(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As String) As PropertyDescriptorCollection
            Dim pd As PropertyDescriptor = TypeDescriptor.GetProperties(context.Instance).Item([For])
            If pd Is Nothing Then
                Return Nothing
            Else
                Dim ds As Object = pd.GetValue(context.Instance)
                If TypeOf ds Is BindingSource AndAlso DirectCast(ds, BindingSource).List.Count = 1 AndAlso TypeOf DirectCast(ds, BindingSource).List(0) Is ICustomTypeDescriptor Then
                    Return DirectCast(DirectCast(ds, BindingSource).List(0), ICustomTypeDescriptor).GetProperties
                Else
                    Return TypeDescriptor.GetProperties(ds)
                End If
            End If
        End Function
        ''' <summary>Gets value indicating if property should be shown</summary>
        ''' <param name="prd">Info about the property</param>
        Protected Overrides Function IsPropertyAcceptable(ByVal prd As System.ComponentModel.PropertyDescriptor) As Boolean
            Return prd.PropertyType.FindInterfaces(AddressOf IsInterface, New Type() {GetType(IList), GetType(IListSource)}).Length > 0
        End Function
    End Class
    ''' <summary>Base class for all the editors which edits list of properties of property which name is loaded from another property</summary>
    ''' <typeparam name="T1">Filtering type 1</typeparam>
    ''' <typeparam name="T2">Filtering type 2</typeparam>
    ''' <typeparam name="T3">Filtering type 3</typeparam>
    ''' <remarks>Only properties of either <typeparamref name="T1"/>, <typeparamref name="T2"/> or <typeparamref name="T3"/> type are listed. If you need less than 3 types, pass same types into rest of typeparams.</remarks>
    Private MustInherit Class DataMeberItemFilteredTypeEditor(Of T1, T2, T3)
        Inherits MemberEditorBase
        ''' <summary>gets object to show properties of</summary>
        ''' <param name="value">The object to edit.</param>
        ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
        Protected Overrides Function GetObjectProperties(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As String) As PropertyDescriptorCollection
            Dim pd1 As PropertyDescriptor = TypeDescriptor.GetProperties(context.Instance).Item(DataSourcePropertyName)
            If pd1 Is Nothing Then Return Nothing
            Dim ds As Object = pd1.GetValue(context.Instance)
            If ds Is Nothing Then Return Nothing
            Dim pd2 As PropertyDescriptor = TypeDescriptor.GetProperties(context.Instance).Item(ListPropertyName)
            If pd2 Is Nothing Then Return Nothing
            Dim ListName As String = pd2.GetValue(context.Instance)
            If ListName Is Nothing OrElse ListName = "" Then Return Nothing
            Dim pd3 As PropertyDescriptor
            Dim prpOwner As Object = Nothing
            If TypeOf ds Is BindingSource AndAlso DirectCast(ds, BindingSource).List.Count = 1 AndAlso TypeOf DirectCast(ds, BindingSource).List(0) Is ICustomTypeDescriptor Then
                pd3 = DirectCast(DirectCast(ds, BindingSource).List(0), ICustomTypeDescriptor).GetProperties.Item(ListName)
                If pd3 IsNot Nothing Then _
                    prpOwner = DirectCast(DirectCast(ds, BindingSource).List(0), ICustomTypeDescriptor).GetPropertyOwner(pd3)
            Else
                pd3 = TypeDescriptor.GetProperties(ds).Item(ListName)
                prpOwner = ds
            End If
            If pd3 Is Nothing Then Return Nothing
            Dim List As Object = pd3.GetValue(prpOwner)
            If TypeOf List Is DataTable Then
                Dim Method As Reflection.MethodInfo = GetType(DataTable).GetMethod("GetRowType", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod Or Reflection.BindingFlags.NonPublic)
                Return TypeDescriptor.GetProperties(DirectCast(Method.Invoke(List, New Object() {}), Type))
            Else
                If TypeOf List Is IListSource Then List = DirectCast(List, IListSource).GetList
                If TypeOf List Is ITypedList Then
                    With DirectCast(List, ITypedList)
                        Return .GetItemProperties(Nothing)
                    End With
                ElseIf TypeOf List Is IList Then
                    With DirectCast(List, IList)
                        If .Count > 0 Then
                            Return TypeDescriptor.GetProperties(.Item(0))
                        Else
                            Return Nothing
                        End If
                    End With
                Else
                    Return Nothing
                End If
            End If
        End Function
        ''' <summary>Returns true if property should be shown</summary>
        ''' <param name="prd">Info about property</param>
        ''' <returns>True to include property to list</returns>
        Protected Overrides Function IsPropertyAcceptable(ByVal prd As System.ComponentModel.PropertyDescriptor) As Boolean
            Return GetType(T1).IsAssignableFrom(prd.PropertyType) OrElse GetType(T2).IsAssignableFrom(prd.PropertyType) OrElse GetType(T3).IsAssignableFrom(prd.PropertyType) 'OrElse _
            'prd.PropertyType.FindInterfaces(AddressOf IsInterface, New Type() {GetType(T1), GetType(T2), GetType(T2)}).Length > 0
        End Function
        ''' <summary>Name of property that contains name of property of object returned by <see cref="DataSourcePropertyName"/>-named property. That property returns object which properties will be listed</summary>
        Protected MustOverride ReadOnly Property ListPropertyName() As String
        ''' <summary>Name of data source</summary>
        Protected MustOverride ReadOnly Property DataSourcePropertyName() As String
    End Class
    ''' <summary>Edits list of properties of object returned by by-DataSource-property-returned object' ItemsDataMember-named property</summary>
    ''' <typeparam name="T1">Filtering type 1</typeparam>
    ''' <typeparam name="T2">Filtering type 2</typeparam>
    ''' <typeparam name="T3">Filtering type 3</typeparam>
    ''' <remarks>Only properties of either <typeparamref name="T1"/>, <typeparamref name="T2"/> or <typeparamref name="T3"/> type are listed. If you need less than 3 types, pass same types into rest of typeparams.</remarks>
    Private Class DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of T1, T2, T3) : Inherits DataMeberItemFilteredTypeEditor(Of T1, T2, T3)
        ''' <summary>Name of data source</summary>
        ''' <returns>"DataSource"</returns>
        Protected Overrides ReadOnly Property DataSourcePropertyName() As String
            Get
                Return "DataSource"
            End Get
        End Property
        ''' <summary>Name of property that contains name of property of object returned by <see cref="DataSourcePropertyName"/>-named property. That property returns object which properties will be listed</summary>
        ''' <returns>"ItemsDataMember"</returns>
        Protected Overrides ReadOnly Property ListPropertyName() As String
            Get
                Return "ItemsDataMember"
            End Get
        End Property
    End Class
    ''' <summary>Edits list of properties of object returned by by-DataSource-property-returned object' RowsDataMember-named property</summary>
    ''' <typeparam name="T1">Filtering type 1</typeparam>
    ''' <typeparam name="T2">Filtering type 2</typeparam>
    ''' <typeparam name="T3">Filtering type 3</typeparam>
    ''' <remarks>Only properties of either <typeparamref name="T1"/>, <typeparamref name="T2"/> or <typeparamref name="T3"/> type are listed. If you need less than 3 types, pass same types into rest of typeparams.</remarks>
    Private Class DataMeberItemFilteredTypeEditor_DataSource_RowsDataMember(Of T1, T2, T3) : Inherits DataMeberItemFilteredTypeEditor(Of T1, T2, T3)
        ''' <summary>Name of data source</summary>
        ''' <returns>"DataSource"</returns>
        Protected Overrides ReadOnly Property DataSourcePropertyName() As String
            Get
                Return "DataSource"
            End Get
        End Property
        ''' <summary>Name of property that contains name of property of object returned by <see cref="DataSourcePropertyName"/>-named property. That property returns object which properties will be listed</summary>
        ''' <returns>"RowsDataMember"</returns>
        Protected Overrides ReadOnly Property ListPropertyName() As String
            Get
                Return "RowsDataMember"
            End Get
        End Property
    End Class
    ''' <summary>Poskytuje slopeènou bázi prop editory výbìrem ze seznamu vlastností</summary>
    Private MustInherit Class MemberEditorBase : Inherits DropDownListEditor(Of String)
        ''' <summary>Urèí jestli daný typ je implementuje nìkterý z uvedených interfacù</summary>
        ''' <param name="m">Typ, který má být otestován</param>
        ''' <param name="FilterCriteria"><see cref="T:System.Type[]"/> - seznam interfacù, z nichž pokud <paramref name="m"/> implementuje alespoò jeden, funkce vrátí True.</param>
        ''' <returns>True pokud <paramref name="m"/> implementuje alespoò jeden z interfacù uvedených v <paramref name="FilterCriteria"/></returns>
        Protected Shared Function IsInterface(ByVal m As Type, ByVal FilterCriteria As Object) As Boolean
            Dim Crit As Type() = FilterCriteria
            For Each t As Type In Crit
                If m.Equals(t) OrElse t.IsSubclassOf(m) Then Return True
            Next t
            Return False
        End Function
        ''' <summary>gets object to show properties of</summary>
        ''' <param name="value">The object to edit.</param>
        ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
        Protected MustOverride Function GetObjectProperties(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As String) As PropertyDescriptorCollection
        ''' <summary>Gets value indicating if property should be shown</summary>
        ''' <param name="prd">Info about the property</param>
        Protected MustOverride Function IsPropertyAcceptable(ByVal prd As PropertyDescriptor) As Boolean

        ''' <summary>Edits the value of the specified object using the editor style indicated by the <see cref="System.Drawing.Design.UITypeEditor.GetEditStyle"/> method.</summary>
        ''' <param name="value">The object to edit.</param>
        ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
        ''' <returns>The new value of the object.</returns>
        Protected Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As String) As String
            Dim Prps As PropertyDescriptorCollection = GetObjectProperties(context, provider, value)
            If Prps Is Nothing OrElse Prps.Count = 0 Then Return value
            'Dim dataSource As Object = TypeDescriptor .GetProperties  descriptor.GetValue(context.Instance)
            Dim Sel As Integer = -1
            If value Is Nothing OrElse (TypeOf value Is String AndAlso DirectCast(value, String) = String.Empty) Then
                Sel = 0
            End If
            ListBox.Items.Clear()
            ListBox.Items.Add("<none>")
            ListBox.DisplayMember = "DisplayName"
            ListBox.ValueMember = "Name"
            Dim i As Integer = 0
            For Each Prd As PropertyDescriptor In Prps
                If Not Prd.DesignTimeOnly AndAlso IsPropertyAcceptable(Prd) Then
                    i += 1
                    ListBox.Items.Add(Prd)
                    If value IsNot Nothing AndAlso TypeOf value Is String AndAlso DirectCast(value, String) = Prd.Name Then
                        Sel = i
                    End If
                End If
            Next Prd
            ListBox.SelectedIndex = Sel
            If ShowDropDown() Then
                If ListBox.SelectedIndex = 0 Then
                    value = Nothing
                ElseIf ListBox.SelectedIndex > 0 Then
                    value = DirectCast(ListBox.SelectedItem, PropertyDescriptor).Name
                End If
            End If
            Return value
        End Function

    End Class
    ''' <summary>Implementuje <see cref="DataMemberEditor"/>, který edituje seznam datových èlenù pro vlastnost <see cref="DataSource"/></summary>
    Private Class DataSourceDataMemberEditor : Inherits DataMemberEditor
        ''' <summary>CTor</summary>
        Public Sub New()
            MyBase.New("DataSource")
        End Sub
    End Class

    '''' <summary>Implementuje <see cref="UITypeEditor"/>, který edituje seznam možných názvù vlastností pro vlastnosti datových èlenù (napø. <see cref="RowDisplayMember"/> a <see cref="RowIDMember"/>)</summary>
    'Private Class DayViewDataMemberEditor : Inherits DropDownListEditor(Of String)
    '    ''' <summary>Edits the value of the specified object using the editor style indicated by the <see cref="System.Drawing.Design.UITypeEditor.GetEditStyle"/> method.</summary>
    '    ''' <param name="value">The object to edit.</param>
    '    ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
    '    ''' <returns>The new value of the object.</returns>
    '    Protected Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As String) As String
    '        If Not TypeOf context.Instance Is DayView Then Return value
    '        Dim dataSource As IList = DirectCast(context.Instance, DayView).RowsSource
    '        If TypeOf dataSource Is BindingSource Then
    '            Dim ItemType As IList = DirectCast(dataSource, BindingSource).List
    '            If TypeOf ItemType Is DataView Then
    '                Dim cols As DataColumnCollection = DirectCast(ItemType, DataView).Table.Columns
    '                Dim Sel As Integer = -1
    '                If value Is Nothing OrElse (TypeOf value Is String AndAlso DirectCast(value, String) = String.Empty) Then
    '                    Sel = 0
    '                End If
    '                ListBox.Items.Clear()
    '                ListBox.Items.Add("<none>")
    '                ListBox.DisplayMember = "ColumnName"
    '                ListBox.ValueMember = "ColumnName"
    '                Dim i As Integer = 0
    '                For Each dc As DataColumn In cols
    '                    i += 1
    '                    ListBox.Items.Add(dc)
    '                    If value IsNot Nothing AndAlso TypeOf value Is String AndAlso DirectCast(value, String) = dc.ColumnName Then
    '                        Sel = i
    '                    End If
    '                Next dc
    '                ListBox.SelectedIndex = Sel
    '                If ShowDropDown() Then
    '                    If ListBox.SelectedIndex = 0 Then
    '                        value = Nothing
    '                    ElseIf ListBox.SelectedIndex > 0 Then
    '                        value = DirectCast(ListBox.SelectedItem, DataColumn).ColumnName
    '                    End If
    '                End If
    '            End If
    '        End If
    '        Return value
    '    End Function
    'End Class
End Class

''' <summary>Implements type-safe <see cref="ListBox"/>-based drop-dow <see cref="UITypeEditor"/></summary>
Public MustInherit Class DropDownListEditor(Of T) : Inherits UITypeEditor
    ''' <summary><see cref="ListBox"/> se seznamem</summary>
    Private WithEvents lst As New ListBox
    ''' <summary>Reaguje na kliknutí na vybranou položku</summary>
    Private Sub lst_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lst.Click
        Currentservice.CloseDropDown()
    End Sub
    ''' <summary>Reaguje na na stisk klávesy - potvrzuje vybranou položku klávesou Enter, storkuje Esc</summary>
    Private Sub lst_KeyDown(ByVal sender As Object, ByVal e As Windows.Forms.KeyEventArgs) Handles lst.KeyDown
        If e.KeyCode = Keys.Escape OrElse e.KeyCode = Keys.Return Then
            CurrentCancel = e.KeyCode = Keys.Escape
            DirectCast(DirectCast(sender, ListBox).Tag, IWindowsFormsEditorService).CloseDropDown()
        End If
    End Sub
    ''' <summary>Edits the value of the specified object using the editor style indicated by the <see cref="System.Drawing.Design.UITypeEditor.GetEditStyle"/> method.</summary>
    ''' <param name="value">The object to edit.</param>
    ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
    ''' <returns>The new value of the object.</returns>
    Public NotOverridable Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        If value IsNot Nothing AndAlso Not TypeOf value Is T Then Return value
        If ((Not provider Is Nothing) AndAlso (Not context.Instance Is Nothing)) Then
            Dim edSvc As IWindowsFormsEditorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If edSvc IsNot Nothing Then
                _Currentservice = edSvc
                Return EditValue(context, provider, DirectCast(value, T))
            End If
        End If
        Return value
    End Function
    ''' <summary>Contains value of the <see cref="Currentservice"/> property</summary>
    Private _Currentservice As IWindowsFormsEditorService
    ''' <summary>Instance <see cref="IWindowsFormsEditorService"/> získaná z aktuálního kontextu metody <see cref="EditValue"/>.</summary>
    Protected ReadOnly Property Currentservice() As IWindowsFormsEditorService
        Get
            Return _Currentservice
        End Get
    End Property
    ''' <summary>Edits the value of the specified object using drop-down <see cref="ListBox"/>.</summary>
    ''' <param name="value">The object to edit.</param>
    ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
    ''' <returns>The new value of the object.</returns>
    ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that can be used to gain additional context information.</param>
    ''' <remarks>Perform any necessary operations on <see cref="ListBox"/>, then call <see cref="ShowDropDown"/></remarks>
    Protected MustOverride Overloads Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As T) As T
    ''' <summary>Shows the drop-down <see cref="ListBox"/></summary>
    Protected Function ShowDropDown() As Boolean
        CurrentCancel = False
        Currentservice.DropDownControl(lst)
        Return Not CurrentCancel
    End Function
    ''' <summary>True when user pressed Esc on drop-down <see cref="ListBox"/></summary>
    Private CurrentCancel As Boolean
    ''' <summary>List box with items</summary>
    Protected ReadOnly Property ListBox() As ListBox
        Get
            Return lst
        End Get
    End Property
    ''' <summary>Gets a value indicating whether drop-down editors should be resizable by the user.</summary>
    ''' <returns>This implementation always returns true</returns>
    Public Overrides ReadOnly Property IsDropDownResizable() As Boolean
        Get
            Return True
        End Get
    End Property
    ''' <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.</summary>
    ''' <returns><see cref="UITypeEditorEditStyle.DropDown"/></returns>
    Public NotOverridable Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function
End Class

Partial Public Class DayView
    ''' <summary>Implements <see cref="IDayViewDataItem"/></summary>
    Private Class DayViewItemWrapper : Inherits DayViewDataItemBase
        ''' <summary>Item that provides data</summary>
        Private DataItem As Object
        ''' <summary><see cref="DayView"/> that provides names of properties</summary>
        Private Parent As DayView
        ''' <summary>CTor</summary>
        ''' <param name="Item">Item that provides data</param>
        ''' <param name="Parent"><see cref="DayView"/> that provides names of properties</param>
        Public Sub New(ByVal Item As Object, ByVal Parent As DayView)
            If Item Is Nothing Then Throw New ArgumentNullException("Item")
            If Parent Is Nothing Then Throw New ArgumentNullException("Parent")
            Me.Parent = Parent
            Me.DataItem = Item
        End Sub
        ''' <summary>Specifies day when item starts and begin</summary>
        ''' <exception cref="InvalidOperationException">There was an error while obtainin value from data item. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        Public Overrides ReadOnly Property [Date]() As Date
            Get
                Try
                    Return Parent.GetItemStart(DataItem).Date
                Catch ex As Exception
                    Throw New InvalidOperationException("Error while calling GetItemStart.", ex)
                End Try
            End Get
        End Property
        ''' <summary>End time of item within <see cref="Day"/></summary>
        ''' <exception cref="InvalidOperationException">There was an error while obtainin value from data item. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        Public Overrides ReadOnly Property EndTime() As System.TimeSpan
            Get
                Try
                    Return Parent.GetItemEnd(DataItem).TimeOfDay
                Catch ex As Exception
                    Throw New InvalidOperationException("Error while calling GetItemEnd.", ex)
                End Try
            End Get
        End Property
        ''' <summary>Start time of item within <see cref="Day"/></summary>
        ''' <exception cref="InvalidOperationException">There was an error while obtainin value from data item. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        Public Overrides ReadOnly Property StartTime() As System.TimeSpan
            Get
                Try
                    Return Parent.GetItemStart(DataItem).TimeOfDay
                Catch ex As Exception
                    Throw New InvalidOperationException("Error while calling GetItemStart.", ex)
                End Try
            End Get
        End Property
        ''' <summary>Gets string representing the item. Used for displaying text of the item</summary>
        ''' <exception cref="InvalidOperationException">There was an error while obtainin value from data item. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        Public Overrides Function ToString() As String
            Try
                Return Parent.GetItemText(DataItem)
            Catch ex As Exception
                Throw New InvalidOperationException("Error while calling GetItemText.", ex)
            End Try
        End Function
        ''' <summary>Gets value indicating if the <see cref="ID"/> property works</summary>
        ''' <returns>True when <see cref="Parent">Parent</see>.<see cref="ItemIDMember">ItemIDMember</see> is set</returns>
        Public Overrides ReadOnly Property IDImplemented() As Boolean
            Get
                Return Parent.ItemIDMember <> ""
            End Get
        End Property
        ''' <summary>ID of this data item</summary>
        ''' <exception cref="InvalidOperationException">There was an error while obtainin value from data item. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        Public Overrides ReadOnly Property ID() As Integer
            Get
                Try
                    Return Parent.GetItemID(DataItem)
                Catch ex As Exception
                    Throw New InvalidOperationException("Error while calling GetItemID.", ex)
                End Try
            End Get
        End Property
        ''' <summary>Value indicating taht this instance is not item itself but wrapper and item must be obtained using <see cref="Item"/> property</summary>
        ''' <returns>True</returns>
        Public Overrides ReadOnly Property IsWrapper() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Item this instance wraps around</summary>
        Public Overrides ReadOnly Property Item() As Object
            Get
                Return DataItem
            End Get
        End Property
        ''' <summary>Returns value indicationg that the <see cref="Enabled"/> property is implemented</summary>
        ''' <returns>True</returns>
        Public Overrides ReadOnly Property EnabledImplemented() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Gets value indicating is this item is unlocked (true) or locked (false)</summary>
        ''' <exception cref="InvalidOperationException">There was an error while obtainin value from data item. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        Public Overrides ReadOnly Property Enabled() As Boolean
            Get
                Try
                    Return Not Parent.GetItemLocked(DataItem)
                Catch ex As Exception
                    Throw New InvalidOperationException("Error while calling GetItemLocked.", ex)
                End Try
            End Get
        End Property
    End Class
End Class