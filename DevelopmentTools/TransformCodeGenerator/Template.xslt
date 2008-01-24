<?xml version="1.0" encoding="UTF-8" ?>
<!--This is sample template. More real-life templates can be foun in Tools project.
Copyright (C) 2006 Chris Stefano-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

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

  <!-- support variables -->
  <xsl:variable name="lf">&#13;</xsl:variable>
  <xsl:variable name="sp">&#32;</xsl:variable>

  <xsl:template match="/">
    <xsl:call-template name="header-comment"/>
    <xsl:text>namespace </xsl:text><xsl:value-of select="$namespace"/><xsl:value-of select="$lf"/>
    <xsl:text>{</xsl:text><xsl:value-of select="$lf"/>
    <xsl:call-template name="code-gen"/>
    <xsl:text>}</xsl:text><xsl:value-of select="$lf"/>
  </xsl:template>

  <xsl:template name="header-comment">
    <xsl:text>#region Generated File</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text>/*</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text> * GENERATED FILE -- DO NOT EDIT</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text> *</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text> * Generator: </xsl:text><xsl:value-of select="$generator"/><xsl:value-of select="$lf"/>
    <xsl:text> * Version:   </xsl:text><xsl:value-of select="$version"/><xsl:value-of select="$lf"/>
    <xsl:text> *</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text> *</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text> * Generated code from "</xsl:text><xsl:value-of select="$filename"/><xsl:text>"</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text> *</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text> * Created: </xsl:text><xsl:value-of select="$date-created"/><xsl:value-of select="$lf"/>
    <xsl:text> * By:      </xsl:text><xsl:value-of select="$created-by"/><xsl:value-of select="$lf"/>
    <xsl:text> *</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text> */</xsl:text><xsl:value-of select="$lf"/>
    <xsl:text>#endregion</xsl:text><xsl:value-of select="$lf"/>
  </xsl:template>

  <xsl:template name="code-gen">

    <!-- TODO: supply code generation XSLT here -->

  </xsl:template>

</xsl:stylesheet>
