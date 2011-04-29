#include "..\Plugin\fsplugin.h"
#include "WFX common.h"
#include "WFX Enums.h"
#pragma once

/// This file defines wrapper classes around TC callback functions.
/// Each TC callback function is defined by one of 3 callbacks - ANSI, Unicode and managed.
/// Function wrapper xlass stores internally one of the callbacks
/// and exposes externaly call operators for all there signatures
/// and internaly converts calls, parameters and return values for the only function stored internally.

using namespace System;
using namespace System::ComponentModel;

namespace Tools{ namespace TotalCommanderT{

    ref class FileSystemPlugin; //Forwared reference

    /// <summary>Wraps ANSI, Unicode or managed version of <see cref="ProgressCallback"/> and allows to call it in either way</summary>
    /// <seealso cref="FileSystemPlugin::ProgressProc"/>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    [EditorBrowsable(EditorBrowsableState::Advanced)]
    public ref class ProgressProcWrapper sealed{
    private:
        tProgressProc ansi;
        tProgressProcW unicode;
        ProgressCallback^ managed;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ProgressProcWrapper"/> class from ANSI function pointer</summary>
        /// <param name="method">Pointer to an ANSI function</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        [CLSCompliant(false)]
        ProgressProcWrapper(tProgressProc method);
        /// <summary>CTor - creates a new instance of the <see cref="ProgressProcWrapper"/> class from Unicode function pointer</summary>
        /// <param name="method">Pointer to a Unicode function</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        [CLSCompliant(false)]
        ProgressProcWrapper(tProgressProcW method);
        /// <summary>CTor - creates a new instance of the <see cref="ProgressProcWrapper"/> class from managed function delegate</summary>
        /// <param name="method">Function delegate</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        ProgressProcWrapper(ProgressCallback^ method);

        /// <summary>Invokes the inner representation of the function using ANSI parameters</summary>
        /// <param name="PluginNr">Here the plugin needs to pass the plugin number received through the <see cref="FileSystemPlugin::FsInit"/> function.</param>
        /// <param name="SourceName">Name of the source file being copied. Depending on the direction of the operation (Get, Put), this may be a local file name of a name in the plugin file system.</param>
        /// <param name="TargetName">Name to which the file is copied.</param>
        /// <param name="PercentDone">Percentage of THIS file being copied. Total Commander automatically shows a second percent bar if possible when multiple files are copied.</param>
        /// <returns>Total Commander returns 1 if the user wants to abort copying, and 0 if the operation can continue.</returns>
        /// <exception cref="InvalidOperationException">Managed function is called and plugin with number <paramref name="PluginNr"/> is not registered</exception>
        [CLSCompliant(false)]
        int operator()(int PluginNr, char* SourceName, char* TargetName, int PercentDone);//ANSI
        /// <summary>Invokes the inner representation of the function using Unicode parameters</summary>
        /// <param name="PluginNr">Here the plugin needs to pass the plugin number received through the <see cref="FileSystemPlugin::FsInit"/> function.</param>
        /// <param name="SourceName">Name of the source file being copied. Depending on the direction of the operation (Get, Put), this may be a local file name of a name in the plugin file system.</param>
        /// <param name="TargetName">Name to which the file is copied.</param>
        /// <param name="PercentDone">Percentage of THIS file being copied. Total Commander automatically shows a second percent bar if possible when multiple files are copied.</param>
        /// <returns>Total Commander returns 1 if the user wants to abort copying, and 0 if the operation can continue.</returns>
        /// <exception cref="InvalidOperationException">Managed function is called and plugin with number <paramref name="PluginNr"/> is not registered</exception>
        [CLSCompliant(false)]
        int operator()(int PluginNr, wchar_t* SourceName, wchar_t* TargetName, int PercentDone);//Unicode
        /// <summary>Invokes the inner representation of the function using managed parameters</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="sourceName">Name of the source file being copied. Depending on the direction of the operation (Get, Put), this may be a local file name of a name in the plugin file system.</param>
        /// <param name="targetName">Name to which the file is copied.</param>
        /// <param name="percentDone">Percentage of THIS file being copied. Total Commander automatically shows a second percent bar if possible when multiple files are copied.</param>
        /// <returns>Total Commander returns <c>true</c> if the user wants to abort copying, and <c>false</c> if the operation can continue.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        /// <remarks>This method is not CLS-compliant. CLS-compliant alternative is <see cref="Invoke"/></remarks>
        [CLSCompliant(false)]
        bool operator()(FileSystemPlugin^ sender, String^ sourceName, String^ targetName, int percentDone);//Managed
        /// <summary>Invokes the inner representation of the function using managed parameters</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="sourceName">Name of the source file being copied. Depending on the direction of the operation (Get, Put), this may be a local file name of a name in the plugin file system.</param>
        /// <param name="targetName">Name to which the file is copied.</param>
        /// <param name="percentDone">Percentage of THIS file being copied. Total Commander automatically shows a second percent bar if possible when multiple files are copied.</param>
        /// <returns>Total Commander returns <c>true</c> if the user wants to abort copying, and <c>false</c> if the operation can continue.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        bool Invoke (FileSystemPlugin^ sender, String^ sourceName, String^ targetName, int percentDone);
    };

