Imports System.Windows.Forms, System.Text
Namespace GUI
    ''' <summary>Dialog výbìru kódování</summary>
    Public Class SelectEncodingDialog
        Public Sub New()
            InitializeComponent()
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            cmbEncoding.DisplayMember = "DisplayName"
            Dim Encodings As New List(Of EncodingInfo)(Encoding.GetEncodings)
            Dim eic As New EncodingInfoComparerInternal
            Encodings.Sort(eic)
            cmbEncoding.Items.AddRange(Encodings.ToArray)
            cmbEncoding.SelectedIndex = 0
        End Sub
        Public Shared Shadows Function Show(Optional ByVal owner As IWin32Window = Nothing) As Encoding
            Dim inst As New SelectEncodingDialog
            If inst.ShowDialog(owner) = Windows.Forms.DialogResult.OK Then
                Return inst.SelectedEncoding.GetEncoding
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>Interní porovnávaè kódování</summary>
        ''' <remarks>Zajisí aby <see cref="Encoding.UTF8"/> bylo první a <see cref="Encoding.[Default]"/> druhý</remarks>
        Private Class EncodingInfoComparerInternal
            Implements IComparer(Of EncodingInfo)
            ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
            ''' <param name="x">The first object to compare.</param>
            ''' <param name="y">The second object to compare.</param>
            ''' <returns>Value Condition Less than zero x is less than y.  Zero x equals y.  Greater than zero x is greater than y.</returns>
            Public Function Compare(ByVal x As System.Text.EncodingInfo, ByVal y As System.Text.EncodingInfo) As Integer Implements System.Collections.Generic.IComparer(Of System.Text.EncodingInfo).Compare
                If x.Name = Encoding.UTF8.WebName AndAlso y.Name = Encoding.UTF8.WebName Then Return 0
                If x.Name = Encoding.UTF8.WebName Then Return -1
                If y.Name = Encoding.UTF8.WebName Then Return 1
                If x.Name = Encoding.Default.WebName AndAlso y.Name = Encoding.Default.WebName Then Return 0
                If x.Name = Encoding.Default.WebName Then Return -1
                If y.Name = Encoding.Default.WebName Then Return 1
                Return String.Compare(y.DisplayName, y.DisplayName)
            End Function
        End Class

        Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub
        ''' <summary>Vybrané kódování</summary>
        Public Property SelectedEncoding() As EncodingInfo
            Get
                Return cmbEncoding.SelectedItem
            End Get
            Set(ByVal value As EncodingInfo)
                cmbEncoding.SelectedItem = value
            End Set
        End Property
    End Class
End Namespace