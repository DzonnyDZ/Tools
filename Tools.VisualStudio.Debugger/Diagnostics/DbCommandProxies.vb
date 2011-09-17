Namespace DiagnosticsT
    ''' <summary>Serializable proxy for <see cref="IDbCommand"/> - used for Visual Studio debugger</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Serializable()>
    Public Class DbCommandVisualizerProxy
        Implements IDbCommand

        ''' <summary>CTor - creates a new instance of the <see cref="DbCommandVisualizerProxy"/> class</summary>
        Public Sub New()
        End Sub

        ''' <summary>CTor - creates a new instance of then <see cref="DbCommandVisualizerProxy"/> class from given <see cref="IDbCommand"/> instance</summary>
        ''' <param name="command">The instance to create proxy for</param>
        ''' <exception cref="ArgumentNullException"><paramref name="command"/> is null</exception>
        Public Sub New(command As IDbCommand)
            If command Is Nothing Then Throw New ArgumentNullException("command")
            Me.CommandText = command.CommandText
            Me.CommandTimeout = command.CommandTimeout
            Me.CommandType = command.CommandType
            Me.Connection = command.Connection
            Me._parameters = DataParameterVisualizerCollectionProxy.EnsureSerializable(command.Parameters)
            Me.Transaction = command.Transaction
            Me.UpdatedRowSource = command.UpdatedRowSource
            _originalTypeName = command.GetType.FullName
        End Sub

        Private ReadOnly _originalTypeName As String
        ''' <summary>In case this instance weas initialized form another instance of <see cref="IDbCommand"/> gets name of type this instance was initialized from instance of</summary>
        ''' <returns>Full name of type this instance was initialized from. If this instance was not initialized from another instance of <see cref="IDbCommand"/> returns name of type <see cref="DbCommandVisualizerProxy"/>.</returns>
        Public ReadOnly Property OriginalTypeName As String
            Get
                If _originalTypeName Is Nothing Then Return MyBase.GetType.FullName
                Return _originalTypeName
            End Get
        End Property

        ''' <summary>For given <see cref="IDbCommand"/> gets serializable implementation of <see cref="IDbCommand"/></summary>
        ''' <param name="command">An <see cref="IDbCommand"/> to get serializable instance for</param>
        ''' <returns>Either <paramref name="command"/> (if it is null or serializable) or a new instance of <see cref="DbCommandVisualizerProxy"/>.</returns>
        Public Shared Function EnsureSerializable(command As IDbCommand) As IDbCommand
            If command Is Nothing OrElse command.GetType.IsSerializable Then Return command Else Return New DbCommandVisualizerProxy(command)
        End Function

        ''' <summary>Gets or sets the text command to run against the data source.</summary>
        ''' <returns>The text command to execute. The default value is an empty string ("").</returns>
        ''' <filterpriority>2</filterpriority>
        Public Property CommandText As String Implements IDbCommand.CommandText
        ''' <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
        ''' <returns>The time (in seconds) to wait for the command to execute. The default value is 30 seconds.</returns>
        ''' <exception cref="T:System.ArgumentException">The property value assigned is less than 0. </exception>
        ''' <filterpriority>2</filterpriority>
        Public Property CommandTimeout As Integer Implements IDbCommand.CommandTimeout
        ''' <summary>Indicates or specifies how the <see cref="P:System.Data.IDbCommand.CommandText" /> property is interpreted.</summary>
        ''' <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Property CommandType As CommandType Implements IDbCommand.CommandType

        ''' <summary>Creates a new instance of an <see cref="T:System.Data.IDbDataParameter" /> object.</summary>
        ''' <returns>An <see cref="IDbDataParameter"/> object.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Function CreateParameter() As IDbDataParameter Implements IDbCommand.CreateParameter
            Return New DbDataParameterVisualizerProxy()
        End Function
        Private _parameters As IDataParameterCollection
        ''' <summary>Gets the <see cref="T:System.Data.IDataParameterCollection" />.</summary>
        ''' <returns>The parameters of the SQL statement or stored procedure.</returns>
        ''' <filterpriority>2</filterpriority>
        Public ReadOnly Property Parameters As IDataParameterCollection Implements IDbCommand.Parameters
            Get
                Return _parameters
            End Get
        End Property
        ''' <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
        ''' <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is Both unless the command is automatically generated. Then the default is None.</returns>
        ''' <exception cref="T:System.ArgumentException">The value entered was not one of the <see cref="T:System.Data.UpdateRowSource" /> values. </exception>
        ''' <filterpriority>2</filterpriority>
        Public Property UpdatedRowSource() As UpdateRowSource Implements IDbCommand.UpdatedRowSource

