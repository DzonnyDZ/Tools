Imports System.Drawing
Imports System.Globalization.CultureInfo
Imports System.ComponentModel
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports Tools.CodeDomT.CompilerT
Imports Tools.ExtensionsT
Imports System.Configuration

''' <summary>Provides functions for Splash Screen generation</summary>
''' <remarks>
''' Splash screens are generated from existing image by wrinting certain assembly information to that image.
''' Typically you use this program from command line however programatic access is also possible.
''' <para>Command line usage:</para>
''' <para><c>SplashScreenGenerator infile outfile assemblies informations</c></para>
''' <list type="table">
''' <item><term><c>infile</c></term><description>Path to original image file (positional, mandatory)</description></item>
''' <item><term><c>outfile</c></term><description>Path to write resulting image to. Must differ from <c>infile</c>. (positional, mandatory)</description></item>
''' <item><term><c>assemblies</c></term><description>
''' Specifies paths to source code files to load assembly information from.
''' Each file is specified in format <c>-a path</c> or <c>-assemblyInfo path</c>. Argument identifier (<c>-a</c> or <c>-assemblyInfo</c>) is case-insensitive.
''' At least one assembly info must be specified. If multiple assembly infos are specified all must be in the same programming language (identified by file extension (case-insensitive)).
''' Given source files are compiled using regestered compiler for that language (see <see cref="CodeDomProvider.CreateProvider"/>).
''' Argument <c>/define:ASSEMBLYINFO</c> is passed to the compiler, so, if compiler supports it preprocessor directive <c>ASSEMBLYINFO</c> is defined.
''' <para><c>-a</c>/<c>-assemblyInfo</c> arguments can be mixed with other arguments.</para>
''' </description></item>
''' <item><term><c>informations</c></term><description>
''' Zero or more deffinition of information blocks, each difining single information to be rendered to resulting image.
''' Each information definition block is in format <c>-informationType fontSize offset [foregroundColor [backgroundColor [font [fontStyle [format]]]]]</c>
''' <list type="table">
''' <item><term><c>informationType</c></term><description>Must be preceded with <c>-</c>, one of <see cref="InfoItemType"/> values or names (case-insensitive). It's passed to <see cref="System.[Enum].Parse"/>. Mandatory, positional. See <see cref="InfoItem.Type"/>.</description></item>
''' <item><term><c>fontSize</c></term><description>Size of font to render information in points. Value is passed to <see cref="Int32.Parse"/> with <see cref="InvariantCulture"/>. Mandatory, positional. See <see cref="InfoItem.FontSize"/>.</description></item>
''' <item><term><c>offset</c></term><description>Offset, in pixels, where to render top-left corner of string to render. Value specified is passed to <see cref="TypeConverter"/> for <see cref="Point"/> structure in <see cref="InvariantCulture"/>. Mandatory, positional. See <see cref="InfoItem.Offset"/>.</description></item>
''' <item><term><c>foregroundColor</c></term><description>Color to render text in. Value is passed to <see cref="TypeConverter"/> for <see cref="Color"/> with <see cref="InvariantCulture"/>. Optional, positional, default "Black". See <see cref="InfoItem.Foreground"/>.</description></item>
''' <item><term><c>backgroundColor</c></term><description>Color to render text background in. Value is passed to <see cref="TypeConverter"/> for <see cref="Color"/> with <see cref="InvariantCulture"/>. Optional, positional, default "Transparent". See <see cref="InfoItem.Background"/>.</description></item>
''' <item><term><c>font</c></term><description>Name of font to render information. Value is passed to <see cref="Font"/> CTor. Positional, optional, default "Arial". Seee <see cref="InfoItem.Font"/>.</description></item>
''' <item><term><c>fontStyle</c></term><description>Style of font to render information. A numeric or string value passed to <see cref="[Enum].Parse"/> for <see cref="FontStyle"/>. Optional, positional, default "Regular". See <see cref="InfoItem.FontStyle"/>.</description></item>
''' <item><term><c>format</c></term><description>Formatting string to format values by. Placceholder {0} is replaced with actual assembly information. For type "Text" {0} is replaced with null. For type "Version" formatting string indicating how much parts of <see cref="Version"/> are rendered is possible. See <see cref="InfoItem.Format"/>.</description></item>
''' </list>
''' Values of all parameters but <c>informationType</c> must not start with <c>-</c> otherwise they are treated as indicator of next argument.
''' Optional parameters cannot be skipped, specify default value instead.
''' <para>Information definition block arguments can be mixed with other arguments.</para>
''' See <see cref="InfoItem"/> and <see cref="InfoItemType"/> for more information.
''' </description></item>
''' </list>
''' </remarks>
Public NotInheritable Class SplashScreenGenerator
    ''' <summary>Private CTor to achieve pseudo-static class</summary>
    ''' <exception cref="NotSupportedException">Always</exception>
    Private Sub New()
        Throw New NotSupportedException("This is static class")
    End Sub

    ''' <summary>Entry point method for Splash Screen Generator application</summary>
    Friend Shared Sub Main()
        Try
            Console.WriteLine("{0} {1}", My.Application.Info.Title, My.Application.Info.Version)

            If My.Application.CommandLineArgs.Count < 2 Then
                Console.WriteLine(My.Resources.Usage, My.Application.Info.Title)
                Environment.Exit(1)
                End
            End If

            Dim infile$ = Nothing
            Dim outfile$ = Nothing
            Dim assemblyInfo As New List(Of String)
            Dim info As New List(Of InfoItem)

            Dim pointConverter = TypeDescriptor.GetConverter(GetType(Point))
            Dim colorConverter = TypeDescriptor.GetConverter(GetType(Color))
            Dim state = CommandLineParsing.InFile
            For Each arg In My.Application.CommandLineArgs
                Select Case state
                    Case CommandLineParsing.InFile
                        infile = arg
                        state = CommandLineParsing.OutFile
                    Case CommandLineParsing.OutFile
                        outfile = arg
                        state = CommandLineParsing.Args
                    Case CommandLineParsing.Args
