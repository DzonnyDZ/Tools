#pragma once
#include "Common.h"
#include "PluginBase.h"
#include "Plugin/contplug.h"
#include "ContentFieldSpecification.h"
#include "PluginMethodAttribute.h"
#include "Attributes.h"

namespace Tools {
    namespace TotalCommanderT {
        using namespace System;
        using namespace System::ComponentModel;

        /// <summary>Common base class for plugins tha can provide custom columns</summary>
        /// <remarks><note type="inheritinfo">Do not derive directly from this class as it does not represent any concrete plugin</note>
        /// <note>Total Commander Plugin Builder emits content plugin functions only when both <see cref="ContentPluginBase::SupportedFields"/>'s getter and <see cref="ContentPluginBase::GetValue"/> functions are implemented in derived class. To be implemented means that the most derived implementation of the function is not marked <see cref="MethodNotSupportedAttribute"/>.</note></remarks>
        /// <version version="1.5.3">Added necessary functions and properties. Before 1.5.3 this class was empty, it had no members not derived from <see cref="PluginBase"/>.</version>.
        /// <version version="1.5.4">Added Unicode support</version>
        public ref class ContentPluginBase abstract : PluginBase {
        internal:
            /// <summary>CTor - creates new instance of the <see cref="ContentPluginBase"/> class</summary>
            ContentPluginBase();
        public:
            /// <summary>Called to enumerate all supported fields. <paramref name="FieldIndex"/> is increased by 1 starting from 0 until the plugin returns <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.NoMoreFields"/>.</summary>
            /// <param name="fieldIndex">The index of the field for which TC requests information. Starting with 0, the <paramref name="FieldIndex"/> is increased until the plugin returns an error.</param>
            /// <param name="fieldName">Here the plugin has to return the name of the field with index <paramref name="FieldIndex"/>. The field may not contain the following chars: . (dot) | (vertical line) : (colon). You may return a maximum of <paramref name="maxlen"/> characters, including the trailing 0.</param>
            /// <param name="units">When a field supports several units like bytes, kbytes, Mbytes etc, they need to be specified here in the following form: bytes|kbytes|Mbytes . The separator is the vertical dash (Alt+0124). As field names, unit names may not contain a vertical dash, a dot, or a colon. You may return a maximum of <paramref name="maxlen"/> characters, including the trailing 0.
            /// <para>If the field type is <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Enum"/>, the plugin needs to return all possible values here. Example: The field "File Type" of the built-in content plugin can have the values "File", "Folder" and "Reparse point". The available choices need to be returned in the following form: File|Folder|Reparse point . The same separator is used as for Units. You may return a maximum of <paramref name="maxlen"/> characters, including the trailing 0. The field type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Enum"/> does NOT support any units.</para></param>
            /// <param name="maxlen">The maximum number of characters, including the trailing 0, which may be returned in each of the fields.</param>
            /// <returns>One of the <see cref="ContentFieldType"/> values</returns>
            /// <remarks>Please note that fields of type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/> only show up in the search function, not in the multi-rename tool or the file lists. All fields of this type MUST be placed at the END of the field list, otherwise you will get errors in Total Commander! This is necessary because these fields will be removed from field lists e.g. in the "configure custom column view" dialog. You should use the <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.String"/> type for shorter one line texts suitable for displaying in file lists and for renaming.
            /// <note>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/> is not supported by file system plugin.</note>
            /// <para>This function is called by Total Commander and is not intended for direct use</para>
            /// <para>Unlike majority of Total Commander content plugin functions, this function is not implemented by function with same name but without the "<c>Content</c>" prefix. This function is implemented by getter of the <see cref="SupportedFields"/> property.</para>
            /// <para>This function is ANSI-only - field names must be ANSI!</para></remarks>
            /// <exception cref="InvalidOperationException">Uncatchable exception is thrown when: Item at index <paramref name="FieldIndex"/> in array returned by <see cref="SupportedFields"/> contains diallowed characters in <see cref="ContentFieldSpecification::Units"/> -or- Length of <see cref="ContentFieldSpecification::FieldName"/> is greater than <paramref name="maxlen"/>-1 -or- Sum of lengths of <see cref="ContentFieldSpecification::Units"/> plus number of them, minus 1 is greater than <paramref name="maxlen"/>-1 -or- Value of the <see cref="ContentFieldSpecification::FieldIndex"/> property differs form index at which the instance was returned.</exception>
            /// <seealso cref="SupportedFields"/>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter names (except <paramref name="maxlen"/> which was already OK) converted to cammeCase</version>
            [EditorBrowsableAttribute(EditorBrowsableState::Never)]
            [CLSCompliantAttribute(false)]
            [PluginMethod("get_SupportedFields", "TC_C_GETSUPPORTEDFIELD")]
            int ContentGetSupportedField(int fieldIndex, char* fieldName, char* units, int maxlen);
            /// <summary>When overridden in derived class gets all supported custom fields.</summary>
            /// <returns>Array columns specifications supported by this plugin. Null or an empty array where there are no plugin-specified columns.
            /// <note>When array returned contains columns of type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/> there shall be no non-<see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/> columns at higher index than column of type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/>. This is required for Total Commander by way it handles fulltext columns.</note>
            /// </returns>
            /// <exception cref="NotSupportedException">The actual implementation of property getter is marked with <see cref="MethodNotSupportedAttribute"/>.</exception>
            /// <remarks>Custom fields are custom columns provided by plugin for details view.
            /// <para>When most derived implementation of property getter is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the property.</para>
            /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
            /// <note type="inheritinfo">Items of array returned by this property must fulfill several constraints:
            /// <list type="bullet">
            /// <item><see cref="ContentFieldSpecification::FieldName"/> shall not contain dot (.), pipe (|), colon (:) or nullchar.</item>
            /// <item><see cref="ContentFieldSpecification::FieldName"/> shall not be longer than <see cref="FieldNameMaxLen"/>.</item>
            /// <item><see cref="ContentFieldSpecification::Units"/> shall not contain item containing dot (.), pipe (|), colon (:) or nullchar.</item>
            /// <item>Sum of lengths of items of <see cref="ContentFieldSpecification::Units"/> plus number of them minus one shall not be greater than <see cref="FieldNameMaxLen"/>.
            /// <para>This is because how Total Commander handles units: Unit names are concatenated to single string separated by pipes (|). And length of the string shall not be greater than <see cref="FieldNameMaxLen"/>.</para></item>
            /// <item>All fields that are not of type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/> must be at indexes lower than index of any item of type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/>.</item>
            /// <item>Value of the <see cref="ContentFieldSpecification::FieldIndex"/> property shall be the same as index at which it is returned.</item>
            /// </list>Violation of any of these rules may lead to uncatchable exception being thrown or to unpredictable behavior of Total Commander.</note></remarks>
            /// <version version="1.5.3">This property is new in version 1.5.3</version>
            virtual property cli::array<ContentFieldSpecification^>^ SupportedFields {
                [MethodNotSupported]
                virtual cli::array<ContentFieldSpecification^>^ get();
            }
        private:
            /// <summary>Contains value of the <see cref="FieldNameMaxLen"/> property</summary>
            int fieldNameMaxLen;
        protected:
            /// <summary>Gets maximal length of string that can be passed to <see cref="ContentFieldSpecification::FieldName"/>.</summary>
            /// <returns>Maximal length of string that can be passed to <see cref="ContentFieldSpecification::FieldName"/></returns>
            /// <remarks>Value of this property is valid only when accessed from within code of getter of the <see cref="SupportedFields"/> property (or methods it calls).</remarks>
            /// <version version="1.5.3">This property is new in version 1.5.3</version>
            property int FieldNameMaxLen {int get(); }
        public:
            /// <summary>Called to retrieve the value of a specific field for a given file, e.g. the date field of a file.</summary>
            /// <param name="fileName">The name of the file (in case of file system plugin in plugin namespace) for which the plugin needs to return the field data.</param>
            /// <param name="fieldIndex">The index of the field for which the content has to be returned. This is the same index as the FieldIndex value in <see cref="ContentGetSupportedField"/>.</param>
            /// <param name="unitIndex">The index of the unit used. If no unit string was returned, UnitIndex is 0. For <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/>, <paramref name="UnitIndex"/> contains the offset of the data to be read.</param>
            /// <param name="fieldValue">Here the plugin needs to return the requested data. The data format depends on the field type. ft_delayed, ft_ondemand: You may return a zero-terminated string as in ft_string, which will be shown until the actual value has been extracted. Requires plugin version>=1.4.</param>
            /// <param name="maxlen">The maximum number of bytes fitting into the <paramref name="FieldValue"/> variable.</param>
            /// <param name="flags">Flags controlling function behavior</param>
            /// <param name="wide">True if this method is called as Unicode, false if it is called as ANSI. This value indicates how strings passed through <paramref name="fieldValue"/> will be treated. <paramref name="fileName"/> is always treated as Unicode.
            /// <returns>Return the field type in case of success, or one of the error values otherwise</returns>
            /// <exception cref="InvalidOperationException"><see cref="GetValue"/> returned string that is longer than <paramref name="maxlen"/>-1.</exception>
            /// <exception cref="Tools::TypeMismatchException"><see cref="GetValue"/> returned value of unexpected type.</exception>
            /// <remarks>
            /// <para>This function is called by Total Commander and is not intended for direct use</para>
            /// <para>Excptions thrown by this function are usually uncatchable, because it is called by Total Commander, which cannot handle managed exceptions. So, do not return values causing exceptions from your <see cref="GetValue"/>!</para>
            /// <para>
            /// Parameter <paramref name="wide"/> is not part of Total Commander plugin interface contract.
            /// It's added here to distinguish between ANSI and Unicode caller. ANSI faller should pass false here. Unicode caller should pass true here.
            /// Even ANSI caller must pass Unicode value to <paramref name="fileName"/> however any strings contained in <paramref name="fieldValue"/> will be interpreted as Unicode or ASNI characters depending on <paramref name="wide"/>.
            /// </para></remarks>
            /// <seealso cref="GetValue"/>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter names <c>FileName</c>, <c>FieldIndex</c>, <c>UnitIndex</c> and <c>FieldValue</c> converted to camelCase</version>
            /// <version version="1.5.4">Type of parameter <paramref name="fileName"> changed from <see cref="char*"/> to <see cref="wchar_t*"/> - the function now supports Unicode</version>
            /// <version version="1.5.4">Parameter <paramref name="wide"/> added and ANSI/Unicode handling of <paramref name="fieldValue"/> depending on type of caller introduced.</version>
            [EditorBrowsableAttribute(EditorBrowsableState::Never)]
            [CLSCompliantAttribute(false)]
            [PluginMethod("GetValue", "TC_C_GETVALUE")]
            int ContentGetValue(wchar_t* fileName, int fieldIndex, int unitIndex, void* fieldValue, int maxlen, int flags, bool wide);
        private:
            /// <summary>Caches streams returned by <see cref="GetValue"/></summary>
            System::Collections::Generic::Dictionary<String^, IO::Stream^>^ StreamCache;
            /// <summary>Gets portion of full text filed value from cached stream</summary>
            /// <param name="streamKey">Key in form "<c>path|index</c>" of stream inside <see cref="StreamCache"/></param>
            /// <param name="offset">Offset to stream to start reading at; -1 to remove stream from cache.</param>
            /// <param name="fieldValue">Target to copy stream data to</param>
            /// <param name="maxlen">Maximum number of bytes to be read from stream to <paramref name="fieldValue"/> (including terminating 0)</param>
            /// <returns><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/> when data from stream was read or <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FieldEmpty"/> when <paramref name="offset"/> is beyond the end of the stream. Returns <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.NoSuchField"/> when stream with key <paramref name="StreamKey"/> does not exist in <see cref="StreamCache"/>.</returns> 
            /// <version version="1.5.4">Names of parameters <c>StreamKey</c> and <c>FieldValue</c> changed to caleCase</version>
            int GetCachedStreamData(String^ streamKey, int offset, Byte* fieldValue, int maxlen);
        public:
            /// <summary>When overridden in derived class called to retrieve the value of a specific field for a given file, e.g. the date field of a file.</summary>
            /// <param name="FileName">The name of the file (in case of file system plugin in plugin namespace) for which the plugin needs to return the field data.</param>
            /// <param name="FieldIndex">The index of the field for which the content has to be returned. This correspond to index of field returned by <see cref="SupportedFields"/>.</param>
            /// <param name="UnitIndex">The index of the unit used. If no unit string was returned, UnitIndex is 0. For <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/>, <paramref name="UnitIndex"/> contains the offset of the data to be read.</param>
            /// <param name="maxlen">The maximum length of returned data (in bytes)</param>
            /// <param name="flags">Flags controlling function behavior</param>
            /// <param name="FieldValueOriginal">Used only when <paramref name="flags"/> contains <see2 cref2="F:Tools.TotalCommanderT.GetFieldValueFlags.PassThrough"/> (otherwise null). It contains field value as <see cref="Double"/>. The value is file size. You then need to apply the appropriate unit, and set the additional string field. This option is used to display the size even in locations where the plugin doesn't work, e.g. on ftp connections or inside archives.</param> 
            /// <returns>Field value in one of recognized data types or return code. Possible return values are:
            /// <list type="table"><listheader><term>Type of value returned</term><description>How it is treated</description></listheader>
            /// <item><term><see cref="Int32"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Integer32"/></description></item>
            /// <item><term><see cref="Int64"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Integer64"/></description></item>
            /// <item><term><see cref="Double"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Double"/></description></item>
            /// <item><term><see2 cref2="T:Tools.DataStructuresT.GenericT.IPair`2{System.Double,System.String}"/> (pair of <see cref="Double"/> and <see cref="String"/>)</term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Double"/>. This allows to pass number and its string representation. Use it in case when you don't like the Total Commander represents your numeric values to the user (loss of precission, rounding etc.). Numberic value will be used for sorting and searching while string value will be show to user. Empty string or null string is ignored. The length of string value must be less than or equal to <paramref name="maxlen"/> - 4 (4 bytes are reserved for <see cref="Double"/> part), or it will be cropped.</description></item>
            /// <item><term><see cref="Date"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Date"/></description></item>
            /// <item><term><see cref="TimeSpan"/> or <see cref="Tools::TimeSpanFormattable"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Time"/></description></item>
            /// <item><term><see cref="Boolean"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Boolean"/></description></item>
            /// <item><term>Array of <see cref="String"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Enum"/>. The array should contain exactly one value. If more values are returned, they are concatenated using ", " as separator. If the array is empty it is treated as when null is returned. Total length of resulting string must be maximally <paramref name="maxlen"/> or uncatchable exception will be thrown.</description></item>
            /// <item><term><see cref="String"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.String"/>. String shall not be longer than <paramref name="maxlen"/>.</description></item>
            /// <item><term>Array of <see cref="Byte"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/>. The array contains only part requested by <paramref name="UnitIndex"/> (start offset) and <paramref name="maxlen"/> (maximum number of bytes). Function is called multiple times in order to get all the data until it returns <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FieldEmpty"/> (alternativelly an empty array can be returned instead of <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FieldEmpty"/>). Bytes are treated as string by Total Commander. When Total Commander terminates reading of particular fulltext filed before function returns <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FieldEmpty"/> (or an empty array), it calls the function with <paramref name="UnitIndex"/> -1 to signal plugin to free any possibly cached data. The call with <paramref name="UnitIndex"/> -1 does not occur when function returns <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FieldEmpty"/> (or an empty array), the function should free cached data when it returns <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FieldEmpty"/> (or an empty array). Do not return this value when it is not expected by column type! The array should not contain 0, because Total Commander will treat 0 as end of array. The lenght of returned array must be maximally <paramref name="maxlen"/> or uncatchable exception will be thrown.</description></item>
            /// <item><term><see cref="IO::Stream"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/>. Stream must be readable and seekable. This is alternative to above (array of <see cref="Byte"/>). Managed plugin interface reads data from the stream and passes byte arrays to Total Commander. When eading is finished, it closes the stream. When the function returns <see cref="IO::Stream"/> it is not called again (as the stream must contain all the fulltext data). Managed plugin interface itself responds to subsequent calls. Function returning <see cref="IO::Stream"/> shall ignore <paramref name="UnitIndex"/> (start offset) and <paramref name="maxlen"/> (number of bytes). Do not return this value when it is not expected by column type! The stream should not contain 0, because Total Commander will treat 0 as end of array.</description></item>
            /// <item><term><see cref="DateTime"/></term><description>Type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.DateAndTime"/></description></item>
            /// <item><term><see cref="GetContentFieldValueReturnCode"/></term><description>Means that value was not extracted. Actual <see cref="GetContentFieldValueReturnCode"/>-enumerated value describes the reason why.</description></item>
            /// <item><term>null</term><description>The field is empty for given file or the filed is fulltext and all previous call returned last chunk of fulltext data (when returning fulltext data as array of <see cref="Byte"/>). Returning null has same effect as returning <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FieldEmpty"/>.</description></item>
            /// </list>
            /// <note type="inheritinfo">Type of value returned should correspond to type of field with index <paramref name="FieldIndex"/> as returned by <see cref="SupportedFields"/>.</note>
            /// <note type="inheritinfo">Do not return any other types, or uncatchable <se cref="Tools::TypeMismatchException"/> will be thrown.</note></returns>
            /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/>.</exception>
            /// <exception cref="ArgumentOutOfRangeException">Field with requested index does not exist. Has same effect as returning <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.NoSuchField"/>.</exception>
            /// <exception cref="IO::IOException">Error accessing given file. Has same effect as returning <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FileError"/>.</exception>
            /// <remarks><para>When most derived implementation of the function is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the function.</para>
            /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter names (except <paramref name="maxlen"/> and <paramref name="flags"/> which already was OK) converted to camelCase</version>
            [MethodNotSupported]
            virtual Object^ GetValue(String^ fileName, int fieldIndex, int unitIndex, int maxlen, GetFieldValueFlags flags, Nullable<Double> fieldValueOriginal);
            /// <summary>Called to tell a plugin that a directory change has occurred, and the plugin should stop loading a value.</summary>
            /// <param name="FileName">The name of the file for which <see cref="ContentGetValue"/> is currently being called.</param>
            /// <remarks>This function only needs to be implemented when handling very slow fields, e.g. the calculation of the total size of all files in a directory. It will be called only while a call to <see cref="ContentGetValue"/> is active in a background thread.
            /// <para>This function is called by Total Commander and is not intended for direct use.</para></remarks>
            /// <seealso cref="StopGetValue"/>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter name <c>FileName</c> changed to <c>fileName</c></version>
            /// <version version="1.5.4">Type of parameter <paramref name="fileName"/> changed from <see cref="char*"/> to <see cref="wchar_t*"/> - the function now supports Unicode</version>
            [EditorBrowsableAttribute(EditorBrowsableState::Never)]
            [CLSCompliantAttribute(false)]
            [PluginMethod("StopGetValue", "TC_C_STOPGETVALUE")]
            void ContentStopGetValue(wchar_t* fileName);
            /// <summary>When overridden in derived class, called to tell a plugin that a directory change has occurred, and the plugin should stop loading a value.</summary>
            /// <param name="FileName">The name of the file for which <see cref="GetValue"/> is currently being called.</param>
            /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/>.</exception>
            /// <remarks>This function only needs to be implemented when handling very slow fields, e.g. the calculation of the total size of all files in a directory. It will be called only while a call to <see cref="GetValue"/> is active in a background thread.
            /// <para>When most derived implementation of the function is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the function.</para>
            /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter name <c>FileName</c> changed to <c>fileName</c></version>
            [MethodNotSupported]
            virtual void StopGetValue(String^ fileName);
            /// <summary>Called when the user clicks on the sorting header above the columns.</summary>
            /// <param name="FieldIndex">The index of the field for which the sort order should be returned</param>
            /// <returns>Return 1 for ascending (a..z, 1..9), or -1 for descending (z..a, 9..0).</returns>
            /// <remarks>You may implement this function if there are fields which are usually sorted in descending order, like the size field (largest file first) or the date/time fields (newest first). If the function isn't implemented, ascending will be the default.
            /// <para>This function is called by Total Commander and is not intended for direct use.</para>
            /// <para>This function is ANSI/Unicode-agnostic.</para></remarks>
            /// <seealso cref="GetDefaultSortOrder"/>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter <c>FiledIndex</c> renamed to <c>fieldIndex</c></version>
            [EditorBrowsableAttribute(EditorBrowsableState::Never)]
            [CLSCompliantAttribute(false)]
            [PluginMethod("GetDefaultSortOrder", "TC_C_GETDEFAULTSORTORDER")]
            int ContentGetDefaultSortOrder(int fieldIndex);
            /// <summary>When overridden in derived class, called when the user clicks on the sorting header above the columns.</summary>
            /// <param name="FieldIndex">The index of the field for which the sort order should be returned</param>
            /// <returns>One of the <see cref="SortOrder"/> values. <see2 cref2="F:Tools::TotalCommanderT::SortOrder::unknown"/> has same meaning as <see2 cref2="F:Tools::TotalCommanderT::SortOrder::Ascending"/>.</returns>
            /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/>.</exception>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="FieldIndex"/> is out of range of field indexes as returned by <see cref="SupportedFields"/>. Throwing this exception has same effect as returning <see2 cref2="F:Tools::TotalCommanderT::SortOrder::unknown"/>.</exception>
            /// <remarks>You may implement this function if there are fields which are usually sorted in descending order, like the size field (largest file first) or the date/time fields (newest first). If the function isn't implemented, ascending will be the default.
            /// <para>When most derived implementation of the function is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the function.</para>
            /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter <c>FiledIndex</c> renamed to <c>fieldIndex</c></version>
            [MethodNotSupported]
            virtual SortOrder GetDefaultSortOrder(int fieldIndex);
            /// <summary>Called just before the plugin is unloaded, e.g. to close buffers, abort operations etc.</summary>
            /// <remarks>This function was added (to Total Commander plugin interface, not Managed plugin interface) by request from a user who needs to unload GDI+. It seems that GDI+ has a bug which makes it crash when unloading it in the DLL unload function, therefore a separate unload function is needed. The function is only called if the content plugin part of the file system plugin is used!
            /// <para>This function is called by Total Commander and is not intended for direct use.</para>
            /// <para>This plugin function is implemented by atypically named protected <see cref="OnContentPluginUnloading"/> function.</para>
            /// <para>This function ANSI/Unicode-agnostic.</para></remarks>
            /// <seealso cref="OnContentPluginUnloading"/>
            /// <version version="1.5.3">This method is new in version 1.5.3</version>
            [EditorBrowsableAttribute(EditorBrowsableState::Never)]
            [CLSCompliantAttribute(false)]
            [PluginMethod("OnContentPluginUnloading", "TC_C_PLUGINUNLOADING")]
            void ContentPluginUnloading(void);
        protected:
            /// <summary>Called just before the plugin is unloaded, e.g. to close buffers, abort operations etc.</summary>
            /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/>. See notes in remarks section.</exception>
            /// <remarks>This function was added (to Total Commander plugin interface, not Managed plugin interface) by request from a user who needs to unload GDI+. It seems that GDI+ has a bug which makes it crash when unloading it in the DLL unload function, therefore a separate unload function is needed. <strong>The function is only called if the content plugin part of the file system plugin is used!</strong>
            /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
            /// <para>By default this method is implemented as do-nothing (empty) method and unlike majority of plugin implementing method it is not marked with <see cref="MethodNotSupportedAttribute"/>, Total Commander calls it and it does not throw <see cref="NotSupportedException"/>.
            /// Plugin authors are free to override this method, apply <see cref="MethodNotSupportedAttribute"/> on it and throw <see cref="NotSupportedException"/> whenever called.</para>
            /// <note>Total Commander does not call this function for true content plugins. Only for file system plugins supporting content as well.</note></remarks>
            /// <version version="1.5.3">This method is new in version 1.5.3</version>
            virtual void OnContentPluginUnloading(void);
        public:
            /// <summary>Called to get various information about a plugin variable. It's first called with <paramref name="FieldIndex"/>=-1 to find out whether the plugin supports any special flags at all, and then for each field separately.</summary>
            /// <param name="FieldIndex">The index of the field for which flags should be returned.
            /// <list type="table"><listheader><term>Parameter value</term><description>Meaning</description></listheader>
            /// <item><term>-1</term><description>Return a combination (or) of all supported flags</description></item>
            /// <item><term>>=0</term><description>Return the field-specific flags</description></item></list></param>
            /// <returns>The function needs to return a combination of the <see cref="FieldFlags"/> flags.</returns>
            /// <remarks>Returning one of the  Subst* flags instructs Total Commander to replace (substitute) the returned variable by the indicated default internal value if no plugin variable can be retrieved.
            /// <para>This function is called by Total Commander and is not intended for direct use.</para>
            /// <para>This function is ANSI/Unicode-agnostic.</para></remarks>
            /// <seelaso cref="GetSupportedFieldFlags"/>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter <c>FiledIndex</c> renamed to <c>fieldIndex</c></version>
            [EditorBrowsableAttribute(EditorBrowsableState::Never)]
            [CLSCompliantAttribute(false)]
            [PluginMethod("GetSupportedFieldFlags", "TC_C_GETSUPPORTEDFIELDFLAGS")]
            int ContentGetSupportedFieldFlags(int fieldIndex);
            /// <summary>Called to get various information about a plugin variable. It's first called with <paramref name="FieldIndex"/>=-1 to find out whether the plugin supports any special flags at all, and then for each field separately.</summary>
            /// <param name="FieldIndex">The index of the field for which flags should be returned.
            /// <list type="table"><listheader><term>Parameter value</term><description>Meaning</description></listheader>
            /// <item><term>-1</term><description>Return a combination (or) of all supported flags</description></item>
            /// <item><term>>=0</term><description>Return the field-specific flags</description></item></list></param>
            /// <returns>The function needs to return a combination of the <see cref="FieldFlags"/> flags. <note>Only one of Subst*/<see2 cref2="F:Tools.TotalCommanderT.FieldFlags.PassThroughSize"/> flags should be set.</note></returns>
            /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/>. See notes in remarks section.</exception>
            /// <exception cref="ArgumentOutOfRangeException">The <paramref name="FieldIndex"/> is out of range of fields as returned by <see cref="SupportedFields"/>. Throwing this exception has same effect as returning zero.</exception>
            /// <remarks><note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
            /// Unlike majority of plugin functions this function is not decorated with <see cref="MethodNotSupportedAttribute"/>, does not throw <see cref="NotSupportedException"/> when called without being overridden in derived class and it even provides functionality.
            /// You usually don't need to override this function because it works over flags returned by <see cref="SupportedFields"/> (<see cref="ContentFieldSpecification::Flags"/>) for <paramref name="FieldIndex"/> -1 as well as for all the valid indexes.</remarks>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter <c>FiledIndex</c> renamed to <c>fieldIndex</c></version>
            virtual FieldFlags GetSupportedFieldFlags(int fieldIndex);
            /// <summary>This value is returned by <see cref="ContentSetValue"/> on success.</summary>
            /// <version version="1.5.3">This constant is new in version 1.5.3</version>
            [EditorBrowsableAttribute(EditorBrowsableState::Never)]
            literal int ContentSetValueSuccess = ft_setsuccess;
            /// <summary>Called to set the value of a specific field for a given file, e.g. to change the date field of a file.</summary>
            /// <param name="fileName">The name of the file (for File System plugins in plugin namespace) for which the plugin needs to change the field data.
            /// <para>This is set to NULL to indicate the end of change attributes.</para></param>
            /// <param name="fieldIndex">The index of the field for which the content has to be returned. This is the same index as the <c>FieldIndex</c> value in <see cref="ContentGetSupportedField"/>. This is set to -1 to signal the end of change attributes.</param>
            /// <param name="unitIndex">The index of the unit used. If no unit string was returned by <see cref="ContentGetSupportedField"/>, <paramref name="UnitIndex"/> is 0.</param>
            /// <param name="fieldType">The type of data passed to the plugin in <paramref name="FieldValue"/> - one of the <see cref="ContentFieldType"/> values. This is the same type as returned by the plugin via <see cref="ContentGetSupportedField"/>. If the plugin returned a different type via <see cref="ContentGetValue"/>, the the <paramref name="FieldType"/> <strong>may</strong> be of that type too.</param>
            /// <param name="fieldValue">Here the plugin receives the data to be changed. The data format depends on the field type.</param>
            /// <param namne="flags">One of the <see cref="SetValueFlags"/> values</param>
            /// <param name="wide">True if this function is called from Unicode environment, false if it is called from ANSI environment. Determines how to interpret string-based data types passed in <paramref name="fieldValue"/>.</param>
            /// <returns><see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.NoSuchField"/>, <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.FileError"/> or <see cref="ContentSetValueSuccess"/>.</returns>
            /// <remarks><note><strong>About caching the data</strong>: Total Commander will not call a mix <see cref="ContentSetValue"/> for different files, it will only call it for the next file when the previous file can be closed. Therefore a single cache per running Total Commander should be sufficient.</note>
            /// <note><strong>About the flags</strong>: If the <paramref name="flags"/> <see2 cref2="F:Tools.TotalCommanderT.SetValueFlags.First"/> and <see2 cref2="F:Tools.TotalCommanderT.SetValueFlags.Last"/> are both set, then this is the only attribute of this plugin which is changed for this file.</note>
            /// <para><paramref name="FileName"/> is set to NULL and <paramref name="FieldIndex"/> to -1 to signal to the plugin that the change attributes operation has ended. This can be used to flush unsaved data to disk, e.g. when setting comments for multiple files.</para>
            /// <para>This function is called by Total Commander and is not intended for direct use.</para>
            /// <para>Parameter <paramref name="wide"/> is not part of Total Commander plugin interface contract. It's added here to be able to distinguish between calls from ANSI and Unicode environments. Based on this value string-containing data passed and returned in <paramref name="fieldValue"/> are interpreted either as Unicode or ANSI.</para></remarks>
            /// <seelaso cref="SetValue"/>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter names (with exception of <paramref name="flags"/> which already was OK) converted to camelCase</version>
            /// <version version="1.5.4">Type of parameter <paramref name="fileName"/> changed from <see cref="char*"/> to <see cref="wchar_t*"/> - this function now supports Unicode</version>
            /// <version version="1.5.4">Parameter <paramref name="wide"/> added</version>
            [EditorBrowsableAttribute(EditorBrowsableState::Never)]
            [CLSCompliantAttribute(false)]
            [PluginMethod("SetValue", "TC_C_SETVALUE")]
            int ContentSetValue(wchar_t* fileName, int fieldIndex, int unitIndex, int fieldType, void* fieldValue, int flags, bool wide);
            /// <summary>When overridden in derived class called to set the value of a specific field for a given file, e.g. to change the date field of a file.</summary>
            /// <param name="FileName">The name of the file (for File System plugins in plugin namespace) for which the plugin needs to change the field data.
            /// <para>This is set to null to indicate the end of change attributes.</para></param>
            /// <param name="FieldIndex">The index of the field for which the content has to be returned. This is the same index as the <see cref="ContentFieldSpecification::FieldIndex"/> value in array returned by <see cref="SupportedFields"/>. This is set to -1 to signal the end of change attributes.</param>
            /// <param name="UnitIndex">The index of the unit used. If no unit string was returned by <see cref="SupportedFields"/>, <paramref name="UnitIndex"/> is 0.</param>
            /// <param name="value">Here the plugin receives the data to be changed. The type depends on the field type. Field type is inferred from <see cref="ContentFieldSpecification::FieldType"/>. When <see cref="GetValue"/> returns different type it may be passed here as well.
            /// <para>Data types are</para>
            /// <list><listheader><term>Data type passed</term><description>Corresponding field type</description></listheader>
            /// <item><term><see cref="Boolean"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Boolean"/></description></item>
            /// <item><term><see cref="Date"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Date"/></description></item>
            /// <item><term><see cref="DateTime"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.DateAndTime"/></description></item>
            /// <item><term><see cref="Double"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Double"/></description></item>
            /// <item><term>Array of <see cref="String"/> (usually one item; in fact string passed by Total Commander split by "<c>, </c>" (comma+space))</term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Enum"/></description></item>
            /// <item><term>Array of <see cref="Byte"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.FullText"/> (Total Commander never passes that)</description></item>
            /// <item><term><see cref="Int32"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Integer32"/></description></item>
            /// <item><term><see cref="Int64"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Integer64"/></description></item>
            /// <item><term><see cref="String"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.String"/></description></item>
            /// <item><term><see cref="TimeSpan"/></term><description><see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Time"/></description></item>
            /// <item><term><see cref="IntPtr"/></term><description>Total Commander have passed unknown type (should not happen)</description></item>
            /// </list></param>
            /// <param namne="flags">One of the <see cref="SetValueFlags"/> values</param>
            /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/>.</exception>
            /// <exception cref="IO::IOException">An error ocured while writing the data to file</exception>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="FieldIndex"/> is out of ragne of fields returned by <see cref="SupportedFields"/></exception>
            /// <remarks><note><strong>About caching the data</strong>: Total Commander will not call a mix <see cref="SetValue"/> for different files, it will only call it for the next file when the previous file can be closed. Therefore a single cache per running Total Commander should be sufficient.</note>
            /// <note><strong>About the flags</strong>: If the <paramref name="flags"/> <see2 cref2="F:Tools.TotalCommanderT.SetValueFlags.First"/> and <see2 cref2="F:Tools.TotalCommanderT.SetValueFlags.Last"/> are both set, then this is the only attribute of this plugin which is changed for this file.</note>
            /// <para><paramref name="FileName"/> is set to NULL and <paramref name="FieldIndex"/> to -1 to signal to the plugin that the change attributes operation has ended. This can be used to flush unsaved data to disk, e.g. when setting comments for multiple files.</para></remarks>
            /// <version version="1.5.3">This function is new in version 1.5.3</version>
            /// <version version="1.5.4">Parameter names (with exception of <paramref name="flags"/> and <paramref name="value"/> which already were OK) converted to camelCase</version>
            [MethodNotSupported]
            virtual void SetValue(String^ fileName, int fieldIndex, int unitIndex, Object^ value, SetValueFlags flags);
        };
    }
}