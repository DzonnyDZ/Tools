Imports System.Runtime.CompilerServices
Imports System.Reflection, System.Linq
Imports System.Data.Objects
Imports System.Data.Objects.DataClasses
Imports Tools.ExtensionsT

#If True
Namespace DataT.ObjectsT
    ''' <summary>Contains extension methods related to Entity Framework</summary>
    ''' <version version="1.5.3">This module is new in version 1.5.3</version>
    Public Module EntityFrameworkExtensions
#Region "Add/Remove"
        ''' <summary>Marks all given entities for deletion from <see cref="IObjectSet(Of TEntity)"/></summary>
        ''' <param name="objectSet">An <see cref="IObjectSet(Of TEntity)"/> to delete entities from</param>
        ''' <param name="objects">Entities to be deleted</param>
        ''' <typeparam name="TEntity">Type of entities</typeparam>
        ''' <exception cref="ArgumentNullException"><paramref name="objectSet"/> is null</exception>
        ''' <exception cref="InvalidOperationException">Removing member object of <paramref name="objects"/> from <paramref name="objectSet"/> reszlted in change in the <paramref name="objects"/> collection. Especially it's invalid to make call like <c>context.Orders.DeleteObjects(customer.Orders)</c> - use somethink like <c>context.Orders.DeleteObjects(customer.Orders.<see cref="Enumerable.ToArray">ToArray</see>)</c> (<see cref="Enumerable.AsEnumerable">AsEnumerable</see> may not be enough).</exception>
        <Extension()>
        Public Sub DeleteObjects(Of TEntity As Class)(ByVal objectSet As IObjectSet(Of TEntity), ByVal objects As IEnumerable(Of TEntity))
            If objectSet Is Nothing Then Throw New ArgumentNullException("objectSet")
            If objects Is Nothing Then Exit Sub
            For Each obj In objects
                objectSet.DeleteObject(obj)
            Next
        End Sub

        ''' <summary>Notifies set than objects representing new entities must be added to the set</summary>
        ''' <param name="objectSet">>An <see cref="IObjectSet(Of TEntity)"/> to notify</param>
        ''' <param name="objects">Objects to be added to the set</param>
        ''' <typeparam name="TEntity">Type of entities</typeparam>
        ''' <exception cref="ArgumentNullException"><paramref name="objectSet"/> is null</exception>
        <Extension()>
        Public Sub AddObjects(Of TEntity As Class)(ByVal objectSet As IObjectSet(Of TEntity), ByVal objects As IEnumerable(Of TEntity))
            If objectSet Is Nothing Then Throw New ArgumentNullException("objectSet")
            If objects Is Nothing Then Exit Sub
            For Each obj In objects
                objectSet.AddObject(obj)
            Next
        End Sub

        ''' <summary>Removes all objects from collection and marks appropriate relations for deletion</summary>
        ''' <param name="collection">Collection to remove objects from</param>
        ''' <param name="objects">Objects to be removed</param>
        ''' <typeparam name="TEntity">Type of entities</typeparam>
        ''' <returns>True if all objects were removed; false if no object was removed; null if some objects were removed and some not. False is also returned in case <paramref name="objects"/> is null or empty.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        <Extension()>
        Public Function RemoveAll(Of TEntity As Class)(ByVal collection As EntityCollection(Of TEntity), ByVal objects As IEnumerable(Of TEntity)) As Boolean?
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            If objects Is Nothing Then Return False
            Dim AllDeleted As Boolean = True
            Dim AnyDeleted As Boolean = False
            For Each item In objects
                Dim result = collection.Remove(item)
                AllDeleted = AllDeleted AndAlso result
                AnyDeleted = AnyDeleted OrElse result
            Next
            If AllDeleted AndAlso Not AnyDeleted Then Return False 'Empty objects
            If AllDeleted Then Return True
            If Not AnyDeleted Then Return False
            Return Nothing
        End Function
        ''' <summary>Adds all given objects to the collection</summary>
        ''' <param name="collection">Collection to add objects to</param>
        ''' <param name="objects">Objects to be added</param>
        ''' <typeparam name="TEntity">Type of entities</typeparam>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        <Extension()>
        Public Sub AddRange(Of TEntity As Class)(ByVal collection As EntityCollection(Of TEntity), ByVal objects As IEnumerable(Of TEntity))
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            If objects Is Nothing Then Return
            For Each item In objects
                collection.Add(item)
            Next
        End Sub

        ''' <summary>Adds given entity to given object context</summary>
        ''' <param name="context">Context to add entity to</param>
        ''' <param name="entity">Entity to be added. Method does nothing if this paraparameter is null.</param>
        ''' <remarks>This method uses reflection to find property of type <see cref="IObjectSet(Of T)"/> of gtype of <paramref name="entity"/>. If such property is found, <paramref name="entity"/> is added to the <see cref="IObjectSet(Of T)"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="context"/> is null.</exception>
        ''' <exception cref="AmbiguousMatchException">More than one <see cref="IObjectSet(Of T)"/> properties are found and none is most specific.</exception>
        ''' <exception cref="MissingMemberException">No suitable <see cref="IObjectSet(Of T)"/> property is found</exception>
        <Extension()>
        Public Sub AddObject(ByVal context As ObjectContext, ByVal entity As EntityObject)
            If context Is Nothing Then Throw New ArgumentNullException("context")
            If entity Is Nothing Then Exit Sub
            Dim TargetProperties As New List(Of PropertyInfo)
            Dim iObjectSet = GetType(IObjectSet(Of )).MakeGenericType(entity.GetType)
            For Each prp In context.GetType.GetProperties
                If iObjectSet.IsAssignableFrom(prp.PropertyType) AndAlso prp.CanRead Then
                    TargetProperties.Add(prp)
                End If
            Next
            Dim eqPrps = From prp In TargetProperties Where prp.PropertyType.Equals(entity.GetType)
            Dim eq2prp = From prp In TargetProperties Where prp.PropertyType.Equals(entity.GetType.BaseType)
            Dim tPrp As PropertyInfo
            If eqPrps.Count = 1 Then
                tPrp = eqPrps(0)
            ElseIf eqPrps.Count > 1 Then
                Throw New AmbiguousMatchException(ResourcesT.Exceptions.NoPropertyIsMostSpecific.f(GetType(IObjectSet(Of )).Name))
            ElseIf eq2prp.Count = 1 Then
                tPrp = eq2prp(0)
            ElseIf eq2prp.Count > 1 Then
                Throw New AmbiguousMatchException(ResourcesT.Exceptions.NoPropertyIsMostSpecific.f(GetType(IObjectSet(Of )).Name))
            ElseIf TargetProperties.Count = 1 Then
                tPrp = TargetProperties(0)
            ElseIf TargetProperties.Count > 0 Then
                Throw New AmbiguousMatchException(ResourcesT.Exceptions.NoPropertyIsMostSpecific.f(GetType(IObjectSet(Of )).Name))
            Else
                Throw New MissingMemberException(ResourcesT.Exceptions.NoSuitablePropertyFound.f(GetType(IObjectSet(Of )).Name))
            End If
            'tPrp.GetValue(context, Nothing).AddObject(entity) 'Late binding
            CallByName(tPrp.GetValue(context, Nothing), "AddObject", CallType.Method, entity)
        End Sub
#End Region
    End Module
End Namespace
#End If