args:                   Select Case arg.ToLowerInvariant
                            Case "-a", "-assemblyinfo"
                                state = CommandLineParsing.AssemblyInfo
                            Case Else
                                Dim itemType As InfoItemType
                                If arg.StartsWith("-") AndAlso [Enum].TryParse(arg.Substring(1), True, itemType) Then
                                    info.Add(New InfoItem With {.Type = itemType})
                                    state = CommandLineParsing.ii_FontSize
                                Else
                                    Console.WriteLine(My.Resources.UnknownOption, arg)
                                    Environment.Exit(2)
                                    End
                                End If
                        End Select
                    Case CommandLineParsing.AssemblyInfo
                        assemblyInfo.Add(arg)
                        state = CommandLineParsing.Args
                    Case CommandLineParsing.ii_FontSize
                        info.Last.FontSize = Integer.Parse(arg, InvariantCulture)
                        state = CommandLineParsing.ii_Offset
                    Case CommandLineParsing.ii_Offset
                        info.Last.Offset = pointConverter.ConvertFrom(Nothing, InvariantCulture, arg)
                        state = CommandLineParsing.ii_Foreground
                    Case CommandLineParsing.ii_Foreground
                        If arg.StartsWith("-") Then GoTo args
                        info.Last.Foreground = colorConverter.ConvertFrom(Nothing, InvariantCulture, arg)
                        state = CommandLineParsing.ii_Background
                    Case CommandLineParsing.ii_Background
                        If arg.StartsWith("-") Then GoTo args
                        info.Last.Background = colorConverter.ConvertFrom(Nothing, InvariantCulture, arg)
                        state = CommandLineParsing.ii_Font
                    Case CommandLineParsing.ii_Font
                        If arg.StartsWith("-") Then GoTo args
                        info.Last.Font = arg
                        state = CommandLineParsing.ii_FontStyle
                    Case CommandLineParsing.ii_FontStyle
                        If arg.StartsWith("-") Then GoTo args
                        info.Last.FontStyle = [Enum].Parse(GetType(FontStyle), arg, True)
                        state = CommandLineParsing.ii_Format
                    Case CommandLineParsing.ii_Format
                        If arg.StartsWith("-") Then GoTo args
                        info.Last.Format = arg
                        state = CommandLineParsing.Args
                End Select
            Next
            Select Case state
                Case CommandLineParsing.ii_Background, CommandLineParsing.ii_Foreground, CommandLineParsing.ii_Font, CommandLineParsing.ii_FontStyle, CommandLineParsing.ii_Format  'are OK
                Case Is <> CommandLineParsing.Args
                    Console.WriteLine(My.Resources.OptionMissing, state)
                    Environment.Exit(3)
                    End
            End Select

            If assemblyInfo.Count = 0 Then
                Console.WriteLine(My.Resources.AssemblyInfoMissing)
                Environment.Exit(4)
            End If

            GenerateSplashScreen(infile, outfile, assemblyInfo, info)
        Catch ex As Exception
            Console.WriteLine(My.Resources.GeneralError, ex.GetType.Name, ex.Message)
            Environment.Exit(8)
            End
        End Try
    End Sub

