Imports System.Data
Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage: Nightly
Namespace DataT
    ''' <summary>Contains extension methods for working with ADO.NET</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Module DataExtensions
        ''' <summary>Creates and returns a Command object associated with the connection. Allows to set command text.</summary>
        ''' <param name="connection">Connection to create command on</param>
        ''' <param name="commandText">Text of SQL command. Ignored when null.</param>
        ''' <returns>A Command object associated with the connection.
        ''' The <see cref="IDbCommand.CommandText"/> is initialized to <paramref name="commandText"/> (unless it's null).
        ''' The <see cref="IDbCommand.CommandType"/> property is set to <see cref="CommandType.Text"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="connection"/> is null</exception>
        <Extension()>
        Public Function CreateCommand(ByVal connection As IDbConnection, ByVal commandText$) As IDbCommand
            Return CreateCommand(connection, commandText, CommandType.Text)
        End Function

        ''' <summary>Creates and returns a Command object associated with the connection. Allows to set command text and command type.</summary>
        ''' <param name="connection">Connection to create command on</param>
        ''' <param name="commandText">Text of SQL command. Ignored when null.</param>
        ''' <returns>A Command object associated with the connection.
        ''' The <see cref="IDbCommand.CommandText"/> is initialized to <paramref name="commandText"/> (unless it's null).
        ''' The <see cref="IDbCommand.CommandType"/> property is set to <paramref name="commandType"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="connection"/> is null</exception>
        <Extension()>
        Public Function CreateCommand(ByVal connection As IDbConnection, ByVal commandText$, ByVal commandType As CommandType) As IDbCommand
            If connection Is Nothing Then Throw New ArgumentNullException("connection")
            Dim cmd = connection.CreateCommand
            If commandText IsNot Nothing Then
                cmd.CommandText = commandText
            End If
            cmd.CommandType = commandType
            Return cmd
        End Function


    End Module
End Namespace
#End If