#Region "Methods - not implemented"
        ''' <summary>Not implemented, throws <see cref="NotImplementedException"/></summary>
        ''' <exception cref="NotImplementedException">Always - this method is not implemented</exception>
        Private Sub Cancel() Implements IDbCommand.Cancel
            Throw New NotImplementedException
        End Sub
        ''' <summary>Not implemented, throws <see cref="NotImplementedException"/></summary>
        ''' <returns>Never returns, always throws <see cref="NotImplementedException"/>.</returns>
        ''' <exception cref="NotImplementedException">Always - this method is not implemented</exception>
        Private Function ExecuteNonQuery() As Integer Implements IDbCommand.ExecuteNonQuery
            Throw New NotImplementedException
        End Function
        ''' <summary>Not implemented, throws <see cref="NotImplementedException"/></summary>
        ''' <returns>Never returns, always throws <see cref="NotImplementedException"/>.</returns>
        ''' <exception cref="NotImplementedException">Always - this method is not implemented</exception>
        Private Function ExecuteReader() As IDataReader Implements IDbCommand.ExecuteReader
            Throw New NotImplementedException
        End Function
        ''' <summary>Not implemented, throws <see cref="NotImplementedException"/></summary>
        ''' <param name="behavior">Ignored</param>
        ''' <returns>Never returns, always throws <see cref="NotImplementedException"/>.</returns>
        ''' <exception cref="NotImplementedException">Always - this method is not implemented</exception>
        Private Function ExecuteReader(behavior As CommandBehavior) As IDataReader Implements IDbCommand.ExecuteReader
            Throw New NotImplementedException
        End Function
        ''' <summary>Not implemented, throws <see cref="NotImplementedException"/></summary>
        ''' <exception cref="NotImplementedException">Always - this method is not implemented</exception>
        ''' <returns>Never returns, always throws <see cref="NotImplementedException"/>.</returns>
        Private Function ExecuteScalar() As Object Implements IDbCommand.ExecuteScalar
            Throw New NotImplementedException
        End Function
        ''' <summary>Not implemented, throws <see cref="NotImplementedException"/></summary>
        ''' <exception cref="NotImplementedException">Always - this method is not implemented</exception>
        Private Sub Prepare() Implements IDbCommand.Prepare
            Throw New NotImplementedException
        End Sub
#End Region

#Region "Properties - non serialized"
        <NonSerialized()>
        Private _connection As IDbConnection
        ''' <summary>Gets or sets the <see cref="T:System.Data.IDbConnection" /> used by this instance of the <see cref="T:System.Data.IDbCommand" />.</summary>
        ''' <returns>The connection to the data source.</returns>
        ''' <filterpriority>2</filterpriority>
        Private Property Connection() As IDbConnection Implements IDbCommand.Connection
            Get
                Return _connection
            End Get
            Set(value As IDbConnection)
                _connection = value
            End Set
        End Property
        <NonSerialized()>
        Private _transaction As IDbTransaction
        ''' <summary>Gets or sets the transaction within which the Command object of a .NET Framework data provider executes.</summary>
        ''' <returns>the Command object of a .NET Framework data provider executes. The default value is null.</returns>
        ''' <filterpriority>2</filterpriority>
        Private Property Transaction() As IDbTransaction Implements IDbCommand.Transaction
            Get
                Return _transaction
            End Get
            Set(value As IDbTransaction)
                _transaction = value
            End Set
        End Property

#End Region

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <filterpriority>2</filterpriority>
        Private Sub Dispose() Implements IDisposable.Dispose
            Connection = Nothing
            Transaction = Nothing
        End Sub

    End Class

    ''' <summary>Serializable proxy for <see cref="IDataParameterCollection"/> - used for Visual Studio debugger</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Serializable()>
    Public Class DataParameterVisualizerCollectionProxy
        Inherits List(Of IDataParameter)
        Implements IDataParameterCollection

        ''' <summary>CTor - creates a new instance of the <see cref="DataParameterVisualizerCollectionProxy"/> class</summary>
        Public Sub New()
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="DataParameterVisualizerCollectionProxy"/> class from another <see cref="IDataParameterCollection"/></summary>
        ''' <param name="other">The collection to clone content of</param>
        ''' <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        ''' <remarks>Items from the <paramref name="other"/> collection are ensured to be serializable using <see cref="DbDataParameterVisualizerProxy.EnsureSerializable"/>.</remarks>
        Public Sub New(other As IDataParameterCollection)
            If other Is Nothing Then Throw New ArgumentNullException("other")
            For Each itm As IDataParameter In other
                Me.Add(DbDataParameterVisualizerProxy.EnsureSerializable(itm))
            Next
        End Sub

        ''' <summary>For given <see cref="IDataParameterCollection"/> gets serializable implementation of <see cref="IDataParameterCollection"/></summary>
        ''' <param name="collection">An <see cref="IDataParameterCollection"/> to get serializable instance for</param>
        ''' <returns>Either <paramref name="collection"/> (if it is null or serializable) or a new instance of <see cref="DataParameterVisualizerCollectionProxy"/>.</returns>
        Public Shared Function EnsureSerializable(collection As IDataParameterCollection) As IDataParameterCollection
            If collection Is Nothing OrElse collection.GetType.IsSerializable Then Return collection
            Return New DataParameterVisualizerCollectionProxy(collection)
        End Function


        ''' <summary>Gets a value indicating whether a parameter in the collection has the specified name.</summary>
        ''' <returns>true if the collection contains the parameter; otherwise, false.</returns>
        ''' <param name="parameterName">The name of the parameter. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overloads Function Contains(parameterName As String) As Boolean Implements IDataParameterCollection.Contains
            Return (From par In Me Where par.ParameterName = parameterName).Any
        End Function

        ''' <summary>Gets the location of the <see cref="T:System.Data.IDataParameter" /> within the collection.</summary>
        ''' <returns>The zero-based location of the <see cref="T:System.Data.IDataParameter" /> within the collection.</returns>
        ''' <param name="parameterName">The name of the parameter. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overloads Function IndexOf(parameterName As String) As Integer Implements IDataParameterCollection.IndexOf
            Return Me.FindIndex(Function(p) p.ParameterName = parameterName)
        End Function

        ''' <summary>Gets or sets the parameter at the specified index.</summary>
        ''' <returns>An <see cref="T:System.Object" /> at the specified index.</returns>
        ''' <param name="parameterName">The name of the parameter to retrieve. </param>
        ''' <filterpriority>2</filterpriority>
        Private Overloads Property Item(parameterName As String) As Object Implements IDataParameterCollection.Item
            Get
                Return Me.Find(Function(p) p.ParameterName = parameterName)
            End Get
            Set(value As Object)
                Me(IndexOf(parameterName)) = value
            End Set
        End Property

        ''' <summary>Removes the <see cref="T:System.Data.IDataParameter" /> from the collection.</summary>
        ''' <param name="parameterName">The name of the parameter. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overloads Sub Remove(parameterName As String) Implements IDataParameterCollection.RemoveAt
            Dim index = IndexOf(parameterName)
            If index >= 0 Then RemoveAt(index)
        End Sub
    End Class

    ''' <summary>Serializable proxy for <see cref="IDbDataParameter"/> - used for Visual Studio debugger</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Serializable()>
    Public Class DbDataParameterVisualizerProxy
        Implements IDbDataParameter
        ''' <summary>CTor - creates an new instance of the <see cref="DbDataParameterVisualizerProxy"/> class</summary>
        Public Sub New()
        End Sub

        ''' <summary>CTor - creates an new instance of the <see cref="DbDataParameterVisualizerProxy"/> class from <see cref="IDbDataParameter"/> instance</summary>
        ''' <param name="other">An instance to populate this instance from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        Public Sub New(other As IDbDataParameter)
            If other Is Nothing Then Throw New ArgumentNullException("other")
            With other
                Me.DbType = .DbType
                Me.Direction = .Direction
                Me._isNullable = .IsNullable
                Me.ParameterName = .ParameterName
                Me.SourceColumn = .SourceColumn
                Me.SourceVersion = .SourceVersion
                Me.Value = If(.Value Is Nothing OrElse .Value.GetType.IsSerializable, .Value, String.Format("({0}) {1}", .Value.GetType().FullName, .Value))
                Me.Size = .Size
                Me.Scale = .Scale
                Me.Precision = .Precision
                _originalTypeName = .GetType.FullName
            End With
        End Sub

        Private ReadOnly _originalTypeName As String
        ''' <summary>In case this instance weas initialized form another instance of <see cref="IDbDataParameter"/> gets name of type this instance was initialized from instance of</summary>
        ''' <returns>Full name of type this instance was initialized from. If this instance was not initialized from another instance of <see cref="IDbDataParameter"/> returns name of type <see cref="DbDataParameterVisualizerProxy"/>.</returns>
        Public ReadOnly Property OriginalTypeName As String
            Get
                If _originalTypeName Is Nothing Then Return MyBase.GetType.FullName
                Return _originalTypeName
            End Get
        End Property

        ''' <summary>For given <see cref="IDataParameter"/> gets serializable implementation of <see cref="IDataParameter"/></summary>
        ''' <param name="param">An <see cref="IDataParameter"/> to get serializable instance for</param>
        ''' <returns>Either <paramref name="param"/> (if it is null or serializable) or a new instance of <see cref="DbDataParameterVisualizerProxy"/>.</returns>
        Public Shared Function EnsureSerializable(param As IDataParameter) As IDataParameter
            If param Is Nothing OrElse param.GetType.IsSerializable Then Return param
            Return New DbDataParameterVisualizerProxy(param)
        End Function

        ''' <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
        ''' <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />. </exception>
        ''' <filterpriority>2</filterpriority>
        Public Property DbType As DbType Implements IDataParameter.DbType
        ''' <summary>Gets or sets a value indicating whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
        ''' <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
        ''' <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values. </exception>
        ''' <filterpriority>2</filterpriority>
        Public Property Direction As ParameterDirection Implements IDataParameter.Direction
        Private _isNullable As Boolean
        ''' <summary>Gets a value indicating whether the parameter accepts null values.</summary>
        ''' <returns>true if null values are accepted; otherwise, false. The default is false.</returns>
        ''' <filterpriority>2</filterpriority>
        Public ReadOnly Property IsNullable As Boolean Implements IDataParameter.IsNullable
            Get
                Return _isNullable
            End Get
        End Property
        ''' <summary>Gets or sets the name of the <see cref="T:System.Data.IDataParameter" />.</summary>
        ''' <returns>The name of the <see cref="T:System.Data.IDataParameter" />. The default is an empty string.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Property ParameterName As String Implements IDataParameter.ParameterName
        ''' <summary>Gets or sets the name of the source column that is mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.IDataParameter.Value" />.</summary>
        ''' <returns>The name of the source column that is mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Property SourceColumn As String Implements IDataParameter.SourceColumn
        ''' <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when loading <see cref="P:System.Data.IDataParameter.Value" />.</summary>
        ''' <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
        ''' <exception cref="T:System.ArgumentException">The property was not set one of the <see cref="T:System.Data.DataRowVersion" /> values. </exception>
        ''' <filterpriority>2</filterpriority>
        Public Property SourceVersion As DataRowVersion Implements IDataParameter.SourceVersion
        ''' <summary>Gets or sets the value of the parameter.</summary>
        ''' <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Property Value As Object Implements IDataParameter.Value
        ''' <summary>Indicates the precision of numeric parameters.</summary>
        ''' <returns>The maximum number of digits used to represent the Value property of a data provider Parameter object. The default value is 0, which indicates that a data provider sets the precision for Value.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Property Precision As Byte Implements IDbDataParameter.Precision
        ''' <summary>Indicates the scale of numeric parameters.</summary>
        ''' <returns>The number of decimal places to which <see cref="T:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Property Scale As Byte Implements IDbDataParameter.Scale
        ''' <summary>The size of the parameter.</summary>
        ''' <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the the parameter value.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Property Size As Integer Implements IDbDataParameter.Size
    End Class
End Namespace