#Region "GenerateSplashScreen"
    ''' <summary>Generates a splash screen image from given splash screen image file and saves it to another splash screen image file obtaining assembly information form suorce files</summary>
    ''' <param name="infile">Path of original image file to render assembly information to</param>
    ''' <param name="outfile">Path of target image file to save image with assembly information added to</param>
    ''' <param name="assemblyInfos">Collection of paths of source code files to load assembly information of. The files must all have same extension (case-insensitive). The files are compiled using appropriate language compiler to generate temporary assembly to load assembly information from.</param>
    ''' <param name="items">Identifies items to render to image</param>
    ''' <exception cref="ArgumentNullException">Any parameter is null -or- an item in <paramref name="items"/> or <paramref name="assemblyInfos"/> collection is null.</exception>
    ''' <exception cref="ArgumentException">
    ''' <paramref name="assemblyInfos"/> is an empty collection or contains paths with different extensions (case-insensitive) -or-
    ''' There was a problem loading bitmap from <paramref name="infile"/>, see <see cref="Exception.InnerException"/> for details. -or-
    ''' Image loaded from <paramref name="infile"/> has an indexed pixel format or its format is undefined, see <see cref="Exception.InnerException"/> for details.
    ''' </exception>
    ''' <exception cref="ConfigurationErrorsException">Language identified by extension of path given in <paramref name="assemblyInfos"/> does not have a configured provider on this computer.</exception>
    ''' <exception cref="Security.SecurityException">The caller does not have the required permission (when creating <see cref="CodeDomProvider"/>).</exception>
    ''' <exception cref="CompilerErrorException">There was an / were error(s) during compilation of assembly info file(s)</exception>
    ''' <exception cref="FormatException">Item in <paramref name="items"/> has property <see cref="InfoItem.Format"/> which represents invalid format for <see cref="System.String.Format"/> or contains placeholder other than {0}.</exception>
    ''' <exception cref="Runtime.InteropServices.ExternalException">
    ''' An attempt to save an image with wrong image format. -or-
    ''' <paramref name="infile"/> and <paramref name="outfile"/> represent the same file.
    ''' </exception>
    ''' <exception cref="IO.IOException">Another error saving image to <paramref name="outfile"/>.</exception>
    Public Shared Sub GenerateSplashScreen(infile$, outfile$, assemblyInfos As IEnumerable(Of String), items As IEnumerable(Of InfoItem))
        If infile Is Nothing Then Throw New ArgumentNullException("infile")
        If outfile Is Nothing Then Throw New ArgumentNullException("outfile")
        If assemblyInfos Is Nothing Then Throw New ArgumentNullException("assemblyInfos")
        If items Is Nothing Then Throw New ArgumentNullException("items")

        Dim asm = CompileAssemblyInfo(assemblyInfos)
        GenerateSplashScreen(infile, outfile, items, asm)
    End Sub

    ''' <summary>Generates a splash screen image from given splash screen image file and saves it to another splash screen image file obtaining assembly information form source files</summary>
    ''' <param name="infile">Path of original image file to render assembly information to</param>
    ''' <param name="outfile">Path of target image file to save image with assembly information added to</param>
    ''' <param name="assemblyInfo">Path of source code file to load assembly information of. The file is compiled using appropriate language compiler to generate temporary assembly to load assembly information from.</param>
    ''' <param name="items">Identifies items to render to image</param>
    ''' <exception cref="ArgumentNullException">Any parameter is null -or- an item in <paramref name="items"/> collection is null.</exception>
    ''' <exception cref="ArgumentException">
    ''' There was a problem loading bitmap from <paramref name="infile"/>, see <see cref="Exception.InnerException"/> for details. -or-
    ''' Image loaded from <paramref name="infile"/> has an indexed pixel format or its format is undefined, see <see cref="Exception.InnerException"/> for details.
    ''' </exception>
    ''' <exception cref="ConfigurationErrorsException">Language identified by extension of path given in <paramref name="assemblyInfo"/> does not have a configured provider on this computer.</exception>
    ''' <exception cref="Security.SecurityException">The caller does not have the required permission (when creating <see cref="CodeDomProvider"/>).</exception>
    ''' <exception cref="CompilerErrorException">There was an / were error(s) during compilation of assembly info file(s)</exception>
    ''' <exception cref="FormatException">Item in <paramref name="items"/> has property <see cref="InfoItem.Format"/> which represents invalid format for <see cref="System.String.Format"/> or contains placeholder other than {0}.</exception>
    ''' <exception cref="Runtime.InteropServices.ExternalException">
    ''' An attempt to save an image with wrong image format. -or-
    ''' <paramref name="infile"/> and <paramref name="outfile"/> represent the same file.
    ''' </exception>
    ''' <exception cref="IO.IOException">Another error saving image to <paramref name="outfile"/>.</exception>
    Public Shared Sub GenerateSplashScreen(infile$, outfile$, assemblyInfo As String, items As IEnumerable(Of InfoItem))
        If assemblyInfo Is Nothing Then Throw New ArgumentNullException("assemblyInfo")
        GenerateSplashScreen(infile, outfile, {assemblyInfo}, items)
    End Sub

    ''' <summary>Compiles assembly form assembly info source files</summary>
    ''' <param name="assemblyInfos">Collection of paths of source code files to load assembly information of. The files must all have same extension (case-insensitive). The files are compiled using appropriate language compiler to generate temporary assembly to load assembly information from. Files are not by-design restricted to assembly info files but it's highly recomemnded to keep them as simple as possible.</param>
    ''' <returns>Compiled assembly</returns>
    ''' <remarks>Compilation defines preprocessor directive <c>ASSEMBLYINFO</c> (if compiler supports <c>/define:</c> parameter.</remarks>
    ''' <exception cref="ArgumentNullException"><paramref name="assemblyInfos"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="assemblyInfos"/> is an empty collection or contains paths with different extensions (case-insensitive)</exception>
    ''' <exception cref="ConfigurationErrorsException">Language identified by extension of path given in <paramref name="assemblyInfos"/> does not have a configured provider on this computer.</exception>
    ''' <exception cref="Security.SecurityException">The caller does not have the required permission (when creating <see cref="CodeDomProvider"/>).</exception>
    ''' <exception cref="CompilerErrorException">There was an / were error(s) during compilation of assembly info file(s)</exception>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Shared Function CompileAssemblyInfo(ByVal assemblyInfos As IEnumerable(Of String)) As Assembly
        If assemblyInfos Is Nothing Then Throw New ArgumentNullException("assemblyInfos")
        Dim extension As String = Nothing
        Dim i% = 0
        For Each ai In assemblyInfos
            If ai Is Nothing Then Throw New ArgumentNullException("assemblyInfos")
            If i = 0 Then
                extension = IO.Path.GetExtension(ai).ToLowerInvariant
            ElseIf IO.Path.GetExtension(ai).ToLowerInvariant <> extension Then
                Throw New ArgumentException(My.Resources.AssemblyInfoExtensionsMustBeSame, "assemblyInfos")
            End If
            i += 1
        Next
        If i = 0 Then Throw New ArgumentException(My.Resources.AtLeastOneAssemblyInfoIsRequired, "assemblyInfos")

        Dim provider As CodeDomProvider = CodeDomProvider.CreateProvider(extension.Substring(1))
        Dim pars As CompilerParameters = New CompilerParameters() With {.GenerateExecutable = False, .GenerateInMemory = True}
        pars.CompilerOptions = "/define:ASSEMBLYINFO"
        Dim compiled = provider.CompileAssemblyFromFile(pars, assemblyInfos.ToArray)
        If compiled.Errors.Count > 0 Then _
            Throw New CompilerErrorException(compiled.Errors)
        Return compiled.CompiledAssembly
    End Function

    ''' <summary>Generates a splash screen image from given splash screen image file and saves it to another splash screen image file obtaining assembly information form given <see cref="Assembly"/></summary>
    ''' <param name="infile">Path of original image file to render assembly information to</param>
    ''' <param name="outfile">Path of target image file to save image with assembly information added to</param>
    ''' <param name="assembly">An assembly to read assembly information from. Assembly can be obtained from source files using <see cref="CompileAssemblyInfo"/>.</param>
    ''' <param name="items">Identifies items to render to image</param>
    ''' <exception cref="ArgumentNullException">Any parameter is null -or- an item in <paramref name="items"/> collection is null.</exception>
    ''' <exception cref="ArgumentException">
    ''' There was a problem loading bitmap from <paramref name="infile"/>, see <see cref="Exception.InnerException"/> for details. -or-
    ''' Image loaded from <paramref name="infile"/> has an indexed pixel format or its format is undefined, see <see cref="Exception.InnerException"/> for details.
    ''' </exception>
    ''' <exception cref="FormatException">Item in <paramref name="items"/> has property <see cref="InfoItem.Format"/> which represents invalid format for <see cref="System.String.Format"/> or contains placeholder other than {0}.</exception>
    ''' <exception cref="Runtime.InteropServices.ExternalException">
    ''' An attempt to save an image with wrong image format. -or-
    ''' <paramref name="infile"/> and <paramref name="outfile"/> represent the same file.
    ''' </exception>
    ''' <exception cref="IO.IOException">Another error saving image to <paramref name="outfile"/>.</exception>
    ''' <seelaso cref="CompileAssemblyInfo"/>
    Public Shared Sub GenerateSplashScreen(ByVal infile$, ByVal outfile$, ByVal items As IEnumerable(Of InfoItem), ByVal assembly As Assembly)
        Dim ainfo As New AssemblyInfo(assembly)
        GenerateSplashScreen(infile, outfile, items, ainfo)
    End Sub

    ''' <summary>Generates a splash screen image from given splash screen image file and saves it to another splash screen image file obtaining assembly information from <see cref="AssemblyInfo"/> object</summary>
    ''' <param name="infile">Path of original image file to render assembly information to</param>
    ''' <param name="outfile">Path of target image file to save image with assembly information added to</param>
    ''' <param name="assemblyInfo">An <see cref="AssemblyInfo"/> object providing assembly information</param>
    ''' <param name="items">Identifies items to render to image</param>
    ''' <exception cref="ArgumentNullException">Any parameter is null -or- an item in <paramref name="items"/> collection is null.</exception>
    ''' <exception cref="ArgumentException">
    ''' There was a problem loading bitmap from <paramref name="infile"/>, see <see cref="Exception.InnerException"/> for details. -or-
    ''' Image loaded from <paramref name="infile"/> has an indexed pixel format or its format is undefined, see <see cref="Exception.InnerException"/> for details.
    ''' </exception>
    ''' <exception cref="FormatException">Item in <paramref name="items"/> has property <see cref="InfoItem.Format"/> which represents invalid format for <see cref="System.String.Format"/> or contains placeholder other than {0}.</exception>
    ''' <exception cref="Runtime.InteropServices.ExternalException">
    ''' An attempt to save an image with wrong image format. -or-
    ''' <paramref name="infile"/> and <paramref name="outfile"/> represent the same file.
    ''' </exception>
    ''' <exception cref="IO.IOException">Another error saving image to <paramref name="outfile"/>.</exception>
    Public Shared Sub GenerateSplashScreen(ByVal infile$, ByVal outfile$, ByVal items As IEnumerable(Of InfoItem), ByVal assemblyInfo As AssemblyInfo)
        Dim creatingBitmap As Boolean = True
        Try
            Using image = New Bitmap(infile)
                creatingBitmap = False
                GenerateSplashScreen(image, items, assemblyInfo)
                Try
                    image.Save(outfile)
                Catch ex As Exception When Not TypeOf ex Is ArgumentNullException AndAlso Not TypeOf ex Is Runtime.InteropServices.ExternalException AndAlso Not TypeOf ex Is IO.IOException
                    Throw New IO.IOException(ex.Message, ex)
                End Try
            End Using
        Catch ex As Exception When creatingBitmap AndAlso Not TypeOf ex Is ArgumentException
            Throw New ArgumentException(ex.Message, "infile", ex)
        End Try
    End Sub

    ''' <summary>Generates a splash screen image from given splash screen image file and saves it to another splash screen image file obtaining assembly information form <see cref="AssemblyInfo"/> object</summary>
    ''' <param name="image">Image to render information to</param>
    ''' <param name="assemblyInfo">An <see cref="AssemblyInfo"/> object providing assembly information</param>
    ''' <param name="items">Identifies items to render to image</param>
    ''' <exception cref="ArgumentNullException">Any parameter is null -or- an item in <paramref name="items"/> collection is null.</exception>
    ''' <exception cref="ArgumentException">Image loaded from <paramref name="image"/> has an indexed pixel format or its format is undefined, see <see cref="Exception.InnerException"/> for details.</exception>
    ''' <exception cref="FormatException">Item in <paramref name="items"/> has property <see cref="InfoItem.Format"/> which represents invalid format for <see cref="System.String.Format"/> or contains placeholder other than {0}.</exception>
    Public Shared Sub GenerateSplashScreen(image As Image, items As IEnumerable(Of InfoItem), assemblyInfo As AssemblyInfo)
        If assemblyInfo Is Nothing Then Throw New ArgumentNullException("assemblyInfo")
        If image Is Nothing Then Throw New ArgumentNullException("image")
        Dim creatingGraphic = True
        Try
            Using g = Graphics.FromImage(image)
                creatingGraphic = False
                For Each item In items
                    item.Render(g, assemblyInfo)
                Next
                g.Flush()
            End Using
        Catch ex As Exception When (creatingGraphic AndAlso Not TypeOf ex Is ArgumentNullException) AndAlso Not TypeOf ex Is ArgumentException
            Throw New ArgumentException(ex.Message, "image", ex)
        End Try
    End Sub

    ''' <summary>Generates a splash screen image from given splash screen image file and saves it to another splash screen image file obtaining assembly information form given <see cref="Assembly"/></summary>
    ''' <param name="image">Image to render information to</param>
    ''' <param name="assembly">An assembly to read information from</param>
    ''' <param name="items">Identifies items to render to image</param>
    ''' <exception cref="ArgumentNullException">Any parameter is null -or- an item in <paramref name="items"/> collection is null.</exception>
    ''' <exception cref="ArgumentException">Image loaded from <paramref name="image"/> has an indexed pixel format or its format is undefined, see <see cref="Exception.InnerException"/> for details.</exception>
    ''' <exception cref="FormatException">Item in <paramref name="items"/> has property <see cref="InfoItem.Format"/> which represents invalid format for <see cref="System.String.Format"/> or contains placeholder other than {0}.</exception>
    Public Shared Sub GenerateSplashScreen(image As Image, items As IEnumerable(Of InfoItem), assembly As Assembly)
        If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
        GenerateSplashScreen(image, items, New AssemblyInfo(assembly))
    End Sub
