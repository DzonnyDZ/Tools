#If True
Imports System.Runtime.CompilerServices, System.Linq
Imports Tools.CollectionsT.GenericT

'#If Framework >= 3.5 Then
Namespace CollectionsT.SpecializedT
    'ASAP: Mark,  Wiki, Forum
    ''' <summary>Extension methods for working with specialized collections related to Windows Forms</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Module FormsCollectionsExtensions
        ''' <summary>Gets last <see cref="System.Windows.Forms.Control"/> in <see cref="System.Windows.Forms.Control.ControlCollection"/></summary>
        ''' <param name="Collection">Collection to get last item from</param>
        ''' <returns>Last item in <paramref name="Collection"/>, null if <paramref name="Collection"/> is empty.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        ''' <version version="1.5.2">Moved from <see cref="CollectionsT.SpecializedT.CollectionTools"/></version>
        <Extension()> Function Last(ByVal Collection As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Collection.Count = 0 Then Return Nothing
            Return Collection(Collection.Count - 1)
        End Function
        ''' <summary>Inserts control at particulare position in collection</summary>
        ''' <param name="Collection">Collection to insert control into</param>
        ''' <param name="index">Index to insert control at</param>
        ''' <param name="Control">Control to be inserted</param>
        ''' <remarks>This method requires removal of all following controls from collection. Removed controls are then placed back.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> or <paramref name="Control"/> is null</exception>
        ''' <exception cref="IndexOutOfRangeException"><paramref name="index"/> is less than zero of greater than <paramref name="Collection"/>.<see cref="System.Windows.Forms.Control.ControlCollection.Count">Count</see>.</exception>
        ''' <seealso cref="Replace"/>
        ''' 
        ''' <version version="1.5.2">Moved from <see cref="CollectionsT.SpecializedT.CollectionTools"/></version>
        <Extension()> Sub Insert(ByVal Collection As System.Windows.Forms.Control.ControlCollection, ByVal index As Integer, ByVal Control As System.Windows.Forms.Control)
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Control Is Nothing Then Throw New ArgumentNullException("Control")
            If index < 0 OrElse index > Collection.Count Then Throw New IndexOutOfRangeException(ResourcesT.Exceptions.IndexWasOutOfRangeOfControlsCollection)
            Dim RemovedControls As New List(Of System.Windows.Forms.Control)(From ToRemove In Collection.AsTypeSafe Skip index)
            If Collection.Owner IsNot Nothing Then Collection.Owner.SuspendLayout()
            Try
                While Collection.Count > index
                    Collection.RemoveAt(index)
                End While
                Collection.Add(Control)
                Collection.AddRange(RemovedControls.ToArray)
            Finally
                If Collection.Owner IsNot Nothing Then Collection.Owner.ResumeLayout()
            End Try
        End Sub
        ''' <summary>Replaces <see cref="System.Windows.Forms.Control"/> at specified index of <see cref="System.Windows.Forms.Control.ControlCollection"/></summary>
        ''' <param name="Collection">Collection to replace item in</param>
        ''' <param name="index">Index to replace item at</param>
        ''' <param name="Control">Control that will be placed at <paramref name="index"/>. If null, old control is removed at <paramref name="index"/> and nothing is inserted instead of it (so <paramref name="Collection"/> gets shorter).</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        ''' <exception cref="IndexOutOfRangeException"><paramref name="index"/> is less tha zero or greater than or equal to <paramref name="Collection"/>.<see cref="System.Windows.Forms.Control.ControlCollection.Count">Count</see>.</exception>
        ''' <remarks>Using this method requires all controls following control being replaced to be removed from collection and reinserted back.
        ''' <para>Raplacing control in collection does not necesarily mean that control will be visualy placed at the same postition in parent control. It is not true for any controls without layout model, such as <see cref="System.Windows.Forms.Form"/> or <see cref="System.Windows.Forms.Panel"/> and for for example <see cref="System.Windows.Forms.TableLayoutPanel"/>. It is true for <see cref="System.Windows.Forms.FlowLayoutPanel"/>.</para></remarks>
        ''' <seealso cref="Insert"/>
        ''' <seealso cref="WindowsT.FormsT.UtilitiesT.WinFormsExtensions.ReplaceControl"/>
        ''' 
        ''' <version version="1.5.2">Moved from <see cref="CollectionsT.SpecializedT.CollectionTools"/></version>
        <Extension()> Sub Replace(ByVal Collection As System.Windows.Forms.Control.ControlCollection, ByVal index As Integer, ByVal Control As System.Windows.Forms.Control)
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If index < 0 OrElse index >= Collection.Count Then Throw New IndexOutOfRangeException(ResourcesT.Exceptions.IndexWasOutOfRangeOfControlsCollection)
            Collection.RemoveAt(index)
            If Control Is Nothing Then Exit Sub
            Collection.Insert(index, Control)
        End Sub
    End Module
End Namespace
'#End If
#End If