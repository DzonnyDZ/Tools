'http://www.twain.org/devfiles/twain.h
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=2)> _
Friend Structure TW_VERSION
    Public MajorNum As Int16
    Public MinorNum As Int16
    Public Language As Int16
    Public Country As Int16
    <MarshalAs(UnmanagedType.ByValTStr, sizeconst:=34)> Public Info As String
End Structure

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=2)> _
Friend Class TW_IDENTITY
    Public Id As IntPtr
    Public Version As TW_VERSION
    Public ProtocolMajor As Int16
    Public ProtocolMinor As Int16
    Public SupportedGroups As Int32
    <MarshalAs(UnmanagedType.ByValTStr, sizeconst:=34)> Public Manufacturer As String
    <MarshalAs(UnmanagedType.ByValTStr, sizeconst:=34)> Public ProductFamily As String
    <MarshalAs(UnmanagedType.ByValTStr, sizeconst:=34)> Public ProductName As String
End Class


'         /* Fixed point structure type. */
'typedef struct {
'    TW_INT16     Whole;        /* maintains the sign */
'    TW_UINT16    Frac;
'} TW_FIX32,  FAR *pTW_FIX32;



'/* No DAT needed. */
'typedef struct {
'   TW_FIX32   X;
'   TW_FIX32   Y;
'   TW_FIX32   Z;
'} TW_CIEPOINT, FAR * pTW_CIEPOINT;

'/* No DAT needed. */
'typedef struct {
'   TW_FIX32   StartIn;
'   TW_FIX32   BreakIn;
'   TW_FIX32   EndIn;
'   TW_FIX32   StartOut;
'   TW_FIX32   BreakOut;
'   TW_FIX32   EndOut;
'   TW_FIX32   Gamma;
'   TW_FIX32   SampleCount;  /* if =0 use the gamma */
'} TW_DECODEFUNCTION, FAR * pTW_DECODEFUNCTION;

'/* No DAT needed. */
'typedef struct {
'   TW_UINT8    Index;    /* Value used to index into the color table. */
'   TW_UINT8    Channel1; /* First  tri-stimulus value (e.g Red)       */
'   TW_UINT8    Channel2; /* Second tri-stimulus value (e.g Green)     */
'   TW_UINT8    Channel3; /* Third  tri-stimulus value (e.g Blue)      */
'} TW_ELEMENT8, FAR * pTW_ELEMENT8;

'/* No DAT.  Defines a frame rectangle in ICAP_UNITS coordinates. */
'typedef struct {
'   TW_FIX32   Left;
'   TW_FIX32   Top;
'   TW_FIX32   Right;
'   TW_FIX32   Bottom;
'} TW_FRAME, FAR * pTW_FRAME;

'/* No DAT needed.  Used to manage memory buffers. */
'typedef struct {
'   TW_UINT32  Flags;  /* Any combination of the TWMF_ constants.           */
'   TW_UINT32  Length; /* Number of bytes stored in buffer TheMem.          */
'   TW_MEMREF  TheMem; /* Pointer or handle to the allocated memory buffer. */
'} TW_MEMORY, FAR * pTW_MEMORY;

'/* No DAT needed. */
'typedef struct {
'   TW_DECODEFUNCTION   Decode[3];
'   TW_FIX32            Mix[3][3];
'} TW_TRANSFORMSTAGE, FAR * pTW_TRANSFORMSTAGE;


'/* TWON_ARRAY. Container for array of values (a simplified TW_ENUMERATION) */
'typedef struct {
'   TW_UINT16  ItemType;
'   TW_UINT32  NumItems;    /* How many items in ItemList           */
'   TW_UINT8   ItemList[1]; /* Array of ItemType values starts here */
'} TW_ARRAY, FAR * pTW_ARRAY;

'/* TWON_ENUMERATION. Container for a collection of values. */
'typedef struct {
'   TW_UINT16  ItemType;
'   TW_UINT32  NumItems;     /* How many items in ItemList                 */
'   TW_UINT32  CurrentIndex; /* Current value is in ItemList[CurrentIndex] */
'   TW_UINT32  DefaultIndex; /* Powerup value is in ItemList[DefaultIndex] */
'   TW_UINT8   ItemList[1];  /* Array of ItemType values starts here       */
'} TW_ENUMERATION, FAR * pTW_ENUMERATION;

