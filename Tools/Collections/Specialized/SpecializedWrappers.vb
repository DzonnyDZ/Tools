'This code was generated from SpecializedWrappers.xml at 21. května 2008 by TransformCodeGenerator, Version=1.0.2833.35810, Culture=neutral, PublicKeyToken=null1.0.2833.35810
'Do not edit, your changes will be lost!
Imports System.ComponentModel, Tools.CollectionsT.GenericT, System.Runtime.CompilerServices
#If Config <= Nightly Then 'Stage:Nightly
Namespace CollectionsT.SpecializedT
    Partial Public MustInherit Class SpecializedWrapper
#Region "BitArray (Boolean)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Collections.BitArray"/></summary>
        ''' <param name="Collection">A <see cref="System.Boolean"/> to be wrapped</param>
        ''' <returns><see cref="BitArrayTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Collections.BitArray) As BitArrayTypeSafeWrapper
            Return New BitArrayTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Collections.BitArray"/> as <see cref="IIndexableCollection(Of System.Boolean, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class BitArrayTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Collections.BitArray, System.Boolean)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Collections.BitArray)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Boolean
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Boolean)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Collections.BitArray"/> to <see cref="BitArrayTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Collections.BitArray"/> to be converted</param>
            ''' <returns>A <see cref="BitArrayTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Collections.BitArray) As BitArrayTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New BitArrayTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Boolean)

                Collection.Length += 1
                Collection.Item(Collection.Length - 1) = item
        
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Boolean) As Boolean

                For i As Integer = 0 To Collection.Length - 1
                    If Collection.Item(i) = item Then
                        For j As Integer = i To Collection.Length - 2
                            Collection(j) = Collection(j + 1)
                        Next
                        Collection.Length -= 1
                        Return True
                    End If
                Next
                Return False
        
            End Function
Public Overrides Sub Clear()
Collection.Length = 0
            End Sub
        End Class
#End Region
#Region "AttributeCollection (Attribute)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.ComponentModel.AttributeCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Attribute"/> to be wrapped</param>
        ''' <returns><see cref="AttributeCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.ComponentModel.AttributeCollection) As AttributeCollectionTypeSafeWrapper
            Return New AttributeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.ComponentModel.AttributeCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Attribute, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class AttributeCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.ComponentModel.AttributeCollection, System.Attribute)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.ComponentModel.AttributeCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Attribute
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.ComponentModel.AttributeCollection"/> to <see cref="AttributeCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.ComponentModel.AttributeCollection"/> to be converted</param>
            ''' <returns>A <see cref="AttributeCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.ComponentModel.AttributeCollection) As AttributeCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New AttributeCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DesignerCollection (IDesignerHost)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.ComponentModel.Design.DesignerCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.ComponentModel.Design.IDesignerHost"/> to be wrapped</param>
        ''' <returns><see cref="DesignerCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.ComponentModel.Design.DesignerCollection) As DesignerCollectionTypeSafeWrapper
            Return New DesignerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.ComponentModel.Design.DesignerCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.ComponentModel.Design.IDesignerHost, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DesignerCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.ComponentModel.Design.DesignerCollection, System.ComponentModel.Design.IDesignerHost)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.ComponentModel.Design.DesignerCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.ComponentModel.Design.IDesignerHost
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.ComponentModel.Design.DesignerCollection"/> to <see cref="DesignerCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.ComponentModel.Design.DesignerCollection"/> to be converted</param>
            ''' <returns>A <see cref="DesignerCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.ComponentModel.Design.DesignerCollection) As DesignerCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DesignerCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataViewSettingCollection (DataViewSetting)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.DataViewSettingCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.DataViewSetting"/> to be wrapped</param>
        ''' <returns><see cref="DataViewSettingCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.DataViewSettingCollection) As DataViewSettingCollectionTypeSafeWrapper
            Return New DataViewSettingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.DataViewSettingCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Data.DataViewSetting, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataViewSettingCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Data.DataViewSettingCollection, System.Data.DataViewSetting)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.DataViewSettingCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Data.DataViewSetting
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Data.DataViewSettingCollection"/> to <see cref="DataViewSettingCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.DataViewSettingCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataViewSettingCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.DataViewSettingCollection) As DataViewSettingCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataViewSettingCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "OdbcErrorCollection (OdbcError)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.Odbc.OdbcErrorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.Odbc.OdbcError"/> to be wrapped</param>
        ''' <returns><see cref="OdbcErrorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.Odbc.OdbcErrorCollection) As OdbcErrorCollectionTypeSafeWrapper
            Return New OdbcErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.Odbc.OdbcErrorCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Data.Odbc.OdbcError, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class OdbcErrorCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Data.Odbc.OdbcErrorCollection, System.Data.Odbc.OdbcError)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.Odbc.OdbcErrorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Data.Odbc.OdbcError
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Data.Odbc.OdbcErrorCollection"/> to <see cref="OdbcErrorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.Odbc.OdbcErrorCollection"/> to be converted</param>
            ''' <returns>A <see cref="OdbcErrorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.Odbc.OdbcErrorCollection) As OdbcErrorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New OdbcErrorCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "OleDbErrorCollection (OleDbError)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.OleDb.OleDbErrorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.OleDb.OleDbError"/> to be wrapped</param>
        ''' <returns><see cref="OleDbErrorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.OleDb.OleDbErrorCollection) As OleDbErrorCollectionTypeSafeWrapper
            Return New OleDbErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.OleDb.OleDbErrorCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Data.OleDb.OleDbError, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class OleDbErrorCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Data.OleDb.OleDbErrorCollection, System.Data.OleDb.OleDbError)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.OleDb.OleDbErrorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Data.OleDb.OleDbError
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Data.OleDb.OleDbErrorCollection"/> to <see cref="OleDbErrorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.OleDb.OleDbErrorCollection"/> to be converted</param>
            ''' <returns>A <see cref="OleDbErrorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.OleDb.OleDbErrorCollection) As OleDbErrorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New OleDbErrorCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SqlErrorCollection (SqlError)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.SqlClient.SqlErrorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.SqlClient.SqlError"/> to be wrapped</param>
        ''' <returns><see cref="SqlErrorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.SqlClient.SqlErrorCollection) As SqlErrorCollectionTypeSafeWrapper
            Return New SqlErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.SqlClient.SqlErrorCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Data.SqlClient.SqlError, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SqlErrorCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Data.SqlClient.SqlErrorCollection, System.Data.SqlClient.SqlError)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.SqlClient.SqlErrorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Data.SqlClient.SqlError
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Data.SqlClient.SqlErrorCollection"/> to <see cref="SqlErrorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.SqlClient.SqlErrorCollection"/> to be converted</param>
            ''' <returns>A <see cref="SqlErrorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.SqlClient.SqlErrorCollection) As SqlErrorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SqlErrorCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "EventLogEntryCollection (EventLogEntry)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Diagnostics.EventLogEntryCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Diagnostics.EventLogEntry"/> to be wrapped</param>
        ''' <returns><see cref="EventLogEntryCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Diagnostics.EventLogEntryCollection) As EventLogEntryCollectionTypeSafeWrapper
            Return New EventLogEntryCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Diagnostics.EventLogEntryCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Diagnostics.EventLogEntry, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class EventLogEntryCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Diagnostics.EventLogEntryCollection, System.Diagnostics.EventLogEntry)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Diagnostics.EventLogEntryCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Diagnostics.EventLogEntry
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Diagnostics.EventLogEntryCollection"/> to <see cref="EventLogEntryCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Diagnostics.EventLogEntryCollection"/> to be converted</param>
            ''' <returns>A <see cref="EventLogEntryCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Diagnostics.EventLogEntryCollection) As EventLogEntryCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New EventLogEntryCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "PaperSourceCollection (PaperSource)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Drawing.Printing.PrinterSettings.PaperSourceCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Drawing.Printing.PaperSource"/> to be wrapped</param>
        ''' <returns><see cref="PaperSourceCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Drawing.Printing.PrinterSettings.PaperSourceCollection) As PaperSourceCollectionTypeSafeWrapper
            Return New PaperSourceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Drawing.Printing.PrinterSettings.PaperSourceCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Drawing.Printing.PaperSource, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class PaperSourceCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Drawing.Printing.PrinterSettings.PaperSourceCollection, System.Drawing.Printing.PaperSource)
            Implements IAddable(Of System.Drawing.Printing.PaperSource)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Drawing.Printing.PrinterSettings.PaperSourceCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Drawing.Printing.PaperSource
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Drawing.Printing.PrinterSettings.PaperSourceCollection"/> to <see cref="PaperSourceCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Drawing.Printing.PrinterSettings.PaperSourceCollection"/> to be converted</param>
            ''' <returns>A <see cref="PaperSourceCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Drawing.Printing.PrinterSettings.PaperSourceCollection) As PaperSourceCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New PaperSourceCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Drawing.Printing.PaperSource) Implements IAddable(Of System.Drawing.Printing.PaperSource).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "PaperSizeCollection (PaperSize)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Drawing.Printing.PrinterSettings.PaperSizeCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Drawing.Printing.PaperSize"/> to be wrapped</param>
        ''' <returns><see cref="PaperSizeCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Drawing.Printing.PrinterSettings.PaperSizeCollection) As PaperSizeCollectionTypeSafeWrapper
            Return New PaperSizeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Drawing.Printing.PrinterSettings.PaperSizeCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Drawing.Printing.PaperSize, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class PaperSizeCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Drawing.Printing.PrinterSettings.PaperSizeCollection, System.Drawing.Printing.PaperSize)
            Implements IAddable(Of System.Drawing.Printing.PaperSize)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Drawing.Printing.PrinterSettings.PaperSizeCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Drawing.Printing.PaperSize
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Drawing.Printing.PrinterSettings.PaperSizeCollection"/> to <see cref="PaperSizeCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Drawing.Printing.PrinterSettings.PaperSizeCollection"/> to be converted</param>
            ''' <returns>A <see cref="PaperSizeCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Drawing.Printing.PrinterSettings.PaperSizeCollection) As PaperSizeCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New PaperSizeCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Drawing.Printing.PaperSize) Implements IAddable(Of System.Drawing.Printing.PaperSize).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "PrinterResolutionCollection (PrinterResolution)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Drawing.Printing.PrinterResolution"/> to be wrapped</param>
        ''' <returns><see cref="PrinterResolutionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection) As PrinterResolutionCollectionTypeSafeWrapper
            Return New PrinterResolutionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Drawing.Printing.PrinterResolution, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class PrinterResolutionCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection, System.Drawing.Printing.PrinterResolution)
            Implements IAddable(Of System.Drawing.Printing.PrinterResolution)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Drawing.Printing.PrinterResolution
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection"/> to <see cref="PrinterResolutionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection"/> to be converted</param>
            ''' <returns>A <see cref="PrinterResolutionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection) As PrinterResolutionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New PrinterResolutionCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Drawing.Printing.PrinterResolution) Implements IAddable(Of System.Drawing.Printing.PrinterResolution).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "CookieCollection (Cookie)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Net.CookieCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Net.Cookie"/> to be wrapped</param>
        ''' <returns><see cref="CookieCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Net.CookieCollection) As CookieCollectionTypeSafeWrapper
            Return New CookieCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Net.CookieCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Net.Cookie, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CookieCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Net.CookieCollection, System.Net.Cookie)
            Implements IAddable(Of System.Net.Cookie)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Net.CookieCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Net.Cookie
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Net.CookieCollection"/> to <see cref="CookieCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Net.CookieCollection"/> to be converted</param>
            ''' <returns>A <see cref="CookieCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Net.CookieCollection) As CookieCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CookieCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Net.Cookie) Implements IAddable(Of System.Net.Cookie).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "AsnEncodedDataCollection (AsnEncodedData)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.Cryptography.AsnEncodedDataCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.Cryptography.AsnEncodedData"/> to be wrapped</param>
        ''' <returns><see cref="AsnEncodedDataCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.Cryptography.AsnEncodedDataCollection) As AsnEncodedDataCollectionTypeSafeWrapper
            Return New AsnEncodedDataCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.Cryptography.AsnEncodedDataCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Security.Cryptography.AsnEncodedData, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class AsnEncodedDataCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Security.Cryptography.AsnEncodedDataCollection, System.Security.Cryptography.AsnEncodedData)
            Implements IAddable(Of System.Security.Cryptography.AsnEncodedData)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.Cryptography.AsnEncodedDataCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Security.Cryptography.AsnEncodedData
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Security.Cryptography.AsnEncodedDataCollection"/> to <see cref="AsnEncodedDataCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.Cryptography.AsnEncodedDataCollection"/> to be converted</param>
            ''' <returns>A <see cref="AsnEncodedDataCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.Cryptography.AsnEncodedDataCollection) As AsnEncodedDataCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New AsnEncodedDataCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Security.Cryptography.AsnEncodedData) Implements IAddable(Of System.Security.Cryptography.AsnEncodedData).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "OidCollection (Oid)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.Cryptography.OidCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.Cryptography.Oid"/> to be wrapped</param>
        ''' <returns><see cref="OidCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.Cryptography.OidCollection) As OidCollectionTypeSafeWrapper
            Return New OidCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.Cryptography.OidCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Security.Cryptography.Oid, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class OidCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Security.Cryptography.OidCollection, System.Security.Cryptography.Oid)
            Implements IAddable(Of System.Security.Cryptography.Oid)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.Cryptography.OidCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Security.Cryptography.Oid
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Security.Cryptography.OidCollection"/> to <see cref="OidCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.Cryptography.OidCollection"/> to be converted</param>
            ''' <returns>A <see cref="OidCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.Cryptography.OidCollection) As OidCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New OidCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Security.Cryptography.Oid) Implements IAddable(Of System.Security.Cryptography.Oid).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "X509ExtensionCollection (X509Extension)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.Cryptography.X509Certificates.X509ExtensionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.Cryptography.X509Certificates.X509Extension"/> to be wrapped</param>
        ''' <returns><see cref="X509ExtensionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.Cryptography.X509Certificates.X509ExtensionCollection) As X509ExtensionCollectionTypeSafeWrapper
            Return New X509ExtensionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.Cryptography.X509Certificates.X509ExtensionCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Security.Cryptography.X509Certificates.X509Extension, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class X509ExtensionCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Security.Cryptography.X509Certificates.X509ExtensionCollection, System.Security.Cryptography.X509Certificates.X509Extension)
            Implements IAddable(Of System.Security.Cryptography.X509Certificates.X509Extension)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.Cryptography.X509Certificates.X509ExtensionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Security.Cryptography.X509Certificates.X509Extension
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Security.Cryptography.X509Certificates.X509ExtensionCollection"/> to <see cref="X509ExtensionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.Cryptography.X509Certificates.X509ExtensionCollection"/> to be converted</param>
            ''' <returns>A <see cref="X509ExtensionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.Cryptography.X509Certificates.X509ExtensionCollection) As X509ExtensionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New X509ExtensionCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Security.Cryptography.X509Certificates.X509Extension) Implements IAddable(Of System.Security.Cryptography.X509Certificates.X509Extension).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "X509ChainElementCollection (X509ChainElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.Cryptography.X509Certificates.X509ChainElementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.Cryptography.X509Certificates.X509ChainElement"/> to be wrapped</param>
        ''' <returns><see cref="X509ChainElementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.Cryptography.X509Certificates.X509ChainElementCollection) As X509ChainElementCollectionTypeSafeWrapper
            Return New X509ChainElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.Cryptography.X509Certificates.X509ChainElementCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Security.Cryptography.X509Certificates.X509ChainElement, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class X509ChainElementCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Security.Cryptography.X509Certificates.X509ChainElementCollection, System.Security.Cryptography.X509Certificates.X509ChainElement)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.Cryptography.X509Certificates.X509ChainElementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Security.Cryptography.X509Certificates.X509ChainElement
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Security.Cryptography.X509Certificates.X509ChainElementCollection"/> to <see cref="X509ChainElementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.Cryptography.X509Certificates.X509ChainElementCollection"/> to be converted</param>
            ''' <returns>A <see cref="X509ChainElementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.Cryptography.X509Certificates.X509ChainElementCollection) As X509ChainElementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New X509ChainElementCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "KeyContainerPermissionAccessEntryCollection (KeyContainerPermissionAccessEntry)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.Permissions.KeyContainerPermissionAccessEntryCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.Permissions.KeyContainerPermissionAccessEntry"/> to be wrapped</param>
        ''' <returns><see cref="KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.Permissions.KeyContainerPermissionAccessEntryCollection) As KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper
            Return New KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.Permissions.KeyContainerPermissionAccessEntryCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Security.Permissions.KeyContainerPermissionAccessEntry, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Security.Permissions.KeyContainerPermissionAccessEntryCollection, System.Security.Permissions.KeyContainerPermissionAccessEntry)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.Permissions.KeyContainerPermissionAccessEntryCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Security.Permissions.KeyContainerPermissionAccessEntry
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Security.Permissions.KeyContainerPermissionAccessEntryCollection"/> to <see cref="KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.Permissions.KeyContainerPermissionAccessEntryCollection"/> to be converted</param>
            ''' <returns>A <see cref="KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.Permissions.KeyContainerPermissionAccessEntryCollection) As KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ApplicationTrustCollection (ApplicationTrust)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.Policy.ApplicationTrustCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.Policy.ApplicationTrust"/> to be wrapped</param>
        ''' <returns><see cref="ApplicationTrustCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.Policy.ApplicationTrustCollection) As ApplicationTrustCollectionTypeSafeWrapper
            Return New ApplicationTrustCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.Policy.ApplicationTrustCollection"/> as <see cref="IIndexableCollection(Of System.Security.Policy.ApplicationTrust, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ApplicationTrustCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Security.Policy.ApplicationTrustCollection, System.Security.Policy.ApplicationTrust)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.Policy.ApplicationTrustCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Security.Policy.ApplicationTrust
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Security.Policy.ApplicationTrust)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Security.Policy.ApplicationTrust)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Security.Policy.ApplicationTrustCollection"/> to <see cref="ApplicationTrustCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.Policy.ApplicationTrustCollection"/> to be converted</param>
            ''' <returns>A <see cref="ApplicationTrustCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.Policy.ApplicationTrustCollection) As ApplicationTrustCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ApplicationTrustCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Security.Policy.ApplicationTrust)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Security.Policy.ApplicationTrust) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
        End Class
#End Region
#Region "CaptureCollection (Capture)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Text.RegularExpressions.CaptureCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Text.RegularExpressions.Capture"/> to be wrapped</param>
        ''' <returns><see cref="CaptureCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Text.RegularExpressions.CaptureCollection) As CaptureCollectionTypeSafeWrapper
            Return New CaptureCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Text.RegularExpressions.CaptureCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Text.RegularExpressions.Capture, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CaptureCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Text.RegularExpressions.CaptureCollection, System.Text.RegularExpressions.Capture)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Text.RegularExpressions.CaptureCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Text.RegularExpressions.Capture
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Text.RegularExpressions.CaptureCollection"/> to <see cref="CaptureCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Text.RegularExpressions.CaptureCollection"/> to be converted</param>
            ''' <returns>A <see cref="CaptureCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Text.RegularExpressions.CaptureCollection) As CaptureCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CaptureCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "GroupCollection (Group)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Text.RegularExpressions.GroupCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Text.RegularExpressions.Group"/> to be wrapped</param>
        ''' <returns><see cref="GroupCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Text.RegularExpressions.GroupCollection) As GroupCollectionTypeSafeWrapper
            Return New GroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Text.RegularExpressions.GroupCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Text.RegularExpressions.Group, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class GroupCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Text.RegularExpressions.GroupCollection, System.Text.RegularExpressions.Group)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Text.RegularExpressions.GroupCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Text.RegularExpressions.Group
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Text.RegularExpressions.GroupCollection"/> to <see cref="GroupCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Text.RegularExpressions.GroupCollection"/> to be converted</param>
            ''' <returns>A <see cref="GroupCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Text.RegularExpressions.GroupCollection) As GroupCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New GroupCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "MatchCollection (Match)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Text.RegularExpressions.MatchCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Text.RegularExpressions.Match"/> to be wrapped</param>
        ''' <returns><see cref="MatchCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Text.RegularExpressions.MatchCollection) As MatchCollectionTypeSafeWrapper
            Return New MatchCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Text.RegularExpressions.MatchCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Text.RegularExpressions.Match, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class MatchCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Text.RegularExpressions.MatchCollection, System.Text.RegularExpressions.Match)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Text.RegularExpressions.MatchCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Text.RegularExpressions.Match
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Text.RegularExpressions.MatchCollection"/> to <see cref="MatchCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Text.RegularExpressions.MatchCollection"/> to be converted</param>
            ''' <returns>A <see cref="MatchCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Text.RegularExpressions.MatchCollection) As MatchCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New MatchCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ControlCollection (Control)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.ControlCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.Control"/> to be wrapped</param>
        ''' <returns><see cref="WebControlCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.ControlCollection) As WebControlCollectionTypeSafeWrapper
            Return New WebControlCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.ControlCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.Control, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebControlCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.ControlCollection, System.Web.UI.Control)
            Implements IAddableRemovable(Of System.Web.UI.Control, Integer)
             Implements ISearchable(Of System.Web.UI.Control, Integer)
                 Implements IInsertable(Of System.Web.UI.Control, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.ControlCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.Control
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.Control)
                    Me.RemoveAt(index)
                    Me.Insert(index,value)
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.ControlCollection"/> to <see cref="WebControlCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.ControlCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebControlCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.ControlCollection) As WebControlCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebControlCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.Control)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.Control) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.UI.Control) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.Control, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.Control) As Integer Implements IReadOnlySearchable(Of System.Web.UI.Control, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Web.UI.Control) Implements IInsertable(Of System.Web.UI.Control, Integer).Insert
Collection.AddAt(index, item)
            End Sub
        End Class
#End Region
#Region "HtmlTableCellCollection (HtmlTableCell)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.HtmlControls.HtmlTableCellCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.HtmlControls.HtmlTableCell"/> to be wrapped</param>
        ''' <returns><see cref="HtmlTableCellCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.HtmlControls.HtmlTableCellCollection) As HtmlTableCellCollectionTypeSafeWrapper
            Return New HtmlTableCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.HtmlControls.HtmlTableCellCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.HtmlControls.HtmlTableCell, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HtmlTableCellCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.HtmlControls.HtmlTableCellCollection, System.Web.UI.HtmlControls.HtmlTableCell)
            Implements IAddableRemovable(Of System.Web.UI.HtmlControls.HtmlTableCell, Integer)
                 Implements IInsertable(Of System.Web.UI.HtmlControls.HtmlTableCell, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.HtmlControls.HtmlTableCellCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.HtmlControls.HtmlTableCell
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.HtmlControls.HtmlTableCell)
                    Me.RemoveAt(index)
                    Me.Insert(index,value)
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.HtmlControls.HtmlTableCellCollection"/> to <see cref="HtmlTableCellCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.HtmlControls.HtmlTableCellCollection"/> to be converted</param>
            ''' <returns>A <see cref="HtmlTableCellCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.HtmlControls.HtmlTableCellCollection) As HtmlTableCellCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HtmlTableCellCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.HtmlControls.HtmlTableCell)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.HtmlControls.HtmlTableCell) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Web.UI.HtmlControls.HtmlTableCell) Implements IInsertable(Of System.Web.UI.HtmlControls.HtmlTableCell, Integer).Insert
                Collection.Insert(index, item)
            End Sub
        End Class
#End Region
#Region "HtmlTableRowCollection (HtmlTableRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.HtmlControls.HtmlTableRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.HtmlControls.HtmlTableRow"/> to be wrapped</param>
        ''' <returns><see cref="HtmlTableRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.HtmlControls.HtmlTableRowCollection) As HtmlTableRowCollectionTypeSafeWrapper
            Return New HtmlTableRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.HtmlControls.HtmlTableRowCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.HtmlControls.HtmlTableRow, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HtmlTableRowCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.HtmlControls.HtmlTableRowCollection, System.Web.UI.HtmlControls.HtmlTableRow)
            Implements IAddableRemovable(Of System.Web.UI.HtmlControls.HtmlTableRow, Integer)
                 Implements IInsertable(Of System.Web.UI.HtmlControls.HtmlTableRow, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.HtmlControls.HtmlTableRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.HtmlControls.HtmlTableRow
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.HtmlControls.HtmlTableRow)
                    Me.RemoveAt(index)
                    Me.Insert(index,value)
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.HtmlControls.HtmlTableRowCollection"/> to <see cref="HtmlTableRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.HtmlControls.HtmlTableRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="HtmlTableRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.HtmlControls.HtmlTableRowCollection) As HtmlTableRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HtmlTableRowCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.HtmlControls.HtmlTableRow)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.HtmlControls.HtmlTableRow) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Web.UI.HtmlControls.HtmlTableRow) Implements IInsertable(Of System.Web.UI.HtmlControls.HtmlTableRow, Integer).Insert
                Collection.Insert(index, item)
            End Sub
        End Class
#End Region
#Region "ValidatorCollection (IValidator)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.ValidatorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.IValidator"/> to be wrapped</param>
        ''' <returns><see cref="ValidatorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.ValidatorCollection) As ValidatorCollectionTypeSafeWrapper
            Return New ValidatorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.ValidatorCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.IValidator, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ValidatorCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.ValidatorCollection, System.Web.UI.IValidator)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.ValidatorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.IValidator
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.IValidator)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Web.UI.IValidator)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.ValidatorCollection"/> to <see cref="ValidatorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.ValidatorCollection"/> to be converted</param>
            ''' <returns>A <see cref="ValidatorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.ValidatorCollection) As ValidatorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ValidatorCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.IValidator)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.IValidator) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                While Me.Count > 0
                    Dim en = Me.GetEnumerator
                    en.MoveNext
                    Me.Remove(en.Current)
                    en.Dispose
                End While
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.UI.IValidator) As Boolean
                Return Collection.Contains(item)
            End Function
        End Class
#End Region
#Region "DataGridColumnCollection (DataGridColumn)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.DataGridColumnCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.DataGridColumn"/> to be wrapped</param>
        ''' <returns><see cref="DataGridColumnCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.DataGridColumnCollection) As DataGridColumnCollectionTypeSafeWrapper
            Return New DataGridColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.DataGridColumnCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.WebControls.DataGridColumn, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataGridColumnCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.WebControls.DataGridColumnCollection, System.Web.UI.WebControls.DataGridColumn)
            Implements IAddableRemovable(Of System.Web.UI.WebControls.DataGridColumn, Integer)
             Implements ISearchable(Of System.Web.UI.WebControls.DataGridColumn, Integer)
                 Implements IInsertable(Of System.Web.UI.WebControls.DataGridColumn, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.DataGridColumnCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.WebControls.DataGridColumn
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.WebControls.DataGridColumn)
                    Me.RemoveAt(index)
                    Me.Insert(index,value)
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.DataGridColumnCollection"/> to <see cref="DataGridColumnCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.DataGridColumnCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataGridColumnCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.DataGridColumnCollection) As DataGridColumnCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataGridColumnCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.WebControls.DataGridColumn)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.WebControls.DataGridColumn) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.UI.WebControls.DataGridColumn) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.DataGridColumn, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.DataGridColumn) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.DataGridColumn, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Web.UI.WebControls.DataGridColumn) Implements IInsertable(Of System.Web.UI.WebControls.DataGridColumn, Integer).Insert
Collection.AddAt(index,item)
            End Sub
        End Class
#End Region
#Region "DataGridItemCollection (DataGridItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.DataGridItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.DataGridItem"/> to be wrapped</param>
        ''' <returns><see cref="DataGridItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.DataGridItemCollection) As DataGridItemCollectionTypeSafeWrapper
            Return New DataGridItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.DataGridItemCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.DataGridItem, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataGridItemCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.DataGridItemCollection, System.Web.UI.WebControls.DataGridItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.DataGridItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.DataGridItem
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.DataGridItemCollection"/> to <see cref="DataGridItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.DataGridItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataGridItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.DataGridItemCollection) As DataGridItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataGridItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataKeyArray (DataKey)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.DataKeyArray"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.DataKey"/> to be wrapped</param>
        ''' <returns><see cref="DataKeyArrayTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.DataKeyArray) As DataKeyArrayTypeSafeWrapper
            Return New DataKeyArrayTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.DataKeyArray"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.DataKey, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataKeyArrayTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.DataKeyArray, System.Web.UI.WebControls.DataKey)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.DataKeyArray)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.DataKey
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.DataKeyArray"/> to <see cref="DataKeyArrayTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.DataKeyArray"/> to be converted</param>
            ''' <returns>A <see cref="DataKeyArrayTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.DataKeyArray) As DataKeyArrayTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataKeyArrayTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataListItemCollection (DataListItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.DataListItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.DataListItem"/> to be wrapped</param>
        ''' <returns><see cref="DataListItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.DataListItemCollection) As DataListItemCollectionTypeSafeWrapper
            Return New DataListItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.DataListItemCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.DataListItem, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataListItemCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.DataListItemCollection, System.Web.UI.WebControls.DataListItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.DataListItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.DataListItem
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.DataListItemCollection"/> to <see cref="DataListItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.DataListItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataListItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.DataListItemCollection) As DataListItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataListItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DetailsViewRowCollection (DetailsViewRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.DetailsViewRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.DetailsViewRow"/> to be wrapped</param>
        ''' <returns><see cref="DetailsViewRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.DetailsViewRowCollection) As DetailsViewRowCollectionTypeSafeWrapper
            Return New DetailsViewRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.DetailsViewRowCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.DetailsViewRow, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DetailsViewRowCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.DetailsViewRowCollection, System.Web.UI.WebControls.DetailsViewRow)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.DetailsViewRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.DetailsViewRow
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.DetailsViewRowCollection"/> to <see cref="DetailsViewRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.DetailsViewRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="DetailsViewRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.DetailsViewRowCollection) As DetailsViewRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DetailsViewRowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "GridViewRowCollection (GridViewRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.GridViewRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.GridViewRow"/> to be wrapped</param>
        ''' <returns><see cref="GridViewRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.GridViewRowCollection) As GridViewRowCollectionTypeSafeWrapper
            Return New GridViewRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.GridViewRowCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.GridViewRow, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class GridViewRowCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.GridViewRowCollection, System.Web.UI.WebControls.GridViewRow)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.GridViewRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.GridViewRow
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.GridViewRowCollection"/> to <see cref="GridViewRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.GridViewRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="GridViewRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.GridViewRowCollection) As GridViewRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New GridViewRowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "MenuItemCollection (MenuItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.MenuItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.MenuItem"/> to be wrapped</param>
        ''' <returns><see cref="WebMenuItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.MenuItemCollection) As WebMenuItemCollectionTypeSafeWrapper
            Return New WebMenuItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.MenuItemCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.WebControls.MenuItem, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebMenuItemCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.WebControls.MenuItemCollection, System.Web.UI.WebControls.MenuItem)
            Implements IAddableRemovable(Of System.Web.UI.WebControls.MenuItem, Integer)
             Implements ISearchable(Of System.Web.UI.WebControls.MenuItem, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.MenuItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.WebControls.MenuItem
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.WebControls.MenuItem)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Web.UI.WebControls.MenuItem)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.MenuItemCollection"/> to <see cref="WebMenuItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.MenuItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebMenuItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.MenuItemCollection) As WebMenuItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebMenuItemCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.WebControls.MenuItem)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.WebControls.MenuItem) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.UI.WebControls.MenuItem) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.MenuItem, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.MenuItem) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.MenuItem, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "RepeaterItemCollection (RepeaterItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.RepeaterItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.RepeaterItem"/> to be wrapped</param>
        ''' <returns><see cref="RepeaterItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.RepeaterItemCollection) As RepeaterItemCollectionTypeSafeWrapper
            Return New RepeaterItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.RepeaterItemCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.RepeaterItem, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class RepeaterItemCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.RepeaterItemCollection, System.Web.UI.WebControls.RepeaterItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.RepeaterItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.RepeaterItem
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.RepeaterItemCollection"/> to <see cref="RepeaterItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.RepeaterItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="RepeaterItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.RepeaterItemCollection) As RepeaterItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New RepeaterItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SelectedDatesCollection (DateTime)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.SelectedDatesCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.DateTime"/> to be wrapped</param>
        ''' <returns><see cref="SelectedDatesCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.SelectedDatesCollection) As SelectedDatesCollectionTypeSafeWrapper
            Return New SelectedDatesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.SelectedDatesCollection"/> as <see cref="IIndexableCollection(Of System.DateTime, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SelectedDatesCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.WebControls.SelectedDatesCollection, System.DateTime)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.SelectedDatesCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.DateTime
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.DateTime)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.DateTime)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.SelectedDatesCollection"/> to <see cref="SelectedDatesCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.SelectedDatesCollection"/> to be converted</param>
            ''' <returns>A <see cref="SelectedDatesCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.SelectedDatesCollection) As SelectedDatesCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SelectedDatesCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.DateTime)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.DateTime) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.DateTime) As Boolean
                Return Collection.Contains(item)
            End Function
        End Class
#End Region
#Region "TreeNodeCollection (TreeNode)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.TreeNodeCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.TreeNode"/> to be wrapped</param>
        ''' <returns><see cref="WebTreeNodeCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.TreeNodeCollection) As WebTreeNodeCollectionTypeSafeWrapper
            Return New WebTreeNodeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.TreeNodeCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.WebControls.TreeNode, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebTreeNodeCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.WebControls.TreeNodeCollection, System.Web.UI.WebControls.TreeNode)
            Implements IAddableRemovable(Of System.Web.UI.WebControls.TreeNode, Integer)
             Implements ISearchable(Of System.Web.UI.WebControls.TreeNode, Integer)
                 Implements IInsertable(Of System.Web.UI.WebControls.TreeNode, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.TreeNodeCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.WebControls.TreeNode
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.WebControls.TreeNode)
                    Me.RemoveAt(index)
                    Me.Insert(index,value)
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.TreeNodeCollection"/> to <see cref="WebTreeNodeCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.TreeNodeCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebTreeNodeCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.TreeNodeCollection) As WebTreeNodeCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebTreeNodeCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.WebControls.TreeNode)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.WebControls.TreeNode) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.UI.WebControls.TreeNode) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.TreeNode, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.TreeNode) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.TreeNode, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Web.UI.WebControls.TreeNode) Implements IInsertable(Of System.Web.UI.WebControls.TreeNode, Integer).Insert
Collection.AddAt(index, item)
            End Sub
        End Class
