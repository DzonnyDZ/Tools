Imports System.Xml.Linq
Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage:Nightly
Namespace XmlT.LinqT
    ''' <summary>Provides extension methods related to <see cref="System.Xml.Linq"/></summary>
    Module Extensions
        ''' <summary>Retruns value indicating if two <see cref="XElement">XElements</see> has same name (this means <see cref="XName.LocalName"/> are same as well as <see cref="XName.NamespaceName"/>.</summary>
        ''' <param name="el">A <see cref="XElement"/> to test name of</param>
        ''' <param name="other">A <see cref="XElement"/> to compare name with</param>
        ''' <returns>Ture if <paramref name="el"/> and <paramref name="other"/> have same <see cref="XElement.Name"/></returns>
        ''' <seealso cref="XName.op_Equality"/>
        <Extension()> Public Function HasSameName(ByVal el As XElement, ByVal other As XElement) As Boolean
            Return el.Name = other.Name
        End Function
    End Module
End Namespace
#End If