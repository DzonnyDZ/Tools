Imports Tools.CollectionsT.GenericT, Tools, System.ComponentModel
Imports tools.WindowsT.FormsT.UtilitiesT
Imports System.Drawing.Design, System.Drawing
''' <summary>Øádek zobrazený v <see cref="DayView"/></summary>
Partial Public Class DayViewRow
    Implements Tools.IReportsChange
    ''' <summary>Název vlastnosti <see cref="RowID"/></summary>
    Public Const PropName_RowID As String = "RowID"
    ''' <summary>Název vlastnosti <see cref="TextColor"/></summary>
    Public Const PropName_TextColor As String = "TextColor"
    ''' <summary>Název vlastnosti <see cref="BackColor"/></summary>
    Public Const PropName_BackColor As String = "BackColor"
    ''' <summary>Název vlastnosti <see cref="RowText"/></summary>
    Public Const PropName_RowText As String = "RowText"
    ''' <summary>Název vlastnosti <see cref="Enabled"/></summary>
    Public Const PropName_Enabled As String = "Enabled"
    ''' <summary>CTor</summary>
    ''' <param name="Text">Text øádku</param>
    ''' <param name="RowID">ID øádku</param>
    Public Sub New(ByVal Text As String, Optional ByVal RowID As Integer = 0)
        Me.RowText = Text
        Me.RowID = RowID
    End Sub
#Region "Basic properties"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Tag"/></summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _Tag As Object
    ''' <summary>Tag</summary>
    Public Property Tag() As Object
        Get
            Return _Tag
        End Get
        Set(ByVal value As Object)
            _Tag = value
        End Set
    End Property
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RowText"/></summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _RowText As String
    ''' <summary>Text øádky</summary>
    ''' <remarks>Pokud je rodièovský <see cref="DayView"/> propojen na data je tato vlastnost nastavena automaticky</remarks>
    Public Property RowText() As String
        Get
            Return _RowText
        End Get
        Set(ByVal value As String)
            Dim old As String = _RowText
            _RowText = value
            If old <> value Then RaiseEvent Changed(Me, New IReportsChange.ValueChangedEventArgs(Of String)(old, value, PropName_RowText))
        End Set
    End Property
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="BackColor"/></summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> Private _BackColor As Color = Color.Transparent
    ''' <summary>Barva pozadí øádku</summary>
    ''' <remarks>Pokud chcete použít výchozí nastavte hodnotu na <see cref="Color.Transparent"/></remarks>
    <DefaultValue(GetType(Color), "Transparent")> _
    Public Property BackColor() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            Dim old As Color = BackColor
            _BackColor = value
            If old <> value Then RaiseEvent Changed(Me, New IReportsChange.ValueChangedEventArgs(Of Color)(old, value, PropName_BackColor))
        End Set
    End Property
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Enabled"/></summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> Private _Enabled As Boolean = True
    ''' <summary>Barva pozadí øádku</summary>
    ''' <remarks>Pokud chcete použít výchozí nastavte hodnotu na <see cref="Color.Transparent"/></remarks>
    <DefaultValue(True)> _
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            Dim old As Boolean = Enabled
            _Enabled = value
            If old <> value Then RaiseEvent Changed(Me, New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, PropName_Enabled))
        End Set
    End Property
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="TextColor"/></summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> Private _TextColor As Color = Color.Transparent
    ''' <summary>Barva textu záhlaví</summary>
    ''' <remarks>Pokud chcete použít výchozí nastavte hodnotu na <see cref="Color.Transparent"/></remarks>
    <DefaultValue(GetType(Color), "Transparent")> _
    Public Property TextColor() As Color
        Get
            Return _TextColor
        End Get
        Set(ByVal value As Color)
            Dim old As Color = TextColor
            _TextColor = value
            If old <> value Then RaiseEvent Changed(Me, New IReportsChange.ValueChangedEventArgs(Of Color)(old, value, PropName_TextColor))
        End Set
    End Property
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RowID"/></summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _RowID As Integer
    ''' <summary>ID datové položky øádky</summary>
    ''' <remarks>Pokud rodièovský <see cref="DayView"/> je napojen na datový zdroj a má nastavenu vlastnost <see cref="DayView.RowIDMember"/>, je <see cref="RowID"/> nastaven automaticky. V takovém pøípadì pokud je aktuální položka null je nastaven na -1, pokud aktuální položka neobsahuje daného èlena, je nastaven na -1, pokud hodnota èlena je null, je nstaven na 0.</remarks>
    Public Property RowID() As Integer
        Get
            Return _RowID
        End Get
        Set(ByVal value As Integer)
            Dim old As Integer = _RowID
            _RowID = value
            If old <> value Then RaiseEvent Changed(Me, New IReportsChange.ValueChangedEventArgs(Of Integer)(old, value, PropName_RowID))
        End Set
    End Property
