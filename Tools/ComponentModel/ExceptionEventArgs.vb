Imports Severity = System.Xml.Schema.XmlSeverityType
#If Config <= Nightly Then 'Stage:Nightly
Namespace ComponentModelT
    ''' <summary>Event arguments carrying <see cref="Exception"/></summary>
    Public Class ExceptionEventArgs
        Inherits EventArgs
        ''' <summary>Contains value of the <see cref="System.Exception"/> property</summary>
        Private ReadOnly _Exception As Exception
        ''' <summary>Gtes the <see cref="System.Exception"/> carried by this instance</summary>
        Public ReadOnly Property Exception() As Exception
            Get
                Return _Exception
            End Get
        End Property
        ''' <summary>CTor from <see cref="System.Exception"/></summary>
        ''' <param name="Exception"><see cref="System.Exception"/> to carry</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As Exception)
            If Exception Is Nothing Then Throw New ArgumentNullException("Exception")
            Me._Exception = Exception
        End Sub
        ''' <summary>CTor from <see cref="System.Exception"/> and <see cref="System.Xml.Schema.XmlSeverityType"/></summary>
        ''' <param name="Exception"><see cref="System.Exception"/> to carry</param>
        ''' <param name="Severity">Severity (level) of the exception</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As Exception, ByVal Severity As Severity)
            Me.new(Exception)
            Me._Severity = Severity
        End Sub
        ''' <summary>Contains value of the <see cref="Severity"/> property</summary>
        Private _Severity As Xml.Schema.XmlSeverityType = Xml.Schema.XmlSeverityType.Error
        ''' <summary>gets severity (level) of the exception</summary>
        ''' <remarks>Default value is <see cref="Severity.Error"/></remarks>
        <DefaultValue(GetType(Severity), "Error")> _
        Public ReadOnly Property Severity() As Severity
            Get
                Return _Severity
            End Get
        End Property
    End Class
    ''' <summary>Type-safe implementation of <see cref="ExceptionEventArgs"/></summary>
    ''' <typeparam name="T">Type of <see cref="Exception"/> carried</typeparam>
    Public Class ExceptionEventArgs(Of T As Exception)
        Inherits ExceptionEventArgs
        ''' <summary>CTor from <typeparamref name="T"/></summary>
        ''' <param name="Exception"><typeparamref name="T"/> to carry</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As T)
            MyBase.new(Exception)
        End Sub
        ''' <summary>CTor from <typeparamref name="T"/> and <see cref="System.Xml.Schema.XmlSeverityType"/></summary>
        ''' <param name="Exception"><typeparamref name="T"/> to carry</param>
        ''' <param name="Severity">Severity (level) of the exception</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As T, ByVal Severity As Severity)
            MyBase.new(Exception, Severity)
        End Sub
        ''' <summary>Gtes the <typeparamref name="T"/> carried by this instance</summary>
        Public Shadows ReadOnly Property Exception() As T
            Get
                Return MyBase.Exception
            End Get
        End Property
    End Class
    ''' <summary>Event arguments to carry exception with possibility to let the consumer to decide to ignore the exception</summary>
    Public Class RecoveryExceptionEventArgs
        Inherits ExceptionEventArgs
        ''' <summary>Contains value of the <see cref="Recover"/> property</summary>
        Private _Recover As Boolean = False
        ''' <summary>Gets or sets value indicating if callee (sender) of the event should recover the exception</summary>
        ''' <remarks>Default value is false, but this can be changed by the callee.</remarks>
        Public Property Recover() As Boolean
            Get
                Return _Recover
            End Get
            Set(ByVal value As Boolean)
                _Recover = value
            End Set
        End Property
        ''' <summary>CTor from <see cref="System.Exception"/></summary>
        ''' <param name="Exception"><see cref="System.Exception"/> to carry</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As Exception)
            MyBase.new(Exception)
        End Sub
        ''' <summary>CTor from <see cref="System.Exception"/> and <see cref="System.Xml.Schema.XmlSeverityType"/></summary>
        ''' <param name="Exception"><see cref="System.Exception"/> to carry</param>
        ''' <param name="Severity">Severity (level) of the exception</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        ''' <remarks>Using this constructor the value of the <see cref="Recover"/> property is set to trie when <paramref name="Severity"/> is <see cref="Severity.Warning"/> or any value greater than or equal to 100</remarks>
        Public Sub New(ByVal Exception As Exception, ByVal Severity As Severity)
            MyBase.new(Exception, Severity)
            Recover = Severity = Xml.Schema.XmlSeverityType.Warning OrElse Severity >= 100
        End Sub
        ''' <summary>CTor from <see cref="System.Exception"/> and <see cref="Recover"/> property value</summary>
        ''' <param name="Exception"><see cref="System.Exception"/> to carry</param>
        ''' <param name="Recover">Indicates initial value of the <see cref="Recover"/> property (true to recover the error, false to throw <see cref="System.Exception"/>)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As Exception, ByVal Recover As Boolean)
            MyBase.New(Exception)
            Me.Recover = Recover
        End Sub
        ''' <summary>Ctor from <see cref="System.Exception"/>, <see cref="System.Xml.Schema.XmlSeverityType"/> and <see cref="Recover"/> property</summary>
        ''' <param name="Exception"><see cref="System.Exception"/> to carry</param>
        ''' <param name="Recover">Indicates initial value of the <see cref="Recover"/> property (true to recover the error, false to throw <see cref="System.Exception"/>)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        ''' <param name="Severity">Severity (level) of the exception</param>
        Public Sub New(ByVal Exception As Exception, ByVal Severity As Severity, ByVal Recover As Boolean)
            MyBase.new(Exception, Severity)
            Me.Recover = Recover
        End Sub
    End Class
    ''' <summary>Type-safe implementation of <see cref="RecoveryExceptionEventArgs"/></summary>
    Public Class RecoveryExceptionEventArgs(Of T As Exception)
        Inherits RecoveryExceptionEventArgs
        ''' <summary>Gtes the <typeparamref name="T"/> carried by this instance</summary>
        Public Shadows ReadOnly Property Exception() As T
            Get
                Return MyBase.Exception
            End Get
        End Property
        ''' <summary>CTor from <typeparamref name="T"/></summary>
        ''' <param name="Exception"><typeparamref name="T"/> to carry</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As T)
            MyBase.new(Exception)
        End Sub
        ''' <summary>CTor from <typeparamref name="T"/> and <see cref="Severity"/></summary>
        ''' <param name="Exception"><typeparamref name="T"/> to carry</param>
        ''' <param name="Severity">Severity (level) of the exception</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        ''' <remarks>Using this constructor the value of the <see cref="Recover"/> property is set to trie when <paramref name="Severity"/> is <see cref="Severity.Warning"/> or any value greater than or equal to 100</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As T, ByVal Severity As Severity)
            MyBase.new(Exception, Severity)
        End Sub
        ''' <summary>CTor from <typeparamref name="T"/> and <see cref="Recover"/> property value</summary>
        ''' <param name="Exception"><typeparamref name="T"/> to carry</param>
        ''' <param name="Recover">Indicates initial value of the <see cref="Recover"/> property (true to recover the error, false to throw <see cref="System.Exception"/>)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        Public Sub New(ByVal Exception As T, ByVal Recover As Boolean)
            MyBase.New(Exception, Recover)
        End Sub
        ''' <summary>Ctor from <typeparamref name="T"/>, <see cref="System.Xml.Schema.XmlSeverityType"/> and <see cref="Recover"/> property</summary>
        ''' <param name="Exception"><typeparamref name="T"/> to carry</param>
        ''' <param name="Recover">Indicates initial value of the <see cref="Recover"/> property (true to recover the error, false to throw <see cref="System.Exception"/>)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> is null</exception>
        ''' <param name="Severity">Severity (level) of the exception</param>
        Public Sub New(ByVal Exception As T, ByVal Severity As Severity, ByVal Recover As Boolean)
            MyBase.new(Exception, Severity, Recover)
        End Sub
    End Class
End Namespace
#End If