#End Region
#Region "PersonalizationStateInfoCollection (PersonalizationStateInfo)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.PersonalizationStateInfo"/> to be wrapped</param>
        ''' <returns><see cref="PersonalizationStateInfoCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection) As PersonalizationStateInfoCollectionTypeSafeWrapper
            Return New PersonalizationStateInfoCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.WebControls.WebParts.PersonalizationStateInfo, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class PersonalizationStateInfoCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection, System.Web.UI.WebControls.WebParts.PersonalizationStateInfo)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.PersonalizationStateInfo
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.WebControls.WebParts.PersonalizationStateInfo)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Web.UI.WebControls.WebParts.PersonalizationStateInfo)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection"/> to <see cref="PersonalizationStateInfoCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection"/> to be converted</param>
            ''' <returns>A <see cref="PersonalizationStateInfoCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection) As PersonalizationStateInfoCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New PersonalizationStateInfoCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.WebControls.WebParts.PersonalizationStateInfo)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.WebControls.WebParts.PersonalizationStateInfo) As Boolean

                    Dim comparer As EqualityComparer(Of System.Web.UI.WebControls.WebParts.PersonalizationStateInfo) = EqualityComparer(Of System.Web.UI.WebControls.WebParts.PersonalizationStateInfo).Default
                    Dim Old As New List(Of System.Web.UI.WebControls.WebParts.PersonalizationStateInfo)(Me)
                    Me.Clear
                    Dim Removed As Boolean = False
                    For i% = 0 To Old.Count - 1
                        If Not Removed AndAlso ((item Is Nothing AndAlso Old(i) Is Nothing) OrElse (Item IsNot Nothing AndAlso comparer.Equals(Item, Old(i)))) Then _
                            Removed = True _
                        Else Me.Add(Old(i))
                    Next i
                    Return Removed
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
        End Class
#End Region
#Region "GridItemCollection (GridItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.GridItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.GridItem"/> to be wrapped</param>
        ''' <returns><see cref="GridItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.GridItemCollection) As GridItemCollectionTypeSafeWrapper
            Return New GridItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.GridItemCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Windows.Forms.GridItem, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class GridItemCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Windows.Forms.GridItemCollection, System.Windows.Forms.GridItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.GridItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Windows.Forms.GridItem
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Windows.Forms.GridItemCollection"/> to <see cref="GridItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.GridItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="GridItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.GridItemCollection) As GridItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New GridItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "HtmlElementCollection (HtmlElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.HtmlElementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.HtmlElement"/> to be wrapped</param>
        ''' <returns><see cref="HtmlElementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.HtmlElementCollection) As HtmlElementCollectionTypeSafeWrapper
            Return New HtmlElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.HtmlElementCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Windows.Forms.HtmlElement, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HtmlElementCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Windows.Forms.HtmlElementCollection, System.Windows.Forms.HtmlElement)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.HtmlElementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Windows.Forms.HtmlElement
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Windows.Forms.HtmlElementCollection"/> to <see cref="HtmlElementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.HtmlElementCollection"/> to be converted</param>
            ''' <returns>A <see cref="HtmlElementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.HtmlElementCollection) As HtmlElementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HtmlElementCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "HtmlWindowCollection (HtmlWindow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.HtmlWindowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.HtmlWindow"/> to be wrapped</param>
        ''' <returns><see cref="HtmlWindowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.HtmlWindowCollection) As HtmlWindowCollectionTypeSafeWrapper
            Return New HtmlWindowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.HtmlWindowCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Windows.Forms.HtmlWindow, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HtmlWindowCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Windows.Forms.HtmlWindowCollection, System.Windows.Forms.HtmlWindow)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.HtmlWindowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Windows.Forms.HtmlWindow
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Windows.Forms.HtmlWindowCollection"/> to <see cref="HtmlWindowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.HtmlWindowCollection"/> to be converted</param>
            ''' <returns>A <see cref="HtmlWindowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.HtmlWindowCollection) As HtmlWindowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HtmlWindowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "PropertyTabCollection (PropertyTab)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.PropertyGrid.PropertyTabCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.Design.PropertyTab"/> to be wrapped</param>
        ''' <returns><see cref="PropertyTabCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.PropertyGrid.PropertyTabCollection) As PropertyTabCollectionTypeSafeWrapper
            Return New PropertyTabCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.PropertyGrid.PropertyTabCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Windows.Forms.Design.PropertyTab, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class PropertyTabCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Windows.Forms.PropertyGrid.PropertyTabCollection, System.Windows.Forms.Design.PropertyTab)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.PropertyGrid.PropertyTabCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Windows.Forms.Design.PropertyTab
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Windows.Forms.PropertyGrid.PropertyTabCollection"/> to <see cref="PropertyTabCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.PropertyGrid.PropertyTabCollection"/> to be converted</param>
            ''' <returns>A <see cref="PropertyTabCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.PropertyGrid.PropertyTabCollection) As PropertyTabCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New PropertyTabCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "WindowCollection (Window)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.WindowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Window"/> to be wrapped</param>
        ''' <returns><see cref="WindowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.WindowCollection) As WindowCollectionTypeSafeWrapper
            Return New WindowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.WindowCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Windows.Window, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WindowCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Windows.WindowCollection, System.Windows.Window)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.WindowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Windows.Window
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Windows.WindowCollection"/> to <see cref="WindowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.WindowCollection"/> to be converted</param>
            ''' <returns>A <see cref="WindowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.WindowCollection) As WindowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WindowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "XmlAttributeCollection (XmlAttribute)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Xml.XmlAttributeCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Xml.XmlAttribute"/> to be wrapped</param>
        ''' <returns><see cref="XmlAttributeCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Xml.XmlAttributeCollection) As XmlAttributeCollectionTypeSafeWrapper
            Return New XmlAttributeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Xml.XmlAttributeCollection"/> as <see cref="IIndexableCollection(Of System.Xml.XmlAttribute, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class XmlAttributeCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Xml.XmlAttributeCollection, System.Xml.XmlAttribute)
            Implements IAddableRemovable(Of System.Xml.XmlAttribute, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Xml.XmlAttributeCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Xml.XmlAttribute
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Xml.XmlAttribute)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Xml.XmlAttribute)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Xml.XmlAttributeCollection"/> to <see cref="XmlAttributeCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Xml.XmlAttributeCollection"/> to be converted</param>
            ''' <returns>A <see cref="XmlAttributeCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Xml.XmlAttributeCollection) As XmlAttributeCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New XmlAttributeCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Xml.XmlAttribute)
Collection.Append(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Xml.XmlAttribute) As Boolean
Return Collection.Remove(Item) IsNot Nothing
            End Function
Public Overrides Sub Clear()
Collection.RemoveAll
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "ComponentCollection (IComponent)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.ComponentModel.ComponentCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.ComponentModel.IComponent"/> to be wrapped</param>
        ''' <returns><see cref="ComponentCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.ComponentModel.ComponentCollection) As ComponentCollectionTypeSafeWrapper
            Return New ComponentCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.ComponentModel.ComponentCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.ComponentModel.IComponent, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ComponentCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.ComponentModel.ComponentCollection, System.ComponentModel.IComponent)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.ComponentModel.ComponentCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.ComponentModel.IComponent
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.ComponentModel.ComponentCollection"/> to <see cref="ComponentCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.ComponentModel.ComponentCollection"/> to be converted</param>
            ''' <returns>A <see cref="ComponentCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.ComponentModel.ComponentCollection) As ComponentCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ComponentCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ConfigurationLocationCollection (ConfigurationLocation)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Configuration.ConfigurationLocationCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Configuration.ConfigurationLocation"/> to be wrapped</param>
        ''' <returns><see cref="ConfigurationLocationCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Configuration.ConfigurationLocationCollection) As ConfigurationLocationCollectionTypeSafeWrapper
            Return New ConfigurationLocationCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Configuration.ConfigurationLocationCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Configuration.ConfigurationLocation, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ConfigurationLocationCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Configuration.ConfigurationLocationCollection, System.Configuration.ConfigurationLocation)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Configuration.ConfigurationLocationCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Configuration.ConfigurationLocation
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Configuration.ConfigurationLocationCollection"/> to <see cref="ConfigurationLocationCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Configuration.ConfigurationLocationCollection"/> to be converted</param>
            ''' <returns>A <see cref="ConfigurationLocationCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Configuration.ConfigurationLocationCollection) As ConfigurationLocationCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ConfigurationLocationCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ProcessModuleCollection (ProcessModule)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Diagnostics.ProcessModuleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Diagnostics.ProcessModule"/> to be wrapped</param>
        ''' <returns><see cref="ProcessModuleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Diagnostics.ProcessModuleCollection) As ProcessModuleCollectionTypeSafeWrapper
            Return New ProcessModuleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Diagnostics.ProcessModuleCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Diagnostics.ProcessModule, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProcessModuleCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Diagnostics.ProcessModuleCollection, System.Diagnostics.ProcessModule)
             Implements IReadOnlySearchable(Of System.Diagnostics.ProcessModule, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Diagnostics.ProcessModuleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Diagnostics.ProcessModule
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Diagnostics.ProcessModuleCollection"/> to <see cref="ProcessModuleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Diagnostics.ProcessModuleCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProcessModuleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Diagnostics.ProcessModuleCollection) As ProcessModuleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProcessModuleCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Diagnostics.ProcessModule) As Boolean Implements IReadOnlySearchable(Of System.Diagnostics.ProcessModule, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Diagnostics.ProcessModule) As Integer Implements IReadOnlySearchable(Of System.Diagnostics.ProcessModule, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ProcessThreadCollection (ProcessThread)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Diagnostics.ProcessThreadCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Diagnostics.ProcessThread"/> to be wrapped</param>
        ''' <returns><see cref="ProcessThreadCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Diagnostics.ProcessThreadCollection) As ProcessThreadCollectionTypeSafeWrapper
            Return New ProcessThreadCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Diagnostics.ProcessThreadCollection"/> as <see cref="IIndexableCollection(Of System.Diagnostics.ProcessThread, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProcessThreadCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Diagnostics.ProcessThreadCollection, System.Diagnostics.ProcessThread)
             Implements ISearchable(Of System.Diagnostics.ProcessThread, Integer)
                 Implements IInsertable(Of System.Diagnostics.ProcessThread, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Diagnostics.ProcessThreadCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Diagnostics.ProcessThread
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Diagnostics.ProcessThread)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Diagnostics.ProcessThread)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Diagnostics.ProcessThreadCollection"/> to <see cref="ProcessThreadCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Diagnostics.ProcessThreadCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProcessThreadCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Diagnostics.ProcessThreadCollection) As ProcessThreadCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProcessThreadCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Diagnostics.ProcessThread)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Diagnostics.ProcessThread) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                While Me.Count > 0
                    Dim en = Me.GetEnumerator
                    en.MoveNext
                    Me.Remove(en.Current)
                    en.Dispose
                End While
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Diagnostics.ProcessThread) As Boolean Implements IReadOnlySearchable(Of System.Diagnostics.ProcessThread, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Diagnostics.ProcessThread) As Integer Implements IReadOnlySearchable(Of System.Diagnostics.ProcessThread, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Diagnostics.ProcessThread) Implements IInsertable(Of System.Diagnostics.ProcessThread, Integer).Insert
                Collection.Insert(index, item)
            End Sub
        End Class
#End Region
#Region "CategoryNameCollection (String)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Drawing.Design.CategoryNameCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.String"/> to be wrapped</param>
        ''' <returns><see cref="CategoryNameCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Drawing.Design.CategoryNameCollection) As CategoryNameCollectionTypeSafeWrapper
            Return New CategoryNameCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Drawing.Design.CategoryNameCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.String, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CategoryNameCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Drawing.Design.CategoryNameCollection, System.String)
             Implements IReadOnlySearchable(Of System.String, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Drawing.Design.CategoryNameCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.String
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Drawing.Design.CategoryNameCollection"/> to <see cref="CategoryNameCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Drawing.Design.CategoryNameCollection"/> to be converted</param>
            ''' <returns>A <see cref="CategoryNameCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Drawing.Design.CategoryNameCollection) As CategoryNameCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CategoryNameCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.String) As Boolean Implements IReadOnlySearchable(Of System.String, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.String) As Integer Implements IReadOnlySearchable(Of System.String, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ToolboxItemCollection (ToolboxItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Drawing.Design.ToolboxItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Drawing.Design.ToolboxItem"/> to be wrapped</param>
        ''' <returns><see cref="ToolboxItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Drawing.Design.ToolboxItemCollection) As ToolboxItemCollectionTypeSafeWrapper
            Return New ToolboxItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Drawing.Design.ToolboxItemCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Drawing.Design.ToolboxItem, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ToolboxItemCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Drawing.Design.ToolboxItemCollection, System.Drawing.Design.ToolboxItem)
             Implements IReadOnlySearchable(Of System.Drawing.Design.ToolboxItem, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Drawing.Design.ToolboxItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Drawing.Design.ToolboxItem
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Drawing.Design.ToolboxItemCollection"/> to <see cref="ToolboxItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Drawing.Design.ToolboxItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="ToolboxItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Drawing.Design.ToolboxItemCollection) As ToolboxItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ToolboxItemCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Drawing.Design.ToolboxItem) As Boolean Implements IReadOnlySearchable(Of System.Drawing.Design.ToolboxItem, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Drawing.Design.ToolboxItem) As Integer Implements IReadOnlySearchable(Of System.Drawing.Design.ToolboxItem, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "AuthorizationRuleCollection (AuthorizationRule)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.AccessControl.AuthorizationRuleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.AccessControl.AuthorizationRule"/> to be wrapped</param>
        ''' <returns><see cref="AuthorizationRuleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.AccessControl.AuthorizationRuleCollection) As AuthorizationRuleCollectionTypeSafeWrapper
            Return New AuthorizationRuleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.AccessControl.AuthorizationRuleCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Security.AccessControl.AuthorizationRule, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class AuthorizationRuleCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Security.AccessControl.AuthorizationRuleCollection, System.Security.AccessControl.AuthorizationRule)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.AccessControl.AuthorizationRuleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Security.AccessControl.AuthorizationRule
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Security.AccessControl.AuthorizationRuleCollection"/> to <see cref="AuthorizationRuleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.AccessControl.AuthorizationRuleCollection"/> to be converted</param>
            ''' <returns>A <see cref="AuthorizationRuleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.AccessControl.AuthorizationRuleCollection) As AuthorizationRuleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New AuthorizationRuleCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "WebBaseEventCollection (WebBaseEvent)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Management.WebBaseEventCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Management.WebBaseEvent"/> to be wrapped</param>
        ''' <returns><see cref="WebBaseEventCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Management.WebBaseEventCollection) As WebBaseEventCollectionTypeSafeWrapper
            Return New WebBaseEventCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Management.WebBaseEventCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Management.WebBaseEvent, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebBaseEventCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Management.WebBaseEventCollection, System.Web.Management.WebBaseEvent)
             Implements IReadOnlySearchable(Of System.Web.Management.WebBaseEvent, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Management.WebBaseEventCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Management.WebBaseEvent
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Management.WebBaseEventCollection"/> to <see cref="WebBaseEventCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Management.WebBaseEventCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebBaseEventCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Management.WebBaseEventCollection) As WebBaseEventCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebBaseEventCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.Management.WebBaseEvent) As Boolean Implements IReadOnlySearchable(Of System.Web.Management.WebBaseEvent, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.Management.WebBaseEvent) As Integer Implements IReadOnlySearchable(Of System.Web.Management.WebBaseEvent, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "CatalogPartCollection (Catalogpart)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.CatalogPartCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.Catalogpart"/> to be wrapped</param>
        ''' <returns><see cref="CatalogPartCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.CatalogPartCollection) As CatalogPartCollectionTypeSafeWrapper
            Return New CatalogPartCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.CatalogPartCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.Catalogpart, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CatalogPartCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.CatalogPartCollection, System.Web.UI.WebControls.WebParts.Catalogpart)
             Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.Catalogpart, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.CatalogPartCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.Catalogpart
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.CatalogPartCollection"/> to <see cref="CatalogPartCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.CatalogPartCollection"/> to be converted</param>
            ''' <returns>A <see cref="CatalogPartCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.CatalogPartCollection) As CatalogPartCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CatalogPartCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.UI.WebControls.WebParts.Catalogpart) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.Catalogpart, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.WebParts.Catalogpart) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.Catalogpart, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ConnectionInterfaceCollection (Type)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Type"/> to be wrapped</param>
        ''' <returns><see cref="ConnectionInterfaceCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection) As ConnectionInterfaceCollectionTypeSafeWrapper
            Return New ConnectionInterfaceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Type, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ConnectionInterfaceCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection, System.Type)
             Implements IReadOnlySearchable(Of System.Type, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Type
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection"/> to <see cref="ConnectionInterfaceCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection"/> to be converted</param>
            ''' <returns>A <see cref="ConnectionInterfaceCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection) As ConnectionInterfaceCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ConnectionInterfaceCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Type) As Boolean Implements IReadOnlySearchable(Of System.Type, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Type) As Integer Implements IReadOnlySearchable(Of System.Type, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ConsumerConnectionPointCollection (ConsumerConnectionPoint)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint"/> to be wrapped</param>
        ''' <returns><see cref="ConsumerConnectionPointCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection) As ConsumerConnectionPointCollectionTypeSafeWrapper
            Return New ConsumerConnectionPointCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ConsumerConnectionPointCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection, System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint)
             Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection"/> to <see cref="ConsumerConnectionPointCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection"/> to be converted</param>
            ''' <returns>A <see cref="ConsumerConnectionPointCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection) As ConsumerConnectionPointCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ConsumerConnectionPointCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "EditorPartCollection (EditorPart)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.EditorPartCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.EditorPart"/> to be wrapped</param>
        ''' <returns><see cref="EditorPartCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.EditorPartCollection) As EditorPartCollectionTypeSafeWrapper
            Return New EditorPartCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.EditorPartCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.EditorPart, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class EditorPartCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.EditorPartCollection, System.Web.UI.WebControls.WebParts.EditorPart)
             Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.EditorPart, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.EditorPartCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.EditorPart
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.EditorPartCollection"/> to <see cref="EditorPartCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.EditorPartCollection"/> to be converted</param>
            ''' <returns>A <see cref="EditorPartCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.EditorPartCollection) As EditorPartCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New EditorPartCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.UI.WebControls.WebParts.EditorPart) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.EditorPart, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.WebParts.EditorPart) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.EditorPart, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ProviderConnectionPointCollection (ProviderConnectionPoint)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.ProviderConnectionPoint"/> to be wrapped</param>
        ''' <returns><see cref="ProviderConnectionPointCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection) As ProviderConnectionPointCollectionTypeSafeWrapper
            Return New ProviderConnectionPointCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.ProviderConnectionPoint, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProviderConnectionPointCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection, System.Web.UI.WebControls.WebParts.ProviderConnectionPoint)
             Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.ProviderConnectionPoint, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.ProviderConnectionPoint
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection"/> to <see cref="ProviderConnectionPointCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProviderConnectionPointCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection) As ProviderConnectionPointCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProviderConnectionPointCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.UI.WebControls.WebParts.ProviderConnectionPoint) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.ProviderConnectionPoint, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.WebParts.ProviderConnectionPoint) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.ProviderConnectionPoint, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "TransformerTypeCollection (Type)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.TransformerTypeCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Type"/> to be wrapped</param>
        ''' <returns><see cref="TransformerTypeCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.TransformerTypeCollection) As TransformerTypeCollectionTypeSafeWrapper
            Return New TransformerTypeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.TransformerTypeCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Type, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TransformerTypeCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.TransformerTypeCollection, System.Type)
             Implements IReadOnlySearchable(Of System.Type, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.TransformerTypeCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Type
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.TransformerTypeCollection"/> to <see cref="TransformerTypeCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.TransformerTypeCollection"/> to be converted</param>
            ''' <returns>A <see cref="TransformerTypeCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.TransformerTypeCollection) As TransformerTypeCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TransformerTypeCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Type) As Boolean Implements IReadOnlySearchable(Of System.Type, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Type) As Integer Implements IReadOnlySearchable(Of System.Type, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "WebPartCollection (WebPart)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.WebPartCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.WebPart"/> to be wrapped</param>
        ''' <returns><see cref="WebPartCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartCollection) As WebPartCollectionTypeSafeWrapper
            Return New WebPartCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.WebPartCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.WebPart, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebPartCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.WebPartCollection, System.Web.UI.WebControls.WebParts.WebPart)
             Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPart, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.WebPart
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.WebPartCollection"/> to <see cref="WebPartCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.WebPartCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebPartCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.WebPartCollection) As WebPartCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebPartCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.UI.WebControls.WebParts.WebPart) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPart, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.WebParts.WebPart) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPart, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "WebPartDescriptionCollection (WebPartdescription)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.WebPartdescription"/> to be wrapped</param>
        ''' <returns><see cref="WebPartDescriptionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection) As WebPartDescriptionCollectionTypeSafeWrapper
            Return New WebPartDescriptionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.WebPartdescription, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebPartDescriptionCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection, System.Web.UI.WebControls.WebParts.WebPartdescription)
             Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartdescription, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.WebPartdescription
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection"/> to <see cref="WebPartDescriptionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebPartDescriptionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection) As WebPartDescriptionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebPartDescriptionCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.UI.WebControls.WebParts.WebPartdescription) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartdescription, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.WebParts.WebPartdescription) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartdescription, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "WebPartVerbCollection (WebPartVerb)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.WebPartVerbCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.WebPartVerb"/> to be wrapped</param>
        ''' <returns><see cref="WebPartVerbCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartVerbCollection) As WebPartVerbCollectionTypeSafeWrapper
            Return New WebPartVerbCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.WebPartVerbCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.WebPartVerb, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebPartVerbCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.WebPartVerbCollection, System.Web.UI.WebControls.WebParts.WebPartVerb)
             Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartVerb, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartVerbCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.WebPartVerb
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.WebPartVerbCollection"/> to <see cref="WebPartVerbCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.WebPartVerbCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebPartVerbCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.WebPartVerbCollection) As WebPartVerbCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebPartVerbCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.UI.WebControls.WebParts.WebPartVerb) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartVerb, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.WebParts.WebPartVerb) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartVerb, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "WebPartZoneCollection (WebPartZoneBase)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.WebPartZoneCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.WebPartZoneBase"/> to be wrapped</param>
        ''' <returns><see cref="WebPartZoneCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartZoneCollection) As WebPartZoneCollectionTypeSafeWrapper
            Return New WebPartZoneCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.WebPartZoneCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.WebPartZoneBase, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebPartZoneCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.WebPartZoneCollection, System.Web.UI.WebControls.WebParts.WebPartZoneBase)
             Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartZoneBase, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartZoneCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.WebPartZoneBase
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.WebPartZoneCollection"/> to <see cref="WebPartZoneCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.WebPartZoneCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebPartZoneCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.WebPartZoneCollection) As WebPartZoneCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebPartZoneCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Web.UI.WebControls.WebParts.WebPartZoneBase) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartZoneBase, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.WebParts.WebPartZoneBase) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.WebParts.WebPartZoneBase, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "FormCollection (Form)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.windows.Forms.FormCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.windows.Forms.Form"/> to be wrapped</param>
        ''' <returns><see cref="FormCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.windows.Forms.FormCollection) As FormCollectionTypeSafeWrapper
            Return New FormCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.windows.Forms.FormCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.windows.Forms.Form, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class FormCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.windows.Forms.FormCollection, System.windows.Forms.Form)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.windows.Forms.FormCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.windows.Forms.Form
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.windows.Forms.FormCollection"/> to <see cref="FormCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.windows.Forms.FormCollection"/> to be converted</param>
            ''' <returns>A <see cref="FormCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.windows.Forms.FormCollection) As FormCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New FormCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "InputLanguageCollection (InputLanguage)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.InputLanguageCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.InputLanguage"/> to be wrapped</param>
        ''' <returns><see cref="InputLanguageCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.InputLanguageCollection) As InputLanguageCollectionTypeSafeWrapper
            Return New InputLanguageCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.InputLanguageCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Windows.Forms.InputLanguage, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class InputLanguageCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Windows.Forms.InputLanguageCollection, System.Windows.Forms.InputLanguage)
             Implements IReadOnlySearchable(Of System.Windows.Forms.InputLanguage, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.InputLanguageCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Windows.Forms.InputLanguage
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Windows.Forms.InputLanguageCollection"/> to <see cref="InputLanguageCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.InputLanguageCollection"/> to be converted</param>
            ''' <returns>A <see cref="InputLanguageCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.InputLanguageCollection) As InputLanguageCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New InputLanguageCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Function Contains(ByVal item As System.Windows.Forms.InputLanguage) As Boolean Implements IReadOnlySearchable(Of System.Windows.Forms.InputLanguage, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Windows.Forms.InputLanguage) As Integer Implements IReadOnlySearchable(Of System.Windows.Forms.InputLanguage, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ConfigurationSectionCollection (ConfigurationSection)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Configuration.ConfigurationSectionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Configuration.ConfigurationSection"/> to be wrapped</param>
        ''' <returns><see cref="ConfigurationSectionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Configuration.ConfigurationSectionCollection) As ConfigurationSectionCollectionTypeSafeWrapper
            Return New ConfigurationSectionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Configuration.ConfigurationSectionCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Configuration.ConfigurationSection, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ConfigurationSectionCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Configuration.ConfigurationSectionCollection, System.Configuration.ConfigurationSection)
            Implements IRemovable(Of Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Configuration.ConfigurationSectionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Configuration.ConfigurationSection
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Configuration.ConfigurationSectionCollection"/> to <see cref="ConfigurationSectionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Configuration.ConfigurationSectionCollection"/> to be converted</param>
            ''' <returns>A <see cref="ConfigurationSectionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Configuration.ConfigurationSectionCollection) As ConfigurationSectionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ConfigurationSectionCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "ConfigurationSectionGroupCollection (ConfigurationSectionGroup)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Configuration.ConfigurationSectionGroupCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Configuration.ConfigurationSectionGroup"/> to be wrapped</param>
        ''' <returns><see cref="ConfigurationSectionGroupCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Configuration.ConfigurationSectionGroupCollection) As ConfigurationSectionGroupCollectionTypeSafeWrapper
            Return New ConfigurationSectionGroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Configuration.ConfigurationSectionGroupCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Configuration.ConfigurationSectionGroup, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ConfigurationSectionGroupCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Configuration.ConfigurationSectionGroupCollection, System.Configuration.ConfigurationSectionGroup)
            Implements IRemovable(Of Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Configuration.ConfigurationSectionGroupCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Configuration.ConfigurationSectionGroup
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Configuration.ConfigurationSectionGroupCollection"/> to <see cref="ConfigurationSectionGroupCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Configuration.ConfigurationSectionGroupCollection"/> to be converted</param>
            ''' <returns>A <see cref="ConfigurationSectionGroupCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Configuration.ConfigurationSectionGroupCollection) As ConfigurationSectionGroupCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ConfigurationSectionGroupCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "VirtualDirectoryMappingCollection (VirtualDirectoryMapping)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.VirtualDirectoryMappingCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.VirtualDirectoryMapping"/> to be wrapped</param>
        ''' <returns><see cref="VirtualDirectoryMappingCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.VirtualDirectoryMappingCollection) As VirtualDirectoryMappingCollectionTypeSafeWrapper
            Return New VirtualDirectoryMappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.VirtualDirectoryMappingCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.VirtualDirectoryMapping, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class VirtualDirectoryMappingCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.VirtualDirectoryMappingCollection, System.Web.Configuration.VirtualDirectoryMapping)
            Implements IRemovable(Of Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.VirtualDirectoryMappingCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.VirtualDirectoryMapping
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.VirtualDirectoryMappingCollection"/> to <see cref="VirtualDirectoryMappingCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.VirtualDirectoryMappingCollection"/> to be converted</param>
            ''' <returns>A <see cref="VirtualDirectoryMappingCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.VirtualDirectoryMappingCollection) As VirtualDirectoryMappingCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New VirtualDirectoryMappingCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "HttpCookieCollection (HttpCookie)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.HttpCookieCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.HttpCookie"/> to be wrapped</param>
        ''' <returns><see cref="HttpCookieCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.HttpCookieCollection) As HttpCookieCollectionTypeSafeWrapper
            Return New HttpCookieCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.HttpCookieCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.HttpCookie, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HttpCookieCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.HttpCookieCollection, System.Web.HttpCookie)
            Implements IAddableRemovable(Of System.Web.HttpCookie, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.HttpCookieCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.HttpCookie
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Private Property ItemRW(ByVal Index As Integer) As System.Web.HttpCookie Implements IIndexable(Of System.Web.HttpCookie, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Web.HttpCookie)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Web.HttpCookie)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.HttpCookieCollection"/> to <see cref="HttpCookieCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.HttpCookieCollection"/> to be converted</param>
            ''' <returns>A <see cref="HttpCookieCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.HttpCookieCollection) As HttpCookieCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HttpCookieCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.HttpCookie) Implements IAddable(Of System.Web.HttpCookie).Add
                Collection.Add(item)
            End Sub
