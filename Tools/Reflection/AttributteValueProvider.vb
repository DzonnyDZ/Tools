Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Resources

Namespace ReflectionT
    ''' <summary>Provides direct access to values of certain selected attributtes</summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module AttributteValueProvider
        ''' <summary>Gets value indicating if the item is decorated as CLS-compliant</summary>
        ''' <param name="item">Item to tellabout</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> is null</exception>
        ''' <remarks>
        ''' If the <paramref name="item"/> itself is not decorated with <see cref="CLSCompliantAttribute"/> <see cref="Parent"/> is checked. 
        ''' Default value (if neither the item nor any ancestor is decorated with <see cref="CLSCompliantAttribute"/> is false.
        ''' </remarks>
        ''' <seelaso cref="CLSCompliantAttribute"/>
        <Extension()>
        Public Function IsClsCompliant(item As ICustomAttributeProvider) As Boolean
            If item Is Nothing Then Throw New ArgumentNullException("item")
            Dim attr = item.GetAttribute(Of CLSCompliantAttribute)()
            If attr Is Nothing Then
                Dim parent = item.Parent
                If parent Is Nothing Then Return False
                Return IsClsCompliant(parent)
            Else
                Return attr.IsCompliant
            End If
        End Function

#Region "Assembly"
        ''' <summary>Gets trademark for an assembly</summary>
        ''' <param name="assembly">An assembly to get trademark of</param>
        ''' <returns>Assembly trademark. Null if it is not defined</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        ''' <seelaso cref="AssemblyTrademarkAttribute"/>
        <Extension()>
        Public Function Trademark(assembly As Assembly) As String
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            Dim attr = assembly.GetAttribute(Of AssemblyTrademarkAttribute)()
            Return If(attr Is Nothing, Nothing, attr.Trademark)
        End Function

        ''' <summary>Gets company name for an assembly</summary>
        ''' <param name="assembly">An assembly to get company name of</param>
        ''' <returns>Assembly company name. Null if it is not defined</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        ''' <seelaso cref="AssemblyCompanyAttribute"/>
        <Extension()>
        Public Function Company(assembly As Assembly) As String
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            Dim attr = assembly.GetAttribute(Of AssemblyCompanyAttribute)()
            Return If(attr Is Nothing, Nothing, attr.Company)
        End Function

        ''' <summary>Gets copyright notice for an assembly</summary>
        ''' <param name="assembly">An assembly to get company copyright of</param>
        ''' <returns>Assembly copyright notice. Null if it is not defined</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        ''' <seelaso cref="AssemblyCopyrightAttribute"/>
        <Extension()>
        Public Function Copyright(assembly As Assembly) As String
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            Dim attr = assembly.GetAttribute(Of AssemblyCopyrightAttribute)()
            Return If(attr Is Nothing, Nothing, attr.Copyright)
        End Function

        ''' <summary>Gets assembly description</summary>
        ''' <param name="assembly">An assembly to get description of</param>
        ''' <returns>Assembly description. Null if it is not defined</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        ''' <seelaso cref="AssemblyDescriptionAttribute"/>
        <Extension()>
        Public Function Description(assembly As Assembly) As String
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            Dim attr = assembly.GetAttribute(Of AssemblyDescriptionAttribute)()
            Return If(attr Is Nothing, Nothing, attr.Description)
        End Function

        ''' <summary>Gets informational version for an assembly</summary>
        ''' <param name="assembly">An assembly to get informational version of</param>
        ''' <returns>Assembly informational version. Null if it is not defined</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        ''' <seelaso cref="AssemblyInformationalVersionAttribute"/>
        <Extension()>
        Public Function InformationalVersion(assembly As Assembly) As String
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            Dim attr = assembly.GetAttribute(Of AssemblyInformationalVersionAttribute)()
            Return If(attr Is Nothing, Nothing, attr.InformationalVersion)
        End Function

        ''' <summary>Gets product name for an assembly</summary>
        ''' <param name="assembly">An assembly to get product name of</param>
        ''' <returns>Assembly product name. Null if it is not defined</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        ''' <seelaso cref="AssemblyProductAttribute"/>
        <Extension()>
        Public Function Product(assembly As Assembly) As String
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            Dim attr = assembly.GetAttribute(Of AssemblyProductAttribute)()
            Return If(attr Is Nothing, Nothing, attr.Product)
        End Function

        ''' <summary>Gets title of an assembly</summary>
        ''' <param name="assembly">An assembly to get title of</param>
        ''' <returns>Assembly title. Null if it is not defined</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        ''' <seelaso cref="AssemblyTitleAttribute"/>
        <Extension()>
        Public Function Title(assembly As Assembly) As String
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            Dim attr = assembly.GetAttribute(Of AssemblyTitleAttribute)()
            Return If(attr Is Nothing, Nothing, attr.Title)
        End Function

        ''' <summary>Gets language of resources embeded in this neutral assembly</summary>
        ''' <param name="assembly">An assembly to neutral resource language for</param>
        ''' <returns>Language of neutral resources in assembly. Null if it is not defined</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        ''' <seelaso cref="NeutralResourcesLanguageAttribute"/>
        <Extension()>
        Public Function NeutralResourcesLanguage(assembly As Assembly) As String
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            Dim attr = assembly.GetAttribute(Of NeutralResourcesLanguageAttribute)()
            Return If(attr Is Nothing, Nothing, attr.CultureName)
        End Function
#End Region
    End Module
End Namespace