Imports System.ComponentModel, System.Configuration
#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModel
    'TODO:I'll bring this from my work ASAP (Ð)
    ''' <summary><see cref="DescriptionAttribute"/> that takes its value from <see cref="System.Configuration.SettingsDescriptionAttribute"/></summary>
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, lastchange:="1/21/2007")> _
    Public Class SettingsInheritDescriptionAttribute : Inherits DescriptionAttribute
    End Class
    ''' <summary><see cref="DefaultValueAttribute"/> that takes its value from <see cref="System.Configuration.DefaultSettingValueAttribute"/></summary>
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, lastchange:="1/21/2007")> _
    Public Class SettingsInheritDefaultValueAttribute : Inherits DefaultValueAttribute
        Public Sub New()
            MyBase.New("")
        End Sub
    End Class
End Namespace
#End If