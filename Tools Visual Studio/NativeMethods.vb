Imports System.Runtime.InteropServices

#If Config <= Nightly Then 'Stage:Nightly
''' <summary>Contains native methods used by Visual-Studio-related tools</summary>
Module NativeMethods
    Public Function ThrowOnFailure(ByVal hr As Integer) As Integer
        Return NativeMethods.ThrowOnFailure(hr, Nothing)
    End Function

    Public Function ThrowOnFailure(ByVal hr As Integer, ByVal ParamArray expectedHRFailure As Integer()) As Integer
        If (NativeMethods.Failed(hr) AndAlso ((expectedHRFailure Is Nothing) OrElse (Array.IndexOf(Of Integer)(expectedHRFailure, hr) < 0))) Then
            Marshal.ThrowExceptionForHR(hr)
        End If
        Return hr
    End Function
    Public Function Failed(ByVal hr As Integer) As Boolean
        Return (hr < 0)
    End Function

End Module

#End If