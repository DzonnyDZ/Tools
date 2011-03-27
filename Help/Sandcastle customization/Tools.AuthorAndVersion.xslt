<?xml version="1.0" encoding="utf-8"?>
<!--
    This is extension file for SandCastle to support <author> and <version> tags.
    To make this working:
    * Copy this file to %SandCastleDir%\Presentation\Shared\Tools.AuthorAndVersion.xslt
    * Apply following changes to %SandCastleDir%\Presentation\vs2005\transforms\main_sandcastle.xsl:
        * Add (at beginning of <xsl:stylesheet> element)
            <xsl:import href="../../Shared/transforms/Tools.AuthorAndVersion.xslt"/>
        * Add (at beginning of <xsl:template name="body">)
            <xsl:apply-templates select="/document/reference/author"/>
        * Add (at the end of <xsl:template name="body">)
            <xsl:call-template name="Tools.VersionGroup">
                <xsl:with-param name="versions" select="document/comments/version"/>
            </xsl:call-template>
    (intended for vs2005 presentation style; may work with other presentation styles)
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns=""
>

    <!--Matches the <author> tag and generates author info-->
    <xsl:template match="author">
        <p>
            Author:
            <xsl:value-of select="text()"/>
            <xsl:if test="@mail">
                |
                <a href="mailto:{@mail}">
                    <xsl:value-of select="@mail"/>
                </a>
            </xsl:if>
            <xsl:if test="@www">
                <a href="{@www}">
                    <xsl:value-of select="@www"/>
                </a>
            </xsl:if>
        </p>
    </xsl:template>

    <!--Matches the <version> tag and generates version info for single change-->
    <xsl:template match="version">
        <li>
            <xsl:if test="@stage">
                (<xsl:value-of select="@stage"/>)
            </xsl:if>
            <xsl:apply-templates/>
        </li>
    </xsl:template>

    <!--Call this template with all the <version> tags of one documentation item-->
    <xsl:template name="Tools.VersionGroup">
        <xsl:param name="versions"/>
        <xsl:if test="count($versions) > 0">
            <h1 class="heading">
                <span onclick="ExpandCollapse(familyToggle)" style="cursor:default;" onkeypress="ExpandCollapse_CheckKey(familyToggle, event)" tabindex="0">
                    <img id="familyToggle" class="toggle" name="toggleSwitch" src="../icons/collapse_all.gif" alt="Collapse/expand"/>
                    Version History
                </span>
            </h1>
            <div id="familySection" class="section" name="collapseableSection">
                <xsl:for-each select="$versions/version">
                    <xsl:variable name="version" select="@version"/>
                    <xsl:if test="count(./preceding-sibling::version[@version=$version])=0">
                        <h2>
                            <xsl:value-of select="$version"/>
                        </h2>
                        <ul>
                            <xsl:apply-templates select="$versions/version[@version=$version]"/>
                        </ul>
                    </xsl:if>
                </xsl:for-each>
            </div>
        </xsl:if>
    </xsl:template>

</xsl:stylesheet>
