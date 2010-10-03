Imports System.Text, Tools.ExtensionsT

#If Config <= Nightly Then
Namespace WebT.CssT.LexicalT
    'CSS namespaces: http://www.w3.org/TR/css3-namespace/
    'CSS selectors: http://www.w3.org/TR/css3-selectors

    ''' <summary>Enumeration of CSS selectors lexical elements</summary>
    ''' <remarks>This enumeration represents lexical elements defined in <a href="http://www.w3.org/TR/css3-selectors/#lex">CSS Slectors Level 3 Lexical Scanner</a> plus single-character elements and plus some special elements.</remarks>
    ''' <version stage="nightly" version="1.5.3">This enumeration is new in version 1.5.3</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Enum SelectorLexicalElement
        ''' <summary>Whitespace</summary>
        S
        ''' <summary>Attribute value inclusion operator ~=</summary>
        Includes
        ''' <summary>Attribute value dash-separated operator |=</summary>
        DashMatch
        ''' <summary>Attribute value prefix match operator ^=</summary>
        PrefixMatch
        ''' <summary>Attribute value suffix match operator $=</summary>
        SufixMatch
        ''' <summary>Attribute value substring match operator *=</summary>
        SubstringMatch
        ''' <summary>Identifier</summary>
        Ident
        ''' <summary>String</summary>
        [String]
        ''' <summary>Function-like call ident(</summary>
        [Function]
        ''' <summary>Number</summary>
        Number
        ''' <summary>ID selector #name</summary>
        Hash
        ''' <summary>Plus combinator</summary>
        Plus
        ''' <summary>Grater combinator ></summary>
        Greater
        ''' <summary>Comma separator</summary>
        Comma
        ''' <summary>Tilde combinator</summary>
        Tilde
        ''' <summary>Not pseudoclass :not(</summary>
        [Not]
        ''' <summary>At-keyword @ident</summary>
        AtKeyword
        ''' <summary>Invalid lexical element</summary>
        Invalid
        ''' <summary>Percentage value num%</summary>
        Percentage
        ''' <summary>Dimension num ident</summary>
        Dimension
        ''' <summary>XML comment start &lt;!--</summary>
        CDO
        ''' <summary>XML comment end --></summary>
        CDC

        ''' <summary>Colon (:)</summary>
        Colon
        ''' <summary>Pipe (|)</summary>
        Pipe
        ''' <summary>Open square brace ([)</summary>
        OpenSq
        ''' <summary>Equal sign (=)</summary>
        Equals
        ''' <summary>Close square brace (])</summary>
        CloseSq
        ''' <summary>Close round brace ())</summary>
        Close

        ''' <summary>Comment - this lexical element is never parset out by scanner but can be returned by special event</summary>
        Comment
        ''' <summary>Start of CSS definition (end of selector; {) - this element is never parsed out by scanner but can be returned by special event</summary>
        CssStart
        ''' <summary>New line - this element is never parsed out by scanner but can be returned by special event</summary>
        NewLine
    End Enum

    ''' <summary>Performs lexical analysis of string representing CSS selector or group of CSS selectors</summary>
    ''' <version stage="nightly" version="1.5.3">This class is new in version 1.5.3</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class SelectorLexer
        ''' <summary>Stack for stack automaton</summary>
        Private stack As New Stack(Of Object)
        ''' <summary>Builds temporary strings</summary>
        Private buffH As New StringBuilder
        ''' <summary>Builds string for output</summary>
        Private buff As New StringBuilder
        ''' <summary>Indicates if backslash parsing allows comments</summary>
        Private allowComments As Boolean = True
        ''' <summary>FSA state</summary>
        Private state As States
        ''' <summary>Indicates index of current character</summary>
        Private i%
        ''' <summary>Builds comment</summary>
        Private commentBuilder As New StringBuilder
        Private _rowIndex%
        Private _columnIndex%

#Region "Small methods"
        ''' <summary>Resets lexical scanner to it's default state</summary>
        Public Sub Reset()
            stack.Clear()
            buffH.Clear()
            buff.Clear()
            allowComments = True
            state = States.NotStarted
            i = -1
            commentBuilder.Clear()
            _columnIndex = 0
            _rowIndex = 0
        End Sub

        ''' <summary>Starts lexical analyzis</summary>
        ''' <remarks>This method must be called prior analysis starts</remarks>
        ''' <exception cref="InvalidOperationException"><see cref="Start"/> was already called on this instance and <see cref="Reset"/> was not called after it.</exception>
        Public Sub Start()
            If state <> States.NotStarted Then Throw New InvalidOperationException("Parsing already has started")
            state = States.Text
            i = 0
            _columnIndex = 1
            _rowIndex = 1
        End Sub
#End Region

#Region "Setup properties"
        ''' <summary>Indicates wheather lexical analyzer detects end of CSS selector (and start of CSS style definition) - it means it detects '{'</summary>
        ''' <value>True to detect '{' as end of CSS rule, false to treat '{' as invalid character</value>
        ''' <returns>True if lexical scanner currently signalizes end of rule when '{' is reached, false otherwise.</returns>
        ''' <remarks>Typically you select this property to true when you are parsing whole CSS file of &lt;style> declaration and to false when youa are parsing isolated CSS selector not followed by CSS definition.</remarks>
        Public Property IndicateEndOfSelectorGroup As Boolean

        ''' <summary>Indicates wheather <see cref="SyntaxErrorException"/> is thrown on first invalid character reached</summary>
        ''' <value>True to throw <see cref="SyntaxErrorException"/> on first invalid character, false to parse invalid parts as <see cref="SelectorLexicalElement.Invalid"/>.</value>
        Public Property ThrowOnError As Boolean

        Private _rowOffset%
        ''' <summary>Gets or sets offset in number of rows selector parsing started from start of greater text (i.e. file)</summary>
        ''' <remarks>Value of this property is used by <see cref="SelectorLexer"/> to report current position withing greater text and to report position of parsed lexical elements. Internally lexicall analyzer works with absolute postion since last call to <see cref="Start"/>.</remarks>
        Public Property RowOffset%
            Get
                Return _rowOffset
            End Get
            Set(ByVal value%)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", "Value must be zero or positive.")
                _rowOffset = value
            End Set
        End Property
        Private _columnOffset%
        ''' <summary>Gets or sets offset in number of characters (columns) from start of first line parsing started</summary>
        ''' <remarks>Value of this property is used by <see cref="SelectorLexer"/> to report current position withing greater text and to report position of parsed lexical elements. Internally lexicall analyzer works with absolute postion since last call to <see cref="Start"/>.</remarks>
        Public Property ColumnOffset%
            Get
                Return _columnIndex
            End Get
            Set(ByVal value%)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", "Value must be zero or positive.")
                _columnIndex = value
            End Set
        End Property
        Private _charIndexOffset%
        ''' <summary>Gets or sets offset in number of characters from start of greater text parsing started</summary>
        ''' <remarks>Value of this property is used by <see cref="SelectorLexer"/> to report current position withing greater text and to report position of parsed lexical elements. Internally lexicall analyzer works with absolute postion since last call to <see cref="Start"/>.</remarks>
        Public Property CharIndexOffset%
            Get
                Return _charIndexOffset
            End Get
            Set(ByVal value%)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", "Value must be zero or positive.")
                _charIndexOffset = value
            End Set
        End Property

#End Region

#Region "Status properties"
        ''' <summary>Gets current state of lexical scanner</summary>
        Public ReadOnly Property LexerState As AnalysisState
            Get
                If state <> AnalysisState.NotStarted AndAlso state <> AnalysisState.Finished Then Return AnalysisState.InProgress
                Return state
            End Get
        End Property
        ''' <summary>Gets 0-based index of current character</summary>
        ''' <returns>Number of character processed by lexical analyzer (excluding current unprocessed character) plus <see cref="CharIndexOffset"/></returns>
        Public ReadOnly Property CharIndex%
            Get
                Return i + CharIndexOffset
            End Get
        End Property
        ''' <summary>Gets 1-based index of current row (line)</summary>
        ''' <returns>Number of lines processed by lexical analyzer (including current yet not completelly processed line) plus <see cref="RowOffset"/></returns>
        Public ReadOnly Property RowIndex%
            Get
                Return _rowIndex + RowOffset
            End Get
        End Property
        ''' <summary>Gets 1-based index of current character withing current line</summary>
        ''' <returns>NUmber of characters processed by lexical analyzer (including current yet unprocessed character). When on first line <see cref="ColumnOffset"/> is added.</returns>
        Public ReadOnly Property ColumnIndex%
            Get
                Return _columnIndex + If(_rowIndex = 0, ColumnOffset, 0)
            End Get
        End Property
#End Region

        ''' <summary>Possible states of lexical analysis</summary>
        Public Enum AnalysisState
            ''' <summary>Anylysis has not started yet</summary>
            NotStarted = States.NotStarted
            ''' <summary>Analysis is in progress - keep sending characters to analyzer</summary>
            InProgress = -1
            ''' <summary>Analysis has finished (can happen only when <see cref="IndicateEndOfSelectorGroup"/> is true)</summary>
            Finished = States.Finished
        End Enum

#Region "Events"
        ''' <summary>Raised whenever lexical element is parsed out</summary>
        ''' <remarks>Not raised for <see cref="SelectorLexicalElement.Comment"/> and <see cref="SelectorLexicalElement.CssStart"/></remarks>
        Public Event LexicalElement As EventHandler(Of SelectorLexer, CssLexicalElement)
        ''' <summary>Raised whenever comment in selector is parsed out</summary>
        ''' <remarks><paramref name="e"/>.<see cref="CssLexicalElement.LexicalElementType">LexicalElementType</see> is always <see cref="SelectorLexicalElement.Comment"/></remarks>
        Public Event Comment As EventHandler(Of SelectorLexer, CssLexicalElement)
        ''' <summary>Raised when end of CSS selector group is detected (only when <see cref="IndicateEndOfSelectorGroup"/> is true</summary>
        ''' <remarks><paramref name="e"/>.<see cref="CssLexicalElement.LexicalElementType">LexicalElementType</see> is always <see cref="SelectorLexicalElement.CssStart"/></remarks>
        Public Event Finish As EventHandler(Of SelectorLexer, CssLexicalElement)
        ''' <summary>Raised when a new line is detected in CSS code</summary>
        ''' <remarks><paramref name="e"/>.<see cref="CssLexicalElement.LexicalElementType">LexicalElementType</see> is always <see cref="SelectorLexicalElement.NewLine"/></remarks>
        Public Event NewLine As EventHandler(Of SelectorLexer, CssLexicalElement)

        ''' <summary>Raises the <see cref="LexicalElement"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <exception cref="ArgumentException"><paramref name="e"/>.<see cref="CssLexicalElement.LexicalElementType">LexicalElementType</see> is either <see cref="SelectorLexicalElement.Comment"/> or <see cref="SelectorLexicalElement.CssStart"/> or <see cref="SelectorLexicalElement.NewLine"/>.</exception>
        Protected Overridable Sub OnLexicalElement(ByVal e As CssLexicalElement)
            If e.LexicalElementType = SelectorLexicalElement.Comment OrElse e.LexicalElementType = SelectorLexicalElement.CssStart OrElse e.LexicalElementType = SelectorLexicalElement.NewLine Then _
                Throw New ArgumentException("Unsupported lexical element {0}".f(e.LexicalElementType), "e")
            RaiseEvent LexicalElement(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="Comment"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <exception cref="ArgumentException"><paramref name="e"/>.<see cref="CssLexicalElement.LexicalElementType">LexicalElementType</see> is not <see cref="SelectorLexicalElement.Comment"/>.</exception>
        Protected Overridable Sub OnComment(ByVal e As CssLexicalElement)
            If e.LexicalElementType <> SelectorLexicalElement.Comment Then Throw New ArgumentException("Unsupported lexical element {0}".f(e.LexicalElementType), "e")
            RaiseEvent Comment(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="Finish"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <exception cref="ArgumentException"><paramref name="e"/>.<see cref="CssLexicalElement.LexicalElementType">LexicalElementType</see> is not <see cref="SelectorLexicalElement.CssStart"/>.</exception>
        Protected Overridable Sub OnFinish(ByVal e As CssLexicalElement)
            If e.LexicalElementType <> SelectorLexicalElement.CssStart Then Throw New ArgumentException("Unsupported lexical element {0}".f(e.LexicalElementType), "e")
            RaiseEvent LexicalElement(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="NewLine"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <exception cref="ArgumentException"><paramref name="e"/>.<see cref="CssLexicalElement.LexicalElementType">LexicalElementType</see> is not <see cref="SelectorLexicalElement.NewLine"/>.</exception>
        Protected Overridable Sub OnNewLine(ByVal e As CssLexicalElement)
            If e.LexicalElementType <> SelectorLexicalElement.NewLine Then Throw New ArgumentException("Unsupported lexical element {0}".f(e.LexicalElementType), "e")
            RaiseEvent NewLine(Me, e)
        End Sub
#End Region

#Region "Parsing"
        ''' <summary>FSA states</summary>
        Private Enum States
            ''' <summary>Special state indicates that analysis has not started yet</summary>
            NotStarted
            ''' <summary>Special state indicates that analysis has finished</summary>
            Finished

            ''' <summary>Basic staate of automaton</summary>
            Start

            ''' <summary>&lt;</summary>
            Lt
            ''' <summary>&lt!</summary>
            LtExcl
            ''' <summary>$lt;-</summary>
            LtExclMinus
            ''' <summary>$</summary>
            Dolar
            ''' <summary>~</summary>
            Tilde
            ''' <summary>|</summary>
            Pipe
            ''' <summary>^</summary>
            Circum
            ''' <summary>*</summary>
            Star
            ''' <summary>Single-quoted string</summary>
            String1
            ''' <summary>Double-quoted string</summary>
            String2
            ''' <summary>:</summary>
            Colon
            ''' <summary>:N</summary>
            N
            ''' <summary>:No</summary>
            No
            ''' <summary>:Not</summary>
            [Not]
            ''' <summary>Whitespace</summary>
            White
            ''' <summary>.</summary>
            Dot
            ''' <summary>Integer number</summary>
            Num1
            ''' <summary>Dot after integer number</summary>
            Num2a
            ''' <summary>Floating-point number</summary>
            Num2
            ''' <summary>- (in normal text)</summary>
            StartMinus
            ''' <summary>--</summary>
            Minus2
            ''' <summary>Retrun from identifier in text</summary>
            AfterNm
            ''' <summary>Return from identifier after @</summary>
            AtKw
            ''' <summary>Return from identifier after #</summary>
            Hash
            ''' <summary>Return from identifier after number</summary>
            Dimension
            ''' <summary>Invalid character processing</summary>
            Invalid
#Region "BackSlash escape processing"
            ''' <summary>\</summary>
            Back
            ''' <summary>\ and <see cref="vbCr"/> (\r)</summary>
            BackR
            ''' <summary>1st hex number in Unicode escape</summary>
            U1
            ''' <summary>2nd hex number in Unicode escape</summary>
            U2
            ''' <summary>3rd hex number in Unicode escape</summary>
            U3
            ''' <summary>4th hex number in Unicode escape</summary>
            U4
            ''' <summary>5th hex number in Unicode escape</summary>
            U5
            ''' <summary>End of backslash escape processing</summary>
            BackSlashEscapeProcessingJumpPop
#End Region
#Region "Identifier processing"
            ''' <summary>Decission step at main entry point of identifier processing</summary>
            Nm1
            ''' <summary>Identifier processing</summary>
            Nm
            ''' <summary>--starting indentifier</summary>
            Minus
            ''' <summary>End of identifier processing</summary>
            IdentifierProcessingJumpPop
#End Region
#Region "Comment processing"
            ''' <summary>/</summary>
            Slash
            ''' <summary>Comment (/*)</summary>
            Comment
            ''' <summary>* in comment</summary>
            EndComment
            ''' <summary>End of comment processing</summary>
            CommentProcessingJumpPop
#End Region
        End Enum

        ''' <summary>Advances scanning by one character</summary>
        ''' <param name="character">Character to be scanned</param>
        ''' <exception cref="SyntaxErrorException"><see cref="ThrowOnError"/> is true and invalid character was reached.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="LexerState"/> is not <see cref="AnalysisState.InProgress"/></exception>
        Public Sub OnCharacter(ByVal character As Char)
            If LexerState <> AnalysisState.InProgress Then Throw New InvalidOperationException("Lexical analysis has not started yet or has already finished.")

            Dim charBuffer As New Queue(Of Char)
            charBuffer.Enqueue(character)

            While charBuffer.Count > 0
                Dim ch = charBuffer.Dequeue

                Select Case state
                    Case States.Start
                        Select Case ch
                            Case "["c : ReportToken(SelectorLexicalElement.OpenSq, "["c)
                            Case "="c : ReportToken(SelectorLexicalElement.Equals, "="c)
                            Case "]"c : ReportToken(SelectorLexicalElement.CloseSq, "]"c)
                            Case ")"c : ReportToken(SelectorLexicalElement.Close, ")"c)
                        End Select
                End Select

            End While

        End Sub

        ''' <summary>Signalizes to scanner that there is no more text on input and that it shall report any unreported tokens</summary>
        ''' <exception cref="SyntaxErrorException"><see cref="ThrowOnError"/> is true and invalid character or unfinished token was reached.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="LexerState"/> is not <see cref="AnalysisState.InProgress"/></exception>
        Public Sub Eof()
            If LexerState <> AnalysisState.InProgress Then Throw New InvalidOperationException("Lexical analysis has not started yet or has already finished.")
            'TODO:
        End Sub
#End Region
#Region "Token reporting"
        ''' <summary>Reports single-character lexical element</summary>
        ''' <param name="type">Type of lexical element</param>
        ''' <param name="value">Value of lexical element (the character)</param>
        ''' <exception cref="ArgumentException"><paramref name="type"/> is either <see cref="SelectorLexicalElement.Comment"/> or <see cref="SelectorLexicalElement.CssStart"/> or <see cref="SelectorLexicalElement.NewLine"/>.</exception>
        Protected Sub ReportToken(ByVal type As SelectorLexicalElement, ByVal value As Char)
            OnLexicalElement(New CssLexicalElement(type, CharIndex, 1, value, 1, RowIndex, CharIndex))
        End Sub
        ''' <summary>Reports lexical element with boundary</summary>
        ''' <param name="type">Type of lexical element</param>
        ''' <param name="startIndex">Character index where the element started (witout <see cref="CharIndexOffset"/>)</param>
        ''' <param name="length">Lenght of element in original text including comments and not-interpreted escape sequences</param>
        ''' <param name="startCharacter">1-based index of character at current line where element started (without <see cref="ColumnOffset"/>)</param>
        ''' <param name="startLine">1-based index of line where the element started (without <see cref="RowOffset"/>)</param>
        ''' <param name="boundary">Length of 1st part of value</param>
        ''' <param name="value">Interptreted value of element (excludes commments, escape sequences are interpreted)</param>
        ''' <exception cref="ArgumentException"><paramref name="type"/> is either <see cref="SelectorLexicalElement.Comment"/> or <see cref="SelectorLexicalElement.CssStart"/> or <see cref="SelectorLexicalElement.NewLine"/>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is an empty string.</exception>
        ''' <exception cref="ArgumentOutOfRangeException">There is an mismatch of lengths and indexes. See <see cref="M:Tools.WebT.CssT.LexicalT.CssLexicalElement.#ctor"/> for details.</exception>
        Protected Sub ReportToken(ByVal type As SelectorLexicalElement, ByVal startIndex%, ByVal startLine%, ByVal startCharacter%, ByVal length%, ByVal value$, ByVal boundary%)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            OnLexicalElement(New CssLexicalElement(type, startIndex + CharIndexOffset, length, value, boundary, startLine + RowOffset, startCharacter + If(startLine = 0, ColumnOffset, 0)))
        End Sub
        ''' <summary>Reports lexical element without boundary</summary>
        ''' <param name="type">Type of lexical element</param>
        ''' <param name="startIndex">Character index where the element started (witout <see cref="CharIndexOffset"/>)</param>
        ''' <param name="length">Lenght of element in original text including comments and not-interpreted escape sequences</param>
        ''' <param name="startCharacter">1-based index of character at current line where element started (without <see cref="ColumnOffset"/>)</param>
        ''' <param name="startLine">1-based index of line where the element started (without <see cref="RowOffset"/>)</param>
        ''' <param name="value">Interptreted value of element (excludes commments, escape sequences are interpreted)</param>
        ''' <exception cref="ArgumentException"><paramref name="type"/> is either <see cref="SelectorLexicalElement.Comment"/> or <see cref="SelectorLexicalElement.CssStart"/> or <see cref="SelectorLexicalElement.NewLine"/>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is an empty string.</exception>
        ''' <exception cref="ArgumentOutOfRangeException">There is an mismatch of lengths and indexes. See <see cref="M:Tools.WebT.CssT.LexicalT.CssLexicalElement.#ctor"/> for details.</exception>
        Protected Sub ReportToken(ByVal type As SelectorLexicalElement, ByVal startIndex%, ByVal startLine%, ByVal startCharacter%, ByVal length%, ByVal value$)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            OnLexicalElement(New CssLexicalElement(type, startIndex, length, value, value.Length, startLine, startCharacter))
        End Sub
#End Region

        ''' <summary>Represents position in text</summary>
        ''' <version version="1.5.3" stage="nightly">This structure is new in version 1.5.3</version>
        <Serializable()>
        Public Structure PositionInText
            Private ReadOnly _charIndex%
            Private ReadOnly _row%
            Private ReadOnly _column%
            ''' <summary>CTor - initializes a new instance of the <see cref="PositionInText"/> structure</summary>
            ''' <param name="charIndex">0-based index of character in string across all lines</param>
            ''' <param name="row">1-based index of line</param>
            ''' <param name="column">1-based index of column (character) on line</param>
            Public Sub New(ByVal charIndex%, ByVal row%, ByVal column%)
                If charIndex < 0 Then Throw New ArgumentOutOfRangeException("charIndex", "Value must be zero or positive.")
                If row <= 0 Then Throw New ArgumentOutOfRangeException("row", "Value must be positive.")
                If column <= 0 Then Throw New ArgumentOutOfRangeException("column", "Value must be positive.")
                _charIndex = charIndex
                _row = row
                _column = column
            End Sub
            ''' <summary>Gets 0-based index of character across all lines</summary>
            Public ReadOnly Property CharIndex%
                Get
                    Return _charIndex
                End Get
            End Property
            ''' <summary>Gets 1-based index of row (line)</summary>
            Public ReadOnly Property Row%
                Get
                    Return _row
                End Get
            End Property
            ''' <summary>Gets 1-based index of character (column) inside current line</summary>
            Public ReadOnly Property Column%
                Get
                    Return _column
                End Get
            End Property
            ''' <summary>Gets string representation of current isntance</summary>
            ''' <returns>String in format (<see cref="Row"/>, <see cref="Column"/>)</returns>
            Public Overrides Function ToString() As String
                Return String.Format("({0}, {1})", Row, Column)
            End Function
            ''' <summary>Server as hash function for this object</summary>
            ''' <returns>Has of this object - which is effectivelly <see cref="CharIndex"/></returns>
            Public Overrides Function GetHashCode() As Integer
                Return CharIndex
            End Function
#Region "Operators"
#Region "PositionInText ¤ PositionInText"
            ''' <summary>Compares two instances of <see cref="PositionInText"/> by less than operator</summary>
            ''' <param name="a">A <see cref="PositionInText"/></param>
            ''' <param name="b">A <see cref="PositionInText"/></param>
            ''' <returns>True if <paramref name="a"/> is less than <paramref name="b"/>; false otherwise</returns>
            ''' <exception cref="InvalidOperationException">Comparison of <see cref="CharIndex"/> and combination of <see cref="Row"/> and <see cref="Column"/> led to different results</exception>
            Public Shared Operator <(ByVal a As PositionInText, ByVal b As PositionInText) As Boolean
                Dim idxC = a.CharIndex < b.CharIndex
                Dim posC = a.Row < b.Row OrElse (a.Row = b.Row AndAlso a.Column < b.Column)
                If idxC <> posC Then Throw New InvalidOperationException("Given two instances cannot be compared because they apparently mark position in different texts (character index and row/column comparisons lead to different results).")
                Return idxC
            End Operator
            ''' <summary>Compares two instances of <see cref="PositionInText"/> by greater than operator</summary>
            ''' <param name="a">A <see cref="PositionInText"/></param>
            ''' <param name="b">A <see cref="PositionInText"/></param>
            ''' <returns>True if <paramref name="a"/> is greater than <paramref name="b"/>; false otherwise</returns>
            ''' <exception cref="InvalidOperationException">Comparison of <see cref="CharIndex"/> and combination of <see cref="Row"/> and <see cref="Column"/> led to different results</exception>
            Public Shared Operator >(ByVal a As PositionInText, ByVal b As PositionInText) As Boolean
                Dim idxC = a.CharIndex > b.CharIndex
                Dim posC = a.Row > b.Row OrElse (a.Row = b.Row AndAlso a.Column > b.Column)
                If idxC <> posC Then Throw New InvalidOperationException("Given two instances cannot be compared because they apparently mark position in different texts (character index and row/column comparisons lead to different results).")
                Return idxC
            End Operator
            ''' <summary>Compares two instances of <see cref="PositionInText"/> by less than or equal operator</summary>
            ''' <param name="a">A <see cref="PositionInText"/></param>
            ''' <param name="b">A <see cref="PositionInText"/></param>
            ''' <returns>True if <paramref name="a"/> is less than or equal to <paramref name="b"/>; false otherwise</returns>
            ''' <exception cref="InvalidOperationException">Comparison of <see cref="CharIndex"/> and combination of <see cref="Row"/> and <see cref="Column"/> led to different results</exception>
            Public Shared Operator <=(ByVal a As PositionInText, ByVal b As PositionInText) As Boolean
                Dim idxC = a.CharIndex <= b.CharIndex
                Dim posC = a.Row < b.Row OrElse (a.Row = b.Row AndAlso a.Column <= b.Column)
                If idxC <> posC Then Throw New InvalidOperationException("Given two instances cannot be compared because they apparently mark position in different texts (character index and row/column comparisons lead to different results).")
                Return idxC
            End Operator
            ''' <summary>Compares two instances of <see cref="PositionInText"/> by greater than or eaqual operator</summary>
            ''' <param name="a">A <see cref="PositionInText"/></param>
            ''' <param name="b">A <see cref="PositionInText"/></param>
            ''' <returns>True if <paramref name="a"/> is greater than or equal to <paramref name="b"/>; false otherwise</returns>
            ''' <exception cref="InvalidOperationException">Comparison of <see cref="CharIndex"/> and combination of <see cref="Row"/> and <see cref="Column"/> led to different results</exception>
            Public Shared Operator >=(ByVal a As PositionInText, ByVal b As PositionInText) As Boolean
                Dim idxC = a.CharIndex >= b.CharIndex
                Dim posC = a.Row > b.Row OrElse (a.Row = b.Row AndAlso a.Column >= b.Column)
                If idxC <> posC Then Throw New InvalidOperationException("Given two instances cannot be compared because they apparently mark position in different texts (character index and row/column comparisons lead to different results).")
                Return idxC
            End Operator
            ''' <summary>Tests two instances of <see cref="PositionInText"/> for equality</summary>
            ''' <param name="a">A <see cref="PositionInText"/></param>
            ''' <param name="b">A <see cref="PositionInText"/></param>
            ''' <returns>Ture when all components of <paramref name="a"/> and <paramref name="b"/> are equal; false otherwise.</returns>
            ''' <remarks>The components are <see cref="CharIndex"/>, <see cref="Row"/> and <see cref="Column"/>.</remarks>
            Public Shared Operator =(ByVal a As PositionInText, ByVal b As PositionInText) As Boolean
                Return a.CharIndex = b.CharIndex AndAlso a.Row = b.Row AndAlso a.Column = b.Column
            End Operator
            ''' <summary>Tests two instances of <see cref="PositionInText"/> for inequality</summary>
            ''' <param name="a">A <see cref="PositionInText"/></param>
            ''' <param name="b">A <see cref="PositionInText"/></param>
            ''' <returns>Ture when any of components of <paramref name="a"/> and <paramref name="b"/> differs; false otherwise.</returns>
            ''' <remarks>The components are <see cref="CharIndex"/>, <see cref="Row"/> and <see cref="Column"/>.</remarks>
            Public Shared Operator <>(ByVal a As PositionInText, ByVal b As PositionInText) As Boolean
                Return Not (a = b)
            End Operator
#End Region
#End Region
        End Structure
    End Class

    ''' <summary>Arguments of lexical element-related event</summary>
    ''' <version version="1.5.3" stage="nightly">This class is new in version 1.5.3</version>
    ''' <remarks>It may not be obvious from name of the class but it inherits <see cref="EventArgs"/>.</remarks>
    <Serializable()>
    Public Class CssLexicalElement
        Inherits EventArgs
        Private ReadOnly _lexicalElementType As SelectorLexicalElement
        Private ReadOnly _start%
        Private ReadOnly _value$
        Private ReadOnly _boundary%
        Private ReadOnly _length%
        Private ReadOnly _rowIndex%
        Private ReadOnly _columnIndex%

        ''' <summary>CTor - creates a new instance of the <see cref="CssLexicalElement"/> class</summary>
        ''' <param name="lexicalElementType">Type of lexical element being reported</param>
        ''' <param name="start">Index of first character of lexical element in containing string</param>
        ''' <param name="length">Length of lexical element in original string (including comments, escape sequences in original format)</param>
        ''' <param name="value">Text of lexical element (without comments, escape sequenses unescaped)</param>
        ''' <param name="boundary">Lenght of first part of lexical element. In case lexical element consists of only one part pass <paramref name="value"/>.<see cref="System.String.Length">Length</see> here.</param>
        ''' <param name="rowIndex">1-based index of row current lexical element starts on. 0 when not available.</param>
        ''' <param name="columnIndex">1-based index of character current lexical element starts on withing current row. 0 when not available.</param>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' Either <paramref name="start"/>, <see cref="ColumnIndex"/> or <see cref="RowIndex"/> is less than zero. -or- 
        ''' <paramref name="boundary"/> is greater than <paramref name="value"/>.<see cref="System.String.Length">Length</see>. -or-
        ''' <paramref name="boundary"/> is less than or equal to zero.
        ''' </exception>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is an empty string.</exception>
        Public Sub New(ByVal lexicalElementType As SelectorLexicalElement, ByVal start%, ByVal length%, ByVal value$, ByVal boundary%, ByVal rowIndex%, ByVal columnIndex%)
            If start < 0 Then Throw New ArgumentOutOfRangeException("start", "Value must be positive or zero")
            If value Is Nothing Then Throw New ArgumentNullException("value")
            If value = "" Then Throw New ArgumentException("Value cannot be empty", "value")
            If boundary > value.Length Then Throw New ArgumentOutOfRangeException("bondary", "Boundary must be withing length of value")
            If boundary <= 0 Then Throw New ArgumentOutOfRangeException("boundary", "Value must be positive")
            If length < value.Length Then Throw New ArgumentException("Length cannot be shorter than length of value")
            If columnIndex < 0 Then Throw New ArgumentOutOfRangeException("columnIndex", "Value must be positive or zero.")
            If rowIndex < 0 Then Throw New ArgumentOutOfRangeException("rowIndex", "Value must be positive or zero.")
            _lexicalElementType = lexicalElementType
            _start = start
            _value = value
            _boundary = boundary
            _length = value
            _rowIndex = rowIndex
            _columnIndex = columnIndex
        End Sub

        ''' <summary>Gets type of lexical element being reported</summary>
        Public ReadOnly Property LexicalElementType As SelectorLexicalElement
            Get
                Return _lexicalElementType
            End Get
        End Property
        ''' <summary>Gets index of first character of lexical element inside string it's being parsed from</summary>
        Public ReadOnly Property Start%
            Get
                Return _start
            End Get
        End Property
        ''' <summary>Gets length of entire lexical element (including comments in it, before unsecaping)</summary>
        Public ReadOnly Property Length%
            Get
                Return _length
            End Get
        End Property
        ''' <summary>Gets text of entire lexical element (excluding comments, afrer unescaping)</summary>
        ''' <remarks><see cref="Value"/>.<see cref="System.String.Length">Length</see> can be less than <see cref="Length"/> because <see cref="Value"/> does not contain comments and escape sequences are unescaped in <see cref="Value"/>.</remarks>
        Public ReadOnly Property Value$
            Get
                Return _value
            End Get
        End Property
        ''' <summary>Gets lenght of first part of value</summary>
        ''' <returns>Lenght of first part of value of current lexical element, <see cref="Value"/>.<see cref="System.String.Length">Length</see> whan this element has only one part.</returns>
        ''' <remarks>Some lexical elements consis of two parts - e.g. special character and indetifier (#example) or number and identfier</remarks>
        Public ReadOnly Property Boundary%
            Get
                Return _boundary
            End Get
        End Property
        ''' <summary>Gets first part of value</summary>
        ''' <returns>Substring of <see cref="Value"/> of length of <see cref="Boundary"/>.</returns>
        Public ReadOnly Property Part1Value$
            Get
                Return Value.Substring(0, Boundary)
            End Get
        End Property
        ''' <summary>Gets second part of value</summary>
        ''' <returns>Substring of <see cref="Value"/> after <see cref="Boundary"/></returns>
        Public ReadOnly Property Part2Value$
            Get
                If Boundary = Length Then Return ""
                Return Value.Substring(Boundary)
            End Get
        End Property
        ''' <summary>Gets 1-based index of row current lexical element begins at</summary>
        ''' <returns>1-based index of row current lexical element starts at, 0 when not available</returns>
        ''' <remarks><see cref="SelectorLexer"/> always sets value of this property to non-zero</remarks>
        Public ReadOnly Property RowIndex%
            Get
                Return _rowIndex
            End Get
        End Property
        ''' <summary>Gets 1-based index of column (character) within current line current lexical element begins at</summary>
        ''' <returns>1-based index of colum (character) within current line current lexical element starts at, 0 when not available</returns>
        ''' <remarks><see cref="SelectorLexer"/> always sets value of this property to non-zero</remarks>
        Public ReadOnly Property ColumnIndex%
            Get
                Return _columnIndex
            End Get
        End Property
    End Class
End Namespace
#End If