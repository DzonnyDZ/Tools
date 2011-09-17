Imports System.Data.Common
Imports System.Windows
Imports Microsoft.VisualStudio.DebuggerVisualizers
Imports Tools.DiagnosticsT

<Assembly: DebuggerVisualizer(GetType(DbCommandVisualizer), GetType(DbCommandSource), Target:=GetType(IDbCommand), Description:="DB Command Visualizer")>  'It seems that using interface as Target is ognored by VS
<Assembly: DebuggerVisualizer(GetType(DbCommandVisualizer), GetType(DbCommandSource), Target:=GetType(dbcommand), Description:="DB Command Visualizer")> 


Namespace DiagnosticsT
    ''' <summary>Implements Debugger Visualizer for database commands - types implementing <see cref="IDbCommand"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class DbCommandVisualizer
        Inherits DialogDebuggerVisualizer

        ''' <summary>Show dialog displaying the object being visualized</summary>
        ''' <param name="windowService">An object of type <see cref="T:Microsoft.VisualStudio.DebuggerVisualizers.IDialogVisualizerService" />, which provides methods your visualizer can use to display Windows forms, controls, and dialogs.</param>
        ''' <param name="objectProvider">An object of type <see cref="T:Microsoft.VisualStudio.DebuggerVisualizers.IVisualizerObjectProvider" />. This object provides communication from the debugger side of the visualizer to the object source (<see cref="T:Microsoft.VisualStudio.DebuggerVisualizers.VisualizerObjectSource" />) on the debuggee side.</param>
        Protected Overrides Sub Show(windowService As IDialogVisualizerService, objectProvider As IVisualizerObjectProvider)
            Dim win As DbCommandVisualizerWindow
            Dim cmd As IDbCommand
            Try
                cmd = TryCast(objectProvider.GetObject, IDbCommand)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Exit Sub
            End Try
            If cmd Is Nothing Then Exit Sub
            win = New DbCommandVisualizerWindow(cmd)
            win.ShowDialog()
        End Sub
    End Class

    ''' <summary>Debugee side implementation for <see cref="DbCommandVisualizer"/>. This class serializes <see cref="IDbCommand"/>s.</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class DbCommandSource
        Inherits VisualizerObjectSource

        ''' <summary>Gets serialized data for given object</summary>
        ''' <param name="target">Object being visualized.</param>
        ''' <param name="outgoingData">Outgoing data stream.</param>
        ''' <remarks>If <paramref name="target"/> is null or it is serializable the object is serialized. Otherwise proxy instance is created and serialized.</remarks>
        Public Overrides Sub GetData(target As Object, outgoingData As System.IO.Stream)
            If target Is Nothing OrElse Not TypeOf target Is IDbCommand Then
                MyBase.GetData(target, outgoingData)
            Else
                MyBase.GetData(DbCommandVisualizerProxy.EnsureSerializable(target), outgoingData)
            End If
        End Sub
    End Class

    ''' <summary>Implements dialog which visualizes <see cref="IDbCommand"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Partial Friend Class DbCommandVisualizerWindow
        Inherits Window
        Private cmd As IDbCommand
        ''' <summary>CTor - creates a new instance of the <see cref="DbCommandVisualizerWindow"/> class</summary>
        ''' <param name="command">The command to visualize</param>
        Public Sub New(command As IDbCommand)
            InitializeComponent()
            cmd = command
            DataContext = Me
        End Sub

        ''' <summary>Provides command text for current command</summary>
        Public ReadOnly Property CommandText$
            Get
                If cmd Is Nothing Then Return Nothing
                Return cmd.CommandText
            End Get
        End Property

        ''' <summary>Provides command type for current command</summary>
        Public ReadOnly Property CommandType As CommandType
            Get
                If cmd Is Nothing Then Return Nothing
                Return cmd.CommandType
            End Get
        End Property

        ''' <summary>Provides command data type full name for current command</summary>
        Public ReadOnly Property CommandTypeName$
            Get
                If cmd Is Nothing Then Return Nothing
                If TypeOf cmd Is DbCommandVisualizerProxy Then Return DirectCast(cmd, DbCommandVisualizerProxy).OriginalTypeName
                Return cmd.GetType.FullName
            End Get
        End Property

        ''' <summary>Provides parameters for current command</summary>
        ''' <returns>Enumerates anonymous type</returns>
        Public ReadOnly Property Parameters As IEnumerable
            Get
                If cmd Is Nothing Then Return Nothing
                Return From par As IDbDataParameter In cmd.Parameters Select
                       par.DbType, par.Direction, par.IsNullable, par.ParameterName, par.Precision, par.Scale, par.Size, par.SourceColumn, par.SourceVersion, par.Value,
                       DataType = If(TypeOf par Is DbDataParameterVisualizerProxy,
                          DirectCast(par, DbDataParameterVisualizerProxy).OriginalTypeName,
                          par.GetType.FullName
                       )
            End Get
        End Property

    End Class

End Namespace