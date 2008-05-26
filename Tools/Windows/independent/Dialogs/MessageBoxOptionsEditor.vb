Imports Tools.ComponentModelT, Tools.WindowsT.FormsT
Imports Tools.DrawingT.DesignT

#If Config <= Nightly Then
Namespace WindowsT.IndependentT
    ''' <summary>Implements editor for <see cref="MessageBox.MessageBoxOptions"/></summary>
    Friend NotInheritable Class MessageBoxOptionsEditor 'Localize:Control UI
        Inherits UserControlExtended
        Implements IEditor(Of MessageBox.MessageBoxOptions)
        ''' <summary>Default CTor</summary>
        Public Sub New()
            Me.New(0)
        End Sub
        ''' <summary>CTor with value</summary>
        ''' <param name="Value">Initial value</param>
        Public Sub New(ByVal Value As MessageBox.MessageBoxOptions)
            InitializeComponent()
            Me.Value = Value
        End Sub
        ''' <summary>Edited value</summary>
        <DefaultValue(GetType(MessageBox.MessageBoxOptions), "AlignLeft")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <Bindable(True)> _
        Public Property Value() As MessageBox.MessageBoxOptions Implements DrawingT.DesignT.IEditor(Of MessageBox.MessageBoxOptions).Value
            Get
                Dim ret As MessageBox.MessageBoxOptions = 0
                Select Case True
                    Case optLeft.Checked : ret = ret Or MessageBox.MessageBoxOptions.AlignLeft
                    Case optRight.Checked : ret = ret Or MessageBox.MessageBoxOptions.AlignRight
                    Case optCenter.Checked : ret = ret Or MessageBox.MessageBoxOptions.AlignCenter
                    Case optJustify.Checked : ret = ret Or MessageBox.MessageBoxOptions.AlignJustify
                End Select
                If optLtR.Checked Then ret = ret Or MessageBox.MessageBoxOptions.Ltr Else ret = ret Or MessageBox.MessageBoxOptions.Rtl
                If chkBring.Checked Then ret = ret Or MessageBox.MessageBoxOptions.BringToFront
                Return ret
            End Get
            Set(ByVal value As MessageBox.MessageBoxOptions)
                Select Case value And MessageBox.MessageBoxOptions.AlignMask
                    Case MessageBox.MessageBoxOptions.AlignCenter : optCenter.Checked = True
                    Case MessageBox.MessageBoxOptions.AlignJustify : optJustify.Checked = True
                    Case MessageBox.MessageBoxOptions.AlignRight : optRight.Checked = True
                    Case MessageBox.MessageBoxOptions.AlignLeft : optLeft.Checked = True
                End Select
                If (value And MessageBox.MessageBoxOptions.Rtl) = MessageBox.MessageBoxOptions.Rtl Then optRtL.Checked = True Else optLtR.Checked = True
                If (value And MessageBox.MessageBoxOptions.BringToFront) = MessageBox.MessageBoxOptions.BringToFront Then chkBring.Checked = True
            End Set
        End Property
        ''' <summary>Raised ehan  value of the <see cref="Value"/> property changes</summary>
        Public Event ValueChanged As EventHandler

        Private Sub All_CheckedChanged() _
            Handles chkBring.CheckedChanged, optCenter.CheckedChanged, optLeft.CheckedChanged, optRight.CheckedChanged, optJustify.CheckedChanged, optLeft.CheckedChanged, optRight.CheckedChanged
            RaiseEvent ValueChanged(Me, New EventArgs)
        End Sub
#Region "IEditor"
        ''' <summary>Contaisn value of the <see cref="Context"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Context As ITypeDescriptorContext

        ''' <summary>Stores context of current editing session</summary>
        ''' <remarks>This property is set by owner of the control and is valid between calls of <see cref="OnBeforeShow"/> and <see cref="OnClosed"/>.</remarks>
        Public Property Context() As System.ComponentModel.ITypeDescriptorContext Implements DrawingT.DesignT.IEditor(Of MessageBox.MessageBoxOptions).Context
            Get
                Return _Context
            End Get
            Private Set(ByVal value As System.ComponentModel.ITypeDescriptorContext)
                _Context = value
            End Set
        End Property
        ''' <summary>Owner of control informs control that it is about to be shown by calling this methos. It is called just befiore the control is shown.</summary>
        Protected Sub OnBeforeShow() Implements DrawingT.DesignT.IEditor(Of MessageBox.MessageBoxOptions).OnBeforeShow
            'Do nothing
        End Sub
        ''' <summary>Informs control that it was just hidden by calling this method.</summary>
        ''' <remarks>When implementing editor for reference type that is edited by changin its properties instead of changing its instance. Properties shouldbe changed in this method and onyl if <see cref="Result"/> is true.</remarks>
        Protected Sub OnClosed() Implements DrawingT.DesignT.IEditor(Of MessageBox.MessageBoxOptions).OnClosed
            'Do nothing
        End Sub
        Private _Result As Boolean
        ''' <summary>Stores editing result</summary>
        ''' <returns>True if editing was terminated with success, false if it was canceled</returns>
        ''' <remarks>This property is set by owner of the control and is valid when and after <see cref="OnClosed"/> is called</remarks>
        Public Property Result() As Boolean Implements DrawingT.DesignT.IEditor(Of MessageBox.MessageBoxOptions).Result
            Get
                Return _Result
            End Get
            Protected Set(ByVal value As Boolean)
                _Result = value
            End Set
        End Property
        ''' <summary>Contaisn value of the <see cref="Service"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Service As System.Windows.Forms.Design.IWindowsFormsEditorService
        ''' <summary>Stores <see cref="System.Windows.Forms.Design.IWindowsFormsEditorService"/> valid for current editing session</summary>
        ''' <remarks>This property is set by owner of the control and is valid between calls of <see cref="OnBeforeShow"/> and <see cref="OnClosed"/>.</remarks>
        Public Property Service() As System.Windows.Forms.Design.IWindowsFormsEditorService Implements DrawingT.DesignT.IEditor(Of MessageBox.MessageBoxOptions).Service
            Get
                Return _Service
            End Get
            Protected Set(ByVal value As System.Windows.Forms.Design.IWindowsFormsEditorService)
                _Service = value
            End Set
        End Property
#End Region
    End Class
End Namespace
#End If