@echo off
echo BUILD:
echo ilasm AssemblyInfo.il Type.il Delegate.il /DLL /INCLUDE=..\SharedFiles /PDB /KEY=..\SharedFiles\Tools.snk %2 /OUTPUT="%1\Tools.IL.dll"
ilasm AssemblyInfo.il Type.il Delegate.il /DLL /INCLUDE=..\SharedFiles /PDB /KEY=..\SharedFiles\Tools.snk %2 /OUTPUT="%1\Tools.IL.dll"
echo Build finished with code %ERRORLEVEL%
IF NOT ERRORLEVEL 0 EXIT
REM /QUIET /NOLOGO
echo COPY OUTPUT:
copy "Tools.IL.xml" "%1\Tools.IL.xml"
IF NOT ERRORLEVEL 0 EXIT
echo VERIFY:
peverify /nologo /md /il "%1\Tools.IL.dll"
IF NOT ERRORLEVEL 0 EXIT
SET ERRORLEVEL = 0