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
        <xsl:call-template name="TagTypes"/>
        <xsl:call-template name="Groups"/>
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Generates imports-->
    <xsl:template name="Imports">
        <xsl:text>Imports System.ComponentModel&#xD;&#xA;</xsl:text>
        <xsl:text>Imports Tools.ComponentModelT&#xD;&#xA;</xsl:text>
        <xsl:text>Imports System.XML.Serialization&#xD;&#xA;</xsl:text>
        <xsl:text>Imports Tools.DataStructuresT.GenericT&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Generates enums for tags-->
    <xsl:template name="Tag-enums">
        <xsl:text>#Region "Tag Enums"&#xD;&#xA;</xsl:text>
        <!--RecordNumbers-->
        <xsl:text>&#9;&#9;''' &lt;summary>Numbers of IPTC records (groups of tags)&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;Public Enum RecordNumbers As Byte&#xD;&#xA;</xsl:text>
        <xsl:for-each select="/I:Root/I:record">
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:call-template name="Summary">
                <xsl:with-param name="Tab" select="3"/>
            </xsl:call-template>
            <xsl:text>&#9;&#9;&#9;</xsl:text>
            <xsl:call-template name="DisplayName"/>
            <xsl:value-of select="@name"/>
            <xsl:text> = </xsl:text>
            <xsl:value-of select="@number"/>
            <xsl:call-template name="nl"/>
        </xsl:for-each>
        <xsl:text>&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
        <!--Record-specific enums-->
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
                <xsl:call-template name="Summary">
                    <xsl:with-param name="Tab" select="3"/>
                </xsl:call-template>
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
        <!--GetEnum-->
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
        <!--DataSetIdentification-->
        <xsl:text>&#9;&#9;Partial Public Structure DataSetIdentification&#xD;&#xA;</xsl:text>
        <!--DataSetIdentification - shared properties-->
        <xsl:for-each select="I:Root/I:record/I:tag | I:Root/I:record/I:group/I:tag">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="@number"/>
            <xsl:call-template name="XML-Doc">
                <xsl:with-param name="Tab" select="3"/>
            </xsl:call-template>
            <xsl:if test="local-name(parent::node())='group'">
                <xsl:text>&#9;&#9;&#9;&lt;EditorBrowsable(EditorBrowsableState.Advanced)> _&#xD;&#xA;</xsl:text>
            </xsl:if>
            <xsl:text>&#9;&#9;&#9;Public Shared ReadOnly Property </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text> As DataSetIdentification&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Return New DataSetIdentification(RecordNumbers.</xsl:text>
            <xsl:value-of select="ancestor::I:record/@name"/>
            <xsl:text>, </xsl:text>
            <xsl:value-of select="ancestor::I:record/@name"/>
            <xsl:text>tags.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>)&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;End Property&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>&#9;&#9;&#9;''' &lt;summary>Returns all known data sets&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;''' &lt;param name="Hidden">Returns also datasets that are within groups&lt;/param>&#xD;&#xA;</xsl:text>
        <!--DataSetIndentificatio.KnownDataSets-->
        <xsl:text>&#9;&#9;&#9;Public Shared Function KnownDataSets(Optional ByVal Hidden As Boolean = False) As DataSetIdentification()&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;If Hidden Then&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Return New DataSetIdentification(){</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:tag | I:Root/I:record/I:group/I:tag">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="@number"/>
            <xsl:if test="position()!=1">
                <xsl:text>, </xsl:text>
            </xsl:if>
            <xsl:value-of select="@name"/>
        </xsl:for-each>
        <xsl:text>}&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;Else&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Return New DataSetIdentification(){</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:tag">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="@number"/>
            <xsl:if test="position()!=1">
                <xsl:text>, </xsl:text>
            </xsl:if>
            <xsl:value-of select="@name"/>
        </xsl:for-each>
        <xsl:text>}&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;End If&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Function&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Structure&#xD;&#xA;</xsl:text>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Creates <summary> from <desc>-->
    <xsl:template name="Tag-Summary">
        <xsl:param name="Tab"/>
        <xsl:call-template name="Tabs">
            <xsl:with-param name="Count" select="$Tab"/>
        </xsl:call-template>
        <xsl:text>''' &lt;summary></xsl:text>
        <xsl:value-of select="normalize-space(I:desc)" disable-output-escaping="yes"/>
        <xsl:text>&lt;/summary>&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Generates given number of tabs-->
    <xsl:template name="Tabs">
        <xsl:param name="Count"/>
        <xsl:if test="$Count>0">
            <xsl:text>&#9;</xsl:text>
            <xsl:call-template name="Tabs">
                <xsl:with-param name="Count" select="$Count - 1"/>
            </xsl:call-template>
        </xsl:if>
    </xsl:template>
    <!--Creates <remarks> from <remarks>-->
    <xsl:template name="Tag-Remarks">
        <xsl:param name="Tab"/>
        <xsl:if test="I:remarks">
            <xsl:call-template name="Tabs">
                <xsl:with-param name="Count" select="$Tab"/>
            </xsl:call-template>
            <xsl:text>''' &lt;remarks></xsl:text>
            <xsl:value-of select="normalize-space(I:remarks)" disable-output-escaping="yes"/>
            <xsl:text>&lt;/remarks>&#xD;&#xA;</xsl:text>
        </xsl:if>
    </xsl:template>
    <!--Creates <summary> from desc=""-->
    <xsl:template name="Attr-Summary">
        <xsl:param name="Tab"/>
        <xsl:call-template name="Tabs">
            <xsl:with-param name="Count" select="$Tab"/>
        </xsl:call-template>
        <xsl:text>''' &lt;summary></xsl:text>
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
        <xsl:param name="Tab"/>
        <xsl:choose>
            <xsl:when test="I:desc">
                <xsl:call-template name="Tag-Summary">
                    <xsl:with-param name="Tab" select="$Tab"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="@desc">
                <xsl:call-template name="Attr-Summary">
                    <xsl:with-param name="Tab" select="$Tab"/>
                </xsl:call-template>
            </xsl:when>
        </xsl:choose>
    </xsl:template>
    <!--Creates XML-doc comments <summary> and <remarks>-->
    <xsl:template name="XML-Doc">
        <xsl:param name="Tab"/>
        <xsl:call-template name="Summary">
            <xsl:with-param name="Tab" select="$Tab"/>
        </xsl:call-template>
        <xsl:call-template name="Tag-Remarks">
            <xsl:with-param name="Tab" select="$Tab"/>
        </xsl:call-template>
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

    <!--Generates all enums declared in XML-->
    <xsl:template name="Enums">
        <xsl:text>#Region "Enums"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:enum">
            <xsl:sort select="@name" order="ascending" data-type="text"/>
            <xsl:call-template name="XML-Doc">
                <xsl:with-param name="Tab" select="2"/>
            </xsl:call-template>
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
                <xsl:call-template name="XML-Doc">
                    <xsl:with-param name="Tab" select="3"/>
                </xsl:call-template>
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

    <!--Generates all string enums declared in XML-->
    <xsl:template name="sEnums">
        <xsl:text>#Region "String enums"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:senum">
            <xsl:sort select="@name" order="ascending" data-type="text"/>
            <xsl:call-template name="XML-Doc">
                <xsl:with-param name="Tab" select="2"/>
            </xsl:call-template>
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
                <xsl:call-template name="XML-Doc">
                    <xsl:with-param name="Tab" select="3"/>
                </xsl:call-template>
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

    <!--Generates GetTag functions-->
    <xsl:template name="TagTypes">
        <xsl:text>#Region "Tag types"&#xD;&#xA;</xsl:text>
        <!--Common GetTag-->
        <xsl:text>&#9;&#9;''' &lt;summary>Gets details about tag format by tag record and number&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="Record">Recor number&lt;/param>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="TagNumber">Number of tag within &lt;paramref name="Record"/>&lt;/param>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;exception cref="InvalidEnumArgumentException">&lt;paramref name="Record"/> is not member of &lt;see cref="RecordNumbers"/> -or- &lt;paramref name="TagNumber"/> is not tag within &lt;paramref name="record"/>&lt;/exception/>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;Public Shared Function GetTag(ByVal Record As RecordNumbers, TagNumber As Byte) As IPTCTag&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;Select Case Record&#xD;&#xA;</xsl:text>
        <xsl:for-each select="/I:Root/I:record">
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:text>&#9;&#9;&#9;&#9;Case RecordNumbers.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:call-template name="nl"/>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Select Case TagNumber&#xD;&#xA;</xsl:text>
            <xsl:for-each select="I:tag">
                <xsl:sort data-type="number" order="ascending" select="@number"/>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Case </xsl:text>
                <xsl:value-of select="ancestor::I:record/@name"/>
                <xsl:text>Tags.</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text> : Return New IPTCTag(Number:=</xsl:text>
                <xsl:value-of select="@number"/>
                <xsl:text>, Record:=</xsl:text>
                <xsl:value-of select="./../@number"/>
                <xsl:text>, Name:="</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>", HumanName:="</xsl:text>
                <xsl:value-of select="normalize-space(@human-name)"/>
                <xsl:text>", Type:=IPTCTypes.</xsl:text>
                <xsl:call-template name="GetType">
                    <xsl:with-param name="Type" select="@type"/>
                </xsl:call-template>
                <xsl:text>, Mandatory:=</xsl:text>
                <xsl:value-of select="boolean(@mandatory)"/>
                <xsl:text>, Repeatable:=</xsl:text>
                <xsl:value-of select="boolean(@repeatable)"/>
                <xsl:text>, Length:=</xsl:text>
                <xsl:value-of select="@length"/>
                <xsl:text>, Fixed:=</xsl:text>
                <xsl:value-of select="boolean(@fixed)"/>
                <xsl:text>, Category:="</xsl:text>
                <xsl:value-of select="normalize-space(@category)"/>
                <xsl:text>", Description:="</xsl:text>
                <xsl:call-template name="SummaryString">
                    <xsl:with-param name="Node" select="I:desc"/>
                </xsl:call-template>
                <xsl:text>"</xsl:text>
                <xsl:if test="@enum">
                    <xsl:text>, [Enum]:=GetType(</xsl:text>
                    <xsl:value-of select="@enum"/>
                    <xsl:text>)</xsl:text>
                </xsl:if>
                <xsl:text>, Lock:=True)&#xD;&#xA;</xsl:text>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Tags))&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;End Select&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>&#9;&#9;&#9;&#9;Case Else : Throw New InvalidEnumArgumentException("Record",Record,GetType(RecordNumbers))&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Select&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Function&#xD;&#xA;</xsl:text>
        <!--Specialized GetTag-s-->
        <xsl:for-each select="/I:Root/I:record">
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:text>&#9;&#9;''' &lt;summary>Get details about tag format for tag from record &lt;see cref="RecordNumbers.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>"/>&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;''' &lt;param name="Number">Number of tag within record&lt;/param></xsl:text>
            <xsl:text>&#9;&#9;''' &lt;exception cref="InvalidEnumargumentException">&lt;paramref name="Number"/> is not member of &lt;see cref="</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Tags"/>&lt;/exception>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;Public Shared Function GetTag(ByVal Number As </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Tags) As IPTCTag&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;Return GetTag(RecordNumbers.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>, Number)&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;End Function&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Translates IPTC tag type name used in XML into IPTC tag type name used in VB-->
    <xsl:template name="GetType">
        <xsl:param name="Type"/>
        <xsl:value-of select="translate($Type,'-','_')"/>
    </xsl:template>
    <!--Translates docummentation into simple string-->
    <xsl:template name="SummaryString">
        <xsl:param name="Node"/>
        <xsl:variable name="Ret">
            <xsl:for-each select="$Node/child::node()">
                <xsl:choose>
                    <xsl:when test="string(.)='' and @cref">
                        <xsl:value-of select="@cref"/>
                    </xsl:when>
                    <xsl:when test="string(.)='' and @name">
                        <xsl:value-of select="@name"/>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:value-of select="string(.)"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each>
        </xsl:variable>
        <xsl:value-of select="normalize-space($Ret)"/>
    </xsl:template>

    <!--Generates tag groups-->
    <xsl:template name="Groups">
        <xsl:text>#Region "Groups"&#xD;&#xA;</xsl:text>
        <!--Groups-->
        <xsl:text>&#9;&#9;''' &lt;summary>Groups of tags&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;Public Enum Groups&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:group">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="I:tag[1]/@number"/>
            <xsl:call-template name="Summary">
                <xsl:with-param name="Tab" select="3"/>
            </xsl:call-template>
            <xsl:text>&#9;&#9;&#9;</xsl:text>
            <xsl:call-template name="Category"/>
            <xsl:call-template name="DisplayName"/>
            <xsl:value-of select="@name"/>
            <xsl:call-template name="nl"/>
        </xsl:for-each>
        <xsl:text>&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
        <!--GetGroup-->
        <xsl:text>&#9;&#9;''' &lt;summary>Gets information about known group of IPTC tags&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="Group">Code of group to get information about&lt;/param>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;Public Shared Function GetGroup(ByVal Group As Groups) As GroupInfo&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;Select Case Group&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:group">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="I:tag[1]/@number"/>
            <xsl:text>&#9;&#9;&#9;&#9;Case Groups.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text> : Return New GroupInfo("</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>", "</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>", Groups.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>, GetType(</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group), "</xsl:text>
            <xsl:value-of select="@category"/>
            <xsl:text>", "</xsl:text>
            <xsl:call-template name="SummaryString">
                <xsl:with-param name="Node" select="I:desc"/>
            </xsl:call-template>
            <xsl:text>", </xsl:text>
            <xsl:value-of select="boolean(@mandatory)"/>
            <xsl:text>, </xsl:text>
            <xsl:value-of select="boolean(@repeatable)"/>
            <xsl:for-each select="I:tag">
                <xsl:sort data-type="number" order="ascending" select="@number"/>
                <xsl:text>, GetTag(RecordNumbers.</xsl:text>
                <xsl:value-of select="../../@name"/>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="../../@name"/>
                <xsl:text>Tags.</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>)</xsl:text>
            </xsl:for-each>
            <xsl:text>)&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>&#9;&#9;&#9;&#9;Case Else : Throw New InvalidEnumArgumentException("Group", Group, GetType(Groups))&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Select&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Function&#xD;&#xA;</xsl:text>
        <!--Groups' classes-->
        <xsl:for-each select="I:Root/I:record/I:group">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="I:tag[1]/@number"/>
            <xsl:text>&#9;&#9;Partial Public Class </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group : Inherits Group&#xD;&#xA;</xsl:text>
            <!--TODO: Generate properties and CTor-->
            <xsl:text>&#9;&#9;&#9;'TODO: Properties and CTor&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;End Class&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Generates property-->
    <xsl:template name="Property">
        <xsl:call-template name="XML-Doc"/>
        <xsl:call-template name="Category"/>
        <xsl:call-template name="DisplayName"/>
        <xsl:call-template name="Attributes"/>
        <xsl:text>&#9;&#9;Public Property </xsl:text>
        <xsl:value-of select="@name"/>
        <xsl:variable name="Type">
            <xsl:call-template name="UnderlyingType">
                <xsl:with-param name="type" select="@type"/>
                <xsl:with-param name="enum" select="@enum"/>
                <xsl:with-param name="len" select="@length"/>
                <xsl:with-param name="param" select="'Value being set'"/>
                <xsl:with-param name="variable" select="'value'"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:text> As </xsl:text>
        <xsl:value-of select="$Type"/>
        <xsl:call-template name="nl"/>
        <xsl:text>&#9;&#9;End Property</xsl:text>
    </xsl:template>

    <!--getenretes underlying type from IPTC type-->
    <!--Returns
        <x:type type="" verify="" exception="" to-byte="" from-byte="">
    -->
    <!--TODO:Completely rebuild way how properties are stored and retrieved!!!-->
    <xsl:template name="UnderlyingType">
        <xsl:param name="type"/><!--IPTC type-->
        <xsl:param name="enum"/><!--Optional enum name (required for enum types)-->
        <xsl:param name="len"/><!--Lenght-->
        <xsl:param name="param"/><!--Reference to parameter for exception XML-doc-->
        <xsl:param name="variable"/><!--name of variable to text-->
        <xsl:param name="byte-data"/><!--Name of variable where the byte data are stored in-->
        <xsl:element name="type" namespace="x">
            <xsl:choose>
                <!--Enum-binary-->
                <xsl:when test="$type='Enum-binary'">
                    <xsl:attribute name="type">
                        <xsl:value-of select="@enum"/>
                    </xsl:attribute>
                    <xsl:if test="I:Root/I:record/I:enum[@name=$enum]/@restrict">
                        <xsl:attribute name="verify">
                            <xsl:text>VerifyNumericEnum(</xsl:text>
                            <xsl:value-of select="$variable"/>
                            <xsl:text>)</xsl:text>
                        </xsl:attribute>
                        <xsl:attribute name="exception">
                            <xsl:text>''' &lt;exception cref="InvalidEnumArgumentException"></xsl:text>
                            <xsl:value-of select="$param"/>
                            <xsl:text> is not member of &lt;see cref="</xsl:text>
                            <xsl:value-of select="$enum"/>
                            <xsl:text>/>&lt;/exception></xsl:text>
                        </xsl:attribute>
                    </xsl:if>
                    <xsl:attribute name="from-byte">
                        <xsl:text>UIntFromBytes(</xsl:text>
                        <xsl:value-of select="len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$byte-data"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="to-byte">
                        <xsl:text>ToBytes(</xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, CULng(</xsl:text>
                        <xsl:value-of select="$variable"/>
                        <xsl:text>))</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Enum-NumChar-->
                <xsl:when test="$type='Enum-NumChar'">
                    <xsl:attribute name="type">
                        <xsl:value-of select="@enum"/>
                    </xsl:attribute>
                    <xsl:if test="I:Root/I:record/I:enum[@name=$enum]/@restrict">
                        <xsl:attribute name="verify">
                            <xsl:text>VerifyNumericEnum(</xsl:text>
                            <xsl:value-of select="$variable"/>
                            <xsl:text>)</xsl:text>
                        </xsl:attribute>
                        <xsl:attribute name="exception">
                            <xsl:text>''' &lt;exception cref="InvalidEnumArgumentException"></xsl:text>
                            <xsl:value-of select="$param"/>
                            <xsl:text> is not member of &lt;see cref="</xsl:text>
                            <xsl:value-of select="$enum"/>
                            <xsl:text>/>&lt;/exception></xsl:text>
                        </xsl:attribute>
                    </xsl:if>
                    <xsl:attribute name="from-byte">
                        <xsl:text>NumCharFromBytes(</xsl:text>
                        <xsl:value-of select="$byte-data"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="to-byte">
                        <xsl:text>ToBytes(</xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>,CDec(</xsl:text>
                        <xsl:value-of select="$variable"/>
                        <xsl:text>))</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--StringEnum-->
                <xsl:when test="$type='StringEnum'">
                    <!--TODO:-->
                    <xsl:choose>
                        <!--Only enumerated values are possible-->
                        <xsl:when test="I:Root/I:record/I:enum[@name=$enum]/@restrict">
                            <xsl:attribute name="type">
                                <xsl:value-of select="@enum"/>
                            </xsl:attribute>
                            <xsl:attribute name="verify">
                                <xsl:text>VerifyNumericEnum(</xsl:text>
                                <xsl:value-of select="$variable"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="exception">
                                <xsl:text>''' &lt;exception cref="InvalidEnumArgumentException"></xsl:text>
                                <xsl:value-of select="$param"/>
                                <xsl:text> is not member of &lt;see cref="</xsl:text>
                                <xsl:value-of select="$enum"/>
                                <xsl:text>/>&lt;/exception></xsl:text>
                            </xsl:attribute>
                        </xsl:when>
                        <!--All values are possible-->
                        <xsl:otherwise>
                            <xsl:attribute name="type">
                                <xsl:text>T1orT2(Of </xsl:text>
                                <xsl:value-of select="@enum"/>
                                <xsl:text>, String)</xsl:text>
                            </xsl:attribute>
                            
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:when>
                <!--UnsignedBinaryNumber-->
                <xsl:when test="$type='UnsignedBinaryNumber'">
                    <xsl:attribute name="type">
                        <xsl:text>ULong</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Boolean-binary-->
                <xsl:when test="$type='Boolean-binary'">
                    <xsl:attribute name="type">
                        <xsl:text>Boolean</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--UShort-binary-->
                <xsl:when test="$type='UShort-binary'">
                    <xsl:attribute name="type">
                        <xsl:text>UShort</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--NumericChar-->
                <xsl:when test="$type='NumericChar'">
                    <xsl:choose>
                        <xsl:when test="$len=0 or $len>19">
                            <xsl:attribute name="type">
                                <xsl:text>Decimal</xsl:text>
                            </xsl:attribute>                            
                        </xsl:when>
                        <xsl:when test="$len&lt;=2">
                            <xsl:attribute name="type">
                                <xsl:text>Byte</xsl:text>
                            </xsl:attribute>
                        </xsl:when>
                        <xsl:when test="$len&lt;=4">
                            <xsl:attribute name="type">
                                <xsl:text>Short</xsl:text>
                            </xsl:attribute>
                        </xsl:when>
                        <xsl:when test="$len&lt;=9">
                            <xsl:attribute name="type">
                                <xsl:text>Integer</xsl:text>
                            </xsl:attribute>
                        </xsl:when>
                        <xsl:when test="$len&lt;=19">
                            <xsl:attribute name="type">
                                <xsl:text>Long</xsl:text>
                            </xsl:attribute>
                        </xsl:when>
                    </xsl:choose>
                </xsl:when>
                <!--GraphicCharacters-->
                <xsl:when test="$type='GraphicCharacters'">
                    <xsl:attribute name="type">
                        <xsl:text>String</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--TextWithSpaces-->
                <xsl:when test="$type='TextWithSpaces'">
                    <xsl:attribute name="type">
                        <xsl:text>String</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Text-->
                <xsl:when test="$type='Text'">
                    <xsl:attribute name="type">
                        <xsl:text>String</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Alpha-->
                <xsl:when test="$type='Alpha'">
                    <xsl:attribute name="type">
                        <xsl:text>String</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--BW460-->
                <xsl:when test="$type='BW460'">
                    <xsl:attribute name="type">
                        <xsl:text>Drawing.Bitmap</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--CCYYMMDD-->
                <xsl:when test="$type='CCYYMMDD'">
                    <xsl:attribute name="type">
                        <xsl:text>Date</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="verify">
                        <xsl:value-of select="$variable"/>
                        <xsl:text> = New Date(</xsl:text>
                        <xsl:value-of select="$variable"/>
                        <xsl:text>.Year, </xsl:text>
                        <xsl:value-of select="$variable"/>
                        <xsl:text>.Month, </xsl:text>
                        <xsl:value-of select="$variable"/>
                        <xsl:text>.Day)&#xD;&#xA;</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--CCYYMMDDOmmitable-->
                <xsl:when test="$type='CCYYMMDDOmmitable'">
                    <xsl:attribute name="type">
                        <xsl:text>OmmitableDate</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--HHMMSS-HHMM-->
                <xsl:when test="$type='HHMMSS-HHMM'">
                    <xsl:attribute name="type">
                        <xsl:text>Time</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--ByteArray-->
                <xsl:when test="$type='ByteArray'">
                    <xsl:attribute name="type">
                        <xsl:text>Byte()</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--UNO-->
                <xsl:when test="$type='UNO'">
                    <xsl:attribute name="type">
                        <xsl:text>UNO</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Num2-Str-->
                <xsl:when test="$type='Num2-Str'">
                    <xsl:attribute name="type">
                        <xsl:text>NumStr2</xsl:text>
                    </xsl:attribute>
                    <!--TODO:verify & exception, enum-->
                </xsl:when>
                <!--Num3-Str-->
                <xsl:when test="$type='Num3-Str'">
                    <xsl:attribute name="type">
                        <xsl:text>NumStr3</xsl:text>
                    </xsl:attribute>
                    <!--TODO:verify & exception, enum-->
                </xsl:when>
                <!--SubjectReference-->
                <xsl:when test="$type='SubjectReference'">
                    <xsl:attribute name="type">
                        <xsl:text>SubjectReference</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--ImageType-->
                <xsl:when test="$type='ImageType'">
                    <xsl:attribute name="type">
                        <xsl:text>ImageType</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--AudioType-->
                <xsl:when test="$type='AudioType'">
                    <xsl:attribute name="type">
                        <xsl:text>AudioType</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--HHMMSS-->
                <xsl:when test="$type='HHMMSS'">
                    <xsl:attribute name="type">
                        <xsl:text>TimeSpan</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="verify">
                        <xsl:text>If </xsl:text>
                        <xsl:value-of select="$variable"/>
                        <xsl:text> &lt; TimeSpan.Zero OrElse </xsl:text>
                        <xsl:value-of select="$variable"/>
                        <xsl:text>.TotalDays > 1 Then Throw New ArgumentException("HHMMSS type can be only non-negative TimeSpan shorter than 1 day")&#xD;&#xA;</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="exception">
                        <xsl:text>''' &lt;exception cref="ArgumentException"></xsl:text>
                        <xsl:value-of select="$param"/>
                        <xsl:text> is less than &lt;see cref="TimeSpan.zero"/> or its &lt;see cref="TimeSpan.TotalDays"/> is greater than or equal to 1&lt;exception/></xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:message>
                        Unknown type <xsl:value-of select="@type"/>
                    </xsl:message>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:element>
    </xsl:template>
</xsl:stylesheet>


