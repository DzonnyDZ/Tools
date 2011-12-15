Imports System.Windows.Markup
Imports System.Linq
Imports <xmlns:my="clr-namespace:Tools.Unisave;assembly=Unisave">

''' <summary>This control represents one screen saver window or screen (used only internally)</summary>
Friend Class SaverScreen
    Inherits Control
    Implements IUnisaveContext


    ''' <summary>Loads layout to this control from file path</summary>
    ''' <param name="layoutPath">Path to load laoyut from</param>
    ''' <exception cref="ArgumentNullException"><paramref name="layoutPath"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="layoutPath"/> is an empty string (""), contains only white space, or contains one or more invalid characters. -or- <paramref name="layoutPath"/> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
    ''' <exception cref="NotSupportedException"><paramref name="layoutPath"/> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
    ''' <exception cref="Security.SecurityException">The caller does not have the required permission.</exception>
    ''' <exception cref="IO.FileNotFoundException"> The file cannot be found (does not exist).</exception>
    ''' <exception cref="IO.IOException">An I/O error.</exception>
    ''' <exception cref="IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
    ''' <exception cref="IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
    Public Sub LoadLayout(layoutPath As String)
        If layoutPath Is Nothing Then Throw New ArgumentNullException("layoutPath")
        Dim rootElement As UIElement
        Using s = New IO.FileStream(layoutPath, IO.FileMode.Open)
            rootElement = XamlReader.Load(s)
        End Using
        While LogicalTreeHelper.GetChildren(Me).Cast(Of Object).Any
            Me.RemoveLogicalChild(LogicalTreeHelper.GetChildren(Me).Cast(Of Object).First)
        End While

        Me.AddLogicalChild(rootElement)
    End Sub

    ''' <summary>Load savers according to configuration XML elements</summary>
    ''' <param name="saverConfigs">A collection of &lt;saver> elements</param>
    ''' <exception cref="ArgumentNullException"><paramref name="saverConfigs"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="saverConfigs"/> contains an element that's not &lt;saver> in namespace clr-namespace:Tools.Unisave;assembly=Unisave -or- The element is missing @name or @type attributte.</exception>
    ''' <remarks>
    ''' Following errors are silently ignored:
    ''' <list type="list">
    ''' <item>A @name attribute points to an element that connot be found or it is not <see cref="SSaverHost"/>.</item>
    ''' <item>A @type attribute points to invalid type (that cannot be loaded)</item>
    ''' <item>Cannot creates instance of type @type (e.g. it does not have public parametre-less CTor or it throws an exception)</item>
    ''' </list>
    ''' </remarks>
    Public Sub LoadSavers(saverConfigs As IEnumerable(Of XElement))
        If saverConfigs Is Nothing Then Throw New ArgumentNullException("saverConfigs")

        For Each el In saverConfigs
            If el.Name <> <my:saver/>.Name Then Throw New ArgumentException(String.Format("saverConfigs", "Only saver elements in namespace {0} are supported", GetXmlNamespace(my).NamespaceName))
            If el.@name Is Nothing Then Throw New ArgumentNullException("saverConfigs", "Missing attribute @name")
            If el.@type Is Nothing Then Throw New ArgumentNullException("saverConfigs", "Missing attribute @type")

            Dim oneHost = TryCast(Me.FindName(el.@name), SSaverHost)
            If oneHost IsNot Nothing Then
                Dim componentType = Type.GetType(el.@type, False)
                If componentType IsNot Nothing Then
                    Dim instance As SaverBase = Nothing
                    Try
                        instance = TryCast(Activator.CreateInstance(componentType), SaverBase)
                    Catch : End Try
                    If instance IsNot Nothing Then
                        instance.LoadSettings(el.Elements.SingleOrDefault)
                        oneHost.HostedSaver = instance
                    End If
                End If
            End If
        Next

    End Sub

    Public Function ShowDialog(dialog As Microsoft.Win32.CommonDialog) As Boolean? Implements IUnisaveContext.ShowDialog

    End Function

    Public Function ShowDialog(dialog As System.Windows.Forms.CommonDialog) As System.Windows.Forms.DialogResult Implements IUnisaveContext.ShowDialog

    End Function

    Public Function ShowDialog(form As System.Windows.Forms.Form) As System.Windows.Forms.DialogResult Implements IUnisaveContext.ShowDialog

    End Function

    Public Function ShowDialog(window As System.Windows.Window) As Boolean? Implements IUnisaveContext.ShowDialog

    End Function
End Class
