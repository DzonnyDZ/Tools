Imports System.ComponentModel

#If Config <= Nightly Then 'Stage: Nightly
Imports System.Windows.Forms
Namespace WindowsT.NativeT
    'ASAP: Mark, Wiki, Forum
    ''' <summary>Represents native window used in Microsoft Windows</summary>
    ''' <remarks>This class can be used to manipulate windows and controls that originates from non-.NET applications as well as .NET ones. It can be used in 64b environment as well.</remarks>
    Public Class Win32Window
        Implements IWin32Window, IDisposable, ICloneable(Of IWin32Window), ICloneable(Of Win32Window)
#Region "Basic"
        ''' <summary>Contains value of the <see cref="Handle"/> property</summary>
        Private _Handle As System.IntPtr
#Region "CTors & CTypes"
        ''' <summary>CTor from <see cref="Integer"/> handle</summary>
        ''' <param name="hWnd">Handle to window</param>
        Public Sub New(ByVal hWnd As Integer)
            _Handle = hWnd
        End Sub
        ''' <summary>CTor from <see cref="IntPtr"/> handle</summary>
        ''' <param name="hWnd">Handle to window</param>
        Public Sub New(ByVal hWnd As IntPtr)
            Me.New(CInt(hWnd))
        End Sub
        ''' <summary>CTor from <see cref="Control"/> (including <see cref="Form"/>)</summary>
        ''' <param name="Control">Control to create new instance from</param>
        Public Sub New(ByVal Control As Control)
            Me.New(Control.Handle)
        End Sub
        ''' <summary>CTor from <see cref="IWin32Window"/></summary>
        ''' <param name="Window">Window to create new instance from</param>
        Public Sub New(ByVal Window As IWin32Window)
            Me.New(Window.Handle)
        End Sub
        ''' <summary>Converts <see cref="Control"/> to <see cref="Win32Window"/></summary>
        ''' <param name="a">A <see cref="Control"/></param>
        ''' <returns>A <see cref="Win32Window"/> with same handle as <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Control) As Win32Window
            Return New Win32Window(a)
        End Operator
#End Region

        ''' <summary>Gets the handle to the window represented by the implementer.</summary>
        ''' <returns>A handle to the window represented by the implementer.</returns>
        Public ReadOnly Property Handle() As System.IntPtr Implements System.Windows.Forms.IWin32Window.Handle
            Get
                Return _Handle
            End Get
        End Property
        ''' <summary>Same as <see cref="Handle"/> but <see cref="Integer"/></summary>
        Public ReadOnly Property hWnd() As Integer
            Get
                Return Handle
            End Get
        End Property

#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary>Sets <see cref="Handle"/> to zero</summary>
        ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                End If
            End If
            _Handle = IntPtr.Zero
            Me.disposedValue = True
        End Sub
        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <remarks>Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.</remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        ''' <remarks>Use type-safe <see cref="Clone"/> instead</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <Obsolete("Use type-safe Clone instead")> _
        Private Function Clone__() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function
        ''' <summary>Implements <see cref="ICloneable(Of System.Windows.Forms.IWin32Window).Clone"/></summary>
        ''' <returns><see cref="Clone"/></returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Function Clone_() As System.Windows.Forms.IWin32Window Implements ICloneable(Of System.Windows.Forms.IWin32Window).Clone
            Return Clone()
        End Function
        ''' <summary>Creates new instance of <see cref="Win32Window"/> pointing to same window as curent instance</summary>
        ''' <returns>New instance pointing to same window as current instance</returns>
        ''' <remarks>In fact there is no need to clone <see cref="Win32Window"/> object, because it has no internal state other than <see cref="Handle"/></remarks>
        Public Function Clone() As Win32Window Implements ICloneable(Of Win32Window).Clone
            Return New Win32Window(Handle)
        End Function
#End Region
#Region "Basic properties and operations"
        ''' <summary>Gets or sets parent of current Window</summary>
        ''' <value>A <see cref="Win32Window"/> to reparent current window into. Can be null to un-parent current window completely.</value>
        ''' <returns>Current parent of current window. Can return null if current window has no parent or there was error when obtaining parent (ie. <see cref="Handle"/> is invalid)</returns>
        ''' <exception cref="API.Win32APIException">Setting failed. It may indicate that <see cref="hWnd"/> does not point to existing window.</exception>
        ''' <remarks>Setting value to <see cref="Win32Window"/> with <see cref="Handle"/> of zero has the same effect as setting it to null.</remarks>
        Public Property Parent() As Win32Window
            Get
                Dim ret As Integer = API.GetParent(hWnd)
                Return If(ret = 0, New Win32Window(ret), Nothing)
            End Get
            Set(ByVal value As Win32Window)
                If Not API.SetParent(hWnd, If(value Is Nothing, 0, value.hWnd)) Then _
                    Throw New API.Win32APIException()
            End Set
        End Property
        ''' <summary>Gets or sets specified window long of current window</summary>
        ''' <param name="Long">Long to get or set. Can be one of <see cref="API.[Public].WindowLongs"/> values or can be any user-defined integer</param>
        ''' <value>New value of window long</value>
        ''' <returns>Current value of window long</returns>
        Public Property WindowLong(ByVal [Long] As API.Public.WindowLongs) As Integer
            Get
                Return API.GetWindowLong(hWnd, [Long])
            End Get
            Set(ByVal value As Integer)
                API.SetWindowLong(hWnd, [Long], value)
            End Set
        End Property
#Region "Size & location"
        'ASAP: Comment, do not forget exceptions
        Public Sub Move(ByVal Left As Integer, ByVal Top As Integer, ByVal Width As Integer, ByVal Height As Integer, Optional ByVal Repaint As Boolean = True)
            If Not API.MoveWindow(hWnd, Left, Top, Width, Height, Repaint) Then _
                Throw New API.Win32APIException
        End Sub
        Public Sub Move(ByVal Rectangle As Rectangle)
            Move(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height)
        End Sub
        Public Property Area() As Rectangle
            Get
                Dim ret As API.RECT
                If API.GetWindowRect(hWnd, ret) Then
                    Return ret
                Else
                    Throw New API.Win32APIException
                End If
            End Get
            Set(ByVal value As Rectangle)
                Move(value)
            End Set
        End Property
        Public Property Size() As Size
            Get
                Return Area.Size
            End Get
            Set(ByVal value As Size)
                Area = New Rectangle(Location, value)
            End Set
        End Property
        Public Property Location() As Point
            Get
                Return Area.Location
            End Get
            Set(ByVal value As Point)
                Area = New Rectangle(value, Size)
            End Set
        End Property
        Public Property Left() As Integer
            Get
                Return Location.X
            End Get
            Set(ByVal value As Integer)
                Location = New Point(value, Top)
            End Set
        End Property
        Public Property Top() As Integer
            Get
                Return Location.Y
            End Get
            Set(ByVal value As Integer)
                Location = New Point(Left, value)
            End Set
        End Property
        Public Property Width() As Integer
            Get
                Return Size.Width
            End Get
            Set(ByVal value As Integer)
                Size = New Size(value, Height)
            End Set
        End Property
        Public Property Height() As Integer
            Get
                Return Size.Height
            End Get
            Set(ByVal value As Integer)
                Size = New Size(Width, value)
            End Set
        End Property
        Public ReadOnly Property Right() As Integer
            Get
                Return Area.Right
            End Get
        End Property
        Public ReadOnly Property Bottom() As Integer
            Get
                Return Area.Bottom
            End Get
        End Property
#End Region
#End Region
    End Class
End Namespace
#End If

