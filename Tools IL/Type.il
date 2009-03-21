.assembly extern mscorlib{
    .ver 2:0:0:0
    .publickeytoken = (B7 7A 5C 56 19 34 E0 89)
}
.assembly 'Tools.IL'{
    #include "Version.il"
    .custom instance void [mscorlib]System.CLSCompliantAttribute::.ctor(bool) = { bool(true) }
}
.assembly extern System.Core{
    .ver 3:5:0:0
    .publickeytoken = (B7 7A 5C 56 19 34 E0 89)
}                           
.class public auto ansi sealed Tools.SpecialT.TypeTools
    extends [mscorlib]System.Object
{
    .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = {}
    .method public static bool IsDefined<([mscorlib]System.Enum) T>(!!T 'value') cil managed{
        .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = {}
        .maxstack  2
        ldtoken    !!T
        call       class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
        call       class [mscorlib]System.Array [mscorlib]System.Enum::GetValues(class [mscorlib]System.Type)
        ldarg.0
        box        !!T
        call       int32 [mscorlib]System.Array::IndexOf(class [mscorlib]System.Array,object)
        ldc.i4.0
        clt
        ldc.i4.0
        ceq
        ret 
    }
    
    .method public static class [mscorlib]System.Reflection.FieldInfo GetConstant <([mscorlib]System.Enum) T>(!!T 'value') cil managed{
        .custom instance void [mscorlib]System.CLSCompliantAttribute::.ctor(bool) = { bool(false) }
        .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = {}
        .maxstack 4
        ldtoken !!T
        call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
        ldtoken !!T
        call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
        ldarg.0 
        box !!T
        call string [mscorlib]System.Enum::GetName(class [mscorlib]System.Type, object)
        dup
        brnull.s isnull
        callvirt instance class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Type::GetField(string)
        ret
isnull:
        pop
        pop
        ldnull
        ret         
    }
}