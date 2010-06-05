.namespace Tools.SpecialT
{
    .class public ansi auto sealed DelegateTools
        extends [mscorlib]System.Object
    {
        .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = {}
    
        .method public static !!T CombineWith<([mscorlib]System.Delegate) T>([in] !!T, [in] !!T) cil managed{
            .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = {}
            .maxstack 2
            ldarg.0
            box !!T
            castclass [mscorlib]System.Delegate
            ldarg.1
            box !!T
            castclass [mscorlib]System.Delegate
            call class [mscorlib]System.Delegate [mscorlib]System.Delegate::Combine(class [mscorlib]System.Delegate, class [mscorlib]System.Delegate)
            unbox.any !!T
            ret
        }

        .method public static !!T Remove<([mscorlib]System.Delegate) T>([in] !!T, [in] !!T) cil managed{
            .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = {}
            .maxstack 2
            ldarg.0
            box !!T
            castclass [mscorlib]System.Delegate
            ldarg.1
            box !!T
            castclass [mscorlib]System.Delegate
            call class [mscorlib]System.Delegate [mscorlib]System.Delegate::Remove(class [mscorlib]System.Delegate, class [mscorlib]System.Delegate)
            unbox.any !!T
            ret
        }

        .method public static !!T RemoveAll<([mscorlib]System.Delegate) T>([in] !!T, [in] !!T) cil managed{
            .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = {}
            .maxstack 2
            ldarg.0
            box !!T
            castclass [mscorlib]System.Delegate
            ldarg.1
            box !!T
            castclass [mscorlib]System.Delegate
            call class [mscorlib]System.Delegate [mscorlib]System.Delegate::RemoveAll(class [mscorlib]System.Delegate, class [mscorlib]System.Delegate)
            unbox.any !!T
            ret
        }

        .method public static !!T CombineWith<([mscorlib]System.Delegate) T>([in] !!T, [in] !!T[]) cil managed
        {
            .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = {}
            .param [2]
            .custom instance void [mscorlib]System.ParamArrayAttribute::.ctor()
            .maxstack 2
            ldarg.0
            box !!T
            castclass class [mscorlib]System.Delegate
            ldarg.1
            castclass class [mscorlib]System.Delegate[]
            call class [mscorlib]System.Delegate [mscorlib]System.Delegate::Combine(class [mscorlib]System.Delegate[])
            call class [mscorlib]System.Delegate [mscorlib]System.Delegate::Combine(class [mscorlib]System.Delegate, class [mscorlib]System.Delegate)
            unbox.any !!T
            ret
        }
    }
}