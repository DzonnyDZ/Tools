'http://www.codeproject.com/KB/security/SimpleEncryption.aspx
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration
Imports System.ComponentModel

' A simple, string-oriented wrapper class for encryption functions, including 
' Hashing, Symmetric Encryption, and Asymmetric Encryption.
'
'  Jeff Atwood
'   http://www.codinghorror.com/
Namespace SecurityT.CryptographyT.EncryptionT
    ''' <summary>Provides static utility methods used by multiple <see cref="EncryptionT"/> classes</summary>
    Friend Class EncryptionUtilities
        ''' <summary>There's no CTor - this is static class</summary>
        Partial Private Sub New()
        End Sub
        ''' <summary>Converts an array of bytes to a string Hex representation</summary>
        ''' <param name="data">Array of bytes</param>
        ''' <returns>Hexadecimal representation of <paramref name="data"/>; null if <paramref name="data"/> is null; <see cref="System.String.Empty"/> if <paramref name="data"/> is an empty array.</returns>
        Public Shared Function ToHex(ByVal data() As Byte) As String
            If data Is Nothing OrElse data.Length = 0 Then
                Return If(data Is Nothing, Nothing, String.Empty)
            End If
            Const HexFormat As String = "{0:X2}"
            Dim sb As New StringBuilder
            For Each b As Byte In data
                sb.Append(String.Format(HexFormat, b))
            Next
            Return sb.ToString
        End Function

        ''' <summary>Converts from a string Hex representation to an array of bytes</summary>
        ''' <param name="hexEncoded">A hexastring</param>
        ''' <remarks>Array of bytes reconstructed from <paramref name="hexEncoded"/>; null if <paramref name="hexEncoded"/> is null; an empty array if <paramref name="hexEncoded"/> is an empty string.</remarks>
        ''' <exception cref="FormatException">String contains an invalid character (character not representing hexanumber)</exception>
        Public Shared Function FromHex(ByVal hexEncoded As String) As Byte()
            If hexEncoded Is Nothing Then
                Return Nothing
            ElseIf hexEncoded.Length = 0 Then
                Return New Byte() {}
            End If
            Try
                Dim l As Integer = Convert.ToInt32(hexEncoded.Length / 2)
                Dim b(l - 1) As Byte
                For i As Integer = 0 To l - 1
                    b(i) = Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16)
                Next
                Return b
            Catch ex As Exception
                Throw New System.FormatException(ResourcesT.Exceptions.HexStringInvalid & _
                    Environment.NewLine & hexEncoded & Environment.NewLine, ex)
            End Try
        End Function

        ''' <summary>Converts from a string Base64 representation to an array of bytes</summary>
        ''' <param name="base64Encoded">Base64-encoded string</param>
        ''' <remarks>Array of bytes; null if <paramref name="base64Encoded"/> is null; an empty array if <paramref name="base64Encoded"/> is an empty string</remarks>
        ''' <exception cref="FormatException"><paramref name="base64Encoded"/> is invalid base64-encoded string</exception>
        Public Shared Function FromBase64(ByVal base64Encoded As String) As Byte()
            If base64Encoded Is Nothing Then
                Return Nothing
            ElseIf base64Encoded.Length = 0 Then
                Return New Byte() {}
            End If
            Try
                Return Convert.FromBase64String(base64Encoded)
            Catch ex As System.FormatException
                Throw New System.FormatException(ResourcesT.Exceptions.Base64StringInvalid & _
                    Environment.NewLine & base64Encoded & Environment.NewLine, ex)
            End Try
        End Function

        ''' <summary>Converts from an array of bytes to a string Base64 representation</summary>
        ''' <param name="data">A data to convert</param>
        ''' <returns>Base64 representation of <paramref name="data"/>; null if <paramref name="data"/> is null; an empty string if <paramref name="data"/> is an empty array</returns>
        Public Shared Function ToBase64(ByVal data() As Byte) As String
            If data Is Nothing OrElse data.Length = 0 Then
                Return If(data Is Nothing, Nothing, String.Empty)
            End If
            Return Convert.ToBase64String(data)
        End Function

        ''' <summary>Retrieves an element from an XML string</summary>
        Friend Shared Function GetXmlElement(ByVal xml As String, ByVal element As String) As String
            Dim m As Match
            m = Regex.Match(xml, "<" & element & ">(?<Element>[^>]*)</" & element & ">", RegexOptions.IgnoreCase)
            If m Is Nothing Then
                Throw New Exception("Could not find <" & element & "></" & element & "> in provided Public Key XML.")
            End If
            Return m.Groups("Element").ToString
        End Function

        ''' <summary>Returns the specified string value from the application .config file</summary>
        Friend Shared Function GetConfigString(ByVal key As String, _
            Optional ByVal isRequired As Boolean = True) As String

            Dim s As String = CType(ConfigurationManager.AppSettings.Get(key), String)
            If s = Nothing Then
                If isRequired Then
                    Throw New ConfigurationErrorsException("key <" & key & "> is missing from .config file")
                Else
                    Return ""
                End If
            Else
                Return s
            End If
        End Function

        Friend Shared Function WriteConfigKey(ByVal key As String, ByVal value As String) As String
            Dim s As String = "<add key=""{0}"" value=""{1}"" />" & Environment.NewLine
            Return String.Format(s, key, value)
        End Function

        Friend Shared Function WriteXmlElement(ByVal element As String, ByVal value As String) As String
            Dim s As String = "<{0}>{1}</{0}>" & Environment.NewLine
            Return String.Format(s, element, value)
        End Function

        Friend Shared Function WriteXmlNode(ByVal element As String, Optional ByVal isClosing As Boolean = False) As String
            Dim s As String
            If isClosing Then
                s = "</{0}>" & Environment.NewLine
            Else
                s = "<{0}>" & Environment.NewLine
            End If
            Return String.Format(s, element)
        End Function

    End Class
End Namespace