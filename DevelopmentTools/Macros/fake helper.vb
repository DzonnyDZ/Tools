''' <summary>Contains helpers necesary for this project to build outside Macros IDE</summary>
<HideModuleName()> _
Public Module fake_helper
    ''' <summary>In case you are trying to use compiled assemby of this project outside Macros IDE, this firld must be set!</summary>
    Public DTE As EnvDTE.DTE
End Module

