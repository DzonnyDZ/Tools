Namespace ReportingT.ReportingEngineLite
    ''' <summary>Interface used by report engine to write log messages</summary>
    Public Interface ILogger
        ''' <summary>Adds a message with given severity</summary>
        ''' <param name="message">Message text</param>
        ''' <param name="severity">Severity level</param>
        Sub Add(ByVal message As String, ByVal severity As LogSeverity)
        ''' <summary>Adds a message with default severity</summary>
        ''' <param name="message">Message text</param>
        ''' <remarks>The message is added with severity <see cref="LogSeverity.Info"/>.</remarks>
        Sub Add(ByVal message$)
        ''' <summary>Adds a formatted message with given severity</summary>
        ''' <param name="message">A composite format string.</param>
        ''' <param name="args">An object array that contains zero or more objects to format.</param>
        ''' <param name="severity">Severity level</param>
        ''' <exception cref="ArgumentNullException"><paramref name="message"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="message"/> is invalid composite format string.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array.</exception>
        ''' <seelaso cref="System.String.Format"/>
        Sub Add(ByVal severity As LogSeverity, ByVal message As String, ParamArray args As Object())
        ''' <summary>Adds a formatted message with default severity</summary>
        ''' <param name="message">A composite format string.</param>
        ''' <param name="args">An object array that contains zero or more objects to format.</param>
        ''' <remarks>The message is added with severity <see cref="LogSeverity.Info"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="message"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="message"/> is invalid composite format string.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array.</exception>
        ''' <seelaso cref="System.String.Format"/>
        Sub Add(ByVal message As String, ParamArray args As Object())
    End Interface

    ''' <summary>An abstract base class providing default implementation of <see cref="ILogger"/> inerface</summary>
    Public MustInherit Class LoggerBase
        Implements ILogger

        ''' <summary>Adds a message with default severity</summary>
        ''' <param name="message">Message text</param>
        ''' <remarks>The message is added with severity <see cref="LogSeverity.Info"/>.</remarks>
        Public Sub Add(message As String) Implements ILogger.Add
            Add(message, LogSeverity.Info)
        End Sub

        ''' <summary>Adds a formatted message with default severity</summary>
        ''' <param name="message">A composite format string.</param>
        ''' <param name="args">An object array that contains zero or more objects to format.</param>
        ''' <remarks>The message is added with severity <see cref="LogSeverity.Info"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="message"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="message"/> is invalid composite format string.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array.</exception>
        ''' <seelaso cref="System.String.Format"/>
        Public Sub Add(message As String, ParamArray args() As Object) Implements ILogger.Add
            Add(String.Format(message, args))
        End Sub

        ''' <summary>When overriden in derived class adds a message with given severity</summary>
        ''' <param name="message">Message text</param>
        ''' <param name="severity">Severity level</param>
        Public MustOverride Sub Add(message As String, severity As LogSeverity) Implements ILogger.Add

        ''' <summary>Adds a formatted message with given severity</summary>
        ''' <param name="message">A composite format string.</param>
        ''' <param name="args">An object array that contains zero or more objects to format.</param>
        ''' <param name="severity">Severity level</param>
        ''' <exception cref="ArgumentNullException"><paramref name="message"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="message"/> is invalid composite format string.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array.</exception>
        ''' <seelaso cref="System.String.Format"/>
        Public Sub Add(severity As LogSeverity, message As String, ParamArray args() As Object) Implements ILogger.Add
            Add(String.Format(message, args), severity)
        End Sub
    End Class

    ''' <summary>Logging severily levels</summary>
    Public Enum LogSeverity
        ''' <summary>Information</summary>
        Info = 0
        ''' <summary>Warning</summary>
        Warning = 1
        ''' <summary>Error</summary>
        [Error] = 2
    End Enum
End Namespace