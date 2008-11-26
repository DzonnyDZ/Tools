Imports System.Reflection
#If Config <= Release Then
Namespace ResourcesT
    ''' <summary>Wraps internal class of .NET Framework <see cref="T:System.SR"/></summary>
    ''' <remarks>Functionality of this class strongly relays on implementation details of .NET Framework!!!</remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public NotInheritable Class SystemResources
        ''' <summary>CTor</summary>
        ''' <remarks>In order not to be possible to create instance</remarks>
        Private Sub New()
        End Sub
        ''' <summary>Gets <see cref="Type"/> that represents <see cref="T:System.SR"/> class</summary>
        Private Shared ReadOnly Property SRType() As Type
            Get
                Static t As Type
                If t Is Nothing Then
                    Dim a As Assembly = GetType(System.Uri).Assembly
                    t = a.GetType("System.SR", True)
                End If
                Return t
            End Get
        End Property
        ''' <summary>Gets names of all constants in <see cref="T:System.SR"/> class</summary>
        ''' <returns>Array that contains name of all public and private constants in <see cref="T:Syste.SR"/></returns>
        Public Shared ReadOnly Property KeyNames() As String()
            Get
                Dim Info As FieldInfo() = SRType.GetFields(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly Or BindingFlags.Static Or BindingFlags.GetField)
                Dim ret(Info.Length - 1) As String
                Dim minus As Integer = 0
                For i As Integer = 0 To Info.Length - 1
                    If Info(i).IsLiteral Then
                        ret(i - minus) = Info(i).Name
                    Else
                        minus += 1
                    End If
                Next i
                If minus > 0 Then
                    ReDim Preserve ret(ret.Length - 1 - minus)
                End If
                Return ret
            End Get
        End Property
        ''' <summary>Get value of specified field</summary>
        ''' <param name="Name">Name of constant defined in <see cref="T:System.SR"/>. All possible names are returned by the <see cref="KeyNames"/> property</param>
        ''' <exception cref="NullReferenceException">Field with name <paramref name="Name"/> is not accessible</exception>
        Public Shared ReadOnly Property Key(ByVal Name As String) As String
            Get
                Return SRType.GetField(Name, BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly Or BindingFlags.Static Or BindingFlags.GetField).GetValue(Nothing)
            End Get
        End Property
        ''' <summary>Gets string value of system resource</summary>
        ''' <returns>Value of given system resource or null when resource is not available.</returns>
        ''' <param name="Key">Key of resource. Possible keys are retruned by the <see cref="Key"/> property</param>
        Public Shared ReadOnly Property Value(ByVal key As KnownValues) As String
            Get
                Return Value(CStr(key))
            End Get
        End Property
        ''' <summary>Gets string value of system resource</summary>
        ''' <returns>Value of given system resource or null when resource is not available.</returns>
        ''' <param name="Key">Key of resource. Possible keys are retruned by the <see cref="Key"/> property</param>
        ''' <exception cref="TargetInvocationException">Unknown unexpected error when obtaining resource value</exception>
        Public Shared ReadOnly Property Value(ByVal Key As String) As String
            Get
                Try
                    Return SRType.GetMethod("GetObject", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.DeclaredOnly Or BindingFlags.InvokeMethod).Invoke(Nothing, New Object() {Key})
                Catch ex As Exception
                    Throw New TargetInvocationException(ex)
                End Try
            End Get
        End Property
        ''' <summary>Gets object value of system resource</summary>
        ''' <returns>Value of given system resource or null when resource is not available.</returns>
        ''' <param name="Key">Key of resource. Possible keys are retruned by the <see cref="Key"/> property</param>
        ''' <exception cref="TargetInvocationException">Unknown unexpected error when obtaining resource value</exception>
        Public Shared ReadOnly Property ObjValue(ByVal Key As String) As Object
            Get
                Try
                    Return SRType.GetMethod("GetObject", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.DeclaredOnly Or BindingFlags.InvokeMethod).Invoke(Nothing, New Object() {Key})
                Catch ex As Exception
                    Throw New TargetInvocationException(ex)
                End Try
            End Get
        End Property
        ''' <summary>Gets formated string value of system resource</summary>
        ''' <param name="Key">Key of resource. Possible keys are retruned by the <see cref="Key"/> property</param>
        ''' <param name="args">Formating arguments</param>
        ''' <returns>Formated value of given system resource or null when resource is not available.</returns>
        Public Shared ReadOnly Property Value(ByVal Key As String, ByVal ParamArray args As Object()) As String
            Get
                Dim Params As New List(Of Object)(args)
                Params.Insert(0, Key)
                Return SRType.GetMethod("GetObject", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.DeclaredOnly Or BindingFlags.InvokeMethod).Invoke(Nothing, Params.ToArray)
            End Get
        End Property

        ''' <summary>Contains values of known keys for the <see cref="Value"/> property</summary>
        ''' <completionlist cref="KnownValues"/>
        Public Structure KnownValues
            ''' <summary>Key value</summary>
            Private Value As String
            ''' <summary>Allows use of <see cref="KnownValues"/> class as <see cref="String"/> by converting it to <see cref="String"/></summary>
            ''' <param name="a">A <see cref="KnownValues"/> instance to convert</param>
            ''' <returns>Key value contained in <paramref name="a"/></returns>
            Public Shared Widening Operator CType(ByVal a As KnownValues) As String
                Return a.Value
            End Operator
            ''' <summary>Allows passings strings where <see cref="KnownValues"/> is expected</summary>
            ''' <param name="a"><see cref="String"/> to be converted</param>
            ''' <returns>Instance of <see cref="KnownValues"/> that contains <paramref name="a"/></returns>
            Public Shared Widening Operator CType(ByVal a As String) As KnownValues
                Dim ret As KnownValues
                ret.Value = a
                Return ret
            End Operator
