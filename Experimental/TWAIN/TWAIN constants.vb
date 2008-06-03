''' <summary>Flags used in <see cref="TW_MEMORY"/> structure.</summary>
<Flags()> _
Friend Enum TWMF_
    APPOWNS = &H1
    DSMOWNS = &H2
    DSOWNS = &H4
    POINTER = &H8
    HANDLE = &H10
End Enum
''' <summary>Palette types for <see cref="TW_PALETTE8"/></summary>
Friend Enum TWPA_
    RGB = 0
    GRAY = 1
    CMY = 2
End Enum

Friend Enum TWTY_ As UShort
    ''' <summary>Means Item is a <see cref="sByte"/></summary>
    INT8 = &H0
    ''' <summary>Means Item is a <see cref="int16"/></summary>
    INT16 = &H1
    ''' <summary>Means Item is a <see cref="INT32"/></summary>
    INT32 = &H2
    ''' <summary>Means Item is a <see cref="byte"/></summary>
    UINT8 = &H3
    ''' <summary>Means Item is a <see cref="UINT16"/></summary>
    UINT16 = &H4
    ''' <summary>Means Item is a <see cref="UINT32"/></summary>
    UINT32 = &H5
    ''' <summary>Means Item is a <see cref="BOOLean"/></summary>
    BOOL = &H6
    ''' <summary>Means Item is a <see cref="TW_FIX32"/></summary>
    FIX32 = &H7
    ''' <summary>Means Item is a <see cref="TW_FRAME"/></summary>
    FRAME = &H8
    ''' <summary>Means Item is a <see cref="TW_STR32"/></summary>
    STR32 = &H9
    ''' <summary>Means Item is a <see cref="TW_STR64"/></summary>
    STR64 = &HA
    ''' <summary>Means Item is a <see cref="TW_STR128"/></summary>
    STR128 = &HB
    ''' <summary>Means Item is a <see cref="TW_STR255"/></summary>
    STR255 = &HC
    ''' <summary>Means Item is a <see cref="TW_STR1024"/></summary><remarks>added 1.9</remarks>
    STR1024 = &HD
    ''' <summary>Means Item is a <see cref="TW_UNI512"/></summary><remarks>added 1.9</remarks>
    UNI512 = &HE
End Enum

Friend Enum TWBO_
    LSBFIRST = &H0
    MSBFIRST = &H1
End Enum

''' <summary><see cref="ICAP_.COMPRESSION"/> values (CP_ means ComPression ) </summary>
Friend Enum TWCP_
    NONE = 0
    PACKBITS = 1
    ''' <summary>Follows CCITT spec (no End Of Line)</summary>
    GROUP31D = 2
    ''' <summary>Follows CCITT spec (has End Of Line)</summary>
    GROUP31DEOL = 3
    ''' <summary>Follows CCITT spec (use cap for K Factor)</summary>
    GROUP32D = 4
    ''' <summary>Follows CCITT spec</summary>
    GROUP4 = 5
    ''' <summary>Use capability for more info</summary>
    JPEG = 6
    ''' <summary>Must license from Unisys and IBM to use</summary>
    LZW = 7
    ''' <summary>For Bitonal images</summary>
    ''' <remarks>Added 1.7 KHL</remarks>
    JBIG = 8
    ''' <remarks>Added 1.8</remarks>
    PNG = 9
    ''' <remarks>Added 1.8</remarks>
    RLE4 = 10
    ''' <remarks>Added 1.8</remarks>
    RLE8 = 11
    ''' <remarks>Added 1.8</remarks>
    BITFIELDS = 12



End Enum
''' <summary><see cref="ICAP_.IMAGEFILEFORMAT"/> values (FF_means File Format)</summary>
Friend Enum TWFF_
    ''' <summary>Tagged Image File Format</summary>
    TIFF = 0
    ''' <summary>Macintosh PICT</summary>
    PICT = 1
    ''' <summary>Windows Bitmap</summary>
    BMP = 2
    ''' <summary>X-Windows Bitmap</summary>
    XBM = 3
    ''' <summary>JPEG File Interchange Format</summary>
    JFIF = 4
    ''' <summary>Flash Pix</summary>
    FPX = 5
    ''' <summary>Multi-page tiff file</summary>
    TIFFMULTI = 6
    PNG = 7
    SPIFF = 8
    EXIF = 9
End Enum
''' <summary><see cref="ICAP_.FILTER"/> values (FT_ means Filter Type)</summary>
Friend Enum TWFT_
    RED = 0
    GREEN = 1
    BLUE = 2
    NONE = 3
    WHITE = 4
    CYAN = 5
    MAGENTA = 6
    YELLOW = 7
    BLACK = 8
End Enum
''' <summary><see cref="ICAP_.LIGHTPATH"/> values (LP_ means Light Path) </summary>
Friend Enum TWLP_
    REFLECTIVE = 0
    TRANSMISSIVE = 1
End Enum
''' <summary><see cref="ICAP_.LIGHTSOURCE"/> values (LS_ means Light Source)</summary>
Friend Enum TWLS_
    RED = 0
    GREEN = 1
    BLUE = 2
    NONE = 3
    WHITE = 4
    UV = 5
    IR = 6
End Enum
''' <summary><see cref="ICAP_.ORIENTATION"/> values (OR_ means ORientation)</summary>
Friend Enum TWOR_
    ROT0 = 0
    ROT90 = 1
    ROT180 = 2
    ROT270 = 3
    PORTRAIT = ROT0
    LANDSCAPE = ROT270
End Enum
''' <summary><see cref="ICAP_.PLANARCHUNKY"/> values (PC_ means Planar/Chunky )</summary>
Friend Enum TWPC_
    CHUNKY = 0
    PLANAR = 1
End Enum
''' <summary><see cref="ICAP_.PIXELFLAVOR"/> values (PF_ means Pixel Flavor)</summary>
Friend Enum TWPF_
    CHOCOLATE = 0  ' zero pixel represents darkest shade  
    VANILLA = 1  ' zero pixel represents lightest shade 
End Enum

