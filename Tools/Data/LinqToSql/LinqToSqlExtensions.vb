Imports System.Runtime.CompilerServices, System.Linq, Tools.ExtensionsT
Namespace Data.LinqToSql
#If Config <= Nightly Then
    ''' <summary>Contains extension methods related to LINQ-to-SQL technology</summary>
    ''' <version version="1.5.3" stage="Nightly">This module is new in version 1.5.3</version>
    Public Module LinqToSqlExtensions
        ''' <summary>Undos all INSERTs performed with <see cref="System.Data.Linq.Table(Of TEntity)"/></summary>
        ''' <param name="Table">Table to delete all inserted not commited items of coresponding type from</param>
        ''' <typeparam name="TEntity">Type of items in table</typeparam>
        ''' <remarks>This methods delets all items in <paramref name="Table"/>.<see cref="System.Data.Linq.Table.Context">Context</see>.<see cref="System.Data.Linq.DataContext.GetChangeSet">GetChangeSet</see>.<see cref="System.Data.Linq.ChangeSet.Inserts">Inserts</see> which are of type <typeparamref name="TEntity"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Table"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Table"/>.<see cref="System.Data.Linq.Table.Context">Context</see> is null</exception>
        <Extension()> Public Sub DeleteAllNew(Of TEntity As Class)(ByVal Table As System.Data.Linq.Table(Of TEntity))
            If Table Is Nothing Then Throw New ArgumentNullException("Table")
            If Table.Context Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.MustNotBeNull.f("Table.Context"))
            Table.DeleteAllOnSubmit(Table.Context.GetChangeSet.Inserts.OfType(Of TEntity))
        End Sub
        ''' <summary>Undos all DELETEs performed with <see cref="System.Data.Linq.Table(Of TEntity)"/></summary>
        ''' <param name="Table">Table to insert (undelete) all deleted not commited items of coresponding type to</param>
        ''' <typeparam name="TEntity">Type of items in table</typeparam>
        ''' <remarks>This methods re-inserts all items in <paramref name="Table"/>.<see cref="System.Data.Linq.Table.Context">Context</see>.<see cref="System.Data.Linq.DataContext.GetChangeSet">GetChangeSet</see>.<see cref="System.Data.Linq.ChangeSet.Deletes">Deletes</see> which are of type <typeparamref name="TEntity"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Table"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Table"/>.<see cref="System.Data.Linq.Table.Context">Context</see> is null</exception>
        <Extension()> Public Sub RecoverAllDeleted(Of TEntity As Class)(ByVal Table As System.Data.Linq.Table(Of TEntity))
            If Table Is Nothing Then Throw New ArgumentNullException("Table")
            If Table.Context Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.MustNotBeNull.f("Table.Context"))
            Table.InsertAllOnSubmit(Table.Context.GetChangeSet.Deletes.OfType(Of TEntity))
        End Sub
    End Module
#End If
End Namespace
