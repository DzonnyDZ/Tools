#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.MarkupT
    ''' <summary>A markup extension that provides array of enumeration members</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class EnumItemsExtension : Inherits MarkupExtensionBase

        ''' <summary>Returns an object that is set as the value of the target property for this markup extension - array of enum members of <see cref="[Enum]"/> enum in this case.</summary>
        ''' <param name="serviceProvider">Object that can provide services for the markup extension. This implementation never passes value null here.</param>
        ''' <returns>The object value to set on the property where the extension is applied - array of enum members of <see cref="[Enum]"/> enum in this case.</returns>
        ''' <exception cref="InvalidOperationException"><see cref="[Enum]"/> is null</exception>
        Protected Overrides Function ProvideValue(serviceProvider As XamlServiceProvider) As Object
            If [Enum] Is Nothing Then Throw New InvalidOperationException($"Property {NameOf([Enum])} must be set before calling {NameOf(ProvideValue)}.")
            Return System.Enum.GetValues([Enum])
        End Function

        ''' <summary>CTor - creates a new instance uninitialized of the <see cref="EnumItemsExtension"/> class</summary>
        Public Sub New()
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="EnumItemsExtension"/> class specifying enum type</summary>
        ''' <param name="enum">Enum type to provide members of</param>
        ''' <exception cref="ArgumentNullException"><paramref name="enum"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="enum"/> is not enumeration type</exception>
        Public Sub New([enum] As Type)
            If [enum] Is Nothing Then Throw New ArgumentNullException(NameOf([enum]))
            If Not [enum].IsEnum Then Throw New ArgumentException("Type must be enum", NameOf([enum]))
            Me.[Enum] = [enum]
        End Sub

        Private _enum As Type

        ''' <summary>Gets or sets enumerated type to provide members of</summary>
        ''' <exception cref="ArgumentNullException"><see cref="[Enum]"/> is not null and value being set is null</exception>
        ''' <exception cref="ArgumentException">Value being set is not enumerated type</exception>
        Public Property [Enum] As Type
            Get
                Return _enum
            End Get
            Set(value As Type)
                If value Is Nothing AndAlso _enum Is Nothing Then Return 'Exception. Allow setting to null when null
                If value Is Nothing Then Throw New ArgumentNullException(NameOf(value))
                If Not value.IsEnum Then Throw New ArgumentException("Type must be enum", NameOf(value))
                _enum = value
            End Set
        End Property


    End Class
End NameSpace
#end if