using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace DMKSoftware.CodeGenerators
{
    public static class NativeMethods
    {
        static NativeMethods()
        {
            InvalidIntPtr = (IntPtr) (-1);
            IID_IServiceProvider = typeof(Microsoft.VisualStudio.OLE.Interop.IServiceProvider).GUID;
            IID_IObjectWithSite = typeof(IObjectWithSite).GUID;
            IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
            GUID_VSStandardCommandSet97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");
            GUID_HtmlEditorFactory = new Guid("{C76D83F8-A489-11D0-8195-00A0C91BBEE3}");
            GUID_TextEditorFactory = new Guid("{8B382828-6202-11d1-8870-0000F87579D2}");
            CLSID_VsEnvironmentPackage = new Guid("{DA9FB551-C724-11d0-AE1F-00A0C90FFFC3}");
            GUID_VsNewProjectPseudoFolder = new Guid("{DCF2A94A-45B0-11d1-ADBF-00C04FB6BE4C}");
            CLSID_MiscellaneousFilesProject = new Guid("{A2FE74E1-B743-11d0-AE1A-00A0C90FFFC3}");
            CLSID_SolutionItemsProject = new Guid("{D1DCDB85-C5E8-11d2-BFCA-00C04F990235}");
            SID_SVsGeneralOutputWindowPane = new Guid("{65482c72-defa-41b7-902c-11c091889c83}");
            SID_SUIHostCommandDispatcher = new Guid("{e69cd190-1276-11d1-9f64-00a0c911004f}");
            CLSID_VsUIHierarchyWindow = new Guid("{7D960B07-7AF8-11D0-8E5E-00A0C911005A}");
            GUID_DefaultEditor = new Guid("{6AC5EF80-12BF-11D1-8E9B-00A0C911005A}");
            GUID_ExternalEditor = new Guid("{8137C9E8-35FE-4AF2-87B0-DE3C45F395FD}");
            GUID_OutWindowGeneralPane = new Guid("{3c24d581-5591-4884-a571-9fe89915cd64}");
            BuildOrder = new Guid("2032b126-7c8d-48ad-8026-0e0348004fc0");
            BuildOutput = new Guid("1BD8A850-02D1-11d1-BEE7-00A0C913D1F8");
            DebugOutput = new Guid("FC076020-078A-11D1-A7DF-00A0C9110051");
            GUID_ItemType_PhysicalFile = new Guid("{6bb5f8ee-4483-11d3-8bcf-00c04f8ec28c}");
            GUID_ItemType_PhysicalFolder = new Guid("{6bb5f8ef-4483-11d3-8bcf-00c04f8ec28c}");
            GUID_ItemType_VirtualFolder = new Guid("{6bb5f8f0-4483-11d3-8bcf-00c04f8ec28c}");
            GUID_ItemType_SubProject = new Guid("{EA6618E8-6E24-4528-94BE-6889FE16485C}");
            UICONTEXT_SolutionBuilding = new Guid("{adfc4e60-0397-11d1-9f4e-00a0c911004f}");
            UICONTEXT_Debugging = new Guid("{adfc4e61-0397-11d1-9f4e-00a0c911004f}");
            UICONTEXT_Dragging = new Guid("{b706f393-2e5b-49e7-9e2e-b1825f639b63}");
            UICONTEXT_FullScreenMode = new Guid("{adfc4e62-0397-11d1-9f4e-00a0c911004f}");
            UICONTEXT_DesignMode = new Guid("{adfc4e63-0397-11d1-9f4e-00a0c911004f}");
            UICONTEXT_NoSolution = new Guid("{adfc4e64-0397-11d1-9f4e-00a0c911004f}");
            UICONTEXT_SolutionExists = new Guid("{f1536ef8-92ec-443c-9ed7-fdadf150da82}");
            UICONTEXT_EmptySolution = new Guid("{adfc4e65-0397-11d1-9f4e-00a0c911004f}");
            UICONTEXT_SolutionHasSingleProject = new Guid("{adfc4e66-0397-11d1-9f4e-00a0c911004f}");
            UICONTEXT_SolutionHasMultipleProjects = new Guid("{93694fa0-0397-11d1-9f4e-00a0c911004f}");
            UICONTEXT_CodeWindow = new Guid("{8fe2df1d-e0da-4ebe-9d5c-415d40e487b5}");
            GUID_VsTaskListViewAll = new Guid("{1880202e-fc20-11d2-8bb1-00c04f8ec28c}");
            GUID_VsTaskListViewUserTasks = new Guid("{1880202f-fc20-11d2-8bb1-00c04f8ec28c}");
            GUID_VsTaskListViewShortcutTasks = new Guid("{18802030-fc20-11d2-8bb1-00c04f8ec28c}");
            GUID_VsTaskListViewHTMLTasks = new Guid("{36ac1c0d-fe86-11d2-8bb1-00c04f8ec28c}");
            GUID_VsTaskListViewCompilerTasks = new Guid("{18802033-fc20-11d2-8bb1-00c04f8ec28c}");
            GUID_VsTaskListViewCommentTasks = new Guid("{18802034-fc20-11d2-8bb1-00c04f8ec28c}");
            GUID_VsTaskListViewCurrentFileTasks = new Guid("{18802035-fc20-11d2-8bb1-00c04f8ec28c}");
            GUID_VsTaskListViewCheckedTasks = new Guid("{18802036-fc20-11d2-8bb1-00c04f8ec28c}");
            GUID_VsTaskListViewUncheckedTasks = new Guid("{18802037-fc20-11d2-8bb1-00c04f8ec28c}");
            CLSID_VsTaskList = new Guid("{BC5955D5-aa0d-11d0-a8c5-00a0c921a4d2}");
            CLSID_VsTaskListPackage = new Guid("{4A9B7E50-aa16-11d0-a8c5-00a0c921a4d2}");
            SID_SVsToolboxActiveXDataProvider = new Guid("{35222106-bb44-11d0-8c46-00c04fc2aae2}");
            CLSID_VsDocOutlinePackage = new Guid("{21af45b0-ffa5-11d0-b63f-00a0c922e851}");
            CLSID_VsCfgProviderEventsHelper = new Guid("{99913f1f-1ee3-11d1-8a6e-00c04f682e21}");
            GUID_COMPlusPage = new Guid("{9A341D95-5A64-11d3-BFF9-00C04F990235}");
            GUID_COMClassicPage = new Guid("{9A341D96-5A64-11d3-BFF9-00C04F990235}");
            GUID_SolutionPage = new Guid("{9A341D97-5A64-11d3-BFF9-00C04F990235}");
            LOGVIEWID_Any = new Guid(uint.MaxValue, 0xffff, 0xffff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff);
            LOGVIEWID_Primary = Guid.Empty;
            LOGVIEWID_Debugging = new Guid("{7651a700-06e5-11d1-8ebd-00a0c90f26ea}");
            LOGVIEWID_Code = new Guid("{7651a701-06e5-11d1-8ebd-00a0c90f26ea}");
            LOGVIEWID_Designer = new Guid("{7651a702-06e5-11d1-8ebd-00a0c90f26ea}");
            LOGVIEWID_TextView = new Guid("{7651a703-06e5-11d1-8ebd-00a0c90f26ea}");
            LOGVIEWID_UserChooseView = new Guid("{7651a704-06e5-11d1-8ebd-00a0c90f26ea}");
            GUID_VsUIHierarchyWindowCmds = new Guid("{60481700-078b-11d1-aaf8-00a0c9055a90}");
        }

        [DllImport("Kernel32", EntryPoint="RtlMoveMemory", ExactSpelling=true)]
        public static extern void CopyMemory(IntPtr pdst, byte[] psrc, int cb);
        [DllImport("Kernel32", EntryPoint="RtlMoveMemory", ExactSpelling=true)]
        public static extern void CopyMemory(IntPtr pdst, HandleRef psrc, int cb);
        [DllImport("Kernel32", EntryPoint="RtlMoveMemory", ExactSpelling=true)]
        public static extern void CopyMemory(byte[] pdst, HandleRef psrc, int cb);
        [DllImport("Kernel32", EntryPoint="RtlMoveMemory", ExactSpelling=true)]
        public static extern void CopyMemory(IntPtr pdst, string psrc, int cb);
        [DllImport("Kernel32", EntryPoint="RtlMoveMemory", CharSet=CharSet.Unicode, ExactSpelling=true)]
        public static extern void CopyMemoryW(char[] pdst, HandleRef psrc, int cb);
        [DllImport("Kernel32", EntryPoint="RtlMoveMemory", CharSet=CharSet.Unicode, ExactSpelling=true)]
        public static extern void CopyMemoryW(IntPtr pdst, string psrc, int cb);
        [DllImport("Kernel32", EntryPoint="RtlMoveMemory", CharSet=CharSet.Unicode, ExactSpelling=true)]
        public static extern void CopyMemoryW(IntPtr pdst, char[] psrc, int cb);
        [DllImport("Kernel32", EntryPoint="RtlMoveMemory", CharSet=CharSet.Unicode, ExactSpelling=true)]
        public static extern void CopyMemoryW(StringBuilder pdst, HandleRef psrc, int cb);
        public static bool Failed(int hr)
        {
            return (hr < 0);
        }

        public static string GetAbsolutePath(string fileName)
        {
            Uri uri1 = new Uri(fileName);
            return (uri1.LocalPath + uri1.Fragment);
        }

        [DllImport("user32.dll", CharSet=CharSet.Unicode)]
        public static extern int GetClientRect(IntPtr hWnd, ref RECT rect);
        [DllImport("Kernel32", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern int GetFileAttributes(string name);
        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();
        public static string GetLocalPath(string fileName)
        {
            Uri uri1 = new Uri(fileName);
            return (uri1.LocalPath + uri1.Fragment);
        }

        public static string GetLocalPathUnescaped(string url)
        {
            string text1 = "file:///";
            if (url.StartsWith(text1, StringComparison.OrdinalIgnoreCase))
            {
                return url.Substring(text1.Length);
            }
            return GetLocalPath(url);
        }

        public static IntPtr GetNativeWndProc(Control control)
        {
            IntPtr ptr1 = control.Handle;
            return GetWindowLong(new HandleRef(control, ptr1), -4);
        }

        public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, nIndex);
            }
            return GetWindowLongPtr64(hWnd, nIndex);
        }

        [DllImport("user32.dll", EntryPoint="GetWindowLong", CharSet=CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint="GetWindowLongPtr", CharSet=CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);
        [DllImport("Kernel32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern IntPtr GlobalAlloc(int uFlags, int dwBytes);
        [DllImport("Kernel32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern IntPtr GlobalFree(HandleRef handle);
        [DllImport("Kernel32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern IntPtr GlobalLock(HandleRef handle);
        [DllImport("Kernel32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern IntPtr GlobalReAlloc(HandleRef handle, int bytes, int flags);
        [DllImport("Kernel32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern int GlobalSize(HandleRef handle);
        [DllImport("Kernel32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern bool GlobalUnlock(HandleRef handle);
        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hwnd, IntPtr rect, bool erase);
        [DllImport("user32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
        public static extern bool IsDialogMessageA(IntPtr hDlg, ref Microsoft.VisualStudio.OLE.Interop.MSG msg);
        public static bool IsSamePath(string file1, string file2)
        {
            if ((file1 == null) || (file1.Length == 0))
            {
                if (file2 != null)
                {
                    return (file2.Length == 0);
                }
                return true;
            }
            Uri uri1 = new Uri(file1);
            Uri uri2 = new Uri(file2);
            if (uri1.IsFile && uri2.IsFile)
            {
                return (0 == string.Compare(uri1.LocalPath, uri2.LocalPath, StringComparison.OrdinalIgnoreCase));
            }
            return (file1 == file2);
        }

        [DllImport("User32", CharSet=CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("oleaut32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr SafeArrayCreate(VarEnum vt, uint cDims, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] SAFEARRAYBOUND[] rgsabound);
        [DllImport("oleaut32.dll", CharSet=CharSet.Auto, PreserveSig=false)]
        public static extern void SafeArrayPutElement(IntPtr psa, [MarshalAs(UnmanagedType.LPArray)] long[] rgIndices, IntPtr pv);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern void SetFocus(IntPtr hwnd);
        [DllImport("User32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);
        [DllImport("user32.dll", CharSet=CharSet.Unicode)]
        public static extern int SetWindowLong(IntPtr hWnd, short nIndex, int value);
        [DllImport("User32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);
        [DllImport("User32", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static int SignedHIWORD(int n)
        {
            return (short) ((n >> 0x10) & 0xffff);
        }

        public static int SignedLOWORD(int n)
        {
            return (short) (n & 0xffff);
        }

        public static bool Succeeded(int hr)
        {
            return (hr >= 0);
        }

        public static int ThrowOnFailure(int hr)
        {
            return ThrowOnFailure(hr, null);
        }

        public static int ThrowOnFailure(int hr, params int[] expectedHRFailure)
        {
            if (Failed(hr) && ((expectedHRFailure == null) || (Array.IndexOf<int>(expectedHRFailure, hr) < 0)))
            {
                Marshal.ThrowExceptionForHR(hr);
            }
            return hr;
        }

        [DllImport("Kernel32", CharSet=CharSet.Unicode, ExactSpelling=true)]
        public static extern int WideCharToMultiByte(int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string wideStr, int chars, [In, Out] byte[] pOutBytes, int bufferBytes, IntPtr defaultChar, IntPtr pDefaultUsed);

        public const uint ALL = 1;
        public static readonly Guid BuildOrder;
        public static readonly Guid BuildOutput;
        public const int CB_SETDROPPEDWIDTH = 0x160;
        public const uint CEF_CLONEFILE = 1;
        public const uint CEF_OPENASNEW = 8;
        public const uint CEF_OPENFILE = 2;
        public const uint CEF_SILENT = 4;
        public const ushort CF_HDROP = 15;
        public const int CLSCTX_INPROC_SERVER = 1;
        public static readonly Guid CLSID_MiscellaneousFilesProject;
        public static readonly Guid CLSID_SolutionItemsProject;
        public static readonly Guid CLSID_VsCfgProviderEventsHelper;
        public static readonly Guid CLSID_VsDocOutlinePackage;
        public static readonly Guid CLSID_VsEnvironmentPackage;
        public static readonly Guid CLSID_VsTaskList;
        public static readonly Guid CLSID_VsTaskListPackage;
        public static readonly Guid CLSID_VsUIHierarchyWindow;
        public const int cmdidToolsOptions = 0x108;
        public static readonly Guid DebugOutput;
        public const int DISP_E_ARRAYISLOCKED = -2147352563;
        public const int DISP_E_BADCALLEE = -2147352560;
        public const int DISP_E_BADINDEX = -2147352565;
        public const int DISP_E_BADPARAMCOUNT = -2147352562;
        public const int DISP_E_BADVARTYPE = -2147352568;
        public const int DISP_E_BUFFERTOOSMALL = -2147352557;
        public const int DISP_E_DIVBYZERO = -2147352558;
        public const int DISP_E_EXCEPTION = -2147352567;
        public const int DISP_E_MEMBERNOTFOUND = -2147352573;
        public const int DISP_E_NONAMEDARGS = -2147352569;
        public const int DISP_E_NOTACOLLECTION = -2147352559;
        public const int DISP_E_OVERFLOW = -2147352566;
        public const int DISP_E_PARAMNOTFOUND = -2147352572;
        public const int DISP_E_PARAMNOTOPTIONAL = -2147352561;
        public const int DISP_E_TYPEMISMATCH = -2147352571;
        public const int DISP_E_UNKNOWNINTERFACE = -2147352575;
        public const int DISP_E_UNKNOWNLCID = -2147352564;
        public const int DISP_E_UNKNOWNNAME = -2147352570;
        public const uint DocumentFrame = 2;
        public const int DWL_MSGRESULT = 0;
        public const int DWLP_MSGRESULT = 0;
        public const int E_ABORT = -2147467260;
        public const int E_ACCESSDENIED = -2147024891;
        public const int E_FAIL = -2147467259;
        public const int E_HANDLE = -2147024890;
        public const int E_INVALIDARG = -2147024809;
        public const int E_NOINTERFACE = -2147467262;
        public const int E_NOTIMPL = -2147467263;
        public const int E_OUTOFMEMORY = -2147024882;
        public const int E_PENDING = -2147483638;
        public const int E_POINTER = -2147467261;
        public const int E_UNEXPECTED = -2147418113;
        public const int FILE_ATTRIBUTE_READONLY = 1;
        public const int FW_BOLD = 700;
        public const int GMEM_DDESHARE = 0x2000;
        public const int GMEM_MOVEABLE = 2;
        public const int GMEM_ZEROINIT = 0x40;
        public static readonly Guid GUID_COMClassicPage;
        public static readonly Guid GUID_COMPlusPage;
        public static readonly Guid GUID_DefaultEditor;
        public static readonly Guid GUID_ExternalEditor;
        public static readonly Guid GUID_HtmlEditorFactory;
        public static readonly Guid GUID_ItemType_PhysicalFile;
        public static readonly Guid GUID_ItemType_PhysicalFolder;
        public static readonly Guid GUID_ItemType_SubProject;
        public static readonly Guid GUID_ItemType_VirtualFolder;
        public static readonly Guid GUID_OutWindowGeneralPane;
        public static readonly Guid GUID_SolutionPage;
        public static readonly Guid GUID_TextEditorFactory;
        public static readonly Guid GUID_VsNewProjectPseudoFolder;
        public static readonly Guid GUID_VSStandardCommandSet97;
        public static readonly Guid GUID_VsTaskListViewAll;
        public static readonly Guid GUID_VsTaskListViewCheckedTasks;
        public static readonly Guid GUID_VsTaskListViewCommentTasks;
        public static readonly Guid GUID_VsTaskListViewCompilerTasks;
        public static readonly Guid GUID_VsTaskListViewCurrentFileTasks;
        public static readonly Guid GUID_VsTaskListViewHTMLTasks;
        public static readonly Guid GUID_VsTaskListViewShortcutTasks;
        public static readonly Guid GUID_VsTaskListViewUncheckedTasks;
        public static readonly Guid GUID_VsTaskListViewUserTasks;
        public static readonly Guid GUID_VsUIHierarchyWindowCmds;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_STYLE = -16;
        public const int GWL_WNDPROC = -4;
        public const int HTMENU = 5;
        public const int IDABORT = 3;
        public const int IDCANCEL = 2;
        public const int IDCLOSE = 8;
        public const int IDCONTINUE = 11;
        public const int IDHELP = 9;
        public const int IDIGNORE = 5;
        public const int IDNO = 7;
        public const int IDOK = 1;
        public const int IDRETRY = 4;
        public const int IDTRYAGAIN = 10;
        public const int IDYES = 6;
        public const int IEI_DoNotLoadDocData = 0x10000000;
        public static readonly Guid IID_IObjectWithSite;
        public static readonly Guid IID_IServiceProvider;
        public static readonly Guid IID_IUnknown;
        public const int ILD_MASK = 0x10;
        public const int ILD_NORMAL = 0;
        public const int ILD_ROP = 0x40;
        public const int ILD_TRANSPARENT = 1;
        public static IntPtr InvalidIntPtr;
        public static readonly Guid LOGVIEWID_Any;
        public static readonly Guid LOGVIEWID_Code;
        public static readonly Guid LOGVIEWID_Debugging;
        public static readonly Guid LOGVIEWID_Designer;
        public static readonly Guid LOGVIEWID_Primary;
        public static readonly Guid LOGVIEWID_TextView;
        public static readonly Guid LOGVIEWID_UserChooseView;
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1036;
        public const int LVS_EX_LABELTIP = 0x4000;
        public const int MAX_PATH = 260;
        public const uint MK_CONTROL = 8;
        public const uint MK_SHIFT = 4;
        public const int OFN_ALLOWMULTISELECT = 0x200;
        public const int OFN_CREATEPROMPT = 0x2000;
        public const int OFN_DONTADDTORECENT = 0x2000000;
        public const int OFN_ENABLEHOOK = 0x20;
        public const int OFN_ENABLEINCLUDENOTIFY = 0x400000;
        public const int OFN_ENABLESIZING = 0x800000;
        public const int OFN_ENABLETEMPLATE = 0x40;
        public const int OFN_ENABLETEMPLATEHANDLE = 0x80;
        public const int OFN_EXPLORER = 0x80000;
        public const int OFN_EXTENSIONDIFFERENT = 0x400;
        public const int OFN_FILEMUSTEXIST = 0x1000;
        public const int OFN_FORCESHOWHIDDEN = 0x10000000;
        public const int OFN_HIDEREADONLY = 4;
        public const int OFN_LONGNAMES = 0x200000;
        public const int OFN_NOCHANGEDIR = 8;
        public const int OFN_NODEREFERENCELINKS = 0x100000;
        public const int OFN_NOLONGNAMES = 0x40000;
        public const int OFN_NONETWORKBUTTON = 0x20000;
        public const int OFN_NOREADONLYRETURN = 0x8000;
        public const int OFN_NOTESTFILECREATE = 0x10000;
        public const int OFN_NOVALIDATE = 0x100;
        public const int OFN_OVERWRITEPROMPT = 2;
        public const int OFN_PATHMUSTEXIST = 0x800;
        public const int OFN_READONLY = 1;
        public const int OFN_SHAREAWARE = 0x4000;
        public const int OFN_SHOWHELP = 0x10;
        public const int OFN_USESHELLITEM = 0x1000000;
        public const int OLE_E_ADVF = -2147221503;
        public const int OLE_E_ADVISENOTSUPPORTED = -2147221501;
        public const int OLE_E_BLANK = -2147221497;
        public const int OLE_E_CANT_BINDTOSOURCE = -2147221494;
        public const int OLE_E_CANT_GETMONIKER = -2147221495;
        public const int OLE_E_CANTCONVERT = -2147221487;
        public const int OLE_E_CLASSDIFF = -2147221496;
        public const int OLE_E_ENUM_NOMORE = -2147221502;
        public const int OLE_E_INVALIDHWND = -2147221489;
        public const int OLE_E_INVALIDRECT = -2147221491;
        public const int OLE_E_NOCACHE = -2147221498;
        public const int OLE_E_NOCONNECTION = -2147221500;
        public const int OLE_E_NOSTORAGE = -2147221486;
        public const int OLE_E_NOT_INPLACEACTIVE = -2147221488;
        public const int OLE_E_NOTRUNNING = -2147221499;
        public const int OLE_E_OLEVERB = -2147221504;
        public const int OLE_E_PROMPTSAVECANCELLED = -2147221492;
        public const int OLE_E_STATIC = -2147221493;
        public const int OLE_E_WRONGCOMPOBJ = -2147221490;
        public const int OLECLOSE_NOSAVE = 1;
        public const int OLECLOSE_PROMPTSAVE = 2;
        public const int OLECLOSE_SAVEIFDIRTY = 0;
        public const int OLECMDERR_E_NOTSUPPORTED = -2147221248;
        public const int OLECMDERR_E_UNKNOWNGROUP = -2147221244;
        public const int OLEIVERB_DISCARDUNDOSTATE = -6;
        public const int OLEIVERB_HIDE = -3;
        public const int OLEIVERB_INPLACEACTIVATE = -5;
        public const int OLEIVERB_OPEN = -2;
        public const int OLEIVERB_PRIMARY = 0;
        public const int OLEIVERB_PROPERTIES = -7;
        public const int OLEIVERB_SHOW = -1;
        public const int OLEIVERB_UIACTIVATE = -4;
        public const int OPAQUE = 2;
        public const uint PropertyBrowserSID = 4;
        public const int PSBTN_APPLYNOW = 4;
        public const int PSBTN_BACK = 0;
        public const int PSBTN_CANCEL = 5;
        public const int PSBTN_FINISH = 2;
        public const int PSBTN_HELP = 6;
        public const int PSBTN_MAX = 6;
        public const int PSBTN_NEXT = 1;
        public const int PSBTN_OK = 3;
        public const int PSH_DEFAULT = 0;
        public const int PSH_HASHELP = 0x200;
        public const int PSH_HEADER = 0x80000;
        public const int PSH_MODELESS = 0x400;
        public const int PSH_NOAPPLYNOW = 0x80;
        public const int PSH_NOCONTEXTHELP = 0x2000000;
        public const int PSH_PROPSHEETPAGE = 8;
        public const int PSH_PROPTITLE = 1;
        public const int PSH_RTLREADING = 0x800;
        public const int PSH_STRETCHWATERMARK = 0x40000;
        public const int PSH_USECALLBACK = 0x100;
        public const int PSH_USEHBMHEADER = 0x100000;
        public const int PSH_USEHBMWATERMARK = 0x10000;
        public const int PSH_USEHICON = 2;
        public const int PSH_USEHPLWATERMARK = 0x20000;
        public const int PSH_USEICONID = 4;
        public const int PSH_USEPAGELANG = 0x200000;
        public const int PSH_USEPSTARTPAGE = 0x40;
        public const int PSH_WATERMARK = 0x8000;
        public const int PSH_WIZARD = 0x20;
        public const int PSH_WIZARD_LITE = 0x400000;
        public const int PSH_WIZARDCONTEXTHELP = 0x1000;
        public const int PSH_WIZARDHASFINISH = 0x10;
        public const int PSN_APPLY = -202;
        public const int PSN_KILLACTIVE = -201;
        public const int PSN_RESET = -203;
        public const int PSN_SETACTIVE = -200;
        public const int PSNRET_INVALID = 1;
        public const int PSNRET_INVALID_NOCHANGEPAGE = 2;
        public const int PSNRET_NOERROR = 0;
        public const int PSP_DEFAULT = 0;
        public const int PSP_DLGINDIRECT = 1;
        public const int PSP_HASHELP = 0x20;
        public const int PSP_HIDEHEADER = 0x800;
        public const int PSP_PREMATURE = 0x400;
        public const int PSP_RTLREADING = 0x10;
        public const int PSP_USECALLBACK = 0x80;
        public const int PSP_USEHEADERSUBTITLE = 0x2000;
        public const int PSP_USEHEADERTITLE = 0x1000;
        public const int PSP_USEHICON = 2;
        public const int PSP_USEICONID = 4;
        public const int PSP_USEREFPARENT = 0x40;
        public const int PSP_USETITLE = 8;
        public const int ROSTATUS_NotReadOnly = 0;
        public const int ROSTATUS_ReadOnly = 1;
        public const int ROSTATUS_Unknown = -1;
        public const int S_FALSE = 1;
        public const int S_OK = 0;
        public const uint SELECTED = 2;
        public static readonly Guid SID_SUIHostCommandDispatcher;
        public static readonly Guid SID_SVsGeneralOutputWindowPane;
        public static readonly Guid SID_SVsToolboxActiveXDataProvider;
        public const uint StartupProject = 3;
        public const int SW_SHOWNORMAL = 1;
        public const int SWP_FRAMECHANGED = 0x20;
        public const int SWP_NOACTIVATE = 0x10;
        public const int SWP_NOMOVE = 2;
        public const int SWP_NOSIZE = 1;
        public const int SWP_NOZORDER = 4;
        public const int TRANSPARENT = 1;
        public const int TVM_GETEDITCONTROL = 0x110f;
        public const int TVM_SETINSERTMARK = 0x111a;
        public static readonly Guid UICONTEXT_CodeWindow;
        public static readonly Guid UICONTEXT_Debugging;
        public static readonly Guid UICONTEXT_DesignMode;
        public static readonly Guid UICONTEXT_Dragging;
        public static readonly Guid UICONTEXT_EmptySolution;
        public static readonly Guid UICONTEXT_FullScreenMode;
        public static readonly Guid UICONTEXT_NoSolution;
        public static readonly Guid UICONTEXT_SolutionBuilding;
        public static readonly Guid UICONTEXT_SolutionExists;
        public static readonly Guid UICONTEXT_SolutionHasMultipleProjects;
        public static readonly Guid UICONTEXT_SolutionHasSingleProject;
        public const int UNDO_E_CLIENTABORT = -2147205119;
        public const uint UndoManager = 0;
        public const uint UserContext = 5;
        public const int VS_E_INCOMPATIBLEDOCDATA = -2147213334;
        public const int VS_E_PACKAGENOTLOADED = -2147213343;
        public const int VS_E_PROJECTMIGRATIONFAILED = -2147213339;
        public const int VS_E_PROJECTNOTLOADED = -2147213342;
        public const int VS_E_SOLUTIONALREADYOPEN = -2147213340;
        public const int VS_E_SOLUTIONNOTOPEN = -2147213341;
        public const int VS_E_UNSUPPORTEDFORMAT = -2147213333;
        public const int VS_E_WIZARDBACKBUTTONPRESS = -2147213313;
        public const int VS_S_PROJECTFORWARDED = 0x41ff0;
        public const int VS_S_TBXMARKER = 0x41ff1;
        public const uint VSCOOKIE_NIL = 0;
        public const uint VSITEMID_NIL = uint.MaxValue;
        public const uint VSITEMID_ROOT = 0xfffffffe;
        public const uint VSITEMID_SELECTION = 0xfffffffd;
        public const int WA_ACTIVE = 1;
        public const int WA_CLICKACTIVE = 2;
        public const int WA_INACTIVE = 0;
        public const int WH_GETMESSAGE = 3;
        public const int WH_JOURNALPLAYBACK = 1;
        public const int WH_MOUSE = 7;
        public const int WHEEL_DELTA = 120;
        public const uint WindowFrame = 1;
        public const int WM_ACTIVATE = 6;
        public const int WM_ACTIVATEAPP = 0x1c;
        public const int WM_AFXFIRST = 0x360;
        public const int WM_AFXLAST = 0x37f;
        public const int WM_APP = 0x8000;
        public const int WM_ASKCBFORMATNAME = 780;
        public const int WM_CANCELJOURNAL = 0x4b;
        public const int WM_CANCELMODE = 0x1f;
        public const int WM_CAPTURECHANGED = 0x215;
        public const int WM_CHANGECBCHAIN = 0x30d;
        public const int WM_CHANGEUISTATE = 0x127;
        public const int WM_CHAR = 0x102;
        public const int WM_CHARTOITEM = 0x2f;
        public const int WM_CHILDACTIVATE = 0x22;
        public const int WM_CHOOSEFONT_GETLOGFONT = 0x401;
        public const int WM_CLEAR = 0x303;
        public const int WM_CLOSE = 0x10;
        public const int WM_COMMAND = 0x111;
        public const int WM_COMMNOTIFY = 0x44;
        public const int WM_COMPACTING = 0x41;
        public const int WM_COMPAREITEM = 0x39;
        public const int WM_CONTEXTMENU = 0x7b;
        public const int WM_COPY = 0x301;
        public const int WM_COPYDATA = 0x4a;
        public const int WM_CREATE = 1;
        public const int WM_CTLCOLOR = 0x19;
        public const int WM_CTLCOLORBTN = 0x135;
        public const int WM_CTLCOLORDLG = 310;
        public const int WM_CTLCOLOREDIT = 0x133;
        public const int WM_CTLCOLORLISTBOX = 0x134;
        public const int WM_CTLCOLORMSGBOX = 0x132;
        public const int WM_CTLCOLORSCROLLBAR = 0x137;
        public const int WM_CTLCOLORSTATIC = 0x138;
        public const int WM_CUT = 0x300;
        public const int WM_DEADCHAR = 0x103;
        public const int WM_DELETEITEM = 0x2d;
        public const int WM_DESTROY = 2;
        public const int WM_DESTROYCLIPBOARD = 0x307;
        public const int WM_DEVICECHANGE = 0x219;
        public const int WM_DEVMODECHANGE = 0x1b;
        public const int WM_DISPLAYCHANGE = 0x7e;
        public const int WM_DRAWCLIPBOARD = 0x308;
        public const int WM_DRAWITEM = 0x2b;
        public const int WM_DROPFILES = 0x233;
        public const int WM_ENABLE = 10;
        public const int WM_ENDSESSION = 0x16;
        public const int WM_ENTERIDLE = 0x121;
        public const int WM_ENTERMENULOOP = 0x211;
        public const int WM_ENTERSIZEMOVE = 0x231;
        public const int WM_ERASEBKGND = 20;
        public const int WM_EXITMENULOOP = 530;
        public const int WM_EXITSIZEMOVE = 0x232;
        public const int WM_FONTCHANGE = 0x1d;
        public const int WM_GETDLGCODE = 0x87;
        public const int WM_GETFONT = 0x31;
        public const int WM_GETHOTKEY = 0x33;
        public const int WM_GETICON = 0x7f;
        public const int WM_GETMINMAXINFO = 0x24;
        public const int WM_GETOBJECT = 0x3d;
        public const int WM_GETTEXT = 13;
        public const int WM_GETTEXTLENGTH = 14;
        public const int WM_HANDHELDFIRST = 0x358;
        public const int WM_HANDHELDLAST = 0x35f;
        public const int WM_HELP = 0x53;
        public const int WM_HOTKEY = 0x312;
        public const int WM_HSCROLL = 0x114;
        public const int WM_HSCROLLCLIPBOARD = 0x30e;
        public const int WM_ICONERASEBKGND = 0x27;
        public const int WM_IME_CHAR = 0x286;
        public const int WM_IME_COMPOSITION = 0x10f;
        public const int WM_IME_COMPOSITIONFULL = 0x284;
        public const int WM_IME_CONTROL = 0x283;
        public const int WM_IME_ENDCOMPOSITION = 270;
        public const int WM_IME_KEYDOWN = 0x290;
        public const int WM_IME_KEYLAST = 0x10f;
        public const int WM_IME_KEYUP = 0x291;
        public const int WM_IME_NOTIFY = 0x282;
        public const int WM_IME_SELECT = 0x285;
        public const int WM_IME_SETCONTEXT = 0x281;
        public const int WM_IME_STARTCOMPOSITION = 0x10d;
        public const int WM_INITDIALOG = 0x110;
        public const int WM_INITMENU = 0x116;
        public const int WM_INITMENUPOPUP = 0x117;
        public const int WM_INPUTLANGCHANGE = 0x51;
        public const int WM_INPUTLANGCHANGEREQUEST = 80;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYFIRST = 0x100;
        public const int WM_KEYLAST = 0x108;
        public const int WM_KEYUP = 0x101;
        public const int WM_KILLFOCUS = 8;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_MBUTTONDBLCLK = 0x209;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_MBUTTONUP = 520;
        public const int WM_MDIACTIVATE = 0x222;
        public const int WM_MDICASCADE = 0x227;
        public const int WM_MDICREATE = 0x220;
        public const int WM_MDIDESTROY = 0x221;
        public const int WM_MDIGETACTIVE = 0x229;
        public const int WM_MDIICONARRANGE = 0x228;
        public const int WM_MDIMAXIMIZE = 0x225;
        public const int WM_MDINEXT = 0x224;
        public const int WM_MDIREFRESHMENU = 0x234;
        public const int WM_MDIRESTORE = 0x223;
        public const int WM_MDISETMENU = 560;
        public const int WM_MDITILE = 550;
        public const int WM_MEASUREITEM = 0x2c;
        public const int WM_MENUCHAR = 0x120;
        public const int WM_MENUSELECT = 0x11f;
        public const int WM_MOUSEACTIVATE = 0x21;
        public const int WM_MOUSEFIRST = 0x200;
        public const int WM_MOUSEHOVER = 0x2a1;
        public const int WM_MOUSELAST = 0x20a;
        public const int WM_MOUSELEAVE = 0x2a3;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_MOUSEWHEEL = 0x20a;
        public const int WM_MOVE = 3;
        public const int WM_MOVING = 0x216;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCCREATE = 0x81;
        public const int WM_NCDESTROY = 130;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCLBUTTONDBLCLK = 0xa3;
        public const int WM_NCLBUTTONDOWN = 0xa1;
        public const int WM_NCLBUTTONUP = 0xa2;
        public const int WM_NCMBUTTONDBLCLK = 0xa9;
        public const int WM_NCMBUTTONDOWN = 0xa7;
        public const int WM_NCMBUTTONUP = 0xa8;
        public const int WM_NCMOUSEMOVE = 160;
        public const int WM_NCPAINT = 0x85;
        public const int WM_NCRBUTTONDBLCLK = 0xa6;
        public const int WM_NCRBUTTONDOWN = 0xa4;
        public const int WM_NCRBUTTONUP = 0xa5;
        public const int WM_NCXBUTTONDBLCLK = 0xad;
        public const int WM_NCXBUTTONDOWN = 0xab;
        public const int WM_NCXBUTTONUP = 0xac;
        public const int WM_NEXTDLGCTL = 40;
        public const int WM_NEXTMENU = 0x213;
        public const int WM_NOTIFY = 0x4e;
        public const int WM_NOTIFYFORMAT = 0x55;
        public const int WM_NULL = 0;
        public const int WM_PAINT = 15;
        public const int WM_PAINTCLIPBOARD = 0x309;
        public const int WM_PAINTICON = 0x26;
        public const int WM_PALETTECHANGED = 0x311;
        public const int WM_PALETTEISCHANGING = 0x310;
        public const int WM_PARENTNOTIFY = 0x210;
        public const int WM_PASTE = 770;
        public const int WM_PENWINFIRST = 0x380;
        public const int WM_PENWINLAST = 0x38f;
        public const int WM_POWER = 0x48;
        public const int WM_POWERBROADCAST = 0x218;
        public const int WM_PRINT = 0x317;
        public const int WM_PRINTCLIENT = 0x318;
        public const int WM_QUERYDRAGICON = 0x37;
        public const int WM_QUERYENDSESSION = 0x11;
        public const int WM_QUERYNEWPALETTE = 0x30f;
        public const int WM_QUERYOPEN = 0x13;
        public const int WM_QUERYUISTATE = 0x129;
        public const int WM_QUEUESYNC = 0x23;
        public const int WM_QUIT = 0x12;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_REFLECT = 0x2000;
        public const int WM_RENDERALLFORMATS = 0x306;
        public const int WM_RENDERFORMAT = 0x305;
        public const int WM_SETCURSOR = 0x20;
        public const int WM_SETFOCUS = 7;
        public const int WM_SETFONT = 0x30;
        public const int WM_SETHOTKEY = 50;
        public const int WM_SETICON = 0x80;
        public const int WM_SETREDRAW = 11;
        public const int WM_SETTEXT = 12;
        public const int WM_SETTINGCHANGE = 0x1a;
        public const int WM_SHOWWINDOW = 0x18;
        public const int WM_SIZE = 5;
        public const int WM_SIZECLIPBOARD = 0x30b;
        public const int WM_SIZING = 0x214;
        public const int WM_SPOOLERSTATUS = 0x2a;
        public const int WM_STYLECHANGED = 0x7d;
        public const int WM_STYLECHANGING = 0x7c;
        public const int WM_SYSCHAR = 0x106;
        public const int WM_SYSCOLORCHANGE = 0x15;
        public const int WM_SYSCOMMAND = 0x112;
        public const int WM_SYSDEADCHAR = 0x107;
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_SYSKEYUP = 0x105;
        public const int WM_TCARD = 0x52;
        public const int WM_TIMECHANGE = 30;
        public const int WM_TIMER = 0x113;
        public const int WM_UNDO = 0x304;
        public const int WM_UPDATEUISTATE = 0x128;
        public const int WM_USER = 0x400;
        public const int WM_USERCHANGED = 0x54;
        public const int WM_VKEYTOITEM = 0x2e;
        public const int WM_VSCROLL = 0x115;
        public const int WM_VSCROLLCLIPBOARD = 0x30a;
        public const int WM_WINDOWPOSCHANGED = 0x47;
        public const int WM_WINDOWPOSCHANGING = 70;
        public const int WM_WININICHANGE = 0x1a;
        public const int WM_XBUTTONDBLCLK = 0x20d;
        public const int WM_XBUTTONDOWN = 0x20b;
        public const int WM_XBUTTONUP = 0x20c;
        public const int WPF_SETMINPOSITION = 1;
        public const int WS_BORDER = 0x800000;
        public const int WS_CAPTION = 0xc00000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_CLIPCHILDREN = 0x2000000;
        public const int WS_CLIPSIBLINGS = 0x4000000;
        public const int WS_DISABLED = 0x8000000;
        public const int WS_DLGFRAME = 0x400000;
        public const int WS_EX_APPWINDOW = 0x40000;
        public const int WS_EX_CLIENTEDGE = 0x200;
        public const int WS_EX_CONTEXTHELP = 0x400;
        public const int WS_EX_CONTROLPARENT = 0x10000;
        public const int WS_EX_DLGMODALFRAME = 1;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_LEFT = 0;
        public const int WS_EX_LEFTSCROLLBAR = 0x4000;
        public const int WS_EX_MDICHILD = 0x40;
        public const int WS_EX_NOPARENTNOTIFY = 4;
        public const int WS_EX_RIGHT = 0x1000;
        public const int WS_EX_RTLREADING = 0x2000;
        public const int WS_EX_STATICEDGE = 0x20000;
        public const int WS_EX_TOOLWINDOW = 0x80;
        public const int WS_EX_TOPMOST = 8;
        public const int WS_HSCROLL = 0x100000;
        public const int WS_MAXIMIZE = 0x1000000;
        public const int WS_MAXIMIZEBOX = 0x10000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_OVERLAPPED = 0;
        public const int WS_POPUP = -2147483648;
        public const int WS_SYSMENU = 0x80000;
        public const int WS_TABSTOP = 0x10000;
        public const int WS_THICKFRAME = 0x40000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_VSCROLL = 0x200000;
        public const int WSF_VISIBLE = 1;


        public sealed class ConnectionPointCookie : IDisposable
        {
            public ConnectionPointCookie(object source, object sink, System.Type eventInterface) : this(source, sink, eventInterface, true)
            {
            }

            public ConnectionPointCookie(object source, object sink, System.Type eventInterface, bool throwException)
            {
                Exception exception1 = null;
                if (source is Microsoft.VisualStudio.OLE.Interop.IConnectionPointContainer)
                {
                    this.cpc = (Microsoft.VisualStudio.OLE.Interop.IConnectionPointContainer) source;
                    try
                    {
                        Guid guid1 = eventInterface.GUID;
                        this.cpc.FindConnectionPoint(ref guid1, out this.connectionPoint);
                    }
                    catch
                    {
                        this.connectionPoint = null;
                    }
                    if (this.connectionPoint == null)
                    {
                        exception1 = new ArgumentException();
                        goto Label_009A;
                    }
                    if ((sink == null) || !eventInterface.IsInstanceOfType(sink))
                    {
                        exception1 = new InvalidCastException();
                        goto Label_009A;
                    }
                    try
                    {
                        this.connectionPoint.Advise(sink, out this.cookie);
                        goto Label_009A;
                    }
                    catch
                    {
                        this.cookie = 0;
                        this.connectionPoint = null;
                        exception1 = new Exception();
                        goto Label_009A;
                    }
                }
                exception1 = new InvalidCastException();
            Label_009A:
                if (!throwException || ((this.connectionPoint != null) && (this.cookie != 0)))
                {
                    return;
                }
                if (exception1 == null)
                {
                    throw new ArgumentException();
                }
                throw exception1;
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    try
                    {
                        if ((this.connectionPoint != null) && (this.cookie != 0))
                        {
                            this.connectionPoint.Unadvise(this.cookie);
                        }
                    }
                    finally
                    {
                        this.cookie = 0;
                        this.connectionPoint = null;
                        this.cpc = null;
                    }
                }
            }

            ~ConnectionPointCookie()
            {
                this.Dispose(false);
            }


            private Microsoft.VisualStudio.OLE.Interop.IConnectionPoint connectionPoint;
            private uint cookie;
            private Microsoft.VisualStudio.OLE.Interop.IConnectionPointContainer cpc;
        }

        internal sealed class DataStreamFromComStream : Stream, IDisposable
        {
            public DataStreamFromComStream(Microsoft.VisualStudio.OLE.Interop.IStream comStream)
            {
                this.comStream = comStream;
            }

            private void _NotImpl(string message)
            {
                NotSupportedException exception1 = new NotSupportedException(message, new ExternalException(string.Empty, -2147467263));
                throw exception1;
            }

            protected override void Dispose(bool disposing)
            {
                try
                {
                    if (disposing && (this.comStream != null))
                    {
                        this.Flush();
                    }
                    this.comStream = null;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }

            ~DataStreamFromComStream()
            {
            }

            public override void Flush()
            {
                if (this.comStream != null)
                {
                    try
                    {
                        this.comStream.Commit(0);
                    }
                    catch
                    {
                    }
                }
            }

            public override int Read(byte[] buffer, int index, int count)
            {
                uint num1;
                byte[] buffer1 = buffer;
                if (index != 0)
                {
                    buffer1 = new byte[buffer.Length - index];
                    buffer.CopyTo(buffer1, 0);
                }
                this.comStream.Read(buffer1, (uint) count, out num1);
                if (index != 0)
                {
                    buffer1.CopyTo(buffer, index);
                }
                return (int) num1;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                LARGE_INTEGER large_integer1 = new LARGE_INTEGER();
                ULARGE_INTEGER[] ularge_integerArray1 = new ULARGE_INTEGER[] { new ULARGE_INTEGER() };
                large_integer1.QuadPart = offset;
                this.comStream.Seek(large_integer1, (uint) origin, ularge_integerArray1);
                return (long) ularge_integerArray1[0].QuadPart;
            }

            public override void SetLength(long value)
            {
                ULARGE_INTEGER ularge_integer1 = new ULARGE_INTEGER();
                ularge_integer1.QuadPart = (ulong) value;
                this.comStream.SetSize(ularge_integer1);
            }

            public override void Write(byte[] buffer, int index, int count)
            {
                if (count > 0)
                {
                    uint num1;
                    byte[] buffer1 = buffer;
                    if (index != 0)
                    {
                        buffer1 = new byte[buffer.Length - index];
                        buffer.CopyTo(buffer1, 0);
                    }
                    this.comStream.Write(buffer1, (uint) count, out num1);
                    if (num1 != count)
                    {
                        throw new IOException();
                    }
                    if (index != 0)
                    {
                        buffer1.CopyTo(buffer, index);
                    }
                }
            }


            public override bool CanRead
            {
                get
                {
                    return true;
                }
            }

            public override bool CanSeek
            {
                get
                {
                    return true;
                }
            }

            public override bool CanWrite
            {
                get
                {
                    return true;
                }
            }

            public override long Length
            {
                get
                {
                    long num1 = this.Position;
                    long num2 = this.Seek((long) 0, SeekOrigin.End);
                    this.Position = num1;
                    return (num2 - num1);
                }
            }

            public override long Position
            {
                get
                {
                    return this.Seek((long) 0, SeekOrigin.Current);
                }
                set
                {
                    this.Seek(value, SeekOrigin.Begin);
                }
            }


            private Microsoft.VisualStudio.OLE.Interop.IStream comStream;
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("23BBD58A-7C59-449b-A93C-43E59EFC080C")]
        public interface ICodeClassBase
        {
            [PreserveSig]
            int GetBaseName(out string pBaseName);
        }

        [ComImport, Guid("9BDA66AE-CA28-4e22-AA27-8A7218A0E3FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEventHandler
        {
            [PreserveSig]
            int AddHandler(string bstrEventName);
            [PreserveSig]
            int RemoveHandler(string bstrEventName);
            IVsEnumBSTR GetHandledEvents();
            bool HandlesEvent(string bstrEventName);
        }

        [ComImport, Guid("3E596484-D2E4-461a-A876-254C4F097EBB"), ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IMethodXML
        {
            void GetXML(ref string pbstrXML);
            [PreserveSig]
            int SetXML(string pszXML);
            [PreserveSig]
            int GetBodyPoint([MarshalAs(UnmanagedType.Interface)] out object bodyPoint);
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("A55CCBCC-7031-432d-B30A-A68DE7BDAD75")]
        public interface IParameterKind
        {
            void SetParameterPassingMode(NativeMethods.PARAMETER_PASSING_MODE ParamPassingMode);
            void SetParameterArrayDimensions(int uDimensions);
            int GetParameterArrayCount();
            int GetParameterArrayDimensions(int uIndex);
            int GetParameterPassingMode();
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("EA1A87AD-7BC5-4349-B3BE-CADC301F17A3")]
        public interface IVBFileCodeModelEvents
        {
            [PreserveSig]
            int StartEdit();
            [PreserveSig]
            int EndEdit();
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        public class LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
            public string lfFaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public int idFrom;
            public int code;
        }

        public sealed class OLECMDTEXT
        {
            private OLECMDTEXT()
            {
            }

            public static OLECMDTEXT.OLECMDTEXTF GetFlags(IntPtr pCmdTextInt)
            {
                Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT olecmdtext1 = (Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT) Marshal.PtrToStructure(pCmdTextInt, typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT));
                if ((olecmdtext1.cmdtextf & 1) != 0)
                {
                    return OLECMDTEXT.OLECMDTEXTF.OLECMDTEXTF_NAME;
                }
                if ((olecmdtext1.cmdtextf & 2) != 0)
                {
                    return OLECMDTEXT.OLECMDTEXTF.OLECMDTEXTF_STATUS;
                }
                return OLECMDTEXT.OLECMDTEXTF.OLECMDTEXTF_NONE;
            }

            public static string GetText(IntPtr pCmdTextInt)
            {
                Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT olecmdtext1 = (Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT) Marshal.PtrToStructure(pCmdTextInt, typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT));
                IntPtr ptr1 = Marshal.OffsetOf(typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT), "rgwz");
                if (olecmdtext1.cwActual == 0)
                {
                    return string.Empty;
                }
                char[] chArray1 = new char[olecmdtext1.cwActual - 1];
                Marshal.Copy((IntPtr) (((long) pCmdTextInt) + ((long) ptr1)), chArray1, 0, chArray1.Length);
                StringBuilder builder1 = new StringBuilder(chArray1.Length);
                builder1.Append(chArray1);
                return builder1.ToString();
            }

            public static void SetText(IntPtr pCmdTextInt, string text)
            {
                Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT olecmdtext1 = (Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT) Marshal.PtrToStructure(pCmdTextInt, typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT));
                char[] chArray1 = text.ToCharArray();
                IntPtr ptr1 = Marshal.OffsetOf(typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT), "rgwz");
                IntPtr ptr2 = Marshal.OffsetOf(typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT), "cwActual");
                int num1 = Math.Min(((int) olecmdtext1.cwBuf) - 1, chArray1.Length);
                Marshal.Copy(chArray1, 0, (IntPtr) (((long) pCmdTextInt) + ((long) ptr1)), num1);
                Marshal.WriteInt16((IntPtr) ((((long) pCmdTextInt) + ((long) ptr1)) + (num1 * 2)), 0);
                Marshal.WriteInt32((IntPtr) (((long) pCmdTextInt) + ((long) ptr2)), num1 + 1);
            }



            public enum OLECMDTEXTF
            {
                OLECMDTEXTF_NONE,
                OLECMDTEXTF_NAME,
                OLECMDTEXTF_STATUS
            }
        }

        [ComImport, Guid("5EFC7974-14BC-11CF-9B2B-00AA00573819")]
        public class OleComponentUIManager
        {
        }

        public enum PARAMETER_PASSING_MODE
        {
            cmParameterTypeIn = 1,
            cmParameterTypeInOut = 3,
            cmParameterTypeOut = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
            public POINT()
            {
            }

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rectangle r)
            {
                this.left = r.Left;
                this.top = r.Top;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

        }

        public sealed class StreamConsts
        {
            public const int LOCK_EXCLUSIVE = 2;
            public const int LOCK_ONLYONCE = 4;
            public const int LOCK_WRITE = 1;
            public const int STATFLAG_DEFAULT = 0;
            public const int STATFLAG_NONAME = 1;
            public const int STATFLAG_NOOPEN = 2;
            public const int STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4;
            public const int STGC_DEFAULT = 0;
            public const int STGC_ONLYIFCURRENT = 2;
            public const int STGC_OVERWRITE = 1;
            public const int STREAM_SEEK_CUR = 1;
            public const int STREAM_SEEK_END = 2;
            public const int STREAM_SEEK_SET = 0;
        }

        public enum tagOLECMDF
        {
            OLECMDF_ENABLED = 2,
            OLECMDF_INVISIBLE = 0x10,
            OLECMDF_LATCHED = 4,
            OLECMDF_NINCHED = 8,
            OLECMDF_SUPPORTED = 1
        }

        public enum VSSELELEMID
        {
            SEID_UndoManager,
            SEID_WindowFrame,
            SEID_DocumentFrame,
            SEID_StartupProject,
            SEID_PropertyBrowserSID,
            SEID_UserContext,
            SEID_ResultList,
            SEID_LastWindowFrame
        }

        public enum VSTASKBITMAP
        {
            BMP_COMMENT = -3,
            BMP_COMPILE = -1,
            BMP_SHORTCUT = -4,
            BMP_SQUIGGLE = -2,
            BMP_USER = -5
        }

        [ComImport, Guid("8E7B96A8-E33D-11D0-A6D5-00C04FB67F6A")]
        public class VsTextBuffer
        {
        }

        public enum VsUIHierarchyWindowCmdIds
        {
            UIHWCMDID_CancelLabelEdit = 6,
            UIHWCMDID_CommitLabelEdit = 5,
            UIHWCMDID_DoubleClick = 2,
            UIHWCMDID_EnterKey = 3,
            UIHWCMDID_RightClick = 1,
            UIHWCMDID_StartLabelEdit = 4
        }
    }
}

