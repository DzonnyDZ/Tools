Imports System.Reflection

Namespace RuntimeT.CompilerServicesT

    ''' <summary>Changes attributes of a member</summary>
    ''' <remarks>Applying this attribute on a member causes nothing on itself. You must run supporting post-processing tool (such as AssemblyPostprocessoer) on your assembly once it's compiled to apply changes denoted by this attributes.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Class Or AttributeTargets.Constructor Or AttributeTargets.Delegate Or AttributeTargets.Enum Or AttributeTargets.Event Or AttributeTargets.Field Or AttributeTargets.Interface Or AttributeTargets.Method Or AttributeTargets.Property Or AttributeTargets.Struct)>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "ChangeAttributes")>
    Public Class ChangeAttributesAttribute
        Inherits PostprocessingAttribute

        ''' <summary>Use this constructor when <see cref="ChangeAttributesAttribute"/> is applied on a method, constructor and accessor</summary>
        ''' <param name="or">This value will be OR-ed with existing method attributes</param>
        ''' <param name="and">This value will be AND-ed with existing method attributes</param>
        ''' <remarks>When used on inappropriate member types values are treated as values from member-appropriate enumeration. This can lead to unexpected results.</remarks>
        Public Sub New([or] As MethodAttributes, Optional [and] As MethodAttributes = &HFFFFFFFF)
            Me.New(CInt([or]), CInt([and]))
        End Sub

        ''' <summary>Use this constructor when <see cref="ChangeAttributesAttribute"/> is applied on a field</summary>
        ''' <param name="or">This value will be OR-ed with existing field attributes</param>
        ''' <param name="and">This value will be AND-ed with existing field attributes</param>
        ''' <remarks>When used on inappropriate member types values are treated as values from member-appropriate enumeration. This can lead to unexpected results.</remarks>
        Public Sub New([or] As FieldAttributes, Optional [and] As FieldAttributes = &HFFFFFFFF)
            Me.New(CInt([or]), CInt([and]))
        End Sub

        ''' <summary>Use this constructor when <see cref="ChangeAttributesAttribute"/> is applied on a property</summary>
        ''' <param name="or">This value will be OR-ed with existing property attributes</param>
        ''' <param name="and">This value will be AND-ed with existing property attributes</param>
        ''' <remarks>When used on inappropriate member types values are treated as values from member-appropriate enumeration. This can lead to unexpected results.</remarks>
        Public Sub New([or] As PropertyAttributes, Optional [and] As PropertyAttributes = &HFFFFFFFF)
            Me.New(CInt([or]), CInt([and]))
        End Sub

        ''' <summary>Use this constructor when <see cref="ChangeAttributesAttribute"/> is applied on an event</summary>
        ''' <param name="or">This value will be OR-ed with existing event attributes</param>
        ''' <param name="and">This value will be AND-ed with existing event attributes</param>
        ''' <remarks>When used on inappropriate member types values are treated as values from member-appropriate enumeration. This can lead to unexpected results.</remarks>
        Public Sub New([or] As EventAttributes, Optional [and] As EventAttributes = &HFFFFFFFF)
            Me.New(CInt([or]), CInt([and]))
        End Sub

        ''' <summary>Use this constructor when <see cref="ChangeAttributesAttribute"/> is applied on a type (i.e. class, structure, delegate or enumeration)</summary>
        ''' <param name="or">This value will be OR-ed with existing type attributes</param>
        ''' <param name="and">This value will be AND-ed with existing type attributes</param>
        ''' <remarks>When used on inappropriate member types values are treated as values from member-appropriate enumeration. This can lead to unexpected results.</remarks>
        Public Sub New([or] As TypeAttributes, Optional [and] As TypeAttributes = &HFFFFFFFF)
            Me.New(CInt([or]), CInt([and]))
        End Sub

        ''' <summary>CTor - creates a new instance of <see cref="ChangeAttributesAttribute"/> class</summary>
        ''' <param name="or">This value will be OR-ed with existing member attributes</param>
        ''' <param name="and">This value will be AND-ed with existing member attributes</param>
        ''' <remarks>Meaning of value of <paramref name="or"/> and <paramref name="and"/> depends on type of member this attribute is applied on.</remarks>
        Public Sub New(or%, and%)
            Me.Or = [or]
            Me.And = [and]
        End Sub

        ''' <summary>Gets attributes to be AND-ed with existing member attributes</summary>
        Public ReadOnly Property And%

        ''' <summary>Gets attributes to be OR-ed with existing member attributes</summary>
        Public ReadOnly Property Or%
    End Class
End Namespace