    /// <summary>Wraps ANSI, Unicode or managed version of <see cref="LogCallback"/> and allows to call it in either way</summary>
    /// <seealso cref="FileSystemPlugin::LogProc"/>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    [EditorBrowsable(EditorBrowsableState::Advanced)]
    public ref class LogProcWrapper sealed{
    private:
        tLogProc ansi;
        tLogProcW unicode;
        LogCallback^ managed;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="LogProcWrapper"/> class from ANSI function pointer</summary>
        /// <param name="method">Pointer to an ANSI function</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        [CLSCompliant(false)]
        LogProcWrapper(tLogProc method);
        /// <summary>CTor - creates a new instance of the <see cref="LogProcWrapper"/> class from Unicode function pointer</summary>
        /// <param name="method">Pointer to a Unicode function</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        [CLSCompliant(false)]
        LogProcWrapper(tLogProcW method);
        /// <summary>CTor - creates a new instance of the <see cref="LogProcWrapper"/> class from managed function delegate</summary>
        /// <param name="method">Function delegate</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        LogProcWrapper(LogCallback^ method);

        /// <summary>Invokes the inner representation of the function using ANSI parameters</summary>
        /// <param name="PluginNr">Here the plugin needs to pass the plugin number received through the <see cref="FileSystemPlugin::FsInit"/> function.</param>
        /// <param name="MsgType">One of the <see cref="LogKind"/> values</param>
        /// <param name="LogString">String which should be logged.</param>
        /// <exception cref="InvalidOperationException">Managed function is called and plugin with number <paramref name="PluginNr"/> is not registered</exception>
        /// <remarks>When <paramref name="MsgType"> is <see cref2="F:Tools.TotalCommader.LogKind.Connect"/> the string must have special format. See <see cref="FileSystemPlugin::LogProc"/> for details.</remarks>
        [CLSCompliant(false)]
        void operator()(int PluginNr, int MsgType, char* LogString);
        /// <summary>Invokes the inner representation of the function using Unicode parameters</summary>
        /// <param name="PluginNr">Here the plugin needs to pass the plugin number received through the <see cref="FileSystemPlugin::FsInit"/> function.</param>
        /// <param name="MsgType">One of the <see cref="LogKind"/> values</param>
        /// <param name="LogString">String which should be logged.</param>
        /// <exception cref="InvalidOperationException">Managed function is called and plugin with number <paramref name="PluginNr"/> is not registered</exception>
        /// <remarks>When <paramref name="MsgType"> is <see cref2="F:Tools.TotalCommader.LogKind.Connect"/> the string must have special format. See <see cref="FileSystemPlugin::LogProc"/> for details.</remarks>
        [CLSCompliant(false)]
        void operator()(int PluginNr, int MsgType, WCHAR* LogString);
        /// <summary>Invokes the inner representation of the function using managed parameters</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="msgType">One of the <see cref="LogKind"/> values</param>
        /// <param name="logString">String which should be logged.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        /// <remarks>This method is not CLS-compliant. CLS-compliant alternative is <see cref="Invoke"/>.
        /// <para>When <paramref name="MsgType"> is <see cref2="F:Tools.TotalCommader.LogKind.Connect"/> the string must have special format. See <see cref="FileSystemPlugin::LogProc"/> for details.</para></remarks>
        [CLSCompliant(false)]
        void operator()(FileSystemPlugin^ sender, LogKind msgType, String^ logString);
        /// <summary>Invokes the inner representation of the function using managed parameters</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        /// <remarks>This is CLS compliant alternative of <see cref="operator()"/></remarks>
        void Invoke(FileSystemPlugin^ sender, LogKind msgType, String^ logString);
    };

