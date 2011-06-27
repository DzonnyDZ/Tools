Imports System.Xml
Imports System.Xml.Linq

Namespace ComponentModelT
    ''' <summary>Provides an interface for objects that are wrappers around XML nodes (based on <see cref="System.Xml"/>)</summary>
    ''' <version version="1.5.4">This interface is new in version 1.5.4</version>
    Public Interface IXmlNodeWrapper
        ''' <summary>Gets node this instance wraps</summary>
        ReadOnly Property Node As XmlNode
    End Interface
    ''' <summary>Provides an interface for objects that are wrappers around XML nodes (based on <see cref="System.Xml.Linq"/>)</summary>
    ''' <version version="1.5.4">This interface is new in version 1.5.4</version>
    Public Interface IXNodeWrapper
        ''' <summary>Gets node this instance wraps</summary>
        ReadOnly Property Node As XNode
    End Interface

    ''' <summary>Provides an interface for objects that are wrappers around XML element (based on <see cref="System.Xml"/>)</summary>
    ''' <version version="1.5.4">This interface is new in version 1.5.4</version>
    Public Interface IXmlElementWrapper : Inherits IXmlNodeWrapper
        ''' <summary>Gets XML element this instance wraps</summary>
        ReadOnly Property Element As XmlElement
    End Interface
    ''' <summary>Provides an interface for objects that are wrappers around XML element (based on <see cref="System.Xml.Linq"/>)</summary>
    ''' <version version="1.5.4">This interface is new in version 1.5.4</version>
    Public Interface IXElementWrapper : Inherits IXNodeWrapper
        ''' <summary>Gets XML element this instance wraps</summary>
        ReadOnly Property Element As XElement
    End Interface
    ''' <summary>Provides an interface for objects that are wrappers around XML document (based on <see cref="System.Xml"/>)</summary>
    ''' <version version="1.5.4">This interface is new in version 1.5.4</version>
    Public Interface IXmlDocumentWrapper : Inherits IXmlNodeWrapper
        ''' <summary>Gets XML document this instance wraps</summary>
        ReadOnly Property Document As XmlDocument
    End Interface
    ''' <summary>Provides an interface for objects that are wrappers around XML document (based on <see cref="System.Xml.Linq"/>)</summary>
    ''' <version version="1.5.4">This interface is new in version 1.5.4</version>
    Public Interface IXDocumentWrapper : Inherits IXNodeWrapper
        ''' <summary>Gets XML document this instance wraps</summary>
        ReadOnly Property Document As XDocument
    End Interface
End Namespace