Private Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
Collection.Remove(Collection.GetKey(index))
            End Sub
        End Class
#End Region
#Region "HttpFileCollection (HttpPostedFile)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.HttpFileCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.HttpPostedFile"/> to be wrapped</param>
        ''' <returns><see cref="HttpFileCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.HttpFileCollection) As HttpFileCollectionTypeSafeWrapper
            Return New HttpFileCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.HttpFileCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.HttpPostedFile, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HttpFileCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.HttpFileCollection, System.Web.HttpPostedFile)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.HttpFileCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.HttpPostedFile
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.HttpFileCollection"/> to <see cref="HttpFileCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.HttpFileCollection"/> to be converted</param>
            ''' <returns>A <see cref="HttpFileCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.HttpFileCollection) As HttpFileCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HttpFileCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "HttpModuleCollection (IHttpModule)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.HttpModuleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.IHttpModule"/> to be wrapped</param>
        ''' <returns><see cref="HttpModuleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.HttpModuleCollection) As HttpModuleCollectionTypeSafeWrapper
            Return New HttpModuleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.HttpModuleCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.IHttpModule, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HttpModuleCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.HttpModuleCollection, System.Web.IHttpModule)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.HttpModuleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.IHttpModule
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.HttpModuleCollection"/> to <see cref="HttpModuleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.HttpModuleCollection"/> to be converted</param>
            ''' <returns>A <see cref="HttpModuleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.HttpModuleCollection) As HttpModuleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HttpModuleCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ConnectionStringSettingsCollection (ConnectionStringSettings)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Configuration.ConnectionStringSettingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Configuration.ConnectionStringSettings"/> to be wrapped</param>
        ''' <returns><see cref="ConnectionStringSettingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Configuration.ConnectionStringSettingsCollection) As ConnectionStringSettingsCollectionTypeSafeWrapper
            Return New ConnectionStringSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Configuration.ConnectionStringSettingsCollection"/> as <see cref="IIndexableCollection(Of System.Configuration.ConnectionStringSettings, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ConnectionStringSettingsCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Configuration.ConnectionStringSettingsCollection, System.Configuration.ConnectionStringSettings)
            Implements IAddableRemovable(Of System.Configuration.ConnectionStringSettings, Integer)
             Implements ISearchable(Of System.Configuration.ConnectionStringSettings, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Configuration.ConnectionStringSettingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Configuration.ConnectionStringSettings
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Configuration.ConnectionStringSettings)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Configuration.ConnectionStringSettings)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Configuration.ConnectionStringSettingsCollection"/> to <see cref="ConnectionStringSettingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Configuration.ConnectionStringSettingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="ConnectionStringSettingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Configuration.ConnectionStringSettingsCollection) As ConnectionStringSettingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ConnectionStringSettingsCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Configuration.ConnectionStringSettings)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Configuration.ConnectionStringSettings) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Configuration.ConnectionStringSettings) As Boolean Implements IReadOnlySearchable(Of System.Configuration.ConnectionStringSettings, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Configuration.ConnectionStringSettings) As Integer Implements IReadOnlySearchable(Of System.Configuration.ConnectionStringSettings, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ProviderSettingsCollection (ProviderSettings)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Configuration.ProviderSettingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Configuration.ProviderSettings"/> to be wrapped</param>
        ''' <returns><see cref="ProviderSettingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Configuration.ProviderSettingsCollection) As ProviderSettingsCollectionTypeSafeWrapper
            Return New ProviderSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Configuration.ProviderSettingsCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Configuration.ProviderSettings, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProviderSettingsCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Configuration.ProviderSettingsCollection, System.Configuration.ProviderSettings)
            Implements IAddable(Of System.Configuration.ProviderSettings)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Configuration.ProviderSettingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Configuration.ProviderSettings
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Configuration.ProviderSettingsCollection"/> to <see cref="ProviderSettingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Configuration.ProviderSettingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProviderSettingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Configuration.ProviderSettingsCollection) As ProviderSettingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProviderSettingsCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Configuration.ProviderSettings) Implements IAddable(Of System.Configuration.ProviderSettings).Add
                Collection.Add(item)
            End Sub
Public Sub Clear()
                Collection.Clear()
            End Sub
        End Class
#End Region
#Region "AuthenticationModuleElementCollection (AuthenticationModuleElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Net.Configuration.AuthenticationModuleElementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Net.Configuration.AuthenticationModuleElement"/> to be wrapped</param>
        ''' <returns><see cref="AuthenticationModuleElementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Net.Configuration.AuthenticationModuleElementCollection) As AuthenticationModuleElementCollectionTypeSafeWrapper
            Return New AuthenticationModuleElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Net.Configuration.AuthenticationModuleElementCollection"/> as <see cref="IIndexableCollection(Of System.Net.Configuration.AuthenticationModuleElement, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class AuthenticationModuleElementCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Net.Configuration.AuthenticationModuleElementCollection, System.Net.Configuration.AuthenticationModuleElement)
            Implements IAddableRemovable(Of System.Net.Configuration.AuthenticationModuleElement, Integer)
             Implements ISearchable(Of System.Net.Configuration.AuthenticationModuleElement, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Net.Configuration.AuthenticationModuleElementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Net.Configuration.AuthenticationModuleElement
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Net.Configuration.AuthenticationModuleElement)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Net.Configuration.AuthenticationModuleElementCollection"/> to <see cref="AuthenticationModuleElementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Net.Configuration.AuthenticationModuleElementCollection"/> to be converted</param>
            ''' <returns>A <see cref="AuthenticationModuleElementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Net.Configuration.AuthenticationModuleElementCollection) As AuthenticationModuleElementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New AuthenticationModuleElementCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Net.Configuration.AuthenticationModuleElement)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Net.Configuration.AuthenticationModuleElement) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Net.Configuration.AuthenticationModuleElement) As Boolean Implements IReadOnlySearchable(Of System.Net.Configuration.AuthenticationModuleElement, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Net.Configuration.AuthenticationModuleElement) As Integer Implements IReadOnlySearchable(Of System.Net.Configuration.AuthenticationModuleElement, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "BypassElementCollection (BypassElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Net.Configuration.BypassElementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Net.Configuration.BypassElement"/> to be wrapped</param>
        ''' <returns><see cref="BypassElementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Net.Configuration.BypassElementCollection) As BypassElementCollectionTypeSafeWrapper
            Return New BypassElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Net.Configuration.BypassElementCollection"/> as <see cref="IIndexableCollection(Of System.Net.Configuration.BypassElement, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class BypassElementCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Net.Configuration.BypassElementCollection, System.Net.Configuration.BypassElement)
            Implements IAddableRemovable(Of System.Net.Configuration.BypassElement, Integer)
             Implements ISearchable(Of System.Net.Configuration.BypassElement, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Net.Configuration.BypassElementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Net.Configuration.BypassElement
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Net.Configuration.BypassElement)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Net.Configuration.BypassElementCollection"/> to <see cref="BypassElementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Net.Configuration.BypassElementCollection"/> to be converted</param>
            ''' <returns>A <see cref="BypassElementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Net.Configuration.BypassElementCollection) As BypassElementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New BypassElementCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Net.Configuration.BypassElement)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Net.Configuration.BypassElement) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Net.Configuration.BypassElement) As Boolean Implements IReadOnlySearchable(Of System.Net.Configuration.BypassElement, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Net.Configuration.BypassElement) As Integer Implements IReadOnlySearchable(Of System.Net.Configuration.BypassElement, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ConnectionManagementElementCollection (ConnectionManagementElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Net.Configuration.ConnectionManagementElementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Net.Configuration.ConnectionManagementElement"/> to be wrapped</param>
        ''' <returns><see cref="ConnectionManagementElementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Net.Configuration.ConnectionManagementElementCollection) As ConnectionManagementElementCollectionTypeSafeWrapper
            Return New ConnectionManagementElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Net.Configuration.ConnectionManagementElementCollection"/> as <see cref="IIndexableCollection(Of System.Net.Configuration.ConnectionManagementElement, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ConnectionManagementElementCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Net.Configuration.ConnectionManagementElementCollection, System.Net.Configuration.ConnectionManagementElement)
            Implements IAddableRemovable(Of System.Net.Configuration.ConnectionManagementElement, Integer)
             Implements ISearchable(Of System.Net.Configuration.ConnectionManagementElement, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Net.Configuration.ConnectionManagementElementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Net.Configuration.ConnectionManagementElement
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Net.Configuration.ConnectionManagementElement)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Net.Configuration.ConnectionManagementElementCollection"/> to <see cref="ConnectionManagementElementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Net.Configuration.ConnectionManagementElementCollection"/> to be converted</param>
            ''' <returns>A <see cref="ConnectionManagementElementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Net.Configuration.ConnectionManagementElementCollection) As ConnectionManagementElementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ConnectionManagementElementCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Net.Configuration.ConnectionManagementElement)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Net.Configuration.ConnectionManagementElement) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Net.Configuration.ConnectionManagementElement) As Boolean Implements IReadOnlySearchable(Of System.Net.Configuration.ConnectionManagementElement, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Net.Configuration.ConnectionManagementElement) As Integer Implements IReadOnlySearchable(Of System.Net.Configuration.ConnectionManagementElement, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "WebrequestModuleElementCollection (WebRequestModuleElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Net.Configuration.WebrequestModuleElementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Net.Configuration.WebRequestModuleElement"/> to be wrapped</param>
        ''' <returns><see cref="WebrequestModuleElementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Net.Configuration.WebrequestModuleElementCollection) As WebrequestModuleElementCollectionTypeSafeWrapper
            Return New WebrequestModuleElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Net.Configuration.WebrequestModuleElementCollection"/> as <see cref="IIndexableCollection(Of System.Net.Configuration.WebRequestModuleElement, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebrequestModuleElementCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Net.Configuration.WebrequestModuleElementCollection, System.Net.Configuration.WebRequestModuleElement)
            Implements IAddableRemovable(Of System.Net.Configuration.WebRequestModuleElement, Integer)
             Implements ISearchable(Of System.Net.Configuration.WebRequestModuleElement, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Net.Configuration.WebrequestModuleElementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Net.Configuration.WebRequestModuleElement
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Net.Configuration.WebRequestModuleElement)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Net.Configuration.WebrequestModuleElementCollection"/> to <see cref="WebrequestModuleElementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Net.Configuration.WebrequestModuleElementCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebrequestModuleElementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Net.Configuration.WebrequestModuleElementCollection) As WebrequestModuleElementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebrequestModuleElementCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Net.Configuration.WebRequestModuleElement)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Net.Configuration.WebRequestModuleElement) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Net.Configuration.WebRequestModuleElement) As Boolean Implements IReadOnlySearchable(Of System.Net.Configuration.WebRequestModuleElement, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Net.Configuration.WebRequestModuleElement) As Integer Implements IReadOnlySearchable(Of System.Net.Configuration.WebRequestModuleElement, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "AssemblyCollection (AssemblyInfo)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.AssemblyCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.AssemblyInfo"/> to be wrapped</param>
        ''' <returns><see cref="WebAssemblyCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.AssemblyCollection) As WebAssemblyCollectionTypeSafeWrapper
            Return New WebAssemblyCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.AssemblyCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.AssemblyInfo, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebAssemblyCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.AssemblyCollection, System.Web.Configuration.AssemblyInfo)
            Implements IAddableRemovable(Of System.Web.Configuration.AssemblyInfo, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.AssemblyCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.AssemblyInfo
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Private Property ItemRW(ByVal Index As Integer) As System.Web.Configuration.AssemblyInfo Implements IIndexable(Of System.Web.Configuration.AssemblyInfo, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.AssemblyInfo)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.AssemblyCollection"/> to <see cref="WebAssemblyCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.AssemblyCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebAssemblyCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.AssemblyCollection) As WebAssemblyCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebAssemblyCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Configuration.AssemblyInfo) Implements IAddable(Of System.Web.Configuration.AssemblyInfo).Add
                Collection.Add(item)
            End Sub
Public Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "AuthorizationRuleCollection (AuthorizationRule)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.AuthorizationRuleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.AuthorizationRule"/> to be wrapped</param>
        ''' <returns><see cref="WebAuthorizationRuleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.AuthorizationRuleCollection) As WebAuthorizationRuleCollectionTypeSafeWrapper
            Return New WebAuthorizationRuleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.AuthorizationRuleCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.AuthorizationRule, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebAuthorizationRuleCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.AuthorizationRuleCollection, System.Web.Configuration.AuthorizationRule)
            Implements IAddableRemovable(Of System.Web.Configuration.AuthorizationRule, Integer)
             Implements ISearchable(Of System.Web.Configuration.AuthorizationRule, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.AuthorizationRuleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.AuthorizationRule
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.AuthorizationRule)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.AuthorizationRuleCollection"/> to <see cref="WebAuthorizationRuleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.AuthorizationRuleCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebAuthorizationRuleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.AuthorizationRuleCollection) As WebAuthorizationRuleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebAuthorizationRuleCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.AuthorizationRule)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.AuthorizationRule) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.Configuration.AuthorizationRule) As Boolean Implements IReadOnlySearchable(Of System.Web.Configuration.AuthorizationRule, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.Configuration.AuthorizationRule) As Integer Implements IReadOnlySearchable(Of System.Web.Configuration.AuthorizationRule, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "BufferModesCollection (BufferModeSettings)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.BufferModesCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.BufferModeSettings"/> to be wrapped</param>
        ''' <returns><see cref="BufferModesCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.BufferModesCollection) As BufferModesCollectionTypeSafeWrapper
            Return New BufferModesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.BufferModesCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.BufferModeSettings, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class BufferModesCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.BufferModesCollection, System.Web.Configuration.BufferModeSettings)
            Implements IAddable(Of System.Web.Configuration.BufferModeSettings)
                 Implements IIndexable(Of System.Web.Configuration.BufferModeSettings, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.BufferModesCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.BufferModeSettings
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Public Property ItemRW(ByVal Index As Integer) As System.Web.Configuration.BufferModeSettings Implements IIndexable(Of System.Web.Configuration.BufferModeSettings, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.BufferModeSettings)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.BufferModesCollection"/> to <see cref="BufferModesCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.BufferModesCollection"/> to be converted</param>
            ''' <returns>A <see cref="BufferModesCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.BufferModesCollection) As BufferModesCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New BufferModesCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Configuration.BufferModeSettings) Implements IAddable(Of System.Web.Configuration.BufferModeSettings).Add
                Collection.Add(item)
            End Sub
Public Sub Clear()
                Collection.Clear()
            End Sub
        End Class
#End Region
#Region "BuildProviderCollection (BuildProvider)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.BuildProviderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.BuildProvider"/> to be wrapped</param>
        ''' <returns><see cref="BuildProviderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.BuildProviderCollection) As BuildProviderCollectionTypeSafeWrapper
            Return New BuildProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.BuildProviderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.BuildProvider, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class BuildProviderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.BuildProviderCollection, System.Web.Configuration.BuildProvider)
            Implements IAddableRemovable(Of System.Web.Configuration.BuildProvider, Integer)
                 Implements IIndexable(Of System.Web.Configuration.BuildProvider, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.BuildProviderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.BuildProvider
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Public Property ItemRW(ByVal Index As Integer) As System.Web.Configuration.BuildProvider Implements IIndexable(Of System.Web.Configuration.BuildProvider, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.BuildProvider)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.BuildProviderCollection"/> to <see cref="BuildProviderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.BuildProviderCollection"/> to be converted</param>
            ''' <returns>A <see cref="BuildProviderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.BuildProviderCollection) As BuildProviderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New BuildProviderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Configuration.BuildProvider) Implements IAddable(Of System.Web.Configuration.BuildProvider).Add
                Collection.Add(item)
            End Sub
Private Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "ClientTargetCollection (ClientTarget)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.ClientTargetCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.ClientTarget"/> to be wrapped</param>
        ''' <returns><see cref="ClientTargetCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.ClientTargetCollection) As ClientTargetCollectionTypeSafeWrapper
            Return New ClientTargetCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.ClientTargetCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.ClientTarget, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ClientTargetCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.ClientTargetCollection, System.Web.Configuration.ClientTarget)
            Implements IAddableRemovable(Of System.Web.Configuration.ClientTarget, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.ClientTargetCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.ClientTarget
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.ClientTarget)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.ClientTargetCollection"/> to <see cref="ClientTargetCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.ClientTargetCollection"/> to be converted</param>
            ''' <returns>A <see cref="ClientTargetCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.ClientTargetCollection) As ClientTargetCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ClientTargetCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.ClientTarget)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.ClientTarget) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "CodeSubDirectoriesCollection (CodeSubDirectory)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.CodeSubDirectoriesCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.CodeSubDirectory"/> to be wrapped</param>
        ''' <returns><see cref="CodeSubDirectoriesCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.CodeSubDirectoriesCollection) As CodeSubDirectoriesCollectionTypeSafeWrapper
            Return New CodeSubDirectoriesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.CodeSubDirectoriesCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.CodeSubDirectory, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeSubDirectoriesCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.CodeSubDirectoriesCollection, System.Web.Configuration.CodeSubDirectory)
            Implements IAddableRemovable(Of System.Web.Configuration.CodeSubDirectory, Integer)
                 Implements IIndexable(Of System.Web.Configuration.CodeSubDirectory, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.CodeSubDirectoriesCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.CodeSubDirectory
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Public Property ItemRW(ByVal Index As Integer) As System.Web.Configuration.CodeSubDirectory Implements IIndexable(Of System.Web.Configuration.CodeSubDirectory, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.CodeSubDirectory)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.CodeSubDirectoriesCollection"/> to <see cref="CodeSubDirectoriesCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.CodeSubDirectoriesCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeSubDirectoriesCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.CodeSubDirectoriesCollection) As CodeSubDirectoriesCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeSubDirectoriesCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Configuration.CodeSubDirectory) Implements IAddable(Of System.Web.Configuration.CodeSubDirectory).Add
                Collection.Add(item)
            End Sub
Public Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "CompilerCollection (Compiler)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.CompilerCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.Compiler"/> to be wrapped</param>
        ''' <returns><see cref="WebCompilerCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.CompilerCollection) As WebCompilerCollectionTypeSafeWrapper
            Return New WebCompilerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.CompilerCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.Compiler, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebCompilerCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.CompilerCollection, System.Web.Configuration.Compiler)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.CompilerCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.Compiler
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.CompilerCollection"/> to <see cref="WebCompilerCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.CompilerCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebCompilerCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.CompilerCollection) As WebCompilerCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebCompilerCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CustomErrorCollection (CustomError)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.CustomErrorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.CustomError"/> to be wrapped</param>
        ''' <returns><see cref="CustomErrorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.CustomErrorCollection) As CustomErrorCollectionTypeSafeWrapper
            Return New CustomErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.CustomErrorCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.CustomError, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CustomErrorCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.CustomErrorCollection, System.Web.Configuration.CustomError)
            Implements IAddableRemovable(Of System.Web.Configuration.CustomError, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.CustomErrorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.CustomError
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.CustomError)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.CustomErrorCollection"/> to <see cref="CustomErrorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.CustomErrorCollection"/> to be converted</param>
            ''' <returns>A <see cref="CustomErrorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.CustomErrorCollection) As CustomErrorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CustomErrorCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.CustomError)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.CustomError) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.StatusCode)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "EventMappingSettingsCollection (EventMappingSettings)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.EventMappingSettingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.EventMappingSettings"/> to be wrapped</param>
        ''' <returns><see cref="EventMappingSettingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.EventMappingSettingsCollection) As EventMappingSettingsCollectionTypeSafeWrapper
            Return New EventMappingSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.EventMappingSettingsCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.EventMappingSettings, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class EventMappingSettingsCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.EventMappingSettingsCollection, System.Web.Configuration.EventMappingSettings)
            Implements IAddableRemovable(Of System.Web.Configuration.EventMappingSettings, Integer)
             Implements ISearchable(Of System.Web.Configuration.EventMappingSettings, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.EventMappingSettingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.EventMappingSettings
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.EventMappingSettings)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.EventMappingSettingsCollection"/> to <see cref="EventMappingSettingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.EventMappingSettingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="EventMappingSettingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.EventMappingSettingsCollection) As EventMappingSettingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New EventMappingSettingsCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.EventMappingSettings)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.EventMappingSettings) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.Name)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.Configuration.EventMappingSettings) As Boolean Implements IReadOnlySearchable(Of System.Web.Configuration.EventMappingSettings, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.Configuration.EventMappingSettings) As Integer Implements IReadOnlySearchable(Of System.Web.Configuration.EventMappingSettings, Integer).IndexOf
Return Collection.IndexOf(Item.Name)
            End Function
        End Class
#End Region
#Region "ExpressionBuilderCollection (ExpressionBuilder)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.ExpressionBuilderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.ExpressionBuilder"/> to be wrapped</param>
        ''' <returns><see cref="ExpressionBuilderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.ExpressionBuilderCollection) As ExpressionBuilderCollectionTypeSafeWrapper
            Return New ExpressionBuilderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.ExpressionBuilderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.ExpressionBuilder, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ExpressionBuilderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.ExpressionBuilderCollection, System.Web.Configuration.ExpressionBuilder)
            Implements IAddableRemovable(Of System.Web.Configuration.ExpressionBuilder, Integer)
                 Implements IIndexable(Of System.Web.Configuration.ExpressionBuilder, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.ExpressionBuilderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.ExpressionBuilder
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Public Property ItemRW(ByVal Index As Integer) As System.Web.Configuration.ExpressionBuilder Implements IIndexable(Of System.Web.Configuration.ExpressionBuilder, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.ExpressionBuilder)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.ExpressionBuilderCollection"/> to <see cref="ExpressionBuilderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.ExpressionBuilderCollection"/> to be converted</param>
            ''' <returns>A <see cref="ExpressionBuilderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.ExpressionBuilderCollection) As ExpressionBuilderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ExpressionBuilderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Configuration.ExpressionBuilder) Implements IAddable(Of System.Web.Configuration.ExpressionBuilder).Add
                Collection.Add(item)
            End Sub
Public Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "FormsAuthenticationUserCollection (FormsAuthenticationUser)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.FormsAuthenticationUserCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.FormsAuthenticationUser"/> to be wrapped</param>
        ''' <returns><see cref="FormsAuthenticationUserCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.FormsAuthenticationUserCollection) As FormsAuthenticationUserCollectionTypeSafeWrapper
            Return New FormsAuthenticationUserCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.FormsAuthenticationUserCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.FormsAuthenticationUser, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class FormsAuthenticationUserCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.FormsAuthenticationUserCollection, System.Web.Configuration.FormsAuthenticationUser)
            Implements IAddableRemovable(Of System.Web.Configuration.FormsAuthenticationUser, Integer)
                 Implements IIndexable(Of System.Web.Configuration.FormsAuthenticationUser, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.FormsAuthenticationUserCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.FormsAuthenticationUser
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Public Property ItemRW(ByVal Index As Integer) As System.Web.Configuration.FormsAuthenticationUser Implements IIndexable(Of System.Web.Configuration.FormsAuthenticationUser, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.FormsAuthenticationUser)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.FormsAuthenticationUserCollection"/> to <see cref="FormsAuthenticationUserCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.FormsAuthenticationUserCollection"/> to be converted</param>
            ''' <returns>A <see cref="FormsAuthenticationUserCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.FormsAuthenticationUserCollection) As FormsAuthenticationUserCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New FormsAuthenticationUserCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Configuration.FormsAuthenticationUser) Implements IAddable(Of System.Web.Configuration.FormsAuthenticationUser).Add
                Collection.Add(item)
            End Sub
