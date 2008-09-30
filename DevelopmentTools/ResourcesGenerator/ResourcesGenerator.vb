Imports Tools.GeneratorsT
Imports System.Runtime.InteropServices
Imports System.CodeDom
Imports System.IO
Imports System.Resources
Imports System.Text

''' <summary>This is Visual Studio custom tool for generating strongyl typed resources class. It extends Microsoft-provided one.</summary>
<Guid("A3915FDD-83B4-412c-B2A1-4B0F25DA3F61")> _
<CustomTool("ResourcesGenerator", "Resources generator")> _
Public Class ResourcesGenerator
    Inherits CustomToolBase
    ''' <summary>Performs code generation</summary>
    ''' <param name="inputFileName">Name of file to convert</param>
    ''' <param name="inputFileContent">Content of file to convert</param>
    ''' <returns>File converted</returns>
    Public Overrides Function DoGenerateCode(ByVal inputFileName As String, ByVal inputFileContent As String) As String
        Dim unit As CodeCompileUnit
        If (String.IsNullOrEmpty(inputFileContent) OrElse IsLocalizedFile(inputFileName)) Then
            If (Not MyBase.CodeGeneratorProgress Is Nothing) Then
                NativeMethods.ThrowOnFailure(MyBase.CodeGeneratorProgress.Progress(100, 100))
            End If
            Return Nothing
        End If
        Dim w As New StreamWriter(New MemoryStream, Encoding.UTF8)
        Dim resourceList As IDictionary = New Hashtable(StringComparer.OrdinalIgnoreCase)
        Dim dictionary2 As IDictionary = New Hashtable(StringComparer.OrdinalIgnoreCase)
        Using reader As IResourceReader = ResXResourceReader.FromFileContents(inputFileContent)
            Dim reader2 As ResXResourceReader = TryCast(reader, ResXResourceReader)
            If (Not reader2 Is Nothing) Then
                reader2.UseResXDataNodes = True
                Dim directoryName As String = Path.GetDirectoryName(inputFileName)
                reader2.BasePath = Path.GetFullPath(directoryName)
            End If
            Dim entry As DictionaryEntry
            For Each entry In reader
                Dim node As ResXDataNode = DirectCast(entry.Value, ResXDataNode)
                resourceList.Add(entry.Key, node)
                dictionary2.Add(entry.Key, node.GetNodePosition)
            Next
        End Using
        Dim resourcesNamespace As String = Me.GetResourcesNamespace
        Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(inputFileName)
        Dim unmatchable As New List(Of ResourceErrorData)
        If (Not resourcesNamespace Is Nothing) Then
            unit = StronglyTypedResourceBuilderEx.Create(MyBase.GetType, resourceList, fileNameWithoutExtension, MyBase.FileNameSpace, resourcesNamespace, Me.CodeProvider, Me.GenerateInternalClass, unmatchable)
        Else
            unit = StronglyTypedResourceBuilderEx.Create(MyBase.GetType, resourceList, fileNameWithoutExtension, MyBase.FileNameSpace, Me.CodeProvider, Me.GenerateInternalClass, unmatchable)
        End If
        If (Not MyBase.CodeGeneratorProgress Is Nothing) Then
            Dim data As ResourceErrorData
            For Each data In unmatchable
                Dim point As Point = DirectCast(dictionary2.Item(data.ResourceKey), Point)
                MyBase.CodeGeneratorProgress.GeneratorError(1, 1, data.ErrorString, DirectCast(point.Y, UInt32), DirectCast(point.X, UInt32))
            Next
            MyBase.CodeGeneratorProgress.Progress(70, 100)
        End If
        Me.HandleCodeCompileUnit(unit)
        If (Not MyBase.CodeGeneratorProgress Is Nothing) Then
            MyBase.CodeGeneratorProgress.Progress(&H4B, 100)
        End If
        Me.CodeProvider.CreateGenerator.GenerateCodeFromCompileUnit(unit, w, Nothing)
        If (Not MyBase.CodeGeneratorProgress Is Nothing) Then
            NativeMethods.ThrowOnFailure(MyBase.CodeGeneratorProgress.Progress(100, 100))
        End If
        w.Flush()
        Return MyBase.StreamToBytes(w.BaseStream)
    End Function
    Private Shared Function IsLocalizedFile(ByVal fileName As String) As Boolean
        If (Not String.IsNullOrEmpty(fileName) AndAlso fileName.EndsWith(".resx", True, CultureInfo.InvariantCulture)) Then
            Dim str As String = fileName.Substring(0, (fileName.Length - 5))
            If Not String.IsNullOrEmpty(str) Then
                Dim num As Integer = str.LastIndexOf("."c)
                If (num > 0) Then
                    Dim str2 As String = str.Substring((num + 1))
                    If Not String.IsNullOrEmpty(str2) Then
                        Try
                            If (Not New CultureInfo(str2) Is Nothing) Then
                                Return True
                            End If
                        Catch exception1 As ArgumentException
                        End Try
                    End If
                End If
            End If
        End If
        Return False
    End Function


End Class
