#pragma once

#include "..\Plugin\fsplugin.h"
#include "..\Common.h"
#include "..\ContentPluginBase.h"
#include "WFX Enums.h"
#include "ViewDefinition.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
    using namespace System::ComponentModel;
  
    /// <summary>Contains information about the file that is found by the <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindFirst(System.String,Tools.TotalCommanderT.FindData@)"/>, or <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindNext(System.Object,Tools.TotalCommanderT.FindData@)"/> function.</summary>
    public value class FindData {
    private:
        /// <summary>Contains value of the <see cref="FileAttributes"/> property</summary>
        FileAttributes dwFileAttributes;
        /// <summary>Contains value of the <see cref="CreationTime"/> property</summary>
        Nullable<DateTime> ftCreationTime;
        /// <summary>Contains value of the <see cref="AccessTime"/> property</summary>
        Nullable<DateTime> ftLastAccessTime;
        /// <summary>Contains value of the <see cref="WriteTime"/> property</summary>
        Nullable<DateTime> ftLastWriteTime;
        /// <summary>Contains value of the high-order part of the <see cref="FileSize"/> property</summary>
        /// <remarks>This value is zero (0) unless the file size is greater than MAXDWORD.
        /// <para>The size of the file is equal to (<see cref="nFileSizeHigh"/> * (MAXDWORD+1)) + <see cref="nFileSizeLow"/>.</para></remarks>
        DWORD nFileSizeHigh;
        /// <summary>Contains value of the low-order part of the <see cref="FileSize"/> property</summary>
        DWORD nFileSizeLow;
        /// <summary>Contains value of the <see cref="ReparsePointTag"/> property</summary>
        ReparsePointTags dwReserved0;
        /// <summary>Contains value of the <see cref="Reserved1"/> property</summary>
        DWORD dwReserved1;
        /// <summary>Contains value of the <see cref="FileName"/> property</summary>
        String^ cFileName;
        /// <summary>Contains value of the <see cref="AlternateFileName"/> property</summary>
        String^ cAlternateFileName;
    internal:
        /// <summary>Ceates new instance of <see cref="FindData"/> from <see cref="WIN32_FIND_DATA"/></summary>
        /// <param name="Original"><see cref="WIN32_FIND_DATA"/> to initialize new instnce with</param>
        FindData(WIN32_FIND_DATA& Original);
        /// <summary>Converts <see cref="FindData"/> to <see cref="WIN32_FIND_DATA"/></summary>
        /// <returns><see cref="WIN32_FIND_DATA"/> created from this instance</returns>
        WIN32_FIND_DATA ToFindData();
        /// <summary>Populates given <see cref="WIN32_FIND_DATA"/> with data stored in current instance</summary>
        /// <param name="target">A <see cref="WIN32_FIND_DATA"/> to be populated</param>
        void Populate(WIN32_FIND_DATA& target);
    public:
        /// <summary>The file attributes of a file.</summary>
        /// <remarks>This property is not CLS-copliant. CLS-copliant alternatives are <see cref="GetAttributes"/> and <see cref="SetAttributes"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property FileAttributes Attributes{FileAttributes get();void set(FileAttributes);}
        /// <summary>Specifies when a file or directory was created.</summary>
        /// <remarks>If the underlying file system does not support creation time, this member is zero.</remarks>
        property Nullable<DateTime> CreationTime{Nullable<DateTime> get(); void set(Nullable<DateTime>);}
        /// <summary>For a file, the structure specifies when the file was last read from, written to, or for executable files, run.</summary>
        /// <remarks>For a directory, the structure specifies when the directory is created. If the underlying file system does not support last access time, this member is zero.
        /// <para>On the FAT file system, the specified date for both files and directories is correct, but the time of day is always set to midnight.</para></remarks>
        property Nullable<DateTime> AccessTime{Nullable<DateTime> get(); void set(Nullable<DateTime>);}
        /// <summary>For a file, the structure specifies when the file was last written to, truncated, or overwritten. The date and time are not updated when file attributes or security descriptors are changed.</summary>
        /// <remarks>For a directory, the structure specifies when the directory is created. If the underlying file system does not support last write time, this member is zero.</remarks>
        property Nullable<DateTime> WriteTime{Nullable<DateTime> get(); void set(Nullable<DateTime>);}
        /// <summary>Size of file in bytes</summary>
        /// <remarks>This property is not CLS-compliant. CLS-cmplant alternative is to use some of following functions: <see cref="SetFileSize"/>, <see cref="GetFileSize"/>, <see cref="SetFileSizeLow"/>, <see cref="SetFileSizeHigh"/>, <see cref="GetFileSizeLow"/>, <see cref="GetFileSizeHigh"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property QWORD FileSize{QWORD get(); void set(QWORD);}
        /// <summary>If the <see cref="Attributes"/> member includes the <see2 cref2="F:Tools.TotalCommanderT.FileAttributes.ReparsePoint"/> attribute, this member specifies the reparse point tag. Otherwise, this value is undefined and should not be used.</summary>
        /// <remarks>This property is not CLS-copliant. CLS-copliant alternatives are <see cref="GetReparsePointTag"/> and <see cref="SetReparsePointTag"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property ReparsePointTags ReparsePointTag{ReparsePointTags get(); void set(ReparsePointTags);}
        /// <summary>Reserved for future use.</summary>
        /// <remarks>This property is not CLS-copliant. CLS-copliant alternatives are <see cref="GetReserved1"/> and <see cref="SetReserved1"/>.</remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        property DWORD Reserved1{
            [EditorBrowsableAttribute(EditorBrowsableState::Never)] DWORD get();
            void set(DWORD);
        }
        /// <summary>The name of the file.</summary>
        /// <remarks>Currently all string are marshalled between plugin and Total Commander using <see cref="System::Text::Encoding::Default"/>, so not all the Unicode characters are supported.</remarks>
        /// <exception cref="ArgumentException">Value being set is longer than <see cref="MaxPath"/> characters</exception>
        property String^ FileName{String^ get(); void set(String^);}
        /// <summary>An alternative name for the file.</summary>
        /// <value>This name is in the classic 8.3 (filename.ext) file name format.</value>
        /// <exception cref="ArgumentException">Value being set is longer than 14 characters</exception>
        property String^ AlternateFileName{String^ get(); void set(String^);}
        /// <summary>Maximal length of path in characters (used by non-Unicode plugin) including terminating nullchar (so <see cref="MaxPathA"/> - 1 is typically usable from managed code)</summary>
        /// <seealso cref="MaxPath"/><seealso cref="MaxPathW"/>
        /// <version version="1.5.4">This constant is new in version 1.5.4</version>
        static const int MaxPathA = MAX_PATH;
        /// <summary>Maximal length of path in characters (used by Unicode plugin) including terminating nullchar (so <see cref="MaxPathW"/> - 1 is typically usable from managed code)</summary>
        /// <seealso cref="MaxPathA"/><seealso cref="MaxPath"/>
        /// <version version="1.5.4">This constant is new in version 1.5.4</version>
        static const int MaxPathW = 1024;
        /// <summary>Maximal length of path in characters (generally used by managed Total Commander plugins) including terminating nullchar (so <see cref="MaxPath"/> - 1 is typically usable from managed code)</summary>
        /// <remarks>Because managed Total Commander plugins support Unicode since version 1.5.4, <see cref="MaxPath"/> equals to <see cref="MaxPathW"/></remarks>
        /// <seealso cref="MaxPathA"/><seealso cref="MaxPathW"/>
        /// <version version="1.5.4">Value changed from 260 (<c>MAX_PATH</>) to 1024 (<see cref="MaxPatW"/>)</version>
        static const int MaxPath = MaxPathW;
