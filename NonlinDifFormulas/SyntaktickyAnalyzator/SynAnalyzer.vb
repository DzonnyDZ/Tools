Public Class SynAnalyzer

    Class WrongSyntaxException
        Inherits ApplicationException
        Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
    End Class

    Dim Lex As Lexan
    Dim Expresion As ExprInterf
    Sub New(ByVal Program As String, ByVal FunctionsArr As IEnumerable(Of Funct), ByVal VarArr As IEnumerable(Of String))
        Functions = New MyGeneric.KeyedDictionary(Of String, Funct)(System.StringComparer.CurrentCultureIgnoreCase)
        For Each f As Funct In FunctionsArr
            Functions.Add(f)
        Next

        LocalVar = New Generic.Dictionary(Of String, Val)(System.StringComparer.CurrentCultureIgnoreCase)
        For Each v As String In VarArr
            LocalVar.Add(v, New Val())
        Next
        Lex = New Lexan(Program)
        Expresion = Vyraz()
    End Sub
    Function Calculate(ByVal VarVal As IEnumerable(Of KeyValuePair(Of String, Double))) As Double
        For Each vv As KeyValuePair(Of String, Double) In VarVal
            LocalVar(vv.Key).Value = vv.Value
        Next
        Return Expresion.Calculate.Value
    End Function

    Public Interface Funct
        Inherits MyGeneric.KeyAble(Of String)
        Function Run(ByVal params As List(Of Val)) As Val
        ReadOnly Property HasNumParam(ByVal count As Integer) As Boolean
    End Interface
    Class Funct1Param
        Implements Funct




        Delegate Function OneParamDelegate(ByVal x As Double) As Double
        Dim d As OneParamDelegate, n As String
        Sub New(ByVal name As String, ByVal del As OneParamDelegate)
            d = del
            n = name
        End Sub

        Public Function Run(ByVal params As System.Collections.Generic.List(Of Val)) As Val Implements Funct.Run
            If Not Me.HasNumParam(params.Count) Then Throw New WrongSyntaxException(String.Format(My.Resources.Analyzer.InvalidParamCount, Key, 1, params.Count))
            Return New Val(d.Invoke(params(0).Value))
        End Function

        Public ReadOnly Property HasNumParam(ByVal count As Integer) As Boolean Implements Funct.HasNumParam
            Get
                Return count = 1
            End Get
        End Property

        Public ReadOnly Property Key() As String Implements MyGeneric.KeyAble(Of String).Key
            Get
                Return n
            End Get
        End Property
    End Class
    Dim Functions As MyGeneric.KeyedDictionary(Of String, Funct)
    Dim LocalVar As Generic.Dictionary(Of String, Val)

    Private Function Vyraz() As ExprInterf
        Return Scit()
    End Function

    Private Function ZbScit(ByVal du As ExprInterf) As ExprInterf
        Select Case Lex.Symb.Type
            Case Lexan.RetSymb.LexSymbol.PLUS
                Lex.ReadNext()
                Return ZbScit(New Bop(Bop.OperatorTyp.Plus, du, Term()))
            Case Lexan.RetSymb.LexSymbol.MINUS
                Lex.ReadNext()
                Return ZbScit(New Bop(Bop.OperatorTyp.Minus, du, Term()))
            Case Else
                Return du
        End Select
    End Function

    Private Function Scit() As ExprInterf
        Return ZbScit(Term())
    End Function

    Private Function Term() As ExprInterf
        Return ZbTermu(Umoc())
    End Function

    Private Function ZbTermu(ByVal du As ExprInterf) As ExprInterf
        Select Case Lex.Symb.Type
            Case Lexan.RetSymb.LexSymbol.TIMES
                Lex.ReadNext()
                Return ZbTermu(New Bop(Bop.OperatorTyp.Times, du, Umoc()))
            Case Lexan.RetSymb.LexSymbol.DIVIDE
                Lex.ReadNext()
                Return ZbTermu(New Bop(Bop.OperatorTyp.Divide, du, Umoc()))
            Case Else
                Return du
        End Select
    End Function

    Private Function Umoc() As ExprInterf
        Return ZbUmoc(Faktor())
    End Function

    Private Function ZbUmoc(ByVal du As ExprInterf) As ExprInterf
        Select Case Lex.Symb.Type
            Case Lexan.RetSymb.LexSymbol.CARET
                Lex.ReadNext()
                Return ZbUmoc(New Bop(Bop.OperatorTyp.Raise, du, Faktor()))
            Case Else
                Return du
        End Select
    End Function

    Private Function Faktor() As ExprInterf
        If Lex.Symb.Type = Lexan.RetSymb.LexSymbol.MINUS Then
            Lex.ReadNext()
            Return New UnMinus(Kons())
        End If
        Return Kons()
    End Function

    Private Function Kons() As ExprInterf
        Select Case Lex.Symb.Type
            Case Lexan.RetSymb.LexSymbol.IDENT
                Dim id As String = Lex.Symb.Ident
                Lex.ReadNext()
                If Lex.Symb.Type = Lexan.RetSymb.LexSymbol.LPAR Then
                    'volani funkce
                    Dim Funct As Funct = Nothing
                    Functions.TryGetValue(id, Funct)
                    If Funct Is Nothing Then Throw New WrongSyntaxException(String.Format(My.Resources.Analyzer.InvalidFunctionName, id))
                    Lex.ReadNext()
                    Dim Params As New List(Of ExprInterf)
                    If Lex.Symb.Type <> Lexan.RetSymb.LexSymbol.RPAR Then
                        Do
                            Params.Add(Vyraz())
                            If Lex.Symb.Type = Lexan.RetSymb.LexSymbol.RPAR Then Exit Do
                            Lex.CompSymb(Lexan.RetSymb.LexSymbol.COMMA)
                        Loop
                    End If
                    Lex.ReadNext()
                    Return New Calling(Funct, Params)
                Else
                    Dim var As Val = Nothing
                    LocalVar.TryGetValue(id, var)
                    If var Is Nothing Then Throw New WrongSyntaxException(String.Format(My.Resources.Analyzer.InvalidVariableName, id))
                    Return var
                End If
            Case Lexan.RetSymb.LexSymbol.LPAR
                Lex.ReadNext()
                Dim su As ExprInterf = Vyraz()
                Lex.CompSymb(Lexan.RetSymb.LexSymbol.RPAR)
                Return su
            Case Lexan.RetSymb.LexSymbol.NUMBER
                Dim v As Val = New Val(Lex.Symb.Number)
                Lex.ReadNext()
                Return v
            Case Else
                Throw New WrongSyntaxException(String.Format(My.Resources.Analyzer.InvalidSymbol, Lexan.RetSymb.GetSybmText(Lex.Symb.Type)))
        End Select
    End Function


    Private Class Lexan
        Class RetSymb
            Dim typ As LexSymbol
            Dim iden As String
            Dim num As Double
            Enum LexSymbol
                IDENT
                NUMBER
                PLUS
                MINUS
                TIMES
                DIVIDE
                'EQ
                'NEQ
                'LT,GT,LTE,GTE
                LPAR
                RPAR
                COMMA
                CARET
                'ENDL
                'kwAND,kwDIM, kwDO, kwELSE, kwELSEIF, kwEND, kwFOR, kwFUNCTION,kwIF,kwLOOP,kwNEXT,kwOR,kwSTEP,kwSUB,kwTHEN,kwTO,kwWHILE,
                EOT
            End Enum
            Shared Function GetSybmText(ByVal symb As LexSymbol) As String
                Select Case symb
                    Case LexSymbol.IDENT
                        Return "Iden"
                    Case LexSymbol.NUMBER
                        Return "Number"
                    Case LexSymbol.PLUS
                        Return "+"
                    Case LexSymbol.MINUS
                        Return "-"
                    Case LexSymbol.TIMES
                        Return "*"
                    Case LexSymbol.DIVIDE
                        Return "/"
                        'EQ,NEQ,LT,GT,LTE,GTE
                    Case LexSymbol.LPAR
                        Return "("
                    Case LexSymbol.RPAR
                        Return ")"
                    Case LexSymbol.COMMA
                        Return ","
                    Case LexSymbol.CARET
                        Return "^"
                    Case LexSymbol.EOT
                        Return "Konec výrazu"
                        'ENDL
                    Case Else
                        Return "Unknown symbol(description not found)"
                End Select
            End Function

            Sub New(ByVal type As LexSymbol)
                Select Case type
                    Case LexSymbol.IDENT, LexSymbol.NUMBER
                        Throw New ArgumentException("Ident and Number types cannot be initialized via standard constructor")
                    Case Else
                        typ = type
                End Select
            End Sub
            Sub New(ByVal number As Double)
                typ = LexSymbol.NUMBER
                num = number
            End Sub
            Sub New(ByVal ident As String)
                typ = LexSymbol.IDENT
                iden = ident
            End Sub
            Public ReadOnly Property Number() As Double
                Get
                    If typ <> LexSymbol.NUMBER Then
                        Throw New InvalidOperationException("Cannot call Ident on non ident type " & System.Enum.GetName(GetType(LexSymbol), typ))
                    End If
                    Return num
                End Get
            End Property
            Public ReadOnly Property Ident() As String
                Get
                    If typ <> LexSymbol.IDENT Then
                        Throw New InvalidOperationException("Cannot call Ident on non ident type " & System.Enum.GetName(GetType(LexSymbol), typ))
                    End If
                    Return iden
                End Get
            End Property
            Public ReadOnly Property Type() As LexSymbol
                Get
                    Return typ
                End Get
            End Property
        End Class
        Private Class Letter
            Dim chr As Char, typ As LetterType
            Sub New(ByVal inp As Char)
                chr = inp
                Select Case chr
                    Case "("c
                        typ = LetterType.LeftParahensis
                    Case ")"c
                        typ = LetterType.RightParahensis
                    Case ","c
                        typ = LetterType.Comma
                    Case "+"c
                        typ = LetterType.Plus
                    Case "-"c
                        typ = LetterType.Minus
                    Case "*"c
                        typ = LetterType.Star
                    Case "/"c
                        typ = LetterType.Slash
                    Case "^"c
                        typ = LetterType.Caret
                    Case "."c
                        typ = LetterType.Dot
                    Case "'"c
                        typ = LetterType.Apostroph
                    Case Else
                        If Char.IsDigit(chr) Then
                            typ = LetterType.Digit
                        ElseIf Char.IsLetter(chr) Or chr = "["c Or chr = "]"c Then
                            typ = LetterType.Letter
                        ElseIf Char.IsWhiteSpace(chr) Then
                            typ = LetterType.WhiteSpace
                        Else
                            typ = LetterType.Unknown
                        End If
                End Select
            End Sub
            Private Sub New()
            End Sub
            Public ReadOnly Property Character() As Char
                Get
                    If typ = LetterType.EOT Then
                        Throw New NotSupportedException("Letter type EOF doesn't have character")
                    End If
                    Return chr
                End Get
            End Property
            Public ReadOnly Property Type() As LetterType
                Get
                    Return typ
                End Get
            End Property
            Enum LetterType
                Digit
                WhiteSpace
                Letter
                LeftParahensis
                RightParahensis
                Comma
                Minus
                Plus
                Star
                Slash
                Caret
                Dot
                Apostroph
                Unknown
                EOT
            End Enum
            Public Shared Function EofLetter() As Letter
                Dim l As New Letter
                l.typ = LetterType.EOT
                Return l
            End Function
        End Class
        Private Class InputProgReader
            Dim prog As String
            Dim ActLetter As Letter
            Dim ActPos As Integer
            Sub New(ByVal Program As String)
                prog = Program
                ReadNext()
            End Sub
            Sub ReadNext()
                If ActLetter IsNot Nothing AndAlso ActLetter.Type = Lexan.Letter.LetterType.EOT Then
                    Throw New ApplicationException("Pokus o cteni za konec progrumu")
                End If
                If ActPos = prog.Length Then
                    ActLetter = Letter.EofLetter
                Else
                    ActLetter = New Letter(prog.Chars(ActPos))
                    ActPos += 1
                End If
            End Sub
            Public ReadOnly Property Letter() As Letter
                Get
                    Return ActLetter
                End Get
            End Property
        End Class
        Dim Input As InputProgReader
        Sub New(ByVal Program As String)
            Input = New InputProgReader(Program)
            ReadNext()
        End Sub
        Dim ActSymb As RetSymb
        Sub ReadNext()
            If ActSymb IsNot Nothing AndAlso ActSymb.Type = RetSymb.LexSymbol.EOT Then
                Throw New ApplicationException("Pokus o cteni za konec progrumu")
            End If
            ActSymb = CtiSymb()
        End Sub
        Public ReadOnly Property Symb() As RetSymb
            Get
                Return ActSymb
            End Get
        End Property
        Sub CompSymb(ByVal typ As RetSymb.LexSymbol)
            If Symb.Type <> typ Then
                Throw New WrongSyntaxException(String.Format(My.Resources.Analyzer.InvalidSyntaxExpected, Lexan.RetSymb.GetSybmText(typ), Lexan.RetSymb.GetSybmText(Symb.Type)))
            End If
            ReadNext()
        End Sub

        Private Function CtiSymb() As RetSymb
            While (Input.Letter.Type = Letter.LetterType.WhiteSpace)
                Input.ReadNext()
            End While

            Select Case Input.Letter.Type
                Case Letter.LetterType.EOT
                    Return New RetSymb(RetSymb.LexSymbol.EOT)

                Case Letter.LetterType.Apostroph
                    'Komentar ignorujeme, az do konce radku
                    Input.ReadNext()
                    While Input.Letter.Type = Letter.LetterType.EOT
                        Input.ReadNext()
                    End While
                    Return New RetSymb(RetSymb.LexSymbol.EOT)

                    'symb.typ=ENDL;
                    'return;

                    'case """:
                    '   input.readnext;
                    '   delkaId = 0;
                    '   for(;;) {
                    '     if (input.letter==""") {
                    '       input.readnext;
                    '       if (input.letter==""") {
                    '                     Ident([delkaId] = """; delkaId++;")
                    '         input.readnext;
                    '       } else {
                    '         Ident[delkaId] = 0;
                    '         symb.typ=VAL;
                    '         symb.param=new Val(Ident);
                    '         return;
                    '       }
                    '     } else if (input.letter=="\n" || input.letter=="e") {
                    '       chyba("neocekavany konec uvnitr retezce");
                    '     } else {
                    '       Ident[delkaId] = Znak; delkaId++;
                    '       input.readnext;
                    '     }
                    '   }
                Case Letter.LetterType.Letter
                    Dim ide As String = Input.Letter.Character
                    Input.ReadNext()
                    While (Input.Letter.Type = Letter.LetterType.Letter Or Input.Letter.Type = Letter.LetterType.Digit)
                        ide = ide & Input.Letter.Character
                        Input.ReadNext()
                    End While
                    Return New RetSymb(ide)

                Case Letter.LetterType.Digit
                    Dim cislo As Double = Char.GetNumericValue(Input.Letter.Character)

                    Input.ReadNext()
                    While (Input.Letter.Type = Letter.LetterType.Digit)
                        cislo = 10 * cislo + Char.GetNumericValue(Input.Letter.Character)
                        Input.ReadNext()
                    End While
                    If Input.Letter.Type = Letter.LetterType.Dot Then
                        Input.ReadNext()
                        Dim Place As Double = 1
                        While (Input.Letter.Type = Letter.LetterType.Digit)
                            Place = Place / 10
                            cislo = cislo + Char.GetNumericValue(Input.Letter.Character) * Place
                            Input.ReadNext()
                        End While
                    End If
                    If Input.Letter.Type = Letter.LetterType.Letter AndAlso Char.ToUpper(Input.Letter.Character) = "E"c Then
                        Input.ReadNext()
                        Dim exp As Integer, ExpIsNegative As Boolean = False
                        If Input.Letter.Type = Letter.LetterType.Minus Then
                            ExpIsNegative = True
                            ReadNext()
                        End If
                        While (Input.Letter.Type = Letter.LetterType.Digit)
                            exp = 10 * exp + Char.GetNumericValue(Input.Letter.Character)
                            Input.ReadNext()
                        End While
                        If ExpIsNegative Then
                            cislo = cislo * 10 ^ -exp
                        Else
                            cislo = cislo * 10 ^ exp
                        End If
                    End If
                    Return New RetSymb(cislo)

                Case Letter.LetterType.Comma
                    Dim s As RetSymb = New RetSymb(RetSymb.LexSymbol.COMMA)
                    Input.ReadNext()
                    Return s
                Case Letter.LetterType.Plus
                    Dim s As RetSymb = New RetSymb(RetSymb.LexSymbol.PLUS)
                    Input.ReadNext()
                    Return s
                Case Letter.LetterType.Minus
                    Dim s As RetSymb = New RetSymb(RetSymb.LexSymbol.MINUS)
                    Input.ReadNext()
                    Return s
                Case Letter.LetterType.Star
                    Dim s As RetSymb = New RetSymb(RetSymb.LexSymbol.TIMES)
                    Input.ReadNext()
                    Return s
                Case Letter.LetterType.Slash
                    Dim s As RetSymb = New RetSymb(RetSymb.LexSymbol.DIVIDE)
                    Input.ReadNext()
                    Return s
                Case Letter.LetterType.Caret
                    Dim s As RetSymb = New RetSymb(RetSymb.LexSymbol.CARET)
                    Input.ReadNext()
                    Return s

                    '          Case "="
                    'input.readnext;
                    'symb.typ=EQ;
                    'return;
                    '          Case "<"
                    'input.readnext;
                    'switch (input.letter) {
                    '          Case ">"
                    '    input.readnext;
                    '    symb.typ=NEQ;
                    '    return;
                    '          Case "="
                    '    input.readnext;
                    '    symb.typ=LTE;
                    '    return;
                    'default:
                    '    symb.typ=LT;
                    '    return;
                    '}
                    '          Case ">"
                    'input.readnext;
                    'if (input.letter=="=") {
                    '    input.readnext;
                    '    symb.typ=GTE;
                    '    return;
                    '} else {
                    '    symb.typ=GT;
                    '    return;
                    '}
                Case Letter.LetterType.LeftParahensis
                    Dim s As RetSymb = New RetSymb(RetSymb.LexSymbol.LPAR)
                    Input.ReadNext()
                    Return s
                Case Letter.LetterType.RightParahensis
                    Dim s As RetSymb = New RetSymb(RetSymb.LexSymbol.RPAR)
                    Input.ReadNext()
                    Return s
                Case Letter.LetterType.Dot
                    Throw New WrongSyntaxException(String.Format(My.Resources.Analyzer.InvalidSymbol, Input.Letter.Character.ToString))
                Case Letter.LetterType.Unknown
                    Throw New WrongSyntaxException(String.Format(My.Resources.Analyzer.InvalidCharacter, Input.Letter.Character.ToString))
                Case Else
                    Throw New ApplicationException("Neodchyceny typ znaku v analyzatoru")
            End Select
        End Function

    End Class


    Private Interface ExprInterf
        Function Calculate() As Val
    End Interface

    Public Class Val
        Implements ExprInterf

        Dim val As Nullable(Of Double)
        Public Property Value() As Double
            Get
                If val.HasValue = False Then Throw New ArgumentNullException(My.Resources.Analyzer.InvalidVariableValue)
                Return val
            End Get
            Set(ByVal value As Double)
                val = value
            End Set
        End Property
        Sub New(ByVal num As Double)
            value = num
        End Sub
        Sub New()
            value = Nothing
        End Sub
        Public Function Calculate() As Val Implements ExprInterf.Calculate
            Return Me
        End Function
        Public Shared Operator +(ByVal class1 As Val, ByVal class2 As Val) As Val
            'If class1.value.HasValue = False Or class2.value.HasValue = False Then Throw New ArgumentNullException(My.Resources.Analyzer.InvalidVariableValue) ' Return New Val()
            Return New Val(class1.Value + class2.Value)
        End Operator
        Public Shared Operator -(ByVal class1 As Val, ByVal class2 As Val) As Val
            'If class1.value.HasValue = False Or class2.value.HasValue = False Then Throw New ArgumentNullException(My.Resources.Analyzer.InvalidVariableValue)
            Return New Val(class1.Value - class2.Value)
        End Operator
        Public Shared Operator ^(ByVal class1 As Val, ByVal class2 As Val) As Val
            'If class1.value.HasValue = False Or class2.value.HasValue = False Then Throw New ArgumentNullException(My.Resources.Analyzer.InvalidVariableValue)
            Return New Val(class1.Value ^ class2.Value)
        End Operator
        Public Shared Operator *(ByVal class1 As Val, ByVal class2 As Val) As Val
            ' If class1.value.HasValue = False Or class2.value.HasValue = False Then Throw New ArgumentNullException(My.Resources.Analyzer.InvalidVariableValue)
            Return New Val(class1.Value * class2.Value)
        End Operator
        Public Shared Operator /(ByVal class1 As Val, ByVal class2 As Val) As Val
            ' If class1.value.HasValue = False Or class2.value.HasValue = False Then Throw New ArgumentNullException(My.Resources.Analyzer.InvalidVariableValue)
            Return New Val(class1.Value / class2.Value)
        End Operator
        Public Shared Operator -(ByVal class1 As Val) As Val
            ' If class1.value.HasValue = False Then Throw New ArgumentNullException(My.Resources.Analyzer.InvalidVariableValue)
            Return New Val(-class1.Value)
        End Operator

        Public Shared Operator <>(ByVal left As Val, ByVal right As Val) As Boolean
            Return Not left = right
        End Operator

        Public Shared Operator =(ByVal left As Val, ByVal right As Val) As Boolean
            'If left.value.HasValue = False Or right.value.HasValue = False Then Throw New ArgumentNullException(My.Resources.Analyzer.InvalidVariableValue)
            Return left.Value = right.Value
        End Operator

     
    End Class

    Private Class Bop
        Implements ExprInterf
        Enum OperatorTyp
            Plus
            Minus
            Times
            Divide
            Raise
            'Eq, NotEq, Less, Greater, LessOrEq, GreaterOrEq,AndOp,OrOp
        End Enum
        Dim op As OperatorTyp
        Dim left As ExprInterf, right As ExprInterf
        Sub New(ByVal Oper As OperatorTyp, ByVal l As ExprInterf, ByVal r As ExprInterf)
            op = Oper
            left = l
            right = r
        End Sub
        Public Function Calculate() As Val Implements ExprInterf.Calculate
            Select Case op
                Case OperatorTyp.Plus
                    Return left.Calculate + right.Calculate
                Case OperatorTyp.Minus
                    Return left.Calculate - right.Calculate
                Case OperatorTyp.Times
                    Return left.Calculate * right.Calculate
                Case OperatorTyp.Divide
                    Return left.Calculate / right.Calculate
                Case OperatorTyp.Raise
                    Return left.Calculate ^ right.Calculate
                Case Else
                    Throw New ApplicationException("Neznamy operator")
            End Select
        End Function
    End Class

    Private Class UnMinus
        Implements ExprInterf
        Dim expr As ExprInterf
        Sub New(ByVal ex As ExprInterf)
            expr = ex
        End Sub
        Public Function Calculate() As Val Implements ExprInterf.Calculate
            Return -expr.Calculate
        End Function
    End Class

    Private Class Calling
        Implements ExprInterf
        Dim funct As Funct
        Dim params As List(Of ExprInterf)
        Sub New(ByVal fun As Funct, ByVal par As List(Of ExprInterf))
            If Not fun.HasNumParam(par.Count) Then Throw New WrongSyntaxException(String.Format(My.Resources.Analyzer.InvalidParamCount, funct.Key, 1, params.Count))
            funct = fun
            params = par
        End Sub
        Public Function Calculate() As Val Implements ExprInterf.Calculate
            Dim vals As New List(Of Val)
            For Each par As ExprInterf In params
                vals.Add(par.Calculate)
            Next
            Return funct.Run(vals)
        End Function
    End Class
End Class
