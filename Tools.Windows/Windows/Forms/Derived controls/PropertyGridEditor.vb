Imports System.ComponentModel
Imports System.Windows.Forms
Imports Tools.ComponentModelT

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.FormsT
    ''' <summary>Strongly-typed <see cref="PropertyGrid"/></summary>
    ''' <typeparam name="T">Type of object being edited</typeparam>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    <DefaultProperty("SelectedObject")>
    Public Class PropertyGridEditor(Of T)
        Inherits PropertyGrid
        ''' <summary>Help provider</summary>
        Private WithEvents hlpHelp As System.Windows.Forms.HelpProvider
        ''' <summary>Default CTor - creates a new instance of the <see cref="PropertyGridEditor(Of T)"/> class</summary>
        Public Sub New()
            InitializeComponent()
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="PropertyGridEditor(Of T)"/> class and sets object to be edited</summary>
        ''' <param name="SelectedObject">And object to be edited by newly created instance</param>
        Public Sub New(ByVal selectedObject As T)
            Me.New()
            Me.SelectedObject = selectedObject
        End Sub
        ''' <summary>Gets or sets the object for which the grid displays properties.</summary>
        ''' <returns>The first object in the object list. If there is no currently selected object the return is null.</returns>
        Public Shadows Property SelectedObject() As T
            Get
                Return MyBase.SelectedObject
            End Get
            Set(ByVal value As T)
                MyBase.SelectedObject = value
            End Set
        End Property

        ''' <summary>Gets or sets the currently selected objects.</summary>
        ''' <returns>An array of type System.Object. The default is an empty array.</returns>
        ''' <exception cref="System.ArgumentException">One of the items in the array of objects had a null value</exception>
        Public Shadows Property SelectedObjects() As T()
            Get
                If MyBase.SelectedObjects Is Nothing Then Return Nothing
                Dim ret(MyBase.SelectedObjects.Length - 1) As T
                Array.Copy(MyBase.SelectedObjects, ret, ret.Length)
                Return ret
            End Get
            Set(ByVal value As T())
                If value Is Nothing Then
                    MyBase.SelectedObjects = Nothing
                ElseIf value.Length = 0 Then
                    MyBase.SelectedObjects = New Object() {}
                Else
                    Dim NewArr(value.Length - 1) As Object
                    Array.Copy(value, NewArr, value.Length)
                    MyBase.SelectedObjects = NewArr
                End If
            End Set
        End Property

#Region "Help"
        ''' <summary>Sets <see cref="HelpNamespace"/> and <see cref="HelpKeyword"/> at once</summary>
        ''' <param name="helpNamespace">New value of the <see cref="HelpNamespace"/> property</param>
        ''' <param name="helpKeyword">New value of the <see cref="HelpKeyword"/> property</param>
        ''' <remarks>This method sets <see cref="ShowHelp"/> to true and <see cref="HelpNavigator"/> to <see cref="HelpNavigator.Topic"/></remarks>
        Public Sub SetHelp(ByVal helpNamespace$, ByVal helpKeyword$)
            Me.ShowHelp = True
            Me.HelpNavigator = Windows.Forms.HelpNavigator.Topic
            Me.HelpNamespace = helpNamespace
            Me.HelpKeyword = helpKeyword
        End Sub

        ''' <summary>Gets or sets a value specifying the name of the Help file associated with this <see cref="System.Windows.Forms.HelpProvider"/> object.</summary>
        ''' <returns>The name of the Help file. This can be of the form C:\path\sample.chm or /folder/file.htm.</returns>
        <DefaultValue(GetType(String), Nothing)>
        <LDescription(GetType(DerivedControls), "HelpNamespace_d")>
        <LCategory(GetType(DerivedControls), "Help_c", "Help")>
        Public Property HelpNamespace$()
            Get
                Return hlpHelp.HelpNamespace
            End Get
            Set(ByVal value$)
                hlpHelp.HelpNamespace = value
            End Set
        End Property
        ''' <summary>The Help keyword for the control.</summary>
        ''' <returns>The Help keyword associated with this control, or null if the <see cref="System.Windows.Forms.HelpProvider"/> is currently configured to display the entire Help file or is configured to provide a Help string.</returns>
        ''' <value>The Help keyword to associate with the control.</value>
        <DefaultValue(GetType(String), Nothing)>
        <LDescription(GetType(DerivedControls), "HelpKeyword_d")>
        <LCategory(GetType(DerivedControls), "Help_c", "Help")>
        Public Property HelpKeyword$()
            Get
                Return hlpHelp.GetHelpKeyword(Me)
            End Get
            Set(ByVal value$)
                hlpHelp.SetHelpKeyword(Me, value)
            End Set
        End Property
        ''' <summary>The current <see cref="System.Windows.Forms.HelpNavigator"/> setting for the control.</summary>
        ''' <returns>The <see cref="System.Windows.Forms.HelpNavigator"/> setting for the specified control. The default is <see cref="System.Windows.Forms.HelpNavigator.AssociateIndex"/>.</returns>
        ''' <value>One of the <see cref="System.Windows.Forms.HelpNavigator"/> values.</value>
        <DefaultValue(GetType(HtmlElementErrorEventArgs), "AssociateIndex")>
        <LDescription(GetType(DerivedControls), "HelpNavigator_d")>
        <LCategory(GetType(DerivedControls), "Help_c", "Help")>
        Public Property HelpNavigator() As HelpNavigator
            Get
                Return hlpHelp.GetHelpNavigator(Me)
            End Get
            Set(ByVal value As HelpNavigator)
                hlpHelp.SetHelpNavigator(Me, value)
            End Set
        End Property
        ''' <summary>Contents of the pop-up Help window for the specified control.</summary>
        ''' <returns>The Help string associated with this control. The default is null.</returns>
        ''' <value>The Help string associated with the control.</value>
        <DefaultValue(GetType(String), Nothing)>
        <LDescription(GetType(DerivedControls), "HelpString_d")>
        <LCategory(GetType(DerivedControls), "Help_c", "Help")>
        Public Property HelpString$()
            Get
                Return hlpHelp.GetHelpString(Me)
            End Get
            Set(ByVal value$)
                hlpHelp.SetHelpString(Me, value)
            End Set
        End Property
        ''' <summary>Inicates whether the specified control's Help should be displayed.</summary>
        ''' <returns>true if Help will be displayed for the control; otherwise, false.</returns>
        ''' <value>true if Help displays for the control; otherwise, false.</value>
        <DefaultValue(False)>
        <LDescription(GetType(DerivedControls), "ShowHelp_d")>
        <LCategory(GetType(DerivedControls), "Help_c", "Help")>
        Public Property ShowHelp() As Boolean
            Get
                Return hlpHelp.GetShowHelp(Me)
            End Get
            Set(ByVal value As Boolean)
                hlpHelp.SetShowHelp(Me, value)
            End Set
        End Property
#End Region
        ''' <summary>Initializes the control's component</summary>
        Private Sub InitializeComponent()
            Me.hlpHelp = New System.Windows.Forms.HelpProvider
            Me.SuspendLayout()
            Me.ResumeLayout(False)
        End Sub
    End Class
End Namespace
#End If