Public Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "HttpHandlerActionCollection (HttpHandlerAction)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.HttpHandlerActionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.HttpHandlerAction"/> to be wrapped</param>
        ''' <returns><see cref="HttpHandlerActionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.HttpHandlerActionCollection) As HttpHandlerActionCollectionTypeSafeWrapper
            Return New HttpHandlerActionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.HttpHandlerActionCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.HttpHandlerAction, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HttpHandlerActionCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.HttpHandlerActionCollection, System.Web.Configuration.HttpHandlerAction)
            Implements IAddableRemovable(Of System.Web.Configuration.HttpHandlerAction, Integer)
             Implements ISearchable(Of System.Web.Configuration.HttpHandlerAction, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.HttpHandlerActionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.HttpHandlerAction
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.HttpHandlerAction)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.HttpHandlerActionCollection"/> to <see cref="HttpHandlerActionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.HttpHandlerActionCollection"/> to be converted</param>
            ''' <returns>A <see cref="HttpHandlerActionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.HttpHandlerActionCollection) As HttpHandlerActionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HttpHandlerActionCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.HttpHandlerAction)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.HttpHandlerAction) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.Configuration.HttpHandlerAction) As Boolean Implements IReadOnlySearchable(Of System.Web.Configuration.HttpHandlerAction, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.Configuration.HttpHandlerAction) As Integer Implements IReadOnlySearchable(Of System.Web.Configuration.HttpHandlerAction, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "HttpModuleActionCollection (HttpModuleAction)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.HttpModuleActionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.HttpModuleAction"/> to be wrapped</param>
        ''' <returns><see cref="HttpModuleActionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.HttpModuleActionCollection) As HttpModuleActionCollectionTypeSafeWrapper
            Return New HttpModuleActionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.HttpModuleActionCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.HttpModuleAction, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HttpModuleActionCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.HttpModuleActionCollection, System.Web.Configuration.HttpModuleAction)
            Implements IAddableRemovable(Of System.Web.Configuration.HttpModuleAction, Integer)
             Implements ISearchable(Of System.Web.Configuration.HttpModuleAction, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.HttpModuleActionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.HttpModuleAction
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.HttpModuleAction)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.HttpModuleActionCollection"/> to <see cref="HttpModuleActionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.HttpModuleActionCollection"/> to be converted</param>
            ''' <returns>A <see cref="HttpModuleActionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.HttpModuleActionCollection) As HttpModuleActionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HttpModuleActionCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.HttpModuleAction)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.HttpModuleAction) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.Configuration.HttpModuleAction) As Boolean Implements IReadOnlySearchable(Of System.Web.Configuration.HttpModuleAction, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.Configuration.HttpModuleAction) As Integer Implements IReadOnlySearchable(Of System.Web.Configuration.HttpModuleAction, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "NamespaceCollection (NamespaceInfo)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.NamespaceCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.NamespaceInfo"/> to be wrapped</param>
        ''' <returns><see cref="WebNamespaceCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.NamespaceCollection) As WebNamespaceCollectionTypeSafeWrapper
            Return New WebNamespaceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.NamespaceCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.NamespaceInfo, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebNamespaceCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.NamespaceCollection, System.Web.Configuration.NamespaceInfo)
            Implements IAddableRemovable(Of System.Web.Configuration.NamespaceInfo, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.NamespaceCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.NamespaceInfo
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.NamespaceInfo)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.NamespaceCollection"/> to <see cref="WebNamespaceCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.NamespaceCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebNamespaceCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.NamespaceCollection) As WebNamespaceCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebNamespaceCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.NamespaceInfo)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.NamespaceInfo) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.Namespace)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "OutputCacheProfileCollection (OutputCacheProfile)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.OutputCacheProfileCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.OutputCacheProfile"/> to be wrapped</param>
        ''' <returns><see cref="OutputCacheProfileCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.OutputCacheProfileCollection) As OutputCacheProfileCollectionTypeSafeWrapper
            Return New OutputCacheProfileCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.OutputCacheProfileCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.OutputCacheProfile, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class OutputCacheProfileCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.OutputCacheProfileCollection, System.Web.Configuration.OutputCacheProfile)
            Implements IAddableRemovable(Of System.Web.Configuration.OutputCacheProfile, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.OutputCacheProfileCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.OutputCacheProfile
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.OutputCacheProfile)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.OutputCacheProfileCollection"/> to <see cref="OutputCacheProfileCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.OutputCacheProfileCollection"/> to be converted</param>
            ''' <returns>A <see cref="OutputCacheProfileCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.OutputCacheProfileCollection) As OutputCacheProfileCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New OutputCacheProfileCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.OutputCacheProfile)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.OutputCacheProfile) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.Name)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "ProfileGroupSettingsCollection (ProfileGroupSettings)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.ProfileGroupSettingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.ProfileGroupSettings"/> to be wrapped</param>
        ''' <returns><see cref="ProfileGroupSettingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.ProfileGroupSettingsCollection) As ProfileGroupSettingsCollectionTypeSafeWrapper
            Return New ProfileGroupSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.ProfileGroupSettingsCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.ProfileGroupSettings, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProfileGroupSettingsCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.ProfileGroupSettingsCollection, System.Web.Configuration.ProfileGroupSettings)
            Implements IAddableRemovable(Of System.Web.Configuration.ProfileGroupSettings, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.ProfileGroupSettingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.ProfileGroupSettings
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.ProfileGroupSettings)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.ProfileGroupSettingsCollection"/> to <see cref="ProfileGroupSettingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.ProfileGroupSettingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProfileGroupSettingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.ProfileGroupSettingsCollection) As ProfileGroupSettingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProfileGroupSettingsCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.ProfileGroupSettings)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.ProfileGroupSettings) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.Name)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "ProfilePropertySettingsCollection (ProfilePropertySettings)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.ProfilePropertySettingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.ProfilePropertySettings"/> to be wrapped</param>
        ''' <returns><see cref="ProfilePropertySettingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.ProfilePropertySettingsCollection) As ProfilePropertySettingsCollectionTypeSafeWrapper
            Return New ProfilePropertySettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.ProfilePropertySettingsCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.ProfilePropertySettings, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProfilePropertySettingsCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.ProfilePropertySettingsCollection, System.Web.Configuration.ProfilePropertySettings)
            Implements IAddableRemovable(Of System.Web.Configuration.ProfilePropertySettings, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.ProfilePropertySettingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.ProfilePropertySettings
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.ProfilePropertySettings)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.ProfilePropertySettingsCollection"/> to <see cref="ProfilePropertySettingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.ProfilePropertySettingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProfilePropertySettingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.ProfilePropertySettingsCollection) As ProfilePropertySettingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProfilePropertySettingsCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.ProfilePropertySettings)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.ProfilePropertySettings) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.Name)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "ProfileSettingsCollection (ProfileSettings)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.ProfileSettingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.ProfileSettings"/> to be wrapped</param>
        ''' <returns><see cref="ProfileSettingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.ProfileSettingsCollection) As ProfileSettingsCollectionTypeSafeWrapper
            Return New ProfileSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.ProfileSettingsCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.ProfileSettings, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProfileSettingsCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.ProfileSettingsCollection, System.Web.Configuration.ProfileSettings)
            Implements IAddableRemovable(Of System.Web.Configuration.ProfileSettings, Integer)
             Implements ISearchable(Of System.Web.Configuration.ProfileSettings, Integer)
                 Implements IInsertable(Of System.Web.Configuration.ProfileSettings, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.ProfileSettingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.ProfileSettings
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.ProfileSettings)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.ProfileSettingsCollection"/> to <see cref="ProfileSettingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.ProfileSettingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProfileSettingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.ProfileSettingsCollection) As ProfileSettingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProfileSettingsCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.ProfileSettings)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.ProfileSettings) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.Name)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.Configuration.ProfileSettings) As Boolean Implements IReadOnlySearchable(Of System.Web.Configuration.ProfileSettings, Integer).Contains
Return Collection.Contains(Item.Name)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.Configuration.ProfileSettings) As Integer Implements IReadOnlySearchable(Of System.Web.Configuration.ProfileSettings, Integer).IndexOf
Return Collection.IndexOf(Item.Name)
            End Function
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Web.Configuration.ProfileSettings) Implements IInsertable(Of System.Web.Configuration.ProfileSettings, Integer).Insert
                Collection.Insert(index, item)
            End Sub
        End Class
#End Region
#Region "ProtocolCollection (ProtocolElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.ProtocolCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.ProtocolElement"/> to be wrapped</param>
        ''' <returns><see cref="ProtocolCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.ProtocolCollection) As ProtocolCollectionTypeSafeWrapper
            Return New ProtocolCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.ProtocolCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.ProtocolElement, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProtocolCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.ProtocolCollection, System.Web.Configuration.ProtocolElement)
            Implements IAddableRemovable(Of System.Web.Configuration.ProtocolElement, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.ProtocolCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.ProtocolElement
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.ProtocolElement)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.ProtocolCollection"/> to <see cref="ProtocolCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.ProtocolCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProtocolCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.ProtocolCollection) As ProtocolCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProtocolCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.ProtocolElement)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.ProtocolElement) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "RuleSettingsCollection (RuleSettings)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.RuleSettingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.RuleSettings"/> to be wrapped</param>
        ''' <returns><see cref="RuleSettingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.RuleSettingsCollection) As RuleSettingsCollectionTypeSafeWrapper
            Return New RuleSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.RuleSettingsCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.RuleSettings, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class RuleSettingsCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.RuleSettingsCollection, System.Web.Configuration.RuleSettings)
            Implements IAddableRemovable(Of System.Web.Configuration.RuleSettings, Integer)
             Implements ISearchable(Of System.Web.Configuration.RuleSettings, Integer)
                 Implements IInsertable(Of System.Web.Configuration.RuleSettings, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.RuleSettingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.RuleSettings
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.RuleSettings)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.RuleSettingsCollection"/> to <see cref="RuleSettingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.RuleSettingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="RuleSettingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.RuleSettingsCollection) As RuleSettingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New RuleSettingsCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.RuleSettings)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.RuleSettings) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.Name)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.Configuration.RuleSettings) As Boolean Implements IReadOnlySearchable(Of System.Web.Configuration.RuleSettings, Integer).Contains
Return Collection.Contains(Item.Name)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.Configuration.RuleSettings) As Integer Implements IReadOnlySearchable(Of System.Web.Configuration.RuleSettings, Integer).IndexOf
Return Collection.IndexOf(Item.Name)
            End Function
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Web.Configuration.RuleSettings) Implements IInsertable(Of System.Web.Configuration.RuleSettings, Integer).Insert
                Collection.Insert(index, item)
            End Sub
        End Class
#End Region
#Region "SqlCacheDependencyDatabaseCollection (SqlCacheDependencyDatabase)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.SqlCacheDependencyDatabaseCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.SqlCacheDependencyDatabase"/> to be wrapped</param>
        ''' <returns><see cref="SqlCacheDependencyDatabaseCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.SqlCacheDependencyDatabaseCollection) As SqlCacheDependencyDatabaseCollectionTypeSafeWrapper
            Return New SqlCacheDependencyDatabaseCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.SqlCacheDependencyDatabaseCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.SqlCacheDependencyDatabase, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SqlCacheDependencyDatabaseCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.SqlCacheDependencyDatabaseCollection, System.Web.Configuration.SqlCacheDependencyDatabase)
            Implements IAddableRemovable(Of System.Web.Configuration.SqlCacheDependencyDatabase, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.SqlCacheDependencyDatabaseCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.SqlCacheDependencyDatabase
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.SqlCacheDependencyDatabase)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.SqlCacheDependencyDatabaseCollection"/> to <see cref="SqlCacheDependencyDatabaseCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.SqlCacheDependencyDatabaseCollection"/> to be converted</param>
            ''' <returns>A <see cref="SqlCacheDependencyDatabaseCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.SqlCacheDependencyDatabaseCollection) As SqlCacheDependencyDatabaseCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SqlCacheDependencyDatabaseCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.SqlCacheDependencyDatabase)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.SqlCacheDependencyDatabase) As Boolean

                    Dim OldCount As Integer = Me.Count
                    Collection.Remove(Item.Name)
                    Return Me.Count < OldCount
            
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "TagMapCollection (TagMapInfo)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.TagMapCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.TagMapInfo"/> to be wrapped</param>
        ''' <returns><see cref="TagMapCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.TagMapCollection) As TagMapCollectionTypeSafeWrapper
            Return New TagMapCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.TagMapCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.TagMapInfo, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TagMapCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.TagMapCollection, System.Web.Configuration.TagMapInfo)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.TagMapCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.TagMapInfo
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.TagMapInfo)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.TagMapCollection"/> to <see cref="TagMapCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.TagMapCollection"/> to be converted</param>
            ''' <returns>A <see cref="TagMapCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.TagMapCollection) As TagMapCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TagMapCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.TagMapInfo)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.TagMapInfo) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
        End Class
