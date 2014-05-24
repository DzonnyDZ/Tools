Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class CopyDialog

End Class

Public Class CopyConfig
    Implements INotifyPropertyChanged

    Private _ask As Boolean
    Public Property Ask As Boolean
        Get
            Return _ask
        End Get
        Set(value As Boolean)
            If value <> Ask Then
                _ask = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Private _operation As CopyOperation
    Public Property Operation As CopyOperation
        Get
            Return _operation
        End Get
        Set(value As CopyOperation)
            If value <> Operation Then
                Dim old = Operation
                _operation = value
                OnPropertyChanged()
                If (old = CopyOperation.Copy) <> (value = CopyOperation.Copy) Then OnPropertyChanged("CopySelected")
                If (old = CopyOperation.Move) <> (value = CopyOperation.Move) Then OnPropertyChanged("MoveSelected")
                If (old = CopyOperation.Link) <> (value = CopyOperation.Link) Then OnPropertyChanged("LinkSelected")
                If (old = CopyOperation.SymLink) <> (value = CopyOperation.SymLink) Then OnPropertyChanged("SymLinkSelected")
                If (old = CopyOperation.HardLink) <> (value = CopyOperation.HardLink) Then OnPropertyChanged("HardLinkSelected")
            End If
        End Set
    End Property

    Public Property CopySelected As Boolean
        Get

        End Get
    End Property

    Public Property MoveSelected As Boolean
        Get

        End Get
    End Property

    Public Property LinkSelected As Boolean
        Get

        End Get
    End Property

    Public Property SymLinkSelected As Boolean
        Get

        End Get
    End Property

    Public Property HardLinkSelected As Boolean
        Get

        End Get
    End Property

    Private _files() As copyfileinfo
    Public ReadOnly Property Files As CopyFileInfo()
        Get
            Return _files
        End Get
    End Property

    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub

    Protected Overridable Sub OnPropertyChanged(<CallerMemberName> Optional propertyName$ = Nothing)
        If propertyName Is Nothing Then Throw New ArgumentNullException("propertyName")
        OnPropertyChanged(New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


End Class

Public Class CopyFileInfo
    Implements INotifyPropertyChanged

    Private _selected As Boolean
    Public Property Selected As Boolean
        Get
            Return _selected
        End Get
        Set(value As Boolean)
            If value <> Selected Then
                _selected = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Private _path$
    Public Property Path$
        Get
            Return _path
        End Get
        Set(value$)
            If value <> Path Then
                _path = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub

    Protected Overridable Sub OnPropertyChanged(<CallerMemberName> Optional propertyName$ = Nothing)
        If propertyName Is Nothing Then Throw New ArgumentNullException("propertyName")
        OnPropertyChanged(New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class

Public Enum CopyOperation
    Copy
    Move
    Link
    SymLink
    HardLink
End Enum
