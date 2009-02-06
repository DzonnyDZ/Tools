<?xml version="1.0" encoding="utf-8"?>
<!--This stylesheet replaces <see2 cref2> with <see cref>. This is workaround to bug 411703 in C++ XML comments generator
See https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=411703-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="see2">
        <see>
            <xsl:apply-templates select="@* | node()"/>
        </see>
    </xsl:template>
    <xsl:template match="@cref2">
        <xsl:attribute name="cref">
            <xsl:value-of select="."/>
        </xsl:attribute>
    </xsl:template>
</xsl:stylesheet>
