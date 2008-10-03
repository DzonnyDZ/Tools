This project is here for following reasons:
1) To provide source control for item included in this project
2) To be able to build items included in this project in order to pass them to SandCastle and obtain documentation

This project is NOT intended to be build and used as assembly.
Instead of it import Macros.vsmproj to Macros IDE.

Note: Part of project dealing with Metadata properties of Solution Explorer items needs plenty of references.
Visual Studio Macros IDE has a big headache of those references. They are in GAC (if you have VS SDK) but Visual Studio does not see some of them, namely:
    * microsoft.msxml
    * Microsoft.VisualStudio.ProjectAggregator
    * microoft.visualstudio.shell.interop.8.0
    * SMdiagnostics
Adding reference to them directly in GAC directory does not work. The best solution is to copy them to macros directory and add reference there.
You may also experience problems loading the macro project or executim macros. This is some kind of stupidness of Macros IDE.
Workarounds:
1) Cannot load macros project to Macro explorer
    a) Copy all the files out fro source-controlled directory and make them writable.
    b) Create a new macro project and addd VB files and references manually.
2) After loading macros to Macro Explorer only Project and modules are seen, no methods.
    a) Opem macros IDE, place cursor to method you wana see and run it. If you have good luck you will then see the procedure in Macro Explorer.
    b) Create keyboard shortcut or menu item for the macro.
    c) Run the procedure as described above and then create shortcut or manu item.
3) I've created keyboard shortcut/menu item, but it vanished after restart of VS.
    No workaround yet :-(. It's caused by fact that VS initially does not see methods.    
    
As a partial solution to this issues I've splitted macro projects to 2 projects
   * XML comments (MacrosXML)
   * Metadata properties (MacrosPROP)