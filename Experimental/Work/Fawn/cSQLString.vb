'===============================================================================
' Name: CSQLString
' Purpose: Tøída se postará o pohodlné naplnìní INSERT SQL pøíkazu. Pokud potøebujeme
' vytvoøit složitìjší INSERT SQL pøíkaz plnìný na více místech programu, pak je dobré použít
' tuto Tøídu. Použití:<br>
'Dim mmm As New CSQLString<br>
'mmm.setTable = "tabulka"<br>
'Call mmm.add("pavel", "333", "N")<br>
'Call mmm.add("emil", "333", "T")<br>
'Call mmm.add("datum", "1.3.2005", "D")<br>
'Print mmm.getSQLinsert<br>
'Set mmm = Nothing<br>
' Functions:
'
' Properties:
'
' Methods:


Public Class cSQLString


    Private Class cItemSQL
        Public Sub New(ByVal col As String, ByVal val As String)
            column = col
            value = val
        End Sub
        Public column As String
        Public value As String
    End Class

    Private mTabulka As String
    Private mlist As List(Of cItemSQL)

    Public Sub New()
        mlist = New List(Of cItemSQL)
    End Sub

    Public Property setTableName() As String
        Get
            Return mTabulka
        End Get
        Set(ByVal value As String)
            mTabulka = value
        End Set
    End Property

    Public Sub add(ByVal promenna As String, ByVal hodnota As String)
        mlist.Add(New cItemSQL(promenna, "'" & hodnota & "'"))
    End Sub

    Public Sub add(ByVal promenna As Double, ByVal hodnota As String)
        'todo + konverze do formátu
    End Sub

    Public Sub add(ByVal promenna As Date, ByVal hodnota As String)
        'todo + konverze do formátu
    End Sub

    Public Sub add(ByVal promenna As Integer, ByVal hodnota As String)
        'todo + konverze do formátu
    End Sub

    Public Function getSQLinsert() As String

        Dim strSQL As String
        Dim strValues As String
        Dim mSQLitem As cItemSQL

        If mlist.Count = 0 Then
            getSQLinsert = ""
            Exit Function
        End If

        If mTabulka = "" Then
            Call MsgBox("Nemùžu udìlat insert, nebyla zvolena tabulka!")
            getSQLinsert = ""
            Exit Function
        End If


        strSQL = "insert into " & mTabulka & "("

        strValues = "("

        For Each mSQLitem In mlist
            strSQL = strSQL & mSQLitem.column & ","
            strValues = strValues & mSQLitem.value & ","
        Next

        strValues = Left(strValues, Len(strValues) - 1) & ")"
        strSQL = Left(strSQL, Len(strSQL) - 1) & ")"

        strSQL = strSQL & " values " & strValues

        'dostava se z ikarosu a nefunguje dobre potom
        getSQLinsert = Strings.Replace(strSQL, Chr(0), "")

    End Function

End Class



