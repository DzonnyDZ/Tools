#pragma once
#include "Common.h"
#include "PluginBase.h"
#include "Plugin/contplug.h"

namespace Tools {
    namespace TotalCommanderT {
        using namespace System;
        using namespace System::ComponentModel;

        /// <summary>Types of content fields</summary>
        /// <remarks>Content fields are custom columns in details view provided by plugin</remarks>
        /// <version version="1.5.3">This enumeration in new in version 1.5.3</version>
        public enum class ContentFieldType : Int32 {
            /// <summary>The field index is beyond the last available field. This is not actual field type. This value denotes that the field does not exist.</summary>
            [EditorBrowsable(EditorBrowsableState::Never)]
        NoMoreFields = ft_nomorefields,
            /// <summary>A 32-bit signed number, type <see cref="Int32"/> (Total Commander calls this numeric_32)</summary>
            Integer32 = ft_numeric_32,
            /// <summary>A 64-bit signed number, e.g. for file sizes, type <see cref="Int64"/> (Total Commander calls this numeric_64)</summary>
            Integer64 = ft_numeric_64,
            /// <summary>A double precision floating point number, type <see cref="Double"/> (Total Commander calls this float)</summary>
            Double = ft_numeric_floating,
            /// <summary>A date value (year, month, day; time part ignored), type <see cref="DateTime"/></summary>
            Date = ft_date,
            /// <summary>A time value (hour, minute, second). Date and time are in local time. Type <see cref="TimeSpan"/></summary>
            Time = ft_time,
            /// <summary>A true/false value, type <see cref="Boolean"/></summary>
            Boolean = ft_boolean,
            /// <summary>A value allowing a limited number of choices. Use the Units field to return all possible values. (Total Commander calls this multiplechoice)</summary>
            Enum = ft_multiplechoice,
            /// <summary>A text string, type <see cref="String"/></summary>
            String = ft_string,
            /// <summary>A full text (multiple text strings), only used for searching. Can be used e.g. for searching in the text portion of binary files, where the plugin makes the necessary translations. All fields of this type MUST be placed at the END of the field list, otherwise you will get errors in Total Commander!</summary>
            /// <remarks>This field is currently not supported in file system plugins. This is Total Commander limitation, not managed plugin limitation.</remarks>
            [EditorBrowsable(EditorBrowsableState::Advanced)]
        FullText = ft_fulltext,
            /// <summary>A timestamp of type <se cref="DateTime"/>. The time MUST be relative to universal time (Greenwich mean time) as returned by the file system, not local time!</summary>
            DateAndTime = ft_datetime
        };

        /// <summary>Flags controlling field extraction behavior</summary>
        /// <version version="1.5.3">This enumeration is new in version 1.5.3</version>
        [Flags]
        public enum class GetFieldValueFlags : Int32 {
            /// <summary>If this flag is set, the plugin should return <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.Delayed"/> for fields which take a long time to extract, like file version information. Total Commander will then call the function again in a background thread without the <see cref="DelayIfSlow"/> flag. This means that your plugin must be implemented thread-safe if you plan to return <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.Delayed"/>.
            /// <para>The plugin may also reutrn <see2 cref2="F:Tools.TotalCommanderT.GetContentFieldValueReturnCode.OnDemand"/> if <see cref="DelayIfSlow"/> is set. In this case, the field will only be retrieved when the user presses &lt;SPACEBAR>. This is only recommended for fields which take a VERY long time, e.g. directory content size. You should offer the same field twice in this case, once as delayed, and once as on demand. The field will be retrieved in the background thread also in this case.</para></summary>
            DelayIfSlow = CONTENT_DELAYIFSLOW,
            /// <summary>If this flag is set, the <c>FieldValueOriginal</c> passes the file size to the plugin as <see cref="Double"/>. This value is only set if you have returned the flag <see2 cref2="F:Tools.TotalCommanderT.FieldFlags.PassThroughSizeFloat"/> from the function <see2 cref2="M:Tools.TotalCommanderT.ContentPluginBase.GetSupportedFieldFlags(System.Int32)"/>. No units have been applied yet, the size is passed to the plugin as bytes. You then need to apply the appropriate unit, and set the additional string field. This option is used to display the size even in locations where the plugin doesn't work, e.g. on ftp connections or inside archives.</summary>
            PassThrough = CONTENT_PASSTHROUGH
        };

