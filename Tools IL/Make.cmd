ilasm Type.il /OUTPUT="%1/Tools IL.dll" /DLL /NOLOGO /PDB /QUIET /KEY=..\SharedFiles\Tools.snk %2
copy "Tools IL.xml" "%1/Tools IL.xml"
peverify /nologo /md /il "%1/Tools IL.dll"