#pragma region "CLS-compliance"
        /// <summary>Gets bitwise same value as <see cref="ReparsePointTag"/> as CLS-compliant type</summary>
        /// <returns>Bitwise same value as <see cref="ReparsePointTag"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        Int32 GetReparsePointTag();
        /// <summary>Sets value of <see cref="ReparsePointTag"/> as CLS-compliant type</summary>
        /// <param name="value">New value of the <see cref="ReparsePointTag"/> property. The property will be set to bitwise same value.</param>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetReparsePointTag(Int32 value);
         
        /// <summary>Gets bitwise same value as <see cref="Reserved1"/> as CLS-compliant type</summary>
        /// <returns>Bitwise same value as <see cref="Reserved1"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        Int32 GetReserved1();
        /// <summary>Sets value of <see cref="Reserved1"/> as CLS-compliant type</summary>
        /// <param name="value">New value of the <see cref="Reserved1"/> property. The property will be set to bitwise same value.</param>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        void SetReserved1(Int32 value);
 
        /// <summary>Gets bitwise same value as <see cref="Attributes"/> as CLS-compliant type</summary>
        /// <returns>Bitwise same value as <see cref="Attributes"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        Int32 GetAttributes();
        /// <summary>Sets value of <see cref="Attributes"/> as CLS-compliant type</summary>
        /// <param name="value">New value of the <see cref="Attributes"/> property. The property will be set to bitwise same value.</param>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetAttributes(Int32 value);
    
        /// <summary>Sets <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative</exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSize(__int64 value);
        /// <summary>Gets <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns><see cref="FileSize"/></returns>
        /// <exception cref="InvalidOperationException"><see cref="FileSize"/> is greater than <see cref="Int64::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSize();
        /// <summary>Sets low word <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property's low word</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="Int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSizeLow(__int64 value);
        /// <summary>Gets low word <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns>Low word of <see cref="FileSize"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSizeLow();
        /// <summary>Sets high word <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property's high word</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="Int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSizeHigh(__int64 value);
        /// <summary>Gets high word <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns>High word of <see cref="FileSize"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSizeHigh();
    };
}}