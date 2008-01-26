#If Config <= Nightly Then 'Stage nightly
Imports System.Data, System.Data.Common, Tools.VisualBasicT
'Experimental: The whole Data.Metadata namespace is experimental. I'm not sure if there will be any final release version of it
'Seelaso: http://msdn2.microsoft.com/en-us/library/1ss96yss(VS.71).aspx
Namespace Data.Metadata
    'TODO: Comment, Mark, Wiki, Forum
    ''' <remarks>Experimental</remarks>
    Public Class MetadataConnection
        Inherits DbConnection

        Protected Overrides Function BeginDbTransaction(ByVal isolationLevel As System.Data.IsolationLevel) As System.Data.Common.DbTransaction
            Throw New NotSupportedException("Transactions are not supported for Metadata data provider")
        End Function
        Private _Open As Boolean = False
        Public Property IsOpen() As Boolean
            Get
                Return _Open
            End Get
            Protected Set(ByVal value As Boolean)
                _Open = value
            End Set
        End Property

        Public Overrides Sub Close()
            IsOpen = False
        End Sub


        ''' <remarks>
        ''' <para>Metadata data provider connection string is set of name-value assignments. Name may consist only of letters A-Z and a-z and _ and numerals 0-9 and may not start wit numeral. Value can, but have not to be, enclosed in ". Value must be enclosed in " in case it contains space, tabuletor, carriage return, line feed, coma or semicolon. In "-escaped values you can use \ to escape any character (backslashes in quoted values are replaced by characters that follows them). Each name-value pair must be separated by any number of spaces, tabs, linefeeds, carriege returns, commas and semicolons. Separator can be ommited after quoted value.</para>
        ''' <para>Supported parameters for connection string are:</para>
        ''' <list type="table">
        ''' <item><term>Database</term><description>Root folder for queries. Can be empty. Must be existing folder.</description></item>
        ''' </list>
        ''' <para>Connection string parameter names are case-insensitive</para>
        ''' </remarks>
        Public Overrides Property ConnectionString() As String
            Get
                Dim ret As New System.Text.StringBuilder
                ret.Append("Database=""" & Me._InitialDatabase & """ ")
                Return ret.ToString
            End Get
            Set(ByVal value As String)
                If IsOpen Then Throw New InvalidOperationException("Cannot change ConnectionString while connection is open")
            End Set
        End Property

        Protected Overrides Function CreateDbCommand() As System.Data.Common.DbCommand
            'TODO: Create db command
        End Function

        ''' <summary>Initial folder set in connection string</summary>
        Private _InitialDatabase As String
        ''' <summary>Current folder</summary>
        ''' <remarks>Eqals to <see cref="_InitialDatabase"/> unless <see cref="ChangeDatabase"/> was called after the connection was opened</remarks>
        Private _CurrentDatabase As String

        ''' <remarks>Database in terms of Metadata data provider is folder name and can be an empty string</remarks>>
        Public Overrides ReadOnly Property Database() As String
            Get
#If Framework >= 3.5 Then
                Return If(IsOpen, _CurrentDatabase, _InitialDatabase)
#Else
                Return iif(IsOpen, _CurrentDatabase, _InitialDatabase)
#End If
            End Get
        End Property

        Public Overrides ReadOnly Property DataSource() As String
            Get
                Throw New NotSupportedException("DataSource is not supported for Metadata data provider")
            End Get
        End Property

        ''' <remarks>Database in terms of Metadata data provider is folder name and can be an empty string</remarks>>
        Public Overrides Sub ChangeDatabase(ByVal databaseName As String)
            If IsOpen Then
                If IO.Directory.Exists(databaseName) Then
                    _CurrentDatabase = databaseName
                Else
                    Throw New ArgumentException("Cannot change to non-existing folder", "databaseName")
                End If
            Else
                _InitialDatabase = databaseName
            End If
        End Sub

        Public Overrides Sub Open()
            'TODO: Check if database (folder) exists
            If _InitialDatabase = "" OrElse IO.Directory.Exists(_InitialDatabase) Then
                IsOpen = True
            Else
                Throw New IO.DirectoryNotFoundException(String.Format("Directory ""{0}"" does not exist", _InitialDatabase))
            End If
        End Sub

        Public Overrides ReadOnly Property ServerVersion() As String
            Get
                Throw New NotSupportedException("ServerVersion is not supported for Metadata data provider")
            End Get
        End Property

        Public Overrides ReadOnly Property State() As System.Data.ConnectionState
            Get
                If IsOpen Then Return ConnectionState.Open Else Return ConnectionState.Closed
                'TODO: Return more states
            End Get
        End Property

        Private Enum ConnectionStringParserStates
            BeforePropertyName
            PropertyName
            AfterPropertyName
            AfterEqual
            NonQuotedPropertyValue
            AfterQuote
            AfterBackSlash
        End Enum
        Private Sub ParseConnectionString(ByVal ConnectionString As String)
            Dim Properties As New Dictionary(Of String, String)
            Dim State As ConnectionStringParserStates = ConnectionStringParserStates.BeforePropertyName
            Dim LastI As Integer = 0
            Dim LastProperty As String = ""
            ConnectionString &= " "c
            For i As Integer = 0 To ConnectionString.Length
                Select Case State
                    Case ConnectionStringParserStates.BeforePropertyName
                        Select Case ConnectionString(i)
                            Case " "c, vbCr, vbLf, vbTab, ","c, ";"c
                            Case "a"c To "z"c, "A"c To "Z"c, "_"c
                                LastI = i
                                State = ConnectionStringParserStates.PropertyName
                            Case Else
                                Throw New ArgumentException(String.Format("Unexpected character at beginning of '{0}' in connection string", ConnectionString.Substring(i)))
                        End Select
                    Case ConnectionStringParserStates.PropertyName
                        Select Case ConnectionString(i)
                            Case "a"c To "z"c, "A"c To "Z"c, "_"c, "0"c To "9"c
                            Case " ", vbCr, vbLf, vbTab
                                LastProperty = ConnectionString.Substring(LastI, i - LastI)
                                State = ConnectionStringParserStates.AfterPropertyName
                            Case "="c
                                LastProperty = ConnectionString.Substring(LastI, i - LastI)
                                State = ConnectionStringParserStates.AfterEqual
                            Case Else
                                Throw New ArgumentException(String.Format("Unexpected character at beginning of '{0}' in connection string", ConnectionString.Substring(i)))
                        End Select
                    Case ConnectionStringParserStates.AfterPropertyName
                        Select Case ConnectionString(i)
                            Case " "c, vbCr, vbLf, vbTab
                            Case "="
                                State = ConnectionStringParserStates.AfterEqual
                            Case Else
                                Throw New ArgumentException(String.Format("Unexpected character at beginning of '{0}' in connection string ('=' expected)", ConnectionString.Substring(i)))
                        End Select
                    Case ConnectionStringParserStates.AfterEqual
                        Select Case ConnectionString(i)
                            Case " ", vbCr, vbLf, vbTab
                            Case """"
                                State = ConnectionStringParserStates.AfterQuote
                                LastI = i + 1
                            Case "="c, ","c, ";"c
                                Throw New ArgumentException(String.Format("Unexpected character at beginning of '{0}' in connection string", ConnectionString.Substring(i)))
                            Case Else
                                State = ConnectionStringParserStates.NonQuotedPropertyValue
                                LastI = i
                        End Select
                    Case ConnectionStringParserStates.NonQuotedPropertyValue
                        Select Case ConnectionString(i)
                            Case ","c, " "c, ";", vbTab, vbCr, vbLf
                                Dim PropertyValue As String = ConnectionString.Substring(LastI, i - LastI)
                                If Properties.ContainsKey(LastProperty) Then Throw New ArgumentException(String.Format("Duplicit specification for the {0} property", LastProperty))
                                Properties.Add(LastProperty, PropertyValue)
                                State = ConnectionStringParserStates.BeforePropertyName
                            Case "="c
                                Throw New ArgumentException(String.Format("Unexpected character at beginning of '{0}' in connection string", ConnectionString.Substring(i)))
                        End Select
                    Case ConnectionStringParserStates.AfterQuote
                        Select Case ConnectionString(i)
                            Case "\"c
                                State = ConnectionStringParserStates.AfterBackSlash
                            Case """"c
                                Dim PropertyValue As String = ConnectionString.Substring(LastI, i - LastI - 1)
                                Properties.Add(LastProperty, PropertyValue.Replace("\", ""))
                                If Properties.ContainsKey(LastProperty) Then Throw New ArgumentException(String.Format("Duplicit specification for the {0} property", LastProperty))
                                State = ConnectionStringParserStates.BeforePropertyName
                        End Select
                    Case ConnectionStringParserStates.AfterBackSlash
                        State = ConnectionStringParserStates.AfterQuote
                End Select
            Next i
            Dim OldConnectionString As String = Me.ConnectionString
            Try
                For Each item As KeyValuePair(Of String, String) In Properties
                    Select Case item.Key.ToLower
                        Case "database"
                            _InitialDatabase = item.Value
                        Case Else
                            Throw New ArgumentException(String.Format("Unknown property {0}", item.Key))
                    End Select
                Next item
            Catch ex As ArgumentException
                Me.ConnectionString = OldConnectionString
                Throw
            End Try
        End Sub
    End Class
End Namespace
#End If