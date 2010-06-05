.corflags 9
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