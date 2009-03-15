Imports System.Xml, System.Xml.Xsl
Imports System.Windows.Forms

Namespace GUI
    ''' <summary>Zobrazuje historii verzí</summary>
    Public Class VersionHistory
        Implements IComparer(Of XmlElement)
#Region "Shared"
        Private Shared _HelpNamespace$
        Public Shared Property HelpNamespace() As String
            Get
                Return _HelpNamespace
            End Get
            Set(ByVal value As String)
                _HelpNamespace = value
            End Set
        End Property
        Private Shared _Assembly As Reflection.Assembly
        Public Shared Property [Assembly]() As Reflection.Assembly
            Get
                Return _Assembly
            End Get
            Set(ByVal value As Reflection.Assembly)
                _Assembly = value
            End Set
        End Property
        Private Shared _VersionHistoryResourceName As String
        Public Shared Property VersionHistoryResourceName() As String
            Get
                Return _VersionHistoryResourceName
            End Get
            Set(ByVal value As String)
                _VersionHistoryResourceName = value
            End Set
        End Property
#End Region

        ''' <summary>Zobrazí celou historii verzí</summary>
        ''' <param name="Owner">Rodièovské okno</param>
        Public Shared Sub ShowHistory(Optional ByVal Owner As IWin32Window = Nothing)
            Dim Els As New List(Of XmlElement)
            Dim vh As XmlDocument = VersionHistory
            Dim nsmgr As New XmlNamespaceManager(vh.NameTable)
            nsmgr.AddNamespace("vh", "http://dzonny.cz/xml/Schemas/VersionHistory")
            For Each El As XmlElement In vh.SelectNodes("/vh:VersionHistory/vh:Version", nsmgr)
                Els.Add(El)
            Next
            ShowHistory(Els)
        End Sub
        ''' <summary>CTor (neveøejný)</summary>
        Private Sub New()
            InitializeComponent()
            hlpHelp.HelpNamespace = HelpNamespace
            Me.Text = String.Format("Novinky v aplikaci {0} {1}", My.Application.Info.Title, My.Application.Info.Version)
        End Sub
        ''' <summary>Nastaví zobrazenou historii verzí</summary>
        ''' <param name="History">Seznam XML elementù &lt;Version></param>
        ''' <exception cref="ArgumentNullException"><see cref="History"/> je null</exception>
        Private Sub SetVersionHistory(ByVal History As List(Of XmlElement))
            If History Is Nothing Then Throw New ArgumentNullException("History")
            Dim transform As New XslCompiledTransform
            transform.Load(XmlReader.Create(GetType(VersionHistory).Assembly.GetManifestResourceStream("EOS.VersionHistory.xslt")))
            Dim doc As New XmlDocument
            Dim el As XmlElement = doc.CreateElement("VersionHistory", "http://dzonny.cz/xml/Schemas/VersionHistory")
            doc.AppendChild(el)
            History.Sort(Me)
            For Each item As XmlElement In History
                el.AppendChild(doc.ImportNode(item, True))
            Next
            Dim sw As New IO.StringWriter()
            Dim xw As New XmlTextWriter(sw)
            transform.Transform(doc, xw)
            webHistory.DocumentText = sw.GetStringBuilder.ToString
            Me.History = History
        End Sub
        ''' <summary>Právì zobrazovaná historie</summary>
        Private History As List(Of XmlElement)

        ''' <summary>Zobrazit historii verzí jako seznam XML elementù</summary>
        ''' <param name="History">Seznam elementù &lt;Version></param>
        ''' <param name="Owner">Rodièovské okno</param>
        ''' <exception cref="ArgumentNullException"><see cref="History"/> je null</exception>
        Private Shared Sub ShowHistory(ByVal History As List(Of XmlElement), Optional ByVal Owner As IWin32Window = Nothing)
            If History Is Nothing Then Throw New ArgumentNullException("History")
            Dim frm As New VersionHistory
            frm.SetVersionHistory(History)
            frm.ShowDialog(Owner)
        End Sub
        ''' <summary>Automaticky, pokud je to potøeba (pokud ji uživatel ještì nevidìl) zobrazí historii verzí</summary>
        ''' <param name="Owner">Rodièovské okno</param>
        Public Shared Sub AutoShow(ByVal LastVersionHistory As Version, Optional ByVal Owner As IWin32Window = Nothing)
            Dim News As List(Of XmlElement) = GetNewer(LastVersionHistory)
            If News.Count > 0 Then
                ShowHistory(News, Owner)
            End If
        End Sub
        ''' <summary>Vrátí elementy &lt;Version>, které jsou novìjší než zadaná verze</summary>
        ''' <param name="Version">Verze k porovnání</param>
        ''' <returns>Seznam XML elementù &lt;Version> popisujících verze novìjší než <paramref name="Version"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Version"/> je null</exception>
        Private Shared Function GetNewer(ByVal Version As Version) As List(Of XmlElement)
            If Version Is Nothing Then Throw New ArgumentNullException("Version")
            Dim vh As XmlDocument = VersionHistory
            Dim nsmgr As New XmlNamespaceManager(vh.NameTable)
            nsmgr.AddNamespace("vh", "http://dzonny.cz/xml/Schemas/VersionHistory")
            Dim News As New List(Of XmlElement)
            For Each el As XmlElement In vh.SelectNodes("/vh:VersionHistory/vh:Version", nsmgr)
                Dim v As Version = getVersion(el)
                If v > Version Then
                    News.Add(el)
                End If
            Next
            Return News
        End Function
        ''' <summary>Získá historii verzí jako XML dokument</summary>
        Private Shared ReadOnly Property VersionHistory() As Xml.XmlDocument
            Get
                Dim xml As New XmlDocument
                xml.Load(Assembly.GetManifestResourceStream(VersionHistoryResourceName))
                Return xml
            End Get
        End Property
        ''' <summary>Získá verzi z XML elementu &lt;Version></summary>
        ''' <param name="el">XML element</param>
        ''' <exception cref="ArgumentNullException"><paramref name="el"/> je null</exception>
        Private Shared Function getVersion(ByVal el As XmlElement) As Version
            If el Is Nothing Then Throw New ArgumentNullException("el")
            Dim v As Version
            If el.HasAttribute("Revision") Then
                v = New Version(el.Attributes!Major.Value, el.Attributes!Minor.Value, el.Attributes!Build.Value, el.Attributes!Revision.Value)
            Else
                v = New Version(el.Attributes!Major.Value, el.Attributes!Minor.Value, el.Attributes!Build.Value)
            End If
            Return v
        End Function
        ''' <summary>Porovnává dva XML elementy &lt;Version> podle jejich verze DESC</summary>
        ''' <param name="x">XML element &lt;Version></param>
        ''' <param name="y">XML element &lt;Version></param>
        ''' <returns>-1 pokud je verze <paramref name="x"/> > <paramref name="y"/>; 1 pokud je verze <paramref name="y"/> > <paramref name="x"/>; jinak 0</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="x"/> nebo <paramref name="y"/> je null</exception>
        Private Function Compare(ByVal x As System.Xml.XmlElement, ByVal y As System.Xml.XmlElement) As Integer Implements System.Collections.Generic.IComparer(Of System.Xml.XmlElement).Compare
            If x Is Nothing Then Throw New ArgumentNullException("x")
            If y Is Nothing Then Throw New ArgumentNullException("y")
            Dim xver As Version = getVersion(x)
            Dim yver As Version = getVersion(y)
            Return -xver.CompareTo(yver)
        End Function

        Public Shared Event SetLastVH(ByVal sender As VersionHistory, ByVal e As VersionEventArgs)
        Public Class VersionEventArgs : Inherits EventArgs
            Private ReadOnly _Version As Version
            Public Sub New(ByVal Version As Version)
                _Version = Version
            End Sub
            Public ReadOnly Property Version() As Version
                Get
                    Return _Version
                End Get
            End Property
        End Class

        Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
            If chkOK.Checked Then
                RaiseEvent SetLastVH(Me, New VersionEventArgs(getVersion(History(0))))
            End If
            Me.Close()
        End Sub

        Private Sub webHistory_Navigating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles webHistory.Navigating
            If e.Url.AbsoluteUri.StartsWith("http://") Then
                e.Cancel = True
                Try : Process.Start(e.Url.AbsoluteUri)
                Catch : End Try
            End If
        End Sub
    End Class
End Namespace