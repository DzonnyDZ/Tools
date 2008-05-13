.assembly 'Tools IL'{}
.assembly extern mscorlib{
    .ver 2:0:0:0
    .publickeytoken = (B7 7A 5C 56 19 34 E0 89)
}
.assembly extern System.Core{
    .ver 3:5:0:0
    .publickeytoken = (B7 7A 5C 56 19 34 E0 89)
}                           
.class public auto ansi sealed Tools.SpecialT.TypeTools
    extends [mscorlib]System.Object
{
    .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor()
    .method public static bool IsDefined<([mscorlib]System.Enum) T>(!!T 'value') cil managed
    {
        .custom instance void [System.Core]System.Runtime.CompilerServices.ExtensionAttribute::.ctor()
        .maxstack 2
        .locals init (
            [0] bool IsDefined)
        L_0000: nop 
        L_0001: ldtoken !!T
        L_0006: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
        L_000b: call class [mscorlib]System.Array [mscorlib]System.Enum::GetValues(class [mscorlib]System.Type)
        L_0010: ldarg.0 
        L_0011: call int32 [mscorlib]System.Array::IndexOf(class [mscorlib]System.Array, object)
        L_0016: ldc.i4.0 
        L_0017: clt 
        L_0019: ldc.i4.0 
        L_001a: ceq 
        L_001c: stloc.0 
        L_001d: br.s L_001f
        L_001f: ldloc.0 
        L_0020: ret 
    }

}