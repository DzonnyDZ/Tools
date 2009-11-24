@echo off
echo BUILD:
echo ilasm Type.il /DLL /INCLUDE=..\SharedFiles /PDB /KEY=..\SharedFiles\Tools.snk %2 /OUTPUT="%1Tools.IL.dll"
ilasm Type.il /DLL /INCLUDE=..\SharedFiles /PDB /KEY=..\SharedFiles\Tools.snk %2 /OUTPUT="%1Tools.IL.dll"
echo Build finished with code %ERRORLEVEL%
IF NOT ERRORLEVEL 0 EXIT
REM /QUIET /NOLOGO
echo COPY OUTPUT:
copy "Tools.IL.xml" "%1Tools.IL.xml"
IF NOT ERRORLEVEL 0 EXIT
echo VERIFY:
peverify /nologo /md /il "%1Tools.IL.dll"
IF NOT ERRORLEVEL 0 EXIT
SET ERRORLEVEL = 0