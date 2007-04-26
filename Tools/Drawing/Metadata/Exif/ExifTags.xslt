<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:et="http://codeplex.com/DTools/ExifTags">

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
Namespace <xsl:value-of select="$namespace"/>
        </xsl:if>
        <xsl:call-template name="code-gen"/>
        <xsl:if test="$namespace!=''">
End Namespace
        </xsl:if>
    </xsl:template>

    <xsl:template name="header-comment">
' GENERATED FILE -- DO NOT EDIT
' 
' Generator: <xsl:value-of select="$generator"/>
' Version: <xsl:value-of select="$version"/>
'
'
' Generated code from "<xsl:value-of select="$filename"/>"
'
' Created: <xsl:value-of select="$date-created"/>
' By: <xsl:value-of select="$created-by"/>
'

    </xsl:template>

    <xsl:template name="code-gen">
    Partial Public Class <xsl:value-of select="$classname"/>
        <xsl:call-template name="Tag-enums"/>
        <xsl:call-template name="Tag-class"/>
        <xsl:call-template name="Tag-types"/>
    End Class
    </xsl:template>

    <xsl:template name="Tag-enums">
        <xsl:if test="/et:Root/et:Group[@IFD='IFD']">
        &lt;CLSCompliant(False)> Public Enum IFDTags As UShor
            <xsl:for-each select="/et:Root/et:Group[@IFD='IFD']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
        End Enum
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='Exif']">
        &lt;CLSCompliant(False)> Public Enum ExifTags As UShort
            <xsl:for-each select="/et:Root/et:Group[@IFD='Exif']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
        End Enum
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='GPS']">
        &lt;CLSCompliant(False)> Public Enum GPSTags As UShort
            <xsl:for-each select="/et:Root/et:Group[@IFD='GPS']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
        End Enum
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='Interop']">
        &lt;CLSCompliant(False)> Public Enum InteropTags As UShort
            <xsl:for-each select="/et:Root/et:Group[@IFD='Interop']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
        End Enum
        </xsl:if>
    </xsl:template>
    <xsl:template name="Tag-enum-content">
#Region "<xsl:value-of select="@Name"/>"
        <xsl:for-each select="et:Tag">
            ''' &lt;summary><xsl:value-of select="et:summary"/>&lt;/summary>
            <xsl:value-of select="@Name"/> = &amp;h<xsl:value-of select="@Tag"/>
        </xsl:for-each>
#End Region
    </xsl:template>

    <xsl:template name="Tag-class">
        <![CDATA[
        Partial Public Class ExifTag
            ''' <summary>Tag number of this tag</summary>
            <CLSCompliant(False)> Public ReadOnly Tag As UShort
            ''' <summary>Number of components of this tag</summary>
            ''' <remarks>Can be 0 if number of components is not constant</remarks>
            Public ReadOnly Components As Integer
            ''' <summary>Type od data stored in this tag</summary>
            ''' <remarks>The most preffered and the widest datatype is listed as first</remarks>
            <CLSCompliant(False)> Public ReadOnly DataTypes As Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes()
            ''' <sumary>CTor</summary>
            ''' <param name="Number">Tag number</param>
            ''' <param name="Componets">Number of components</param>
            ''' <param name="DataTypes">Possible datatypes of this tag. The most preffered and the widest data type must be at first index</param>
            <CLSCompliant(False)> Public Sub New(ByVal Number As UShort, ByVal Components As Integer, ParamArray ByVal DataTypes As Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes())
                Me.Tag = Number
                Me.Components = Components
                Me.DataTypes = DataTypes
            End Sub
        End Class
        ]]>
    </xsl:template>

    <xsl:template name="Tag-types">
        <xsl:if test="/et:Root/et:Group[@IFD='IFD']">
        Public NotInheritable Class MainIFD
            <xsl:for-each select="/et:Root/et:Group[@IFD='IFD']">
                <xsl:call-template name="Tag-type-content"/>
            </xsl:for-each>
        End Class
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='Exif']">
        Public NotInheritable Class ExifIFD
            <xsl:for-each select="/et:Root/et:Group[@IFD='Exif']">
                <xsl:call-template name="Tag-type-content"/>
            </xsl:for-each>
        End Class
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='GPS']">
        Public NotInheritable Class GPSIFD
            <xsl:for-each select="/et:Root/et:Group[@IFD='GPS']">
                <xsl:call-template name="Tag-type-content"/>
            </xsl:for-each>
        End Class
        </xsl:if>
        <xsl:if test="/et:Root/et:Group[@IFD='Interop']">
        Public NotInheritable Class InteropIFD
            <xsl:for-each select="/et:Root/et:Group[@IFD='Interop']">
                <xsl:call-template name="Tag-type-content"/>
            </xsl:for-each>
        End Class
        </xsl:if>
    </xsl:template>
    <xsl:template name="Tag-type-content">
