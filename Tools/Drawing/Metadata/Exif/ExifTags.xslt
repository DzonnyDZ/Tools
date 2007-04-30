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
            <xsl:call-template name="nl"/>
        </xsl:if>
        <xsl:call-template name="code-gen"/>
        <xsl:if test="$namespace!=''">
            <xsl:text>End Namespace</xsl:text>
            <xsl:call-template name="nl"/>
        </xsl:if>
    </xsl:template>
    <!--Generates end of line-->
    <xsl:template name="nl">
        <xsl:text xml:space="preserve">&#xD;&#xA;</xsl:text>
    </xsl:template>
    <xsl:template name="header-comment">
        <xsl:text>' GENERATED FILE -- DO NOT EDIT&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Generator: </xsl:text>
        <xsl:value-of select="$generator"/>
        <xsl:call-template name="nl"/>        
        <xsl:text>' Version: </xsl:text>
        <xsl:value-of select="$version"/>
        <xsl:call-template name="nl"/>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Generated code from "</xsl:text>
        <xsl:value-of select="$filename"/>
        <xsl:text>"&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Created: </xsl:text>
        <xsl:value-of select="$date-created"/>
        <xsl:call-template name="nl"/>
        <xsl:text>' By:</xsl:text>
        <xsl:value-of select="$created-by"/>
        <xsl:call-template name="nl"/>
        <xsl:text>'&#xD;&#xA;</xsl:text>
    </xsl:template>

    <xsl:template name="code-gen">
        <xsl:text>&#9;Partial Public Class Exif&#xD;&#xA;</xsl:text>
        <xsl:call-template name="Tag-enums"/>
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
    </xsl:template>
    <xsl:template name="end-class">
        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;End Select&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Property&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Class&#xD;&#xA;</xsl:text>
    </xsl:template>
    <xsl:template name="property-head">
        <xsl:text>&#9;&#9;&#9;''' &lt;summary>Gets format for tag specified&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;''' &lt;exception cref="InvalidEnumArgumentException">&lt;paramref name="Tag"/> contains unknown value&lt;/exception>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Const any As ushort=0&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Select Case Tag&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--For each supported IFD/Sub IFD generates partial class that contains enumeration of tag numbers used in that IFD-->
    <xsl:template name="Tag-enums">
        <xsl:if test="/et:Root/et:Group[@IFD='IFD']">
            <xsl:text>&#9;&#9;Partial Public Class IFDMain&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;''' &lt;summary>Tag numbers used in IFD0 and IFD1&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public Enum Tags As UShort&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='IFD']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
            <xsl:call-template name="property-head"/>
            <xsl:for-each select="et:Root/et:Group[@IFD='IFD']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
            <xsl:call-template name="end-class"/>            
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='Exif']">
            <xsl:text>&#9;&#9;Partial Public Class IFDExif&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;''' &lt;summary>Tag numbers used in Exif Sub IFD&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public Enum Tags As UShort&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='Exif']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
            <xsl:call-template name="property-head"/>
            <xsl:for-each select="et:Root/et:Group[@IFD='Exif']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
            <xsl:call-template name="end-class"/>
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='GPS']">
            <xsl:text>&#9;&#9;Partial Public Class IFDGPS&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;''' &lt;summary>Tag numbers used in GPS Sub IFD&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public Enum Tags As UShort&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='GPS']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
            <xsl:call-template name="property-head"/>
            <xsl:for-each select="et:Root/et:Group[@IFD='GPS']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
            <xsl:call-template name="end-class"/>
        </xsl:if>


        <xsl:if test="/et:Root/et:Group[@IFD='Interop']">
            <xsl:text>&#9;&#9;Partial Public Class IFDInterop&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;''' &lt;summary>Tag numbers used in Exif Interoperability IFD&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public Enum Tags As UShort&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='Interop']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
            <xsl:call-template name="property-head"/>
            <xsl:for-each select="et:Root/et:Group[@IFD='Interop']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
            <xsl:call-template name="end-class"/>
        </xsl:if>
    </xsl:template>
    
    <!--Generates content of one group of tags in one IFD-->
    <xsl:template name="Tag-enum-content">
        <xsl:text>&#9;&#9;&#9;&#9;#Region "</xsl:text>
        <xsl:value-of select="@Name"/>
        <xsl:text>"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="et:Tag">
            <xsl:text>&#9;&#9;&#9;&#9;&#9;''' &lt;summary></xsl:text>
            <xsl:value-of select="et:summary"/>
            <xsl:text>&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;&lt;Category("</xsl:text>
            <xsl:value-of select="../@ShortName"/>
            <xsl:text>")></xsl:text>
            <xsl:value-of select="@Name"/>
            <xsl:text> = &amp;h</xsl:text>
            <xsl:value-of select="@Tag"/>
            <xsl:call-template name="nl"/>
        </xsl:for-each>
        <xsl:text>&#9;&#9;&#9;&#9;#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Generates content of Select statement that returns description of tag datatype-->
    <xsl:template name="Datatype-description">
        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Case Tags.</xsl:text>
        <xsl:value-of select="@Name"/>
        <xsl:text> : Return New ExifTagFormat(</xsl:text>
        <xsl:value-of select="@Components"/>
        <xsl:text>, &amp;h</xsl:text>
        <xsl:value-of select="@Tag"/>        
        <xsl:text>, "</xsl:text>
        <xsl:value-of select="@Name"/>
        <xsl:text>"</xsl:text>
        <xsl:for-each select="et:Type">
            <xsl:text>, ExifIFDReader.DirectoryEntry.ExifDataTypes.</xsl:text>
            <xsl:value-of select="."/>
        </xsl:for-each>
        <xsl:text>)&#xD;&#xA;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
