Imports System.Runtime.InteropServices

#If True
''' <summary>Contains native methods used by Visual-Studio-related tools</summary>
Public Module NativeMethods
    ''' <summary>In case HRESULT represents error throws an exception</summary>
    ''' <param name="hr">HRESULT</param>
    ''' <returns><paramref name="hr"/></returns>
    Public Function ThrowOnFailure(ByVal hr As Integer) As Integer
        Return NativeMethods.ThrowOnFailure(hr, Nothing)
    End Function
    ''' <summary>In case HRESULT represents error throws an exception</summary>
    ''' <param name="hr">HRESULT</param>
    ''' <param name="expectedHRFailure">Expected failure</param>
    ''' <returns><paramref name="hr"/></returns>
    Public Function ThrowOnFailure(ByVal hr As Integer, ByVal ParamArray expectedHRFailure As Integer()) As Integer
        If (NativeMethods.Failed(hr) AndAlso ((expectedHRFailure Is Nothing) OrElse (Array.IndexOf(Of Integer)(expectedHRFailure, hr) < 0))) Then
            Marshal.ThrowExceptionForHR(hr)
        End If
        Return hr
    End Function
    ''' <summary>Indicates if HRESULT is error</summary>
    ''' <param name="hr">HRESULT</param>
    ''' <returns>True if <paramref name="hr"/> is less than zero</returns>
    Public Function Failed(ByVal hr As Integer) As Boolean
        Return (hr < 0)
    End Function

End Module

#End If