''' <summary><see cref="ICAP_.PIXELTYPE"/> values (PT_ means Pixel Type)</summary>
Friend Enum TWPT_
    BW = 0 ' Black and White 
    GRAY = 1
    RGB = 2
    PALETTE = 3
    CMY = 4
    CMYK = 5
    YUV = 6
    YUVK = 7
    CIEXYZ = 8
End Enum
''' <summary><see cref="ICAP_.SUPPORTEDSIZES"/> values (SS_ means Supported Sizes)</summary>
Friend Enum TWSS_
    NONE = 0
    A4LETTER = 1
    B5LETTER = 2
    USLETTER = 3
    USLEGAL = 4
    ''' <remarks>Added 1.5</remarks> 
    A5 = 5
    ''' <remarks>Added 1.5</remarks> 
    B4 = 6
    ''' <remarks>Added 1.5</remarks> 
    B6 = 7
    '''' <remarks>Added 1.5</remarks> 
    'TWSS_B=          8
    ''' <remarks>Added 1.7</remarks> 
    USLEDGER = 9
    ''' <remarks>Added 1.7</remarks> 
    USEXECUTIVE = 10
    ''' <remarks>Added 1.7</remarks> 
    A3 = 11
    ''' <remarks>Added 1.7</remarks> 
    B3 = 12
    ''' <remarks>Added 1.7</remarks> 
    A6 = 13
    ''' <remarks>Added 1.7</remarks> 
    C4 = 14
    ''' <remarks>Added 1.7</remarks> 
    C5 = 15
    ''' <remarks>Added 1.7</remarks> 
    C6 = 16
    ''' <remarks>Added 1.8</remarks> 
    _4A0 = 17
    ''' <remarks>Added 1.8</remarks> 
    _2A0 = 18
    ''' <remarks>Added 1.8</remarks> 
    A0 = 19
    ''' <remarks>Added 1.8</remarks> 
    A1 = 20
    ''' <remarks>Added 1.8</remarks> 
    A2 = 21
    ''' <remarks>Added 1.8</remarks> 
    A4 = A4LETTER
    ''' <remarks>Added 1.8</remarks> 
    A7 = 22
    ''' <remarks>Added 1.8</remarks> 
    A8 = 23
    ''' <remarks>Added 1.8</remarks> 
    A9 = 24
    ''' <remarks>Added 1.8</remarks> 
    A10 = 25
    ''' <remarks>Added 1.8</remarks> 
    ISOB0 = 26
    ''' <remarks>Added 1.8</remarks> 
    ISOB1 = 27
    ''' <remarks>Added 1.8</remarks> 
    ISOB2 = 28
    ''' <remarks>Added 1.8</remarks> 
    ISOB3 = B3
    ''' <remarks>Added 1.8</remarks> 
    ISOB4 = B4
    ''' <remarks>Added 1.8</remarks> 
    ISOB5 = 29
    ''' <remarks>Added 1.8</remarks> 
    ISOB6 = B6
    ''' <remarks>Added 1.8</remarks> 
    ISOB7 = 30
    ''' <remarks>Added 1.8</remarks> 
    ISOB8 = 31
    ''' <remarks>Added 1.8</remarks> 
    ISOB9 = 32
    ''' <remarks>Added 1.8</remarks> 
    ISOB10 = 33
    ''' <remarks>Added 1.8</remarks> 
    JISB0 = 34
    ''' <remarks>Added 1.8</remarks> 
    JISB1 = 35
    ''' <remarks>Added 1.8</remarks> 
    JISB2 = 36
    ''' <remarks>Added 1.8</remarks> 
    JISB3 = 37
    ''' <remarks>Added 1.8</remarks> 
    JISB4 = 38
    ''' <remarks>Added 1.8</remarks> 
    JISB5 = B5LETTER
    ''' <remarks>Added 1.8</remarks> 
    JISB6 = 39
    ''' <remarks>Added 1.8</remarks> 
    JISB7 = 40
    ''' <remarks>Added 1.8</remarks> 
    JISB8 = 41
    ''' <remarks>Added 1.8</remarks> 
    JISB9 = 42
    ''' <remarks>Added 1.8</remarks> 
    JISB10 = 43
    ''' <remarks>Added 1.8</remarks> 
    C0 = 44
    ''' <remarks>Added 1.8</remarks> 
    C1 = 45
    ''' <remarks>Added 1.8</remarks> 
    C2 = 46
    ''' <remarks>Added 1.8</remarks> 
    C3 = 47
    ''' <remarks>Added 1.8</remarks> 
    C7 = 48
    ''' <remarks>Added 1.8</remarks> 
    C8 = 49
    ''' <remarks>Added 1.8</remarks> 
    C9 = 50
    ''' <remarks>Added 1.8</remarks> 
    C10 = 51
    ''' <remarks>Added 1.8</remarks> 
    USSTATEMENT = 52
    ''' <remarks>Added 1.8</remarks> 
    BUSINESSCARD = 53
End Enum
''' <summary><see cref="ICAP_.XFERMECH"/> values (SX_ means Setup XFer)</summary>
Friend Enum TWSX_
    NATIVE = 0
    FILE = 1
    MEMORY = 2
    ''' <remarks>added 1.9</remarks>
    FILE2 = 3
End Enum

''' <summary><see cref="ICAP_.UNITS"/> values (UN_ means UNits)</summary>
Friend Enum TWUN_
    INCHES = 0
    CENTIMETERS = 1
    PICAS = 2
    POINTS = 3
    TWIPS = 4
    PIXELS = 5
End Enum

''' <summary><see cref="ICAP_.BITDEPTHREDUCTION"/> values (BR_ means Bitdepth Reduction)</summary>
''' <remarks>Added 1.5</remarks>
Friend Enum TWBR_
    THRESHOLD = 0
    HALFTONE = 1
    CUSTHALFTONE = 2
    DIFFUSION = 3
End Enum

''' <summary><see cref="ICAP_.DUPLEX"/> values</summary>
''' <remarks>Added 1.7</remarks>
Friend Enum TWDX_
    NONE = 0
    _1PASSDUPLEX = 1
    _2PASSDUPLEX = 2
End Enum


''' <summary><see cref="TWEI_.BARCODETYPE"/> values</summary>
''' <remarks>Added 1.7</remarks>
Friend Enum TWBT_
    _3OF9 = 0
    _2OF5INTERLEAVED = 1
    _2OF5NONINTERLEAVED = 2
    CODE93 = 3
    CODE128 = 4
    UCC128 = 5
    CODABAR = 6
    UPCA = 7
    UPCE = 8
    EAN8 = 9
    EAN13 = 10
    POSTNET = 11
    PDF417 = 12
    ''' <remarks>Added 1.8</remarks>
    _2OF5INDUSTRIAL = 13
    ''' <remarks>Added 1.8</remarks>
    _2OF5MATRIX = 14
    ''' <remarks>Added 1.8</remarks>
    _2OF5DATALOGIC = 15
    ''' <remarks>Added 1.8</remarks>
    _2OF5IATA = 16
    ''' <remarks>Added 1.8</remarks>
    _3OF9FULLASCII = 17
    ''' <remarks>Added 1.8</remarks>
    CODABARWITHSTARTSTOP = 18
    ''' <remarks>Added 1.8</remarks>
    MAXICODE = 19
End Enum
''' <summary><see cref="TWEI_.DESKEWSTATUS"/> values</summary>
''' <remarks>Added 1.7</remarks>
Friend Enum TWDSK_
    SUCCESS = 0
    REPORTONLY = 1
    FAIL = 2
    DISABLED = 3
End Enum

''' <summary><see cref="TWEI_.PATCHCODE"/> values</summary>
''' <remarks>Added 1.7</remarks>
Friend Enum TWPCH_
    PATCH1 = 0
    PATCH2 = 1
    PATCH3 = 2
    PATCH4 = 3
    PATCH6 = 4
    PATCHT = 5
End Enum


''' <summary><see cref="CAP_.JOBCONTROL"/> values</summary>
''' <remarks>Added 1.7</remarks>
Friend Enum TWJC_
    TWJC_NONE = 0
    TWJC_JSIC = 1
    TWJC_JSIS = 2
    TWJC_JSXC = 3
    TWJC_JSXS = 4
End Enum
''' <summary><see cref="TWEI_.BARCODEROTATION"/> values (BCOR_ means barcode rotation)</summary>
''' <remarks>Added 1.7 </remarks>
Friend Enum TWBCOR_
    ROT0 = 0
    ROT90 = 1
    ROT180 = 2
    ROT270 = 3
    ROTX = 4
End Enum
''' <summary><see cref="ACAP_.AUDIOFILEFORMAT"/> values (AF_ means audio format) </summary>
''' <remarks>Added 1.8</remarks>
Friend Enum TWAF_
    WAV = 0
    AIFF = 1
    AU = 3
    SND = 4
End Enum
''' <summary><see cref="CAP_.ALARMS"/> values (AL_ means alarms)</summary>
Friend Enum TWAL_
    ALARM = 0
    FEEDERERROR = 1
    FEEDERWARNING = 2
    BARCODE = 3
    DOUBLEFEED = 4
    JAM = 5
    PATCHCODE = 6
    POWER = 7
    SKEW = 8
End Enum
''' <summary><see cref="CAP_.CLEARBUFFERS"/> values (CB_ means clear buffers)</summary>
Friend Enum TWCB_
    TWCB_AUTO = 0
    TWCB_CLEAR = 1
    TWCB_NOCLEAR = 2
End Enum
''' <summary><see cref="CAP_.DEVICEEVENT"/> values (DE_ means device event)</summary>
Friend Enum TWDE_
    CUSTOMEVENTS = &H8000
    CHECKAUTOMATICCAPTURE = 0
    CHECKBATTERY = 1
    CHECKDEVICEONLINE = 2
    CHECKFLASH = 3
    CHECKPOWERSUPPLY = 4
    CHECKRESOLUTION = 5
    DEVICEADDED = 6
    DEVICEOFFLINE = 7
    DEVICEREADY = 8
    DEVICEREMOVED = 9
    IMAGECAPTURED = 10
    IMAGEDELETED = 11
    PAPERDOUBLEFEED = 12
    PAPERJAM = 13
    LAMPFAILURE = 14
    POWERSAVE = 15
    POWERSAVENOTIFY = 16
End Enum
''' <summary><see cref="CAP_.FEEDERALIGNMENT"/> values (FA_ means feeder alignment)</summary>
Friend Enum TWFA_
   NONE = 0
    LEFT = 1
    CENTER = 2
    RIGHT = 3
