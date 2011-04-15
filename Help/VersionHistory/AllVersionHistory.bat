@echo off
echo Merge comments:
..\..\DevelopmentTools\CommentsMerge\bin\Debug\AnyCPU\CommentsMerge "..\..\DevelopmentTools\ResXFileCodeGeneratorEx\bin\Debug\x86\ResXFileCodeGeneratorEx.xml" "..\..\DevelopmentTools\SplashScreenGenerator\bin\Debug\SplashScreenGenerator.xml" "..\..\DevelopmentTools\Macros\bin\Debug\AnyCPU\Tools.VisualStudio.Macros.xml" "..\..\DevelopmentTools\TransformCodeGenerator\bin\Debug\x86\TransformCodeGenerator.xml" "..\..\DevelopmentTools\XsdGenerator\bin\Debug\x86\XsdGenerator.xml" "..\..\Projects\Metanol2\bin\Debug\AnyCPU\Metanol.xml" "..\..\bin\Debug\x86\Tools.TotalCommander.xml" "..\..\Tools.TotalCommander\TotalCommander Plugin Builder\bin\x86\Debug\TCPluginBuilder.xml" "..\..\bin\Debug\AnyCPU\Tools Experimental.xml" "..\..\bin\Debug\AnyCPU\ReportingEngine Lite.xml" "..\..\bin\Debug\AnyCPU\Tools.xml" "..\..\bin\Debug\AnyCPU\Tools.Data.Schema.Deploy.xml" "..\..\bin\Debug\AnyCPU\Tools.Hardcore.xml" "..\..\bin\Debug\AnyCPU\Tools.Metadata.xml" "..\..\bin\Debug\AnyCPU\Tools.SqlServer.xml" "..\..\Tools Visual Studio\bin\x86\Debug\Tools.VisualStudio.xml" "..\..\Tools VisualStudio CS\bin\x86\Debug\Tools.VisualStudio.CS.xml" "..\..\bin\Debug\AnyCPU\Tools.Win.xml" "..\..\bin\Debug\AnyCPU\Tools.Windows.xml" "..\..\Created doc\Namespaces.xml" AllComments.xml
echo Create topic files:
echo brief.aml
"C:\Program Files (x86)\Sandcastle\ProductionTools\XslTransform.exe" "..\..\Created doc\VersionHistory.xml" /xsl:VersionHistory.xslt /out:brief.aml
echo 1.5.2.aml
"C:\Program Files (x86)\Sandcastle\ProductionTools\XslTransform.exe" AllComments.xml /xsl:VersionHistory-all.xslt /arg:version=1.5.2,guid=A4257124-C37D-472d-B189-79528BC483B4 /out:1.5.2.aml
echo 1.5.3.aml
"C:\Program Files (x86)\Sandcastle\ProductionTools\XslTransform.exe" AllComments.xml /xsl:VersionHistory-all.xslt /arg:version=1.5.3,guid=87bfaabf-6850-42dd-9c91-815bd77b2aa4 /out:1.5.3.aml
echo 1.5.4.aml
"C:\Program Files (x86)\Sandcastle\ProductionTools\XslTransform.exe" AllComments.xml /xsl:VersionHistory-all.xslt /arg:version=1.5.4,guid=923042ec-783b-47a0-91cd-d859e9354613 /out:1.5.4.aml