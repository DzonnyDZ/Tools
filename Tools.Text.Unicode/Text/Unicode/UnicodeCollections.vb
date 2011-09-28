Imports System.Xml.Linq
Imports System.Xml.XPath
Imports System.Linq
Imports Tools.RuntimeT.CompilerServicesT
Imports Tools.TextT.UnicodeT
Imports Tools.ExtensionsT
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Xml
Imports Tools.ComponentModelT
Imports System.Globalization.CultureInfo

Namespace TextT.UnicodeT

    ''' <summary>Common base class for <see cref="UcdCollection(Of T)"/> generic classes</summary>
    ''' <remarks>This is helper class, you usually don't need to dela with it.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public MustInherit Class UcdCollection
        Implements IXElementWrapper, IEnumerable(Of IXElementWrapper)

        ''' <summary>When overriden in derived class gets XML element this instance wraps</summary>
        Public MustOverride ReadOnly Property Element As System.Xml.Linq.XElement Implements ComponentModelT.IXElementWrapper.Element

        ''' <summary>Gets node this instance wraps</summary>
        Private ReadOnly Property Node As System.Xml.Linq.XNode Implements ComponentModelT.IXNodeWrapper.Node
            Get
                Return Element
            End Get
        End Property

        ''' <summary>When overriden in derived class returns an enumerator that iterates through the collection of <see cref="IXElementWrapper"/>s.</summary>
        ''' <returns>A <see cref="IEnumerator(Of T)" />[<see cref="IXElementWrapper"/>] that can be used to iterate through the collection.</returns>
        Public MustOverride Function GetXElementWrapperEnumerator() As System.Collections.Generic.IEnumerator(Of ComponentModelT.IXElementWrapper) Implements System.Collections.Generic.IEnumerable(Of ComponentModelT.IXElementWrapper).GetEnumerator

        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        ''' <filterpriority>2</filterpriority>
        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetXElementWrapperEnumerator()
        End Function
    End Class

    ''' <summary>Generic class that serves as collection of <see cref="IXElementWrapper"/>-based classes</summary>
    ''' <typeparam name="T">Type of items in collection. The type must have public contructor that accepts exactly one parameter of type <see cref="XElement"/>.</typeparam>
    ''' <remarks>This is helper type. You usually don't need to dela with it.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class UcdCollection(Of T As IXElementWrapper)
        Inherits UcdCollection
        Implements IEnumerable(Of T)

        ''' <summary>CTor - creates a anew instance of the <see cref="UcdCollection(Of T)"/> class</summary>
        ''' <param name="element">A XML element that contains elements that populates instances of classes to iterate over.</param>
        ''' <remarks>Elements in <paramref name="element"/> must all be of type that's acceptable by <typeparamref name="T"/>'s constructor</remarks>
        Friend Sub New(element As XElement)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            _element = element
        End Sub
        Private ReadOnly _element As XElement


        ''' <summary>Gets XML element this instance wraps</summary>
        Public Overrides ReadOnly Property Element As System.Xml.Linq.XElement
            Get
                Return _element
            End Get
        End Property

        ''' <summary>Returns an enumerator that iterates through the collection of <see cref="IXElementWrapper"/>s.</summary>
        ''' <returns>A <see cref="IEnumerator(Of T)" />[<see cref="IXElementWrapper"/>] that can be used to iterate through the collection.</returns>
        Public Overrides Function GetXElementWrapperEnumerator() As System.Collections.Generic.IEnumerator(Of ComponentModelT.IXElementWrapper)
            Return GetEnumerator()
        End Function

        ''' <summary>Returns an enumerator that iterates through the collection of <typeparamref name="T"/>.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
            Return (From itm In Element.Elements Select DirectCast(Activator.CreateInstance(GetType(T), Element), T)).GetEnumerator
        End Function
    End Class

End Namespace