End Enum
''' <summary><see cref="CAP_.FEEDERORDER"/> values (FO_ means feeder order)</summary>
Friend Enum TWFO_
    FIRSTPAGEFIRST = 0
    LASTPAGEFIRST = 1
End Enum
''' <summary><see cref="CAP_.FILESYSTEM"/> values (FS_ means file system)</summary>
Friend Enum TWFS_
    FILESYSTEM = 0
    RECURSIVEDELETE = 1
End Enum
''' <summary><see cref="CAP_.POWERSUPPLY"/> values (PS_ means power supply)</summary>
Friend Enum TWPS_
    EXTERNAL = 0
    BATTERY = 1
End Enum
''' <summary><see cref="CAP_.PRINTER"/> values (PR_ means printer)</summary>
Friend Enum TWPR_
    IMPRINTERTOPBEFORE = 0
    IMPRINTERTOPAFTER = 1
    IMPRINTERBOTTOMBEFORE = 2
    IMPRINTERBOTTOMAFTER = 3
    ENDORSERTOPBEFORE = 4
    ENDORSERTOPAFTER = 5
    ENDORSERBOTTOMBEFORE = 6
    ENDORSERBOTTOMAFTER = 7
End Enum
''' <summary><see cref="CAP_.PRINTERMODE"/> values (PM_ means printer mode)</summary>
Friend Enum TWPM_
    SINGLESTRING = 0
    MULTISTRING = 1
    COMPOUNDSTRING = 2
End Enum
''' <summary><see cref="ICAP_.BARCODESEARCHMODE"/> values (TWBD_ means search)</summary>
Friend Enum TWBD_
    HORZ = 0
    VERT = 1
    HORZVERT = 2
    VERTHORZ = 3
End Enum
''' <summary><see cref="ICAP_.FLASHUSED2"/> values (FL_ means flash)</summary>
Friend Enum TWFL_
    NONE = 0
    OFF = 1
    [ON] = 2
    AUTO = 3
    REDEYE = 4
End Enum
''' <summary><see cref="ICAP_.FLIPROTATION"/> values (FR_ means flip rotation)</summary>
Friend Enum TWFR_
    BOOK = 0
    FANFOLD = 1
End Enum
''' <summary><see cref="ICAP_.IMAGEFILTER"/> values (IF_ means image filter)</summary>
Friend Enum TWIF_
    NONE = 0
    AUTO = 1
    LOWPASS = 2
    BANDPASS = 3
    HIGHPASS = 4
    TEXT = BANDPASS
    FINELINE = HIGHPASS
End Enum
''' <summary><see cref="ICAP_.NOISEFILTER"/> values (NF_ means noise filter)</summary>
Friend Enum TWNF_
    NONE = 0
    AUTO = 1
    LONEPIXEL = 2
    MAJORITYRULE = 3
End Enum
''' <summary><see cref="ICAP_.OVERSCAN"/> values (OV_ means overscan)</summary>
Friend Enum TWOV_
    TWOV_NONE = 0
    TWOV_AUTO = 1
    TWOV_TOPBOTTOM = 2
    TWOV_LEFTRIGHT = 3
    TWOV_ALL = 4
End Enum
''' <summary><see cref="TW_FILESYSTEM.FileType"/> values (FT_ means file type)</summary>
Friend Enum TWFY_
    CAMERA = 0
    CAMERATOP = 1
    CAMERABOTTOM = 2
    CAMERAPREVIEW = 3
    DOMAIN = 4
    HOST = 5
    DIRECTORY = 6
    IMAGE = 7
    UNKNOWN = 8
