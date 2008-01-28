'ASAP:
Imports System.Drawing, Tools.DrawingT.ImageTools
Imports System.Runtime.CompilerServices
Imports System.Reflection

#If Config <= Nightly Then 'Stage:Nightly
Namespace ReflectionT
    ''' <summary>Provides images for graphic representation of code members</summary>
    Public Module CodeImages
        ''' <summary>Represents supported object types for code images</summary>
        ''' <remarks>Values of items of this enumeration are constructed acording to type of object represented by them. But this construction is not very clear and application should not rely on it. Description follows:
        ''' <list type="table">
        ''' <listheader><term>Bit no.</term><description>Meaning</description></listheader>
        ''' <item><term>0</term><description>Object is either collection, produces new instance (CTor) or is managed but somewhat strange (union).</description></item>
        ''' <item><term>1</term><description>Object either has own <see cref="Reflection.MemberInfo"/>-derived class or is treated as fallback by logic of <see cref="CodeImages"/></description></item>
        ''' <item><term>2</term><description>Object represent somethign representable by <see cref="MemberInfo"/>-derived class</description></item>
        ''' <item><term>3</term><description>Member is atomic (from that poit of view taht it does not contain another members. Properties and event are not considered atomic)</description></item>
        ''' <item><term>4</term><description>Members represents specific class (<see cref="[Delegate]"/>, <see cref="Exception"/>, <see cref="Attribute"/>)</description></item>
        ''' <item><term>5</term><description>Member can be changed (assigned value to)</description></item>
        ''' <item><term>6</term><description>Member has value</description></item>
        ''' <item><term>7</term><description>Object represents error or exception</description></item>
        ''' <item><term>8</term><description>Mmeber is callable</description></item>
        ''' <item><term>9</term><description>Object is instantiable</description></item>
        ''' <item><term>11, 10</term><description>
        ''' <list type="table"><item><term>00</term><description>none of following</description></item>
        ''' <item><term>01</term><description>Object represents value type or top-level object</description></item>
        ''' <item><term>10</term><description>Object represents reference type, medium-level object (between class and assembly) or group of objects</description></item>
        ''' <item><term>11</term><description>Object is not object of code</description></item></list>
        ''' </description></item>
        ''' <item><term>12</term><description>Object is part of managed application</description></item>
        ''' <item><term>14, 13</term><description><list type="table">
        ''' <item><term>00</term><description>none of following</description></item>
        ''' <item><term>01</term><description>Object of higher level tha type</description></item>
        ''' <item><term>10</term><description>Type-level object</description></item>
        ''' <item><term>11</term><description>Object of lower level than type</description></item>
        ''' </list></description></item>
        ''' <item><term>16, 15</term><description><list type="table">
        ''' <item><term>00</term><description>none of following</description></item>
        ''' <item><term>01</term><description>Object is not generic</description></item>
        ''' <item><term>10</term><description>Object is open generic</description></item>
        ''' <item><term>11</term><description>Object is closed generic</description></item>
        ''' </list></description></item>
        ''' </list>
        ''' </remarks>
        Public Enum Objects
            ''' <summary>Assembly</summary>
            Assembly = &H5A02
            ''' <summary>Attribute. Class that derives from <see cref="Attribute"/></summary>
            Attribute = &H6D0A
            ''' <summary>Class (reference type)</summary>
            [Class] = &H6D04
            ''' <summary>Constant</summary>
            ''' <remarks>Represents .NET constant, not C++ one that is more like read-only field</remarks>
            Constant = &H7826
            ''' <summary>Constructor method</summary>
            CTor = &H7889
            ''' <summary>Delegate. Class that inherits from <see cref="[Delegate]"/></summary>
            [Delegate] = &H6D82
            ''' <summary>Enumeration. Class that inherits from <see cref="[Enum]"/></summary>
            [Enum] = &H6B0A
            ''' <summary>Enumeration item. Constant inside class that derives from <see cref="[Enum]"/></summary>
            EnumItem = &H7A26
            ''' <summary>Event</summary>
            [Event] = &H7884
            ''' <summary>Exception. Class that derives from <see cref="Exception"/></summary>
            Exception = &H6D4A
            ''' <summary>Field (class- or global-level variable)</summary>
            Field = &H7838
            ''' <summary>Read-only property</summary>
            Getter = &H78A2
            ''' <summary>Interface</summary>
            [Interface] = &H6C02
            ''' <summary>Library (represents unmanaged library)</summary>
            Library = &H5200
            ''' <summary>C/C++ macro</summary>
            Macro = &H7084
            ''' <summary>Map</summary>
            Map = &H6101
            ''' <summary>Map item</summary>
            MapItem = &H7034
            ''' <summary>Method (Sub and Function in Visual Basic)</summary>
            Method = &H7888
            ''' <summary>Group of overloaded methods</summary>
            MethodOverload = &H7C81
            ''' <summary>.NET PE module</summary>
            ''' <remarks>This is something different tha VB standard module. See <seealso cref="StandardModule"/></remarks>
            [Module] = &H5C02
            ''' <summary>Namespace</summary>
            [Namespace] = &H5C00
            ''' <summary>Any object</summary>
            [Object] = &H6D02
            ''' <summary>Operator</summary>
            [Operator] = &H7886
            ''' <summary>Property (read-write)</summary>
            [Property] = &H78B4
            ''' <summary>Single resource (this was image, single string, audio file etc. before compilation)</summary>
            Resource = &H7824
            ''' <summary>Group of resources (this was a resx file before compilation)</summary>
            Resources = &H7821
            ''' <summary>Write-only property</summary>
            Setter = &H7889
            ''' <summary>VB standard module</summary>
            ''' <remarks>This is somethign different than .NET PE module. See <seealso cref="[Module]"/></remarks>
            StandardModule = &H6C0A
            ''' <summary>Structure (value type, class that inherits <see cref="ValueType"/>).</summary>
            [Structure] = &H6B02
            ''' <summary>C++ template (not .NET generic)</summary>
            Template = &H2100
            ''' <summary>Type (unmanaged)</summary>
            Type = &H6100
            ''' <summary>C++ typedef</summary>
            TypeDef = &H6104
            ''' <summary>Union (union-layered structure)</summary>
            Union = &H6B03
            ''' <summary>Value type that is not considered to be a structure</summary>
            ValueType = &H6B04
            ''' <summary>Error. Used when erro ocured during aquiring reflection data.</summary>
            [Error] = &H644
            ''' <summary>Open generic class (with type parameters unspecified)</summary>
            GenericClassOpen = &HAD02
            ''' <summary>Closed generic type (unknown if class or structure or interface)</summary>
            GenericTypeClosed = &HED04
            ''' <summary>Closed generic class (with type parameters specified)</summary>
            GenericClassClosed = &HED02
            ''' <summary>Closed generic method (with type parameters specified)</summary>
            GenericMethodClosed = &HF886
            ''' <summary>Closed generic structure (with type parameters specified)</summary>
            GenericStructureClosed = &HEB02
            ''' <summary>Open generic method (with type parameters unspecified)</summary>
            GenericMethodOpen = &HB886
            ''' <summary>Generic parameter</summary>
            GenericParameter = &H2902
            ''' <summary>Open gneric structure (with type parameters unspecified)</summary>
            GenericStructureOpen = &HAB02
            ''' <summary>Open generic interface (with type parameters unspecified)</summary>
            GenericInterfaceOpen = &HAC02
            ''' <summary>Closed generic interface (with type parameters specified)</summary>
            GenericInterfaceClosed = &HEC02
            ''' <summary>Open generic type (unknown if class or structure or interface)</summary>
            GenericTypeOpen = &HA904
            ''' <summary>Generic open exception class</summary>
            GenericExceptionClosed = &HED4A
            ''' <summary>Generic closed exception class</summary>
            GenericExceptionOpen = &HAD4A
            ''' <summary>Generic closed attribute class</summary>
            GenericAttributeClosed = &HED0A
            ''' <summary>Generic open attribute class</summary>
            GenericAttributeOpen = &HAD0A
            ''' <summary>Generic open delegate</summary>
            GenericDelegateOpen = &HAD8A
            ''' <summary>Generic closed delegate</summary>
            GenericDelegateClosed = &HED8A
            ''' <summary>Question. Used for unknown kind of member.</summary>
            Question = &H604
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
                Case Objects.Error : Return My.Resources.ObjectImages.iError
                Case Objects.Question : Return My.Resources.ObjectImages.iQuestion
                Case Objects.GenericClassClosed : Return My.Resources.ObjectImages.iGenericInstanceClass
                Case Objects.GenericClassOpen : Return My.Resources.ObjectImages.iGenericClass
                Case Objects.GenericMethodClosed : Return My.Resources.ObjectImages.iGenericInstanceMethod
                Case Objects.GenericMethodOpen : Return My.Resources.ObjectImages.iGenericMethod
                Case Objects.GenericParameter : Return My.Resources.ObjectImages.iGenericParameter
                Case Objects.GenericStructureClosed : Return My.Resources.ObjectImages.iGenericInstanceStructure
                Case Objects.GenericStructureOpen : Return My.Resources.ObjectImages.iGenericStructure
                Case Objects.GenericTypeClosed : Return My.Resources.ObjectImages.iGenericInstance
                Case Objects.GenericInterfaceClosed : Return My.Resources.ObjectImages.iGenericInstanceInterface
                Case Objects.GenericInterfaceOpen : Return My.Resources.ObjectImages.iGenericInterface
                Case Objects.GenericTypeOpen : Return My.Resources.ObjectImages.iGeneric
                Case Objects.GenericAttributeClosed : Return My.Resources.ObjectImages.iAttribute_GenericClosed
                Case Objects.GenericAttributeOpen : Return My.Resources.ObjectImages.iAttribute_GenericOpen
                Case Objects.GenericExceptionClosed : Return My.Resources.ObjectImages.iException_GenericClosed
                Case Objects.GenericExceptionOpen : Return My.Resources.ObjectImages.iException_GenericOpen
                Case Objects.GenericDelegateClosed : Return My.Resources.ObjectImages.iDelegate_GenericClosed
                Case Objects.GenericDelegateOpen : Return My.Resources.ObjectImages.iDelegate_GenericOpen
                Case Else : Throw New InvalidEnumArgumentException("ObjectType", ObjectType, GetType(Objects))
            End Select
        End Function
        ''' <summary>Code element modifiers that produces overlay images</summary>
        ''' <remarks>With exception of <see cref="ObjectModifiers.Shortcut"/> modifiers are subset of <see cref="Reflection.MethodAttributes"/>. Thos attributes can be applied on any supported membert as defined in <seealso cref="Objects"/>. Modifiers can be combined, but not each with each.</remarks>
        Public Enum ObjectModifiers As Integer
            ''' <summary>No modifier</summary>
            None = 0
            ''' <summary>Private (See <see cref="Reflection.MethodAttributes.[Private]"/>)</summary>
            [Private] = Reflection.MethodAttributes.Private
            ''' <summary>Protected (family visibility) (See <see cref="Reflection.MethodAttributes.[Family]"/>)</summary>
            [Protected] = Reflection.MethodAttributes.Family
            ''' <summary>Friend (internal, assembly visibility  (See <see cref="Reflection.MethodAttributes.[Assembly]"/>)</summary>
            [Friend] = Reflection.MethodAttributes.Assembly
            ''' <summary>Public (See <see cref="Reflection.MethodAttributes.[Public]"/>)</summary>
            [Public] = Reflection.MethodAttributes.Public
            ''' <summary>Protected Friend (accessible from derived class in any assembly and from whole defining) (See <see cref="Reflection.MethodAttributes.FamORAssem"/>)</summary>
            [ProtectedFriend] = Reflection.MethodAttributes.FamORAssem
            ''' <summary>Accessible only from derived classes in defining assembly (See <see cref="Reflection.MethodAttributes.FamANDAssem"/>)</summary>
            [FriendProtected] = Reflection.MethodAttributes.FamANDAssem
            ''' <summary>Static (Shared in VB) (See <see cref="Reflection.MethodAttributes.Static"/>)</summary>
            [Static] = Reflection.MethodAttributes.Static
            ''' <summary>Final (NotInheritable class or Overrides NotOveridable or without Overridable method in VB) (See <see cref="Reflection.MethodAttributes.Final"/>)</summary>
            [Sealed] = Reflection.MethodAttributes.Final
            ''' <summary>Shortcust or reference</summary>
            [Shortcut] = &H10000
        End Enum
        ''' <summary>Gets overlay image tha repreents given modifiers</summary>
        ''' <param name="Modifiers">Modifiers to get overlay image for</param>
        ''' <returns>16×16 px image with transparent background that graphicaly represents <paramref name="Modifiers"/></returns>
        ''' <remarks>
        ''' <para>Images are dynamically generated on request and cached. If you alter the image returned if will be altered in chache as well and on next call with same <paramref name="Modifiers"/> it will be returned modified</para>.
        ''' <paramref name="Modifiers"/> Can be or-combination of values of <paramref name="Modifiers"/> enumeration. But with some limitation. <paramref name="Modifiers"/> &amp; <see cref="Reflection.MethodAttributes.MemberAccessMask"/> should be one of <see cref="ObjectModifiers.[Private]"/>, <see cref="ObjectModifiers.FriendProtected"/>, <see cref="ObjectModifiers.[Friend]"/>, <see cref="ObjectModifiers.[Protected]"/>, <see cref="ObjectModifiers.ProtectedFriend"/>, <see cref="ObjectModifiers.[Public]"/> (<see cref="ObjectModifiers.[Public]"/> generates no overlay) or this part will be ignored.
        ''' Other <see cref="ObjectModifiers"/> members can be or-ed with no limitation. Overlay will be combined and single images will be smartly positioned into 4 corners of it.
        ''' Combination of either <see cref="ObjectModifiers.ProtectedFriend"/> or <see cref="ObjectModifiers.FriendProtected"/> with <see cref="ObjectModifiers.[Static]"/>, <see cref="ObjectModifiers.Sealed"/> and <see cref="ObjectModifiers.Shortcut"/> results to need to position 5 images into 4 corners. So, shortcut overly is placed as last one to bottom left corner overlaying protected overlay.
        ''' </remarks>
        Public Function GetOverlayImage(ByVal Modifiers As ObjectModifiers) As Image
            Static dic As New Dictionary(Of ObjectModifiers, Image)
            If dic.ContainsKey(Modifiers) Then Return dic(Modifiers)
            If Modifiers = ObjectModifiers.None Then Return Nothing
            Dim BottomLeft As Image = Nothing
            Select Case Modifiers And Reflection.MethodAttributes.MemberAccessMask
                Case ObjectModifiers.Private : BottomLeft = My.Resources.ObjectImages.oPrivate
                Case ObjectModifiers.Protected : BottomLeft = My.Resources.ObjectImages.oProtected
                Case ObjectModifiers.Friend : BottomLeft = My.Resources.ObjectImages.oFriend
                Case ObjectModifiers.ProtectedFriend : BottomLeft = ProtectedFriend
                Case ObjectModifiers.FriendProtected : BottomLeft = FriendProtected
            End Select
            Dim TopLeft As Image = Nothing
            If Modifiers And ObjectModifiers.Static Then _
                TopLeft = My.Resources.ObjectImages.oStatic
            Dim BottomRight As Image = Nothing
            Dim TopRight As Image = Nothing
            If Modifiers And ObjectModifiers.Sealed Then
                If BottomLeft Is Nothing Then
                    BottomLeft = My.Resources.ObjectImages.oNotInheritable
                ElseIf (Modifiers And Reflection.MethodAttributes.MemberAccessMask) <> ObjectModifiers.ProtectedFriend AndAlso (Modifiers And Reflection.MethodAttributes.MemberAccessMask) <> ObjectModifiers.FriendProtected Then
                    BottomRight = My.Resources.ObjectImages.oNotInheritable
                Else
                    TopRight = Nothing
                End If
            End If
            If Modifiers And ObjectModifiers.Shortcut Then
                If BottomLeft Is Nothing Then
                    BottomLeft = My.Resources.ObjectImages.oShortcut
                ElseIf (Modifiers And Reflection.MethodAttributes.MemberAccessMask) <> ObjectModifiers.ProtectedFriend AndAlso (Modifiers And Reflection.MethodAttributes.MemberAccessMask) <> ObjectModifiers.FriendProtected AndAlso BottomRight Is Nothing Then
                    BottomRight = My.Resources.ObjectImages.oShortcut
                ElseIf TopRight Is Nothing Then
                    TopRight = My.Resources.ObjectImages.oShortcut
                ElseIf TopLeft Is Nothing Then
                    TopLeft = My.Resources.ObjectImages.oShortcut
                Else
                    BottomRight = My.Resources.ObjectImages.oShortcut
                End If
            End If
            Dim img As New Bitmap(16, 16)
            img = img.Overlay(BottomLeft, ContentAlignment.BottomLeft)
            img = img.Overlay(TopLeft, ContentAlignment.TopLeft)
            img = img.Overlay(TopRight, ContentAlignment.TopRight)
            If BottomRight Is My.Resources.ObjectImages.oShortcut AndAlso (BottomLeft Is ProtectedFriend OrElse BottomLeft Is FriendProtected) Then
                img = img.Overlay(BottomRight, ContentAlignment.BottomLeft)
            Else
                img = img.Overlay(BottomRight, ContentAlignment.BottomRight)
            End If
            dic(Modifiers) = img
            Return img
        End Function
        ''' <summary>Gets image that represents <see cref="ObjectModifiers.ProtectedFriend"/></summary>
        ''' <remarks>The image is dynamically generated an the cached</remarks>
        Private ReadOnly Property ProtectedFriend() As Image
            Get
                Static val As Image
                If val IsNot Nothing Then Return val
                val = New Bitmap(My.Resources.ObjectImages.oFriend.Width + My.Resources.ObjectImages.oProtected.Width, Math.Max(My.Resources.ObjectImages.oProtected.Height, My.Resources.ObjectImages.oFriend.Height))
                Dim g = Graphics.FromImage(val)
                g.DrawImageInPixels(My.Resources.ObjectImages.oProtected, 0, val.Height - My.Resources.ObjectImages.oProtected.Height)
                g.DrawImageInPixels(My.Resources.ObjectImages.oFriend, val.Width - My.Resources.ObjectImages.oFriend.Width, val.Height - My.Resources.ObjectImages.oFriend.Height)
                g.DrawImageInPixels(My.Resources.ObjectImages.oOr, val.Width \ 2 - My.Resources.ObjectImages.oOr.Width \ 2, val.Height - My.Resources.ObjectImages.oOr.Height)
                g.Flush(Drawing2D.FlushIntention.Sync)
                Return val
            End Get
        End Property
        ''' <summary>Gets image that represents <see cref="ObjectModifiers.FriendProtected"/></summary>
        ''' <remarks>The image is dynamically generated an the cached</remarks>
        Private ReadOnly Property FriendProtected() As Image
            Get
                Static val As Image
                If val IsNot Nothing Then Return val
                val = New Bitmap(My.Resources.ObjectImages.oFriend.Width + My.Resources.ObjectImages.oProtected.Width, Math.Max(My.Resources.ObjectImages.oProtected.Height, My.Resources.ObjectImages.oFriend.Height))
                Dim g = Graphics.FromImage(val)
                g.DrawImage(My.Resources.ObjectImages.oProtected, 0, val.Height - My.Resources.ObjectImages.oProtected.Height)
                g.DrawImage(My.Resources.ObjectImages.oFriend, val.Width - My.Resources.ObjectImages.oFriend.Width, val.Height - My.Resources.ObjectImages.oFriend.Height)
                g.DrawImage(My.Resources.ObjectImages.oAnd, val.Width \ 2 - My.Resources.ObjectImages.oAnd.Width \ 2, val.Height - My.Resources.ObjectImages.oAnd.Height)
                g.Flush(Drawing2D.FlushIntention.Sync)
                Return val
            End Get
        End Property
        ''' <summary>Returns image got from <see cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects)"/> overlayed with image got from <see cref="GetOverlayImage"/></summary>
        ''' <param name="ObjectType">Type of object for background image</param>
        ''' <param name="Modifiers">Object modifiers</param>
        ''' <returns>16×16px image that graphicaly represents <paramref name="ObjectType"/> with its modifiers.</returns>
        ''' <remarks>Images are dynamically created on request and cached. So if you change the image returned, it will be returned changed on next call.</remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="ObjectType"/> is not member of <see cref="Objects"/></exception>
        <DebuggerStepThrough()> _
        Public Function GetImage(ByVal ObjectType As Objects, ByVal Modifiers As ObjectModifiers) As Image
            Dim ret As Image
            If Cache.ContainsKey((CLng(ObjectType) << 32) Or CLng(Modifiers)) Then
                ret = Cache((CLng(ObjectType) << 32) Or CLng(Modifiers))
            Else
                ret = GetImage(ObjectType).Overlay(GetOverlayImage(Modifiers), ContentAlignment.MiddleCenter)
                Cache((CLng(ObjectType) << 32) Or CLng(Modifiers)) = ret
                RaiseEvent ImageAdded(ret, ObjectType, Modifiers)
            End If
            RaiseEvent ImageRequested(ret, ObjectType, Modifiers)
            Return ret
        End Function
        ''' <summary>Contains cache for <see cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects,Tools.ReflectionT.CodeImages.ObjectType)"/></summary>
        Private Cache As New Dictionary(Of Long, Image)
        ''' <summary>Raised after image is added to cache of images with overlay</summary>
        ''' <param name="Image">Image added</param>
        ''' <param name="ObjectType">Type of object for added image</param>
        ''' <param name="Modifiers">Object modifiers for added image</param>
        Public Event ImageAdded(ByVal Image As Image, ByVal ObjectType As Objects, ByVal Modifiers As ObjectModifiers)
        ''' <summary>Rased after before <see cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects,Tools.ReflectionT.CodeImages.ObjectType)"/> returns image</summary>
        ''' <param name="Image">Image to be returned</param>
        ''' <param name="ObjectType">Type of object for image</param>
        ''' <param name="Modifiers">Object modifiers for  image</param>
        Public Event ImageRequested(ByVal Image As Image, ByVal ObjectType As Objects, ByVal Modifiers As ObjectModifiers)
        ''' <summary>For each image in cache calls given callback method</summary>
        ''' <param name="Callback">Method to call. Parameters are same as of the <see cref="ImageAdded"/> event.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Callback"/> is null</exception>
        Public Sub WithAllImages(ByVal Callback As dSub(Of Image, Objects, ObjectModifiers))
            If Callback Is Nothing Then Throw New ArgumentNullException("Callback")
            For Each item In Cache
                Callback.Invoke(item.Value, item.Key >> 32, item.Key And &HFFFFFFFF)
            Next item
        End Sub
        ''' <summary>Returns image got from <see cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects)"/> overlayed with image got from <see cref="GetOverlayImage"/></summary>
        ''' <param name="ObjectType">Type of object for background image</param>
        ''' <param name="Attributes">Object modifiers</param>
        ''' <returns>16×16px image that graphicaly represents <paramref name="ObjectType"/> with its modifiers.</returns>
        ''' <remarks>Images are dynamically created on request and cached. So if you change the image returned, it will be returned changed on next call.</remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="ObjectType"/> is not member of <see cref="Objects"/></exception>
        Public Function GetImage(ByVal ObjectType As Objects, ByVal Attributes As MethodAttributes) As Image
            Return GetImage(ObjectType, DirectCast(Attributes, ObjectModifiers))
        End Function
