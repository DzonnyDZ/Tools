//This file should be shared by all C# project in ĐTools
using Tools.InternalT;
#if Release
[assembly: AssemblyBuildStageAttribute(BuildStates.Release)]
#else
#if RC
[assembly: AssemblyBuildStageAttribute(BuildStates.RC)]
#else
#if Beta
[assembly: AssemblyBuildStageAttribute(BuildStates.Beta)]
#else
#if Alpha
[assembly: AssemblyBuildStageAttribute(BuildStates.Alpha)]
#else
#if Nightly
[assembly: AssemblyBuildStageAttribute(BuildStates.Nightly)]
#endif
#endif
#endif
#endif
#endif 