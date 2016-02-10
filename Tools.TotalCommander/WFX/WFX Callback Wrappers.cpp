#include "WFX Callback Wrappers.h"
#include "..\Exceptions.h"
#include "FileSystemPlugin.h"

using namespace System;
using namespace System::ComponentModel;

namespace Tools {
    namespace TotalCommanderT {

#pragma region ProgressProcWrapper
        Delegate^ ProgressProcWrapper::Delegate::get() {
            return this->managed != nullptr ? this->managed : gcnew ProgressCallback(this, &ProgressProcWrapper::operator());
        }

        ProgressProcWrapper::ProgressProcWrapper(tProgressProc method) {
            if (method == NULL) throw gcnew ArgumentNullException("method");
            this->ansi = method;
        }
        ProgressProcWrapper::ProgressProcWrapper(tProgressProcW method) {
            if (method == NULL) throw gcnew ArgumentNullException("method");
            this->unicode = method;
        }
        ProgressProcWrapper::ProgressProcWrapper(ProgressCallback^ method) {
            if (method == nullptr) throw gcnew ArgumentNullException("method");
            this->managed = method;
        }
        int ProgressProcWrapper::operator()(int PluginNr, char* SourceName, char* TargetName, int PercentDone) {//ANSI
            if (this->managed) {
                FileSystemPlugin^ source = FileSystemPlugin::GetPluginByNumber(PluginNr);
                if (source == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::PluginNotRegistered, PluginNr));
                return this->managed(source, gcnew String(SourceName), gcnew String(TargetName), PercentDone) ? TRUE : FALSE;
            }
            else if (this->ansi) {
                return this->ansi(PluginNr, SourceName, TargetName, PercentDone);
            }
            else if (this->unicode) {
                wchar_t* sn = AnsiToUnicode(SourceName);
                try {
                    wchar_t* tn = AnsiToUnicode(TargetName);
                    try {
                        return this->unicode(PluginNr, sn, tn, PercentDone);
                    }
                    finally{ delete[] tn; }
                }
                finally{ delete[] sn; }
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        int ProgressProcWrapper::operator()(int PluginNr, wchar_t* SourceName, wchar_t* TargetName, int PercentDone) {//Unicode
            if (this->managed) {
                FileSystemPlugin^ source = FileSystemPlugin::GetPluginByNumber(PluginNr);
                if (source == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::PluginNotRegistered, PluginNr));
                return this->managed(source, gcnew String(SourceName), gcnew String(TargetName), PercentDone) ? TRUE : FALSE;
            }
            else if (this->ansi) {
                char* sn = UnicodeToAnsi(SourceName);
                try {
                    char* tn = UnicodeToAnsi(TargetName);
                    try {
                        return this->ansi(PluginNr, sn, tn, PercentDone);
                    }
                    finally{ delete[] tn; }
                }
                finally{ delete[] sn; }
            }
            else if (this->unicode) {
                return this->unicode(PluginNr, SourceName, TargetName, PercentDone);
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        bool ProgressProcWrapper::operator()(FileSystemPlugin^ sender, String^ sourceName, String^ targetName, int percentDone) {//Managed
            if (this->managed) {
                return this->managed(sender, sourceName, targetName, percentDone);
            }
            else if (this->ansi) {
                if (sender == nullptr) throw gcnew ArgumentNullException("sender");
                char* sn = StringCopyA(sourceName);
                try {
                    char* tn = StringCopyA(targetName);
                    try {
                        return this->ansi(sender->PluginNr, sn, tn, percentDone) == TRUE;
                    }
                    finally{ delete[] tn; }
                }
                finally{ delete[] sn; }
            }
            else if (this->unicode) {
                if (sender == nullptr) throw gcnew ArgumentNullException("sender");
                wchar_t* sn = StringCopyW(sourceName);
                try {
                    wchar_t* tn = StringCopyW(targetName);
                    try {
                        return this->unicode(sender->PluginNr, sn, tn, percentDone) == TRUE;
                    }
                    finally{ delete[] tn; }
                }
                finally{ delete[] sn; }
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        inline bool ProgressProcWrapper::Invoke(FileSystemPlugin^ sender, String^ sourceName, String^ targetName, int percentDone) {
            return this(sender, sourceName, targetName, percentDone);
        }

#pragma endregion

#pragma region LogProcWrapper
        Delegate^ LogProcWrapper::Delegate::get() {
            return this->managed != nullptr ? this->managed : gcnew LogCallback(this, &LogProcWrapper::operator());
        }

        LogProcWrapper::LogProcWrapper(tLogProc method) {
            if (method == NULL) throw gcnew ArgumentNullException("method");
            this->ansi = method;
        }
        LogProcWrapper::LogProcWrapper(tLogProcW method) {
            if (method == NULL) throw gcnew ArgumentNullException("method");
            this->unicode = method;
        }
        LogProcWrapper::LogProcWrapper(LogCallback^ method) {
            if (method == nullptr) throw gcnew ArgumentNullException("method");
            this->managed = method;
        }

        void LogProcWrapper::operator()(int PluginNr, int MsgType, char* LogString) {//ANSI
            if (this->managed != nullptr) {
                FileSystemPlugin^ source = FileSystemPlugin::GetPluginByNumber(PluginNr);
                if (source == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::PluginNotRegistered, PluginNr));
                this->managed(source, (LogKind)MsgType, gcnew String(LogString));
            }
            else if (this->ansi != NULL)
                this->ansi(PluginNr, MsgType, LogString);
            else if (this->unicode != NULL) {
                wchar_t* lstr = AnsiToUnicode(LogString);
                try {
                    this->unicode(PluginNr, MsgType, lstr);
                }
                finally{ delete[] lstr; }
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        void LogProcWrapper::operator()(int PluginNr, int MsgType, WCHAR* LogString) {//Unicode
            if (this->managed != nullptr) {
                FileSystemPlugin^ source = FileSystemPlugin::GetPluginByNumber(PluginNr);
                if (source == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::PluginNotRegistered, PluginNr));
                this->managed(source, (LogKind)MsgType, gcnew String(LogString));
            }
            else if (this->ansi != NULL) {
                char* lstr = UnicodeToAnsi(LogString);
                try {
                    this->ansi(PluginNr, MsgType, lstr);
                }
                finally{ delete[] lstr; }
            }
            else if (this->unicode != NULL)
                this->unicode(PluginNr, MsgType, LogString);
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        void LogProcWrapper::operator()(FileSystemPlugin^ sender, LogKind msgType, String^ logString) {//managed
            if (this->managed != nullptr)
                this->managed(sender, msgType, logString);
            else if (this->ansi != NULL) {
                if (sender == nullptr) throw gcnew ArgumentNullException("sender");
                char* lstr = StringCopyA(logString);
                try {
                    this->ansi(sender->PluginNr, (int)msgType, lstr);
                }
                finally{ delete[] lstr; }
            }
            else if (this->unicode != NULL) {
                if (sender == nullptr) throw gcnew ArgumentNullException("sender");
                wchar_t* lstr = StringCopyW(logString);
                try {
                    this->unicode(sender->PluginNr, (int)msgType, lstr);
                }
                finally{ delete[] lstr; }
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }

        inline void LogProcWrapper::Invoke(FileSystemPlugin^ sender, LogKind msgType, String^ logString) {
            this(sender, msgType, logString);
        }
#pragma endregion

#pragma region RequestProcWrapper
        Delegate^ RequestProcWrapper::Delegate::get() {
            return this->managed != nullptr ? this->managed : gcnew RequestCallback(this, &RequestProcWrapper::operator());
        }

        RequestProcWrapper::RequestProcWrapper(tRequestProc method) {
            if (method == NULL) throw gcnew ArgumentNullException("method");
            this->ansi = method;
        }
        RequestProcWrapper::RequestProcWrapper(tRequestProcW method) {
            if (method == NULL) throw gcnew ArgumentNullException("method");
            this->unicode = method;
        }
        RequestProcWrapper::RequestProcWrapper(RequestCallback^ method) {
            if (method == nullptr) throw gcnew ArgumentNullException("method");
            this->managed = method;
        }
        BOOL RequestProcWrapper::operator()(int PluginNr, int RequestType, char* CustomTitle, char* CustomText, char* ReturnedText, int maxlen) {//ANSI
            if (this->managed != nullptr) {
                FileSystemPlugin^ source = FileSystemPlugin::GetPluginByNumber(PluginNr);
                if (source == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::PluginNotRegistered, PluginNr));
                String^ cutt = StringCopy(CustomTitle), ^cutx = StringCopy(CustomText), ^dt = StringCopy(ReturnedText);
                String^ ret = this->managed(source, (InputRequestKind)RequestType, cutt, cutx, dt, maxlen);
                StringCopy(ret, ReturnedText, maxlen);
                return ret == nullptr ? FALSE : TRUE;
            }
            else if (this->ansi != NULL) {
                return this->ansi(PluginNr, RequestType, CustomTitle, CustomText, ReturnedText, maxlen);
            }
            else if (this->unicode != NULL) {
                wchar_t* cutt = AnsiToUnicode(CustomTitle);
                try {
                    wchar_t* cutx = AnsiToUnicode(CustomText);
                    try {
                        wchar_t* rt = AnsiToUnicode(ReturnedText, maxlen);
                        try {
                            BOOL ret = this->unicode(PluginNr, RequestType, cutt, cutx, rt, maxlen);
                            UnicodeToAnsi(rt, ReturnedText, maxlen);
                            return ret;
                        }
                        finally{ delete[] rt; }
                    }
                    finally{ delete[] cutx; }
                }
                finally{ delete[] cutt; }
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        BOOL RequestProcWrapper::operator()(int PluginNr, int RequestType, WCHAR* CustomTitle, WCHAR* CustomText, WCHAR* ReturnedText, int maxlen) {//Unicode
            if (this->managed != nullptr) {
                FileSystemPlugin^ source = FileSystemPlugin::GetPluginByNumber(PluginNr);
                if (source == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::PluginNotRegistered, PluginNr));
                String^ cutt = StringCopy(CustomTitle), ^cutx = StringCopy(CustomText), ^dt = StringCopy(ReturnedText);
                String^ ret = this->managed(source, (InputRequestKind)RequestType, cutt, cutx, dt, maxlen);
                StringCopy(ret, ReturnedText, maxlen);
                return ret == nullptr ? FALSE : TRUE;
            }
            else if (this->ansi != NULL) {
                char* cutt = UnicodeToAnsi(CustomTitle);
                try {
                    char* cutx = UnicodeToAnsi(CustomText);
                    try {
                        char* rt = UnicodeToAnsi(ReturnedText, maxlen);
                        try {
                            BOOL ret = this->ansi(PluginNr, RequestType, cutt, cutx, rt, maxlen);
                            AnsiToUnicode(rt, ReturnedText, maxlen);
                            return ret;
                        }
                        finally{ delete[] rt; }
                    }
                    finally{ delete[] cutx; }
                }
                finally{ delete[] cutt; }
            }
            else if (this->unicode != NULL) {
                return this->unicode(PluginNr, RequestType, CustomTitle, CustomText, ReturnedText, maxlen);
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        String^ RequestProcWrapper::operator()(FileSystemPlugin^ sender, InputRequestKind requestType, String^ customTitle, String^ customText, String^ defaultText, int maxlen) {//managed
            if (this->managed != nullptr) {
                return this->managed(sender, requestType, customTitle, customText, defaultText, maxlen);
            }
            else if (this->ansi != NULL) {
                if (sender == nullptr) throw gcnew ArgumentNullException("sender");
                char* cutt = StringCopyA(customTitle);
                try {
                    char* cutx = StringCopyA(customText);
                    try {
                        char* ret = new char[maxlen];
                        try {
                            StringCopy(defaultText, ret, maxlen);
                            this->ansi(sender->PluginNr, (int)requestType, cutt, cutx, ret, maxlen);
                            return StringCopy(ret);
                        }
                        finally{ delete[] ret; }
                    }
                    finally{ delete[] cutx; }
                }
                finally{ delete[] cutt; }
            }
            else if (this->unicode != NULL) {
                if (sender == nullptr) throw gcnew ArgumentNullException("sender");
                wchar_t* cutt = StringCopyW(customTitle);
                try {
                    wchar_t* cutx = StringCopyW(customText);
                    try {
                        wchar_t* ret = new wchar_t[maxlen];
                        try {
                            StringCopy(defaultText, ret, maxlen);
                            this->unicode(sender->PluginNr, (int)requestType, cutt, cutx, ret, maxlen);
                            return StringCopy(ret);
                        }
                        finally{ delete[] ret; }
                    }
                    finally{ delete[] cutx; }
                }
                finally{ delete[] cutt; }
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        inline String^ RequestProcWrapper::Invoke(FileSystemPlugin^ sender, InputRequestKind requestType, String^ customTitle, String^ customText, String^ defaultText, int maxlen) {
            return this(sender, requestType, customTitle, customText, defaultText, maxlen);
        }
#pragma endregion

#pragma region CryptProcWrapper
        Delegate^ CryptProcWrapper::Delegate::get() {
            return this->managed != nullptr ? this->managed : gcnew CryptCallback(this, &CryptProcWrapper::operator());
        }

        CryptProcWrapper::CryptProcWrapper(tCryptProc method) {
            if (method == NULL) throw gcnew ArgumentNullException("method");
            this->ansi = method;
        }
        CryptProcWrapper::CryptProcWrapper(tCryptProcW method) {
            if (method == NULL) throw gcnew ArgumentNullException("method");
            this->unicode = method;
        }
        CryptProcWrapper::CryptProcWrapper(CryptCallback^ method) {
            if (method == nullptr) throw gcnew ArgumentNullException("method");
            this->managed = method;
        }

        int CryptProcWrapper::operator()(int PluginNr, int CryptoNr, int Mode, char* ConnectionName, char* Password, int maxlen) {//ANSI
            if (this->managed != nullptr) {
                FileSystemPlugin^ source = FileSystemPlugin::GetPluginByNumber(PluginNr);
                if (source == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::PluginNotRegistered, PluginNr));
                String^ ret;
                try {
                    ret = this->managed(source, (CryptMode)Mode, gcnew String(ConnectionName), gcnew String(Password), maxlen);
                }
                catch (CryptException^ ex) {
                    return (int)ex->Reason;
                }
                catch (...) {
                    return (int)CryptResult::Fail;
                }
                StringCopy(ret, Password, maxlen);
                return (int)CryptResult::OK;
            }
            else if (this->ansi != NULL) {
                return this->ansi(PluginNr, CryptoNr, Mode, ConnectionName, Password, maxlen);
            }
            else if (this->unicode != NULL) {
                wchar_t* cn = AnsiToUnicode(ConnectionName);
                try {
                    wchar_t* pwd = AnsiToUnicode(Password, maxlen);
                    try {
                        int ret = this->unicode(PluginNr, CryptoNr, Mode, cn, pwd, maxlen);
                        UnicodeToAnsi(pwd, Password, maxlen);
                        return ret;
                    }
                    finally{ delete[] pwd; }
                }
                finally{ delete[] cn; }
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        int CryptProcWrapper::operator()(int PluginNr, int CryptoNr, int Mode, wchar_t* ConnectionName, wchar_t* Password, int maxlen) {//Unicode
            if (this->managed != nullptr) {
                FileSystemPlugin^ source = FileSystemPlugin::GetPluginByNumber(PluginNr);
                if (source == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::PluginNotRegistered, PluginNr));
                String^ ret;
                try {
                    ret = this->managed(source, (CryptMode)Mode, gcnew String(ConnectionName), gcnew String(Password), maxlen);
                }
                catch (CryptException^ ex) {
                    return (int)ex->Reason;
                }
                catch (...) {
                    return (int)CryptResult::Fail;
                }
                StringCopy(ret, Password, maxlen);
                return (int)CryptResult::OK;
            }
            else if (this->ansi != NULL) {
                char* cn = UnicodeToAnsi(ConnectionName);
                try {
                    char* pwd = UnicodeToAnsi(Password, maxlen);
                    try {
                        int ret = this->ansi(PluginNr, CryptoNr, Mode, cn, pwd, maxlen);
                        AnsiToUnicode(pwd, Password, maxlen);
                        return ret;
                    }
                    finally{ delete[] pwd; }
                }
                finally{ delete[] cn; }
            }
            else if (this->unicode != NULL) {
                return this->unicode(PluginNr, CryptoNr, Mode, ConnectionName, Password, maxlen);
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        String^ CryptProcWrapper::operator()(FileSystemPlugin^ sender, CryptMode mode, String^ connectionName, String^ password, int maxlen) {//managed
            if (this->managed != nullptr) {
                return this->managed(sender, mode, connectionName, password, maxlen);
            }
            else if (this->ansi != NULL) {
                if (sender == nullptr) throw gcnew ArgumentNullException("sender");
                char* cn = StringCopyA(connectionName);
                try {
                    char* pwd = new char[maxlen];
                    try {
                        StringCopy(password, pwd, maxlen);
                        CryptResult ret = (CryptResult)this->ansi(sender->PluginNr, sender->CryptoNr, (int)mode, cn, pwd, maxlen);
                        switch (ret) {
                        case CryptResult::OK: return StringCopy(pwd);
                        case CryptResult::Fail:
                        case CryptResult::WriteError:
                        case CryptResult::ReadError:
                        case CryptResult::NoMasterPassword:
                            throw gcnew CryptException(ret);
                        default: throw gcnew CryptException(ResourcesT::Exceptions::UnknownError, CryptResult::Fail);
                        }
                    }
                    finally{ delete[] pwd; }
                }
                finally{ delete[] cn; }
            }
            else if (this->unicode != NULL) {
                if (sender == nullptr) throw gcnew ArgumentNullException("sender");
                wchar_t* cn = StringCopyW(connectionName);
                try {
                    wchar_t* pwd = new wchar_t[maxlen];
                    try {
                        StringCopy(password, pwd, maxlen);
                        CryptResult ret = (CryptResult)this->unicode(sender->PluginNr, sender->CryptoNr, (int)mode, cn, pwd, maxlen);
                        switch (ret) {
                        case CryptResult::OK: return StringCopy(pwd);
                        case CryptResult::Fail:
                        case CryptResult::WriteError:
                        case CryptResult::ReadError:
                        case CryptResult::NoMasterPassword:
                            throw gcnew CryptException(ret);
                        default: throw gcnew CryptException(ResourcesT::Exceptions::UnknownError, CryptResult::Fail);
                        }
                    }
                    finally{ delete[] pwd; }
                }
                finally{ delete[] cn; }
            }
            else throw gcnew InvalidOperationException(ResourcesT::Exceptions::NotInitialized);
        }
        inline String^ CryptProcWrapper::Invoke(FileSystemPlugin^ sender, CryptMode mode, String^ connectionName, String^ password, int maxlen) {
            return this(sender, mode, connectionName, password, maxlen);
        }
#pragma endregion
    }
}