#If Framework >= 3.5 Then
        ''' <summary>Gets image that graphically represents given type</summary>
        ''' <param name="Type">Type to get image for</param>
        ''' <returns>16×16 image representing type obtained using <see cref="GetImage"/></returns>
        ''' <remarks><seealso cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects)"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        <Extension()> Public Function GetImage(ByVal Type As Type) As Image
            If Type Is Nothing Then Throw New ArgumentNullException("Type")
            Dim TypeType As Objects
            'Generic parameters:
            If Type.IsGenericParameter Then
                TypeType = Objects.GenericParameter
                'Open generics:
            ElseIf Type.IsGenericTypeDefinition AndAlso GetType(Exception).IsAssignableFrom(Type) Then
                TypeType = Objects.GenericExceptionOpen
            ElseIf Type.IsGenericTypeDefinition AndAlso GetType(Attribute).IsAssignableFrom(Type) Then
                TypeType = Objects.GenericAttributeOpen
            ElseIf Type.IsGenericTypeDefinition AndAlso GetType([Delegate]).IsAssignableFrom(Type) Then
                TypeType = Objects.GenericDelegateOpen
            ElseIf Type.IsGenericTypeDefinition AndAlso Type.IsInterface Then
                TypeType = Objects.GenericInterfaceOpen
            ElseIf Type.IsGenericTypeDefinition AndAlso Type.IsClass Then
                TypeType = Objects.GenericClassOpen
            ElseIf Type.IsGenericTypeDefinition AndAlso Type.IsValueType Then
                TypeType = Objects.GenericStructureClosed
            ElseIf Type.IsGenericTypeDefinition Then
                TypeType = Objects.GenericTypeOpen
                'Closed generics:
            ElseIf Type.IsGenericType AndAlso GetType(Exception).IsAssignableFrom(Type) Then
                TypeType = Objects.GenericExceptionClosed
            ElseIf Type.IsGenericType AndAlso GetType(Attribute).IsAssignableFrom(Type) Then
                TypeType = Objects.GenericAttributeClosed
            ElseIf Type.IsGenericType AndAlso GetType([Delegate]).IsAssignableFrom(Type) Then
                TypeType = Objects.GenericDelegateClosed
            ElseIf Type.IsGenericType AndAlso Type.IsInterface Then
                TypeType = Objects.GenericInterfaceClosed
            ElseIf Type.IsGenericType AndAlso Type.IsClass Then
                TypeType = Objects.GenericClassClosed
            ElseIf Type.IsGenericType AndAlso Type.IsValueType Then
                TypeType = Objects.GenericStructureClosed
            ElseIf Type.IsGenericType Then
                TypeType = Objects.GenericTypeClosed
                'Non-generics
            ElseIf Type.IsInterface Then
                TypeType = Objects.Interface
            ElseIf GetType(Exception).IsAssignableFrom(Type) Then
                TypeType = Objects.Exception
            ElseIf GetType(Attribute).IsAssignableFrom(Type) Then
                TypeType = Objects.Attribute
            ElseIf Type.IsEnum Then
                TypeType = Objects.Enum
            ElseIf Type.IsValueType Then
                TypeType = Objects.Structure
            ElseIf GetType([Delegate]).IsAssignableFrom(Type) Then
                TypeType = Objects.Delegate
            ElseIf Type.IsClass AndAlso Not Type.IsGenericType AndAlso Not Type.IsGenericTypeDefinition AndAlso Type.IsDefined(GetType(Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute), False) AndAlso Type.GetConstructors(Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public).Length = 0 AndAlso Type.BaseType.Equals(GetType(Object)) Then
                TypeType = Objects.StandardModule
            ElseIf Type.Equals(GetType(Object)) Then
                TypeType = Objects.Object
            ElseIf Type.IsPrimitive Then
                TypeType = Objects.ValueType
            Else
                TypeType = Objects.Class
            End If
            'Overlay (modifiers)
            Dim Modifiers As ObjectModifiers
            If (Not Type.IsNested AndAlso Type.IsPublic) OrElse Type.IsNestedPublic Then
                Modifiers = ObjectModifiers.Public
            ElseIf Type.IsNestedAssembly OrElse (Not Type.IsNested AndAlso Type.IsNotPublic) Then
                Modifiers = ObjectModifiers.Friend
            ElseIf Type.IsNestedFamANDAssem Then
                Modifiers = ObjectModifiers.FriendProtected
            ElseIf Type.IsNestedFamily Then
                Modifiers = ObjectModifiers.Protected
            ElseIf Type.IsNestedFamORAssem Then
                Modifiers = ObjectModifiers.ProtectedFriend
            ElseIf Type.IsNestedPrivate Then
                Modifiers = ObjectModifiers.Private
            End If
            If Type.IsSealed Then Modifiers = Modifiers Or ObjectModifiers.Sealed
            'TODO: C# Static classes?
            Return GetImage(TypeType, Modifiers)
        End Function
        ''' <summary>Gets image that graphically represents given member</summary>
        ''' <param name="Member">Member to represent</param>
        ''' <returns>16×16px image that represents <paramref name="Member"/></returns>
        ''' <remarks><seealso cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects)"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function GetImage(ByVal Member As Reflection.MemberInfo) As Image
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            'TODO: Specialized GetImages()s or recursion will ocur
            Select Case Member.MemberType
                Case Reflection.MemberTypes.Constructor
                    Dim m = TryCast(Member, ConstructorInfo)
                    If m IsNot Nothing Then Return m.GetImage
                Case Reflection.MemberTypes.Event
                    Dim m = TryCast(Member, EventInfo)
                    If m IsNot Nothing Then Return m.GetImage
                Case Reflection.MemberTypes.Field
                    Dim m = TryCast(Member, FieldInfo)
                    If m IsNot Nothing Then Return m.GetImage
                Case Reflection.MemberTypes.Method
                    Dim m = TryCast(Member, MethodInfo)
                    If m IsNot Nothing Then Return m.GetImage
                Case Reflection.MemberTypes.Property
                    Dim m = TryCast(Member, PropertyInfo)
                    If m IsNot Nothing Then Return m.GetImage
                Case Reflection.MemberTypes.TypeInfo, Reflection.MemberTypes.NestedType
                    Dim m = TryCast(Member, Type)
                    If m IsNot Nothing Then Return m.GetImage
            End Select
            Return GetImage(Objects.Question)
        End Function
        ''' <summary>Gets image that graphicaly represents give method</summary>
        ''' <param name="Member">Method to get image for</param>
        ''' <returns>16×16px image that graphicaly represents <paramref name="Member"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
''' <remarks><seealso cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects)"/></remarks>
        <Extension()> Public Function GetImage(ByVal Member As MethodBase) As Image
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Dim Type As Objects
            If Member.IsConstructor Then
                Type = Objects.CTor
            ElseIf Member.IsGenericMethodDefinition Then
                Type = Objects.GenericMethodOpen
            ElseIf Member.IsGenericMethod Then
                Type = Objects.GenericMethodClosed
            Else
                Type = Objects.Method
            End If
            Return GetImage(Type, Member.Attributes)
        End Function

        ''' <summary>Gets image that graphically represents given property</summary>
        ''' <param name="Member">Property to get image for</param>
        ''' <returns>Image that graphically represents <paramref name="Member"/></returns>
        ''' <remarks><seealso cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects)"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and.</exception>
        <Extension()> Public Function GetImage(ByVal Member As PropertyInfo) As Image
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Dim Type As Objects
            Dim Overlay As ObjectModifiers = Member.GetAccessibility
            If Not Member.CanRead Then
                Type = Objects.Setter
            ElseIf Not Member.CanWrite Then
                Type = Objects.Getter
            Else
                Type = Objects.Property
            End If
            If Member.IsStatic Then Overlay = Overlay Or ObjectModifiers.Static
            If Member.IsFinal Then Overlay = Overlay Or ObjectModifiers.Sealed
            Return GetImage(Type, Overlay)
        End Function

        ''' <summary>Gets image that graphicaly represents given event</summary>
        ''' <param name="Member">Event to get image for</param>
        ''' <returns>16×16px image that graphicaly represents <paramref name="Member"/></returns>
        ''' <remarks><seealso cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects)"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and.</exception>
        <Extension()> Public Function GetImage(ByVal Member As EventInfo) As Image
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Dim Overlay As ObjectModifiers = Member.GetAccessibility
            If Member.IsFamily Then Overlay = Overlay Or ObjectModifiers.Sealed
            If Member.IsStatic Then Overlay = Overlay Or ObjectModifiers.Static
            Return GetImage(Objects.Event, Overlay)
        End Function

        ''' <summary>gets image that graphicaly represents given field</summary>
        ''' <param name="Member">Field to get image for</param>
        ''' <returns>16×16px image that graphiocaly represents <paramref name="Member"/></returns>
        ''' <remarks><seealso cref="M:Tools.ReflectionT.CodeImages.GetImage(Tools.ReflectionT.CodeImages.Objects)"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        <Extension()> Public Function GetImage(ByVal Member As FieldInfo) As Image
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Dim Type As Objects
            If Member.IsLiteral AndAlso Member.DeclaringType IsNot Nothing AndAlso Member.DeclaringType.IsEnum AndAlso Member.MemberType.Equals([Enum].GetUnderlyingType(Member.DeclaringType)) Then
                Type = Objects.EnumItem
            ElseIf Member.IsLiteral Then
                Type = Objects.Constant
            Else
                Type = Objects.Field
            End If
            Dim Overlay As ObjectModifiers
            Select Case Member.Attributes And FieldAttributes.FieldAccessMask
                Case FieldAttributes.Public : Overlay = ObjectModifiers.Public
                Case FieldAttributes.Family : Overlay = ObjectModifiers.Protected
                Case FieldAttributes.Assembly : Overlay = ObjectModifiers.Friend
                Case FieldAttributes.FamANDAssem : Overlay = ObjectModifiers.FriendProtected
                Case FieldAttributes.FamORAssem : Overlay = ObjectModifiers.ProtectedFriend
                Case FieldAttributes.Private : Overlay = ObjectModifiers.Private
            End Select
            If Member.IsStatic Then Overlay = Overlay Or ObjectModifiers.Static
            If Member.IsInitOnly Then Overlay = Overlay Or ObjectModifiers.Sealed
            Return GetImage(Type, Overlay)
        End Function

#End If
    End Module
End Namespace
#End If
