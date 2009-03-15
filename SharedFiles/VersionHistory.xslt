<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:vh="http://dzonny.cz/xml/Schemas/VersionHistory"
    xmlns="http://www.w3.org/1999/xhtml"                
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="ms" extension-element-prefixes="ms"
>
    <xsl:output method="xml" omit-xml-declaration="yes"/>
    <xsl:template match="/">
        <html>
            <head>
                <title>Version history</title>
                <style type="text/css">
                    body,html{color:Black;background-color:White;}
                    h1{font-family:sans-serif;text-align:center;font-size:12pt;}
                    body ul{margin-left:0;padding-left:0;}
                    body ul li ul {list-style-type:disc; margin-left:2em;}
                    span.vhead{display:block; background-color:Orange;}
                </style>
            </head>
            <body>
                <h1>Version history</h1>
                <ul>
                    <xsl:apply-templates/>
                </ul>
            </body>
        </html>
    </xsl:template>

    <xsl:template match="vh:Version">
        <li>
            <span class="vhead">
                <strong>
                    <xsl:value-of select="@Major"/>.<xsl:value-of select="@Minor"/>.<xsl:value-of select="@Build"/><xsl:if test="@Revision">.<xsl:value-of select="@Revision"/></xsl:if>
                </strong>
                <xsl:if test="@Date">
                    <xsl:text> - </xsl:text>
                    <xsl:value-of select="ms:format-date(@Date,'d.M. yyyy')"/>
                </xsl:if>
            </span>
            <ul>
                <xsl:apply-templates/>
            </ul>
        </li>
    </xsl:template>

    <xsl:template match="vh:i">
        <li>
            <xsl:copy-of select="node()"/>
        </li>
    </xsl:template>
    
</xsl:stylesheet>