'/* TWON_ONEVALUE. Container for one value. */
'typedef struct {
'   TW_UINT16  ItemType;
'   TW_UINT32  Item;
'} TW_ONEVALUE, FAR * pTW_ONEVALUE;

'/* TWON_RANGE. Container for a range of values. */
'typedef struct {
'   TW_UINT16  ItemType;
'   TW_UINT32  MinValue;     /* Starting value in the range.           */
'   TW_UINT32  MaxValue;     /* Final value in the range.              */
'   TW_UINT32  StepSize;     /* Increment from MinValue to MaxValue.   */
'   TW_UINT32  DefaultValue; /* Power-up value.                        */
'   TW_UINT32  CurrentValue; /* The value that is currently in effect. */
'} TW_RANGE, FAR * pTW_RANGE;

'/* DAT_CAPABILITY. Used by application to get/set capability from/in a data source. */
'typedef struct {
'   TW_UINT16  Cap; /* id of capability to set or get, e.g. CAP_BRIGHTNESS */
'   TW_UINT16  ConType; /* TWON_ONEVALUE, _RANGE, _ENUMERATION or _ARRAY   */
'   TW_HANDLE  hContainer; /* Handle to container of type Dat              */
'} TW_CAPABILITY, FAR * pTW_CAPABILITY;

'/* DAT_CIECOLOR. */
'typedef struct {
'   TW_UINT16           ColorSpace;
'   TW_INT16            LowEndian;
'   TW_INT16            DeviceDependent;
'   TW_INT32            VersionNumber;
'   TW_TRANSFORMSTAGE   StageABC;
'   TW_TRANSFORMSTAGE   StageLMN;
'   TW_CIEPOINT         WhitePoint;
'   TW_CIEPOINT         BlackPoint;
'   TW_CIEPOINT         WhitePaper;
'   TW_CIEPOINT         BlackInk;
'   TW_FIX32            Samples[1];
'} TW_CIECOLOR, FAR * pTW_CIECOLOR;

'/* DAT_EVENT. For passing events down from the application to the DS. */
'typedef struct {
'   TW_MEMREF  pEvent;    /* Windows pMSG or Mac pEvent.                 */
'   TW_UINT16  TWMessage; /* TW msg from data source, e.g. MSG_XFERREADY */
'} TW_EVENT, FAR * pTW_EVENT;

'/* DAT_GRAYRESPONSE */
'typedef struct {
'   TW_ELEMENT8         Response[1];
'} TW_GRAYRESPONSE, FAR * pTW_GRAYRESPONSE;


'/* DAT_IMAGEINFO. Application gets detailed image info from DS with this. */
'typedef struct {
'   TW_FIX32   XResolution;      /* Resolution in the horizontal             */
'   TW_FIX32   YResolution;      /* Resolution in the vertical               */
'   TW_INT32   ImageWidth;       /* Columns in the image, -1 if unknown by DS*/
'   TW_INT32   ImageLength;      /* Rows in the image, -1 if unknown by DS   */
'   TW_INT16   SamplesPerPixel;  /* Number of samples per pixel, 3 for RGB   */
'   TW_INT16   BitsPerSample[8]; /* Number of bits for each sample           */
'   TW_INT16   BitsPerPixel;     /* Number of bits for each padded pixel     */
'   TW_BOOL    Planar;           /* True if Planar, False if chunky          */
'   TW_INT16   PixelType;        /* How to interp data; photo interp (TWPT_) */
'   TW_UINT16  Compression;      /* How the data is compressed (TWCP_xxxx)   */
'} TW_IMAGEINFO, FAR * pTW_IMAGEINFO;

'/* DAT_IMAGELAYOUT. Provides image layout information in current units. */
'typedef struct {
'   TW_FRAME   Frame;          /* Frame coords within larger document */
'   TW_UINT32  DocumentNumber;
'   TW_UINT32  PageNumber;     /* Reset when you go to next document  */
'   TW_UINT32  FrameNumber;    /* Reset when you go to next page      */
'} TW_IMAGELAYOUT, FAR * pTW_IMAGELAYOUT;

