''' <summary>A control to host single screen saver component</summary>
''' <remarks>Use this control in layout templates to indicate place where to place saver part</remarks>
Public Class SSaverHost
    Inherits Control

    ''' <summary>Gets or sets instance of saver part hosted in this host</summary>
    Public Property HostedSaver As SaverBase
        Get
            Return LogicalTreeHelper.GetChildren(Me).OfType(Of SaverBase).SingleOrDefault
        End Get
        Set(value As SaverBase)
            While LogicalTreeHelper.GetChildren(Me).Cast(Of Object).Any
                Me.RemoveLogicalChild(LogicalTreeHelper.GetChildren(Me).Cast(Of Object).First)
            End While
            If value IsNot Nothing Then Me.AddLogicalChild(value)
        End Set
    End Property
End Class
