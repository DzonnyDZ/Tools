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
        Protected Overridable Property NumericChar_Value(ByVal key As DataSetIdentification, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False) As List(Of Decimal)
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
        Protected Overridable Property GraphicCharacters_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
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
        Protected Overridable Property TextWithSpaces_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
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
        Protected Overridable Property Text_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
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
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed length of serialized bitmap (ignored in Getter)</param>
        ''' <param name="Fixed"><paramref name="Len"/> represents fixed lenght of serialized bitmap (ignored in Getter)</param>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Fixed"/> is True and <paramref name="Len"/> is 0 (in Setter) -or-
        ''' Bitmap being set has width different form 460 (in Setter) -or-
        ''' Bitmap violates lenght constraint after serialization (in Setter)
        ''' </exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property BW460_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False) As List(Of Drawing.Bitmap)
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
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.Enum_Binary"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Type">Type of neumeration </param>
        ''' <param name="Restrict">Value can be only one of enumerated constants (ignored in Getter)</param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <exception cref="ArgumentException">
        ''' Underlying type of enumeration is neither <see cref="Byte"/>, <see cref="UShort"/>, <see cref="UInteger"/>, <see cref="ULong"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="Integer"/> nor <see cref="Long"/> (in Setter) -or-
        ''' <paramref name="Type"/> is not <see cref="System.Enum"/> (in Setter)</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Restrict"/> is True and value being set is not member of <paramref name="Type"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <exception cref="MissingMethodException">Failed to create instance of given enumeration (in Getter; sohold not occure if norma enumeration is passed to <paramref name="Type"/>)</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property Enum_Binary_Value(ByVal Key As DataSetIdentification, ByVal Type As Type, Optional ByVal Restrict As Boolean = True) As List(Of [Enum])
            Get
                If Type Is Nothing Then Throw New ArgumentNullException("Type", "Type cannot be null")
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of [Enum])(values.Count)
                For Each item As Byte() In values
                    ret.Add(Type.Assembly.CreateInstance(Type.FullName, False, Reflection.BindingFlags.CreateInstance Or Reflection.BindingFlags.Public, Nothing, New Object() {UIntFromBytes(item.Length, item)}, Nothing, Nothing))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of [Enum]))
                If Type Is Nothing Then Throw New ArgumentNullException("Type", "Type cannot be null")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As [Enum] In value
                        If Restrict AndAlso Not Array.IndexOf([Enum].GetValues(Type), value) >= 0 Then Throw New InvalidEnumArgumentException("value", CObj(item), Type)
                        If [Enum].GetUnderlyingType(Type).Equals(GetType(Byte)) Then
                            values.Add(ToBytes(CByte(1), CULng(CObj(item))))
                        ElseIf [Enum].GetUnderlyingType(Type).Equals(GetType(UShort)) Then
                            values.Add(ToBytes(CByte(2), CULng(CObj(item))))
                        ElseIf [Enum].GetUnderlyingType(Type).Equals(GetType(UInteger)) Then
                            values.Add(ToBytes(CByte(4), CULng(CObj(item))))
                        ElseIf [Enum].GetUnderlyingType(Type).Equals(GetType(ULong)) Then
                            values.Add(ToBytes(CByte(8), CULng(CObj(item))))
                        ElseIf [Enum].GetUnderlyingType(Type).Equals(GetType(SByte)) Then
                            values.Add(ToBytes(CByte(1), CLng(CObj(item))))
                        ElseIf [Enum].GetUnderlyingType(Type).Equals(GetType(Short)) Then
                            values.Add(ToBytes(CByte(2), CLng(CObj(item))))
                        ElseIf [Enum].GetUnderlyingType(Type).Equals(GetType(Integer)) Then
                            values.Add(ToBytes(CByte(4), CLng(CObj(item))))
                        ElseIf [Enum].GetUnderlyingType(Type).Equals(GetType(Long)) Then
                            values.Add(ToBytes(CByte(8), CLng(CObj(item))))
                        Else
                            Throw New ArgumentException("Unknown base type of enum")
                        End If
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property


        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.Enum_NumChar"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Type">Type of neumeration </param>
        ''' <param name="Restrict">Value can be only one of enumerated constants (ignored in Getter)</param>
        ''' <param name="Len">Maximal or fixed length of serialized bitmap (ignored in Getter)</param>
        ''' <param name="Fixed"><paramref name="Len"/> represents fixed lenght of serialized bitmap (ignored in Getter)</param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Fixed"/> is True and <paramref name="Len"/> is 0 -or-
        ''' <paramref name="Type"/> is not <see cref="System.Enum"/> (in Setter)</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Restrict"/> is True and value being set is not member of <paramref name="Type"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <exception cref="MissingMethodException">Failed to create instance of given enumeration (in Getter; sohold not occure if norma enumeration is passed to <paramref name="Type"/>)</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property Enum_NumChar_Value(ByVal Key As DataSetIdentification, ByVal Type As Type, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Restrict As Boolean = True) As List(Of [Enum])
            Get
                If Type Is Nothing Then Throw New ArgumentNullException("Type", "Type cannot be null")
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of [Enum])(values.Count)
                For Each item As Byte() In values
                    ret.Add(Type.Assembly.CreateInstance(Type.FullName, False, Reflection.BindingFlags.CreateInstance Or Reflection.BindingFlags.Public, Nothing, New Object() {CDec(NumCharFromBytes(item))}, Nothing, Nothing))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of [Enum]))
                If Len = 0 And Fixed = True Then Throw New ArgumentException("Len cannot be 0 when Fixed is true")
                If Type Is Nothing Then Throw New ArgumentNullException("Type", "Type cannot be null")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As [Enum] In value
                        If Restrict AndAlso Not Array.IndexOf([Enum].GetValues(Type), value) >= 0 Then Throw New InvalidEnumArgumentException("value", CObj(item), Type)
                        values.Add(ToBytes(Len, CDec(CObj(item)), Fixed))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.CCYYMMDD"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <exception cref="ArgumentException">Stored date has lenght different from 8 (in Getter)</exception>
        ''' <exception cref="InvalidCastException">Stored date contains non-number (in Getter)</exception>
        ''' <exception cref="ArgumentOutOfRangeException">Stored date's value of month or day is invalid (i.e. 0 or 13 or more months or 0 or more than valid in month days) (in Getter)</exception>
        <EditorBrowsableAttribute(EditorBrowsableState.Advanced)> _
        Protected Overridable Property CCYYMMDD_Value(ByVal Key As DataSetIdentification) As List(Of Date)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Date)(values.Count)
                For Each item As Byte() In values
                    Dim ItemStr As String = System.Text.Encoding.ASCII.GetString(item)
                    If ItemStr.Length <> 8 Then Throw New ArgumentException("Length of data stored under this tag is different from 8 which is necessary for datatype CCYYMMDD")
                    ret.Add(New Date(ItemStr.Substring(0, 4), ItemStr.Substring(4, 2), ItemStr.Substring(6)))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of Date))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As Date In value
                        values.Add(System.Text.Encoding.ASCII.GetBytes(item.ToString("yyyyMMdd", InvariantCulture)))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.CCYYMMDDOmmitable"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <exception cref="ArgumentException">Stored date ha lenght different from 8 (in Getter)</exception>
        ''' <exception cref="InvalidCastException">Stored date contains non-number (in Getter)</exception>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' Stored date's value of month or day is invalid (i.e. 13 or more months or more than 31 days) (in Getter) -or-
        ''' Date being set is invalid (day is invalid in month context) (in Setter) -or-
        ''' Month or year is ommited when day is not ommited or year is ommited when month or day is not ommited (in Setter)
        ''' </exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property CCYYMMDDOmmitable_Value(ByVal Key As DataSetIdentification) As List(Of OmmitableDate)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of OmmitableDate)(values.Count)
                For Each item As Byte() In values
                    Dim ItemStr As String = System.Text.Encoding.ASCII.GetString(item)
                    If ItemStr.Length <> 8 Then Throw New ArgumentException("Length of data stored under this tag is different from 8 which is necessary for datatype CCYYMMDD")
                    ret.Add(New OmmitableDate(ItemStr.Substring(0, 4), ItemStr.Substring(4, 2), ItemStr.Substring(6)))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of OmmitableDate))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As OmmitableDate In value
                        If item.Year <> 0 AndAlso item.Month <> 0 AndAlso item.Day <> 0 Then
                            Dim dt As New Date(item.Year, item.Month, item.Day)
                        End If
                        If (item.Day <> 0 AndAlso (item.Month = 0 OrElse item.Year = 0)) OrElse (item.Month <> 0 AndAlso item.Year = 0) Then Throw New ArgumentException("If year is ommited also month must be ommited, if month is ommited also day must be ommited")
                        values.Add(System.Text.Encoding.ASCII.GetBytes(item.ToString))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.HHMMSS_HHMM"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">
        ''' Stored time has length different than 11 (in Getter) -or-
        ''' Stored time has something else then + or - on 7th position (in Getter)
        ''' </exception>
        ''' <exception cref="InvalidCastException">Stored time has non-numeric character on any position excepting 7th (in Getter)</exception>
        ''' <exception cref="ArgumentOutOfRangeException">Stored time is out of range of possible values (see <seealso cref="Time"/> for details)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property HHMMSS_HHMM_Value(ByVal Key As DataSetIdentification) As List(Of Time)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Time)(values.Count)
                For Each item As Byte() In values
                    Dim ItemStr As String = System.Text.Encoding.ASCII.GetString(item)
                    If ItemStr.Length <> 11 Then Throw New ArgumentException("Length of data stored under this tag is different then 11 which is necessary for datatype HHMMSS_HHMM")
                    Dim Sig As String = ItemStr(6)
                    If Sig <> "-"c AndAlso Sig <> "+"c Then Throw New ArgumentException("Stored time does not contain valied character on time zone offset sign position")
                    ret.Add(New Time(ItemStr.Substring(0, 2), ItemStr.Substring(2, 2), ItemStr.Substring(4, 2), VisualBasicT.iif(Sig = "+"c, 1, -1) * ItemStr.Substring(7, 2), ItemStr.Substring(9, 2)))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of Time))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As Time In value
                        values.Add(System.Text.Encoding.ASCII.GetBytes(item.ToString))
                    Next item
                End If
            End Set
        End Property
        ''' <summary>Gets or sets value of <see cref="IPTCTypes.Byte_Binary"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        <EditorBrowsable(EditorBrowsableState.Always)> _
        Protected Overridable Property ByteArray_Value(ByVal Key As DataSetIdentification) As List(Of Byte())
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Return values
            End Get
            Set(ByVal value As List(Of Byte()))
                Tag(Key) = value
            End Set
        End Property
        ''' <summary>Gets or sets value of <see cref="IPTCTypes.UNO"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentNullException">Stored value is null or empty (in Getter)</exception>
        ''' <exception cref="ArgumentException">IPR or OVI part of stored value is invalid: contains unallowed charactes (white space, *, :, /, ?), is empty or violates lenght constraint. See <seealso cref="UNO.OVI"/> and <seealso cref="UNO.IPR"/> for more information (in Getter)</exception>
        ''' <exception cref="IndexOutOfRangeException">There is not enough (4) parts separated by : in stored value (in Getter)</exception>
        ''' <exception cref="ArgumentException">UCD component of stored value is to short or contains invalid date (in Getter)</exception>
        ''' <exception cref="InvalidCastException">UCD component odf stored value contains non-numeric character (in Getter)</exception>
        ''' <exception cref="OperationCanceledException">ODE part is invalid. See <seealso cref="UNO.ODE"/> for more information. (in Getter)</exception>
        <EditorBrowsable(EditorBrowsableState.Always)> _
        Protected Overridable Property UNO_Value(ByVal Key As DataSetIdentification) As List(Of UNO)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of UNO)(values.Count)
                For Each item As Byte() In values
                    ret.Add(New UNO(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of UNO))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As UNO In value
                        values.Add(System.Text.Encoding.ASCII.GetBytes(item.ToString))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value of <see cref="IPTCTypes.Num2_Str"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="MaxLenght">Max length of serialized byte array (ignored in getter)</param>
        ''' <param name="Encoding">Encoding for string patr (numeric always uses <see cref="System.Text.Encoding.ASCII"/>). If ommited or null then <see cref="Encoding"/> is used.</param>
        ''' <exception cref="ArgumentException">Serialized value is longer than <paramref name="MaxLenght"/> bytes or serialized numeric part is not of lenght 2 bytes</exception> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property Num2_Str_Value(ByVal Key As DataSetIdentification, Optional ByVal MaxLenght As Integer = 0, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of NumStr2)
            Get 'TODO:Enums?
                If encoding Is Nothing Then encoding = Me.Encoding
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of NumStr2)(values.Count)
                For Each item As Byte() In values
                    Dim itm As New NumStr2
                    itm.Number = System.Text.Encoding.ASCII.GetString(item, 0, 2)
                    itm.String = encoding.GetString(item, 2, item.Length - 2)
                    ret.Add(itm)
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of NumStr2))
                If encoding Is Nothing Then encoding = Me.Encoding
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As NumStr2 In value
                        Dim B1 As Byte() = System.Text.Encoding.ASCII.GetBytes(item.Number.ToString("00", InvariantCulture))
                        Dim B2 As Byte() = Encoding.GetBytes(item.String)
                        If B1.Length + B2.Length > MaxLenght OrElse B1.Length <> 2 Then Throw New ArgumentException("Lenght constraint violation")
                        Dim B3(B1.Length + B2.Length - 1) As Byte
                        B1.CopyTo(B3, 0)
                        B2.CopyTo(B3, 2)
                        values.Add(B3)
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property

        ''' <summary>Gets or sets value of <see cref="IPTCTypes.Num2_Str"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="MaxLenght">Max length of serialized byte array (ignored in getter)</param>
        ''' <param name="Encoding">Encoding for string patr (numeric always uses <see cref="System.Text.Encoding.ASCII"/>). If ommited or null then <see cref="Encoding"/> is used.</param>
        ''' <exception cref="ArgumentException">Serialized value is longer than <paramref name="MaxLenght"/> bytes or serialized numeric part is not of lenght 3 bytes</exception> 
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property Num3_Str_Value(ByVal Key As DataSetIdentification, Optional ByVal MaxLenght As Integer = 0, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of NumStr3)
            Get 'TODO:Enums?
                If Encoding Is Nothing Then Encoding = Me.Encoding
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of NumStr3)(values.Count)
                For Each item As Byte() In values
                    Dim itm As New NumStr2
                    itm.Number = System.Text.Encoding.ASCII.GetString(item, 0, 3)
                    itm.String = Encoding.GetString(item, 3, item.Length - 3)
                    ret.Add(itm)
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of NumStr3))
                If Encoding Is Nothing Then Encoding = Me.Encoding
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As NumStr3 In value
                        Dim B1 As Byte() = System.Text.Encoding.ASCII.GetBytes(item.Number.ToString("000", InvariantCulture))
                        Dim B2 As Byte() = Encoding.GetBytes(item.String)
                        If B1.Length + B2.Length > MaxLenght OrElse B1.Length <> 3 Then Throw New ArgumentException("Lenght constraint violation")
                        Dim B3(B1.Length + B2.Length - 1) As Byte
                        B1.CopyTo(B3, 0)
                        B2.CopyTo(B3, 3)
                        values.Add(B3)
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of <see cref="IPTCTypes.SubjectReference"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Encoding">Encoding used to encode and decode names</param>
        ''' <exception cref="IndexOutOfRangeException">Stored value have more than 5 :-separated parts (in Getter)</exception>
        ''' <exception cref="ArgumentException">Stored value have less then 5 :-separated parts (in Getter)</exception>
        ''' <exception cref="InvalidOperationException">Setting value which's part(s) serializes into byte array of bad lengths (allowed lenghts are 1÷32 for <see cref="SubjectReference.IPR"/>, 8 for <see cref="SubjectReference.SubjectReferenceNumber"/> and 0÷64 for names) (in setter)</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property SubjectReference_Value(ByVal Key As DataSetIdentification, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of SubjectReference)
            Get
                If Encoding Is Nothing Then Encoding = Me.Encoding
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of SubjectReference)(values.Count)
                For Each item As Byte() In values
                    ret.Add(New SubjectReference(item, Encoding))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of SubjectReference))
                If Encoding Is Nothing Then Encoding = Me.Encoding
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As SubjectReference In value
                        values.Add(item.ToBytes(Encoding))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property

        ''' <summary>Gets or sets values(s) of type <see cref="IPTCTypes.Alpha"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed lenght of string value after encoding (ignored in Getter)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <param name="Encoding">Encoding to be used. Is ommited or nothing then <see cref="IPTC.Encoding"/> is used</param>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Len"/> is 0 and <paramref name="Fixed"/> is true (in Setter) -or- 
        ''' One of values being set contains non-alpha character (in setter) -or-
        ''' One of values being set violates <paramref name="Len"/> and/or <paramref name="Fixed"/> constraint after being encoded
        ''' </exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Property Alpha_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
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
                        If Not IsAlpha(item) Then Throw New ArgumentException(String.Format("Item {0} contains non-alpha character"))
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