        /// <summary>Return codes taht can be returned instead of actual field value</summary>
        /// <version version="1.5.3">This enumeration in new in version 1.5.3</version>
        public enum class GetContentFieldValueReturnCode : Int32 {
            /// <summary>Field with requestend index does not exist. Same effect as throwing <see cref="ArgumentOutOfRangeException"/>.</summary>
            NoSuchField = ft_nosuchfield,
            /// <summary>Error accessiong given file. Same effect as throwing <see cref="IO::IOException"/>.</summary>
            FileError = ft_fileerror,
            /// <summary>The file does not contain the specified field. Same efect as returning null.</summary>
            FieldEmpty = ft_fieldempty,
            /// <summary>The extraction of the field would take a long time, so Total Commander should request it again in a background thread. This code may only be returned if the flag <see2 cref2="F:Tools.TotalCommanderT.GetFieldValueFlags.DelayIfSlow"/> was set, and if the plugin is thread-safe.</summary>
            Delayed = ft_delayed,
            /// <summary>The extraction of the field would take a very long time, so it should only be retrieved when the user presses the space bar. This error may only be returned if the flag <see2 cref2="F:Tools.TotalCommanderT.GetFieldValueFlags.DelayIfSlow"/> was set, and if the plugin is thread-safe.</summary>
            OnDemand = ft_ondemand
        };

        /// <summary>Field flags controlling how Total Commander treats the field and its value</summary>
        /// <remarks><note>Only one of Subst*/<see2 cref2="F:Tools.TotalCommanderT.FieldFlags.PassThroughSize"/> flags should be set.</note></remarks>
        /// <version version="1.5.3">This enumeration is new in version 1.5.3</version>
        [Flags]
        public enum class FieldFlags : Int32 {
            /// <summary>Use the file size if field value cannot be obtained via plugin (i.e. file is remote or in archive)</summary>
            SubstSize = contflags_substsize,
            /// <summary>use the file date+time (<see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.DateAndTime"/>)  if field value cannot be obtained via plugin (i.e. file is remote or in archive)</summary>
            SubstDateTime = contflags_substdatetime,
            /// <summary>use the file date (<see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Date"/>) if field value cannot be obtained via plugin (i.e. file is remote or in archive)</summary>
            SubstDate = contflags_substdate,
            /// <summary>use the file time (<see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Time"/>) if field value cannot be obtained via plugin (i.e. file is remote or in archive)</summary>
            SubstTime = contflags_substtime,
            /// <summary>use the file attributes (numeric) if field value cannot be obtained via plugin (i.e. file is remote or in archive)</summary>
            SubstAttributesNum = contflags_substattributes,
            /// <summary>use the file attribute string in form -a-- if field value cannot be obtained via plugin (i.e. file is remote or in archive)</summary>
            SubstAttributesStr = contflags_substattributestr,
            /// <summary>A combination of all substituion flags. Should be returned for index -1 if the content plugin contains ANY of the substituted fields.</summary>
            SubstMask = contflags_substmask,
            /// <summary>pass the size as ft_numeric_floating to ContentGetValue. The plugin will then apply the correct units, and return the formatted display string in the additional string field.</summary>
            PassThroughSize = contflags_passthrough_size_float,
            /// <summary>If set, TC will show a button >> in change attributes which lets the user call the function EditValue. This allows plugins to have their own field editors, like the custom editor for tc.comments or tc.*date/time fields.
            /// <para>Not supported by file system plugin.</para></summary>
            FieldEdit = contflags_fieldedit
        };