'/* DAT_IMAGEMEMXFER. Used to pass image data (e.g. in strips) from DS to application.*/
'typedef struct {
'   TW_UINT16  Compression;  /* How the data is compressed                */
'   TW_UINT32  BytesPerRow;  /* Number of bytes in a row of data          */
'   TW_UINT32  Columns;      /* How many columns                          */
'   TW_UINT32  Rows;         /* How many rows                             */
'   TW_UINT32  XOffset;      /* How far from the side of the image        */
'   TW_UINT32  YOffset;      /* How far from the top of the image         */
'   TW_UINT32  BytesWritten; /* How many bytes written in Memory          */
'   TW_MEMORY  Memory;       /* Mem struct used to pass actual image data */
'} TW_IMAGEMEMXFER, FAR * pTW_IMAGEMEMXFER;

'/* Changed in 1.1: QuantTable, HuffmanDC, HuffmanAC TW_MEMREF -> TW_MEMORY  */
'/* DAT_JPEGCOMPRESSION. Based on JPEG Draft International Std, ver 10918-1. */
'typedef struct {
'   TW_UINT16   ColorSpace;       /* One of the TWPT_xxxx values                */
'   TW_UINT32   SubSampling;      /* Two word "array" for subsampling values    */
'   TW_UINT16   NumComponents;    /* Number of color components in image        */
'   TW_UINT16   RestartFrequency; /* Frequency of restart marker codes in MDU's */
'   TW_UINT16   QuantMap[4];      /* Mapping of components to QuantTables       */
'   TW_MEMORY   QuantTable[4];    /* Quantization tables                        */
'   TW_UINT16   HuffmanMap[4];    /* Mapping of components to Huffman tables    */
'   TW_MEMORY   HuffmanDC[2];     /* DC Huffman tables                          */
'   TW_MEMORY   HuffmanAC[2];     /* AC Huffman tables                          */
'} TW_JPEGCOMPRESSION, FAR * pTW_JPEGCOMPRESSION;

'/* DAT_PALETTE8. Color palette when TWPT_PALETTE pixels xfer'd in mem buf. */
'typedef struct {
'   TW_UINT16    NumColors;   /* Number of colors in the color table.  */
'   TW_UINT16    PaletteType; /* TWPA_xxxx, specifies type of palette. */
'   TW_ELEMENT8  Colors[256]; /* Array of palette values starts here.  */
'} TW_PALETTE8, FAR * pTW_PALETTE8;

'/* DAT_PENDINGXFERS. Used with MSG_ENDXFER to indicate additional data. */
'typedef struct {
'   TW_UINT16 Count;
'   union {
'      TW_UINT32 EOJ;
'      TW_UINT32 Reserved;
'   };
'} TW_PENDINGXFERS, FAR *pTW_PENDINGXFERS;

'/* DAT_RGBRESPONSE */
'typedef struct {
'   TW_ELEMENT8         Response[1];
'} TW_RGBRESPONSE, FAR * pTW_RGBRESPONSE;

'/* DAT_SETUPFILEXFER. Sets up DS to application data transfer via a file. */
'typedef struct {
'   TW_STR255 FileName;
'   TW_UINT16 Format;   /* Any TWFF_ constant */
'   TW_INT16  VRefNum;  /* Used for Mac only  */
'} TW_SETUPFILEXFER, FAR * pTW_SETUPFILEXFER;

'/* DAT_SETUPFILEXFER2. Sets up DS to application data transfer via a file. */
'/* Added 1.9                                                               */
'typedef struct {
'   TW_MEMREF FileName;     /* Pointer to file name text */
'   TW_UINT16 FileNameType; /* TWTY_STR1024 or TWTY_UNI512 */
'   TW_UINT16 Format;       /* Any TWFF_ constant */
'   TW_INT16  VRefNum;      /* Used for Mac only  */
'   TW_UINT32 parID;        /* Used for Mac only */
'} TW_SETUPFILEXFER2, FAR * pTW_SETUPFILEXFER2;

'/* DAT_SETUPMEMXFER. Sets up DS to application data transfer via a memory buffer. */
'typedef struct {
'   TW_UINT32 MinBufSize;
'   TW_UINT32 MaxBufSize;
'   TW_UINT32 Preferred;
'} TW_SETUPMEMXFER, FAR * pTW_SETUPMEMXFER;

'/* DAT_STATUS. Application gets detailed status info from a data source with this. */
'typedef struct {
'   TW_UINT16  ConditionCode; /* Any TWCC_ constant     */
'   TW_UINT16  Reserved;      /* Future expansion space */
'} TW_STATUS, FAR * pTW_STATUS;

