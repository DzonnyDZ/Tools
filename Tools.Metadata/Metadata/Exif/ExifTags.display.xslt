<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns="http://www.w3.org/1999/xhtml"
                xmlns:Exif="http://codeplex.com/DTools/ExifTags"
                extension-element-prefixes="Exif"
>
    <xsl:output method="xml" indent="yes" encoding="utf-8" version="1.0" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" doctype-system="http://www.w3.org/TR/2002/REC-xhtml1-20020801/DTD/xhtml1-strict.dtd"/>

    <xsl:template match="/Exif:Root">
        <html xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <title>Exif tags for ĐTools</title>
                <link rel="stylesheet" type="text/css" href="ExifTags.css" title="main style"/>
                <link rel="alternate" title="XML version" href="Exiftags.xml" type="text/xml" />
            </head>
            <body>
                <h1>Exif tags for ĐTools</h1>
                <p>
                    Stage: <xsl:value-of select="@Stage"/>
                </p>
                <xsl:apply-templates/>
                <p>
                    <a href="http://validator.w3.org/check?uri=referer">
                        <img src="http://www.w3.org/Icons/valid-xhtml10-blue" alt="Valid XHTML 1.0 Strict" height="31" width="88" />
                    </a>
                </p>

            </body>
        </html>
    </xsl:template>

    <xsl:template match="Exif:Group">
        <h2>
            <xsl:value-of select="@Name"/>
            <xsl:text>&#x20;</xsl:text>
            <span>
                <xsl:value-of select="@ShortName"/>
            </span>
        </h2>
        <p>
            IFD: <xsl:value-of select="@IFD"/>
        </p>
        <table>
            <caption>Tags</caption>
            <colgroup span="5">
                <col class="Tag"/>
                <col class="ID"/>
                <col class="Desc"/>
                <col class="Type"/>
                <col class="Comps"/>
            </colgroup>
            <colgroup class="Enum" span="3">
                <col class="e-name"/>
                <col class="e-value"/>
                <col class="e-desc"/>
            </colgroup>
            <thead>
                <tr>
                    <th>Tag</th>
                    <th>ID</th>
                    <th>Description</th>
                    <th>Data types</th>
                    <th>Components</th>
                    <th colspan="3">Enumeration</th>
                </tr>
            </thead>
            <tbody>
                <xsl:apply-templates/>
            </tbody>
        </table>
    </xsl:template>

    <xsl:template match="Exif:Tag">
        <tr>
            <xsl:variable name="rowspan">
                <xsl:choose>
                    <xsl:when test="Exif:enum/Exif:item">
                        <xsl:value-of select="count(Exif:enum/Exif:item)"/>
                    </xsl:when>
                    <xsl:otherwise>1</xsl:otherwise>
                </xsl:choose>
            </xsl:variable>
            <td id="tag_{../@IFD}_{@Name}" rowspan="{$rowspan}">
                <xsl:value-of select="@Name"/>
            </td>
            <td rowspan="{$rowspan}">
                <xsl:value-of select="@Tag"/>
            </td>
            <td rowspan="{$rowspan}">
                <xsl:value-of select="Exif:summary/text()"/>
            </td>
            <td rowspan="{$rowspan}">
                <xsl:for-each select="Exif:Type">
                    <xsl:value-of select="text()"/>
                    <xsl:if test="position() &lt; last()">, </xsl:if>
                </xsl:for-each>
            </td>
            <td rowspan="{$rowspan}">
                <xsl:value-of select="@Components"/>
            </td>
            <xsl:choose>
                <xsl:when test="Exif:enum">
                    <xsl:apply-templates select="Exif:enum" mode="intr"/>
                </xsl:when>
                <xsl:otherwise>
                    <td colspan="3"/>
                </xsl:otherwise>
            </xsl:choose>
        </tr>
        <xsl:if test="Exif:enum">
            <xsl:apply-templates select="Exif:enum" mode="outtr"/>
        </xsl:if>
    </xsl:template>
    <xsl:template match="Exif:item[position()=1]" mode="intr">
        <xsl:call-template name="enum"/>
    </xsl:template>
    <xsl:template match="Exif:item[position()>1]" mode="outtr">
        <tr class="enum">
            <xsl:call-template name="enum"/>
        </tr>
    </xsl:template>
    <xsl:template name="enum">
        <td>
            <xsl:value-of select="@name"/>
        </td>
        <td>
            <xsl:value-of select="@value"/>
        </td>
        <td>
            <xsl:value-of select="@summary"/>
        </td>        
    </xsl:template>
    <xsl:template match="Exif:ref" mode="intr">
        <td colspan="3">
            <span class="see">
                see
                <a href="#tag_{../../../@IFD}_{@ref}">
                    <xsl:value-of select="@ref"/>
                </a>
            </span>
        </td>
    </xsl:template>
</xsl:stylesheet>

