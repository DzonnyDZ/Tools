Imports Tools.CollectionsT.GenericT, Tools, System.ComponentModel
Imports tools.WindowsT.FormsT.UtilitiesT
Imports System.Drawing.Design, System.Drawing
''' <summary>��dek zobrazen� v <see cref="DayView"/></summary>
Partial Public Class DayViewRow
    Implements Tools.IReportsChange
    ''' <summary>N�zev vlastnosti <see cref="RowID"/></summary>
    Public Const PropName_RowID As String = "RowID"
    ''' <summary>N�zev vlastnosti <see cref="TextColor"/></summary>
    Public Const PropName_TextColor As String = "TextColor"
    ''' <summary>N�zev vlastnosti <see cref="BackColor"/></summary>
    Public Const PropName_BackColor As String = "BackColor"
    ''' <summary>N�zev vlastnosti <see cref="RowText"/></summary>
    Public Const PropName_RowText As String = "RowText"
    ''' <summary>N�zev vlastnosti <see cref="Enabled"/></summary>
    Public Const PropName_Enabled As String = "Enabled"
    ''' <summary>CTor</summary>
    ''' <param name="Text">Text ��dku</param>
    ''' <param name="RowID">ID ��dku</param>
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
    ''' <summary>Text ��dky</summary>
    ''' <remarks>Pokud je rodi�ovsk� <see cref="DayView"/> propojen na data je tato vlastnost nastavena automaticky</remarks>
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
    ''' <summary>Barva pozad� ��dku</summary>
    ''' <remarks>Pokud chcete pou��t v�choz� nastavte hodnotu na <see cref="Color.Transparent"/></remarks>
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
    ''' <summary>Barva pozad� ��dku</summary>
    ''' <remarks>Pokud chcete pou��t v�choz� nastavte hodnotu na <see cref="Color.Transparent"/></remarks>
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
    ''' <summary>Barva textu z�hlav�</summary>
    ''' <remarks>Pokud chcete pou��t v�choz� nastavte hodnotu na <see cref="Color.Transparent"/></remarks>
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
    ''' <summary>ID datov� polo�ky ��dky</summary>
    ''' <remarks>Pokud rodi�ovsk� <see cref="DayView"/> je napojen na datov� zdroj a m� nastavenu vlastnost <see cref="DayView.RowIDMember"/>, je <see cref="RowID"/> nastaven automaticky. V takov�m p��pad� pokud je aktu�ln� polo�ka null je nastaven na -1, pokud aktu�ln� polo�ka neobsahuje dan�ho �lena, je nastaven na -1, pokud hodnota �lena je null, je nstaven na 0.</remarks>
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

#Region "Z�znamy"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Records"/></summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> Private WithEvents _Records As New ListWithEvents(Of DayViewItem)(True)
    ''' <summary>Z�znamy k dan�mu ��dku</summary>
    Public ReadOnly Property Records() As ListWithEvents(Of DayViewItem)
        <DebuggerStepThrough()> Get
            Return _Records
        End Get
    End Property
#Region "Events"

#End Region
#End Region

#Region "Record events"
    ''' <summary>Nastane po t� co je do kolekce <see cref="Records"/> p�id�n prvek</summary>
    ''' <param name="sender">Zdroj ud�losti</param>
    ''' <param name="e">Parametry ud�losti</param>
    Public Event RecordAdded(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).ItemIndexEventArgs)
    Private Sub _Records_Added(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).ItemIndexEventArgs) Handles _Records.Added
        RaiseEvent RecordAdded(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).ItemIndexEventArgs)(ItemOperationEventArgsBase.Operations.Added, e))
    End Sub
    ''' <summary>Nastane po t� co je kolekce <see cref="Records"/> vypr�zdn�na</summary>
    ''' <param name="sender">Zdroj ud�losti</param>
    ''' <param name="e">Parametry ud�losti</param>
    Public Event RecordsCleared(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).ItemsEventArgs)
    Private Sub _Records_Cleared(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).ItemsEventArgs) Handles _Records.Cleared
        RaiseEvent RecordsCleared(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).ItemsEventArgs)(ItemOperationEventArgsBase.Operations.Cleared, e))
    End Sub
    ''' <summary>Nastane po t� co je nahrazen prvek v kolekci <see cref="Records"/> jin�m</summary>
    ''' <param name="sender">Zdroj ud�losti</param>
    ''' <param name="e">Parametry ud�losti</param>
    Public Event RecordChanged(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).OldNewItemEventArgs)
    Private Sub _Records_ItemChanged(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).OldNewItemEventArgs) Handles _Records.ItemChanged
        RaiseEvent RecordChanged(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).OldNewItemEventArgs)(ItemOperationEventArgsBase.Operations.ItemChanged, e))
    End Sub
    ''' <summary>Nastane po t� co je zm�n�na n�kter� vlastnost prvku v kolekci <see cref="Records"/></summary>
    ''' <param name="sender">Zdroj ud�losti</param>
    ''' <param name="e">Parametry ud�losti</param>
    Public Event RecordValueChanged(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).ItemValueChangedEventArgs)
    Private Sub _Records_ItemValueChanged(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).ItemValueChangedEventArgs) Handles _Records.ItemValueChanged
        RaiseEvent RecordValueChanged(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).ItemValueChangedEventArgs)(ItemOperationEventArgsBase.Operations.ItemValueChanged, e))
    End Sub
    ''' <summary>Nastane po t� co je z kolekce <see cref="Records"/> odebr�n prvek</summary>
    ''' <param name="sender">Zdroj ud�losti</param>
    ''' <param name="e">Parametry ud�losti</param>
    ''' <remarks>Nenast�v� pokud je kolekce vy�i�t�na metodou <see cref="IList(Of DayViewItem).Clear"/>. Viz <seealso cref="RecordsCleared"/> a <seealso cref="ListWithEvents(Of DayViewItem).Removed"/></remarks>
    Public Event RecordRemoved(ByVal sender As DayViewRow, ByVal e As ListWithEvents(Of DayViewItem).ItemIndexEventArgs)
    Private Sub _Records_Removed(ByVal sender As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem), ByVal e As Tools.CollectionsT.GenericT.ListWithEvents(Of DayViewItem).ItemIndexEventArgs) Handles _Records.Removed
        RaiseEvent RecordRemoved(Me, e)
        RaiseEvent Changed(Me, New ItemOperationEventArgs(Of ListWithEvents(Of DayViewItem).ItemIndexEventArgs)(ItemOperationEventArgsBase.Operations.Removed, e))
    End Sub
#End Region
End Class




