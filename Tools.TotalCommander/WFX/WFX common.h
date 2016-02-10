#pragma once

#include "..\Plugin\fsplugin.h"
#include "..\Common.h"
#include "..\ContentPluginBase.h"
#include "WFX Enums.h"
#include "ViewDefinition.h"
#include "FindData.h"
#include "RemoteInfo.h"

namespace Tools {
    namespace TotalCommanderT {
        using namespace System;
        using namespace System::ComponentModel;

        /// <summary>Arguments of operation status notifications</summary>
        public ref class OperationEventArgs : EventArgs {
        private:
            /// <summary>Contains value of the <see cref="RemoteDir"/> property</summary>
            String^ remoteDir;
            /// <summary>Contains value of the <see cref="Kind"/> property</summary>
            OperationKind kind;
            /// <summary>Contains value of the <see cref="Status"/> property</summary>
            OperationStatus status;
        public:
            /// <summary>CTor - Creates new instance of the <see cref="OperationEventArgs"/> class</summary>
            /// <param name="remoteDir">This is the current source directory when the operation starts. May be used to find out which part of the file system is affected.</param>
            /// <param name="status">Information whether the operation starts or ends</param>
            /// <param name="kind">Information of which operation starts/ends</param>
            /// <exception cref="ArgumentNullException"><paramref name="remoteDir"/> is null</exception>
            OperationEventArgs(String^ remoteDir, OperationKind kind, OperationStatus status);
            /// <summary>This is the current source directory when the operation starts. May be used to find out which part of the file system is affected.</summary>
            property String^ RemoteDir {String^ get(); }
            /// <summary>Information of which operation starts/ends.</summary>
            property OperationKind Kind {OperationKind get(); }
            /// <summary>Information whether the operation starts or ends.</summary>
            property OperationStatus Status {OperationStatus get(); }
        };

        /// <summary>Holds reference to file miniature bitmap</summary>
        /// <remarks>If you do not provide bitmap for certain file/directory return null instead of instance of this class</remarks>
        public ref class BitmapResult {
        private:
            /// <summary>Contains value of the <see cref="ImageKey"/> property</summary>
            String^ imagekey;
        public:
            /// <summary>CTor - creates new instance of the <see cref="BitmapResult"/> class from image path</summary>
            /// <param name="ImagePath">Path to image preview bitmap is stored in.</param>
            /// <para name="Temporary">True if <paramref name="ImagePath"/> points to temporary file Total Commander shall delete when it is no longer needed (after bitmap is read)</para>
            /// <exception cref="IO::PathTooLongException">The length of <paramref name="ImagePath"/> is greater than <see cref="FindData::MaxPath"/> - 1</exception>
            /// <exception cref="ArgumentNullException"><paramref name="ImagePath"/> is null</exception>
            BitmapResult(String^ ImagePath, bool Temporary);
            /// <summary>CTor - creates new instance of the <see cref="BitmapResult"/> class from miniature image</summary>
            /// <param name="Bitmap">Miniature image</param>
            /// <exception cref="ArgumentNullException"><paramref name="Bitmap"/> is null</exception>
            BitmapResult(Drawing::Bitmap^ Bitmap);
            /// <summary>CTor - creates new instance of the <see cref="BitmapResult"/> class from miniature image and caching key</summary>
            /// <param name="Bitmap">Miniature image</param>
            /// <param name="ImageKey">Key that uniquelly identifies the image. Total Commander will internally cache the image using this key. Null to prevent chaching (i.e. the plugin caches the image)</param>
            /// <exception cref="ArgumentNullException"><paramref name="Bitmap"/> is null</exception>
            BitmapResult(Drawing::Bitmap^ Bitmap, String^ ImageKey);
            /// <summary>Gets or sets the miniature image</summary>
            /// <returns>The miniature image</returns>
            /// <value>When value of this property is non-null the image will be passed to Total Commander.
            /// <para>When value of this property is null, Total Commander will be instructed to extract miniature image from file at path <see cref="ImageKey"/>.</para></value>
            /// <remarks>Make sure you scale your image correctly to the desired maximum width+height! Do not fill the rest of the bitmap - instead, create a bitmap which is SMALLER than requested! This way, Total Commander can center your image and fill the rest with the default background color.</remarks>
            property Drawing::Bitmap^ Image;
            /// <summary>Gets or sets image key - it can be either path to image file Total Commander should extract miniature from or unique key of image or null</summary>
            /// <returns>Image key</returns>
            /// <value>When <see cref="Image"/> is null this property represents path to image file containing miniature image.
            /// <para>When <see cref="Image"/> is not null this property represents unique image key. Total Commander then caches the image under that key.</para></value>
            /// <exception cref="IO::PathTooLongException">Value longer than <see ctef="FindData::MaxPath"/> - 1 is set</exception>
            /// <remarks>When both - <see cref="Image"/> and <see cref="ImageKey"/> are null, Total Commander will be left with original image path as path to load miniature from. But the path targets to plugin file system space - so it is invalid from Total Commmander perspective. So, do not set both <see cref="Image"/> and <see cref="ImageKey"/> to null.</remarks>
            property String^ ImageKey {String^ get(); void set(String^); }
            /// <summary>Indicates if image should be cahced by Total Commander</summary>
            /// <value>Set this property to true to make Total Commander cahce the image under <see cref="ImageKey"/>. Do not set this property to true when you cahce the image in plugin.</value>
            /// <remarks>If value of this property is true Total Commander will chache the image under key <see cref="ImageKey"/>. Ignored when <see cref="Image"/> is not null.
            /// <para>When <see cref="Cache"/> is true and <see cref="ImageKey"/> is null, image is cached under key of its original path in plugin file system space by Total Commander.</para></remarks>
            property bool Cache;
            /// <summary>Indicates that image file pointer by path stored in <see cref="ImageKey"/> is temporary and should be deleted by Total Commander when it is no longer necessary.</summary>
            /// <returns>True when image will be deleted by Total Commander; false if not</returns>
            /// <value>True to make Total Commander delete the image when no longer necessary; false to make sure that image file will not be deleted by Total Commander.</value>
            /// <remarks>Ignored when <see cref="Image"/> is not null</remarks>
            property bool Temporary;
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            /// <summary>Gets <see cref="BitmapHandling"/> represented by this instance</summary>
            /// <returns>A <see cref="BitmapHandling"/> value to pass to Total Commander</returns>
            BitmapHandling GetFlag();
        };