#End Region

    ''' <summary>Enumerates states of command line reading</summary>
    Private Enum CommandLineParsing
        ''' <summary>Expecting in file</summary>
        InFile
        ''' <summary>Expecting out file</summary>
        OutFile
        ''' <summary>Expecting -argumentName</summary>
        Args
        ''' <summary>Expecting path of assembly info file</summary>
        AssemblyInfo
        ''' <summary>Expecting font size</summary>
        ii_FontSize
        ''' <summary>Expecting offset</summary>
        ii_Offset
        ''' <summary>Expecting foreground color</summary>
        ii_Foreground
        ''' <summary>Expecting background color</summary>
        ii_Background
        ''' <summary>Expeciting font name</summary>
        ii_Font
        ''' <summary>Expecting font style</summary>
        ii_FontStyle
        ''' <summary>Expecting format string</summary>
        ii_Format
    End Enum
End Class

''' <summary>Types of items for Spash Screen Generation</summary>
''' <remarks>Items are read from assembly attributes using <see cref="AssemblyInfo"/></remarks>
Public Enum InfoItemType
    ''' <summary>Company name - <see cref="AssemblyCompanyAttribute"/> or <see cref="AssemblyInfo.CompanyName"/></summary>
    CompanyName
    ''' <summary>Coypright - <see cref="AssemblyCopyrightAttribute"/> or <see cref="AssemblyInfo.Copyright"/></summary>
    Copyright
    ''' <summary>Description - <see cref="AssemblyDescriptionAttribute"/> or <see cref="AssemblyInfo.Description"/></summary>
    Description
    ''' <summary>Product name - <see cref="AssemblyProductAttribute"/> or <see cref="AssemblyInfo.ProductName"/></summary>
    ProductName
    ''' <summary>Title - <see cref="AssemblyTitleAttribute"/> or <see cref="AssemblyInfo.Title"/></summary>
    Title
    ''' <summary>Trademark - <see cref="AssemblyTrademarkAttribute"/> or <see cref="AssemblyInfo.Trademark"/></summary>
    Trademark
    ''' <summary>Version - <see cref="AssemblyVersionAttribute"/> or <see cref="AssemblyInfo.Version"/>, format string can contain numbers 1 to 4 as format specifier passed to <see cref="Version.ToString"/></summary>
    Version
    ''' <summary>Any plain text stored in <see cref="InfoItem.Format"/></summary>
    Text
