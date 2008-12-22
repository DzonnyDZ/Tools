Imports Tools.ExtensionsT, System.Linq
#If Config <= Nightly Then 'Stage:Nighlty
'Localize: Needs localization
Namespace PhysicsT
    ''' <summary>Base class for physical units</summary>
    Public MustInherit Class Unit : Implements IEquatable(Of Unit)
        Protected Sub New()
        End Sub
        Public MustOverride ReadOnly Property Name$()
        Public MustOverride ReadOnly Property Mark$()
        Public Overridable Overloads Function Equals(ByVal other As Unit) As Boolean Implements System.IEquatable(Of Unit).Equals
            Return other.Name = Me.Name AndAlso other.Equals(Me)
        End Function
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is Unit Then Return Equals(DirectCast(obj, Unit))
            Return MyBase.Equals(obj)
        End Function
        Public Overrides Function GetHashCode() As Integer
            Return Mark.GetHashCode
        End Function
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    ''' <summary>Physical unit which can be prefixed to denote exponent (or other multiplication)</summary>
    Public MustInherit Class PrefixableUnit : Inherits Unit
        Protected Sub New()
        End Sub
        MustOverride Function GetAllPrefixes() As UnitPrefix()
        Public Overridable Function GetPrefferedPrefixes() As UnitPrefix()
            Return GetAllPrefixes()
        End Function
        ''' <exception cref="ArgumentException">If overriden in derived class may be thrown if <paramref name="Prefix"/> is not within <see cref="GetAllPrefixes"/>.</exception>
        Public Overridable Function GetNameWithPrefix$(ByVal Prefix As UnitPrefix)
            If Prefix Is Nothing Then Return Me.Name
            Return String.Format(Prefix.NameFormat, Prefix.Name, Me.Name)
        End Function
        ''' <exception cref="ArgumentException">If overriden in derived class may be thrown if <paramref name="Prefix"/> is not within <see cref="GetAllPrefixes"/>.</exception>
        Public Overridable Function GetMarkWithPrefix$(ByVal Prefix As UnitPrefix)
            If Prefix Is Nothing Then Return Me.Mark
            Return String.Format(Prefix.MarkFormat, Prefix.Mark, Me.Mark)
        End Function
        Public Overridable Function CreatePrefixedUnit(ByVal Prefix As UnitPrefix) As Unit
            If Prefix Is Nothing Then Return Me
            Return PrefixedUnit.Create(Me, Prefix)
        End Function
    End Class
    ''' <summary>Represents base SI unit</summary>
    Public Class BaseUnit : Inherits PrefixableUnit
        Private ReadOnly _Name$
        Private ReadOnly _Mark$
        Public NotOverridable Overrides ReadOnly Property Mark() As String
            Get
                Return _Mark
            End Get
        End Property
        Public NotOverridable Overrides ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property
        Public Overrides Function GetAllPrefixes() As UnitPrefix()
            Return StandardPrefix.GetPrefixes
        End Function
        Public Overrides Function GetPrefferedPrefixes() As UnitPrefix()
            Return StandardPrefix.GetPrefixes3
        End Function
        Protected Sub New(ByVal Name$, ByVal Mark$)
            _Name = Name
            _Mark = Mark
        End Sub

        Public Shared ReadOnly metre As BaseUnit = metreUnit.metre
        Public Shared ReadOnly kilogram As PrefixedUnit = kilogramUnit.kilogram
        Public Shared ReadOnly second As New BaseUnit("second", "s")
        Public Shared ReadOnly Ampere As New BaseUnit("Ampere", "A")
        Public Shared ReadOnly Kelvin As BaseUnit = kelvinUnit.Kelvin
        Public Shared ReadOnly mole As BaseUnit = moleUnit.mole
        Public Shared ReadOnly candela As New BaseUnit("candela", "cd")

        Private NotInheritable Class metreUnit : Inherits BaseUnit
            Private Sub New()
                MyBase.New("metre", "m")
            End Sub
            Public Overrides Function GetPrefferedPrefixes() As UnitPrefix()
                Return New UnitPrefix() {StandardPrefix.yocto, StandardPrefix.zepto, StandardPrefix.atto, StandardPrefix.femto, StandardPrefix.pico, StandardPrefix.nano, StandardPrefix.micro, StandardPrefix.milli, StandardPrefix.centi, StandardPrefix.deci, StandardPrefix.kilo, StandardPrefix.mega, StandardPrefix.giga, StandardPrefix.tera, StandardPrefix.peta, StandardPrefix.exa, StandardPrefix.zetta, StandardPrefix.yotta}
            End Function
            Public Shared Shadows ReadOnly metre As metreUnit = New metreUnit
        End Class
        Private NotInheritable Class kilogramUnit : Inherits PrefixedUnit
            Private Sub New()
                MyBase.New(gramUnit.gram, StandardPrefix.kilo)
            End Sub
            Public Shared Shadows ReadOnly kilogram As kilogramUnit = New kilogramUnit
        End Class
        Public NotInheritable Class gramUnit : Inherits BaseUnit
            Private Sub New()
                MyBase.New("gram", "g")
            End Sub
            Public Shared Shadows ReadOnly gram As gramUnit = New gramUnit
        End Class
        Private NotInheritable Class kelvinUnit : Inherits BaseUnit
            Private Sub New()
                MyBase.New("Kelvin", "K")
            End Sub
            Public Overrides Function GetPrefferedPrefixes() As UnitPrefix()
                Return New UnitPrefix() {}
            End Function
            Public Shared Shadows ReadOnly Kelvin As kelvinUnit = New kelvinUnit
        End Class
        Private NotInheritable Class moleUnit : Inherits BaseUnit
            Private Sub New()
                MyBase.New("mole", "mol")
            End Sub
            Public Overrides Function GetPrefferedPrefixes() As UnitPrefix()
                Return New UnitPrefix() {StandardPrefix.kilo, StandardPrefix.mega, StandardPrefix.giga, StandardPrefix.tera, StandardPrefix.peta, StandardPrefix.exa, StandardPrefix.zetta, StandardPrefix.yotta}
            End Function
            Public Shared Shadows mole As New moleUnit
        End Class
    End Class

    Public Class CompoundUnit : Inherits Unit
        Private _Units As New Dictionary(Of Unit, Integer)

        Public Overrides ReadOnly Property Mark() As String
            Get
                Dim ret As New System.Text.StringBuilder
                For Each Unit In _Units
                    ret.Append(Unit.Key.Mark)
                    For Each ch In Unit.Value.ToString(Globalization.CultureInfo.InvariantCulture)
                        Select Case ch
                            Case "-"c : ret.Append("⁻"c)
                            Case "0"c : ret.Append("⁰"c)
                            Case "1"c : ret.Append("¹"c)
                            Case "2"c : ret.Append("²"c)
                            Case "3"c : ret.Append("³"c)
                            Case "4"c : ret.Append("⁴"c)
                            Case "5"c : ret.Append("⁵"c)
                            Case "6"c : ret.Append("⁶"c)
                            Case "7"c : ret.Append("⁷"c)
                            Case "8"c : ret.Append("⁸"c)
                            Case "9"c : ret.Append("⁹"c)
                        End Select
                    Next
                Next
                Return ret.ToString
            End Get
        End Property

        Public Overrides ReadOnly Property Name() As String
            Get
                'TODO: Better
                Dim ret As New System.Text.StringBuilder
                For Each Unit In _Units
                    ret.Append(Unit.Key.Name)
                    For Each ch In Unit.Value.ToString(Globalization.CultureInfo.InvariantCulture)
                        Select Case ch
                            Case "-"c : ret.Append("⁻"c)
                            Case "0"c : ret.Append("⁰"c)
                            Case "1"c : ret.Append("¹"c)
                            Case "2"c : ret.Append("²"c)
                            Case "3"c : ret.Append("³"c)
                            Case "4"c : ret.Append("⁴"c)
                            Case "5"c : ret.Append("⁵"c)
                            Case "6"c : ret.Append("⁶"c)
                            Case "7"c : ret.Append("⁷"c)
                            Case "8"c : ret.Append("⁸"c)
                            Case "9"c : ret.Append("⁹"c)
                        End Select
                    Next
                    ret.Append(" * ")
                Next
                Return ret.ToString
            End Get
        End Property
        Public Overrides Function Equals(ByVal other As Unit) As Boolean
            If TypeOf other Is CompoundUnit Then
                With DirectCast(other, CompoundUnit)
                    If _Units.Count <> ._Units.Count Then Return False
                    Dim x = From mu In Me._Units Join ou In ._Units On mu.Value Equals ou.Value And mu.Key Equals ou.Key
                    Return x.Count = _Units.Count
                End With
            Else
                Return MyBase.Equals(other)
            End If
        End Function
        Public Overrides Function GetHashCode() As Integer
            Dim HC As Long
            For Each un In Me._Units
                HC += un.Key.GetHashCode + un.Value.GetHashCode
                If HC > Integer.MaxValue Then HC = HC.Low.BitwiseSame
            Next
            Return HC.Low.BitwiseSame
        End Function
    End Class

    ''' <summary>Represents unit with multiplicative prefix</summary>
    Public Class PrefixedUnit : Inherits Unit
        Private ReadOnly _ParentUnit As PrefixableUnit
        Private Sub New(ByVal Prefix As UnitPrefix, ByVal ParentUnit As PrefixableUnit)
            If ParentUnit Is Nothing Then Throw New ArgumentNullException("ParentUnit")
            _ParentUnit = ParentUnit
            If Prefix IsNot Nothing Then
                Dim OK As Boolean = False
                For Each pfx In ParentUnit.GetAllPrefixes
                    If pfx.Equals(Prefix) Then
                        OK = True
                        Exit For
                    End If
                Next pfx
                If Not OK Then Throw New ArgumentException("Unit does not support given prefix.")
            End If
            _Prefix = Prefix
        End Sub
        Protected Sub New(ByVal ParentUnit As PrefixableUnit, ByVal Prefix As UnitPrefix)
            Me.New(Prefix, ParentUnit)
            If PrefixedUnits.ContainsKey(Me) Then Throw New InvalidOperationException("This unit was already created")
            PrefixedUnits.Add(Me, Me)
        End Sub
        Private Shared PrefixedUnits As New Dictionary(Of PrefixedUnit, PrefixedUnit)

        Public ReadOnly Property ParentUnit() As PrefixableUnit
            Get
                Return _ParentUnit
            End Get
        End Property
        Private ReadOnly _Prefix As UnitPrefix
        Public ReadOnly Property Prefix() As UnitPrefix
            Get
                Return _Prefix
            End Get
        End Property
        Public Overrides ReadOnly Property Mark() As String
            Get
                Return ParentUnit.GetMarkWithPrefix(Prefix)
            End Get
        End Property
        Public Overrides ReadOnly Property Name() As String
            Get
                Return ParentUnit.GetNameWithPrefix(Prefix)
            End Get
        End Property
        Public Shared Function Create(ByVal Unit As PrefixableUnit, ByVal Prefix As UnitPrefix) As PrefixedUnit
            Dim NewUnit As New PrefixedUnit(Prefix, Unit)
            If PrefixedUnits.ContainsKey(NewUnit) Then Return PrefixedUnits(NewUnit)
            PrefixedUnits.Add(NewUnit, NewUnit)
            Return NewUnit
        End Function
        Public Overrides Function Equals(ByVal other As Unit) As Boolean
            If TypeOf other Is PrefixedUnit AndAlso DirectCast(other, PrefixedUnit).Prefix IsNot Nothing Then
                Return Me.ParentUnit.Equals(DirectCast(other, PrefixedUnit).ParentUnit) AndAlso Me.Prefix.Equals(DirectCast(other, PrefixedUnit).Prefix)
            ElseIf TypeOf other Is PrefixedUnit Then
                Return Me.ParentUnit.Equals(DirectCast(other, PrefixedUnit).ParentUnit) AndAlso Me.Prefix Is Nothing
            ElseIf TypeOf other Is PrefixableUnit AndAlso Me.Prefix Is Nothing Then
                Return Me.ParentUnit.Equals(other)
            End If
            Return False
        End Function
        Public Overrides Function GetHashCode() As Integer
            Dim HC = CLng(Prefix.GetHashCode) + CLng(ParentUnit.GetHashCode)
            If HC > Integer.MaxValue Then
                Return (HC.Low Or HC.High).BitwiseSame
            Else
                Return HC
            End If
        End Function
    End Class


    ''' <summary>Unit multiplication prefix</summary>
    ''' <completionlist cref="StandardPrefix"/>
    Public MustInherit Class UnitPrefix : Inherits Unit
        Implements IEquatable(Of UnitPrefix)
        Protected Sub New()
        End Sub
        Public MustOverride ReadOnly Property MultiplicationFactor() As Double
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public MustOverride ReadOnly Property NameFormat() As String
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public MustOverride ReadOnly Property MarkFormat() As String
        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public MustOverride Overloads Function Equals(ByVal other As UnitPrefix) As Boolean Implements System.IEquatable(Of UnitPrefix).Equals
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is UnitPrefix Then Return Equals(DirectCast(obj, UnitPrefix))
            Return MyBase.Equals(obj)
        End Function
    End Class
    ''' <summary>Prefix with standard naming behavior - name prepends unit name with nos space</summary>
    Public MustInherit Class StandardBehaviorPrefix : Inherits UnitPrefix
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public NotOverridable Overrides ReadOnly Property MarkFormat() As String
            Get
                Return "{0}{1}"
            End Get
        End Property
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public NotOverridable Overrides ReadOnly Property NameFormat() As String
            Get
                Return "{0}{1}"
            End Get
        End Property
        Protected Sub New()
        End Sub


    End Class
    ''' <summary>Standard SI unit prefix</summary>
    ''' <completionlist cref="StandardPrefix"/>
    Public NotInheritable Class StandardPrefix : Inherits StandardBehaviorPrefix
        Private _MultiplicationFactor As Double
        Private _PrefixName$
        Private _PrefixMark$
        Private Sub New(ByVal Name$, ByVal Mark$, ByVal Factor As Double)
            _MultiplicationFactor = Factor
            _PrefixName = Name
            _PrefixMark = Mark
        End Sub

        Public Shared ReadOnly yocto As New StandardPrefix("yocto", "y", 1.0E-24)
        Public Shared ReadOnly zepto As New StandardPrefix("zepto", "z", 1.0E-21)
        Public Shared ReadOnly atto As New StandardPrefix("atto", "a", 1.0E-18)
        Public Shared ReadOnly femto As New StandardPrefix("femto", "f", 0.000000000000001)
        Public Shared ReadOnly pico As New StandardPrefix("pico", "p", 0.000000000001)
        Public Shared ReadOnly nano As New StandardPrefix("nano", "n", 0.000000001)
        Public Shared ReadOnly micro As New StandardPrefix("micro", "μ", 0.000001)
        Public Shared ReadOnly milli As New StandardPrefix("milli", "m", 0.001)
        Public Shared ReadOnly centi As New StandardPrefix("centi", "c", 0.01)
        Public Shared ReadOnly deci As New StandardPrefix("deci", "d", 0.1)
        Public Shared ReadOnly deca As New StandardPrefix("deca", "da", 10.0)
        Public Shared ReadOnly hecto As New StandardPrefix("hecto", "h", 100.0)
        Public Shared ReadOnly kilo As New StandardPrefix("kilo", "k", 1000.0)
        Public Shared ReadOnly mega As New StandardPrefix("mega", "M", 1000000.0)
        Public Shared ReadOnly giga As New StandardPrefix("giga", "G", 1000000000.0)
        Public Shared ReadOnly tera As New StandardPrefix("tera", "T", 1000000000000.0)
        Public Shared ReadOnly peta As New StandardPrefix("peta", "P", 1.0E+15)
        Public Shared ReadOnly exa As New StandardPrefix("exa", "E", 1.0E+18)
        Public Shared ReadOnly zetta As New StandardPrefix("zetta", "Z", 1.0E+21)
        Public Shared ReadOnly yotta As New StandardPrefix("yotta", "Y", 1.0E+24)

        Public Shared Function GetPrefix(ByVal Value As Double) As StandardPrefix
            Dim Exponent As Double = Math.Log10(Value)
            If Math.Round(Exponent) <> Exponent Then Throw New ArgumentException("Prefixes can be got only for values that are powers of 10")
            Return GetPrefix(Exponent)
        End Function
        ''' <summary>Gets value indicating if prefix for given exponent is defined</summary>
        ''' <param name="Exponent">Exponent to test</param>
        ''' <returns>True if prefix for <paramref name="Exponent"/> exists and can be obtained via <see cref="GetPrefix"/>. Returns true for zero as well.</returns>
        ''' <remarks>Prefixes are defined for 3-divideable exponsts in ragne -24 to 24 and for any exponent in ragne -3 to 3.</remarks>
        Public Shared Function PrefixExists(ByVal Exponent As Integer) As Boolean
            Return (Exponent >= -24 AndAlso Exponent <= 24 AndAlso Exponent Mod 3 = 0) OrElse (Exponent >= -3 AndAlso Exponent <= 3)
        End Function

        Public Shared Function GetPrefix(ByVal Exponent As Integer) As StandardPrefix
            Select Case Exponent
                Case -24 : Return yocto
                Case -21 : Return zepto
                Case -18 : Return atto
                Case -15 : Return femto
                Case -12 : Return pico
                Case -9 : Return nano
                Case -6 : Return micro
                Case -3 : Return milli
                Case -2 : Return centi
                Case -1 : Return deci
                Case 0 : Return Nothing
                Case 1 : Return deca
                Case 2 : Return hecto
                Case 3 : Return kilo
                Case 6 : Return mega
                Case 9 : Return giga
                Case 12 : Return tera
                Case 15 : Return peta
                Case 18 : Return exa
                Case 21 : Return zetta
                Case 24 : Return yotta
                Case Else : Throw New ArgumentException("Given exponent has no prefix")
            End Select
        End Function

        Public Overrides ReadOnly Property MultiplicationFactor() As Double
            Get
                Return _MultiplicationFactor
            End Get
        End Property

        Public Overrides ReadOnly Property Mark() As String
            Get
                Return _PrefixMark
            End Get
        End Property

        Public Overrides ReadOnly Property Name() As String
            Get
                Return _PrefixName
            End Get
        End Property
        Public Shared Function GetPrefixes() As UnitPrefix()
            Return New UnitPrefix() {yocto, zepto, atto, femto, pico, nano, micro, milli, centi, deci, deca, hecto, kilo, mega, giga, tera, peta, exa, zetta, yotta}
        End Function
        Public Shared Function GetPrefixes3() As UnitPrefix()
            Return New UnitPrefix() {yocto, zepto, atto, femto, pico, nano, micro, milli, kilo, mega, giga, tera, peta, exa, zetta, yotta}
        End Function
        Public Overrides Function Equals(ByVal other As UnitPrefix) As Boolean
            Return TypeOf other Is StandardBehaviorPrefix AndAlso other.Mark = Me.Mark
        End Function
    End Class

End Namespace
#End If