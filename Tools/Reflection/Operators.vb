Imports System.Runtime.CompilerServices, Tools.ExtensionsT
Imports System.Reflection
Imports System.Linq, Tools.LinqT
Imports System.Runtime.InteropServices

#If Config <= Nightly Then 'Stage: Nightly
Namespace ReflectionT

    ''' <summary>Operators supported by CLI</summary>
    ''' <remarks>High order byte (exluding its MSB) is number that uniquely identifies the operator.
    ''' Low-order half-byte represents number of operands of the operator (1 or 2).
    ''' If MSB of low-order byte is set then operator is non-standard.
    ''' If LSB of high-order half-byle of low-order byte (9th LSB bit in whole number) is set then operator is assignment.
    ''' See <seealso cref="Operators_masks"/>.
    ''' Names of items of the enumeration are names of operator methods without 'op_' prefix.</remarks>
    ''' <version version="1.5.3">Added CLS names of operators to documentation</version>
    Public Enum Operators As Short
        ''' <summary>No operator</summary>
        no = False
        ''' <summary>Decrement (unary, like C++/C# --) <c>op_decrement</c></summary>
        Decrement = &H101
        ''' <summary>Increment (unary, like C++/C# ++) <c>op_Increment</c></summary>
        Increment = &H201
        ''' <summary>Unary negation (unary minus operator like C++/C#/VB -) <c>op_UnaryNegation</c></summary>
        UnaryNegation = &H301
        ''' <summary>Unary plus (like C++/C#/VB +) <c>op_UnaryPlus</c></summary>
        UnaryPlus = &H401
        ''' <summary>Logical not (unary, like C++/C# !, VB Not) <c>op_LogicalNot</c></summary>
        LogicalNot = &H501
        ''' <summary>True operator - if value should be treated as True (unary, like VB IsTrue) <c>op_True</c></summary>
        [True] = &H601
        ''' <summary>False operator - if value should be treated as False (unary, like VB IsFalse) <c>op_False</c></summary>
        [False] = &H701
        ''' <summary>Reference operator (unary, like C++ &amp;) <c>op_AddressOf</c></summary>
        [AddressOf] = &H801
        ''' <summary>Bitwise not operator (unary, like C++/C# ~, VB Not) <c>op_OnesComplement</c></summary>
        OnesComplement = &H901
        ''' <summary>Pointer dereference (unary, like C++ *) <c>op_PointerDereference</c></summary>
        PointerDereference = &HA01

        ''' <summary>Addition (binary, like C++/C#/VB +) <c>op_Addition</c></summary>
        Addition = &HB02
        ''' <summary>Subtraction (binary, like C++/C#/VB -) <c>op_Subtraction</c></summary>
        Subtraction = &HC02
        ''' <summary>Multiplication (binary, like C++/C#/VB *) <c>op_Multiply</c></summary>
        Multiply = &HD02
        ''' <summary>Division (binary, like C++/C#/VB /) <c>op_Division</c></summary>
        Division = &HE02
        ''' <summary>Modulus (division remainder, binary, like C++/C# %, VB Mod) <c>op_Modulus</c></summary>
        Modulus = &HF02
        ''' <summary>Bitwise xor (exclusive or, binary, like C++/C# ^, VB Xor) <c>op_ExclusiveOr</c></summary>
        ExclusiveOr = &H1002
        ''' <summary>Bitwise and (binary, like C++/C# &amp;, VB And) <c>op_BitwiseAnd</c></summary>
        BitwiseAnd = &H1102
        ''' <summary>Bitwise or (binary, like C++/C# |, VB Or) <c>op_BitwiseOr</c></summary>
        BitwiseOr = &H1202
        ''' <summary>Logical and (binary, like C++/C# &amp;&amp;, VB AndAlso) <c>op_LogicalAnd</c></summary>
        LogicalAnd = &H1302
        ''' <summary>Logical or (binary, like C++/C# ||, VB OrElse) <c>op_LogicalOr</c></summary>
        LogicalOr = &H1402
        ''' <summary>Assignment(binary, like C++/C#/VB =) <c>op_Assign</c></summary>
        Assign = &H1512
        ''' <summary>Left shift (binary, like C++/C#/VB &lt;&lt;) <c>op_LeftShift</c></summary>
        LeftShift = &H1602
        ''' <summary>Right shift (binary, like C++/C#/VB >>) <c>op_RightShift</c></summary>
        RightShift = &H1702
        ''' <summary>Signed right shift (binary) <c>op_SignedRightShift</c></summary>
        ''' <version version="1.5.4">Fix: member renamed form <c>SignedRightShif</c> to <c>SignedRightShift</c></version>
        SignedRightShift = &H1802
        ''' <summary>Unsigned right shift (binary) <c>op_UnignedRightShift</c></summary>
        UnsignedRightShift = &H1902
        ''' <summary>Equality comparison (binary, like C++/C# ==, VB =) <c>op_Equality</c></summary>
        Equality = &H1A02
        ''' <summary>Greater than comparison (binary, like C++/C#/VB >) <c>op_GreaterThan</c></summary>
        GreaterThan = &H1B02
        ''' <summary>Less than comparison (binary, like C++/C#/VB &lt;) <c>op_LessThan</c></summary>
        LessThan = &H1C02
        ''' <summary>Inequality comparison (binary, like C++/C# !=; VB &lt;>) <c>op_Inequality</c></summary>
        Inequality = &H1E02
        ''' <summary>Greater than or equal comparison (binary, like C++/C#/VB >=) <c>op_GreaterThanOrEqual</c></summary>
        GreaterThanOrEqual = &H1F02
        ''' <summary>Less than or equal comparison (binary, like C++/C#/VB &lt;=) <c>op_LessThanOrEqual</c></summary>
        LessThanOrEqual = &H200
        ''' <summary>Self-assignment of unsigned right shift (binary) <c>op_UnsignedRightShiftAssignment</c></summary>
        UnsignedRightShiftAssignment = &H2012
        ''' <summary>Member selection (binary, like C++ ->) <c>op_MemberSelection</c></summary>
        MemberSelection = &H2102
        ''' <summary>Self-assignment of right shift (binary, like C++/C#/VB >>=) <c>op_RightShiftAssignment</c></summary>
        RightShifAssignment = &H2212
        ''' <summary>Self-assigment of multiplication (binary, like C++/C#/VB *=) <c>op_MultiplicationAssignment</c></summary>
        MultiplicationAssignment = &H2312
        ''' <summary>Selection of pointer to member (binary, like C++ ->*) <c>op_PointerToMemberSelection</c></summary>
        PointerToMemberSelection = &H2402
        ''' <summary>Self-assignment of subtraction (binary, like C++/C#/VB -=) <c>op_SubtractionAssignment</c></summary>
        SubtractionAssignment = &H2512
        ''' <summary>Bitwise exclusive or self-assigment (binary, like C++/C# ^=) <c>op_ExclusiveOrAssignment</c></summary>
        ExclusiveOrAssignment = &H2612
        ''' <summary>Self-assigment of left shift (binary, like C++/C#/VB &lt;&lt;=) <c>op_LeftShiftAssignment</c></summary>
        LeftShiftAssignment = &H2712
        ''' <summary>Modulus (division remainder) self-assignment (binary, like C++/C# %=) <c>op_ModulusAssignment</c></summary>
        ModulusAssignment = &H2812
        ''' <summary>Self-assigmment of addition (binary, like C++/C#/VB +=) <c>op_AdditionAssignment</c></summary>
        AditionAssignment = &H2912
        ''' <summary>Self-assignment of witwise and (binary, like C++/C# &amp;=) <c>op_BitwiseAndAssignment</c></summary>
        BitwiseAndAssignment = &H2A12
        ''' <summary>Self-assignment of bitwise or (binary, like C++/C# |=) <c>op_BitwiseOrAssignment</c></summary>
        BitwiseOrAssignment = &H2B12
        ''' <summary>Comma (operation grouping, binary, like C++ ,) <c>op_Comma</c></summary>
        Comma = &H2C02
        ''' <summary>Self-assignment of division (binary, like C++/C#/VB /=) <c>op_DivisionAssignment</c></summary>
        DivisionAssignment = &H2D12

        ''' <summary>String contactenation (VB specific, binary, like VB &amp;) <c>op_Concatenate</c></summary>
        Concatenate = &H2E82
        ''' <summary>Exponent (VB specific, binary, like VB ^) <c>op_Exponent</c></summary>
        Exponent = &H2F82
        ''' <summary>Force-integral division (VB specific, binary, like VB \, C++/C# / on integers) <c>op_IntegerDivision</c></summary>
        IntegerDivision = &H3082

        ''' <summary>Implicit conversion (unary, like C# implicit, VB Narrowing CType) <c>op_Implicit</c></summary>
        Implicit = &H3101
        ''' <summary>Explicit conversion (unary, like C# explicit, VB Widening CType) <c>op_Explicit</c></summary>
        Explicit = &H3201
    End Enum

    ''' <summary>Masks for the <see cref="Operators"/> enumeration</summary>
    <Flags()> _
    Public Enum Operators_masks As Short
        ''' <summary>Masks operator number. This number is unique within <see cref="Operators"/>, but has no relation to anything in CLI.</summary>
        OperatorID = &H7F00
        ''' <summary>Masks number of operands</summary>
        NoOfOperands = &HF
        ''' <summary>Masks if operator is standard (0) or non-standard (1)</summary>
        NonStandard = &H80
        ''' <summary>Masks if operator is assignment (1) or not (0)</summary>
        Assignment = &H10
    End Enum
End Namespace
#End If