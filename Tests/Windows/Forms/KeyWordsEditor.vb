Imports Tools.CollectionsT.GenericT
'#If Config <= Release This conditional compilation is done in Tests.vbproj
'ASAP: Is statement above true?
Namespace WindowsT.FormsT
    ''' <summary>Tests <see cref="Tools.WindowsT.FormsT.KeyWordsEditor"/></summary>
    Public Class frmKeyWordsEditor
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmKeyWordsEditor
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon

            kweEditor.KeyWords.Add("KeyWord1")
            kweEditor.KeyWords.Add("KeyWord2")
            kweEditor.AutoCompleteCacheName = "TestCache"
            kweEditor.AutoCompleteStable = New ListWithEvents(Of String)(New String() {"FirstStabe", "SecondStable", "ThirdStable"})
            kweEditor.Synonyms = New List(Of KeyValuePair(Of String(), String()))(New KeyValuePair(Of String(), String())() {New KeyValuePair(Of String(), String())(New String() {"Key1", "Key2"}, New String() {"Value1"})})
        End Sub

    End Class
End Namespace