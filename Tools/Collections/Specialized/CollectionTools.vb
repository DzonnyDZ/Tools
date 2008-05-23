﻿#If Config <= Nightly Then 'Stage: Nightly
Imports System.Runtime.CompilerServices
Imports Tools.CollectionsT.GenericT

#If Framework >= 3.5 Then
Namespace CollectionsT.SpecializedT
    'ASAP: Mark,  Wiki, Forum
    ''' <summary>Extension methods for working with specialized collections</summary>
    <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(CollectionTools), LastChange:="05/16/2008")> _
    <FirstVersion("05/16/2008")> _
    Public Module CollectionTools
        ''' <summary>Gets last <see cref="Windows.Forms.Control"/> in <see cref="Windows.Forms.Control.ControlCollection"/></summary>
        ''' <param name="Collection">Collection to get last item from</param>
        ''' <returns>Last item in <paramref name="Collection"/>, null if <paramref name="Collection"/> is empty.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Function Last(ByVal Collection As Windows.Forms.Control.ControlCollection) As Windows.Forms.Control
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Collection.Count = 0 Then Return Nothing
            Return Collection(Collection.Count - 1)
        End Function
        ''' <summary>Gets last item in <see cref="BitArray"/></summary>
        ''' <param name="Collection">Collection to get last item from</param>
        ''' <returns>Last item in <paramref name="Collection"/>, false if <paramref name="Collection"/> is empty.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Function Last(ByVal Collection As BitArray) As Boolean
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Collection.Count = 0 Then Return False
            Return Collection(Collection.Count - 1)
        End Function
        ''' <summary>Gets last <see cref="String"/> in <see cref="Specialized.StringCollection"/></summary>
        ''' <param name="Collection">Collection to get last item from</param>
        ''' <returns>Last item in <paramref name="Collection"/>, null if <paramref name="Collection"/> is empty.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Function Last(ByVal Collection As Specialized.StringCollection) As String
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Collection.Count = 0 Then Return Nothing
            Return Collection(Collection.Count - 1)
        End Function
        ''' <summary>Gets last <see cref="Attribute"/> in <see cref="AttributeCollection"/></summary>
        ''' <param name="Collection">Collection to get last item from</param>
        ''' <returns>Last item in <paramref name="Collection"/>, null if <paramref name="Collection"/> is empty.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> Function Last(ByVal Collection As AttributeCollection) As Attribute
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Collection.Count = 0 Then Return Nothing
            Return Collection(Collection.Count - 1)
        End Function
    End Module
End Namespace
#End If
#End If