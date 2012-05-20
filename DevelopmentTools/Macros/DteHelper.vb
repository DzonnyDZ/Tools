''' <summary>Contains helpers necesary for this project to build and run outside Macros IDE</summary>
''' <version version="1.5.4">Module renamed form <c>fake_helper</c> to <c>DteHelper</c></version>"
<HideModuleName()> _
Public Module DteHelper
    ''' <summary>Provides <see cref="EnvDTE.DTE"/> instance to macro methods</summary>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    <CLSCompliant(False)>
    Public DTE As EnvDTE.DTE
End Module