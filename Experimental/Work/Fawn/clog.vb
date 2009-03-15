Imports System.Windows.Forms
''' <summary>Logovac� t��da - logov�n� do <see cref="ListBox">ListBoxu</see></summary>
Public Class clog
    ''' <summary><see  cref="ListBox"/>, do kter�ho se loguje</summary>
    Private mlog As ListBox
    ''' <summary>Posledn� zpr�va</summary>
    Private lastmsg As String
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Format"/></summary>
    Private _Format As String = "{1}"
    ''' <summary>CTor</summary>
    ''' <param name="ListBox"><see cref="ListBox"/> do kter�ho se m� logovat</param>
    ''' <exception cref="ArgumentNullException"><paramref name="ListBox"/> je null</exception>
    Public Sub New(ByVal ListBox As ListBox)
        If ListBox Is Nothing Then Throw New ArgumentNullException("ListBox")
        Me.mlog = ListBox
    End Sub
    ''' <summary>P�idat zpr�vu</summary>
    ''' <param name="line_text">Text zpr�vy</param>
    Public Sub add(ByVal line_text As String)
        If mlog.InvokeRequired Then
            mlog.Invoke(New Action(Of String)(AddressOf add), line_text)
            Exit Sub
        End If
        lastmsg = String.Format(Format, Now(), line_text)
        mlog.Items.Add(lastmsg)
        mlog.SelectedIndex = mlog.Items.Count - 1
        mlog.Refresh()
    End Sub
    ''' <summary>P�idat zpr�vu s form�tov�n�m</summary>
    ''' <param name="line_text">Text zpr�vy s form�tov�n�m</param>
    ''' <param name="obj">Polo�ky pro vlo�en� do textu</param>
    Public Sub add(ByVal line_text As String, ByVal ParamArray obj As Object())
        add(String.Format(line_text, obj))
    End Sub
    ''' <summary>Text posledn� zpr�vy</summary>
    ReadOnly Property lastMsgText() As String
        Get
            Return lastmsg
        End Get
    End Property
    ''' <summary>Form�tovac� �e�ezec pou�it� na zpr�vu</summary>
    ''' <remarks>{0} zastupuje aktu�ln� datum, {1} text zpr�vy</remarks>
    ''' <exception cref="System.ArgumentNullException">format being set is null.</exception>
    ''' <exception cref="System.FormatException">format bein set is invalid.-or- The number indicating an argument to format is less than zero, or greater than or equal to the number of specified objects to format (2).</exception>
    Public Property Format$()
        Get
            Return _Format
        End Get
        Set(ByVal value$)
            String.Format(value, Now, "test")
            _Format = value
        End Set
    End Property
End Class
