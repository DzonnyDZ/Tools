Imports System.ComponentModel, System.Windows.Forms.Design
Imports System.Windows.Forms
Imports System.Drawing

<ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)> _
Public Class ToolStripCheckBox
    Inherits ToolStripControlHost
    Public Sub New()
        MyBase.New(GetCheckBox)
    End Sub
    Private Shared Function GetCheckBox() As CheckBox
        GetCheckBox = New CheckBox
        GetCheckBox.BackColor = Color.Transparent
    End Function
    '<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    <Browsable(True)> _
    Public ReadOnly Property CheckBox() As CheckBox
        Get
            Return MyBase.Control
        End Get
    End Property

    'Public Property Checked() As Boolean
    '    Get
    '        Return CheckBox.Checked
    '    End Get
    '    Set(ByVal value As Boolean)
    '        CheckBox.Checked = value
    '    End Set
    'End Property
    'Public Property ChcekState() As CheckState
    '    Get
    '        Return CheckBox.CheckState
    '    End Get
    '    Set(ByVal value As CheckState)
    '        CheckBox.CheckState = value
    '    End Set
    'End Property

End Class