#Region "<xsl:value-of select="@Name"/>"
        <xsl:for-each select="et:Tag">
            ''' &lt;summary>Type of  <xsl:value-of select="et:summary"/> tag&lt;/summary>
            Public Shared ReadOnly Property TypeOf<xsl:value-of select="@Name"/>
                Get
                    Return New ExifTag(&amp;h<xsl:value-of select="@Tag"/>, <xsl:value-of select="@Components"/>
            <xsl:for-each select="et:Type">
                <xsl:text>, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.</xsl:text>
                <xsl:value-of select="."/>
            </xsl:for-each>
            <xsl:text>)</xsl:text>
                End Get
            End Property

            ''' &lt;summary><xsl:value-of select="et:summary"/>&lt;/summary>
            Public Property <xsl:value-of select="@Name"/> As <xsl:call-template name="type"/>
                Get
                    Return Me(&amp;h<xsl:value-of select="@Tag"/>)
                End Get
                Set
                    Me(&amp;h<xsl:value-of select="@Tag"/>) = valu
                End Set
            End Property
            <xsl:if test="enum">
                <xsl:call-template name="enum"/>
            </xsl:if>
        </xsl:for-each>
#End Region
    </xsl:template>

    <xsl:template name="type">
        <xsl:if test="et:enum">
            <xsl:value-of select="@Name"/>
            <xsl:text>Values</xsl:text>
            <xsl:if test="@Components!=1">
                <xsl:text>()</xsl:text>
            </xsl:if>
        </xsl:if>
        <xsl:if test="count(et:enum)=0">
            <xsl:choose>
                <xsl:when test="et:Type[0]='ASCII'">
                    <xsl:if test="@Components=1">
                        <xsl:text>Char</xsl:text>
                    </xsl:if>
                    <xsl:if test="@Components!=1">
                        <xsl:text>String</xsl:text>
                    </xsl:if>
                </xsl:when>
                <xsl:when test="et:Type[0]='SRational'">
                    <xsl:text>Tools.Drawing.Metadata.SRational</xsl:text>
                    <xsl:if test="@Components!=1">
                        <xsl:text>()</xsl:text>
                    </xsl:if>
                </xsl:when>
                <xsl:when test="et:Type[0]='URational'">
                    <xsl:text>Tools.Drawing.Metadata.URational</xsl:text>
                    <xsl:if test="@Components!=1">
                        <xsl:text>()</xsl:text>
                    </xsl:if>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="@Type"/>
                    <xsl:if test="@Components!=1">
                        <xsl:text>()</xsl:text>
                    </xsl:if>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:if>
    </xsl:template>

    <xsl:template name="enum">
        <xsl:text>Public Enum </xsl:text>
        <xsl:value-of select="@Name"/>
        <xsl:text>Values&#10;&#13;</xsl:text>
        <xsl:for-each select="et:enum/et:item">
            <xsl:value-of select="@name"/>
            <xsl:text> = </xsl:text>
            <xsl:value-of select="@value"/>
            <xsl:text>&#10;&#13;</xsl:text>
        </xsl:for-each>
        <xsl:text>End Enum&#10;&#13;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
