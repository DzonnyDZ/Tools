#pragma once

#include "..\Plugin\fsplugin.h"
#include "..\Common.h"
#include "..\ContentPluginBase.h"
#include "WFX Enums.h"
#include "ViewDefinition.h"
#include "FindData.h"

namespace Tools {
    namespace TotalCommanderT {
        using namespace System;
        using namespace System::ComponentModel;

        /// <summary>Contains details about the remote file being copied.</summary>
        public value class RemoteInfo {
        public:
            /// <summary>Low DWORD of remote file size. Useful for a progress indicator.</summary>
            /// <remarks>This property is not CLS-compliat. CLS-compliant alternative is to use <see cref="SetSizeLow"/> and <see cref="GetSizeLow"/>.</remarks>
            [CLSCompliantAttribute(false)]
            property DWORD SizeLow;
            /// <summary>High DWORD of remote file size. Useful for a progress indicator.</summary>
            /// <remarks>This property is not CLS-compliat. CLS-compliant alternative is to use <see cref="SetSizeHigh"/> and <see cref="GetSizeHigh"/>.</remarks>
            [CLSCompliantAttribute(false)]
            property DWORD SizeHigh;
            /// <summary>Time stamp of the remote file - should be copied with the file.</summary>
            property Nullable<DateTime> LastWriteTime;
            /// <summary>Attributes of the remote file - should be copied with the file.</summary>
            /// <remarks>This property is not CLS-comliant. CLS-compliant alternative is <see cref="Attributes"/>.</remarks>
            [CLSCompliant(false)]
            property FileAttributes Attr;
            /// <summary>Remote file size. Useful for a progress indicator.</summary>
            /// <remarks>This property is not CLS-compliant. CLS-compliant alternative is to use <see cref="SetSize"/> and <see cref="GetSize"/>.</remarks>
            [CLSCompliantAttribute(false)]
            property QWORD Size {QWORD get(); void set(QWORD); }
#pragma region "CLS-compliance"
            /// <summary>CLS-comliant alternative to the <see cref="Attr"/> property - gets or sets file attributes.</summary>
            /// <returns>Bitwise-same value as <see cref="Attr"/> representing file attrbutes</returns>
            /// <value>Value of the <see cref="Attr"/> property (bitwise same)</value>
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            property Int32 Attributes {Int32 get(); void set(Int32); }
            /// <summary>Sets <see cref="Size"/> as CLS-compliant <see cref="Int64"/></summary>
            /// <param name="value">New value of the <see cref="Size"/> property</param>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative</exception>
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            void SetSize(__int64 value);
            /// <summary>Gets <see cref="Size"/> as CLS-compliant <see cref="Int64"/></summary>
            /// <returns><see cref="Size"/></returns>
            /// <exception cref="InvalidOperationException"><see cref="Size"/> is greater than <see cref="Int64::MaxValue"/></exception>
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            __int64 GetSize();
            /// <summary>Sets <see cref="SizeLow"/> as CLS-compliant <see cref="Int64"/></summary>
            /// <param name="value">New value of the <see cref="SizeLow"/> property</param>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="Int32::MaxValue"/></exception>
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            void SetSizeLow(__int64 value);
            /// <summary>Gets <see cref="SizeLow"/> as CLS-compliant <see cref="Int64"/></summary>
            /// <returns><see cref="SizeLow"/></returns>
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            __int64 GetSizeLow();
            /// <summary>Sets <see cref="SizeHigh"/> as CLS-compliant <see cref="Int64"/></summary>
            /// <param name="value">New value of the <see cref="SizeHigh"/> property</param>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="Int32::MaxValue"/></exception>
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            void SetSizeHigh(__int64 value);
            /// <summary>Gets <see cref="SizeHigh"/> as CLS-compliant <see cref="Int64"/></summary>
            /// <returns><see cref="SizeHigh"/></returns>
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            __int64 GetSizeHigh();
#pragma endregion
        internal:
            /// <summary>CTor from <see cref="RemoteInfoStruct"/></summary>
            /// <param name="ri"><see cref="RemoteInfoStruct"/> to initialize new instance with</param>
            RemoteInfo(const RemoteInfoStruct& ri);
        };

    }
}