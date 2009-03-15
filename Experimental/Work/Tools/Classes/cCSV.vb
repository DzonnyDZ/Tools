Imports System
Imports System.IO
Imports System.Text

''' <summary>Cíls cvs zápisu</summary>
Public Interface ICsvTarget
    ''' <summary>Pødat text</summary>
    ''' <param name="str">text k pøidání</param>
    Sub Append(ByVal str$)
    ''' <summary>Ukonèit zápis</summary>
    Sub [End]()
End Interface

''' <summary>Cíl csv zápisu do <see cref="StringBuilder">StringBuilderu</see></summary>
Public Class StringBuilderTarget
    Implements ICsvTarget
    ''' <summary>Cíl zápisu</summary>
    Private sb As New StringBuilder
    ''' <summary>Pødat text</summary>
    ''' <param name="str">text k pøidání</param>
    Public Sub Append(ByVal str As String) Implements ICsvTarget.Append
        sb.Append(str)
    End Sub
    ''' <summary>Získá zapsaný text</summary>
    ''' <returns>Zapsaný text</returns>
    Public Overrides Function ToString() As String
        Return sb.ToString
    End Function
    ''' <summary>Ukonèit zápis</summary>
    Public Sub [End]() Implements ICsvTarget.End
        'Do nothing
    End Sub
End Class

''' <summary>Cíl zápisu do souboru</summary>
Public Class FileTarget
    Implements ICsvTarget
    ''' <summary>Zapisuje do souboru</summary>
    Dim sv As IO.StreamWriter
    ''' <summary>CTor</summary>
    ''' <param name="File">Adresa souboru</param>
    ''' <param name="Encoding">Kódování</param>
    Public Sub New(ByVal File As String, ByVal Encoding As System.Text.Encoding)
        Dim fs As IO.FileStream = IO.File.Open(File, FileMode.Create, FileAccess.Write)
        sv = New IO.StreamWriter(fs, Encoding)
    End Sub
    ''' <summary>Pødat text</summary>
    ''' <param name="str">text k pøidání</param>
    Public Sub Append(ByVal str As String) Implements ICsvTarget.Append
        sv.Write(str)
    End Sub
    ''' <summary>Ukonèit zápis</summary>
    Public Sub [End]() Implements ICsvTarget.End
        sv.Flush()
        sv.Close()
    End Sub
End Class
''' <summary>Generátor CSV</summary>
Public Class cCSV
    ''' <summary>Obsahuje hodntu vlastnosti <see cref="Separator"/></summary>
    Dim _separator As Char
    ''' <summary>Cíl zapisování</summary>
    Dim Target As ICsvTarget
    ''' <summary>Jsem na nové øádce?</summary>
    Dim newLine As Boolean = True
    ''' <summary>Už se nìco zapsalo?</summary>
    Private Written As Boolean = False
    ''' <summary>CTor</summary>
    ''' <param name="Target">Cíl zapisování</param>
   Public Sub New(ByVal Target As ICsvTarget)
        separator = ";"
        newLine = False
        Me.Target = Target
        LinesColCounts.Add(0)
    End Sub

    ''' <summary>Pøidává hodnoty do správného tvaru "value";"value"; ...</summary>
    ''' <param name="value">Hodnota</param>
    ''' <param name="NumericType">True pokud je typ èíslo nebo datum</param>
    Public Sub addField(ByVal value As Object, Optional ByVal NumericType As Boolean = False)
        Dim uvozovky As Boolean = Not NumericType
        Dim val As String
        If value Is Nothing Then
            val = ""
        ElseIf TypeOf value Is IFormattable Then
            val = DirectCast(value, IFormattable).ToString("G", Globalization.CultureInfo.InvariantCulture)
        Else
            val = value.ToString
        End If
        Call writeValue(val, uvozovky)
    End Sub

   

    ''' <summary>Zapíše hodnotu do výstupu</summary>
    Private Sub writeValue(ByVal strVal As String, ByVal ForceQuote As Boolean)
        strVal = strVal.Trim
        If strVal.Contains(Me.separator) OrElse strVal.Contains(""""c) Then
            Dim b As New System.Text.StringBuilder
            b.Append("""")
            For Each ch As Char In strVal
                If ch = """"c Then
                    b.Append("""""")
                Else
                    b.Append(ch)
                End If
            Next
            b.Append("""")
            strVal = b.ToString
        ElseIf ForceQuote Then : strVal = """" & strVal & """"
        End If

        Select Case Written
            Case False : Target.Append(strVal)
            Case Else
                If newLine = True Then
                    Target.Append(strVal)
                    newLine = False
                Else : Target.Append(separator & strVal)
                End If
        End Select
        Written = True
        LinesColCounts(LinesColCounts.Count - 1) += 1
    End Sub
    ''' <summary>Oddìlovaè</summary>
    Public Property Separator() As Char
        Get
            Return _separator
        End Get
        Set(ByVal value As Char)
            _separator = value
        End Set
    End Property

    Public Sub startLine()
        If Not newLine Then
            Call endLine()
        End If
    End Sub
    ''' <summary>Poèty sloupcù v øádcích</summary>
    Private LinesColCounts As New List(Of Integer)

    ' Nový øádek
    Public Sub endLine()
        Target.Append(vbCrLf)
        newLine = True
        LinesColCounts.Add(0)
    End Sub

    Public Shared Function VerifyFileName(ByVal Jmeno As String) As String
        'FileN = Jmeno
        'zkontroluje jméno souboru
        If File.Exists(Jmeno) = True Then
            Jmeno = NewFileN(Jmeno)
            Return ("ERROR: Soubor již existuje. Existující soubor je pøejmenován na " & Jmeno)
        End If
        Return ""
    End Function


    Public Function endCSV() As String
        Target.End()
        Dim chyba As String = ""
        Dim RetStr As String = ""

        chyba = KontrolaSloupcu()
        If chyba <> "" Then RetStr = "ERROR: Nesprávný poèet sloupcù v øádcích:" & vbCrLf & chyba & vbCrLf

        'Zapíše vše
        'My.Computer.FileSystem.WriteAllText(FileN, Target.ToString, True, Encoding.UTF8)

        'RetStr += "VYTVOØEN SOUBOR " & FileN

        Return RetStr
    End Function

    'Kontrola, zda je v každém øádku stejný poèet sloupcù
    Private Function KontrolaSloupcu() As String
        Dim ColCount As Integer = -1
        Dim Chyba$ = ""
        Dim i%
        For Each LineColCount As Integer In LinesColCounts
            If ColCount - 1 Then
                ColCount = LineColCount
            ElseIf ColCount <> LineColCount Then
                Chyba = IIf(Chyba = "", i + 1, Chyba & ", " & i + 1)
            End If
            i += 1
        Next
        Return Chyba
    End Function

    Private Shared Function NewFileN(ByVal FileN As String) As String
        Dim bezKoncovky As String = FileN.Replace(".csv", "")
        Dim NovyNazev As String = ""

        Dim i As Integer = 1
        Do
            i += 1
            NovyNazev = bezKoncovky & i & ".csv"
        Loop While File.Exists(NovyNazev) = True

        File.Copy(FileN, NovyNazev)
        File.Delete(FileN)

        Return NovyNazev

    End Function

End Class
