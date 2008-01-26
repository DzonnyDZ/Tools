'ASAP:
Imports System.Drawing

#If Config <= Nightly Then 'Stage:Nightly
Namespace CodeDomT
    ''' <summary>Provides images for graphic representation of code members</summary>
    Module CodeImages
        ''' <summary>Represents supported object types for code images</summary>
        Public Enum Objects
            ''' <summary>Assembly</summary>
            Assembly
            ''' <summary>Attribute. Class that derives from <see cref="Attribute"/></summary>
            Attribute
            ''' <summary>Class (reference type)</summary>
            [Class]
            ''' <summary>Constant</summary>
            ''' <remarks>Represents .NET constant, not C++ one that is more like read-only field</remarks>
            Constant
            ''' <summary>Constructor method</summary>
            CTor
            ''' <summary>Delegate. Class that inherits from <see cref="[Delegate]"/></summary>
            [Delegate]
            ''' <summary>Enumeration. Class that inherits from <see cref="[Enum]"/></summary>
            [Enum]
            ''' <summary>Enumeration item. Constant inside class that derives from <see cref="[Enum]"/></summary>
            EnumItem
            ''' <summary>Event</summary>
            [Event]
            ''' <summary>Exception. Class that derives from <see cref="Exception"/></summary>
            Exception
            ''' <summary>Field (class- or global-level variable)</summary>
            Field
            ''' <summary>Read-only property</summary>
            Getter
            ''' <summary>Interface</summary>
            [Interface]
            ''' <summary>Library (represents unmanaged library)</summary>
            Library
            ''' <summary>C/C++ macro</summary>
            Macro
            ''' <summary>Map</summary>
            Map
            ''' <summary>Map item</summary>
            MapItem
            ''' <summary>Method (Sub and Function in Visual Basic)</summary>
            Method
            ''' <summary>Group of overloaded methods</summary>
            MethodOverload
            ''' <summary>.NET PE module</summary>
            ''' <remarks>This is something different tha VB standard module. See <seealso cref="StandardModule"/></remarks>
            [Module]
            ''' <summary>Namespace</summary>
            [Namespace]
            ''' <summary>Any object</summary>
            [Object]
            ''' <summary>Operator</summary>
            [Operator]
            ''' <summary>Property (read-write)</summary>
            [Property]
            ''' <summary>Single resource (this was image, single string, audio file etc. before compilation)</summary>
            Resource
            ''' <summary>Group of resources (this was a resx file before compilation)</summary>
            Resources
            ''' <summary>Write-only property</summary>
            Setter
            ''' <summary>VB standard module</summary>
            ''' <remarks>This is somethign different than .NET PE module. See <seealso cref="[Module]"/></remarks>
            StandardModule
            ''' <summary>Structure (value type, class that inherits <see cref="ValueType"/>).</summary>
            [Structure]
            ''' <summary>C++ template (not .NET generic)</summary>
            Template
            ''' <summary>Type (unmanaged)</summary>
            Type
            ''' <summary>C++ typedef</summary>
            TypeDef
            ''' <summary>Union (union-layered structure)</summary>
            Union
            ''' <summary>Value type that is not considered to be a structure</summary>
            ValueType
        End Enum
        ''' <summary>Gets image that represents code object</summary>
        ''' <param name="ObjectType">Object type</param>
        ''' <returns>Image that represents given object type</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="ObjectType"/> is not member of <see cref="Objects"/></exception>
        Public Function GetImage(ByVal ObjectType As Objects) As Image
            Select Case ObjectType
                Case Objects.Assembly : Return My.Resources.ObjectImages.iAssembly
                Case Objects.Attribute : Return My.Resources.ObjectImages.iAttribute
                Case Objects.Class : Return My.Resources.ObjectImages.iClass
                Case Objects.Constant : Return My.Resources.ObjectImages.iConstant
                Case Objects.CTor : Return My.Resources.ObjectImages.iCTor
                Case Objects.Delegate : Return My.Resources.ObjectImages.iDelegate
                Case Objects.Enum : Return My.Resources.ObjectImages.iEnum
                Case Objects.EnumItem : Return My.Resources.ObjectImages.iEnumItem
                Case Objects.Event : Return My.Resources.ObjectImages.iEvent
                Case Objects.Exception : Return My.Resources.ObjectImages.iException
                Case Objects.Field : Return My.Resources.ObjectImages.iField
                Case Objects.Getter : Return My.Resources.ObjectImages.iGetter
                Case Objects.Interface : Return My.Resources.ObjectImages.iInterface
                Case Objects.Library : Return My.Resources.ObjectImages.iLibrary
                Case Objects.Macro : Return My.Resources.ObjectImages.iMacro
                Case Objects.Map : Return My.Resources.ObjectImages.iMap
                Case Objects.MapItem : Return My.Resources.ObjectImages.iMapItem
                Case Objects.Method : Return My.Resources.ObjectImages.iMethod
                Case Objects.MethodOverload : Return My.Resources.ObjectImages.iMethodOverload
                Case Objects.Module : Return My.Resources.ObjectImages.iModule
                Case Objects.Namespace : Return My.Resources.ObjectImages.iNamespace
                Case Objects.Object : Return My.Resources.ObjectImages.iObject
                Case Objects.Operator : Return My.Resources.ObjectImages.iOperator
                Case Objects.Property : Return My.Resources.ObjectImages.iProperty
                Case Objects.Resource : Return My.Resources.ObjectImages.iResource
                Case Objects.Resources : Return My.Resources.ObjectImages.iResources
                Case Objects.Setter : Return My.Resources.ObjectImages.iSetter
                Case Objects.StandardModule : Return My.Resources.ObjectImages.iStandardModule
                Case Objects.Structure : Return My.Resources.ObjectImages.iStructure
                Case Objects.Template : Return My.Resources.ObjectImages.iTemplate
                Case Objects.Type : Return My.Resources.ObjectImages.iType
                Case Objects.TypeDef : Return My.Resources.ObjectImages.iTypeDef
                Case Objects.Union : Return My.Resources.ObjectImages.iUnion
                Case Objects.ValueType : Return My.Resources.ObjectImages.iValueType

                Case Else : Throw New InvalidEnumArgumentException("ObjectType", ObjectType, GetType(Objects))
            End Select
        End Function
    End Module
End Namespace
#End If