#End Region
    ''' <summary>Raised when value of member changes</summary>
    ''' <param name="sender">The source of the event</param>
    ''' <param name="e">
    ''' Event information:
    ''' <see cref="IReportsChange.ValueChangedEventArgs"/> - when scalar properties are changed (in this case the <paramref name="e"/>.<see cref="IReportsChange.ValueChangedEventArgsBase.ValueName">ValueName</see> contains one of <see cref="DayViewRow"/>.PropName_... constants);
    ''' <see cref="ItemOperationEventArgsBase"/>'s derived class when change occures in <see cref="Records"/> collection
    ''' </param>
    Public Event Changed(ByVal sender As Tools.IReportsChange, ByVal e As System.EventArgs) Implements Tools.IReportsChange.Changed

#Region "Záznamy"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Records"/></summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private WithEvents _Records As New ListWithEvents(Of DayViewItem)(True)
    ''' <summary>Záznamy k danému øádku</summary>
    Public ReadOnly Property Records() As ListWithEvents(Of DayViewItem)
        <DebuggerStepThrough()> Get
            Return _Records
        End Get
    End Property
#Region "Events"

#End Region
#End Region

#Region "Record events"
    ''' <summary>Nastane po té co je do kolekce <see cref="Records"/> pøidán prvek</summary>
    ''' <param name="sender">Zdroj události</param>
    ''' <param name="e">Parametry události</param>
    Public Event RecordAdded(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).ItemIndexEventArgs)
    Private Sub _Records_Added(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).ItemIndexEventArgs) Handles _Records.Added
        RaiseEvent RecordAdded(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).ItemIndexEventArgs)(ItemOperationEventArgsBase.Operations.Added, e))
    End Sub
    ''' <summary>Nastane po té co je kolekce <see cref="Records"/> vyprázdnìna</summary>
    ''' <param name="sender">Zdroj události</param>
    ''' <param name="e">Parametry události</param>
    Public Event RecordsCleared(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).ItemsEventArgs)
    Private Sub _Records_Cleared(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).ItemsEventArgs) Handles _Records.Cleared
        RaiseEvent RecordsCleared(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).ItemsEventArgs)(ItemOperationEventArgsBase.Operations.Cleared, e))
    End Sub
    ''' <summary>Nastane po té co je nahrazen prvek v kolekci <see cref="Records"/> jiným</summary>
    ''' <param name="sender">Zdroj události</param>
    ''' <param name="e">Parametry události</param>
    Public Event RecordChanged(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).OldNewItemEventArgs)
    Private Sub _Records_ItemChanged(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).OldNewItemEventArgs) Handles _Records.ItemChanged
        RaiseEvent RecordChanged(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).OldNewItemEventArgs)(ItemOperationEventArgsBase.Operations.ItemChanged, e))
    End Sub
    ''' <summary>Nastane po té co je zmìnìna nìkterá vlastnost prvku v kolekci <see cref="Records"/></summary>
    ''' <param name="sender">Zdroj události</param>
    ''' <param name="e">Parametry události</param>
    Public Event RecordValueChanged(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).ItemValueChangedEventArgs)
    Private Sub _Records_ItemValueChanged(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).ItemValueChangedEventArgs) Handles _Records.ItemValueChanged
        RaiseEvent RecordValueChanged(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).ItemValueChangedEventArgs)(ItemOperationEventArgsBase.Operations.ItemValueChanged, e))
    End Sub
    ''' <summary>Nastane po té co je z kolekce <see cref="Records"/> odebrán prvek</summary>
    ''' <param name="sender">Zdroj události</param>
    ''' <param name="e">Parametry události</param>
    ''' <remarks>Nenastává pokud je kolekce vyèištìna metodou <see cref="IList(Of DayViewItem).Clear"/>. Viz <seealso cref="RecordsCleared"/> a <seealso cref="ListWithEvents(Of DayViewItem).Removed"/></remarks>
    Public Event RecordRemoved(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).ItemIndexEventArgs)
    Private Sub _Records_Removed(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).ItemIndexEventArgs) Handles _Records.Removed
        RaiseEvent RecordRemoved(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).ItemIndexEventArgs)(ItemOperationEventArgsBase.Operations.Removed, e))
    End Sub
#End Region
End Class




