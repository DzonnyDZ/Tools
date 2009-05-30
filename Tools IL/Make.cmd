@echo off
echo BUILD:
ilasm Type.il /OUTPUT="%1/Tools.IL.dll" /DLL /PDB /KEY=..\SharedFiles\Tools.snk %2 /INCLUDE=..\SharedFiles\
IF NOT ERRORLEVEL 0 EXIT
REM /QUIET /NOLOGO
echo COPY OUTPUT:
copy "Tools.IL.xml" "%1/Tools.IL.xml"
IF NOT ERRORLEVEL 0 EXIT
echo VERIFY:
peverify /nologo /md /il "%1/Tools.IL.dll"
IF NOT ERRORLEVEL 0 EXIT
SET ERRORLEVEL = 0
ECHO %ERRORLEVEL%