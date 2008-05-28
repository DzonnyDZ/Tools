Imports System.ComponentModel

Namespace Framework.SystemF.WindowsF.FormsF
    ''' <summary>Test <see cref="Control.AutoSize"/></summary>
    Public Class frmAutoSize
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Launches the test</summary>
        Public Shared Sub Test()
            Dim frm As New frmAutoSize
            frm.ShowDialog()
        End Sub

        Private Class LayoutSettings
            Private ReadOnly frm As frmAutoSize
            Private Const Form$ = "Form"
            Private Const TableLayoutPanel$ = "TableLayoutPanel"
            Private Const FlowLayoutPanel$ = "FlowLayoutPanel"
            Private Const Button1$ = "Button1"
            Private Const Button2$ = "Button2"
            Private Const Button3$ = "Button3"
            Private Const Button4$ = "Button4"
            Private Const Button5$ = "Button5"
            Public Sub New(ByVal Form As frmAutoSize)
                Me.frm = Form
            End Sub
#Region "Form"
            <Category(Form), DisplayName("Size")> _
            Public Property FormSize() As Size
                Get
                    Return frm.Size
                End Get
                Set(ByVal value As Size)
                    frm.Size = value
                End Set
            End Property
            <Category(Form), DisplayName("ClientSize")> _
            Public Property FormClientSize() As Size
                Get
                    Return frm.ClientSize
                End Get
                Set(ByVal value As Size)
                    frm.ClientSize = value
                End Set
            End Property
            <Category(Form), DisplayName("MinimumSize")> _
            Public Property FormMinimumSize() As Size
                Get
                    Return frm.MinimumSize
                End Get
                Set(ByVal value As Size)
                    frm.MinimumSize = value
                End Set
            End Property
            <Category(Form), DisplayName("MaximumSize")> _
            Public Property FormMaximumSize() As Size
                Get
                    Return frm.MaximumSize
                End Get
                Set(ByVal value As Size)
                    frm.MaximumSize = value
                End Set
            End Property
            <Category(Form), DisplayName("AutoSize")> _
            Public Property FormAutoSize() As Boolean
                Get
                    Return frm.AutoSize
                End Get
                Set(ByVal value As Boolean)
                    frm.AutoSize = value
                End Set
            End Property
            <Category(Form), DisplayName("AutoSizeMode")> _
            Public Property FormAutoSizeMode() As AutoScaleMode
                Get
                    Return frm.AutoSizeMode
                End Get
                Set(ByVal value As AutoScaleMode)
                    frm.AutoSizeMode = value
                End Set
            End Property
#End Region
#Region "TableLayoutPanel"
            <Category(TableLayoutPanel), DisplayName("Size")> _
            Public Property TableLayoutPanelSize() As Size
                Get
                    Return frm.TableLayoutPanel1.Size
                End Get
                Set(ByVal value As Size)
                    frm.TableLayoutPanel1.Size = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("ClientSize")> _
            Public Property TableLayoutPanelClientSize() As Size
                Get
                    Return frm.TableLayoutPanel1.ClientSize
                End Get
                Set(ByVal value As Size)
                    frm.TableLayoutPanel1.ClientSize = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("MinimumSize")> _
            Public Property TableLayoutPanelMinimumSize() As Size
                Get
                    Return frm.TableLayoutPanel1.MinimumSize
                End Get
                Set(ByVal value As Size)
                    frm.TableLayoutPanel1.MinimumSize = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("MaximumSize")> _
            Public Property TableLayoutPanelMaximumSize() As Size
                Get
                    Return frm.TableLayoutPanel1.MaximumSize
                End Get
                Set(ByVal value As Size)
                    frm.TableLayoutPanel1.MaximumSize = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("AutoSize")> _
            Public Property TableLayoutPanelAutoSize() As Boolean
                Get
                    Return frm.TableLayoutPanel1.AutoSize
                End Get
                Set(ByVal value As Boolean)
                    frm.TableLayoutPanel1.AutoSize = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("AutoSizeMode")> _
            Public Property TableLayoutPanelAutoSizeMode() As AutoScaleMode
                Get
                    Return frm.TableLayoutPanel1.AutoSizeMode
                End Get
                Set(ByVal value As AutoScaleMode)
                    frm.TableLayoutPanel1.AutoSizeMode = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Dock")> _
            Public Property TableLayoutPanelDock() As DockStyle
                Get
                    Return frm.TableLayoutPanel1.Dock
                End Get
                Set(ByVal value As DockStyle)
                    frm.TableLayoutPanel1.Dock = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Location")> _
            Public Property TableLayoutPanelLocation() As Point
                Get
                    Return frm.TableLayoutPanel1.Location
                End Get
                Set(ByVal value As Point)
                    frm.TableLayoutPanel1.Location = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Anchor")> _
            Public Property TableLayoutPanelAnchor() As AnchorStyles
                Get
                    Return frm.TableLayoutPanel1.Anchor
                End Get
                Set(ByVal value As AnchorStyles)
                    frm.TableLayoutPanel1.Anchor = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Column0SizeType")> _
            Public Property TableLayoutPanelColumn0SizeMode() As SizeType
                Get
                    Return frm.TableLayoutPanel1.ColumnStyles(0).SizeType
                End Get
                Set(ByVal value As SizeType)
                    frm.TableLayoutPanel1.ColumnStyles(0).SizeType = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Column1SizeType")> _
            Public Property TableLayoutPanelColumn1SizeMode() As SizeType
                Get
                    Return frm.TableLayoutPanel1.ColumnStyles(1).SizeType
                End Get
                Set(ByVal value As SizeType)
                    frm.TableLayoutPanel1.ColumnStyles(1).SizeType = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Row0SizeType")> _
            Public Property TableLayoutPanelRow0SizeMode() As SizeType
                Get
                    Return frm.TableLayoutPanel1.RowStyles(0).SizeType
                End Get
                Set(ByVal value As SizeType)
                    frm.TableLayoutPanel1.RowStyles(0).SizeType = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Row1SizeType")> _
            Public Property TableLayoutPanelRow1SizeMode() As SizeType
                Get
                    Return frm.TableLayoutPanel1.RowStyles(1).SizeType
                End Get
                Set(ByVal value As SizeType)
                    frm.TableLayoutPanel1.RowStyles(1).SizeType = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Column0Width")> _
            Public Property TableLayoutPanelColumn0Width() As Single
                Get
                    Return frm.TableLayoutPanel1.ColumnStyles(0).Width
                End Get
                Set(ByVal value As Single)
                    frm.TableLayoutPanel1.ColumnStyles(0).Width = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Column1Width")> _
            Public Property TableLayoutPanelColumn1Width() As Single
                Get
                    Return frm.TableLayoutPanel1.ColumnStyles(1).Width
                End Get
                Set(ByVal value As Single)
                    frm.TableLayoutPanel1.ColumnStyles(1).Width = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Row0Height")> _
            Public Property TableLayoutPanelRow0Height() As Single
                Get
                    Return frm.TableLayoutPanel1.RowStyles(0).Height
                End Get
                Set(ByVal value As Single)
                    frm.TableLayoutPanel1.RowStyles(0).Height = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Row1Height")> _
            Public Property TableLayoutPanelRow1Height() As Single
                Get
                    Return frm.TableLayoutPanel1.RowStyles(1).Height
                End Get
                Set(ByVal value As Single)
                    frm.TableLayoutPanel1.RowStyles(1).Height = value
                End Set
            End Property
            <Category(TableLayoutPanel), DisplayName("Column0ActualWidth")> _
            Public ReadOnly Property TableLayoutPanelColumn0ActualWidth() As Integer
                Get
                    Return frm.TableLayoutPanel1.GetColumnWidths(0)
                End Get
            End Property
            <Category(TableLayoutPanel), DisplayName("Column1ActualWidth")> _
            Public ReadOnly Property TableLayoutPanelColumn1ActualWidth() As Integer
                Get
                    Return frm.TableLayoutPanel1.GetColumnWidths(1)
                End Get
            End Property
            <Category(TableLayoutPanel), DisplayName("Row0ActualHeight")> _
            Public ReadOnly Property TableLayoutPanelRow0ActualHeight() As Integer
                Get
                    Return frm.TableLayoutPanel1.GetRowHeights(0)
                End Get
            End Property
            <Category(TableLayoutPanel), DisplayName("Row1ActualHeight")> _
            Public ReadOnly Property TableLayoutPanelRow1ActualHeight() As Integer
                Get
                    Return frm.TableLayoutPanel1.GetRowHeights(1)
                End Get
            End Property
#End Region
#Region "FlowLayoutPanel"
            <Category(FlowLayoutPanel), DisplayName("Size")> _
            Public Property FlowLayoutPanelSize() As Size
                Get
                    Return frm.FlowLayoutPanel1.Size
                End Get
                Set(ByVal value As Size)
                    frm.FlowLayoutPanel1.Size = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("ClientSize")> _
            Public Property FlowLayoutPanelClientSize() As Size
                Get
                    Return frm.FlowLayoutPanel1.ClientSize
                End Get
                Set(ByVal value As Size)
                    frm.FlowLayoutPanel1.ClientSize = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("MinimumSize")> _
            Public Property FlowLayoutPanelMinimumSize() As Size
                Get
                    Return frm.FlowLayoutPanel1.MinimumSize
                End Get
                Set(ByVal value As Size)
                    frm.FlowLayoutPanel1.MinimumSize = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("MaximumSize")> _
            Public Property FlowLayoutPanelMaximumSize() As Size
                Get
                    Return frm.FlowLayoutPanel1.MaximumSize
                End Get
                Set(ByVal value As Size)
                    frm.FlowLayoutPanel1.MaximumSize = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("AutoSize")> _
            Public Property FlowLayoutPanelAutoSize() As Boolean
                Get
                    Return frm.FlowLayoutPanel1.AutoSize
                End Get
                Set(ByVal value As Boolean)
                    frm.FlowLayoutPanel1.AutoSize = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("AutoSizeMode")> _
            Public Property FlowLayoutPanelAutoSizeMode() As AutoScaleMode
                Get
                    Return frm.FlowLayoutPanel1.AutoSizeMode
                End Get
                Set(ByVal value As AutoScaleMode)
                    frm.FlowLayoutPanel1.AutoSizeMode = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("Dock")> _
            Public Property FlowLayoutPanelDock() As DockStyle
                Get
                    Return frm.FlowLayoutPanel1.Dock
                End Get
                Set(ByVal value As DockStyle)
                    frm.FlowLayoutPanel1.Dock = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("Location")> _
            Public Property FlowLayoutPanelLocation() As Point
                Get
                    Return frm.FlowLayoutPanel1.Location
                End Get
                Set(ByVal value As Point)
                    frm.FlowLayoutPanel1.Location = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("Anchor")> _
            Public Property FlowLayoutPanelAnchor() As AnchorStyles
                Get
                    Return frm.FlowLayoutPanel1.Anchor
                End Get
                Set(ByVal value As AnchorStyles)
                    frm.FlowLayoutPanel1.Anchor = value
                End Set
            End Property
            <Category(FlowLayoutPanel), DisplayName("FlowDirection")> _
            Public Property FlowLayoutPanelFlowDirection() As FlowDirection
                Get
                    Return frm.FlowLayoutPanel1.FlowDirection
                End Get
                Set(ByVal value As FlowDirection)
                    frm.FlowLayoutPanel1.FlowDirection = value
                End Set
            End Property
#End Region
#Region "Button1"
            <Category(Button1), DisplayName("Size")> _
           Public Property Button1Size() As Size
                Get
                    Return frm.Button1.Size
                End Get
                Set(ByVal value As Size)
                    frm.Button1.Size = value
                End Set
            End Property
            <Category(Button1), DisplayName("MinimumSize")> _
            Public Property Button1MinimumSize() As Size
                Get
                    Return frm.Button1.MinimumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button1.MinimumSize = value
                End Set
            End Property
            <Category(Button1), DisplayName("MaximumSize")> _
            Public Property Button1MaximumSize() As Size
                Get
                    Return frm.Button1.MaximumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button1.MaximumSize = value
                End Set
            End Property
            <Category(Button1), DisplayName("AutoSize")> _
            Public Property Button1AutoSize() As Boolean
                Get
                    Return frm.Button1.AutoSize
                End Get
                Set(ByVal value As Boolean)
                    frm.Button1.AutoSize = value
                End Set
            End Property
            <Category(Button1), DisplayName("AutoSizeMode")> _
            Public Property Button1AutoSizeMode() As AutoScaleMode
                Get
                    Return frm.Button1.AutoSizeMode
                End Get
                Set(ByVal value As AutoScaleMode)
                    frm.Button1.AutoSizeMode = value
                End Set
            End Property
            <Category(Button1), DisplayName("Dock")> _
            Public Property Button1Dock() As DockStyle
                Get
                    Return frm.Button1.Dock
                End Get
                Set(ByVal value As DockStyle)
                    frm.Button1.Dock = value
                End Set
            End Property
            <Category(Button1), DisplayName("Location")> _
            Public Property Button1Location() As Point
                Get
                    Return frm.Button1.Location
                End Get
                Set(ByVal value As Point)
                    frm.Button1.Location = value
                End Set
            End Property
            <Category(Button1), DisplayName("Anchor")> _
            Public Property Button1Anchor() As AnchorStyles
                Get
                    Return frm.Button1.Anchor
                End Get
                Set(ByVal value As AnchorStyles)
                    frm.Button1.Anchor = value
                End Set
            End Property
            <Category(Button1), DisplayName("Text")> _
            Public Property Button1Text$()
                Get
                    Return frm.Button1.Text
                End Get
                Set(ByVal value$)
                    frm.Button1.Text = value
                End Set
            End Property
#End Region
#Region "Button2"
            <Category(Button2), DisplayName("Size")> _
           Public Property Button2Size() As Size
                Get
                    Return frm.Button2.Size
                End Get
                Set(ByVal value As Size)
                    frm.Button2.Size = value
                End Set
            End Property
            <Category(Button2), DisplayName("MinimumSize")> _
            Public Property Button2MinimumSize() As Size
                Get
                    Return frm.Button2.MinimumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button2.MinimumSize = value
                End Set
            End Property
            <Category(Button2), DisplayName("MaximumSize")> _
            Public Property Button2MaximumSize() As Size
                Get
                    Return frm.Button2.MaximumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button2.MaximumSize = value
                End Set
            End Property
            <Category(Button2), DisplayName("AutoSize")> _
            Public Property Button2AutoSize() As Boolean
                Get
                    Return frm.Button2.AutoSize
                End Get
                Set(ByVal value As Boolean)
                    frm.Button2.AutoSize = value
                End Set
            End Property
            <Category(Button2), DisplayName("AutoSizeMode")> _
            Public Property Button2AutoSizeMode() As AutoScaleMode
                Get
                    Return frm.Button2.AutoSizeMode
                End Get
                Set(ByVal value As AutoScaleMode)
                    frm.Button2.AutoSizeMode = value
                End Set
            End Property
            <Category(Button2), DisplayName("Dock")> _
            Public Property Button2Dock() As DockStyle
                Get
                    Return frm.Button2.Dock
                End Get
                Set(ByVal value As DockStyle)
                    frm.Button2.Dock = value
                End Set
            End Property
            <Category(Button2), DisplayName("Location")> _
            Public Property Button2Location() As Point
                Get
                    Return frm.Button2.Location
                End Get
                Set(ByVal value As Point)
                    frm.Button2.Location = value
                End Set
            End Property
            <Category(Button2), DisplayName("Anchor")> _
            Public Property Button2Anchor() As AnchorStyles
                Get
                    Return frm.Button2.Anchor
                End Get
                Set(ByVal value As AnchorStyles)
                    frm.Button2.Anchor = value
                End Set
            End Property
            <Category(Button2), DisplayName("Text")> _
            Public Property Button2Text$()
                Get
                    Return frm.Button2.Text
                End Get
                Set(ByVal value$)
                    frm.Button2.Text = value
                End Set
            End Property
#End Region
#Region "Button3"
            <Category(Button3), DisplayName("Size")> _
           Public Property Button3Size() As Size
                Get
                    Return frm.Button3.Size
                End Get
                Set(ByVal value As Size)
                    frm.Button3.Size = value
                End Set
            End Property
            <Category(Button3), DisplayName("MinimumSize")> _
            Public Property Button3MinimumSize() As Size
                Get
                    Return frm.Button3.MinimumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button3.MinimumSize = value
                End Set
            End Property
            <Category(Button3), DisplayName("MaximumSize")> _
            Public Property Button3MaximumSize() As Size
                Get
                    Return frm.Button3.MaximumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button3.MaximumSize = value
                End Set
            End Property
            <Category(Button3), DisplayName("AutoSize")> _
            Public Property Button3AutoSize() As Boolean
                Get
                    Return frm.Button3.AutoSize
                End Get
                Set(ByVal value As Boolean)
                    frm.Button3.AutoSize = value
                End Set
            End Property
            <Category(Button3), DisplayName("AutoSizeMode")> _
            Public Property Button3AutoSizeMode() As AutoScaleMode
                Get
                    Return frm.Button3.AutoSizeMode
                End Get
                Set(ByVal value As AutoScaleMode)
                    frm.Button3.AutoSizeMode = value
                End Set
            End Property
            <Category(Button3), DisplayName("Dock")> _
            Public Property Button3Dock() As DockStyle
                Get
                    Return frm.Button3.Dock
                End Get
                Set(ByVal value As DockStyle)
                    frm.Button3.Dock = value
                End Set
            End Property
            <Category(Button3), DisplayName("Location")> _
            Public Property Button3Location() As Point
                Get
                    Return frm.Button3.Location
                End Get
                Set(ByVal value As Point)
                    frm.Button3.Location = value
                End Set
            End Property
            <Category(Button3), DisplayName("Anchor")> _
            Public Property Button3Anchor() As AnchorStyles
                Get
                    Return frm.Button3.Anchor
                End Get
                Set(ByVal value As AnchorStyles)
                    frm.Button3.Anchor = value
                End Set
            End Property
            <Category(Button3), DisplayName("Text")> _
            Public Property Button3Text$()
                Get
                    Return frm.Button3.Text
                End Get
                Set(ByVal value$)
                    frm.Button3.Text = value
                End Set
            End Property
#End Region
#Region "Button4"
            <Category(Button4), DisplayName("Size")> _
           Public Property Button4Size() As Size
                Get
                    Return frm.Button4.Size
                End Get
                Set(ByVal value As Size)
                    frm.Button4.Size = value
                End Set
            End Property
            <Category(Button4), DisplayName("MinimumSize")> _
            Public Property Button4MinimumSize() As Size
                Get
                    Return frm.Button4.MinimumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button4.MinimumSize = value
                End Set
            End Property
            <Category(Button4), DisplayName("MaximumSize")> _
            Public Property Button4MaximumSize() As Size
                Get
                    Return frm.Button4.MaximumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button4.MaximumSize = value
                End Set
            End Property
            <Category(Button4), DisplayName("AutoSize")> _
            Public Property Button4AutoSize() As Boolean
                Get
                    Return frm.Button4.AutoSize
                End Get
                Set(ByVal value As Boolean)
                    frm.Button4.AutoSize = value
                End Set
            End Property
            <Category(Button4), DisplayName("AutoSizeMode")> _
            Public Property Button4AutoSizeMode() As AutoScaleMode
                Get
                    Return frm.Button4.AutoSizeMode
                End Get
                Set(ByVal value As AutoScaleMode)
                    frm.Button4.AutoSizeMode = value
                End Set
            End Property
            <Category(Button4), DisplayName("Dock")> _
            Public Property Button4Dock() As DockStyle
                Get
                    Return frm.Button4.Dock
                End Get
                Set(ByVal value As DockStyle)
                    frm.Button4.Dock = value
                End Set
            End Property
            <Category(Button4), DisplayName("Location")> _
            Public Property Button4Location() As Point
                Get
                    Return frm.Button4.Location
                End Get
                Set(ByVal value As Point)
                    frm.Button4.Location = value
                End Set
            End Property
            <Category(Button4), DisplayName("Anchor")> _
            Public Property Button4Anchor() As AnchorStyles
                Get
                    Return frm.Button4.Anchor
                End Get
                Set(ByVal value As AnchorStyles)
                    frm.Button4.Anchor = value
                End Set
            End Property
            <Category(Button4), DisplayName("Text")> _
            Public Property Button4Text$()
                Get
                    Return frm.Button4.Text
                End Get
                Set(ByVal value$)
                    frm.Button4.Text = value
                End Set
            End Property
#End Region
#Region "Button5"
            <Category(Button5), DisplayName("Size")> _
           Public Property Button5Size() As Size
                Get
                    Return frm.Button5.Size
                End Get
                Set(ByVal value As Size)
                    frm.Button5.Size = value
                End Set
            End Property
            <Category(Button5), DisplayName("MinimumSize")> _
            Public Property Button5MinimumSize() As Size
                Get
                    Return frm.Button5.MinimumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button5.MinimumSize = value
                End Set
            End Property
            <Category(Button5), DisplayName("MaximumSize")> _
            Public Property Button5MaximumSize() As Size
                Get
                    Return frm.Button5.MaximumSize
                End Get
                Set(ByVal value As Size)
                    frm.Button5.MaximumSize = value
                End Set
            End Property
            <Category(Button5), DisplayName("AutoSize")> _
            Public Property Button5AutoSize() As Boolean
                Get
                    Return frm.Button5.AutoSize
                End Get
                Set(ByVal value As Boolean)
                    frm.Button5.AutoSize = value
                End Set
            End Property
            <Category(Button5), DisplayName("AutoSizeMode")> _
            Public Property Button5AutoSizeMode() As AutoScaleMode
                Get
                    Return frm.Button5.AutoSizeMode
                End Get
                Set(ByVal value As AutoScaleMode)
                    frm.Button5.AutoSizeMode = value
                End Set
            End Property
            <Category(Button5), DisplayName("Dock")> _
            Public Property Button5Dock() As DockStyle
                Get
                    Return frm.Button5.Dock
                End Get
                Set(ByVal value As DockStyle)
                    frm.Button5.Dock = value
                End Set
            End Property
            <Category(Button5), DisplayName("Location")> _
            Public Property Button5Location() As Point
                Get
                    Return frm.Button5.Location
                End Get
                Set(ByVal value As Point)
                    frm.Button5.Location = value
                End Set
            End Property
            <Category(Button5), DisplayName("Anchor")> _
            Public Property Button5Anchor() As AnchorStyles
                Get
                    Return frm.Button5.Anchor
                End Get
                Set(ByVal value As AnchorStyles)
                    frm.Button5.Anchor = value
                End Set
            End Property
            <Category(Button5), DisplayName("Text")> _
            Public Property Button5Text$()
                Get
                    Return frm.Button5.Text
                End Get
                Set(ByVal value$)
                    frm.Button5.Text = value
                End Set
            End Property
#End Region
        End Class
        Private fpg As FloatingPropertyGrid
        Private Sub frmAutoSize_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
            fpg = New FloatingPropertyGrid(New LayoutSettings(Me))
            fpg.Show(Me)
        End Sub
    End Class
End Namespace