    /// <summary>Wraps ANSI, Unicode or managed version of <see cref="RequestCallback"/> and allows to call it in either way</summary>
    /// <seealso cref="FileSystemPlugin::RequestProc"/>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    [EditorBrowsable(EditorBrowsableState::Advanced)]
    public ref class RequestProcWrapper sealed{
    private:
        tRequestProc ansi;
        tRequestProcW unicode;
        RequestCallback^ managed;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="RequestProcWrapper"/> class from ANSI function pointer</summary>
        /// <param name="method">Pointer to an ANSI function</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        [CLSCompliant(false)]
        RequestProcWrapper(tRequestProc method);
        /// <summary>CTor - creates a new instance of the <see cref="RequestProcWrapper"/> class from Unicode function pointer</summary>
        /// <param name="method">Pointer to a Unicode function</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        [CLSCompliant(false)]
        RequestProcWrapper(tRequestProcW method);
        /// <summary>CTor - creates a new instance of the <see cref="RequestProcWrapper"/> class from managed function delegate</summary>
        /// <param name="method">Function delegate</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        RequestProcWrapper(RequestCallback^ method);

        /// <summary>Invokes the inner representation of the function using ANSI parameters</summary>
        /// <param name="PluginNr">Here the plugin needs to pass the plugin number received through the <see cref="FileSystemPlugin::FsInit"/> function.</param>
        /// <param name="RequestType">One of the <see cref="InputRequestKind"/> values</param>
        /// <param name="CustomTitle">Custom title for the dialog box. If null or empty, it will be "Total Commander"</param>
        /// <param name="CustomText">Override the text defined with RequestType. Set this to null or an empty string to use the default text. The default text will be translated to the language set in the calling program.</param>
        /// <param name="ReturnedText">This string contains the default text presented to the user, and will receive the (modified) text which the user enters. set <paramref name="ReturnedText"/>[0]=0 to have no default text.</param>
        /// <param name="maxlen">Maximum length allowed for <paramref name="ReturnedText"/>. The pointer <paramref name="ReturnedText"/> must point to a buffer which can hold at least maxlen characters.</param>
        /// <returns>Returns True if the user clicked OK or Yes, False otherwise.</returns>
        /// <exception cref="InvalidOperationException">Managed function is called and plugin with number <paramref name="PluginNr"/> is not registered</exception>
        [CLSCompliant(false)]
        BOOL operator()(int PluginNr, int RequestType, char* CustomTitle, char* CustomText, char* ReturnedText, int maxlen);
        /// <summary>Invokes the inner representation of the function using Unicode parameters</summary>
        /// <param name="PluginNr">Here the plugin needs to pass the plugin number received through the <see cref="FileSystemPlugin::FsInit"/> function.</param>
        /// <param name="RequestType">One of the <see cref="InputRequestKind"/> values</param>
        /// <param name="CustomTitle">Custom title for the dialog box. If null or empty, it will be "Total Commander"</param>
        /// <param name="CustomText">Override the text defined with RequestType. Set this to null or an empty string to use the default text. The default text will be translated to the language set in the calling program.</param>
        /// <param name="ReturnedText">This string contains the default text presented to the user, and will receive the (modified) text which the user enters. set <paramref name="ReturnedText"/>[0]=0 to have no default text.</param>
        /// <param name="maxlen">Maximum length allowed for <paramref name="ReturnedText"/>. The pointer <paramref name="ReturnedText"/> must point to a buffer which can hold at least maxlen characters.</param>
        /// <returns>Returns True if the user clicked OK or Yes, False otherwise.</returns>
        /// <exception cref="InvalidOperationException">Managed function is called and plugin with number <paramref name="PluginNr"/> is not registered</exception>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        [CLSCompliant(false)]
        BOOL operator()(int PluginNr, int RequestType, WCHAR* CustomTitle, WCHAR* CustomText, WCHAR* ReturnedText, int maxlen);
        /// <summary>Invokes the inner representation of the function using managed parameters</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="requestType">One of the <see cref="InputRequestKind"/> values</param>
        /// <param name="customTitle">Custom title for the dialog box. If null or empty, it will be "Total Commander"</param>
        /// <param name="customText">Override the text defined with RequestType. Set this to null or an empty string to use the default text. The default text will be translated to the language set in the calling program.</param>
        /// <param name="maxlen">Maximum length allowed for <paramref name="ReturnedText"/>. The pointer <paramref name="ReturnedText"/> must point to a buffer which can hold at least maxlen characters.</param>
        /// <returns>User-entered text if user clicked Yes or OK. Null otherwise</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        /// <exception cref="ArgumentException"><paramref name="defaultText"/> is longer than <paramref name="maxlen"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxlen"/> is less than 1</exception>
        /// <remarks>This method is not CLS-compliant. CLS-compliant alternative is <see cref="Invoke"/></remarks>
        [CLSCompliant(false)]
        String^ operator()(FileSystemPlugin^ sender, InputRequestKind requestType,String^ customTitle, String^ customText, String^ defaultText, int maxlen);
        /// <summary>Invokes the inner representation of the function using managed parameters</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="requestType">One of the <see cref="InputRequestKind"/> values</param>
        /// <param name="customTitle">Custom title for the dialog box. If null or empty, it will be "Total Commander"</param>
        /// <param name="customText">Override the text defined with RequestType. Set this to null or an empty string to use the default text. The default text will be translated to the language set in the calling program.</param>
        /// <param name="maxlen">Maximum length allowed for <paramref name="ReturnedText"/>. The pointer <paramref name="ReturnedText"/> must point to a buffer which can hold at least maxlen characters.</param>
        /// <returns>User-entered text if user clicked Yes or OK. Null otherwise</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        /// <exception cref="ArgumentException"><paramref name="defaultText"/> is longer than <paramref name="maxlen"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxlen"/> is less than 1</exception>
        /// <remarks>This is CLS compliant alternative of <see cref="operator()"/></remarks>
        String^ Invoke(FileSystemPlugin^ sender, InputRequestKind requestType,String^ customTitle, String^ customText, String^ defaultText, int maxlen);
    };

