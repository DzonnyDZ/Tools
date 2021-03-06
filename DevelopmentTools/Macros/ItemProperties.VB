Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports System.Diagnostics
Imports System.Collections.Generic
''' <summary>Contains macro method for working with item properties</summary>
Public Module ItemProperties
    ''' <summary>Edits LogicalName property of selected project items using input box</summary>
    ''' <version version="1.5.2">Fixed: Uses "LocalName" instead of "LogicalName"</version>
    ''' <version version="1.5.2">Renamed from SetLocalName to set LogicalName</version>
    Public Sub SetLogicalName()
        SetProperty("LogicalName")
    End Sub
    ''' <summary>Removes property LogicalName from all selected project items (after user confirms such action)</summary>
    ''' <version version="1.5.2">Fixed: Uses "LocalName" instead of "LogicalName"</version>
    ''' <version version="1.5.2">Renamed from UnsetLocalName to UnsetLogicalName</version>
    Public Sub UnsetLogicalName()
        UnsetProperty("LogicalName")
    End Sub

    ''' <summary>Shows dialog to edit all the properties supported by <see cref="ItemPropertiesProxy"/></summary>
    Public Sub EditProperties()
        Dim Items As New List(Of ItemPropertiesProxy)
        For i = 1 To DTE.SelectedItems.Count
            Items.Add(New ItemPropertiesProxy(GetPropertyStorage(DTE.SelectedItems.Item(i).ProjectItem)))
        Next
        Dim frm As New frmItemProperties(Items.ToArray)
        frm.ShowDialog()
    End Sub
End Module