'/* DAT_USERINTERFACE. Coordinates UI between application and data source. */
'typedef struct {
'   TW_BOOL    ShowUI;  /* TRUE if DS should bring up its UI           */
'   TW_BOOL    ModalUI; /* For Mac only - true if the DS's UI is modal */
'   TW_HANDLE  hParent; /* For windows only - Application window handle        */
'} TW_USERINTERFACE, FAR * pTW_USERINTERFACE;

'/* SDH - 03/21/95 - TWUNK */
'/* DAT_TWUNKIDENTITY. Provides DS identity and 'other' information necessary */
'/*                    across thunk link. */
'typedef struct {
'   TW_IDENTITY identity;        /* Identity of data source.                 */
'   TW_STR255   dsPath;          /* Full path and file name of data source.  */
'} TW_TWUNKIDENTITY, FAR * pTW_TWUNKIDENTITY;

'/* SDH - 03/21/95 - TWUNK */
'/* Provides DS_Entry parameters over thunk link. */
'typedef struct
'{
'    TW_INT8     destFlag;       /* TRUE if dest is not NULL                 */
'    TW_IDENTITY dest;           /* Identity of data source (if used)        */
'    TW_INT32    dataGroup;      /* DSM_Entry dataGroup parameter            */
'    TW_INT16    dataArgType;    /* DSM_Entry dataArgType parameter          */
'    TW_INT16    message;        /* DSM_Entry message parameter              */
'    TW_INT32    pDataSize;      /* Size of pData (0 if NULL)                */
'    //  TW_MEMREF   pData;      /* Based on implementation specifics, a     */
'                                /* pData parameter makes no sense in this   */
'                                /* structure, but data (if provided) will be*/
'                                /* appended in the data block.              */
'   } TW_TWUNKDSENTRYPARAMS, FAR * pTW_TWUNKDSENTRYPARAMS;

'/* SDH - 03/21/95 - TWUNK */
'/* Provides DS_Entry results over thunk link. */
'typedef struct
'{
'    TW_UINT16   returnCode;     /* Thunker DsEntry return code.             */
'    TW_UINT16   conditionCode;  /* Thunker DsEntry condition code.          */
'    TW_INT32    pDataSize;      /* Size of pData (0 if NULL)                */
'    //  TW_MEMREF   pData;      /* Based on implementation specifics, a     */
'                                /* pData parameter makes no sense in this   */
'                                /* structure, but data (if provided) will be*/
'                                /* appended in the data block.              */
'} TW_TWUNKDSENTRYRETURN, FAR * pTW_TWUNKDSENTRYRETURN;

'/* WJD - 950818 */
'/* Added for 1.6 Specification */
'/* TWAIN 1.6 CAP_SUPPORTEDCAPSEXT structure */
'typedef struct
'{
'    TW_UINT16 Cap;   /* Which CAP/ICAP info is relevant to */
'    TW_UINT16 Properties;  /* Messages this CAP/ICAP supports */
'} TW_CAPEXT, FAR * pTW_CAPEXT;

'/* ----------------------------------------------------------------------- *\

'  Version 1.7:      Added Following data structure for Document Imaging 
'  July 1997         Enhancement.
'  KHL               TW_CUSTOMDSDATA --  For Saving and Restoring Source's 
'                                        state.
'                    TW_INFO         --  Each attribute for extended image
'                                        information.
'                    TW_EXTIMAGEINFO --  Extended image information structure.

'\* ----------------------------------------------------------------------- */

'typedef struct {
'    TW_UINT32  InfoLength;     /* Length of Information in bytes.  */
'    TW_HANDLE  hData;          /* Place holder for data, DS Allocates */
'}TW_CUSTOMDSDATA, FAR *pTW_CUSTOMDSDATA;

'typedef struct {
'    TW_UINT16   InfoID;
'    TW_UINT16   ItemType;
'    TW_UINT16   NumItems;
'    TW_UINT16   CondCode;
'    TW_UINT32   Item;
'}TW_INFO, FAR* pTW_INFO;

'typedef struct {
'    TW_UINT32   NumInfos;
'    TW_INFO     Info[1];
'}TW_EXTIMAGEINFO, FAR* pTW_EXTIMAGEINFO;

'/* Added 1.8 */

