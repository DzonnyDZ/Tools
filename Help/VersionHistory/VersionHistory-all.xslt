<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:c="http://ddue.schemas.microsoft.com/authoring/2003/5"
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
            <c:developerConceptualDocument>
                <c:introduction>
                    <c:para>
                        This file describe all the changes in version <xsl:value-of select="$version"/>. For brief description of changes see <c:link xlink:href="9b0f2e22-3088-4c9b-af71-51717771bb58"/>.
                    </c:para>
                </c:introduction>
                <c:section address="ChangeList">
                    <c:title>Changes in version <xsl:value-of select="$version"/></c:title>
                    <c:content>
                        <xsl:apply-templates select="/doc/members/member[version/@version=$version]">
                            <xsl:sort select="substring-after(@name,':')" data-type="text" lang="en-US"/>
                        </xsl:apply-templates>
                    </c:content>
                </c:section>
            </c:developerConceptualDocument>
        </topic>
    </xsl:template>
    <xsl:template match="member">
            <c:title>
                <c:codeEntityReference qualifyHint="true">
                    <xsl:value-of select="@name"/>
                </c:codeEntityReference>
            </c:title>
            <xsl:if test="version[@version=$version][@stage]">
                <c:para>
                    <c:legacyBold>Development stage:</c:legacyBold>
                    <xsl:text xml:space="preserve"> </xsl:text>
                    <xsl:choose>
                        <xsl:when test="version[@version=$version][@stage][1]/@stage = 'RC'">Release Candidate</xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="version[@version=$version][@stage][1]/@stage"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </c:para>
            </xsl:if>
            <c:list class="bullet">
                <xsl:apply-templates select="version[@version=$version]"/>
            </c:list>
    </xsl:template>

    <xsl:template match="version">
        <c:listItem>
            <xsl:apply-templates/>
        </c:listItem>
    </xsl:template>

    <xsl:template match="see">
        <c:codeEntityReference qualifyHint="true">
            <xsl:value-of select="@cref"/>
        </c:codeEntityReference>
    </xsl:template>
    <xsl:template match="code">
        <c:code>
            <xsl:apply-templates select="@*"/>
            <xsl:apply-templates/>
        </c:code>
    </xsl:template>
    <xsl:template match="c">
        <c:codeInline>
            <xsl:apply-templates/>
        </c:codeInline>
    </xsl:template>
    <xsl:template match="paramref | typeparamref">
        <c:codeInline>
            <xsl:choose>
                <xsl:when test="node()">
                    <xsl:apply-templates />
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="@name"/>
                </xsl:otherwise>
            </xsl:choose>
        </c:codeInline>
    </xsl:template>
    <xsl:template match="example">
        <c:para>
            <xsl:apply-templates />
        </c:para>
    </xsl:template>
    <xsl:template match="list">
        <c:list class="bullet">
            <xsl:apply-templates/>
        </c:list>
    </xsl:template>
    <xsl:template match="list[@type='number'] | list[@type='ordered']">
        <c:list class="ordered">
            <xsl:apply-templates/>
        </c:list>
    </xsl:template>
    <xsl:template match="list[@type='table']">
        <c:table>
            <xsl:apply-templates/>
        </c:table>
    </xsl:template>
    <xsl:template match="item">
        <c:listItem>
            <xsl:apply-templates />
        </c:listItem>
    </xsl:template>
    <xsl:template match="listheader">
        <c:listItem>
            <c:legacyBold>
                <xsl:apply-templates />
            </c:legacyBold>
        </c:listItem>
    </xsl:template>
    <xsl:template match="list[@type='table']/item">
        <c:row>
            <xsl:apply-templates/>
        </c:row>
    </xsl:template>
    <xsl:template match="list[@type='table']/listheader">
        <c:tableHeader>
            <c:row>
                <xsl:apply-templates/>
            </c:row>
        </c:tableHeader>
    </xsl:template>
    <xsl:template match="term | description">
        <c:entry>
            <xsl:apply-templates />
        </c:entry>
    </xsl:template>
    <xsl:template match="para">
        <c:para>
            <xsl:apply-templates/>
        </c:para>
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
        <c:alert class="{$type}">
            <xsl:apply-templates/>
        </c:alert>
    </xsl:template>
</xsl:stylesheet>
