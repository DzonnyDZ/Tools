Imports System.ComponentModel
Imports System.Globalization.CultureInfo

''' <summary>VersionUpdater implementation</summary>
Friend Module VersionUpdater
    ''' <summary>Entry point of version updater</summary>
    Sub Main()
        'Usage: VersionUpdater infile mode outfile1 outfile2 outfile3 ...

        Console.WriteLine("{0} {1}", My.Application.Info.Title, My.Application.Info.Version)
        If My.Application.CommandLineArgs.Count < 3 Then Usage(1) : End

        Dim infile As String = My.Application.CommandLineArgs(0)
        Dim mode As VersionUpdaterMode
        Dim svnPath$ = Nothing
        If My.Application.CommandLineArgs(1).ToLowerInvariant.StartsWith("svn:") Then
            mode = VersionUpdaterMode.Svn
            svnPath = My.Application.CommandLineArgs(1).Substring(4)
        Else
            mode = [Enum].Parse(GetType(VersionUpdaterMode), My.Application.CommandLineArgs(1), True)
        End If

        Dim outfiles = From itm In My.Application.CommandLineArgs Skip 2

        Dim actualVersion = New Version(My.Computer.FileSystem.ReadAllText(infile))
        Dim newVersion As Version
        Select Case mode
            Case VersionUpdaterMode.Timestamp : newVersion = New Version(actualVersion.Major, actualVersion.Minor, actualVersion.Build, CInt(CDbl(Math.Truncate((DateTime.UtcNow - Date.SpecifyKind(#1/1/1970#, DateTimeKind.Utc)).TotalSeconds)) And &HFFFF))
            Case VersionUpdaterMode.Increment : newVersion = New Version(actualVersion.Major, actualVersion.Minor, actualVersion.Build, (actualVersion.Revision + 1) And &HFFFF)
            Case VersionUpdaterMode.Svn
                Using r = My.Computer.FileSystem.OpenTextFileReader(IO.Path.Combine(svnPath, ".svn\entries"))
                    r.ReadLine() : r.ReadLine() : r.ReadLine() 'Ignore 3 lines
                    '4th line contains version
                    newVersion = New Version(actualVersion.Major, actualVersion.Minor, actualVersion.Build, Integer.Parse(r.ReadLine, InvariantCulture) And &HFFFF)
                End Using
            Case Else : Usage(2) : End
        End Select

        For Each file In outfiles
            Dim text As String
            Select Case IO.Path.GetExtension(file).ToLowerInvariant
                Case ".vb", ".bas" : text = GetVB(newVersion)
                Case ".cs", ".c#" : text = GetCS(newVersion)
                Case ".c", ".cpp", ".h", ".c++" : text = GetCPP(newVersion)
                Case ".il", ".msil", ".cil" : text = GetIL(newVersion)
                Case ".js" : text = GetJS(newVersion)
                Case ".fs", ".f#" : text = GetFS(newVersion)
                Case ".java", ".j#" : text = GetJSharp(newVersion)
                Case ".php", ".inc", ".php3", "php4", ".php5", "php6" : text = GetPHP(newVersion)
                Case ".xml" : text = GetXml(newVersion)
                Case ".p" : text = GetPascal(newVersion)
                Case Else : text = newVersion.ToString
            End Select
            My.Computer.FileSystem.WriteAllText(file, text, False)
        Next
    End Sub

    ''' <summary>Writes program usage to the console and exists with given exit code</summary>
    Private Sub Usage(exitCode As Integer)
        Console.WriteLine(My.Resources.Usage, IO.Path.GetFileNameWithoutExtension(GetType(VersionUpdater).Assembly.Location))
        Console.WriteLine(My.Resources.Mode)
        Console.WriteLine(My.Resources.SvnDirpath)
        Environment.Exit(exitCode)
    End Sub

    ''' <summary>Generates a code for Visual Basic language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetVB(newVersion As Version) As String
        Return String.Format("<Assembly: System.Reflection.AssemblyVersion(""{0}"")>" & vbCrLf &
                             "<Assembly: System.Reflection.AssemblyFileVersion(""{0}"")>", newVersion)
    End Function
    ''' <summary>Generates a code for C# language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetCS(newVersion As Version) As String
        Return String.Format("[assembly: System.Reflection.AssemblyVersion(""{0}"")]" & vbCrLf &
                             "[assembly: System.Reflection.AssemblyFileVersion(""{0}"")]", newVersion)
    End Function
    ''' <summary>Generates a code for C++/CLI language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetCPP(newVersion As Version) As String
        Return String.Format("[assembly: System::Reflection::AssemblyVersion(""{0}"")]" & vbCrLf &
                             "[assembly: System::Reflection::AssemblyFileVersion(""{0}"")]", newVersion)
    End Function
    ''' <summary>Generates a code for Common Intermediate Language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetIL(newVersion As Version) As String
        Return String.Format(".ver {0}:{1}:{2}:{3}" & vbCrLf &
                             ".custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = {{ string('{4}') }}",
                             newVersion.Major, newVersion.Minor, newVersion.Build, newVersion.Revision, newVersion)
    End Function

    ''' <summary>Generates a code for JScript.NET language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetJS(newVersion As Version) As String
        Return String.Format("assembly: System.Reflection.AssemblyVersion(""{0}"")" & vbCrLf &
                             "assembly: System.Reflection.AssemblyFileVersion(""{0}"")", newVersion)
    End Function

    ''' <summary>Generates a code for F# language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetFS(newVersion As Version) As String
        Return String.Format("[<assembly: System.Reflection.AssemblyVersion(""{0}"")>]" & vbCrLf &
                             "[<assembly: System.Reflection.AssemblyFileVersion(""{0}"")>]", newVersion)
    End Function

    ''' <summary>Generates a code for J# language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetJSharp(newVersion As Version) As String
        Return String.Format("/** @assembly System.Reflection.AssemblyVersion(""{0}"") */" & vbCrLf &
                             "/** @assembly System.Reflection.AssemblyFileVersion(""{0}"") */", newVersion)
    End Function

    ''' <summary>Generates a code for PHP (Phalanger) language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetPHP(newVersion As Version) As String
        Return String.Format("[assembly: System:::Reflection:::AssemblyVersion(""{0}"")]" & vbCrLf &
                             "[assembly: System:::Reflection:::AssemblyFileVersion(""{0}"")]", newVersion)
    End Function

    ''' <summary>Generates a code for Pascal (Delphi) language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetPascal(newVersion As Version) As String
        Return String.Format("[assembly: System.Reflection.AssemblyVersion(""{0}"")]" & vbCrLf &
                             "[assembly: System.Reflection.AssemblyFileVersion(""{0}"")]", newVersion)
    End Function

    ''' <summary>Generates a code for XML language</summary>
    ''' <param name="newVersion">Version</param>
    ''' <returns>A code containing assembly-level attributtes</returns>
    Private Function GetXml(newVersion As Version) As String
        Return <?xml version="1.0"?>
               <assembly>
                   <System.Reflection.AssemblyVersion><%= newVersion.ToString %></System.Reflection.AssemblyVersion>
                   <System.Reflection.AssemblyFileVersion><%= newVersion.ToString %></System.Reflection.AssemblyFileVersion>
               </assembly>.ToString
    End Function

End Module
''' <summary>Possibme operation modes of Version Updater</summary>
Friend Enum VersionUpdaterMode
    ''' <summary>Revision is set to Unix timestamp</summary>
    Timestamp
    ''' <summary>Revision is increased by 1</summary>
    Increment
    ''' <summary>Revision is obtainded from SVN</summary>
    Svn
End Enum