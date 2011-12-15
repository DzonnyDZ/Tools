''' <summary>Base class for individual parts of screensaver</summary>
''' <remarks>Every configurable placable control in screen saver must inherit from this class</remarks>
Public MustInherit Class SaverBase
    Inherits UserControl

    ''' <summary>When overriden in derived class loads settings of this component form an XML element</summary>
    ''' <param name="settings">An XML element to load settings from</param>
    MustOverride Sub LoadSettings(settings As XElement)

    ''' <summary>When overriden in derived class saves current settings off a component as XML element</summary>
    ''' <returns>An XML element containing saved serttings of the component</returns>
    MustOverride Function GetSettings() As XElement

    ''' <summary>When overriden in derived class shows settings dialog for user.</summary>
    ''' <remarks>Use <see cref="SSaverContext"/>.<see cref="IUnisaveContext.ShowDialog">ShowDialog</see> to display settings <see cref="Window"/>.</remarks>
    MustOverride Sub ShowSettings()

#Region "SaverMode"
    ''' <summary>Gets or sets value indicating current mode of the screensaver</summary>      
    Public Property SaverMode As SaverMode
        Get
            Return GetValue(SaverModeProperty)
        End Get
        Set(ByVal value As SaverMode)
            SetValue(SaverModeProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="SaverMode"/> dependency property</summary>                                                   
    Public Shared ReadOnly SaverModeProperty As DependencyProperty = DependencyProperty.Register(
        "SaverMode", GetType(SaverMode), GetType(SaverBase), New FrameworkPropertyMetadata(SaverMode.Saver))
#End Region

#Region "SSaverContext"
    ''' <summary>Gets or sets current screensaver context</summary>      
    Public Property SSaverContext As IUnisaveContext
        Get
            Return GetValue(SSaverContextProperty)
        End Get
        Set(ByVal value As IUnisaveContext)
            SetValue(SSaverContextProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="SSaverContext"/> dependency property</summary>                                                   
    Public Shared ReadOnly SSaverContextProperty As DependencyProperty = DependencyProperty.Register(
        "SSaverContext", GetType(IUnisaveContext), GetType(SaverBase), New FrameworkPropertyMetadata(Nothing))
#End Region

End Class

''' <summary>Defines modes of screen saver components</summary>
Public Enum SaverMode
    ''' <summary>Normal - screen saver mode</summary>
    Saver
    ''' <summary>Preview mode (small preview)</summary>
    Preview
    ''' <summary>Desig mode (in configuration)</summary>
    Design
    ''' <summary>Custom mode not used by <see cref="Unisave"/>.</summary>
    Custom
End Enum
