'TODO: Zkontrolovat doc-com
Imports System.ComponentModel, System.Reflection
Imports Tools.CollectionsT.GenericT
Imports Tools.VisualBasicT
Imports Tools.WindowsT.FormsT.UtilitiesT, Tools.ComponentModelT
Imports System.Windows.Forms

Partial Public Class DayView
#Region "Common"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="DataSource"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _DataSource As Object
    ''' <summary>Zdroj dat objektu, který naplní vlastnost <see cref="Rows"/></summary>
    ''' <value>Specifically supported types for this value are <see cref="Data.DataSet"/> and <see cref="BindingSource"/>. Set to null to turn data binding off.</value>
    ''' <returns>Current data source. Can be null.</returns>
    ''' <remarks>Data nejsou získávána pøímo z tohoto objektu, ale z jeho vlastností jejichž názvy jsou urèeny v
    ''' <see cref="RowsDataMember"/> a <see cref="ItemsDataMember"/>.</remarks>
    <Description("Data source that populates this control. Must be IListSource.")> _
    <DefaultValue(CStr(Nothing)), AttributeProvider(GetType(IListSource))> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    Public Property DataSource() As Object
        Get
            Return _DataSource
        End Get
        Set(ByVal value As Object)
            RowsChangedHandler(False)
            ItemsChangedHandler(False)
            BSChangedHandler(False)
            _DataSource = value
            If RowsChangedHandler(True) IsNot Nothing Then BindRows()
            'If ItemsChangedHandler(True) IsNot Nothing Then BindItems() - neni potøeba
            If TypeOf value Is BindingSource Then BSChangedHandler(True)
        End Set
    End Property
    ''' <summary>Registers or unregisters event handlers oor <see cref="DataSource">DataSource</see>.<see cref="BindingSource.ListChanged">ListChanged</see> to <see cref="BS_ListChanged"/></summary>
    ''' <param name="Add">True to register handlers, false to unregister</param>
    Private Sub BSChangedHandler(ByVal Add As Boolean)
        If DataSource Is Nothing Then Return
        If Not TypeOf DataSource Is BindingSource Then Return
        With DirectCast(DataSource, BindingSource)
            If Add Then
                AddHandler .ListChanged, AddressOf BS_ListChanged
            Else
                RemoveHandler .ListChanged, AddressOf BS_ListChanged
            End If
        End With
    End Sub
    ''' <summary>Handles <see cref="DataSource">DataSource</see>.<see cref="BindingSource.ListChanged">ListChanged</see> event</summary>
    ''' <param name="sender">Event source</param>
    ''' <param name="e">Event arguments</param>
    Private Sub BS_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        If e.ListChangedType = ListChangedType.PropertyDescriptorChanged OrElse e.ListChangedType = ListChangedType.PropertyDescriptorAdded OrElse e.ListChangedType = ListChangedType.PropertyDescriptorDeleted OrElse e.ListChangedType = ListChangedType.Reset Then
            RowsChangedHandler(False)
            ItemsChangedHandler(False)
            If RowsChangedHandler(True) IsNot Nothing OrElse ItemsChangedHandler(True) IsNot Nothing Then _
                BindRows()
        End If
    End Sub

    ''' <summary>Gets value of member of <see cref="DataSource"/> with specified name</summary>
    ''' <param name="Name">Name of member to get value of</param>
    ''' <returns>Value of given member. Null if <see cref="DataSource"/> is null or <paramref name="Name"/> is null or an empty string</returns>
    ''' <exception cref="TypeMismatchException">Value of given member is not convertible to <see cref="IList"/></exception>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/> ==or== <see cref="DataSource"/> is <see cref="BindingSource"/> but it haven't provided pproperty with given name as property of only item of <see cref="BindingSource.List"/> of type <see cref="ICustomTypeDescriptor"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    Protected Function GetSource(ByVal Name$) As IList
        If Me.DataSource Is Nothing OrElse Name Is Nothing OrElse Name = "" Then Return Nothing
        Dim Source As Object = Nothing
        If TypeOf Me.DataSource Is BindingSource AndAlso DirectCast(Me.DataSource, BindingSource).List.Count = 1 AndAlso TypeOf DirectCast(Me.DataSource, BindingSource).List(0) Is ICustomTypeDescriptor Then
            Dim pd As PropertyDescriptor = DirectCast(DirectCast(Me.DataSource, BindingSource).List(0), ICustomTypeDescriptor).GetProperties.Item(Name)
            If pd Is Nothing Then Throw New MissingMemberException("BindingSource haven't provided property " & Name)
            Source = pd.GetValue(DirectCast(DirectCast(Me.DataSource, BindingSource).List(0), ICustomTypeDescriptor).GetPropertyOwner(pd))
        Else
            Source = GetMemberValue(DataSource, Name)
        End If
        If Source Is Nothing Then Return Nothing
        If TypeOf Source Is IListSource Then Source = DirectCast(Source, IListSource).GetList
        Dim List As IList = TryCast(Source, IList)
        If List Is Nothing Then Throw New TypeMismatchException(Source, GetType(IList))
        Return List
    End Function
    ''' <summary>Via <see cref="TypeDescriptor"/> or <see cref="Type"/> gets value of property or fied</summary>
    ''' <param name="Name">Name of member to get value of</param>
    ''' <param name="obj">Object to get value of member of</param>
    ''' <typeparam name="T">Type of object to return</typeparam>
    ''' <returns>Value of given member of given object</returns>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    ''' <exception cref="TypeMismatchException">Value of member is not of type <typeperamref name="T"/></exception>
    Protected Shared Function GetMemberValue(Of T)(ByVal obj As Object, ByVal Name As String) As T
        Dim ret As Object = GetMemberValue(obj, Name)
        If TypeOf ret Is T Then Return ret
        Throw New TypeMismatchException(ret, GetType(T))
    End Function
    ''' <summary>Via <see cref="TypeDescriptor"/> or <see cref="Type"/> gets value of property or fied</summary>
    ''' <param name="Name">Name of member to get value of</param>
    ''' <param name="obj">Object to get value of member of</param>
    ''' <returns>Value of given member of given object</returns>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    Protected Shared Function GetMemberValue(ByVal obj As Object, ByVal Name As String) As Object
        'Type descriptor
        Dim Prp As PropertyDescriptor = TypeDescriptor.GetProperties(obj).Item(Name)
        If Prp IsNot Nothing Then
            Return Prp.GetValue(obj)
        Else
            'Property
            Dim Prp2 As PropertyInfo = obj.GetType.GetProperty(Name)
            Try
                If Prp2 IsNot Nothing AndAlso Prp2.GetGetMethod IsNot Nothing AndAlso Prp2.GetGetMethod.GetParameters.Length = 0 Then
                    Return Prp2.GetValue(obj, Nothing)
                Else
                    'Field
                    Dim field As FieldInfo = obj.GetType.GetField(Name)
                    If field IsNot Nothing Then
                        Return field.GetValue(obj)
                    Else
                        Throw New MissingMemberException(String.Format("Member {0} of type {1} was not found.", Name, obj.GetType.FullName))
                    End If
                End If
            Catch ex As Exception When TypeOf ex Is MethodAccessException OrElse TypeOf ex Is ArgumentException OrElse TypeOf ex Is TargetException OrElse TypeOf ex Is TargetParameterCountException OrElse TypeOf ex Is NotSupportedException OrElse TypeOf ex Is FieldAccessException
                Throw New TargetInvocationException("Error while obtaining value of property " & Name, ex)
            End Try
        End If
    End Function