'/* DAT_AUDIOINFO, information about audio data */
'typedef struct {
'   TW_STR255  Name;       /* name of audio data */
'   TW_UINT32  Reserved;   /* reserved space */
'} TW_AUDIOINFO, FAR * pTW_AUDIOINFO;

'/* DAT_DEVICEEVENT, information about events */
'typedef struct {
'   TW_UINT32  Event;                  /* One of the TWDE_xxxx values. */
'   TW_STR255  DeviceName;             /* The name of the device that generated the event */
'   TW_UINT32  BatteryMinutes;         /* Battery Minutes Remaining    */
'   TW_INT16   BatteryPercentage;      /* Battery Percentage Remaining */
'   TW_INT32   PowerSupply;            /* Power Supply                 */
'   TW_FIX32   XResolution;            /* Resolution                   */
'   TW_FIX32   YResolution;            /* Resolution                   */
'   TW_UINT32  FlashUsed2;             /* Flash Used2                  */
'   TW_UINT32  AutomaticCapture;       /* Automatic Capture            */
'   TW_UINT32  TimeBeforeFirstCapture; /* Automatic Capture            */
'   TW_UINT32  TimeBetweenCaptures;    /* Automatic Capture            */
'} TW_DEVICEEVENT, FAR * pTW_DEVICEEVENT;

'/* DAT_FILESYSTEM, information about TWAIN file system */
'typedef struct {
'   /* DG_CONTROL / DAT_FILESYSTEM / MSG_xxxx fields     */
'   TW_STR255  InputName; /* The name of the input or source file */
'   TW_STR255  OutputName; /* The result of an operation or the name of a destination file */
'   TW_MEMREF  Context; /* Source specific data used to remember state information */
'   /* DG_CONTROL / DAT_FILESYSTEM / MSG_DELETE field    */
'   int        Recursive; /* recursively delete all sub-directories */
'   /* DG_CONTROL / DAT_FILESYSTEM / MSG_GETINFO fields  */
'   TW_INT32   FileType; /* One of the TWFT_xxxx values */
'   TW_UINT32  Size; /* Size of current FileType */
'   TW_STR32   CreateTimeDate; /* creation date of the file */
'   TW_STR32   ModifiedTimeDate; /* last date the file was modified */
'   TW_UINT32  FreeSpace; /* bytes of free space on the current device */
'   TW_INT32   NewImageSize; /* estimate of the amount of space a new image would take up */
'   TW_UINT32  NumberOfFiles; /* number of files, depends on FileType */
'   TW_UINT32  NumberOfSnippets; /* number of audio snippets */
'   TW_UINT32  DeviceGroupMask; /* used to group cameras (ex: front/rear bitonal, front/rear grayscale...) */
'   char       Reserved[508]; /**/
'} TW_FILESYSTEM, FAR * pTW_FILESYSTEM;

'/* DAT_PASSTHRU, device dependant data to pass through Data Source */
'typedef struct {
'   TW_MEMREF  pCommand;        /* Pointer to Command buffer */
'   TW_UINT32  CommandBytes;    /* Number of bytes in Command buffer */
'   TW_INT32   Direction;       /* One of the TWDR_xxxx values.  Defines the direction of data flow */
'   TW_MEMREF  pData;           /* Pointer to Data buffer */
'   TW_UINT32  DataBytes;       /* Number of bytes in Data buffer */
'   TW_UINT32  DataBytesXfered; /* Number of bytes successfully transferred */
'} TW_PASSTHRU, FAR * pTW_PASSTHRU;

'/* DAT_SETUPAUDIOFILEXFER, information required to setup an audio file transfer */
'typedef struct {
'   TW_STR255  FileName; /* full path target file */
'   TW_UINT16  Format;   /* one of TWAF_xxxx */
'   TW_INT16 VRefNum;
'} TW_SETUPAUDIOFILEXFER, FAR * pTW_SETUPAUDIOFILEXFER;

'#ifdef _MAC_
'     /*
'     * Restore original Macintosh structure packing
'     */
'#If PRAGMA_STRUCT_ALIGN Then
'          #pragma options align=reset
'     #elif PRAGMA_STRUCT_PACKPUSH
'          #pragma pack(pop)
'     #elif PRAGMA_STRUCT_PACK
'          #pragma pack()
'#End If
'#endif /* _MAC_ */