        /// <summary>Exception thrown when there is problem in crypt functions (Total Commander secure passsword store)</summary>
        /// <version version="1.5.4">This class is new in version 1.5.4</version>
        public ref class CryptException sealed : System::Security::Cryptography::CryptographicException {
        public:
            /// <summary>CTor - creates a new instance of the <see cref="CryptException"/> class from error message and reason code</summary>
            /// <param name="message">The error message that explains the reason for the exception.</param>
            /// <param name="reason">One of <seee cref="CryptResult"/> values indicating kind of failure</param>
            /// <exception cref="ArgumentException"><paramref name="reason"/> is <see cref2="F:Tools.TotalCommanderT.CryptResult.OK"/></exception>
            /// <exception cref="InvalidEnumArgumentException"><paramref name="reason"/> is not one of <see cref="CryptResult"/> values</exception>
            CryptException(String^ message, CryptResult reason);
            /// <summary>CTor - creates a new instance of the <see cref="CryptException"/> class from error reason</summary>
            /// <param name="reason">One of <seee cref="CryptResult"/> values indicating kind of failure</param>
            /// <exception cref="ArgumentException"><paramref name="reason"/> is <see cref2="F:Tools.TotalCommanderT.CryptResult.OK"/></exception>
            /// <exception cref="InvalidEnumArgumentException"><paramref name="reason"/> is not one of <see cref="CryptResult"/> values</exception>
            CryptException(CryptResult reason);
            /// <summary>Gets value indicating why the operation failed</summary>
            property CryptResult Reason { CryptResult get(); }
        private:
            /// <summary>Gets default string error message from <see cref="CryptResult"/></summary>
            /// <param name="reason">Indicates why the crypto operation failed</param>
            /// <exception cref="ArgumentException"><paramref name="reason"/> is <see cref2="F:Tools.TotalCommanderT.CryptResult.OK"/></exception>
            /// <exception cref="InvalidEnumArgumentException"><paramref name="reason"/> is not one of <see cref="CryptResult"/> values</exception>
            static String^ GetMessage(CryptResult reason);
        };

#pragma region "Delegates"
        ref class FileSystemPlugin;//Forward declaration
        /// <summary>Callback function, which the plugin can call to show copy progress.</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="sourceName">Name of the source file being copied. Depending on the direction of the operation (Get, Put), this may be a local file name of a name in the plugin file system.</param>
        /// <param name="targetName">Name to which the file is copied.</param>
        /// <param name="percentDone">Percentage of THIS file being copied. Total Commander automatically shows a second percent bar if possible when multiple files are copied.</param>
        /// <returns>Total Commander returns <c>true</c> if the user wants to abort copying, and <c>false</c> if the operation can continue.</returns>
        /// <remarks>You should call this function at least twice in the copy functions <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.GetFile(System.String,System.String@,Tools.TotalCommanderT.CopyFlags,Tools.TotalCommanderT.RemoteInfo)"/>, <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.PutFile(System.String,System.String@,Tools.TotalCommanderT.CopyFlags)"/> and <see2 cref2="M:Tools.TotalCommanderT.RenMovFile(System.String,System.String,System.Boolean,System.Boolean,Tools.TotalCommanderT.RemoteInfo)"/>, at the beginning and at the end. If you can't determine the progress, call it with 0% at the beginning and 100% at the end.
        /// <para>During the <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindFirst(System.String,Tools.TotalCommanderT.FindData@)"/>/<see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindNext(System.Object,Tools.TotalCommanderT.FindData@)"/>/<see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindClose(System.Object)"/> loop, the plugin may now call the <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.ProgressProc(System.String,System.String,System.Int32)"/> to make a progess dialog appear. This is useful for very slow connections. Don't call <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.ProgressProc(System.String,System.String,System.Int32)"/> for fast connections! The progress dialog will only be shown for normal dir changes, not for compound operations like get/put. The calls to <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.ProgressProc(System.String,System.String,System.Int32)"/> will also be ignored during the first 5 seconds, so the user isn't bothered with a progress dialog on every dir change.</para></remarks>
        /// <version version="1.5.4">Parameters renamed: <c>SourceName</c> to <c>sourceName</c>, <c>TargetName</c> to <c>targetName</c>, <c>PercentDone</c> to <c>percentDone</c>.</version>
        public delegate bool ProgressCallback(FileSystemPlugin^ sender, String^ sourceName, String^ targetName, int percentDone);
        /// <summary>Callback function, which the plugin can call to show the FTP connections toolbar, and to pass log messages to it. Totalcmd can show these messages in the log window (ftp toolbar) and write them to a log file.</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="msgType">Can be one of the <see cref="LogKind"/> flags</param>
        /// <param name="logString">String which should be logged.
        /// <para>When <paramref name="MsgType"/>is <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/>, the string MUST have a specific format:</para>
        /// <para><c>"CONNECT"</c> followed by a single whitespace, then the root of the file system which was connected, without trailing backslash. Example: <c>CONNECT \Filesystem</c></para>
        /// <para>When <paramref name="MsgType"/> is <see2 cref2="F:Tools.TotalCommanderT.LogKind.TransferComplete"/>, this parameter should contain both the source and target names, separated by an arrow <c>" -> "</c>, e.g. <c>Download complete: \Filesystem\dir1\file1.txt -> c:\localdir\file1.txt</c></para></param>
        /// <remarks>Do NOT call <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.LogProc(Tools.TotalCommanderT.LogKind,System.String)"/> with <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/> if your plugin does not require connect/disconnect! If you call it with <paramref name="MsgType"/> <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/>, the function <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.Disconnect(System.String)"/> will be called (if defined) when the user presses the Disconnect button.</remarks>
        /// <version version="1.5.4">Parameters renamed: <c>MsgType</c> to <c>msgType</c>, <c>LogString</c> to <c>logString</c>.</version>
        public delegate void LogCallback(FileSystemPlugin^ sender, LogKind msgType, String^ logString);
        /// <summary>Callback function, which the plugin can call to request input from the user. When using one of the standard parameters, the request will be in the selected language.</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="requestType">Can be one of the <see cref="InputRequestKind"/> flags</param>
        /// <param name="customTitle">Custom title for the dialog box. If NULL or empty, it will be "Total Commander"</param>
        /// <param name="customText">Override the text defined with <paramref name="RequestType"/>. Set this to NULL or an empty string to use the default text. The default text will be translated to the language set in the calling program.</param>
        /// <param name="defaultText">This string contains the default text presented to the user. Set <paramref name="DefaultText"/>[0]=0 to have no default text.</param>
        /// <param name="maxlen">Maximum length allowed for returned text.</param>
        /// <returns>User-entered text if user clicked Yes or OK. Null otherwise</returns>
        /// <remarks>Leave <paramref name="CustomText"/> empty if you want to use the (translated) default strings!</remarks>
        /// <exception cref="ArgumentException"><paramref name="DefaultText"/> is longer than <paramref name="maxlen"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxlen"/> is less than 1</exception>
        /// <version version="1.5.4">Parameters renamed: <c>RequestType</c> to <c>requestType</c>, <c>CustomTitle</c> to <c>customTitle</c>, <c>CustomText</c> to <c>customText</c>, <c>DefaultText</c> to <c>defaultText</c>.</version>
        public delegate String^ RequestCallback(FileSystemPlugin^ sender, InputRequestKind requestType, String^ customTitle, String^ customText, String^ defaultText, int maxlen);
        /// <summary>Callback function, which the plugin can call to store passwords in the secure password store, read them back, or copy them to a new connection.</summary>
        /// <param name="sender">The plugin requesting the operation</param>
        /// <param name="mode">The mode of operation</param>
        /// <param name="connectionName">Name of the connection for this operation</param>
        /// <param name="password">Operation-specific, usually the password to be stored, or the target connection when copying/moving a connection</param>
        /// <param name="maxlen">Maximum length, in characters, the password buffer can store when calling one of the load functions</param>
        /// <returns>Password retrieved. Only when <paramref name="mode"/> is <see cref2="F:Tools.TotalCommanderT.CryptMode.LoadPassword"/> or <see cref2="F:Tools.TotalCommanderT.CryptMode.LoadPasswordNoUI"/>. Otherwise returns <paramref name="password"/>.</returns>
        /// <exception cref="CryptException">Crypto operation failed.</exception>
        /// <remarks>This delegate is used only when Total Commander plugin is used outside Total Commander.</remarks>
        /// <version version="1.5.4">This delegate is new in version 1.5.4</version>
        public delegate String^ CryptCallback(FileSystemPlugin^ sender, CryptMode mode, String^ connectionName, String^ password, int maxlen);
#pragma endregion
    }
}
