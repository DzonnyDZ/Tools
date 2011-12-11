Imports System.Windows.Forms, System.Linq

Namespace WindowsT.FormsT
    ''' <summary>Shows <see cref="PropertyGrid"/> in a window</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class PropertyDialog
        ''' <summary>CTor - creates a new instance of the <see cref="PropertyDialog"/> class without initializing selected object</summary>
        Public Sub New()
            InitializeComponent()
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="PropertyDialog"/> class and selects single object</summary>
        ''' <param name="selectedObject">An object to be selected in <see cref="PropertyGrid"/>.</param>
        Public Sub New(selectedObject As Object)
            Me.New()
            prgProperties.SelectedObject = selectedObject
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="PropertyDialog"/> class and selects multiple objects</summary>
        ''' <param name="selectedObjects">Objects to be selected in <see cref="PropertyGrid"/></param>
        Public Sub New(ParamArray selectedObjects As Object())
            Me.New()
            prgProperties.SelectedObjects = selectedObjects
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="PropertyDialog"/> class and selects multiple objects</summary>
        ''' <param name="selectedObjects">Objects to be selected in <see cref="PropertyGrid"/></param>
        Public Sub New(selectedObjects As IEnumerable)
            Me.New((From itm In selectedObjects.Cast(Of Object)()).ToArray)
        End Sub

        ''' <summary>gets or sets single selected object</summary>
        Public Property SelectedObject As Object
            Get
                Return prgProperties.SelectedObject
            End Get
            Set(value As Object)
                prgProperties.SelectedObject = SelectedObject
            End Set
        End Property

        ''' <summary>Gets or sets multiple selected objects</summary>
        Public Property SelectedObjects As Object()
            Get
                Return prgProperties.SelectedObjects
            End Get
            Set(value As Object())
                prgProperties.SelectedObjects = SelectedObjects
            End Set
        End Property

        ''' <summary>gets an instance of a <see cref="PropertyGrid"/> used to manipulate objects</summary>
        Public ReadOnly Property PropertyGrid As PropertyGrid
            Get
                Return prgProperties
            End Get
        End Property

        ''' <summary>Gets or sets value indicating which buttons are visible in dialog</summary>
        ''' <value>Only buttons <see cref="IndependentT.MessageBox.MessageBoxButton.Buttons.OK"/> and <see cref="IndependentT.MessageBox.MessageBoxButton.Buttons.Cancel"/> are supported. Other values are ignored</value>
        ''' <returns>Currently visible buttons</returns>
        <DefaultValue(IndependentT.MessageBox.MessageBoxButton.Buttons.OK)>
        Public Property Buttons As IndependentT.MessageBox.MessageBoxButton.Buttons
            Get
                Return If(cmdOK.Visible, IndependentT.MessageBox.MessageBoxButton.Buttons.OK, 0) Or If(cmdCancel.Visible, IndependentT.MessageBox.MessageBoxButton.Buttons.Cancel, 0)
            End Get
            Set(value As IndependentT.MessageBox.MessageBoxButton.Buttons)
                cmdOK.Visible = value.HasFlag(IndependentT.MessageBox.MessageBoxButton.Buttons.OK)
                cmdCancel.Visible = value.HasFlag(IndependentT.MessageBox.MessageBoxButton.Buttons.Cancel)
            End Set
        End Property
    End Class

End Namespace