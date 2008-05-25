Imports Tools.ComponentModelT

#If Config <= Nightly Then
Namespace WindowsT.IndependentT
    ''' <summary>Implements editor for <see cref="MessageBox.MessageBoxOptions"/></summary>
    Friend NotInheritable Class MessageBoxOptionsEditor 'Localize:Control
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
        Public Property Value() As MessageBox.MessageBoxOptions
            Get
                Dim ret As MessageBox.MessageBoxOptions
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

    End Class
End Namespace
#End If