<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5"
    xmlns:xlink="http://www.w3.org/1999/xlink"
    xmlns:vh="http://dzonny.cz/xml/Schemas/VersionHistory"
    xmlns:i="http://dzonny.cz/xml/schemas/intellisense"
    exclude-result-prefixes="msxsl vh i"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="vh:VersionHistory">
        <xsl:processing-instruction name="xml-stylesheet">href="../Conceptual/conceptual.css" type="text/css"</xsl:processing-instruction>
        <topic id="9b0f2e22-3088-4c9b-af71-51717771bb58" revisionNumber="1" xmlns="">
            <developerConceptualDocument
              xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">
                <introduction>
                    <autoOutline />
                    <para>
                        This document documents version history of the ĐTools project (starting with version 1.5.2.0).
                        Only main changes are listed here. All changes are listed in doccumentation of appropriate members.
                        Whene there was a bigger change - like added namespace or class it's listed only in documentation of top-most changed object (e.g. a new namespace or a new class).
                    </para>
                </introduction>
                <xsl:for-each select="vh:Version">
                    <xsl:sort data-type="number" order="descending" select="@Major"/>
                    <xsl:sort data-type="number" order="descending" select="@Minor"/>
                    <xsl:sort data-type="number" order="descending" select="@Build"/>
                    <xsl:sort data-type="number" order="descending" select="@Revision"/>
                    <xsl:apply-templates select="."/>
                </xsl:for-each>
                <relatedTopics />
            </developerConceptualDocument>
        </topic>
    </xsl:template>

    <xsl:template match="vh:Version">
        <section address="v{@Major}.{@Minor}.{@Build}.{@Revision}">
            <title>
                <xsl:value-of select="@Major"/>.<xsl:value-of select="@Minor"/>.<xsl:value-of select="@Build"/><xsl:if test="@Revision">.<xsl:value-of select="@Revision"/></xsl:if>
            </title>
            <content>
                <list class="bullet">
                    <xsl:apply-templates />
                </list>
            </content>
        </section>
    </xsl:template>
    <xsl:template match="vh:i">
        <listItem>
            <para>
                <xsl:apply-templates/>
            </para>
        </listItem>    
    </xsl:template>
    <xsl:template match="i:see">
        <codeEntityReference qualifyHint="true">
            <xsl:value-of select="@cref"/>
        </codeEntityReference>
    </xsl:template>
    <xsl:template match="i:code">
        <code>
            <xsl:apply-templates select="@*"/>
            <xsl:apply-templates/>
        </code>
    </xsl:template>
    <xsl:template match="i:c">
        <codeInline>
            <xsl:apply-templates/>
        </codeInline>
    </xsl:template>
    <xsl:template match="i:paramref | i:typeparamref">
        <codeInline>
            <xsl:choose>
                <xsl:when test="node()">
                    <xsl:apply-templates />
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="@name"/>
                </xsl:otherwise>
            </xsl:choose>
        </codeInline>
    </xsl:template>
    <xsl:template match="i:example">
        <para>
            <xsl:apply-templates />
        </para>
    </xsl:template>
    <xsl:template match="i:list">
        <list class="bullet">
            <xsl:apply-templates/>
        </list>
    </xsl:template>
    <xsl:template match="i:list[@type='number'] | i:list[@type='ordered']">
        <list class="ordered">
            <xsl:apply-templates/>
        </list>
    </xsl:template>
    <xsl:template match="i:list[@type='table']">
        <table>
            <xsl:apply-templates/>            
        </table>        
    </xsl:template>
    <xsl:template match="i:item">
        <listItem>
            <xsl:apply-templates />
        </listItem>
    </xsl:template>
    <xsl:template match="i:listheader">
        <listItem>
            <legacyBold>
                <xsl:apply-templates />
            </legacyBold>
        </listItem>
    </xsl:template>
    <xsl:template match="i:list[@type='table']/i:item">
        <row>
            <xsl:apply-templates/>
        </row>    
    </xsl:template>
    <xsl:template match="i:list[@type='table']/i:listheader">
        <tableHeader>
            <row>
                <xsl:apply-templates/>
            </row>
        </tableHeader>
    </xsl:template>
    <xsl:template match="i:term | i:description">
        <entry>
            <xsl:apply-templates />
        </entry>
    </xsl:template>
    <xsl:template match="i:para">
        <para>
            <xsl:apply-templates/>
        </para>
    </xsl:template>
    <xsl:template match="i:include">
        <xsl:apply-templates select="document(@file,@path)"/>
    </xsl:template>
    <xsl:template match="i:note">
        <xsl:variable name="type">
            <xsl:choose>
                <xsl:when test="@type='inheritinfo'">inherit</xsl:when>
                <xsl:when test="@type='implementnotes'">implement</xsl:when>
            </xsl:choose>
        </xsl:variable>
        <alert class="{$type}">
            <xsl:apply-templates/>
        </alert>
    </xsl:template>
</xsl:stylesheet>
