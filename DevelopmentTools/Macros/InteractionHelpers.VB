Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports System.Diagnostics
Imports Microsoft.VisualStudio.Shell.Interop
Imports System.Xml
Imports Microsoft.VisualStudio.Shell
Imports System.Windows.Forms
Imports Microsoft.VisualStudio.VSConstants
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports System.ComponentModel


''' <summary>Contains methods for converting DTE to VSIP objects and vice versa</summary>
''' <remarks>See <a href="http://www.clariusconsulting.net/blogs/kzu/archive/2006/01/06/DteToVSIP.aspx">http://www.clariusconsulting.net/blogs/kzu/archive/2006/01/06/DteToVSIP.aspx</a></remarks>
Public Module VsHelper
    ''' <summary>Gets current <see cref="IVsHierarchy"/> from <see cref="IServiceProvider"/></summary>
    ''' <param name="provider">An <see cref="IServiceProvider"/></param>
    ''' <returns>Current hierarchy</returns>
    ''' <exception cref="InvalidOperationException">DTE not found</exception>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    <CLSCompliant(False)>
    Public Function GetCurrentHierarchy(ByVal provider As IServiceProvider) As IVsHierarchy
        Dim vs As DTE = CType(provider.GetService(GetType(DTE)), DTE)
        If vs Is Nothing Then
            Throw New InvalidOperationException("DTE not found.")
        End If
        Return ToHierarchy(vs.SelectedItems.Item(1).ProjectItem.ContainingProject)
    End Function
    ''' <summary>Converts <see cref="EnvDTE.Project"/> to <see cref="IVsHierarchy"/></summary>
    ''' <param name="project">A <see cref="EnvDTE.Project"/></param>
    ''' <returns>A <see cref="IVsHierarchy"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="project"/> is null</exception>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    <CLSCompliant(False)>
    Public Function ToHierarchy(ByVal project As EnvDTE.Project) As IVsHierarchy
        If project Is Nothing Then Throw New ArgumentNullException("project")
        Dim projectGuid As String = Nothing
        ' DTE does not expose the project GUID that exists at in the msbuild project file.
        ' Cannot use MSBuild object model because it uses a static instance of the Engine, 
        ' and using the Project will cause it to be unloaded from the engine when the 
        ' GC collects the variable that we declare.
        Using projectReader As XmlReader = XmlReader.Create(project.FileName)
            projectReader.MoveToContent()
            Dim nodeName As Object = projectReader.NameTable.Add("ProjectGuid")
            While projectReader.Read()
                If Object.Equals(projectReader.LocalName, nodeName) Then
                    projectGuid = projectReader.ReadElementContentAsString()
                    Exit While
                End If
            End While
        End Using
        Dim serviceProvider As IServiceProvider = New ServiceProvider(DirectCast(project.DTE, Microsoft.VisualStudio.OLE.Interop.IServiceProvider))
        Return VsShellUtilities.GetHierarchy(serviceProvider, New Guid(projectGuid))
    End Function

    ''' <summary>Converts <see cref="EnvDTE.Project"/> to <see cref="IVsProject3"/></summary>
    ''' <param name="project">A <see cref="EnvDTE.Project"/></param>
    ''' <returns>A <see cref="IVsProject3"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="project"/> is null</exception>
    ''' <exception cref="ArgumentException">Item obtained is not a project</exception>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    <CLSCompliant(False)>
    Public Function ToVsProject(ByVal project As EnvDTE.Project) As IVsProject3
        If project Is Nothing Then Throw New ArgumentNullException("project")
        Dim vsProject As IVsProject3 = TryCast(ToHierarchy(project), IVsProject3)
        If vsProject Is Nothing Then Throw New ArgumentException("Project is not a VS project.")
        Return vsProject
    End Function
    ''' <summary>Converts <see cref="IVsHierarchy"/> to <see cref="EnvDTE.Project"/></summary>
    ''' <param name="hierarchy">A <see cref="IVsHierarchy"/></param>
    ''' <returns>A <see cref="EnvDTE.Project"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="hierarchy"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="hierarchy"/> is not project</exception>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    <CLSCompliant(False)>
    Public Function ToDteProject(ByVal hierarchy As IVsHierarchy) As EnvDTE.Project
        If hierarchy Is Nothing Then Throw New ArgumentNullException("hierarchy")
        Dim prjObject As Object = Nothing
        If hierarchy.GetProperty(&HFFFFFFFEUI, -2027, prjObject) >= 0 Then
            Return CType(prjObject, EnvDTE.Project)
        Else
            Throw New ArgumentException("Hierarchy is not a project.")
        End If
    End Function

    ''' <summary>Converts <see cref="IVsProject"/> to <see cref="EnvDTE.Project"/></summary>
    ''' <param name="project">A <see cref="IVsProject"/></param>
    ''' <returns>A <see cref="EnvDTE.Project"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="project"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="project"/> is not project</exception>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    <CLSCompliant(False)>
    Public Function ToDteProject(ByVal project As IVsProject) As EnvDTE.Project
        If project Is Nothing Then Throw New ArgumentNullException("project")
        Return ToDteProject(TryCast(project, IVsHierarchy))
    End Function

End Module


''' <summary>Wraps <see cref="IVsBuildPropertyStorage"/> </summary>
Public NotInheritable Class BuildStorageWrapper
    ''' <summary>Internal <see cref="IVsBuildPropertyStorage"/></summary>
    Private Storage As IVsBuildPropertyStorage
    ''' <summary>ID of item being edited by this instance</summary>
    Private itemId As UInt32
    ''' <summary>CTor</summary>
    ''' <param name="Storage"><see cref="IVsBuildPropertyStorage"/> to wrap</param>
    ''' <param name="ItemId">Id of item to be edited by this instace</param>
    ''' <exception cref="ArgumentNullException"><paramraf name="Storage"/> is null</exception>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    <CLSCompliant(False)>
    Public Sub New(ByVal Storage As IVsBuildPropertyStorage, ByVal ItemId As UInt32)
        If Storage Is Nothing Then Throw New ArgumentNullException("Storage")
        Me.Storage = Storage
        Me.itemId = ItemId
    End Sub
    ''' <summary>Gets or sets value of given attribute</summary>
    ''' <param name="Name">Name of attribute to get or set value of</param>
    ''' <value>New value of attribute. An empty string to remove the attribute</value>
    ''' <returns>Current value of attribute. Null when attribute is not present or obtaining fails</returns>
    ''' <exception cref="ArgumentNullException">Value being set is null or <paramref name="Name"/> is null</exception>
    ''' <exception cref="COMException">Setting of attribute failed</exception>
    ''' <seelaso cref="IVsBuildPropertyStorage.GetItemAttribute"/>
    ''' <seelaso cref="IVsBuildPropertyStorage.SetItemAttribute"/>
    Default Public Property Attributes$(ByVal Name$)
        Get
            If Name Is Nothing Then Throw New ArgumentNullException("Name")
            Dim value$ = Nothing
            Dim rc = Storage.GetItemAttribute(itemId, Name, value)
            If rc <> S_OK Then Return Nothing 'Throw New ComponentModel.Win32Exception(rc)
            Return value
        End Get
        Set(ByVal value$)
            If Name Is Nothing Then Throw New ArgumentNullException("Name")
            If value Is Nothing Then Throw New ArgumentNullException("value")
            Dim rc = Storage.SetItemAttribute(itemId, Name, value)
            If rc <> S_OK Then Throw New COMException("Attribute setting failed", rc)
        End Set
    End Property
