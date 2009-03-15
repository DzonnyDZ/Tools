Imports System.Drawing
''' <summary>Abstract interface for items that can populate <see cref="DayViewItem"/></summary>
Public Interface IDayViewDataItem
    ''' <summary>Specifies day when item starts and begin</summary>
    ReadOnly Property [Date]() As Date
    ''' <summary>Start time of item within <see cref="Day"/></summary>
    ReadOnly Property StartTime() As TimeSpan
    ''' <summary>End time of item within <see cref="Day"/></summary>
    ReadOnly Property EndTime() As TimeSpan
    ''' <summary>Gets value indicating if <see cref="Enabled"/> is implemented</summary>
    ReadOnly Property EnabledImplemented() As Boolean
    ''' <summary>If implemented true if item is enabled (user can manipulate with it)</summary>
    ''' <exception cref="NotImplementedException"><see cref="EnabledImplemented"/> is false</exception>
    ReadOnly Property Enabled() As Boolean
    ''' <summary>Gets string representing the item. Used for displaying text of the item</summary>
    Function ToString$()
    ''' <summary>If implemented gets value indicating if <see cref="ForeColor"/> is implemented</summary>
    ReadOnly Property ForeColorImplemented() As Boolean
    ''' <summary>Gets value indicating if <see cref="BackColor"/> is implemented</summary>
    ReadOnly Property BackColorImplemented() As Boolean
    ''' <summary>If implemented gets fore color for rendering the item</summary>
    ''' <exception cref="NotImplementedException"><see cref="ForeColorImplemented"/> is false</exception>
    ReadOnly Property ForeColor() As Color
    ''' <summary>Gets back color for rendering the item</summary>
    ''' <exception cref="NotImplementedException"><see cref="ForeColorImplemented"/> is false</exception>''' <exception cref="NotImplementedException"><see cref="BackColorImplemented"/> is false</exception>
    ReadOnly Property BackColor() As Color
    ''' <summary>Gets value indicating if the <see cref="ID"/> property is implemented</summary>
    ReadOnly Property IDImplemented() As Boolean
    ''' <summary>If implemented gets ID of this data item</summary>
    ''' <exception cref="NotImplementedException"><see cref="IDImplemented"/> is false</exception>
    ReadOnly Property ID() As Integer
    ''' <summary>Gets value indicating it this instance is data item itself or wrapper only</summary>
    ''' <returns>True if this instance is not item itself but wrapper only. In shuch case item itseld can be obtained via the <see cref="Item"/> property</returns>
    ReadOnly Property IsWrapper() As Boolean
    ''' <summary>If <see cref="IsWrapper"/> is true returns item itself</summary>
    ''' <exception cref="NotImplementedException"><see cref="IsWrapper"/> is false</exception>
    ReadOnly Property Item() As Object
End Interface

''' <summary>Provides basic abstract class implementing <see cref="IDayViewDataItem"/></summary>
Public MustInherit Class DayViewDataItemBase : Implements IDayViewDataItem
    ''' <summary>Gets back color for rendering the item</summary>
    ''' <exception cref="NotImplementedException"><see cref="ForeColorImplemented"/> is false (always for this implementation)</exception>
    Public Overridable ReadOnly Property BackColor() As System.Drawing.Color Implements IDayViewDataItem.BackColor
        Get
            Throw New NotImplementedException("BackColor is not implemented")
        End Get
    End Property

    ''' <summary>Gets value indicating if <see cref="BackColor"/> is implemented</summary>    
    ''' <returns>This implementation always returns false</returns>
    Public Overridable ReadOnly Property BackColorImplemented() As Boolean Implements IDayViewDataItem.BackColorImplemented
        Get
            Return False
        End Get
    End Property

    ''' <summary>Specifies day when item starts and begin</summary>    
    Public MustOverride ReadOnly Property [Date]() As Date Implements IDayViewDataItem.Date

    ''' <summary>If implemented true if item is enabled (user can manipulate with it)</summary>
    ''' <exception cref="NotImplementedException"><see cref="EnabledImplemented"/> is false (always for this implementation)</exception>
    Public Overridable ReadOnly Property Enabled() As Boolean Implements IDayViewDataItem.Enabled
        Get
            Throw New NotImplementedException("Enabled  is not implemented")
        End Get
    End Property


    ''' <summary>Gets value indicating if <see cref="Enabled"/> is implemented</summary>    
    ''' <returns>This implementation always returns false</returns>
    Public Overridable ReadOnly Property EnabledImplemented() As Boolean Implements IDayViewDataItem.EnabledImplemented
        Get
            Return False
        End Get
    End Property

    ''' <summary>End time of item within <see cref="Day"/></summary>    
    Public MustOverride ReadOnly Property EndTime() As System.TimeSpan Implements IDayViewDataItem.EndTime


    ''' <summary>If implemented gets fore color for rendering the item</summary>
    ''' <exception cref="NotImplementedException"><see cref="ForeColorImplemented"/> is false (always for this implementation)</exception>
    Public Overridable ReadOnly Property ForeColor() As System.Drawing.Color Implements IDayViewDataItem.ForeColor
        Get
            Throw New NotImplementedException("ForeColor is not implemented")
        End Get
    End Property

    ''' <summary>If implemented gets value indicating if <see cref="ForeColor"/> is implemented</summary>    
    ''' <returns>This implementation always returns false</returns>
    Public Overridable ReadOnly Property ForeColorImplemented() As Boolean Implements IDayViewDataItem.ForeColorImplemented
        Get
            Return False
        End Get
    End Property

    ''' <summary>If implemented gets ID of this data item</summary>
    ''' <exception cref="NotImplementedException"><see cref="IDImplemented"/> is false (always for this implementation)</exception>
    Public Overridable ReadOnly Property ID() As Integer Implements IDayViewDataItem.ID
        Get
            Throw New NotImplementedException("ID is not implemented")
        End Get
    End Property

    ''' <summary>Gets value indicating if the <see cref="ID"/> property is implemented</summary>    
    ''' <returns>This implementation always returns false</returns>
    Public Overridable ReadOnly Property IDImplemented() As Boolean Implements IDayViewDataItem.IDImplemented
        Get
            Return False
        End Get
    End Property

    ''' <summary>Gets value indicating it this instance is data item itself or wrapper only</summary>
    ''' <returns>True if this instance is not item itself but wrapper only. In shuch case item itseld can be obtained via the <see cref="Item"/> property. This implementation always returns false</returns>
    Public Overridable ReadOnly Property IsWrapper() As Boolean Implements IDayViewDataItem.IsWrapper
        Get
            Return False
        End Get
    End Property

    ''' <summary>If <see cref="IsWrapper"/> is true returns item itself</summary>
    ''' <exception cref="NotImplementedException"><see cref="IsWrapper"/> is false (always for this implementation)</exception>
    Public Overridable ReadOnly Property Item() As Object Implements IDayViewDataItem.Item
        Get
            Throw New NotImplementedException("Item is not implemented")
        End Get
    End Property

    ''' <summary>Start time of item within <see cref="Day"/></summary>    
    Public MustOverride ReadOnly Property StartTime() As System.TimeSpan Implements IDayViewDataItem.StartTime

    ''' <summary>Gets string representing the item. Used for displaying text of the item</summary>    
    Public MustOverride Overrides Function ToString() As String Implements IDayViewDataItem.ToString
End Class