#Region "Consts"
            ''' <summary>Key for resource getting something like "RTL_False"</summary>
            Public Const RTL$ = "RTL"
            ''' <summary>Key for resource getting something like "Cannot launch the debugger.  Make sure that a Microsoft (R) .NET Framework debugger is properly installed."</summary>
            Public Const DebugLaunchFailed$ = "DebugLaunchFailed"
            ''' <summary>Key for resource getting something like "Microsoft .NET Framework Debug Launch Failure"</summary>
            Public Const DebugLaunchFailedTitle$ = "DebugLaunchFailedTitle"
            ''' <summary>Key for resource getting something like "Assertion Failed: Abort=Quit, Retry=Debug, Ignore=Continue"</summary>
            Public Const DebugAssertTitle$ = "DebugAssertTitle"
            ''' <summary>Key for resource getting something like "---- DEBUG ASSERTION FAILED ----"</summary>
            Public Const DebugAssertBanner$ = "DebugAssertBanner"
            ''' <summary>Key for resource getting something like "---- Assert Short Message ----"</summary>
            Public Const DebugAssertShortMessage$ = "DebugAssertShortMessage"
            ''' <summary>Key for resource getting something like "---- Assert Long Message ----"</summary>
            Public Const DebugAssertLongMessage$ = "DebugAssertLongMessage"
            ''' <summary>Key for resource getting something like "{0}...&#13;&#10;&lt;truncated>"</summary>
            Public Const DebugMessageTruncated$ = "DebugMessageTruncated"
            ''' <summary>Key for resource getting something like "Object {0} has been disposed and can no longer be used."</summary>
            Public Const ObjectDisposed$ = "ObjectDisposed"
            ''' <summary>Key for resource getting something like "This operation is not supported."</summary>
            Public Const NotSupported$ = "NotSupported"
            ''' <summary>Key for resource getting something like "An exception occurred writing trace output to log file '{0}'. {1}"</summary>
            Public Const ExceptionOccurred$ = "ExceptionOccurred"
            ''' <summary>Key for resource getting something like "Only TraceListeners can be added to a TraceListenerCollection."</summary>
            Public Const MustAddListener$ = "MustAddListener"
            ''' <summary>Key for resource getting something like "(null)"</summary>
            Public Const ToStringNull$ = "ToStringNull"
            ''' <summary>Key for resource getting something like "The value '{0}' is not a valid value for the enum '{1}'."</summary>
            Public Const EnumConverterInvalidValue$ = "EnumConverterInvalidValue"
            ''' <summary>Key for resource getting something like "{0} cannot convert from {1}."</summary>
            Public Const ConvertFromException$ = "ConvertFromException"
            ''' <summary>Key for resource getting something like "'{0}' is unable to convert '{1}' to '{2}'."</summary>
            Public Const ConvertToException$ = "ConvertToException"
            ''' <summary>Key for resource getting something like "{0} is not a valid value for {1}."</summary>
            Public Const ConvertInvalidPrimitive$ = "ConvertInvalidPrimitive"
            ''' <summary>Key for resource getting something like "Accessor methods for the {0} property are missing."</summary>
            Public Const ErrorMissingPropertyAccessors$ = "ErrorMissingPropertyAccessors"
            ''' <summary>Key for resource getting something like "Invalid type for the {0} property."</summary>
            Public Const ErrorInvalidPropertyType$ = "ErrorInvalidPropertyType"
            ''' <summary>Key for resource getting something like "Accessor methods for the {0} event are missing."</summary>
            Public Const ErrorMissingEventAccessors$ = "ErrorMissingEventAccessors"
            ''' <summary>Key for resource getting something like "Invalid event handler for the {0} event."</summary>
            Public Const ErrorInvalidEventHandler$ = "ErrorInvalidEventHandler"
            ''' <summary>Key for resource getting something like "Invalid type for the {0} event."</summary>
            Public Const ErrorInvalidEventType$ = "ErrorInvalidEventType"
            ''' <summary>Key for resource getting something like "Invalid member name."</summary>
            Public Const InvalidMemberName$ = "InvalidMemberName"
            ''' <summary>Key for resource getting something like "The {0} extender provider is not compatible with the {1} type."</summary>
            Public Const ErrorBadExtenderType$ = "ErrorBadExtenderType"
            ''' <summary>Key for resource getting something like "The specified type is not a nullable type."</summary>
            Public Const NullableConverterBadCtorArg$ = "NullableConverterBadCtorArg"
            ''' <summary>Key for resource getting something like "Expected types in the collection to be of type {0}."</summary>
            Public Const TypeDescriptorExpectedElementType$ = "TypeDescriptorExpectedElementType"
            ''' <summary>Key for resource getting something like "Cannot create an association when the primary and secondary objects are the same."</summary>
            Public Const TypeDescriptorSameAssociation$ = "TypeDescriptorSameAssociation"
            ''' <summary>Key for resource getting something like "The primary and secondary objects are already associated with each other."</summary>
            Public Const TypeDescriptorAlreadyAssociated$ = "TypeDescriptorAlreadyAssociated"
            ''' <summary>Key for resource getting something like "The type description provider {0} has returned null from {1} which is illegal."</summary>
            Public Const TypeDescriptorProviderError$ = "TypeDescriptorProviderError"
            ''' <summary>Key for resource getting something like "The object {0} is being remoted by a proxy that does not support interface discovery.  This type of remoted object is not supported."</summary>
            Public Const TypeDescriptorUnsupportedRemoteObject$ = "TypeDescriptorUnsupportedRemoteObject"
            ''' <summary>Key for resource getting something like "The number of elements in the Type and Object arrays must match."</summary>
            Public Const TypeDescriptorArgsCountMismatch$ = "TypeDescriptorArgsCountMismatch"
            ''' <summary>Key for resource getting something like "Failed to create system events window thread."</summary>
            Public Const ErrorCreateSystemEvents$ = "ErrorCreateSystemEvents"
            ''' <summary>Key for resource getting something like "Cannot create timer."</summary>
            Public Const ErrorCreateTimer$ = "ErrorCreateTimer"
            ''' <summary>Key for resource getting something like "Cannot end timer."</summary>
            Public Const ErrorKillTimer$ = "ErrorKillTimer"
            ''' <summary>Key for resource getting something like "System event notifications are not supported under the current context. Server processes, for example, may not support global system event notifications."</summary>
            Public Const ErrorSystemEventsNotSupported$ = "ErrorSystemEventsNotSupported"
            ''' <summary>Key for resource getting something like "Cannot get temporary file name"</summary>
            Public Const ErrorGetTempPath$ = "ErrorGetTempPath"
            ''' <summary>Key for resource getting something like "The checkout was canceled by the user."</summary>
            Public Const CHECKOUTCanceled$ = "CHECKOUTCanceled"
            ''' <summary>Key for resource getting something like "The service instance must derive from or implement {0}."</summary>
            Public Const ErrorInvalidServiceInstance$ = "ErrorInvalidServiceInstance"
            ''' <summary>Key for resource getting something like "The service {0} already exists in the service container."</summary>
            Public Const ErrorServiceExists$ = "ErrorServiceExists"
            ''' <summary>Key for resource getting something like "Key cannot be null."</summary>
            Public Const ArgumentNull_Key$ = "ArgumentNull_Key"
            ''' <summary>Key for resource getting something like "An entry with the same key already exists."</summary>
            Public Const Argument_AddingDuplicate$ = "Argument_AddingDuplicate"
            ''' <summary>Key for resource getting something like "Argument {0} should be larger than {1}."</summary>
            Public Const Argument_InvalidValue$ = "Argument_InvalidValue"
            ''' <summary>Key for resource getting something like "Index is less than zero."</summary>
            Public Const ArgumentOutOfRange_NeedNonNegNum$ = "ArgumentOutOfRange_NeedNonNegNum"
            ''' <summary>Key for resource getting something like "The specified threshold for creating dictionary is out of range."</summary>
            Public Const ArgumentOutOfRange_InvalidThreshold$ = "ArgumentOutOfRange_InvalidThreshold"
            ''' <summary>Key for resource getting something like "Collection was modified after the enumerator was instantiated."</summary>
            Public Const InvalidOperation_EnumFailedVersion$ = "InvalidOperation_EnumFailedVersion"
            ''' <summary>Key for resource getting something like "Enumerator is positioned before the first element or after the last element of the collection."</summary>
            Public Const InvalidOperation_EnumOpCantHappen$ = "InvalidOperation_EnumOpCantHappen"
            ''' <summary>Key for resource getting something like "Multi dimension array is not supported on this operation."</summary>
            Public Const Arg_MultiRank$ = "Arg_MultiRank"
            ''' <summary>Key for resource getting something like "The lower bound of target array must be zero."</summary>
            Public Const Arg_NonZeroLowerBound$ = "Arg_NonZeroLowerBound"
            ''' <summary>Key for resource getting something like "Insufficient space in the target location to copy the information."</summary>
            Public Const Arg_InsufficientSpace$ = "Arg_InsufficientSpace"
            ''' <summary>Key for resource getting something like "Reset is not supported on the Enumerator."</summary>
            Public Const NotSupported_EnumeratorReset$ = "NotSupported_EnumeratorReset"
            ''' <summary>Key for resource getting something like "Target array type is not compatible with the type of items in the collection."</summary>
            Public Const Invalid_Array_Type$ = "Invalid_Array_Type"
            ''' <summary>Key for resource getting something like "OnDeserialization method was called while the object was not being deserialized."</summary>
            Public Const Serialization_InvalidOnDeser$ = "Serialization_InvalidOnDeser"
            ''' <summary>Key for resource getting something like "The values for this collection are missing."</summary>
            Public Const Serialization_MissingValues$ = "Serialization_MissingValues"
            ''' <summary>Key for resource getting something like "The serialized Count information doesn't match the number of items."</summary>
            Public Const Serialization_MismatchedCount$ = "Serialization_MismatchedCount"
            ''' <summary>Key for resource getting something like "The LinkedList node does not belong to current LinkedList."</summary>
            Public Const ExternalLinkedListNode$ = "ExternalLinkedListNode"
            ''' <summary>Key for resource getting something like "The LinkedList node belongs a LinkedList."</summary>
            Public Const LinkedListNodeIsAttached$ = "LinkedListNodeIsAttached"
            ''' <summary>Key for resource getting something like "The LinkedList is empty."</summary>
            Public Const LinkedListEmpty$ = "LinkedListEmpty"
            ''' <summary>Key for resource getting something like "The value "{0}" isn't of type "{1}" and can't be used in this generic collection."</summary>
            Public Const Arg_WrongType$ = "Arg_WrongType"
            ''' <summary>Key for resource getting something like "The specified item does not exist in this KeyedCollection."</summary>
            Public Const Argument_ItemNotExist$ = "Argument_ItemNotExist"
            ''' <summary>Key for resource getting something like "At least one object must implement IComparable."</summary>
            Public Const Argument_ImplementIComparable$ = "Argument_ImplementIComparable"
            ''' <summary>Key for resource getting something like "This operation is not valid on an empty collection."</summary>
            Public Const InvalidOperation_EmptyCollection$ = "InvalidOperation_EmptyCollection"
            ''' <summary>Key for resource getting something like "Queue empty."</summary>
            Public Const InvalidOperation_EmptyQueue$ = "InvalidOperation_EmptyQueue"
            ''' <summary>Key for resource getting something like "Stack empty."</summary>
            Public Const InvalidOperation_EmptyStack$ = "InvalidOperation_EmptyStack"
            ''' <summary>Key for resource getting something like "Removal is an invalid operation for Stack or Queue."</summary>
            Public Const InvalidOperation_CannotRemoveFromStackOrQueue$ = "InvalidOperation_CannotRemoveFromStackOrQueue"
            ''' <summary>Key for resource getting something like "Index was out of range. Must be non-negative and less than the size of the collection."</summary>
            Public Const ArgumentOutOfRange_Index$ = "ArgumentOutOfRange_Index"
            ''' <summary>Key for resource getting something like "capacity was less than the current size."</summary>
            Public Const ArgumentOutOfRange_SmallCapacity$ = "ArgumentOutOfRange_SmallCapacity"
            ''' <summary>Key for resource getting something like "Destination array is not long enough to copy all the items in the collection. Check array index and length."</summary>
            Public Const Arg_ArrayPlusOffTooSmall$ = "Arg_ArrayPlusOffTooSmall"
            ''' <summary>Key for resource getting something like "Mutating a key collection derived from a dictionary is not allowed."</summary>
            Public Const NotSupported_KeyCollectionSet$ = "NotSupported_KeyCollectionSet"
            ''' <summary>Key for resource getting something like "Mutating a value collection derived from a dictionary is not allowed."</summary>
            Public Const NotSupported_ValueCollectionSet$ = "NotSupported_ValueCollectionSet"
            ''' <summary>Key for resource getting something like "Collection is read-only."</summary>
            Public Const NotSupported_ReadOnlyCollection$ = "NotSupported_ReadOnlyCollection"
            ''' <summary>Key for resource getting something like "This operation is not supported on SortedList nested types because they require modifying the original SortedList."</summary>
            Public Const NotSupported_SortedListNestedWrite$ = "NotSupported_SortedListNestedWrite"
            ''' <summary>Key for resource getting something like "Once a ListSortDescriptionCollection has been created it can't be modified."</summary>
            Public Const CantModifyListSortDescriptionCollection$ = "CantModifyListSortDescriptionCollection"
            ''' <summary>Key for resource getting something like "Invalid Primitive Type: {0}. Consider using CodeObjectCreateExpression."</summary>
            Public Const InvalidPrimitiveType$ = "InvalidPrimitiveType"
            ''' <summary>Key for resource getting something like "The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class."</summary>
            Public Const CodeGenOutputWriter$ = "CodeGenOutputWriter"
            ''' <summary>Key for resource getting something like "This code generation API cannot be called while the generator is being used to generate something else."</summary>
            Public Const CodeGenReentrance$ = "CodeGenReentrance"
            ''' <summary>Key for resource getting something like "The identifier:"{0}" on the property:"{1}" of type:"{2}" is not a valid language-independent identifier name. Check to see if CodeGenerator.IsValidLanguageIndependentIdentifier allows the identifier name."</summary>
            Public Const InvalidLanguageIdentifier$ = "InvalidLanguageIdentifier"
            ''' <summary>Key for resource getting something like "The type name:"{0}" on the property:"{1}" of type:"{2}" is not a valid language-independent type name."</summary>
            Public Const InvalidTypeName$ = "InvalidTypeName"
            ''' <summary>Key for resource getting something like "The '{0}' attribute cannot be an empty string."</summary>
            Public Const Empty_attribute$ = "Empty_attribute"
            ''' <summary>Key for resource getting something like "The '{0}' attribute must be a non-negative integer."</summary>
            Public Const Invalid_nonnegative_integer_attribute$ = "Invalid_nonnegative_integer_attribute"
            ''' <summary>Key for resource getting something like "There is no CodeDom provider defined for the language."</summary>
            Public Const CodeDomProvider_NotDefined$ = "CodeDomProvider_NotDefined"
            ''' <summary>Key for resource getting something like "You need to specify a non-empty String for a language name in the CodeDom configuration section."</summary>
            Public Const Language_Names_Cannot_Be_Empty$ = "Language_Names_Cannot_Be_Empty"
            ''' <summary>Key for resource getting something like "An extension name in the CodeDom configuration section must be a non-empty string which starts with a period."</summary>
            Public Const Extension_Names_Cannot_Be_Empty_Or_Non_Period_Based$ = "Extension_Names_Cannot_Be_Empty_Or_Non_Period_Based"
            ''' <summary>Key for resource getting something like "The CodeDom provider type "{0}" could not be located."</summary>
            Public Const Unable_To_Locate_Type$ = "Unable_To_Locate_Type"
            ''' <summary>Key for resource getting something like "This CodeDomProvider does not support this method."</summary>
            Public Const NotSupported_CodeDomAPI$ = "NotSupported_CodeDomAPI"
            ''' <summary>Key for resource getting something like "The total arity specified in '{0}' does not match the number of TypeArguments supplied.  There were '{1}' TypeArguments supplied. "</summary>
            Public Const ArityDoesntMatch$ = "ArityDoesntMatch"
            ''' <summary>Key for resource getting something like "&lt;The original value of this property potentially contains file system information and has been suppressed.>"</summary>
            Public Const PartialTrustErrorTextReplacement$ = "PartialTrustErrorTextReplacement"
            ''' <summary>Key for resource getting something like "When used in partial trust, langID must be C#, VB, J#, or JScript, and the language provider must be in the global assembly cache."</summary>
            Public Const PartialTrustIllegalProvider$ = "PartialTrustIllegalProvider"
            ''' <summary>Key for resource getting something like "Assembly references cannot begin with '-', or contain a '/' or '\'."</summary>
            Public Const IllegalAssemblyReference$ = "IllegalAssemblyReference"
            ''' <summary>Key for resource getting something like "auto-generated>"</summary>
            Public Const AutoGen_Comment_Line1$ = "AutoGen_Comment_Line1"
            ''' <summary>Key for resource getting something like "This code was generated by a tool."</summary>
            Public Const AutoGen_Comment_Line2$ = "AutoGen_Comment_Line2"
            ''' <summary>Key for resource getting something like "Runtime Version:"</summary>
            Public Const AutoGen_Comment_Line3$ = "AutoGen_Comment_Line3"
            ''' <summary>Key for resource getting something like "Changes to this file may cause incorrect behavior and will be lost if"</summary>
            Public Const AutoGen_Comment_Line4$ = "AutoGen_Comment_Line4"
            ''' <summary>Key for resource getting something like "the code is regenerated."</summary>
            Public Const AutoGen_Comment_Line5$ = "AutoGen_Comment_Line5"
            ''' <summary>Key for resource getting something like "Array '{0}' cannot contain null entries."</summary>
            Public Const CantContainNullEntries$ = "CantContainNullEntries"
            ''' <summary>Key for resource getting something like "The CodeChecksumPragma file name '{0}' contains invalid path characters. "</summary>
            Public Const InvalidPathCharsInChecksum$ = "InvalidPathCharsInChecksum"
            ''' <summary>Key for resource getting something like "The region directive '{0}' contains invalid characters.  RegionText cannot contain any new line characters. "</summary>
            Public Const InvalidRegion$ = "InvalidRegion"
            ''' <summary>Key for resource getting something like "{0} on {1}"</summary>
            Public Const MetaExtenderName$ = "MetaExtenderName"
            ''' <summary>Key for resource getting something like "The value of argument '{0}' ({1}) is invalid for Enum type '{2}'."</summary>
            Public Const InvalidEnumArgument$ = "InvalidEnumArgument"
            ''' <summary>Key for resource getting something like "'{1}' is not a valid value for '{0}'."</summary>
            Public Const InvalidArgument$ = "InvalidArgument"
            ''' <summary>Key for resource getting something like "Null is not a valid value for {0}."</summary>
            Public Const InvalidNullArgument$ = "InvalidNullArgument"
            ''' <summary>Key for resource getting something like "Argument {0} cannot be null or zero-length."</summary>
            Public Const InvalidNullEmptyArgument$ = "InvalidNullEmptyArgument"
            ''' <summary>Key for resource getting something like "A valid license cannot be granted for the type {0}. Contact the manufacturer of the component for more information."</summary>
            Public Const LicExceptionTypeOnly$ = "LicExceptionTypeOnly"
            ''' <summary>Key for resource getting something like "An instance of type '{1}' was being created, and a valid license could not be granted for the type '{0}'. Please,  contact the manufacturer of the component for more information."</summary>
            Public Const LicExceptionTypeAndInstance$ = "LicExceptionTypeAndInstance"
            ''' <summary>Key for resource getting something like "The CurrentContext property of the LicenseManager is currently locked and cannot be changed."</summary>
            Public Const LicMgrContextCannotBeChanged$ = "LicMgrContextCannotBeChanged"
            ''' <summary>Key for resource getting something like "The CurrentContext property of the LicenseManager is already locked by another user."</summary>
            Public Const LicMgrAlreadyLocked$ = "LicMgrAlreadyLocked"
            ''' <summary>Key for resource getting something like "The CurrentContext property of the LicenseManager can only be unlocked with the same contextUser."</summary>
            Public Const LicMgrDifferentUser$ = "LicMgrDifferentUser"
            ''' <summary>Key for resource getting something like "Element type {0} is not supported."</summary>
            Public Const InvalidElementType$ = "InvalidElementType"
            ''' <summary>Key for resource getting something like "Identifier '{0}' is not valid."</summary>
            Public Const InvalidIdentifier$ = "InvalidIdentifier"
            ''' <summary>Key for resource getting something like "Failed to create file {0}."</summary>
            Public Const ExecFailedToCreate$ = "ExecFailedToCreate"
            ''' <summary>Key for resource getting something like "Timed out waiting for a program to execute. The command being executed was {0}."</summary>
            Public Const ExecTimeout$ = "ExecTimeout"
            ''' <summary>Key for resource getting something like "An invalid return code was encountered waiting for a program to execute. The command being executed was {0}."</summary>
            Public Const ExecBadreturn$ = "ExecBadreturn"
            ''' <summary>Key for resource getting something like "Unable to get the return code for a program being executed. The command that was being executed was '{0}'."</summary>
            Public Const ExecCantGetRetCode$ = "ExecCantGetRetCode"
            ''' <summary>Key for resource getting something like "Cannot execute a program. The command being executed was {0}."</summary>
            Public Const ExecCantExec$ = "ExecCantExec"
            ''' <summary>Key for resource getting something like "Cannot execute a program. Impersonation failed."</summary>
            Public Const ExecCantRevert$ = "ExecCantRevert"
            ''' <summary>Key for resource getting something like "Compiler executable file {0} cannot be found."</summary>
            Public Const CompilerNotFound$ = "CompilerNotFound"
            ''' <summary>Key for resource getting something like "The file name '{0}' was already in the collection."</summary>
            Public Const DuplicateFileName$ = "DuplicateFileName"
            ''' <summary>Key for resource getting something like "Collection is read-only."</summary>
            Public Const CollectionReadOnly$ = "CollectionReadOnly"
            ''' <summary>Key for resource getting something like "Bit vector is full."</summary>
            Public Const BitVectorFull$ = "BitVectorFull"
            ''' <summary>Key for resource getting something like "Specifies support for transacted initialization. "</summary>
            Public Const ISupportInitializeDescr$ = "ISupportInitializeDescr"
            ''' <summary>Key for resource getting something like "{0} Array"</summary>
            Public Const ArrayConverterText$ = "ArrayConverterText"
            ''' <summary>Key for resource getting something like "(Collection)"</summary>
            Public Const CollectionConverterText$ = "CollectionConverterText"
            ''' <summary>Key for resource getting something like "(Text)"</summary>
            Public Const MultilineStringConverterText$ = "MultilineStringConverterText"
            ''' <summary>Key for resource getting something like "(Default)"</summary>
            Public Const CultureInfoConverterDefaultCultureString$ = "CultureInfoConverterDefaultCultureString"
            ''' <summary>Key for resource getting something like "The {0} culture cannot be converted to a CultureInfo object on this computer."</summary>
            Public Const CultureInfoConverterInvalidCulture$ = "CultureInfoConverterInvalidCulture"
            ''' <summary>Key for resource getting something like "The text {0} is not a valid {1}."</summary>
            Public Const InvalidPrimitive$ = "InvalidPrimitive"
            ''' <summary>Key for resource getting something like "'{0}' is not a valid value for 'Interval'. 'Interval' must be greater than {1}."</summary>
            Public Const TimerInvalidInterval$ = "TimerInvalidInterval"
            ''' <summary>Key for resource getting something like "Attempted to set {0} to a value that is too high.  Setting level to TraceLevel.Verbose"</summary>
            Public Const TraceSwitchLevelTooHigh$ = "TraceSwitchLevelTooHigh"
            ''' <summary>Key for resource getting something like "Attempted to set {0} to a value that is too low.  Setting level to TraceLevel.Off"</summary>
            Public Const TraceSwitchLevelTooLow$ = "TraceSwitchLevelTooLow"
            ''' <summary>Key for resource getting something like "The Level must be set to a value in the enumeration TraceLevel."</summary>
            Public Const TraceSwitchInvalidLevel$ = "TraceSwitchInvalidLevel"
            ''' <summary>Key for resource getting something like "The IndentSize property must be non-negative."</summary>
            Public Const TraceListenerIndentSize$ = "TraceListenerIndentSize"
            ''' <summary>Key for resource getting something like "Fail:"</summary>
            Public Const TraceListenerFail$ = "TraceListenerFail"
            ''' <summary>Key for resource getting something like "Trace"</summary>
            Public Const TraceAsTraceSource$ = "TraceAsTraceSource"
            ''' <summary>Key for resource getting something like "'{1}' is not a valid value for '{0}'. '{0}' must be greater than {2}."</summary>
            Public Const InvalidLowBoundArgument$ = "InvalidLowBoundArgument"
            ''' <summary>Key for resource getting something like "Duplicate component name '{0}'.  Component names must be unique and case-insensitive."</summary>
            Public Const DuplicateComponentName$ = "DuplicateComponentName"
            ''' <summary>Key for resource getting something like "{0}: Not implemented"</summary>
            Public Const NotImplemented$ = "NotImplemented"
            ''' <summary>Key for resource getting something like "Could not allocate needed memory."</summary>
            Public Const OutOfMemory$ = "OutOfMemory"
            ''' <summary>Key for resource getting something like "End of data stream encountered."</summary>
            Public Const EOF$ = "EOF"
            ''' <summary>Key for resource getting something like "Unknown input/output failure."</summary>
            Public Const IOError$ = "IOError"
            ''' <summary>Key for resource getting something like "Unexpected Character: '{0}'."</summary>
            Public Const BadChar$ = "BadChar"
            ''' <summary>Key for resource getting something like "(none)"</summary>
            Public Const toStringNone$ = "toStringNone"
            ''' <summary>Key for resource getting something like "(unknown)"</summary>
            Public Const toStringUnknown$ = "toStringUnknown"
            ''' <summary>Key for resource getting something like "{0} is not a valid {1} value."</summary>
            Public Const InvalidEnum$ = "InvalidEnum"
            ''' <summary>Key for resource getting something like "Index {0} is out of range."</summary>
            Public Const IndexOutOfRange$ = "IndexOutOfRange"
            ''' <summary>Key for resource getting something like "Property accessor '{0}' on object '{1}' threw the following exception:'{2}'"</summary>
            Public Const ErrorPropertyAccessorException$ = "ErrorPropertyAccessorException"
            ''' <summary>Key for resource getting something like "Invalid operation."</summary>
            Public Const InvalidOperation$ = "InvalidOperation"
            ''' <summary>Key for resource getting something like "Stack has no items in it."</summary>
            Public Const EmptyStack$ = "EmptyStack"
            ''' <summary>Key for resource getting something like "Represents a Windows performance counter component."</summary>
            Public Const PerformanceCounterDesc$ = "PerformanceCounterDesc"
            ''' <summary>Key for resource getting something like "Category name of the performance counter object."</summary>
            Public Const PCCategoryName$ = "PCCategoryName"
            ''' <summary>Key for resource getting something like "Counter name of the performance counter object."</summary>
            Public Const PCCounterName$ = "PCCounterName"
            ''' <summary>Key for resource getting something like "Instance name of the performance counter object."</summary>
            Public Const PCInstanceName$ = "PCInstanceName"
            ''' <summary>Key for resource getting something like "Specifies the machine from where to read the performance data."</summary>
            Public Const PCMachineName$ = "PCMachineName"
            ''' <summary>Key for resource getting something like "Specifies the lifetime of the instance."</summary>
            Public Const PCInstanceLifetime$ = "PCInstanceLifetime"
            ''' <summary>Key for resource getting something like "Action"</summary>
            Public Const PropertyCategoryAction$ = "PropertyCategoryAction"
            ''' <summary>Key for resource getting something like "Appearance"</summary>
            Public Const PropertyCategoryAppearance$ = "PropertyCategoryAppearance"
            ''' <summary>Key for resource getting something like "Asynchronous"</summary>
            Public Const PropertyCategoryAsynchronous$ = "PropertyCategoryAsynchronous"
            ''' <summary>Key for resource getting something like "Behavior"</summary>
            Public Const PropertyCategoryBehavior$ = "PropertyCategoryBehavior"
            ''' <summary>Key for resource getting something like "Data"</summary>
            Public Const PropertyCategoryData$ = "PropertyCategoryData"
            ''' <summary>Key for resource getting something like "DDE"</summary>
            Public Const PropertyCategoryDDE$ = "PropertyCategoryDDE"
            ''' <summary>Key for resource getting something like "Design"</summary>
            Public Const PropertyCategoryDesign$ = "PropertyCategoryDesign"
            ''' <summary>Key for resource getting something like "Drag Drop"</summary>
            Public Const PropertyCategoryDragDrop$ = "PropertyCategoryDragDrop"
            ''' <summary>Key for resource getting something like "Focus"</summary>
            Public Const PropertyCategoryFocus$ = "PropertyCategoryFocus"
            ''' <summary>Key for resource getting something like "Font"</summary>
            Public Const PropertyCategoryFont$ = "PropertyCategoryFont"
            ''' <summary>Key for resource getting something like "Format"</summary>
            Public Const PropertyCategoryFormat$ = "PropertyCategoryFormat"
            ''' <summary>Key for resource getting something like "Key"</summary>
            Public Const PropertyCategoryKey$ = "PropertyCategoryKey"
            ''' <summary>Key for resource getting something like "List"</summary>
            Public Const PropertyCategoryList$ = "PropertyCategoryList"
            ''' <summary>Key for resource getting something like "Layout"</summary>
            Public Const PropertyCategoryLayout$ = "PropertyCategoryLayout"
            ''' <summary>Key for resource getting something like "Misc"</summary>
            Public Const PropertyCategoryDefault$ = "PropertyCategoryDefault"
            ''' <summary>Key for resource getting something like "Mouse"</summary>
            Public Const PropertyCategoryMouse$ = "PropertyCategoryMouse"
            ''' <summary>Key for resource getting something like "Position"</summary>
            Public Const PropertyCategoryPosition$ = "PropertyCategoryPosition"
            ''' <summary>Key for resource getting something like "Text"</summary>
            Public Const PropertyCategoryText$ = "PropertyCategoryText"
            ''' <summary>Key for resource getting something like "Scale"</summary>
            Public Const PropertyCategoryScale$ = "PropertyCategoryScale"
            ''' <summary>Key for resource getting something like "Window Style"</summary>
            Public Const PropertyCategoryWindowStyle$ = "PropertyCategoryWindowStyle"
            ''' <summary>Key for resource getting something like "Configurations"</summary>
            Public Const PropertyCategoryConfig$ = "PropertyCategoryConfig"
            ''' <summary>Key for resource getting something like "This operation is only allowed once per object."</summary>
            Public Const OnlyAllowedOnce$ = "OnlyAllowedOnce"
            ''' <summary>Key for resource getting something like "Start index cannot be less than 0 or greater than input length."</summary>
            Public Const BeginIndexNotNegative$ = "BeginIndexNotNegative"
            ''' <summary>Key for resource getting something like "Length cannot be less than 0 or exceed input length."</summary>
            Public Const LengthNotNegative$ = "LengthNotNegative"
            ''' <summary>Key for resource getting something like "Unimplemented state."</summary>
            Public Const UnimplementedState$ = "UnimplementedState"
            ''' <summary>Key for resource getting something like "Unexpected opcode in regular expression generation: {0}."</summary>
            Public Const UnexpectedOpcode$ = "UnexpectedOpcode"
            ''' <summary>Key for resource getting something like "Result cannot be called on a failed Match."</summary>
            Public Const NoResultOnFailed$ = "NoResultOnFailed"
            ''' <summary>Key for resource getting something like "Unterminated [] set."</summary>
            Public Const UnterminatedBracket$ = "UnterminatedBracket"
            ''' <summary>Key for resource getting something like "Too many )'s."</summary>
            Public Const TooManyParens$ = "TooManyParens"
            ''' <summary>Key for resource getting something like "Nested quantifier {0}."</summary>
            Public Const NestedQuantify$ = "NestedQuantify"
            ''' <summary>Key for resource getting something like "Quantifier {x,y} following nothing."</summary>
            Public Const QuantifyAfterNothing$ = "QuantifyAfterNothing"
            ''' <summary>Key for resource getting something like "Internal error in ScanRegex."</summary>
            Public Const InternalError$ = "InternalError"
            ''' <summary>Key for resource getting something like "Illegal {x,y} with x > y."</summary>
            Public Const IllegalRange$ = "IllegalRange"
            ''' <summary>Key for resource getting something like "Not enough )'s."</summary>
            Public Const NotEnoughParens$ = "NotEnoughParens"
            ''' <summary>Key for resource getting something like "Cannot include class \{0} in character range."</summary>
            Public Const BadClassInCharRange$ = "BadClassInCharRange"
            ''' <summary>Key for resource getting something like "[x-y] range in reverse order."</summary>
            Public Const ReversedCharRange$ = "ReversedCharRange"
            ''' <summary>Key for resource getting something like "(?({0}) ) reference to undefined group."</summary>
            Public Const UndefinedReference$ = "UndefinedReference"
            ''' <summary>Key for resource getting something like "(?({0}) ) malformed."</summary>
            Public Const MalformedReference$ = "MalformedReference"
            ''' <summary>Key for resource getting something like "Unrecognized grouping construct."</summary>
            Public Const UnrecognizedGrouping$ = "UnrecognizedGrouping"
            ''' <summary>Key for resource getting something like "Unterminated (?#...) comment."</summary>
            Public Const UnterminatedComment$ = "UnterminatedComment"
            ''' <summary>Key for resource getting something like "Illegal \ at end of pattern."</summary>
            Public Const IllegalEndEscape$ = "IllegalEndEscape"
            ''' <summary>Key for resource getting something like "Malformed \k&lt;...> named back reference."</summary>
            Public Const MalformedNameRef$ = "MalformedNameRef"
            ''' <summary>Key for resource getting something like "Reference to undefined group number {0}."</summary>
            Public Const UndefinedBackref$ = "UndefinedBackref"
            ''' <summary>Key for resource getting something like "Reference to undefined group name {0}."</summary>
            Public Const UndefinedNameRef$ = "UndefinedNameRef"
            ''' <summary>Key for resource getting something like "Insufficient hexadecimal digits."</summary>
            Public Const TooFewHex$ = "TooFewHex"
            ''' <summary>Key for resource getting something like "Missing control character."</summary>
            Public Const MissingControl$ = "MissingControl"
            ''' <summary>Key for resource getting something like "Unrecognized control character."</summary>
            Public Const UnrecognizedControl$ = "UnrecognizedControl"
            ''' <summary>Key for resource getting something like "Unrecognized escape sequence \{0}."</summary>
            Public Const UnrecognizedEscape$ = "UnrecognizedEscape"
            ''' <summary>Key for resource getting something like "Illegal conditional (?(...)) expression."</summary>
            Public Const IllegalCondition$ = "IllegalCondition"
            ''' <summary>Key for resource getting something like "Too many | in (?()|)."</summary>
            Public Const TooManyAlternates$ = "TooManyAlternates"
            ''' <summary>Key for resource getting something like "parsing "{0}" - {1}"</summary>
            Public Const MakeException$ = "MakeException"
            ''' <summary>Key for resource getting something like "Incomplete \p{X} character escape."</summary>
            Public Const IncompleteSlashP$ = "IncompleteSlashP"
            ''' <summary>Key for resource getting something like "Malformed \p{X} character escape."</summary>
            Public Const MalformedSlashP$ = "MalformedSlashP"
            ''' <summary>Key for resource getting something like "Invalid group name: Group names must begin with a word character."</summary>
            Public Const InvalidGroupName$ = "InvalidGroupName"
            ''' <summary>Key for resource getting something like "Capture number cannot be zero."</summary>
            Public Const CapnumNotZero$ = "CapnumNotZero"
            ''' <summary>Key for resource getting something like "Alternation conditions do not capture and cannot be named."</summary>
            Public Const AlternationCantCapture$ = "AlternationCantCapture"
            ''' <summary>Key for resource getting something like "Alternation conditions cannot be comments."</summary>
            Public Const AlternationCantHaveComment$ = "AlternationCantHaveComment"
            ''' <summary>Key for resource getting something like "Capture group numbers must be less than or equal to Int32.MaxValue."</summary>
            Public Const CaptureGroupOutOfRange$ = "CaptureGroupOutOfRange"
            ''' <summary>Key for resource getting something like "A subtraction must be the last element in a character class."</summary>
            Public Const SubtractionMustBeLast$ = "SubtractionMustBeLast"
            ''' <summary>Key for resource getting something like "Unknown property '{0}'."</summary>
            Public Const UnknownProperty$ = "UnknownProperty"
            ''' <summary>Key for resource getting something like "Replacement pattern error."</summary>
            Public Const ReplacementError$ = "ReplacementError"
            ''' <summary>Key for resource getting something like "Count cannot be less than -1."</summary>
            Public Const CountTooSmall$ = "CountTooSmall"
            ''' <summary>Key for resource getting something like "Enumeration has either not started or has already finished."</summary>
            Public Const EnumNotStarted$ = "EnumNotStarted"
            ''' <summary>Key for resource getting something like "The file is already open.  Call Close before trying to open the FileObject again."</summary>
            Public Const FileObject_AlreadyOpen$ = "FileObject_AlreadyOpen"
            ''' <summary>Key for resource getting something like "The FileObject is currently closed.  Try opening it."</summary>
            Public Const FileObject_Closed$ = "FileObject_Closed"
            ''' <summary>Key for resource getting something like "File information cannot be queried while open for writing."</summary>
            Public Const FileObject_NotWhileWriting$ = "FileObject_NotWhileWriting"
            ''' <summary>Key for resource getting something like "File information cannot be queried if the file does not exist."</summary>
            Public Const FileObject_FileDoesNotExist$ = "FileObject_FileDoesNotExist"
            ''' <summary>Key for resource getting something like "This operation can only be done when the FileObject is closed."</summary>
            Public Const FileObject_MustBeClosed$ = "FileObject_MustBeClosed"
            ''' <summary>Key for resource getting something like "You must specify a file name, not a relative or absolute path."</summary>
            Public Const FileObject_MustBeFileName$ = "FileObject_MustBeFileName"
            ''' <summary>Key for resource getting something like "FileObject's open mode wasn't set to a valid value.  This FileObject is corrupt."</summary>
            Public Const FileObject_InvalidInternalState$ = "FileObject_InvalidInternalState"
            ''' <summary>Key for resource getting something like "The path has not been set, or is an empty string.  Please ensure you specify some path."</summary>
            Public Const FileObject_PathNotSet$ = "FileObject_PathNotSet"
            ''' <summary>Key for resource getting something like "The file is currently open for reading.  Close the file and reopen it before attempting this."</summary>
            Public Const FileObject_Reading$ = "FileObject_Reading"
            ''' <summary>Key for resource getting something like "The file is currently open for writing.  Close the file and reopen it before attempting this."</summary>
            Public Const FileObject_Writing$ = "FileObject_Writing"
            ''' <summary>Key for resource getting something like "Enumerator is positioned before the first line or after the last line of the file."</summary>
            Public Const FileObject_InvalidEnumeration$ = "FileObject_InvalidEnumeration"
            ''' <summary>Key for resource getting something like "Reset is not supported on a FileLineEnumerator."</summary>
            Public Const FileObject_NoReset$ = "FileObject_NoReset"
            ''' <summary>Key for resource getting something like "You must specify a directory name, not a relative or absolute path."</summary>
            Public Const DirectoryObject_MustBeDirName$ = "DirectoryObject_MustBeDirName"
            ''' <summary>Key for resource getting something like "The fully qualified, or relative path to the directory you wish to read from. E.g., "c:\temp"."</summary>
            Public Const DirectoryObjectPathDescr$ = "DirectoryObjectPathDescr"
            ''' <summary>Key for resource getting something like "Determines whether the file will be parsed to see if it has a byte order mark indicating its encoding.  If it does, this will be used rather than the current specified encoding."</summary>
            Public Const FileObjectDetectEncodingDescr$ = "FileObjectDetectEncodingDescr"
            ''' <summary>Key for resource getting something like "The encoding to use when reading the file. UTF-8 is the default."</summary>
            Public Const FileObjectEncodingDescr$ = "FileObjectEncodingDescr"
            ''' <summary>Key for resource getting something like "The fully qualified, or relative path to the file you wish to read from. E.g., "myfile.txt"."</summary>
            Public Const FileObjectPathDescr$ = "FileObjectPathDescr"
            ''' <summary>Key for resource getting something like "Only single dimensional arrays are supported for the requested action."</summary>
            Public Const Arg_RankMultiDimNotSupported$ = "Arg_RankMultiDimNotSupported"
            ''' <summary>Key for resource getting something like "Illegal enum value: {0}."</summary>
            Public Const Arg_EnumIllegalVal$ = "Arg_EnumIllegalVal"
            ''' <summary>Key for resource getting something like "Non-negative number required."</summary>
            Public Const Arg_OutOfRange_NeedNonNegNum$ = "Arg_OutOfRange_NeedNonNegNum"
            ''' <summary>Key for resource getting something like "Invalid permission state."</summary>
            Public Const Argument_InvalidPermissionState$ = "Argument_InvalidPermissionState"
            ''' <summary>Key for resource getting something like "The OID value was invalid."</summary>
            Public Const Argument_InvalidOidValue$ = "Argument_InvalidOidValue"
            ''' <summary>Key for resource getting something like "Operation on type '{0}' attempted with target of incorrect type."</summary>
            Public Const Argument_WrongType$ = "Argument_WrongType"
            ''' <summary>Key for resource getting something like "String cannot be empty or null."</summary>
            Public Const Arg_EmptyOrNullString$ = "Arg_EmptyOrNullString"
            ''' <summary>Key for resource getting something like "Array cannot be empty or null."</summary>
            Public Const Arg_EmptyOrNullArray$ = "Arg_EmptyOrNullArray"
            ''' <summary>Key for resource getting something like "The value of "class" attribute is invalid."</summary>
            Public Const Argument_InvalidClassAttribute$ = "Argument_InvalidClassAttribute"
            ''' <summary>Key for resource getting something like "The value of "nameType" is invalid."</summary>
            Public Const Argument_InvalidNameType$ = "Argument_InvalidNameType"
            ''' <summary>Key for resource getting something like "Enumeration has not started.  Call MoveNext."</summary>
            Public Const InvalidOperation_EnumNotStarted$ = "InvalidOperation_EnumNotStarted"
            ''' <summary>Key for resource getting something like "Duplicate items are not allowed in the collection."</summary>
            Public Const InvalidOperation_DuplicateItemNotAllowed$ = "InvalidOperation_DuplicateItemNotAllowed"
            ''' <summary>Key for resource getting something like "The AsnEncodedData object does not have the same OID for the collection."</summary>
            Public Const Cryptography_Asn_MismatchedOidInCollection$ = "Cryptography_Asn_MismatchedOidInCollection"
            ''' <summary>Key for resource getting something like "Cannot create CMS enveloped for empty content."</summary>
            Public Const Cryptography_Cms_Envelope_Empty_Content$ = "Cryptography_Cms_Envelope_Empty_Content"
            ''' <summary>Key for resource getting something like "The recipient info type {0} is not valid."</summary>
            Public Const Cryptography_Cms_Invalid_Recipient_Info_Type$ = "Cryptography_Cms_Invalid_Recipient_Info_Type"
            ''' <summary>Key for resource getting something like "The subject identifier type {0} is not valid."</summary>
            Public Const Cryptography_Cms_Invalid_Subject_Identifier_Type$ = "Cryptography_Cms_Invalid_Subject_Identifier_Type"
            ''' <summary>Key for resource getting something like "The subject identifier type {0} does not match the value data type {1}."</summary>
            Public Const Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch$ = "Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"
            ''' <summary>Key for resource getting something like "The Date property is not available for none KID key agree recipient."</summary>
            Public Const Cryptography_Cms_Key_Agree_Date_Not_Available$ = "Cryptography_Cms_Key_Agree_Date_Not_Available"
            ''' <summary>Key for resource getting something like "The OtherKeyAttribute property is not available for none KID key agree recipient."</summary>
            Public Const Cryptography_Cms_Key_Agree_Other_Key_Attribute_Not_Available$ = "Cryptography_Cms_Key_Agree_Other_Key_Attribute_Not_Available"
            ''' <summary>Key for resource getting something like "The CMS message is not signed."</summary>
            Public Const Cryptography_Cms_MessageNotSigned$ = "Cryptography_Cms_MessageNotSigned"
            ''' <summary>Key for resource getting something like "The CMS message is not signed by NoSignature."</summary>
            Public Const Cryptography_Cms_MessageNotSignedByNoSignature$ = "Cryptography_Cms_MessageNotSignedByNoSignature"
            ''' <summary>Key for resource getting something like "The CMS message is not encrypted."</summary>
            Public Const Cryptography_Cms_MessageNotEncrypted$ = "Cryptography_Cms_MessageNotEncrypted"
            ''' <summary>Key for resource getting something like "The Cryptographic Message Standard (CMS) is not supported on this platform."</summary>
            Public Const Cryptography_Cms_Not_Supported$ = "Cryptography_Cms_Not_Supported"
            ''' <summary>Key for resource getting something like "The recipient certificate is not specified."</summary>
            Public Const Cryptography_Cms_RecipientCertificateNotFound$ = "Cryptography_Cms_RecipientCertificateNotFound"
            ''' <summary>Key for resource getting something like "Cannot create CMS signature for empty content."</summary>
            Public Const Cryptography_Cms_Sign_Empty_Content$ = "Cryptography_Cms_Sign_Empty_Content"
            ''' <summary>Key for resource getting something like "CmsSigner has to be the first signer with NoSignature."</summary>
            Public Const Cryptography_Cms_Sign_No_Signature_First_Signer$ = "Cryptography_Cms_Sign_No_Signature_First_Signer"
            ''' <summary>Key for resource getting something like "The length of the data should be a multiple of 16 bytes."</summary>
            Public Const Cryptography_DpApi_InvalidMemoryLength$ = "Cryptography_DpApi_InvalidMemoryLength"
            ''' <summary>Key for resource getting something like "{0} is an invalid handle."</summary>
            Public Const Cryptography_InvalidHandle$ = "Cryptography_InvalidHandle"
            ''' <summary>Key for resource getting something like "The chain context handle is invalid."</summary>
            Public Const Cryptography_InvalidContextHandle$ = "Cryptography_InvalidContextHandle"
            ''' <summary>Key for resource getting something like "The store handle is invalid."</summary>
            Public Const Cryptography_InvalidStoreHandle$ = "Cryptography_InvalidStoreHandle"
            ''' <summary>Key for resource getting something like "The OID value is invalid."</summary>
            Public Const Cryptography_Oid_InvalidValue$ = "Cryptography_Oid_InvalidValue"
            ''' <summary>Key for resource getting something like "The PKCS 9 attribute cannot be explicitly added to the collection."</summary>
            Public Const Cryptography_Pkcs9_ExplicitAddNotAllowed$ = "Cryptography_Pkcs9_ExplicitAddNotAllowed"
            ''' <summary>Key for resource getting something like "The OID does not represent a valid PKCS 9 attribute."</summary>
            Public Const Cryptography_Pkcs9_InvalidOid$ = "Cryptography_Pkcs9_InvalidOid"
            ''' <summary>Key for resource getting something like "Cannot add multiple PKCS 9 signing time attributes."</summary>
            Public Const Cryptography_Pkcs9_MultipleSigningTimeNotAllowed$ = "Cryptography_Pkcs9_MultipleSigningTimeNotAllowed"
            ''' <summary>Key for resource getting something like "The parameter should be a PKCS 9 attribute."</summary>
            Public Const Cryptography_Pkcs9_AttributeMismatch$ = "Cryptography_Pkcs9_AttributeMismatch"
            ''' <summary>Key for resource getting something like "Adding certificate with index '{0}' failed."</summary>
            Public Const Cryptography_X509_AddFailed$ = "Cryptography_X509_AddFailed"
            ''' <summary>Key for resource getting something like "Input data cannot be coded as a valid certificate."</summary>
            Public Const Cryptography_X509_BadEncoding$ = "Cryptography_X509_BadEncoding"
            ''' <summary>Key for resource getting something like "The certificate export operation failed."</summary>
            Public Const Cryptography_X509_ExportFailed$ = "Cryptography_X509_ExportFailed"
            ''' <summary>Key for resource getting something like "The parameter should be an X509Extension."</summary>
            Public Const Cryptography_X509_ExtensionMismatch$ = "Cryptography_X509_ExtensionMismatch"
            ''' <summary>Key for resource getting something like "Invalid find type."</summary>
            Public Const Cryptography_X509_InvalidFindType$ = "Cryptography_X509_InvalidFindType"
            ''' <summary>Key for resource getting something like "Invalid find value."</summary>
            Public Const Cryptography_X509_InvalidFindValue$ = "Cryptography_X509_InvalidFindValue"
            ''' <summary>Key for resource getting something like "Invalid encoding format."</summary>
            Public Const Cryptography_X509_InvalidEncodingFormat$ = "Cryptography_X509_InvalidEncodingFormat"
            ''' <summary>Key for resource getting something like "Invalid content type."</summary>
            Public Const Cryptography_X509_InvalidContentType$ = "Cryptography_X509_InvalidContentType"
            ''' <summary>Key for resource getting something like "The public key of the certificate does not match the value specified."</summary>
            Public Const Cryptography_X509_KeyMismatch$ = "Cryptography_X509_KeyMismatch"
            ''' <summary>Key for resource getting something like "Removing certificate with index '{0}' failed."</summary>
            Public Const Cryptography_X509_RemoveFailed$ = "Cryptography_X509_RemoveFailed"
            ''' <summary>Key for resource getting something like "The current session is not interactive."</summary>
            Public Const Environment_NotInteractive$ = "Environment_NotInteractive"
            ''' <summary>Key for resource getting something like "Only asymmetric keys that implement ICspAsymmetricAlgorithm are supported."</summary>
            Public Const NotSupported_InvalidKeyImpl$ = "NotSupported_InvalidKeyImpl"
            ''' <summary>Key for resource getting something like "The certificate key algorithm is not supported."</summary>
            Public Const NotSupported_KeyAlgorithm$ = "NotSupported_KeyAlgorithm"
            ''' <summary>Key for resource getting something like "This operation is only supported on Windows 2000, Windows XP, and higher."</summary>
            Public Const NotSupported_PlatformRequiresNT$ = "NotSupported_PlatformRequiresNT"
            ''' <summary>Key for resource getting something like "Stream does not support reading."</summary>
            Public Const NotSupported_UnreadableStream$ = "NotSupported_UnreadableStream"
            ''' <summary>Key for resource getting something like "The {0} value was invalid."</summary>
            Public Const Security_InvalidValue$ = "Security_InvalidValue"
            ''' <summary>Key for resource getting something like "Unknown error."</summary>
            Public Const Unknown_Error$ = "Unknown_Error"
            ''' <summary>Key for resource getting something like "A non-CLS Compliant Exception (i.e. an object that does not derive from System.Exception) was thrown."</summary>
            Public Const net_nonClsCompliantException$ = "net_nonClsCompliantException"
            ''' <summary>Key for resource getting something like "The '{0}' attribute cannot appear when '{1}' is present."</summary>
            Public Const net_illegalConfigWith$ = "net_illegalConfigWith"
            ''' <summary>Key for resource getting something like "The '{0}' attribute can only appear when '{1}' is present."</summary>
            Public Const net_illegalConfigWithout$ = "net_illegalConfigWithout"
            ''' <summary>Key for resource getting something like "The value of the date string in the header is invalid."</summary>
            Public Const net_baddate$ = "net_baddate"
            ''' <summary>Key for resource getting something like "This property cannot be set after writing has started."</summary>
            Public Const net_writestarted$ = "net_writestarted"
            ''' <summary>Key for resource getting something like "The Content-Length value must be greater than or equal to zero."</summary>
            Public Const net_clsmall$ = "net_clsmall"
            ''' <summary>Key for resource getting something like "This operation cannot be performed after the request has been submitted."</summary>
            Public Const net_reqsubmitted$ = "net_reqsubmitted"
            ''' <summary>Key for resource getting something like "This operation cannot be performed after the response has been submitted."</summary>
            Public Const net_rspsubmitted$ = "net_rspsubmitted"
            ''' <summary>Key for resource getting something like "The requested FTP command is not supported when using HTTP proxy."</summary>
            Public Const net_ftp_no_http_cmd$ = "net_ftp_no_http_cmd"
            ''' <summary>Key for resource getting something like "FTP Method names cannot be null or empty."</summary>
            Public Const net_ftp_invalid_method_name$ = "net_ftp_invalid_method_name"
            ''' <summary>Key for resource getting something like "The RenameTo filename cannot be null or empty."</summary>
            Public Const net_ftp_invalid_renameto$ = "net_ftp_invalid_renameto"
            ''' <summary>Key for resource getting something like "Default credentials are not supported on an FTP request."</summary>
            Public Const net_ftp_no_defaultcreds$ = "net_ftp_no_defaultcreds"
            ''' <summary>Key for resource getting something like "This type of FTP request does not return a response stream."</summary>
            Public Const net_ftpnoresponse$ = "net_ftpnoresponse"
            ''' <summary>Key for resource getting something like "The response string '{0}' has invalid format."</summary>
            Public Const net_ftp_response_invalid_format$ = "net_ftp_response_invalid_format"
            ''' <summary>Key for resource getting something like "Offsets are not supported when sending an FTP request over an HTTP proxy."</summary>
            Public Const net_ftp_no_offsetforhttp$ = "net_ftp_no_offsetforhttp"
            ''' <summary>Key for resource getting something like "The requested URI is invalid for this FTP command."</summary>
            Public Const net_ftp_invalid_uri$ = "net_ftp_invalid_uri"
            ''' <summary>Key for resource getting something like "The status response ({0}) is not expected in response to '{1}' command."</summary>
            Public Const net_ftp_invalid_status_response$ = "net_ftp_invalid_status_response"
            ''' <summary>Key for resource getting something like "The server failed the passive mode request with status response ({0})."</summary>
            Public Const net_ftp_server_failed_passive$ = "net_ftp_server_failed_passive"
            ''' <summary>Key for resource getting something like "The server returned an address in response to the PASV command that is different than the address to which the FTP connection was made."</summary>
            Public Const net_ftp_passive_address_different$ = "net_ftp_passive_address_different"
            ''' <summary>Key for resource getting something like "The data connection was made from an address that is different than the address to which the FTP connection was made."</summary>
            Public Const net_ftp_active_address_different$ = "net_ftp_active_address_different"
            ''' <summary>Key for resource getting something like "SSL cannot be enabled when using a proxy."</summary>
            Public Const net_ftp_proxy_does_not_support_ssl$ = "net_ftp_proxy_does_not_support_ssl"
            ''' <summary>Key for resource getting something like "The server returned the filename ({0}) which is not valid."</summary>
            Public Const net_ftp_invalid_response_filename$ = "net_ftp_invalid_response_filename"
            ''' <summary>Key for resource getting something like "This method is not supported."</summary>
            Public Const net_ftp_unsupported_method$ = "net_ftp_unsupported_method"
            ''' <summary>Key for resource getting something like "An error occurred on an automatic resubmission of the request."</summary>
            Public Const net_resubmitcanceled$ = "net_resubmitcanceled"
            ''' <summary>Key for resource getting something like "WebPermission demand failed for redirect URI."</summary>
            Public Const net_redirect_perm$ = "net_redirect_perm"
            ''' <summary>Key for resource getting something like "Cannot handle redirect from HTTP/HTTPS protocols to other dissimilar ones."</summary>
            Public Const net_resubmitprotofailed$ = "net_resubmitprotofailed"
            ''' <summary>Key for resource getting something like "TransferEncoding requires the SendChunked property to be set to true."</summary>
            Public Const net_needchunked$ = "net_needchunked"
            ''' <summary>Key for resource getting something like "Chunked encoding must be set via the SendChunked property."</summary>
            Public Const net_nochunked$ = "net_nochunked"
            ''' <summary>Key for resource getting something like "Chunked encoding upload is not supported on the HTTP/1.0 protocol."</summary>
            Public Const net_nochunkuploadonhttp10$ = "net_nochunkuploadonhttp10"
            ''' <summary>Key for resource getting something like "Keep-Alive and Close may not be set using this property."</summary>
            Public Const net_connarg$ = "net_connarg"
            ''' <summary>Key for resource getting something like "100-Continue may not be set using this property."</summary>
            Public Const net_no100$ = "net_no100"
            ''' <summary>Key for resource getting something like "The From parameter cannot be less than To."</summary>
            Public Const net_fromto$ = "net_fromto"
            ''' <summary>Key for resource getting something like "The From or To parameter cannot be less than 0."</summary>
            Public Const net_rangetoosmall$ = "net_rangetoosmall"
            ''' <summary>Key for resource getting something like "Bytes to be written to the stream exceed the Content-Length bytes size specified."</summary>
            Public Const net_entitytoobig$ = "net_entitytoobig"
            ''' <summary>Key for resource getting something like "This protocol version is not supported."</summary>
            Public Const net_invalidversion$ = "net_invalidversion"
            ''' <summary>Key for resource getting something like "The status code must be exactly three digits."</summary>
            Public Const net_invalidstatus$ = "net_invalidstatus"
            ''' <summary>Key for resource getting something like "The specified value must be greater than 0."</summary>
            Public Const net_toosmall$ = "net_toosmall"
            ''' <summary>Key for resource getting something like "The size of {0} is too long. It cannot be longer than {1} characters."</summary>
            Public Const net_toolong$ = "net_toolong"
            ''' <summary>Key for resource getting something like "The underlying connection was closed: {0}."</summary>
            Public Const net_connclosed$ = "net_connclosed"
            ''' <summary>Key for resource getting something like "This header must be modified using the appropriate property."</summary>
            Public Const net_headerrestrict$ = "net_headerrestrict"
            ''' <summary>Key for resource getting something like "The '{0}' header cannot be modified directly."</summary>
            Public Const net_headerrestrict_resp$ = "net_headerrestrict_resp"
            ''' <summary>Key for resource getting something like "This stream does not support seek operations."</summary>
            Public Const net_noseek$ = "net_noseek"
            ''' <summary>Key for resource getting something like "The remote server returned an error: {0}."</summary>
            Public Const net_servererror$ = "net_servererror"
            ''' <summary>Key for resource getting something like "Cannot send a content-body with this verb-type."</summary>
            Public Const net_nouploadonget$ = "net_nouploadonget"
            ''' <summary>Key for resource getting something like "The requirement for mutual authentication was not met by the remote server."</summary>
            Public Const net_mutualauthfailed$ = "net_mutualauthfailed"
            ''' <summary>Key for resource getting something like "Cannot block a call on this socket while an earlier asynchronous call is in progress."</summary>
            Public Const net_invasync$ = "net_invasync"
            ''' <summary>Key for resource getting something like "An asynchronous call is already in progress. It must be completed or canceled before you can call this method."</summary>
            Public Const net_inasync$ = "net_inasync"
            ''' <summary>Key for resource getting something like "The {0} parameter must represent a valid Uri (see inner exception)."</summary>
            Public Const net_mustbeuri$ = "net_mustbeuri"
            ''' <summary>Key for resource getting something like "The shell expression '{0}' could not be parsed because it is formatted incorrectly."</summary>
            Public Const net_format_shexp$ = "net_format_shexp"
            ''' <summary>Key for resource getting something like "Failed to load the proxy script runtime environment from the Microsoft.JScript assembly."</summary>
            Public Const net_cannot_load_proxy_helper$ = "net_cannot_load_proxy_helper"
            ''' <summary>Key for resource getting something like "Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."</summary>
            Public Const net_repcall$ = "net_repcall"
            ''' <summary>Key for resource getting something like "Only HTTP/1.0 and HTTP/1.1 version requests are currently supported."</summary>
            Public Const net_wrongversion$ = "net_wrongversion"
            ''' <summary>Key for resource getting something like "Cannot set null or blank methods on request."</summary>
            Public Const net_badmethod$ = "net_badmethod"
            ''' <summary>Key for resource getting something like "Cannot close stream until all bytes are written."</summary>
            Public Const net_io_notenoughbyteswritten$ = "net_io_notenoughbyteswritten"
            ''' <summary>Key for resource getting something like "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value >= 0."</summary>
            Public Const net_io_timeout_use_ge_zero$ = "net_io_timeout_use_ge_zero"
            ''' <summary>Key for resource getting something like "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0."</summary>
            Public Const net_io_timeout_use_gt_zero$ = "net_io_timeout_use_gt_zero"
            ''' <summary>Key for resource getting something like "NetworkStream does not support a 0 millisecond timeout, use a value greater than zero for the timeout instead."</summary>
            Public Const net_io_no_0timeouts$ = "net_io_no_0timeouts"
            ''' <summary>Key for resource getting something like "The request was aborted: {0}."</summary>
            Public Const net_requestaborted$ = "net_requestaborted"
            ''' <summary>Key for resource getting something like "Too many automatic redirections were attempted."</summary>
            Public Const net_tooManyRedirections$ = "net_tooManyRedirections"
            ''' <summary>Key for resource getting something like "The supplied authentication module is not registered."</summary>
            Public Const net_authmodulenotregistered$ = "net_authmodulenotregistered"
            ''' <summary>Key for resource getting something like "There is no registered module for this authentication scheme."</summary>
            Public Const net_authschemenotregistered$ = "net_authschemenotregistered"
            ''' <summary>Key for resource getting something like "The ServicePointManager does not support proxies with the {0} scheme."</summary>
            Public Const net_proxyschemenotsupported$ = "net_proxyschemenotsupported"
            ''' <summary>Key for resource getting something like "The maximum number of service points was exceeded."</summary>
            Public Const net_maxsrvpoints$ = "net_maxsrvpoints"
            ''' <summary>Key for resource getting something like "Reached maximum number of BindIPEndPointDelegate retries."</summary>
            Public Const net_maxbinddelegateretry$ = "net_maxbinddelegateretry"
            ''' <summary>Key for resource getting something like "The URI prefix is not recognized."</summary>
            Public Const net_unknown_prefix$ = "net_unknown_prefix"
            ''' <summary>Key for resource getting something like "The operation is not allowed on non-connected sockets."</summary>
            Public Const net_notconnected$ = "net_notconnected"
            ''' <summary>Key for resource getting something like "The operation is not allowed on non-stream oriented sockets."</summary>
            Public Const net_notstream$ = "net_notstream"
            ''' <summary>Key for resource getting something like "The operation has timed out."</summary>
            Public Const net_timeout$ = "net_timeout"
            ''' <summary>Key for resource getting something like "Content-Length cannot be set for an operation that does not write data."</summary>
            Public Const net_nocontentlengthonget$ = "net_nocontentlengthonget"
            ''' <summary>Key for resource getting something like "When performing a write operation with AllowWriteStreamBuffering set to false, you must either set ContentLength to a non-negative number or set SendChunked to true."</summary>
            Public Const net_contentlengthmissing$ = "net_contentlengthmissing"
            ''' <summary>Key for resource getting something like "The URI scheme for the supplied IWebProxy has the illegal value '{0}'. Only 'http' is supported."</summary>
            Public Const net_nonhttpproxynotallowed$ = "net_nonhttpproxynotallowed"
            ''' <summary>Key for resource getting something like "The supplied string is not a valid HTTP token."</summary>
            Public Const net_nottoken$ = "net_nottoken"
            ''' <summary>Key for resource getting something like "A different range specifier has already been added to this request."</summary>
            Public Const net_rangetype$ = "net_rangetype"
            ''' <summary>Key for resource getting something like "This request requires buffering data to succeed."</summary>
            Public Const net_need_writebuffering$ = "net_need_writebuffering"
            ''' <summary>Key for resource getting something like "The requested security package is not supported."</summary>
            Public Const net_securitypackagesupport$ = "net_securitypackagesupport"
            ''' <summary>Key for resource getting something like "The requested security protocol is not supported."</summary>
            Public Const net_securityprotocolnotsupported$ = "net_securityprotocolnotsupported"
            ''' <summary>Key for resource getting something like "Default credentials cannot be supplied for the {0} authentication scheme."</summary>
            Public Const net_nodefaultcreds$ = "net_nodefaultcreds"
            ''' <summary>Key for resource getting something like "Not listening. You must call the Start() method before calling this method."</summary>
            Public Const net_stopped$ = "net_stopped"
            ''' <summary>Key for resource getting something like "Cannot send packets to an arbitrary host while connected."</summary>
            Public Const net_udpconnected$ = "net_udpconnected"
            ''' <summary>Key for resource getting something like "The stream does not support writing."</summary>
            Public Const net_readonlystream$ = "net_readonlystream"
            ''' <summary>Key for resource getting something like "The stream does not support reading."</summary>
            Public Const net_writeonlystream$ = "net_writeonlystream"
            ''' <summary>Key for resource getting something like "The stream does not support concurrent IO read or write operations."</summary>
            Public Const net_no_concurrent_io_allowed$ = "net_no_concurrent_io_allowed"
            ''' <summary>Key for resource getting something like "There were not enough free threads in the ThreadPool to complete the operation."</summary>
            Public Const net_needmorethreads$ = "net_needmorethreads"
            ''' <summary>Key for resource getting something like "This method is not implemented by this class."</summary>
            Public Const net_MethodNotImplementedException$ = "net_MethodNotImplementedException"
            ''' <summary>Key for resource getting something like "This property is not implemented by this class."</summary>
            Public Const net_PropertyNotImplementedException$ = "net_PropertyNotImplementedException"
            ''' <summary>Key for resource getting something like "This method is not supported by this class."</summary>
            Public Const net_MethodNotSupportedException$ = "net_MethodNotSupportedException"
            ''' <summary>Key for resource getting something like "This property is not supported by this class."</summary>
            Public Const net_PropertyNotSupportedException$ = "net_PropertyNotSupportedException"
            ''' <summary>Key for resource getting something like "The '{0}' protocol is not supported by this class."</summary>
            Public Const net_ProtocolNotSupportedException$ = "net_ProtocolNotSupportedException"
            ''' <summary>Key for resource getting something like "The '{0}' hash algorithm not supported by this class."</summary>
            Public Const net_HashAlgorithmNotSupportedException$ = "net_HashAlgorithmNotSupportedException"
            ''' <summary>Key for resource getting something like "The '{0}' quality of service is not supported by this class."</summary>
            Public Const net_QOPNotSupportedException$ = "net_QOPNotSupportedException"
            ''' <summary>Key for resource getting something like "The '{0}' select mode is not supported by this class."</summary>
            Public Const net_SelectModeNotSupportedException$ = "net_SelectModeNotSupportedException"
            ''' <summary>Key for resource getting something like "The socket handle is not valid."</summary>
            Public Const net_InvalidSocketHandle$ = "net_InvalidSocketHandle"
            ''' <summary>Key for resource getting something like "The AddressFamily {0} is not valid for the {1} end point, use {2} instead."</summary>
            Public Const net_InvalidAddressFamily$ = "net_InvalidAddressFamily"
            ''' <summary>Key for resource getting something like "The supplied {0} is an invalid size for the {1} end point."</summary>
            Public Const net_InvalidSocketAddressSize$ = "net_InvalidSocketAddressSize"
            ''' <summary>Key for resource getting something like "None of the discovered or specified addresses match the socket address family."</summary>
            Public Const net_invalidAddressList$ = "net_invalidAddressList"
            ''' <summary>Key for resource getting something like "The buffer length must not exceed 65500 bytes."</summary>
            Public Const net_invalidPingBufferSize$ = "net_invalidPingBufferSize"
            ''' <summary>Key for resource getting something like "This operation cannot be performed while the AppDomain is shutting down."</summary>
            Public Const net_cant_perform_during_shutdown$ = "net_cant_perform_during_shutdown"
            ''' <summary>Key for resource getting something like "Unable to create another web proxy script environment at this time."</summary>
            Public Const net_cant_create_environment$ = "net_cant_create_environment"
            ''' <summary>Key for resource getting something like "This operation cannot be performed on a completed asynchronous result object."</summary>
            Public Const net_completed_result$ = "net_completed_result"
            ''' <summary>Key for resource getting something like "'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses."</summary>
            Public Const net_protocol_invalid_family$ = "net_protocol_invalid_family"
            ''' <summary>Key for resource getting something like "Multicast family is not the same as the family of the '{0}' Client."</summary>
            Public Const net_protocol_invalid_multicast_family$ = "net_protocol_invalid_multicast_family"
            ''' <summary>Key for resource getting something like "The parameter {0} must contain one or more elements."</summary>
            Public Const net_sockets_zerolist$ = "net_sockets_zerolist"
            ''' <summary>Key for resource getting something like "The operation is not allowed on a non-blocking Socket."</summary>
            Public Const net_sockets_blocking$ = "net_sockets_blocking"
            ''' <summary>Key for resource getting something like "Use the Blocking property to change the status of the Socket."</summary>
            Public Const net_sockets_useblocking$ = "net_sockets_useblocking"
            ''' <summary>Key for resource getting something like "The operation is not allowed on objects of type {0}. Use only objects of type {1}."</summary>
            Public Const net_sockets_select$ = "net_sockets_select"
            ''' <summary>Key for resource getting something like "The {0} list contains too many items; a maximum of {1} is allowed."</summary>
            Public Const net_sockets_toolarge_select$ = "net_sockets_toolarge_select"
            ''' <summary>Key for resource getting something like "All lists are either null or empty."</summary>
            Public Const net_sockets_empty_select$ = "net_sockets_empty_select"
            ''' <summary>Key for resource getting something like "You must call the Bind method before performing this operation."</summary>
            Public Const net_sockets_mustbind$ = "net_sockets_mustbind"
            ''' <summary>Key for resource getting something like "You must call the Listen method before performing this operation."</summary>
            Public Const net_sockets_mustlisten$ = "net_sockets_mustlisten"
            ''' <summary>Key for resource getting something like "You may not perform this operation after calling the Listen method."</summary>
            Public Const net_sockets_mustnotlisten$ = "net_sockets_mustnotlisten"
            ''' <summary>Key for resource getting something like "The socket must not be bound or connected."</summary>
            Public Const net_sockets_mustnotbebound$ = "net_sockets_mustnotbebound"
            ''' <summary>Key for resource getting something like "{0}: The socket must not be bound or connected."</summary>
            Public Const net_sockets_namedmustnotbebound$ = "net_sockets_namedmustnotbebound"
            ''' <summary>Key for resource getting something like "The specified value for the socket information in invalid."</summary>
            Public Const net_sockets_invalid_socketinformation$ = "net_sockets_invalid_socketinformation"
            ''' <summary>Key for resource getting something like "The number of specified IP addresses has to be greater than 0."</summary>
            Public Const net_sockets_invalid_ipaddress_length$ = "net_sockets_invalid_ipaddress_length"
            ''' <summary>Key for resource getting something like "The specified value is not a valid '{0}'."</summary>
            Public Const net_sockets_invalid_optionValue$ = "net_sockets_invalid_optionValue"
            ''' <summary>Key for resource getting something like "The specified value is not valid."</summary>
            Public Const net_sockets_invalid_optionValue_all$ = "net_sockets_invalid_optionValue_all"
            ''' <summary>Key for resource getting something like "Once the socket has been disconnected, you can only reconnect again asynchronously, and only to a different EndPoint.  BeginConnect must be called on a thread that won't exit until the operation has been completed."</summary>
            Public Const net_sockets_disconnectedConnect$ = "net_sockets_disconnectedConnect"
            ''' <summary>Key for resource getting something like "Once the socket has been disconnected, you can only accept again asynchronously.  BeginAccept must be called on a thread that won't exit until the operation has been completed."</summary>
            Public Const net_sockets_disconnectedAccept$ = "net_sockets_disconnectedAccept"
            ''' <summary>Key for resource getting something like "The TcpListener must not be listening before performing this operation."</summary>
            Public Const net_tcplistener_mustbestopped$ = "net_tcplistener_mustbestopped"
            ''' <summary>Key for resource getting something like "BeginConnect cannot be called while another asynchronous operation is in progress on the same Socket."</summary>
            Public Const net_sockets_no_duplicate_async$ = "net_sockets_no_duplicate_async"
            ''' <summary>Key for resource getting something like "Error creating the Web Proxy specified in the 'system.net/defaultProxy' configuration section."</summary>
            Public Const net_config_proxy$ = "net_config_proxy"
            ''' <summary>Key for resource getting something like "The specified proxy module type is not public."</summary>
            Public Const net_config_proxy_module_not_public$ = "net_config_proxy_module_not_public"
            ''' <summary>Key for resource getting something like "Error creating the modules specified in the 'system.net/authenticationModules' configuration section."</summary>
            Public Const net_config_authenticationmodules$ = "net_config_authenticationmodules"
            ''' <summary>Key for resource getting something like "Error creating the modules specified in the 'system.net/webRequestModules' configuration section."</summary>
            Public Const net_config_webrequestmodules$ = "net_config_webrequestmodules"
            ''' <summary>Key for resource getting something like "Error creating the Web Request caching policy specified in the 'system.net/requestCaching' configuration section."</summary>
            Public Const net_config_requestcaching$ = "net_config_requestcaching"
            ''' <summary>Key for resource getting something like "Insufficient permissions for setting the configuration section '{0}'."</summary>
            Public Const net_config_section_permission$ = "net_config_section_permission"
            ''' <summary>Key for resource getting something like "Insufficient permissions for setting the configuration element '{0}'."</summary>
            Public Const net_config_element_permission$ = "net_config_element_permission"
            ''' <summary>Key for resource getting something like "Insufficient permissions for setting the configuration property '{0}'."</summary>
            Public Const net_config_property_permission$ = "net_config_property_permission"
            ''' <summary>Key for resource getting something like "Header name is invalid"</summary>
            Public Const net_WebResponseParseError_InvalidHeaderName$ = "net_WebResponseParseError_InvalidHeaderName"
            ''' <summary>Key for resource getting something like "'Content-Length' header value is invalid"</summary>
            Public Const net_WebResponseParseError_InvalidContentLength$ = "net_WebResponseParseError_InvalidContentLength"
            ''' <summary>Key for resource getting something like "Invalid header name"</summary>
            Public Const net_WebResponseParseError_IncompleteHeaderLine$ = "net_WebResponseParseError_IncompleteHeaderLine"
            ''' <summary>Key for resource getting something like "CR must be followed by LF"</summary>
            Public Const net_WebResponseParseError_CrLfError$ = "net_WebResponseParseError_CrLfError"
            ''' <summary>Key for resource getting something like "Response chunk format is invalid"</summary>
            Public Const net_WebResponseParseError_InvalidChunkFormat$ = "net_WebResponseParseError_InvalidChunkFormat"
            ''' <summary>Key for resource getting something like "Unexpected server response received"</summary>
            Public Const net_WebResponseParseError_UnexpectedServerResponse$ = "net_WebResponseParseError_UnexpectedServerResponse"
            ''' <summary>Key for resource getting something like "Specified value has invalid Control characters."</summary>
            Public Const net_WebHeaderInvalidControlChars$ = "net_WebHeaderInvalidControlChars"
            ''' <summary>Key for resource getting something like "Specified value has invalid CRLF characters."</summary>
            Public Const net_WebHeaderInvalidCRLFChars$ = "net_WebHeaderInvalidCRLFChars"
            ''' <summary>Key for resource getting something like "Specified value has invalid HTTP Header characters."</summary>
            Public Const net_WebHeaderInvalidHeaderChars$ = "net_WebHeaderInvalidHeaderChars"
            ''' <summary>Key for resource getting something like "Specified value has invalid non-ASCII characters."</summary>
            Public Const net_WebHeaderInvalidNonAsciiChars$ = "net_WebHeaderInvalidNonAsciiChars"
            ''' <summary>Key for resource getting something like "Specified value does not have a ':' separator."</summary>
            Public Const net_WebHeaderMissingColon$ = "net_WebHeaderMissingColon"
            ''' <summary>Key for resource getting something like "Status success"</summary>
            Public Const net_webstatus_Success$ = "net_webstatus_Success"
            ''' <summary>Key for resource getting something like "The remote name could not be resolved"</summary>
            Public Const net_webstatus_NameResolutionFailure$ = "net_webstatus_NameResolutionFailure"
            ''' <summary>Key for resource getting something like "Unable to connect to the remote server"</summary>
            Public Const net_webstatus_ConnectFailure$ = "net_webstatus_ConnectFailure"
            ''' <summary>Key for resource getting something like "An unexpected error occurred on a receive"</summary>
            Public Const net_webstatus_ReceiveFailure$ = "net_webstatus_ReceiveFailure"
            ''' <summary>Key for resource getting something like "An unexpected error occurred on a send"</summary>
            Public Const net_webstatus_SendFailure$ = "net_webstatus_SendFailure"
            ''' <summary>Key for resource getting something like "A pipeline failure occurred"</summary>
            Public Const net_webstatus_PipelineFailure$ = "net_webstatus_PipelineFailure"
            ''' <summary>Key for resource getting something like "The request was canceled"</summary>
            Public Const net_webstatus_RequestCanceled$ = "net_webstatus_RequestCanceled"
            ''' <summary>Key for resource getting something like "The connection was closed unexpectedly"</summary>
            Public Const net_webstatus_ConnectionClosed$ = "net_webstatus_ConnectionClosed"
            ''' <summary>Key for resource getting something like "Could not establish trust relationship for the SSL/TLS secure channel"</summary>
            Public Const net_webstatus_TrustFailure$ = "net_webstatus_TrustFailure"
            ''' <summary>Key for resource getting something like "Could not create SSL/TLS secure channel"</summary>
            Public Const net_webstatus_SecureChannelFailure$ = "net_webstatus_SecureChannelFailure"
            ''' <summary>Key for resource getting something like "The server committed a protocol violation"</summary>
            Public Const net_webstatus_ServerProtocolViolation$ = "net_webstatus_ServerProtocolViolation"
            ''' <summary>Key for resource getting something like "A connection that was expected to be kept alive was closed by the server"</summary>
            Public Const net_webstatus_KeepAliveFailure$ = "net_webstatus_KeepAliveFailure"
            ''' <summary>Key for resource getting something like "The proxy name could not be resolved"</summary>
            Public Const net_webstatus_ProxyNameResolutionFailure$ = "net_webstatus_ProxyNameResolutionFailure"
            ''' <summary>Key for resource getting something like "The message length limit was exceeded"</summary>
            Public Const net_webstatus_MessageLengthLimitExceeded$ = "net_webstatus_MessageLengthLimitExceeded"
            ''' <summary>Key for resource getting something like "The request cache-only policy does not allow a network request and the response is not found in cache"</summary>
            Public Const net_webstatus_CacheEntryNotFound$ = "net_webstatus_CacheEntryNotFound"
            ''' <summary>Key for resource getting something like "The request could not be satisfied using a cache-only policy"</summary>
            Public Const net_webstatus_RequestProhibitedByCachePolicy$ = "net_webstatus_RequestProhibitedByCachePolicy"
            ''' <summary>Key for resource getting something like "The operation has timed out"</summary>
            Public Const net_webstatus_Timeout$ = "net_webstatus_Timeout"
            ''' <summary>Key for resource getting something like "The IWebProxy object associated with the request did not allow the request to proceed"</summary>
            Public Const net_webstatus_RequestProhibitedByProxy$ = "net_webstatus_RequestProhibitedByProxy"
            ''' <summary>Key for resource getting something like "The server returned a status code outside the valid range of 100-599."</summary>
            Public Const net_InvalidStatusCode$ = "net_InvalidStatusCode"
            ''' <summary>Key for resource getting something like "Service not available, closing control connection"</summary>
            Public Const net_ftpstatuscode_ServiceNotAvailable$ = "net_ftpstatuscode_ServiceNotAvailable"
            ''' <summary>Key for resource getting something like "Can't open data connection"</summary>
            Public Const net_ftpstatuscode_CantOpenData$ = "net_ftpstatuscode_CantOpenData"
            ''' <summary>Key for resource getting something like "Connection closed; transfer aborted"</summary>
            Public Const net_ftpstatuscode_ConnectionClosed$ = "net_ftpstatuscode_ConnectionClosed"
            ''' <summary>Key for resource getting something like "File unavailable (e.g., file busy)"</summary>
            Public Const net_ftpstatuscode_ActionNotTakenFileUnavailableOrBusy$ = "net_ftpstatuscode_ActionNotTakenFileUnavailableOrBusy"
            ''' <summary>Key for resource getting something like "Local error in processing"</summary>
            Public Const net_ftpstatuscode_ActionAbortedLocalProcessingError$ = "net_ftpstatuscode_ActionAbortedLocalProcessingError"
            ''' <summary>Key for resource getting something like "Insufficient storage space in system"</summary>
            Public Const net_ftpstatuscode_ActionNotTakenInsufficentSpace$ = "net_ftpstatuscode_ActionNotTakenInsufficentSpace"
            ''' <summary>Key for resource getting something like "Syntax error, command unrecognized"</summary>
            Public Const net_ftpstatuscode_CommandSyntaxError$ = "net_ftpstatuscode_CommandSyntaxError"
            ''' <summary>Key for resource getting something like "Syntax error in parameters or arguments"</summary>
            Public Const net_ftpstatuscode_ArgumentSyntaxError$ = "net_ftpstatuscode_ArgumentSyntaxError"
            ''' <summary>Key for resource getting something like "Command not implemented"</summary>
            Public Const net_ftpstatuscode_CommandNotImplemented$ = "net_ftpstatuscode_CommandNotImplemented"
            ''' <summary>Key for resource getting something like "Bad sequence of commands"</summary>
            Public Const net_ftpstatuscode_BadCommandSequence$ = "net_ftpstatuscode_BadCommandSequence"
            ''' <summary>Key for resource getting something like "Not logged in"</summary>
            Public Const net_ftpstatuscode_NotLoggedIn$ = "net_ftpstatuscode_NotLoggedIn"
            ''' <summary>Key for resource getting something like "Need account for storing files"</summary>
            Public Const net_ftpstatuscode_AccountNeeded$ = "net_ftpstatuscode_AccountNeeded"
            ''' <summary>Key for resource getting something like "File unavailable (e.g., file not found, no access)"</summary>
            Public Const net_ftpstatuscode_ActionNotTakenFileUnavailable$ = "net_ftpstatuscode_ActionNotTakenFileUnavailable"
            ''' <summary>Key for resource getting something like "Page type unknown"</summary>
            Public Const net_ftpstatuscode_ActionAbortedUnknownPageType$ = "net_ftpstatuscode_ActionAbortedUnknownPageType"
            ''' <summary>Key for resource getting something like "Exceeded storage allocation (for current directory or data set)"</summary>
            Public Const net_ftpstatuscode_FileActionAborted$ = "net_ftpstatuscode_FileActionAborted"
            ''' <summary>Key for resource getting something like "File name not allowed"</summary>
            Public Const net_ftpstatuscode_ActionNotTakenFilenameNotAllowed$ = "net_ftpstatuscode_ActionNotTakenFilenameNotAllowed"
            ''' <summary>Key for resource getting something like "No Content"</summary>
            Public Const net_httpstatuscode_NoContent$ = "net_httpstatuscode_NoContent"
            ''' <summary>Key for resource getting something like "Non Authoritative Information"</summary>
            Public Const net_httpstatuscode_NonAuthoritativeInformation$ = "net_httpstatuscode_NonAuthoritativeInformation"
            ''' <summary>Key for resource getting something like "Reset Content"</summary>
            Public Const net_httpstatuscode_ResetContent$ = "net_httpstatuscode_ResetContent"
            ''' <summary>Key for resource getting something like "Partial Content"</summary>
            Public Const net_httpstatuscode_PartialContent$ = "net_httpstatuscode_PartialContent"
            ''' <summary>Key for resource getting something like "Multiple Choices Redirect"</summary>
            Public Const net_httpstatuscode_MultipleChoices$ = "net_httpstatuscode_MultipleChoices"
            ''' <summary>Key for resource getting something like "Ambiguous Redirect"</summary>
            Public Const net_httpstatuscode_Ambiguous$ = "net_httpstatuscode_Ambiguous"
            ''' <summary>Key for resource getting something like "Moved Permanently Redirect"</summary>
            Public Const net_httpstatuscode_MovedPermanently$ = "net_httpstatuscode_MovedPermanently"
            ''' <summary>Key for resource getting something like "Moved Redirect"</summary>
            Public Const net_httpstatuscode_Moved$ = "net_httpstatuscode_Moved"
            ''' <summary>Key for resource getting something like "Found Redirect"</summary>
            Public Const net_httpstatuscode_Found$ = "net_httpstatuscode_Found"
            ''' <summary>Key for resource getting something like "Redirect"</summary>
            Public Const net_httpstatuscode_Redirect$ = "net_httpstatuscode_Redirect"
            ''' <summary>Key for resource getting something like "See Other"</summary>
            Public Const net_httpstatuscode_SeeOther$ = "net_httpstatuscode_SeeOther"
            ''' <summary>Key for resource getting something like "Redirect Method"</summary>
            Public Const net_httpstatuscode_RedirectMethod$ = "net_httpstatuscode_RedirectMethod"
            ''' <summary>Key for resource getting something like "Not Modified"</summary>
            Public Const net_httpstatuscode_NotModified$ = "net_httpstatuscode_NotModified"
            ''' <summary>Key for resource getting something like "Use Proxy Redirect"</summary>
            Public Const net_httpstatuscode_UseProxy$ = "net_httpstatuscode_UseProxy"
            ''' <summary>Key for resource getting something like "Temporary Redirect"</summary>
            Public Const net_httpstatuscode_TemporaryRedirect$ = "net_httpstatuscode_TemporaryRedirect"
            ''' <summary>Key for resource getting something like "Redirect Keep Verb"</summary>
            Public Const net_httpstatuscode_RedirectKeepVerb$ = "net_httpstatuscode_RedirectKeepVerb"
            ''' <summary>Key for resource getting something like "Bad Request"</summary>
            Public Const net_httpstatuscode_BadRequest$ = "net_httpstatuscode_BadRequest"
            ''' <summary>Key for resource getting something like "Unauthorized"</summary>
            Public Const net_httpstatuscode_Unauthorized$ = "net_httpstatuscode_Unauthorized"
            ''' <summary>Key for resource getting something like "Payment Required"</summary>
            Public Const net_httpstatuscode_PaymentRequired$ = "net_httpstatuscode_PaymentRequired"
            ''' <summary>Key for resource getting something like "Forbidden"</summary>
            Public Const net_httpstatuscode_Forbidden$ = "net_httpstatuscode_Forbidden"
            ''' <summary>Key for resource getting something like "Not Found"</summary>
            Public Const net_httpstatuscode_NotFound$ = "net_httpstatuscode_NotFound"
            ''' <summary>Key for resource getting something like "Method Not Allowed"</summary>
            Public Const net_httpstatuscode_MethodNotAllowed$ = "net_httpstatuscode_MethodNotAllowed"
            ''' <summary>Key for resource getting something like "Not Acceptable"</summary>
            Public Const net_httpstatuscode_NotAcceptable$ = "net_httpstatuscode_NotAcceptable"
            ''' <summary>Key for resource getting something like "Proxy Authentication Required"</summary>
            Public Const net_httpstatuscode_ProxyAuthenticationRequired$ = "net_httpstatuscode_ProxyAuthenticationRequired"
            ''' <summary>Key for resource getting something like "Request Timeout"</summary>
            Public Const net_httpstatuscode_RequestTimeout$ = "net_httpstatuscode_RequestTimeout"
            ''' <summary>Key for resource getting something like "Conflict"</summary>
            Public Const net_httpstatuscode_Conflict$ = "net_httpstatuscode_Conflict"
            ''' <summary>Key for resource getting something like "Gone"</summary>
            Public Const net_httpstatuscode_Gone$ = "net_httpstatuscode_Gone"
            ''' <summary>Key for resource getting something like "Length Required"</summary>
            Public Const net_httpstatuscode_LengthRequired$ = "net_httpstatuscode_LengthRequired"
            ''' <summary>Key for resource getting something like "Internal Server Error"</summary>
            Public Const net_httpstatuscode_InternalServerError$ = "net_httpstatuscode_InternalServerError"
            ''' <summary>Key for resource getting something like "Not Implemented"</summary>
            Public Const net_httpstatuscode_NotImplemented$ = "net_httpstatuscode_NotImplemented"
            ''' <summary>Key for resource getting something like "Bad Gateway"</summary>
            Public Const net_httpstatuscode_BadGateway$ = "net_httpstatuscode_BadGateway"
            ''' <summary>Key for resource getting something like "Server Unavailable"</summary>
            Public Const net_httpstatuscode_ServiceUnavailable$ = "net_httpstatuscode_ServiceUnavailable"
            ''' <summary>Key for resource getting something like "Gateway Timeout"</summary>
            Public Const net_httpstatuscode_GatewayTimeout$ = "net_httpstatuscode_GatewayTimeout"
            ''' <summary>Key for resource getting something like "Http Version Not Supported"</summary>
            Public Const net_httpstatuscode_HttpVersionNotSupported$ = "net_httpstatuscode_HttpVersionNotSupported"
            ''' <summary>Key for resource getting something like "Invalid URI: The URI scheme is not valid."</summary>
            Public Const net_uri_BadScheme$ = "net_uri_BadScheme"
            ''' <summary>Key for resource getting something like "Invalid URI: The format of the URI could not be determined."</summary>
            Public Const net_uri_BadFormat$ = "net_uri_BadFormat"
            ''' <summary>Key for resource getting something like "Invalid URI: The username:password construct is badly formed."</summary>
            Public Const net_uri_BadUserPassword$ = "net_uri_BadUserPassword"
            ''' <summary>Key for resource getting something like "Invalid URI: The hostname could not be parsed."</summary>
            Public Const net_uri_BadHostName$ = "net_uri_BadHostName"
            ''' <summary>Key for resource getting something like "Invalid URI: The Authority/Host could not be parsed."</summary>
            Public Const net_uri_BadAuthority$ = "net_uri_BadAuthority"
            ''' <summary>Key for resource getting something like "Invalid URI: The Authority/Host cannot end with a backslash character ('\')."</summary>
            Public Const net_uri_BadAuthorityTerminator$ = "net_uri_BadAuthorityTerminator"
            ''' <summary>Key for resource getting something like "Invalid URI: Can't parse data as a filename."</summary>
            Public Const net_uri_BadFileName$ = "net_uri_BadFileName"
            ''' <summary>Key for resource getting something like "Invalid URI: The URI is empty."</summary>
            Public Const net_uri_EmptyUri$ = "net_uri_EmptyUri"
            ''' <summary>Key for resource getting something like "Invalid URI: There is an invalid sequence in the string."</summary>
            Public Const net_uri_BadString$ = "net_uri_BadString"
            ''' <summary>Key for resource getting something like "Invalid URI: A Dos path must be rooted, for example, 'c:\'."</summary>
            Public Const net_uri_MustRootedPath$ = "net_uri_MustRootedPath"
            ''' <summary>Key for resource getting something like "Invalid URI: A port was expected because of there is a colon (':') present but the port could not be parsed."</summary>
            Public Const net_uri_BadPort$ = "net_uri_BadPort"
            ''' <summary>Key for resource getting something like "Invalid URI: The Uri string is too long."</summary>
            Public Const net_uri_SizeLimit$ = "net_uri_SizeLimit"
            ''' <summary>Key for resource getting something like "Invalid URI: The Uri scheme is too long."</summary>
            Public Const net_uri_SchemeLimit$ = "net_uri_SchemeLimit"
            ''' <summary>Key for resource getting something like "This operation is not supported for a relative URI."</summary>
            Public Const net_uri_NotAbsolute$ = "net_uri_NotAbsolute"
            ''' <summary>Key for resource getting something like "A special Uri parsing request: '{0}' cannot be mixed with other Uri component parsing flags: '{1}'."</summary>
            Public Const net_uri_SpecialUriComponent$ = "net_uri_SpecialUriComponent"
            ''' <summary>Key for resource getting something like "A Uri derived class has refused to accept the input string as a valid absolute Uri."</summary>
            Public Const net_uri_CustomValidationFailed$ = "net_uri_CustomValidationFailed"
            ''' <summary>Key for resource getting something like "A derived type '{0}' has reported an invalid value for the Uri port '{1}'."</summary>
            Public Const net_uri_PortOutOfRange$ = "net_uri_PortOutOfRange"
            ''' <summary>Key for resource getting something like "A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used."</summary>
            Public Const net_uri_UserDrivenParsing$ = "net_uri_UserDrivenParsing"
            ''' <summary>Key for resource getting something like "A URI scheme name '{0}' already has a registered custom parser."</summary>
            Public Const net_uri_AlreadyRegistered$ = "net_uri_AlreadyRegistered"
            ''' <summary>Key for resource getting something like "The URI parser instance passed into 'uriParser' parameter is already registered with the scheme name '{0}'."</summary>
            Public Const net_uri_NeedFreshParser$ = "net_uri_NeedFreshParser"
            ''' <summary>Key for resource getting something like "A relative URI cannot be created because the 'uriString' parameter represents an absolute URI."</summary>
            Public Const net_uri_CannotCreateRelative$ = "net_uri_CannotCreateRelative"
            ''' <summary>Key for resource getting something like "The value '{0}' passed for the UriKind parameter is invalid."</summary>
            Public Const net_uri_InvalidUriKind$ = "net_uri_InvalidUriKind"
            ''' <summary>Key for resource getting something like "The socket has already been bound to an io completion port."</summary>
            Public Const net_io_completionportwasbound$ = "net_io_completionportwasbound"
            ''' <summary>Key for resource getting something like "Unable to write data to the transport connection: {0}."</summary>
            Public Const net_io_writefailure$ = "net_io_writefailure"
            ''' <summary>Key for resource getting something like "Unable to read data from the transport connection: {0}."</summary>
            Public Const net_io_readfailure$ = "net_io_readfailure"
            ''' <summary>Key for resource getting something like "The connection was closed"</summary>
            Public Const net_io_connectionclosed$ = "net_io_connectionclosed"
            ''' <summary>Key for resource getting something like "Unable to create a transport connection."</summary>
            Public Const net_io_transportfailure$ = "net_io_transportfailure"
            ''' <summary>Key for resource getting something like "Internal Error: A socket handle could not be bound to a completion port."</summary>
            Public Const net_io_internal_bind$ = "net_io_internal_bind"
            ''' <summary>Key for resource getting something like "The IAsyncResult object was not returned from the corresponding asynchronous method on this class."</summary>
            Public Const net_io_invalidasyncresult$ = "net_io_invalidasyncresult"
            ''' <summary>Key for resource getting something like "The {0} method cannot be called when another {1} operation is pending."</summary>
            Public Const net_io_invalidnestedcall$ = "net_io_invalidnestedcall"
            ''' <summary>Key for resource getting something like "{0} can only be called once for each asynchronous operation."</summary>
            Public Const net_io_invalidendcall$ = "net_io_invalidendcall"
            ''' <summary>Key for resource getting something like "The stream has to be read/write."</summary>
            Public Const net_io_must_be_rw_stream$ = "net_io_must_be_rw_stream"
            ''' <summary>Key for resource getting something like "Found a wrong header field {0} read = {1}, expected = {2}."</summary>
            Public Const net_io_header_id$ = "net_io_header_id"
            ''' <summary>Key for resource getting something like "The byte count must not exceed {0} bytes for this stream type."</summary>
            Public Const net_io_out_range$ = "net_io_out_range"
            ''' <summary>Key for resource getting something like "The encryption operation failed, see inner exception."</summary>
            Public Const net_io_encrypt$ = "net_io_encrypt"
            ''' <summary>Key for resource getting something like "The decryption operation failed, see inner exception."</summary>
            Public Const net_io_decrypt$ = "net_io_decrypt"
            ''' <summary>Key for resource getting something like "The read operation failed, see inner exception."</summary>
            Public Const net_io_read$ = "net_io_read"
            ''' <summary>Key for resource getting something like "The write operation failed, see inner exception."</summary>
            Public Const net_io_write$ = "net_io_write"
            ''' <summary>Key for resource getting something like "Received an unexpected EOF or 0 bytes from the transport stream."</summary>
            Public Const net_io_eof$ = "net_io_eof"
            ''' <summary>Key for resource getting something like "The parameter: {0} is not valid. Use the object returned from corresponding Begin async call."</summary>
            Public Const net_io_async_result$ = "net_io_async_result"
            ''' <summary>Key for resource getting something like "This collection holds response headers and cannot contain the specified request header."</summary>
            Public Const net_headers_req$ = "net_headers_req"
            ''' <summary>Key for resource getting something like "This collection holds request headers and cannot contain the specified response header."</summary>
            Public Const net_headers_rsp$ = "net_headers_rsp"
            ''' <summary>Key for resource getting something like "Header values cannot be longer than {0} characters."</summary>
            Public Const net_headers_toolong$ = "net_headers_toolong"
            ''' <summary>Key for resource getting something like "This property cannot be set to an empty string."</summary>
            Public Const net_emptystringset$ = "net_emptystringset"
            ''' <summary>Key for resource getting something like "The parameter '{0}' cannot be an empty string."</summary>
            Public Const net_emptystringcall$ = "net_emptystringcall"
            ''' <summary>Key for resource getting something like "Please call the {0} method before calling this method."</summary>
            Public Const net_listener_mustcall$ = "net_listener_mustcall"
            ''' <summary>Key for resource getting something like "The in-progress method {0} must be completed first."</summary>
            Public Const net_listener_mustcompletecall$ = "net_listener_mustcompletecall"
            ''' <summary>Key for resource getting something like "Cannot re-call {0} while a previous call is still in progress."</summary>
            Public Const net_listener_callinprogress$ = "net_listener_callinprogress"
            ''' <summary>Key for resource getting something like "Only Uri prefixes starting with 'http://' or 'https://' are supported."</summary>
            Public Const net_listener_scheme$ = "net_listener_scheme"
            ''' <summary>Key for resource getting something like "Only Uri prefixes with a valid hostname are supported."</summary>
            Public Const net_listener_host$ = "net_listener_host"
            ''' <summary>Key for resource getting something like "Only Uri prefixes ending in '/' are allowed."</summary>
            Public Const net_listener_slash$ = "net_listener_slash"
            ''' <summary>Key for resource getting something like "This method cannot be called twice."</summary>
            Public Const net_listener_repcall$ = "net_listener_repcall"
            ''' <summary>Key for resource getting something like "The SSL version is not supported."</summary>
            Public Const net_tls_version$ = "net_tls_version"
            ''' <summary>Key for resource getting something like "Cannot cast target permission type."</summary>
            Public Const net_perm_target$ = "net_perm_target"
            ''' <summary>Key for resource getting something like "Cannot subset Regex. Only support if both patterns are identical."</summary>
            Public Const net_perm_both_regex$ = "net_perm_both_regex"
            ''' <summary>Key for resource getting something like "There are no permissions to check."</summary>
            Public Const net_perm_none$ = "net_perm_none"
            ''' <summary>Key for resource getting something like "The value for '{0}' must be specified."</summary>
            Public Const net_perm_attrib_count$ = "net_perm_attrib_count"
            ''' <summary>Key for resource getting something like "The parameter value '{0}={1}' is invalid."</summary>
            Public Const net_perm_invalid_val$ = "net_perm_invalid_val"
            ''' <summary>Key for resource getting something like "The permission '{0}={1}' cannot be added. Add a separate Attribute statement."</summary>
            Public Const net_perm_attrib_multi$ = "net_perm_attrib_multi"
            ''' <summary>Key for resource getting something like "The argument value '{0}' is invalid for creating a SocketPermission object."</summary>
            Public Const net_perm_epname$ = "net_perm_epname"
            ''' <summary>Key for resource getting something like "The '{0}' element contains one or more invalid values."</summary>
            Public Const net_perm_invalid_val_in_element$ = "net_perm_invalid_val_in_element"
            ''' <summary>Key for resource getting something like "IPv4 address 0.0.0.0 and IPv6 address ::0 are unspecified addresses that cannot be used as a target address."</summary>
            Public Const net_invalid_ip_addr$ = "net_invalid_ip_addr"
            ''' <summary>Key for resource getting something like "An invalid IP address was specified."</summary>
            Public Const dns_bad_ip_address$ = "dns_bad_ip_address"
            ''' <summary>Key for resource getting something like "An invalid physical address was specified."</summary>
            Public Const net_bad_mac_address$ = "net_bad_mac_address"
            ''' <summary>Key for resource getting something like "An exception occurred during a Ping request."</summary>
            Public Const net_ping$ = "net_ping"
            ''' <summary>Key for resource getting something like "An exception occurred during a WebClient request."</summary>
            Public Const net_webclient$ = "net_webclient"
            ''' <summary>Key for resource getting something like "The Content-Type header cannot be changed from its default value for this request."</summary>
            Public Const net_webclient_ContentType$ = "net_webclient_ContentType"
            ''' <summary>Key for resource getting something like "The Content-Type header cannot be set to a multipart type for this request."</summary>
            Public Const net_webclient_Multipart$ = "net_webclient_Multipart"
            ''' <summary>Key for resource getting something like "WebClient does not support concurrent I/O operations."</summary>
            Public Const net_webclient_no_concurrent_io_allowed$ = "net_webclient_no_concurrent_io_allowed"
            ''' <summary>Key for resource getting something like "The specified value is not a valid base address."</summary>
            Public Const net_webclient_invalid_baseaddress$ = "net_webclient_invalid_baseaddress"
            ''' <summary>Key for resource getting something like "An error occurred when adding a cookie to the container."</summary>
            Public Const net_container_add_cookie$ = "net_container_add_cookie"
            ''' <summary>Key for resource getting something like "Invalid contents for cookie = '{0}'."</summary>
            Public Const net_cookie_invalid$ = "net_cookie_invalid"
            ''' <summary>Key for resource getting something like "The value size of the cookie is '{0}'. This exceeds the configured maximum size, which is '{1}'."</summary>
            Public Const net_cookie_size$ = "net_cookie_size"
            ''' <summary>Key for resource getting something like "An error occurred when parsing the Cookie header for Uri '{0}'."</summary>
            Public Const net_cookie_parse_header$ = "net_cookie_parse_header"
            ''' <summary>Key for resource getting something like "The '{0}'='{1}' part of the cookie is invalid."</summary>
            Public Const net_cookie_attribute$ = "net_cookie_attribute"
            ''' <summary>Key for resource getting something like "Cookie format error."</summary>
            Public Const net_cookie_format$ = "net_cookie_format"
            ''' <summary>Key for resource getting something like "Cookie already exists."</summary>
            Public Const net_cookie_exists$ = "net_cookie_exists"
            ''' <summary>Key for resource getting something like "'{0}' has to be greater than '{1}' and less than '{2}'."</summary>
            Public Const net_cookie_capacity_range$ = "net_cookie_capacity_range"
            ''' <summary>Key for resource getting something like "Failed to impersonate a thread doing authentication of a Web Request."</summary>
            Public Const net_set_token$ = "net_set_token"
            ''' <summary>Key for resource getting something like "Failed to revert the thread token after authenticating a Web Request."</summary>
            Public Const net_revert_token$ = "net_revert_token"
            ''' <summary>Key for resource getting something like "Async context creation failed."</summary>
            Public Const net_ssl_io_async_context$ = "net_ssl_io_async_context"
            ''' <summary>Key for resource getting something like "The encryption operation failed, see inner exception."</summary>
            Public Const net_ssl_io_encrypt$ = "net_ssl_io_encrypt"
            ''' <summary>Key for resource getting something like "The decryption operation failed, see inner exception."</summary>
            Public Const net_ssl_io_decrypt$ = "net_ssl_io_decrypt"
            ''' <summary>Key for resource getting something like "The security context has expired."</summary>
            Public Const net_ssl_io_context_expired$ = "net_ssl_io_context_expired"
            ''' <summary>Key for resource getting something like "The handshake failed. The remote side has dropped the stream."</summary>
            Public Const net_ssl_io_handshake_start$ = "net_ssl_io_handshake_start"
            ''' <summary>Key for resource getting something like "The handshake failed, see inner exception."</summary>
            Public Const net_ssl_io_handshake$ = "net_ssl_io_handshake"
            ''' <summary>Key for resource getting something like "The handshake failed due to an unexpected packet format."</summary>
            Public Const net_ssl_io_frame$ = "net_ssl_io_frame"
            ''' <summary>Key for resource getting something like "The stream is corrupted due to an invalid SSL version number in the SSL protocol header."</summary>
            Public Const net_ssl_io_corrupted$ = "net_ssl_io_corrupted"
            ''' <summary>Key for resource getting something like "The remote certificate is invalid according to the validation procedure."</summary>
            Public Const net_ssl_io_cert_validation$ = "net_ssl_io_cert_validation"
            ''' <summary>Key for resource getting something like "{0} can only be called once for each asynchronous operation."</summary>
            Public Const net_ssl_io_invalid_end_call$ = "net_ssl_io_invalid_end_call"
            ''' <summary>Key for resource getting something like "{0} cannot be called when another {1} operation is pending."</summary>
            Public Const net_ssl_io_invalid_begin_call$ = "net_ssl_io_invalid_begin_call"
            ''' <summary>Key for resource getting something like "The server mode SSL must use a certificate with the associated private key."</summary>
            Public Const net_ssl_io_no_server_cert$ = "net_ssl_io_no_server_cert"
            ''' <summary>Key for resource getting something like "The server has rejected the client credentials."</summary>
            Public Const net_auth_bad_client_creds$ = "net_auth_bad_client_creds"
            ''' <summary>Key for resource getting something like "Either the target name is incorrect or the server has rejected the client credentials."</summary>
            Public Const net_auth_bad_client_creds_or_target_mismatch$ = "net_auth_bad_client_creds_or_target_mismatch"
            ''' <summary>Key for resource getting something like "A security requirement was not fulfilled during authentication. Required: {0}, negotiated: {1}."</summary>
            Public Const net_auth_context_expectation$ = "net_auth_context_expectation"
            ''' <summary>Key for resource getting something like "A remote side security requirement was not fulfilled during authentication. Try increasing the ProtectionLevel and/or ImpersonationLevel."</summary>
            Public Const net_auth_context_expectation_remote$ = "net_auth_context_expectation_remote"
            ''' <summary>Key for resource getting something like "The supported values are Identification, Impersonation or Delegation."</summary>
            Public Const net_auth_supported_impl_levels$ = "net_auth_supported_impl_levels"
            ''' <summary>Key for resource getting something like "The current platform only supports ProtectionLevel.None."</summary>
            Public Const net_auth_no_protection_on_win9x$ = "net_auth_no_protection_on_win9x"
            ''' <summary>Key for resource getting something like "The TokenImpersonationLevel.Anonymous level is not supported for authentication."</summary>
            Public Const net_auth_no_anonymous_support$ = "net_auth_no_anonymous_support"
            ''' <summary>Key for resource getting something like "This operation is not allowed on a security context that has already been authenticated."</summary>
            Public Const net_auth_reauth$ = "net_auth_reauth"
            ''' <summary>Key for resource getting something like "This operation is only allowed using a successfully authenticated context."</summary>
            Public Const net_auth_noauth$ = "net_auth_noauth"
            ''' <summary>Key for resource getting something like "Once authentication is attempted as the client or server, additional authentication attempts must use the same client or server role."</summary>
            Public Const net_auth_client_server$ = "net_auth_client_server"
            ''' <summary>Key for resource getting something like "This authenticated context does not support data encryption."</summary>
            Public Const net_auth_noencryption$ = "net_auth_noencryption"
            ''' <summary>Key for resource getting something like "A call to SSPI failed, see inner exception."</summary>
            Public Const net_auth_SSPI$ = "net_auth_SSPI"
            ''' <summary>Key for resource getting something like "Authentication failed, see inner exception."</summary>
            Public Const net_auth_failure$ = "net_auth_failure"
            ''' <summary>Key for resource getting something like "Authentication failed because the remote party has closed the transport stream."</summary>
            Public Const net_auth_eof$ = "net_auth_eof"
            ''' <summary>Key for resource getting something like "Authentication failed on the remote side (the stream might still be available for additional authentication attempts)."</summary>
            Public Const net_auth_alert$ = "net_auth_alert"
            ''' <summary>Key for resource getting something like "Re-authentication failed because the remote party continued to encrypt more than {0} bytes before answering re-authentication."</summary>
            Public Const net_auth_ignored_reauth$ = "net_auth_ignored_reauth"
            ''' <summary>Key for resource getting something like "Protocol error: cannot proceed with SSPI handshake because an empty blob was received."</summary>
            Public Const net_auth_empty_read$ = "net_auth_empty_read"
            ''' <summary>Key for resource getting something like "Protocol error: A received message contains a valid signature but it was not encrypted as required by the effective Protection Level."</summary>
            Public Const net_auth_message_not_encrypted$ = "net_auth_message_not_encrypted"
            ''' <summary>Key for resource getting something like "Received an invalid authentication frame. The message size is limited to {0} bytes, attempted to read {1} bytes."</summary>
            Public Const net_frame_size$ = "net_frame_size"
            ''' <summary>Key for resource getting something like "Received incomplete authentication message. Remote party has probably closed the connection."</summary>
            Public Const net_frame_read_io$ = "net_frame_read_io"
            ''' <summary>Key for resource getting something like "Cannot determine the frame size or a corrupted frame was received."</summary>
            Public Const net_frame_read_size$ = "net_frame_read_size"
            ''' <summary>Key for resource getting something like "The payload size is limited to {0}, attempted set it to {1}."</summary>
            Public Const net_frame_max_size$ = "net_frame_max_size"
            ''' <summary>Key for resource getting something like "The proxy JScript file threw an exception while being initialized: {0}."</summary>
            Public Const net_jscript_load$ = "net_jscript_load"
            ''' <summary>Key for resource getting something like "The specified value is not a valid GMT time."</summary>
            Public Const net_proxy_not_gmt$ = "net_proxy_not_gmt"
            ''' <summary>Key for resource getting something like "The specified value is not a valid day of the week."</summary>
            Public Const net_proxy_invalid_dayofweek$ = "net_proxy_invalid_dayofweek"
            ''' <summary>Key for resource getting something like "Argument must be a string instead of {0}."</summary>
            Public Const net_param_not_string$ = "net_param_not_string"
            ''' <summary>Key for resource getting something like "The specified value cannot be negative."</summary>
            Public Const net_value_cannot_be_negative$ = "net_value_cannot_be_negative"
            ''' <summary>Key for resource getting something like "Value of offset cannot be negative or greater than the length of the buffer."</summary>
            Public Const net_invalid_offset$ = "net_invalid_offset"
            ''' <summary>Key for resource getting something like "Sum of offset and count cannot be greater than the length of the buffer."</summary>
            Public Const net_offset_plus_count$ = "net_offset_plus_count"
            ''' <summary>Key for resource getting something like "The specified value cannot be false."</summary>
            Public Const net_cannot_be_false$ = "net_cannot_be_false"
            ''' <summary>Key for resource getting something like "The specified value is not valid in the '{0}' enumeration."</summary>
            Public Const net_invalid_enum$ = "net_invalid_enum"
            ''' <summary>Key for resource getting something like "Failed to listen on prefix '{0}' because it conflicts with an existing registration on the machine."</summary>
            Public Const net_listener_already$ = "net_listener_already"
            ''' <summary>Key for resource getting something like "Shadow stream must be writable."</summary>
            Public Const net_cache_shadowstream_not_writable$ = "net_cache_shadowstream_not_writable"
            ''' <summary>Key for resource getting something like "The validation method {0}() returned a failure for this request."</summary>
            Public Const net_cache_validator_fail$ = "net_cache_validator_fail"
            ''' <summary>Key for resource getting something like "For this RequestCache object, {0} access is denied."</summary>
            Public Const net_cache_access_denied$ = "net_cache_access_denied"
            ''' <summary>Key for resource getting something like "The validation method {0}() returned the unexpected status: {1}."</summary>
            Public Const net_cache_validator_result$ = "net_cache_validator_result"
            ''' <summary>Key for resource getting something like "Cache retrieve failed: {0}."</summary>
            Public Const net_cache_retrieve_failure$ = "net_cache_retrieve_failure"
            ''' <summary>Key for resource getting something like "The cached response is not supported for a request with a content body."</summary>
            Public Const net_cache_not_supported_body$ = "net_cache_not_supported_body"
            ''' <summary>Key for resource getting something like "The cached response is not supported for a request with the specified request method."</summary>
            Public Const net_cache_not_supported_command$ = "net_cache_not_supported_command"
            ''' <summary>Key for resource getting something like "The cache protocol refused the server response. To allow automatic request retrying, set request.AllowAutoRedirect to true."</summary>
            Public Const net_cache_not_accept_response$ = "net_cache_not_accept_response"
            ''' <summary>Key for resource getting something like "The request (Method = {0}) cannot be served from the cache and will fail because of the effective CachePolicy: {1}."</summary>
            Public Const net_cache_method_failed$ = "net_cache_method_failed"
            ''' <summary>Key for resource getting something like "The request failed because no cache entry (CacheKey = {0}) was found and the effective CachePolicy is {1}."</summary>
            Public Const net_cache_key_failed$ = "net_cache_key_failed"
            ''' <summary>Key for resource getting something like "The cache protocol returned a cached response but the cache entry is invalid because it has a null stream. (Cache Key = {0})."</summary>
            Public Const net_cache_no_stream$ = "net_cache_no_stream"
            ''' <summary>Key for resource getting something like "A partial content stream does not support this operation or some method argument is out of range."</summary>
            Public Const net_cache_unsupported_partial_stream$ = "net_cache_unsupported_partial_stream"
            ''' <summary>Key for resource getting something like "No cache protocol is available for this request."</summary>
            Public Const net_cache_not_configured$ = "net_cache_not_configured"
            ''' <summary>Key for resource getting something like "The transport stream instance passed in the RangeStream constructor is not seekable and therefore is not supported."</summary>
            Public Const net_cache_non_seekable_stream_not_supported$ = "net_cache_non_seekable_stream_not_supported"
            ''' <summary>Key for resource getting something like "Invalid cast from {0} to {1}."</summary>
            Public Const net_invalid_cast$ = "net_invalid_cast"
            ''' <summary>Key for resource getting something like "The collection is read-only."</summary>
            Public Const net_collection_readonly$ = "net_collection_readonly"
            ''' <summary>Key for resource getting something like "Specified value does not contain 'IPermission' as its tag."</summary>
            Public Const net_not_ipermission$ = "net_not_ipermission"
            ''' <summary>Key for resource getting something like "Specified value does not contain a 'class' attribute."</summary>
            Public Const net_no_classname$ = "net_no_classname"
            ''' <summary>Key for resource getting something like "The value class attribute is not valid."</summary>
            Public Const net_no_typename$ = "net_no_typename"
            ''' <summary>Key for resource getting something like "The target array is too small."</summary>
            Public Const net_array_too_small$ = "net_array_too_small"
            ''' <summary>Key for resource getting something like "This property is not supported for protocols that do not use URI."</summary>
            Public Const net_servicePointAddressNotSupportedInHostMode$ = "net_servicePointAddressNotSupportedInHostMode"
            ''' <summary>Key for resource getting something like "Sending 500 response, AuthenticationSchemeSelectorDelegate threw an exception: {0}."</summary>
            Public Const net_log_listener_delegate_exception$ = "net_log_listener_delegate_exception"
            ''' <summary>Key for resource getting something like "Received a request with an unsupported authentication scheme, Authorization:{0} SupportedSchemes:{1}."</summary>
            Public Const net_log_listener_unsupported_authentication_scheme$ = "net_log_listener_unsupported_authentication_scheme"
            ''' <summary>Key for resource getting something like "Received a request with an unmatched or no authentication scheme. AuthenticationSchemes:{0}, Authorization:{1}."</summary>
            Public Const net_log_listener_unmatched_authentication_scheme$ = "net_log_listener_unmatched_authentication_scheme"
            ''' <summary>Key for resource getting something like "Failed to create a valid Identity for an incoming request."</summary>
            Public Const net_log_listener_create_valid_identity_failed$ = "net_log_listener_create_valid_identity_failed"
            ''' <summary>Key for resource getting something like "Enumerating security packages:"</summary>
            Public Const net_log_sspi_enumerating_security_packages$ = "net_log_sspi_enumerating_security_packages"
            ''' <summary>Key for resource getting something like "Security package '{0}' was not found."</summary>
            Public Const net_log_sspi_security_package_not_found$ = "net_log_sspi_security_package_not_found"
            ''' <summary>Key for resource getting something like "{0}(In-Buffer length={1}, Out-Buffer length={2}, returned code={3})."</summary>
            Public Const net_log_sspi_security_context_input_buffer$ = "net_log_sspi_security_context_input_buffer"
            ''' <summary>Key for resource getting something like "{0}(In-Buffers count={1}, Out-Buffer length={2}, returned code={3})."</summary>
            Public Const net_log_sspi_security_context_input_buffers$ = "net_log_sspi_security_context_input_buffers"
            ''' <summary>Key for resource getting something like "Remote certificate: {0}."</summary>
            Public Const net_log_remote_certificate$ = "net_log_remote_certificate"
            ''' <summary>Key for resource getting something like "Locating the private key for the certificate: {0}."</summary>
            Public Const net_log_locating_private_key_for_certificate$ = "net_log_locating_private_key_for_certificate"
            ''' <summary>Key for resource getting something like "Certificate is of type X509Certificate2 and contains the private key."</summary>
            Public Const net_log_cert_is_of_type_2$ = "net_log_cert_is_of_type_2"
            ''' <summary>Key for resource getting something like "Found the certificate in the {0} store."</summary>
            Public Const net_log_found_cert_in_store$ = "net_log_found_cert_in_store"
            ''' <summary>Key for resource getting something like "Cannot find the certificate in either the LocalMachine store or the CurrentUser store."</summary>
            Public Const net_log_did_not_find_cert_in_store$ = "net_log_did_not_find_cert_in_store"
            ''' <summary>Key for resource getting something like "Opening Certificate store {0} failed, exception: {1}."</summary>
            Public Const net_log_open_store_failed$ = "net_log_open_store_failed"
            ''' <summary>Key for resource getting something like "Got a certificate from the client delegate."</summary>
            Public Const net_log_got_certificate_from_delegate$ = "net_log_got_certificate_from_delegate"
            ''' <summary>Key for resource getting something like "Client delegate did not provide a certificate; and there are not other user-provided certificates. Need to attempt a session restart."</summary>
            Public Const net_log_no_delegate_and_have_no_client_cert$ = "net_log_no_delegate_and_have_no_client_cert"
            ''' <summary>Key for resource getting something like "Client delegate did not provide a certificate; but there are other user-provided certificates"."</summary>
            Public Const net_log_no_delegate_but_have_client_cert$ = "net_log_no_delegate_but_have_client_cert"
            ''' <summary>Key for resource getting something like "Attempting to restart the session using the user-provided certificate: {0}."</summary>
            Public Const net_log_attempting_restart_using_cert$ = "net_log_attempting_restart_using_cert"
            ''' <summary>Key for resource getting something like "We have user-provided certificates. The server has not specified any issuers, so try all the certificates."</summary>
            Public Const net_log_no_issuers_try_all_certs$ = "net_log_no_issuers_try_all_certs"
            ''' <summary>Key for resource getting something like "We have user-provided certificates. The server has specified {0} issuer(s). Looking for certificates that match any of the issuers."</summary>
            Public Const net_log_server_issuers_look_for_matching_certs$ = "net_log_server_issuers_look_for_matching_certs"
            ''' <summary>Key for resource getting something like "Selected certificate: {0}."</summary>
            Public Const net_log_selected_cert$ = "net_log_selected_cert"
            ''' <summary>Key for resource getting something like "Left with {0} client certificates to choose from."</summary>
            Public Const net_log_n_certs_after_filtering$ = "net_log_n_certs_after_filtering"
            ''' <summary>Key for resource getting something like "Trying to find a matching certificate in the certificate store."</summary>
            Public Const net_log_finding_matching_certs$ = "net_log_finding_matching_certs"
            ''' <summary>Key for resource getting something like "Using the cached credential handle."</summary>
            Public Const net_log_using_cached_credential$ = "net_log_using_cached_credential"
            ''' <summary>Key for resource getting something like "Remote certificate was verified as valid by the user."</summary>
            Public Const net_log_remote_cert_user_declared_valid$ = "net_log_remote_cert_user_declared_valid"
            ''' <summary>Key for resource getting something like "Remote certificate was verified as invalid by the user."</summary>
            Public Const net_log_remote_cert_user_declared_invalid$ = "net_log_remote_cert_user_declared_invalid"
            ''' <summary>Key for resource getting something like "Remote certificate has no errors."</summary>
            Public Const net_log_remote_cert_has_no_errors$ = "net_log_remote_cert_has_no_errors"
            ''' <summary>Key for resource getting something like "Remote certificate has errors:"</summary>
            Public Const net_log_remote_cert_has_errors$ = "net_log_remote_cert_has_errors"
            ''' <summary>Key for resource getting something like "The remote server did not provide a certificate."</summary>
            Public Const net_log_remote_cert_not_available$ = "net_log_remote_cert_not_available"
            ''' <summary>Key for resource getting something like "Certificate name mismatch."</summary>
            Public Const net_log_remote_cert_name_mismatch$ = "net_log_remote_cert_name_mismatch"
            ''' <summary>Key for resource getting something like "WebProxy failed to parse the auto-detected location of a proxy script:"{0}" into a Uri."</summary>
            Public Const net_log_proxy_autodetect_script_location_parse_error$ = "net_log_proxy_autodetect_script_location_parse_error"
            ''' <summary>Key for resource getting something like "WebProxy failed to autodetect a Uri for a proxy script."</summary>
            Public Const net_log_proxy_autodetect_failed$ = "net_log_proxy_autodetect_failed"
            ''' <summary>Key for resource getting something like "WebProxy caught an exception while executing the ScriptReturn script: {0}."</summary>
            Public Const net_log_proxy_script_execution_error$ = "net_log_proxy_script_execution_error"
            ''' <summary>Key for resource getting something like "WebProxy caught an exception while  downloading/compiling the proxy script: {0}."</summary>
            Public Const net_log_proxy_script_download_compile_error$ = "net_log_proxy_script_download_compile_error"
            ''' <summary>Key for resource getting something like "ScriptEngine was notified of a potential change in the system's proxy settings and will update WebProxy settings."</summary>
            Public Const net_log_proxy_system_setting_update$ = "net_log_proxy_system_setting_update"
            ''' <summary>Key for resource getting something like "ScriptEngine was notified of a change in the IP configuration and will update WebProxy settings."</summary>
            Public Const net_log_proxy_update_due_to_ip_config_change$ = "net_log_proxy_update_due_to_ip_config_change"
            ''' <summary>Key for resource getting something like "{0} was called with a null '{1}' parameter."</summary>
            Public Const net_log_proxy_called_with_null_parameter$ = "net_log_proxy_called_with_null_parameter"
            ''' <summary>Key for resource getting something like "{0} was called with an invalid parameter."</summary>
            Public Const net_log_proxy_called_with_invalid_parameter$ = "net_log_proxy_called_with_invalid_parameter"
            ''' <summary>Key for resource getting something like "Resubmitting this request because cache cannot validate the response."</summary>
            Public Const net_log_cache_validation_failed_resubmit$ = "net_log_cache_validation_failed_resubmit"
            ''' <summary>Key for resource getting something like "Caching protocol has refused the server response. To allow automatic request retrying set request.AllowAutoRedirect=true."</summary>
            Public Const net_log_cache_refused_server_response$ = "net_log_cache_refused_server_response"
            ''' <summary>Key for resource getting something like "This FTP request is configured to use a proxy through HTTP protocol. Cache revalidation and partially cached responses are not supported."</summary>
            Public Const net_log_cache_ftp_proxy_doesnt_support_partial$ = "net_log_cache_ftp_proxy_doesnt_support_partial"
            ''' <summary>Key for resource getting something like "FTP request method={0}."</summary>
            Public Const net_log_cache_ftp_method$ = "net_log_cache_ftp_method"
            ''' <summary>Key for resource getting something like "Caching is not supported for non-binary FTP request mode."</summary>
            Public Const net_log_cache_ftp_supports_bin_only$ = "net_log_cache_ftp_supports_bin_only"
            ''' <summary>Key for resource getting something like "Replacing cache entry metadata with 'HTTP/1.1 200 OK' status line to satisfy HTTP cache protocol logic."</summary>
            Public Const net_log_cache_replacing_entry_with_HTTP_200$ = "net_log_cache_replacing_entry_with_HTTP_200"
            ''' <summary>Key for resource getting something like "[Now Time (UTC)] = {0}."</summary>
            Public Const net_log_cache_now_time$ = "net_log_cache_now_time"
            ''' <summary>Key for resource getting something like "[MaxAge] Absolute time expiration check (sensitive to clock skew), cache Expires: {0}."</summary>
            Public Const net_log_cache_max_age_absolute$ = "net_log_cache_max_age_absolute"
            ''' <summary>Key for resource getting something like "[Age1] Now - LastSynchronized = [Age1] Now - LastSynchronized = {0}, Last Synchronized: {1}."</summary>
            Public Const net_log_cache_age1$ = "net_log_cache_age1"
            ''' <summary>Key for resource getting something like "[Age1] NowTime-Date Header = {0}, Date Header: {1}."</summary>
            Public Const net_log_cache_age1_date_header$ = "net_log_cache_age1_date_header"
            ''' <summary>Key for resource getting something like "[Age1] Now - LastSynchronized + AgeHeader = {0}, Last Synchronized: {1}."</summary>
            Public Const net_log_cache_age1_last_synchronized$ = "net_log_cache_age1_last_synchronized"
            ''' <summary>Key for resource getting something like "[Age1] Now - LastSynchronized + AgeHeader = {0}, Last Synchronized: {1}, Age Header: {2}."</summary>
            Public Const net_log_cache_age1_last_synchronized_age_header$ = "net_log_cache_age1_last_synchronized_age_header"
            ''' <summary>Key for resource getting something like "[Age2] AgeHeader = {0}."</summary>
            Public Const net_log_cache_age2$ = "net_log_cache_age2"
            ''' <summary>Key for resource getting something like "[MaxAge] Cache s_MaxAge = {0}."</summary>
            Public Const net_log_cache_max_age_cache_s_max_age$ = "net_log_cache_max_age_cache_s_max_age"
            ''' <summary>Key for resource getting something like "[MaxAge] Cache Expires - Date = {0}, Expires: {1}."</summary>
            Public Const net_log_cache_max_age_expires_date$ = "net_log_cache_max_age_expires_date"
            ''' <summary>Key for resource getting something like "[MaxAge] Cache MaxAge = {0}."</summary>
            Public Const net_log_cache_max_age_cache_max_age$ = "net_log_cache_max_age_cache_max_age"
            ''' <summary>Key for resource getting something like "[MaxAge] Cannot compute Cache MaxAge, use 10% since LastModified: {0}, LastModified: {1}."</summary>
            Public Const net_log_cache_no_max_age_use_10_percent$ = "net_log_cache_no_max_age_use_10_percent"
            ''' <summary>Key for resource getting something like "[MaxAge] Cannot compute Cache MaxAge, using default RequestCacheValidator.UnspecifiedMaxAge: {0}."</summary>
            Public Const net_log_cache_no_max_age_use_default$ = "net_log_cache_no_max_age_use_default"
            ''' <summary>Key for resource getting something like "This validator should not be called for policy : {0}."</summary>
            Public Const net_log_cache_validator_invalid_for_policy$ = "net_log_cache_validator_invalid_for_policy"
            ''' <summary>Key for resource getting something like "Response LastModified={0},  ContentLength= {1}."</summary>
            Public Const net_log_cache_response_last_modified$ = "net_log_cache_response_last_modified"
            ''' <summary>Key for resource getting something like "Cache    LastModified={0},  ContentLength= {1}."</summary>
            Public Const net_log_cache_cache_last_modified$ = "net_log_cache_cache_last_modified"
            ''' <summary>Key for resource getting something like "A Cache Entry is partial and the user request has non zero ContentOffset = {0}. A restart from cache is not supported for partial cache entries."</summary>
            Public Const net_log_cache_partial_and_non_zero_content_offset$ = "net_log_cache_partial_and_non_zero_content_offset"
            ''' <summary>Key for resource getting something like "Response is valid based on Policy = {0}."</summary>
            Public Const net_log_cache_response_valid_based_on_policy$ = "net_log_cache_response_valid_based_on_policy"
            ''' <summary>Key for resource getting something like "Response is null so this Request should fail."</summary>
            Public Const net_log_cache_null_response_failure$ = "net_log_cache_null_response_failure"
            ''' <summary>Key for resource getting something like "FTP Response Status={0}, {1}."</summary>
            Public Const net_log_cache_ftp_response_status$ = "net_log_cache_ftp_response_status"
            ''' <summary>Key for resource getting something like "Accept this response as valid based on the retry count = {0}."</summary>
            Public Const net_log_cache_resp_valid_based_on_retry$ = "net_log_cache_resp_valid_based_on_retry"
            ''' <summary>Key for resource getting something like "Cache is not updated based on the request Method = {0}."</summary>
            Public Const net_log_cache_no_update_based_on_method$ = "net_log_cache_no_update_based_on_method"
            ''' <summary>Key for resource getting something like "Existing entry is removed because it was found invalid."</summary>
            Public Const net_log_cache_removed_existing_invalid_entry$ = "net_log_cache_removed_existing_invalid_entry"
            ''' <summary>Key for resource getting something like "Cache is not updated based on Policy = {0}."</summary>
            Public Const net_log_cache_not_updated_based_on_policy$ = "net_log_cache_not_updated_based_on_policy"
            ''' <summary>Key for resource getting something like "Cache is not updated because there is no response associated with the request."</summary>
            Public Const net_log_cache_not_updated_because_no_response$ = "net_log_cache_not_updated_because_no_response"
            ''' <summary>Key for resource getting something like "Existing cache entry is removed based on the request Method = {0}."</summary>
            Public Const net_log_cache_removed_existing_based_on_method$ = "net_log_cache_removed_existing_based_on_method"
            ''' <summary>Key for resource getting something like "Existing cache entry should but cannot be removed due to unexpected response Status = ({0}) {1}."</summary>
            Public Const net_log_cache_existing_not_removed_because_unexpected_response_status$ = "net_log_cache_existing_not_removed_because_unexpected_response_status"
            ''' <summary>Key for resource getting something like "Existing cache entry is removed based on Policy = {0}."</summary>
            Public Const net_log_cache_removed_existing_based_on_policy$ = "net_log_cache_removed_existing_based_on_policy"
            ''' <summary>Key for resource getting something like "Cache is not updated based on the FTP response status. Expected = {0}, actual = {1}."</summary>
            Public Const net_log_cache_not_updated_based_on_ftp_response_status$ = "net_log_cache_not_updated_based_on_ftp_response_status"
            ''' <summary>Key for resource getting something like "Cache update is not supported for restarted FTP responses. Restart offset = {0}."</summary>
            Public Const net_log_cache_update_not_supported_for_ftp_restart$ = "net_log_cache_update_not_supported_for_ftp_restart"
            ''' <summary>Key for resource getting something like "Existing cache entry is removed since a restarted response was changed on the server, cache LastModified date = {0}, new LastModified date = {1}."</summary>
            Public Const net_log_cache_removed_entry_because_ftp_restart_response_changed$ = "net_log_cache_removed_entry_because_ftp_restart_response_changed"
            ''' <summary>Key for resource getting something like "The cache entry last synchronized time = {0}."</summary>
            Public Const net_log_cache_last_synchronized$ = "net_log_cache_last_synchronized"
            ''' <summary>Key for resource getting something like "Suppressing cache update since the entry was synchronized within the last minute."</summary>
            Public Const net_log_cache_suppress_update_because_synched_last_minute$ = "net_log_cache_suppress_update_because_synched_last_minute"
            ''' <summary>Key for resource getting something like "Updating cache entry last synchronized time = {0}."</summary>
            Public Const net_log_cache_updating_last_synchronized$ = "net_log_cache_updating_last_synchronized"
            ''' <summary>Key for resource getting something like "{0} Cannot Remove (throw): Key = {1}, Error = {2}."</summary>
            Public Const net_log_cache_cannot_remove$ = "net_log_cache_cannot_remove"
            ''' <summary>Key for resource getting something like "{0}, Key = {1}, -> Status = {2}."</summary>
            Public Const net_log_cache_key_status$ = "net_log_cache_key_status"
            ''' <summary>Key for resource getting something like "{0}, Key = {1}, Remove operation failed -> Status = {2}."</summary>
            Public Const net_log_cache_key_remove_failed_status$ = "net_log_cache_key_remove_failed_status"
            ''' <summary>Key for resource getting something like "{0}, UseCount = {1}, File = {2}."</summary>
            Public Const net_log_cache_usecount_file$ = "net_log_cache_usecount_file"
            ''' <summary>Key for resource getting something like "{0}, stream = {1}."</summary>
            Public Const net_log_cache_stream$ = "net_log_cache_stream"
            ''' <summary>Key for resource getting something like "{0} -> Filename = {1}, Status = {2}."</summary>
            Public Const net_log_cache_filename$ = "net_log_cache_filename"
            ''' <summary>Key for resource getting something like "{0}, Lookup operation failed -> {1}."</summary>
            Public Const net_log_cache_lookup_failed$ = "net_log_cache_lookup_failed"
            ''' <summary>Key for resource getting something like "{0}, Exception = {1}."</summary>
            Public Const net_log_cache_exception$ = "net_log_cache_exception"
            ''' <summary>Key for resource getting something like "Expected length (0=none)= {0}."</summary>
            Public Const net_log_cache_expected_length$ = "net_log_cache_expected_length"
            ''' <summary>Key for resource getting something like "LastModified    (0=none)= {0}."</summary>
            Public Const net_log_cache_last_modified$ = "net_log_cache_last_modified"
            ''' <summary>Key for resource getting something like "Expires         (0=none)= {0}."</summary>
            Public Const net_log_cache_expires$ = "net_log_cache_expires"
            ''' <summary>Key for resource getting something like "MaxStale (sec)          = {0}."</summary>
            Public Const net_log_cache_max_stale$ = "net_log_cache_max_stale"
            ''' <summary>Key for resource getting something like "...Dumping Metadata... "</summary>
            Public Const net_log_cache_dumping_metadata$ = "net_log_cache_dumping_metadata"
            ''' <summary>Key for resource getting something like "Create operation failed -> {0}."</summary>
            Public Const net_log_cache_create_failed$ = "net_log_cache_create_failed"
            ''' <summary>Key for resource getting something like "Set Expires               ={0}."</summary>
            Public Const net_log_cache_set_expires$ = "net_log_cache_set_expires"
            ''' <summary>Key for resource getting something like "Set LastModified          ={0}."</summary>
            Public Const net_log_cache_set_last_modified$ = "net_log_cache_set_last_modified"
            ''' <summary>Key for resource getting something like "Set LastSynchronized      ={0}."</summary>
            Public Const net_log_cache_set_last_synchronized$ = "net_log_cache_set_last_synchronized"
            ''' <summary>Key for resource getting something like "Enable MaxStale (sec) ={0}."</summary>
            Public Const net_log_cache_enable_max_stale$ = "net_log_cache_enable_max_stale"
            ''' <summary>Key for resource getting something like "Disable MaxStale (set to 0)."</summary>
            Public Const net_log_cache_disable_max_stale$ = "net_log_cache_disable_max_stale"
            ''' <summary>Key for resource getting something like "Set new Metadata."</summary>
            Public Const net_log_cache_set_new_metadata$ = "net_log_cache_set_new_metadata"
            ''' <summary>Key for resource getting something like "...Dumping... "</summary>
            Public Const net_log_cache_dumping$ = "net_log_cache_dumping"
            ''' <summary>Key for resource getting something like "{0}, Key = {1}."</summary>
            Public Const net_log_cache_key$ = "net_log_cache_key"
            ''' <summary>Key for resource getting something like "{0}, Nothing was written to the stream, do not commit that cache entry."</summary>
            Public Const net_log_cache_no_commit$ = "net_log_cache_no_commit"
            ''' <summary>Key for resource getting something like "{0}, Error deleting a Filename = {1}."</summary>
            Public Const net_log_cache_error_deleting_filename$ = "net_log_cache_error_deleting_filename"
            ''' <summary>Key for resource getting something like "{0}, Key = {1}, Update operation failed -> {2}."</summary>
            Public Const net_log_cache_update_failed$ = "net_log_cache_update_failed"
            ''' <summary>Key for resource getting something like "{0}, Key = {1}, Delete operation failed -> {2}."</summary>
            Public Const net_log_cache_delete_failed$ = "net_log_cache_delete_failed"
            ''' <summary>Key for resource getting something like "{0}, Key = {1}, Commit operation failed -> {2}."</summary>
            Public Const net_log_cache_commit_failed$ = "net_log_cache_commit_failed"
            ''' <summary>Key for resource getting something like "{0}, Key = {1}, Committed entry as partial, not cached bytes count = {2}."</summary>
            Public Const net_log_cache_committed_as_partial$ = "net_log_cache_committed_as_partial"
            ''' <summary>Key for resource getting something like "{0}, MaxStale = {1}, Update Status = {2}."</summary>
            Public Const net_log_cache_max_stale_and_update_status$ = "net_log_cache_max_stale_and_update_status"
            ''' <summary>Key for resource getting something like "Failing request with the WebExceptionStatus = {0}."</summary>
            Public Const net_log_cache_failing_request_with_exception$ = "net_log_cache_failing_request_with_exception"
            ''' <summary>Key for resource getting something like "Request Method = {0}."</summary>
            Public Const net_log_cache_request_method$ = "net_log_cache_request_method"
            ''' <summary>Key for resource getting something like "Cannot Parse Cache HTTP Status Line: {0}."</summary>
            Public Const net_log_cache_http_status_parse_failure$ = "net_log_cache_http_status_parse_failure"
            ''' <summary>Key for resource getting something like "Entry Status Line = HTTP/{0} {1} {2}."</summary>
            Public Const net_log_cache_http_status_line$ = "net_log_cache_http_status_line"
            ''' <summary>Key for resource getting something like "Cache Cache-Control = {0}."</summary>
            Public Const net_log_cache_cache_control$ = "net_log_cache_cache_control"
            ''' <summary>Key for resource getting something like "The cached version is invalid, assuming HTTP 1.0."</summary>
            Public Const net_log_cache_invalid_http_version$ = "net_log_cache_invalid_http_version"
            ''' <summary>Key for resource getting something like "This Cache Entry does not carry HTTP response headers."</summary>
            Public Const net_log_cache_no_http_response_header$ = "net_log_cache_no_http_response_header"
            ''' <summary>Key for resource getting something like "Cannot parse HTTP headers in entry metadata, offending string: {0}."</summary>
            Public Const net_log_cache_http_header_parse_error$ = "net_log_cache_http_header_parse_error"
            ''' <summary>Key for resource getting something like "Cannot parse all strings in system metadata as "name:value", offending string: {0}."</summary>
            Public Const net_log_cache_metadata_name_value_parse_error$ = "net_log_cache_metadata_name_value_parse_error"
            ''' <summary>Key for resource getting something like "Invalid format of Response Content-Range:{0}."</summary>
            Public Const net_log_cache_content_range_error$ = "net_log_cache_content_range_error"
            ''' <summary>Key for resource getting something like "Invalid CacheControl header = {0}."</summary>
            Public Const net_log_cache_cache_control_error$ = "net_log_cache_cache_control_error"
            ''' <summary>Key for resource getting something like "The cache protocol method {0} has returned unexpected status: {1}."</summary>
            Public Const net_log_cache_unexpected_status$ = "net_log_cache_unexpected_status"
            ''' <summary>Key for resource getting something like "{0} exception: {1}."</summary>
            Public Const net_log_cache_object_and_exception$ = "net_log_cache_object_and_exception"
            ''' <summary>Key for resource getting something like "{0}, No cache entry revalidation is needed."</summary>
            Public Const net_log_cache_revalidation_not_needed$ = "net_log_cache_revalidation_not_needed"
            ''' <summary>Key for resource getting something like "{0}, Cache is not updated based on the current cache protocol status = {1}."</summary>
            Public Const net_log_cache_not_updated_based_on_cache_protocol_status$ = "net_log_cache_not_updated_based_on_cache_protocol_status"
            ''' <summary>Key for resource getting something like "{0}: {1} Closing effective cache stream, type = {2}, cache entry key = {3}."</summary>
            Public Const net_log_cache_closing_cache_stream$ = "net_log_cache_closing_cache_stream"
            ''' <summary>Key for resource getting something like "{0}: an exception (ignored) on {1} = {2}."</summary>
            Public Const net_log_cache_exception_ignored$ = "net_log_cache_exception_ignored"
            ''' <summary>Key for resource getting something like "{0} has requested a cache response but the entry does not exist (Stream.Null)."</summary>
            Public Const net_log_cache_no_cache_entry$ = "net_log_cache_no_cache_entry"
            ''' <summary>Key for resource getting something like "{0} has requested a cache response but the cached stream is null."</summary>
            Public Const net_log_cache_null_cached_stream$ = "net_log_cache_null_cached_stream"
            ''' <summary>Key for resource getting something like "{0} has requested a combined response but the cached stream is null."</summary>
            Public Const net_log_cache_requested_combined_but_null_cached_stream$ = "net_log_cache_requested_combined_but_null_cached_stream"
            ''' <summary>Key for resource getting something like "{0} has returned a range cache stream, Offset = {1}, Length = {2}."</summary>
            Public Const net_log_cache_returned_range_cache$ = "net_log_cache_returned_range_cache"
            ''' <summary>Key for resource getting something like "{0}, Cache Entry not found, freshness result = Undefined."</summary>
            Public Const net_log_cache_entry_not_found_freshness_undefined$ = "net_log_cache_entry_not_found_freshness_undefined"
            ''' <summary>Key for resource getting something like "...Dumping Cache Context..."</summary>
            Public Const net_log_cache_dumping_cache_context$ = "net_log_cache_dumping_cache_context"
            ''' <summary>Key for resource getting something like "{0}, result = {1}."</summary>
            Public Const net_log_cache_result$ = "net_log_cache_result"
            ''' <summary>Key for resource getting something like "Request Uri has a Query, and no explicit expiration time is provided."</summary>
            Public Const net_log_cache_uri_with_query_has_no_expiration$ = "net_log_cache_uri_with_query_has_no_expiration"
            ''' <summary>Key for resource getting something like "Request Uri has a Query, and cached response is from HTTP 1.0 server."</summary>
            Public Const net_log_cache_uri_with_query_and_cached_resp_from_http_10$ = "net_log_cache_uri_with_query_and_cached_resp_from_http_10"
            ''' <summary>Key for resource getting something like "Valid as fresh or because of Cache Policy = {0}."</summary>
            Public Const net_log_cache_valid_as_fresh_or_because_policy$ = "net_log_cache_valid_as_fresh_or_because_policy"
            ''' <summary>Key for resource getting something like "Accept this response base on the retry count = {0}."</summary>
            Public Const net_log_cache_accept_based_on_retry_count$ = "net_log_cache_accept_based_on_retry_count"
            ''' <summary>Key for resource getting something like "Response Date header value is older than that of the cache entry."</summary>
            Public Const net_log_cache_date_header_older_than_cache_entry$ = "net_log_cache_date_header_older_than_cache_entry"
            ''' <summary>Key for resource getting something like "Server did not satisfy the range: {0}."</summary>
            Public Const net_log_cache_server_didnt_satisfy_range$ = "net_log_cache_server_didnt_satisfy_range"
            ''' <summary>Key for resource getting something like "304 response was received on an unconditional request."</summary>
            Public Const net_log_cache_304_received_on_unconditional_request$ = "net_log_cache_304_received_on_unconditional_request"
            ''' <summary>Key for resource getting something like "304 response was received on an unconditional request, but expected response code is 200 or 206."</summary>
            Public Const net_log_cache_304_received_on_unconditional_request_expected_200_206$ = "net_log_cache_304_received_on_unconditional_request_expected_200_206"
            ''' <summary>Key for resource getting something like "HTTP 1.0 Response Last-Modified header value is older than that of the cache entry."</summary>
            Public Const net_log_cache_last_modified_header_older_than_cache_entry$ = "net_log_cache_last_modified_header_older_than_cache_entry"
            ''' <summary>Key for resource getting something like "Response freshness is not within the specified policy limits."</summary>
            Public Const net_log_cache_freshness_outside_policy_limits$ = "net_log_cache_freshness_outside_policy_limits"
            ''' <summary>Key for resource getting something like "Need to remove an invalid cache entry with status code == 304(NotModified)."</summary>
            Public Const net_log_cache_need_to_remove_invalid_cache_entry_304$ = "net_log_cache_need_to_remove_invalid_cache_entry_304"
            ''' <summary>Key for resource getting something like "Response Status = {0}."</summary>
            Public Const net_log_cache_resp_status$ = "net_log_cache_resp_status"
            ''' <summary>Key for resource getting something like "Response==304 or Request was HEAD, updating cache entry."</summary>
            Public Const net_log_cache_resp_304_or_request_head$ = "net_log_cache_resp_304_or_request_head"
            ''' <summary>Key for resource getting something like "Do not update Cached Headers."</summary>
            Public Const net_log_cache_dont_update_cached_headers$ = "net_log_cache_dont_update_cached_headers"
            ''' <summary>Key for resource getting something like "Update Cached Headers."</summary>
            Public Const net_log_cache_update_cached_headers$ = "net_log_cache_update_cached_headers"
            ''' <summary>Key for resource getting something like "A partial response is not combined with existing cache entry, Cache Stream Size = {0}, response Range Start = {1}."</summary>
            Public Const net_log_cache_partial_resp_not_combined_with_existing_entry$ = "net_log_cache_partial_resp_not_combined_with_existing_entry"
            ''' <summary>Key for resource getting something like "User Request contains a conditional header."</summary>
            Public Const net_log_cache_request_contains_conditional_header$ = "net_log_cache_request_contains_conditional_header"
            ''' <summary>Key for resource getting something like "This was Not a GET, HEAD or POST request."</summary>
            Public Const net_log_cache_not_a_get_head_post$ = "net_log_cache_not_a_get_head_post"
            ''' <summary>Key for resource getting something like "Cannot update cache if Response status == 304 and a cache entry was not found."</summary>
            Public Const net_log_cache_cannot_update_cache_if_304$ = "net_log_cache_cannot_update_cache_if_304"
            ''' <summary>Key for resource getting something like "Cannot update cache with HEAD response if the cache entry does not exist."</summary>
            Public Const net_log_cache_cannot_update_cache_with_head_resp$ = "net_log_cache_cannot_update_cache_with_head_resp"
            ''' <summary>Key for resource getting something like "HttpWebResponse is null."</summary>
            Public Const net_log_cache_http_resp_is_null$ = "net_log_cache_http_resp_is_null"
            ''' <summary>Key for resource getting something like "Response Cache-Control = no-store."</summary>
            Public Const net_log_cache_resp_cache_control_is_no_store$ = "net_log_cache_resp_cache_control_is_no_store"
            ''' <summary>Key for resource getting something like "Response Cache-Control = public."</summary>
            Public Const net_log_cache_resp_cache_control_is_public$ = "net_log_cache_resp_cache_control_is_public"
            ''' <summary>Key for resource getting something like "Response Cache-Control = private, and Cache is public."</summary>
            Public Const net_log_cache_resp_cache_control_is_private$ = "net_log_cache_resp_cache_control_is_private"
            ''' <summary>Key for resource getting something like "Response Cache-Control = private+Headers, removing those headers."</summary>
            Public Const net_log_cache_resp_cache_control_is_private_plus_headers$ = "net_log_cache_resp_cache_control_is_private_plus_headers"
            ''' <summary>Key for resource getting something like "HttpWebResponse date is older than of the cached one."</summary>
            Public Const net_log_cache_resp_older_than_cache$ = "net_log_cache_resp_older_than_cache"
            ''' <summary>Key for resource getting something like "Response revalidation is always required but neither Last-Modified nor ETag header is set on the response."</summary>
            Public Const net_log_cache_revalidation_required$ = "net_log_cache_revalidation_required"
            ''' <summary>Key for resource getting something like "Response can be cached although it will always require revalidation."</summary>
            Public Const net_log_cache_needs_revalidation$ = "net_log_cache_needs_revalidation"
            ''' <summary>Key for resource getting something like "Response explicitly allows caching = Cache-Control: {0}."</summary>
            Public Const net_log_cache_resp_allows_caching$ = "net_log_cache_resp_allows_caching"
            ''' <summary>Key for resource getting something like "Request carries Authorization Header and no s-maxage, proxy-revalidate or public directive found."</summary>
            Public Const net_log_cache_auth_header_and_no_s_max_age$ = "net_log_cache_auth_header_and_no_s_max_age"
            ''' <summary>Key for resource getting something like "POST Response without Cache-Control or Expires headers."</summary>
            Public Const net_log_cache_post_resp_without_cache_control_or_expires$ = "net_log_cache_post_resp_without_cache_control_or_expires"
            ''' <summary>Key for resource getting something like "Valid based on Status Code: {0}."</summary>
            Public Const net_log_cache_valid_based_on_status_code$ = "net_log_cache_valid_based_on_status_code"
            ''' <summary>Key for resource getting something like "Response with no CacheControl and Status Code = {0}."</summary>
            Public Const net_log_cache_resp_no_cache_control$ = "net_log_cache_resp_no_cache_control"
            ''' <summary>Key for resource getting something like "Cache Age = {0}, Cache MaxAge = {1}."</summary>
            Public Const net_log_cache_age$ = "net_log_cache_age"
            ''' <summary>Key for resource getting something like "Client Policy MinFresh = {0}."</summary>
            Public Const net_log_cache_policy_min_fresh$ = "net_log_cache_policy_min_fresh"
            ''' <summary>Key for resource getting something like "Client Policy MaxAge = {0}."</summary>
            Public Const net_log_cache_policy_max_age$ = "net_log_cache_policy_max_age"
            ''' <summary>Key for resource getting something like "Client Policy CacheSyncDate (UTC) = {0}, Cache LastSynchronizedUtc = {1}."</summary>
            Public Const net_log_cache_policy_cache_sync_date$ = "net_log_cache_policy_cache_sync_date"
            ''' <summary>Key for resource getting something like "Client Policy MaxStale = {0}."</summary>
            Public Const net_log_cache_policy_max_stale$ = "net_log_cache_policy_max_stale"
            ''' <summary>Key for resource getting something like "Cached CacheControl = no-cache."</summary>
            Public Const net_log_cache_control_no_cache$ = "net_log_cache_control_no_cache"
            ''' <summary>Key for resource getting something like "Cached CacheControl = no-cache, Removing some headers."</summary>
            Public Const net_log_cache_control_no_cache_removing_some_headers$ = "net_log_cache_control_no_cache_removing_some_headers"
            ''' <summary>Key for resource getting something like "Cached CacheControl = must-revalidate and Cache is not fresh."</summary>
            Public Const net_log_cache_control_must_revalidate$ = "net_log_cache_control_must_revalidate"
            ''' <summary>Key for resource getting something like "The cached entry has Authorization Header and cache is not fresh."</summary>
            Public Const net_log_cache_cached_auth_header$ = "net_log_cache_cached_auth_header"
            ''' <summary>Key for resource getting something like "The cached entry has Authorization Header and no Cache-Control directive present that would allow to use that entry."</summary>
            Public Const net_log_cache_cached_auth_header_no_control_directive$ = "net_log_cache_cached_auth_header_no_control_directive"
            ''' <summary>Key for resource getting something like "After Response Cache Validation."</summary>
            Public Const net_log_cache_after_validation$ = "net_log_cache_after_validation"
            ''' <summary>Key for resource getting something like "Response status == 304 but the cache entry does not exist."</summary>
            Public Const net_log_cache_resp_status_304$ = "net_log_cache_resp_status_304"
            ''' <summary>Key for resource getting something like "A response resulted from a HEAD request has different Content-Length header."</summary>
            Public Const net_log_cache_head_resp_has_different_content_length$ = "net_log_cache_head_resp_has_different_content_length"
            ''' <summary>Key for resource getting something like "A response resulted from a HEAD request has different Content-MD5 header."</summary>
            Public Const net_log_cache_head_resp_has_different_content_md5$ = "net_log_cache_head_resp_has_different_content_md5"
            ''' <summary>Key for resource getting something like "A response resulted from a HEAD request has different ETag header."</summary>
            Public Const net_log_cache_head_resp_has_different_etag$ = "net_log_cache_head_resp_has_different_etag"
            ''' <summary>Key for resource getting something like "A 304 response resulted from a HEAD request has different Last-Modified header."</summary>
            Public Const net_log_cache_304_head_resp_has_different_last_modified$ = "net_log_cache_304_head_resp_has_different_last_modified"
            ''' <summary>Key for resource getting something like "An existing cache entry has to be discarded."</summary>
            Public Const net_log_cache_existing_entry_has_to_be_discarded$ = "net_log_cache_existing_entry_has_to_be_discarded"
            ''' <summary>Key for resource getting something like "An existing cache entry should be discarded."</summary>
            Public Const net_log_cache_existing_entry_should_be_discarded$ = "net_log_cache_existing_entry_should_be_discarded"
            ''' <summary>Key for resource getting something like "A 206 Response has been received and either ETag or Last-Modified header value does not match cache entry."</summary>
            Public Const net_log_cache_206_resp_non_matching_entry$ = "net_log_cache_206_resp_non_matching_entry"
            ''' <summary>Key for resource getting something like "The starting position for 206 Response is not adjusted to the end of cache entry."</summary>
            Public Const net_log_cache_206_resp_starting_position_not_adjusted$ = "net_log_cache_206_resp_starting_position_not_adjusted"
            ''' <summary>Key for resource getting something like "Creation of a combined response has been requested from the cache protocol."</summary>
            Public Const net_log_cache_combined_resp_requested$ = "net_log_cache_combined_resp_requested"
            ''' <summary>Key for resource getting something like "Updating headers on 304 response."</summary>
            Public Const net_log_cache_updating_headers_on_304$ = "net_log_cache_updating_headers_on_304"
            ''' <summary>Key for resource getting something like "Suppressing cache headers update on 304, new headers don't add anything."</summary>
            Public Const net_log_cache_suppressing_headers_update_on_304$ = "net_log_cache_suppressing_headers_update_on_304"
            ''' <summary>Key for resource getting something like "A Response Status Code is not 304 or 206."</summary>
            Public Const net_log_cache_status_code_not_304_206$ = "net_log_cache_status_code_not_304_206"
            ''' <summary>Key for resource getting something like "A 5XX Response and Cache-Only like policy, serving from cache."</summary>
            Public Const net_log_cache_sxx_resp_cache_only$ = "net_log_cache_sxx_resp_cache_only"
            ''' <summary>Key for resource getting something like "A 5XX Response that can be replaced by existing cache entry."</summary>
            Public Const net_log_cache_sxx_resp_can_be_replaced$ = "net_log_cache_sxx_resp_can_be_replaced"
            ''' <summary>Key for resource getting something like "Cache entry Vary header is empty."</summary>
            Public Const net_log_cache_vary_header_empty$ = "net_log_cache_vary_header_empty"
            ''' <summary>Key for resource getting something like "Cache entry Vary header contains '*'."</summary>
            Public Const net_log_cache_vary_header_contains_asterisks$ = "net_log_cache_vary_header_contains_asterisks"
            ''' <summary>Key for resource getting something like "No request headers are found in cached metadata to test based on the cached response Vary header."</summary>
            Public Const net_log_cache_no_headers_in_metadata$ = "net_log_cache_no_headers_in_metadata"
            ''' <summary>Key for resource getting something like "Vary header: Request and cache header fields count does not match, header name = {0}."</summary>
            Public Const net_log_cache_vary_header_mismatched_count$ = "net_log_cache_vary_header_mismatched_count"
            ''' <summary>Key for resource getting something like "Vary header: A Cache header field mismatch the request one, header name = {0}, cache field = {1}, request field = {2}."</summary>
            Public Const net_log_cache_vary_header_mismatched_field$ = "net_log_cache_vary_header_mismatched_field"
            ''' <summary>Key for resource getting something like "All required Request headers match based on cached Vary response header."</summary>
            Public Const net_log_cache_vary_header_match$ = "net_log_cache_vary_header_match"
            ''' <summary>Key for resource getting something like "Request Range (not in Cache yet) = Range:{0}."</summary>
            Public Const net_log_cache_range$ = "net_log_cache_range"
            ''' <summary>Key for resource getting something like "Invalid format of Request Range:{0}."</summary>
            Public Const net_log_cache_range_invalid_format$ = "net_log_cache_range_invalid_format"
            ''' <summary>Key for resource getting something like "Cannot serve from Cache, Range:{0}."</summary>
            Public Const net_log_cache_range_not_in_cache$ = "net_log_cache_range_not_in_cache"
            ''' <summary>Key for resource getting something like "Serving Request Range from cache, Range:{0}."</summary>
            Public Const net_log_cache_range_in_cache$ = "net_log_cache_range_in_cache"
            ''' <summary>Key for resource getting something like "Serving Partial Response (206) from cache, Content-Range:{0}."</summary>
            Public Const net_log_cache_partial_resp$ = "net_log_cache_partial_resp"
            ''' <summary>Key for resource getting something like "Range Request (user specified), Range: {0}."</summary>
            Public Const net_log_cache_range_request_range$ = "net_log_cache_range_request_range"
            ''' <summary>Key for resource getting something like "Could be a Partial Cached Response, Size = {0}, Response Content Length = {1}."</summary>
            Public Const net_log_cache_could_be_partial$ = "net_log_cache_could_be_partial"
            ''' <summary>Key for resource getting something like "Request Condition = If-None-Match:{0}."</summary>
            Public Const net_log_cache_condition_if_none_match$ = "net_log_cache_condition_if_none_match"
            ''' <summary>Key for resource getting something like "Request Condition = If-Modified-Since:{0}."</summary>
            Public Const net_log_cache_condition_if_modified_since$ = "net_log_cache_condition_if_modified_since"
            ''' <summary>Key for resource getting something like "A Conditional Request cannot be constructed."</summary>
            Public Const net_log_cache_cannot_construct_conditional_request$ = "net_log_cache_cannot_construct_conditional_request"
            ''' <summary>Key for resource getting something like "A Conditional Range request cannot be constructed."</summary>
            Public Const net_log_cache_cannot_construct_conditional_range_request$ = "net_log_cache_cannot_construct_conditional_range_request"
            ''' <summary>Key for resource getting something like "Cached Entry Size = {0} is too big, cannot do a range request."</summary>
            Public Const net_log_cache_entry_size_too_big$ = "net_log_cache_entry_size_too_big"
            ''' <summary>Key for resource getting something like "Request Condition = If-Range:{0}."</summary>
            Public Const net_log_cache_condition_if_range$ = "net_log_cache_condition_if_range"
            ''' <summary>Key for resource getting something like "A Conditional Range request on Http &lt;= 1.0 is not implemented."</summary>
            Public Const net_log_cache_conditional_range_not_implemented_on_http_10$ = "net_log_cache_conditional_range_not_implemented_on_http_10"
            ''' <summary>Key for resource getting something like "Saving Request Headers, Vary: {0}."</summary>
            Public Const net_log_cache_saving_request_headers$ = "net_log_cache_saving_request_headers"
            ''' <summary>Key for resource getting something like "Ranges other than bytes are not implemented."</summary>
            Public Const net_log_cache_only_byte_range_implemented$ = "net_log_cache_only_byte_range_implemented"
            ''' <summary>Key for resource getting something like "Multiple/complexe ranges are not implemented."</summary>
            Public Const net_log_cache_multiple_complex_range_not_implemented$ = "net_log_cache_multiple_complex_range_not_implemented"
            ''' <summary>Key for resource getting something like "unknown"</summary>
            Public Const net_log_unknown$ = "net_log_unknown"
            ''' <summary>Key for resource getting something like "{0} returned {1}."</summary>
            Public Const net_log_operation_returned_something$ = "net_log_operation_returned_something"
            ''' <summary>Key for resource getting something like "{0} failed with error {1}."</summary>
            Public Const net_log_operation_failed_with_error$ = "net_log_operation_failed_with_error"
            ''' <summary>Key for resource getting something like "Buffered {0} bytes."</summary>
            Public Const net_log_buffered_n_bytes$ = "net_log_buffered_n_bytes"
            ''' <summary>Key for resource getting something like "Method={0}."</summary>
            Public Const net_log_method_equal$ = "net_log_method_equal"
            ''' <summary>Key for resource getting something like "Releasing FTP connection#{0}."</summary>
            Public Const net_log_releasing_connection$ = "net_log_releasing_connection"
            ''' <summary>Key for resource getting something like "Unexpected exception in {0}."</summary>
            Public Const net_log_unexpected_exception$ = "net_log_unexpected_exception"
            ''' <summary>Key for resource getting something like "Error code {0} was received from server response."</summary>
            Public Const net_log_server_response_error_code$ = "net_log_server_response_error_code"
            ''' <summary>Key for resource getting something like "Resubmitting request."</summary>
            Public Const net_log_resubmitting_request$ = "net_log_resubmitting_request"
            ''' <summary>Key for resource getting something like "An unexpected exception while retrieving the local address list: {0}."</summary>
            Public Const net_log_retrieving_localhost_exception$ = "net_log_retrieving_localhost_exception"
            ''' <summary>Key for resource getting something like "A resolved ServicePoint host could be wrongly considered as a remote server."</summary>
            Public Const net_log_resolved_servicepoint_may_not_be_remote_server$ = "net_log_resolved_servicepoint_may_not_be_remote_server"
            ''' <summary>Key for resource getting something like "Received status line: Version={0}, StatusCode={1}, StatusDescription={2}."</summary>
            Public Const net_log_received_status_line$ = "net_log_received_status_line"
            ''' <summary>Key for resource getting something like "Sending headers&#10;{{&#10;{0}}}."</summary>
            Public Const net_log_sending_headers$ = "net_log_sending_headers"
            ''' <summary>Key for resource getting something like "Received headers&#10;{{&#10;{0}}}."</summary>
            Public Const net_log_received_headers$ = "net_log_received_headers"
            ''' <summary>Key for resource getting something like "ShellServices.ShellExpression.Parse() was called with a badly formatted 'pattern':{0}."</summary>
            Public Const net_log_shell_expression_pattern_format_warning$ = "net_log_shell_expression_pattern_format_warning"
            ''' <summary>Key for resource getting something like "Exception in callback: {0}."</summary>
            Public Const net_log_exception_in_callback$ = "net_log_exception_in_callback"
            ''' <summary>Key for resource getting something like "Sending command [{0}]"</summary>
            Public Const net_log_sending_command$ = "net_log_sending_command"
            ''' <summary>Key for resource getting something like "Received response [{0}]"</summary>
            Public Const net_log_received_response$ = "net_log_received_response"
            ''' <summary>Key for resource getting something like "An invalid character was found in the 7-bit stream."</summary>
            Public Const Mail7BitStreamInvalidCharacter$ = "Mail7BitStreamInvalidCharacter"
            ''' <summary>Key for resource getting something like "The specified string is not in the form required for an e-mail address."</summary>
            Public Const MailAddressInvalidFormat$ = "MailAddressInvalidFormat"
            ''' <summary>Key for resource getting something like "The specified e-mail address is currently not supported."</summary>
            Public Const MailAddressUnsupportedFormat$ = "MailAddressUnsupportedFormat"
            ''' <summary>Key for resource getting something like "The specified string is not in the form required for a subject."</summary>
            Public Const MailSubjectInvalidFormat$ = "MailSubjectInvalidFormat"
            ''' <summary>Key for resource getting something like "An invalid character was found in the Base-64 stream."</summary>
            Public Const MailBase64InvalidCharacter$ = "MailBase64InvalidCharacter"
            ''' <summary>Key for resource getting something like "The collection is read-only."</summary>
            Public Const MailCollectionIsReadOnly$ = "MailCollectionIsReadOnly"
            ''' <summary>Key for resource getting something like "The date is in an invalid format."</summary>
            Public Const MailDateInvalidFormat$ = "MailDateInvalidFormat"
            ''' <summary>Key for resource getting something like "The specified singleton field already exists in the collection and cannot be added."</summary>
            Public Const MailHeaderFieldAlreadyExists$ = "MailHeaderFieldAlreadyExists"
            ''' <summary>Key for resource getting something like "An invalid character was found in the mail header."</summary>
            Public Const MailHeaderFieldInvalidCharacter$ = "MailHeaderFieldInvalidCharacter"
            ''' <summary>Key for resource getting something like "The mail header is malformed."</summary>
            Public Const MailHeaderFieldMalformedHeader$ = "MailHeaderFieldMalformedHeader"
            ''' <summary>Key for resource getting something like "The header name does not match this property."</summary>
            Public Const MailHeaderFieldMismatchedName$ = "MailHeaderFieldMismatchedName"
            ''' <summary>Key for resource getting something like "The index value is outside the bounds of the array."</summary>
            Public Const MailHeaderIndexOutOfBounds$ = "MailHeaderIndexOutOfBounds"
            ''' <summary>Key for resource getting something like "The Item property can only be used with singleton fields."</summary>
            Public Const MailHeaderItemAccessorOnlySingleton$ = "MailHeaderItemAccessorOnlySingleton"
            ''' <summary>Key for resource getting something like "The underlying list has been changed and the enumeration is out of date."</summary>
            Public Const MailHeaderListHasChanged$ = "MailHeaderListHasChanged"
            ''' <summary>Key for resource getting something like "The stream should have been consumed before resetting."</summary>
            Public Const MailHeaderResetCalledBeforeEOF$ = "MailHeaderResetCalledBeforeEOF"
            ''' <summary>Key for resource getting something like "The target array is too small to contain all the headers."</summary>
            Public Const MailHeaderTargetArrayTooSmall$ = "MailHeaderTargetArrayTooSmall"
            ''' <summary>Key for resource getting something like "The ContentID cannot contain a '&lt;' or '>' character."</summary>
            Public Const MailHeaderInvalidCID$ = "MailHeaderInvalidCID"
            ''' <summary>Key for resource getting something like "The SMTP host was not found."</summary>
            Public Const MailHostNotFound$ = "MailHostNotFound"
            ''' <summary>Key for resource getting something like "GetContentStream() can only be called once."</summary>
            Public Const MailReaderGetContentStreamAlreadyCalled$ = "MailReaderGetContentStreamAlreadyCalled"
            ''' <summary>Key for resource getting something like "Premature end of stream."</summary>
            Public Const MailReaderTruncated$ = "MailReaderTruncated"
            ''' <summary>Key for resource getting something like "This operation cannot be performed while in content."</summary>
            Public Const MailWriterIsInContent$ = "MailWriterIsInContent"
            ''' <summary>Key for resource getting something like "Maximum line length too small."</summary>
            Public Const MailWriterLineLengthTooSmall$ = "MailWriterLineLengthTooSmall"
            ''' <summary>Key for resource getting something like "Server does not support secure connections."</summary>
            Public Const MailServerDoesNotSupportStartTls$ = "MailServerDoesNotSupportStartTls"
            ''' <summary>Key for resource getting something like "The server response was: {0}"</summary>
            Public Const MailServerResponse$ = "MailServerResponse"
            ''' <summary>Key for resource getting something like "AuthenticationType and ServicePrincipalName cannot be specified as null for server's SSPI Negotiation module."</summary>
            Public Const SSPIAuthenticationOrSPNNull$ = "SSPIAuthenticationOrSPNNull"
            ''' <summary>Key for resource getting something like "{0} failed with error {1}."</summary>
            Public Const SSPIPInvokeError$ = "SSPIPInvokeError"
            ''' <summary>Key for resource getting something like "'{0}' is not a supported handle type."</summary>
            Public Const SSPIInvalidHandleType$ = "SSPIInvalidHandleType"
            ''' <summary>Key for resource getting something like "Already connected."</summary>
            Public Const SmtpAlreadyConnected$ = "SmtpAlreadyConnected"
            ''' <summary>Key for resource getting something like "Authentication failed."</summary>
            Public Const SmtpAuthenticationFailed$ = "SmtpAuthenticationFailed"
            ''' <summary>Key for resource getting something like "Authentication failed due to lack of credentials."</summary>
            Public Const SmtpAuthenticationFailedNoCreds$ = "SmtpAuthenticationFailedNoCreds"
            ''' <summary>Key for resource getting something like "Data stream is still open."</summary>
            Public Const SmtpDataStreamOpen$ = "SmtpDataStreamOpen"
            ''' <summary>Key for resource getting something like "This is a multi-part MIME message."</summary>
            Public Const SmtpDefaultMimePreamble$ = "SmtpDefaultMimePreamble"
            ''' <summary>Key for resource getting something like "@@SOAP Application Message"</summary>
            Public Const SmtpDefaultSubject$ = "SmtpDefaultSubject"
            ''' <summary>Key for resource getting something like "Smtp server returned an invalid response."</summary>
            Public Const SmtpInvalidResponse$ = "SmtpInvalidResponse"
            ''' <summary>Key for resource getting something like "Not connected."</summary>
            Public Const SmtpNotConnected$ = "SmtpNotConnected"
            ''' <summary>Key for resource getting something like "System status, or system help reply."</summary>
            Public Const SmtpSystemStatus$ = "SmtpSystemStatus"
            ''' <summary>Key for resource getting something like "Help message."</summary>
            Public Const SmtpHelpMessage$ = "SmtpHelpMessage"
            ''' <summary>Key for resource getting something like "Service ready."</summary>
            Public Const SmtpServiceReady$ = "SmtpServiceReady"
            ''' <summary>Key for resource getting something like "Service closing transmission channel."</summary>
            Public Const SmtpServiceClosingTransmissionChannel$ = "SmtpServiceClosingTransmissionChannel"
            ''' <summary>Key for resource getting something like "Completed."</summary>
            Public Const SmtpOK$ = "SmtpOK"
            ''' <summary>Key for resource getting something like "User not local; will forward to specified path."</summary>
            Public Const SmtpUserNotLocalWillForward$ = "SmtpUserNotLocalWillForward"
            ''' <summary>Key for resource getting something like "Start mail input; end with &lt;CRLF>.&lt;CRLF>."</summary>
            Public Const SmtpStartMailInput$ = "SmtpStartMailInput"
            ''' <summary>Key for resource getting something like "Service not available, closing transmission channel."</summary>
            Public Const SmtpServiceNotAvailable$ = "SmtpServiceNotAvailable"
            ''' <summary>Key for resource getting something like "Mailbox unavailable."</summary>
            Public Const SmtpMailboxBusy$ = "SmtpMailboxBusy"
            ''' <summary>Key for resource getting something like "Error in processing."</summary>
            Public Const SmtpLocalErrorInProcessing$ = "SmtpLocalErrorInProcessing"
            ''' <summary>Key for resource getting something like "Insufficient system storage."</summary>
            Public Const SmtpInsufficientStorage$ = "SmtpInsufficientStorage"
            ''' <summary>Key for resource getting something like "Client does not have permission to Send As this sender."</summary>
            Public Const SmtpPermissionDenied$ = "SmtpPermissionDenied"
            ''' <summary>Key for resource getting something like "Syntax error, command unrecognized."</summary>
            Public Const SmtpCommandUnrecognized$ = "SmtpCommandUnrecognized"
            ''' <summary>Key for resource getting something like "Syntax error in parameters or arguments."</summary>
            Public Const SmtpSyntaxError$ = "SmtpSyntaxError"
            ''' <summary>Key for resource getting something like "Command not implemented."</summary>
            Public Const SmtpCommandNotImplemented$ = "SmtpCommandNotImplemented"
            ''' <summary>Key for resource getting something like "Bad sequence of commands."</summary>
            Public Const SmtpBadCommandSequence$ = "SmtpBadCommandSequence"
            ''' <summary>Key for resource getting something like "Command parameter not implemented."</summary>
            Public Const SmtpCommandParameterNotImplemented$ = "SmtpCommandParameterNotImplemented"
            ''' <summary>Key for resource getting something like "Mailbox unavailable."</summary>
            Public Const SmtpMailboxUnavailable$ = "SmtpMailboxUnavailable"
            ''' <summary>Key for resource getting something like "User not local; please try a different path."</summary>
            Public Const SmtpUserNotLocalTryAlternatePath$ = "SmtpUserNotLocalTryAlternatePath"
            ''' <summary>Key for resource getting something like "Exceeded storage allocation."</summary>
            Public Const SmtpExceededStorageAllocation$ = "SmtpExceededStorageAllocation"
            ''' <summary>Key for resource getting something like "Mailbox name not allowed."</summary>
            Public Const SmtpMailboxNameNotAllowed$ = "SmtpMailboxNameNotAllowed"
            ''' <summary>Key for resource getting something like "Transaction failed."</summary>
            Public Const SmtpTransactionFailed$ = "SmtpTransactionFailed"
            ''' <summary>Key for resource getting something like "Failure sending mail."</summary>
            Public Const SmtpSendMailFailure$ = "SmtpSendMailFailure"
            ''' <summary>Key for resource getting something like "Unable to send to a recipient."</summary>
            Public Const SmtpRecipientFailed$ = "SmtpRecipientFailed"
            ''' <summary>Key for resource getting something like "A recipient must be specified."</summary>
            Public Const SmtpRecipientRequired$ = "SmtpRecipientRequired"
            ''' <summary>Key for resource getting something like "A from address must be specified."</summary>
            Public Const SmtpFromRequired$ = "SmtpFromRequired"
            ''' <summary>Key for resource getting something like "Unable to send to all recipients."</summary>
            Public Const SmtpAllRecipientsFailed$ = "SmtpAllRecipientsFailed"
            ''' <summary>Key for resource getting something like "Client does not have permission to submit mail to this server."</summary>
            Public Const SmtpClientNotPermitted$ = "SmtpClientNotPermitted"
            ''' <summary>Key for resource getting something like "The SMTP server requires a secure connection or the client was not authenticated."</summary>
            Public Const SmtpMustIssueStartTlsFirst$ = "SmtpMustIssueStartTlsFirst"
            ''' <summary>Key for resource getting something like "Only absolute directories are allowed for pickup directory."</summary>
            Public Const SmtpNeedAbsolutePickupDirectory$ = "SmtpNeedAbsolutePickupDirectory"
            ''' <summary>Key for resource getting something like "Cannot get IIS pickup directory."</summary>
            Public Const SmtpGetIisPickupDirectoryFailed$ = "SmtpGetIisPickupDirectoryFailed"
            ''' <summary>Key for resource getting something like "SSL must not be enabled for pickup-directory delivery methods."</summary>
            Public Const SmtpPickupDirectoryDoesnotSupportSsl$ = "SmtpPickupDirectoryDoesnotSupportSsl"
            ''' <summary>Key for resource getting something like "Previous operation is still in progress."</summary>
            Public Const SmtpOperationInProgress$ = "SmtpOperationInProgress"
            ''' <summary>Key for resource getting something like "The server returned an invalid response in the authentication handshake."</summary>
            Public Const SmtpAuthResponseInvalid$ = "SmtpAuthResponseInvalid"
            ''' <summary>Key for resource getting something like "The server returned an invalid response to the EHLO command."</summary>
            Public Const SmtpEhloResponseInvalid$ = "SmtpEhloResponseInvalid"
            ''' <summary>Key for resource getting something like "The MIME transfer encoding '{0}' is not supported."</summary>
            Public Const MimeTransferEncodingNotSupported$ = "MimeTransferEncodingNotSupported"
            ''' <summary>Key for resource getting something like "Seeking is not supported on this stream."</summary>
            Public Const SeekNotSupported$ = "SeekNotSupported"
            ''' <summary>Key for resource getting something like "Writing is not supported on this stream."</summary>
            Public Const WriteNotSupported$ = "WriteNotSupported"
            ''' <summary>Key for resource getting something like "Invalid hex digit '{0}'."</summary>
            Public Const InvalidHexDigit$ = "InvalidHexDigit"
            ''' <summary>Key for resource getting something like "The SSPI context is not valid."</summary>
            Public Const InvalidSSPIContext$ = "InvalidSSPIContext"
            ''' <summary>Key for resource getting something like "A null session key was obtained from SSPI."</summary>
            Public Const InvalidSSPIContextKey$ = "InvalidSSPIContextKey"
            ''' <summary>Key for resource getting something like "Invalid SSPI BinaryNegotiationElement."</summary>
            Public Const InvalidSSPINegotiationElement$ = "InvalidSSPINegotiationElement"
            ''' <summary>Key for resource getting something like "An invalid character was found in header name."</summary>
            Public Const InvalidHeaderName$ = "InvalidHeaderName"
            ''' <summary>Key for resource getting something like "An invalid character was found in header value."</summary>
            Public Const InvalidHeaderValue$ = "InvalidHeaderValue"
            ''' <summary>Key for resource getting something like "Cannot get the effective time of the SSPI context."</summary>
            Public Const CannotGetEffectiveTimeOfSSPIContext$ = "CannotGetEffectiveTimeOfSSPIContext"
            ''' <summary>Key for resource getting something like "Cannot get the expiry time of the SSPI context."</summary>
            Public Const CannotGetExpiryTimeOfSSPIContext$ = "CannotGetExpiryTimeOfSSPIContext"
            ''' <summary>Key for resource getting something like "Reading is not supported on this stream."</summary>
            Public Const ReadNotSupported$ = "ReadNotSupported"
            ''' <summary>Key for resource getting something like "The AsyncResult is not valid."</summary>
            Public Const InvalidAsyncResult$ = "InvalidAsyncResult"
            ''' <summary>Key for resource getting something like "The SMTP host was not specified."</summary>
            Public Const UnspecifiedHost$ = "UnspecifiedHost"
            ''' <summary>Key for resource getting something like "The specified port is invalid. The port must be greater than 0."</summary>
            Public Const InvalidPort$ = "InvalidPort"
            ''' <summary>Key for resource getting something like "This operation cannot be performed while a message is being sent."</summary>
            Public Const SmtpInvalidOperationDuringSend$ = "SmtpInvalidOperationDuringSend"
            ''' <summary>Key for resource getting something like "One of the streams has already been used and can't be reset to the origin."</summary>
            Public Const MimePartCantResetStream$ = "MimePartCantResetStream"
            ''' <summary>Key for resource getting something like "The specified media type is invalid."</summary>
            Public Const MediaTypeInvalid$ = "MediaTypeInvalid"
            ''' <summary>Key for resource getting something like "The specified content type is invalid."</summary>
            Public Const ContentTypeInvalid$ = "ContentTypeInvalid"
            ''' <summary>Key for resource getting something like "The specified content disposition is invalid."</summary>
            Public Const ContentDispositionInvalid$ = "ContentDispositionInvalid"
            ''' <summary>Key for resource getting something like "'{0}' is not a valid configuration attribute for type '{1}'."</summary>
            Public Const AttributeNotSupported$ = "AttributeNotSupported"
            ''' <summary>Key for resource getting something like "Cannot remove with null name."</summary>
            Public Const Cannot_remove_with_null$ = "Cannot_remove_with_null"
            ''' <summary>Key for resource getting something like "Only elements allowed."</summary>
            Public Const Config_base_elements_only$ = "Config_base_elements_only"
            ''' <summary>Key for resource getting something like "Child nodes not allowed."</summary>
            Public Const Config_base_no_child_nodes$ = "Config_base_no_child_nodes"
            ''' <summary>Key for resource getting something like "Required attribute '{0}' cannot be empty."</summary>
            Public Const Config_base_required_attribute_empty$ = "Config_base_required_attribute_empty"
            ''' <summary>Key for resource getting something like "Required attribute '{0}' not found."</summary>
            Public Const Config_base_required_attribute_missing$ = "Config_base_required_attribute_missing"
            ''' <summary>Key for resource getting something like "The time span for the property '{0}' exceeds the maximum that can be stored in the configuration."</summary>
            Public Const Config_base_time_overflow$ = "Config_base_time_overflow"
            ''' <summary>Key for resource getting something like "The ConfigurationValidation attribute must be derived from ConfigurationValidation."</summary>
            Public Const Config_base_type_must_be_configurationvalidation$ = "Config_base_type_must_be_configurationvalidation"
            ''' <summary>Key for resource getting something like "The ConfigurationPropertyConverter attribute must be derived from TypeConverter."</summary>
            Public Const Config_base_type_must_be_typeconverter$ = "Config_base_type_must_be_typeconverter"
            ''' <summary>Key for resource getting something like "Unknown"</summary>
            Public Const Config_base_unknown_format$ = "Config_base_unknown_format"
            ''' <summary>Key for resource getting something like "Unrecognized attribute '{0}'. Note that attribute names are case-sensitive."</summary>
            Public Const Config_base_unrecognized_attribute$ = "Config_base_unrecognized_attribute"
            ''' <summary>Key for resource getting something like "Unrecognized element."</summary>
            Public Const Config_base_unrecognized_element$ = "Config_base_unrecognized_element"
            ''' <summary>Key for resource getting something like "The property '{0}' must have value 'true' or 'false'."</summary>
            Public Const Config_invalid_boolean_attribute$ = "Config_invalid_boolean_attribute"
            ''' <summary>Key for resource getting something like "The '{0}' attribute must be set to an integer value."</summary>
            Public Const Config_invalid_integer_attribute$ = "Config_invalid_integer_attribute"
            ''' <summary>Key for resource getting something like "The '{0}' attribute must be set to a positive integer value."</summary>
            Public Const Config_invalid_positive_integer_attribute$ = "Config_invalid_positive_integer_attribute"
            ''' <summary>Key for resource getting something like "The '{0}' attribute must be set to a valid Type name."</summary>
            Public Const Config_invalid_type_attribute$ = "Config_invalid_type_attribute"
            ''' <summary>Key for resource getting something like "The '{0}' attribute must be specified on the '{1}' tag."</summary>
            Public Const Config_missing_required_attribute$ = "Config_missing_required_attribute"
            ''' <summary>Key for resource getting something like "The root element must match the name of the section referencing the file, '{0}'"</summary>
            Public Const Config_name_value_file_section_file_invalid_root$ = "Config_name_value_file_section_file_invalid_root"
            ''' <summary>Key for resource getting something like "Provider must implement the class '{0}'."</summary>
            Public Const Config_provider_must_implement_type$ = "Config_provider_must_implement_type"
            ''' <summary>Key for resource getting something like "Provider name cannot be null or empty."</summary>
            Public Const Config_provider_name_null_or_empty$ = "Config_provider_name_null_or_empty"
            ''' <summary>Key for resource getting something like "The provider was not found in the collection."</summary>
            Public Const Config_provider_not_found$ = "Config_provider_not_found"
            ''' <summary>Key for resource getting something like "Property '{0}' cannot be empty or null."</summary>
            Public Const Config_property_name_cannot_be_empty$ = "Config_property_name_cannot_be_empty"
            ''' <summary>Key for resource getting something like "Cannot clear section handlers.  Section '{0}' is locked."</summary>
            Public Const Config_section_cannot_clear_locked_section$ = "Config_section_cannot_clear_locked_section"
            ''' <summary>Key for resource getting something like "SectionRecord not found."</summary>
            Public Const Config_section_record_not_found$ = "Config_section_record_not_found"
            ''' <summary>Key for resource getting something like "The 'File' property cannot be used with the ConfigSource property."</summary>
            Public Const Config_source_cannot_contain_file$ = "Config_source_cannot_contain_file"
            ''' <summary>Key for resource getting something like "The configuration system can only be set once.  Configuration system is already set"</summary>
            Public Const Config_system_already_set$ = "Config_system_already_set"
            ''' <summary>Key for resource getting something like "Unable to read security policy."</summary>
            Public Const Config_unable_to_read_security_policy$ = "Config_unable_to_read_security_policy"
            ''' <summary>Key for resource getting something like "WriteXml returned null."</summary>
            Public Const Config_write_xml_returned_null$ = "Config_write_xml_returned_null"
            ''' <summary>Key for resource getting something like "Server cannot clear configuration sections from within section groups.  &lt;clear/> must be a child of &lt;configSections>."</summary>
            Public Const Cannot_clear_sections_within_group$ = "Cannot_clear_sections_within_group"
            ''' <summary>Key for resource getting something like "Cannot use a leading .. to exit above the top directory."</summary>
            Public Const Cannot_exit_up_top_directory$ = "Cannot_exit_up_top_directory"
            ''' <summary>Key for resource getting something like "Couldn't create listener '{0}'."</summary>
            Public Const Could_not_create_listener$ = "Could_not_create_listener"
            ''' <summary>Key for resource getting something like "initializeData needs to be a valid file name for TextWriterTraceListener.  "</summary>
            Public Const TextWriterTL_DefaultConstructor_NotSupported$ = "TextWriterTL_DefaultConstructor_NotSupported"
            ''' <summary>Key for resource getting something like "Could not create {0}."</summary>
            Public Const Could_not_create_type_instance$ = "Could_not_create_type_instance"
            ''' <summary>Key for resource getting something like "Couldn't find type for class {0}."</summary>
            Public Const Could_not_find_type$ = "Could_not_find_type"
            ''' <summary>Key for resource getting something like "Couldn't find constructor for class {0}."</summary>
            Public Const Could_not_get_constructor$ = "Could_not_get_constructor"
            ''' <summary>Key for resource getting something like "switchType needs to be a valid class name. It can't be empty."</summary>
            Public Const EmptyTypeName_NotAllowed$ = "EmptyTypeName_NotAllowed"
            ''' <summary>Key for resource getting something like "The specified type, '{0}' is not derived from the appropriate base type, '{1}'."</summary>
            Public Const Incorrect_base_type$ = "Incorrect_base_type"
            ''' <summary>Key for resource getting something like "'switchValue' and 'switchName' cannot both be specified on source '{0}'."</summary>
            Public Const Only_specify_one$ = "Only_specify_one"
            ''' <summary>Key for resource getting something like "This provider instance has already been initialized."</summary>
            Public Const Provider_Already_Initialized$ = "Provider_Already_Initialized"
            ''' <summary>Key for resource getting something like "A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'."</summary>
            Public Const Reference_listener_cant_have_properties$ = "Reference_listener_cant_have_properties"
            ''' <summary>Key for resource getting something like "Listener '{0}' does not exist in the sharedListeners section."</summary>
            Public Const Reference_to_nonexistent_listener$ = "Reference_to_nonexistent_listener"
            ''' <summary>Key for resource getting something like "The settings property '{0}' was not found."</summary>
            Public Const SettingsPropertyNotFound$ = "SettingsPropertyNotFound"
            ''' <summary>Key for resource getting something like "The settings property '{0}' is read-only."</summary>
            Public Const SettingsPropertyReadOnly$ = "SettingsPropertyReadOnly"
            ''' <summary>Key for resource getting something like "The settings property '{0}' is of a non-compatible type."</summary>
            Public Const SettingsPropertyWrongType$ = "SettingsPropertyWrongType"
            ''' <summary>Key for resource getting something like "Could not add trace listener {0} because it is not a subclass of TraceListener."</summary>
            Public Const Type_isnt_tracelistener$ = "Type_isnt_tracelistener"
            ''' <summary>Key for resource getting something like "Could not find a type-converter to convert object if type '{0}' from string."</summary>
            Public Const Unable_to_convert_type_from_string$ = "Unable_to_convert_type_from_string"
            ''' <summary>Key for resource getting something like "Could not find a type-converter to convert object if type '{0}' to string."</summary>
            Public Const Unable_to_convert_type_to_string$ = "Unable_to_convert_type_to_string"
            ''' <summary>Key for resource getting something like "Error in trace switch '{0}': The value of a switch must be integral."</summary>
            Public Const Value_must_be_numeric$ = "Value_must_be_numeric"
            ''' <summary>Key for resource getting something like "The property '{0}' could not be created from it's default value. Error message: {1}"</summary>
            Public Const Could_not_create_from_default_value$ = "Could_not_create_from_default_value"
            ''' <summary>Key for resource getting something like "The property '{0}' could not be created from it's default value because the default value is of a different type."</summary>
            Public Const Could_not_create_from_default_value_2$ = "Could_not_create_from_default_value_2"
            ''' <summary>Key for resource getting something like "The directory name {0} is invalid."</summary>
            Public Const InvalidDirName$ = "InvalidDirName"
            ''' <summary>Key for resource getting something like "Error reading the {0} directory."</summary>
            Public Const FSW_IOError$ = "FSW_IOError"
            ''' <summary>Key for resource getting something like "The character '{0}' in the pattern provided is not valid."</summary>
            Public Const PatternInvalidChar$ = "PatternInvalidChar"
            ''' <summary>Key for resource getting something like "The specified buffer size is too large. FileSystemWatcher cannot allocate {0} bytes for the internal buffer."</summary>
            Public Const BufferSizeTooLarge$ = "BufferSizeTooLarge"
            ''' <summary>Key for resource getting something like "Flag to indicate which change event to monitor."</summary>
            Public Const FSW_ChangedFilter$ = "FSW_ChangedFilter"
            ''' <summary>Key for resource getting something like "Flag to indicate whether this component is active or not."</summary>
            Public Const FSW_Enabled$ = "FSW_Enabled"
            ''' <summary>Key for resource getting something like "The file pattern filter."</summary>
            Public Const FSW_Filter$ = "FSW_Filter"
            ''' <summary>Key for resource getting something like "Flag to watch subdirectories."</summary>
            Public Const FSW_IncludeSubdirectories$ = "FSW_IncludeSubdirectories"
            ''' <summary>Key for resource getting something like "The path to the directory to monitor."</summary>
            Public Const FSW_Path$ = "FSW_Path"
            ''' <summary>Key for resource getting something like "The object used to marshal the event handler calls issued as a result of a Directory change."</summary>
            Public Const FSW_SynchronizingObject$ = "FSW_SynchronizingObject"
            ''' <summary>Key for resource getting something like "Occurs when a file and/or directory change matches the filter."</summary>
            Public Const FSW_Changed$ = "FSW_Changed"
            ''' <summary>Key for resource getting something like "Occurs when a file and/or directory creation matches the filter."</summary>
            Public Const FSW_Created$ = "FSW_Created"
            ''' <summary>Key for resource getting something like "Occurs when a file and/or directory deletion matches the filter."</summary>
            Public Const FSW_Deleted$ = "FSW_Deleted"
            ''' <summary>Key for resource getting something like "Occurs when a file and/or directory rename matches the filter."</summary>
            Public Const FSW_Renamed$ = "FSW_Renamed"
            ''' <summary>Key for resource getting something like "Too many changes at once in directory:{0}."</summary>
            Public Const FSW_BufferOverflow$ = "FSW_BufferOverflow"
            ''' <summary>Key for resource getting something like "Monitors file system change notifications and raises events when a directory or file changes."</summary>
            Public Const FileSystemWatcherDesc$ = "FileSystemWatcherDesc"
            ''' <summary>Key for resource getting something like "[Not Set]"</summary>
            Public Const NotSet$ = "NotSet"
            ''' <summary>Key for resource getting something like "Indicates whether the timer will be restarted when it is enabled."</summary>
            Public Const TimerAutoReset$ = "TimerAutoReset"
            ''' <summary>Key for resource getting something like "Indicates whether the timer is enabled to fire events at a defined interval."</summary>
            Public Const TimerEnabled$ = "TimerEnabled"
            ''' <summary>Key for resource getting something like "The number of milliseconds between timer events."</summary>
            Public Const TimerInterval$ = "TimerInterval"
            ''' <summary>Key for resource getting something like "Occurs when the Interval has elapsed."</summary>
            Public Const TimerIntervalElapsed$ = "TimerIntervalElapsed"
            ''' <summary>Key for resource getting something like "The object used to marshal the event handler calls issued when an interval has elapsed."</summary>
            Public Const TimerSynchronizingObject$ = "TimerSynchronizingObject"
            ''' <summary>Key for resource getting something like "Mismatched counter types."</summary>
            Public Const MismatchedCounterTypes$ = "MismatchedCounterTypes"
            ''' <summary>Key for resource getting something like "Could not find a property for the attribute '{0}'."</summary>
            Public Const NoPropertyForAttribute$ = "NoPropertyForAttribute"
            ''' <summary>Key for resource getting something like "The value of attribute '{0}' could not be converted to the proper type."</summary>
            Public Const InvalidAttributeType$ = "InvalidAttributeType"
            ''' <summary>Key for resource getting something like "'{0}' can not be empty string."</summary>
            Public Const Generic_ArgCantBeEmptyString$ = "Generic_ArgCantBeEmptyString"
            ''' <summary>Key for resource getting something like "Event log names must consist of printable characters and cannot contain \, *, ?, or spaces"</summary>
            Public Const BadLogName$ = "BadLogName"
            ''' <summary>Key for resource getting something like "Invalid value {1} for property {0}."</summary>
            Public Const InvalidProperty$ = "InvalidProperty"
            ''' <summary>Key for resource getting something like "Cannot create Notify event."</summary>
            Public Const NotifyCreateFailed$ = "NotifyCreateFailed"
            ''' <summary>Key for resource getting something like "Cannot monitor Event log. The log may exist on a remote computer."</summary>
            Public Const CantMonitorEventLog$ = "CantMonitorEventLog"
            ''' <summary>Key for resource getting something like "Cannot initialize the same object twice."</summary>
            Public Const InitTwice$ = "InitTwice"
            ''' <summary>Key for resource getting something like "Invalid value '{1}' for parameter '{0}'."</summary>
            Public Const InvalidParameter$ = "InvalidParameter"
            ''' <summary>Key for resource getting something like "Must specify value for {0}."</summary>
            Public Const MissingParameter$ = "MissingParameter"
            ''' <summary>Key for resource getting something like "The size of {0} is too big. It cannot be longer than {1} characters."</summary>
            Public Const ParameterTooLong$ = "ParameterTooLong"
            ''' <summary>Key for resource getting something like "Source {0} already exists on the local computer."</summary>
            Public Const LocalSourceAlreadyExists$ = "LocalSourceAlreadyExists"
            ''' <summary>Key for resource getting something like "Source {0} already exists on the computer '{1}'."</summary>
            Public Const SourceAlreadyExists$ = "SourceAlreadyExists"
            ''' <summary>Key for resource getting something like "Log {0} has already been registered as a source on the local computer."</summary>
            Public Const LocalLogAlreadyExistsAsSource$ = "LocalLogAlreadyExistsAsSource"
            ''' <summary>Key for resource getting something like "Log {0} has already been registered as a source on the computer '{1}'."</summary>
            Public Const LogAlreadyExistsAsSource$ = "LogAlreadyExistsAsSource"
            ''' <summary>Key for resource getting something like "Only the first eight characters of a custom log name are significant, and there is already another log on the system using the first eight characters of the name given. Name given: '{0}', name of existing log: '{1}'."</summary>
            Public Const DuplicateLogName$ = "DuplicateLogName"
            ''' <summary>Key for resource getting something like "Cannot open registry key {0}\{1}\{2} on computer '{3}'."</summary>
            Public Const RegKeyMissing$ = "RegKeyMissing"
            ''' <summary>Key for resource getting something like "Cannot open registry key {0}\{1}\{2}."</summary>
            Public Const LocalRegKeyMissing$ = "LocalRegKeyMissing"
            ''' <summary>Key for resource getting something like "Cannot open registry key {0} on computer {1}."</summary>
            Public Const RegKeyMissingShort$ = "RegKeyMissingShort"
            ''' <summary>Key for resource getting something like "Invalid format for argument {0}."</summary>
            Public Const InvalidParameterFormat$ = "InvalidParameterFormat"
            ''' <summary>Key for resource getting something like "Log to delete was not specified."</summary>
            Public Const NoLogName$ = "NoLogName"
            ''' <summary>Key for resource getting something like "Cannot open registry key {0} on computer {1}. You might not have access."</summary>
            Public Const RegKeyNoAccess$ = "RegKeyNoAccess"
            ''' <summary>Key for resource getting something like "Cannot find Log {0} on computer '{1}'."</summary>
            Public Const MissingLog$ = "MissingLog"
            ''' <summary>Key for resource getting something like "The source '{0}' is not registered on machine '{1}', or you do not have write access to the {2} registry key."</summary>
            Public Const SourceNotRegistered$ = "SourceNotRegistered"
            ''' <summary>Key for resource getting something like "Source {0} is not registered on the local computer."</summary>
            Public Const LocalSourceNotRegistered$ = "LocalSourceNotRegistered"
            ''' <summary>Key for resource getting something like "Cannot retrieve all entries."</summary>
            Public Const CantRetrieveEntries$ = "CantRetrieveEntries"
            ''' <summary>Key for resource getting something like "Index {0} is out of bounds."</summary>
            Public Const IndexOutOfBounds$ = "IndexOutOfBounds"
            ''' <summary>Key for resource getting something like "Cannot read log entry number {0}.  The event log may be corrupt."</summary>
            Public Const CantReadLogEntryAt$ = "CantReadLogEntryAt"
            ''' <summary>Key for resource getting something like "Log property value has not been specified."</summary>
            Public Const MissingLogProperty$ = "MissingLogProperty"
            ''' <summary>Key for resource getting something like "Cannot open log {0} on machine {1}. Windows has not provided an error code."</summary>
            Public Const CantOpenLog$ = "CantOpenLog"
            ''' <summary>Key for resource getting something like "Source property was not set before opening the event log in write mode."</summary>
            Public Const NeedSourceToOpen$ = "NeedSourceToOpen"
            ''' <summary>Key for resource getting something like "Source property was not set before writing to the event log."</summary>
            Public Const NeedSourceToWrite$ = "NeedSourceToWrite"
            ''' <summary>Key for resource getting something like "Cannot open log for source '{0}'. You may not have write access."</summary>
            Public Const CantOpenLogAccess$ = "CantOpenLogAccess"
            ''' <summary>Key for resource getting something like "Log entry string is too long. A string written to the event log cannot exceed 32766 characters."</summary>
            Public Const LogEntryTooLong$ = "LogEntryTooLong"
            ''' <summary>Key for resource getting something like "The maximum allowed number of replacement strings is 255."</summary>
            Public Const TooManyReplacementStrings$ = "TooManyReplacementStrings"
            ''' <summary>Key for resource getting something like "The source '{0}' is not registered in log '{1}'. (It is registered in log '{2}'.) " The Source and Log properties must be matched, or you may set Log to the empty string, and it will automatically be matched to the Source property."</summary>
            Public Const LogSourceMismatch$ = "LogSourceMismatch"
            ''' <summary>Key for resource getting something like "Cannot obtain account information."</summary>
            Public Const NoAccountInfo$ = "NoAccountInfo"
            ''' <summary>Key for resource getting something like "No current EventLog entry available, cursor is located before the first or after the last element of the enumeration."</summary>
            Public Const NoCurrentEntry$ = "NoCurrentEntry"
            ''' <summary>Key for resource getting something like "The description for Event ID '{0}' in Source '{1}' cannot be found.  The local computer may not have the necessary registry information or message DLL files to display the message, or you may not have permission to access them.  The following information is part of the event:"</summary>
            Public Const MessageNotFormatted$ = "MessageNotFormatted"
            ''' <summary>Key for resource getting something like "Invalid eventID value '{0}'. It must be in the range between '{1}' and '{2}'."</summary>
            Public Const EventID$ = "EventID"
            ''' <summary>Key for resource getting something like "The event log '{0}' on computer '{1}' does not exist."</summary>
            Public Const LogDoesNotExists$ = "LogDoesNotExists"
            ''' <summary>Key for resource getting something like "The log name: '{0}' is invalid for customer log creation."</summary>
            Public Const InvalidCustomerLogName$ = "InvalidCustomerLogName"
            ''' <summary>Key for resource getting something like "The event log source '{0}' cannot be deleted, because it's equal to the log name."</summary>
            Public Const CannotDeleteEqualSource$ = "CannotDeleteEqualSource"
            ''' <summary>Key for resource getting something like "'retentionDays' must be between 1 and 365 days."</summary>
            Public Const RentionDaysOutOfRange$ = "RentionDaysOutOfRange"
            ''' <summary>Key for resource getting something like "MaximumKilobytes must be between 64 KB and 4 GB, and must be in 64K increments."</summary>
            Public Const MaximumKilobytesOutOfRange$ = "MaximumKilobytesOutOfRange"
            ''' <summary>Key for resource getting something like "The source was not found, but some or all event logs could not be searched.  Inaccessible logs: {0}."</summary>
            Public Const SomeLogsInaccessible$ = "SomeLogsInaccessible"
            ''' <summary>Key for resource getting something like "The config value for Switch '{0}' was invalid."</summary>
            Public Const BadConfigSwitchValue$ = "BadConfigSwitchValue"
            ''' <summary>Key for resource getting something like "The '{0}' section can only appear once per config file."</summary>
            Public Const ConfigSectionsUnique$ = "ConfigSectionsUnique"
            ''' <summary>Key for resource getting something like "The '{0}' tag can only appear once per section."</summary>
            Public Const ConfigSectionsUniquePerSection$ = "ConfigSectionsUniquePerSection"
            ''' <summary>Key for resource getting something like "The listener '{0}' added to source '{1}' must have a listener with the same name defined in the main Trace listeners section."</summary>
            Public Const SourceListenerDoesntExist$ = "SourceListenerDoesntExist"
            ''' <summary>Key for resource getting something like "The source '{0}' must have a switch with the same name defined in the Switches section."</summary>
            Public Const SourceSwitchDoesntExist$ = "SourceSwitchDoesntExist"
            ''' <summary>Key for resource getting something like "Cannot update Performance Counter, this object has been initialized as ReadOnly."</summary>
            Public Const ReadOnlyCounter$ = "ReadOnlyCounter"
            ''' <summary>Key for resource getting something like "Cannot remove Performance Counter Instance, this object as been initialized as ReadOnly."</summary>
            Public Const ReadOnlyRemoveInstance$ = "ReadOnlyRemoveInstance"
            ''' <summary>Key for resource getting something like "The requested Performance Counter is not a custom counter, it has to be initialized as ReadOnly."</summary>
            Public Const NotCustomCounter$ = "NotCustomCounter"
            ''' <summary>Key for resource getting something like "Failed to initialize because CategoryName is missing."</summary>
            Public Const CategoryNameMissing$ = "CategoryNameMissing"
            ''' <summary>Key for resource getting something like "Failed to initialize because CounterName is missing."</summary>
            Public Const CounterNameMissing$ = "CounterNameMissing"
            ''' <summary>Key for resource getting something like "Counter is single instance, instance name '{0}' is not valid for this counter category."</summary>
            Public Const InstanceNameProhibited$ = "InstanceNameProhibited"
            ''' <summary>Key for resource getting something like "Counter is not single instance, an instance name needs to be specified."</summary>
            Public Const InstanceNameRequired$ = "InstanceNameRequired"
            ''' <summary>Key for resource getting something like "Instance {0} does not exist in category {1}."</summary>
            Public Const MissingInstance$ = "MissingInstance"
            ''' <summary>Key for resource getting something like "Cannot create Performance Category '{0}' because it already exists."</summary>
            Public Const PerformanceCategoryExists$ = "PerformanceCategoryExists"
            ''' <summary>Key for resource getting something like "Invalid empty or null string for counter name."</summary>
            Public Const InvalidCounterName$ = "InvalidCounterName"
            ''' <summary>Key for resource getting something like "Cannot create Performance Category with counter name {0} because the name is a duplicate."</summary>
            Public Const DuplicateCounterName$ = "DuplicateCounterName"
            ''' <summary>Key for resource getting something like "Cannot delete Performance Category because this category is not registered or is a system category."</summary>
            Public Const CantDeleteCategory$ = "CantDeleteCategory"
            ''' <summary>Key for resource getting something like "Category does not exist."</summary>
            Public Const MissingCategory$ = "MissingCategory"
            ''' <summary>Key for resource getting something like "Category {0} does not exist."</summary>
            Public Const MissingCategoryDetail$ = "MissingCategoryDetail"
            ''' <summary>Key for resource getting something like "Cannot read Category {0}."</summary>
            Public Const CantReadCategory$ = "CantReadCategory"
            ''' <summary>Key for resource getting something like "Counter {0} does not exist."</summary>
            Public Const MissingCounter$ = "MissingCounter"
            ''' <summary>Key for resource getting something like "Category name property has not been set."</summary>
            Public Const CategoryNameNotSet$ = "CategoryNameNotSet"
            ''' <summary>Key for resource getting something like "Could not locate Performance Counter with specified category name '{0}', counter name '{1}'."</summary>
            Public Const CounterExists$ = "CounterExists"
            ''' <summary>Key for resource getting something like "Could not Read Category Index: {0}."</summary>
            Public Const CantReadCategoryIndex$ = "CantReadCategoryIndex"
            ''' <summary>Key for resource getting something like "Counter '{0}' does not exist in the specified Category."</summary>
            Public Const CantReadCounter$ = "CantReadCounter"
            ''' <summary>Key for resource getting something like "Instance '{0}' does not exist in the specified Category."</summary>
            Public Const CantReadInstance$ = "CantReadInstance"
            ''' <summary>Key for resource getting something like "Cannot write to a Performance Counter in a remote machine."</summary>
            Public Const RemoteWriting$ = "RemoteWriting"
            ''' <summary>Key for resource getting something like "The Counter layout for the Category specified is invalid, a counter of the type:  AverageCount64, AverageTimer32, CounterMultiTimer, CounterMultiTimerInverse, CounterMultiTimer100Ns, CounterMultiTimer100NsInverse, RawFraction, or SampleFraction has to be immediately followed by any of the base counter types: AverageBase, CounterMultiBase, RawBase or SampleBase."</summary>
            Public Const CounterLayout$ = "CounterLayout"
            ''' <summary>Key for resource getting something like "The operation couldn't be completed, potential internal deadlock."</summary>
            Public Const PossibleDeadlock$ = "PossibleDeadlock"
            ''' <summary>Key for resource getting something like "Cannot access shared memory, AppDomain has been unloaded."</summary>
            Public Const SharedMemoryGhosted$ = "SharedMemoryGhosted"
            ''' <summary>Key for resource getting something like "Help not available."</summary>
            Public Const HelpNotAvailable$ = "HelpNotAvailable"
            ''' <summary>Key for resource getting something like "Invalid help string. Its length must be in the range between '{0}' and '{1}'."</summary>
            Public Const PerfInvalidHelp$ = "PerfInvalidHelp"
            ''' <summary>Key for resource getting something like "Invalid counter name. Its length must be in the range between '{0}' and '{1}'. Double quotes, control characters and leading or trailing spaces are not allowed."</summary>
            Public Const PerfInvalidCounterName$ = "PerfInvalidCounterName"
            ''' <summary>Key for resource getting something like "Invalid category name. Its length must be in the range between '{0}' and '{1}'. Double quotes, control characters and leading or trailing spaces are not allowed."</summary>
            Public Const PerfInvalidCategoryName$ = "PerfInvalidCategoryName"
            ''' <summary>Key for resource getting something like "Only objects of type CounterCreationData can be added to a CounterCreationDataCollection."</summary>
            Public Const MustAddCounterCreationData$ = "MustAddCounterCreationData"
            ''' <summary>Key for resource getting something like "Creating or Deleting Performance Counter Categories on remote machines is not supported."</summary>
            Public Const RemoteCounterAdmin$ = "RemoteCounterAdmin"
            ''' <summary>Key for resource getting something like "The {0} category doesn't provide any instance information, no accurate data can be returned."</summary>
            Public Const NoInstanceInformation$ = "NoInstanceInformation"
            ''' <summary>Key for resource getting something like "There was an error calculating the PerformanceCounter value (0x{0})."</summary>
            Public Const PerfCounterPdhError$ = "PerfCounterPdhError"
            ''' <summary>Key for resource getting something like "Category '{0}' is marked as multi-instance.  Performance counters in this category can only be created with instance names."</summary>
            Public Const MultiInstanceOnly$ = "MultiInstanceOnly"
            ''' <summary>Key for resource getting something like "Category '{0}' is marked as single-instance.  Performance counters in this category can only be created without instance names."</summary>
            Public Const SingleInstanceOnly$ = "SingleInstanceOnly"
            ''' <summary>Key for resource getting something like "Instance names used for writing to custom counters must be 127 characters or less. "</summary>
            Public Const InstanceNameTooLong$ = "InstanceNameTooLong"
            ''' <summary>Key for resource getting something like "Category names must be 1024 characters or less. "</summary>
            Public Const CategoryNameTooLong$ = "CategoryNameTooLong"
            ''' <summary>Key for resource getting something like "InstanceLifetime is unused by ReadOnly counters. "</summary>
            Public Const InstanceLifetimeProcessonReadOnly$ = "InstanceLifetimeProcessonReadOnly"
            ''' <summary>Key for resource getting something like "Single instance categories are only valid with the Global lifetime. "</summary>
            Public Const InstanceLifetimeProcessforSingleInstance$ = "InstanceLifetimeProcessforSingleInstance"
            ''' <summary>Key for resource getting something like "Instance '{0}' already exists with a lifetime of Process.  It cannot be recreated or reused until it has been removed or until the process using it has exited. "</summary>
            Public Const InstanceAlreadyExists$ = "InstanceAlreadyExists"
            ''' <summary>Key for resource getting something like "The InstanceLifetime cannot be set after the instance has been initialized.  You must use the default constructor and set the CategoryName, InstanceName, CounterName, InstanceLifetime and ReadOnly properties manually before setting the RawValue. "</summary>
            Public Const CantSetLifetimeAfterInitialized$ = "CantSetLifetimeAfterInitialized"
            ''' <summary>Key for resource getting something like "PerformanceCounterInstanceLifetime.Process is not valid in the global shared memory.  If your performance counter category was created with an older version of the Framework, it uses the global shared memory.  Either use PerformanceCounterInstanceLifetime.Global, or if applications running on older versions of the Framework do not need to write to your category, delete and recreate it. "</summary>
            Public Const ProcessLifetimeNotValidInGlobal$ = "ProcessLifetimeNotValidInGlobal"
            ''' <summary>Key for resource getting something like "An instance with a lifetime of Process can only be accessed from a PerformanceCounter with the InstanceLifetime set to PerformanceCounterInstanceLifetime.Process. "</summary>
            Public Const CantConvertProcessToGlobal$ = "CantConvertProcessToGlobal"
            ''' <summary>Key for resource getting something like "An instance with a lifetime of Global can only be accessed from a PerformanceCounter with the InstanceLifetime set to PerformanceCounterInstanceLifetime.Global. "</summary>
            Public Const CantConvertGlobalToProcess$ = "CantConvertGlobalToProcess"
            ''' <summary>Key for resource getting something like "The AboveNormal and BelowNormal priority classes are not available on this platform."</summary>
            Public Const PriorityClassNotSupported$ = "PriorityClassNotSupported"
            ''' <summary>Key for resource getting something like "Feature requires Windows NT."</summary>
            Public Const WinNTRequired$ = "WinNTRequired"
            ''' <summary>Key for resource getting something like "Feature requires Windows 2000."</summary>
            Public Const Win2kRequired$ = "Win2kRequired"
            ''' <summary>Key for resource getting something like "No process is associated with this object."</summary>
            Public Const NoAssociatedProcess$ = "NoAssociatedProcess"
            ''' <summary>Key for resource getting something like "Feature requires a process identifier."</summary>
            Public Const ProcessIdRequired$ = "ProcessIdRequired"
            ''' <summary>Key for resource getting something like "Feature is not supported for remote machines."</summary>
            Public Const NotSupportedRemote$ = "NotSupportedRemote"
            ''' <summary>Key for resource getting something like "Process has exited, so the requested information is not available."</summary>
            Public Const NoProcessInfo$ = "NoProcessInfo"
            ''' <summary>Key for resource getting something like "Process must exit before requested information can be determined."</summary>
            Public Const WaitTillExit$ = "WaitTillExit"
            ''' <summary>Key for resource getting something like "Process was not started by this object, so requested information cannot be determined."</summary>
            Public Const NoProcessHandle$ = "NoProcessHandle"
            ''' <summary>Key for resource getting something like "Process with an Id of {0} is not running."</summary>
            Public Const MissingProccess$ = "MissingProccess"
            ''' <summary>Key for resource getting something like "Minimum working set size is invalid. It must be less than or equal to the maximum working set size."</summary>
            Public Const BadMinWorkset$ = "BadMinWorkset"
            ''' <summary>Key for resource getting something like "Maximum working set size is invalid. It must be greater than or equal to the minimum working set size."</summary>
            Public Const BadMaxWorkset$ = "BadMaxWorkset"
            ''' <summary>Key for resource getting something like "Operating system does not support accessing processes on remote computers. This feature requires Windows NT or later."</summary>
            Public Const WinNTRequiredForRemote$ = "WinNTRequiredForRemote"
            ''' <summary>Key for resource getting something like "Cannot process request because the process ({0}) has exited."</summary>
            Public Const ProcessHasExited$ = "ProcessHasExited"
            ''' <summary>Key for resource getting something like "Cannot process request because the process has exited."</summary>
            Public Const ProcessHasExitedNoId$ = "ProcessHasExitedNoId"
            ''' <summary>Key for resource getting something like "The request cannot be processed because the thread ({0}) has exited."</summary>
            Public Const ThreadExited$ = "ThreadExited"
            ''' <summary>Key for resource getting something like "Feature requires Windows 2000 or later."</summary>
            Public Const Win2000Required$ = "Win2000Required"
            ''' <summary>Key for resource getting something like "Feature requires Windows XP or later."</summary>
            Public Const WinXPRequired$ = "WinXPRequired"
            ''' <summary>Key for resource getting something like "Feature requires Windows Server 2003 or later."</summary>
            Public Const Win2k3Required$ = "Win2k3Required"
            ''' <summary>Key for resource getting something like "Thread {0} found, but no process {1} found."</summary>
            Public Const ProcessNotFound$ = "ProcessNotFound"
            ''' <summary>Key for resource getting something like "Cannot retrieve process identifier from the process handle."</summary>
            Public Const CantGetProcessId$ = "CantGetProcessId"
            ''' <summary>Key for resource getting something like "Process performance counter is disabled, so the requested operation cannot be performed."</summary>
            Public Const ProcessDisabled$ = "ProcessDisabled"
            ''' <summary>Key for resource getting something like "WaitReason is only available if the ThreadState is Wait."</summary>
            Public Const WaitReasonUnavailable$ = "WaitReasonUnavailable"
            ''' <summary>Key for resource getting something like "Feature is not supported for threads on remote computers."</summary>
            Public Const NotSupportedRemoteThread$ = "NotSupportedRemoteThread"
            ''' <summary>Key for resource getting something like "Current thread is not in Single Thread Apartment (STA) mode. Starting a process with UseShellExecute set to True requires the current thread be in STA mode.  Ensure that your Main function has STAThreadAttribute marked."</summary>
            Public Const UseShellExecuteRequiresSTA$ = "UseShellExecuteRequiresSTA"
            ''' <summary>Key for resource getting something like "The Process object must have the UseShellExecute property set to false in order to redirect IO streams."</summary>
            Public Const CantRedirectStreams$ = "CantRedirectStreams"
            ''' <summary>Key for resource getting something like "The Process object must have the UseShellExecute property set to false in order to use environment variables."</summary>
            Public Const CantUseEnvVars$ = "CantUseEnvVars"
            ''' <summary>Key for resource getting something like "The Process object must have the UseShellExecute property set to false in order to start a process as a user."</summary>
            Public Const CantStartAsUser$ = "CantStartAsUser"
            ''' <summary>Key for resource getting something like "Couldn't connect to remote machine."</summary>
            Public Const CouldntConnectToRemoteMachine$ = "CouldntConnectToRemoteMachine"
            ''' <summary>Key for resource getting something like "Couldn't get process information from performance counter."</summary>
            Public Const CouldntGetProcessInfos$ = "CouldntGetProcessInfos"
            ''' <summary>Key for resource getting something like "WaitForInputIdle failed.  This could be because the process does not have a graphical interface."</summary>
            Public Const InputIdleUnkownError$ = "InputIdleUnkownError"
            ''' <summary>Key for resource getting something like "Cannot start process because a file name has not been provided."</summary>
            Public Const FileNameMissing$ = "FileNameMissing"
            ''' <summary>Key for resource getting something like "The environment block provided doesn't have the correct format."</summary>
            Public Const EnvironmentBlock$ = "EnvironmentBlock"
            ''' <summary>Key for resource getting something like "Unable to enumerate the process modules."</summary>
            Public Const EnumProcessModuleFailed$ = "EnumProcessModuleFailed"
            ''' <summary>Key for resource getting something like "An async read operation has already been started on the stream."</summary>
            Public Const PendingAsyncOperation$ = "PendingAsyncOperation"
            ''' <summary>Key for resource getting something like "No async read operation is in progress on the stream."</summary>
            Public Const NoAsyncOperation$ = "NoAsyncOperation"
            ''' <summary>Key for resource getting something like "The specified executable is not a valid Win32 application."</summary>
            Public Const InvalidApplication$ = "InvalidApplication"
            ''' <summary>Key for resource getting something like "StandardOutputEncoding is only supported when standard output is redirected."</summary>
            Public Const StandardOutputEncodingNotAllowed$ = "StandardOutputEncodingNotAllowed"
            ''' <summary>Key for resource getting something like "StandardErrorEncoding is only supported when standard error is redirected."</summary>
            Public Const StandardErrorEncodingNotAllowed$ = "StandardErrorEncodingNotAllowed"
            ''' <summary>Key for resource getting something like "Custom counters file view is out of memory."</summary>
            Public Const CountersOOM$ = "CountersOOM"
            ''' <summary>Key for resource getting something like "Cannot continue the current operation, the performance counters memory mapping has been corrupted."</summary>
            Public Const MappingCorrupted$ = "MappingCorrupted"
            ''' <summary>Key for resource getting something like "Cannot initialize security descriptor initialized."</summary>
            Public Const SetSecurityDescriptorFailed$ = "SetSecurityDescriptorFailed"
            ''' <summary>Key for resource getting something like "Cannot create file mapping."</summary>
            Public Const CantCreateFileMapping$ = "CantCreateFileMapping"
            ''' <summary>Key for resource getting something like "Cannot map view of file."</summary>
            Public Const CantMapFileView$ = "CantMapFileView"
            ''' <summary>Key for resource getting something like "Cannot calculate the size of the file view. "</summary>
            Public Const CantGetMappingSize$ = "CantGetMappingSize"
            ''' <summary>Key for resource getting something like "StandardOut has not been redirected or the process hasn't started yet."</summary>
            Public Const CantGetStandardOut$ = "CantGetStandardOut"
            ''' <summary>Key for resource getting something like "StandardIn has not been redirected."</summary>
            Public Const CantGetStandardIn$ = "CantGetStandardIn"
            ''' <summary>Key for resource getting something like "StandardError has not been redirected."</summary>
            Public Const CantGetStandardError$ = "CantGetStandardError"
            ''' <summary>Key for resource getting something like "Cannot mix synchronous and asynchronous operation on process stream."</summary>
            Public Const CantMixSyncAsyncOperation$ = "CantMixSyncAsyncOperation"
            ''' <summary>Key for resource getting something like "Cannot retrieve file mapping size while initializing configuration settings."</summary>
            Public Const NoFileMappingSize$ = "NoFileMappingSize"
            ''' <summary>Key for resource getting something like "The environment block used to start a process cannot be longer than 65535 bytes.  Your environment block is {0} bytes long.  Remove some environment variables and try again. "</summary>
            Public Const EnvironmentBlockTooLong$ = "EnvironmentBlockTooLong"
            ''' <summary>Key for resource getting something like "The given port name does not start with COM/com or does not resolve to a valid serial port."</summary>
            Public Const Arg_InvalidSerialPort$ = "Arg_InvalidSerialPort"
            ''' <summary>Key for resource getting something like "The given port name is invalid.  It may be a valid port, but not a serial port."</summary>
            Public Const Arg_InvalidSerialPortExtended$ = "Arg_InvalidSerialPortExtended"
            ''' <summary>Key for resource getting something like "The port name cannot start with '\'."</summary>
            Public Const Arg_SecurityException$ = "Arg_SecurityException"
            ''' <summary>Key for resource getting something like "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."</summary>
            Public Const Argument_InvalidOffLen$ = "Argument_InvalidOffLen"
            ''' <summary>Key for resource getting something like "Array cannot be null."</summary>
            Public Const ArgumentNull_Array$ = "ArgumentNull_Array"
            ''' <summary>Key for resource getting something like "Buffer cannot be null."</summary>
            Public Const ArgumentNull_Buffer$ = "ArgumentNull_Buffer"
            ''' <summary>Key for resource getting something like "Argument must be between {0} and {1}."</summary>
            Public Const ArgumentOutOfRange_Bounds_Lower_Upper$ = "ArgumentOutOfRange_Bounds_Lower_Upper"
            ''' <summary>Key for resource getting something like "Enum value was out of legal range."</summary>
            Public Const ArgumentOutOfRange_Enum$ = "ArgumentOutOfRange_Enum"
            ''' <summary>Key for resource getting something like "Non-negative number required."</summary>
            Public Const ArgumentOutOfRange_NeedNonNegNumRequired$ = "ArgumentOutOfRange_NeedNonNegNumRequired"
            ''' <summary>Key for resource getting something like "The timeout must be greater than or equal to -1."</summary>
            Public Const ArgumentOutOfRange_Timeout$ = "ArgumentOutOfRange_Timeout"
            ''' <summary>Key for resource getting something like "The timeout must be either a positive number or -1."</summary>
            Public Const ArgumentOutOfRange_WriteTimeout$ = "ArgumentOutOfRange_WriteTimeout"
            ''' <summary>Key for resource getting something like "Positive number required."</summary>
            Public Const ArgumentOutOfRange_NeedPosNum$ = "ArgumentOutOfRange_NeedPosNum"
            ''' <summary>Key for resource getting something like "Either offset did not refer to a position in the string, or there is an insufficient length of destination character array."</summary>
            Public Const ArgumentOutOfRange_OffsetOut$ = "ArgumentOutOfRange_OffsetOut"
            ''' <summary>Key for resource getting something like "Probable I/O race condition detected while copying memory.  The I/O package is not thread safe by default.  In multithreaded applications, a stream must be accessed in a thread-safe way, such as a thread-safe wrapper returned by TextReader's or TextWriter's Synchronized methods.  This also applies to classes like StreamWriter and StreamReader."</summary>
            Public Const IndexOutOfRange_IORaceCondition$ = "IndexOutOfRange_IORaceCondition"
            ''' <summary>Key for resource getting something like "BindHandle for ThreadPool failed on this handle."</summary>
            Public Const IO_BindHandleFailed$ = "IO_BindHandleFailed"
            ''' <summary>Key for resource getting something like "The I/O operation has been aborted because of either a thread exit or an application request."</summary>
            Public Const IO_OperationAborted$ = "IO_OperationAborted"
            ''' <summary>Key for resource getting something like "Stream does not support seeking."</summary>
            Public Const NotSupported_UnseekableStream$ = "NotSupported_UnseekableStream"
            ''' <summary>Key for resource getting something like "Access to the port '{0}' is denied."</summary>
            Public Const UnauthorizedAccess_IODenied_Path$ = "UnauthorizedAccess_IODenied_Path"
            ''' <summary>Key for resource getting something like "Unable to read beyond the end of the stream."</summary>
            Public Const IO_EOF_ReadBeyondEOF$ = "IO_EOF_ReadBeyondEOF"
            ''' <summary>Key for resource getting something like "Unknown Error '{0}'."</summary>
            Public Const IO_UnknownError$ = "IO_UnknownError"
            ''' <summary>Key for resource getting something like "Can not access a closed Stream."</summary>
            Public Const ObjectDisposed_StreamClosed$ = "ObjectDisposed_StreamClosed"
            ''' <summary>Key for resource getting something like "IAsyncResult object did not come from the corresponding async method on this type."</summary>
            Public Const Arg_WrongAsyncResult$ = "Arg_WrongAsyncResult"
            ''' <summary>Key for resource getting something like "EndRead can only be called once for each asynchronous operation."</summary>
            Public Const InvalidOperation_EndReadCalledMultiple$ = "InvalidOperation_EndReadCalledMultiple"
            ''' <summary>Key for resource getting something like "EndWrite can only be called once for each asynchronous operation."</summary>
            Public Const InvalidOperation_EndWriteCalledMultiple$ = "InvalidOperation_EndWriteCalledMultiple"
            ''' <summary>Key for resource getting something like "The specified port does not exist."</summary>
            Public Const IO_PortNotFound$ = "IO_PortNotFound"
            ''' <summary>Key for resource getting something like "The port '{0}' does not exist."</summary>
            Public Const IO_PortNotFoundFileName$ = "IO_PortNotFoundFileName"
            ''' <summary>Key for resource getting something like "Access to the port is denied."</summary>
            Public Const UnauthorizedAccess_IODenied_NoPathName$ = "UnauthorizedAccess_IODenied_NoPathName"
            ''' <summary>Key for resource getting something like "The specified port name is too long.  The port name must be less than 260 characters."</summary>
            Public Const IO_PathTooLong$ = "IO_PathTooLong"
            ''' <summary>Key for resource getting something like "The process cannot access the port because it is being used by another process."</summary>
            Public Const IO_SharingViolation_NoFileName$ = "IO_SharingViolation_NoFileName"
            ''' <summary>Key for resource getting something like "The process cannot access the port '{0}' because it is being used by another process."</summary>
            Public Const IO_SharingViolation_File$ = "IO_SharingViolation_File"
            ''' <summary>Key for resource getting something like "Stream does not support writing."</summary>
            Public Const NotSupported_UnwritableStream$ = "NotSupported_UnwritableStream"
            ''' <summary>Key for resource getting something like "Can not write to a closed TextWriter."</summary>
            Public Const ObjectDisposed_WriterClosed$ = "ObjectDisposed_WriterClosed"
            ''' <summary>Key for resource getting something like "The BaseStream is only available when the port is open."</summary>
            Public Const BaseStream_Invalid_Not_Open$ = "BaseStream_Invalid_Not_Open"
            ''' <summary>Key for resource getting something like "The PortName cannot be empty."</summary>
            Public Const PortNameEmpty_String$ = "PortNameEmpty_String"
            ''' <summary>Key for resource getting something like "The port is closed."</summary>
            Public Const Port_not_open$ = "Port_not_open"
            ''' <summary>Key for resource getting something like "The port is already open."</summary>
            Public Const Port_already_open$ = "Port_already_open"
            ''' <summary>Key for resource getting something like "'{0}' cannot be set while the port is open."</summary>
            Public Const Cant_be_set_when_open$ = "Cant_be_set_when_open"
            ''' <summary>Key for resource getting something like "The maximum baud rate for the device is {0}."</summary>
            Public Const Max_Baud$ = "Max_Baud"
            ''' <summary>Key for resource getting something like "The port is in the break state and cannot be written to."</summary>
            Public Const In_Break_State$ = "In_Break_State"
            ''' <summary>Key for resource getting something like "The write timed out."</summary>
            Public Const Write_timed_out$ = "Write_timed_out"
            ''' <summary>Key for resource getting something like "RtsEnable cannot be accessed if Handshake is set to RequestToSend or RequestToSendXOnXOff."</summary>
            Public Const CantSetRtsWithHandshaking$ = "CantSetRtsWithHandshaking"
            ''' <summary>Key for resource getting something like "GetPortNames is not supported on Win9x platforms."</summary>
            Public Const NotSupportedOS$ = "NotSupportedOS"
            ''' <summary>Key for resource getting something like "SerialPort does not support encoding '{0}'.  The supported encodings include ASCIIEncoding, UTF8Encoding, UnicodeEncoding, UTF32Encoding, and most single or double byte code pages.  For a complete list please see the documentation."</summary>
            Public Const NotSupportedEncoding$ = "NotSupportedEncoding"
            ''' <summary>Key for resource getting something like "The baud rate to use on this serial port."</summary>
            Public Const BaudRate$ = "BaudRate"
            ''' <summary>Key for resource getting something like "The number of data bits per transmitted/received byte."</summary>
            Public Const DataBits$ = "DataBits"
            ''' <summary>Key for resource getting something like "Whether to discard null bytes received on the port before adding to serial buffer."</summary>
            Public Const DiscardNull$ = "DiscardNull"
            ''' <summary>Key for resource getting something like "Whether to enable the Data Terminal Ready (DTR) line during communications."</summary>
            Public Const DtrEnable$ = "DtrEnable"
            ''' <summary>Key for resource getting something like "The encoding to use when reading and writing strings."</summary>
            Public Const Encoding$ = "Encoding"
            ''' <summary>Key for resource getting something like "The handshaking protocol for flow control in data exchange, which can be None."</summary>
            Public Const Handshake$ = "Handshake"
            ''' <summary>Key for resource getting something like "The string used by ReadLine and WriteLine to denote a new line."</summary>
            Public Const NewLine$ = "NewLine"
            ''' <summary>Key for resource getting something like "The scheme for parity checking each received byte and marking each transmitted byte."</summary>
            Public Const Parity$ = "Parity"
            ''' <summary>Key for resource getting something like "Byte with which to replace bytes received with parity errors."</summary>
            Public Const ParityReplace$ = "ParityReplace"
            ''' <summary>Key for resource getting something like "The name of the communications port to open."</summary>
            Public Const PortName$ = "PortName"
            ''' <summary>Key for resource getting something like "The size of the read buffer in bytes.  This is the maximum number of read bytes which can be buffered."</summary>
            Public Const ReadBufferSize$ = "ReadBufferSize"
            ''' <summary>Key for resource getting something like "The read timeout in Milliseconds."</summary>
            Public Const ReadTimeout$ = "ReadTimeout"
            ''' <summary>Key for resource getting something like "Number of bytes required to be available before the Read event is fired."</summary>
            Public Const ReceivedBytesThreshold$ = "ReceivedBytesThreshold"
            ''' <summary>Key for resource getting something like "Whether to enable the Request To Send (RTS) line during communications."</summary>
            Public Const RtsEnable$ = "RtsEnable"
            ''' <summary>Key for resource getting something like "Represents a serial port resource."</summary>
            Public Const SerialPortDesc$ = "SerialPortDesc"
            ''' <summary>Key for resource getting something like "The number of stop bits per transmitted/received byte."</summary>
            Public Const StopBits$ = "StopBits"
            ''' <summary>Key for resource getting something like "The size of the write buffer in bytes.  This is the maximum number of bytes which can be queued for write."</summary>
            Public Const WriteBufferSize$ = "WriteBufferSize"
            ''' <summary>Key for resource getting something like "The write timeout in milliseconds."</summary>
            Public Const WriteTimeout$ = "WriteTimeout"
            ''' <summary>Key for resource getting something like "Raised each time when an error is received from the SerialPort."</summary>
            Public Const SerialErrorReceived$ = "SerialErrorReceived"
            ''' <summary>Key for resource getting something like "Raised each time when pin is changed on the SerialPort."</summary>
            Public Const SerialPinChanged$ = "SerialPinChanged"
            ''' <summary>Key for resource getting something like "Raised each time when data is received from the SerialPort."</summary>
            Public Const SerialDataReceived$ = "SerialDataReceived"
            ''' <summary>Key for resource getting something like "The type of this counter."</summary>
            Public Const CounterType$ = "CounterType"
            ''' <summary>Key for resource getting something like "The name of this counter."</summary>
            Public Const CounterName$ = "CounterName"
            ''' <summary>Key for resource getting something like "Help information for this counter."</summary>
            Public Const CounterHelp$ = "CounterHelp"
            ''' <summary>Key for resource getting something like "Provides interaction with Windows event logs."</summary>
            Public Const EventLogDesc$ = "EventLogDesc"
            ''' <summary>Key for resource getting something like "User event handler to call for async IO with StandardError stream."</summary>
            Public Const ErrorDataReceived$ = "ErrorDataReceived"
            ''' <summary>Key for resource getting something like "The contents of the log."</summary>
            Public Const LogEntries$ = "LogEntries"
            ''' <summary>Key for resource getting something like "Gets or sets the name of the log to read from and write to."</summary>
            Public Const LogLog$ = "LogLog"
            ''' <summary>Key for resource getting something like "The name of the machine on which to read or write events."</summary>
            Public Const LogMachineName$ = "LogMachineName"
            ''' <summary>Key for resource getting something like "Indicates if the component monitors the event log for changes."</summary>
            Public Const LogMonitoring$ = "LogMonitoring"
            ''' <summary>Key for resource getting something like "The object used to marshal the event handler calls issued as a result of an EventLog change."</summary>
            Public Const LogSynchronizingObject$ = "LogSynchronizingObject"
            ''' <summary>Key for resource getting something like "The application name (source name) to use when writing to the event log."</summary>
            Public Const LogSource$ = "LogSource"
            ''' <summary>Key for resource getting something like "Raised each time any application writes an entry to the event log."</summary>
            Public Const LogEntryWritten$ = "LogEntryWritten"
            ''' <summary>Key for resource getting something like "The machine on which this event log resides."</summary>
            Public Const LogEntryMachineName$ = "LogEntryMachineName"
            ''' <summary>Key for resource getting something like "The binary data associated with this entry in the event log."</summary>
            Public Const LogEntryData$ = "LogEntryData"
            ''' <summary>Key for resource getting something like "The sequence of this entry in the event log."</summary>
            Public Const LogEntryIndex$ = "LogEntryIndex"
            ''' <summary>Key for resource getting something like "The category for this message."</summary>
            Public Const LogEntryCategory$ = "LogEntryCategory"
            ''' <summary>Key for resource getting something like "An application-specific category number assigned to this entry."</summary>
            Public Const LogEntryCategoryNumber$ = "LogEntryCategoryNumber"
            ''' <summary>Key for resource getting something like "The number identifying the message for this source."</summary>
            Public Const LogEntryEventID$ = "LogEntryEventID"
            ''' <summary>Key for resource getting something like "The type of entry - Information, Warning, etc."</summary>
            Public Const LogEntryEntryType$ = "LogEntryEntryType"
            ''' <summary>Key for resource getting something like "The text of the message for this entry"</summary>
            Public Const LogEntryMessage$ = "LogEntryMessage"
            ''' <summary>Key for resource getting something like "The name of the application that wrote this entry."</summary>
            Public Const LogEntrySource$ = "LogEntrySource"
            ''' <summary>Key for resource getting something like "The application-supplied strings used in the message."</summary>
            Public Const LogEntryReplacementStrings$ = "LogEntryReplacementStrings"
            ''' <summary>Key for resource getting something like "The full number identifying the message in the event message dll."</summary>
            Public Const LogEntryResourceId$ = "LogEntryResourceId"
            ''' <summary>Key for resource getting something like "The time at which the application logged this entry."</summary>
            Public Const LogEntryTimeGenerated$ = "LogEntryTimeGenerated"
            ''' <summary>Key for resource getting something like "The time at which the system logged this entry to the event log."</summary>
            Public Const LogEntryTimeWritten$ = "LogEntryTimeWritten"
            ''' <summary>Key for resource getting something like "The username of the account associated with this entry by the writing application."</summary>
            Public Const LogEntryUserName$ = "LogEntryUserName"
            ''' <summary>Key for resource getting something like "User event handler to call for async IO with StandardOutput stream."</summary>
            Public Const OutputDataReceived$ = "OutputDataReceived"
            ''' <summary>Key for resource getting something like "The description message for this counter."</summary>
            Public Const PC_CounterHelp$ = "PC_CounterHelp"
            ''' <summary>Key for resource getting something like "The counter type indicates how to interpret the value of the counter, for example an actual count or a rate of change."</summary>
            Public Const PC_CounterType$ = "PC_CounterType"
            ''' <summary>Key for resource getting something like "Indicates if the counter is read only.  Remote counters and counters not created using this component are read-only."</summary>
            Public Const PC_ReadOnly$ = "PC_ReadOnly"
            ''' <summary>Key for resource getting something like "Directly accesses the raw value of this counter.  The counter must have been created using this component."</summary>
            Public Const PC_RawValue$ = "PC_RawValue"
            ''' <summary>Key for resource getting something like "Indicates if the process component is associated with a real process."</summary>
            Public Const ProcessAssociated$ = "ProcessAssociated"
            ''' <summary>Key for resource getting something like "Provides access to local and remote processes, enabling starting and stopping of local processes."</summary>
            Public Const ProcessDesc$ = "ProcessDesc"
            ''' <summary>Key for resource getting something like "The value returned from the associated process when it terminated."</summary>
            Public Const ProcessExitCode$ = "ProcessExitCode"
            ''' <summary>Key for resource getting something like "Indicates if the associated process has been terminated."</summary>
            Public Const ProcessTerminated$ = "ProcessTerminated"
            ''' <summary>Key for resource getting something like "The time that the associated process exited."</summary>
            Public Const ProcessExitTime$ = "ProcessExitTime"
            ''' <summary>Key for resource getting something like "Returns the native handle for this process.   The handle is only available if the process was started using this component."</summary>
            Public Const ProcessHandle$ = "ProcessHandle"
            ''' <summary>Key for resource getting something like "The number of native handles associated with this process."</summary>
            Public Const ProcessHandleCount$ = "ProcessHandleCount"
            ''' <summary>Key for resource getting something like "The unique identifier for the process."</summary>
            Public Const ProcessId$ = "ProcessId"
            ''' <summary>Key for resource getting something like "The name of the machine the running the process."</summary>
            Public Const ProcessMachineName$ = "ProcessMachineName"
            ''' <summary>Key for resource getting something like "The main module for the associated process."</summary>
            Public Const ProcessMainModule$ = "ProcessMainModule"
            ''' <summary>Key for resource getting something like "The modules that have been loaded by the associated process."</summary>
            Public Const ProcessModules$ = "ProcessModules"
            ''' <summary>Key for resource getting something like "The object used to marshal the event handler calls issued as a result of a Process exit."</summary>
            Public Const ProcessSynchronizingObject$ = "ProcessSynchronizingObject"
            ''' <summary>Key for resource getting something like "The identifier for the session of the process."</summary>
            Public Const ProcessSessionId$ = "ProcessSessionId"
            ''' <summary>Key for resource getting something like "The threads running in the associated process."</summary>
            Public Const ProcessThreads$ = "ProcessThreads"
            ''' <summary>Key for resource getting something like "Whether the process component should watch for the associated process to exit, and raise the Exited event."</summary>
            Public Const ProcessEnableRaisingEvents$ = "ProcessEnableRaisingEvents"
            ''' <summary>Key for resource getting something like "If the WatchForExit property is set to true, then this event is raised when the associated process exits."</summary>
            Public Const ProcessExited$ = "ProcessExited"
            ''' <summary>Key for resource getting something like "The name of the application, document or URL to start."</summary>
            Public Const ProcessFileName$ = "ProcessFileName"
            ''' <summary>Key for resource getting something like "The initial working directory for the process."</summary>
            Public Const ProcessWorkingDirectory$ = "ProcessWorkingDirectory"
            ''' <summary>Key for resource getting something like "The base priority computed based on the priority class that all threads run relative to."</summary>
            Public Const ProcessBasePriority$ = "ProcessBasePriority"
            ''' <summary>Key for resource getting something like "The handle of the main window for the process."</summary>
            Public Const ProcessMainWindowHandle$ = "ProcessMainWindowHandle"
            ''' <summary>Key for resource getting something like "The caption of the main window for the process."</summary>
            Public Const ProcessMainWindowTitle$ = "ProcessMainWindowTitle"
            ''' <summary>Key for resource getting something like "The maximum amount of physical memory the process has required since it was started."</summary>
            Public Const ProcessMaxWorkingSet$ = "ProcessMaxWorkingSet"
            ''' <summary>Key for resource getting something like "The minimum amount of physical memory the process has required since it was started."</summary>
            Public Const ProcessMinWorkingSet$ = "ProcessMinWorkingSet"
            ''' <summary>Key for resource getting something like "The number of bytes of non pageable system  memory the process is using."</summary>
            Public Const ProcessNonpagedSystemMemorySize$ = "ProcessNonpagedSystemMemorySize"
            ''' <summary>Key for resource getting something like "The current amount of memory that can be paged to disk that the process is using."</summary>
            Public Const ProcessPagedMemorySize$ = "ProcessPagedMemorySize"
            ''' <summary>Key for resource getting something like "The number of bytes of pageable system memory the process is using."</summary>
            Public Const ProcessPagedSystemMemorySize$ = "ProcessPagedSystemMemorySize"
            ''' <summary>Key for resource getting something like "The maximum amount of memory that can be paged to disk that the process has used since it was started."</summary>
            Public Const ProcessPeakPagedMemorySize$ = "ProcessPeakPagedMemorySize"
            ''' <summary>Key for resource getting something like "The maximum amount of physical memory the process has used since it was started."</summary>
            Public Const ProcessPeakWorkingSet$ = "ProcessPeakWorkingSet"
            ''' <summary>Key for resource getting something like "The maximum amount of virtual memory the process has allocated since it was started."</summary>
            Public Const ProcessPeakVirtualMemorySize$ = "ProcessPeakVirtualMemorySize"
            ''' <summary>Key for resource getting something like "Whether this process would like a priority boost when the user interacts with it."</summary>
            Public Const ProcessPriorityBoostEnabled$ = "ProcessPriorityBoostEnabled"
            ''' <summary>Key for resource getting something like "The priority that the threads in the process run relative to."</summary>
            Public Const ProcessPriorityClass$ = "ProcessPriorityClass"
            ''' <summary>Key for resource getting something like "The current amount of memory that the process has allocated that cannot be shared with other processes."</summary>
            Public Const ProcessPrivateMemorySize$ = "ProcessPrivateMemorySize"
            ''' <summary>Key for resource getting something like "The amount of CPU time the process spent inside the operating system core."</summary>
            Public Const ProcessPrivilegedProcessorTime$ = "ProcessPrivilegedProcessorTime"
            ''' <summary>Key for resource getting something like "The name of the process."</summary>
            Public Const ProcessProcessName$ = "ProcessProcessName"
            ''' <summary>Key for resource getting something like "A bit mask which represents the processors that the threads within the process are allowed to run on."</summary>
            Public Const ProcessProcessorAffinity$ = "ProcessProcessorAffinity"
            ''' <summary>Key for resource getting something like "Whether this process is currently responding."</summary>
            Public Const ProcessResponding$ = "ProcessResponding"
            ''' <summary>Key for resource getting something like "Standard error stream of the process."</summary>
            Public Const ProcessStandardError$ = "ProcessStandardError"
            ''' <summary>Key for resource getting something like "Standard input stream of the process."</summary>
            Public Const ProcessStandardInput$ = "ProcessStandardInput"
            ''' <summary>Key for resource getting something like "Standard output stream of the process."</summary>
            Public Const ProcessStandardOutput$ = "ProcessStandardOutput"
            ''' <summary>Key for resource getting something like "Specifies information used to start a process."</summary>
            Public Const ProcessStartInfo$ = "ProcessStartInfo"
            ''' <summary>Key for resource getting something like "The time at which the process was started."</summary>
            Public Const ProcessStartTime$ = "ProcessStartTime"
            ''' <summary>Key for resource getting something like "The amount of CPU time the process has used."</summary>
            Public Const ProcessTotalProcessorTime$ = "ProcessTotalProcessorTime"
            ''' <summary>Key for resource getting something like "The amount of CPU time the process spent outside the operating system core."</summary>
            Public Const ProcessUserProcessorTime$ = "ProcessUserProcessorTime"
            ''' <summary>Key for resource getting something like "The amount of virtual memory the process has currently allocated."</summary>
            Public Const ProcessVirtualMemorySize$ = "ProcessVirtualMemorySize"
            ''' <summary>Key for resource getting something like "The current amount of physical memory the process is using."</summary>
            Public Const ProcessWorkingSet$ = "ProcessWorkingSet"
            ''' <summary>Key for resource getting something like "The name of the module."</summary>
            Public Const ProcModModuleName$ = "ProcModModuleName"
            ''' <summary>Key for resource getting something like "The file name of the module."</summary>
            Public Const ProcModFileName$ = "ProcModFileName"
            ''' <summary>Key for resource getting something like "The memory address that the module loaded at."</summary>
            Public Const ProcModBaseAddress$ = "ProcModBaseAddress"
            ''' <summary>Key for resource getting something like "The amount of virtual memory required by the code and data in the module file."</summary>
            Public Const ProcModModuleMemorySize$ = "ProcModModuleMemorySize"
            ''' <summary>Key for resource getting something like "The memory address of the function that runs when the module is loaded."</summary>
            Public Const ProcModEntryPointAddress$ = "ProcModEntryPointAddress"
            ''' <summary>Key for resource getting something like "The verb to apply to the document specified by the FileName property."</summary>
            Public Const ProcessVerb$ = "ProcessVerb"
            ''' <summary>Key for resource getting something like "Command line arguments that will be passed to the application specified by the FileName property."</summary>
            Public Const ProcessArguments$ = "ProcessArguments"
            ''' <summary>Key for resource getting something like "Whether to show an error dialog to the user if there is an error."</summary>
            Public Const ProcessErrorDialog$ = "ProcessErrorDialog"
            ''' <summary>Key for resource getting something like "How the main window should be created when the process starts."</summary>
            Public Const ProcessWindowStyle$ = "ProcessWindowStyle"
            ''' <summary>Key for resource getting something like "Whether to start the process without creating a new window to contain it."</summary>
            Public Const ProcessCreateNoWindow$ = "ProcessCreateNoWindow"
            ''' <summary>Key for resource getting something like "Set of environment variables that apply to this process and child processes."</summary>
            Public Const ProcessEnvironmentVariables$ = "ProcessEnvironmentVariables"
            ''' <summary>Key for resource getting something like "Whether the process command input is read from the Process instance's StandardInput member."</summary>
            Public Const ProcessRedirectStandardInput$ = "ProcessRedirectStandardInput"
            ''' <summary>Key for resource getting something like "Whether the process output is written to the Process instance's StandardOutput member."</summary>
            Public Const ProcessRedirectStandardOutput$ = "ProcessRedirectStandardOutput"
            ''' <summary>Key for resource getting something like "Whether the process's error output is written to the Process instance's StandardError member."</summary>
            Public Const ProcessRedirectStandardError$ = "ProcessRedirectStandardError"
            ''' <summary>Key for resource getting something like "Whether to use the operating system shell to start the process."</summary>
            Public Const ProcessUseShellExecute$ = "ProcessUseShellExecute"
            ''' <summary>Key for resource getting something like "The current base priority of the thread."</summary>
            Public Const ThreadBasePriority$ = "ThreadBasePriority"
            ''' <summary>Key for resource getting something like "The current priority level of the thread."</summary>
            Public Const ThreadCurrentPriority$ = "ThreadCurrentPriority"
            ''' <summary>Key for resource getting something like "The unique identifier for the thread."</summary>
            Public Const ThreadId$ = "ThreadId"
            ''' <summary>Key for resource getting something like "Whether the thread would like a priority boost when the user interacts with UI associated with the thread."</summary>
            Public Const ThreadPriorityBoostEnabled$ = "ThreadPriorityBoostEnabled"
            ''' <summary>Key for resource getting something like "The priority level of the thread."</summary>
            Public Const ThreadPriorityLevel$ = "ThreadPriorityLevel"
            ''' <summary>Key for resource getting something like "The amount of CPU time the thread spent inside the operating system core."</summary>
            Public Const ThreadPrivilegedProcessorTime$ = "ThreadPrivilegedProcessorTime"
            ''' <summary>Key for resource getting something like "The memory address of the function that was run when the thread started."</summary>
            Public Const ThreadStartAddress$ = "ThreadStartAddress"
            ''' <summary>Key for resource getting something like "The time the thread was started."</summary>
            Public Const ThreadStartTime$ = "ThreadStartTime"
            ''' <summary>Key for resource getting something like "The execution state of the thread."</summary>
            Public Const ThreadThreadState$ = "ThreadThreadState"
            ''' <summary>Key for resource getting something like "The amount of CPU time the thread has consumed since it was started."</summary>
            Public Const ThreadTotalProcessorTime$ = "ThreadTotalProcessorTime"
            ''' <summary>Key for resource getting something like "The amount of CPU time the thread spent outside the operating system core."</summary>
            Public Const ThreadUserProcessorTime$ = "ThreadUserProcessorTime"
            ''' <summary>Key for resource getting something like "The reason the thread is waiting, if it is waiting."</summary>
            Public Const ThreadWaitReason$ = "ThreadWaitReason"
            ''' <summary>Key for resource getting something like "(Default)"</summary>
            Public Const VerbEditorDefault$ = "VerbEditorDefault"
            ''' <summary>Key for resource getting something like "The key '{0}' does not exist in the appSettings configuration section."</summary>
            Public Const AppSettingsReaderNoKey$ = "AppSettingsReaderNoKey"
            ''' <summary>Key for resource getting something like "Type '{0}' does not have a Parse method."</summary>
            Public Const AppSettingsReaderNoParser$ = "AppSettingsReaderNoParser"
            ''' <summary>Key for resource getting something like "The value '{0}' was found in the appSettings configuration section for key '{1}', and this value is not a valid {2}."</summary>
            Public Const AppSettingsReaderCantParse$ = "AppSettingsReaderCantParse"
            ''' <summary>Key for resource getting something like "(empty string)"</summary>
            Public Const AppSettingsReaderEmptyString$ = "AppSettingsReaderEmptyString"
            ''' <summary>Key for resource getting something like "Invalid permission state."</summary>
            Public Const InvalidPermissionState$ = "InvalidPermissionState"
            ''' <summary>Key for resource getting something like "The number of elements on the access path must be the same as the number of tag names."</summary>
            Public Const PermissionNumberOfElements$ = "PermissionNumberOfElements"
            ''' <summary>Key for resource getting something like "The item provided already exists."</summary>
            Public Const PermissionItemExists$ = "PermissionItemExists"
            ''' <summary>Key for resource getting something like "The requested item doesn't exist."</summary>
            Public Const PermissionItemDoesntExist$ = "PermissionItemDoesntExist"
            ''' <summary>Key for resource getting something like "Parameter must be of type enum."</summary>
            Public Const PermissionBadParameterEnum$ = "PermissionBadParameterEnum"
            ''' <summary>Key for resource getting something like "Length must be greater than {0}."</summary>
            Public Const PermissionInvalidLength$ = "PermissionInvalidLength"
            ''' <summary>Key for resource getting something like "Type mismatch."</summary>
            Public Const PermissionTypeMismatch$ = "PermissionTypeMismatch"
            ''' <summary>Key for resource getting something like "'securityElement' was not a permission element."</summary>
            Public Const Argument_NotAPermissionElement$ = "Argument_NotAPermissionElement"
            ''' <summary>Key for resource getting something like "Invalid Xml - can only parse elements of version one."</summary>
            Public Const Argument_InvalidXMLBadVersion$ = "Argument_InvalidXMLBadVersion"
            ''' <summary>Key for resource getting something like "Invalid permission level."</summary>
            Public Const InvalidPermissionLevel$ = "InvalidPermissionLevel"
            ''' <summary>Key for resource getting something like "Target not WebBrowserPermissionLevel."</summary>
            Public Const TargetNotWebBrowserPermissionLevel$ = "TargetNotWebBrowserPermissionLevel"
            ''' <summary>Key for resource getting something like "Bad Xml {0}"</summary>
            Public Const WebBrowserBadXml$ = "WebBrowserBadXml"
            ''' <summary>Key for resource getting something like "Need a non negative number for capacity."</summary>
            Public Const KeyedCollNeedNonNegativeNum$ = "KeyedCollNeedNonNegativeNum"
            ''' <summary>Key for resource getting something like "Cannot add item since the item with the key already exists in the collection."</summary>
            Public Const KeyedCollDuplicateKey$ = "KeyedCollDuplicateKey"
            ''' <summary>Key for resource getting something like "The key reference with respect to which the insertion operation was to be performed was not found."</summary>
            Public Const KeyedCollReferenceKeyNotFound$ = "KeyedCollReferenceKeyNotFound"
            ''' <summary>Key for resource getting something like "Cannot find the key {0} in the collection."</summary>
            Public Const KeyedCollKeyNotFound$ = "KeyedCollKeyNotFound"
            ''' <summary>Key for resource getting something like "Keys must be non-null non-empty Strings."</summary>
            Public Const KeyedCollInvalidKey$ = "KeyedCollInvalidKey"
            ''' <summary>Key for resource getting something like "Capacity overflowed and went negative.  Check capacity of the collection."</summary>
            Public Const KeyedCollCapacityOverflow$ = "KeyedCollCapacityOverflow"
            ''' <summary>Key for resource getting something like "The enumeration has already completed."</summary>
            Public Const InvalidOperation_EnumEnded$ = "InvalidOperation_EnumEnded"
            ''' <summary>Key for resource getting something like "The OrderedDictionary is readonly and cannot be modified."</summary>
            Public Const OrderedDictionary_ReadOnly$ = "OrderedDictionary_ReadOnly"
            ''' <summary>Key for resource getting something like "There was an error deserializing the OrderedDictionary.  The ArrayList does not contain DictionaryEntries."</summary>
            Public Const OrderedDictionary_SerializationMismatch$ = "OrderedDictionary_SerializationMismatch"
            ''' <summary>Key for resource getting something like "An exception occurred during the operation, making the result invalid.  Check InnerException for exception details."</summary>
            Public Const Async_ExceptionOccurred$ = "Async_ExceptionOccurred"
            ''' <summary>Key for resource getting something like "Queuing WaitCallback failed."</summary>
            Public Const Async_QueueingFailed$ = "Async_QueueingFailed"
            ''' <summary>Key for resource getting something like "Operation has been cancelled."</summary>
            Public Const Async_OperationCancelled$ = "Async_OperationCancelled"
            ''' <summary>Key for resource getting something like "This operation has already had OperationCompleted called on it and further calls are illegal."</summary>
            Public Const Async_OperationAlreadyCompleted$ = "Async_OperationAlreadyCompleted"
            ''' <summary>Key for resource getting something like "A non-null SendOrPostCallback must be supplied."</summary>
            Public Const Async_NullDelegate$ = "Async_NullDelegate"
            ''' <summary>Key for resource getting something like "This BackgroundWorker is currently busy and cannot run multiple tasks concurrently."</summary>
            Public Const BackgroundWorker_WorkerAlreadyRunning$ = "BackgroundWorker_WorkerAlreadyRunning"
            ''' <summary>Key for resource getting something like "This BackgroundWorker states that it doesn't report progress. Modify WorkerReportsProgress to state that it does report progress."</summary>
            Public Const BackgroundWorker_WorkerDoesntReportProgress$ = "BackgroundWorker_WorkerDoesntReportProgress"
            ''' <summary>Key for resource getting something like "This BackgroundWorker states that it doesn't support cancellation. Modify WorkerSupportsCancellation to state that it does support cancellation."</summary>
            Public Const BackgroundWorker_WorkerDoesntSupportCancellation$ = "BackgroundWorker_WorkerDoesntSupportCancellation"
            ''' <summary>Key for resource getting something like "Percentage progress made in operation."</summary>
            Public Const Async_ProgressChangedEventArgs_ProgressPercentage$ = "Async_ProgressChangedEventArgs_ProgressPercentage"
            ''' <summary>Key for resource getting something like "User-supplied state to identify operation."</summary>
            Public Const Async_ProgressChangedEventArgs_UserState$ = "Async_ProgressChangedEventArgs_UserState"
            ''' <summary>Key for resource getting something like "True if operation was cancelled."</summary>
            Public Const Async_AsyncEventArgs_Cancelled$ = "Async_AsyncEventArgs_Cancelled"
            ''' <summary>Key for resource getting something like "Exception that occurred during operation.  Null if no error."</summary>
            Public Const Async_AsyncEventArgs_Error$ = "Async_AsyncEventArgs_Error"
            ''' <summary>Key for resource getting something like "User-supplied state to identify operation."</summary>
            Public Const Async_AsyncEventArgs_UserState$ = "Async_AsyncEventArgs_UserState"
            ''' <summary>Key for resource getting something like "Has the user attempted to cancel the operation? To be accessed from DoWork event handler."</summary>
            Public Const BackgroundWorker_CancellationPending$ = "BackgroundWorker_CancellationPending"
            ''' <summary>Key for resource getting something like "Event handler to be run on a different thread when the operation begins."</summary>
            Public Const BackgroundWorker_DoWork$ = "BackgroundWorker_DoWork"
            ''' <summary>Key for resource getting something like "Is the worker still currently working on a background operation?"</summary>
            Public Const BackgroundWorker_IsBusy$ = "BackgroundWorker_IsBusy"
            ''' <summary>Key for resource getting something like "Raised when the worker thread indicates that some progress has been made."</summary>
            Public Const BackgroundWorker_ProgressChanged$ = "BackgroundWorker_ProgressChanged"
            ''' <summary>Key for resource getting something like "Raised when the worker has completed (either through success, failure, or cancellation)."</summary>
            Public Const BackgroundWorker_RunWorkerCompleted$ = "BackgroundWorker_RunWorkerCompleted"
            ''' <summary>Key for resource getting something like "Whether the worker will report progress."</summary>
            Public Const BackgroundWorker_WorkerReportsProgress$ = "BackgroundWorker_WorkerReportsProgress"
            ''' <summary>Key for resource getting something like "Whether the worker supports cancellation."</summary>
            Public Const BackgroundWorker_WorkerSupportsCancellation$ = "BackgroundWorker_WorkerSupportsCancellation"
            ''' <summary>Key for resource getting something like "Argument passed into the worker handler from BackgroundWorker.RunWorkerAsync."</summary>
            Public Const BackgroundWorker_DoWorkEventArgs_Argument$ = "BackgroundWorker_DoWorkEventArgs_Argument"
            ''' <summary>Key for resource getting something like "Result from the worker function."</summary>
            Public Const BackgroundWorker_DoWorkEventArgs_Result$ = "BackgroundWorker_DoWorkEventArgs_Result"
            ''' <summary>Key for resource getting something like "Executes an operation on a separate thread."</summary>
            Public Const BackgroundWorker_Desc$ = "BackgroundWorker_Desc"
            ''' <summary>Key for resource getting something like "(New...)"</summary>
            Public Const InstanceCreationEditorDefaultText$ = "InstanceCreationEditorDefaultText"
            ''' <summary>Key for resource getting something like "Scope must be PropertyTabScope.Document or PropertyTabScope.Component"</summary>
            Public Const PropertyTabAttributeBadPropertyTabScope$ = "PropertyTabAttributeBadPropertyTabScope"
            ''' <summary>Key for resource getting something like "Couldn't find type {0}"</summary>
            Public Const PropertyTabAttributeTypeLoadException$ = "PropertyTabAttributeTypeLoadException"
            ''' <summary>Key for resource getting something like "tabClasses must have the same number of items as tabScopes"</summary>
            Public Const PropertyTabAttributeArrayLengthMismatch$ = "PropertyTabAttributeArrayLengthMismatch"
            ''' <summary>Key for resource getting something like "An array of tab type names or tab types must be specified"</summary>
            Public Const PropertyTabAttributeParamsBothNull$ = "PropertyTabAttributeParamsBothNull"
            ''' <summary>Key for resource getting something like "Parameter cannot be static."</summary>
            Public Const InstanceDescriptorCannotBeStatic$ = "InstanceDescriptorCannotBeStatic"
            ''' <summary>Key for resource getting something like "Parameter must be static."</summary>
            Public Const InstanceDescriptorMustBeStatic$ = "InstanceDescriptorMustBeStatic"
            ''' <summary>Key for resource getting something like "Parameter must be readable."</summary>
            Public Const InstanceDescriptorMustBeReadable$ = "InstanceDescriptorMustBeReadable"
            ''' <summary>Key for resource getting something like "Length mismatch."</summary>
            Public Const InstanceDescriptorLengthMismatch$ = "InstanceDescriptorLengthMismatch"
            ''' <summary>Key for resource getting something like "Failed to create ToolboxItem of type: {0}"</summary>
            Public Const ToolboxItemAttributeFailedGetType$ = "ToolboxItemAttributeFailedGetType"
            ''' <summary>Key for resource getting something like "Parameter must be of type PropertyDescriptor."</summary>
            Public Const PropertyDescriptorCollectionBadValue$ = "PropertyDescriptorCollectionBadValue"
            ''' <summary>Key for resource getting something like "Parameter must be of type int or string."</summary>
            Public Const PropertyDescriptorCollectionBadKey$ = "PropertyDescriptorCollectionBadKey"
            ''' <summary>Key for resource getting something like "Bad Xml {0}"</summary>
            Public Const AspNetHostingPermissionBadXml$ = "AspNetHostingPermissionBadXml"
            ''' <summary>Key for resource getting something like "The magic number in GZip header is not correct. Make sure you are passing in a GZip stream."</summary>
            Public Const CorruptedGZipHeader$ = "CorruptedGZipHeader"
            ''' <summary>Key for resource getting something like "The compression mode specified in GZip header is unknown."</summary>
            Public Const UnknownCompressionMode$ = "UnknownCompressionMode"
            ''' <summary>Key for resource getting something like "Decoder is in some unknown state. This might be caused by corrupted data."</summary>
            Public Const UnknownState$ = "UnknownState"
            ''' <summary>Key for resource getting something like "Failed to construct a huffman tree using the length array. The stream might be corrupted."</summary>
            Public Const InvalidHuffmanData$ = "InvalidHuffmanData"
            ''' <summary>Key for resource getting something like "The CRC in GZip footer does not match the CRC calculated from the decompressed data."</summary>
            Public Const InvalidCRC$ = "InvalidCRC"
            ''' <summary>Key for resource getting something like "The stream size in GZip footer does not match the real stream size."</summary>
            Public Const InvalidStreamSize$ = "InvalidStreamSize"
            ''' <summary>Key for resource getting something like "Unknown block type. Stream might be corrupted."</summary>
            Public Const UnknownBlockType$ = "UnknownBlockType"
            ''' <summary>Key for resource getting something like "Block length does not match with its complement."</summary>
            Public Const InvalidBlockLength$ = "InvalidBlockLength"
            ''' <summary>Key for resource getting something like "Found invalid data while decoding."</summary>
            Public Const GenericInvalidData$ = "GenericInvalidData"
            ''' <summary>Key for resource getting something like "Reading from the compression stream is not supported."</summary>
            Public Const CannotReadFromDeflateStream$ = "CannotReadFromDeflateStream"
            ''' <summary>Key for resource getting something like "Writing to the compression stream is not supported."</summary>
            Public Const CannotWriteToDeflateStream$ = "CannotWriteToDeflateStream"
            ''' <summary>Key for resource getting something like "The base stream is not readable."</summary>
            Public Const NotReadableStream$ = "NotReadableStream"
            ''' <summary>Key for resource getting something like "The base stream is not writeable."</summary>
            Public Const NotWriteableStream$ = "NotWriteableStream"
            ''' <summary>Key for resource getting something like "Offset plus count is larger than the length of target array."</summary>
            Public Const InvalidArgumentOffsetCount$ = "InvalidArgumentOffsetCount"
            ''' <summary>Key for resource getting something like "Only one asynchronous reader is allowed time at one time."</summary>
            Public Const InvalidBeginCall$ = "InvalidBeginCall"
            ''' <summary>Key for resource getting something like "EndRead is only callable when there is one pending asynchronous reader ."</summary>
            Public Const InvalidEndCall$ = "InvalidEndCall"
            ''' <summary>Key for resource getting something like "The gzip stream can't contain more than 4GB data."</summary>
            Public Const StreamSizeOverflow$ = "StreamSizeOverflow"
            ''' <summary>Key for resource getting something like "Handle collector count overflows or underflows."</summary>
            Public Const InvalidOperation_HCCountOverflow$ = "InvalidOperation_HCCountOverflow"
            ''' <summary>Key for resource getting something like "maximumThreshold cannot be less than initialThreshold."</summary>
            Public Const Argument_InvalidThreshold$ = "Argument_InvalidThreshold"
            ''' <summary>Key for resource getting something like "The initial count for the semaphore must be greater than or equal to zero and less than the maximum count."</summary>
            Public Const Argument_SemaphoreInitialMaximum$ = "Argument_SemaphoreInitialMaximum"
            ''' <summary>Key for resource getting something like "The name can be no more than 260 characters in length."</summary>
            Public Const Argument_WaitHandleNameTooLong$ = "Argument_WaitHandleNameTooLong"
            ''' <summary>Key for resource getting something like "Adding the given count to the semaphore would cause it to exceed its maximum count."</summary>
            Public Const Threading_SemaphoreFullException$ = "Threading_SemaphoreFullException"
            ''' <summary>Key for resource getting something like "A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name."</summary>
            Public Const WaitHandleCannotBeOpenedException_InvalidHandle$ = "WaitHandleCannotBeOpenedException_InvalidHandle"
            ''' <summary>Key for resource getting something like "Argument was not a permission Element."</summary>
            Public Const ArgumentNotAPermissionElement$ = "ArgumentNotAPermissionElement"
            ''' <summary>Key for resource getting something like "Argument should be of type {0}."</summary>
            Public Const ArgumentWrongType$ = "ArgumentWrongType"
            ''' <summary>Key for resource getting something like "Xml version was wrong."</summary>
            Public Const BadXmlVersion$ = "BadXmlVersion"
            ''' <summary>Key for resource getting something like "Binary serialization is current not supported by the LocalFileSettingsProvider."</summary>
            Public Const BinarySerializationNotSupported$ = "BinarySerializationNotSupported"
            ''' <summary>Key for resource getting something like "The setting {0} has both an ApplicationScopedSettingAttribute and a UserScopedSettingAttribute."</summary>
            Public Const BothScopeAttributes$ = "BothScopeAttributes"
            ''' <summary>Key for resource getting something like "The setting {0} does not have either an ApplicationScopedSettingAttribute or UserScopedSettingAttribute."</summary>
            Public Const NoScopeAttributes$ = "NoScopeAttributes"
            ''' <summary>Key for resource getting something like "Position cannot be less than zero."</summary>
            Public Const PositionOutOfRange$ = "PositionOutOfRange"
            ''' <summary>Key for resource getting something like "Failed to instantiate provider: {0}."</summary>
            Public Const ProviderInstantiationFailed$ = "ProviderInstantiationFailed"
            ''' <summary>Key for resource getting something like "Failed to load provider type: {0}."</summary>
            Public Const ProviderTypeLoadFailed$ = "ProviderTypeLoadFailed"
            ''' <summary>Key for resource getting something like "Error saving {0} - The LocalFileSettingsProvider does not support saving changes to application-scoped settings."</summary>
            Public Const SaveAppScopedNotSupported$ = "SaveAppScopedNotSupported"
            ''' <summary>Key for resource getting something like "Failed to reset settings: unable to access the configuration section."</summary>
            Public Const SettingsResetFailed$ = "SettingsResetFailed"
            ''' <summary>Key for resource getting something like "Failed to save settings: {0}"</summary>
            Public Const SettingsSaveFailed$ = "SettingsSaveFailed"
            ''' <summary>Key for resource getting something like "Failed to save settings: unable to access the configuration section."</summary>
            Public Const SettingsSaveFailedNoSection$ = "SettingsSaveFailedNoSection"
            ''' <summary>Key for resource getting something like "Could not use String deserialization for setting: {0}."</summary>
            Public Const StringDeserializationFailed$ = "StringDeserializationFailed"
            ''' <summary>Key for resource getting something like "Could not use String serialization for setting: {0}."</summary>
            Public Const StringSerializationFailed$ = "StringSerializationFailed"
            ''' <summary>Key for resource getting something like "Unknown serialization format specified."</summary>
            Public Const UnknownSerializationFormat$ = "UnknownSerializationFormat"
            ''' <summary>Key for resource getting something like "Unknown SeekOrigin specified."</summary>
            Public Const UnknownSeekOrigin$ = "UnknownSeekOrigin"
            ''' <summary>Key for resource getting something like "Unknown ConfigurationUserLevel specified."</summary>
            Public Const UnknownUserLevel$ = "UnknownUserLevel"
            ''' <summary>Key for resource getting something like "The current configuration system does not support user-scoped settings."</summary>
            Public Const UserSettingsNotSupported$ = "UserSettingsNotSupported"
            ''' <summary>Key for resource getting something like "Could not use Xml deserialization for setting: {0}."</summary>
            Public Const XmlDeserializationFailed$ = "XmlDeserializationFailed"
            ''' <summary>Key for resource getting something like "Could not use Xml serialization for setting: {0}."</summary>
            Public Const XmlSerializationFailed$ = "XmlSerializationFailed"
            ''' <summary>Key for resource getting something like "Relationships between {0}.{1} and {2}.{3} are not supported."</summary>
            Public Const MemberRelationshipService_RelationshipNotSupported$ = "MemberRelationshipService_RelationshipNotSupported"
            ''' <summary>Key for resource getting something like "The PasswordChar and PromptChar values cannot be the same."</summary>
            Public Const MaskedTextProviderPasswordAndPromptCharError$ = "MaskedTextProviderPasswordAndPromptCharError"
            ''' <summary>Key for resource getting something like "The specified character value is not allowed for this property."</summary>
            Public Const MaskedTextProviderInvalidCharError$ = "MaskedTextProviderInvalidCharError"
            ''' <summary>Key for resource getting something like "The Mask value cannot be null or empty."</summary>
            Public Const MaskedTextProviderMaskNullOrEmpty$ = "MaskedTextProviderMaskNullOrEmpty"
            ''' <summary>Key for resource getting something like "The specified mask contains invalid characters."</summary>
            Public Const MaskedTextProviderMaskInvalidChar$ = "MaskedTextProviderMaskInvalidChar"
            ''' <summary>Key for resource getting something like "Failed to get marshaler for IID {0}."</summary>
            Public Const StandardOleMarshalObjectGetMarshalerFailed$ = "StandardOleMarshalObjectGetMarshalerFailed"
            ''' <summary>Key for resource getting something like "Could not determine a universal resource identifier for the sound location."</summary>
            Public Const SoundAPIBadSoundLocation$ = "SoundAPIBadSoundLocation"
            ''' <summary>Key for resource getting something like "Please be sure a sound file exists at the specified location."</summary>
            Public Const SoundAPIFileDoesNotExist$ = "SoundAPIFileDoesNotExist"
            ''' <summary>Key for resource getting something like "Sound API only supports playing PCM wave files."</summary>
            Public Const SoundAPIFormatNotSupported$ = "SoundAPIFormatNotSupported"
            ''' <summary>Key for resource getting something like "The file located at {0} is not a valid wave file."</summary>
            Public Const SoundAPIInvalidWaveFile$ = "SoundAPIInvalidWaveFile"
            ''' <summary>Key for resource getting something like "The wave header is corrupt."</summary>
            Public Const SoundAPIInvalidWaveHeader$ = "SoundAPIInvalidWaveHeader"
            ''' <summary>Key for resource getting something like "The request to load the wave file in memory timed out."</summary>
            Public Const SoundAPILoadTimedOut$ = "SoundAPILoadTimedOut"
            ''' <summary>Key for resource getting something like "The LoadTimeout property of a SoundPlayer cannot be negative."</summary>
            Public Const SoundAPILoadTimeout$ = "SoundAPILoadTimeout"
            ''' <summary>Key for resource getting something like "There was an error reading the file located at {0}. Please make sure that a valid wave file exists at the specified location."</summary>
            Public Const SoundAPIReadError$ = "SoundAPIReadError"
#End Region
        End Structure
    End Class

    ''' <summary>Exposes functionality of internall (friend) .NET class System.SRDescriptionAttribute - applies <see cref="DescriptionAttribute"/> which's value is loaded from internal .NET Framework resource</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
  ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class SRDescriptionAttribute
        Inherits DescriptionAttribute
        ''' <summary>Contains value of the <see cref="ResourceKey"/> property</summary>
        Private _ResourceKey As String
        ''' <summary>CTor</summary>
        ''' <param name="ResourceKey">Known resource key to get value from</param>
        ''' <remarks>This CTor is only hint for intellisense. However it is fully functional you will propebly never use it.</remarks>
        Public Sub New(ByVal ResourceKey As SystemResources.KnownValues)
            _ResourceKey = ResourceKey
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="ResourceKey">Resource key to get value from</param>
        Public Sub New(ByVal ResourceKey As String)
            _ResourceKey = ResourceKey
        End Sub
        ''' <summary>Gets the description stored in this attribute.</summary>
        ''' <returns>The description stored in system resource key <see cref="ResourceKey"/></returns>
        ''' <exception cref="TargetInvocationException">Unknown unexpected error when obtaining resource value</exception>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return SystemResources.Value(ResourceKey)
            End Get
        End Property
        ''' <summary>Key of system resource that contains value of this attribute</summary>
        Public ReadOnly Property ResourceKey() As String
            Get
                Return _ResourceKey
            End Get
        End Property
    End Class

    ''' <summary>Exposes functionality of internall (friend) .NET class System.SRCategoryAttribute - applies <see cref="DescriptionAttribute"/> which's value is loaded from internal .NET Framework resource</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
  ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class SRCategoryAttribute
        Inherits DescriptionAttribute
        ''' <summary>CTor</summary>
        ''' <param name="ResourceKey">Known resource key to get value from</param>
        ''' <remarks>This CTor is only hint for intellisense. However it is fully functional you will propebly never use it.</remarks>
        Public Sub New(ByVal ResourceKey As SystemResources.KnownValues)
            MyBase.New(SystemResources.Value(ResourceKey))
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="ResourceKey">Resource key to get value from</param>
        Public Sub New(ByVal ResourceKey As String)
            MyBase.New(SystemResources.Value(ResourceKey))
        End Sub
    End Class
End Namespace
#End If