End Class

''' <summary>Dialog for changin item attributes</summary>
Friend Class frmItemProperties
    Inherits Form
    ''' <summary><see cref="PropertyGrid"/> to show properties</summary>
    Private WithEvents prg As New PropertyGrid
    ''' <summary>CTor</summary>
    ''' <param name="objects">Project items to edit properties of</param>
    Public Sub New(ByVal ParamArray objects As ItemPropertiesProxy())
        prg.Dock = DockStyle.Fill
        prg.SelectedObjects = objects
        Me.Controls.Add(prg)
    End Sub
End Class
''' <summary>Proxy of <see cref="BuildStorageWrapper"/> for <see cref="PropertyGrid"/></summary>
Friend Class ItemPropertiesProxy
    ''' <summary>CTor</summary>
    ''' <param name="Item">Items to be edited by this instace</param>
    ''' <exception cref="ArgumentNullException"><paramref name="Item"/> is null</exception>
    Public Sub New(ByVal Item As BuildStorageWrapper)
        If Item Is Nothing Then Throw New ArgumentNullException
        Me.ps = Item
    End Sub
    ''' <summary>Provdes properties for this instace</summary>
    Dim ps As BuildStorageWrapper
#Region "Common"
    ''' <summary>Gets or sets value of property SubType</summary>
    <Category("Common")> _
    Public Property SubType$()
        Get
            Return ps.Attributes("SubType")
        End Get
        Set(ByVal value$)
            ps.Attributes("SubType") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property DependentUpon</summary>
    <Category("Common")> _
    Public Property DependentUpon$()
        Get
            Return ps.Attributes("DependentUpon")
        End Get
        Set(ByVal value$)
            ps.Attributes("DependentUpon") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property Link</summary>
    <Category("Common")> _
    Public Property Link$()
        Get
            Return ps.Attributes("Link")
        End Get
        Set(ByVal value$)
            ps.Attributes("Link") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property Visible</summary>
    <Category("Common")> _
    Public Property Visible() As Nullable(Of Boolean)
        Get
            Select Case ps.Attributes("Visible")
                Case "true", "True" : Return True
                Case "false", "False" : Return False
                Case Else : Return Nothing
            End Select
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            If value.HasValue AndAlso value Then
                ps.Attributes("Visible") = "True"
            ElseIf value.HasValue Then
                ps.Attributes("Visible") = "False"
            Else
                ps.Attributes("Visible") = ""
            End If
        End Set
    End Property
    ''' <summary>Gets or sets value of property CopyToOutputDirectory</summary>
    <Category("Common")> _
    Public Property CopyToOutputDirectory$()
        Get
            Return ps.Attributes("CopyToOutputDirectory")
        End Get
        Set(ByVal value$)
            ps.Attributes("CopyToOutputDirectory") = value
        End Set
    End Property
#End Region
#Region "Non-code"
    ''' <summary>Gets or sets value of property LastGenOutput</summary>
    <Category("Non-code")> _
    Public Property LastGenOutput$()
        Get
            Return ps.Attributes("LastGenOutput")
        End Get
        Set(ByVal value$)
            ps.Attributes("LastGenOutput") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property CustomToolNamespace</summary>
    <Category("Non-code")> _
    Public Property CustomToolNamespace$()
        Get
            Return ps.Attributes("CustomToolNamespace")
        End Get
        Set(ByVal value$)
            ps.Attributes("CustomToolNamespace") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property Generator</summary>
    <Category("Non-code")> _
    Public Property Generator$()
        Get
            Return ps.Attributes("Generator")
        End Get
        Set(ByVal value$)
            ps.Attributes("Generator") = value
        End Set
    End Property