End Enum

''' <summary>Describes one item to be rendered on splash screen image</summary>
<DebuggerDisplay("ToString()"), Serializable>
Public Class InfoItem
    ''' <summary>Identifies type of the item</summary>
    <DefaultValue(InfoItemType.Text)>
    Public Property Type As InfoItemType = InfoItemType.Text
    ''' <summary>Size of font (in points) to render item text using it</summary>
    <DefaultValue(12)>
    Public Property FontSize As Integer = 12
    ''' <summary>Font to render item text using it</summary>
    <DefaultValue("Arial")>
    Public Property Font As String = "Arial"
    ''' <summary>Position (in pixels) to renret top-left corner of text onto</summary>
    <DefaultValue(GetType(Point), "0,0")>
    Public Property Offset As Point
    ''' <summary>Background color of text, ignored if <see cref="Color.Transparent"/></summary>
    <DefaultValue(GetType(Color), "Transparent")>
    Public Property Background As Color = Color.Transparent
    ''' <summary>Foreground color of text</summary>
    <DefaultValue(GetType(Color), "Black")>
    Public Property Foreground As Color = Color.Black
    ''' <summary>Format string for <see cref="System.String.Format"/></summary>
    ''' <value>String possibly containing placholder {0} to be replaced with actual value</value>
    ''' <remarks>
    ''' <para>If <see cref="Type"/> is <see cref="InfoItemType.Text"/> null is passed to placeholder {0}.</para>
    ''' <para>If <see cref="Type"/> is <see cref="InfoItemType.Version"/> format speccifier can be specified as {0:1}, {0:2}, {0:3} or {0:4} and value 1, 2, 3 or 4 is passed to <see cref="Version.ToString"/>.</para>
    ''' </remarks>
    <DefaultValue("{0}")>
    Public Property Format As String = "{0}"
    ''' <summary>Style of font to render text using it</summary>
    Public Property FontStyle As FontStyle = FontStyle.Regular

    ''' <summary>Renders the item</summary>
    ''' <param name="g"><see cref="Graphics"/> to render item to</param>
    ''' <param name="info">Assembly information to obtain value for this item from</param>
    ''' <exception cref="InvalidOperationException">
    ''' Value of the <see cref="Type"/> property is not one of <see cref="InfoItemType"/> values -or-
    ''' <see cref="FontSize"/> is less than or equal to 0. -or-
    ''' <see cref="Font"/> specifies a font that is not installed on the computer running the application or it is not a TrueType font. -or-
    ''' <see cref="Format"/> is null.
    ''' </exception>
    ''' <exception cref="ArgumentNullException"><paramref name="g"/> or <paramref name="info"/> is null</exception>
    ''' <exception cref="FormatException"><see cref="Format"/> is invalid or contains placeholder other than {0}.</exception>
    Public Overridable Sub Render(g As Graphics, info As AssemblyInfo)
        If g Is Nothing Then Throw New ArgumentNullException("g")
        If info Is Nothing Then Throw New ArgumentNullException("info")
        Dim textToRender$ = ToString(info)
        Dim font As Font
        Try
            If Me.Font = "" Then
                font = New Font("Arial", FontSize, FontStyle, GraphicsUnit.Point)
            Else
                font = New Font(Me.Font, FontSize, FontStyle, GraphicsUnit.Point)
            End If
        Catch ex As ArgumentException
            Throw New InvalidOperationException(ex.Message, ex)
        End Try
        If Background <> Color.Transparent Then
            Dim strSize = g.MeasureString(textToRender, font)
            g.FillRectangle(New SolidBrush(Background), New RectangleF(Offset, strSize))
        End If

        g.DrawString(textToRender, font, New SolidBrush(Foreground), Offset)
    End Sub

    ''' <summary>Obtains string representation of this <see cref="InfoItem"/> based on given <see cref="AssemblyInfo"/></summary>
    ''' <param name="info">Assembly information to obtain value for this item from</param>
    ''' <returns>Text representation of current <see cref="InfoItem"/> based on information read from <paramref name="info"/>.</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="info"/> is null</exception>
    ''' <exception cref="InvalidOperationException">Value of the <see cref="Type"/> property is not one of <see cref="InfoItemType"/> values -or- <see cref="Format"/> is null.</exception>
    ''' <exception cref="FormatException"><see cref="Format"/> is invalid or contains placeholder other than {0}.</exception>
    Public Overridable Overloads Function ToString(ByVal info As AssemblyInfo) As String
        If info Is Nothing Then Throw New ArgumentNullException("info")
        Try
            Select Case Type
                Case InfoItemType.CompanyName : Return String.Format(Format, info.CompanyName)
                Case InfoItemType.Copyright : Return String.Format(Format, info.Copyright)
                Case InfoItemType.Description : Return String.Format(Format, info.Description)
                Case InfoItemType.ProductName : Return String.Format(Format, info.ProductName)
                Case InfoItemType.Title : Return String.Format(Format, info.Title)
                Case InfoItemType.Trademark : Return String.Format(Format, info.Trademark)
                Case InfoItemType.Version : Return String.Format(Format, New VersionFormattable(info.Version))
                Case InfoItemType.Text : Return String.Format(Format, Nothing)
                Case Else : Throw New InvalidOperationException(String.Format(My.Resources.SomethingNotSuppported, GetType(InfoItemType).Name, Type))
            End Select
        Catch ex As ArgumentNullException
            Throw New InvalidOperationException(My.Resources.FormatWasNull, ex)
        End Try
    End Function

    ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
    ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
    ''' <filterpriority>2</filterpriority>
    Public Overloads Overrides Function ToString() As String
        Return String.Format("{0} {1}", Type, Format)
    End Function
End Class

''' <summary>Implements <see cref="IFormattable"/> for <see cref="Version"/></summary>
''' <remarks>
''' Not to be used publicly.
''' <para>This implementation of <see cref="IFormattable"/> supports following formatting arguments:</para>
''' <list type="Table">
''' <item><term>null, an empty string, "G" or "g"</term><description><see cref="M:System.Version.ToString()"/> is used</description></item>
''' <item><term>An integral number</term><description><see cref="M:System.Version.ToString(System:Int32)"/> is used</description></item>
''' <item><term>Anything else</term><description>A <see cref="FormatException"/> is thrown.</description></item>
''' </list>
''' </remarks>
Friend NotInheritable Class VersionFormattable
    Implements IFormattable
    ''' <summary>Internal <see cref="Version"/> value</summary>
    Private version As Version
    ''' <summary>CTor - creates a new instance of the <see cref="VersionFormattable"/> class</summary>
    ''' <param name="version">A <see cref="Version"/> instance to wrap in new instance</param>
    ''' <exception cref="ArgumentNullException"><paramref name="version"/> is null</exception>
    Public Sub New(version As Version)
        If version Is Nothing Then Throw New ArgumentNullException("version")
        Me.version = version
    End Sub
    ''' <summary>Formats the value of the current instance using the specified format.</summary>
    ''' <returns>The value of the current instance in the specified format.</returns>
    ''' <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable" /> implementation. </param>
    ''' <param name="formatProvider">Ignored.</param>
    ''' <exception cref="FormatException">
    ''' <paramref name="format"/> cannot be parsed as <see cref="Integer"/> in <see cref="InvariantCulture"/> -or-
    ''' <paramref name="format"/> represents number thats bigger than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>. -or-
    ''' <paramref name="format"/> represents number thats less than 0 or bigger than 4 or bigger than number of components specified in <see cref="Version"/> object.
    ''' </exception>
    ''' <filterpriority>2</filterpriority>
    Public Overloads Function ToString(format As String, formatProvider As System.IFormatProvider) As String Implements System.IFormattable.ToString
        If String.IsNullOrEmpty(format) OrElse format = "G" OrElse format = "g" Then
            Return version.ToString
        Else
            Try
                Return version.ToString(Integer.Parse(format, InvariantCulture))
            Catch ex As Exception When TypeOf ex Is FormatException OrElse TypeOf ex Is OverflowException OrElse TypeOf ex Is ArgumentException
                Throw New FormatException(My.Resources.InvalidFormat.f(format, MyClass.GetType.Name))
            End Try
        End If
    End Function

    ''' <summary>Formats the value of the current instance using the specified format.</summary>
    ''' <returns>The value of the current instance in the specified format.</returns>
    ''' <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable" /> implementation. </param>
    ''' <exception cref="FormatException">
    ''' <paramref name="format"/> cannot be parsed as <see cref="Integer"/> in <see cref="InvariantCulture"/> -or-
    ''' <paramref name="format"/> represents number thats bigger than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>. -or-
    ''' <paramref name="format"/> represents number thats less than 0 or bigger than 4 or bigger than number of components specified in <see cref="Version"/> object.
    ''' </exception>
    Public Overloads Function ToString(format As String) As String
        Return ToString(format, CurrentCulture)
    End Function

    ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
    ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
    ''' <filterpriority>2</filterpriority>
    Public Overloads Overrides Function ToString() As String
        Return ToString(Nothing, CurrentCulture)
    End Function
End Class