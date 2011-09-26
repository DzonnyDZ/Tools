Imports System.Xml
Imports Tools.ExtensionsT
Imports System.Xml.Linq

Namespace ComponentModelT
#Region "Interfaces"
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
#End Region

#Region "Implementations"
    ''' <summary>Simple implementation of <see cref="IXmlNodeWrapper"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class XmlNodeWrapper
        Inherits Wrapper(Of XmlNode)
        Implements IXmlNodeWrapper
        ''' <summary>CTor - creates a new instance of the <see cref="XmlNodeWrapper"/> class</summary>
        ''' <param name="node">The <see cref="XmlNode"/> to wrap</param>
        ''' <exception cref="ArgumentNullException"><paramref name="node"/> is null</exception>
        Public Sub New(node As XmlNode)
            MyBase.New(node.ThrowIfNull("node"))
        End Sub

        ''' <summary>Gets the <see cref="XmlNode"/> wrapped by this instance</summary>
        Public ReadOnly Property Node As XmlNode Implements IXmlNodeWrapper.Node
            Get
                Return [Object]
            End Get
        End Property
    End Class

    ''' <summary>Simple implementation of <see cref="IXNodeWrapper"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class XNodeWrapper
        Inherits Wrapper(Of XNode)
        Implements IXNodeWrapper
        ''' <summary>CTor - creates a new instance of the <see cref="XNodeWrapper"/> class</summary>
        ''' <param name="node">The <see cref="XNode"/> to wrap</param>
        ''' <exception cref="ArgumentNullException"><paramref name="node"/> is null</exception>
        Public Sub New(node As XNode)
            MyBase.New(node.ThrowIfNull("node"))
        End Sub

        ''' <summary>Gets the <see cref="XNode"/> wrapped by this instance</summary>
        Public ReadOnly Property Node As XNode Implements IXNodeWrapper.Node
            Get
                Return [Object]
            End Get
        End Property
    End Class

    ''' <summary>Simple implementation of <see cref="IXmlElementWrapper"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class XmlElementWrapper
        Inherits XmlNodeWrapper
        Implements IXmlElementWrapper
        ''' <summary>CTor - creates a new instance of the <see cref="XmlElementWrapper"/> class</summary>
        ''' <param name="element">The <see cref="XmlElement"/> to wrap</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        Public Sub New(element As XmlElement)
            MyBase.New(element)
        End Sub

        ''' <summary>Gets the <see cref="XmlElement"/> wrapped by this instance</summary>
        Public ReadOnly Property Element As XmlElement Implements IXmlElementWrapper.Element
            Get
                Return Node
            End Get
        End Property
    End Class

    ''' <summary>Simple implementation of <see cref="IXElementWrapper"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class XElementWrapper
        Inherits XNodeWrapper
        Implements IXElementWrapper
        ''' <summary>CTor - creates a new instance of the <see cref="XElementWrapper"/> class</summary>
        ''' <param name="element">The <see cref="XElement"/> to wrap</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        Public Sub New(element As XElement)
            MyBase.New(element)
        End Sub

        ''' <summary>Gets the <see cref="XElement"/> wrapped by this instance</summary>
        Public ReadOnly Property Element As XElement Implements IXElementWrapper.Element
            Get
                Return Node
            End Get
        End Property
    End Class

    ''' <summary>Simple implementation of <see cref="IXmlDocumentWrapper"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class XmlDocumentWrapper
        Inherits XmlNodeWrapper
        Implements IXmlDocumentWrapper

        ''' <summary>CTor - creates a new instance of the <see cref="XmlDocumentWrapper"/> class</summary>
        ''' <param name="document">The <see cref="XmlDocument"/> to wrap</param>
        ''' <exception cref="ArgumentNullException"><paramref name="document"/> is null</exception>
        Public Sub New(document As XmlDocument)
            MyBase.New(document)
        End Sub

        ''' <summary>Gets the <see cref="XmlDocument"/> wrapped by this instance</summary>
        Public ReadOnly Property Document As XmlDocument Implements IXmlDocumentWrapper.Document
            Get
                Return Node
            End Get
        End Property
    End Class

    ''' <summary>Simple implementation of <see cref="IXDocumentWrapper"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class XDocumentWrapper
        Inherits XNodeWrapper
        Implements IXDocumentWrapper

        ''' <summary>CTor - creates a new instance of the <see cref="XDocumentWrapper"/> class</summary>
        ''' <param name="document">The <see cref="XDocument"/> to wrap</param>
        ''' <exception cref="ArgumentNullException"><paramref name="document"/> is null</exception>
        Public Sub New(Document As XDocument)
            MyBase.New(Document)
        End Sub

        ''' <summary>Gets the <see cref="XDocument"/> wrapped by this instance</summary>
        Public ReadOnly Property Document As XDocument Implements IXDocumentWrapper.Document
            Get
                Return Node
            End Get
        End Property
    End Class
#End Region
End Namespace
