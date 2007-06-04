<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:I="http://codeplex.com/DTools/IPTCTags">

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
        <xsl:call-template name="Imports"/>
        <xsl:if test="$namespace!=''">
            <xsl:text>Namespace </xsl:text>
            <xsl:value-of select="$namespace"/>
            <xsl:call-template name="nl"/>
        </xsl:if>
        <xsl:text>#If Congig &lt;= </xsl:text>
        <xsl:value-of select="I:Root/@Stage"/>
        <xsl:if test="I:Root/@Stage!='Release'">
            <xsl:text> 'Stage: </xsl:text>
            <xsl:value-of select="I:Root/@Stage"/>
        </xsl:if>
        <xsl:call-template name="nl"/>
        <xsl:call-template name="code-gen"/>
        <xsl:text>#End If&#xD;&#xA;</xsl:text>
        <xsl:if test="$namespace!=''">
            <xsl:text>End Namespace</xsl:text>
            <xsl:call-template name="nl"/>
        </xsl:if>
    </xsl:template>
    <!--Generates end of line-->
    <xsl:template name="nl">
        <xsl:text xml:space="preserve">&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Generates header comment-->
    <xsl:template name="header-comment">
        <xsl:text>' GENERATED FILE -- DO NOT EDIT&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Generator: </xsl:text>
        <xsl:value-of select="$generator"/>
        <xsl:call-template name="nl"/>        
        <xsl:text>' Version: </xsl:text>
        <xsl:value-of select="$version"/>
        <xsl:call-template name="nl"/>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Generated code from "</xsl:text>
        <xsl:value-of select="$filename"/>
        <xsl:text>"&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Created: </xsl:text>
        <xsl:value-of select="$date-created"/>
        <xsl:call-template name="nl"/>
        <xsl:text>' By:</xsl:text>
        <xsl:value-of select="$created-by"/>
        <xsl:call-template name="nl"/>
        <xsl:text>'&#xD;&#xA;</xsl:text>
    </xsl:template>

    <xsl:template name="code-gen">
        <xsl:text>&#9;Partial Public Class IPTC&#xD;&#xA;</xsl:text>
        <xsl:call-template name="Tag-enums"/>
        <xsl:call-template name="Enums"/>
        <xsl:call-template name="sEnums"/>
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
    </xsl:template>
    
    <!--Generates imports-->
    <xsl:template name="Imports">
        <xsl:text>Imports System.ComponentModel&#xD;&#xA;</xsl:text>
        <xsl:text>Imports Tools.ComponentModelT&#xD;&#xA;</xsl:text>
        <xsl:text>Imports System.XML.Serialization&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Generates enums for tags-->
    <xsl:template name="Tag-enums">
        <xsl:text>#Region "Tag Enums"&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;summary>Numbers of IPTC records (groups of tags)&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;Public Enum RecordNumbers As Byte&#xD;&#xA;</xsl:text>
        <xsl:for-each select="/I:Root/I:record">
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:call-template name="Summary"/>
            <xsl:text>&#9;&#9;&#9;</xsl:text>
            <xsl:call-template name="DisplayName"/>
            <xsl:value-of select="@name"/>
            <xsl:text> = </xsl:text>
            <xsl:value-of select="@number"/>
            <xsl:call-template name="nl"/>
        </xsl:for-each>
        <xsl:text>&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
        <xsl:for-each select="/I:Root/I:record">
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:text>&#9;&#9;''' &lt;summary>Numbers of data sets (tags) inside record &lt;see cref="RecordNumbers.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>"/> (</xsl:text>
            <xsl:value-of select="@number"/>
            <xsl:text>)&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;Public Enum </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Tags As Byte&#xD;&#xA;</xsl:text>
            <xsl:for-each select="I:tag | I:group/I:tag">
                <xsl:sort data-type="number" order="ascending" select="@number"/>
                <xsl:call-template name="Summary"/>
                <xsl:text>&#9;&#9;&#9;''' &lt;remarks>See &lt;seealso cref="IPTC.</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>"/> for more info.&lt;/remarks>&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;</xsl:text>
                <xsl:if test="local-name(./..)='group'">
                    <xsl:text>&lt;EditorBrowsable(EditorBrowsableState.Advanced)> </xsl:text>
                </xsl:if>
                <xsl:call-template name="DisplayName"/>
                <xsl:call-template name="Category"/>
                <xsl:value-of select="@name"/>
                <xsl:text> = </xsl:text>
                <xsl:value-of select="@number"/>
                <xsl:call-template name="nl"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;End Enum&#xD;&#xA;</xsl:text>            
        </xsl:for-each>
        <xsl:text>&#9;&#9;''' &lt;summary>Gets Enum that contains list of tags for specific record (group of tags)&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="Record">Number of record to get enum for&lt;/param>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;exception name="InvalidEnumArgumentException">Value of &lt;paramref name="Record"/> is not member of &lt;see cref="RecordNumbers"/>&lt;/exception>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;Public Shared Function GetEnum(ByVal Record As RecordNumbers) As Type&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;Select Case Record&#xD;&#xA;</xsl:text>
        <xsl:for-each select="/I:Root/I:record">
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:text>&#9;&#9;&#9;&#9;Case RecordNumbers.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text> : Return GetType(</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Tags)&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>&#9;&#9;&#9;&#9;Case Else : Throw New InvalidEnumArgumentException("Record", Record, GetType(RecordNumbers))&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Select&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Function&#xD;&#xA;</xsl:text>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Creates <summary> from <desc>-->
    <xsl:template name="Tag-Summary">
        <xsl:text>&#9;&#9;&#9;''' &lt;summary></xsl:text>
        <xsl:value-of select="normalize-space(I:desc)" disable-output-escaping="yes"/>
        <xsl:text>&lt;/summary>&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Creates <remarks> from <remarks>-->
    <xsl:template name="Tag-Remarks">
        <xsl:if test="I:remarks">
            <xsl:text>&#9;&#9;&#9;''' &lt;remarks></xsl:text>
            <xsl:value-of select="normalize-space(I:remarks)" disable-output-escaping="yes"/>
            <xsl:text>&lt;/remarks>&#xD;&#xA;</xsl:text>
        </xsl:if>
    </xsl:template>
    <!--Creates <summary> from desc=""-->
    <xsl:template name="Attr-Summary">
        <xsl:text>&#9;&#9;&#9;''' &lt;summary></xsl:text>
        <xsl:call-template name="Amp2Entity">
            <xsl:with-param name="Text" select="normalize-space(@desc)"/>
        </xsl:call-template>
        <xsl:text>&lt;/summary>&#xD;&#xA;</xsl:text>    
    </xsl:template>
    <xsl:template name="Amp2Entity">
        <xsl:param name="Text"/>
        <xsl:if test="string-length($Text)>0">
            <xsl:choose>
                <xsl:when test="starts-with($Text,'&amp;') and not(starts-with($Text,'&amp;amp;'))">
                    <xsl:text>&amp;amp;</xsl:text>
                    <xsl:call-template name="Amp2Entity">
                        <xsl:with-param name="Text" select="substring-after($Text,'&amp;')"/>
                    </xsl:call-template>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="substring($Text,1,1)"/>
                    <xsl:call-template name="Amp2Entity">
                        <xsl:with-param name="Text" select="substring($Text,2)"/>
                    </xsl:call-template>
                </xsl:otherwise>
            </xsl:choose>            
        </xsl:if>        
    </xsl:template>
    <!--Universally creates <summary>-->
    <xsl:template name="Summary">
        <xsl:choose>
            <xsl:when test="I:desc">
                <xsl:call-template name="Tag-Summary"/>
            </xsl:when>
            <xsl:when test="@desc">
                <xsl:call-template name="Attr-Summary"/>
            </xsl:when>
        </xsl:choose>
    </xsl:template>
    <!--Creates XML-doc comments <summary> and <remarks>-->
    <xsl:template name="XML-Doc">
        <xsl:call-template name="Summary"/>
        <xsl:call-template name="Tag-Remarks"/>
    </xsl:template>
    <!--Renders DisplayNameAttribute-->
    <xsl:template name="DisplayName">
        <xsl:text>&lt;FieldDisplayName("</xsl:text>
        <xsl:value-of select="@human-name"/>
        <xsl:text>")> </xsl:text>
    </xsl:template>
    <!--Renders CategoryAttribute-->
    <xsl:template name="Category">
        <xsl:text>&lt;Category("</xsl:text>
        <xsl:value-of select="@human-name"/>
        <xsl:text>")> </xsl:text>
    </xsl:template>
    <!--Renders attributes-->
    <xsl:template name="Attributes">
        <xsl:if test="@attributes">
            <xsl:value-of select="@attributes"/>
            <xsl:text>&#32;</xsl:text>
        </xsl:if>
    </xsl:template>
    
    <!--generates all enums declared in XML-->
    <xsl:template name="Enums">
        <xsl:text>#Region "Enums"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:enum">
            <xsl:sort select="@name" order="ascending" data-type="text"/>
            <xsl:call-template name="XML-Doc"/>
            <xsl:text>&#9;&#9;</xsl:text>
            <xsl:call-template name="Attributes"/>
            <xsl:choose>
                <xsl:when test="(self::node()[@restrict] and boolean(@restrict)) or (not (self::node()[@restrict]))">
                    <xsl:text>&lt;Restrict(True)> </xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>&lt;Restrict(False)> </xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:if test="@type='SByte' or @type='UShort' or @type='UInteger' or @type='ULong'">
                <xsl:text>&lt;CLSCompliant(False)> </xsl:text>
            </xsl:if>
            <xsl:text>Public Enum </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text> As </xsl:text>
            <xsl:value-of select="@type"/>
            <xsl:call-template name="nl"/>
            <xsl:for-each select="I:item">
                <xsl:sort order="ascending" data-type="number" select="@value"/>
                <xsl:call-template name="XML-Doc"/>
                <xsl:text>&#9;&#9;&#9;</xsl:text>
                <xsl:call-template name="Attributes"/>
                <xsl:call-template name="DisplayName"/>
                <xsl:value-of select="@name"/>
                <xsl:text> = </xsl:text>
                <xsl:value-of select="@value"/>
                <xsl:call-template name="nl"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--generates all string enums declared in XML-->
    <xsl:template name="sEnums">
        <xsl:text>#Region "String enums"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:senum">
            <xsl:sort select="@name" order="ascending" data-type="text"/>
            <xsl:call-template name="XML-Doc"/>
            <xsl:text>&#9;&#9;</xsl:text>
            <xsl:call-template name="Attributes"/>
            <xsl:choose>
                <xsl:when test="(self::node()[@restrict] and boolean(@restrict)) or (not (self::node()[@restrict]))">
                    <xsl:text>&lt;Restrict(True)> </xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>&lt;Restrict(False)> </xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:if test="@type='SByte' or @type='UShort' or @type='UInteger' or @type='ULong'">
                <xsl:text>&lt;CLSCompliant(False)> </xsl:text>
            </xsl:if>
            <xsl:text>Public Enum </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:call-template name="nl"/>
            <xsl:for-each select="I:item">
                <xsl:sort order="ascending" data-type="number" select="@value"/>
                <xsl:call-template name="XML-Doc"/>
                <xsl:text>&#9;&#9;&#9;</xsl:text>
                <xsl:call-template name="Attributes"/>
                <xsl:call-template name="DisplayName"/>
                <xsl:text>&lt;XmlEnum("</xsl:text>
                <xsl:value-of select="@value"/>
                <xsl:text>")> </xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:call-template name="nl"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>
    
</xsl:stylesheet>


