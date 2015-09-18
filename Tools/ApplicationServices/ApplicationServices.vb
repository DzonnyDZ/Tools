#If True
Namespace ApplicationServicesT
    Public Module ApplicationServices
        ''' <summary>Pareses command line arguments from string array to key-values dictionary</summary>
        ''' <param name="arguments">Application arguments</param>
        ''' <param name="ignore1st">True to indicate that 1st (index 0) argument in <paramref name="arguments"/> contains application executable name and should be ignored by this method</param>
        ''' <returns>Dictionary containing values in front of = from each parameter as keys and remainders of parameters as values.</returns>
        ''' <remarks>Each command line argument is expected to have form <c>key</c> or <c>key=value</c></remarks>
        Public Function ParseParameters(ByVal arguments As IEnumerable(Of String), Optional ByVal ignore1st As Boolean = False) As Dictionary(Of String, List(Of String))
            Dim i% = 0
            Dim ret As New Dictionary(Of String, List(Of String))
            For Each arg In arguments
                Try
                    If i = 0 AndAlso ignore1st Then Continue For
                    Dim name$, value$
                    If arg.Contains("="c) Then
                        name = arg.Substring(0, arg.IndexOf("="c))
                        value = arg.Substring(arg.IndexOf("="c) + 1)
                    Else
                        name = arg
                        value = Nothing
                    End If
                    If ret.ContainsKey(name) AndAlso value <> "" Then
                        ret(name).Add(value)
                    Else
                        ret.Add(name, New List(Of String))
                        If value <> "" Then ret(name).Add(value)
                    End If
                Finally
                    i += 1
                End Try
            Next
            Return ret
        End Function
    End Module
End Namespace
#End If