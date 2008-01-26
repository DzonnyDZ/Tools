Imports Tools.InternalT

'This file should be shared with all VB project in Tools
#If Config = Nightly Then
<Assembly: AssemblyBuildStage(BuildStates.Nightly)> 
#ElseIf Config = Alpha Then
<Assembly: AssemblyBuildStage(BuildStates.Alpha)>
#ElseIf Config = Beta Then
<Assembly: AssemblyBuildStage(BuildStates.Beta)>
#ElseIf Config = RC Then
<Assembly: AssemblyBuildStage(BuildStates.RC)>
#ElseIf Config = Release Then
<Assembly: AssemblyBuildStage(BuildStates.Release )>
#End If