        /// <summary>Defines content field</summary>
        /// <remarks>Content field represents single custom column prtovided by plugin in details view</remarks>
        /// <version version="1.5.3">This class in new in version 1.5.3</version>
        public ref class ContentFieldSpecification sealed {
        private:
            /// <summary>Contains value of the <see cref="FieldIndex"/> property</summary>
            initonly int fieldIndex;
            /// <summary>Contains vaue of the <see cref="FieldType"/> property</summary>
            initonly ContentFieldType fieldType;
            /// <summary>Contains value of the <see cref="FieldName"/> property</summary>
            initonly String^ fieldName;
            /// <summary>Contains value of the <see ctef="Units"/> property</summary>
            initonly cli::array<String^>^ units;
            /// <summary>Contains value of the <see cref="Flags"/> property</summary>
            initonly FieldFlags flags;
        public:
            /// <summary>CTor - creates new instance of the <see cref="ContentFieldSpecification"/> class</summary>
            /// <param name="fieldIndex">Index of the field within array of fields. This must be set to the same value as is 0-base index in array this instance is returned in from <see2 cref2="P:Tools.TotalCommanderT.ContentPluginBase.SupportedFields"/>.</param>
            /// <param name="fieldType">Type of value of field. Shall not be <see2 cref2="F:Tools.TotalCommanderT.NoMoreFields"/>.
            /// <note>When array returned by <see2 cref2="P:Tools.TotalCommanderT.ContentPluginBase.SupportedFields"/> contains item of type <see2 cref2="F:Tools.TotalCommanderT.FullText"/>, it shal not contain any item of non-<see2 cref2="F:Tools.TotalCommanderT.Fulltext"/> type at higner index. This is requited by Total Commander by the way it handle full text fields.</note></param>
            /// <param name="fieldName">Name of the field. It shall contain neither dot (.), pipe (|), colon (:) nor nullchar. Its length shall not break limit set up by <see2 cref2="P:Tools.TotalCommanderT.ContentPluginBase.FileNameMaxLen"/>.</param>
            /// <param name="flags">Filed flags to precise field behavior. <note>Only one of Subst*/<see2 cref2="F:Tools.TotalCommanderT.FieldFlags.PassThroughSize"/> flags should be set.</note></param>
            /// <param name="units">Unit[s] used by the field. Field (column) may support multiple units of its value like bytes, kilobytes and megabytes for size. Name of unit shall not contain dot (.), pipe (|), colon (:) or nullchar. Sum of lengts of unit names plus number of units minus 1 shall not break limit set up by <see2 cref2="P:Tools.TotalCommanderT.ContentPluginBase.FileNameMaxLen"/>.
            /// <para>When <paramref name="fieldType"/> is <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Enum"/> this parameter contains list of possible enumerated values instead of list of units (enumes don't support units, it supports list of values). Same restrictions apply.</para>
            /// Value of this parameter may be null or an empty array.</param>
            /// <exception cref="InvalidEnumArgumentException"><paramref name="fieldType"/> is not member of <see cref="ContentFieldType"/></exception>
            /// <exception cref="ArgumentNullException"><paramref name="fieldName"/> is null</exception>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="fieldIndex"/> is less than zero</exception>
            /// <exception cref="ArgumentException"><paramref name="fieldName"/> or <paramref name="units"/> contains dot (.), pipe (|), colon (:) or nullchar. -or- <paramref name="FieldType"/> is <see2 cref2="F:Tools.TotalCommanderT.NoMoreFields"/>.</exception>
            /// <remarks>More and uncatchable exceptions can be thrown when newly created instance is passed to Total Commander and it violates another restrictions as restriction of length of <paramref name="FieldName"/> or accumulated length of <paramref name="Units"/>.
            /// <note type="warning">Dues to Total Commander plugin interface limitations <paramref name="filedName"/> and <paramref name="units"/> should be ANSI strings (i.e. contain only characters from <see cref="System::Text::Encoding::Default"/>).</note></remarks>
            /// <version version="1.5.4">Parameter names converted to camelCase</version>
            ContentFieldSpecification(int fieldIndex, ContentFieldType fieldType, String^ fieldName, FieldFlags flags, ... cli::array<String^>^ units);
            /// <summary>Gets index of this columns</summary>
            /// <returns>Index of this column when this instance is returned by <see2 cref2="P:Tools.TotalCommanderT.ContentPluginBase.SupportedFields"/>.</returns>
            property int FieldIndex {int get(); }
            /// <summary>Gets type of value of field</summary>
            /// <returns>Type of value of field. Never returns <see2 cref2="F:Tools.TotalCommanderT.NoMoreFields"/>.</returns>
            /// <remarks>When array returned by <see2 cref2="P:Tools.TotalCommanderT.ContentPluginBase.SupportedFields"/> contains item of type <see2 cref2="F:Tools.TotalCommanderT.FullText"/>, it shal not contain any item of non-<see2 cref2="F:Tools.TotalCommanderT.Fulltext"/> type at higner index. This is requited by Total Commander by the way it handle full text fields.</remarks>
            property ContentFieldType FieldType {ContentFieldType get(); }
            /// <summary>Gets name of the field (column)</summary>
            /// <returns>Name of the field (column). It does not contain dot (.), pipe (|), colon (:) or nullchar.</returns>
            property String^ FieldName {String^ get(); }
            /// <summary>Gets array of units supported by this column</summary>
            /// <returns>Array of units supported by this column. When <see cref="FieldType"/> is <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.Enum"/> this property returns list of possible values instead of list of units.</returns>
            /// <remarks>Changing item of array returned by this property does NOT change appropriate column name in Total Commander. When changin vlaue of array item of instance that will be retiurned from <see2 cref2="P:Tools.TotalCommanderT.ContentPluginBase.SupportedFields"/>, keep in kid that sum of lengths of array items plus number of items minus one must not exceed <see2 cref2="P:Tools.TotalCommanderT.ContentPluginBase.FileNameMaxLen"/>. Also do not set values if array items to strings containing dots (.), pipes (|), colons (:) or nullchars - it may leed to uncatchable exception thrown by caller.</remarks>
            property cli::array<String^>^ Units {cli::array<String^>^ get(); }
            /// <summary>Gets flags to precise field behavior</summary>
            /// <returns>Filed flags precising field behavior</returns>
            /// <remarks>Those flags are pased to Total Commander by <see2 cref2="M:Tools.TotalCommanderT.ContentPluginBase.GetSupportedFieldFlags(System:Int32)"/>, which by default utilizes this property and can be overridden.</remarks>
            property FieldFlags Flags {FieldFlags get(); }
        };


