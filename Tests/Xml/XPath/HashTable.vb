Imports Tools.CollectionsT.GenericT
Namespace XmlT.XPathT
    '#If Config <= Nightly This conditional compilation is done in Tests.vbproj
    ''' <summary>Test Form for testing <see cref="HashTable(Of String)"/></summary>
    Friend Class frmXPathObjectNavigator
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
      
        ''' <summary>Displays test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmXPathObjectNavigator
            frm.ShowDialog()
        End Sub
    End Class
End Namespace