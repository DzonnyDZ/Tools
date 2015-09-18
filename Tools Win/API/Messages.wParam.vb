Imports System.ComponentModel, Tools.ExtensionsT
#If True
Namespace API.Messages.wParam
        ''' <summary>Values used for wParam used by <see cref="WindowMessages.WM_ACTIVATE"/> message</summary>
        Public Enum WM_ACTIVATE As Integer
            ''' <summary>Activated by some method other than a mouse click (for example, by a call to the SetActiveWindow function or by use of the keyboard interface to select the window).</summary>
            WA_ACTIVE = 1
            ''' <summary>Activated by a mouse click.</summary>
            WA_CLICKACTIVE = 2
            ''' <summary>Deactivated.</summary>
            WA_INACTIVE = 0
        End Enum
        ''' <summary>Values for wParam used by <see cref="WindowMessages.WM_CHANGEUISTATE"/>. Those constants are used for low-order word of wParam.</summary>
        Public Enum WM_CHANGEUISTATE_low As Short
            ''' <summary>The UI state flags specified by the high-order word should be cleared.</summary>
            UIS_CLEAR = 2
            ''' <summary>The UI state flags specified by the high-order word should be changed based on the last input event. For more information, see Remarks.</summary>
            UIS_INITIALIZE = 3
            ''' <summary>The UI state flags specified by the high-order word should be set.</summary>
            UIS_SET = 1
        End Enum
        ''' <summary>Values for wParam used by <see cref="WindowMessages.WM_CHANGEUISTATE"/>. Those constants are used for high-order word of wParam.</summary>
        Public Enum WM_CHANGEUISTATE_high As Short
            ''' <summary>Keyboard accelerators are hidden.</summary>
            UISF_HIDEACCEL = &H2
            ''' <summary>Focus indicators are hidden.</summary>
            UISF_HIDEFOCUS = &H1
            ''' <summary>Windows XP: A control should be drawn in the style used for active controls.</summary>
            UISF_ACTIVE = 4
        End Enum
        ''' <summary>Values for wParam used by <see cref="WindowMessages.WM_CHANGEUISTATE"/>.</summary>
        ''' <remarks>Actual value of wParam can be or-ed UIS_* and UISF_* constant</remarks>
        Public Enum WM_CHANGEUISTATE As Integer
            ''' <summary>The UI state flags specified by the high-order word should be cleared.</summary>
            UIS_CLEAR = WM_CHANGEUISTATE_low.UIS_CLEAR
            ''' <summary>The UI state flags specified by the high-order word should be changed based on the last input event. For more information, see Remarks.</summary>
            UIS_INITIALIZE = WM_CHANGEUISTATE_low.UIS_INITIALIZE
            ''' <summary>The UI state flags specified by the high-order word should be set.</summary>
            UIS_SET = WM_CHANGEUISTATE_low.UIS_SET
            ''' <summary>''' <summary>Keyboard accelerators are hidden.</summary></summary>
            UISF_HIDEACCEL = CInt(WM_CHANGEUISTATE_high.UISF_HIDEACCEL) << 16
            ''' <summary>Focus indicators are hidden.</summary>
            UISF_HIDEFOCUS = CInt(WM_CHANGEUISTATE_high.UISF_HIDEFOCUS) << 16
            ''' <summary>Windows XP: A control should be drawn in the style used for active controls.</summary>
            UISF_ACTIVE = CInt(WM_CHANGEUISTATE_high.UISF_ACTIVE) << 16
        End Enum
        ''' <summary>Constants used for low word of wParam of <see cref="WindowMessages.WM_COMMAND"/> message</summary>
        ''' <remarks>Sorry, I haven't found any documentation for those constants</remarks>
        Public Enum WM_COMMAND_low As Short
            'TODO: Docummentation, there is some documentation related to DHTML
            IDM_1D = 2170
            IDM_ADDFAVORITES = 2261
            IDM_ADDRESS = 2189
            IDM_ADDTOGLYPHTABLE = 2337
            IDM_ALIGNBOTTOM = 1
            IDM_ALIGNHORIZONTALCENTERS = 2
            IDM_ALIGNLEFT = 3
            IDM_ALIGNRIGHT = 4
            IDM_ALIGNTOGRID = 5
            IDM_ALIGNTOP = 6
            IDM_ALIGNVERTICALCENTERS = 7
            IDM_APPLYHEADING1 = 2255
            IDM_APPLYHEADING2 = 2256
            IDM_APPLYHEADING3 = 2257
            IDM_APPLYNORMAL = 2254
            IDM_ARRANGEBOTTOM = 8
            IDM_ARRANGERIGHT = 9
            IDM_AUTODETECT = 2329
            IDM_BACKCOLOR = 51
            IDM_BASELINEFONT1 = 2141
            IDM_BASELINEFONT2 = 2142
            IDM_BASELINEFONT3 = 2143
            IDM_BASELINEFONT4 = 2144
            IDM_BASELINEFONT5 = 2145
            IDM_BLINK = 2190
            IDM_BLOCKDIRLTR = 2352
            IDM_BLOCKDIRRTL = 2353
            IDM_BLOCKFMT = 2234
            IDM_BOLD = 52
            IDM_BOOKMARK = 2123
            IDM_BORDERCOLOR = 53
            IDM_BREAKATNEXT = 2311
            IDM_BRINGFORWARD = 10
            IDM_BRINGTOFRONT = 11
            IDM_BROWSEMODE = 2126
            IDM_BUTTON = 2167
            IDM_CANCEL = 89
            IDM_CAPTIONINSERT = 2203
            IDM_CELLINSERT = 2202
            IDM_CELLMERGE = 2204
            IDM_CELLPROPERTIES = 2211
            IDM_CELLSELECT = 2206
            IDM_CELLSPLIT = 2205
            IDM_CENTERALIGNPARA = 2250
            IDM_CENTERHORIZONTALLY = 12
            IDM_CENTERVERTICALLY = 13
            IDM_CHANGECASE = 2246
            IDM_CHANGEFONT = 2240
            IDM_CHANGEFONTSIZE = 2241
            IDM_CHECKBOX = 2163
            IDM_CHISELED = 64
            IDM_CLEARSELECTION = 2007
            IDM_CODE = 14
            IDM_COLUMNINSERT = 2213
            IDM_COLUMNSELECT = 2208
            IDM_COMMENT = 2173
            IDM_COMPOSESETTINGS = 2318
            IDM_CONTEXT = 1
            IDM_CONTEXTMENU = 2280
            IDM_CONVERTOBJECT = 82
            IDM_COPY = 15
            IDM_COPYBACKGROUND = 2265
            IDM_COPYCONTENT = 2291
            IDM_COPYFORMAT = 2237
            IDM_COPYSHORTCUT = 2262
            IDM_CREATELINK = 2290
            IDM_CREATESHORTCUT = 2266
            IDM_CUSTOMCONTROL = 83
            IDM_CUSTOMIZEITEM = 84
            IDM_CUT = 16
            IDM_DECFONTSIZE = 2243
            IDM_DECFONTSIZE1PT = 2245
            IDM_DELETE = 17
            IDM_DELETEWORD = 92
            IDM_DIRLTR = 2350
            IDM_DIRRTL = 2351
            IDM_DIV = 2191
            IDM_DOCPROPERTIES = 2260
            IDM_DROPDOWNBOX = 2165
            IDM_DYNSRCPLAY = 2271
            IDM_DYNSRCSTOP = 2272
            IDM_EDITMODE = 2127
            IDM_EDITSOURCE = 2122
            IDM_EMPTYGLYPHTABLE = 2336
            IDM_ENABLE_INTERACTION = 2302
            IDM_ETCHED = 65
            IDM_EXECPRINT = 93
            IDM_FILE = 2172
            IDM_FIND = 67
            IDM_FLAT = 54
            IDM_FOLLOW_ANCHOR = 2008
            IDM_FOLLOWLINKC = 2136
            IDM_FOLLOWLINKN = 2137
            IDM_FONT = 90
            IDM_FONTNAME = 18
            IDM_FONTSIZE = 19
            IDM_FORECOLOR = 55
            IDM_FORM = 2181
            IDM_FORMATMARK = 2132
            IDM_GETBLOCKFMTS = 2233
            IDM_GETBYTESDOWNLOADED = 2331
            IDM_GETZOOM = 68
            IDM_GETZOOMDENOMINATOR = 2346
            IDM_GETZOOMNUMERATOR = 2345
            IDM_GOBACKWARD = 2282
            IDM_GOFORWARD = 2283
            IDM_GOTO = 2239
            IDM_GROUP = 20
            IDM_HELP_ABOUT = 2221
            IDM_HELP_CONTENT = 2220
            IDM_HELP_README = 2222
            IDM_HORIZONTALLINE = 2150
            IDM_HORIZSPACECONCATENATE = 21
            IDM_HORIZSPACEDECREASE = 22
            IDM_HORIZSPACEINCREASE = 23
            IDM_HORIZSPACEMAKEEQUAL = 24
            IDM_HTMLAREA = 2178
            IDM_HTMLCONTAIN = 2159
            IDM_HTMLEDITMODE = 2316
            IDM_HTMLSOURCE = 2157
            IDM_HWND = 2
            IDM_HYPERLINK = 2124
            IDM_IFRAME = 2158
            IDM_IMAGE = 2168
            IDM_IMAGEMAP = 2171
            IDM_IMGARTPLAY = 2274
            IDM_IMGARTREWIND = 2276
            IDM_IMGARTSTOP = 2275
            IDM_IMPORT = 86
            IDM_INCFONTSIZE = 2242
            IDM_INCFONTSIZE1PT = 2244
            IDM_INDENT = 2186
            IDM_INLINEDIRLTR = 2354
            IDM_INLINEDIRRTL = 2355
            IDM_INSERTOBJECT = 25
            IDM_INSERTSPAN = 2357
            IDM_INSFIELDSET = 2119
            IDM_INSINPUTBUTTON = 2115
            IDM_INSINPUTHIDDEN = 2312
            IDM_INSINPUTIMAGE = 2114
            IDM_INSINPUTPASSWORD = 2313
            IDM_INSINPUTRESET = 2116
            IDM_INSINPUTSUBMIT = 2117
            IDM_INSINPUTUPLOAD = 2118
            IDM_ISTRUSTEDDLG = 2356
            IDM_ITALIC = 56
            IDM_JAVAAPPLET = 2175
            IDM_JUSTIFYCENTER = 57
            IDM_JUSTIFYFULL = 50
            IDM_JUSTIFYGENERAL = 58
            IDM_JUSTIFYLEFT = 59
            IDM_JUSTIFYNONE = 94
            IDM_JUSTIFYRIGHT = 60
            IDM_LANGUAGE = 2292
            IDM_LAUNCHDEBUGGER = 2310
            IDM_LEFTALIGNPARA = 2251
            IDM_LINEBREAKBOTH = 2154
            IDM_LINEBREAKLEFT = 2152
            IDM_LINEBREAKNORMAL = 2151
            IDM_LINEBREAKRIGHT = 2153
            IDM_LIST = 2183
            IDM_LISTBOX = 2166
            IDM_LOCALIZEEDITOR = 2358
            IDM_MARQUEE = 2182
            IDM_MENUEXT_COUNT = 3733
            IDM_MENUEXT_FIRST__ = 3700
            IDM_MENUEXT_LAST__ = 3732
            IDM_MIMECSET__FIRST__ = 3609
            IDM_MIMECSET__LAST__ = 3699
            IDM_MOVE = 88
            IDM_MULTILEVELREDO = 30
            IDM_MULTILEVELUNDO = 44
            IDM_NEW = 2001
            IDM_NEWPAGE = 87
            IDM_NOACTIVATEDESIGNTIMECONTROLS = 2333
            IDM_NOACTIVATEJAVAAPPLETS = 2334
            IDM_NOACTIVATENORMALOLECONTROLS = 2332
            IDM_NOFIXUPURLSONPASTE = 2335
            IDM_NONBREAK = 2155
            IDM_OBJECT = 2169
            IDM_OBJECTVERBLIST0 = 72
            IDM_OBJECTVERBLIST1 = 73
            IDM_OBJECTVERBLIST2 = 74
            IDM_OBJECTVERBLIST3 = 75
            IDM_OBJECTVERBLIST4 = 76
            IDM_OBJECTVERBLIST5 = 77
            IDM_OBJECTVERBLIST6 = 78
            IDM_OBJECTVERBLIST7 = 79
            IDM_OBJECTVERBLIST8 = 80
            IDM_OBJECTVERBLIST9 = 81
            IDM_OBJECTVERBLISTLAST = IDM_OBJECTVERBLIST9
            IDM_OPEN = 2000
            IDM_OPTIONS = 2135
            IDM_ORDERLIST = 2184
            IDM_OUTDENT = 2187
            IDM_OVERWRITE = 2314
            IDM_PAGE = 2267
            IDM_PAGEBREAK = 2177
            IDM_PAGEINFO = 2231
            IDM_PAGESETUP = 2004
            IDM_PARAGRAPH = 2180
            IDM_PARSECOMPLETE = 2315
            IDM_PASTE = 26
            IDM_PASTEFORMAT = 2238
            IDM_PASTEINSERT = 2120
            IDM_PASTESPECIAL = 2006
            IDM_PERSISTSTREAMSYNC = 2341
            IDM_PLUGIN = 2176
            IDM_PREFORMATTED = 2188
            IDM_PRESTOP = 2284
            IDM_PRINT = 27
            IDM_PRINTPREVIEW = 2003
            IDM_PRINTQUERYJOBSPENDING = 2277
            IDM_PRINTTARGET = 2273
            IDM_PROPERTIES = 28
            IDM_RADIOBUTTON = 2164
            IDM_RAISED = 61
            IDM_RCINSERT = 2201
            IDM_REDO = 29
            IDM_REFRESH = 2300
            IDM_REGISTRYREFRESH = 2317
            IDM_REMOVEFORMAT = 2230
            IDM_REMOVEFROMGLYPHTABLE = 2338
            IDM_REMOVEPARAFORMAT = 2253
            IDM_RENAME = 85
            IDM_REPLACE = 2121
            IDM_REPLACEGLYPHCONTENTS = 2339
            IDM_RIGHTALIGNPARA = 2252
            IDM_ROWINSERT = 2212
            IDM_ROWSELECT = 2207
            IDM_RUNURLSCRIPT = 2343
            IDM_SAVE = 70
            IDM_SAVEAS = 71
            IDM_SAVEBACKGROUND = 2263
            IDM_SAVECOPYAS = 2002
            IDM_SAVEPICTURE = 2270
            IDM_SAVEPRETRANSFORMSOURCE = 2370
            IDM_SAVETARGET = 2268
            IDM_SCRIPT = 2174
            IDM_SCRIPTDEBUGGER = 2330
            IDM_SELECTALL = 31
            IDM_SENDBACKWARD = 32
            IDM_SENDTOBACK = 33
            IDM_SETDESKTOPITEM = 2278
            IDM_SETDIRTY = 2342
            IDM_SETWALLPAPER = 2264
            IDM_SHADOWED = 66
            IDM_SHOWALIGNEDSITETAGS = 2321
            IDM_SHOWALLTAGS = 2327
            IDM_SHOWAREATAGS = 2325
            IDM_SHOWCOMMENTTAGS = 2324
            IDM_SHOWGRID = 69
            IDM_SHOWHIDE_CODE = 2235
            IDM_SHOWMISCTAGS = 2320
            IDM_SHOWPICTURE = 2269
            IDM_SHOWSCRIPTTAGS = 2322
            IDM_SHOWSPECIALCHAR = 2249
            IDM_SHOWSTYLETAGS = 2323
            IDM_SHOWTABLE = 34
            IDM_SHOWUNKNOWNTAGS = 2326
            IDM_SHOWWBRTAGS = 2340
            IDM_SHOWZEROBORDERATDESIGNTIME = 2328
            IDM_SIZETOCONTROL = 35
            IDM_SIZETOCONTROLHEIGHT = 36
            IDM_SIZETOCONTROLWIDTH = 37
            IDM_SIZETOFIT = 38
            IDM_SIZETOGRID = 39
            IDM_SNAPTOGRID = 40
            IDM_SPECIALCHAR = 2156
            IDM_SPELL = 2005
            IDM_STATUSBAR = 2131
            IDM_STOP = 2138
            IDM_STOPDOWNLOAD = 2301
            IDM_STRIKETHROUGH = 91
            IDM_SUBSCRIPT = 2247
            IDM_SUNKEN = 62
            IDM_SUPERSCRIPT = 2248
            IDM_TABLE = 2236
            IDM_TABLEINSERT = 2200
            IDM_TABLEPROPERTIES = 2210
            IDM_TABLESELECT = 2209
            IDM_TABORDER = 41
            IDM_TELETYPE = 2232
            IDM_TEXTAREA = 2162
            IDM_TEXTBOX = 2161
            IDM_TEXTONLY = 2133
            IDM_TOOLBARS = 2130
            IDM_TOOLBOX = 42
            IDM_TRIED_ABSOLUTE_DROP_MODE = 13
            IDM_TRIED_ACTIVATEACTIVEXCONTROLS = 23
            IDM_TRIED_ACTIVATEAPPLETS = 24
            IDM_TRIED_ACTIVATEDTCS = 25
            IDM_TRIED_BACKCOLOR = 26
            IDM_TRIED_BLOCKFMT = 27
            IDM_TRIED_BOLD = 28
            IDM_TRIED_BRING_ABOVE_TEXT = 11
            IDM_TRIED_BRING_FORWARD = 9
            IDM_TRIED_BRING_TO_FRONT = 7
            IDM_TRIED_BROWSEMODE = 29
            IDM_TRIED_CONSTRAIN = 12
            IDM_TRIED_COPY = 30
            IDM_TRIED_CUT = 31
            IDM_TRIED_DELETE = 32
            IDM_TRIED_DELETECELLS = 21
            IDM_TRIED_DELETECOLS = 17
            IDM_TRIED_DELETEROWS = 16
            IDM_TRIED_DOVERB = 61
            IDM_TRIED_EDITMODE = 33
            IDM_TRIED_FIND = 34
            IDM_TRIED_FONT = 35
            IDM_TRIED_FONTNAME = 36
            IDM_TRIED_FONTSIZE = 37
            IDM_TRIED_FORECOLOR = 38
            IDM_TRIED_GETBLOCKFMTS = 39
            IDM_TRIED_HYPERLINK = 40
            IDM_TRIED_IMAGE = 41
            IDM_TRIED_INDENT = 42
            IDM_TRIED_INSERTCELL = 20
            IDM_TRIED_INSERTCOL = 15
            IDM_TRIED_INSERTROW = 14
            IDM_TRIED_INSERTTABLE = 22
            IDM_TRIED_IS_1D_ELEMENT = 0
            IDM_TRIED_IS_2D_ELEMENT = 1
            IDM_TRIED_ITALIC = 43
            IDM_TRIED_JUSTIFYCENTER = 44
            IDM_TRIED_JUSTIFYLEFT = 45
            IDM_TRIED_JUSTIFYRIGHT = 46
            IDM_TRIED_LAST_CID = IDM_TRIED_DOVERB
            IDM_TRIED_LOCK_ELEMENT = 5
            IDM_TRIED_MAKE_ABSOLUTE = 4
            IDM_TRIED_MERGECELLS = 18
            IDM_TRIED_NUDGE_ELEMENT = 2
            IDM_TRIED_ORDERLIST = 47
            IDM_TRIED_OUTDENT = 48
            IDM_TRIED_PASTE = 50
            IDM_TRIED_PRINT = 51
            IDM_TRIED_REDO = 52
            IDM_TRIED_REMOVEFORMAT = 53
            IDM_TRIED_SELECTALL = 54
            IDM_TRIED_SEND_BACKWARD = 8
            IDM_TRIED_SEND_BEHIND_1D = IDM_TRIED_SEND_BELOW_TEXT
            IDM_TRIED_SEND_BELOW_TEXT = 10
            IDM_TRIED_SEND_FORWARD = IDM_TRIED_BRING_FORWARD
            IDM_TRIED_SEND_FRONT_1D = IDM_TRIED_BRING_ABOVE_TEXT
            IDM_TRIED_SEND_TO_BACK = 6
            IDM_TRIED_SEND_TO_FRONT = IDM_TRIED_BRING_TO_FRONT
            IDM_TRIED_SET_2D_DROP_MODE = IDM_TRIED_ABSOLUTE_DROP_MODE
            IDM_TRIED_SET_ALIGNMENT = 3
            IDM_TRIED_SHOWBORDERS = 55
            IDM_TRIED_SHOWDETAILS = 56
            IDM_TRIED_SPLITCELL = 19
            IDM_TRIED_UNDERLINE = 57
            IDM_TRIED_UNDO = 58
            IDM_TRIED_UNLINK = 59
            IDM_TRIED_UNORDERLIST = 60
            IDM_UNBOOKMARK = 2128
            IDM_UNDERLINE = 63
            IDM_UNDO = 43
            IDM_UNGROUP = 45
            IDM_UNKNOWN = 0
            IDM_UNLINK = 2125
            IDM_UNORDERLIST = 2185
            IDM_VERTSPACECONCATENATE = 46
            IDM_VERTSPACEDECREASE = 47
            IDM_VERTSPACEINCREASE = 48
            IDM_VERTSPACEMAKEEQUAL = 49
            IDM_VIEWPRETRANSFORMSOURCE = 2371
            IDM_VIEWSOURCE = 2139
            IDM_ZOOMPERCENT = 50
            IDM_ZOOMPOPUP = 2140
            IDM_ZOOMRATIO = 2344
        End Enum
        ''' <summary>Constants used for wParam of <see cref="WindowMessages.WM_DEVICECHANGE"/> message</summary>
        Public Enum WM_DEVICECHANGE As Integer
            ''' <summary>A request to change the current configuration (dock or undock) has been canceled.</summary>
            DBT_CONFIGCHANGECANCELED = &H19
            ''' <summary>The current configuration has changed, due to a dock or undock.</summary>
            DBT_CONFIGCHANGED = &H18
            ''' <summary>A custom event has occurred.</summary>
            DBT_CUSTOMEVENT = &H8006
            ''' <summary>A device or piece of media has been inserted and is now available.</summary>
            DBT_DEVICEARRIVAL = &H8000
            ''' <summary>Permission is requested to remove a device or piece of media. Any application can deny this request and cancel the removal.</summary>
            DBT_DEVICEQUERYREMOVE = &H8001
            ''' <summary>A request to remove a device or piece of media has been canceled.</summary>
            DBT_DEVICEQUERYREMOVEFAILED = &H8002
            ''' <summary>A device or piece of media has been removed.</summary>
            DBT_DEVICEREMOVECOMPLETE = &H8004
            ''' <summary>A device or piece of media is about to be removed. Cannot be denied.</summary>
            DBT_DEVICEREMOVEPENDING = &H8003
            ''' <summary>A device-specific event has occurred.</summary>
            DBT_DEVICETYPESPECIFIC = &H8005
            ''' <summary>A device has been added to or removed from the system.</summary>
            DBT_DEVNODES_CHANGED = &H7
            ''' <summary>Permission is requested to change the current configuration (dock or undock).</summary>
            DBT_QUERYCHANGECONFIG = &H17
            ''' <summary>The meaning of this message is user-defined.</summary>
            DBT_USERDEFINED = &HFFFF
        End Enum
        ''' <summary>Values used by wParam of <see cref="WindowMessages.WM_ENTERIDLE"/> message</summary>
        Public Enum WM_ENTERIDLE As Integer
            ''' <summary>The system is idle because a dialog box is displayed.</summary>
            MSGF_DIALOGBOX = 0
            ''' <summary>The system is idle because a menu is displayed.</summary>
            MSGF_MENU = 2
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_GETICON"/> message</summary>
        ''' <remarks><see cref="WM_GETICON.ICON_BIG"/> and <see cref="WM_GETICON.ICON_SMALL"/> ia also used by <see cref="WindowMessages.WM_SETICON"/>'s wParam.</remarks>
        Public Enum WM_GETICON As Integer
            ''' <summary>Retrieve the large icon for the window.</summary>
            ''' <remarks><see cref="WindowMessages.WM_SETICON"/>: Set the large icon for the window.</remarks>
            ICON_BIG = 1
            ''' <summary>Retrieve the small icon for the window.</summary>
            ''' <remarks><see cref="WindowMessages.WM_SETICON"/>: Set the small icon for the window.</remarks>
            ICON_SMALL = 0
            ''' <summary>Windows XP: Retrieves the small icon provided by the application. If the application does not provide one, the system uses the system-generated icon for that window.</summary>
            ICON_SMALL2 = 2
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_HOTKEY"/> message</summary>
        Public Enum WM_HOTKEY As Integer
            ''' <summary>The "snap desktop" hot key was pressed.</summary>
            IDHOT_SNAPDESKTOP = (-2)
            ''' <summary>The "snap window" hot key was pressed.</summary>
            IDHOT_SNAPWINDOW = (-1)
        End Enum
        ''' <summary>Used for low word of wParam of <see cref="WindowMessages.WM_HSCROLL"/> message and for low word of lParam of <see cref="WindowMessages.WM_HSCROLLCLIPBOARD"/> message</summary>
        Public Enum WM_HSCROLL_low As Short
            ''' <summary>Ends scroll.</summary>
            SB_ENDSCROLL = 8
            ''' <summary>Scrolls to the upper left.</summary>
            SB_LEFT = 6
            ''' <summary>Scrolls to the lower right.</summary>
            SB_RIGHT = 7
            ''' <summary>Scrolls left by one unit.</summary>
            SB_LINELEFT = 0
            ''' <summary>Scrolls right by one unit.</summary>
            SB_LINERIGHT = 1
            ''' <summary>Scrolls left by the width of the window.</summary>
            SB_PAGELEFT = 2
            ''' <summary>Scrolls right by the width of the window.</summary>
            SB_PAGERIGHT = 3
            ''' <summary>The user has dragged the scroll box (thumb) and released the mouse button. The high-order word indicates the position of the scroll box at the end of the drag operation.</summary>
            SB_THUMBPOSITION = 4
            ''' <summary>The user is dragging the scroll box. This message is sent repeatedly until the user releases the mouse button. The high-order word indicates the position that the scroll box has been dragged to.</summary>
            SB_THUMBTRACK = 5
        End Enum
        ''' <summary>Used for low word of wParam of <see cref="WindowMessages.WM_VSCROLL"/> message and for low word of lParam of <see cref="WindowMessages.WM_VSCROLLCLIPBOARD"/> message</summary>
        Public Enum WM_VSCROLL_low As Short
            ''' <summary>Scrolls to the lower right.</summary>
            SB_BOTTOM = 7
            ''' <summary>Ends scroll.</summary>
            SB_ENDSCROLL = WM_HSCROLL_low.SB_ENDSCROLL
            ''' <summary>Scrolls one line down.</summary>
            SB_LINEDOWN = 1
            ''' <summary>Scrolls one line up.</summary>
            SB_LINEUP = 0
            ''' <summary>Scrolls one page down.</summary>
            SB_PAGEDOWN = 3
            ''' <summary>Scrolls one page up.</summary>
            SB_PAGEUP = 2
            ''' <summary>The user has dragged the scroll box (thumb) and released the mouse button. The high-order word indicates the position of the scroll box at the end of the drag operation.</summary>
            SB_THUMBPOSITION = WM_HSCROLL_low.SB_THUMBPOSITION
            ''' <summary>The user is dragging the scroll box. This message is sent repeatedly until the user releases the mouse button. The high-order word indicates the position that the scroll box has been dragged to.</summary>
            SB_THUMBTRACK = WM_HSCROLL_low.SB_THUMBTRACK
            ''' <summary>Scrolls to the upper left.</summary>
            SB_TOP = 6
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_IME_CONTROL"/> message</summary>
        Public Enum WM_IME_CONTROL As Integer
            ''' <summary>Instructs the IME window to hide the status window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776088.aspx</seealso></remarks>
            IMC_CLOSESTATUSWINDOW = &H21
            ''' <summary>Instructs an IME window to get the position of the candidate window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a CANDIDATEFORM structure that contains the position of the candidate window.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776097.aspx</seealso></remarks>
            IMC_GETCANDIDATEPOS = &H7
            ''' <summary>Instructs an IME window to retrieve the logical font used for displaying intermediate characters in the composition window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a LOGFONT structure that receives information about the logical font.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776132.aspx</seealso></remarks>
            IMC_GETCOMPOSITIONFONT = &H9
            ''' <summary>Instructs an IME window to get the position of the composition window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a COMPOSITIONFORM structure that contains the position of the composition window.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776104.aspx</seealso></remarks>
            IMC_GETCOMPOSITIONWINDOW = &HB
            ''' <summary>Instructs an IME window to get the position of the status window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>Returns a POINTS structure that contains the x coordinate and y coordinate of the status window position in screen coordinates, relative to the upper left corner of the display screen.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776079.aspx</seealso></remarks>
            IMC_GETSTATUSWINDOWPOS = &HF
            ''' <summary>Instructs the IME window to show the status window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776106.aspx</seealso></remarks>
            IMC_OPENSTATUSWINDOW = &H22
            ''' <summary>Instructs an IME window to set the position of the candidate window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a CANDIDATEFORM structure that contains the x coordinate and y coordinate for the candidate window. The application should set the dwIndex member of this structure.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776165.aspx</seealso></remarks>
            IMC_SETCANDIDATEPOS = &H8
            ''' <summary>Instructs an IME window to specify the logical font to use for displaying intermediate characters in the composition window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a LOGFONT structure that contains information about the logical font.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776168.aspx</seealso></remarks>
            IMC_SETCOMPOSITIONFONT = &HA
            ''' <summary>Instructs an IME window to set the style of the composition window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a COMPOSITIONFORM structure that contains the style information.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776180.aspx</seealso></remarks>
            IMC_SETCOMPOSITIONWINDOW = &HC
            ''' <summary>Instructs an IME window to set the position of the status window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a POINTS structure that contains the x coordinate and y coordinate of the position of the status window. The coordinates are in screen coordinates, relative to the upper left corner of the display screen.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776113.aspx</seealso></remarks>
            IMC_SETSTATUSWINDOWPOS = &H10
        End Enum
        ''' <summary>Values used for wParam of message <see cref="WindowMessages.WM_IME_NOTIFY"/></summary>
        Public Enum WM_IME_NOTIFY As Integer
            ''' <summary>Notifies the application when an IME is about to change the content of the candidate window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Candidate list flag. Each bit corresponds to a candidate list: bit 0 to the first list, bit 1 to the second list, and so on. If a specified bit is 1, the corresponding candidate window is about to be changed.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776085.aspx</seealso></remarks>
            IMN_CHANGECANDIDATE = 3
            ''' <summary>Notifies an application when an IME is about to close the candidate window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Candidate list flag. Each bit corresponds to a candidate list: bit 0 to the first list, bit 1 to the second, and so on. If a specified bit is 1, the corresponding candidate window is about to be closed.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776137.aspx</seealso></remarks>
            IMN_CLOSECANDIDATE = &H4
            ''' <summary>Notifies an application when an IME is about to close the status window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776129.aspx</seealso></remarks>
            IMN_CLOSESTATUSWINDOW = &H1
            ''' <summary>Notifies an application when an IME is about to show an error message or other information. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776142.aspx</seealso></remarks>
            IMN_GUIDELINE = &HD
            ''' <summary>Notifies an application when an IME is about to open the candidate window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Candidate list flag. Each bit corresponds to a candidate list: bit 0 to the first list, bit 1 to the second, and so on. If a specified bit is 1, the corresponding candidate window is about to be opened.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776142.aspx</seealso></remarks>
            IMN_OPENCANDIDATE = &H5
            ''' <summary>Notifies an application when an IME is about to open the candidate window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Candidate list flag. Each bit corresponds to a candidate list: bit 0 to the first list, bit 1 to the second, and so on. If a specified bit is 1, the corresponding candidate window is about to be opened.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776148.aspx</seealso></remarks>
            IMN_OPENSTATUSWINDOW = &H2
            ''' <summary>Notifies an application when an IME is about to create the status window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776166.aspx</seealso></remarks>
            IMN_SETCANDIDATEPOS = &H9
            ''' <summary>Notifies an application when the font of the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776123.aspx</seealso></remarks>
            IMN_SETCOMPOSITIONFONT = &HA
            ''' <summary>Notifies an application when the style or position of the composition window is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776098.aspx</seealso></remarks>
            IMN_SETCOMPOSITIONWINDOW = &HB
            ''' <summary>Notifies an application when the conversion mode of the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776163.aspx</seealso></remarks>
            IMN_SETCONVERSIONMODE = &H6
            ''' <summary>Notifies an application when the open status of the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776139.aspx</seealso></remarks>
            IMN_SETOPENSTATUS = &H8
            ''' <summary>Notifies an application when the sentence mode of the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso></seealso></remarks>
            IMN_SETSENTENCEMODE = &H7
            ''' <summary>Notifies an application when the status window position in the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776184.aspx</seealso></remarks>
            IMN_SETSTATUSWINDOWPOS = &HC
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_IME_REQUEST"/> message</summary>
        Public Enum WM_IME_REQUEST As Integer
            ''' <summary>Notfies an application when a selected IME needs information about the candidate window. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer containing a CANDIDATEFORM structure. Its dwIndex member contains the index to the candidate window referenced.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application fills in the CANDIDATEFORM structure. Otherwise, the command returns 0.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776080.aspx</seealso></remarks>
            IMR_CANDIDATEWINDOW = &H2
            ''' <summary>Notifies an application when a selected IME needs information about the font used by the composition window. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer containing a LOGFONT structure. The application fills in the values for the current composition window.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application fills in the LOGFONT structure. Otherwise, the command returns 0.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776149.aspx</seealso></remarks>
            IMR_COMPOSITIONFONT = &H3
            ''' <summary>Notifies an application when a selected IME needs information about the composition window. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer containing a COMPOSITIONFORM structure.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application fills in the COMPOSITIONFORM structure. Otherwise, the command returns 0.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776169.aspx</seealso></remarks>
            IMR_COMPOSITIONWINDOW = &H1
            ''' <summary>Notifies an application when the IME needs to change the RECONVERTSTRING structure. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a RECONVERTSTRING structure from the IME. For more information, see the Remarks section.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application accepts the changed RECONVERTSTRING structure. Otherwise, the command returns 0 and the IME uses the original RECONVERTSTRING structure.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776182.aspx</seealso></remarks>
            IMR_CONFIRMRECONVERTSTRING = &H5
            ''' <summary>Notifies an application when the selected IME needs the converted string from the application. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer to contain theRECONVERTSTRING structure.</description></item>
            ''' <item><term>Return value</term><description>Returns the current reconversion string structure. If lParam is set to a null pointer, the application returns the required size for the buffer to hold the structure. The command returns 0 if it does not succeed.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776164.aspx</seealso></remarks>
            IMR_DOCUMENTFEED = &H7
            ''' <summary>Notifies an application when the selected IME needs information about the coordinates of a character in the composition string. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to an IMECHARPOSITION structure that contains the position of the character in the composition window.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application fills the IMECHARPOSITION structure. Otherwise, the command returns 0.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776110.aspx</seealso></remarks>
            IMR_QUERYCHARPOSITION = &H6
            ''' <summary>Notifies an application when a selected IME needs a string for reconversion. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer containing the RECONVERTSTRING structure and strings.</description></item>
            ''' <item><term>Return value</term><description>Returns the current reconversion string structure. If lParam is set to a null pointer, the application returns the size for the buffer required to hold the structure. The command returns 0 if it does not succeed.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776135.aspx</seealso></remarks>
            IMR_RECONVERTSTRING = &H4
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_INPUTLANGCHANGEREQUEST"/> message</summary>
        <Flags()> Public Enum WM_INPUTLANGCHANGEREQUEST As Integer
            ''' <summary>Windows 2000/XP: A hot key was used to choose the previous input locale in the installed list of input locales. This flag cannot be used with the INPUTLANGCHANGE_FORWARD flag.</summary>
            INPUTLANGCHANGE_BACKWARD = &H4
            ''' <summary>Windows 2000/XP: A hot key was used to choose the next input locale in the installed list of input locales. This flag cannot be used with the INPUTLANGCHANGE_BACKWARD flag.</summary>
            INPUTLANGCHANGE_FORWARD = &H2
            ''' <summary>Windows 2000/XP:The new input locale's keyboard layout can be used with the system character set.</summary>
            INPUTLANGCHANGE_SYSCHARSET = &H1
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_LBUTTONDBLCLK"/>, <see cref="WindowMessages.WM_LBUTTONDOWN"/>, <see cref="WindowMessages.WM_LBUTTONUP"/>, <see cref="WindowMessages.WM_MBUTTONDBLCLK"/>, <see cref="WindowMessages.WM_MBUTTONDOWN"/>, <see cref="WindowMessages.WM_MBUTTONUP"/>, <see cref="WindowMessages.WM_MOUSEHOVER"/>, <see cref="WindowMessages.WM_MOUSEMOVE"/>, <see cref="WindowMessages.WM_MOUSEWHEEL"/> (low-order word as <see cref="Short"/>), <see cref="WindowMessages.WM_RBUTTONDBLCLK"/>, <see cref="WindowMessages.WM_RBUTTONDOWN"/>, <see cref="WindowMessages.WM_RBUTTONUP"/></summary>
        <Flags()> Public Enum WM_LBUTTONDBLCLK As Integer
            ''' <summary>The CTRL key is down.</summary>
            MK_CONTROL = &H8
            ''' <summary>The left mouse button is down.</summary>
            MK_LBUTTON = &H1
            ''' <summary>The middle mouse button is down.</summary>
            MK_MBUTTON = &H10
            ''' <summary>The right mouse button is down.</summary>
            MK_RBUTTON = &H2
            ''' <summary>The SHIFT key is down.</summary>
            MK_SHIFT = &H4
            ''' <summary>Windows 2000/XP: The first X button is down.</summary>
            MK_XBUTTON1 = &H20
            ''' <summary>Windows 2000/XP: The second X button is down.</summary>
            MK_XBUTTON2 = &H40
        End Enum
        ''' <summary>Values used fro wParam as <see cref="WindowMessages.WM_MDICASCADE"/></summary>
        <Flags()> Public Enum WM_MDICASCADE As Integer
            ''' <summary>Prevents disabled MDI child windows from being cascaded.</summary>
            MDITILE_SKIPDISABLED = &H2
            ''' <summary>Windows 2000/XP: Arranges the windows in Z order.</summary>
            MDITILE_ZORDER = &H4
        End Enum
        ''' <summary>Values for wParam of <see cref="WindowMessages.WM_MDITILE"/> message</summary>
        Public Enum WM_MDITILE As Integer
            ''' <summary>Tiles windows horizontally.</summary>
            MDITILE_HORIZONTAL = &H1
            ''' <summary>Tiles windows vertically.</summary>
            MDITILE_VERTICAL = &H0
            ''' <summary>prevent disabled MDI child windows from being tiled</summary>
            MDITILE_SKIPDISABLED = &H2
        End Enum
        ''' <summary>Values used for high word of wParam of the <see cref="WindowMessages.WM_MENUCHAR"/> message</summary>
        Public Enum WM_MENUCHAR_high As Short
            ''' <summary>A drop-down menu, submenu, or shortcut menu.</summary>
            MF_POPUP = &H10S
            ''' <summary>The window menu.</summary>
            MF_SYSMENU = &H2000S
        End Enum
        ''' <summary>Values used for high word of wParam of the <see cref="WindowMessages.WM_MENUSELECT"/> message</summary>
        <Flags(), CLSCompliant(False)> Public Enum WM_MENUSELECT_high As UShort
            ''' <summary>Item displays a bitmap.</summary>
            MF_BITMAP = &H4US
            ''' <summary></summary>
            MF_CHECKED = &H8US
            ''' <summary>Item is disabled.</summary>
            MF_DISABLED = &H2US
            ''' <summary>Item is grayed.</summary>
            MF_GRAYED = &H1US
            ''' <summary>Item is highlighted.</summary>
            MF_HILITE = &H80US
            ''' <summary>Item is selected with the mouse.</summary>
            MF_MOUSESELECT = &H8000US
            ''' <summary>    Item is an owner-drawn item.</summary>
            MF_OWNERDRAW = &H100US
            ''' <summary>Item is contained in the window menu. The lParam parameter contains a handle to the menu associated with the message.</summary>
            MF_POPUP = &H10US
            ''' <summary>Item is contained in the window menu. The lParam parameter contains a handle to the menu associated with the message.</summary>
            MF_SYSMENU = &H2000US
        End Enum
        ''' <summary>Values used for low word of wParam of the <see cref="WindowMessages.WM_PARENTNOTIFY"/> message</summary>
        Public Enum WM_PARENTNOTIFY_low As Short
            ''' <summary>The child window is being created.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>High-order word of wParam</term><description>Identifier of the child window.</description></item>
            ''' <item><term>lParam</term><description>Handle of the child window.</description></item>
            ''' </list></remarks>
            WM_CREATE = WindowMessages.WM_CREATE
            ''' <summary>The child window is being destroyed.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>High-order word of wParam</term><description>Identifier of the child window.</description></item>
            ''' <item><term>lParam</term><description>Handle of the child window.</description></item>
            ''' </list></remarks>
            WM_DESTROY = WindowMessages.WM_DESTROY
            ''' <summary>The user has placed the cursor over the child window and has clicked the left mouse button.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>High-order word of wParam</term><description>Undefined.</description></item>
            ''' <item><term>lParam</term><description>The x-coordinate of the cursor is the low-order word, and the y-coordinate of the cursor is the high-order word.</description></item>
            ''' </list></remarks>
            WM_LBUTTONDOWN = WindowMessages.WM_LBUTTONDOWN
            ''' <summary>The user has placed the cursor over the child window and has clicked the middle mouse button.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>High-order word of wParam</term><description>Undefined.</description></item>
            ''' <item><term>lParam</term><description>The x-coordinate of the cursor is the low-order word, and the y-coordinate of the cursor is the high-order word.</description></item>
            ''' </list></remarks>
            WM_MBUTTONDOWN = WindowMessages.WM_MBUTTONDOWN
            ''' <summary>The user has placed the cursor over the child window and has clicked the right mouse button.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>High-order word of wParam</term><description>Undefined.</description></item>
            ''' <item><term>lParam</term><description>The x-coordinate of the cursor is the low-order word, and the y-coordinate of the cursor is the high-order word.</description></item>
            ''' </list></remarks>
            WM_RBUTTONDOWN = WindowMessages.WM_RBUTTONDOWN
            ''' <summary>Windows 2000/XP: The user has placed the cursor over the child window and has clicked the first or second X button.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>High-order word of wParam</term><description>Windows 2000/XP: Indicates which button was pressed. This parameter can be one of the <see cref="wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high"/> values.</description></item>
            ''' <item><term>lParam</term><description>The x-coordinate of the cursor is the low-order word, and the y-coordinate of the cursor is the high-order word.</description></item>
            ''' </list></remarks>
            WM_XBUTTONDOWN = WindowMessages.WM_XBUTTONDOWN
        End Enum
        ''' <summary>Values used for high word of wParam of <see cref="WindowMessages.WM_PARENTNOTIFY"/> when low word is <see cref="WM_PARENTNOTIFY_low.WM_XBUTTONDOWN"/> and for high-order word of wParam of <see cref="WindowMessages.WM_XBUTTONDBLCLK"/>, <see cref="WindowMessages.WM_XBUTTONDOWN"/> and <see cref="WindowMessages.WM_XBUTTONUP"/></summary>
        Public Enum WM_PARENTNOTIFY_WM_XBUTTONDOWN_high As Short
            ''' <summary>X-button 1</summary>
            XBUTTON1 = &H1
            ''' <summary>X-button 2</summary>
            XBUTTON2 = &H2
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_POWER"/> message</summary>
        Public Enum WM_POWER As Integer
            ''' <summary>Indicates that the system is resuming operation after entering suspended mode without first broadcasting a PWR_SUSPENDREQUEST notification message to the application. An application should perform any necessary recovery actions.</summary>
            PWR_CRITICALRESUME = 3
            ''' <summary>Indicates that the system is about to enter suspended mode.</summary>
            PWR_SUSPENDREQUEST = 1
            ''' <summary>Indicates that the system is resuming operation after having entered suspended mode normally—that is, the system broadcast a PWR_SUSPENDREQUEST notification message to the application before the system was suspended. An application should perform any necessary recovery actions.</summary>
            PWR_SUSPENDRESUME = 2
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_POWERBROADCAST"/> message</summary>
        Public Enum WM_POWERBROADCAST As Integer
            ''' <summary>Battery power is low. In Windows Server 2008 and Windows Vista, use <see cref="PBT_APMPOWERSTATUSCHANGE"/> instead.</summary>
            PBT_APMBATTERYLOW = &H9
            ''' <summary>Used in Windows Server 2008 and Windows Vista instead of <see cref="PBT_APMBATTERYLOW"/></summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> PBT_APMPOWERSTATUSCHANGE = &HA
            ''' <summary>OEM-defined event occurred. In Windows Server 2008 and Windows Vista, this event is not available because these operating systems support only ACPI; APM BIOS events are not supported.</summary>
            PBT_APMOEMEVENT = &HB
            ''' <summary>Request for permission to suspend. In Windows Server 2008 and Windows Vista, use the SetThreadExecutionState function instead.</summary>
            PBT_APMQUERYSUSPEND = &H0
            ''' <summary>Suspension request denied. In Windows Server 2008 and Windows Vista, use SetThreadExecutionState instead.</summary>
            PBT_APMQUERYSUSPENDFAILED = &H2
            ''' <summary>Operation resuming after critical suspension. In Windows Server 2008 and Windows Vista, use <see cref="PBT_APMRESUMEAUTOMATIC"/> instead.</summary>
            PBT_APMRESUMECRITICAL = &H6
            ''' <summary>Used in Windows Server 2008 and Windows Vista instead of <see cref="PBT_APMRESUMECRITICAL"/></summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> PBT_APMRESUMEAUTOMATIC = &H12
            ''' <summary>If the wParam parameter is <see cref="wParam.WM_POWERBROADCAST.PBT_POWERSETTINGCHANGE"/>, the lParam parameter is a pointer to a POWERBROADCAST_SETTING structure.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> PBT_POWERSETTINGCHANGE = &H8013
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WM_SIZE"/> message</summary>
        Public Enum WM_SIZE As Integer
            ''' <summary>Message is sent to all pop-up windows when some other window is maximized.</summary>
            SIZE_MAXHIDE = 4
            ''' <summary>The window has been maximized.</summary>
            SIZE_MAXIMIZED = 2
            ''' <summary>Message is sent to all pop-up windows when some other window has been restored to its former size.</summary>
            SIZE_MAXSHOW = 3
            ''' <summary>The window has been minimized.</summary>
            SIZE_MINIMIZED = 1
            ''' <summary>The window has been resized, but neither the SIZE_MINIMIZED nor SIZE_MAXIMIZED value applies.</summary>
            SIZE_RESTORED = 0
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_SIZING"/> message</summary>
        Public Enum WM_SIZING As Integer
            ''' <summary>Bottom edge</summary>
            WMSZ_BOTTOM = 6
            ''' <summary>Bottom-left corner</summary>
            WMSZ_BOTTOMLEFT = 7
            ''' <summary>Bottom-right corner</summary>
            WMSZ_BOTTOMRIGHT = 8
            ''' <summary>Left edge</summary>
            WMSZ_LEFT = 1
            ''' <summary>Right edge</summary>
            WMSZ_RIGHT = 2
            ''' <summary>Top edge</summary>
            WMSZ_TOP = 3
            ''' <summary>Top-left corner</summary>
            WMSZ_TOPLEFT = 4
            ''' <summary>Top-right corner</summary>
            WMSZ_TOPRIGHT = 5
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_STYLECHANGED"/> message</summary>
        <Flags()> Public Enum WM_STYLECHANGED As Integer
            ''' <summary>The extended window styles have changed.</summary>
            GWL_EXSTYLE = -20
            ''' <summary>The window styles have changed.</summary>
            GWL_STYLE = -16
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_SYSCOMMAND"/> message</summary>
        Public Enum WM_SYSCOMMAND As Integer
            ''' <summary>Closes the window.</summary>
            SC_CLOSE = &HF060I
            ''' <summary>Changes the cursor to a question mark with a pointer. If the user then clicks a control in the dialog box, the control receives a WM_HELP message.</summary>
            SC_CONTEXTHELP = &HF180
            ''' <summary>Activates the window associated with the application-specified hot key. The lParam parameter identifies the window to activate.</summary>
            SC_DEFAULT = &HF160
            ''' <summary>Activates the window associated with the application-specified hot key. The lParam parameter identifies the window to activate.</summary>
            SC_HOTKEY = &HF150
            ''' <summary>Scrolls horizontally.</summary>
            SC_HSCROLL = &HF080
            ''' <summary>Retrieves the window menu as a result of a keystroke. For more information, see the Remarks section.</summary>
            SC_KEYMENU = &HF100
            ''' <summary>Maximizes the window.</summary>
            SC_MAXIMIZE = &HF030I
            ''' <summary>Minimizes the window.</summary>
            SC_MINIMIZE = &HF020I
            ''' <summary>    Sets the state of the display. This command supports devices that have power-saving features, such as a battery-powered personal computer.</summary>
            ''' <remarks>The lParam parameter can have the following values:
            ''' <list type="table">
            ''' <item><term>1</term><description>the display is going to low power</description></item>
            ''' <item><term>2</term><description> the display is being shut off</description></item>
            ''' </list></remarks>
            SC_MONITORPOWER = &HF170
            ''' <summary>Retrieves the window menu as a result of a mouse click.</summary>
            SC_MOUSEMENU = &HF090
            ''' <summary>Moves the window.</summary>
            SC_MOVE = &HF010
            ''' <summary>Moves to the next window.</summary>
            SC_NEXTWINDOW = &HF040
            ''' <summary>Moves to the previous window.</summary>
            SC_PREVWINDOW = &HF050
            ''' <summary>Restores the window to its normal position and size.</summary>
            SC_RESTORE = &HF120
            ''' <summary>Executes the screen saver application specified in the [boot] section of the System.ini file.</summary>
            SC_SCREENSAVE = &HF140
            ''' <summary>Sizes the window.</summary>
            SC_SIZE = &HF000
            ''' <summary>Activates the Start menu.</summary>
            SC_TASKLIST = &HF130
            ''' <summary>Scrolls vertically.</summary>
            SC_VSCROLL = &HF070
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_TCARD"/> message</summary>
        Public Enum WM_TCARD As Integer
            ''' <summary>The user clicked an authorable Abort button.</summary>
            IDABORT = 3
            ''' <summary>The user clicked an authorable Cancel button.</summary>
            IDCANCEL = 2
            ''' <summary>The user closed the training card.</summary>
            IDCLOSE = 8
            ''' <summary>The user clicked an authorable Windows Help button.</summary>
            IDHELP = 9
            ''' <summary>The user clicked an authorable Ignore button.</summary>
            IDIGNORE = 5
            ''' <summary>The user clicked an authorable OK button.</summary>
            IDOK = 1
            ''' <summary>The user clicked an authorable No button.</summary>
            IDNO = 7
            ''' <summary>The user clicked an authorable Retry button.</summary>
            IDRETRY = 4
            ''' <summary>The user clicked an authorable button. The dwActionData parameter contains a long integer specified by the Help author.</summary>
            HELP_TCARD_DATA = &H10
            'ASAP: Find values of this constants!!!
            ''' <summary>The user clicked an authorable Next button.</summary>
            <Obsolete("I haven't found value of this constant. Do not use it!", True)> _
            <EditorBrowsable(EditorBrowsableState.Never)> HELP_TCARD_NEXT = Integer.MinValue
            ''' <summary>Another application has requested training cards.</summary>
            HELP_TCARD_OTHER_CALLER = &H11
            ''' <summary>The user clicked an authorable Yes button.</summary>
            IDYES = 6
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_INPUT"/> message</summary>
        Public Enum WM_INPUT
            ''' <summary>Input occurred while the application was in the foreground. The application must call DefWindowProc so the system can perform cleanup.</summary>
            RIM_INPUT = 0
            ''' <summary>Input occurred while the application was not in the foreground. The application must call DefWindowProc so the system can perform the cleanup.</summary>
            RIM_INPUTSINK = 1
        End Enum
    End Namespace
#End If