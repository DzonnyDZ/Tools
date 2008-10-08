<?xml version="1.0" encoding="utf-8"?>
<!--This template was once used to add documentation to COdeDom.xsd-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:variable name="System.xml" select="'file:///C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\en\System.xml'"/>
    <xsl:variable name="mscorlib.xml" select="'file:///c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\mscorlib.xml'"/>
    <xsl:variable name="System" select="document($System.xml)"/>
    <xsl:variable name="mscorlib" select="document($mscorlib.xml)"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <!--Complex types-->
    <xsl:template match="xs:complexType[@name]">
        <xs:complexType>
            <xsl:apply-templates select="@*"/>
            <xsl:variable name="name" select="@name"/>
            <xsl:choose>
                <xsl:when test="$name='key-value-pair' or $name='AssemblyReferences' or $name='Null'">
                    <xsl:apply-templates select="./node()"/>
                </xsl:when>
                <xsl:otherwise>
                    <xs:annotation>
                        <xs:documentation>
                            <xsl:variable name="testA">
                                <xsl:choose>
                                    <xsl:when test="$name='Guid'">
                                        <xsl:value-of select="System.Guid"/>
                                    </xsl:when>
                                    <xsl:when test="starts-with($name,'Code')">
                                        <xsl:value-of select="concat('System.CodeDom.',$name)"/>
                                    </xsl:when>
                                    <xsl:when test="substring($name,string-length($name))='s'">
                                        <xsl:value-of select="concat('System.CodeDom.Code',substring($name,1,string-length($name)-1),'Collection')"/>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <xsl:value-of select="concat('System.CodeDom.Code',$name)"/>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </xsl:variable>
                            <xsl:variable name="test" select="normalize-space($testA)"/>
                            <xsl:variable name="summary" select="($System | $mscorlib)/doc/members/member[@name=concat('T:',$test)]/summary"/>
                            <xsl:if test="not(($System | $mscorlib)/doc/members/member[@name=concat('T:',$test)]/summary)">
                                <xsl:message>Documentation for type <xsl:value-of select="$name"/> not found.
                            </xsl:message>
                            </xsl:if>
                            <xsl:apply-templates select="$summary"/>
                            <seelaso xmlns="" cref="{concat('T:',$test)}"/>  
                        </xs:documentation>
                    </xs:annotation>
                    <xsl:apply-templates select="./node()[local-name()!='annotation']"/>
                </xsl:otherwise>
            </xsl:choose>
        </xs:complexType>
    </xsl:template>
    <!--Enum types-->
    <xsl:template match="xs:simpleType[@name][xs:restriction[@base='xs:string']/xs:enumeration or xs:list]">
        <xs:simpleType>
            <xsl:apply-templates select="@*"/>
            <xsl:variable name="name" select="@name"/>
            <xs:annotation>
                <xs:documentation>
                    <xsl:variable name="members" select="($System/doc/members/member | $mscorlib/doc/members/member)[starts-with(@name,'T:System.CodeDom.') or starts-with(@name,'T:System.Reflection.')]"/>
                    <xsl:variable name="testA">
                        <xsl:choose>
                            <xsl:when test="$members[@name=concat('T:System.CodeDom.',$name)]">
                                <xsl:value-of select="concat('T:System.CodeDom.',$name)"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.CodeDom.Code',$name)]">
                                <xsl:value-of select="concat('T:System.CodeDom.Code',$name)"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.CodeDom.',$name,'s')]">
                                <xsl:value-of select="concat('T:System.CodeDom.',$name,'s')"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.CodeDom.Code',$name,'s')]">
                                <xsl:value-of select="concat('T:System.CodeDom.Code',$name,'s')"/>
                            </xsl:when>
                            <xsl:when test="substring($name,string-length($name))='s' and $members[@name=concat('T:System.CodeDom.',substring($name,1,string-length($name)-1))]">
                                <xsl:value-of select="concat('T:System.CodeDom.',substring($name,1,string-length($name)-1))"/>
                            </xsl:when>
                            <xsl:when test="substring($name,string-length($name))='s' and $members[@name=concat('T:System.CodeDom.Code',substring($name,1,string-length($name)-1))]">
                                <xsl:value-of select="concat('T:System.CodeDom.Code',substring($name,1,string-length($name)-1))"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.Reflection.',$name)]">
                                <xsl:value-of select="concat('T:System.Reflection.',$name)"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.Reflection.',$name,'s')]">
                                <xsl:value-of select="concat('T:System.Reflection.',$name,'s')"/>
                            </xsl:when>
                            <xsl:when test="substring($name,string-length($name))='s' and $members[@name=concat('T:System.Reflection.',substring($name,1,string-length($name)-1))]">
                                <xsl:value-of select="concat('T:System.Reflection.',substring($name,1,string-length($name)-1))"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:message>Cannot found type <xsl:value-of select="$name"/>.</xsl:message>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:variable>
                    <xsl:variable name="test" select="normalize-space($testA)"/>
                    <xsl:variable name="summary" select="$members[@name=$test]/summary"/>
                    <xsl:apply-templates select="$summary"/>
                    <seealso xmlns="" cref="{$test}"/>
                </xs:documentation>
            </xs:annotation>
            <xsl:apply-templates select="./node()[local-name()!='annotation']"/>
        </xs:simpleType>    
    </xsl:template>
    <!--.NET types-->
    <xsl:template match="xs:simpleType[@name][./preceding-sibling::comment()[.='.NET types']]">
        <xs:simpleType>
            <xsl:apply-templates select="@*"/>
            <xs:annotation>
                <xs:documentation>
                    <xsl:variable name="name" select="@name"/>
                    <xsl:variable name="doc" select="$mscorlib/doc/members/member[@name=concat('T:System.',$name)]"/>
                    <xsl:apply-templates select="$doc/summary"/>
                    <seelaso xmlns="" cref="T:System.{$name}"/>
                </xs:documentation>
            </xs:annotation>
            <xsl:apply-templates select="node()"/>
        </xs:simpleType>
    </xsl:template>
    
    <!--Enum mmebers-->
    <xsl:template match="xs:simpleType[@name]/xs:restriction[@base='xs:string']/xs:enumeration">
        <xs:enumeration>
            <xsl:apply-templates select="@*"/>
            <xsl:variable name="name" select="./parent::xs:restriction/parent::xs:simpleType/@name"/>
            <xsl:variable name="enumName" select="@value"/>
            <xs:annotation>
                <xs:documentation>
                    <xsl:variable name="members" select="($System/doc/members/member | $mscorlib/doc/members/member)[starts-with(@name,'T:System.CodeDom.') or starts-with(@name,'T:System.Reflection.')]"/>
                    <xsl:variable name="testA">
                        <xsl:choose>
                            <xsl:when test="$members[@name=concat('T:System.CodeDom.',$name)]">
                                <xsl:value-of select="concat('System.CodeDom.',$name)"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.CodeDom.Code',$name)]">
                                <xsl:value-of select="concat('System.CodeDom.Code',$name)"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.CodeDom.',$name,'s')]">
                                <xsl:value-of select="concat('System.CodeDom.',$name,'s')"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.CodeDom.Code',$name,'s')]">
                                <xsl:value-of select="concat('System.CodeDom.Code',$name,'s')"/>
                            </xsl:when>
                            <xsl:when test="substring($name,string-length($name))='s' and $members[@name=concat('T:System.CodeDom.',substring($name,1,string-length($name)-1))]">
                                <xsl:value-of select="concat('System.CodeDom.',substring($name,1,string-length($name)-1))"/>
                            </xsl:when>
                            <xsl:when test="substring($name,string-length($name))='s' and $members[@name=concat('T:System.CodeDom.Code',substring($name,1,string-length($name)-1))]">
                                <xsl:value-of select="concat('System.CodeDom.Code',substring($name,1,string-length($name)-1))"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.Reflection.',$name)]">
                                <xsl:value-of select="concat('System.Reflection.',$name)"/>
                            </xsl:when>
                            <xsl:when test="$members[@name=concat('T:System.Reflection.',$name,'s')]">
                                <xsl:value-of select="concat('System.Reflection.',$name,'s')"/>
                            </xsl:when>
                            <xsl:when test="substring($name,string-length($name))='s' and $members[@name=concat('T:System.Reflection.',substring($name,1,string-length($name)-1))]">
                                <xsl:value-of select="concat('System.Reflection.',substring($name,1,string-length($name)-1))"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:message>
                                    Cannot found type <xsl:value-of select="$name"/>.
                                </xsl:message>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:variable>
                    <xsl:variable name="test" select="concat('F:',normalize-space($testA),'.',$enumName)"/>
                    <xsl:variable name="members2" select="($System/doc/members/member | $mscorlib/doc/members/member)[starts-with(@name,'F:System.CodeDom.') or starts-with(@name,'F:System.Reflection.')]"/>
                    <xsl:variable name="summary" select="$members2[@name=$test]/summary"/>
                    <xsl:if test="not($summary)">
                        <xsl:message>Cannot find enum member <xsl:value-of select="$name"/>.<xsl:value-of select="$enumName"/>.</xsl:message>
                    </xsl:if>
                    <xsl:apply-templates select="$summary"/>
                    <seealso xmlns="" cref="{$test}"/>
                </xs:documentation>
                <xsl:apply-templates select="xs:appinfo"/>
            </xs:annotation>
            <xsl:apply-templates select="./node()[local-name()!='annotation']"/>
        </xs:enumeration>
    </xsl:template>
</xsl:stylesheet>
