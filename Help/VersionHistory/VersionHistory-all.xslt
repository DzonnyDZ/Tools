<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5"
    xmlns:xlink="http://www.w3.org/1999/xlink"
    xmlns:vh="http://dzonny.cz/xml/Schemas/VersionHistory"
    exclude-result-prefixes="msxsl vh"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:param name="version"/>
    <xsl:param name="guid"/>

    <xsl:template match="/doc">
        <topic id="{$guid}" revisionNumber="1">
            <developerConceptualDocument>
                <introduction>
                    <para>
                        This file describe all the changes in version <xsl:value-of select="$version"/>. For brief description of changes see <link xlink:href="9b0f2e22-3088-4c9b-af71-51717771bb58"/>.
                    </para>
                </introduction>
                <section address="ChangeList">
                    <title>Changes in version <xsl:value-of select="$version"/></title>
                    <content>
                        <xsl:apply-templates select="/doc/members/member[version/@version=$version]">
                            <xsl:sort select="substring-after(@name,':')" data-type="text" lang="en-US"/>
                        </xsl:apply-templates>
                    </content>
                </section>
                <relatedTopics />
            </developerConceptualDocument>
        </topic>
    </xsl:template>
    <xsl:template match="member">
            <title>
                <codeEntityReference qualifyHint="true">
                    <xsl:value-of select="@name"/>
                </codeEntityReference>
            </title>
            <xsl:if test="version[@version=$version][@stage]">
                <para>
                    <legacyBold>Development stage:</legacyBold>
                    <xsl:text xml:space="preserve"> </xsl:text>
                    <xsl:choose>
                        <xsl:when test="version[@version=$version][@stage][1]/@stage = 'RC'">Release Candidate</xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="version[@version=$version][@stage][1]/@stage"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </para>
            </xsl:if>
            <list class="bullet">
                <xsl:apply-templates select="version[@version=$version]"/>
            </list>
    </xsl:template>

    <xsl:template match="version">
        <listItem>
            <para>
            <xsl:apply-templates/>
            </para>
        </listItem>
    </xsl:template>

    <xsl:template match="see">
        <codeEntityReference qualifyHint="true">
            <xsl:value-of select="@cref"/>
        </codeEntityReference>
    </xsl:template>
    <xsl:template match="code">
        <code>
            <xsl:apply-templates select="@*"/>
            <xsl:apply-templates/>
        </code>
    </xsl:template>
    <xsl:template match="c">
        <codeInline>
            <xsl:apply-templates/>
        </codeInline>
    </xsl:template>
    <xsl:template match="paramref | typeparamref">
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
    <xsl:template match="example">
        <para>
            <xsl:apply-templates />
        </para>
    </xsl:template>
    <xsl:template match="list">
        <list class="bullet">
            <xsl:apply-templates/>
        </list>
    </xsl:template>
    <xsl:template match="list[@type='number'] | list[@type='ordered']">
        <list class="ordered">
            <xsl:apply-templates/>
        </list>
    </xsl:template>
    <xsl:template match="list[@type='table']">
        <table>
            <xsl:apply-templates/>
        </table>
    </xsl:template>
    <xsl:template match="item">
        <listItem>
            <xsl:apply-templates />
        </listItem>
    </xsl:template>
    <xsl:template match="listheader">
        <listItem>
            <legacyBold>
                <xsl:apply-templates />
            </legacyBold>
        </listItem>
    </xsl:template>
    <xsl:template match="list[@type='table']/item">
        <row>
            <xsl:apply-templates/>
        </row>
    </xsl:template>
    <xsl:template match="list[@type='table']/listheader">
        <tableHeader>
            <row>
                <xsl:apply-templates/>
            </row>
        </tableHeader>
    </xsl:template>
    <xsl:template match="term | description">
        <entry>
            <xsl:apply-templates />
        </entry>
    </xsl:template>
    <xsl:template match="para">
        <para>
            <xsl:apply-templates/>
        </para>
    </xsl:template>
    <xsl:template match="include">
        <xsl:apply-templates select="document(@file,@path)"/>
    </xsl:template>
    <xsl:template match="note">
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
