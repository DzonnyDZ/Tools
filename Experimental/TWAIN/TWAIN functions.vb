Imports System.Runtime.InteropServices

Friend Module TWAIN_functions
#Region "Constants"
    Public Const TWON_PROTOCOLMINOR As Short = 9
    Public Const TWON_PROTOCOLMAJOR As Short = 1
    ''' <summary>indicates <see cref="TW_ARRAY"/> container</summary>
    Public Const TWON_ARRAY As Short = 3
    ''' <summary>indicates <see cref="TW_ENUMERATION"/> container</summary>
    Public Const TWON_ENUMERATION As Short = 4
    ''' <summary>indicates <see cref="TW_ONEVALUE"/> container</summary>
    Public Const TWON_ONEVALUE As Short = 5
    ''' <summary>indicates <see cref="TW_RANGE"/> container</summary>
    Public Const TWON_RANGE As Short = 6
    ''' <summary>res Id of icon used in USERSELECT lbox</summary>
    Public Const TWON_ICONID As Short = 962
    ''' <summary>res Id of the DSM version num resource</summary>
    Public Const TWON_DSMID As Short = 461
    ''' <summary>res Id of the Mac SM Code resource</summary>
    Public Const TWON_DSMCODEID As Short = 63

    Public Const TWON_DONTCARE8% = &HFF
    Public Const TWON_DONTCARE16% = &HFFFF
    Public Const TWON_DONTCARE32% = &HFFFFFFFF
#End Region
    ''' <summary>Function: DSM_Entry, the only entry point into the Data Source Manager.</summary>
    ''' <param name="pOrigin">Identifies the source module of the message. This could identify an Application, a Source, or the Source Manager.</param>
    ''' <param name="DG">The Data Group. <example><see cref="DG_.IMAGE"/></example></param>
    ''' <param name="DAT">The Data Attribute Type. <example><see cref="DAT_.IMAGEMEMXFER"/></example></param>
    ''' <param name="pDest">Identifies the destination module for the message. This could identify an application or a data source. If this is NULL, the message goes to the Source Manager.</param>
    ''' <param name="MSG">The message.  Messages are interpreted by the destination module with respect to the Data Group and the Data Attribute Type.  <example><see cref="MSG_.[GET]"/></example></param>
    ''' <param name="pData">A pointer to the data structure or variable identified  by the Data Attribute Type. <example><c>(<see cref="TW_MEMREF"/>)&ImageMemXfer</c> where ImageMemXfer is a <see cref="TW_IMAGEMEMXFER"/> structure.</example></param>
    ''' <returns>ReturnCode <example><see cref="TWRC_.SUCCESS"/></example></returns>
    ''' <remarks></remarks>
    <DllImport("twain_32.dll", entrypoint:="#1", SetLastError:=False, CharSet:=CharSet.Ansi, exactspelling:=False, CallingConvention:=CallingConvention.StdCall)> _
    Public Function DSM_Entry( _
        <[In](), Out()> ByVal pOrigin As TW_IDENTITY, _
        <[In](), Out()> ByVal pDest As TW_IDENTITY, _
        ByVal DG As DG_, _
        ByVal DAT As DAT_, _
        ByVal MSG As MSG_, _
        ByRef pData As IntPtr _
    ) As TWRC_
    End Function
    ''' <summary>Function: DS_Entry, the entry point provided by a Data Source.</summary>
    ''' <param name="pOrigin">Identifies the source module of the message. This could identify an application or the Data Source Manager.</param>
    ''' <param name="DG">The Data Group.</param>
    ''' <param name="DAT">The Data Attribute Type.</param>
    ''' <param name="MSG">The message.  Messages are interpreted by the data source with respect to the Data Group and the Data Attribute Type.</param>
    ''' <param name="pData">A pointer to the data structure or variable identified by the Data Attribute Type.</param>
    ''' <returns>ReturnCode</returns>
    ''' <remarks>The DSPROC type is only used by an application when it calls  a Data Source directly, bypassing the Data Source Manager.</remarks>
    <DllImport("twain_32.dll", entrypoint:="#2", setlasterror:=False, CharSet:=CharSet.Ansi, exactspelling:=False, CallingConvention:=CallingConvention.StdCall)> _
    Public Function DS_Entry( _
        <[In](), Out()> ByVal pOrigin As TW_IDENTITY, _
        ByVal DG As DG_, ByVal DAT As DAT_, ByVal MSG As MSG_, ByRef pData As IntPtr) As TWRC_
    End Function
End Module
