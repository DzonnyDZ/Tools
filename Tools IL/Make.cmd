@echo off
echo %0 %*
SET _out=%1
SET _out=###%_out%###
SET _out=%_out:"###=%
SET _out=%_out:###"=%
SET _out=%_out:###=%
echo Output directory: %_out%
echo BUILD:
echo ilasm AssemblyInfo.il Type.il /DLL /INCLUDE=..\SharedFiles /PDB /KEY=..\SharedFiles\Tools.snk %2 /OUTPUT="%_out%\Tools.IL.dll"
ilasm AssemblyInfo.il Type.il /DLL /INCLUDE=..\SharedFiles /PDB /KEY=..\SharedFiles\Tools.snk %2 /OUTPUT="%_out%\Tools.IL.dll"
echo Build finished with code %ERRORLEVEL%
IF NOT ERRORLEVEL 0 EXIT
REM /QUIET /NOLOGO
echo COPY OUTPUT:
copy "Tools.IL.xml" "%_out%\Tools.IL.xml"
IF NOT ERRORLEVEL 0 EXIT
echo VERIFY:
echo peverify /nologo /md /il "%_out%\Tools.IL.dll"
peverify /nologo /md /il "%_out%\Tools.IL.dll"
IF NOT ERRORLEVEL 0 EXIT
SET ERRORLEVEL = 0