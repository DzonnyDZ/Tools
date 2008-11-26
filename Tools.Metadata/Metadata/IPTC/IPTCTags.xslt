<?xml version="1.0" encoding="UTF-8" ?>
<!--
This XSLT transform file is used to transform IPTCTags.xml to IPTCTags.vb.
This should be tested and should work with current IPTCTags.xml, but it cannot be guaranteed that it will work correctly with any valid IPTCTags.xsd instance.
-->
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:I="http://codeplex.com/DTools/IPTCTags"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
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

    <!--Header templates:_-->
    <!--Main template-->
    <xsl:template match="/">
        <xsl:call-template name="header-comment"/>
        <xsl:text>'Localize: This auto-generated file was skipped during localization!&#xD;&#xA;</xsl:text>
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
        <xsl:text>'Localize: IPTC needs localization of Decriptions, DisplayNames and error messages&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Runs other templates-->
    <xsl:template name="code-gen">
        <!--<xsl:text>&#9;Partial Public Class IPTC&#xD;&#xA;</xsl:text>-->
        <xsl:call-template name="Tag-enums"/>
        <xsl:call-template name="Enums"/>
        <xsl:call-template name="sEnums"/>
        <xsl:call-template name="TagTypes"/>
        <xsl:call-template name="Groups"/>
        <xsl:call-template name="Properties"/>
        <!--<xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>-->
    </xsl:template>
    <!--Generates imports-->
    <xsl:template name="Imports">
        <xsl:text>Imports System.ComponentModel&#xD;&#xA;</xsl:text>
        <xsl:text>Imports Tools.ComponentModelT&#xD;&#xA;</xsl:text>
        <xsl:text>Imports System.XML.Serialization&#xD;&#xA;</xsl:text>
        <xsl:text>Imports Tools.DataStructuresT.GenericT&#xD;&#xA;</xsl:text>
        <xsl:text>Imports Tools.DrawingT.DesignT&#xD;&#xA;</xsl:text>
        <xsl:text>Imports Tools.MetadataT.IptcT.IptcDataTypes&#xD;&#xA;</xsl:text>
        <xsl:text>Imports Tools.MetadataT.IptcT.Iptc&#xD;&#xA;</xsl:text>
    </xsl:template>
    
    <!--Main content-creating templates:-->
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
        <xsl:text>&#9;Partial Class Iptc&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;summary>Gets Enum that contains list of tags for specific record (group of tags)&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="Record">Number of record to get enum for&lt;/param>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;exception cref="InvalidEnumArgumentException">Value of &lt;paramref name="Record"/> is not member of &lt;see cref="RecordNumbers"/>&lt;/exception>&#xD;&#xA;</xsl:text>
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
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
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
            <xsl:text>&#9;&#9;&#9;&#9;&lt;DebuggerStepThrough()> Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Return New DataSetIdentification(RecordNumbers.</xsl:text>
            <xsl:value-of select="ancestor::I:record/@name"/>
            <xsl:text>, </xsl:text>
            <xsl:value-of select="ancestor::I:record/@name"/>
            <xsl:text>tags.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>, "</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>", "</xsl:text>
            <xsl:value-of select="@human-name"/>
            <xsl:text>"</xsl:text>
            <!--<xsl:if test="local-name(parent::node())='group'">
                <xsl:text>, Group:=GroupInfo.</xsl:text>
                <xsl:value-of select="parent::I:group/@name"/>
            </xsl:if>-->
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
    <!--Generates all enums declared in XML-->
    <xsl:template name="Enums">
        <xsl:text>#Region "Enums"&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;Partial Class Iptc&#xD;&#xA;</xsl:text>
                <xsl:for-each select="I:Root/I:record/I:enum">
            <xsl:sort select="@name" order="ascending" data-type="text"/>
            <xsl:call-template name="XML-Doc">
                <xsl:with-param name="Tab" select="2"/>
            </xsl:call-template>
            <xsl:text>&#9;&#9;</xsl:text>
            <xsl:call-template name="Attributes"/>
            <xsl:choose>
                <xsl:when test="(self::node()[@restrict] and (@restrict=1 or @restrict='true')) or (not (self::node()[@restrict]))">
                    <xsl:text>&lt;Restrict(True)> </xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>&lt;Restrict(False)> </xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:if test="@type='SByte' or @type='UShort' or @type='UInteger' or @type='ULong'">
                <xsl:text>&lt;CLSCompliant(False)> </xsl:text>
            </xsl:if>
            <xsl:text>&lt;TypeConverter(GetType(EnumConverterWithAttributes(Of </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>)))> </xsl:text>
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
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Generates all string enums declared in XML-->
    <xsl:template name="sEnums">
        <xsl:text>#Region "String enums"&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;Partial Class Iptc&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:senum">
            <xsl:sort select="@name" order="ascending" data-type="text"/>
            <xsl:call-template name="XML-Doc">
                <xsl:with-param name="Tab" select="2"/>
            </xsl:call-template>
            <xsl:text>&#9;&#9;</xsl:text>
            <xsl:call-template name="Attributes"/>
            <xsl:choose>
                <xsl:when test="(self::node()[@restrict] and (@restrict=1 or @restrict='true')) or (not (self::node()[@restrict]))">
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
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Generates GetTag functions-->
    <xsl:template name="TagTypes">
        <xsl:text>#Region "Tag types"&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;Partial Class Iptc&#xD;&#xA;</xsl:text>
        <!--Common GetTag-->
        <xsl:text>&#9;&#9;''' &lt;summary>Gets details about tag format by tag record and number&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="Record">Recor number&lt;/param>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="TagNumber">Number of tag within &lt;paramref name="Record"/>&lt;/param>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="UseThisGroup">If not null given instance of &lt;see cref="GroupInfo"/> is used instead of obtaining new instance using shared property of the &lt;see cref="GroupInfo"/> class. (Relevant only for tags grouped into groups.)&lt;/param></xsl:text>
        <xsl:text>&#9;&#9;''' &lt;exception cref="InvalidEnumArgumentException">&lt;paramref name="Record"/> is not member of &lt;see cref="RecordNumbers"/> -or- &lt;paramref name="TagNumber"/> is not tag within &lt;paramref name="record"/>&lt;/exception>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;Friend Shared Function GetTag(ByVal Record As RecordNumbers, TagNumber As Byte, ByVal UseThisGroup As GroupInfo) As IPTCTag&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;Select Case Record&#xD;&#xA;</xsl:text>
        <xsl:for-each select="/I:Root/I:record">
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:text>&#9;&#9;&#9;&#9;Case RecordNumbers.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:call-template name="nl"/>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Select Case TagNumber&#xD;&#xA;</xsl:text>
            <xsl:for-each select="I:tag | I:group/I:tag">
                <xsl:sort data-type="number" order="ascending" select="@number"/>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Case </xsl:text>
                <xsl:value-of select="ancestor::I:record/@name"/>
                <xsl:text>Tags.</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text> : Return New IPTCTag(Number:=</xsl:text>
                <xsl:value-of select="ancestor::I:record/@name"/>
                <xsl:text>Tags.</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>, Record:=RecordNumbers.</xsl:text>
                <xsl:value-of select="ancestor::I:record/@name"/>
                <xsl:text>, Name:="</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>", HumanName:="</xsl:text>
                <xsl:value-of select="normalize-space(@human-name)"/>
                <xsl:text>", Type:=IPTCTypes.</xsl:text>
                <xsl:call-template name="GetType">
                    <xsl:with-param name="Type" select="@type"/>
                </xsl:call-template>
                <xsl:text>, Mandatory:=</xsl:text>
                <xsl:value-of select="@mandatory"/>
                <xsl:text>, Repeatable:=</xsl:text>
                <xsl:value-of select="@repeatable"/>
                <xsl:text>, Length:=</xsl:text>
                <xsl:value-of select="@length"/>
                <xsl:text>, Fixed:=</xsl:text>
                <xsl:value-of select="@fixed"/>
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
                <xsl:if test="parent::I:group">
                    <xsl:text>, Group:=If(UseThisGroup,GroupInfo.</xsl:text>
                    <xsl:value-of select="parent::I:group/@name"/>
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
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Translates IPTC tag type name used in XML into IPTC tag type name used in VB-->
    <xsl:template name="GetType">
        <xsl:param name="Type"/>
        <xsl:value-of select="translate($Type,'-','_')"/>
    </xsl:template>
    <!--Generates tag groups-->
    <xsl:template name="Groups">
        <xsl:text>#Region "Groups"&#xD;&#xA;</xsl:text>
        <!--Groups - enum-->
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
        <!--Groups - property for each group-->
        <xsl:text>&#9;&#9;Partial Class GroupInfo&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:group">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="I:tag[1]/@number"/>
            <xsl:call-template name="Summary">
                <xsl:with-param name="Tab" select="3"/>
            </xsl:call-template>
            <xsl:text>&#9;&#9;&#9;''' <![CDATA[<returns>Information about known group <see cref="Groups.]]></xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text><![CDATA["/></returns>]]></xsl:text>
            <xsl:call-template name="nl"/>
            <xsl:text>&#9;&#9;&#9;Public Shared ReadOnly Property </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text> As GroupInfo&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Dim g As New GroupInfo("</xsl:text>
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
            <xsl:value-of select="@mandatory"/>
            <xsl:text>, </xsl:text>
            <xsl:value-of select="@repeatable"/>
            <xsl:text>)&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;g.SetTags(</xsl:text>
            <xsl:for-each select="I:tag">
                <xsl:sort data-type="number" order="ascending" select="@number"/>
                <xsl:if test="position()>1">
                    <xsl:text>, </xsl:text>
                </xsl:if>
                <xsl:text>GetTag(RecordNumbers.</xsl:text>
                <xsl:value-of select="../../@name"/>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="../../@name"/>
                <xsl:text>Tags.</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>, g)</xsl:text>
            </xsl:for-each>
            <xsl:text>)&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Return g&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;End Property&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <!--GetAllGroups-->
        <xsl:text>&#9;&#9;&#9;''' <![CDATA[<summary>Gets all known groups of IPTC tags</summary>]]></xsl:text>
        <xsl:call-template name="nl"/>
        <xsl:text>&#9;&#9;&#9;''' <![CDATA[<returns>All known groups of IPTC tags</returns>]]></xsl:text>
        <xsl:call-template name="nl"/>
        <xsl:text>&#9;&#9;&#9;Public Shared Function GetAllGroups() As GroupInfo()&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;Return New GroupInfo(){</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:group">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="I:tag[1]/@number"/>
            <xsl:if test="not(position()=1)">
                <xsl:text>, </xsl:text>
            </xsl:if>
            <xsl:text>GroupInfo.</xsl:text>
            <xsl:value-of select="@name"/>
        </xsl:for-each>
        <xsl:text>}&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Function&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Class&#xD;&#xA;</xsl:text>
        <!--GetGroup-->
        <xsl:text>&#9;Partial Class Iptc&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;summary>Gets information about known group of IPTC tags&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;param name="Group">Code of group to get information about&lt;/param>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;Public Shared Function GetGroup(ByVal Group As Groups) As GroupInfo&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;Select Case Group&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:group">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="I:tag[1]/@number"/>
            <xsl:text>&#9;&#9;&#9;&#9;Case Groups.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text> : Return GroupInfo.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:call-template name="nl"/>
            <!--<xsl:text> : Return New GroupInfo("</xsl:text>
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
            <xsl:value-of select="@mandatory"/>
            <xsl:text>, </xsl:text>
            <xsl:value-of select="@repeatable"/>
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
            <xsl:text>)&#xD;&#xA;</xsl:text>-->
        </xsl:for-each>
        <xsl:text>&#9;&#9;&#9;&#9;Case Else : Throw New InvalidEnumArgumentException("Group", Group, GetType(Groups))&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Select&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Function&#xD;&#xA;</xsl:text>
        <!--Groups' classes-->
        <xsl:text>#Region "Classes"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:group">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="I:tag[1]/@number"/>
            <xsl:call-template name="XML-Doc">
                <xsl:with-param name="Tab" select="2"/>
            </xsl:call-template>
            <xsl:text>&#9;&#9;</xsl:text>
            <xsl:call-template name="DisplayName"/>
            <xsl:call-template name="Category"/>
            <xsl:text>&lt;TypeConverter(GetType(</xsl:text>
            <xsl:value-of select="@converter"/>
            <xsl:text>))> </xsl:text>
            <xsl:text>Partial Public NotInheritable Class </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group : Inherits Group&#xD;&#xA;</xsl:text>
            <!--<xsl:text>&#9;&#9;&#9;&#9;''' &lt;summary>CTor&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;Public Sub New()&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;End Sub&#xD;&#xA;</xsl:text>-->
            <!--Load-->
            <xsl:text>&#9;&#9;&#9;&#9;''' &lt;summary>Loads groups from IPTC&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;''' &lt;param name="IPTC">&lt;see cref="IPTC"/> to load groups from&lt;/param>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;''' &lt;exception cref="ArgumentNullException">&lt;paramref name="IPTC"/> is null&lt;/exception>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;Public Shared Function Load(ByVal IPTC As IPTC) As List(Of </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group)&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;If IPTC Is Nothing Then Throw New ArgumentNullException("IPTC")&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Dim Map as List(Of Integer()) = GetGroupMap(IPTC</xsl:text>
            <xsl:for-each select="I:tag">
                <xsl:text>, GetTag(</xsl:text>
                <xsl:value-of select="../../@name"/>
                <xsl:text>Tags.</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>)</xsl:text>
            </xsl:for-each>
            <xsl:text>)&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;If Map Is Nothing OrElse Map.Count = 0 Then Return Nothing&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Dim ret As New List(Of </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group)&#xD;&#xA;</xsl:text>
            <xsl:for-each select="I:tag">
                <xsl:variable name="type">
                    <xsl:call-template name="UnderlyingType">
                        <xsl:with-param name="type" select="@type"/>
                        <xsl:with-param name="enum" select="@enum"/>
                        <xsl:with-param name="len" select="@length"/>
                        <xsl:with-param name="name" select="@name"/>
                        <xsl:with-param name="fixed" select="@fixed"/>
                        <xsl:with-param name="instance" select="'IPTC.'"/>
                    </xsl:call-template>
                </xsl:variable>
                <xsl:variable name="Type" select="msxsl:node-set($type)"/>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;Dim _all_</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>s As List(Of </xsl:text>
                <xsl:value-of select="$Type/type/@type"/>
                <xsl:text>) = </xsl:text>
                <xsl:value-of select="$Type/type/@get"/>
                <xsl:call-template name="nl"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;For Each item As Integer() In Map&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;ret.add(New </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group)&#xD;&#xA;</xsl:text>
            <xsl:for-each select="I:tag">
                <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;If item(</xsl:text>
                <xsl:value-of select="position()-1"/>
                <xsl:text>) >= 0 Then ret(ret.Count - 1).</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text> = _all_</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>s(item(</xsl:text>
                <xsl:value-of select="position()-1"/>
                <xsl:text>))&#xD;&#xA;</xsl:text>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Next Item&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Return ret&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;End Function&#xD;&#xA;</xsl:text>
            <!--GetGroup-->
            <xsl:text>&#9;&#9;&#9;&#9;''' &lt;summary>Gets information about this group&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;Public Shared ReadOnly Property GroupInfo As GroupInfo&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Return GetGroup(Groups.</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>)&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;End Property&#xD;&#xA;</xsl:text>
            <!--Properties-->
            <xsl:for-each select="I:tag">
                <xsl:sort data-type="number" order="ascending" select="@number"/>
                <xsl:variable name="type">
                    <xsl:call-template name="UnderlyingType">
                        <xsl:with-param name="type" select="@type"/>
                        <xsl:with-param name="enum" select="@enum"/>
                        <xsl:with-param name="len" select="@length"/>
                        <xsl:with-param name="name" select="@name"/>
                        <xsl:with-param name="fixed" select="@fixed"/>
                    </xsl:call-template>
                </xsl:variable>
                <xsl:variable name="Type" select="msxsl:node-set($type)"/>
                <xsl:text>&#9;&#9;&#9;&#9;''' &lt;summary>Contains value of the &lt;see cref="</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text>"/> property&lt;/summary>&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&lt;EditorBrowsable(EditorBrowsableState.Never)> Private Dim _</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text> As </xsl:text>
                <xsl:value-of select="$Type/type/@type"/>
                <xsl:call-template name="nl"/>
                <xsl:call-template name="XML-Doc">
                    <xsl:with-param name="Tab" select="4"/>
                </xsl:call-template>
                <xsl:text>&#9;&#9;&#9;&#9;</xsl:text>
                <xsl:call-template name="Attributes"/>
                <xsl:call-template name="Category"/>
                <xsl:call-template name="DisplayName"/>
                <xsl:value-of select="$Type/type/@attr"/>
                <xsl:text>Public Property </xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text> As </xsl:text>
                <xsl:value-of select="$Type/type/@type"/>
                <xsl:call-template name="nl"/>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Return _</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:call-template name="nl"/>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;Set&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;_</xsl:text>
                <xsl:value-of select="@name"/>
                <xsl:text> = value&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;End Set&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;End Property&#xD;&#xA;</xsl:text>
                <xsl:call-template name="nl"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;End Class&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
        <!--Group properties-->
        <xsl:text>#Region "Properties"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="I:Root/I:record/I:group">
            <xsl:sort order="ascending" data-type="number" select="ancestor::I:record/@number"/>
            <xsl:sort order="ascending" data-type="number" select="I:tag[1]/@number"/>
            <xsl:call-template name="XML-Doc">
                <xsl:with-param name="Tab" select="2"/>
            </xsl:call-template>
            <xsl:text>&#9;&#9;</xsl:text>
            <xsl:call-template name="DisplayName"/>
            <xsl:call-template name="Category"/>
            <xsl:call-template name="Attributes"/>
            <xsl:text>Public Property </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text> As </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group</xsl:text>
            <xsl:if test="@repeatable=1 or @repeatable='true'">
                <xsl:text>()</xsl:text>
            </xsl:if>
            <xsl:call-template name="nl"/>
            <!--Get-->
            <xsl:text>&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;Dim v As List(Of </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group)=</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>Group.Load(Me)&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;If v Is Nothing OrElse v.Count = 0 Then Return Nothing&#xD;&#xA;</xsl:text>
            <xsl:choose>
                <xsl:when test="@repeatable=1 or @repeatable='true'">
                    <xsl:text>&#9;&#9;&#9;&#9;Return v.ToArray&#xD;&#xA;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>&#9;&#9;&#9;&#9;Return v(0)&#xD;&#xA;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
            <!--Set-->
            <xsl:text>&#9;&#9;&#9;Set&#xD;&#xA;</xsl:text>
            <xsl:choose>
                <xsl:when test="@repeatable=1 or @repeatable='true'">
                    <xsl:text>&#9;&#9;&#9;&#9;Dim Items As </xsl:text>
                    <xsl:value-of select="@name"/>
                    <xsl:text>Group() = </xsl:text>
                    <xsl:text>value&#xD;&#xA;</xsl:text>
                    <xsl:for-each select="I:tag">
                        <xsl:text>&#9;&#9;&#9;&#9;Clear(dataSetIdentification.</xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>)&#xD;&#xA;</xsl:text>
                    </xsl:for-each>
                    <xsl:text>&#9;&#9;&#9;&#9;If Items IsNot Nothing Then&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;For Each item As </xsl:text>
                    <xsl:value-of select="@name"/>
                    <xsl:text>Group In Items&#xD;&#xA;</xsl:text>
                    <xsl:for-each select="I:tag">
                        <xsl:if test="position()>1"><xsl:call-template name="nl"/></xsl:if>
                        <xsl:variable name="type">
                            <xsl:call-template name="UnderlyingType">
                                <xsl:with-param name="type" select="@type"/>
                                <xsl:with-param name="enum" select="@enum"/>
                                <xsl:with-param name="len" select="@length"/>
                                <xsl:with-param name="name" select="@name"/>
                                <xsl:with-param name="fixed" select="@fixed"/>
                            </xsl:call-template>
                        </xsl:variable>
                        <xsl:variable name="Type" select="msxsl:node-set($type)"/>
                        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Dim </xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>Values As </xsl:text>
                        <xsl:value-of select="$Type/type/@type"/>
                        <xsl:text>() = </xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:call-template name="nl"/>
                        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;If </xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>Values Is Nothing Then </xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>Values = New </xsl:text>
                        <xsl:value-of select="$Type/type/@type"/>
                        <xsl:text>(){}&#xD;&#xA;</xsl:text>
                        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;ReDim Preserve </xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>Values(</xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>Values.Length)&#xD;&#xA;</xsl:text>
                        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;</xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>Values(</xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>Values.Length - 1) = item.</xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:call-template name="nl"/>
                        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;</xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text> = </xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text>Values&#xD;&#xA;</xsl:text>
                    </xsl:for-each>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;Next item&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;End If&#xD;&#xA;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:for-each select="I:tag">
                        <xsl:text>&#9;&#9;&#9;&#9;</xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:text> = value.</xsl:text>
                        <xsl:value-of select="@name"/>
                        <xsl:call-template name="nl"/>
                    </xsl:for-each>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>&#9;&#9;&#9;End Set&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;End Property&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Generates properties for all IPTC tags-->
    <xsl:template name="Properties">
        <!--Stand Alone-->
        <xsl:text>#Region "Properties"&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;Partial Class Iptc&#xD;&#xA;</xsl:text>
        <xsl:for-each select="/I:Root/I:record/I:tag">
            <xsl:sort data-type="number" order="ascending" select="../@number"/>
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:call-template name="Property">
                <xsl:with-param name="access" select="'Public Overridable'"/>
            </xsl:call-template>
        </xsl:for-each>
        <!--Of groups-->
        <xsl:text>#Region "Grouped" 'Those propertiers can be accessed via groups, do not use them directly!&#xD;&#xA;</xsl:text>
        <xsl:for-each select="/I:Root/I:record/I:group/I:tag">
            <xsl:sort data-type="number" order="ascending" select="../../@number"/>
            <xsl:sort data-type="number" order="ascending" select="@number"/>
            <xsl:call-template name="Property">
                <xsl:with-param name="access" select="'&lt;EditorBrowsable(EditorBrowsableState.Never)> Private'"/>
            </xsl:call-template>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Generates property-->
    <xsl:template name="Property">
        <xsl:param name="access"/>
        <xsl:variable name="type">
            <xsl:call-template name="UnderlyingType">
                <xsl:with-param name="type" select="@type"/>
                <xsl:with-param name="enum" select="@enum"/>
                <xsl:with-param name="len" select="@length"/>
                <xsl:with-param name="name" select="@name"/>
                <xsl:with-param name="fixed" select="@fixed"/>
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="Type" select="msxsl:node-set($type)"/>
        <xsl:call-template name="XML-Doc">
            <xsl:with-param name="Tab" select="2"/>
        </xsl:call-template>
        <xsl:choose>
            <xsl:when test="(@repeatable=1 or @repeatable='true') or (ancestor::I:group/@repeatable=1 or ancestor::I:group/@repeatable='true')">
                <xsl:text>&#9;&#9;''' &lt;returns>If this instance contains this tag(s) retuns them. Otherwise returns null&lt;/returns>&#xD;&#xA;</xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>&#9;&#9;''' &lt;returns>If this instance contains this tag retuns it. Otherwise returns null&lt;/returns>&#xD;&#xA;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
        <xsl:text>&#9;&#9;''' &lt;exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.&lt;/exception>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;''' &lt;exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured&lt;/exception>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;</xsl:text>
        <xsl:call-template name="Description"/>
        <xsl:text> _&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;</xsl:text>
        <xsl:call-template name="Category"/>
        <xsl:call-template name="DisplayName"/>
        <xsl:value-of select="$Type/type/@attr"/>
        <xsl:call-template name="Attributes"/>
        <xsl:value-of select="$access"/>
        <xsl:text> Property </xsl:text>
        <xsl:value-of select="@name"/>
        <xsl:text> As </xsl:text>
        <xsl:value-of select="$Type/type/@type"/>
        <xsl:if test="(@repeatable=1 or @repeatable='true') or (ancestor::I:group/@repeatable=1 or ancestor::I:group/@repeatable='true')">
            <xsl:text>()</xsl:text>
        </xsl:if>
        <xsl:call-template name="nl"/>
        <!--Get-->
        <xsl:text>&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;Try&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Dim AllValues As List(Of </xsl:text>
        <xsl:value-of select="$Type/type/@type"/>
        <xsl:text>) = </xsl:text>
        <xsl:value-of select="$Type/type/@get"/>
        <xsl:call-template name="nl"/>
        <xsl:choose>
            <xsl:when test="(@repeatable=1 or @repeatable='true') or (ancestor::I:group/@repeatable=1 or ancestor::I:group/@repeatable='true')">
                <xsl:text>&#9;&#9;&#9;&#9;&#9;If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;Return AllValues.ToArray&#xD;&#xA;</xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;If AllValues IsNot Nothing AndAlso AllValues.Count &lt;> 0 Then Return AllValues(0) Else Return Nothing&#xD;&#xA;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
        <xsl:text>&#9;&#9;&#9;&#9;Catch ex As Exception&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Throw New IPTCGetException(ex)&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;End Try&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
        <!--Set-->
        <xsl:text>&#9;&#9;&#9;Set&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;Try&#xD;&#xA;</xsl:text>        
        <xsl:text>&#9;&#9;&#9;&#9;&#9;</xsl:text>
        <xsl:value-of select="$Type/type/@set"/>
        <xsl:text> = </xsl:text>
        <xsl:if test="$Type/type/@convert-back">
            <xsl:value-of select="$Type/type/@convert-back"/>
            <xsl:text>(</xsl:text>
        </xsl:if>
        <xsl:choose>
            <xsl:when test="(@repeatable=1 or @repeatable='true') or (ancestor::I:group/@repeatable=1 or ancestor::I:group/@repeatable='true')">
                <xsl:text>New List(Of </xsl:text>
                <xsl:value-of select="$Type/type/@type"/>
                <xsl:text>)(value)</xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>New List(Of </xsl:text>
                <xsl:value-of select="$Type/type/@type"/>
                <xsl:text>)(New </xsl:text>
                <xsl:value-of select="$Type/type/@type"/>
                <xsl:text>(){value})</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
        <xsl:if test="$Type/type/@convert-back">
            <xsl:text>)</xsl:text>
        </xsl:if>
        <xsl:call-template name="nl"/>
        <xsl:text>&#9;&#9;&#9;&#9;Catch ex As Exception&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Throw New IPTCSetException(ex)&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;End Try&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Set&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Property&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--getenretes underlying type from IPTC type-->
    <!--Returns
        <type type="" get="" set="" attr="" convert-back=""/>
    -->
    <xsl:template name="UnderlyingType">
        <xsl:param name="type"/>
        <!--IPTC type-->
        <xsl:param name="enum"/>
        <!--Optional enum name (required for enum types)-->
        <xsl:param name="len"/>
        <!--Lenght-->
        <xsl:param name="fixed"/>
        <!--Fixed-->
        <xsl:param name="name"/>
        <!--Name of property-->
        <xsl:param name="instance"/>
        <!--Instance to get data from (e.g. Me.; Me. can be ommited)-->
        <xsl:element name="type">
            <xsl:choose>
                <!--Enum-binary-->
                <xsl:when test="$type='Enum-binary'">
                    <xsl:attribute name="type">
                        <xsl:value-of select="@enum"/>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:text>ConvertEnumList(Of </xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>)(</xsl:text>
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Enum_Binary_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, GetType(</xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>)))</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Enum_Binary_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, GetType(</xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>))</xsl:text>
                    </xsl:attribute>
                    <xsl:if test="/I:Root/I:record/I:enum[@name=$enum]/@type='SByte' or /I:Root/I:record/I:enum[@name=$enum]/@type='UShort' or /I:Root/I:record/I:enum[@name=$enum]/@type='UInteger' or /I:Root/I:record/I:enum[@name=$enum]/@type='ULong'">
                        <xsl:attribute name="attr">
                            <xsl:text>&lt;CLSCompliant(False)></xsl:text>
                        </xsl:attribute>
                    </xsl:if>
                    <xsl:attribute name="convert-back">
                        <xsl:text>ConvertEnumList</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Enum-NumChar-->
                <xsl:when test="$type='Enum-NumChar'">
                    <xsl:attribute name="type">
                        <xsl:value-of select="@enum"/>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:text>ConvertEnumList(Of </xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>)(</xsl:text>
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Enum_NumChar_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, GetType(</xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>)))</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Enum_NumChar_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, GetType(</xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>), </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="convert-back">
                        <xsl:text>ConvertEnumList</xsl:text>
                    </xsl:attribute>
                    <xsl:if test="/I:Root/I:record/I:enum[@name=$enum]/@type='SByte' or /I:Root/I:record/I:enum[@name=$enum]/@type='UShort' or /I:Root/I:record/I:enum[@name=$enum]/@type='UInteger' or /I:Root/I:record/I:enum[@name=$enum]/@type='ULong'">
                        <xsl:attribute name="attr">
                            <xsl:text>&lt;CLSCompliant(False)></xsl:text>
                        </xsl:attribute>
                    </xsl:if>
                </xsl:when>
                <!--StringEnum-->
                <xsl:when test="$type='StringEnum'">
                    <xsl:attribute name="type">
                        <xsl:text>StringEnum(Of </xsl:text>
                        <xsl:value-of select="@enum"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:text>ConvertEnumList(Of </xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>)(</xsl:text>
                        <xsl:value-of select="$instance"/>
                        <xsl:text>StringEnum_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, GetType(</xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>)))</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="convert-back">
                        <xsl:text>ConvertEnumList(Of </xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>StringEnum_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, GetType(</xsl:text>
                        <xsl:value-of select="$enum"/>
                        <xsl:text>), </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="attr">
                        <xsl:text>&lt;CLSCompliant(False)></xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--UnsignedBinaryNumber-->
                <xsl:when test="$type='UnsignedBinaryNumber'">
                    <xsl:attribute name="type">
                        <xsl:text>ULong</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>UnsignedBinaryNumber_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>UnsignedBinaryNumber_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="attr">
                        <xsl:text>&lt;CLSCompliant(False)></xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Boolean-binary-->
                <xsl:when test="$type='Boolean-binary'">
                    <xsl:attribute name="type">
                        <xsl:text>Boolean</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Boolean_Binary_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Boolean_Binary_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--UShort-binary-->
                <xsl:when test="$type='UShort-binary'">
                    <xsl:attribute name="type">
                        <xsl:text>UShort</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>UShort_Binary_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>UShort_Binary_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="attr">
                        <xsl:text>&lt;CLSCompliant(False)></xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--NumericChar-->
                <xsl:when test="$type='NumericChar'">
                    <xsl:attribute name="type">
                        <xsl:text>Decimal</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>NumericChar_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>NumericChar_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--GraphicCharacters-->
                <xsl:when test="$type='GraphicCharacters'">
                    <xsl:attribute name="type">
                        <xsl:text>String</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>GraphicCharacters_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>GraphicCharacters_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--TextWithSpaces-->
                <xsl:when test="$type='TextWithSpaces'">
                    <xsl:attribute name="type">
                        <xsl:text>String</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>TextWithSpaces_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>TextWithSpaces_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Text-->
                <xsl:when test="$type='Text'">
                    <xsl:attribute name="type">
                        <xsl:text>String</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Text_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Text_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Alpha-->
                <xsl:when test="$type='Alpha'">
                    <xsl:attribute name="type">
                        <xsl:text>String</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Alpha_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Alpha_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--BW460-->
                <xsl:when test="$type='BW460'">
                    <xsl:attribute name="type">
                        <xsl:text>Drawing.Bitmap</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>BW460_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>BW460_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--CCYYMMDD-->
                <xsl:when test="$type='CCYYMMDD'">
                    <xsl:attribute name="type">
                        <xsl:text>Date</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>CCYYMMDD_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>CCYYMMDD_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--CCYYMMDDommitable-->
                <xsl:when test="$type='CCYYMMDDommitable'">
                    <xsl:attribute name="type">
                        <xsl:text>OmmitableDate</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>CCYYMMDDOmmitable_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>CCYYMMDDOmmitable_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--HHMMSS-HHMM-->
                <xsl:when test="$type='HHMMSS-HHMM'">
                    <xsl:attribute name="type">
                        <xsl:text>Time</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>HHMMSS_HHMM_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>HHMMSS_HHMM_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--ByteArray-->
                <xsl:when test="$type='ByteArray'">
                    <xsl:attribute name="type">
                        <xsl:text>Byte()</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>ByteArray_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Bytearray_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$len"/>
                        <xsl:text>, </xsl:text>
                        <xsl:value-of select="$fixed"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--UNO-->
                <xsl:when test="$type='UNO'">
                    <xsl:attribute name="type">
                        <xsl:text>iptcUNO</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>UNO_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>UNO_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--Num2-Str-->
                <xsl:when test="$type='Num2-Str'">
                    <xsl:choose>
                        <xsl:when test="$enum!=''">
                            <xsl:attribute name="type">
                                <xsl:text>NumStr2(Of </xsl:text>
                                <xsl:value-of select="$enum"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="attr">
                                <xsl:text>&lt;CLSCompliant(False)></xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="get">
                                <xsl:text>ConvertNumStrList(Of NumStr2, NumStr2(Of </xsl:text>
                                <xsl:value-of select="$enum"/>
                                <xsl:text>))(</xsl:text>
                                <xsl:value-of select="$instance"/>
                                <xsl:text>Num2_Str_Value(DataSetIdentification.</xsl:text>
                                <xsl:value-of select="$name"/>
                                <xsl:text>))</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="set">
                                <xsl:value-of select="$instance"/>
                                <xsl:text>Num2_Str_Value(DataSetIdentification.</xsl:text>
                                <xsl:value-of select="$name"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="$len"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="convert-back">
                                <xsl:text>ConvertNumStrList(Of NumStr2, NumStr2(Of </xsl:text>
                                <xsl:value-of select="$enum"/>
                                <xsl:text>))</xsl:text>
                            </xsl:attribute>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:attribute name="type">
                                <xsl:text>NumStr2</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="get">
                                <xsl:value-of select="$instance"/>
                                <xsl:text>Num2_Str_Value(DataSetIdentification.</xsl:text>
                                <xsl:value-of select="$name"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="set">
                                <xsl:value-of select="$instance"/>
                                <xsl:text>Num2_Str_Value(DataSetIdentification.</xsl:text>
                                <xsl:value-of select="$name"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="$len"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:when>
                <!--Num3-Str-->
                <xsl:when test="$type='Num3-Str'">
                    <xsl:choose>
                        <xsl:when test="$enum!=''">
                            <xsl:attribute name="type">
                                <xsl:text>NumStr3(Of </xsl:text>
                                <xsl:value-of select="$enum"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="attr">
                                <xsl:text>&lt;CLSCompliant(False)></xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="get">
                                <xsl:text>ConvertNumStrList(Of NumStr3, NumStr3(Of </xsl:text>
                                <xsl:value-of select="$enum"/>
                                <xsl:text>))(</xsl:text>
                                <xsl:value-of select="$instance"/>
                                <xsl:text>Num3_Str_Value(DataSetIdentification.</xsl:text>
                                <xsl:value-of select="$name"/>
                                <xsl:text>))</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="set">
                                <xsl:value-of select="$instance"/>
                                <xsl:text>Num3_Str_Value(DataSetIdentification.</xsl:text>
                                <xsl:value-of select="$name"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="$len"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="convert-back">
                                <xsl:text>ConvertNumStrList(Of NumStr3, NumStr3(Of </xsl:text>
                                <xsl:value-of select="$enum"/>
                                <xsl:text>))</xsl:text>
                            </xsl:attribute>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:attribute name="type">
                                <xsl:text>NumStr3</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="get">
                                <xsl:value-of select="$instance"/>
                                <xsl:text>Num3_Str_Value(DataSetIdentification.</xsl:text>
                                <xsl:value-of select="$name"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="set">
                                <xsl:value-of select="$instance"/>
                                <xsl:text>Num3_Str_Value(DataSetIdentification.</xsl:text>
                                <xsl:value-of select="$name"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="$len"/>
                                <xsl:text>)</xsl:text>
                            </xsl:attribute>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:when>
                <!--SubjectReference-->
                <xsl:when test="$type='SubjectReference'">
                    <xsl:attribute name="type">
                        <xsl:text>iptcSubjectReference</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>SubjectReference_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>SubjectReference_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--ImageType-->
                <xsl:when test="$type='ImageType'">
                    <xsl:attribute name="type">
                        <xsl:text>iptcImageType</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>ImageType_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>ImageType_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--AudioType-->
                <xsl:when test="$type='AudioType'">
                    <xsl:attribute name="type">
                        <xsl:text>iptcAudioType</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Audiotype_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>Audiotype_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <!--HHMMSS-->
                <xsl:when test="$type='HHMMSS'">
                    <xsl:attribute name="type">
                        <xsl:text>TimeSpan</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="get">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>HHMMSS_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="set">
                        <xsl:value-of select="$instance"/>
                        <xsl:text>HHMMSS_Value(DataSetIdentification.</xsl:text>
                        <xsl:value-of select="$name"/>
                        <xsl:text>)</xsl:text>
                    </xsl:attribute>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:message>
                        <xsl:text>Unknown type </xsl:text>
                        <xsl:value-of select="@type"/>
                    </xsl:message>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:element>
    </xsl:template>
    
    <!--Supporting templates (common):-->
    <!--Generates end of line-->
    <xsl:template name="nl">
        <xsl:text xml:space="preserve">&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Converts & to &amp;-->
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
    <!--Replaces " with ""-->
    <xsl:template name="Quot">
        <xsl:param name="str"/>
        <xsl:choose>
            <xsl:when test="contains($str,'&quot;')">
                <xsl:value-of select="substring-before($str,'&quot;')"/>
                <xsl:text>""</xsl:text>
                <xsl:call-template name="Quot">
                    <xsl:with-param name="str" select="substring-after($str,'&quot;')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$str"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <!--Supporting templates (specific):-->
    <!--Creates <summary> from <desc>-->
    <xsl:template name="Tag-Summary">
        <xsl:param name="Tab"/>
        <xsl:call-template name="Tabs">
            <xsl:with-param name="Count" select="$Tab"/>
        </xsl:call-template>
        <xsl:text>''' &lt;summary></xsl:text>
        <xsl:variable name="content">
            <xsl:call-template name="Amp2Entity">
                <xsl:with-param name="Text">
                    <xsl:call-template name="DocText">
                        <xsl:with-param name="node" select="I:desc"/>
                    </xsl:call-template>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
        <xsl:value-of select="normalize-space($content)" disable-output-escaping="yes"/>
        <xsl:text>&lt;/summary>&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Rewrites content of <summary> or <remarks>-->
    <xsl:template name="DocText">
        <xsl:param name="node"/>
        <xsl:for-each select="$node/child::node()">
            <xsl:call-template name="DocTextBody"/>
        </xsl:for-each>
    </xsl:template>
    <!--Work-doing part of DocText-->
    <xsl:template name="DocTextBody">
        <xsl:choose>
            <xsl:when test="count(self::text())>0">
                <xsl:value-of select="."/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>&lt;</xsl:text>
                <xsl:value-of select="local-name(.)"/>
                <xsl:for-each select="attribute::*">
                    <xsl:text>&#32;</xsl:text>
                    <xsl:value-of select="local-name(.)"/>
                    <xsl:text>="</xsl:text>
                    <xsl:value-of select="."/>
                    <xsl:text>"</xsl:text>
                </xsl:for-each>
                <xsl:choose>
                    <xsl:when test="self::text() or child::*">
                        <xsl:text>></xsl:text>
                        <xsl:for-each select="child::node()">
                            <xsl:call-template name="DocTextBody"/>
                        </xsl:for-each>
                        <xsl:text>&lt;/</xsl:text>
                        <xsl:value-of select="local-name(.)"/>
                        <xsl:text>></xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:text>/></xsl:text>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:otherwise>
        </xsl:choose>
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
        <xsl:value-of select="@category"/>
        <xsl:text>")> </xsl:text>
    </xsl:template>
    <!--Renders attributes-->
    <xsl:template name="Attributes">
        <xsl:if test="@attributes">
            <xsl:value-of select="@attributes"/>
            <xsl:text>&#32;</xsl:text>
        </xsl:if>
    </xsl:template>
    <!--Renders description attribute-->
    <xsl:template name="Description">
        <xsl:choose>
            <xsl:when test="I:desc">
                <xsl:text>&lt;Description("</xsl:text>
                <xsl:call-template name="Doc-text">
                    <xsl:with-param name="Doc" select="I:desc"/>
                </xsl:call-template>
                <xsl:text>")></xsl:text>
            </xsl:when>
            <xsl:when test="@desc">
                <xsl:text>&lt;Description("</xsl:text>
                <xsl:value-of select="@desc"/>
                <xsl:text>")></xsl:text>
            </xsl:when>
        </xsl:choose>
    </xsl:template>
    <!--Gets text from XML-doc (with simple plain-text formating). Used for DescriptionAttribute-->
    <xsl:template name="Doc-text">
        <xsl:param name="Doc"/>
        <xsl:for-each select="$Doc/child::node()">
            <xsl:choose>
                <xsl:when test="count(self::text())>0">
                    <xsl:call-template name="Quot">
                        <xsl:with-param name="str" select="normalize-space(.)"/>
                    </xsl:call-template>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>&#32;</xsl:text>
                    <xsl:choose>
                        <xsl:when test="string(.) or ./child::*">
                            <xsl:if test="local-name(.)='para' or local-name(.)='list' or local-name(.)='item'">
                                <xsl:text>" &amp; vbCrLf &amp; "</xsl:text>
                            </xsl:if>
                            <xsl:if test="local-name(.)='item' and parent::node()/@type!='table'">
                                <xsl:text>* </xsl:text>
                            </xsl:if>
                            <xsl:if test="local-name(.)='description'">
                                <xsl:text>" &amp; vbTab &amp; "</xsl:text>
                            </xsl:if>
                            <xsl:call-template name="Doc-text"/>
                        </xsl:when>
                        <xsl:when test="@cref">
                            <xsl:variable name="cref" select="@cref"/>
                            <xsl:choose>
                                <xsl:when test="/I:Root/I:record//I:tag[@name=$cref]/@human-name">
                                    <xsl:value-of select="/I:Root/I:record//I:tag[@name=$cref]/@human-name"/>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:value-of select="@cref"/>
                                </xsl:otherwise>
                            </xsl:choose>
                        </xsl:when>
                        <xsl:when test="name">
                            <xsl:value-of select="@name"/>
                        </xsl:when>
                    </xsl:choose>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:for-each>
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
</xsl:stylesheet>


