Imports System.ComponentModel, Tools.ComponentModelT
Imports System.Windows.Forms

''' <summary>Dva textboy a dva labely</summary>
Public Class DiTextBox
    ''' <summary>První <see cref="TextBox"/></summary>
    <Description("První  TextBox")> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property TextBox1() As TextBox
        Get
            Return _TextBox1
        End Get
    End Property
    ''' <summary>Druhý <see cref="TextBox"/></summary>
    <Description("Druhý  TextBox")> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property TextBox2() As TextBox
        Get
            Return _TextBox2
        End Get
    End Property
    ''' <summary>První <see cref="Label"/></summary>
    <Description("První  Label")> _
     <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property Label1() As Label
        Get
            Return _Label1
        End Get
    End Property
    ''' <summary>Druhý <see cref="Label"/></summary>
    <Description("Druhý  Label")> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property Label2() As Label
        Get
            Return _Label2
        End Get
    End Property
End Class