#End Region
#Region "Rows"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RowsDataMember"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _RowsDataMember As String
    ''' <summary>Obsahuje hodnbotu vlastnosti <see cref="RowDisplayMember"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _RowDisplayMember As String
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RowIDMember"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _RowIDMember As String
    ''' <summary>Adds or removes handler of list of rows' <see cref="IBindingList.ListChanged"/></summary>
    ''' <param name="Add">True to add, false to remove</param>
    ''' <remarks>Return value of <see cref="RowsSource"/> or null if it thrown a <see cref="InvalidOperationException"/> or <see cref="MissingMemberException"/></remarks>
    Private Function RowsChangedHandler(ByVal Add As Boolean) As IList
        Dim ARDS As IList = Nothing
        Try
            ARDS = RowsSource
        Catch ex As Exception When TypeOf ex Is InvalidOperationException OrElse TypeOf ex Is MissingMemberException
        End Try
        If ARDS IsNot Nothing AndAlso TypeOf ARDS Is IBindingList Then
            If Add Then
                AddHandler DirectCast(ARDS, IBindingList).ListChanged, AddressOf Rows_ListChanged
            Else
                RemoveHandler DirectCast(ARDS, IBindingList).ListChanged, AddressOf Rows_ListChanged
            End If
        End If
        Return ARDS
    End Function
    ''' <summary>Reaguje na událost <see cref="IBindingList.ListChanged"/> seznamu øádkù</summary>
    Private Sub Rows_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        BindRows()
    End Sub
    ''' <summary>Obsahuje název seznamu z <see cref="DataSource"/> který obsahuje seznam øádkù.</summary>
    <Editor(GetType(DataSourceDataMemberEditor), GetType(Drawing.Design.UITypeEditor))> _
    <Description("Name of list from DataSource that contains list of rows.")> _
    <DefaultValue(""), KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    Public Property RowsDataMember() As String
        Get
            Return _RowsDataMember
        End Get
        Set(ByVal value As String)
            RowsChangedHandler(False)
            _RowsDataMember = value
            If RowsChangedHandler(True) IsNot Nothing Then BindRows()
        End Set
    End Property
    ''' <summary>Název vlastnosti použité ke zobrazení textu øádku (<see cref="DayViewRow.RowText"/>).</summary>
    ''' <value>Mùže být ponecháno prázdné (null nebo <see cref="String.Empty"/> a bude použita funkce <see cref="System.Object.ToString"/></value>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <Description("Name of property used for text of row. Or leave empty and ToString function will be used." & vbCrLf & "Note: In designer you can get list of possible members if your datasource is BindingSource and its List is DataView. This is list of columns in Table. In order these names to be valid the table must physicaly contain properties with these names.")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_RowsDataMember(Of Object, Object, Object)), GetType(Drawing.Design.UITypeEditor))> _
    <DefaultValue("")> _
    Public Property RowDisplayMember() As String
        Get
            Return _RowDisplayMember
        End Get
        Set(ByVal value As String)
            Dim Changed As Boolean = value <> RowDisplayMember
            _RowDisplayMember = value
            If Changed Then
                For Each Row As DayViewRow In Rows
                    Row.SetDataValue(Row.DataValue, RowDisplayMember, RowIDMember)
                Next Row
            End If
        End Set
    End Property
    ''' <summary>Název vlastnosti použité pro <see cref="DayViewRow.RowID"/></summary>
    ''' <value>Mùže být ponecháno prázdné (null nebo <see cref="String.Empty"/> a <see cref="DayViewRow.RowID"/> nebude nastavováno</value>
    ''' <remarks>Vlastnost musí vracet <see cref="IConvertible"/>, jinak mùže pozdìji dojít k <see cref="InvalidCastException"/></remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <Description("Name of property for RowID of row. Or leave empty and RowID will not be used." & vbCrLf & "Note: In designer you can get list of possible members if your datasource is BindingSource and its List is DataView. This is list of columns in Table. In order these names to be valid the table must physicaly contain properties with these names.")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_RowsDataMember(Of IConvertible, IConvertible, IConvertible)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property RowIDMember() As String
        Get
            Return _RowIDMember
        End Get
        Set(ByVal value As String)
            Dim Changed As Boolean = value <> RowIDMember
            _RowIDMember = value
            If Changed Then
                For Each Row As DayViewRow In Rows
                    Row.SetDataValue(Row.DataValue, RowDisplayMember, RowIDMember)
                Next Row
            End If
        End Set
    End Property
    ''' <summary>Vrátí objekt použitý jako datový zdroj pro øádky. Hodnotu èlena <see cref="DataSource"/>, jehož název je urèen <see cref="RowsDataMember"/></summary>
    ''' <exception cref="TypeMismatchException">Value of given member is not convertible to <see cref="IList"/></exception>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    Protected ReadOnly Property RowsSource() As IList
        Get
            Return GetSource(RowsDataMember)
        End Get
    End Property
    ''' <summary>Vrátí objekt použitý jako datový zdroj pro položky. Hodnotu èlena <see cref="DataSource"/>, jehož název je urèen <see cref="ItemsDataMember"/></summary>
    ''' <exception cref="TypeMismatchException">Value of given member is not convertible to <see cref="IList"/></exception>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    Protected ReadOnly Property ItemsSource() As IList
        Get
            Return GetSource(ItemsDataMember)
        End Get
    End Property
    ''' <summary>Naplní <see cref="Rows"/> z datového zdroje øádek (<see cref="DataSource"/>)</summary>
    ''' <exception cref="MissingMemberException">Hodnota <see cref="RowsDataMember"/> je nastavena, ale <see cref="DataSource"/> neobsahuje dostupnou vlastnost ani promìnnou s daným jménem, nebo hodnota vlastnosti nemùže být získána (write-only, indexovaná)</exception>
    ''' <exception cref="InvalidOperationException">
    ''' Objekt použitý k vyplnìní seznamu øádkù není ani jednoho z podporovaných typù.</exception>
    ''' <remarks>You can use this method to force reload of content</remarks>
    Public Sub BindRows()
        Dim DataSource As IList = RowsSource
        If RowsSource Is Nothing Then Return
        Rows.Clear()
        For Each item As Object In DataSource
            Rows.Add(New DayViewRow(item, RowDisplayMember, RowIDMember))
        Next item
        BindItems()
    End Sub
#End Region
#Region "Items"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="ItemsDataMember"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemsDataMember As String
    ''' <summary>Obsahuje název seznamu z <see cref="DataSource"/> který obsahuje seznam položek v øádcích.</summary>
    <Editor(GetType(DataSourceDataMemberEditor), GetType(Drawing.Design.UITypeEditor))> _
    <Description("Name of list from DataSource that contains list of items in rows.")> _
    <DefaultValue(""), KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    Public Property ItemsDataMember() As String
        Get
            Return _ItemsDataMember
        End Get
        Set(ByVal value As String)
            ItemsChangedHandler(False)
            _ItemsDataMember = value
            If ItemsChangedHandler(True) IsNot Nothing Then BindItems()
        End Set
    End Property
    ''' <summary>Reloads contens of all rows (if item binding is set properly)</summary>
    Public Sub BindItems()
        If ItemsDataMember Is Nothing OrElse ItemsDataMember = "" Then Exit Sub
        If (ItemRowIDMember Is Nothing OrElse ItemRowIDMember = "") OrElse _
           (ItemStartMember Is Nothing OrElse ItemStartMember = "") OrElse _
           (ItemEndMember Is Nothing OrElse ItemEndMember = "") Then Exit Sub
        For Each row As DayViewRow In Me.Rows
            BindRow(row)
        Next row
    End Sub
#Region "Members"
    ''' <summary>Gets text representation of item for data binding</summary>
    ''' <param name="DataMember">Item's <see cref="DayViewItem.DataItem"/></param>
    ''' <returns>Text representation of the item depending on <see cref="ItemDisplayMember"/></returns>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    Protected Function GetItemText$(ByVal DataMember As Object)
        If DataMember Is Nothing Then Return ""
        If ItemDisplayMember Is Nothing OrElse ItemDisplayMember = "" Then Return DataMember.ToString
        Dim Obj As Object = GetMemberValue(DataMember, ItemDisplayMember)
        If Obj Is Nothing Then Return ""
        Return Obj.ToString
    End Function

    ''' <summary>Contains value of the <see cref="ItemDisplayMember"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemDisplayMember As String
    ''' <summary>Name of property of item used to display it. If not set, <see cref="System.Object.ToString"/> is used</summary>
    ''' <remarks>Change of this property does not change items already bound.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue("")> _
    <Description("Name of property of item used to display it. If not set ToString() is used.")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of Object, Object, Object)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property ItemDisplayMember() As String
        Get
            Return _ItemDisplayMember
        End Get
        Set(ByVal value As String)
            _ItemDisplayMember = value
        End Set
    End Property

    ''' <summary>Gets id of item for data binding</summary>
    ''' <param name="DataMember">Item's <see cref="DayViewItem.DataItem"/></param>
    ''' <returns>Id of item <see cref="ItemIDMember"/>. -1 if <paramref name="DataMember"/> or value returned if null</returns>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    ''' <exception cref="InvalidOperationException"><see cref="ItemIDMember"/> is not set</exception>
    ''' <exception cref="TypeMismatchException">Value of member is not of type <see cref="IConvertible"/></exception>
    Protected Function GetItemID(ByVal DataMember As Object) As Integer
        If ItemIDMember Is Nothing OrElse ItemIDMember = "" Then Throw New InvalidOperationException("Property ItemIDMember is not set")
        If DataMember Is Nothing Then Return -1
        Dim Obj As IConvertible = GetMemberValue(Of IConvertible)(DataMember, ItemIDMember)
        If Obj Is Nothing Then Return -1
        Return Obj.ToInt32(System.Globalization.CultureInfo.CurrentCulture)
    End Function

    ''' <summary>Contains value of the <see cref="ItemIDMember"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemIDMember As String
    ''' <summary>Name of property of item used to populate <see cref="DayViewItem.DataID"/></summary>
    ''' <remarks>Change of this property does not change items already bound.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue("")> _
    <Description("Name of property of item used to populate DataID")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of IConvertible, IConvertible, IConvertible)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property ItemIDMember() As String
        Get
            Return _ItemIDMember
        End Get
        Set(ByVal value As String)
            _ItemIDMember = value
        End Set
    End Property

    ''' <summary>Gets foreign-key id pointing to <see cref="DayViewRow.RowID"/> of item for data binding</summary>
    ''' <param name="DataMember">Item's <see cref="DayViewItem.DataItem"/></param>
    ''' <returns>Row-Id of item <see cref="ItemRowIDMember"/>. -1 if <paramref name="DataMember"/> or value returned if null</returns>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    ''' <exception cref="InvalidOperationException"><see cref="ItemRowIDMember"/> is not set</exception>
    ''' <exception cref="TypeMismatchException">Value of member is not of type <see cref="IConvertible"/></exception>
    Protected Function GetItemRowID(ByVal DataMember As Object) As Integer
        If ItemRowIDMember Is Nothing OrElse ItemRowIDMember = "" Then Throw New InvalidOperationException("Property ItemRowIDMember is not set")
        If DataMember Is Nothing Then Return -1
        Dim Obj As IConvertible = GetMemberValue(Of IConvertible)(DataMember, ItemRowIDMember)
        If Obj Is Nothing Then Return -1
        Return Obj.ToInt32(System.Globalization.CultureInfo.CurrentCulture)
    End Function

    ''' <summary>Contains value of the <see cref="ItemRowIDMember"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemRowIDMember As String
    ''' <summary>Name of property of item that contains id of row (<see cref="DayViewRow.RowID"/>). Used to place items in right rows.</summary>
    ''' <remarks>This property must be set in order items data binding to work.
    ''' Change of this property does not change items already bound.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue("")> _
    <Description("Name of property of item that contains id of row. This property must be set in order items data binding to work")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of IConvertible, IConvertible, IConvertible)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property ItemRowIDMember() As String
        Get
            Return _ItemRowIDMember
        End Get
        Set(ByVal value As String)
            _ItemRowIDMember = value
        End Set
    End Property

    ''' <summary>Gets date of start of item</summary>
    ''' <param name="DataMember">Item's <see cref="DayViewItem.DataItem"/></param>
    ''' <returns>Date of start of item depending on <see cref="ItemStartMember"/></returns>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    ''' <exception cref="InvalidOperationException"><see cref="ItemIDMember"/> is not set ==or== 
    ''' Value retuned by <see cref="ItemStartMember"/>-named member is not <see cref="Date"/> and <see cref="ItemDateMember"/> is not set</exception>
    ''' <exception cref="TypeMismatchException">Value of member is neither of type <see cref="Date"/> nor <see cref="TimeSpan"/> nor <see cref="tools.TimeSpanFormattable"/></exception>
    ''' <exception cref="NullReferenceException">Value of <see cref="ItemStartMember"/>-named property is null</exception>
    Protected Function GetItemStart(ByVal DataMember As Object) As Date
        If ItemStartMember Is Nothing OrElse ItemStartMember = "" Then Throw New InvalidOperationException("Property ItemStartMember is not set")
        If DataMember Is Nothing Then Return Date.MinValue
        Dim Time As Object = GetMemberValue(DataMember, ItemStartMember)
        If Time Is Nothing Then Throw New NullReferenceException("Value returned by ItemStartMember must not be null.")
        If ItemDateMember Is Nothing OrElse ItemDateMember = "" Then
            If Not TypeOf Time Is Date Then
                If TypeOf Time Is TimeSpan OrElse TypeOf Time Is Tools.TimeSpanFormattable Then
                    Throw New InvalidOperationException("Type of value returned by ItemStartMember must be DateTime when ItemDateMember is not set.")
                Else
                    Throw New TypeMismatchException("Type of value returned by ItemStartMember must be either TimeSpan, TimeSpanFormattable or DateTime", Time)
                End If
            Else
                Return Time
            End If
        Else
            Dim [Date] As Date = GetMemberValue(Of Date)(DataMember, ItemDateMember)
            If TypeOf Time Is Date AndAlso DirectCast(Time, Date).Date <> [Date].Date Then _
                Throw New InvalidOperationException("Start of item must be reported to same date as date of item.")
            If TypeOf Time Is Date Then Return Time
            If TypeOf Time Is TimeSpan Then Return [Date].Date + DirectCast(Time, TimeSpan)
            If TypeOf Time Is Tools.TimeSpanFormattable Then Return [Date].Date + DirectCast(Time, Tools.TimeSpanFormattable)
            Throw New TypeMismatchException("Type of value returned by ItemStartMember must be either TimeSpan, TimeSpanFormattable or DateTime", Time)
        End If
    End Function

    ''' <summary>Contains value of the <see cref="ItemStartMember"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemStartMember As String
    ''' <summary>Name of property of item that returns start of item. It can return either <see cref="TimeSpan"/>, <see cref="Tools.TimeSpanFormattable"/> or <see cref="Date"/>.
    ''' It must return <see cref="Date"/> when <see cref="ItemDateMember"/> is not set.
    ''' If <see cref="DateTime"/> is returned and <see cref="ItemDateMember"/> is set, returned value must be within same day as value returned by property with name stored in <see cref="ItemDateMember"/></summary>
    ''' <remarks>This property must be set in order items data binding to work.
    ''' Change of this property does not change items already bound.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue("")> _
    <Description("Name of property of item that returns start of item. It can return either TimeSpan, TimeSpanFormattable or DateTime. This property must be set in order items data binding to work.")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of Date, TimeSpan, Tools.TimeSpanFormattable)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property ItemStartMember() As String
        Get
            Return _ItemStartMember
        End Get
        Set(ByVal value As String)
            _ItemStartMember = value
        End Set
    End Property

    ''' <summary>Gets date of end of item</summary>
    ''' <param name="DataMember">Item's <see cref="DayViewItem.DataItem"/></param>
    ''' <returns>Date of end of item depending on <see cref="ItemEndMember"/> or <see cref="ItemLengthMember"/></returns>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    ''' <exception cref="InvalidOperationException"><see cref="ItemIDMember"/> is not set ==or== 
    ''' Value retuned by <see cref="ItemStartMember"/>-named member is not <see cref="Date"/> and <see cref="ItemDateMember"/> is not set</exception>
    ''' <exception cref="TypeMismatchException">Value of member is neither of type <see cref="Date"/> nor <see cref="TimeSpan"/> nor <see cref="Tools.TimeSpanFormattable"/> (or <see cref="IConvertible"/> if <see cref="ItemLengthMember"/> is used)</exception>
    ''' <exception cref="NullReferenceException">Value of <see cref="ItemStartMember"/>-named property is null</exception>
    Protected Function GetItemEnd(ByVal DataMember As Object) As Date
        If (ItemEndMember Is Nothing OrElse ItemEndMember = "") AndAlso (ItemLengthMember Is Nothing OrElse ItemLengthMember = "") Then Throw New InvalidOperationException("Property ItemEndMember is not set and ItemLengthMember is not set too.")
        If DataMember Is Nothing Then Return Date.MinValue
        If ItemEndMember IsNot Nothing AndAlso ItemEndMember <> "" Then
            Dim Time As Object = GetMemberValue(DataMember, ItemEndMember)
            If Time Is Nothing Then Throw New NullReferenceException("Value returned by ItemStartMember must not be null.")
            If ItemDateMember Is Nothing OrElse ItemDateMember = "" Then
                If Not TypeOf Time Is Date Then
                    If TypeOf Time Is TimeSpan OrElse TypeOf Time Is Tools.TimeSpanFormattable Then
                        Throw New InvalidOperationException("Type of value returned by ItemEndMember must be DateTime when ItemDateMember is not set.")
                    Else
                        Throw New TypeMismatchException("Type of value returned by ItemEndMember must be either TimeSpan, TimeSpanFormattable or DateTime", Time)
                    End If
                Else
                    Return Time
                End If
            Else
                Dim [Date] As Date = GetMemberValue(Of Date)(DataMember, ItemDateMember)
                If TypeOf Time Is Date AndAlso DirectCast(Time, Date).Date <> [Date].Date Then _
                    Throw New InvalidOperationException("End of item must be reported to same date as date of item.")
                If TypeOf Time Is Date Then Return Time
                If TypeOf Time Is TimeSpan Then Return [Date].Date + DirectCast(Time, TimeSpan)
                If TypeOf Time Is Tools.TimeSpanFormattable Then Return [Date].Date + DirectCast(Time, Tools.TimeSpanFormattable)
                Throw New TypeMismatchException("Type of value returned by ItemEndMember must be either TimeSpan, TimeSpanFormattable or DateTime", Time)
            End If
        Else
            Dim Start As Date = GetItemStart(DataMember)
            Dim Duration As IConvertible = GetMemberValue(Of IConvertible)(DataMember, ItemLengthMember)
            If Duration Is Nothing Then Return Start
            Dim DurationMin As Integer = Duration.ToInt32(System.Globalization.CultureInfo.CurrentCulture)
            If LengthSec Then DurationMin /= 60
            Return Start.AddMinutes(DurationMin)
        End If
    End Function

    ''' <summary>Contains value of the <see cref="ItemEndMember"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemEndMember As String
    ''' <summary>Name of property of item that returns end of item. It fan return either <see cref="TimeSpan"/>, <see cref="Tools.TimeSpanFormattable"/> or <see cref="Date"/>.
    ''' If <see cref="Date"/> is returned, it must be within same day as date of start.</summary>
    ''' <remarks>This property or <see cref="ItemLengthMember"/> must be set in order data binding to work. If both are set, <see cref="ItemStartMember"/> is used.
    ''' Change of this property does not change items already bound.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue("")> _
    <Description("Name of property of item that returns end of item. It can return either TimeSpan, TimeSpanFormattable or DateTime. This or ItemLengthMember property must be set in order items data binding to work.")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of Date, TimeSpan, Tools.TimeSpanFormattable)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property ItemEndMember() As String
        Get
            Return _ItemEndMember
        End Get
        Set(ByVal value As String)
            _ItemEndMember = value
        End Set
    End Property

    ''' <summary>Contains value of the <see cref="ItemLengthMember"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemLengthMember As String
    ''' <summary>Name of property of item that returns duration of item in minutes. It must return number (<see cref="IConvertible"/>).</summary>
    ''' <remarks>This property or <see cref="ItemEndMember"/> must be set in order items data binding to work.  If both are set, <see cref="ItemStartMember"/> is used.
    ''' Change of this property does not change items already bound.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue("")> _
    <Description("Name of property of item that returns duration of item in minutes. It must return number. This property or ItemEndMember must be set in order items data binding to work.  If both are set, ItemStartMember is used.")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of IConvertible, IConvertible, IConvertible)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property ItemLengthMember() As String
        Get
            Return _ItemLengthMember
        End Get
        Set(ByVal value As String)
            _ItemLengthMember = value
        End Set
    End Property

    ''' <summary>Contains value of the <see cref="ItemDateMember"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemDateMember As String
    ''' <summary>Name of property of item that returns date in which item starts and ends. It must return <see cref="Date"/></summary>
    ''' <remarks>This property must be set if <see cref="ItemStartMember"/> does not return <see cref="Date"/> in order items data binding to work.
    ''' Change of this property does not change items already bound.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue("")> _
    <Description("ame of property of item that returns date in which item starts and ends. It must return Date. This property must be set if ItemStartMember does not return DateTime in order data binding to work.")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of Date, Date, Date)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property ItemDateMember() As String
        Get
            Return _ItemDateMember
        End Get
        Set(ByVal value As String)
            _ItemDateMember = value
        End Set
    End Property

    ''' <summary>Contains value of the <see cref="ItemLockedMember"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ItemLockedMember As String
    ''' <summary>Name of property of item that returns if item is locked. Any non-null, non-zero value is treated as true.</summary>
    ''' <remarks>If this property is not all items all treated as not-locked.
    ''' Change of this property does not change items already bound.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue("")> _
    <Description("Name of property of item that returns if item is locked. Any non-null, non-zero value is treated as true. If this property is not all items all treated as not-locked.")> _
    <Editor(GetType(DataMeberItemFilteredTypeEditor_DataSource_ItemsDataMember(Of Object, Object, Object)), GetType(Drawing.Design.UITypeEditor))> _
    Public Property ItemLockedMember() As String
        Get
            Return _ItemLockedMember
        End Get
        Set(ByVal value As String)
            _ItemLockedMember = value
        End Set
    End Property
    ''' <summary>Gets value indicating if item should be locked</summary>
    ''' <param name="DataMember">Item's <see cref="DayViewItem.DataItem"/></param>
    ''' <returns>Lock indication depending on <see cref="ItemLockedMember"/>. Any non-null non-zero value is treated as true.</returns>
    ''' <exception cref="MissingMemberException">Property or field can be found neither via <see cref="TypeDescriptor"/> nor via <see cref="Type"/></exception>
    ''' <exception cref="TargetInvocationException">One of following exceptions was thrown by <see cref="PropertyInfo.GetValue"/> or <see cref="FieldInfo.GetValue"/>. Thrown only when getting value via <see cref="Type"/> (not via <see cref="TypeDescriptor"/>). 
    ''' Exceptions found in <see cref="TargetInvocationException.InnerException"/> property: <see cref="MethodAccessException"/>, <see cref="ArgumentException"/>, <see cref="TargetException"/>, <see cref="TargetParameterCountException"/>, <see cref="NotSupportedException"/>, <see cref="FieldAccessException"/></exception>
    Protected Function GetItemLocked(ByVal DataMember As Object) As Boolean
        If ItemLockedMember Is Nothing OrElse ItemLockedMember = "" Then Return False
        If DataMember Is Nothing Then Return False
        Dim Val As Object = GetMemberValue(DataMember, ItemLockedMember)
        Dim ret As Boolean
        If TypeOf Val Is Boolean Then
            ret = Val
        ElseIf TypeOf Val Is IConvertible Then
            ret = DirectCast(Val, IConvertible).ToInt32(System.Globalization.CultureInfo.CurrentCulture) <> 0
        Else
            ret = Val IsNot Nothing
        End If
        If LockedNot Then Return Not ret Else Return ret
    End Function
