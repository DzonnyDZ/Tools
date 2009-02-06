#pragma once
#include "Common.h"
#include "PluginBase.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
    using namespace System::ComponentModel;

    /// <summary>Common base class for plugins tha can provide custom columns</summary>
    /// <remarks><note type="inheritinfo">Do not derive directly from this class as it does not represent any concrete plugin</note></remarks>
    public ref class ContentPluginBase abstract : PluginBase{
    internal:
        /// <summary>CTor - creates new instance of the <see cref="ContentPluginBase"/> class</summary>
        ContentPluginBase();
    public:
        /*int FsContentGetSupportedField(int FieldIndex,char* FieldName, char* Units,int maxlen);
        int FsContentGetValue(char* FileName,int FieldIndex,int UnitIndex, void* FieldValue,int maxlen,int flags);
        void FsContentStopGetValue(char* FileName);
        int FsContentGetDefaultSortOrder(int FieldIndex);
        void FsContentPluginUnloading(void);
        int FsContentGetSupportedFieldFlags(int FieldIndex);
        int FsContentSetValue(char* FileName,int FieldIndex,int UnitIndex,int FieldType, void* FieldValue,int flags);
        BOOL FsContentGetDefaultView(char* ViewContents,char* ViewHeaders,char* ViewWidths,char* ViewOptions,int maxlen);*/
    };
}}