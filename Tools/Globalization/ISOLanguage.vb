Imports System.ComponentModel
Namespace GlobalizationT
#If Config <= Beta Then 'Stage: Beta
    ''' <summary>Represents language represented by ISO 639 language code and provides list of all defined ISO 639-1 and ISO 639-2 language codes</summary>
    ''' <completionlist cref="ISOLanguage"/>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ISOLanguage), LastChMMDDYYYY:="05/28/2007")> _
    Partial Public Class ISOLanguage : Implements IEquatable(Of ISOLanguage)
        ''' <summary>Possible "kinds" of languages</summary>
        Public Enum CodeTypes
            ''' <summary>Marks language as spoken (currently in use, not extinct)</summary>
            Spoken
            ''' <summary>Marks language as formerly spoken, but now dead (extinct)</summary>
            Extinct
            ''' <summary>Marks language as historic variant of current language</summary>
            Historic
            ''' <summary>Marks language as artificial</summary>
            Artificial
            ''' <summary>Marks item as group of languages - not one distinct langauge that can be spoken</summary>
            Group
            ''' <summary>Marks item as special language code with no language meaning</summary>
            Special
            ''' <summary>Marks language code as reserved for local use (applies to codes qaa-qtz, those codes are not listed)</summary>
            Reserved
        End Enum
        ''' <summary>CTor</summary>
        ''' <param name="ISO1">ISO 639-1 (2 characters) code (can be null or an empty <see cref="String"/>)</param>
        ''' <param name="ISO2">ISO 639-2/B (3 characters, primary) code (can be null or an empty <see cref="String"/>)</param>
        ''' <param name="English">English name of language. Cannot be null or an enmpty <see cref="String"/></param>
        ''' <param name="Native">Native name of language. Cannot be null or an empty <see cref="String"/>. If no native name is available use same as <paramref name="English"/></param>
        ''' <param name="Scale">Scale of language - very very approximate number of speakers (but you can put here actual acurate number of course, too)</param>
        ''' <param name="Type">Type of code - whearher it reffers living or exting language or group of languages etc.</param>
        ''' <param name="Duplicate">Duplicate code ISO 639-2/T for <paramref name="ISO2"/></param>
        ''' <remarks>This CTor is used internally by the <see cref="ISOLanguage"/> class. However it is public you'd better consited using another more developper-friendly overloaded CTor</remarks>
        ''' <exception cref="ArgumentException">The code specified in <paramref name="ISO1"/>, <paramref name="ISO2"/> or <paramref name="Duplicate"/> is invalid</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="English"/> or <paramref name="Native"/> is either null or an empty <see cref="String"/></exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Type"/> is not valid <see cref="CodeTypes"/> value</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <CLSCompliant(False)> _
        Public Sub New(ByVal ISO1 As String, ByVal ISO2 As String, ByVal English As String, ByVal Native As String, ByVal Scale As UInteger, ByVal Type As CodeTypes, ByVal Duplicate As String)
            Me.ISO1 = ISO1
            Me.ISO2 = ISO2
            Me.English = English
            Me.Native = Native
            Me.Scale = Scale
            Me.Type = Type
            Me.Duplicate = Duplicate
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="ISO1">ISO 639-1 (2 characters) code (can be null or an empty <see cref="String"/>)</param>
        ''' <param name="ISO2">ISO 639-2/B (3 characters, primary) code (can be null or an empty <see cref="String"/>)</param>
        ''' <param name="English">English name of language. Cannot be null or an enmpty <see cref="String"/></param>
        ''' <param name="Native">Native name of language. Cannot be an empty <see cref="String"/>. If no native name is available use same as <paramref name="English"/>. If ommited (or null passed) that <paramref name="English"/> is used for <see cref="Native"/> automatically.</param>
        ''' <param name="Scale">Scale of language - very very approximate number of speakers (but you can put here actual acurate number of course, too)</param>
        ''' <param name="Type">Type of code - whearher it reffers living or exting language or group of languages etc.</param>
        ''' <param name="Duplicate">Duplicate code ISO 639-2/T for <paramref name="ISO2"/></param>
        ''' <exception cref="ArgumentException">The code specified in <paramref name="ISO1"/>, <paramref name="ISO2"/> or <paramref name="Duplicate"/> is invalid</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="English"/> is is either null or an empty <see cref="String"/> -or- <paramref name="Native"/> is an empty <see cref="String"/> ("")</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Type"/> is not valid <see cref="CodeTypes"/> value</exception>
        <CLSCompliant(False)> _
        Public Sub New(ByVal ISO2 As String, ByVal English As String, Optional ByVal Native As String = Nothing, Optional ByVal ISO1 As String = Nothing, Optional ByVal Type As CodeTypes = CodeTypes.Spoken, Optional ByVal Scale As UInteger = 0, Optional ByVal Duplicate As String = Nothing)
#If VBC_VER >= 9.0 Then
            Me.new(ISO1, ISO2, English, If(Native Is Nothing, English, Native), Scale, Type, Duplicate)
#Else
            Me.new(ISO1, ISO2, English, Tools.VisualBasicT.Interaction.iif(Native Is Nothing, English, Native), Scale, Type, Duplicate)
#End If
        End Sub
        ''' <summary>Contains value of the <see cref="ISO1"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ISO1 As String
        ''' <summary>Gets or sets ISO 639-1 language code</summary>
        ''' <value>New code. It must be 2 characters long <see cref="String"/> containin only lowerase Latin letters or it can be an empty <see cref="String"/> or null. An empty string is converted to null</value>
        ''' <returns>ISO 639-1 language code or null of code is not specified</returns>
        ''' <exception cref="ArgumentException">Setting code to invalid code. Invalid means that it is not null, it is not an empty <see cref="String"/> and it is not 2 characters long <see cref="String"/> of lowercase Latin letters.</exception>
        Public Property ISO1() As String
            Get
                Return _ISO2
            End Get
            Set(ByVal value As String)
                If value = "" Then
                    value = Nothing
                ElseIf Not ValidateCode(value, 2) Then
                    Throw New ArgumentException("ISO 639-1 code must contain exactly 2 lowercase latin letters.")
                End If
                _ISO2 = value
            End Set
        End Property
        ''' <summary>Validates ISO language code</summary>
        ''' <param name="Code">Code to be validated</param>
        ''' <param name="Len">Langth of code that only satisfies validation</param>
        ''' <returns>True if <paramref name="Code"/> is an empty <see cref="String"/>, nothing or contains exactly <paramref name="Len"/> lowercase Latin letters, otherwise false</returns>
        Protected Shared Function ValidateCode(ByVal Code As String, ByVal Len As Byte) As Boolean
            If Code Is Nothing Then Return True
            If Code = "" Then Return True
            If Code.Length <> Len Then Return False
            For Each Ch As Char In Code
                If AscW(Ch) < AscW("a"c) OrElse AscW(Ch) > AscW("z"c) Then Return False
            Next Ch
            Return True
        End Function
        ''' <summary>Contains value of the <see cref="ISO2"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ISO2 As String
        ''' <summary>Gets or sets ISO 639-2 language code</summary>
        ''' <value>New code. It must be 3 characters long <see cref="String"/> containin only lowerase Latin letters or it can be an empty <see cref="String"/> or null. An empty string is converted to null</value>
        ''' <returns>ISO 639-2 language code or null of code is not specified</returns>
        ''' <remarks><see cref="ISO2"/> contains ISO 639-2/B code while <see cref="Duplicate"/> contains ISO 639-2/T code</remarks>
        ''' <exception cref="ArgumentException">Setting code to invalid code. Invalid means that it is not null, it is not an empty <see cref="String"/> and it is not 3 characters long <see cref="String"/> of lowercase Latin letters.</exception>
        Public Property ISO2() As String
            Get
                Return _ISO2
            End Get
            Set(ByVal value As String)
                If value = "" Then
                    value = Nothing
                ElseIf Not ValidateCode(value, 3) Then
                    Throw New ArgumentException("ISO 639-2 code must contain exactly 3 lowercase latin letters.")
                End If
                _ISO2 = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="English"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _English As String
        ''' <summary>Gets or sets English name of laguage</summary>
        ''' <exception cref="ArgumentNullException">Setting value to null or an empty <see cref="String"/></exception>
        Public Property English() As String
            Get
                Return _English
            End Get
            Set(ByVal value As String)
                If value Is Nothing OrElse value = "" Then Throw New ArgumentNullException("English name cannot be null or an empty string")
                _English = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Native"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Native As String
        ''' <summary>Gets or sets native name of laguage</summary>
        ''' <exception cref="ArgumentNullException">Setting value to null or an empty <see cref="String"/></exception>
        ''' <remarks>Native name of language if native name is available. Othervise returns English name (see <seealso cref="English"/>)</remarks>
        ''' <value>New value for native name. If native name is not available use English name. Value cannot be nothing or an empty <see cref="String"/></value>
        Public Property Native() As String
            Get
                Return _Native
            End Get
            Set(ByVal value As String)
                If value Is Nothing OrElse value = "" Then Throw New ArgumentNullException("Native name cannot be null or an empty string")
                _Native = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Scale"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never), CLSCompliant(False)> _
        Private _Scale As UInteger
        ''' <summary>Scale of the language</summary>
        ''' <returns>
        ''' <para>For built-it languages returns very very very approximate number of speakers of this language. Returns 0 for <see cref="CodeTypes.Group"/> and <see cref="CodeTypes.Special"/>. For <see cref="CodeTypes.Historic"/> and <see cref="CodeTypes.Extinct"/> can return non-zero if there is some comunity of speakers of such language. For <see cref="CodeTypes.Artificial"/> returns zero or non-zero depending on wheather there is any community of speakers. For <see cref="CodeTypes.Spoken"/> returns always non-zero. If non-zero is returned it describes the 'size' of the languages rather than actual number of speakers. The number has only first three digits meaningfull, other are zeros. This number should be used only to distinguish between small and big languages (etc.) not tu measure exact number of speakers. Those numbers comes mainly from Wikipedia. If Wikipedia reffers range the average is returned. If Wikipedia reports two numbers for native and non-native speakers, average of native and sum of native and non-native is returned. (Note: There is no on-line binding to wikipedia. Those numbers were got on May 2007.)</para>
        ''' If property was filled from another source (not by property of <see cref="ISOLanguage"/> than exactness and meaning depends on source.
        ''' </returns>
        <CLSCompliant(False)> _
        Public Property Scale() As UInteger
            Get
                Return _Scale
            End Get
            Set(ByVal value As UInteger)
                _Scale = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Type"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Type As CodeTypes
        ''' <summary>Gets or sets type of language (code)</summary>
        ''' <exception cref="InvalidEnumArgumentException">Setting value to unknown type</exception>
        Public Property Type() As CodeTypes
            Get
                Return _Type
            End Get
            Set(ByVal value As CodeTypes)
                If [Enum].GetName(GetType(CodeTypes), value) Is Nothing Then Throw New InvalidEnumArgumentException("value", value, GetType(CodeTypes))
                _Type = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Duplicate"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Duplicate As String
        ''' <summary>Gets or sets duplicate of ISO 639-2 language code</summary>
        ''' <value>New code. It must be 3 characters long <see cref="String"/> containin only lowerase Latin letters or it can be an empty <see cref="String"/> or null. An empty string is converted to null</value>
        ''' <returns>Another ISO 639-2 language code or null of code is not specified for same language</returns>
        ''' <remarks><see cref="ISO2"/> contains ISO 639-2/B code while <see cref="Duplicate"/> contains ISO 639-2/T code</remarks>
        ''' <exception cref="ArgumentException">Setting code to invalid code. Invalid means that it is not null, it is not an empty <see cref="String"/> and it is not 3 characters long <see cref="String"/> of lowercase Latin letters.</exception>
        Public Property Duplicate() As String
            Get
                Return _Duplicate
            End Get
            Set(ByVal value As String)
                If value = "" Then
                    value = Nothing
                ElseIf Not ValidateCode(value, 3) Then
                    Throw New ArgumentException("ISO 639-2 duplicate code must contain exactly 3 lowercase latin letters.")
                End If
                _Duplicate = value
            End Set
        End Property
        ''' <summary>The qaa ISO 639-2 code that represents beginning of range of reserved codes</summary>
        Public Const qaa As String = "qaa"
        ''' <summary>The qtz ISO 639-2 code that represents end of range of reserved codes</summary>
        Public Const qtz As String = "qtz"

        ''' <summary>Gets instance of <see cref="ISOLanguage"/> by the ISO 639-1 or ISO 639-2 language code</summary>
        ''' <param name="code">Code of language to get</param>
        ''' <returns>Instance of <see cref="ISOLanguage"/> that contains description of language represented by code specified or null of such cude is not known</returns>
        ''' <remarks>Works also for codes from reserved range qaa-qtz</remarks>
        ''' <exception cref="ArgumentException"><paramref name="code"/> is valid neither for ISO 639-1 nor for ISO 639-2 code (contains invalid characters or is too long or too short)</exception>
        Public Shared Function GetByCode(ByVal code As String) As ISOLanguage
            If code = "" OrElse code Is Nothing OrElse (Not ValidateCode(code, 2) AndAlso Not ValidateCode(code, 3)) Then
                Throw New ArgumentException("Code is not valid ISO 639 language code", "code")
            End If
            If code.Length = 3 AndAlso String.CompareOrdinal(code, qaa) >= 0 AndAlso String.CompareOrdinal(code, qtz) <= 0 Then
                Return New ISOLanguage(code, "Reserved", "Reserved", , CodeTypes.Reserved)
            Else
                For Each Lng As ISOLanguage In GetAllCodes()
                    If Lng.ISO1 = code OrElse Lng.ISO2 = code OrElse Lng.Duplicate = code Then Return Lng
                Next Lng
                Return Nothing
            End If
        End Function
        ''' <summary>Converts <see cref="String"/> into <see cref="ISOLanguage"/></summary>
        ''' <param name="a"><see cref="String"/> to be converted</param>
        ''' <returns><see cref="ISOLanguage"/> with specified code (same as result of <see cref="GetByCode"/> whan it returns non-null)</returns>
        ''' <exception cref="InvalidCastException"><paramref name="a"/> is unknown code -or- <paramref name="a"/> is invalid code</exception>
        Public Shared Narrowing Operator CType(ByVal a As String) As ISOLanguage
            Dim ret As ISOLanguage
            Try
                ret = GetByCode(a)
            Catch ex As ArgumentException
                Throw New InvalidCastException(String.Format("Cannot convert string {0} to ISOLanguage"), ex)
            End Try
            If ret Is Nothing Then Throw New InvalidCastException(String.Format("Cannot convert string {0} to ISOLanguage"))
            Return ret
        End Operator
        ''' <summary>String representation (code(s) and english name)</summary>
        Public Overrides Function ToString() As String
            Dim ret As New System.Text.StringBuilder
            If ISO1 IsNot Nothing Then ret.Append(ISO1 & " "c)
            If ISO2 IsNot Nothing Then ret.Append(ISO2 & " "c)
            If Duplicate IsNot Nothing Then ret.Append(Duplicate & " "c)
            ret.Append("("c & English & ")"c)
            Return ret.ToString
        End Function
        ''' <summary>Compares two instances of <see cref="ISOLanguage"/></summary>
        ''' <param name="a">A <see cref="ISOLanguage"/> to be compared</param>
        ''' <param name="b">A <see cref="ISOLanguage"/> to be compared</param>
        ''' <returns>True if both instances have same both codes (<see cref="ISO1"/> and <see cref="ISO2"/>). If one of codes of one operand if null then it is ignored. For <see cref="ISO2"/> also <see cref="Duplicate"/> is compared. Result is Ture when at least one pair of cross comparison of both <see cref="ISO2"/>s and <see cref="Duplicate"/>s retuns true. Result is ignored if both <see cref="ISO2"/> and <see cref="Duplicate"/> of one operand are null. If comparison of <see cref="ISO1"/> leads to true and comparison of <see cref="ISO2"/> leads to false (or vice versa) an <see cref="InvalidOperationException"/> is thrown.</returns>
        ''' <exception cref="InvalidOperationException"><see cref="ISO1"/> of both operands are considered equal while <see cref="ISO2"/> not.</exception>
        Public Shared Operator =(ByVal a As ISOLanguage, ByVal b As ISOLanguage) As Boolean
            Dim State As CompState = CompState.Null
            State = Compare(a.ISO1, b.ISO1, State)
            State = Compare(a.ISO2, a.Duplicate, b.ISO2, b.Duplicate, State)
            Return State <> CompState.NotEqual
        End Operator
        ''' <summary>Compares two <see cref="String"/>s while ignoring nulls</summary>
        ''' <param name="a">A <see cref="String"/> to compare</param>
        ''' <param name="b">A <see cref="String"/> to compare</param>
        ''' <param name="State">State of previous comparison</param>
        ''' <returns>New state of comparison</returns>
        ''' <exception cref="InvalidOperationException">State of comparison is abou to be changed when it is <see cref="CompState.Equal"/> or <see cref="CompState.NotEqual"/></exception>
        Private Shared Function Compare(ByVal a As String, ByVal b As String, ByVal State As CompState) As CompState
            If a = b AndAlso a IsNot Nothing Then
                If State = CompState.NotEqual Then Throw New InvalidOperationException("Cannot change state from NotEqual to Equal")
                Return CompState.Equal
            ElseIf a Is Nothing OrElse b Is Nothing Then
                Return State
            Else
                If State = CompState.Equal Then Throw New InvalidOperationException("Cannot change state from Equal to NotEqual")
                Return CompState.NotEqual
            End If
        End Function
        ''' <summary>Compares two pairs of <see cref="String"/> while ignoring nulls</summary>
        ''' <param name="a1">A <see cref="String"/> from first pair to compare</param>
        ''' <param name="a2">A <see cref="String"/> from first pair to compare</param>
        ''' <param name="b1">A <see cref="String"/> from second pair to compare</param>
        ''' <param name="b2">A <see cref="String"/> from second pair to compare</param>
        ''' <param name="State">State of previous comparison</param>
        ''' <returns>New state of comparison</returns>
        ''' <exception cref="InvalidOperationException">State of comparison is abou to be changed when it is <see cref="CompState.Equal"/> or <see cref="CompState.NotEqual"/></exception>
        Private Shared Function Compare(ByVal a1 As String, ByVal a2 As String, ByVal b1 As String, ByVal b2 As String, ByVal State As CompState) As CompState
            If (a1 = b1 AndAlso a1 IsNot Nothing) OrElse (a2 = b2 AndAlso a2 IsNot Nothing) OrElse (a1 = b2 AndAlso b2 IsNot Nothing) OrElse (a2 = b1 AndAlso a2 IsNot Nothing) Then
                If State = CompState.NotEqual Then Throw New InvalidOperationException("Cannot change state from NotEqual to Equal")
                Return CompState.Equal
            ElseIf a1 Is Nothing AndAlso a2 Is Nothing OrElse b1 Is Nothing AndAlso b2 Is Nothing Then
                Return State
            Else
                If State = CompState.Equal Then Throw New InvalidOperationException("Cannot change state from Equal to NotEqual")
                Return CompState.NotEqual
            End If
        End Function
        ''' <summary>States of comparison</summary>
        Private Enum CompState
            ''' <summary>Operand equal</summary>
            Equal
            ''' <summary>Unknown state</summary>
            Null
            ''' <summary>Operands did not equal</summary>
            NotEqual
        End Enum
        ''' <summary>Compares two instances of <see cref="ISOLanguage"/></summary>
        ''' <param name="a">A <see cref="ISOLanguage"/> to be compared</param>
        ''' <param name="b">A <see cref="ISOLanguage"/> to be compared</param>
        ''' <returns>False if both instances have same both codes (<see cref="ISO1"/> and <see cref="ISO2"/>). If one of codes of one operand if null then it is ignored. For <see cref="ISO2"/> also <see cref="Duplicate"/> is compared. Result is Ture when at least one pair of cross comparison of both <see cref="ISO2"/>s and <see cref="Duplicate"/>s retuns true. Result is ignored if both <see cref="ISO2"/> and <see cref="Duplicate"/> of one operand are null. If comparison of <see cref="ISO1"/> leads to true and comparison of <see cref="ISO2"/> leads to false (or vice versa) an <see cref="InvalidOperationException"/> is thrown.</returns>
        ''' <exception cref="InvalidOperationException"><see cref="ISO1"/> of both operands are considered equal while <see cref="ISO2"/> not.</exception>
        Public Shared Operator <>(ByVal a As ISOLanguage, ByVal b As ISOLanguage) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="System.Object"/>.</summary>
        ''' <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="System.Object"/>.</param>
        ''' <returns>true if the specified System.Object is equal to the current <see cref="System.Object"/>; otherwise, false.</returns>
        ''' <remarks>Use type safe overload <see cref="Equals"/> instead. This function uses it internally</remarks>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type safe ovcerload instead")> _
        Public NotOverridable Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is ISOLanguage Then
                Return Equals(DirectCast(obj, ISOLanguage))
            Else
                Return MyBase.Equals(obj)
            End If
        End Function
        ''' <summary>Compares this instance of <see cref="ISOLanguage"/> to another. Unlike the = operator does not throw an exception but rather returns false</summary>
        ''' <param name="other">Another instance to compare this instance with</param>
        ''' <returns>True if instances are considered equal. See the = operator for details</returns>
        Public Overridable Overloads Function Equals(ByVal other As ISOLanguage) As Boolean Implements System.IEquatable(Of ISOLanguage).Equals
            Try
                Return Me = other
            Catch ex As InvalidOperationException
                Return False
            End Try
        End Function
    End Class
#End If
End Namespace