        /// <summary>Defines various information for custom field (column) value being set</summary>
        /// <version version="1.5.3">This enumeration is new in version 1.5.3</version>
        [Flags]
        public enum class SetValueFlags : Int32 {
            /// <summary>This is the first attribute to be set for this file via this plugin. May be used for optimization.</summary>
            First = setflags_first_attribute,
            /// <summary>This is the last attribute to be set for this file via this plugin.</summary>
            Last = setflags_last_attribute,
            /// <summary>For field type <see2 cref2="F:Tools.TotalCommanderT.ContentFieldType.DateAndTime"/> only: User has only entered a date, don't change the time</summary>
            OnlyDate = setflags_only_date
        };

        /// <summary>Defines sorting orders</summary>
        /// <seealso cref2="T:System.Windows.Forms.SortOrder"/>
        /// <version version="1.5.3">This enumeration is new in version 1.5.3</version>
        public enum class SortOrder : Int32 {
            /// <summary>Sort order is not set. May default to <see cref2="F:Tools.TotalCommanderT.SortOrder.Ascending"/></summary>
            unknown = 0,
            /// <summary>Items are sorted in ascending order (from lower to greater)</summary>
            Ascending = 1,
            /// <summary>Items are sorted in descending order (from greater to smaller)</summary>
            Descending = 2
        };
    }
}