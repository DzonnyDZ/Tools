<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:swd="http://dzonny.cz/xml/SpecializedWrapperDefinition"
>

    <!-- parameters passed in by the TransformCodeGenerator -->
    <xsl:param name="generator"></xsl:param>
    <xsl:param name="version"></xsl:param>
    <xsl:param name="fullfilename"></xsl:param>
    <xsl:param name="filename"></xsl:param>
    <xsl:param name="date-created"></xsl:param>
    <xsl:param name="created-by"></xsl:param>
    <xsl:param name="namespace"></xsl:param>
    <xsl:param name="classname"></xsl:param>

    <xsl:output method="text" indent="no" encoding="utf-8" omit-xml-declaration="yes"/>

    <xsl:template match="@* | node()"/>

    <xsl:template match="/swd:wrappers">
        <xsl:text>'This code was generated from </xsl:text>
        <xsl:value-of select="$filename"/>
        <xsl:text> at </xsl:text>
        <xsl:value-of select="$date-created"/>
        <xsl:text> by </xsl:text>
        <xsl:value-of select="$generator"/>
        <xsl:text> </xsl:text>
        <xsl:value-of select="$version"/>
        <xsl:text>&#xd;&#xa;'Do not edit, your changes will be lost!&#xd;&#xa;</xsl:text>
        <xsl:text>Imports System.ComponentModel, Tools.CollectionsT.GenericT, System.Runtime.CompilerServices&#xd;&#xa;</xsl:text>
        <xsl:text>#If Config &lt;= </xsl:text>
        <xsl:value-of select="@stage"/>
        <xsl:text> Then 'Stage:</xsl:text>
        <xsl:value-of select="@stage"/>
        <xsl:if test="$namespace">
        <xsl:text>&#xd;&#xa;Namespace </xsl:text><xsl:value-of select="$namespace"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text></xsl:if>
        <xsl:text>    Partial Public MustInherit Class SpecializedWrapper&#xd;&#xa;</xsl:text>
        <xsl:apply-templates select="swd:*"/> 
        <xsl:text>    End Class&#xd;&#xa;</xsl:text>
        <xsl:text>    ''' &lt;summary>Contains extension methods for getting specialized collections as type-safe generic collections&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>    Public Module AsTypeSafe&#xd;&#xa;</xsl:text>
        <xsl:apply-templates mode="Module" select="swd:*"/>
        <xsl:text>    End Module&#xd;&#xa;</xsl:text>
        <xsl:if test="$namespace">
        <xsl:text>End Namespace&#xd;&#xa;</xsl:text>
        </xsl:if>
        <xsl:text>#End If</xsl:text>
    </xsl:template>


    <xsl:template match="swd:wrapper">
        <xsl:variable name="ColFull" select="concat(@CollectionNamespace,'.',@Collection)"/>
        <xsl:variable name="ItemNamespace">
            <xsl:choose>
                <xsl:when test="@ItemNamespace">
                    <xsl:value-of select="@ItemNamespace"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="@CollectionNamespace"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="ItemFull" select="concat(normalize-space($ItemNamespace),'.',@Item)"/>
        <xsl:variable name="WrapperName_">
            <xsl:choose>
                <xsl:when test="@WrapperName">
                    <xsl:value-of select="@WrapperName"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="concat(@Collection,'TypeSafeWrapper')"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="WrapperName" select="normalize-space($WrapperName_)"/>
        
        <!--GetWrapper--> 
        <xsl:text>#Region "</xsl:text><xsl:value-of select="@Collection"/><xsl:text> (</xsl:text><xsl:value-of select="@Item"/><xsl:text>)"&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;summary>Gets type-sfafe wrapper for &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/>&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;param name="Collection">A &lt;see cref="</xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>"/> to be wrapped&lt;/param>&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;returns>&lt;see cref="</xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>"/> that wraps &lt;paramref name="Collection"/>&lt;/returns>&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;exception cref="ArgumentNullException">&lt;paramref name="Collection"/> is null&lt;/exception>&#xd;&#xa;</xsl:text>
        <xsl:text>        Public Shared Function GetWrapper(ByVal Collection As </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>) As </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
        <xsl:text>            Return New </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>(Collection)&#xd;&#xa;</xsl:text>
        <xsl:text>        End Function&#xd;&#xa;</xsl:text>
        
        <!--Header-->
        <xsl:choose>
        <xsl:when test="@RW='1' or @RW='true'">
        <xsl:text>        ''' &lt;summary>Wraps &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/> as &lt;see cref="IIndexableCollection(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer)"/>)"/>&lt;/summary> &#xd;&#xa;</xsl:text>
        </xsl:when>
        <xsl:otherwise>
        <xsl:text>        ''' &lt;summary>Wraps &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/> as &lt;see cref="IReadOnlyIndexableCollection(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer)"/>)"/>&lt;/summary> &#xd;&#xa;</xsl:text>        
        </xsl:otherwise>
        </xsl:choose>
        
        <xsl:text>        &lt;EditorBrowsable(EditorBrowsableState.Advanced)> _&#xd;&#xa;</xsl:text>
        <xsl:text>        Public NotInheritable Class </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>

        <xsl:choose>
        <xsl:when test="@RW='1' or @RW='true'">
        <xsl:text>            Inherits SpecializedWrapper(Of </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>, </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>)&#xd;&#xa;</xsl:text>            
        </xsl:when>
        <xsl:otherwise>
        <xsl:text>            Inherits SpecializedReadOnlyWrapper(Of </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>, </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>)&#xd;&#xa;</xsl:text>
        </xsl:otherwise>
        </xsl:choose>

        <!--Implements-->
        <xsl:choose>
            <xsl:when test="(@Addable='1' or @Addable='true') and not(@RW='1' or @RW='true') and (@RemoveAt='1' or @RemoveAt='true')">
                <xsl:text>            Implements IAddableRemovable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer)&#xd;&#xa;</xsl:text>                
            </xsl:when>
            <xsl:when test="(@Addable='1' or @Addable='true') and not(@RW='1' or @RW='true')">
                <xsl:text>            Implements IAddable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>)&#xd;&#xa;</xsl:text>
            </xsl:when>
            <xsl:when test="(@RemoveAt='1' or @RemoveAt='true') and not(@RW='1' or @RW='true')">
                <xsl:text>            Implements IRemovable(Of Integer)&#xd;&#xa;</xsl:text>                
            </xsl:when>
            <xsl:when test="@RemoveAt='1' or @RemoveAt='true'">
                <xsl:text>            Implements IAddableRemovable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer)&#xd;&#xa;</xsl:text>                
            </xsl:when>
        </xsl:choose>

        <xsl:choose>
            <xsl:when test="(@IndexOf='1' or @IndexOf='true') and (@RW='1' or @RW='true')">
                <xsl:text>             Implements ISearchable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer)&#xd;&#xa;</xsl:text>
            </xsl:when>
            <xsl:when test="(@IndexOf='1' or @IndexOf='true')">
                <xsl:text>             Implements IReadOnlySearchable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer)&#xd;&#xa;</xsl:text>
            </xsl:when>
        </xsl:choose>

        <xsl:if test="(@Insert='1' or @Insert='true') and (@RW='1' or @RW='true')">
            <xsl:text>                 Implements IInsertable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer)&#xd;&#xa;</xsl:text>
        </xsl:if>

        <xsl:if test="(@Write='1' or @Write='true') and not(@RW='1' or @RW='true')">
            <xsl:text>                 Implements IIndexable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer)&#xd;&#xa;</xsl:text>
        </xsl:if>

        <!--Members-->
        <xsl:if test="swd:snippet">
            <xsl:value-of select="swd:snippet/text()"/>
        </xsl:if>
        
        <xsl:text>            ''' &lt;summary>CTor&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;param name="Collection">Collection to be wrapped&lt;/param>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;exception cref="ArgumentNullException">&lt;paramref name="Collection"/> is null&lt;/exception>&#xd;&#xa;</xsl:text>
        <xsl:text>            Public Sub New(ByVal Collection As </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>)&#xd;&#xa;</xsl:text>
        <xsl:text>                MyBase.new(Collection)&#xd;&#xa;</xsl:text>
        <xsl:text>            End Sub&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;summary>Gets or sets value on specified index&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;param name="index">Index to set or obtain value&lt;/param>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;returns>Value lying on specified &lt;paramref name="index"/>&lt;/returns>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;value>New value to be stored at specified index&lt;/value>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;exception cref="ArgumentException">Specified &lt;paramref name="index"/> is invalid&lt;/exception>&#xd;&#xa;</xsl:text>
        
        <xsl:choose>
        <xsl:when test="@RW='1' or @RW='true'">
        <xsl:text>            Public Overrides Property Item(ByVal index As Integer) As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
        </xsl:when>
        <xsl:otherwise>
        <xsl:text>            Protected Overrides ReadOnly Property ItemRO(ByVal index As Integer) As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>                
        </xsl:otherwise>
        </xsl:choose>
        
        <xsl:text>                Get&#xd;&#xa;</xsl:text>
        <xsl:text>                    Return Collection.Item(index)&#xd;&#xa;</xsl:text>
        <xsl:text>                End Get&#xd;&#xa;</xsl:text>

        <xsl:if test="@RW='1' or @RW='true' or @Write='1' or @Write='true' or (not(@RW='1' or @RW='true') and (@RemoveAt='1' or @RemoveAt='true' and (@Addable='1' or @Addable='true')))">
            <xsl:if test="not(@RW='1' or @RW='true') and ((@RemoveAt='1' or @RemoveAt='true') and (@Addable='1' or @Addable='true') or (@Write='1' or @Write='true'))">
                <xsl:text>            End Property&#xd;&#xa;</xsl:text>
                <xsl:text xml:space="preserve">            </xsl:text><xsl:choose><xsl:when test="@Write='1' or @Write='true'">Public</xsl:when><xsl:otherwise>Private</xsl:otherwise></xsl:choose><xsl:text> Property ItemRW(ByVal Index As Integer) As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text> Implements IIndexable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer).Item&#xd;&#xa;</xsl:text>
                <xsl:text>                &lt;DebuggerStepThrough()> Get&#xd;&#xa;</xsl:text>
                <xsl:text>                    Return Item(index)&#xd;&#xa;</xsl:text>
                <xsl:text>                End Get&#xd;&#xa;</xsl:text>
            </xsl:if>
        <xsl:text>                Set(ByVal value As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>)&#xd;&#xa;</xsl:text>
        <xsl:choose>
            <xsl:when test="swd:setter">
                <xsl:value-of select="swd:setter/text()"/>
                <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
            </xsl:when>
            <xsl:when test="(@NoSetter='true' or @NoSetter='1') and (@Insert='true' or @Insert='1') and (@RemoveAt='true' or @RemoveAt='1')">
                <xsl:text>                    Me.RemoveAt(index)&#xd;&#xa;</xsl:text>
                <xsl:text>                    Me.Insert(index,value)&#xd;&#xa;</xsl:text>
            </xsl:when>
            <xsl:when test="(@NoSetter='true' or @NoSetter='1')">
                <xsl:text>                    If index &lt; 0 OrElse index >= Me.Count Then Throw New ArgumentOutOfRangeException("index","index must be in range of the collection.") 'Localize:Exception&#xd;&#xa;</xsl:text>
                <xsl:text>                    Dim OldCollection As New List(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>)(Me)&#xd;&#xa;</xsl:text>
                <xsl:text>                    Me.Clear&#xd;&#xa;</xsl:text>
                <xsl:text>                    For i% = 0 To OldCollection.Count - 1&#xd;&#xa;</xsl:text>
                <xsl:text>                        If i = index Then&#xd;&#xa;</xsl:text>
                <xsl:text>                            Me.Add(value)&#xd;&#xa;</xsl:text>
                <xsl:text>                        Else&#xd;&#xa;</xsl:text>
                <xsl:text>                            Me.Add(OldCollection(i))&#xd;&#xa;</xsl:text>
                <xsl:text>                        End If&#xd;&#xa;</xsl:text>
                <xsl:text>                    Next i&#xd;&#xa;</xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>                    Collection.Item(index) = value&#xd;&#xa;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
        <xsl:text>                End Set&#xd;&#xa;</xsl:text>
        </xsl:if>
        
        <xsl:text>            End Property&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;summary>Converts &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/> to &lt;see cref="</xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>"/>&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;param name="a">A &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/> to be converted&lt;/param>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;returns>A &lt;see cref="</xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>"/> which wraps &lt;paramref name="a"/>&lt;/returns>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;exception cref="ArgumentNullException">&lt;paramref name="a"/> is null&lt;/exception>&#xd;&#xa;</xsl:text>
        <xsl:text>            Public Overloads Shared Widening Operator CType(ByVal a As </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>) As </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
        <xsl:text>                If a Is Nothing Then Throw New ArgumentNullException("a")&#xd;&#xa;</xsl:text>
        <xsl:text>                Return New </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>(a)&#xd;&#xa;</xsl:text>
        <xsl:text>            End Operator&#xd;&#xa;</xsl:text>
        
        <xsl:if test="(@Addable='1' or @Addable='true') and not(@RW='true' or @RW='1')">
        <xsl:text>            ''' &lt;summary>Adds item to collection&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;param name="item">Item to be added&lt;/param>&#xd;&#xa;</xsl:text>
        <xsl:text>            Public Sub Add(ByVal item As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>) Implements IAddable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>).Add&#xd;&#xa;</xsl:text>
        <xsl:choose>
            <xsl:when test="swd:add">
                <xsl:value-of select="swd:add/text()"/>
                <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>                Collection.Add(item)&#xd;&#xa;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
        <xsl:text>            End Sub&#xd;&#xa;</xsl:text>
        </xsl:if>

        <xsl:if test="@RW='1' or @RW='true'">
            <xsl:text>            ''' &lt;summary>Adds an item to the &lt;see cref="T:System.Collections.Generic.ICollection`1" />.&lt;/summary>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;param name="item">The object to add to the &lt;see cref="T:System.Collections.Generic.ICollection`1" />.&lt;/param>&#xd;&#xa;</xsl:text>
            <xsl:text>            Public Overrides Sub Add(ByVal item As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>)&#xd;&#xa;</xsl:text>
            <xsl:choose>
            <xsl:when test="swd:add">
                <xsl:value-of select="swd:add/text()"/>
                <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
            </xsl:when>
            <xsl:otherwise>
            <xsl:text>                Collection.Add(item)&#xd;&#xa;</xsl:text>    
            </xsl:otherwise>
            </xsl:choose>
            <xsl:text>            End Sub&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;summary>Removes the first occurrence of a specific object from the &lt;see cref="T:System.Collections.Generic.ICollection`1" />.&lt;/summary>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;returns>true if &lt;paramref name="item" /> was successfully removed from the &lt;see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if &lt;paramref name="item" /> is not found in the original &lt;see cref="T:System.Collections.Generic.ICollection`1" />.&lt;/returns>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;param name="item">The object to remove from the &lt;see cref="T:System.Collections.Generic.ICollection`1" />.&lt;/param>&#xd;&#xa;</xsl:text>
            <xsl:text>            Public Overrides Function Remove(ByVal item As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>) As Boolean&#xd;&#xa;</xsl:text>
            <xsl:choose>
            <xsl:when test="swd:remove">
                <xsl:value-of select="swd:remove/text()"/>
                <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
            </xsl:when>
            <xsl:when test="@RemoveIsSub='1' or @RemoveIsSub='true'">
            <xsl:text>                Dim OldCount = Count&#xd;&#xa;</xsl:text>
            <xsl:text>                Collection.Remove(item)&#xd;&#xa;</xsl:text>
            <xsl:text>                Return Count &lt; OldCount&#xd;&#xa;</xsl:text>        
            </xsl:when>
            <xsl:otherwise>
            <xsl:text>                Return Collection.Remove(item)&#xd;&#xa;</xsl:text>
            </xsl:otherwise>
            </xsl:choose>
            <xsl:text>            End Function&#xd;&#xa;</xsl:text>
        </xsl:if>
        <xsl:if test="@RW='1' or @RW='true' or @Clear='true' or @Clear='1' or ((@RemoveAt='1' or @RemoveAt='true') and (@Addable='1' or @Addable='true'))">
            <xsl:text>            </xsl:text><xsl:choose><xsl:when test="@RW='1' or @RW='true'">Public Overrides</xsl:when><xsl:when test="@Clear='true' or @Clear='1'">Public</xsl:when><xsl:otherwise>Private</xsl:otherwise></xsl:choose><xsl:text> Sub Clear()&#xd;&#xa;</xsl:text>
            <xsl:choose>
                <xsl:when test="swd:clear">
                    <xsl:value-of select="swd:clear/text()"/>
                    <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
                </xsl:when>
                <xsl:when test="(@NoClear='1' or @NoClear='true') and (@RemoveAt='1' or @RemoveAt='true')">
                    <xsl:text>                While Me.Count > 0&#xd;&#xa;</xsl:text>
                    <xsl:text>                    Me.RemoveAt(0)&#xd;&#xa;</xsl:text>
                    <xsl:text>                End While</xsl:text>
                </xsl:when>
                <xsl:when test="(@NoClear='1' or @NoClear='true')">
                    <xsl:text>                While Me.Count > 0&#xd;&#xa;</xsl:text>
                    <xsl:text>                    Dim en = Me.GetEnumerator&#xd;&#xa;</xsl:text>
                    <xsl:text>                    en.MoveNext&#xd;&#xa;</xsl:text>
                    <xsl:text>                    Me.Remove(en.Current)&#xd;&#xa;</xsl:text>
                    <xsl:text>                    en.Dispose&#xd;&#xa;</xsl:text>
                    <xsl:text>                End While&#xd;&#xa;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>                Collection.Clear()&#xd;&#xa;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>            End Sub&#xd;&#xa;</xsl:text>
        </xsl:if>
        
        <xsl:if test="@RemoveAt='1' or @RemoveAt='true'">
            <xsl:text>            ''' &lt;summary>Removes item at specified index&lt;/summary>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;param name="Index">Index to remove item at&lt;/param>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;exception cref="ArgumentException">Index is not valid&lt;/exception>&#xd;&#xa;</xsl:text>
            <xsl:text>            Public Sub RemoveAt(Index As Integer) Implements IRemovable(Of Integer).RemoveAt&#xd;&#xa;</xsl:text>
            <xsl:choose>
                <xsl:when test="swd:remove-at">
                    <xsl:value-of select="swd:remove-at/text()"/>
                    <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>                Collection.RemoveAt(Index)&#xd;&#xa;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>            End Sub&#xd;&#xa;</xsl:text>
        </xsl:if>

        <xsl:if test="@Contains='auto' or @Contains='IndexOf' or (not(@Contains) and (@IndexOf='1' or @IndexOf='true'))">
            <xsl:text>            ''' &lt;summary>Determines whether the &lt;see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.&lt;/summary>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;returns>true if &#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;paramref name="item" /> is found in the &lt;see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.&lt;/returns>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;param name="item">The object to locate in the &lt;see cref="T:System.Collections.Generic.ICollection`1" />.&lt;/param>&#xd;&#xa;</xsl:text>
            <xsl:text>            Public </xsl:text><xsl:if test="@RW='true' or @RW='1'"><xsl:text>Overrides </xsl:text></xsl:if><xsl:text>Function Contains(ByVal item As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>) As Boolean</xsl:text><xsl:if test="@IndexOf='true' or @IndexOf='1'"><xsl:text> Implements IReadOnlySearchable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer).Contains</xsl:text></xsl:if><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
            <xsl:choose>
                <xsl:when test="swd:contains">
                    <xsl:value-of select="swd:contains/text()"/>
                    <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
                </xsl:when>
                <xsl:when test="@Contains='auto'">
                    <xsl:text>                Return Collection.Contains(item)&#xd;&#xa;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>                Return Me.IndexOf(item) >= 0&#xd;&#xa;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            
            <xsl:text>            End Function&#xd;&#xa;</xsl:text>
        </xsl:if>
        <xsl:if test="@IndexOf='1' or @IndexOf='true'">
            <xsl:if test="not(@Contains='auto' or @Contains='IndexOf' or (not(@Contains) and (@IndexOf='1' or @IndexOf='true')))">
                <xsl:text>            ''' &lt;summary>Determines whether the &lt;see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.&lt;/summary>&#xd;&#xa;</xsl:text>
                <xsl:text>            ''' &lt;returns>true if &#xd;&#xa;</xsl:text>
                <xsl:text>            ''' &lt;paramref name="item" /> is found in the &lt;see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.&lt;/returns>&#xd;&#xa;</xsl:text>
                <xsl:text>            ''' &lt;param name="item">The object to locate in the &lt;see cref="T:System.Collections.Generic.ICollection`1" />.&lt;/param>&#xd;&#xa;</xsl:text>
                <xsl:text>            Public </xsl:text><xsl:if test="@RW='true' or @RW='1'"><xsl:text>Overrides </xsl:text></xsl:if><xsl:text>Function Contains(ByVal item As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>) As Boolean Implements IReadOnlySearchable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer).Contains&#xd;&#xa;</xsl:text>
                <xsl:choose>
                    <xsl:when test="swd:contains">
                        <xsl:value-of select="swd:contains/text()"/>
                        <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:text>Return Me.IndexOf(item) >= 0&#xd;&#xa;</xsl:text>
                        <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
                    </xsl:otherwise>
                </xsl:choose>
                <xsl:text>            End Function&#xd;&#xa;</xsl:text>
            </xsl:if>
            <xsl:text>            ''' &lt;summary>Gets index at which lies given object&lt;/summary>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;param name="item">Object to search for&lt;/param>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;returns>Index of first occurence of &lt;paramref name="item"/> in collection. If &lt;paramref name="item"/> is not present in collection returns -1.&lt;/returns>&#xd;&#xa;</xsl:text>
            <xsl:text>            Function IndexOf(ByVal item As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>) As Integer Implements IReadOnlySearchable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer).IndexOf&#xd;&#xa;</xsl:text>
            <xsl:choose>
                <xsl:when test="swd:index-of">
                    <xsl:value-of select="swd:index-of"/>
                    <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>                Return Collection.IndexOf(item)&#xd;&#xa;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>            End Function&#xd;&#xa;</xsl:text>
        </xsl:if>

        <xsl:if test="(@Insert='1' or @Insert='true') and (@RW='1' or @RW='true')">
            <xsl:text>            ''' &lt;summary>Inserts item into collection at specified index&lt;/summary>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;param name="index">Index to insert item onto&lt;/param>&#xd;&#xa;</xsl:text>
            <xsl:text>            ''' &lt;param name="item">Item to be inserted&lt;/param>&#xd;&#xa;</xsl:text>
            <xsl:text>            Sub Insert(ByVal index As Integer, ByVal item As </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>) Implements IInsertable(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>, Integer).Insert&#xd;&#xa;</xsl:text>
            <xsl:choose>
                <xsl:when test="swd:insert">
                    <xsl:value-of select="swd:insert/text()"/>
                    <xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>                Collection.Insert(index, item)&#xd;&#xa;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>            End Sub&#xd;&#xa;</xsl:text>
        </xsl:if>
        
        <xsl:text>        End Class&#xd;&#xa;</xsl:text>
        <xsl:text>#End Region&#xd;&#xa;</xsl:text>
    </xsl:template>

    <xsl:template match="swd:IList">
        <xsl:variable name="ColFull" select="concat(@CollectionNamespace,'.',@Collection)"/>
        <xsl:variable name="ItemNamespace">
            <xsl:choose>
                <xsl:when test="@ItemNamespace">
                    <xsl:value-of select="@ItemNamespace"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="@CollectionNamespace"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="ItemFull" select="concat(normalize-space($ItemNamespace),'.',@Item)"/>
        <xsl:variable name="WrapperName_">
            <xsl:choose>
                <xsl:when test="@WrapperName">
                    <xsl:value-of select="@WrapperName"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="concat(@Collection,'TypeSafeWrapper')"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="WrapperName" select="normalize-space($WrapperName_)"/>
        
        <!--GetWrapper--> 
        <xsl:text>#Region "</xsl:text><xsl:value-of select="@Collection"/><xsl:text> (</xsl:text><xsl:value-of select="@Item"/><xsl:text>)"&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;summary>Gets type-sfafe wrapper for &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/>&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;param name="Collection">A &lt;see cref="</xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>"/> to be wrapped&lt;/param>&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;returns>&lt;see cref="</xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>"/> that wraps &lt;paramref name="Collection"/>&lt;/returns>&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;exception cref="ArgumentNullException">&lt;paramref name="Collection"/> is null&lt;/exception>&#xd;&#xa;</xsl:text>
        <xsl:text>        Public Shared Function GetWrapper(ByVal Collection As </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>) As </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
        <xsl:text>            Return New </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>(Collection)&#xd;&#xa;</xsl:text>
        <xsl:text>        End Function&#xd;&#xa;</xsl:text>
        
        <!--Header-->
        <xsl:text>        ''' &lt;summary>Wraps &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/> as &lt;see cref="IList(Of </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>)"/>)"/>&lt;/summary> &#xd;&#xa;</xsl:text>
        <xsl:text>        &lt;EditorBrowsable(EditorBrowsableState.Advanced)> _&#xd;&#xa;</xsl:text>
        <xsl:text>        Public NotInheritable Class </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
        <xsl:text>            Inherits IListTypeSafeWrapper(Of </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>, </xsl:text><xsl:value-of select="$ItemFull"/><xsl:text>)&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;summary>CTor&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;param name="Collection">Collection to wrapp&lt;/param>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;exception cref="ArgumentNullException">&lt;paramref name="Collection"/> is null&lt;/exception>&#xd;&#xa;</xsl:text>
        <xsl:text>            Public Sub New(ByVal Collection As </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>)&#xd;&#xa;</xsl:text>
        <xsl:text>                MyBase.new(Collection)&#xd;&#xa;</xsl:text>
        <xsl:text>            End Sub&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;summary>Converts &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/> to &lt;see cref="</xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>"/>&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;param name="a">A &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/> to be converted&lt;/param>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;returns>A &lt;see cref="</xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>"/> which wraps &lt;paramref name="a"/>&lt;/returns>&#xd;&#xa;</xsl:text>
        <xsl:text>            ''' &lt;exception cref="ArgumentNullException">&lt;paramref name="a"/> is null&lt;/exception>&#xd;&#xa;</xsl:text>
        <xsl:text>            Public Overloads Shared Widening Operator CType(ByVal a As </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>) As </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
        <xsl:text>                If a Is Nothing Then Throw New ArgumentNullException("a")&#xd;&#xa;</xsl:text>
        <xsl:text>                Return New </xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>(a)&#xd;&#xa;</xsl:text>
        <xsl:text>            End Operator&#xd;&#xa;</xsl:text>
        <xsl:text>        End Class&#xd;&#xa;</xsl:text>
        <xsl:text>#End Region&#xd;&#xa;</xsl:text>
</xsl:template>

    <xsl:template match="swd:wrapper | swd:IList" mode="Module">
        <xsl:variable name="ColFull" select="concat(@CollectionNamespace,'.',@Collection)"/>
        <xsl:variable name="ItemNamespace">
            <xsl:choose>
                <xsl:when test="@ItemNamespace">
                    <xsl:value-of select="@ItemNamespace"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="@CollectionNamespace"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="ItemFull" select="concat(normalize-space($ItemNamespace),'.',@Item)"/>
        <xsl:variable name="WrapperName_">
            <xsl:choose>
                <xsl:when test="@WrapperName">
                    <xsl:value-of select="@WrapperName"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="concat(@Collection,'TypeSafeWrapper')"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="WrapperName" select="normalize-space($WrapperName_)"/>
        
        <xsl:text>        ''' &lt;summary>Gets type-safe wrapper of &lt;see cref="</xsl:text><xsl:value-of select="$ColFull"/><xsl:text>"/>&lt;/summary>&#xd;&#xa;</xsl:text>
        <xsl:text>        ''' &lt;exception cref="ArgumentNullException">&lt;paramref name="Collection"/> is null&lt;/exception>&#xd;&#xa;</xsl:text>
        <xsl:text>        &lt;Extension()> Public Function AsTypeSafe(ByVal Collection As </xsl:text><xsl:value-of select="$ColFull"/><xsl:text>) As SpecializedWrapper.</xsl:text><xsl:value-of select="$WrapperName"/><xsl:text xml:space="preserve">&#xd;&#xa;</xsl:text>
        <xsl:text>            Return New SpecializedWrapper.</xsl:text><xsl:value-of select="$WrapperName"/><xsl:text>(Collection)&#xd;&#xa;</xsl:text>
        <xsl:text>        End Function&#xd;&#xa;</xsl:text>
    </xsl:template>
    
</xsl:stylesheet>
