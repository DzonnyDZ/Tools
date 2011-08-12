Imports Tools.MetadataT.IptcT
Imports System.ComponentModel

''' <summary>Extends <see cref="Iptc"/> class with MetanolBrowser-specific functionality</summary>
Public Class IptcInternal
    Inherits Iptc
    Implements INotifyPropertyChanged

    ''' <summary>Default CTor - creates a new empty instance of the <see cref="IptcInternal"/> class</summary>
    Public Sub New()
        If Me.Tags.Count = 0 AndAlso My.Settings.IptcUtf8 Then
            Me.CodedCharacterSet = New Byte() {Tools.TextT.EncodingT.ISO2022.AsciiEscape, &H25, &H47}
        End If
        IsChanged = False
    End Sub
    ''' <summary>CTor - creates a new instance of the <see cref="IptcInternal"/> class and reads its data from given <see cref="IIptcGetter"/></summary>
    ''' <param name="getter">Source of IPTC data</param>
    Public Sub New(getter As IIptcGetter)
        MyBase.New(getter)
        If Me.Tags.Count = 0 AndAlso My.Settings.IptcUtf8 Then
            Me.CodedCharacterSet = New Byte() {Tools.TextT.EncodingT.ISO2022.AsciiEscape, &H25, &H47}
        End If
        IgnoreLenghtConstraints = My.Settings.IgnoreIptcLengthConstraints
        IsChanged = False
    End Sub

    ''' <summary>Called when value of any tag changes</summary>
    ''' <param name="Tag">Recod and dataset number</param>
    ''' <remarks>
    ''' <para>Called by <see cref="Tag"/>'s setter.</para>
    ''' <para>Note for inheritors: Call base class method in order to automatically compute size of embdeded file and invalidate cache for <see cref="BW460_Value"/></para>
    ''' </remarks>
    Protected Overrides Sub OnValueChanged(tag As MetadataT.IptcT.DataSetIdentification)
        MyBase.OnValueChanged(tag)
        Dim e As PropertyChangedEventArgs = New PropertyChangedEventArgs(tag.PropertyName)
        RaiseEvent ValueChanged(Me, e)
        RaiseEvent PropertyChanged(Me, e)
        IsChanged = True
    End Sub

    ''' <summary>Raised whan value of IPTC tag changes</summary>
    Public Event ValueChanged As PropertyChangedEventHandler
    ''' <summary>Occurs when a property value changes.</summary>
    Private Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _isChanged As Boolean
    ''' <summary>Gets or sets value indicating if this instance was changed since it was created or since this property was last set to false</summary>
    Public Property IsChanged As Boolean
        Get
            Return _isChanged
        End Get
        Set(value As Boolean)
            If IsChanged <> value Then
                _isChanged = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsChanged"))
            End If
        End Set
    End Property

End Class
