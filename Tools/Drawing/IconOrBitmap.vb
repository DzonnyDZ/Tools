Imports System.Drawing, Tools.DataStructuresT.GenericT
#If Config <= Nightly Then 'Stage:Nightly
Namespace DrawingT
    ''' <summary>Represents bitmap stored either as <see cref="Image"/> or as <see cref="Icon"/></summary>
    ''' <remarks>Each instance of <see cref="IconOrBitmap"/> can be converted to <see cref="Image"/> and <see cref="Icon"/></remarks>
    Public NotInheritable Class IconOrBitmap
        Inherits T1orT2(Of Image, Icon)
        ''' <summary>CTor form <see cref="Drawing.Icon"/></summary>
        ''' <param name="Icon">An <see cref="Drawing.Icon"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Icon"/> is null</exception>
        Public Sub New(ByVal Icon As Icon)
            MyBase.New(Icon)
            If Icon Is Nothing Then Throw New ArgumentNullException("Icon")
        End Sub
        ''' <summary>CTor form <see cref="Drawing.Image"/></summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Image"/> is null</exception>
        ''' <param name="Image">An <see cref="Drawing.Image"/></param>
        Public Sub New(ByVal Image As Image)
            MyBase.New(Image)
            If Image Is Nothing Then Throw New ArgumentNullException("Image")
        End Sub
        ''' <summary>Converts <see cref="IconOrBitmap"/> to <see  cref="Image"/></summary>
        ''' <param name="a">A <see cref="IconOrBitmap"/></param>
        ''' <returns><see cref="Drawing.Image"/> that represents by this instance. It is either <see cref="Image"/> or <see cref="Icon"/>.<see cref="Icon.ToBitmap">ToBitmap</see>; null when <paramref name="a"/> is null.</returns>
        ''' <seelaso cref="Image"/>
        ''' <version version="1.5.3">Fix: <see cref="NullReferenceException"/> when <paramref name="a"/> is null. Now returns null instead.</version>
        Overloads Shared Widening Operator CType(ByVal a As IconOrBitmap) As Image
            if a is nothing then return nothing
            Return a.Image
        End Operator
        ''' <summary>Converts <see cref="IconOrBitmap"/> to <see  cref="Image"/></summary>
        ''' <param name="a">A <see cref="IconOrBitmap"/></param>
        ''' <returns><see cref="Drawing.Image"/> that represents by this instance. It is either <see cref="Image"/> or <see cref="Icon"/>.<see cref="Icon.ToBitmap">ToBitmap</see>; null when <paramref name="a"/> is null.</returns>
        ''' <seelaso cref="Icon"/>
        ''' <version version="1.5.3">Fix: <see cref="NullReferenceException"/> when <paramref name="a"/> is null. Now returns null instead.</version>
        Overloads Shared Widening Operator CType(ByVal a As IconOrBitmap) As Icon
            if a is nothing then return nothing
            Return a.Icon
        End Operator
        ''' <summary>Converts <see cref="T1orT2"/>[<see cref="Bitmap"/>, <see cref="Icon"/>] to <see cref="IconOrBitmap"/></summary>
        ''' <param name="a">Value to be converted</param>
        ''' <returns>New instance of <see cref="IconOrBitmap"/>. Null if <paramref name="a"/> is null.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> does contain neither value</exception>
        Overloads Shared Widening Operator CType(ByVal a As T1orT2(Of Bitmap, Icon)) As IconOrBitmap
            If a Is Nothing Then Return Nothing
            If a.contains1 Then Return New IconOrBitmap(a.value1) Else Return New IconOrBitmap(a.value2)
        End Operator
        '''' <summary>Converts <see cref="T1orT2"/>[<see cref="Image"/>, <see cref="Icon"/>] to <see cref="IconOrBitmap"/></summary>
        '''' <param name="a">Value to be converted</param>
        '''' <returns>New instance of <see cref="IconOrBitmap"/>. Null if <paramref name="a"/> is null.</returns>
        '''' <exception cref="ArgumentNullException"><paramref name="a"/> does contain neither value</exception>
        'Overloads Shared Widening Operator CType(ByVal a As T1orT2(Of Image, Icon)) As IconOrBitmap
        '    If a Is Nothing Then Return Nothing
        '    If a.contains1 Then Return New IconOrBitmap(a.value1) Else Return New IconOrBitmap(a.value2)
        'End Operator
        ''' <summary>Converts <see cref="T1orT2"/>[<see cref="Icon"/>, <see cref="Bitmap"/>] to <see cref="IconOrBitmap"/></summary>
        ''' <param name="a">Value to be converted</param>
        ''' <returns>New instance of <see cref="IconOrBitmap"/>. Null if <paramref name="a"/> is null.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> does contain neither value</exception>
        Overloads Shared Widening Operator CType(ByVal a As T1orT2(Of Icon, Bitmap)) As IconOrBitmap
            If a.contains1 Then Return New IconOrBitmap(a.value1) Else Return New IconOrBitmap(a.value2)
        End Operator
        ''' <summary>Converts <see cref="T1orT2"/>[<see cref="Icon"/>, <see cref="Image"/>] to <see cref="IconOrBitmap"/></summary>
        ''' <param name="a">Value to be converted</param>
        ''' <returns>New instance of <see cref="IconOrBitmap"/>. Null if <paramref name="a"/> is null.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> does contain neither value</exception>
        Overloads Shared Widening Operator CType(ByVal a As T1orT2(Of Icon, Image)) As IconOrBitmap
            If a Is Nothing Then Return Nothing
            If a.contains1 Then Return New IconOrBitmap(a.value1) Else Return New IconOrBitmap(a.value2)
        End Operator
        ''' <summary>Converts <see cref="Drawing.Icon"/> to <see cref="IconOrBitmap"/></summary>
        ''' <param name="a">A <see cref="Drawing.Icon"/></param>
        ''' <returns><see cref="IconOrBitmap"/> initialized with <paramref name="a"/>. Null if <paramref name="a"/> is null.</returns>
        Overloads Shared Widening Operator CType(ByVal a As Icon) As IconOrBitmap
            If a Is Nothing Then Return Nothing
            Return New IconOrBitmap(a)
        End Operator
        ''' <summary>Converts <see cref="Drawing.Image"/> to <see cref="IconOrBitmap"/></summary>
        ''' <param name="a">A <see cref="Drawing.Image"/></param>
        ''' <returns><see cref="IconOrBitmap"/> initialized with <paramref name="a"/>. Null if <paramref name="a"/> is null.</returns>
        Overloads Shared Widening Operator CType(ByVal a As Image) As IconOrBitmap
            If a Is Nothing Then Return Nothing
            Return New IconOrBitmap(a)
        End Operator
        ''' <summary>Gets or sets value of type <see cref="Image"/></summary>
        ''' <value>Non-null value to set value of type <see cref="Image"/> and delete value of type <see cref="Icon"/></value>
        ''' <returns>If this instance contains value of type <see cref="Image"/> then returns it, otherwise return null</returns>
        ''' <remarks>
        ''' If <see cref="value1"/> retruns null it means that either value of type <see cref="Image"/> is not present in this instance or it is present but it is null. Check <see cref="contains1"/> property in order to determine actual situation.
        ''' You must set this property to nothing and then set <see cref="contains1"/> property to true in order to store null value of type <see cref="Image"/>.
        ''' </remarks>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <seelaso cref="Image"/>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <Browsable(False)> _
        Public Overrides Property value1() As System.Drawing.Image
            <DebuggerStepThrough()> Get
                Return MyBase.value1
            End Get
            Set(ByVal value As System.Drawing.Image)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                MyBase.value1 = value
            End Set
        End Property

        ''' <summary>Gets or sets value of type <see cref="Image"/></summary>
        ''' <value>Non-null value to set value of type <see cref="Image"/> and delete value of type <see cref="Icon"/></value>
        ''' <returns>Image represented by this instance</returns>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <seelaso cref="Icon.ToBitmap"/>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Image() As Image
            Get
                If contains1 Then : Return value1 : ElseIf contains2 Then : Return value2.ToBitmap : Else : Return Nothing : End If
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Image)
                value1 = value
            End Set
        End Property

        ''' <summary>Gets or sets value of type <see cref="Icon"/></summary>
        ''' <value>Non-null value to set value of type <see cref="Icon"/> and delete value of type <see cref="Image"/></value>
        ''' <returns>If this instance contains value of type <see cref="Icon"/> then returns it, otherwise return null</returns>
        ''' <remarks>
        ''' If <see cref="value2"/> retruns null it means that either value of type <see cref="Icon"/> is not present in this instance or it is present but it is null. Check <see cref="contains2"/> property in order to determine actual situation.
        ''' You must set this property to nothing and then set <see cref="contains1"/> property to true in order to store null value of type <see cref="Icon"/>.
        ''' </remarks>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <seelaso cref="Icon"/>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property value2() As System.Drawing.Icon
            <DebuggerStepThrough()> Get
                Return MyBase.value2
            End Get
            Set(ByVal value As System.Drawing.Icon)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                MyBase.value2 = value
            End Set
        End Property
        ''' <summary>Gets or sets value of type <see cref="Icon"/></summary>
        ''' <value>Non-null value to set value of type <see cref="Icon"/> and delete value of type <see cref="Image"/></value>
        ''' <returns>Representation of this instance as <see cref="Drawing.Icon"/></returns>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <seelaso cref="Bitmap.FromHicon"/><seelaso cref="Icon.FromHandle"/>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Icon() As Icon
            Get
                If contains2 Then
                    Return value2
                ElseIf contains1 Then
                    If TypeOf Image Is Bitmap Then
                        Return Drawing.Icon.FromHandle(DirectCast(Image, Bitmap).GetHicon)
                    Else
                        Dim bmp As New Bitmap(Image)
                        Return Drawing.Icon.FromHandle(bmp.GetHicon)
                    End If
                Else
                    Return Nothing
                End If
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Icon)
                value2 = value
            End Set
        End Property
    End Class
End Namespace
#End If