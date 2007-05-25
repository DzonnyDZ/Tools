<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ISO="http://codeplex.com/DTools/ISOLanguages">

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
        <xsl:text>#If Congig &lt;= </xsl:text>
        <xsl:value-of select="ISO:Root/@Stage"/>
        <xsl:if test="ISO:Root/@Stage!='Release'">
            <xsl:text> 'Stage: </xsl:text>
            <xsl:value-of select="ISO:Root/@Stage"/>
        </xsl:if>
        <xsl:call-template name="nl"/>
        <xsl:call-template name="code-gen"/>
        <xsl:text>#End If&#xD;&#xA;</xsl:text>
        <xsl:if test="$namespace!=''">
            <xsl:text>End Namespace</xsl:text>
            <xsl:call-template name="nl"/>
        </xsl:if>
    </xsl:template>
    <!--Generates end of line-->
    <xsl:template name="nl">
        <xsl:text xml:space="preserve">&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Generates header comment-->
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
        <xsl:text>&#9;Partial Public Class ISOLanguage&#xD;&#xA;</xsl:text>
        <xsl:call-template name="Properties"/>
        <!--TODO: Call more templates-->
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Generates shared properties for languages-->
    <xsl:template name="Properties">
        <xsl:for-each select="ISO:Root/ISO:lng">
            <xsl:text>&#9;&#9;Public Shared Readonly Property [</xsl:text>
            <xsl:value-of select="@ISO639-2"/>
            <xsl:text>]() As ISOLanguage&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;Return New ISOLanguage("</xsl:text>
            <xsl:value-of select="@ISO639-1"/>
            <xsl:text>", "</xsl:text>
            <xsl:value-of select="@ISO639-2"/>
            <xsl:text>", "</xsl:text>
            <xsl:value-of select="@English"/>
            <xsl:text>", "</xsl:text>
            <xsl:value-of select="@Native"/>
            <xsl:text>", </xsl:text>
            <xsl:value-of select="@Scale"/>
            <xsl:text>, CodeTypes.</xsl:text>
            <xsl:value-of select="@flag"/>
            <xsl:text>, "</xsl:text>
            <xsl:value-of select="../ISO:duplicate[@of=/@ISO639-2]/@ISO639-2"/>
            <xsl:text>")&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;End Property&#xD;&#xA;</xsl:text>
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>