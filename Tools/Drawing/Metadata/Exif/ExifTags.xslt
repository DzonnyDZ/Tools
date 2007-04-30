<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:et="http://codeplex.com/DTools/ExifTags">

    <xsl:output method="text" version="1.0" encoding="UTF-8" indent="no" omit-xml-declaration="yes"/>

    <!-- parameters passed in by the TransformCodeGenerator -->
    <xsl:param name="generator"></xsl:param>
    <xsl:param name="version"></xsl:param>
    <xsl:param name="fullfilename"></xsl:param>
    <xsl:param name="filename"></xsl:param>
    <xsl:param name="date-created"></xsl:param>
    <xsl:param name="created-by"></xsl:param>
    <xsl:param name="namespace"></xsl:param>
    <xsl:param name="classname"></xsl:param>

    <xsl:template match="/">
        <xsl:call-template name="header-comment"/>
        <xsl:if test="$namespace!=''">
            <xsl:text>Namespace </xsl:text>
            <xsl:value-of select="$namespace"/>
        </xsl:if>
        <xsl:call-template name="code-gen"/>
        <xsl:if test="$namespace!=''">
            <xsl:text>End Namespace</xsl:text>
        </xsl:if>
    </xsl:template>

    <xsl:template name="header-comment">
        <xsl:text>' GENERATED FILE -- DO NOT EDIT
' 
' Generator: </xsl:text>
        <xsl:value-of select="$generator"/>
        <xsl:text>
' Version: </xsl:text>
        <xsl:value-of select="$version"/>
        <xsl:text>'
'
' Generated code from "</xsl:text>
        <xsl:value-of select="$filename"/>
        <xsl:text>"
'
' Created: </xsl:text>
        <xsl:value-of select="$date-created"/>
        <xsl:text>' By:</xsl:text>
        <xsl:value-of select="$created-by"/>
        <xsl:text>'</xsl:text>

    </xsl:template>

    <xsl:template name="code-gen">
        <xsl:text>Partial Public Class Exif</xsl:text>
        <xsl:call-template name="Tag-enums"/>
        <xsl:text>End Class</xsl:text>
    </xsl:template>

    <!--For each supported IFD/Sub IFD generates partial class that contains enumeration of tag numbers used in that IFD-->
    <xsl:template name="Tag-enums">
        <xsl:if test="/et:Root/et:Group[@IFD='IFD']">
        Partial Public Class IFDMain
            ''' &lt;summary>Tag numbers used in IFD0 and IFD1&lt;/summary>
            &lt;CLSCompliant(False)> Public Enum Tags As UShort
            <xsl:for-each select="/et:Root/et:Group[@IFD='IFD']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            End Enum
            ''' &lt;summary>Gets format for tag specified&lt;/summary>
            ''' &lt;exception cref="InvalidEnumArgumentException">&lt;paramref name="Tag"/> contains unknown value&lt;/exception>
            &lt;CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
            Get
            Const any As ushort=0
            Select Case Tag
            <xsl:for-each select="et:Root/et:Group[@IFD='IFD']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
                        Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
                    End Select
                End Get
           End Property
        End Class
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='Exif']">
        Partial Public Class IFDExif
            ''' &lt;summary>Tag numbers used in Exif Sub IFD&lt;/summary>
            &lt;CLSCompliant(False)> Public Enum Tags As UShort
            <xsl:for-each select="/et:Root/et:Group[@IFD='Exif']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            End Enum
            ''' &lt;summary>Gets format for tag specified&lt;/summary>
            ''' &lt;exception cref="InvalidEnumArgumentException">&lt;paramref name="Tag"/> contains unknown value&lt;/exception>
            &lt;CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
            Get
            Const any As ushort=0
            Select Case Tag
            <xsl:for-each select="et:Root/et:Group[@IFD='Exif']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
                        Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
                    End Select
                End Get
           End Property
        End Class
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='GPS']">
        Partial Public Class IFDGPS
            ''' &lt;summary>Tag numbers used in GPS Sub IFD&lt;/summary>
            &lt;CLSCompliant(False)> Public Enum Tags As UShort
            <xsl:for-each select="/et:Root/et:Group[@IFD='GPS']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            End Enum
            ''' &lt;summary>Gets format for tag specified&lt;/summary>
            ''' &lt;exception cref="InvalidEnumArgumentException">&lt;paramref name="Tag"/> contains unknown value&lt;/exception>
            &lt;CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
            Get
            Const any As ushort=0
            Select Case Tag
            <xsl:for-each select="et:Root/et:Group[@IFD='GPS']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
                        Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
                    End Select
                End Get
           End Property
        End Class
        </xsl:if>
        
        Partial Public Class IFDInterop
        <xsl:if test="/et:Root/et:Group[@IFD='Interop']">        
            ''' &lt;summary>Tag numbers used in Exif Interoperability IFD&lt;/summary>
            &lt;CLSCompliant(False)> Public Enum Tags As UShort
            <xsl:for-each select="/et:Root/et:Group[@IFD='Interop']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            End Enum
            ''' &lt;summary>Gets format for tag specified&lt;/summary>
            ''' &lt;exception cref="InvalidEnumArgumentException">&lt;paramref name="Tag"/> contains unknown value&lt;/exception>
            &lt;CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat 
                Get
                    Const any As ushort=0
                    Select Case Tag
            <xsl:for-each select="et:Root/et:Group[@IFD='Interop']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
                        Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
                    End Select
                End Get
           End Property
        </xsl:if>
        End Class
    </xsl:template>
    
    <!--Generates content of one group of tags in one IFD-->
    <xsl:template name="Tag-enum-content">
#Region "<xsl:value-of select="@Name"/>"
        <xsl:for-each select="et:Tag">
            ''' &lt;summary><xsl:value-of select="et:summary"/>&lt;/summary>
            &lt;Category("<xsl:value-of select="../@ShortName"/>")> <xsl:value-of select="@Name"/> = &amp;h<xsl:value-of select="@Tag"/>
        </xsl:for-each>
#End Region
    </xsl:template>

    <!--Generates content of Select statement that returns description of tag datatype-->
    <xsl:template name="Datatype-description">
        <xsl:text>Case Tags.</xsl:text><xsl:value-of select="@Name"/><xsl:text> : </xsl:text>
        <xsl:text>Return New ExifTagFormat(</xsl:text><xsl:value-of select="@Components"/>
        <xsl:text>,"</xsl:text><xsl:value-of select="@Name"/><xsl:text>"</xsl:text>
        <xsl:for-each select="et:Type">
            <xsl:text>,ExifIFDReader.DirectoryEntry.ExifDataTypes.</xsl:text>
            <xsl:value-of select="."/>
        </xsl:for-each>
        <xsl:text>)
        </xsl:text>
    </xsl:template>

</xsl:stylesheet>
