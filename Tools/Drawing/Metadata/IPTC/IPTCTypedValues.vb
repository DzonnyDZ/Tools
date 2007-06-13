Imports Tools.CollectionsT.GenericT, System.Globalization.CultureInfo, Tools.DataStructuresT.GenericT
Namespace DrawingT.MetadataT
#If Congig <= Nightly Then 'Stage: Nightly
    Partial Public Class IPTC
        ''' <summary>Contains value of the <see cref="Encoding"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Encoding As System.Text.Encoding = System.Text.Encoding.Default
        ''' <summary>Encoding used for encoding and decoding some texts</summary>
        Public Property Encoding() As System.Text.Encoding
            Get
                Return _Encoding
            End Get
            Set(ByVal value As System.Text.Encoding)
                _Encoding = value
            End Set
        End Property
#Region "Readers and writers"
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.UnsignedBinaryNumber"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">Value stored in IPTC stream has lenght neither 1, 2, 4 nor 8 (in Getter)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
      Protected Overridable Property UnsignedBinaryNumber_Value(ByVal Key As DataSetIdentification) As List(Of ULong)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of ULong)(values.Count)
                For Each item As Byte() In values
                    ret.Add(UIntFromBytes(item.Length, item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of ULong))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As ULong In value
                        Select Case item
                            Case Is < Byte.MaxValue
                                values.Add(ToBytes(CByte(1), item))
                            Case Is < UShort.MaxValue
                                values.Add(ToBytes(CByte(2), item))
                            Case Is < UInteger.MaxValue
                                values.Add(ToBytes(CByte(4), item))
                            Case Else
                                values.Add(ToBytes(CByte(8), item))
                        End Select
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.Boolean_Binary"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Bytes">Number of bytes per one boolean item (ignored in Getter)</param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
       Protected Overridable Property Boolean_Binary_Value(ByVal Key As DataSetIdentification, Optional ByVal Bytes As Byte = 1) As List(Of Boolean)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Boolean)(values.Count)
                For Each item As Byte() In values
                    Dim val As Boolean = False
                    For Each b As Byte In item
                        If b <> 0 Then val = True : Exit For
                    Next b
                    ret.Add(val)
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of Boolean))
                If Bytes = 0 Then Throw New ArgumentException("Bytes cannot be 0")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each val As Boolean In value
                        Dim arr(Bytes - 1) As Byte
                        If val Then arr(Bytes - 1) = 1
                        values.Add(arr)
                    Next val
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.Byte_Binary"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">Value stored in IPTC stream has lenght neither 1, 2, 4 nor 8 (in Getter)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
       Protected Overridable Property Byte_Binary_Value(ByVal Key As DataSetIdentification) As List(Of Byte)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Byte)(values.Count)
                For Each item As Byte() In values
                    ret.Add(UIntFromBytes(item.Length, item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of Byte))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As ULong In value
                        values.Add(ToBytes(CByte(1), item))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.UShort_Binary"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">Value stored in IPTC stream has lenght neither 1, 2, 4 nor 8 (in Getter)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property UShort_Binary_Value(ByVal Key As DataSetIdentification) As List(Of UShort)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of UShort)(values.Count)
                For Each item As Byte() In values
                    ret.Add(UIntFromBytes(item.Length, item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of UShort))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As ULong In value
                        values.Add(ToBytes(CByte(2), item))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.NumericChar"/></summary>
        ''' <param name="key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed lenght of string (ignored in Getter, 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght (ignored in Getter)</param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <exception cref="InvalidCastException">Cannot convert stored bytes into number (in Getter)</exception>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Len"/> is 0 and <paramref name="Fixed"/> is True (in Setter) -or-
        ''' Number cannot be stored in given number of bytes (if <paramref name="Len"/> is non-zero, in Setter) -or-
        ''' Number to be stored is negative (in Setter)
        ''' </exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property NumericChar_Value(ByVal key As DataSetIdentification, ByVal Len As Byte, Optional ByVal Fixed As Boolean = False) As List(Of Decimal)
            Get
                Dim values As List(Of Byte()) = Tag(key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Decimal)(values.Count)
                For Each item As Byte() In values
                    ret.Add(NumCharFromBytes(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of Decimal))
                If Len = 0 And Fixed = True Then Throw New ArgumentException("Len cannot be 0 when Fixed is true")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As Decimal In value
                        If item < 0 Then Throw New ArgumentException("Number cannot be negative")
                        values.Add(ToBytes(Len, item, Fixed))
                    Next item
                End If
                Tag(key) = values
            End Set
        End Property
        ''' <summary>Gets or sets values(s) of type <see cref="IPTCTypes.GraphicCharacters"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed lenght of string value after encoding (ignored in Getter)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <param name="Encoding">Encoding to be used. Is ommited or nothing then <see cref="IPTC.Encoding"/> is used</param>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Len"/> is 0 and <paramref name="Fixed"/> is true (in Setter) -or- 
        ''' One of values being set contains non-graphic character (in setter) -or-
        ''' One of values being set violates <paramref name="Len"/> and/or <paramref name="Fixed"/> constraint after being encoded
        ''' </exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property GraphicCharacters_Value(ByVal Key As DataSetIdentification, ByVal Len As Byte, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
            Get
                If Encoding Is Nothing Then Encoding = Me.Encoding
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of String)(values.Count)
                For Each item As Byte() In values
                    ret.Add(Encoding.GetString(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of String))
                If Encoding Is Nothing Then Encoding = Me.Encoding
                If Len = 0 And Fixed = True Then Throw New ArgumentException("Len cannot be 0 when Fixed is true")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As String In value
                        If Not IsGraphicCharacters(item) Then Throw New ArgumentException(String.Format("Item {0} contains non-graphic character"))
                        Dim bytes As Byte() = Encoding.GetBytes(item)
                        If (Len <> 0 AndAlso bytes.Length > 0) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New ArgumentException(String.Format("String ""{0}"" canot be stored without violating length and/or fixed constraint", item))
                        Else
                            values.Add(bytes)
                        End If
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets values(s) of type <see cref="IPTCTypes.TextWithSpaces"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed lenght of string value after encoding (ignored in Getter)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <param name="Encoding">Encoding to be used. Is ommited or nothing then <see cref="IPTC.Encoding"/> is used</param>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Len"/> is 0 and <paramref name="Fixed"/> is true (in Setter) -or- 
        ''' One of values being set contains non-graphic-non-space character (in setter) -or-
        ''' One of values being set violates <paramref name="Len"/> and/or <paramref name="Fixed"/> constraint after being encoded
        ''' </exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property TextWithSpaces_Value(ByVal Key As DataSetIdentification, ByVal Len As Byte, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
            Get
                If Encoding Is Nothing Then Encoding = Me.Encoding
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of String)(values.Count)
                For Each item As Byte() In values
                    ret.Add(Encoding.GetString(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of String))
                If Encoding Is Nothing Then Encoding = Me.Encoding
                If Len = 0 And Fixed = True Then Throw New ArgumentException("Len cannot be 0 when Fixed is true")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As String In value
                        If Not IsTextWithSpaces(item) Then Throw New ArgumentException(String.Format("Item {0} contains non-graphic-non-space character"))
                        Dim bytes As Byte() = Encoding.GetBytes(item)
                        If (Len <> 0 AndAlso bytes.Length > 0) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New ArgumentException(String.Format("String ""{0}"" canot be stored without violating length and/or fixed constraint", item))
                        Else
                            values.Add(bytes)
                        End If
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property

        ''' <summary>Gets or sets values(s) of type <see cref="IPTCTypes.TextWithSpaces"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed lenght of string value after encoding (ignored in Getter)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <param name="Encoding">Encoding to be used. Is ommited or nothing then <see cref="IPTC.Encoding"/> is used</param>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Len"/> is 0 and <paramref name="Fixed"/> is true (in Setter) -or- 
        ''' One of values being set contains non-graphic-non-space-non-cr-non-lf character (in setter) -or-
        ''' One of values being set violates <paramref name="Len"/> and/or <paramref name="Fixed"/> constraint after being encoded
        ''' </exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property Text_Value(ByVal Key As DataSetIdentification, ByVal Len As Byte, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
            Get
                If Encoding Is Nothing Then Encoding = Me.Encoding
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of String)(values.Count)
                For Each item As Byte() In values
                    ret.Add(Encoding.GetString(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of String))
                If Encoding Is Nothing Then Encoding = Me.Encoding
                If Len = 0 And Fixed = True Then Throw New ArgumentException("Len cannot be 0 when Fixed is true")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As String In value
                        If Not IsText(item) Then Throw New ArgumentException(String.Format("Item {0} contains non-graphic-non-space-non-cr-non-lf character"))
                        Dim bytes As Byte() = Encoding.GetBytes(item)
                        If (Len <> 0 AndAlso bytes.Length > 0) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New ArgumentException(String.Format("String ""{0}"" canot be stored without violating length and/or fixed constraint", item))
                        Else
                            values.Add(bytes)
                        End If
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of <see cref="IPTCTypes.BW460"/> type</summary>
        ''' <param name="key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed length of serialized bitmap (ignored in Getter)</param>
        ''' <param name="Fixed"><paramref name="Len"/> represents fixed lenght of serialized bitmap (ignored in Getter)</param>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Fixed"/> is True and <paramref name="Len"/> is 0 (in Setter) -or-
        ''' Bitmap being set has width different form 460 (in Setter) -or-
        ''' Bitmap violates lenght constraint after serialization (in Setter)
        ''' </exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property BW460_Value(ByVal key As DataSetIdentification, ByVal Len As Byte, Optional ByVal Fixed As Boolean = False) As List(Of Drawing.Bitmap)
            'TODO:Optional len!
            Get
                Dim values As List(Of Byte()) = Tag(key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Drawing.Bitmap)(values.Count)
                For Each item As Byte() In values
                    Dim ba As New BitArray(item)
                    If ba.Length Mod 460 <> 0 Then Throw New ArgumentException("Invalid bitmap. Number of bits in bitmap must be multiplication of 460")
                    Dim bmp As New Drawing.Bitmap(460, ba.Length / 460, Drawing.Imaging.PixelFormat.Format1bppIndexed)
                    For i As Integer = 0 To ba.Length - 1 Step 460
                        For j As Integer = i To i + 460 - 1
                            bmp.SetPixel(j - i, i / 460, VisualBasicT.iif(ba(j), Drawing.Color.Black, Drawing.Color.White))
                        Next j
                    Next i
                    ret.Add(bmp)
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of Drawing.Bitmap))
                If Len = 0 And Fixed = True Then Throw New ArgumentException("Len cannot be 0 when Fixed is true")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As Drawing.Bitmap In value
                        If item.Width <> 460 Then Throw New ArgumentException("Bitmap's width must be 460px")
                        Dim ba As New BitArray(item.Width * item.Height)
                        For i As Integer = 0 To item.Height
                            For j As Integer = 0 To item.Width
                                ba(i * 460 + j) = Not item.GetPixel(j, i) = Drawing.Color.Wheat
                            Next j
                        Next i
                        values.Add(Ba2Bytes(ba))
                        Dim BLen As Integer = values(values.Count - 1).Length
                        If (BLen > Len AndAlso Len <> 0) OrElse (Fixed AndAlso BLen <> Len) Then Throw New ArgumentException("Bitmap violates lenght constraint")
                    Next item
                End If
                Tag(key) = values
            End Set
        End Property
        ''' <summary>Converts <see cref="BitArray"/> into <see cref="Byte()"/></summary>
        ''' <param name="ba">Bits to be converted</param>
        ''' <returns>Array of <see cref="Byte"/>()</returns>
        Private Function Ba2Bytes(ByVal ba As BitArray) As Byte() 'TODO:Extract as separate tool
            Dim bytes(Math.Ceiling(ba.Length / 8)) As Byte
            For i As Integer = 0 To ba.Length - 1
                bytes(i \ 8) = bytes(i \ 8) Or 1 << (i Mod 8)
            Next
            Return bytes
        End Function

        Public Property Enum_Binary_Value(ByVal Key As DataSetIdentification, ByVal Type As Type) As List(Of [Enum])
            Get

            End Get
            Set(ByVal value As List(Of [Enum]))

            End Set
        End Property


        Public Property Enum_NumChar_Value(ByVal Key As DataSetIdentification, ByVal Type As Type) As List(Of [Enum])
            Get

            End Get
            Set(ByVal value As [Enum])

            End Set
        End Property
        Public Property CCYYMMDD_Value(ByVal Key As DataSetIdentification) As List(Of Date)
            Get

            End Get
            Set(ByVal value As Date)

            End Set
        End Property

        Public Property CCYYMMDDOmmitable_Value(ByVal Key As DataSetIdentification) As List(Of OmmitableDate)
            Get

            End Get
            Set(ByVal value As OmmitableDate)

            End Set
        End Property
        Public Property HHMMSS_HHMM_Value(ByVal Key As DataSetIdentification) As List(Of Time)
            Get

            End Get
            Set(ByVal value As Time)

            End Set
        End Property
        Public Property ByteArray_Value(ByVal Key As DataSetIdentification) As List(Of Byte())
            Get

            End Get
            Set(ByVal value As Byte())

            End Set
        End Property
        Public Property UNO_Value(ByVal Key As DataSetIdentification) As List(Of UNO)
            Get

            End Get
            Set(ByVal value As UNO)

            End Set
        End Property

        Public Property Num2_Str_Value(ByVal Key As DataSetIdentification) As List(Of NumStr2)
            Get

            End Get
            Set(ByVal value As NumStr2)

            End Set
        End Property

        Public Property Num3_Str_Value(ByVal Key As DataSetIdentification) As List(Of NumStr3)
            Get

            End Get
            Set(ByVal value As NumStr3)

            End Set
        End Property

        Public Property SubjectReference_Value(ByVal Key As DataSetIdentification) As List(Of SubjectReference)
            Get

            End Get
            Set(ByVal value As SubjectReference)

            End Set
        End Property

        Public Property Alpha_Value(ByVal Key As DataSetIdentification) As List(Of String)
            Get

            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Public Property StringEnum_Value(ByVal Key As DataSetIdentification) As List(Of T1orT2(Of [Enum], String))
            Get

            End Get
            Set(ByVal value As T1orT2(Of [Enum], String))

            End Set
        End Property

        Public Property ImageType_Value(ByVal Key As DataSetIdentification) As List(Of ImageType)
            Get

            End Get
            Set(ByVal value As ImageType)

            End Set
        End Property

        Public Property AudioType_Value(ByVal Key As DataSetIdentification) As List(Of AudioDataType)
            Get

            End Get
            Set(ByVal value As AudioDataType)

            End Set
        End Property

        Public Property HHMMSS_Value(ByVal Key As DataSetIdentification) As List(Of TimeSpan)
            Get

            End Get
            Set(ByVal value As TimeSpan)

            End Set
        End Property
#End Region
    End Class
#End If
End Namespace