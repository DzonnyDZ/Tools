<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:cd="http://dzonny.cz/xml/schemas/CodeDom" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns:xs="http://www.w3.org/2001/XMLSchema" 
>

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

    <!--Generates file envelop (#If and #Region)-->
    <xsl:template match="/" xml:space="preserve">
        <xsl:call-template name="header-comment"/>
        <![CDATA[Imports System.Xml.Linq, <xmlns="http://dzonny.cz/xml/schemas/CodeDom">]]>
        Imports System.CodeDom
        #Region "Generated code"
        <xsl:if test="$namespace!=''" xml:space="preserve">Namespace  <xsl:value-of select="$namespace"/></xsl:if>
        <xsl:call-template name="code-gen"/>
        <xsl:if test="$namespace!=''" xml:space="preserve">End Namespace</xsl:if>
        #End Region
    </xsl:template>
    <!--Generates header comment-->
    <xsl:template name="header-comment" xml:space="preserve">
        ' GENERATED FILE -- DO NOT EDIT
        ' Generator: <xsl:value-of select="$generator"/>
        ' Version: <xsl:value-of select="$version"/>
        ' Generated code from "<xsl:value-of select="$filename"/>"
        ' Created: <xsl:value-of select="$date-created"/> by: <xsl:value-of select="$created-by"/>
    </xsl:template>

    <xsl:template name="code-gen" xml:space="preserve">
        Partial Class Xml2CodeDom
            <![CDATA[''' <summary>Contains definitions of XML elements used by <see cref="Xml2CodeDom"/> class</summary>]]>
            Private Class Names
                <![CDATA[''' <summary>This method actually does not exists which prevents this class of being instantiated</summary>]]>
                Private Partial Sub New()
                End Sub
                <xsl:for-each select="/xs:schema/xs:element | /xs:schema/xs:complexType[@name='PrimitiveExpression']/xs:complexContent/xs:extension/xs:choice/xs:element">
                    <![CDATA[''' <summary>Represents name of the &lt;]]><xsl:value-of select="@name"/><![CDATA[> element and ]]><xsl:value-of select="@name"/><![CDATA[ type</summary>]]>
                    Public Shared Readonly [<xsl:value-of select="@name"/>] As XName = &lt;<xsl:value-of select="@name"/>/&gt;.Name
                </xsl:for-each>
            End Class
            
            <!--#Region "Serialize"
            #Region "Callers"
            <xsl:for-each select="/xs:schema/xs:element[@name!='UserData']">
                <xsl:choose>
                    <xsl:when test="@type='xs:string'">
                        ''' &lt;summary>Serializes given string representing <xsl:value-of select="@name"/> as <see cref="XElement"/>&lt;/summary>
                        ''' &lt;param name="<xsl:value-of select="@name"/>">String to serialize&lt;/param>
                        ''' &lt;param name="ElementName">Name of element to be generated. If null, default name is used.&lt;/param>
                        ''' &lt;returns>&lt;see cref="XElement"/> representing serialized &lt;paramref name="<xsl:value-of select="@name"/>"/>&lt;/returns>
                        Private Function Serialize<xsl:value-of select="@name"/>(ByVal [<xsl:value-of select="@name"/>] As String, Optional ByVal ElementName As XName = Nothing) As XElement
                    </xsl:when>
                    <xsl:otherwise>
                        ''' &lt;summary>Serializes given &lt;see cref="Code<xsl:value-of select="@name"/>"/> as <see cref="XElement"/>&lt;/summary>
                        ''' &lt;param name="<xsl:value-of select="@name"/>">A &lt;see cref="Code<xsl:value-of select="@name"/>"/> to serialize&lt;/param>
                        ''' &lt;param name="ElementName">Name of element to be generated. If null, default name is used.&lt;/param>
                        ''' &lt;returns>&lt;see cref="XElement"/> representing serialized &lt;paramref name="<xsl:value-of select="@name"/>"/>&lt;/returns>
                        Private Function Serialize<xsl:value-of select="@name"/>(ByVal [<xsl:value-of select="@name"/>] As Code<xsl:value-of select="@name"/>, Optional ByVal ElementName As XName = Nothing) As XElement
                    </xsl:otherwise>
                </xsl:choose>
                    If ElementName Is Nothing Then ElementName = Names.<xsl:value-of select="@name"/>--><!--Dim Element = &lt;<xsl:value-of select="@name"/>/>
                    Serialize<xsl:value-of select="@name"/>Content(Element,[<xsl:value-of select="@name"/>])
                    Return Element--><!--
                End Function
            </xsl:for-each>
            #End Region
            --><!--#Region "Partial"
            <xsl:for-each select="/xs:schema/xs:element[@name!='UserData']">
                ''' &lt;summary>Serializes content of given <xsl:choose><xsl:when test="@type='xs:string'">String representing <xsl:value-of select="@name"/></xsl:when><xsl:otherwise>&lt;see cref="Code<xsl:value-of select="@name"/>"/></xsl:otherwise></xsl:choose> to given &lt;see cref="XElement"/>&lt;/summary>
                ''' &lt;param name="Element">&lt;see cref="XElement"/> to embded elements and attributes representing &lt;paramref name="<xsl:value-of select="@name"/>"/> into&lt;/param>
                ''' &lt;param name="<xsl:value-of select="@name"/>"><xsl:choose><xsl:when test="@type='xs:string'">String representing <xsl:value-of select="@name"/></xsl:when><xsl:otherwise>&lt;see cref="Code<xsl:value-of select="@name"/>"/></xsl:otherwise></xsl:choose> to be serialized&lt;/param>
                Private Partial Sub Serialize<xsl:value-of select="@name"/>Content(Element As XElement, [<xsl:value-of select="@name"/>] As <xsl:choose><xsl:when test="@type='xs:string'">String</xsl:when><xsl:otherwise>Code<xsl:value-of select="@name"/></xsl:otherwise></xsl:choose>)
                End Sub
            </xsl:for-each>
            #End Region--><!--
            #End Region-->
        End Class
        
        <!--<xsl:for-each select="/xs:schema/xs:complexType[substring(@name,string-length(@name) - 9)='Expression']">
            'Else If TypeOf Expression Is Code<xsl:value-of select="@name"/> Then : Return Serialize<xsl:value-of select="@name"/>(Expression)</xsl:for-each>-->
    </xsl:template>
</xsl:stylesheet>
