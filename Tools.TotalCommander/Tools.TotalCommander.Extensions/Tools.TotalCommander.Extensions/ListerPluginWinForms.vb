Imports System.Windows.Forms
Imports System.ComponentModel
Imports Tools.WindowsT.NativeT

''' <summary>Implements base class for WinForms-base lister (WLX) plugins for Total Commander</summary>
''' <remarks>Plugin derived from this class is supposed to provide user interface. If you are about to implement a plugin which only generates thumbnails derive from <see cref="ListerPlugin"/> directly.</remarks>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
<NotAPlugin()>
Public MustInherit Class ListerPluginWinForms
    Inherits ListerPlugin

#Region "Load"
    ''' <summary>Called when user opens lister.</summary>
    ''' <returns>
    ''' Handle of window (control) that lister plugin uses to display it's content.
    ''' Returns <see cref="IntPtr.Zero"/> if plugin load was unsuccessfull (i.e. plugin does not support this file type). In this case Total Commander will attempt to load next plugin.
    ''' </returns>
    ''' <remarks>At <see cref="ListerPluginWinForms"/> interitance level use <see cref="LoadControl"/> instead.</remarks>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Protected NotOverridable Overrides Function OnInit() As IntPtr
        Dim ctl = LoadControl()
        If ctl Is Nothing Then Return IntPtr.Zero
        _control = ctl
        Return ctl.Handle
    End Function

    Private _control As Control
    ''' <summary>After the plugin is initialized thsi property gets a control representing plugin user interface</summary>
    ''' <returns>A control representing plugin user interface. Null before <see cref="LoadControl"/> was called or if it returned null.</returns>
    ''' <seelaso cref="ControlHandle"/>
    Public ReadOnly Property Control As Control
        Get
            Return _control
        End Get
    End Property

    Private _parentWindow As Win32Window

    Public ReadOnly Property ParentWindow As Win32Window
        Get
            If _parentWindow Is Nothing Then _parentWindow = New Win32Window(ParentWindowHandle)
            Return _parentWindow
        End Get
    End Property

    ''' <summary>When implemented in derived class creates an instance of control to represent plugin user interface</summary>
    ''' <returns>A control which represents plugin user interface. Null if this plugin lload failed (i.e. this implementation does not support given file format). This makes Total Commander load next plugin.</returns>
    ''' <remarks>When this method is called <see cref="Initialized"/> is already true.
    ''' <para>Use <see cref="ParentWindow"/>, <see cref="FileName"/> and <see cref="Options"/> to set up UI.</para></remarks>
    Protected MustOverride Function LoadControl() As Control

#End Region
End Class