#End Region
    ''' <summary>Adds or removes handler of list of rows' members' <see cref="IBindingList.ListChanged"/></summary>
    ''' <param name="Add">True to add, false to remove</param>
    ''' <remarks>Return value of <see cref="ItemsSource"/> or null if it throws a <see cref="InvalidOperationException"/> or <see cref="MissingMemberException"/></remarks>
    Private Function ItemsChangedHandler(ByVal Add As Boolean) As IList
        Dim AIDS As IList = Nothing
        Try
            AIDS = ItemsSource
        Catch ex As Exception When TypeOf ex Is InvalidOperationException OrElse TypeOf ex Is MissingMemberException
        End Try
        If AIDS IsNot Nothing AndAlso TypeOf AIDS Is IBindingList Then
            If Add Then
                AddHandler DirectCast(AIDS, IBindingList).ListChanged, AddressOf Items_ListChanged
            Else
                RemoveHandler DirectCast(AIDS, IBindingList).ListChanged, AddressOf Items_ListChanged
            End If
        End If
        Return AIDS
    End Function
    ''' <summary>Reaguje na událost <see cref="IBindingList.ListChanged"/> seznamu øádkù</summary>
    Private Sub Items_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        BindItems()
    End Sub
    ''' <summary>Contains value ot the <see cref="BindItemsBack"/> property</summary>
    Private _BindItemsBack As Boolean = False ' ItemsBindings.None
    ''' <summary>Gets or sets value indicating if items are bound back to datasource as changed.</summary>
    ''' <exception cref="NotImplementedException">Setting value to true. 2-wy data binding is not implemented yet.</exception>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue(False)> _
    <Description("Gets or sets value indicating if items are bound back to datasource as changed.")> _
    Public Property BindItemsBack() As Boolean
        <DebuggerStepThrough()> Get
            Return _BindItemsBack
        End Get
        Set(ByVal value As Boolean)
            If value Then Throw New NotImplementedException("2-way data binding is not implemented for DayView yet.")
            Dim old As Boolean = BindItemsBack
            _BindItemsBack = value
            If Not old AndAlso value Then
                BindRows()
            End If
        End Set
    End Property
    ''' <summary>Populates content of row from <see cref="DayViewItem.DataItem"/></summary>
    ''' <param name="Row">Row to bind content of</param>
    ''' <exception cref="InvalidOperationException">Error while obtainintg values of row properties. ==or== <see cref="ItemsDataMember"/> is null or an empty string.</exception>
    Private Sub BindRow(ByVal Row As DayViewRow)
        If Me.ItemsDataMember = "" OrElse Me.ItemsDataMember Is Nothing Then _
                Throw New InvalidOperationException("Property ItemsDataMember must not be null or an empty string in order to use items binding.")
        Row.Records.Clear()
        If Row.DataValue Is Nothing Then Return
        For Each Item As Object In ItemsSource
            Try
                If BindFromOtherDays OrElse GetItemRowID(Item) = Row.RowID AndAlso GetItemStart(Item).Date = Me.Start.Date Then _
                    Row.Records.Add(New DayViewItem(New DayViewItemWrapper(Item, Me))) 'GetItemStart(item),(getitemend(item)-getitemstart(item)).TotalMinutes ,GetItemText(item),geite
            Catch ex As Exception When TypeOf ex Is MissingMemberException OrElse _
                TypeOf ex Is TargetInvocationException OrElse _
                TypeOf ex Is TypeMismatchException
                Throw New InvalidOperationException("Error while loading row content. See InnerException for details.", ex)
            End Try
        Next Item
    End Sub
    ''' <summary>Contains value of the <see cref="LengthSec"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _LengthSec As Boolean
    ''' <summary>If true <see cref="ItemLengthMember"/>-named property is interpreted as seconds instead of as minutes</summary>
    ''' <remarks>Changing this value at runtime does not affect items already loaded.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue(False)> _
    <Description("If true ItemLengthMember-named property is interpreted as seconds instead of as minutes")> _
    Public Property LengthSec() As Boolean
        Get
            Return _LengthSec
        End Get
        Set(ByVal value As Boolean)
            _LengthSec = value
        End Set
    End Property

    ''' <summary>Contains value of the <see cref="LockedNot"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _LockedNot As Boolean
    ''' <summary>If true <see cref="ItemLockedMember"/>-named property is interpreted as enabled instead of as locked (negative).</summary>
    ''' <remarks>Changing this value at runtime does not affect items already loaded.</remarks>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue(False)> _
    <Description("If true ItemLockedMember-named property is interpreted as enabled instead of as locked (negative).")> _
    Public Property LockedNot() As Boolean
        Get
            Return _LockedNot
        End Get
        Set(ByVal value As Boolean)
            _LockedNot = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="BindFromOtherDays"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _BindFromOtherDays As Boolean
    ''' <summary>If true all items read from data source are stored in the control even it tehy are for days other than cuurently displayed.</summary>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <DefaultValue(False)> _
    <Description("If true all items read from data source are stored in the control even it tehy are for days other than cuurently displayed.")> _
    Public Property BindFromOtherDays() As Boolean
        Get
            Return _BindFromOtherDays
        End Get
        Set(ByVal value As Boolean)
            _BindFromOtherDays = value
        End Set
    End Property
#End Region

    Private Sub DayView_StartChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.StartChanged
        BindItems()
    End Sub
End Class

Partial Public Class DayViewRow
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="DataValue"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _DataValue As Object
    ''' <summary>Pokud je rodièovský <see cref="DayView"/> pøipojen na data, obsahuje položku dat, která reprezentuje tento øádek</summary>
    <Browsable(False)> _
    Public Property DataValue() As Object
        Get
            Return _DataValue
        End Get
        Private Set(ByVal value As Object)
            _DataValue = value
        End Set
    End Property
    ''' <summary>CTor z datového objketu</summary>
    ''' <param name="DataValue">Value</param>
    ''' <param name="DisplayMember">Member for <see cref="RowText"/></param>
    ''' <param name="IDMember">Member for <see cref="RowID"/></param>
    ''' <exception cref="InvalidCastException">Value of member (property or field) with nam <paramref name="IDMember"/> does not implement <see cref="IConvertible"/></exception>   
    Public Sub New(ByVal DataValue As Object, Optional ByVal DisplayMember As String = Nothing, Optional ByVal IDMember As String = Nothing)
        SetDataValue(DataValue, DisplayMember, IDMember)
    End Sub
    ''' <summary>Sets value of the <see cref="DataValue"/> property</summary>
    ''' <param name="DataValue">Value</param>
    ''' <param name="DisplayMember">Member for <see cref="RowText"/></param>
    ''' <param name="IDMember">Member for <see cref="RowID"/></param>
    ''' <exception cref="InvalidCastException">Value of member (property or field) with nam <paramref name="IDMember"/> does not implement <see cref="IConvertible"/></exception>
    Friend Sub SetDataValue(ByVal DataValue As Object, Optional ByVal DisplayMember As String = Nothing, Optional ByVal IDMember As String = Nothing)
        Me.DataValue = DataValue
        If DataValue IsNot Nothing Then
            If DisplayMember IsNot Nothing AndAlso DisplayMember <> String.Empty Then
                'Try get property
                '1st via TypeDescriptor
                Dim prp_TD As PropertyDescriptor = TypeDescriptor.GetProperties(DataValue).Item(DisplayMember)
                If prp_TD IsNot Nothing Then
                    Me.RowText = prp_TD.GetValue(DataValue).ToString
                Else '2nd via Type
                    Dim prp As PropertyInfo = DataValue.GetType.GetProperty(DisplayMember)
                    If prp IsNot Nothing AndAlso prp.GetGetMethod IsNot Nothing AndAlso prp.GetGetMethod.GetParameters.Length = 0 Then
                        Me.RowText = prp.GetValue(DataValue, Nothing).ToString
                    Else 'If not successfull, try get field
                        Dim fld As FieldInfo = DataValue.GetType.GetField(DisplayMember)
                        If fld IsNot Nothing Then
                            Me.RowText = fld.GetValue(DataValue).ToString
                        Else
                            Me.RowText = DataValue.ToString
                        End If
                    End If
                End If
            Else 'Default
                Me.RowText = DataValue.ToString
            End If
            If IDMember IsNot Nothing AndAlso IDMember <> String.Empty Then
                'Try get property
                '1st via TypeDescriptor
                Dim ID As Object = Nothing
                Dim IdGot As Boolean = False
                Dim prp_TD As PropertyDescriptor = TypeDescriptor.GetProperties(DataValue).Item(IDMember)
                If prp_TD IsNot Nothing Then
                    ID = prp_TD.GetValue(DataValue).ToString
                    IdGot = True
                Else '2nd via Type
                    Dim prp As PropertyInfo = DataValue.GetType.GetProperty(IDMember)

                    If prp IsNot Nothing AndAlso prp.GetGetMethod IsNot Nothing AndAlso prp.GetGetMethod.GetParameters.Length = 0 Then
                        ID = prp.GetValue(DataValue, Nothing)
                        IdGot = True
                    Else 'If not successfull, try get field
                        Dim fld As FieldInfo = DataValue.GetType.GetField(IDMember)
                        If fld IsNot Nothing Then
                            ID = fld.GetValue(DataValue)
                            IdGot = True
                        End If
                    End If
                End If
                If IdGot Then 'Try to convert to Integer
                    If ID IsNot Nothing AndAlso TypeOf ID Is IConvertible Then
                        Me.RowID = DirectCast(ID, IConvertible).ToInt32(System.Globalization.CultureInfo.CurrentCulture)
                    ElseIf ID Is Nothing Then
                        Me.RowID = 0
                    Else
                        Throw New InvalidCastException(String.Format("{0} does not implement IConvertible", ID.GetType.FullName))
                    End If
                Else
                    Me.RowID = -1
                End If
            End If
        Else
            Me.RowText = ""
            If IDMember IsNot Nothing AndAlso IDMember <> String.Empty Then
                Me.RowID = -1
            End If
        End If
    End Sub
End Class