End Enum
''' <summary><see cref="ICAP_.JPEGQUALITY"/> values (JQ_ means jpeg quality)</summary>
Friend Enum TWJQ_
    UNKNOWN = -4
    LOW = -3
    MEDIUM = -2
    HIGH = -1
End Enum
''' <summary>Country Constants</summary>
Friend Enum TWCY_
    AFGHANISTAN = 1001
    ALGERIA = 213
    AMERICANSAMOA = 684
    ANDORRA = 33
    ANGOLA = 1002
    ANGUILLA = 8090
    ANTIGUA = 8091
    ARGENTINA = 54
    ARUBA = 297
    ASCENSIONI = 247
    AUSTRALIA = 61
    AUSTRIA = 43
    BAHAMAS = 8092
    BAHRAIN = 973
    BANGLADESH = 880
    BARBADOS = 8093
    BELGIUM = 32
    BELIZE = 501
    BENIN = 229
    BERMUDA = 8094
    BHUTAN = 1003
    BOLIVIA = 591
    BOTSWANA = 267
    BRITAIN = 6
    BRITVIRGINIS = 8095
    BRAZIL = 55
    BRUNEI = 673
    BULGARIA = 359
    BURKINAFASO = 1004
    BURMA = 1005
    BURUNDI = 1006
    CAMAROON = 237
    CANADA = 2
    CAPEVERDEIS = 238
    CAYMANIS = 8096
    CENTRALAFREP = 1007
    CHAD = 1008
    CHILE = 56
    CHINA = 86
    CHRISTMASIS = 1009
    COCOSIS = 1009
    COLOMBIA = 57
    COMOROS = 1010
    CONGO = 1011
    COOKIS = 1012
    COSTARICA = 506
    CUBA = 5
    CYPRUS = 357
    CZECHOSLOVAKIA = 42
    DENMARK = 45
    DJIBOUTI = 1013
    DOMINICA = 8097
    DOMINCANREP = 8098
    EASTERIS = 1014
    ECUADOR = 593
    EGYPT = 20
    ELSALVADOR = 503
    EQGUINEA = 1015
    ETHIOPIA = 251
    FALKLANDIS = 1016
    FAEROEIS = 298
    FIJIISLANDS = 679
    FINLAND = 358
    FRANCE = 33
    FRANTILLES = 596
    FRGUIANA = 594
    FRPOLYNEISA = 689
    FUTANAIS = 1043
    GABON = 241
    GAMBIA = 220
    GERMANY = 49
    GHANA = 233
    GIBRALTER = 350
    GREECE = 30
    GREENLAND = 299
    GRENADA = 8099
    GRENEDINES = 8015
    GUADELOUPE = 590
    GUAM = 671
    GUANTANAMOBAY = 5399
    GUATEMALA = 502
    GUINEA = 224
    GUINEABISSAU = 1017
    GUYANA = 592
    HAITI = 509
    HONDURAS = 504
    HONGKONG = 852
    HUNGARY = 36
    ICELAND = 354
    INDIA = 91
    INDONESIA = 62
    IRAN = 98
    IRAQ = 964
    IRELAND = 353
    ISRAEL = 972
    ITALY = 39
    IVORYCOAST = 225
    JAMAICA = 8010
    JAPAN = 81
    JORDAN = 962
    KENYA = 254
    KIRIBATI = 1018
    KOREA = 82
    KUWAIT = 965
    LAOS = 1019
    LEBANON = 1020
    LIBERIA = 231
    LIBYA = 218
    LIECHTENSTEIN = 41
    LUXENBOURG = 352
    MACAO = 853
    MADAGASCAR = 1021
    MALAWI = 265
    MALAYSIA = 60
    MALDIVES = 960
    MALI = 1022
    MALTA = 356
    MARSHALLIS = 692
    MAURITANIA = 1023
    MAURITIUS = 230
    MEXICO = 3
    MICRONESIA = 691
    MIQUELON = 508
    MONACO = 33
    MONGOLIA = 1024
    MONTSERRAT = 8011
    MOROCCO = 212
    MOZAMBIQUE = 1025
    NAMIBIA = 264
    NAURU = 1026
    NEPAL = 977
    NETHERLANDS = 31
    NETHANTILLES = 599
    NEVIS = 8012
    NEWCALEDONIA = 687
    NEWZEALAND = 64
    NICARAGUA = 505
    NIGER = 227
    NIGERIA = 234
    NIUE = 1027
    NORFOLKI = 1028
    NORWAY = 47
    OMAN = 968
    PAKISTAN = 92
    PALAU = 1029
    PANAMA = 507
    PARAGUAY = 595
    PERU = 51
    PHILLIPPINES = 63
    PITCAIRNIS = 1030
    PNEWGUINEA = 675
    POLAND = 48
    PORTUGAL = 351
    QATAR = 974
    REUNIONI = 1031
    ROMANIA = 40
    RWANDA = 250
    SAIPAN = 670
    SANMARINO = 39
    SAOTOME = 1033
    SAUDIARABIA = 966
    SENEGAL = 221
    SEYCHELLESIS = 1034
    SIERRALEONE = 1035
    SINGAPORE = 65
    SOLOMONIS = 1036
    SOMALI = 1037
    SOUTHAFRICA = 27
    SPAIN = 34
    SRILANKA = 94
    STHELENA = 1032
    STKITTS = 8013
    STLUCIA = 8014
    STPIERRE = 508
    STVINCENT = 8015
    SUDAN = 1038
    SURINAME = 597
    SWAZILAND = 268
    SWEDEN = 46
    SWITZERLAND = 41
    SYRIA = 1039
    TAIWAN = 886
    TANZANIA = 255
    THAILAND = 66
    TOBAGO = 8016
    TOGO = 228
    TONGAIS = 676
    TRINIDAD = 8016
    TUNISIA = 216
    TURKEY = 90
    TURKSCAICOS = 8017
    TUVALU = 1040
    UGANDA = 256
    USSR = 7
    UAEMIRATES = 971
    UNITEDKINGDOM = 44
    USA = 1
    URUGUAY = 598
    VANUATU = 1041
    VATICANCITY = 39
    VENEZUELA = 58
    WAKE = 1042
    WALLISIS = 1043
    WESTERNSAHARA = 1044
    WESTERNSAMOA = 1045
    YEMEN = 1046
    YUGOSLAVIA = 38
    ZAIRE = 243
    ZAMBIA = 260
    ZIMBABWE = 263
    ''' <remarks>Added for 1.8</remarks>
    ALBANIA = 355
    ''' <remarks>Added for 1.8</remarks>
    ARMENIA = 374
    ''' <remarks>Added for 1.8</remarks>
    AZERBAIJAN = 994
    ''' <remarks>Added for 1.8</remarks>
    BELARUS = 375
    ''' <remarks>Added for 1.8</remarks>
    BOSNIAHERZGO = 387
    ''' <remarks>Added for 1.8</remarks>
    CAMBODIA = 855
    ''' <remarks>Added for 1.8</remarks>
    CROATIA = 385
    ''' <remarks>Added for 1.8</remarks>
    CZECHREPUBLIC = 420
    ''' <remarks>Added for 1.8</remarks>
    DIEGOGARCIA = 246
    ''' <remarks>Added for 1.8</remarks>
    ERITREA = 291
    ''' <remarks>Added for 1.8</remarks>
    ESTONIA = 372
    ''' <remarks>Added for 1.8</remarks>
    GEORGIA = 995
    ''' <remarks>Added for 1.8</remarks>
    LATVIA = 371
    ''' <remarks>Added for 1.8</remarks>
    LESOTHO = 266
    ''' <remarks>Added for 1.8</remarks>
    LITHUANIA = 370
    ''' <remarks>Added for 1.8</remarks>
    MACEDONIA = 389
    ''' <remarks>Added for 1.8</remarks>
    MAYOTTEIS = 269
    ''' <remarks>Added for 1.8</remarks>
    MOLDOVA = 373
    ''' <remarks>Added for 1.8</remarks>
    MYANMAR = 95
    ''' <remarks>Added for 1.8</remarks>
    NORTHKOREA = 850
    ''' <remarks>Added for 1.8</remarks>
    PUERTORICO = 787
    ''' <remarks>Added for 1.8</remarks>
    RUSSIA = 7
    ''' <remarks>Added for 1.8</remarks>
    SERBIA = 381
    ''' <remarks>Added for 1.8</remarks>
    SLOVAKIA = 421
    ''' <remarks>Added for 1.8</remarks>
    SLOVENIA = 386
    ''' <remarks>Added for 1.8</remarks>
    SOUTHKOREA = 82
    ''' <remarks>Added for 1.8</remarks>
    UKRAINE = 380
    ''' <remarks>Added for 1.8</remarks>
    USVIRGINIS = 340
    ''' <remarks>Added for 1.8</remarks>
    VIETNAM = 84
End Enum

''' <summary>Language Constants</summary>
Friend Enum TWLG_
    ''' <summary>Danish</summary>
    DAN = 0
    ''' <summary>Dutch</summary>
    DUT = 1
    ''' <summary>International English</summary>
    ENG = 2
    ''' <summary>French Canadian</summary>
    FCF = 3
    ''' <summary>Finnish</summary>
    FIN = 4
    ''' <summary>French</summary>
    FRN = 5
    ''' <summary>German</summary>
    GER = 6
    ''' <summary>Icelandic</summary>
    ICE = 7
    ''' <summary>Italian</summary>
    ITN = 8
    ''' <summary>Norwegian</summary>
    NOR = 9
    ''' <summary>Portuguese</summary>
    POR = 10
    ''' <summary>Spanish</summary>
    SPA = 11
    ''' <summary>Swedish</summary>
    SWE = 12
    ''' <summary>U.S. English</summary>
    USA = 13
    ''' <remarks>Added for 1.8</remarks> 
    USERLOCALE = -1
    ''' <remarks>Added for 1.8</remarks> 
    AFRIKAANS = 14
    ''' <remarks>Added for 1.8</remarks> 
    ALBANIA = 15
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC = 16
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_ALGERIA = 17
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_BAHRAIN = 18
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_EGYPT = 19
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_IRAQ = 20
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_JORDAN = 21
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_KUWAIT = 22
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_LEBANON = 23
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_LIBYA = 24
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_MOROCCO = 25
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_OMAN = 26
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_QATAR = 27
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_SAUDIARABIA = 28
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_SYRIA = 29
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_TUNISIA = 30
    ''' <summary>United Arabic Emirates</summary>
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_UAE = 31
    ''' <remarks>Added for 1.8</remarks> 
    ARABIC_YEMEN = 32
    ''' <remarks>Added for 1.8</remarks> 
    BASQUE = 33
    ''' <remarks>Added for 1.8</remarks> 
    BYELORUSSIAN = 34
    ''' <remarks>Added for 1.8</remarks> 
    BULGARIAN = 35
    ''' <remarks>Added for 1.8</remarks> 
    CATALAN = 36
    ''' <remarks>Added for 1.8</remarks> 
    CHINESE = 37
    ''' <remarks>Added for 1.8</remarks> 
    CHINESE_HONGKONG = 38
    ''' <summary>People's Republic of China</summary>
    ''' <remarks>Added for 1.8</remarks> 
    CHINESE_PRC = 39
    ''' <remarks>Added for 1.8</remarks> 
    CHINESE_SINGAPORE = 40
    ''' <remarks>Added for 1.8</remarks> 
    CHINESE_SIMPLIFIED = 41
    ''' <remarks>Added for 1.8</remarks> 
    CHINESE_TAIWAN = 42
    ''' <remarks>Added for 1.8</remarks> 
    CHINESE_TRADITIONAL = 43
    ''' <remarks>Added for 1.8</remarks> 
    CROATIA = 44
    ''' <remarks>Added for 1.8</remarks> 
    CZECH = 45
    ''' <remarks>Added for 1.8</remarks> 
    DANISH = DAN
    ''' <remarks>Added for 1.8</remarks> 
    DUTCH = DUT
    ''' <remarks>Added for 1.8</remarks> 
    DUTCH_BELGIAN = 46
    ''' <remarks>Added for 1.8</remarks> 
    ENGLISH = ENG
    ''' <remarks>Added for 1.8</remarks> 
    ENGLISH_AUSTRALIAN = 47
    ''' <remarks>Added for 1.8</remarks> 
    ENGLISH_CANADIAN = 48
    ''' <remarks>Added for 1.8</remarks> 
    ENGLISH_IRELAND = 49
    ''' <remarks>Added for 1.8</remarks> 
    ENGLISH_NEWZEALAND = 50
    ''' <remarks>Added for 1.8</remarks> 
    ENGLISH_SOUTHAFRICA = 51
    ''' <remarks>Added for 1.8</remarks> 
    ENGLISH_UK = 52
    ''' <remarks>Added for 1.8</remarks> 
    ENGLISH_USA = USA
    ''' <remarks>Added for 1.8</remarks> 
    ESTONIAN = 53
    ''' <remarks>Added for 1.8</remarks> 
    FAEROESE = 54
    ''' <remarks>Added for 1.8</remarks> 
    FARSI = 55
    ''' <remarks>Added for 1.8</remarks> 
    FINNISH = FIN
    ''' <remarks>Added for 1.8</remarks> 
    FRENCH = FRN
    ''' <remarks>Added for 1.8</remarks> 
    FRENCH_BELGIAN = 56
    ''' <remarks>Added for 1.8</remarks> 
    FRENCH_CANADIAN = FCF
    ''' <remarks>Added for 1.8</remarks> 
    FRENCH_LUXEMBOURG = 57
    ''' <remarks>Added for 1.8</remarks> 
    FRENCH_SWISS = 58
    ''' <remarks>Added for 1.8</remarks> 
    GERMAN = GER
    ''' <remarks>Added for 1.8</remarks> 
    GERMAN_AUSTRIAN = 59
    ''' <remarks>Added for 1.8</remarks> 
    GERMAN_LUXEMBOURG = 60
    ''' <remarks>Added for 1.8</remarks> 
    GERMAN_LIECHTENSTEIN = 61
    ''' <remarks>Added for 1.8</remarks> 
    GERMAN_SWISS = 62
    ''' <remarks>Added for 1.8</remarks> 
    GREEK = 63
    ''' <remarks>Added for 1.8</remarks> 
    HEBREW = 64
    ''' <remarks>Added for 1.8</remarks> 
    HUNGARIAN = 65
    ''' <remarks>Added for 1.8</remarks> 
    ICELANDIC = ICE
    ''' <remarks>Added for 1.8</remarks> 
    INDONESIAN = 66
    ''' <remarks>Added for 1.8</remarks> 
    ITALIAN = ITN
    ''' <remarks>Added for 1.8</remarks> 
    ITALIAN_SWISS = 67
    ''' <remarks>Added for 1.8</remarks> 
    JAPANESE = 68
    ''' <remarks>Added for 1.8</remarks> 
    KOREAN = 69
    ''' <remarks>Added for 1.8</remarks> 
    KOREAN_JOHAB = 70
    ''' <remarks>Added for 1.8</remarks> 
    LATVIAN = 71
    ''' <remarks>Added for 1.8</remarks> 
    LITHUANIAN = 72
    ''' <remarks>Added for 1.8</remarks> 
    NORWEGIAN = NOR
    ''' <remarks>Added for 1.8</remarks> 
    NORWEGIAN_BOKMAL = 73
    ''' <remarks>Added for 1.8</remarks> 
    NORWEGIAN_NYNORSK = 74
    ''' <remarks>Added for 1.8</remarks> 
    POLISH = 75
    ''' <remarks>Added for 1.8</remarks> 
    PORTUGUESE = POR
    ''' <remarks>Added for 1.8</remarks> 
    PORTUGUESE_BRAZIL = 76
    ''' <remarks>Added for 1.8</remarks> 
    ROMANIAN = 77
    ''' <remarks>Added for 1.8</remarks> 
    RUSSIAN = 78
    ''' <remarks>Added for 1.8</remarks> 
    SERBIAN_LATIN = 79
    ''' <remarks>Added for 1.8</remarks> 
    SLOVAK = 80
    ''' <remarks>Added for 1.8</remarks> 
    SLOVENIAN = 81
    ''' <remarks>Added for 1.8</remarks> 
    SPANISH = SPA
    ''' <remarks>Added for 1.8</remarks> 
    SPANISH_MEXICAN = 82
    ''' <remarks>Added for 1.8</remarks> 
    SPANISH_MODERN = 83
    ''' <remarks>Added for 1.8</remarks> 
    SWEDISH = SWE
    ''' <remarks>Added for 1.8</remarks> 
    THAI = 84
    ''' <remarks>Added for 1.8</remarks> 
    TURKISH = 85
    ''' <remarks>Added for 1.8</remarks> 
    UKRANIAN = 86
    ''' <remarks>More stuff added for 1.8 </remarks>
    ASSAMESE = 87
    ''' <remarks>More stuff added for 1.8 </remarks>
    BENGALI = 88
    ''' <remarks>More stuff added for 1.8 </remarks>
    BIHARI = 89
    ''' <remarks>More stuff added for 1.8 </remarks>
    BODO = 90
    ''' <remarks>More stuff added for 1.8 </remarks>
    DOGRI = 91
    ''' <remarks>More stuff added for 1.8 </remarks>
    GUJARATI = 92
    ''' <remarks>More stuff added for 1.8 </remarks>
    HARYANVI = 93
    ''' <remarks>More stuff added for 1.8 </remarks>
    HINDI = 94
    ''' <remarks>More stuff added for 1.8 </remarks>
    KANNADA = 95
    ''' <remarks>More stuff added for 1.8 </remarks>
    KASHMIRI = 96
    ''' <remarks>More stuff added for 1.8 </remarks>
    MALAYALAM = 97
    ''' <remarks>More stuff added for 1.8 </remarks>
    MARATHI = 98
    ''' <remarks>More stuff added for 1.8 </remarks>
    MARWARI = 99
    ''' <remarks>More stuff added for 1.8 </remarks>
    MEGHALAYAN = 100
    ''' <remarks>More stuff added for 1.8 </remarks>
    MIZO = 101
    ''' <remarks>More stuff added for 1.8 </remarks>
    NAGA = 102
    ''' <remarks>More stuff added for 1.8 </remarks>
    ORISSI = 103
    ''' <remarks>More stuff added for 1.8 </remarks>
    PUNJABI = 104
    ''' <remarks>More stuff added for 1.8 </remarks>
    PUSHTU = 105
    ''' <remarks>More stuff added for 1.8 </remarks>
    SERBIAN_CYRILLIC = 106
    ''' <remarks>More stuff added for 1.8 </remarks>
    SIKKIMI = 107
    ''' <remarks>More stuff added for 1.8 </remarks>
    SWEDISH_FINLAND = 108
    ''' <remarks>More stuff added for 1.8 </remarks>
    TAMIL = 109
    ''' <remarks>More stuff added for 1.8 </remarks>
    TELUGU = 110
    ''' <remarks>More stuff added for 1.8 </remarks>
    TRIPURI = 111
    ''' <remarks>More stuff added for 1.8 </remarks>
    URDU = 112
    ''' <remarks>More stuff added for 1.8 </remarks>
    VIETNAMESE = 113
End Enum


''' <summary>Data Groups</summary>
''' <remarks>
''' <para>More Data Groups may be added in the future. Possible candidates include text, vector graphics, sound, etc.</para>
''' <para>NOTE: Data Group constants must be powers of 2 as they are used as bitflags when Application asks DSM to present a list of DSs.</para>
''' </remarks>
<Flags()> _
Friend Enum DG_ As UInt32
    ''' <summary>data pertaining to control</summary>
    CONTROL = &H1L
    ''' <summary>data pertaining to raster images</summary>
    IMAGE = &H2L
    ''' <summary>data pertaining to audio</summary>
    ''' <remarks>Added 1.8</remarks>
    AUDIO = &H4L
