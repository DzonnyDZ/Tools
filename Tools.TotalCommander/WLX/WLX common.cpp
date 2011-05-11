#include "WLX common.h"

namespace Tools{namespace TotalCommanderT{

#pragma region ListerPluginInitEventArgs

    ListerPluginInitEventArgs::ListerPluginInitEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options){
        this->parentWindowHandle = parentWindowHandle;
        this->fileToLoad = fileToLoad;
        this->options = options;
    }
    inline ListerShowFlags ListerPluginInitEventArgs::Options::get(){ return this->options; }
    inline IntPtr ListerPluginInitEventArgs::ParentWindowHandle::get(){ return this->parentWindowHandle; }
    inline String^ ListerPluginInitEventArgs::FileToLoad::get(){ return this->fileToLoad; }
    inline IntPtr ListerPluginInitEventArgs::PluginWindowHandle::get(){ return this->PluginWindow == nullptr ? IntPtr::Zero : this->PluginWindow->Handle; }

#pragma endregion

}}