    /// <summary>Wraps ANSI, Unicode or managed version of <see cref="CryptCallback"/> and allows to call it in either way</summary>
    /// <seealso cref="FileSystemPlugin::PerformCryptoOperation"/>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    [EditorBrowsable(EditorBrowsableState::Advanced)]
    public ref class CryptProcWrapper sealed{
    private:
        tCryptProc ansi;
        tCryptProcW unicode;
        CryptCallback^ managed;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="RequestProcWrapper"/> class from ANSI function pointer</summary>
        /// <param name="method">Pointer to an ANSI function</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        [CLSCompliant(false)]
        CryptProcWrapper(tCryptProc method);
        /// <summary>CTor - creates a new instance of the <see cref="RequestProcWrapper"/> class from Unicode function pointer</summary>
        /// <param name="method">Pointer to a Unicode function</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        [CLSCompliant(false)]
        CryptProcWrapper(tCryptProcW method);
        /// <summary>CTor - creates a new instance of the <see cref="RequestProcWrapper"/> class from managed function delegate</summary>
        /// <param name="method">Function delegate</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        CryptProcWrapper(CryptCallback^ method);

        /// <summary>Invokes the inner representation of the function using ANSI parameters</summary>
        /// <param name="PluginNr">Here the plugin needs to pass the plugin number received through the <see cref="FileSystemPlugin::FsInit"/> function.</param>
        /// <param name="CryptoNr">Here the plugin needs to pass the crypto number received through the <see cref="FileSystemPlugin::FsSetCryptCallback"/> function.</param>
        /// <param name="Mode">The mode of operation (one of the <see cref="CryptMode"/> values)</param>
        /// <param name="ConnectionName">Name of the connection for this operation</param>
        /// <param name="Password">Operation-specific, usually the password to be stored/retrieved, or the target connection when copying/moving a connection</param>
        /// <param name="maxlen">Maximum length, in characters, the password buffer can store when calling one of the load functions</param>
        /// <exception cref="InvalidOperationException">Managed function is called and plugin with number <paramref name="PluginNr"/> is not registered</exception>
        [CLSCompliant(false)]
        int operator()(int PluginNr, int CryptoNr, int Mode, char* ConnectionName, char* Password, int maxlen);
        /// <summary>Invokes the inner representation of the function using Unicode parameters</summary>
        /// <param name="PluginNr">Here the plugin needs to pass the plugin number received through the <see cref="FileSystemPlugin::FsInit"/> function.</param>
        /// <param name="CryptoNr">Here the plugin needs to pass the crypto number received through the <see cref="FileSystemPlugin::FsSetCryptCallback"/> function.</param>
        /// <param name="Mode">The mode of operation (one of the <see cref="CryptMode"/> values)</param>
        /// <param name="ConnectionName">Name of the connection for this operation</param>
        /// <param name="Password">Operation-specific, usually the password to be stored/retrieved, or the target connection when copying/moving a connection</param>
        /// <param name="maxlen">Maximum length, in characters, the password buffer can store when calling one of the load functions</param>
        /// <returns>Total Commander returns one of the <see cref="CryptResult"/> values</returns>
        /// <exception cref="InvalidOperationException">Managed function is called and plugin with number <paramref name="PluginNr"/> is not registered</exception>
        [CLSCompliant(false)]
        int operator()(int PluginNr, int CryptoNr, int Mode, wchar_t* ConnectionName, wchar_t* Password, int maxlen);
        /// <summary>Invokes the inner representation of the function using managed parameters</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="mode">The mode of operation</param>
        /// <param name="connectionName">Name of the connection for this operation</param>
        /// <param name="password">Operation-specific, usually the password to be stored/retrieved, or the target connection when copying/moving a connection</param>
        /// <param name="maxlen">Maximum length, in characters, the password buffer can store when calling one of the load functions</param>
        /// <returns>Total Commander returns one of the <see cref="CryptResult"/> values</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxlen"/> is less than 1</exception>
        /// <exception cref="CryptException">Crypto operation failed.</exception>
        /// <remarks>This method is not CLS-compliant. CLS-compliant alternative is <see cref="Invoke"/></remarks>
        [CLSCompliant(false)]
        String^ operator()(FileSystemPlugin^ sender, CryptMode mode, String^ connectionName, String^ password, int maxlen);
        /// <summary>Invokes the inner representation of the function using managed parameters</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="mode">The mode of operation</param>
        /// <param name="connectionName">Name of the connection for this operation</param>
        /// <param name="password">Operation-specific, usually the password to be stored/retrieved, or the target connection when copying/moving a connection</param>
        /// <param name="maxlen">Maximum length, in characters, the password buffer can store when calling one of the load functions</param>
        /// <returns>Total Commander returns one of the <see cref="CryptResult"/> values</returns>
        /// <returns>Password retrieved. Only when <paramref name="mode"/> is <see cref2="F:Tools.TotalCommanderT.CryptMode.LoadPassword"/> or <see cref="F:Tools.TotalCommanderT.CryptMode.LoadPasswordNoUI"/>. Otherwise returns <paramref name="password"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxlen"/> is less than 1</exception>
        /// <exception cref="CryptException">Crypto operation failed.</exception>
        /// <remarks>This is CLS compliant alternative of <see cref="operator()"/></remarks>
        String^ Invoke(FileSystemPlugin^ sender, CryptMode mode, String^ connectionName, String^ password, int maxlen);
    };

}}