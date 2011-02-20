Imports System.ComponentModel
Imports System.Xml.Serialization
Imports Tools.ComponentModelT

Namespace ReportingT.ReportingEngineLite
    ''' <summary>Settings of <see cref="XMLTemplate"/></summary>
    <System.ComponentModel.TypeDescriptionProvider(GetType(SingleTypeDescriptionProvider(Of XmlTemplateSettings, XmlTemplateSettings.XmlTemplateSettingsTypeDescriptor)))> _
    Partial Class XmlTemplateSettings
        Implements ICloneable
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function Clone() As XmlTemplateSettings
            Dim s As New Xml.Serialization.XmlSerializer(Me.GetType)
            Dim str As New IO.MemoryStream
            s.Serialize(str, Me)
            str.Position = 0
            Return s.Deserialize(str)
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function

        ''' <summary>Called as last statement of class constructor</summary>
        Protected Overridable Sub OnAfterInit()
            Me.filterField = My.Resources.filter_XML
        End Sub

#Region "TypeDescriptor"
        ''' <summary>Implements <see cref="TypeDescriptor"/> for <see cref="SimpleXlsSettings"/></summary>
        Friend Class XmlTemplateSettingsTypeDescriptor
            Inherits CustomTypeDescriptor
            Private ReadOnly instance As XmlTemplateSettings
            ''' <summary>CTor - creates a new instance of the <see cref="XMLTemplateSettingsTypeDescriptor"/> class</summary>
            Public Sub New(ByVal instance As XmlTemplateSettings, ByVal Parent As ICustomTypeDescriptor)
                MyBase.New(Parent)
                Me.instance = instance
            End Sub
            ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
            ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
            Public Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
                Return ChangeProperties(MyBase.GetProperties())
            End Function
            ''' <summary>Alters property attributes</summary>
            ''' <param name="properties">Properties to change attributes of</param>
            ''' <returns><paramref name="properties"/> with attributes changed</returns>
            Private Function ChangeProperties(ByVal properties As PropertyDescriptorCollection) As PropertyDescriptorCollection
                Dim ret As New List(Of PropertyDescriptor)
                For Each prp As PropertyDescriptor In properties
                    Select Case prp.Name
                        Case "EnableDocumentFunction"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_AllowDocumentFunction),
                                New DescriptionAttribute(My.Resources.d_AllowDocumentFunction),
                                New CategoryAttribute(My.Resources.cat_Xml)}))
                        Case "EnableScript"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_AllowScript),
                                New DescriptionAttribute(My.Resources.d_AllowScript),
                                New CategoryAttribute(My.Resources.cat_Xml)}))
                        Case "DataSetName"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_DatasetName),
                                New DescriptionAttribute(My.Resources.d_DataSetName),
                                New CategoryAttribute(My.Resources.cat_Dataset)}))
                        Case "DataSetNamespace"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_DatasetNamespace),
                                New DescriptionAttribute(My.Resources.d_DatasetNamespace),
                                New CategoryAttribute(My.Resources.cat_Dataset)}))
                        Case "TableName"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_datasetTableName),
                                New DescriptionAttribute(My.Resources.d_datasetTableName),
                                New CategoryAttribute(My.Resources.cat_Dataset)}))
                        Case "DatasetNamespacePrefix"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_XmlPrefix),
                                New DescriptionAttribute(My.Resources.d_XmlPrefix),
                                New CategoryAttribute(My.Resources.cat_Xml)}))
                        Case Else : ret.Add(prp)
                    End Select
                Next
                Return New PropertyDescriptorCollection(ret.ToArray)
            End Function
            ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
            ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
            Public Overrides Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
                Return ChangeProperties(MyBase.GetProperties(attributes))
            End Function
            ''' <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
            ''' <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
            Public Overrides Function GetAttributes() As System.ComponentModel.AttributeCollection
                Dim ret As New List(Of Attribute)
                For Each attr As Attribute In MyBase.GetAttributes()
                    ret.Add(attr)
                Next
                ret.Add(New DefaultPropertyAttribute("Name"))
                Return New AttributeCollection(ret.ToArray)
            End Function
        End Class
#End Region
    End Class
End Namespace