#End Region
#Region "TagPrefixCollection (TagPrefixInfo)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.TagPrefixCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.TagPrefixInfo"/> to be wrapped</param>
        ''' <returns><see cref="TagPrefixCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.TagPrefixCollection) As TagPrefixCollectionTypeSafeWrapper
            Return New TagPrefixCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.TagPrefixCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.TagPrefixInfo, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TagPrefixCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.TagPrefixCollection, System.Web.Configuration.TagPrefixInfo)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.TagPrefixCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.TagPrefixInfo
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.TagPrefixCollection"/> to <see cref="TagPrefixCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.TagPrefixCollection"/> to be converted</param>
            ''' <returns>A <see cref="TagPrefixCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.TagPrefixCollection) As TagPrefixCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TagPrefixCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TransformerInfoCollection (TransformerInfo)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.TransformerInfoCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.TransformerInfo"/> to be wrapped</param>
        ''' <returns><see cref="TransformerInfoCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.TransformerInfoCollection) As TransformerInfoCollectionTypeSafeWrapper
            Return New TransformerInfoCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.TransformerInfoCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.TransformerInfo, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TransformerInfoCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.TransformerInfoCollection, System.Web.Configuration.TransformerInfo)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.TransformerInfoCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.TransformerInfo
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.TransformerInfoCollection"/> to <see cref="TransformerInfoCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.TransformerInfoCollection"/> to be converted</param>
            ''' <returns>A <see cref="TransformerInfoCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.TransformerInfoCollection) As TransformerInfoCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TransformerInfoCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TrustLevelCollection (TrustLevel)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.TrustLevelCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.TrustLevel"/> to be wrapped</param>
        ''' <returns><see cref="TrustLevelCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.TrustLevelCollection) As TrustLevelCollectionTypeSafeWrapper
            Return New TrustLevelCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.TrustLevelCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Configuration.TrustLevel, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TrustLevelCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.TrustLevelCollection, System.Web.Configuration.TrustLevel)
            Implements IRemovable(Of Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.TrustLevelCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Configuration.TrustLevel
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.TrustLevelCollection"/> to <see cref="TrustLevelCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.TrustLevelCollection"/> to be converted</param>
            ''' <returns>A <see cref="TrustLevelCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.TrustLevelCollection) As TrustLevelCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TrustLevelCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "UrlmappingCollection (Urlmapping)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.UrlmappingCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Configuration.Urlmapping"/> to be wrapped</param>
        ''' <returns><see cref="UrlmappingCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.UrlmappingCollection) As UrlmappingCollectionTypeSafeWrapper
            Return New UrlmappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.UrlmappingCollection"/> as <see cref="IIndexableCollection(Of System.Web.Configuration.Urlmapping, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class UrlmappingCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.Configuration.UrlmappingCollection, System.Web.Configuration.Urlmapping)
            Implements IAddableRemovable(Of System.Web.Configuration.Urlmapping, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.UrlmappingCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.Configuration.Urlmapping
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.Configuration.Urlmapping)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.UrlmappingCollection"/> to <see cref="UrlmappingCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.UrlmappingCollection"/> to be converted</param>
            ''' <returns>A <see cref="UrlmappingCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.UrlmappingCollection) As UrlmappingCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New UrlmappingCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.Configuration.Urlmapping)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.Configuration.Urlmapping) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "SchemaImporterExtensionElementCollection (SchemaImporterExtensionElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Xml.Serialization.Configuration.SchemaImporterExtensionElement"/> to be wrapped</param>
        ''' <returns><see cref="SchemaImporterExtensionElementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection) As SchemaImporterExtensionElementCollectionTypeSafeWrapper
            Return New SchemaImporterExtensionElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection"/> as <see cref="IIndexableCollection(Of System.Xml.Serialization.Configuration.SchemaImporterExtensionElement, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SchemaImporterExtensionElementCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection, System.Xml.Serialization.Configuration.SchemaImporterExtensionElement)
            Implements IAddableRemovable(Of System.Xml.Serialization.Configuration.SchemaImporterExtensionElement, Integer)
             Implements ISearchable(Of System.Xml.Serialization.Configuration.SchemaImporterExtensionElement, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Xml.Serialization.Configuration.SchemaImporterExtensionElement
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Xml.Serialization.Configuration.SchemaImporterExtensionElement)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection"/> to <see cref="SchemaImporterExtensionElementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection"/> to be converted</param>
            ''' <returns>A <see cref="SchemaImporterExtensionElementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection) As SchemaImporterExtensionElementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SchemaImporterExtensionElementCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Xml.Serialization.Configuration.SchemaImporterExtensionElement)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Xml.Serialization.Configuration.SchemaImporterExtensionElement) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Xml.Serialization.Configuration.SchemaImporterExtensionElement) As Boolean Implements IReadOnlySearchable(Of System.Xml.Serialization.Configuration.SchemaImporterExtensionElement, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Xml.Serialization.Configuration.SchemaImporterExtensionElement) As Integer Implements IReadOnlySearchable(Of System.Xml.Serialization.Configuration.SchemaImporterExtensionElement, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "ProtectedConfigurationProviderCollection (ProtectedConfigurationProvider)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Configuration.ProtectedConfigurationProviderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Configuration.ProtectedConfigurationProvider"/> to be wrapped</param>
        ''' <returns><see cref="ProtectedConfigurationProviderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Configuration.ProtectedConfigurationProviderCollection) As ProtectedConfigurationProviderCollectionTypeSafeWrapper
            Return New ProtectedConfigurationProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Configuration.ProtectedConfigurationProviderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Configuration.ProtectedConfigurationProvider, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProtectedConfigurationProviderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Configuration.ProtectedConfigurationProviderCollection, System.Configuration.ProtectedConfigurationProvider)
            Implements IAddable(Of System.Configuration.ProtectedConfigurationProvider)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Configuration.ProtectedConfigurationProviderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Configuration.ProtectedConfigurationProvider
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Configuration.ProtectedConfigurationProviderCollection"/> to <see cref="ProtectedConfigurationProviderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Configuration.ProtectedConfigurationProviderCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProtectedConfigurationProviderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Configuration.ProtectedConfigurationProviderCollection) As ProtectedConfigurationProviderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProtectedConfigurationProviderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Configuration.ProtectedConfigurationProvider) Implements IAddable(Of System.Configuration.ProtectedConfigurationProvider).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "SettingsProviderCollection (SettingsProvider)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Configuration.SettingsProviderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Configuration.SettingsProvider"/> to be wrapped</param>
        ''' <returns><see cref="SettingsProviderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Configuration.SettingsProviderCollection) As SettingsProviderCollectionTypeSafeWrapper
            Return New SettingsProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Configuration.SettingsProviderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Configuration.SettingsProvider, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SettingsProviderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Configuration.SettingsProviderCollection, System.Configuration.SettingsProvider)
            Implements IAddable(Of System.Configuration.SettingsProvider)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Configuration.SettingsProviderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Configuration.SettingsProvider
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Configuration.SettingsProviderCollection"/> to <see cref="SettingsProviderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Configuration.SettingsProviderCollection"/> to be converted</param>
            ''' <returns>A <see cref="SettingsProviderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Configuration.SettingsProviderCollection) As SettingsProviderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SettingsProviderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Configuration.SettingsProvider) Implements IAddable(Of System.Configuration.SettingsProvider).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "ProfileProviderCollection (ProfileProvider)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Profile.ProfileProviderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Profile.ProfileProvider"/> to be wrapped</param>
        ''' <returns><see cref="ProfileProviderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Profile.ProfileProviderCollection) As ProfileProviderCollectionTypeSafeWrapper
            Return New ProfileProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Profile.ProfileProviderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Profile.ProfileProvider, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProfileProviderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Profile.ProfileProviderCollection, System.Web.Profile.ProfileProvider)
            Implements IAddable(Of System.Web.Profile.ProfileProvider)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Profile.ProfileProviderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Profile.ProfileProvider
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Profile.ProfileProviderCollection"/> to <see cref="ProfileProviderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Profile.ProfileProviderCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProfileProviderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Profile.ProfileProviderCollection) As ProfileProviderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProfileProviderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Profile.ProfileProvider) Implements IAddable(Of System.Web.Profile.ProfileProvider).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "MembershipProviderCollection (MembershipProvider)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Security.MembershipProviderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Security.MembershipProvider"/> to be wrapped</param>
        ''' <returns><see cref="MembershipProviderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Security.MembershipProviderCollection) As MembershipProviderCollectionTypeSafeWrapper
            Return New MembershipProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Security.MembershipProviderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Security.MembershipProvider, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class MembershipProviderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Security.MembershipProviderCollection, System.Web.Security.MembershipProvider)
            Implements IAddable(Of System.Web.Security.MembershipProvider)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Security.MembershipProviderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Security.MembershipProvider
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Security.MembershipProviderCollection"/> to <see cref="MembershipProviderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Security.MembershipProviderCollection"/> to be converted</param>
            ''' <returns>A <see cref="MembershipProviderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Security.MembershipProviderCollection) As MembershipProviderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New MembershipProviderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Security.MembershipProvider) Implements IAddable(Of System.Web.Security.MembershipProvider).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "RoleProviderCollection (RoleProvider)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Security.RoleProviderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.Security.RoleProvider"/> to be wrapped</param>
        ''' <returns><see cref="RoleProviderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Security.RoleProviderCollection) As RoleProviderCollectionTypeSafeWrapper
            Return New RoleProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Security.RoleProviderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.Security.RoleProvider, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class RoleProviderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Security.RoleProviderCollection, System.Web.Security.RoleProvider)
            Implements IAddable(Of System.Web.Security.RoleProvider)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Security.RoleProviderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.Security.RoleProvider
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Security.RoleProviderCollection"/> to <see cref="RoleProviderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Security.RoleProviderCollection"/> to be converted</param>
            ''' <returns>A <see cref="RoleProviderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Security.RoleProviderCollection) As RoleProviderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New RoleProviderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.Security.RoleProvider) Implements IAddable(Of System.Web.Security.RoleProvider).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "SiteMapProviderCollection (SiteMapProvider)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.SiteMapProviderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.SiteMapProvider"/> to be wrapped</param>
        ''' <returns><see cref="SiteMapProviderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.SiteMapProviderCollection) As SiteMapProviderCollectionTypeSafeWrapper
            Return New SiteMapProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.SiteMapProviderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.SiteMapProvider, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SiteMapProviderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.SiteMapProviderCollection, System.Web.SiteMapProvider)
            Implements IAddable(Of System.Web.SiteMapProvider)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.SiteMapProviderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.SiteMapProvider
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.SiteMapProviderCollection"/> to <see cref="SiteMapProviderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.SiteMapProviderCollection"/> to be converted</param>
            ''' <returns>A <see cref="SiteMapProviderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.SiteMapProviderCollection) As SiteMapProviderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SiteMapProviderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.SiteMapProvider) Implements IAddable(Of System.Web.SiteMapProvider).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "PersonalizationProviderCollection (PersonalizationProvider)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.PersonalizationProvider"/> to be wrapped</param>
        ''' <returns><see cref="PersonalizationProviderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection) As PersonalizationProviderCollectionTypeSafeWrapper
            Return New PersonalizationProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Web.UI.WebControls.WebParts.PersonalizationProvider, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class PersonalizationProviderCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection, System.Web.UI.WebControls.WebParts.PersonalizationProvider)
            Implements IAddable(Of System.Web.UI.WebControls.WebParts.PersonalizationProvider)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Web.UI.WebControls.WebParts.PersonalizationProvider
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection"/> to <see cref="PersonalizationProviderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection"/> to be converted</param>
            ''' <returns>A <see cref="PersonalizationProviderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection) As PersonalizationProviderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New PersonalizationProviderCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds item to collection</summary>
            ''' <param name="item">Item to be added</param>
            Public Sub Add(ByVal item As System.Web.UI.WebControls.WebParts.PersonalizationProvider) Implements IAddable(Of System.Web.UI.WebControls.WebParts.PersonalizationProvider).Add
                Collection.Add(item)
            End Sub
        End Class
#End Region
#Region "ConstraintCollection (Constraint)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.ConstraintCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.Constraint"/> to be wrapped</param>
        ''' <returns><see cref="ConstraintCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.ConstraintCollection) As ConstraintCollectionTypeSafeWrapper
            Return New ConstraintCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.ConstraintCollection"/> as <see cref="IIndexableCollection(Of System.Data.Constraint, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ConstraintCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Data.ConstraintCollection, System.Data.Constraint)
            Implements IAddableRemovable(Of System.Data.Constraint, Integer)
             Implements ISearchable(Of System.Data.Constraint, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.ConstraintCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Data.Constraint
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Data.Constraint)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Data.Constraint)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Data.ConstraintCollection"/> to <see cref="ConstraintCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.ConstraintCollection"/> to be converted</param>
            ''' <returns>A <see cref="ConstraintCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.ConstraintCollection) As ConstraintCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ConstraintCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Data.Constraint)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Data.Constraint) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Data.Constraint) As Boolean Implements IReadOnlySearchable(Of System.Data.Constraint, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Data.Constraint) As Integer Implements IReadOnlySearchable(Of System.Data.Constraint, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "DataColumnCollection (DataColumn)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.DataColumnCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.DataColumn"/> to be wrapped</param>
        ''' <returns><see cref="DataColumnCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.DataColumnCollection) As DataColumnCollectionTypeSafeWrapper
            Return New DataColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.DataColumnCollection"/> as <see cref="IIndexableCollection(Of System.Data.DataColumn, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataColumnCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Data.DataColumnCollection, System.Data.DataColumn)
            Implements IAddableRemovable(Of System.Data.DataColumn, Integer)
             Implements ISearchable(Of System.Data.DataColumn, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.DataColumnCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Data.DataColumn
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Data.DataColumn)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Data.DataColumn)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Data.DataColumnCollection"/> to <see cref="DataColumnCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.DataColumnCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataColumnCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.DataColumnCollection) As DataColumnCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataColumnCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Data.DataColumn)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Data.DataColumn) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Data.DataColumn) As Boolean Implements IReadOnlySearchable(Of System.Data.DataColumn, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Data.DataColumn) As Integer Implements IReadOnlySearchable(Of System.Data.DataColumn, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "DataRelationCollection (DataRelation)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.DataRelationCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.DataRelation"/> to be wrapped</param>
        ''' <returns><see cref="DataRelationCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.DataRelationCollection) As DataRelationCollectionTypeSafeWrapper
            Return New DataRelationCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.DataRelationCollection"/> as <see cref="IIndexableCollection(Of System.Data.DataRelation, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataRelationCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Data.DataRelationCollection, System.Data.DataRelation)
            Implements IAddableRemovable(Of System.Data.DataRelation, Integer)
             Implements ISearchable(Of System.Data.DataRelation, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.DataRelationCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Data.DataRelation
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Data.DataRelation)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Data.DataRelation)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Data.DataRelationCollection"/> to <see cref="DataRelationCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.DataRelationCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataRelationCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.DataRelationCollection) As DataRelationCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataRelationCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Data.DataRelation)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Data.DataRelation) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Data.DataRelation) As Boolean Implements IReadOnlySearchable(Of System.Data.DataRelation, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Data.DataRelation) As Integer Implements IReadOnlySearchable(Of System.Data.DataRelation, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "DataRowCollection (DataRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.DataRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.DataRow"/> to be wrapped</param>
        ''' <returns><see cref="DataRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.DataRowCollection) As DataRowCollectionTypeSafeWrapper
            Return New DataRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.DataRowCollection"/> as <see cref="IIndexableCollection(Of System.Data.DataRow, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataRowCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Data.DataRowCollection, System.Data.DataRow)
            Implements IAddableRemovable(Of System.Data.DataRow, Integer)
             Implements ISearchable(Of System.Data.DataRow, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.DataRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Data.DataRow
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Data.DataRow)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Data.DataRow)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Data.DataRowCollection"/> to <see cref="DataRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.DataRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.DataRowCollection) As DataRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataRowCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Data.DataRow)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Data.DataRow) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Data.DataRow) As Boolean Implements IReadOnlySearchable(Of System.Data.DataRow, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Data.DataRow) As Integer Implements IReadOnlySearchable(Of System.Data.DataRow, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "DataTableCollection (DataTable)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.DataTableCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.DataTable"/> to be wrapped</param>
        ''' <returns><see cref="DataTableCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.DataTableCollection) As DataTableCollectionTypeSafeWrapper
            Return New DataTableCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.DataTableCollection"/> as <see cref="IIndexableCollection(Of System.Data.DataTable, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataTableCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Data.DataTableCollection, System.Data.DataTable)
            Implements IAddableRemovable(Of System.Data.DataTable, Integer)
             Implements ISearchable(Of System.Data.DataTable, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.DataTableCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Data.DataTable
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Data.DataTable)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Data.DataTable)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Data.DataTableCollection"/> to <see cref="DataTableCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.DataTableCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataTableCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.DataTableCollection) As DataTableCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataTableCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Data.DataTable)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Data.DataTable) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Data.DataTable) As Boolean Implements IReadOnlySearchable(Of System.Data.DataTable, Integer).Contains
                Return Me.IndexOf(item) >= 0
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Data.DataTable) As Integer Implements IReadOnlySearchable(Of System.Data.DataTable, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
        End Class
#End Region
#Region "GenericAcl (GenericAce)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.AccessControl.GenericAcl"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.AccessControl.GenericAce"/> to be wrapped</param>
        ''' <returns><see cref="GenericAclTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.AccessControl.GenericAcl) As GenericAclTypeSafeWrapper
            Return New GenericAclTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.AccessControl.GenericAcl"/> as <see cref="IReadOnlyIndexableCollection(Of System.Security.AccessControl.GenericAce, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class GenericAclTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Security.AccessControl.GenericAcl, System.Security.AccessControl.GenericAce)
                 Implements IIndexable(Of System.Security.AccessControl.GenericAce, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.AccessControl.GenericAcl)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Security.AccessControl.GenericAce
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Public Property ItemRW(ByVal Index As Integer) As System.Security.AccessControl.GenericAce Implements IIndexable(Of System.Security.AccessControl.GenericAce, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Security.AccessControl.GenericAce)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Security.AccessControl.GenericAcl"/> to <see cref="GenericAclTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.AccessControl.GenericAcl"/> to be converted</param>
            ''' <returns>A <see cref="GenericAclTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.AccessControl.GenericAcl) As GenericAclTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New GenericAclTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "RawAcl (GenericAce)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.AccessControl.RawAcl"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.AccessControl.GenericAce"/> to be wrapped</param>
        ''' <returns><see cref="RawAclTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.AccessControl.RawAcl) As RawAclTypeSafeWrapper
            Return New RawAclTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.AccessControl.RawAcl"/> as <see cref="IReadOnlyIndexableCollection(Of System.Security.AccessControl.GenericAce, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class RawAclTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Security.AccessControl.RawAcl, System.Security.AccessControl.GenericAce)
            Implements IRemovable(Of Integer)
                 Implements IIndexable(Of System.Security.AccessControl.GenericAce, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.AccessControl.RawAcl)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Security.AccessControl.GenericAce
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            Public Property ItemRW(ByVal Index As Integer) As System.Security.AccessControl.GenericAce Implements IIndexable(Of System.Security.AccessControl.GenericAce, Integer).Item
                <DebuggerStepThrough()> Get
                    Return Item(index)
                End Get
                Set(ByVal value As System.Security.AccessControl.GenericAce)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Security.AccessControl.RawAcl"/> to <see cref="RawAclTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.AccessControl.RawAcl"/> to be converted</param>
            ''' <returns>A <see cref="RawAclTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.AccessControl.RawAcl) As RawAclTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New RawAclTypeSafeWrapper(a)
            End Operator
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
Collection.RemoveAce(index)
            End Sub
        End Class
#End Region
#Region "ViewCollection (View)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.ViewCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.View"/> to be wrapped</param>
        ''' <returns><see cref="WebViewCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.ViewCollection) As WebViewCollectionTypeSafeWrapper
            Return New WebViewCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.ViewCollection"/> as <see cref="IIndexableCollection(Of System.Web.UI.WebControls.View, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebViewCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Web.UI.WebControls.ViewCollection, System.Web.UI.WebControls.View)
            Implements IAddableRemovable(Of System.Web.UI.WebControls.View, Integer)
             Implements ISearchable(Of System.Web.UI.WebControls.View, Integer)
                 Implements IInsertable(Of System.Web.UI.WebControls.View, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.ViewCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Web.UI.WebControls.View
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Web.UI.WebControls.View)
                    Me.RemoveAt(index)
                    Me.Insert(index,value)
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.ViewCollection"/> to <see cref="WebViewCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.ViewCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebViewCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.ViewCollection) As WebViewCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebViewCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Web.UI.WebControls.View)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Web.UI.WebControls.View) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
            ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Contains(ByVal item As System.Web.UI.WebControls.View) As Boolean Implements IReadOnlySearchable(Of System.Web.UI.WebControls.View, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Gets index at which lies given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns -1.</returns>
            Function IndexOf(ByVal item As System.Web.UI.WebControls.View) As Integer Implements IReadOnlySearchable(Of System.Web.UI.WebControls.View, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
            ''' <summary>Inserts item into collection at specified index</summary>
            ''' <param name="index">Index to insert item onto</param>
            ''' <param name="item">Item to be inserted</param>
            Sub Insert(ByVal index As Integer, ByVal item As System.Web.UI.WebControls.View) Implements IInsertable(Of System.Web.UI.WebControls.View, Integer).Insert
Collection.AddAt(index, item)
            End Sub
        End Class
#End Region
#Region "BindingsCollection (Binding)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.BindingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.Binding"/> to be wrapped</param>
        ''' <returns><see cref="BindingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.BindingsCollection) As BindingsCollectionTypeSafeWrapper
            Return New BindingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.BindingsCollection"/> as <see cref="IReadOnlyIndexableCollection(Of System.Windows.Forms.Binding, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class BindingsCollectionTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Windows.Forms.BindingsCollection, System.Windows.Forms.Binding)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.BindingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.Windows.Forms.Binding
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Windows.Forms.BindingsCollection"/> to <see cref="BindingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.BindingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="BindingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.BindingsCollection) As BindingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New BindingsCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ControlBindingsCollection (Binding)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ControlBindingsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.Binding"/> to be wrapped</param>
        ''' <returns><see cref="ControlBindingsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ControlBindingsCollection) As ControlBindingsCollectionTypeSafeWrapper
            Return New ControlBindingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ControlBindingsCollection"/> as <see cref="IIndexableCollection(Of System.Windows.Forms.Binding, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ControlBindingsCollectionTypeSafeWrapper
            Inherits SpecializedWrapper(Of System.Windows.Forms.ControlBindingsCollection, System.Windows.Forms.Binding)
            Implements IAddableRemovable(Of System.Windows.Forms.Binding, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ControlBindingsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Public Overrides Property Item(ByVal index As Integer) As System.Windows.Forms.Binding
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As System.Windows.Forms.Binding)
                    If index < 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception
                    Dim OldCollection As New List(Of System.Windows.Forms.Binding)(Me)
                    Me.Clear
                    For i% = 0 To OldCollection.Count - 1
                        If i = index Then
                            Me.Add(value)
                        Else
                            Me.Add(OldCollection(i))
                        End If
                    Next i
                End Set
            End Property
            ''' <summary>Converts <see cref="System.Windows.Forms.ControlBindingsCollection"/> to <see cref="ControlBindingsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ControlBindingsCollection"/> to be converted</param>
            ''' <returns>A <see cref="ControlBindingsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ControlBindingsCollection) As ControlBindingsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ControlBindingsCollectionTypeSafeWrapper(a)
            End Operator
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Sub Add(ByVal item As System.Windows.Forms.Binding)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public Overrides Function Remove(ByVal item As System.Windows.Forms.Binding) As Boolean
                Dim OldCount = Count
                Collection.Remove(item)
                Return Count < OldCount
            End Function
Public Overrides Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "AdapterDictionary (String)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.Configuration.AdapterDictionary"/></summary>
        ''' <param name="Collection">A <see cref="System.String"/> to be wrapped</param>
        ''' <returns><see cref="AdapterDictionaryTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.Configuration.AdapterDictionary) As AdapterDictionaryTypeSafeWrapper
            Return New AdapterDictionaryTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.Configuration.AdapterDictionary"/> as <see cref="IReadOnlyIndexableCollection(Of System.String, Integer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class AdapterDictionaryTypeSafeWrapper
            Inherits SpecializedReadOnlyWrapper(Of System.Web.Configuration.AdapterDictionary, System.String)
            Implements IRemovable(Of Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.Configuration.AdapterDictionary)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Gets or sets value on specified index</summary>
            ''' <param name="index">Index to set or obtain value</param>
            ''' <returns>Value lying on specified <paramref name="index"/></returns>
            ''' <value>New value to be stored at specified index</value>
            ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As System.String
                Get
                    Return Collection.Item(index)
                End Get
            End Property
            ''' <summary>Converts <see cref="System.Web.Configuration.AdapterDictionary"/> to <see cref="AdapterDictionaryTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.Configuration.AdapterDictionary"/> to be converted</param>
            ''' <returns>A <see cref="AdapterDictionaryTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.Configuration.AdapterDictionary) As AdapterDictionaryTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New AdapterDictionaryTypeSafeWrapper(a)
            End Operator
Public Sub Clear()
                Collection.Clear()
            End Sub
            ''' <summary>Removes item at specified index</summary>
            ''' <param name="Index">Index to remove item at</param>
            ''' <exception cref="ArgumentException">Index is not valid</exception>
            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(Index)
            End Sub
        End Class
#End Region
#Region "CodeNamespaceImportCollection (CodeNamespaceImport)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeNamespaceImportCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeNamespaceImport"/> to be wrapped</param>
        ''' <returns><see cref="CodeNamespaceImportCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeNamespaceImportCollection) As CodeNamespaceImportCollectionTypeSafeWrapper
            Return New CodeNamespaceImportCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeNamespaceImportCollection"/> as <see cref="IList(Of System.CodeDom.CodeNamespaceImport)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeNamespaceImportCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeNamespaceImportCollection, System.CodeDom.CodeNamespaceImport)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeNamespaceImportCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeNamespaceImportCollection"/> to <see cref="CodeNamespaceImportCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeNamespaceImportCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeNamespaceImportCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeNamespaceImportCollection) As CodeNamespaceImportCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeNamespaceImportCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeAttributeArgumentCollection (CodeAttributeArgument)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeAttributeArgumentCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeAttributeArgument"/> to be wrapped</param>
        ''' <returns><see cref="CodeAttributeArgumentCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeAttributeArgumentCollection) As CodeAttributeArgumentCollectionTypeSafeWrapper
            Return New CodeAttributeArgumentCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeAttributeArgumentCollection"/> as <see cref="IList(Of System.CodeDom.CodeAttributeArgument)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeAttributeArgumentCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeAttributeArgumentCollection, System.CodeDom.CodeAttributeArgument)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeAttributeArgumentCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeAttributeArgumentCollection"/> to <see cref="CodeAttributeArgumentCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeAttributeArgumentCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeAttributeArgumentCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeAttributeArgumentCollection) As CodeAttributeArgumentCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeAttributeArgumentCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeAttributeDeclarationCollection (CodeAttributeDeclaration)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeAttributeDeclarationCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeAttributeDeclaration"/> to be wrapped</param>
        ''' <returns><see cref="CodeAttributeDeclarationCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeAttributeDeclarationCollection) As CodeAttributeDeclarationCollectionTypeSafeWrapper
            Return New CodeAttributeDeclarationCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeAttributeDeclarationCollection"/> as <see cref="IList(Of System.CodeDom.CodeAttributeDeclaration)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeAttributeDeclarationCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeAttributeDeclarationCollection, System.CodeDom.CodeAttributeDeclaration)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeAttributeDeclarationCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeAttributeDeclarationCollection"/> to <see cref="CodeAttributeDeclarationCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeAttributeDeclarationCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeAttributeDeclarationCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeAttributeDeclarationCollection) As CodeAttributeDeclarationCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeAttributeDeclarationCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeCatchClauseCollection (CodeCatchClause)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeCatchClauseCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeCatchClause"/> to be wrapped</param>
        ''' <returns><see cref="CodeCatchClauseCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeCatchClauseCollection) As CodeCatchClauseCollectionTypeSafeWrapper
            Return New CodeCatchClauseCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeCatchClauseCollection"/> as <see cref="IList(Of System.CodeDom.CodeCatchClause)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeCatchClauseCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeCatchClauseCollection, System.CodeDom.CodeCatchClause)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeCatchClauseCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeCatchClauseCollection"/> to <see cref="CodeCatchClauseCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeCatchClauseCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeCatchClauseCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeCatchClauseCollection) As CodeCatchClauseCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeCatchClauseCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeCommentStatementCollection (CodeCommentStatement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeCommentStatementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeCommentStatement"/> to be wrapped</param>
        ''' <returns><see cref="CodeCommentStatementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeCommentStatementCollection) As CodeCommentStatementCollectionTypeSafeWrapper
            Return New CodeCommentStatementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeCommentStatementCollection"/> as <see cref="IList(Of System.CodeDom.CodeCommentStatement)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeCommentStatementCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeCommentStatementCollection, System.CodeDom.CodeCommentStatement)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeCommentStatementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeCommentStatementCollection"/> to <see cref="CodeCommentStatementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeCommentStatementCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeCommentStatementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeCommentStatementCollection) As CodeCommentStatementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeCommentStatementCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeDirectiveCollection (CodeDirective)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeDirectiveCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeDirective"/> to be wrapped</param>
        ''' <returns><see cref="CodeDirectiveCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeDirectiveCollection) As CodeDirectiveCollectionTypeSafeWrapper
            Return New CodeDirectiveCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeDirectiveCollection"/> as <see cref="IList(Of System.CodeDom.CodeDirective)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeDirectiveCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeDirectiveCollection, System.CodeDom.CodeDirective)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeDirectiveCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeDirectiveCollection"/> to <see cref="CodeDirectiveCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeDirectiveCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeDirectiveCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeDirectiveCollection) As CodeDirectiveCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeDirectiveCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeExpressionCollection (CodeExpression)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeExpressionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeExpression"/> to be wrapped</param>
        ''' <returns><see cref="CodeExpressionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeExpressionCollection) As CodeExpressionCollectionTypeSafeWrapper
            Return New CodeExpressionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeExpressionCollection"/> as <see cref="IList(Of System.CodeDom.CodeExpression)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeExpressionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeExpressionCollection, System.CodeDom.CodeExpression)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeExpressionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeExpressionCollection"/> to <see cref="CodeExpressionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeExpressionCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeExpressionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeExpressionCollection) As CodeExpressionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeExpressionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeNamespaceCollection (CodeNamespace)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeNamespaceCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeNamespace"/> to be wrapped</param>
        ''' <returns><see cref="CodeNamespaceCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeNamespaceCollection) As CodeNamespaceCollectionTypeSafeWrapper
            Return New CodeNamespaceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeNamespaceCollection"/> as <see cref="IList(Of System.CodeDom.CodeNamespace)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeNamespaceCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeNamespaceCollection, System.CodeDom.CodeNamespace)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeNamespaceCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeNamespaceCollection"/> to <see cref="CodeNamespaceCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeNamespaceCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeNamespaceCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeNamespaceCollection) As CodeNamespaceCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeNamespaceCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeParameterDeclarationExpressionCollection (CodeParameterDeclarationexpression)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeParameterDeclarationExpressionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeParameterDeclarationexpression"/> to be wrapped</param>
        ''' <returns><see cref="CodeParameterDeclarationExpressionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeParameterDeclarationExpressionCollection) As CodeParameterDeclarationExpressionCollectionTypeSafeWrapper
            Return New CodeParameterDeclarationExpressionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeParameterDeclarationExpressionCollection"/> as <see cref="IList(Of System.CodeDom.CodeParameterDeclarationexpression)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeParameterDeclarationExpressionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeParameterDeclarationExpressionCollection, System.CodeDom.CodeParameterDeclarationexpression)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeParameterDeclarationExpressionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeParameterDeclarationExpressionCollection"/> to <see cref="CodeParameterDeclarationExpressionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeParameterDeclarationExpressionCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeParameterDeclarationExpressionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeParameterDeclarationExpressionCollection) As CodeParameterDeclarationExpressionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeParameterDeclarationExpressionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeStatementCollection (CodeStatement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeStatementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeStatement"/> to be wrapped</param>
        ''' <returns><see cref="CodeStatementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeStatementCollection) As CodeStatementCollectionTypeSafeWrapper
            Return New CodeStatementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeStatementCollection"/> as <see cref="IList(Of System.CodeDom.CodeStatement)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeStatementCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeStatementCollection, System.CodeDom.CodeStatement)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeStatementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeStatementCollection"/> to <see cref="CodeStatementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeStatementCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeStatementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeStatementCollection) As CodeStatementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeStatementCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeTypeDeclarationCollection (CodeTypeDeclaration)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeTypeDeclarationCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeTypeDeclaration"/> to be wrapped</param>
        ''' <returns><see cref="CodeTypeDeclarationCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeTypeDeclarationCollection) As CodeTypeDeclarationCollectionTypeSafeWrapper
            Return New CodeTypeDeclarationCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeTypeDeclarationCollection"/> as <see cref="IList(Of System.CodeDom.CodeTypeDeclaration)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeTypeDeclarationCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeTypeDeclarationCollection, System.CodeDom.CodeTypeDeclaration)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeTypeDeclarationCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeTypeDeclarationCollection"/> to <see cref="CodeTypeDeclarationCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeTypeDeclarationCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeTypeDeclarationCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeTypeDeclarationCollection) As CodeTypeDeclarationCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeTypeDeclarationCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeTypeMemberCollection (CodeTypeMember)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeTypeMemberCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeTypeMember"/> to be wrapped</param>
        ''' <returns><see cref="CodeTypeMemberCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeTypeMemberCollection) As CodeTypeMemberCollectionTypeSafeWrapper
            Return New CodeTypeMemberCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeTypeMemberCollection"/> as <see cref="IList(Of System.CodeDom.CodeTypeMember)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeTypeMemberCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeTypeMemberCollection, System.CodeDom.CodeTypeMember)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeTypeMemberCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeTypeMemberCollection"/> to <see cref="CodeTypeMemberCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeTypeMemberCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeTypeMemberCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeTypeMemberCollection) As CodeTypeMemberCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeTypeMemberCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeTypeParameterCollection (CodeTypeParameter)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeTypeParameterCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeTypeParameter"/> to be wrapped</param>
        ''' <returns><see cref="CodeTypeParameterCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeTypeParameterCollection) As CodeTypeParameterCollectionTypeSafeWrapper
            Return New CodeTypeParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeTypeParameterCollection"/> as <see cref="IList(Of System.CodeDom.CodeTypeParameter)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeTypeParameterCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeTypeParameterCollection, System.CodeDom.CodeTypeParameter)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeTypeParameterCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeTypeParameterCollection"/> to <see cref="CodeTypeParameterCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeTypeParameterCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeTypeParameterCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeTypeParameterCollection) As CodeTypeParameterCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeTypeParameterCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CodeTypeReferenceCollection (CodeTypereference)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.CodeTypeReferenceCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.CodeTypereference"/> to be wrapped</param>
        ''' <returns><see cref="CodeTypeReferenceCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.CodeTypeReferenceCollection) As CodeTypeReferenceCollectionTypeSafeWrapper
            Return New CodeTypeReferenceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.CodeTypeReferenceCollection"/> as <see cref="IList(Of System.CodeDom.CodeTypereference)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CodeTypeReferenceCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.CodeTypeReferenceCollection, System.CodeDom.CodeTypereference)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.CodeTypeReferenceCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.CodeTypeReferenceCollection"/> to <see cref="CodeTypeReferenceCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.CodeTypeReferenceCollection"/> to be converted</param>
            ''' <returns>A <see cref="CodeTypeReferenceCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.CodeTypeReferenceCollection) As CodeTypeReferenceCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CodeTypeReferenceCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CompilerErrorCollection (CompilerError)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.CodeDom.Compiler.CompilerErrorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.CodeDom.Compiler.CompilerError"/> to be wrapped</param>
        ''' <returns><see cref="CompilerErrorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.CodeDom.Compiler.CompilerErrorCollection) As CompilerErrorCollectionTypeSafeWrapper
            Return New CompilerErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.CodeDom.Compiler.CompilerErrorCollection"/> as <see cref="IList(Of System.CodeDom.Compiler.CompilerError)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CompilerErrorCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.CodeDom.Compiler.CompilerErrorCollection, System.CodeDom.Compiler.CompilerError)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.CodeDom.Compiler.CompilerErrorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.CodeDom.Compiler.CompilerErrorCollection"/> to <see cref="CompilerErrorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.CodeDom.Compiler.CompilerErrorCollection"/> to be converted</param>
            ''' <returns>A <see cref="CompilerErrorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.CodeDom.Compiler.CompilerErrorCollection) As CompilerErrorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CompilerErrorCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DesignerVerbCollection (DesignerVerb)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.ComponentModel.Design.DesignerVerbCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.ComponentModel.Design.DesignerVerb"/> to be wrapped</param>
        ''' <returns><see cref="DesignerVerbCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.ComponentModel.Design.DesignerVerbCollection) As DesignerVerbCollectionTypeSafeWrapper
            Return New DesignerVerbCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.ComponentModel.Design.DesignerVerbCollection"/> as <see cref="IList(Of System.ComponentModel.Design.DesignerVerb)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DesignerVerbCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.ComponentModel.Design.DesignerVerbCollection, System.ComponentModel.Design.DesignerVerb)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.ComponentModel.Design.DesignerVerbCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.ComponentModel.Design.DesignerVerbCollection"/> to <see cref="DesignerVerbCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.ComponentModel.Design.DesignerVerbCollection"/> to be converted</param>
            ''' <returns>A <see cref="DesignerVerbCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.ComponentModel.Design.DesignerVerbCollection) As DesignerVerbCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DesignerVerbCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SqlBulkCopyColumnMappingCollection (SqlBulkCopyColumnMapping)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.SqlClient.SqlBulkCopyColumnMappingCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.SqlClient.SqlBulkCopyColumnMapping"/> to be wrapped</param>
        ''' <returns><see cref="SqlBulkCopyColumnMappingCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.SqlClient.SqlBulkCopyColumnMappingCollection) As SqlBulkCopyColumnMappingCollectionTypeSafeWrapper
            Return New SqlBulkCopyColumnMappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.SqlClient.SqlBulkCopyColumnMappingCollection"/> as <see cref="IList(Of System.Data.SqlClient.SqlBulkCopyColumnMapping)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SqlBulkCopyColumnMappingCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Data.SqlClient.SqlBulkCopyColumnMappingCollection, System.Data.SqlClient.SqlBulkCopyColumnMapping)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.SqlClient.SqlBulkCopyColumnMappingCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Data.SqlClient.SqlBulkCopyColumnMappingCollection"/> to <see cref="SqlBulkCopyColumnMappingCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.SqlClient.SqlBulkCopyColumnMappingCollection"/> to be converted</param>
            ''' <returns>A <see cref="SqlBulkCopyColumnMappingCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.SqlClient.SqlBulkCopyColumnMappingCollection) As SqlBulkCopyColumnMappingCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SqlBulkCopyColumnMappingCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CounterCreationDataCollection (CounterCreationdata)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Diagnostics.CounterCreationDataCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Diagnostics.CounterCreationdata"/> to be wrapped</param>
        ''' <returns><see cref="CounterCreationDataCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Diagnostics.CounterCreationDataCollection) As CounterCreationDataCollectionTypeSafeWrapper
            Return New CounterCreationDataCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Diagnostics.CounterCreationDataCollection"/> as <see cref="IList(Of System.Diagnostics.CounterCreationdata)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CounterCreationDataCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Diagnostics.CounterCreationDataCollection, System.Diagnostics.CounterCreationdata)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Diagnostics.CounterCreationDataCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Diagnostics.CounterCreationDataCollection"/> to <see cref="CounterCreationDataCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Diagnostics.CounterCreationDataCollection"/> to be converted</param>
            ''' <returns>A <see cref="CounterCreationDataCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Diagnostics.CounterCreationDataCollection) As CounterCreationDataCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CounterCreationDataCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "EventLogPermissionEntryCollection (EventLogPermissionEntry)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Diagnostics.EventLogPermissionEntryCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Diagnostics.EventLogPermissionEntry"/> to be wrapped</param>
        ''' <returns><see cref="EventLogPermissionEntryCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Diagnostics.EventLogPermissionEntryCollection) As EventLogPermissionEntryCollectionTypeSafeWrapper
            Return New EventLogPermissionEntryCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Diagnostics.EventLogPermissionEntryCollection"/> as <see cref="IList(Of System.Diagnostics.EventLogPermissionEntry)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class EventLogPermissionEntryCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Diagnostics.EventLogPermissionEntryCollection, System.Diagnostics.EventLogPermissionEntry)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Diagnostics.EventLogPermissionEntryCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Diagnostics.EventLogPermissionEntryCollection"/> to <see cref="EventLogPermissionEntryCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Diagnostics.EventLogPermissionEntryCollection"/> to be converted</param>
            ''' <returns>A <see cref="EventLogPermissionEntryCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Diagnostics.EventLogPermissionEntryCollection) As EventLogPermissionEntryCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New EventLogPermissionEntryCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "PerformanceCounterPermissionEntryCollection (PerformanceCounterPermissionEntry)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Diagnostics.PerformanceCounterPermissionEntryCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Diagnostics.PerformanceCounterPermissionEntry"/> to be wrapped</param>
        ''' <returns><see cref="PerformanceCounterPermissionEntryCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Diagnostics.PerformanceCounterPermissionEntryCollection) As PerformanceCounterPermissionEntryCollectionTypeSafeWrapper
            Return New PerformanceCounterPermissionEntryCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Diagnostics.PerformanceCounterPermissionEntryCollection"/> as <see cref="IList(Of System.Diagnostics.PerformanceCounterPermissionEntry)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class PerformanceCounterPermissionEntryCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Diagnostics.PerformanceCounterPermissionEntryCollection, System.Diagnostics.PerformanceCounterPermissionEntry)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Diagnostics.PerformanceCounterPermissionEntryCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Diagnostics.PerformanceCounterPermissionEntryCollection"/> to <see cref="PerformanceCounterPermissionEntryCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Diagnostics.PerformanceCounterPermissionEntryCollection"/> to be converted</param>
            ''' <returns>A <see cref="PerformanceCounterPermissionEntryCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Diagnostics.PerformanceCounterPermissionEntryCollection) As PerformanceCounterPermissionEntryCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New PerformanceCounterPermissionEntryCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "X509CertificateCollection (X509Certificate)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.Cryptography.X509Certificates.X509CertificateCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.Cryptography.X509Certificates.X509Certificate"/> to be wrapped</param>
        ''' <returns><see cref="X509CertificateCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.Cryptography.X509Certificates.X509CertificateCollection) As X509CertificateCollectionTypeSafeWrapper
            Return New X509CertificateCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.Cryptography.X509Certificates.X509CertificateCollection"/> as <see cref="IList(Of System.Security.Cryptography.X509Certificates.X509Certificate)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class X509CertificateCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Security.Cryptography.X509Certificates.X509CertificateCollection, System.Security.Cryptography.X509Certificates.X509Certificate)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.Cryptography.X509Certificates.X509CertificateCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Security.Cryptography.X509Certificates.X509CertificateCollection"/> to <see cref="X509CertificateCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.Cryptography.X509Certificates.X509CertificateCollection"/> to be converted</param>
            ''' <returns>A <see cref="X509CertificateCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.Cryptography.X509Certificates.X509CertificateCollection) As X509CertificateCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New X509CertificateCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "X509Certificate2Collection (X509Certificate2)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/></summary>
        ''' <param name="Collection">A <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2"/> to be wrapped</param>
        ''' <returns><see cref="X509Certificate2CollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Security.Cryptography.X509Certificates.X509Certificate2Collection) As X509Certificate2CollectionTypeSafeWrapper
            Return New X509Certificate2CollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> as <see cref="IList(Of System.Security.Cryptography.X509Certificates.X509Certificate2)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class X509Certificate2CollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Security.Cryptography.X509Certificates.X509Certificate2Collection, System.Security.Cryptography.X509Certificates.X509Certificate2)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Security.Cryptography.X509Certificates.X509Certificate2Collection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> to <see cref="X509Certificate2CollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/> to be converted</param>
            ''' <returns>A <see cref="X509Certificate2CollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Security.Cryptography.X509Certificates.X509Certificate2Collection) As X509Certificate2CollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New X509Certificate2CollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ParserErrorCollection (ParserError)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.ParserErrorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.ParserError"/> to be wrapped</param>
        ''' <returns><see cref="ParserErrorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.ParserErrorCollection) As ParserErrorCollectionTypeSafeWrapper
            Return New ParserErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.ParserErrorCollection"/> as <see cref="IList(Of System.Web.ParserError)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ParserErrorCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.ParserErrorCollection, System.Web.ParserError)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.ParserErrorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.ParserErrorCollection"/> to <see cref="ParserErrorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.ParserErrorCollection"/> to be converted</param>
            ''' <returns>A <see cref="ParserErrorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.ParserErrorCollection) As ParserErrorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ParserErrorCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "EmbeddedMailObjectsCollection (EmbeddedMailObject)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.EmbeddedMailObjectsCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.EmbeddedMailObject"/> to be wrapped</param>
        ''' <returns><see cref="EmbeddedMailObjectsCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.EmbeddedMailObjectsCollection) As EmbeddedMailObjectsCollectionTypeSafeWrapper
            Return New EmbeddedMailObjectsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.EmbeddedMailObjectsCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.EmbeddedMailObject)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class EmbeddedMailObjectsCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.EmbeddedMailObjectsCollection, System.Web.UI.WebControls.EmbeddedMailObject)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.EmbeddedMailObjectsCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.EmbeddedMailObjectsCollection"/> to <see cref="EmbeddedMailObjectsCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.EmbeddedMailObjectsCollection"/> to be converted</param>
            ''' <returns>A <see cref="EmbeddedMailObjectsCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.EmbeddedMailObjectsCollection) As EmbeddedMailObjectsCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New EmbeddedMailObjectsCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "RoleGroupCollection (RoleGroup)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.RoleGroupCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.RoleGroup"/> to be wrapped</param>
        ''' <returns><see cref="RoleGroupCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.RoleGroupCollection) As RoleGroupCollectionTypeSafeWrapper
            Return New RoleGroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.RoleGroupCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.RoleGroup)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class RoleGroupCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.RoleGroupCollection, System.Web.UI.WebControls.RoleGroup)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.RoleGroupCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.RoleGroupCollection"/> to <see cref="RoleGroupCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.RoleGroupCollection"/> to be converted</param>
            ''' <returns>A <see cref="RoleGroupCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.RoleGroupCollection) As RoleGroupCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New RoleGroupCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ProxyWebPartConnectionCollection (WebPartConnection)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.WebPartConnection"/> to be wrapped</param>
        ''' <returns><see cref="ProxyWebPartConnectionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection) As ProxyWebPartConnectionCollectionTypeSafeWrapper
            Return New ProxyWebPartConnectionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.WebParts.WebPartConnection)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ProxyWebPartConnectionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection, System.Web.UI.WebControls.WebParts.WebPartConnection)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection"/> to <see cref="ProxyWebPartConnectionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection"/> to be converted</param>
            ''' <returns>A <see cref="ProxyWebPartConnectionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection) As ProxyWebPartConnectionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ProxyWebPartConnectionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "WebPartConnectionCollection (WebPartConnection)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.WebPartConnectionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.WebPartConnection"/> to be wrapped</param>
        ''' <returns><see cref="WebPartConnectionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartConnectionCollection) As WebPartConnectionCollectionTypeSafeWrapper
            Return New WebPartConnectionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.WebPartConnectionCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.WebParts.WebPartConnection)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebPartConnectionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.WebParts.WebPartConnectionCollection, System.Web.UI.WebControls.WebParts.WebPartConnection)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartConnectionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.WebPartConnectionCollection"/> to <see cref="WebPartConnectionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.WebPartConnectionCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebPartConnectionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.WebPartConnectionCollection) As WebPartConnectionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebPartConnectionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "WebPartDisplayModeCollection (WebPartDisplayMode)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.WebPartDisplayMode"/> to be wrapped</param>
        ''' <returns><see cref="WebPartDisplayModeCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection) As WebPartDisplayModeCollectionTypeSafeWrapper
            Return New WebPartDisplayModeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.WebParts.WebPartDisplayMode)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebPartDisplayModeCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection, System.Web.UI.WebControls.WebParts.WebPartDisplayMode)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection"/> to <see cref="WebPartDisplayModeCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebPartDisplayModeCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection) As WebPartDisplayModeCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebPartDisplayModeCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "WebPartTransformerCollection (WebPartTransformer)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WebParts.WebPartTransformerCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WebParts.WebPartTransformer"/> to be wrapped</param>
        ''' <returns><see cref="WebPartTransformerCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartTransformerCollection) As WebPartTransformerCollectionTypeSafeWrapper
            Return New WebPartTransformerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WebParts.WebPartTransformerCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.WebParts.WebPartTransformer)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebPartTransformerCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.WebParts.WebPartTransformerCollection, System.Web.UI.WebControls.WebParts.WebPartTransformer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartTransformerCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WebParts.WebPartTransformerCollection"/> to <see cref="WebPartTransformerCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WebParts.WebPartTransformerCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebPartTransformerCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WebParts.WebPartTransformerCollection) As WebPartTransformerCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebPartTransformerCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "LinkTargetCollection (LinkTarget)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Documents.LinkTargetCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Documents.LinkTarget"/> to be wrapped</param>
        ''' <returns><see cref="LinkTargetCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Documents.LinkTargetCollection) As LinkTargetCollectionTypeSafeWrapper
            Return New LinkTargetCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Documents.LinkTargetCollection"/> as <see cref="IList(Of System.Windows.Documents.LinkTarget)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class LinkTargetCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Documents.LinkTargetCollection, System.Windows.Documents.LinkTarget)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Documents.LinkTargetCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Documents.LinkTargetCollection"/> to <see cref="LinkTargetCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Documents.LinkTargetCollection"/> to be converted</param>
            ''' <returns>A <see cref="LinkTargetCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Documents.LinkTargetCollection) As LinkTargetCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New LinkTargetCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SchemaImporterExtensionCollection (SchemaImporterExtension)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Xml.Serialization.Advanced.SchemaImporterExtension"/> to be wrapped</param>
        ''' <returns><see cref="SchemaImporterExtensionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection) As SchemaImporterExtensionCollectionTypeSafeWrapper
            Return New SchemaImporterExtensionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection"/> as <see cref="IList(Of System.Xml.Serialization.Advanced.SchemaImporterExtension)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SchemaImporterExtensionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection, System.Xml.Serialization.Advanced.SchemaImporterExtension)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection"/> to <see cref="SchemaImporterExtensionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection"/> to be converted</param>
            ''' <returns>A <see cref="SchemaImporterExtensionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection) As SchemaImporterExtensionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SchemaImporterExtensionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "XmlAnyElementAttributes (XmlAnyElementAttribute)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Xml.Serialization.XmlAnyElementAttributes"/></summary>
        ''' <param name="Collection">A <see cref="System.Xml.Serialization.XmlAnyElementAttribute"/> to be wrapped</param>
        ''' <returns><see cref="XmlAnyElementAttributesTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Xml.Serialization.XmlAnyElementAttributes) As XmlAnyElementAttributesTypeSafeWrapper
            Return New XmlAnyElementAttributesTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Xml.Serialization.XmlAnyElementAttributes"/> as <see cref="IList(Of System.Xml.Serialization.XmlAnyElementAttribute)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class XmlAnyElementAttributesTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Xml.Serialization.XmlAnyElementAttributes, System.Xml.Serialization.XmlAnyElementAttribute)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Xml.Serialization.XmlAnyElementAttributes)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Xml.Serialization.XmlAnyElementAttributes"/> to <see cref="XmlAnyElementAttributesTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Xml.Serialization.XmlAnyElementAttributes"/> to be converted</param>
            ''' <returns>A <see cref="XmlAnyElementAttributesTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Xml.Serialization.XmlAnyElementAttributes) As XmlAnyElementAttributesTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New XmlAnyElementAttributesTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "XmlArrayItemAttributes (XmlArrayItemAttribute)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Xml.Serialization.XmlArrayItemAttributes"/></summary>
        ''' <param name="Collection">A <see cref="System.Xml.Serialization.XmlArrayItemAttribute"/> to be wrapped</param>
        ''' <returns><see cref="XmlArrayItemAttributesTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Xml.Serialization.XmlArrayItemAttributes) As XmlArrayItemAttributesTypeSafeWrapper
            Return New XmlArrayItemAttributesTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Xml.Serialization.XmlArrayItemAttributes"/> as <see cref="IList(Of System.Xml.Serialization.XmlArrayItemAttribute)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class XmlArrayItemAttributesTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Xml.Serialization.XmlArrayItemAttributes, System.Xml.Serialization.XmlArrayItemAttribute)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Xml.Serialization.XmlArrayItemAttributes)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Xml.Serialization.XmlArrayItemAttributes"/> to <see cref="XmlArrayItemAttributesTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Xml.Serialization.XmlArrayItemAttributes"/> to be converted</param>
            ''' <returns>A <see cref="XmlArrayItemAttributesTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Xml.Serialization.XmlArrayItemAttributes) As XmlArrayItemAttributesTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New XmlArrayItemAttributesTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "XmlElementAttributes (XmlElementAttribute)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Xml.Serialization.XmlElementAttributes"/></summary>
        ''' <param name="Collection">A <see cref="System.Xml.Serialization.XmlElementAttribute"/> to be wrapped</param>
        ''' <returns><see cref="XmlElementAttributesTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Xml.Serialization.XmlElementAttributes) As XmlElementAttributesTypeSafeWrapper
            Return New XmlElementAttributesTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Xml.Serialization.XmlElementAttributes"/> as <see cref="IList(Of System.Xml.Serialization.XmlElementAttribute)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class XmlElementAttributesTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Xml.Serialization.XmlElementAttributes, System.Xml.Serialization.XmlElementAttribute)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Xml.Serialization.XmlElementAttributes)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Xml.Serialization.XmlElementAttributes"/> to <see cref="XmlElementAttributesTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Xml.Serialization.XmlElementAttributes"/> to be converted</param>
            ''' <returns>A <see cref="XmlElementAttributesTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Xml.Serialization.XmlElementAttributes) As XmlElementAttributesTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New XmlElementAttributesTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "XmlSchemas (XmlSchema)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Xml.Serialization.XmlSchemas"/></summary>
        ''' <param name="Collection">A <see cref="System.Xml.Schema.XmlSchema"/> to be wrapped</param>
        ''' <returns><see cref="XmlSchemasTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Xml.Serialization.XmlSchemas) As XmlSchemasTypeSafeWrapper
            Return New XmlSchemasTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Xml.Serialization.XmlSchemas"/> as <see cref="IList(Of System.Xml.Schema.XmlSchema)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class XmlSchemasTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Xml.Serialization.XmlSchemas, System.Xml.Schema.XmlSchema)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Xml.Serialization.XmlSchemas)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Xml.Serialization.XmlSchemas"/> to <see cref="XmlSchemasTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Xml.Serialization.XmlSchemas"/> to be converted</param>
            ''' <returns>A <see cref="XmlSchemasTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Xml.Serialization.XmlSchemas) As XmlSchemasTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New XmlSchemasTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "XmlSchemaObjectCollection (XmlSchemaObject)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Xml.Schema.XmlSchemaObjectCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Xml.Schema.XmlSchemaObject"/> to be wrapped</param>
        ''' <returns><see cref="XmlSchemaObjectCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Xml.Schema.XmlSchemaObjectCollection) As XmlSchemaObjectCollectionTypeSafeWrapper
            Return New XmlSchemaObjectCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Xml.Schema.XmlSchemaObjectCollection"/> as <see cref="IList(Of System.Xml.Schema.XmlSchemaObject)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class XmlSchemaObjectCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Xml.Schema.XmlSchemaObjectCollection, System.Xml.Schema.XmlSchemaObject)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Xml.Schema.XmlSchemaObjectCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Xml.Schema.XmlSchemaObjectCollection"/> to <see cref="XmlSchemaObjectCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Xml.Schema.XmlSchemaObjectCollection"/> to be converted</param>
            ''' <returns>A <see cref="XmlSchemaObjectCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Xml.Schema.XmlSchemaObjectCollection) As XmlSchemaObjectCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New XmlSchemaObjectCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "StringCollection (String)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Collections.Specialized.StringCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.String"/> to be wrapped</param>
        ''' <returns><see cref="StringCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Collections.Specialized.StringCollection) As StringCollectionTypeSafeWrapper
            Return New StringCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Collections.Specialized.StringCollection"/> as <see cref="IList(Of System.String)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class StringCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Collections.Specialized.StringCollection, System.String)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Collections.Specialized.StringCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Collections.Specialized.StringCollection"/> to <see cref="StringCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Collections.Specialized.StringCollection"/> to be converted</param>
            ''' <returns>A <see cref="StringCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Collections.Specialized.StringCollection) As StringCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New StringCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DesignerOptionCollection (DesignerOptionCollection)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection"/> to be wrapped</param>
        ''' <returns><see cref="DesignerOptionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection) As DesignerOptionCollectionTypeSafeWrapper
            Return New DesignerOptionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection"/> as <see cref="IList(Of System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DesignerOptionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection, System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection"/> to <see cref="DesignerOptionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection"/> to be converted</param>
            ''' <returns>A <see cref="DesignerOptionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection) As DesignerOptionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DesignerOptionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "EventDescriptorCollection (EventDescriptor)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.ComponentModel.EventDescriptorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.ComponentModel.EventDescriptor"/> to be wrapped</param>
        ''' <returns><see cref="EventDescriptorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.ComponentModel.EventDescriptorCollection) As EventDescriptorCollectionTypeSafeWrapper
            Return New EventDescriptorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.ComponentModel.EventDescriptorCollection"/> as <see cref="IList(Of System.ComponentModel.EventDescriptor)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class EventDescriptorCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.ComponentModel.EventDescriptorCollection, System.ComponentModel.EventDescriptor)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.ComponentModel.EventDescriptorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.ComponentModel.EventDescriptorCollection"/> to <see cref="EventDescriptorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.ComponentModel.EventDescriptorCollection"/> to be converted</param>
            ''' <returns>A <see cref="EventDescriptorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.ComponentModel.EventDescriptorCollection) As EventDescriptorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New EventDescriptorCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ListSortDescriptionCollection (ListSortDescription)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.ComponentModel.ListSortDescriptionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.ComponentModel.ListSortDescription"/> to be wrapped</param>
        ''' <returns><see cref="ListSortDescriptionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.ComponentModel.ListSortDescriptionCollection) As ListSortDescriptionCollectionTypeSafeWrapper
            Return New ListSortDescriptionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.ComponentModel.ListSortDescriptionCollection"/> as <see cref="IList(Of System.ComponentModel.ListSortDescription)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ListSortDescriptionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.ComponentModel.ListSortDescriptionCollection, System.ComponentModel.ListSortDescription)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.ComponentModel.ListSortDescriptionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.ComponentModel.ListSortDescriptionCollection"/> to <see cref="ListSortDescriptionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.ComponentModel.ListSortDescriptionCollection"/> to be converted</param>
            ''' <returns>A <see cref="ListSortDescriptionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.ComponentModel.ListSortDescriptionCollection) As ListSortDescriptionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ListSortDescriptionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "PropertyDescriptorCollection (PropertyDescriptor)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.ComponentModel.PropertyDescriptorCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.ComponentModel.PropertyDescriptor"/> to be wrapped</param>
        ''' <returns><see cref="PropertyDescriptorCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.ComponentModel.PropertyDescriptorCollection) As PropertyDescriptorCollectionTypeSafeWrapper
            Return New PropertyDescriptorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.ComponentModel.PropertyDescriptorCollection"/> as <see cref="IList(Of System.ComponentModel.PropertyDescriptor)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class PropertyDescriptorCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.ComponentModel.PropertyDescriptorCollection, System.ComponentModel.PropertyDescriptor)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.ComponentModel.PropertyDescriptorCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.ComponentModel.PropertyDescriptorCollection"/> to <see cref="PropertyDescriptorCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.ComponentModel.PropertyDescriptorCollection"/> to be converted</param>
            ''' <returns>A <see cref="PropertyDescriptorCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.ComponentModel.PropertyDescriptorCollection) As PropertyDescriptorCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New PropertyDescriptorCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataColumnMappingCollection (DataColumnMapping)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.Common.DataColumnMappingCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.Common.DataColumnMapping"/> to be wrapped</param>
        ''' <returns><see cref="DataColumnMappingCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.Common.DataColumnMappingCollection) As DataColumnMappingCollectionTypeSafeWrapper
            Return New DataColumnMappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.Common.DataColumnMappingCollection"/> as <see cref="IList(Of System.Data.Common.DataColumnMapping)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataColumnMappingCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Data.Common.DataColumnMappingCollection, System.Data.Common.DataColumnMapping)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.Common.DataColumnMappingCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Data.Common.DataColumnMappingCollection"/> to <see cref="DataColumnMappingCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.Common.DataColumnMappingCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataColumnMappingCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.Common.DataColumnMappingCollection) As DataColumnMappingCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataColumnMappingCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DbParameterCollection (DbParameter)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.Common.DbParameterCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.Common.DbParameter"/> to be wrapped</param>
        ''' <returns><see cref="DbParameterCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.Common.DbParameterCollection) As DbParameterCollectionTypeSafeWrapper
            Return New DbParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.Common.DbParameterCollection"/> as <see cref="IList(Of System.Data.Common.DbParameter)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DbParameterCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Data.Common.DbParameterCollection, System.Data.Common.DbParameter)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.Common.DbParameterCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Data.Common.DbParameterCollection"/> to <see cref="DbParameterCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.Common.DbParameterCollection"/> to be converted</param>
            ''' <returns>A <see cref="DbParameterCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.Common.DbParameterCollection) As DbParameterCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DbParameterCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "OdbcParameterCollection (OdbcParameter)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.Odbc.OdbcParameterCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.Odbc.OdbcParameter"/> to be wrapped</param>
        ''' <returns><see cref="OdbcParameterCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.Odbc.OdbcParameterCollection) As OdbcParameterCollectionTypeSafeWrapper
            Return New OdbcParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.Odbc.OdbcParameterCollection"/> as <see cref="IList(Of System.Data.Odbc.OdbcParameter)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class OdbcParameterCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Data.Odbc.OdbcParameterCollection, System.Data.Odbc.OdbcParameter)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.Odbc.OdbcParameterCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Data.Odbc.OdbcParameterCollection"/> to <see cref="OdbcParameterCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.Odbc.OdbcParameterCollection"/> to be converted</param>
            ''' <returns>A <see cref="OdbcParameterCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.Odbc.OdbcParameterCollection) As OdbcParameterCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New OdbcParameterCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "OleDbParameterCollection (OleDbParameter)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.OleDb.OleDbParameterCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.OleDb.OleDbParameter"/> to be wrapped</param>
        ''' <returns><see cref="OleDbParameterCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.OleDb.OleDbParameterCollection) As OleDbParameterCollectionTypeSafeWrapper
            Return New OleDbParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.OleDb.OleDbParameterCollection"/> as <see cref="IList(Of System.Data.OleDb.OleDbParameter)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class OleDbParameterCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Data.OleDb.OleDbParameterCollection, System.Data.OleDb.OleDbParameter)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.OleDb.OleDbParameterCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Data.OleDb.OleDbParameterCollection"/> to <see cref="OleDbParameterCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.OleDb.OleDbParameterCollection"/> to be converted</param>
            ''' <returns>A <see cref="OleDbParameterCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.OleDb.OleDbParameterCollection) As OleDbParameterCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New OleDbParameterCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SqlParameterCollection (SqlParameter)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.SqlClient.SqlParameterCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.SqlClient.SqlParameter"/> to be wrapped</param>
        ''' <returns><see cref="SqlParameterCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.SqlClient.SqlParameterCollection) As SqlParameterCollectionTypeSafeWrapper
            Return New SqlParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.SqlClient.SqlParameterCollection"/> as <see cref="IList(Of System.Data.SqlClient.SqlParameter)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SqlParameterCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Data.SqlClient.SqlParameterCollection, System.Data.SqlClient.SqlParameter)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.SqlClient.SqlParameterCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Data.SqlClient.SqlParameterCollection"/> to <see cref="SqlParameterCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.SqlClient.SqlParameterCollection"/> to be converted</param>
            ''' <returns>A <see cref="SqlParameterCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.SqlClient.SqlParameterCollection) As SqlParameterCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SqlParameterCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataTableMappingCollection (DataTableMapping)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Data.Common.DataTableMappingCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Data.Common.DataTableMapping"/> to be wrapped</param>
        ''' <returns><see cref="DataTableMappingCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Data.Common.DataTableMappingCollection) As DataTableMappingCollectionTypeSafeWrapper
            Return New DataTableMappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Data.Common.DataTableMappingCollection"/> as <see cref="IList(Of System.Data.Common.DataTableMapping)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataTableMappingCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Data.Common.DataTableMappingCollection, System.Data.Common.DataTableMapping)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Data.Common.DataTableMappingCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Data.Common.DataTableMappingCollection"/> to <see cref="DataTableMappingCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Data.Common.DataTableMappingCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataTableMappingCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Data.Common.DataTableMappingCollection) As DataTableMappingCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataTableMappingCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TraceListenerCollection (TraceListener)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Diagnostics.TraceListenerCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Diagnostics.TraceListener"/> to be wrapped</param>
        ''' <returns><see cref="TraceListenerCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Diagnostics.TraceListenerCollection) As TraceListenerCollectionTypeSafeWrapper
            Return New TraceListenerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Diagnostics.TraceListenerCollection"/> as <see cref="IList(Of System.Diagnostics.TraceListener)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TraceListenerCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Diagnostics.TraceListenerCollection, System.Diagnostics.TraceListener)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Diagnostics.TraceListenerCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Diagnostics.TraceListenerCollection"/> to <see cref="TraceListenerCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Diagnostics.TraceListenerCollection"/> to be converted</param>
            ''' <returns>A <see cref="TraceListenerCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Diagnostics.TraceListenerCollection) As TraceListenerCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TraceListenerCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SiteMapNodeCollection (SiteMapNode)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.SiteMapNodeCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.SiteMapNode"/> to be wrapped</param>
        ''' <returns><see cref="SiteMapNodeCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.SiteMapNodeCollection) As SiteMapNodeCollectionTypeSafeWrapper
            Return New SiteMapNodeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.SiteMapNodeCollection"/> as <see cref="IList(Of System.Web.SiteMapNode)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SiteMapNodeCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.SiteMapNodeCollection, System.Web.SiteMapNode)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.SiteMapNodeCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.SiteMapNodeCollection"/> to <see cref="SiteMapNodeCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.SiteMapNodeCollection"/> to be converted</param>
            ''' <returns>A <see cref="SiteMapNodeCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.SiteMapNodeCollection) As SiteMapNodeCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SiteMapNodeCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataControlFieldCollection (DataControlField)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.DataControlFieldCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.DataControlField"/> to be wrapped</param>
        ''' <returns><see cref="DataControlFieldCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.DataControlFieldCollection) As DataControlFieldCollectionTypeSafeWrapper
            Return New DataControlFieldCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.DataControlFieldCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.DataControlField)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataControlFieldCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.DataControlFieldCollection, System.Web.UI.WebControls.DataControlField)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.DataControlFieldCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.DataControlFieldCollection"/> to <see cref="DataControlFieldCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.DataControlFieldCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataControlFieldCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.DataControlFieldCollection) As DataControlFieldCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataControlFieldCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "HotSpotCollection (HotSpot)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.HotSpotCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.HotSpot"/> to be wrapped</param>
        ''' <returns><see cref="HotSpotCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.HotSpotCollection) As HotSpotCollectionTypeSafeWrapper
            Return New HotSpotCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.HotSpotCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.HotSpot)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class HotSpotCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.HotSpotCollection, System.Web.UI.WebControls.HotSpot)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.HotSpotCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.HotSpotCollection"/> to <see cref="HotSpotCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.HotSpotCollection"/> to be converted</param>
            ''' <returns>A <see cref="HotSpotCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.HotSpotCollection) As HotSpotCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New HotSpotCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "MenuItemBindingCollection (MenuItemBinding)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.MenuItemBindingCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.MenuItemBinding"/> to be wrapped</param>
        ''' <returns><see cref="MenuItemBindingCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.MenuItemBindingCollection) As MenuItemBindingCollectionTypeSafeWrapper
            Return New MenuItemBindingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.MenuItemBindingCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.MenuItemBinding)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class MenuItemBindingCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.MenuItemBindingCollection, System.Web.UI.WebControls.MenuItemBinding)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.MenuItemBindingCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.MenuItemBindingCollection"/> to <see cref="MenuItemBindingCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.MenuItemBindingCollection"/> to be converted</param>
            ''' <returns>A <see cref="MenuItemBindingCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.MenuItemBindingCollection) As MenuItemBindingCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New MenuItemBindingCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "MenuItemStyleCollection (MenuItemStyle)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.MenuItemStyleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.MenuItemStyle"/> to be wrapped</param>
        ''' <returns><see cref="MenuItemStyleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.MenuItemStyleCollection) As MenuItemStyleCollectionTypeSafeWrapper
            Return New MenuItemStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.MenuItemStyleCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.MenuItemStyle)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class MenuItemStyleCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.MenuItemStyleCollection, System.Web.UI.WebControls.MenuItemStyle)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.MenuItemStyleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.MenuItemStyleCollection"/> to <see cref="MenuItemStyleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.MenuItemStyleCollection"/> to be converted</param>
            ''' <returns>A <see cref="MenuItemStyleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.MenuItemStyleCollection) As MenuItemStyleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New MenuItemStyleCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ParameterCollection (Parameter)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.ParameterCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.Parameter"/> to be wrapped</param>
        ''' <returns><see cref="WebParameterCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.ParameterCollection) As WebParameterCollectionTypeSafeWrapper
            Return New WebParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.ParameterCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.Parameter)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebParameterCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.ParameterCollection, System.Web.UI.WebControls.Parameter)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.ParameterCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.ParameterCollection"/> to <see cref="WebParameterCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.ParameterCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebParameterCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.ParameterCollection) As WebParameterCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebParameterCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "StyleCollection (Style)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.StyleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.Style"/> to be wrapped</param>
        ''' <returns><see cref="WebStyleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.StyleCollection) As WebStyleCollectionTypeSafeWrapper
            Return New WebStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.StyleCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.Style)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebStyleCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.StyleCollection, System.Web.UI.WebControls.Style)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.StyleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.StyleCollection"/> to <see cref="WebStyleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.StyleCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebStyleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.StyleCollection) As WebStyleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebStyleCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SubMenuStyleCollection (SubMenuStyle)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.SubMenuStyleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.SubMenuStyle"/> to be wrapped</param>
        ''' <returns><see cref="SubMenuStyleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.SubMenuStyleCollection) As SubMenuStyleCollectionTypeSafeWrapper
            Return New SubMenuStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.SubMenuStyleCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.SubMenuStyle)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SubMenuStyleCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.SubMenuStyleCollection, System.Web.UI.WebControls.SubMenuStyle)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.SubMenuStyleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.SubMenuStyleCollection"/> to <see cref="SubMenuStyleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.SubMenuStyleCollection"/> to be converted</param>
            ''' <returns>A <see cref="SubMenuStyleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.SubMenuStyleCollection) As SubMenuStyleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SubMenuStyleCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TreeNodeBindingCollection (TreeNodeBinding)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.TreeNodeBindingCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.TreeNodeBinding"/> to be wrapped</param>
        ''' <returns><see cref="TreeNodeBindingCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.TreeNodeBindingCollection) As TreeNodeBindingCollectionTypeSafeWrapper
            Return New TreeNodeBindingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.TreeNodeBindingCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.TreeNodeBinding)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TreeNodeBindingCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.TreeNodeBindingCollection, System.Web.UI.WebControls.TreeNodeBinding)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.TreeNodeBindingCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.TreeNodeBindingCollection"/> to <see cref="TreeNodeBindingCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.TreeNodeBindingCollection"/> to be converted</param>
            ''' <returns>A <see cref="TreeNodeBindingCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.TreeNodeBindingCollection) As TreeNodeBindingCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TreeNodeBindingCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TreeNodeStyleCollection (TreeNodeStyle)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.TreeNodeStyleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.TreeNodeStyle"/> to be wrapped</param>
        ''' <returns><see cref="TreeNodeStyleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.TreeNodeStyleCollection) As TreeNodeStyleCollectionTypeSafeWrapper
            Return New TreeNodeStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.TreeNodeStyleCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.TreeNodeStyle)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TreeNodeStyleCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.TreeNodeStyleCollection, System.Web.UI.WebControls.TreeNodeStyle)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.TreeNodeStyleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.TreeNodeStyleCollection"/> to <see cref="TreeNodeStyleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.TreeNodeStyleCollection"/> to be converted</param>
            ''' <returns>A <see cref="TreeNodeStyleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.TreeNodeStyleCollection) As TreeNodeStyleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TreeNodeStyleCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ListItemCollection (ListItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.ListItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.ListItem"/> to be wrapped</param>
        ''' <returns><see cref="WebListItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.ListItemCollection) As WebListItemCollectionTypeSafeWrapper
            Return New WebListItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.ListItemCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.ListItem)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebListItemCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.ListItemCollection, System.Web.UI.WebControls.ListItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.ListItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.ListItemCollection"/> to <see cref="WebListItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.ListItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebListItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.ListItemCollection) As WebListItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebListItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TableCellCollection (TableCell)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.TableCellCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.TableCell"/> to be wrapped</param>
        ''' <returns><see cref="WebTableCellCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.TableCellCollection) As WebTableCellCollectionTypeSafeWrapper
            Return New WebTableCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.TableCellCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.TableCell)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebTableCellCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.TableCellCollection, System.Web.UI.WebControls.TableCell)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.TableCellCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.TableCellCollection"/> to <see cref="WebTableCellCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.TableCellCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebTableCellCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.TableCellCollection) As WebTableCellCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebTableCellCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TableRowCollection (TableRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.TableRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.TableRow"/> to be wrapped</param>
        ''' <returns><see cref="WebTableRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.TableRowCollection) As WebTableRowCollectionTypeSafeWrapper
            Return New WebTableRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.TableRowCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.TableRow)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebTableRowCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.TableRowCollection, System.Web.UI.WebControls.TableRow)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.TableRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.TableRowCollection"/> to <see cref="WebTableRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.TableRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebTableRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.TableRowCollection) As WebTableRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebTableRowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "WizardStepCollection (WizardStep)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Web.UI.WebControls.WizardStepCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Web.UI.WebControls.WizardStep"/> to be wrapped</param>
        ''' <returns><see cref="WizardStepCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Web.UI.WebControls.WizardStepCollection) As WizardStepCollectionTypeSafeWrapper
            Return New WizardStepCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Web.UI.WebControls.WizardStepCollection"/> as <see cref="IList(Of System.Web.UI.WebControls.WizardStep)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WizardStepCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Web.UI.WebControls.WizardStepCollection, System.Web.UI.WebControls.WizardStep)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Web.UI.WebControls.WizardStepCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Web.UI.WebControls.WizardStepCollection"/> to <see cref="WizardStepCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Web.UI.WebControls.WizardStepCollection"/> to be converted</param>
            ''' <returns>A <see cref="WizardStepCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Web.UI.WebControls.WizardStepCollection) As WizardStepCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WizardStepCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ColumnDefinitionCollection (ColumnDefinition)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Controls.ColumnDefinitionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Controls.ColumnDefinition"/> to be wrapped</param>
        ''' <returns><see cref="WebColumnDefinitionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Controls.ColumnDefinitionCollection) As WebColumnDefinitionCollectionTypeSafeWrapper
            Return New WebColumnDefinitionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Controls.ColumnDefinitionCollection"/> as <see cref="IList(Of System.Windows.Controls.ColumnDefinition)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class WebColumnDefinitionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Controls.ColumnDefinitionCollection, System.Windows.Controls.ColumnDefinition)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Controls.ColumnDefinitionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Controls.ColumnDefinitionCollection"/> to <see cref="WebColumnDefinitionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Controls.ColumnDefinitionCollection"/> to be converted</param>
            ''' <returns>A <see cref="WebColumnDefinitionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Controls.ColumnDefinitionCollection) As WebColumnDefinitionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New WebColumnDefinitionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "RowDefinitionCollection (RowDefinition)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Controls.RowDefinitionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Controls.RowDefinition"/> to be wrapped</param>
        ''' <returns><see cref="RowDefinitionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Controls.RowDefinitionCollection) As RowDefinitionCollectionTypeSafeWrapper
            Return New RowDefinitionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Controls.RowDefinitionCollection"/> as <see cref="IList(Of System.Windows.Controls.RowDefinition)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class RowDefinitionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Controls.RowDefinitionCollection, System.Windows.Controls.RowDefinition)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Controls.RowDefinitionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Controls.RowDefinitionCollection"/> to <see cref="RowDefinitionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Controls.RowDefinitionCollection"/> to be converted</param>
            ''' <returns>A <see cref="RowDefinitionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Controls.RowDefinitionCollection) As RowDefinitionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New RowDefinitionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "UIElementCollection (UIElement)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Controls.UIElementCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.UIElement"/> to be wrapped</param>
        ''' <returns><see cref="UIElementCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Controls.UIElementCollection) As UIElementCollectionTypeSafeWrapper
            Return New UIElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Controls.UIElementCollection"/> as <see cref="IList(Of System.Windows.UIElement)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class UIElementCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Controls.UIElementCollection, System.Windows.UIElement)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Controls.UIElementCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Controls.UIElementCollection"/> to <see cref="UIElementCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Controls.UIElementCollection"/> to be converted</param>
            ''' <returns>A <see cref="UIElementCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Controls.UIElementCollection) As UIElementCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New UIElementCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TableCellCollection (TableCell)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Documents.TableCellCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Documents.TableCell"/> to be wrapped</param>
        ''' <returns><see cref="TableCellCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Documents.TableCellCollection) As TableCellCollectionTypeSafeWrapper
            Return New TableCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Documents.TableCellCollection"/> as <see cref="IList(Of System.Windows.Documents.TableCell)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TableCellCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Documents.TableCellCollection, System.Windows.Documents.TableCell)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Documents.TableCellCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Documents.TableCellCollection"/> to <see cref="TableCellCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Documents.TableCellCollection"/> to be converted</param>
            ''' <returns>A <see cref="TableCellCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Documents.TableCellCollection) As TableCellCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TableCellCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TableColumnCollection (TableColumn)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Documents.TableColumnCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Documents.TableColumn"/> to be wrapped</param>
        ''' <returns><see cref="TableColumnCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Documents.TableColumnCollection) As TableColumnCollectionTypeSafeWrapper
            Return New TableColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Documents.TableColumnCollection"/> as <see cref="IList(Of System.Windows.Documents.TableColumn)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TableColumnCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Documents.TableColumnCollection, System.Windows.Documents.TableColumn)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Documents.TableColumnCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Documents.TableColumnCollection"/> to <see cref="TableColumnCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Documents.TableColumnCollection"/> to be converted</param>
            ''' <returns>A <see cref="TableColumnCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Documents.TableColumnCollection) As TableColumnCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TableColumnCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TableRowCollection (TableRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Documents.TableRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Documents.TableRow"/> to be wrapped</param>
        ''' <returns><see cref="TableRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Documents.TableRowCollection) As TableRowCollectionTypeSafeWrapper
            Return New TableRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Documents.TableRowCollection"/> as <see cref="IList(Of System.Windows.Documents.TableRow)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TableRowCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Documents.TableRowCollection, System.Windows.Documents.TableRow)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Documents.TableRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Documents.TableRowCollection"/> to <see cref="TableRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Documents.TableRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="TableRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Documents.TableRowCollection) As TableRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TableRowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TableRowGroupCollection (TableRowGroup)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Documents.TableRowGroupCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Documents.TableRowGroup"/> to be wrapped</param>
        ''' <returns><see cref="TableRowGroupCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Documents.TableRowGroupCollection) As TableRowGroupCollectionTypeSafeWrapper
            Return New TableRowGroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Documents.TableRowGroupCollection"/> as <see cref="IList(Of System.Windows.Documents.TableRowGroup)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TableRowGroupCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Documents.TableRowGroupCollection, System.Windows.Documents.TableRowGroup)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Documents.TableRowGroupCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Documents.TableRowGroupCollection"/> to <see cref="TableRowGroupCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Documents.TableRowGroupCollection"/> to be converted</param>
            ''' <returns>A <see cref="TableRowGroupCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Documents.TableRowGroupCollection) As TableRowGroupCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TableRowGroupCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "AutoCompleteStringCollection (String)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.AutoCompleteStringCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.String"/> to be wrapped</param>
        ''' <returns><see cref="AutoCompleteStringCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.AutoCompleteStringCollection) As AutoCompleteStringCollectionTypeSafeWrapper
            Return New AutoCompleteStringCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.AutoCompleteStringCollection"/> as <see cref="IList(Of System.String)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class AutoCompleteStringCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.AutoCompleteStringCollection, System.String)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.AutoCompleteStringCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.AutoCompleteStringCollection"/> to <see cref="AutoCompleteStringCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.AutoCompleteStringCollection"/> to be converted</param>
            ''' <returns>A <see cref="AutoCompleteStringCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.AutoCompleteStringCollection) As AutoCompleteStringCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New AutoCompleteStringCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataGridViewCellCollection (DataGridViewCell)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.DataGridViewCellCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.DataGridViewCell"/> to be wrapped</param>
        ''' <returns><see cref="DataGridViewCellCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.DataGridViewCellCollection) As DataGridViewCellCollectionTypeSafeWrapper
            Return New DataGridViewCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.DataGridViewCellCollection"/> as <see cref="IList(Of System.Windows.Forms.DataGridViewCell)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataGridViewCellCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.DataGridViewCellCollection, System.Windows.Forms.DataGridViewCell)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.DataGridViewCellCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.DataGridViewCellCollection"/> to <see cref="DataGridViewCellCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.DataGridViewCellCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataGridViewCellCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.DataGridViewCellCollection) As DataGridViewCellCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataGridViewCellCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataGridViewColumnCollection (DataGridViewColumn)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.DataGridViewColumnCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.DataGridViewColumn"/> to be wrapped</param>
        ''' <returns><see cref="DataGridViewColumnCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.DataGridViewColumnCollection) As DataGridViewColumnCollectionTypeSafeWrapper
            Return New DataGridViewColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.DataGridViewColumnCollection"/> as <see cref="IList(Of System.Windows.Forms.DataGridViewColumn)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataGridViewColumnCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.DataGridViewColumnCollection, System.Windows.Forms.DataGridViewColumn)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.DataGridViewColumnCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.DataGridViewColumnCollection"/> to <see cref="DataGridViewColumnCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.DataGridViewColumnCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataGridViewColumnCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.DataGridViewColumnCollection) As DataGridViewColumnCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataGridViewColumnCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataGridViewRowCollection (DataGridViewRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.DataGridViewRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.DataGridViewRow"/> to be wrapped</param>
        ''' <returns><see cref="DataGridViewRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.DataGridViewRowCollection) As DataGridViewRowCollectionTypeSafeWrapper
            Return New DataGridViewRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.DataGridViewRowCollection"/> as <see cref="IList(Of System.Windows.Forms.DataGridViewRow)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataGridViewRowCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.DataGridViewRowCollection, System.Windows.Forms.DataGridViewRow)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.DataGridViewRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.DataGridViewRowCollection"/> to <see cref="DataGridViewRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.DataGridViewRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataGridViewRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.DataGridViewRowCollection) As DataGridViewRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataGridViewRowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataGridViewSelectedCellCollection (DataGridViewCell)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.DataGridViewSelectedCellCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.DataGridViewCell"/> to be wrapped</param>
        ''' <returns><see cref="DataGridViewSelectedCellCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.DataGridViewSelectedCellCollection) As DataGridViewSelectedCellCollectionTypeSafeWrapper
            Return New DataGridViewSelectedCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.DataGridViewSelectedCellCollection"/> as <see cref="IList(Of System.Windows.Forms.DataGridViewCell)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataGridViewSelectedCellCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.DataGridViewSelectedCellCollection, System.Windows.Forms.DataGridViewCell)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.DataGridViewSelectedCellCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.DataGridViewSelectedCellCollection"/> to <see cref="DataGridViewSelectedCellCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.DataGridViewSelectedCellCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataGridViewSelectedCellCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.DataGridViewSelectedCellCollection) As DataGridViewSelectedCellCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataGridViewSelectedCellCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataGridViewSelectedColumnCollection (DataGridViewColumn)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.DataGridViewSelectedColumnCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.DataGridViewColumn"/> to be wrapped</param>
        ''' <returns><see cref="DataGridViewSelectedColumnCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.DataGridViewSelectedColumnCollection) As DataGridViewSelectedColumnCollectionTypeSafeWrapper
            Return New DataGridViewSelectedColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.DataGridViewSelectedColumnCollection"/> as <see cref="IList(Of System.Windows.Forms.DataGridViewColumn)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataGridViewSelectedColumnCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.DataGridViewSelectedColumnCollection, System.Windows.Forms.DataGridViewColumn)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.DataGridViewSelectedColumnCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.DataGridViewSelectedColumnCollection"/> to <see cref="DataGridViewSelectedColumnCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.DataGridViewSelectedColumnCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataGridViewSelectedColumnCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.DataGridViewSelectedColumnCollection) As DataGridViewSelectedColumnCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataGridViewSelectedColumnCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "DataGridViewSelectedRowCollection (DataGridViewRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.DataGridViewSelectedRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.DataGridViewRow"/> to be wrapped</param>
        ''' <returns><see cref="DataGridViewSelectedRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.DataGridViewSelectedRowCollection) As DataGridViewSelectedRowCollectionTypeSafeWrapper
            Return New DataGridViewSelectedRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.DataGridViewSelectedRowCollection"/> as <see cref="IList(Of System.Windows.Forms.DataGridViewRow)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class DataGridViewSelectedRowCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.DataGridViewSelectedRowCollection, System.Windows.Forms.DataGridViewRow)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.DataGridViewSelectedRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.DataGridViewSelectedRowCollection"/> to <see cref="DataGridViewSelectedRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.DataGridViewSelectedRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="DataGridViewSelectedRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.DataGridViewSelectedRowCollection) As DataGridViewSelectedRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New DataGridViewSelectedRowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "GridColumnStylesCollection (DataGridColumnStyle)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.GridColumnStylesCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.DataGridColumnStyle"/> to be wrapped</param>
        ''' <returns><see cref="GridColumnStylesCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.GridColumnStylesCollection) As GridColumnStylesCollectionTypeSafeWrapper
            Return New GridColumnStylesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.GridColumnStylesCollection"/> as <see cref="IList(Of System.Windows.Forms.DataGridColumnStyle)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class GridColumnStylesCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.GridColumnStylesCollection, System.Windows.Forms.DataGridColumnStyle)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.GridColumnStylesCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.GridColumnStylesCollection"/> to <see cref="GridColumnStylesCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.GridColumnStylesCollection"/> to be converted</param>
            ''' <returns>A <see cref="GridColumnStylesCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.GridColumnStylesCollection) As GridColumnStylesCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New GridColumnStylesCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "GridTableStylesCollection (DataGridTableStyle)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.GridTableStylesCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.DataGridTableStyle"/> to be wrapped</param>
        ''' <returns><see cref="GridTableStylesCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.GridTableStylesCollection) As GridTableStylesCollectionTypeSafeWrapper
            Return New GridTableStylesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.GridTableStylesCollection"/> as <see cref="IList(Of System.Windows.Forms.DataGridTableStyle)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class GridTableStylesCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.GridTableStylesCollection, System.Windows.Forms.DataGridTableStyle)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.GridTableStylesCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.GridTableStylesCollection"/> to <see cref="GridTableStylesCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.GridTableStylesCollection"/> to be converted</param>
            ''' <returns>A <see cref="GridTableStylesCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.GridTableStylesCollection) As GridTableStylesCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New GridTableStylesCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CheckedIndexCollection (Int32)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.CheckedListBox.CheckedIndexCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Int32"/> to be wrapped</param>
        ''' <returns><see cref="ListBoxCheckedIndexCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.CheckedListBox.CheckedIndexCollection) As ListBoxCheckedIndexCollectionTypeSafeWrapper
            Return New ListBoxCheckedIndexCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.CheckedListBox.CheckedIndexCollection"/> as <see cref="IList(Of System.Int32)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ListBoxCheckedIndexCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.CheckedListBox.CheckedIndexCollection, System.Int32)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.CheckedListBox.CheckedIndexCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.CheckedListBox.CheckedIndexCollection"/> to <see cref="ListBoxCheckedIndexCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.CheckedListBox.CheckedIndexCollection"/> to be converted</param>
            ''' <returns>A <see cref="ListBoxCheckedIndexCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.CheckedListBox.CheckedIndexCollection) As ListBoxCheckedIndexCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ListBoxCheckedIndexCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ImageCollection (Image)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ImageList.ImageCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Drawing.Image"/> to be wrapped</param>
        ''' <returns><see cref="ImageCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ImageList.ImageCollection) As ImageCollectionTypeSafeWrapper
            Return New ImageCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ImageList.ImageCollection"/> as <see cref="IList(Of System.Drawing.Image)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ImageCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ImageList.ImageCollection, System.Drawing.Image)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ImageList.ImageCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ImageList.ImageCollection"/> to <see cref="ImageCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ImageList.ImageCollection"/> to be converted</param>
            ''' <returns>A <see cref="ImageCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ImageList.ImageCollection) As ImageCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ImageCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ControlCollection (Control)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.Control.ControlCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.Control"/> to be wrapped</param>
        ''' <returns><see cref="ControlCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.Control.ControlCollection) As ControlCollectionTypeSafeWrapper
            Return New ControlCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.Control.ControlCollection"/> as <see cref="IList(Of System.Windows.Forms.Control)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ControlCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.Control.ControlCollection, System.Windows.Forms.Control)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.Control.ControlCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.Control.ControlCollection"/> to <see cref="ControlCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.Control.ControlCollection"/> to be converted</param>
            ''' <returns>A <see cref="ControlCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.Control.ControlCollection) As ControlCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ControlCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ToolStripItemCollection (ToolStripItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ToolStripItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ToolStripItem"/> to be wrapped</param>
        ''' <returns><see cref="ToolStripItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ToolStripItemCollection) As ToolStripItemCollectionTypeSafeWrapper
            Return New ToolStripItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ToolStripItemCollection"/> as <see cref="IList(Of System.Windows.Forms.ToolStripItem)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ToolStripItemCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ToolStripItemCollection, System.Windows.Forms.ToolStripItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ToolStripItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ToolStripItemCollection"/> to <see cref="ToolStripItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ToolStripItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="ToolStripItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ToolStripItemCollection) As ToolStripItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ToolStripItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ToolStripPanelRowCollection (ToolStripPanelRow)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ToolStripPanelRow"/> to be wrapped</param>
        ''' <returns><see cref="ToolStripPanelRowCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection) As ToolStripPanelRowCollectionTypeSafeWrapper
            Return New ToolStripPanelRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection"/> as <see cref="IList(Of System.Windows.Forms.ToolStripPanelRow)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ToolStripPanelRowCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection, System.Windows.Forms.ToolStripPanelRow)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection"/> to <see cref="ToolStripPanelRowCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection"/> to be converted</param>
            ''' <returns>A <see cref="ToolStripPanelRowCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection) As ToolStripPanelRowCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ToolStripPanelRowCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "LinkCollection (Link)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.LinkLabel.LinkCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.LinkLabel.Link"/> to be wrapped</param>
        ''' <returns><see cref="LinkCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.LinkLabel.LinkCollection) As LinkCollectionTypeSafeWrapper
            Return New LinkCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.LinkLabel.LinkCollection"/> as <see cref="IList(Of System.Windows.Forms.LinkLabel.Link)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class LinkCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.LinkLabel.LinkCollection, System.Windows.Forms.LinkLabel.Link)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.LinkLabel.LinkCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.LinkLabel.LinkCollection"/> to <see cref="LinkCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.LinkLabel.LinkCollection"/> to be converted</param>
            ''' <returns>A <see cref="LinkCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.LinkLabel.LinkCollection) As LinkCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New LinkCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "IntegerCollection (Int32)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListBox.IntegerCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Int32"/> to be wrapped</param>
        ''' <returns><see cref="IntegerCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListBox.IntegerCollection) As IntegerCollectionTypeSafeWrapper
            Return New IntegerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListBox.IntegerCollection"/> as <see cref="IList(Of System.Int32)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class IntegerCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListBox.IntegerCollection, System.Int32)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListBox.IntegerCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListBox.IntegerCollection"/> to <see cref="IntegerCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListBox.IntegerCollection"/> to be converted</param>
            ''' <returns>A <see cref="IntegerCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListBox.IntegerCollection) As IntegerCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New IntegerCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SelectedIndexCollection (Int32)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListBox.SelectedIndexCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Int32"/> to be wrapped</param>
        ''' <returns><see cref="ListBoxSelectedIndexCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListBox.SelectedIndexCollection) As ListBoxSelectedIndexCollectionTypeSafeWrapper
            Return New ListBoxSelectedIndexCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListBox.SelectedIndexCollection"/> as <see cref="IList(Of System.Int32)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ListBoxSelectedIndexCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListBox.SelectedIndexCollection, System.Int32)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListBox.SelectedIndexCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListBox.SelectedIndexCollection"/> to <see cref="ListBoxSelectedIndexCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListBox.SelectedIndexCollection"/> to be converted</param>
            ''' <returns>A <see cref="ListBoxSelectedIndexCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListBox.SelectedIndexCollection) As ListBoxSelectedIndexCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ListBoxSelectedIndexCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ColumnHeaderCollection (ColumnHeader)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListView.ColumnHeaderCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ColumnHeader"/> to be wrapped</param>
        ''' <returns><see cref="ColumnHeaderCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListView.ColumnHeaderCollection) As ColumnHeaderCollectionTypeSafeWrapper
            Return New ColumnHeaderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListView.ColumnHeaderCollection"/> as <see cref="IList(Of System.Windows.Forms.ColumnHeader)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ColumnHeaderCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListView.ColumnHeaderCollection, System.Windows.Forms.ColumnHeader)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListView.ColumnHeaderCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListView.ColumnHeaderCollection"/> to <see cref="ColumnHeaderCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListView.ColumnHeaderCollection"/> to be converted</param>
            ''' <returns>A <see cref="ColumnHeaderCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListView.ColumnHeaderCollection) As ColumnHeaderCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ColumnHeaderCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CheckedIndexCollection (Int32)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListView.CheckedIndexCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Int32"/> to be wrapped</param>
        ''' <returns><see cref="ListViewCheckedIndexCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListView.CheckedIndexCollection) As ListViewCheckedIndexCollectionTypeSafeWrapper
            Return New ListViewCheckedIndexCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListView.CheckedIndexCollection"/> as <see cref="IList(Of System.Int32)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ListViewCheckedIndexCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListView.CheckedIndexCollection, System.Int32)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListView.CheckedIndexCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListView.CheckedIndexCollection"/> to <see cref="ListViewCheckedIndexCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListView.CheckedIndexCollection"/> to be converted</param>
            ''' <returns>A <see cref="ListViewCheckedIndexCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListView.CheckedIndexCollection) As ListViewCheckedIndexCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ListViewCheckedIndexCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "CheckedListViewItemCollection (ListViewItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListView.CheckedListViewItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ListViewItem"/> to be wrapped</param>
        ''' <returns><see cref="CheckedListViewItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListView.CheckedListViewItemCollection) As CheckedListViewItemCollectionTypeSafeWrapper
            Return New CheckedListViewItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListView.CheckedListViewItemCollection"/> as <see cref="IList(Of System.Windows.Forms.ListViewItem)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class CheckedListViewItemCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListView.CheckedListViewItemCollection, System.Windows.Forms.ListViewItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListView.CheckedListViewItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListView.CheckedListViewItemCollection"/> to <see cref="CheckedListViewItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListView.CheckedListViewItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="CheckedListViewItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListView.CheckedListViewItemCollection) As CheckedListViewItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New CheckedListViewItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ListViewItemCollection (ListViewItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListView.ListViewItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ListViewItem"/> to be wrapped</param>
        ''' <returns><see cref="ListViewItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListView.ListViewItemCollection) As ListViewItemCollectionTypeSafeWrapper
            Return New ListViewItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListView.ListViewItemCollection"/> as <see cref="IList(Of System.Windows.Forms.ListViewItem)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ListViewItemCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListView.ListViewItemCollection, System.Windows.Forms.ListViewItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListView.ListViewItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListView.ListViewItemCollection"/> to <see cref="ListViewItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListView.ListViewItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="ListViewItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListView.ListViewItemCollection) As ListViewItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ListViewItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SelectedIndexCollection (Int32)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListView.SelectedIndexCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Int32"/> to be wrapped</param>
        ''' <returns><see cref="SelectedIndexCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListView.SelectedIndexCollection) As SelectedIndexCollectionTypeSafeWrapper
            Return New SelectedIndexCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListView.SelectedIndexCollection"/> as <see cref="IList(Of System.Int32)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SelectedIndexCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListView.SelectedIndexCollection, System.Int32)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListView.SelectedIndexCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListView.SelectedIndexCollection"/> to <see cref="SelectedIndexCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListView.SelectedIndexCollection"/> to be converted</param>
            ''' <returns>A <see cref="SelectedIndexCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListView.SelectedIndexCollection) As SelectedIndexCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SelectedIndexCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "SelectedListViewItemCollection (ListViewItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListView.SelectedListViewItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ListViewItem"/> to be wrapped</param>
        ''' <returns><see cref="SelectedListViewItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListView.SelectedListViewItemCollection) As SelectedListViewItemCollectionTypeSafeWrapper
            Return New SelectedListViewItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListView.SelectedListViewItemCollection"/> as <see cref="IList(Of System.Windows.Forms.ListViewItem)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class SelectedListViewItemCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListView.SelectedListViewItemCollection, System.Windows.Forms.ListViewItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListView.SelectedListViewItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListView.SelectedListViewItemCollection"/> to <see cref="SelectedListViewItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListView.SelectedListViewItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="SelectedListViewItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListView.SelectedListViewItemCollection) As SelectedListViewItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New SelectedListViewItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ListViewGroupCollection (ListViewGroup)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListViewGroupCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ListViewGroup"/> to be wrapped</param>
        ''' <returns><see cref="ListViewGroupCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListViewGroupCollection) As ListViewGroupCollectionTypeSafeWrapper
            Return New ListViewGroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListViewGroupCollection"/> as <see cref="IList(Of System.Windows.Forms.ListViewGroup)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ListViewGroupCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListViewGroupCollection, System.Windows.Forms.ListViewGroup)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListViewGroupCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListViewGroupCollection"/> to <see cref="ListViewGroupCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListViewGroupCollection"/> to be converted</param>
            ''' <returns>A <see cref="ListViewGroupCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListViewGroupCollection) As ListViewGroupCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ListViewGroupCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ListViewSubItemCollection (ListViewSubItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ListViewItem.ListViewSubItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/> to be wrapped</param>
        ''' <returns><see cref="ListViewSubItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ListViewItem.ListViewSubItemCollection) As ListViewSubItemCollectionTypeSafeWrapper
            Return New ListViewSubItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ListViewItem.ListViewSubItemCollection"/> as <see cref="IList(Of System.Windows.Forms.ListViewItem.ListViewSubItem)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ListViewSubItemCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ListViewItem.ListViewSubItemCollection, System.Windows.Forms.ListViewItem.ListViewSubItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ListViewItem.ListViewSubItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ListViewItem.ListViewSubItemCollection"/> to <see cref="ListViewSubItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ListViewItem.ListViewSubItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="ListViewSubItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ListViewItem.ListViewSubItemCollection) As ListViewSubItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ListViewSubItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "MenuItemCollection (MenuItem)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.Menu.MenuItemCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.MenuItem"/> to be wrapped</param>
        ''' <returns><see cref="MenuItemCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.Menu.MenuItemCollection) As MenuItemCollectionTypeSafeWrapper
            Return New MenuItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.Menu.MenuItemCollection"/> as <see cref="IList(Of System.Windows.Forms.MenuItem)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class MenuItemCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.Menu.MenuItemCollection, System.Windows.Forms.MenuItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.Menu.MenuItemCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.Menu.MenuItemCollection"/> to <see cref="MenuItemCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.Menu.MenuItemCollection"/> to be converted</param>
            ''' <returns>A <see cref="MenuItemCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.Menu.MenuItemCollection) As MenuItemCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New MenuItemCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "StatusBarPanelCollection (StatusBarPanel)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.StatusBar.StatusBarPanelCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.StatusBarPanel"/> to be wrapped</param>
        ''' <returns><see cref="StatusBarPanelCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.StatusBar.StatusBarPanelCollection) As StatusBarPanelCollectionTypeSafeWrapper
            Return New StatusBarPanelCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.StatusBar.StatusBarPanelCollection"/> as <see cref="IList(Of System.Windows.Forms.StatusBarPanel)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class StatusBarPanelCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.StatusBar.StatusBarPanelCollection, System.Windows.Forms.StatusBarPanel)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.StatusBar.StatusBarPanelCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.StatusBar.StatusBarPanelCollection"/> to <see cref="StatusBarPanelCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.StatusBar.StatusBarPanelCollection"/> to be converted</param>
            ''' <returns>A <see cref="StatusBarPanelCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.StatusBar.StatusBarPanelCollection) As StatusBarPanelCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New StatusBarPanelCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TabPageCollection (TabPage)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.TabControl.TabPageCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.TabPage"/> to be wrapped</param>
        ''' <returns><see cref="TabPageCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.TabControl.TabPageCollection) As TabPageCollectionTypeSafeWrapper
            Return New TabPageCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.TabControl.TabPageCollection"/> as <see cref="IList(Of System.Windows.Forms.TabPage)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TabPageCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.TabControl.TabPageCollection, System.Windows.Forms.TabPage)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.TabControl.TabPageCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.TabControl.TabPageCollection"/> to <see cref="TabPageCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.TabControl.TabPageCollection"/> to be converted</param>
            ''' <returns>A <see cref="TabPageCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.TabControl.TabPageCollection) As TabPageCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TabPageCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TableLayoutStyleCollection (TableLayoutStyle)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.TableLayoutStyleCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.TableLayoutStyle"/> to be wrapped</param>
        ''' <returns><see cref="TableLayoutStyleCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.TableLayoutStyleCollection) As TableLayoutStyleCollectionTypeSafeWrapper
            Return New TableLayoutStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.TableLayoutStyleCollection"/> as <see cref="IList(Of System.Windows.Forms.TableLayoutStyle)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TableLayoutStyleCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.TableLayoutStyleCollection, System.Windows.Forms.TableLayoutStyle)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.TableLayoutStyleCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.TableLayoutStyleCollection"/> to <see cref="TableLayoutStyleCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.TableLayoutStyleCollection"/> to be converted</param>
            ''' <returns>A <see cref="TableLayoutStyleCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.TableLayoutStyleCollection) As TableLayoutStyleCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TableLayoutStyleCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ToolBarButtonCollection (ToolBarButton)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.ToolBar.ToolBarButtonCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.ToolBarButton"/> to be wrapped</param>
        ''' <returns><see cref="ToolBarButtonCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.ToolBar.ToolBarButtonCollection) As ToolBarButtonCollectionTypeSafeWrapper
            Return New ToolBarButtonCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.ToolBar.ToolBarButtonCollection"/> as <see cref="IList(Of System.Windows.Forms.ToolBarButton)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ToolBarButtonCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.ToolBar.ToolBarButtonCollection, System.Windows.Forms.ToolBarButton)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.ToolBar.ToolBarButtonCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.ToolBar.ToolBarButtonCollection"/> to <see cref="ToolBarButtonCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.ToolBar.ToolBarButtonCollection"/> to be converted</param>
            ''' <returns>A <see cref="ToolBarButtonCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.ToolBar.ToolBarButtonCollection) As ToolBarButtonCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ToolBarButtonCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TreeNodeCollection (TreeNode)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Forms.TreeNodeCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Forms.TreeNode"/> to be wrapped</param>
        ''' <returns><see cref="TreeNodeCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Forms.TreeNodeCollection) As TreeNodeCollectionTypeSafeWrapper
            Return New TreeNodeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Forms.TreeNodeCollection"/> as <see cref="IList(Of System.Windows.Forms.TreeNode)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TreeNodeCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Forms.TreeNodeCollection, System.Windows.Forms.TreeNode)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Forms.TreeNodeCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Forms.TreeNodeCollection"/> to <see cref="TreeNodeCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Forms.TreeNodeCollection"/> to be converted</param>
            ''' <returns>A <see cref="TreeNodeCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Forms.TreeNodeCollection) As TreeNodeCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TreeNodeCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "ThicknessKeyFrameCollection (ThicknessKeyFrame)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.Media.Animation.ThicknessKeyFrameCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.Media.Animation.ThicknessKeyFrame"/> to be wrapped</param>
        ''' <returns><see cref="ThicknessKeyFrameCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.Media.Animation.ThicknessKeyFrameCollection) As ThicknessKeyFrameCollectionTypeSafeWrapper
            Return New ThicknessKeyFrameCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.Media.Animation.ThicknessKeyFrameCollection"/> as <see cref="IList(Of System.Windows.Media.Animation.ThicknessKeyFrame)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ThicknessKeyFrameCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.Media.Animation.ThicknessKeyFrameCollection, System.Windows.Media.Animation.ThicknessKeyFrame)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.Media.Animation.ThicknessKeyFrameCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.Media.Animation.ThicknessKeyFrameCollection"/> to <see cref="ThicknessKeyFrameCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.Media.Animation.ThicknessKeyFrameCollection"/> to be converted</param>
            ''' <returns>A <see cref="ThicknessKeyFrameCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.Media.Animation.ThicknessKeyFrameCollection) As ThicknessKeyFrameCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New ThicknessKeyFrameCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
#Region "TriggerActionCollection (TriggerAction)"
        ''' <summary>Gets type-sfafe wrapper for <see cref="System.Windows.TriggerActionCollection"/></summary>
        ''' <param name="Collection">A <see cref="System.Windows.TriggerAction"/> to be wrapped</param>
        ''' <returns><see cref="TriggerActionCollectionTypeSafeWrapper"/> that wraps <paramref name="Collection"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Public Shared Function GetWrapper(ByVal Collection As System.Windows.TriggerActionCollection) As TriggerActionCollectionTypeSafeWrapper
            Return New TriggerActionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Wraps <see cref="System.Windows.TriggerActionCollection"/> as <see cref="IList(Of System.Windows.TriggerAction)"/>)"/></summary> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class TriggerActionCollectionTypeSafeWrapper
            Inherits IListTypeSafeWrapper(Of System.Windows.TriggerActionCollection, System.Windows.TriggerAction)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Public Sub New(ByVal Collection As System.Windows.TriggerActionCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Converts <see cref="System.Windows.TriggerActionCollection"/> to <see cref="TriggerActionCollectionTypeSafeWrapper"/></summary>
            ''' <param name="a">A <see cref="System.Windows.TriggerActionCollection"/> to be converted</param>
            ''' <returns>A <see cref="TriggerActionCollectionTypeSafeWrapper"/> which wraps <paramref name="a"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Overloads Shared Widening Operator CType(ByVal a As System.Windows.TriggerActionCollection) As TriggerActionCollectionTypeSafeWrapper
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return New TriggerActionCollectionTypeSafeWrapper(a)
            End Operator
        End Class
#End Region
    End Class
    ''' <summary>Contains extension methods for getting specialized collections as type-safe generic collections</summary>
    Public Module AsTypeSafe
        ''' <summary>Gets type-safe wrapper of <see cref="System.Collections.BitArray"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Collections.BitArray) As SpecializedWrapper.BitArrayTypeSafeWrapper
            Return New SpecializedWrapper.BitArrayTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.ComponentModel.AttributeCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.ComponentModel.AttributeCollection) As SpecializedWrapper.AttributeCollectionTypeSafeWrapper
            Return New SpecializedWrapper.AttributeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.ComponentModel.Design.DesignerCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.ComponentModel.Design.DesignerCollection) As SpecializedWrapper.DesignerCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DesignerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.DataViewSettingCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.DataViewSettingCollection) As SpecializedWrapper.DataViewSettingCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataViewSettingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.Odbc.OdbcErrorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.Odbc.OdbcErrorCollection) As SpecializedWrapper.OdbcErrorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.OdbcErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.OleDb.OleDbErrorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.OleDb.OleDbErrorCollection) As SpecializedWrapper.OleDbErrorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.OleDbErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.SqlClient.SqlErrorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.SqlClient.SqlErrorCollection) As SpecializedWrapper.SqlErrorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SqlErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Diagnostics.EventLogEntryCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Diagnostics.EventLogEntryCollection) As SpecializedWrapper.EventLogEntryCollectionTypeSafeWrapper
            Return New SpecializedWrapper.EventLogEntryCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Drawing.Printing.PrinterSettings.PaperSourceCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Drawing.Printing.PrinterSettings.PaperSourceCollection) As SpecializedWrapper.PaperSourceCollectionTypeSafeWrapper
            Return New SpecializedWrapper.PaperSourceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Drawing.Printing.PrinterSettings.PaperSizeCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Drawing.Printing.PrinterSettings.PaperSizeCollection) As SpecializedWrapper.PaperSizeCollectionTypeSafeWrapper
            Return New SpecializedWrapper.PaperSizeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection) As SpecializedWrapper.PrinterResolutionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.PrinterResolutionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Net.CookieCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Net.CookieCollection) As SpecializedWrapper.CookieCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CookieCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.Cryptography.AsnEncodedDataCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.Cryptography.AsnEncodedDataCollection) As SpecializedWrapper.AsnEncodedDataCollectionTypeSafeWrapper
            Return New SpecializedWrapper.AsnEncodedDataCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.Cryptography.OidCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.Cryptography.OidCollection) As SpecializedWrapper.OidCollectionTypeSafeWrapper
            Return New SpecializedWrapper.OidCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.Cryptography.X509Certificates.X509ExtensionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.Cryptography.X509Certificates.X509ExtensionCollection) As SpecializedWrapper.X509ExtensionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.X509ExtensionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.Cryptography.X509Certificates.X509ChainElementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.Cryptography.X509Certificates.X509ChainElementCollection) As SpecializedWrapper.X509ChainElementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.X509ChainElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.Permissions.KeyContainerPermissionAccessEntryCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.Permissions.KeyContainerPermissionAccessEntryCollection) As SpecializedWrapper.KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper
            Return New SpecializedWrapper.KeyContainerPermissionAccessEntryCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.Policy.ApplicationTrustCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.Policy.ApplicationTrustCollection) As SpecializedWrapper.ApplicationTrustCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ApplicationTrustCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Text.RegularExpressions.CaptureCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Text.RegularExpressions.CaptureCollection) As SpecializedWrapper.CaptureCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CaptureCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Text.RegularExpressions.GroupCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Text.RegularExpressions.GroupCollection) As SpecializedWrapper.GroupCollectionTypeSafeWrapper
            Return New SpecializedWrapper.GroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Text.RegularExpressions.MatchCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Text.RegularExpressions.MatchCollection) As SpecializedWrapper.MatchCollectionTypeSafeWrapper
            Return New SpecializedWrapper.MatchCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.ControlCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.ControlCollection) As SpecializedWrapper.WebControlCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebControlCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.HtmlControls.HtmlTableCellCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.HtmlControls.HtmlTableCellCollection) As SpecializedWrapper.HtmlTableCellCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HtmlTableCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.HtmlControls.HtmlTableRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.HtmlControls.HtmlTableRowCollection) As SpecializedWrapper.HtmlTableRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HtmlTableRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.ValidatorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.ValidatorCollection) As SpecializedWrapper.ValidatorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ValidatorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.DataGridColumnCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.DataGridColumnCollection) As SpecializedWrapper.DataGridColumnCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataGridColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.DataGridItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.DataGridItemCollection) As SpecializedWrapper.DataGridItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataGridItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.DataKeyArray"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.DataKeyArray) As SpecializedWrapper.DataKeyArrayTypeSafeWrapper
            Return New SpecializedWrapper.DataKeyArrayTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.DataListItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.DataListItemCollection) As SpecializedWrapper.DataListItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataListItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.DetailsViewRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.DetailsViewRowCollection) As SpecializedWrapper.DetailsViewRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DetailsViewRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.GridViewRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.GridViewRowCollection) As SpecializedWrapper.GridViewRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.GridViewRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.MenuItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.MenuItemCollection) As SpecializedWrapper.WebMenuItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebMenuItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.RepeaterItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.RepeaterItemCollection) As SpecializedWrapper.RepeaterItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.RepeaterItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.SelectedDatesCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.SelectedDatesCollection) As SpecializedWrapper.SelectedDatesCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SelectedDatesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.TreeNodeCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.TreeNodeCollection) As SpecializedWrapper.WebTreeNodeCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebTreeNodeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection) As SpecializedWrapper.PersonalizationStateInfoCollectionTypeSafeWrapper
            Return New SpecializedWrapper.PersonalizationStateInfoCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.GridItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.GridItemCollection) As SpecializedWrapper.GridItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.GridItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.HtmlElementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.HtmlElementCollection) As SpecializedWrapper.HtmlElementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HtmlElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.HtmlWindowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.HtmlWindowCollection) As SpecializedWrapper.HtmlWindowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HtmlWindowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.PropertyGrid.PropertyTabCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.PropertyGrid.PropertyTabCollection) As SpecializedWrapper.PropertyTabCollectionTypeSafeWrapper
            Return New SpecializedWrapper.PropertyTabCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.WindowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.WindowCollection) As SpecializedWrapper.WindowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WindowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Xml.XmlAttributeCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Xml.XmlAttributeCollection) As SpecializedWrapper.XmlAttributeCollectionTypeSafeWrapper
            Return New SpecializedWrapper.XmlAttributeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.ComponentModel.ComponentCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.ComponentModel.ComponentCollection) As SpecializedWrapper.ComponentCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ComponentCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Configuration.ConfigurationLocationCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Configuration.ConfigurationLocationCollection) As SpecializedWrapper.ConfigurationLocationCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ConfigurationLocationCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Diagnostics.ProcessModuleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Diagnostics.ProcessModuleCollection) As SpecializedWrapper.ProcessModuleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProcessModuleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Diagnostics.ProcessThreadCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Diagnostics.ProcessThreadCollection) As SpecializedWrapper.ProcessThreadCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProcessThreadCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Drawing.Design.CategoryNameCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Drawing.Design.CategoryNameCollection) As SpecializedWrapper.CategoryNameCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CategoryNameCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Drawing.Design.ToolboxItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Drawing.Design.ToolboxItemCollection) As SpecializedWrapper.ToolboxItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ToolboxItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.AccessControl.AuthorizationRuleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.AccessControl.AuthorizationRuleCollection) As SpecializedWrapper.AuthorizationRuleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.AuthorizationRuleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Management.WebBaseEventCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Management.WebBaseEventCollection) As SpecializedWrapper.WebBaseEventCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebBaseEventCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.CatalogPartCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.CatalogPartCollection) As SpecializedWrapper.CatalogPartCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CatalogPartCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection) As SpecializedWrapper.ConnectionInterfaceCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ConnectionInterfaceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection) As SpecializedWrapper.ConsumerConnectionPointCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ConsumerConnectionPointCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.EditorPartCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.EditorPartCollection) As SpecializedWrapper.EditorPartCollectionTypeSafeWrapper
            Return New SpecializedWrapper.EditorPartCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection) As SpecializedWrapper.ProviderConnectionPointCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProviderConnectionPointCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.TransformerTypeCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.TransformerTypeCollection) As SpecializedWrapper.TransformerTypeCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TransformerTypeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.WebPartCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartCollection) As SpecializedWrapper.WebPartCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebPartCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection) As SpecializedWrapper.WebPartDescriptionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebPartDescriptionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.WebPartVerbCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartVerbCollection) As SpecializedWrapper.WebPartVerbCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebPartVerbCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.WebPartZoneCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartZoneCollection) As SpecializedWrapper.WebPartZoneCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebPartZoneCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.windows.Forms.FormCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.windows.Forms.FormCollection) As SpecializedWrapper.FormCollectionTypeSafeWrapper
            Return New SpecializedWrapper.FormCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.InputLanguageCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.InputLanguageCollection) As SpecializedWrapper.InputLanguageCollectionTypeSafeWrapper
            Return New SpecializedWrapper.InputLanguageCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Configuration.ConfigurationSectionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Configuration.ConfigurationSectionCollection) As SpecializedWrapper.ConfigurationSectionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ConfigurationSectionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Configuration.ConfigurationSectionGroupCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Configuration.ConfigurationSectionGroupCollection) As SpecializedWrapper.ConfigurationSectionGroupCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ConfigurationSectionGroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.VirtualDirectoryMappingCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.VirtualDirectoryMappingCollection) As SpecializedWrapper.VirtualDirectoryMappingCollectionTypeSafeWrapper
            Return New SpecializedWrapper.VirtualDirectoryMappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.HttpCookieCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.HttpCookieCollection) As SpecializedWrapper.HttpCookieCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HttpCookieCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.HttpFileCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.HttpFileCollection) As SpecializedWrapper.HttpFileCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HttpFileCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.HttpModuleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.HttpModuleCollection) As SpecializedWrapper.HttpModuleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HttpModuleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Configuration.ConnectionStringSettingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Configuration.ConnectionStringSettingsCollection) As SpecializedWrapper.ConnectionStringSettingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ConnectionStringSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Configuration.ProviderSettingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Configuration.ProviderSettingsCollection) As SpecializedWrapper.ProviderSettingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProviderSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Net.Configuration.AuthenticationModuleElementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Net.Configuration.AuthenticationModuleElementCollection) As SpecializedWrapper.AuthenticationModuleElementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.AuthenticationModuleElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Net.Configuration.BypassElementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Net.Configuration.BypassElementCollection) As SpecializedWrapper.BypassElementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.BypassElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Net.Configuration.ConnectionManagementElementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Net.Configuration.ConnectionManagementElementCollection) As SpecializedWrapper.ConnectionManagementElementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ConnectionManagementElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Net.Configuration.WebrequestModuleElementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Net.Configuration.WebrequestModuleElementCollection) As SpecializedWrapper.WebrequestModuleElementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebrequestModuleElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.AssemblyCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.AssemblyCollection) As SpecializedWrapper.WebAssemblyCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebAssemblyCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.AuthorizationRuleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.AuthorizationRuleCollection) As SpecializedWrapper.WebAuthorizationRuleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebAuthorizationRuleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.BufferModesCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.BufferModesCollection) As SpecializedWrapper.BufferModesCollectionTypeSafeWrapper
            Return New SpecializedWrapper.BufferModesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.BuildProviderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.BuildProviderCollection) As SpecializedWrapper.BuildProviderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.BuildProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.ClientTargetCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.ClientTargetCollection) As SpecializedWrapper.ClientTargetCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ClientTargetCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.CodeSubDirectoriesCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.CodeSubDirectoriesCollection) As SpecializedWrapper.CodeSubDirectoriesCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeSubDirectoriesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.CompilerCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.CompilerCollection) As SpecializedWrapper.WebCompilerCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebCompilerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.CustomErrorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.CustomErrorCollection) As SpecializedWrapper.CustomErrorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CustomErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.EventMappingSettingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.EventMappingSettingsCollection) As SpecializedWrapper.EventMappingSettingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.EventMappingSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.ExpressionBuilderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.ExpressionBuilderCollection) As SpecializedWrapper.ExpressionBuilderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ExpressionBuilderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.FormsAuthenticationUserCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.FormsAuthenticationUserCollection) As SpecializedWrapper.FormsAuthenticationUserCollectionTypeSafeWrapper
            Return New SpecializedWrapper.FormsAuthenticationUserCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.HttpHandlerActionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.HttpHandlerActionCollection) As SpecializedWrapper.HttpHandlerActionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HttpHandlerActionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.HttpModuleActionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.HttpModuleActionCollection) As SpecializedWrapper.HttpModuleActionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HttpModuleActionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.NamespaceCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.NamespaceCollection) As SpecializedWrapper.WebNamespaceCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebNamespaceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.OutputCacheProfileCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.OutputCacheProfileCollection) As SpecializedWrapper.OutputCacheProfileCollectionTypeSafeWrapper
            Return New SpecializedWrapper.OutputCacheProfileCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.ProfileGroupSettingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.ProfileGroupSettingsCollection) As SpecializedWrapper.ProfileGroupSettingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProfileGroupSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.ProfilePropertySettingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.ProfilePropertySettingsCollection) As SpecializedWrapper.ProfilePropertySettingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProfilePropertySettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.ProfileSettingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.ProfileSettingsCollection) As SpecializedWrapper.ProfileSettingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProfileSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.ProtocolCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.ProtocolCollection) As SpecializedWrapper.ProtocolCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProtocolCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.RuleSettingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.RuleSettingsCollection) As SpecializedWrapper.RuleSettingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.RuleSettingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.SqlCacheDependencyDatabaseCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.SqlCacheDependencyDatabaseCollection) As SpecializedWrapper.SqlCacheDependencyDatabaseCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SqlCacheDependencyDatabaseCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.TagMapCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.TagMapCollection) As SpecializedWrapper.TagMapCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TagMapCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.TagPrefixCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.TagPrefixCollection) As SpecializedWrapper.TagPrefixCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TagPrefixCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.TransformerInfoCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.TransformerInfoCollection) As SpecializedWrapper.TransformerInfoCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TransformerInfoCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.TrustLevelCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.TrustLevelCollection) As SpecializedWrapper.TrustLevelCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TrustLevelCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.UrlmappingCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.UrlmappingCollection) As SpecializedWrapper.UrlmappingCollectionTypeSafeWrapper
            Return New SpecializedWrapper.UrlmappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection) As SpecializedWrapper.SchemaImporterExtensionElementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SchemaImporterExtensionElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Configuration.ProtectedConfigurationProviderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Configuration.ProtectedConfigurationProviderCollection) As SpecializedWrapper.ProtectedConfigurationProviderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProtectedConfigurationProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Configuration.SettingsProviderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Configuration.SettingsProviderCollection) As SpecializedWrapper.SettingsProviderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SettingsProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Profile.ProfileProviderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Profile.ProfileProviderCollection) As SpecializedWrapper.ProfileProviderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProfileProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Security.MembershipProviderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Security.MembershipProviderCollection) As SpecializedWrapper.MembershipProviderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.MembershipProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Security.RoleProviderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Security.RoleProviderCollection) As SpecializedWrapper.RoleProviderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.RoleProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.SiteMapProviderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.SiteMapProviderCollection) As SpecializedWrapper.SiteMapProviderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SiteMapProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection) As SpecializedWrapper.PersonalizationProviderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.PersonalizationProviderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.ConstraintCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.ConstraintCollection) As SpecializedWrapper.ConstraintCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ConstraintCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.DataColumnCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.DataColumnCollection) As SpecializedWrapper.DataColumnCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.DataRelationCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.DataRelationCollection) As SpecializedWrapper.DataRelationCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataRelationCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.DataRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.DataRowCollection) As SpecializedWrapper.DataRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.DataTableCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.DataTableCollection) As SpecializedWrapper.DataTableCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataTableCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.AccessControl.GenericAcl"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.AccessControl.GenericAcl) As SpecializedWrapper.GenericAclTypeSafeWrapper
            Return New SpecializedWrapper.GenericAclTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.AccessControl.RawAcl"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.AccessControl.RawAcl) As SpecializedWrapper.RawAclTypeSafeWrapper
            Return New SpecializedWrapper.RawAclTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.ViewCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.ViewCollection) As SpecializedWrapper.WebViewCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebViewCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.BindingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.BindingsCollection) As SpecializedWrapper.BindingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.BindingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ControlBindingsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ControlBindingsCollection) As SpecializedWrapper.ControlBindingsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ControlBindingsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.Configuration.AdapterDictionary"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.Configuration.AdapterDictionary) As SpecializedWrapper.AdapterDictionaryTypeSafeWrapper
            Return New SpecializedWrapper.AdapterDictionaryTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeNamespaceImportCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeNamespaceImportCollection) As SpecializedWrapper.CodeNamespaceImportCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeNamespaceImportCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeAttributeArgumentCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeAttributeArgumentCollection) As SpecializedWrapper.CodeAttributeArgumentCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeAttributeArgumentCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeAttributeDeclarationCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeAttributeDeclarationCollection) As SpecializedWrapper.CodeAttributeDeclarationCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeAttributeDeclarationCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeCatchClauseCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeCatchClauseCollection) As SpecializedWrapper.CodeCatchClauseCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeCatchClauseCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeCommentStatementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeCommentStatementCollection) As SpecializedWrapper.CodeCommentStatementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeCommentStatementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeDirectiveCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeDirectiveCollection) As SpecializedWrapper.CodeDirectiveCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeDirectiveCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeExpressionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeExpressionCollection) As SpecializedWrapper.CodeExpressionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeExpressionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeNamespaceCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeNamespaceCollection) As SpecializedWrapper.CodeNamespaceCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeNamespaceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeParameterDeclarationExpressionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeParameterDeclarationExpressionCollection) As SpecializedWrapper.CodeParameterDeclarationExpressionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeParameterDeclarationExpressionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeStatementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeStatementCollection) As SpecializedWrapper.CodeStatementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeStatementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeTypeDeclarationCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeTypeDeclarationCollection) As SpecializedWrapper.CodeTypeDeclarationCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeTypeDeclarationCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeTypeMemberCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeTypeMemberCollection) As SpecializedWrapper.CodeTypeMemberCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeTypeMemberCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeTypeParameterCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeTypeParameterCollection) As SpecializedWrapper.CodeTypeParameterCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeTypeParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.CodeTypeReferenceCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.CodeTypeReferenceCollection) As SpecializedWrapper.CodeTypeReferenceCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CodeTypeReferenceCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.CodeDom.Compiler.CompilerErrorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.CodeDom.Compiler.CompilerErrorCollection) As SpecializedWrapper.CompilerErrorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CompilerErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.ComponentModel.Design.DesignerVerbCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.ComponentModel.Design.DesignerVerbCollection) As SpecializedWrapper.DesignerVerbCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DesignerVerbCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.SqlClient.SqlBulkCopyColumnMappingCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.SqlClient.SqlBulkCopyColumnMappingCollection) As SpecializedWrapper.SqlBulkCopyColumnMappingCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SqlBulkCopyColumnMappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Diagnostics.CounterCreationDataCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Diagnostics.CounterCreationDataCollection) As SpecializedWrapper.CounterCreationDataCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CounterCreationDataCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Diagnostics.EventLogPermissionEntryCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Diagnostics.EventLogPermissionEntryCollection) As SpecializedWrapper.EventLogPermissionEntryCollectionTypeSafeWrapper
            Return New SpecializedWrapper.EventLogPermissionEntryCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Diagnostics.PerformanceCounterPermissionEntryCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Diagnostics.PerformanceCounterPermissionEntryCollection) As SpecializedWrapper.PerformanceCounterPermissionEntryCollectionTypeSafeWrapper
            Return New SpecializedWrapper.PerformanceCounterPermissionEntryCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.Cryptography.X509Certificates.X509CertificateCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.Cryptography.X509Certificates.X509CertificateCollection) As SpecializedWrapper.X509CertificateCollectionTypeSafeWrapper
            Return New SpecializedWrapper.X509CertificateCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2Collection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Security.Cryptography.X509Certificates.X509Certificate2Collection) As SpecializedWrapper.X509Certificate2CollectionTypeSafeWrapper
            Return New SpecializedWrapper.X509Certificate2CollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.ParserErrorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.ParserErrorCollection) As SpecializedWrapper.ParserErrorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ParserErrorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.EmbeddedMailObjectsCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.EmbeddedMailObjectsCollection) As SpecializedWrapper.EmbeddedMailObjectsCollectionTypeSafeWrapper
            Return New SpecializedWrapper.EmbeddedMailObjectsCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.RoleGroupCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.RoleGroupCollection) As SpecializedWrapper.RoleGroupCollectionTypeSafeWrapper
            Return New SpecializedWrapper.RoleGroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection) As SpecializedWrapper.ProxyWebPartConnectionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ProxyWebPartConnectionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.WebPartConnectionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartConnectionCollection) As SpecializedWrapper.WebPartConnectionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebPartConnectionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection) As SpecializedWrapper.WebPartDisplayModeCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebPartDisplayModeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WebParts.WebPartTransformerCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WebParts.WebPartTransformerCollection) As SpecializedWrapper.WebPartTransformerCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebPartTransformerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Documents.LinkTargetCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Documents.LinkTargetCollection) As SpecializedWrapper.LinkTargetCollectionTypeSafeWrapper
            Return New SpecializedWrapper.LinkTargetCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection) As SpecializedWrapper.SchemaImporterExtensionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SchemaImporterExtensionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Xml.Serialization.XmlAnyElementAttributes"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Xml.Serialization.XmlAnyElementAttributes) As SpecializedWrapper.XmlAnyElementAttributesTypeSafeWrapper
            Return New SpecializedWrapper.XmlAnyElementAttributesTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Xml.Serialization.XmlArrayItemAttributes"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Xml.Serialization.XmlArrayItemAttributes) As SpecializedWrapper.XmlArrayItemAttributesTypeSafeWrapper
            Return New SpecializedWrapper.XmlArrayItemAttributesTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Xml.Serialization.XmlElementAttributes"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Xml.Serialization.XmlElementAttributes) As SpecializedWrapper.XmlElementAttributesTypeSafeWrapper
            Return New SpecializedWrapper.XmlElementAttributesTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Xml.Serialization.XmlSchemas"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Xml.Serialization.XmlSchemas) As SpecializedWrapper.XmlSchemasTypeSafeWrapper
            Return New SpecializedWrapper.XmlSchemasTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Xml.Schema.XmlSchemaObjectCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Xml.Schema.XmlSchemaObjectCollection) As SpecializedWrapper.XmlSchemaObjectCollectionTypeSafeWrapper
            Return New SpecializedWrapper.XmlSchemaObjectCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Collections.Specialized.StringCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Collections.Specialized.StringCollection) As SpecializedWrapper.StringCollectionTypeSafeWrapper
            Return New SpecializedWrapper.StringCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection) As SpecializedWrapper.DesignerOptionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DesignerOptionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.ComponentModel.EventDescriptorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.ComponentModel.EventDescriptorCollection) As SpecializedWrapper.EventDescriptorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.EventDescriptorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.ComponentModel.ListSortDescriptionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.ComponentModel.ListSortDescriptionCollection) As SpecializedWrapper.ListSortDescriptionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ListSortDescriptionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.ComponentModel.PropertyDescriptorCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.ComponentModel.PropertyDescriptorCollection) As SpecializedWrapper.PropertyDescriptorCollectionTypeSafeWrapper
            Return New SpecializedWrapper.PropertyDescriptorCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.Common.DataColumnMappingCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.Common.DataColumnMappingCollection) As SpecializedWrapper.DataColumnMappingCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataColumnMappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.Common.DbParameterCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.Common.DbParameterCollection) As SpecializedWrapper.DbParameterCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DbParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.Odbc.OdbcParameterCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.Odbc.OdbcParameterCollection) As SpecializedWrapper.OdbcParameterCollectionTypeSafeWrapper
            Return New SpecializedWrapper.OdbcParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.OleDb.OleDbParameterCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.OleDb.OleDbParameterCollection) As SpecializedWrapper.OleDbParameterCollectionTypeSafeWrapper
            Return New SpecializedWrapper.OleDbParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.SqlClient.SqlParameterCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.SqlClient.SqlParameterCollection) As SpecializedWrapper.SqlParameterCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SqlParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Data.Common.DataTableMappingCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Data.Common.DataTableMappingCollection) As SpecializedWrapper.DataTableMappingCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataTableMappingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Diagnostics.TraceListenerCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Diagnostics.TraceListenerCollection) As SpecializedWrapper.TraceListenerCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TraceListenerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.SiteMapNodeCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.SiteMapNodeCollection) As SpecializedWrapper.SiteMapNodeCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SiteMapNodeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.DataControlFieldCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.DataControlFieldCollection) As SpecializedWrapper.DataControlFieldCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataControlFieldCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.HotSpotCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.HotSpotCollection) As SpecializedWrapper.HotSpotCollectionTypeSafeWrapper
            Return New SpecializedWrapper.HotSpotCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.MenuItemBindingCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.MenuItemBindingCollection) As SpecializedWrapper.MenuItemBindingCollectionTypeSafeWrapper
            Return New SpecializedWrapper.MenuItemBindingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.MenuItemStyleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.MenuItemStyleCollection) As SpecializedWrapper.MenuItemStyleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.MenuItemStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.ParameterCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.ParameterCollection) As SpecializedWrapper.WebParameterCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebParameterCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.StyleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.StyleCollection) As SpecializedWrapper.WebStyleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.SubMenuStyleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.SubMenuStyleCollection) As SpecializedWrapper.SubMenuStyleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SubMenuStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.TreeNodeBindingCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.TreeNodeBindingCollection) As SpecializedWrapper.TreeNodeBindingCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TreeNodeBindingCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.TreeNodeStyleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.TreeNodeStyleCollection) As SpecializedWrapper.TreeNodeStyleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TreeNodeStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.ListItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.ListItemCollection) As SpecializedWrapper.WebListItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebListItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.TableCellCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.TableCellCollection) As SpecializedWrapper.WebTableCellCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebTableCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.TableRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.TableRowCollection) As SpecializedWrapper.WebTableRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebTableRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Web.UI.WebControls.WizardStepCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Web.UI.WebControls.WizardStepCollection) As SpecializedWrapper.WizardStepCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WizardStepCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Controls.ColumnDefinitionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Controls.ColumnDefinitionCollection) As SpecializedWrapper.WebColumnDefinitionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.WebColumnDefinitionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Controls.RowDefinitionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Controls.RowDefinitionCollection) As SpecializedWrapper.RowDefinitionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.RowDefinitionCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Controls.UIElementCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Controls.UIElementCollection) As SpecializedWrapper.UIElementCollectionTypeSafeWrapper
            Return New SpecializedWrapper.UIElementCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Documents.TableCellCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Documents.TableCellCollection) As SpecializedWrapper.TableCellCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TableCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Documents.TableColumnCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Documents.TableColumnCollection) As SpecializedWrapper.TableColumnCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TableColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Documents.TableRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Documents.TableRowCollection) As SpecializedWrapper.TableRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TableRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Documents.TableRowGroupCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Documents.TableRowGroupCollection) As SpecializedWrapper.TableRowGroupCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TableRowGroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.AutoCompleteStringCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.AutoCompleteStringCollection) As SpecializedWrapper.AutoCompleteStringCollectionTypeSafeWrapper
            Return New SpecializedWrapper.AutoCompleteStringCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.DataGridViewCellCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.DataGridViewCellCollection) As SpecializedWrapper.DataGridViewCellCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataGridViewCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.DataGridViewColumnCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.DataGridViewColumnCollection) As SpecializedWrapper.DataGridViewColumnCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataGridViewColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.DataGridViewRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.DataGridViewRowCollection) As SpecializedWrapper.DataGridViewRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataGridViewRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.DataGridViewSelectedCellCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.DataGridViewSelectedCellCollection) As SpecializedWrapper.DataGridViewSelectedCellCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataGridViewSelectedCellCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.DataGridViewSelectedColumnCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.DataGridViewSelectedColumnCollection) As SpecializedWrapper.DataGridViewSelectedColumnCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataGridViewSelectedColumnCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.DataGridViewSelectedRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.DataGridViewSelectedRowCollection) As SpecializedWrapper.DataGridViewSelectedRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.DataGridViewSelectedRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.GridColumnStylesCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.GridColumnStylesCollection) As SpecializedWrapper.GridColumnStylesCollectionTypeSafeWrapper
            Return New SpecializedWrapper.GridColumnStylesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.GridTableStylesCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.GridTableStylesCollection) As SpecializedWrapper.GridTableStylesCollectionTypeSafeWrapper
            Return New SpecializedWrapper.GridTableStylesCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.CheckedListBox.CheckedIndexCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.CheckedListBox.CheckedIndexCollection) As SpecializedWrapper.ListBoxCheckedIndexCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ListBoxCheckedIndexCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ImageList.ImageCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ImageList.ImageCollection) As SpecializedWrapper.ImageCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ImageCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.Control.ControlCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.Control.ControlCollection) As SpecializedWrapper.ControlCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ControlCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ToolStripItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ToolStripItemCollection) As SpecializedWrapper.ToolStripItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ToolStripItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection) As SpecializedWrapper.ToolStripPanelRowCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ToolStripPanelRowCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.LinkLabel.LinkCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.LinkLabel.LinkCollection) As SpecializedWrapper.LinkCollectionTypeSafeWrapper
            Return New SpecializedWrapper.LinkCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListBox.IntegerCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListBox.IntegerCollection) As SpecializedWrapper.IntegerCollectionTypeSafeWrapper
            Return New SpecializedWrapper.IntegerCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListBox.SelectedIndexCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListBox.SelectedIndexCollection) As SpecializedWrapper.ListBoxSelectedIndexCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ListBoxSelectedIndexCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListView.ColumnHeaderCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListView.ColumnHeaderCollection) As SpecializedWrapper.ColumnHeaderCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ColumnHeaderCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListView.CheckedIndexCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListView.CheckedIndexCollection) As SpecializedWrapper.ListViewCheckedIndexCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ListViewCheckedIndexCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListView.CheckedListViewItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListView.CheckedListViewItemCollection) As SpecializedWrapper.CheckedListViewItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.CheckedListViewItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListView.ListViewItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListView.ListViewItemCollection) As SpecializedWrapper.ListViewItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ListViewItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListView.SelectedIndexCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListView.SelectedIndexCollection) As SpecializedWrapper.SelectedIndexCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SelectedIndexCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListView.SelectedListViewItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListView.SelectedListViewItemCollection) As SpecializedWrapper.SelectedListViewItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.SelectedListViewItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListViewGroupCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListViewGroupCollection) As SpecializedWrapper.ListViewGroupCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ListViewGroupCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ListViewItem.ListViewSubItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ListViewItem.ListViewSubItemCollection) As SpecializedWrapper.ListViewSubItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ListViewSubItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.Menu.MenuItemCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.Menu.MenuItemCollection) As SpecializedWrapper.MenuItemCollectionTypeSafeWrapper
            Return New SpecializedWrapper.MenuItemCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.StatusBar.StatusBarPanelCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.StatusBar.StatusBarPanelCollection) As SpecializedWrapper.StatusBarPanelCollectionTypeSafeWrapper
            Return New SpecializedWrapper.StatusBarPanelCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.TabControl.TabPageCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.TabControl.TabPageCollection) As SpecializedWrapper.TabPageCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TabPageCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.TableLayoutStyleCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.TableLayoutStyleCollection) As SpecializedWrapper.TableLayoutStyleCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TableLayoutStyleCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.ToolBar.ToolBarButtonCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.ToolBar.ToolBarButtonCollection) As SpecializedWrapper.ToolBarButtonCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ToolBarButtonCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Forms.TreeNodeCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Forms.TreeNodeCollection) As SpecializedWrapper.TreeNodeCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TreeNodeCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.Media.Animation.ThicknessKeyFrameCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.Media.Animation.ThicknessKeyFrameCollection) As SpecializedWrapper.ThicknessKeyFrameCollectionTypeSafeWrapper
            Return New SpecializedWrapper.ThicknessKeyFrameCollectionTypeSafeWrapper(Collection)
        End Function
        ''' <summary>Gets type-safe wrapper of <see cref="System.Windows.TriggerActionCollection"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Public Function AsTypeSafe(ByVal Collection As System.Windows.TriggerActionCollection) As SpecializedWrapper.TriggerActionCollectionTypeSafeWrapper
            Return New SpecializedWrapper.TriggerActionCollectionTypeSafeWrapper(Collection)
        End Function
    End Module
End Namespace
#End If