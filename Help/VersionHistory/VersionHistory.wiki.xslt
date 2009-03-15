<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:vh="http://dzonny.cz/xml/Schemas/VersionHistory"
                xmlns:i="http://dzonny.cz/xml/schemas/intellisense"
>
    <xsl:output method="text" indent="no"/>

    <xsl:param name="version" select="'1.5.2'"/>
    <xsl:template match="vh:VersionHistory">
        <xsl:text xml:space="preserve">! New in version </xsl:text><xsl:value-of select="$version"/>
        <xsl:text>&#xD;&#xA;</xsl:text>
        <xsl:apply-templates select="vh:Version[concat(@Major,'.',@Minor,'.',@Build,'.',@Revision)=$version or (not(@Revision) and concat(@Major,'.',@Minor,'.',@Build)=$version)]/vh:i" />
        <xsl:text>... and much more</xsl:text>        
    </xsl:template>

    <xsl:template match="vh:i">
        <xsl:text xml:space="preserve">* </xsl:text>
        <xsl:apply-templates />
        <xsl:text>&#xD;&#xA;</xsl:text>
    </xsl:template>
    <xsl:template match="text()">
        <xsl:value-of select="normalize-space(.)"/>
    </xsl:template>
    <xsl:template match="i:see">
        <xsl:choose>
            <xsl:when test="node()">
                <xsl:apply-templates />
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="substring-after(@cref,':')"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
</xsl:stylesheet>
