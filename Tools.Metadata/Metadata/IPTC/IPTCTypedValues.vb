Imports Tools.CollectionsT.GenericT, System.Globalization.CultureInfo, Tools.DataStructuresT.GenericT, Tools.ComponentModelT
Imports Tools.MetadataT.IptcT.IptcDataTypes
Imports System.Linq
Imports Tools.TextT.EncodingT

Namespace MetadataT.IptcT
    Partial Public Class Iptc
        ''' <summary>Contains value of the <see cref="Encoding"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Private _encoding As System.Text.Encoding
        ''' <summary>Indicates whether value of the <see cref="Encoding"/> property was set externally or not</summary>
        Private encodingSetExternally As Boolean
        ''' <summary>Encoding used for encoding and decoding some texts (applies only to records 2 - 6 and 8 (see remarks))</summary>
        ''' <value>
        ''' By setting this property to null encoding is reset to default (either <see cref="System.Text.Encoding.[Default]"/> or auto detect from <see cref="CodedCharacterSet"/>) and <see cref="CodedCharacterSet"/> tracing is turned on.
        ''' By setting this property to non-null value <see cref="CodedCharacterSet"/> change-tracking is turned of.
        ''' </value>
        ''' <returns>Current encoding used for decoding string values stored in records 2-6.</returns>
        ''' <remarks>
        ''' <para>
        ''' By setting this property you can change encoding strings are treated as being stored in. Default encoding is <see cref="System.Text.Encoding.[Default]"/>.
        ''' By setting this property <see cref="CodedCharacterSet"/> is NOT changed and existing string properties are NOT converted to a new encoding. Simply bytes of existing string properties are kept unchanged and treated as string stored in a new encoding specified.
        ''' </para>
        ''' <para>
        ''' Until this property is set from ouside of an <see cref="Iptc"/> object it's value is determined by following logic:
        ''' When <see cref="CodedCharacterSet"/> is not specified or not understood <see cref="System.Text.Encoding.[Default]"/> is used.
        ''' When <see cref="CodedCharacterSet"/> is specified and understood appropriate encoding is used.
        ''' When value of <see cref="CodedCharacterSet"/> changes <see cref="Encoding"/> changes as well - either to a new encoding (if it is understood) or to <see cref="System.Text.Encoding.[Default]"/> (if a new encoding is not understood).
        ''' Making change of <see cref="CodedCharacterSet"/> has same affect on string properties as making change of <see cref="Encoding"/> - existing binary values are treated as being under a new encoding.
        ''' </para>
        ''' <para>Currently of a few encodings are understood by <see cref="CodedCharacterSet"/>. 
        ''' Only UTF-8 encodings with ISO 2022 registration numbers 190, 191, 192 and 196 are supported.
        ''' Other encodings are ignored (treated as <see cref="System.Text.Encoding.[Default]"/>).
        ''' </para>
        ''' <para>Once you set value of this property from code automatic tracking of changes of <see cref="CodedCharacterSet"/> stops.</para>
        ''' <para>
        ''' <see cref="CodedCharacterSet"/> also specifies encoding hint for record 8 (<see cref="RecordNumbers.ObjectDataRecord"/>).
        ''' You shall decide yourself whether to use this hint or not when decoding binary data from record 8.
        ''' <see cref="Iptc"/> class does not apply any encoding on binary data in record 8.
        ''' However when text data are read from record 8 encoding is applied same way as in records 2 - 6.
        ''' </para>
        ''' </remarks>
        ''' <seelaso cref="CodedCharacterSet"/><seelaso cref="Tools.TextT.EncodingT.ISO2022.DetectEncoding"/>
        ''' <version version="1.5.3">Support to detect UTF-8 encodings (numbers 190, 191, 192 and 196 according to ISO 2022) from <see cref="CodedCharacterSet"/> added.</version>
        ''' <version version="1.5.3"><see cref="DebuggerStepThroughAttribute"/> removed from getter.</version>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property Encoding() As System.Text.Encoding
            Get
                If _encoding Is Nothing Then
                    If CodedCharacterSet IsNot Nothing AndAlso CodedCharacterSet.Length > 0 Then
                        Dim enc = ISO2022.DefaultInstance.DetectEncoding(CodedCharacterSet)
                        Select Case If(enc Is Nothing, -1, enc.Number)
                            Case 190, 191, 192, 196 : _encoding = New System.Text.UTF8Encoding(False)
                            Case Else : _encoding = System.Text.Encoding.Default
                        End Select
                    Else
                        _encoding = System.Text.Encoding.Default
                    End If
                End If
                Return _encoding
            End Get
            Set(ByVal value As System.Text.Encoding)
                _encoding = value
                encodingSetExternally = True
            End Set
        End Property

#Region "Readers and writers"
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.UnsignedBinaryNumber"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">Value stored in IPTC stream has length neither 1, 2, 4 nor 8 (in Getter)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property UnsignedBinaryNumber_Value(ByVal Key As DataSetIdentification) As List(Of ULong)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of ULong)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then
                        ret.Add(0)
                    Else
                        ret.Add(UIntFromBytes(item.Length, item))
                    End If
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
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property Boolean_Binary_Value(ByVal Key As DataSetIdentification, Optional ByVal Bytes As Byte = 1) As List(Of Boolean)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Boolean)(values.Count)
                For Each item As Byte() In values
                    Dim val As Boolean = False
                    If item IsNot Nothing Then
                        For Each b As Byte In item
                            If b <> 0 Then val = True : Exit For
                        Next b
                    End If
                    ret.Add(val)
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of Boolean))
                If Bytes = 0 Then Throw New ArgumentException(ResourcesT.Exceptions.BytesCannotBe0)
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
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property Byte_Binary_Value(ByVal Key As DataSetIdentification) As List(Of Byte)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Byte)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    ret.Add(UIntFromBytes(item.Length, item))
                Next item
                If ret.Count = 0 Then Return Nothing
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
        <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property UShort_Binary_Value(ByVal Key As DataSetIdentification) As List(Of UShort)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of UShort)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    ret.Add(UIntFromBytes(item.Length, item))
                Next item
                If ret.Count = 0 Then Return Nothing
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

        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.UInt_Binary"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">Value stored in IPTC stream has lenght neither 1, 2, 4 nor 8 (in Getter)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property UInt_Binary_Value(ByVal Key As DataSetIdentification) As List(Of UInt32)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of UInt32)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    ret.Add(UIntFromBytes(item.Length, item))
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(value As List(Of UInt32))
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
        ''' <param name="Len">Maximal or fixed lenght of string (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght (ignored in Getter)</param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <exception cref="InvalidCastException">Cannot convert stored bytes into number (in Getter)</exception>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Len"/> is 0 and <paramref name="Fixed"/> is True (in Setter) -or- Number to be stored is negative (in Setter)</exception>
        ''' <exception cref="LengthConstraintViolationException">Number cannot be stored in given number of bytes (if <paramref name="Len"/> is non-zero and <see cref="IgnoreLenghtConstraints"/> is false, in Setter)</exception>
        ''' <version version="1.5.3">Fix: <paramref name="Len"/> is not enforced in setter.</version>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        ''' <version version="1.5.3"><see cref="LengthConstraintViolationException">Is thrown instead of <see cref="ArgumentException"/> when lenght constraint is violated.</see></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property NumericChar_Value(ByVal key As DataSetIdentification, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False) As List(Of Decimal)
            Get
                Dim values As List(Of Byte()) = Tag(key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Decimal)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then
                        ret.Add(0)
                    Else
                        ret.Add(NumCharFromBytes(item))
                    End If
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of Decimal))
                If Len = 0 And Fixed = True Then Throw New ArgumentException(ResourcesT.Exceptions.LenCannotBe0WhenFixedIsTrue)
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As Decimal In value
                        If item < 0 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CannotBeNegative, "Number"))
                        Dim bytes As Byte() = ToBytes(Len, item, Fixed)
                        If (Len <> 0 AndAlso Not IgnoreLenghtConstraints AndAlso bytes.Length > Len) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New LengthConstraintViolationException(key, Len, Fixed) ' ArgumentException(String.Format(ResourcesT.Exceptions.String0CanotBeStoredWithoutViolatingLengthAndOrFixedConstraint, item))
                        Else
                            values.Add(bytes)
                        End If
                        values.Add(bytes)
                    Next item
                End If
                Tag(key) = values
            End Set
        End Property
        ''' <summary>Gets or sets values(s) of type <see cref="IPTCTypes.GraphicCharacters"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed lenght of string (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <param name="Encoding">Encoding to be used. Is ommited or nothing then <see cref="IPTC.Encoding"/> or default is used</param>
        ''' <exception cref="ArgumentException"><paramref name="Len"/> is 0 and <paramref name="Fixed"/> is true (in Setter) -or-  One of values being set contains non-graphic character (in setter)</exception>
        ''' <exception cref="LengthConstraintViolationException">One of values being set violates <paramref name="Len"/> and/or <paramref name="Fixed"/> constraint after being encoded (not thrown when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true).</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Len"/> is negative (in Setter)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <seelaso cref="Encoding"/>
        ''' <version version="1.5.3">When <paramref name="Encoding"/> is null <see cref="Encoding"/> is used for records 2 - 6 and 8, otherwise <see cref="System.Text.Encoding.ASCII"/> (equal to ISO 646 IRV) is used. (Previous behavior was use <see cref="Encoding"/> whenever <paramref name="Encoding"/> is null. Also note that <see cref="Encoding"/> behavior is changed in 1.5.3.)</version>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        ''' <version version="1.5.3"><see cref="LengthConstraintViolationException">Is thrown instead of <see cref="ArgumentException"/> when lenght constraint is violated.</see></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property GraphicCharacters_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Integer = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
            Get
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of String)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then ret.Add("") _
                    Else ret.Add(Encoding.GetString(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of String))
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                If Len = 0 And Fixed = True Then Throw New ArgumentException(ResourcesT.Exceptions.LenCannotBe0WhenFixedIsTrue)
                If Len < 0 Then Throw New ArgumentOutOfRangeException("Len", String.Format(ResourcesT.Exceptions.CannotBeNegative, "Len"))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As String In value
                        If Not IsGraphicCharacters(item) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.Item0ContainsNonGraphicCharacter))
                        Dim bytes As Byte() = Encoding.GetBytes(item)
                        If (Len <> 0 AndAlso Not IgnoreLenghtConstraints AndAlso bytes.Length > Len) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New LengthConstraintViolationException(Key, Len, Fixed) '(String.Format(ResourcesT.Exceptions.String0CanotBeStoredWithoutViolatingLengthAndOrFixedConstraint, item))
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
        ''' <param name="Len">Maximal or fixed lenght of string (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <param name="Encoding">Encoding to be used. Is ommited or nothing then <see cref="IPTC.Encoding"/> or default is used</param>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Len"/> is 0 and <paramref name="Fixed"/> is true (in Setter) -or-  One of values being set contains non-graphic-non-space character (in setter) -or- <paramref name="Len"/> is negative (in setter)</exception>
        ''' <exception cref="LengthConstraintViolationException">One of values being set violates <paramref name="Len"/> and/or <paramref name="Fixed"/> constraint after being encoded (not thrown when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true.</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <seelaso cref="Tag"/><seelaso cref="Encoding"/><seelaso cref="Text_Value"/>
        ''' <version version="1.5.3">When <paramref name="Encoding"/> is null <see cref="Encoding"/> is used for records 2 - 6 and 8, otherwise <see cref="System.Text.Encoding.ASCII"/> (equal to ISO 646 IRV) is used. (Previous behavior was use <see cref="Encoding"/> whenever <paramref name="Encoding"/> is null. Also note that <see cref="Encoding"/> behavior is changed in 1.5.3.)</version>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        ''' <version version="1.5.3"><see cref="LengthConstraintViolationException">Is thrown instead of <see cref="ArgumentException"/> when lenght constraint is violated.</see></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property TextWithSpaces_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Integer = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
            Get
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of String)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then ret.Add("") _
                    Else ret.Add(Encoding.GetString(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of String))
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                If Len = 0 And Fixed = True Then Throw New ArgumentException(ResourcesT.Exceptions.LenCannotBe0WhenFixedIsTrue)
                If Len < 0 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CannotBeNegative, "Len"))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As String In value
                        If Not IsTextWithSpaces(item) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.Item0ContainsNonGraphicNonSpaceCharacter, item))
                        Dim bytes As Byte() = Encoding.GetBytes(item)
                        If (Len <> 0 AndAlso Not IgnoreLenghtConstraints AndAlso bytes.Length > Len) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New LengthConstraintViolationException(Key, Len, Fixed) 'ArgumentException(String.Format(ResourcesT.Exceptions.String0CanotBeStoredWithoutViolatingLengthAndOrFixedConstraint, item))
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
        ''' <param name="Len">Maximal or fixed lenght of string (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <param name="Encoding">Encoding to be used. Is ommited or nothing then <see cref="IPTC.Encoding"/> or default is used</param>
        ''' <exception cref="ArgumentException"><paramref name="Len"/> is 0 and <paramref name="Fixed"/> is true (in Setter) -or- One of values being set contains non-graphic-non-space-non-cr-non-lf character (in setter)</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Len"/> is negative (in Setter)</exception>
        ''' <exception cref="LengthConstraintViolationException">One of values being set violates <paramref name="Len"/> and/or <paramref name="Fixed"/> constraint after being encoded (not thrown when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true.</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <seelaso cref="Tag"/><seelaso cref="Encoding"/><seelaso cref="TextWithSpaces_Value"/>
        ''' <version version="1.5.3">When <paramref name="Encoding"/> is null <see cref="Encoding"/> is used for records 2 - 6 and 8, otherwise <see cref="System.Text.Encoding.ASCII"/> (equal to ISO 646 IRV) is used. (Previous behavior was use <see cref="Encoding"/> whenever <paramref name="Encoding"/> is null. Also note that <see cref="Encoding"/> behavior is changed in 1.5.3.)</version>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        ''' <version version="1.5.3"><see cref="LengthConstraintViolationException">Is thrown instead of <see cref="ArgumentException"/> when lenght constraint is violated.</see></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property Text_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Integer = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
            Get
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of String)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then ret.Add("") _
                    Else ret.Add(Encoding.GetString(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of String))
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                If Len = 0 And Fixed = True Then Throw New ArgumentException(ResourcesT.Exceptions.LenCannotBe0WhenFixedIsTrue)
                If Len < 0 Then Throw New ArgumentOutOfRangeException("Len", String.Format(ResourcesT.Exceptions.CannotBeNegative, "Len"))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As String In value
                        If Not IsText(item) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.Item0ContainsNonGraphicNonSpaceNonCrNonLfCharacter, item))
                        Dim bytes As Byte() = Encoding.GetBytes(item)
                        If (Len <> 0 AndAlso Not IgnoreLenghtConstraints AndAlso bytes.Length > Len) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New LengthConstraintViolationException(Key, Len, Fixed) 'ArgumentException(String.Format(ResourcesT.Exceptions.String0CanotBeStoredWithoutViolatingLengthAndOrFixedConstraint, item))
                        Else
                            values.Add(bytes)
                        End If
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Bitmap of type <see cref="IPTCTypes.BW460"/> has 460 columns</summary>
        Private Const BW460_460 As Integer = 460
        ''' <summary>Gets or sets value(s) of <see cref="IPTCTypes.BW460"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed lenght of serialized bitmap (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> represents fixed lenght of serialized bitmap (ignored in Getter)</param>
        ''' <exception cref="ArgumentException"><paramref name="Fixed"/> is True and <paramref name="Len"/> is 0 (in Setter) -or- Bitmap being set has width different form 460 (in Setter)</exception>
        ''' <exception cref="LengthConstraintViolationException">Bitmap violates lenght constraint after serialization (in Setter; not thrown when <paramref name="Fixed"/> is true and <see cref="IgnoreLenghtConstraints"/> is false)</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Len"/> is negative (in Setter)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        ''' <version version="1.5.3"><see cref="LengthConstraintViolationException">Is thrown instead of <see cref="ArgumentException"/> when lenght constraint is violated.</see></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property BW460_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Integer = 0, Optional ByVal Fixed As Boolean = False) As List(Of Drawing.Bitmap)
            Get
                If Cache.ContainsKey(Key) Then Return Cache(Key)
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Drawing.Bitmap)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    'Dim ba As New BitArray(item)
                    If (item.Length * 8) Mod BW460_460 <> 0 Then Throw New ArgumentException(ResourcesT.Exceptions.InvalidBitmapNumberOfBitsInBitmapMustBeMultiplicationOf460)
                    Dim bmp As New Drawing.Bitmap(BW460_460, (item.Length * 8) \ BW460_460)
                    'Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(bmp)
                    For i As Integer = 0 To item.Length * 8 - 1 Step BW460_460
                        For j As Integer = i To i + BW460_460 - 1
                            Dim x As Integer = j - i
                            Dim y As Integer = bmp.Height - i / BW460_460 - 1
                            bmp.SetPixel(x, y, If((item(j \ 8) And CByte(2 ^ (7 - j Mod 8))) <> 0, Drawing.Color.Black, Drawing.Color.White))
                        Next j
                    Next i
                    'g.Flush(Drawing.Drawing2D.FlushIntention.Flush)
                    ret.Add(bmp)
                Next item
                If ret.Count = 0 Then Return Nothing
                If Cache.ContainsKey(Key) Then Cache(Key) = ret Else Cache.Add(Key, ret)
                Return ret
            End Get
            Set(ByVal value As List(Of Drawing.Bitmap))
                If Len = 0 And Fixed = True Then Throw New ArgumentException(ResourcesT.Exceptions.LenCannotBe0WhenFixedIsTrue)
                If Len < 0 Then Throw New ArgumentOutOfRangeException("Len", String.Format(ResourcesT.Exceptions.CannotBeNegative, "Len"))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As Drawing.Bitmap In value
                        If item.Width <> BW460_460 Then Throw New ArgumentException(ResourcesT.Exceptions.BitmapWidthMustBe460px)
                        Dim ba As New BitArray(item.Width * item.Height)
                        For i As Integer = 0 To item.Height - 1
                            For j As Integer = 0 To item.Width - 1
                                ba(i * BW460_460 + j) = Not (item.GetPixel(j, item.Height - i - 1) = Drawing.Color.White OrElse item.GetPixel(j, item.Height - i - 1) = Drawing.Color.FromArgb(255, 255, 255, 255))
                            Next j
                        Next i
                        values.Add(Ba2Bytes(ba))
                        Dim BLen As Integer = values(values.Count - 1).Length
                        If (BLen > Len AndAlso Not IgnoreLenghtConstraints AndAlso Len <> 0) OrElse (Fixed AndAlso BLen <> Len) Then _
                            Throw New LengthConstraintViolationException(Key, Len, Fixed) 'ArgumentException(String.Format(ResourcesT.Exceptions.BitmapViolatesLenghtConstraintImageSizeMustBe4600Px, Len * 8 / 460))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Cache for <see cref="BW460_Value"/></summary>
        ''' <remarks>FIlled by <see cref="BW460_Value"/> getter, invalidated by <see cref="OnValueChanged"/></remarks>
        Private Cache As New Dictionary(Of DataSetIdentification, List(Of Drawing.Bitmap))
        ''' <summary>Converts <see cref="BitArray"/> into <see cref="Byte()"/></summary>
        ''' <param name="ba">Bits to be converted</param>
        ''' <returns>Array of <see cref="Byte"/>()</returns>
        Private Function Ba2Bytes(ByVal ba As BitArray) As Byte() 'TODO:Extract as separate tool
            Dim bytes(Math.Ceiling(ba.Length / 8 - 1)) As Byte
            For i As Integer = 0 To ba.Length - 1
                bytes(i \ 8) = bytes(i \ 8) Or If(ba(i), CByte(1), CByte(0)) << (7 - i Mod 8)
            Next i
            Return bytes
        End Function
        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.Enum_Binary"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Type">Type of neumeration </param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <exception cref="ArgumentException">
        ''' Underlying type of enumeration is neither <see cref="Byte"/>, <see cref="UShort"/>, <see cref="UInteger"/>, <see cref="ULong"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="Integer"/> nor <see cref="Long"/> (in Setter) -or-
        ''' <paramref name="Type"/> is not <see cref="System.Enum"/> (in Setter)</exception>
        ''' <exception cref="InvalidEnumArgumentException">Enum is restricted and value being set is not member of <paramref name="Type"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <exception cref="MissingMethodException">Failed to create instance of given enumeration (in Getter; sohold not occure if norma enumeration is passed to <paramref name="Type"/>)</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property Enum_Binary_Value(ByVal Key As DataSetIdentification, ByVal Type As Type) As List(Of [Enum])
            Get
                If Type Is Nothing Then Throw New ArgumentNullException("Type")
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of [Enum])(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    'ret.Add(Type.Assembly.CreateInstance(Type.FullName, False, Reflection.BindingFlags.CreateInstance Or Reflection.BindingFlags.Public, Nothing, New Object() {UIntFromBytes(item.Length, item)}, Nothing, Nothing))
                    'ret.Add(CObj(UIntFromBytes(item.Length, item))) 'Activator.CreateInstance(Type, UIntFromBytes(item.Length, item)))
                    ret.Add([Enum].ToObject(Type, UIntFromBytes(item.Length, item)))
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of [Enum]))
                If Type Is Nothing Then Throw New ArgumentNullException("Type")
                Dim Attrs As Object() = Type.GetCustomAttributes(GetType(RestrictAttribute), False)
                Dim Restrict As Boolean
                If Attrs Is Nothing OrElse Attrs.Length = 0 Then Restrict = True Else Restrict = DirectCast(Attrs(0), RestrictAttribute).Restrict
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As [Enum] In value
                        If Restrict AndAlso Not Array.IndexOf([Enum].GetValues(Type), item) >= 0 Then Throw New InvalidEnumArgumentException("value", CObj(item), Type)
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
                            Throw New ArgumentException(ResourcesT.Exceptions.UnknownBaseTypeOfEnum)
                        End If
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property


        ''' <summary>Gets or sets value(s) of type <see cref="IPTCTypes.Enum_NumChar"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Type">Type of neumeration </param>
        ''' <param name="Len">Maximal or fixed lenght of string (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Fixed"/> is True and <paramref name="Len"/> is 0 -or-
        ''' <paramref name="Type"/> is not <see cref="System.Enum"/> (in Setter)</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Restrict"/> is True and value being set is not member of <paramref name="Type"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <exception cref="MissingMethodException">Failed to create instance of given enumeration (in Getter; sohold not occure if norma enumeration is passed to <paramref name="Type"/>)</exception>
        ''' <exception cref="LengthConstraintViolationException">Encoded length of string violates lenght constraint (not thrown when <paramref name="Fixed"/> is true and <see cref="IgnoreLenghtConstraints"/> is false).</exception>
        ''' <version version="1.5.3">Fix: <paramref name="Len"/> is not enforced in setter.</version>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        ''' <version version="1.5.3"><see cref="LengthConstraintViolationException">Is thrown instead of <see cref="ArgumentException"/> when lenght constraint is violated.</see></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property Enum_NumChar_Value(ByVal Key As DataSetIdentification, ByVal Type As Type, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False) As List(Of [Enum])
            Get
                If Type Is Nothing Then Throw New ArgumentNullException("Type")
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of [Enum])(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    ret.Add(GetEnumValue(Type, NumCharFromBytes(item)))
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of [Enum]))
                If Len = 0 And Fixed = True Then Throw New ArgumentException(ResourcesT.Exceptions.LenCannotBe0WhenFixedIsTrue)
                If Type Is Nothing Then Throw New ArgumentNullException("Type")
                Dim values As New List(Of Byte())
                Dim Attrs As Object() = Type.GetCustomAttributes(GetType(RestrictAttribute), False)
                Dim Restrict As Boolean
                If Attrs Is Nothing OrElse Attrs.Length = 0 Then Restrict = True Else Restrict = DirectCast(Attrs(0), RestrictAttribute).Restrict
                If value IsNot Nothing Then
                    For Each item As [Enum] In value
                        If Restrict AndAlso Not Array.IndexOf([Enum].GetValues(Type), item) >= 0 Then Throw New InvalidEnumArgumentException("value", CObj(item), Type)
                        Dim bytes = ToBytes(Len, CDec(CObj(item)), Fixed)
                        If (Len <> 0 AndAlso Not IgnoreLenghtConstraints AndAlso bytes.Length > Len) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New LengthConstraintViolationException(Key, Len, Fixed) 'ArgumentException(String.Format(ResourcesT.Exceptions.String0CanotBeStoredWithoutViolatingLengthAndOrFixedConstraint, item))
                        Else
                            values.Add(bytes)
                        End If
                        values.Add(bytes)
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
        <EditorBrowsableAttribute(EditorBrowsableState.Advanced)>
        Protected Overridable Property CCYYMMDD_Value(ByVal Key As DataSetIdentification) As List(Of Date)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Date)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    Dim ItemStr As String = System.Text.Encoding.ASCII.GetString(item)
                    If ItemStr.Length <> 8 Then Throw New ArgumentException(ResourcesT.Exceptions.LengthOfDataStoredUnderThisTagIsDifferentFrom8WhichIsNecessaryForDatatypeCCYYMMDD)
                    ret.Add(New Date(ItemStr.Substring(0, 4), ItemStr.Substring(4, 2), ItemStr.Substring(6)))
                Next item
                If ret.Count = 0 Then Return Nothing
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
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property CCYYMMDDOmmitable_Value(ByVal Key As DataSetIdentification) As List(Of OmmitableDate)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of OmmitableDate)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    Dim ItemStr As String = System.Text.Encoding.ASCII.GetString(item)
                    If ItemStr.Length <> 8 Then Throw New ArgumentException(ResourcesT.Exceptions.LengthOfDataStoredUnderThisTagIsDifferentFrom8WhichIsNecessaryForDatatypeCCYYMMDD)
                    ret.Add(New OmmitableDate(ItemStr.Substring(0, 4), ItemStr.Substring(4, 2), ItemStr.Substring(6)))
                Next item
                If ret.Count = 0 Then Return Nothing
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
        ''' Stored time has length different than 11 and 6 (in Getter) -or-
        ''' Stored time has something else then + or - on 7th position (in Getter)
        ''' </exception>
        ''' <exception cref="InvalidCastException">Stored time has non-numeric character on any position excepting 7th (in Getter)</exception>
        ''' <exception cref="ArgumentOutOfRangeException">Stored time is out of range of possible values (see <seealso cref="Time"/> for details; <see cref="M:MetadataT.IptcT.IptcDataTypes.Time.#ctor(System.TimeSpan)"/> when length of stored time is 6)</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <seelaso cref="Tag"/><seelaso cref="HHMMSS_Value"/>
        ''' <version version="1.5.3">In getter: When stored time lenght is 6 <see cref="HHMMSS_Value"/> is returned instead (with zero offset; previously <see cref="ArgumentException"/> used to be thrown).</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property HHMMSS_HHMM_Value(ByVal Key As DataSetIdentification) As List(Of Time)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of Time)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    Dim ItemStr As String = System.Text.Encoding.ASCII.GetString(item)

                    If ItemStr.Length = 6 Then
                        Return (From iitem In HHMMSS_Value(Key) Select New Time(iitem)).ToList
                    End If

                    If ItemStr.Length <> 11 Then Throw New ArgumentException(ResourcesT.Exceptions.LengthOfDataStoredUnderThisTagIsDifferentThen11WhichIsNecessaryForDatatypeHHMMSSHHMM)
                    Dim Sig As String = ItemStr(6)
                    If Sig <> "-"c AndAlso Sig <> "+"c Then Throw New ArgumentException(ResourcesT.Exceptions.StoredTimeDoesNotContainValidCharacterOnTimeZoneOffsetSignPosition)
                    ret.Add(New Time(ItemStr.Substring(0, 2), ItemStr.Substring(2, 2), ItemStr.Substring(4, 2), If(Sig = "+"c, 1, -1) * ItemStr.Substring(7, 2), ItemStr.Substring(9, 2)))
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of Time))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As Time In value
                        values.Add(System.Text.Encoding.ASCII.GetBytes(item.ToString))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value of <see cref="IPTCTypes.Byte_Binary"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed data lenght (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> is fixed length (ignored in Getter)</param>
        ''' <exception cref="ArgumentException"><paramref name="Fixed"/> is True and <paramref name="Len"/> is 0 (in Setter)</exception>
        ''' <exception cref="LengthConstraintViolationException">Lenght of byte array is greater then <paramref name="Len"/> and <paramref name="Len"/> is non-zero or length of byte array differs from <paramref name="Len"/> and <paramref name="Fixed"/> is True (not thrown when <paramref name="Fixed"/> is true and <see cref="IgnoreLenghtConstraints"/> is false).</exception>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        <EditorBrowsable(EditorBrowsableState.Always)>
        Protected Overridable Property ByteArray_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Integer = 0, Optional ByVal Fixed As Boolean = False) As List(Of Byte())
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Return values
            End Get
            Set(ByVal value As List(Of Byte()))
                If Len = 0 AndAlso Fixed Then Throw New ArgumentException(ResourcesT.Exceptions.WhenFixedIsTrueLenCannotBe0)
                For Each item As Byte() In value
                    If (Len <> 0 AndAlso Not IgnoreLenghtConstraints AndAlso Fixed AndAlso item.Length <> Len) OrElse (Not Fixed AndAlso Len <> 0 AndAlso item.Length > Len) Then _
                        Throw New LengthConstraintViolationException(Key, Len, Fixed)
                Next item
                Tag(Key) = value
            End Set
        End Property
        ''' <summary>Gets or sets value of <see cref="IPTCTypes.UNO"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentNullException">Stored value is null or empty (in Getter)</exception>
        ''' <exception cref="ArgumentException">IPR or OVI part of stored value is invalid: contains unallowed charactes (white space, *, :, /, ?), is empty or violates lenght constraint. See <seealso cref="iptcUNO.OVI"/> and <seealso cref="iptcUNO.IPR"/> for more information (in Getter)</exception>
        ''' <exception cref="IndexOutOfRangeException">There is not enough (4) parts separated by : in stored value (in Getter)</exception>
        ''' <exception cref="ArgumentException">UCD component of stored value is to short or contains invalid date (in Getter)</exception>
        ''' <exception cref="InvalidCastException">UCD component odf stored value contains non-numeric character (in Getter)</exception>
        ''' <exception cref="OperationCanceledException">ODE part is invalid. See <seealso cref="iptcUNO.ODE"/> for more information. (in Getter)</exception>
        <EditorBrowsable(EditorBrowsableState.Always)>
        Protected Overridable Property UNO_Value(ByVal Key As DataSetIdentification) As List(Of IptcUno)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of IptcUno)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    ret.Add(New IptcUno(item))
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of IptcUno))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As IptcUno In value
                        values.Add(System.Text.Encoding.ASCII.GetBytes(item.ToString))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value of <see cref="IPTCTypes.Num2_Str"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="MaxLenght">Maximal lenght of serialized byte array (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Encoding">Encoding for string part (numeric should always uses <see cref="System.Text.Encoding.ASCII"/>). If ommited or null then <see cref="Encoding"/> or default is used.</param>
        ''' <exception cref="LengthConstraintViolationException">Serialized value is longer than <paramref name="MaxLenght"/> bytes or serialized numeric part is not of lenght 2 bytes (not thrown for entire lenght when <paramref name="Fixed"/> is true and <see cref="IgnoreLenghtConstraints"/> is false).</exception> 
        ''' <seelaso cref="Encoding"/>
        ''' <version version="1.5.3">When <paramref name="Encoding"/> is null <see cref="Encoding"/> is used for records 2 - 6 and 8, otherwise <see cref="System.Text.Encoding.ASCII"/> (equal to ISO 646 IRV) is used. (Previous behavior was use <see cref="Encoding"/> whenever <paramref name="Encoding"/> is null. Also note that <see cref="Encoding"/> behavior is changed in 1.5.3.)</version>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies.</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property Num2_Str_Value(ByVal Key As DataSetIdentification, Optional ByVal MaxLenght As Integer = 0, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of NumStr2)
            Get
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of NumStr2)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    Dim itm As New NumStr2
                    itm.Number = System.Text.Encoding.ASCII.GetString(item, 0, 2)
                    itm.String = Encoding.GetString(item, 2, item.Length - 2)
                    ret.Add(itm)
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of NumStr2))
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As NumStr2 In value
                        Dim B1 As Byte() = System.Text.Encoding.ASCII.GetBytes(item.Number.ToString("00", InvariantCulture))
                        Dim B2 As Byte() = Encoding.GetBytes(item.String)
                        If (B1.Length + B2.Length > MaxLenght AndAlso Not IgnoreLenghtConstraints) OrElse B1.Length <> 2 Then _
                            Throw New LengthConstraintViolationException(Key, MaxLenght)
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
        ''' <param name="MaxLenght">Maximal lenght of serialized byte array (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Encoding">Encoding for string patr (numeric should always uses <see cref="System.Text.Encoding.ASCII"/>). If ommited or null then <see cref="Encoding"/> or default is used.</param>
        ''' <exception cref="LengthConstraintViolationException">Serialized value is longer than <paramref name="MaxLenght"/> bytes or serialized numeric part is not of lenght 3 bytes (not thrown for entire length when <paramref name="Fixed"/> is true and <see cref="IgnoreLenghtConstraints"/> is false).</exception> 
        ''' <seelaso cref="Encoding"/>
        ''' <version version="1.5.3">When <paramref name="Encoding"/> is null <see cref="Encoding"/> is used for records 2 - 6 and 8, otherwise <see cref="System.Text.Encoding.ASCII"/> (equal to ISO 646 IRV) is used. (Previous behavior was use <see cref="Encoding"/> whenever <paramref name="Encoding"/> is null. Also note that <see cref="Encoding"/> behavior is changed in 1.5.3.)</version>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies.</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property Num3_Str_Value(ByVal Key As DataSetIdentification, Optional ByVal MaxLenght As Integer = 0, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of NumStr3)
            Get
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of NumStr3)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    Dim itm As New NumStr3
                    itm.Number = System.Text.Encoding.ASCII.GetString(item, 0, 3)
                    itm.String = Encoding.GetString(item, 3, item.Length - 3)
                    ret.Add(itm)
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of NumStr3))
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As NumStr3 In value
                        Dim B1 As Byte() = System.Text.Encoding.ASCII.GetBytes(item.Number.ToString("000", InvariantCulture))
                        Dim B2 As Byte() = Encoding.GetBytes(item.String)
                        If (B1.Length + B2.Length > MaxLenght AndAlso Not IgnoreLenghtConstraints) OrElse B1.Length <> 3 Then _
                            Throw New LengthConstraintViolationException(Key, MaxLenght)
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
        ''' <param name="Encoding">Encoding used to encode and decode names. If ommited <see cref="Encoding"/> or default is used.</param>
        ''' <exception cref="IndexOutOfRangeException">Stored value have more than 5 :-separated parts (in Getter)</exception>
        ''' <exception cref="ArgumentException">Stored value have less then 5 :-separated parts (in Getter)</exception>
        ''' <exception cref="InvalidOperationException">Setting value which's part(s) serializes into byte array of bad lengths (allowed lenghts are 1÷32 for <see cref="iptcSubjectReference.IPR"/>, 8 for <see cref="iptcSubjectReference.SubjectReferenceNumber"/> and 0÷64 for names) (in setter)</exception>
        ''' <seelaso cref="Encoding"/>
        ''' <version version="1.5.3">When <paramref name="Encoding"/> is null <see cref="Encoding"/> is used for records 2 - 6 and 8, otherwise <see cref="System.Text.Encoding.ASCII"/> (equal to ISO 646 IRV) is used. (Previous behavior was use <see cref="Encoding"/> whenever <paramref name="Encoding"/> is null. Also note that <see cref="Encoding"/> behavior is changed in 1.5.3.)</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property SubjectReference_Value(ByVal Key As DataSetIdentification, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of IptcSubjectReference)
            Get
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of IptcSubjectReference)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    ret.Add(New IptcSubjectReference(item, Encoding))
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of IptcSubjectReference))
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As IptcSubjectReference In value
                        values.Add(item.ToBytes(Encoding))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property

        ''' <summary>Gets or sets value(s) of <see cref="IPTCTypes.PictureNumber"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">In getter: Value of dataset does not have exactly 15 octets</exception>
        ''' <version version="1.5.4">This property is new in version 1.5.4</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property PictureNumber_Value(ByVal Key As DataSetIdentification) As List(Of IptcPictureNumber)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of IptcPictureNumber)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    ret.Add(New IptcPictureNumber(item))
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(value As List(Of IptcPictureNumber))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As IptcPictureNumber In value
                        values.Add(item.Octets)
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property

        ''' <summary>Gets or sets values(s) of type <see cref="IPTCTypes.Alpha"/></summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <param name="Len">Maximal or fixed lenght of string (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <param name="Encoding">Encoding to be used. Is ommited or nothing then <see cref="IPTC.Encoding"/> or default is used</param>
        ''' <exception cref="ArgumentException"><paramref name="Len"/> is 0 and <paramref name="Fixed"/> is true (in Setter) -or- One of values being set contains non-alpha character (in setter)</exception>
        ''' <exception cref="LengthConstraintViolationException">One of values being set violates <paramref name="Len"/> and/or <paramref name="Fixed"/> constraint after being encoded (not thrown when <paramref name="Fixed"/> is true and <see cref="IgnoreLenghtConstraints"/> is false).</exception>
        ''' <remarks><seealso cref="Tag"/> for behavior details</remarks>
        ''' <seelaso cref="Encoding"/>
        ''' <version version="1.5.3">When <paramref name="Encoding"/> is null <see cref="Encoding"/> is used for records 2 - 6 and 8, otherwise <see cref="System.Text.Encoding.ASCII"/> (equal to ISO 646 IRV) is used. (Previous behavior was use <see cref="Encoding"/> whenever <paramref name="Encoding"/> is null. Also note that <see cref="Encoding"/> behavior is changed in 1.5.3.)</version>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        ''' <version version="1.5.3"><see cref="LengthConstraintViolationException">Is thrown instead of <see cref="ArgumentException"/> when lenght constraint is violated.</see></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Property Alpha_Value(ByVal Key As DataSetIdentification, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False, Optional ByVal Encoding As System.Text.Encoding = Nothing) As List(Of String)
            Get
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of String)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then ret.Add("") _
                    Else ret.Add(Encoding.GetString(item))
                Next item
                Return ret
            End Get
            Set(ByVal value As List(Of String))
                If Encoding Is Nothing Then
                    Select Case Key.RecordNumber
                        Case 2 To 6, 8 : Encoding = Me.Encoding
                        Case Else : Encoding = System.Text.Encoding.ASCII
                    End Select
                End If
                If Len = 0 And Fixed = True Then Throw New ArgumentException(ResourcesT.Exceptions.LenCannotBe0WhenFixedIsTrue)
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As String In value
                        If Not IsAlpha(item) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.Item0ContainsNonAlphaCharacter, item))
                        Dim bytes As Byte() = Encoding.GetBytes(item)
                        If (Len <> 0 AndAlso Not IgnoreLenghtConstraints AndAlso bytes.Length > Len) OrElse (Fixed AndAlso bytes.Length <> Len) Then
                            Throw New LengthConstraintViolationException(Key, Len, Fixed) ' ArgumentException(String.Format(ResourcesT.Exceptions.String0CanotBeStoredWithoutViolatingLengthAndOrFixedConstraint, item))
                        Else
                            values.Add(bytes)
                        End If
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value of <see cref="IPTCTypes.StringEnum"/> type</summary>
        ''' <param name="Key">Record or dataset number</param>
        ''' <param name="Type">Type of enum in value</param>
        ''' <param name="Len">Maximal or fixed lenght of string after encoding (ignored in Getter, and in setter when <paramref name="Fixed"/> is false and <see cref="IgnoreLenghtConstraints"/> is true; 0 for no limit)</param>
        ''' <param name="Fixed"><paramref name="Len"/> determines fixed lenght instead of maximal if True (ignored in Getter)</param>
        ''' <exception cref="InvalidEnumArgumentException"><see cref="P:Tools.MetadataT.IptcT.StringEnum.EnumType"/> has no <see cref="RestrictAttribute"/> or it has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> set to true and value is not member of <see cref="P:Tools.MetadataT.IptcT.StringEnum.EnumType"/> (in Setter)</exception>
        ''' <exception cref="ArrayTypeMismatchException"><see cref="P:Tools.MetadataT.IptcT.StringEnum.EnumType"/> differs from <paramref name="Type"/> (in setter)</exception>
        ''' <exception cref="ArgumentException">
        ''' Error while creating generic instance - caused by wrong <paramref name="Type"/> (in Getter) -or-
        ''' Stored value contains invalid character (non-graphic-non-space-non-ASCII) (in getter) -or-
        ''' <paramref name="Fixed"/> is true and <paramref name="Len"/> is 0
        ''' </exception>
        ''' <exception cref="LengthConstraintViolationException">Value violates length constaraint after serialization (in Setter, not thrown when <paramref name="Fixed"/> is true and <see cref="IgnoreLenghtConstraints"/> is false)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <version version="1.5.3"><see cref="IgnoreLenghtConstraints"/> applies when <paramref name="Fixed"/> is false.</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Property StringEnum_Value(ByVal Key As DataSetIdentification, ByVal Type As Type, Optional ByVal Len As Byte = 0, Optional ByVal Fixed As Boolean = False) As List(Of StringEnum)
            Get
                If Type Is Nothing Then Throw New ArgumentNullException("Type")
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of StringEnum)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then
                        Dim attr = Type.GetAttribute(Of RestrictAttribute)()
                        If attr Is Nothing OrElse Not attr.Restrict Then 'Restricted enum can never be an empty string
                            Try
                                ret.Add(StringEnum.GetInstance(Type, ""))
                            Catch ex As InvalidEnumArgumentException : End Try
                        End If
                    Else
                        Dim str As String = System.Text.Encoding.ASCII.GetString(item)
                        ret.Add(StringEnum.GetInstance(Type, str))
                    End If
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of StringEnum))
                If Len = 0 And Fixed = True Then Throw New ArgumentException(ResourcesT.Exceptions.LenCannotBe0WhenFixedIsTrue)
                If Type Is Nothing Then Throw New ArgumentNullException("Type")
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As StringEnum In value
                        If Not item.GetType.IsGenericType OrElse Not Type.Equals(item.GetType.GetGenericArguments(0)) Then Throw New ArrayTypeMismatchException(ResourcesT.Exceptions.EnumTypeOfItemsPassedToStringEnumValueMustBeSameAsThatInTheTypeParameter)
                        Dim Attrs As Object() = item.EnumType.GetCustomAttributes(GetType(RestrictAttribute), False)
                        Dim ra As RestrictAttribute = Nothing
                        If Attrs IsNot Nothing AndAlso Attrs.Length > 0 Then ra = Attrs(0)
                        If Not item.ContainsEnum AndAlso (ra Is Nothing OrElse ra.Restrict) Then Throw New InvalidEnumArgumentException(ResourcesT.Exceptions.ThisEnumerationDoesNotAllowValuesThatAreNotMemberOfIt)
                        Dim Bytes As Byte() = System.Text.Encoding.ASCII.GetBytes(item.StringValue)
                        If (Fixed AndAlso Bytes.Length <> Len) OrElse (Len <> 0 AndAlso Not IgnoreLenghtConstraints AndAlso Bytes.Length > Len) Then _
                            Throw New LengthConstraintViolationException(Key, Len, Fixed)
                        values.Add(Bytes)
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of <see cref="IPTCTypes.ImageType"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">
        ''' Stored value has length different than 2B (in Getter) -or-
        ''' 2nd byte of stored value cannot be interpreted as <see cref="ImageTypeContents"/> (in Getter)
        ''' </exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Property ImageType_Value(ByVal Key As DataSetIdentification) As List(Of IptcImageType)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of IptcImageType)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    Dim val As New IptcImageType
                    If item.Length <> 2 Then Throw New ArgumentException(ResourcesT.Exceptions.StoredValueHasInvalidLenght)
                    val.Components = CStr(ChrW(item(0)))
                    val.TypeCode = ChrW(item(1))
                    ret.Add(val)
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of IptcImageType))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As IptcImageType In value
                        values.Add(System.Text.Encoding.ASCII.GetBytes(item.ToString))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property

        ''' <summary>Gets or sets value(s) of <see cref="IPTCTypes.ImageType"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">
        ''' Stored value has length different than 2B (in Getter) -or-
        ''' 2nd byte of stored value cannot be interpreted as <see cref="AudioDataType"/> (in Getter) -or-
        ''' Setting value which's serializatazion produes more or less than 2 bytes
        ''' </exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Property AudioType_Value(ByVal Key As DataSetIdentification) As List(Of IptcAudioType)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of IptcAudioType)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    Dim val As New IptcAudioType
                    If item.Length <> 2 Then Throw New ArgumentException(ResourcesT.Exceptions.StoredValueHasInvalidLenght)
                    val.Components = CStr(ChrW(item(0)))
                    val.TypeCode = CStr(ChrW(item(1)))
                    ret.Add(val)
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of IptcAudioType))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As IptcAudioType In value
                        Dim Bytes As Byte() = System.Text.Encoding.ASCII.GetBytes(item.ToString)
                        If Bytes.Length <> 2 Then Throw New ArgumentException(ResourcesT.Exceptions.SerializedValueHasNotLength2Bytes)
                        values.Add(Bytes)
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
        ''' <summary>Gets or sets value(s) of <see cref="IPTCTypes.HHMMSS"/> type</summary>
        ''' <param name="Key">Record and dataset number</param>
        ''' <exception cref="ArgumentException">Stored item's length differs from 6 (in Getter)</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><see cref="TimeSpan"/> to be stored is less than <see cref="TimeSpan.Zero"/> or it's <see cref="TimeSpan.TotalDays"/> is greater than or equal to 1 (in setter)</exception>
        ''' <exception cref="InvalidCastException">Stored item contains non-numeric character (in Getter)</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Property HHMMSS_Value(ByVal Key As DataSetIdentification) As List(Of TimeSpan)
            Get
                Dim values As List(Of Byte()) = Tag(Key)
                If values Is Nothing OrElse values.Count = 0 Then Return Nothing
                Dim ret As New List(Of TimeSpan)(values.Count)
                For Each item As Byte() In values
                    If item Is Nothing OrElse item.Length = 0 Then Continue For
                    If item.Length <> 6 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.StoredItemLenghtMustBe0, 6))
                    Dim Str As String = System.Text.Encoding.ASCII.GetString(item)
                    ret.Add(New TimeSpan(Str.Substring(0, 2), Str.Substring(2, 2), Str.Substring(4, 2)))
                Next item
                If ret.Count = 0 Then Return Nothing
                Return ret
            End Get
            Set(ByVal value As List(Of TimeSpan))
                Dim values As New List(Of Byte())
                If value IsNot Nothing Then
                    For Each item As TimeSpan In value
                        If item < TimeSpan.Zero OrElse item.TotalDays > 1 Then Throw New ArgumentOutOfRangeException(ResourcesT.Exceptions.TimeMustBeNonNegativeAndLessThen1Day)
                        values.Add(System.Text.Encoding.ASCII.GetBytes(String.Format("{0:00}{1:00}{2:00}", item.Hours, item.Minutes, item.Seconds)))
                    Next item
                End If
                Tag(Key) = values
            End Set
        End Property
#Region "Helpers"
        ''' <summary>Converts <see cref="List(Of [Enum])"/> into <see cref="List"/> of any <see cref="[Enum]"/></summary>
        ''' <param name="From"><see cref="List(Of [Enum])"/> to be converted</param>
        ''' <typeparam name="TEnum">Type of <see cref="[Enum]"/></typeparam>
        Private Shared Function ConvertEnumList(Of TEnum As {IConvertible, Structure})(ByVal From As List(Of [Enum])) As List(Of TEnum)
            If From Is Nothing Then Return Nothing
            Dim ret As New List(Of TEnum)(From.Count)
            For Each item As [Enum] In From
                ret.Add(CObj(item))
            Next item
            Return ret
        End Function
        ''' <summary>Converts <see cref="List"/> of any <see cref="[Enum]"/> into <see cref="List(Of [Enum])"/></summary>
        ''' <param name="From"><see cref="List"/> to be converted</param>
        ''' <typeparam name="TEnum">Type of <see cref="[Enum]"/></typeparam>
        Private Shared Function ConvertEnumList(Of TEnum As {IConvertible, Structure})(ByVal From As List(Of TEnum)) As List(Of [Enum])
            If From Is Nothing Then Return Nothing
            Dim ret As New List(Of [Enum])(From.Count)
            For Each item As TEnum In From
                'ret.Add(CObj(item))
                ret.Add([Enum].ToObject(GetType(TEnum), item))
            Next item
            Return ret
        End Function
        ''' <summary>Converts <see cref="List"/> of <see cref="NumStr"/> to <see cref="List"/> of another <see cref="NumStr"/> that drives from first one</summary>
        ''' <param name="From"><see cref="List"/> to be converted</param>
        ''' <typeparam name="TNumStr1">Type of items in <paramref name="From"/></typeparam>
        ''' <typeparam name="TNumStr2">Type of items in return value</typeparam>
        Private Shared Function ConvertNumStrList(Of TNumStr1 As NumStr, TNumStr2 As {TNumStr1, New})(ByVal From As List(Of TNumStr1)) As List(Of TNumStr2)
            If From Is Nothing Then Return Nothing
            Dim ret As New List(Of TNumStr2)(From.Count)
            For Each item As TNumStr1 In From
                Dim NewNumStr As New TNumStr2
                NewNumStr.Number = item.Number
                NewNumStr.String = item.String
                ret.Add(NewNumStr)
            Next item
            Return ret
        End Function
        ''' <summary>Converts <see cref="List"/> of <see cref="NumStr"/> that derives from another <see cref="NumStr"/> to list of that another <see cref="NumStr"/></summary>
        ''' <param name="From"><see cref="List"/> to be converted</param>
        ''' <typeparam name="TNumStr1">Type of items in return value</typeparam>
        ''' <typeparam name="TNumStr2">Type of item in <paramref name="From"/></typeparam>
        Private Shared Function ConvertNumStrList(Of TNumStr1 As NumStr, TNumStr2 As TNumStr1)(ByVal From As List(Of TNumStr2)) As List(Of TNumStr1)
            If From Is Nothing Then Return Nothing
            Dim ret As New List(Of TNumStr1)(From.Count)
            For Each item As TNumStr2 In From
                ret.Add(item)
            Next item
            Return ret
        End Function

        ''' <summary>Converts <see cref="List(Of [Enum])"/> into <see cref="List"/> of any <see cref="[Enum]"/></summary>
        ''' <param name="From"><see cref="List(Of [Enum])"/> to be converted</param>
        ''' <typeparam name="TEnum">Type of <see cref="[Enum]"/></typeparam>
        Private Shared Function ConvertEnumList(Of TEnum As {IConvertible, Structure})(ByVal From As List(Of StringEnum)) As List(Of StringEnum(Of TEnum))
            If From Is Nothing Then Return Nothing
            Dim ret As New List(Of StringEnum(Of TEnum))(From.Count)
            For Each item As StringEnum In From
                ret.Add(item)
            Next item
            Return ret
        End Function
        ''' <summary>Converts <see cref="List"/> of any <see cref="[Enum]"/> into <see cref="List(Of [Enum])"/></summary>
        ''' <param name="From"><see cref="List"/> to be converted</param>
        ''' <typeparam name="TEnum">Type of <see cref="[Enum]"/></typeparam>
        Private Shared Function ConvertEnumList(Of TEnum As {IConvertible, Structure})(ByVal From As List(Of StringEnum(Of TEnum))) As List(Of StringEnum)
            If From Is Nothing Then Return Nothing
            Dim ret As New List(Of StringEnum)(From.Count)
            For Each item As StringEnum(Of TEnum) In From
                ret.Add(item)
            Next item
            Return ret
        End Function
#End Region
#End Region


        ''' <summary>Gets or sets value indicating wheather this instance of <see cref="Iptc"/> allows to save limited variable-length values when value being set exceeds maximum allowed lenght of the property.</summary>
        ''' <value>True to allow saving variable-lenght values with lenght exceeding lenght constraint; false to throw <see cref="LengthConstraintViolationException"/>.</value>
        ''' <remarks>It's always possible to read values violating lenght constraints.</remarks>
        ''' <version version="1.5.3">This property is new in version 1.5.3</version>
        Public Property IgnoreLenghtConstraints As Boolean
    End Class
    ''' <summary>Exception thrown when length constraint of string- or byte-oriented IPTC property is violated (and such violation is not allowed)</summary>
    Public Class LengthConstraintViolationException : Inherits ArgumentException
        ''' <summary>CTor from property name, maximal allowed lenght (in bytes) and fixed indicator</summary>
        ''' <param name="PropertyName">Name of property setting of which caused the exception (see <see cref="PropertyName"/>)</param>
        ''' <param name="MaximalLength">Maximal length (in bytes) allowed for property value (see <see cref="MaximalLength"/></param>
        ''' <param name="Fixed">True when <paramref name="MaximalLength"/> is also  inimal length (in bytes) allowed for property value (and thus the only allowed lenght) (see <see cref="Fixed"/>)</param>
        Public Sub New(ByVal PropertyName$, ByVal MaximalLength%, Optional ByVal Fixed As Boolean = False)
            MyBase.New(ResourcesT.Exceptions.LenghtConstraintViolation & " " & String.Format(If(Fixed, MetadataT.IptcT.IptcResources.StringValueOfTheProperty0MustBe1BytesLong, MetadataT.IptcT.IptcResources.StringValueOfTheProperty0CanBeMaximally1BytesLong), PropertyName, MaximalLength), "value")
            _PropertyName = PropertyName
            _MaximalLength = MaximalLength
            _Fixed = Fixed
        End Sub
        ''' <summary>CTor from <see cref="DataSetIdentification"/>, maximal allowed lenght (in bytes) and fixed indicator</summary>
        ''' <param name="Property">DataSet (IPTC property) which caused the exception (see <see cref="PropertyName"/>)</param>
        ''' <param name="MaximalLength">Maximal length (in bytes) allowed for property value (see <see cref="MaximalLength"/></param>
        ''' <param name="Fixed">True when <paramref name="MaximalLength"/> is also  inimal length (in bytes) allowed for property value (and thus the only allowed lenght) (see <see cref="Fixed"/>)</param>
        Public Sub New(ByVal [Property] As DataSetIdentification, ByVal MaximalLength%, Optional ByVal fixed As Boolean = False)
            MyBase.New(ResourcesT.Exceptions.LenghtConstraintViolation & " " & String.Format(
                       If(fixed,
                          IptcResources.StringValueOfTheProperty0MustBe1BytesLong,
                          IptcResources.StringValueOfTheProperty0CanBeMaximally1BytesLong),
                        [Property].DisplayName & "(" & CInt([Property].RecordNumber) & ":" & [Property].DatasetNumber, MaximalLength))
            _PropertyName = [Property].PropertyName
            _MaximalLength = MaximalLength
            _Fixed = fixed
        End Sub
        ''' <summary>Contains value of the <see cref="PropertyName"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _PropertyName$
        ''' <summary>Contains value of the <see cref="MaximalLength"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _MaximalLength%
        ''' <summary>Contains value of the <see cref="Fixed"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _Fixed As Boolean
        ''' <summary>Name of property setting of which caused the exception</summary>
        Public ReadOnly Property PropertyName$()
            <DebuggerStepThrough()> Get
                Return _PropertyName
            End Get
        End Property
        ''' <summary>Maximal length (in bytes) allowed for property value</summary>
        Public ReadOnly Property MaximalLength%()
            <DebuggerStepThrough()> Get
                Return _MaximalLength
            End Get
        End Property
        ''' <summary>True when <see cref="MaximalLength"/> is also  inimal length (in bytes) allowed for property value (and thus the only allowed lenght)</summary>
        Public ReadOnly Property Fixed() As Boolean
            <DebuggerStepThrough()> Get
                Return _Fixed
            End Get
        End Property
    End Class
End Namespace