''' <summary>An interface for screensaver context</summary>
Public Interface IUnisaveContext

    ''' <summary>Shows a window modally</summary>
    ''' <param name="window">Window to be shown</param>
    ''' <returns>Result of window showed</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="window"/> is null</exception>
    Function ShowDialog(window As Window) As Boolean?
    ''' <summary>Shows a form modallly</summary>
    ''' <param name="form">Form to be shown</param>
    ''' <returns>Result of form shown</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="form"/> is null</exception>
    Function ShowDialog(form As Forms.Form) As Forms.DialogResult
    ''' <summary>Shows <see cref="System.Windows.Forms.CommonDialog"/> modally</summary>
    ''' <param name="dialog">A dialog to be shown</param>
    ''' <returns>Dialog result</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="dialog"/> is null</exception>
    Function ShowDialog(dialog As Forms.CommonDialog) As Forms.DialogResult
    ''' <summary>Shows <see cref="Microsoft.Win32.CommonDialog"/> modally</summary>
    ''' <param name="dialog">A dialog to be shown</param>
    ''' <returns>Dialog result</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="dialog"/> is null</exception>
    Function ShowDialog(dialog As Microsoft.Win32.CommonDialog) As Boolean?

End Interface