#End Region
#Region "Code"
    ''' <summary>Gets or sets value of property AutoGen</summary>
    <Category("Code")> _
    Public Property AutoGen$()
        Get
            Return ps.Attributes("AutoGen")
        End Get
        Set(ByVal value$)
            ps.Attributes("AutoGen") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property DesignTime</summary>
    <Category("Code")> _
    Public Property DesignTime$()
        Get
            Return ps.Attributes("DesignTime")
        End Get
        Set(ByVal value$)
            ps.Attributes("DesignTime") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property DesignTimeSharedInput</summary>
    <Category("Code")> _
    Public Property DesignTimeSharedInput$()
        Get
            Return ps.Attributes("DesignTimeSharedInput")
        End Get
        Set(ByVal value$)
            ps.Attributes("DesignTimeSharedInput") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property VBMyExtensionTemplateID</summary>
    <Category("Code")> _
    Public Property VBMyExtensionTemplateID$()
        Get
            Return ps.Attributes("VBMyExtensionTemplateID")
        End Get
        Set(ByVal value$)
            ps.Attributes("VBMyExtensionTemplateID") = value
        End Set
    End Property
#End Region
#Region "Embdeded resources"
    ''' <summary>Gets or sets value of property LogicalName</summary>
    <Category("Embdeded resources")> _
    Public Property LogicalName$()
        Get
            Return ps.Attributes("LogicalName")
        End Get
        Set(ByVal value$)
            ps.Attributes("LogicalName") = value
        End Set
    End Property
#End Region
#Region "Content"
    ''' <summary>Gets or sets value of property Group</summary>
    <Category("Group")> _
    Public Property Group$()
        Get
            Return ps.Attributes("Group")
        End Get
        Set(ByVal value$)
            ps.Attributes("Group") = value
        End Set
    End Property
    ''' <summary>Gets or sets value of property IsAssembly</summary>
    <Category("IsAssembly")> _
    Public Property IsAssembly() As Nullable(Of Boolean)
        Get
            Select Case ps.Attributes("IsAssembly")
                Case "true", "True" : Return True
                Case "false", "False" : Return False
                Case Else : Return Nothing
            End Select
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            If value.HasValue AndAlso value Then
                ps.Attributes("IsAssembly") = "True"
            ElseIf value.HasValue Then
                ps.Attributes("IsAssembly") = "False"
            Else
                ps.Attributes("IsAssembly") = ""
            End If
        End Set
    End Property
    ''' <summary>Gets or sets value of property PublishState</summary>
    <Category("PublishState")> _
    Public Property PublishState$()
        Get
            Return ps.Attributes("PublishState")
        End Get
        Set(ByVal value$)
            ps.Attributes("PublishState") = value
        End Set
    End Property
#End Region




End Class