End Enum

''' <summary>Data Argument Types</summary>
''' <remarks>
''' <para>SDH - 03/23/95 - WATCH</para>
''' <para>The thunker requires knowledge about size of data being passed in the lpData parameter to DS_Entry (which is not readily available due to type LPVOID.  Thus, we key off the DAT_ argument to determine the size.</para>
''' <para>This has a couple implications:</para>
''' <list type="list"><item>Any additional <see cref="DAT_"/> features require modifications to the thunk code for thunker support.</item>
''' <item>Any applications which use the custom capabailites are not supported under thunking since we have no way of knowing what size data (if any) is being passed.</item>
''' </list>
''' </remarks>
Friend Enum DAT_ As UInt16
    ''' <summary>No data or structure. </summary>
    NULL = &H0
    ''' <summary>Base of custom DATs.</summary>
    CUSTOMBASE = &H8000

    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_CAPABILITY"/></summary>
    CAPABILITY = &H1
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_EVENT"/></summary>
    [EVENT] = &H2
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_IDENTITY"/></summary>
    IDENTITY = &H3
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_HANDLE"/>, application win handle in Windows</summary>
    PARENT = &H4
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_PENDINGXFERS"/></summary>
    PENDINGXFERS = &H5
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_SETUPMEMXFER"/></summary>
    SETUPMEMXFER = &H6
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_SETUPFILEXFER"/></summary>
    SETUPFILEXFER = &H7
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_STATUS"/></summary>
    STATUS = &H8
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_USERINTERFACE"/></summary>
    USERINTERFACE = &H9
    ''' <summary>Data Argument Types for the <see cref="DG_.CONTROL"/> Data Group. <see cref="TW_UINT32"/></summary>
    XFERGROUP = &HA
    ''' <summary>Additional message required for thunker to request the special identity information. <see cref="TW_TWUNKIDENTITY"/></summary>
    ''' <remarks>SDH - 03/21/95 - TWUNK</remarks>
    TWUNKIDENTITY = &HB
    ''' <summary><see cref="TW_CUSTOMDSDATA"/>.</summary>
    CUSTOMDSDATA = &HC
    ''' <summary><see cref="TW_DEVICEEVENT"/></summary>
    ''' <remarks>Added 1.8</remarks>
    DEVICEEVENT = &HD
    ''' <summary><see cref="TW_FILESYSTEM"/></summary>
    ''' <remarks>Added 1.8</remarks>
    FILESYSTEM = &HE
    ''' <summary><see cref="TW_PASSTHRU"/></summary>
    ''' <remarks>Added 1.8</remarks>
    PASSTHRU = &HF

    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_IMAGEINFO"/></summary>
    IMAGEINFO = &H101
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_IMAGELAYOUT"/></summary>
    IMAGELAYOUT = &H102
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_IMAGEMEMXFER"/></summary>
    IMAGEMEMXFER = &H103
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="UINT32"/> loword is hDIB, PICHandle</summary>
    IMAGENATIVEXFER = &H104
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. Null data</summary>
    IMAGEFILEXFER = &H105
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_CIECOLOR"/></summary>
    CIECOLOR = &H106
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_GRAYRESPONSE"/></summary>
    GRAYRESPONSE = &H107
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_RGBRESPONSE"/></summary>
    RGBRESPONSE = &H108
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_JPEGCOMPRESSION"/></summary>
    JPEGCOMPRESSION = &H109
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_PALETTE8"/></summary>
    PALETTE8 = &H10A
    ''' <summary>Data Argument Types for the <see cref="DG_.IMAGE"/> Data Group. <see cref="TW_EXTIMAGEINFO"/></summary>
    ''' <remarks>for 1.7 Spec.</remarks>
    EXTIMAGEINFO = &H10B


    ''' <summary>Data Argument Types for the <see cref="DG_.AUDIO"/> Data Group. Null data</summary> 
    ''' <remarks>Added 1.8</remarks>
    AUDIOFILEXFER = &H201
    ''' <summary>Data Argument Types for the <see cref="DG_.AUDIO"/> Data Group. <see cref="TW_AUDIOINFO"/></summary>
    ''' <remarks>Added 1.8</remarks>
    AUDIOINFO = &H202
    ''' <summary>Data Argument Types for the <see cref="DG_.AUDIO"/> Data Group. <see cref="UINT32"/> handle to WAV, (AIFF Mac)</summary>
    ''' <remarks>Added 1.8</remarks>
    AUDIONATIVEXFER = &H203
    ''' <summary>New file xfer operation</summary>
    ''' <remarks>Added 1.9</remarks> 
    SETUPFILEXFER2 = &H301
End Enum


''' <summary>Messages</summary>
''' <remarks>All message constants are unique. Messages are grouped according to which DATs they are used with.</remarks>
Friend Enum MSG_ As UInt16
    ''' <summary>Used in <see cref="TW_EVENT"/> structure</summary>
    NULL = &H0
    ''' <summary>Base of custom messages</summary>
    CUSTOMBASE = &H8000

    ''' <summary>Generic messages may be used with any of several DATs. Get one or more values</summary>
    [GET] = &H1
    ''' <summary>Generic messages may be used with any of several DATs. Get current value</summary>
    GETCURRENT = &H2
    ''' <summary>Generic messages may be used with any of several DATs. Get default (e.g. power up) value </summary>
    GETDEFAULT = &H3 '       
    ''' <summary>Generic messages may be used with any of several DATs. Get first of a series of items, e.g. DSs </summary>
    GETFIRST = &H4
    ''' <summary>Generic messages may be used with any of several DATs. Iterate through a series of items.</summary>
    GETNEXT = &H5
    ''' <summary>Generic messages may be used with any of several DATs. Set one or more values</summary>
    [SET] = &H6
    ''' <summary>Generic messages may be used with any of several DATs. Set current value to default value</summary>
    RESET = &H7
    ''' <summary>Generic messages may be used with any of several DATs. Get supported operations on the cap.</summary>
    QUERYSUPPORT = &H8

    ''' <summary>Messages used with <see cref="DAT_.NULL"/>. The data source has data ready</summary>
    XFERREADY = &H101
    ''' <summary>Messages used with <see cref="DAT_.NULL"/>. Request for Application. to close DS</summary>
    CLOSEDSREQ = &H102
    ''' <summary>Messages used with <see cref="DAT_.NULL"/>. Tell the Application. to save the state.</summary>
    CLOSEDSOK = &H103
    ''' <summary>Messages used with <see cref="DAT_.NULL"/>. Some event has taken place</summary>
    ''' <remarks>Added 1.8</remarks>
    DEVICEEVENT = &H104

    ''' <summary>Messages used with a pointer to a <see cref="DAT_.STATUS"/> structure. Get status information</summary>
    CHECKSTATUS = &H201

    ''' <summary>Messages used with a pointer to <see cref="DAT_.PARENT"/> data. Open the DSM </summary>
    OPENDSM = &H301
    ''' <summary>Messages used with a pointer to <see cref="DAT_.PARENT"/> data. Close the DSM</summary>
    CLOSEDSM = &H302

    ''' <summary>Messages used with a pointer to a <see cref="DAT_.IDENTITY"/> structure. Open a data source</summary>
    OPENDS = &H401
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.IDENTITY"/> structure. Close a data source</summary>
    CLOSEDS = &H402
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.IDENTITY"/> structure. Put up a dialog of all DS</summary>
    USERSELECT = &H403

    ''' <summary>Messages used with a pointer to a <see cref="DAT_.USERINTERFACE"/> structure. Disable data transfer in the DS</summary>
    DISABLEDS = &H501
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.USERINTERFACE"/> structure. Enable data transfer in the DS </summary>
    ENABLEDS = &H502
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.USERINTERFACE"/> structure. Enable for saving DS state only. </summary>
    ENABLEDSUIONLY = &H503

    ''' <summary>Messages used with a pointer to a <see cref="DAT_.EVENT"/> structure</summary>
    PROCESSEVENT = &H601

    ''' <summary>Messages used with a pointer to a <see cref="DAT_.PENDINGXFERS"/> structure</summary>
    ENDXFER = &H701
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.PENDINGXFERS"/> structure</summary>
    STOPFEEDER = &H702

    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    CHANGEDIRECTORY = &H801
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    CREATEDIRECTORY = &H802
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    DELETE = &H803
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    FORMATMEDIA = &H804
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    GETCLOSE = &H805
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    GETFIRSTFILE = &H806
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    GETINFO = &H807
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    GETNEXTFILE = &H808
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    RENAME = &H809
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    COPY = &H80A
    ''' <summary>Messages used with a pointer to a <see cref="DAT_.FILESYSTEM"/> structure</summary>
    ''' <remarks>Added 1.8</remarks>
    AUTOMATICCAPTUREDIRECTORY = &H80B

    ''' <summary>Messages used with a pointer to a <see cref="DAT_.PASSTHRU"/> structure</summary>
    PASSTHRU = &H901
End Enum
''' <summary>Capabilities</summary>
Friend Enum CAP_ As UShort
    ''' <summary>Base of custom capabilities</summary>
    CUSTOMBASE = &H8000

    ''' <summary>all data sources are REQUIRED to support these caps</summary>
    XFERCOUNT = &H1

    ''' <summary>all data sources MAY support these caps</summary>
    AUTHOR = &H1000
    ''' <summary>all data sources MAY support these caps</summary>
    CAPTION = &H1001
    ''' <summary>all data sources MAY support these caps</summary>
    FEEDERENABLED = &H1002
    ''' <summary>all data sources MAY support these caps</summary>
    FEEDERLOADED = &H1003
    ''' <summary>all data sources MAY support these caps</summary>
    TIMEDATE = &H1004
    ''' <summary>all data sources MAY support these caps</summary>
    SUPPORTEDCAPS = &H1005
    ''' <summary>all data sources MAY support these caps</summary>
    EXTENDEDCAPS = &H1006
    ''' <summary>all data sources MAY support these caps</summary>
    AUTOFEED = &H1007
    ''' <summary>all data sources MAY support these caps</summary>
    CLEARPAGE = &H1008
    ''' <summary>all data sources MAY support these caps</summary>
    FEEDPAGE = &H1009
    ''' <summary>all data sources MAY support these caps</summary>
    REWINDPAGE = &H100A
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.1 </remarks>
    INDICATORS = &H100B
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.6 </remarks>
    SUPPORTEDCAPSEXT = &H100C
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.6 </remarks>
    PAPERDETECTABLE = &H100D
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.6 </remarks>
    UICONTROLLABLE = &H100E
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.6 </remarks>
    DEVICEONLINE = &H100F
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.6 </remarks>
    AUTOSCAN = &H1010
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    THUMBNAILSENABLED = &H1011
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    DUPLEX = &H1012
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    DUPLEXENABLED = &H1013
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    ENABLEDSUIONLY = &H1014
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    CUSTOMDSDATA = &H1015
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    ENDORSER = &H1016
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    JOBCONTROL = &H1017
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    ALARMS = &H1018
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    ALARMVOLUME = &H1019
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    AUTOMATICCAPTURE = &H101A
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    TIMEBEFOREFIRSTCAPTURE = &H101B
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    TIMEBETWEENCAPTURES = &H101C
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    CLEARBUFFERS = &H101D
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    MAXBATCHBUFFERS = &H101E
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    DEVICETIMEDATE = &H101F
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    POWERSUPPLY = &H1020
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    CAMERAPREVIEWUI = &H1021
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    DEVICEEVENT = &H1022
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    SERIALNUMBER = &H1024
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PRINTER = &H1026
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PRINTERENABLED = &H1027
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PRINTERINDEX = &H1028
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PRINTERMODE = &H1029
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PRINTERSTRING = &H102A
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PRINTERSUFFIX = &H102B
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    LANGUAGE = &H102C
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    FEEDERALIGNMENT = &H102D
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    FEEDERORDER = &H102E
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    REACQUIREALLOWED = &H1030
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    BATTERYMINUTES = &H1032
    ''' <summary>all data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    BATTERYPERCENTAGE = &H1033
End Enum
Friend Enum ICAP_ As UShort
    ''' <summary>image data sources are REQUIRED to support these caps</summary>
    COMPRESSION = &H100
    ''' <summary>image data sources are REQUIRED to support these caps</summary>
    PIXELTYPE = &H101
    ''' <summary>image data sources are REQUIRED to support these caps. default is <see cref="TWUN_.INCHES"/></summary>
    UNITS = &H102
    ''' <summary>image data sources are REQUIRED to support these caps</summary>
    XFERMECH = &H103
    ''' <summary>image data sources MAY support these caps</summary>
    AUTOBRIGHT = &H1100
    ''' <summary>image data sources MAY support these caps</summary>
    BRIGHTNESS = &H1101
    ''' <summary>image data sources MAY support these caps</summary>
    CONTRAST = &H1103
    ''' <summary>image data sources MAY support these caps</summary>
    CUSTHALFTONE = &H1104
    ''' <summary>image data sources MAY support these caps</summary>
    EXPOSURETIME = &H1105
    ''' <summary>image data sources MAY support these caps</summary>
    FILTER = &H1106
    ''' <summary>image data sources MAY support these caps</summary>
    FLASHUSED = &H1107
    ''' <summary>image data sources MAY support these caps</summary>
    GAMMA = &H1108
    ''' <summary>image data sources MAY support these caps</summary>
    HALFTONES = &H1109
    ''' <summary>image data sources MAY support these caps</summary>
    HIGHLIGHT = &H110A
    ''' <summary>image data sources MAY support these caps</summary>
    IMAGEFILEFORMAT = &H110C
    ''' <summary>image data sources MAY support these caps</summary>
    LAMPSTATE = &H110D
    ''' <summary>image data sources MAY support these caps</summary>
    LIGHTSOURCE = &H110E
    ''' <summary>image data sources MAY support these caps</summary>
    ORIENTATION = &H1110
    ''' <summary>image data sources MAY support these caps</summary>
    PHYSICALWIDTH = &H1111
    ''' <summary>image data sources MAY support these caps</summary>
    PHYSICALHEIGHT = &H1112
    ''' <summary>image data sources MAY support these caps</summary>
    SHADOW = &H1113
    ''' <summary>image data sources MAY support these caps</summary>
    FRAMES = &H1114
    ''' <summary>image data sources MAY support these caps</summary>
    XNATIVERESOLUTION = &H1116
    ''' <summary>image data sources MAY support these caps</summary>
    YNATIVERESOLUTION = &H1117
    ''' <summary>image data sources MAY support these caps</summary>
    XRESOLUTION = &H1118
    ''' <summary>image data sources MAY support these caps</summary>
    YRESOLUTION = &H1119
    ''' <summary>image data sources MAY support these caps</summary>
    MAXFRAMES = &H111A
    ''' <summary>image data sources MAY support these caps</summary>
    TILES = &H111B
    ''' <summary>image data sources MAY support these caps</summary>
    BITORDER = &H111C
    ''' <summary>image data sources MAY support these caps</summary>
    CCITTKFACTOR = &H111D
    ''' <summary>image data sources MAY support these caps</summary>
    LIGHTPATH = &H111E
    ''' <summary>image data sources MAY support these caps</summary>
    PIXELFLAVOR = &H111F
    ''' <summary>image data sources MAY support these caps</summary>
    PLANARCHUNKY = &H1120
    ''' <summary>image data sources MAY support these caps</summary>
    ROTATION = &H1121
    ''' <summary>image data sources MAY support these caps</summary>
    SUPPORTEDSIZES = &H1122
    ''' <summary>image data sources MAY support these caps</summary>
    THRESHOLD = &H1123
    ''' <summary>image data sources MAY support these caps</summary>
    XSCALING = &H1124
    ''' <summary>image data sources MAY support these caps</summary>
    YSCALING = &H1125
    ''' <summary>image data sources MAY support these caps</summary>
    BITORDERCODES = &H1126
    ''' <summary>image data sources MAY support these caps</summary>
    PIXELFLAVORCODES = &H1127
    ''' <summary>image data sources MAY support these caps</summary>
    JPEGPIXELTYPE = &H1128
    ''' <summary>image data sources MAY support these caps</summary>
    TIMEFILL = &H112A
    ''' <summary>image data sources MAY support these caps</summary>
    BITDEPTH = &H112B
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.5</remarks>
    BITDEPTHREDUCTION = &H112C
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.6</remarks>
    UNDEFINEDIMAGESIZE = &H112D
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    IMAGEDATASET = &H112E
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    EXTIMAGEINFO = &H112F
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    MINIMUMHEIGHT = &H1130
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.7</remarks>
    MINIMUMWIDTH = &H1131
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    FLIPROTATION = &H1136
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    BARCODEDETECTIONENABLED = &H1137
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    SUPPORTEDBARCODETYPES = &H1138
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    BARCODEMAXSEARCHPRIORITIES = &H1139
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    BARCODESEARCHPRIORITIES = &H113A
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    BARCODESEARCHMODE = &H113B
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    BARCODEMAXRETRIES = &H113C
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    BARCODETIMEOUT = &H113D
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    ZOOMFACTOR = &H113E
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PATCHCODEDETECTIONENABLED = &H113F
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    SUPPORTEDPATCHCODETYPES = &H1140
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PATCHCODEMAXSEARCHPRIORITIES = &H1141
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PATCHCODESEARCHPRIORITIES = &H1142
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PATCHCODESEARCHMODE = &H1143
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PATCHCODEMAXRETRIES = &H1144
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    PATCHCODETIMEOUT = &H1145
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    FLASHUSED2 = &H1146
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    IMAGEFILTER = &H1147
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    NOISEFILTER = &H1148
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    OVERSCAN = &H1149
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    AUTOMATICBORDERDETECTION = &H1150
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    AUTOMATICDESKEW = &H1151
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.8</remarks>
    AUTOMATICROTATE = &H1152
    ''' <summary>image data sources MAY support these caps</summary>
    ''' <remarks>Added 1.9</remarks>
    JPEGQUALITY = &H1153
End Enum
''' <summary>image data sources MAY support these audio caps </summary>
Friend Enum ACAP_
    ''' <remarks>Added 1.8</remarks>
    AUDIOFILEFORMAT = &H1201
    ''' <remarks>Added 1.8</remarks>
    XFERMECH = &H1202
End Enum

''' <summary>Following is Extended Image Info Attributes.</summary>
''' <remarks>Version 1.7: July 1997 KHL</remarks>
Friend Enum TWEI_
    BARCODEX = &H1200
    BARCODEY = &H1201
    BARCODETEXT = &H1202
    BARCODETYPE = &H1203
    DESHADETOP = &H1204
    DESHADELEFT = &H1205
    DESHADEHEIGHT = &H1206
    DESHADEWIDTH = &H1207
    DESHADESIZE = &H1208
    SPECKLESREMOVED = &H1209
    HORZLINEXCOORD = &H120A
    HORZLINEYCOORD = &H120B
    HORZLINELENGTH = &H120C
    HORZLINETHICKNESS = &H120D
    VERTLINEXCOORD = &H120E
    VERTLINEYCOORD = &H120F
    VERTLINELENGTH = &H1210
    VERTLINETHICKNESS = &H1211
    PATCHCODE = &H1212
    ENDORSEDTEXT = &H1213
    FORMCONFIDENCE = &H1214
    FORMTEMPLATEMATCH = &H1215
    FORMTEMPLATEPAGEMATCH = &H1216
    FORMHORZDOCOFFSET = &H1217
    FORMVERTDOCOFFSET = &H1218
    BARCODECOUNT = &H1219
    BARCODECONFIDENCE = &H121A
    BARCODEROTATION = &H121B
    BARCODETEXTLENGTH = &H121C
    DESHADECOUNT = &H121D
    DESHADEBLACKCOUNTOLD = &H121E
    DESHADEBLACKCOUNTNEW = &H121F
    DESHADEBLACKRLMIN = &H1220
    DESHADEBLACKRLMAX = &H1221
    DESHADEWHITECOUNTOLD = &H1222
    DESHADEWHITECOUNTNEW = &H1223
    DESHADEWHITERLMIN = &H1224
    DESHADEWHITERLAVE = &H1225
    DESHADEWHITERLMAX = &H1226
    BLACKSPECKLESREMOVED = &H1227
    WHITESPECKLESREMOVED = &H1228
    HORZLINECOUNT = &H1229
    VERTLINECOUNT = &H122A
    DESKEWSTATUS = &H122B
    SKEWORIGINALANGLE = &H122C
    SKEWFINALANGLE = &H122D
    SKEWCONFIDENCE = &H122E
    SKEWWINDOWX1 = &H122F
    SKEWWINDOWY1 = &H1230
    SKEWWINDOWX2 = &H1231
    SKEWWINDOWY2 = &H1232
    SKEWWINDOWX3 = &H1233
    SKEWWINDOWY3 = &H1234
    SKEWWINDOWX4 = &H1235
    SKEWWINDOWY4 = &H1236
    ''' <remarks>added 1.9</remarks>
    BOOKNAME = &H1238
    ''' <remarks>added 1.9</remarks>
    CHAPTERNUMBER = &H1239
    ''' <remarks>added 1.9</remarks>
    DOCUMENTNUMBER = &H123A
    ''' <remarks>added 1.9</remarks>
    PAGENUMBER = &H123B
    ''' <remarks>added 1.9</remarks>
    CAMERA = &H123C
    ''' <remarks>added 1.9</remarks>
    FRAMENUMBER = &H123D
    ''' <remarks>added 1.9</remarks>
    FRAME = &H123E
    ''' <remarks>added 1.9</remarks>
    PIXELFLAVOR = &H123F
End Enum
Friend Enum TWEJ_
    NONE = &H0
    MIDSEPARATOR = &H1
    PATCH1 = &H2
    PATCH2 = &H3
    PATCH3 = &H4
    PATCH4 = &H5
    PATCH6 = &H6
    PATCHT = &H7
End Enum

''' <summary><see cref="TW_PASSTHRU.Direction"/> values</summary>
''' <remarks>Added 1.8</remarks>
Friend Enum TWDR_
    [GET] = 1
    [SET] = 2
End Enum


''' <summary>Return Codes: <see cref="DSM_Entry"/> and <see cref="DS_Entry"/> may return any one of these values.</summary>
Friend Enum TWRC_ As UInt32
    CUSTOMBASE = &H8000

    SUCCESS = 0
    ''' <summary>Application may get TW_STATUS for info on failure </summary>
    FAILURE = 1
    ''' <summary>"tried hard"; get status</summary>
    CHECKSTATUS = 2
    CANCEL = 3
    DSEVENT = 4
    NOTDSEVENT = 5
    XFERDONE = 6
    ''' <summary>After <see cref="MSG_.GETNEXT"/> if nothing left</summary>
    ENDOFLIST = 7
    INFONOTSUPPORTED = 8
    DATANOTAVAILABLE = 9
End Enum
''' <summary>Condition Codes: Application gets these by doing <see cref="DG_.CONTROL"/> <see cref="DAT_.STATUS"/> <see cref="MSG_.GET"/>.</summary>
Friend Enum TWCC_ As UShort
    CUSTOMBASE = &H8000
    ''' <summary>It worked!</summary>
    SUCCESS = 0
    ''' <summary>Failure due to unknown causes</summary>
    BUMMER = 1
    ''' <summary>Not enough memory to perform operation</summary>
    LOWMEMORY = 2
    ''' <summary>No Data Source</summary>
    NODS = 3
    ''' <summary>DS is connected to max possible applications</summary>
    MAXCONNECTIONS = 4
    ''' <summary>DS or DSM reported error, application shouldn't</summary>
    OPERATIONERROR = 5
    ''' <summary>Unknown capability</summary>
    BADCAP = 6
    ''' <summary>Unrecognized MSG DG DAT combination</summary>
    BADPROTOCOL = 9
    ''' <summary>Data parameter out of range</summary>
    BADVALUE = 10
    ''' <summary>DG DAT MSG out of expected sequence</summary>
    SEQERROR = 11
    ''' <summary>Unknown destination Application/Source in <see cref="DSM_Entry"/></summary>
    BADDEST = 12
    ''' <summary>Capability not supported by source </summary>
    CAPUNSUPPORTED = 13
    ''' <summary>Operation not supported by capability</summary>
    CAPBADOPERATION = 14
    ''' <summary>Capability has dependancy on other capability</summary>
    CAPSEQERROR = 15
    ''' <summary>File System operation is denied (file is protected)</summary><remarks>Added 1.8</remarks>
    DENIED = 16
    ''' <summary>Operation failed because file already exists.</summary><remarks>Added 1.8</remarks>
    FILEEXISTS = 17
    ''' <summary>File not found</summary><remarks>Added 1.8</remarks>
    FILENOTFOUND = 18
    ''' <summary>Operation failed because directory is not empty</summary><remarks>Added 1.8</remarks>
    NOTEMPTY = 19
    ''' <summary>The feeder is jammed</summary><remarks>Added 1.8</remarks>
    PAPERJAM = 20
    ''' <summary>The feeder detected multiple pages</summary><remarks>Added 1.8</remarks>
    PAPERDOUBLEFEED = 21
    ''' <summary>Error writing the file (meant for things like disk full conditions)</summary><remarks>Added 1.8</remarks>
    FILEWRITEERROR = 22
    ''' <summary>The device went offline prior to or during this operation</summary><remarks>Added 1.8</remarks>
    CHECKDEVICEONLINE = 23
End Enum


''' <summary>bit patterns: for query the operation that are supported by the data source on a capability Application gets these through <see cref="DG_.CONTROL"/>/<see cref="DAT_.CAPABILITY"/>/<see cref="MSG_.QUERYSUPPORT"/></summary>
''' <remarks>Added 1.6 </remarks>
<Flags()> _
Friend Enum TWQC_
    [GET] = &H1
    [SET] = &H2
    GETDEFAULT = &H4
    GETCURRENT = &H8
    RESET = &H10
End Enum