Imports System.Windows.Forms
Imports System.ComponentModel
Imports Tools.WindowsT.NativeT

''' <summary>Implements base class for WinForms-base lister (WLX) plugins for Total Commander</summary>
''' <remarks>Plugin derived from this class is supposed to provide user interface. If you are about to implement a plugin which only generates thumbnails derive from <see cref="ListerPlugin"/> directly.</remarks>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
<NotAPlugin()>
Public MustInherit Class ListerPluginWinForms
    Inherits ListerPlugin(Of ListerPluginUIWinForms)


#Region "Load"


#End Region

  
End Class
