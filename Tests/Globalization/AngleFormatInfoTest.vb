Imports System.Resources
Imports System.Globalization.CultureInfo
Imports Tools.GlobalizationT
Imports System.Text

Namespace GlobalizationT
    Public Module AngleFormatInfoTest
        Public Sub Test()
            Dim b As New StringBuilder
            AngleFormatInfoResources(b)
            AngleFormatInfoLocalizedValues(b)
            MsgBox(b.ToString)
        End Sub

        Public Sub AngleFormatInfoResources(b As StringBuilder)
            Dim rm As New ResourceManager("Tools.GlobalizationT.AngleFormatInfo", GetType(AngleFormatInfo).Assembly)
            Dim cs = GetCultureInfo("cs")
            Dim en = GetCultureInfo("en")
            Dim csCZ = GetCultureInfo("cs-CZ")
            Dim enUS = GetCultureInfo("en-US")

            b.AppendLine(String.Format("LatitudeNorthLongSymbol en={0}, cs={1}", rm.GetString("LatitudeNorthLongSymbol", en), rm.GetString("LatitudeNorthLongSymbol", cs)))
            b.AppendLine(String.Format("LatitudeNorthLongSymbol en-US={0}, cs-CZ={1}", rm.GetString("LatitudeNorthLongSymbol", enUS), rm.GetString("LatitudeNorthLongSymbol", csCZ)))

            b.AppendLine(String.Format("LongitudeEastShortSymbol en={0}, cs={1}", rm.GetString("LongitudeEastShortSymbol", en), rm.GetString("LongitudeEastShortSymbol", cs)))
            b.AppendLine(String.Format("LongitudeEastShortSymbol en-US={0}, cs-CZ={1}", rm.GetString("LongitudeEastShortSymbol", enUS), rm.GetString("LongitudeEastShortSymbol", csCZ)))

        End Sub

        Public Sub AngleFormatInfoLocalizedValues(b As StringBuilder)
            Dim ccs = GetCultureInfo("cs")
            Dim cen = GetCultureInfo("en")
            Dim ccsCZ = GetCultureInfo("cs-CZ")
            Dim cenUS = GetCultureInfo("en-US")
            Dim cs = AngleFormatInfo.Get(ccs)
            Dim csCZ = AngleFormatInfo.Get(ccsCZ)
            Dim en = AngleFormatInfo.Get(cen)
            Dim enUS = AngleFormatInfo.Get(cenUS)

            b.AppendLine(String.Format("LongitudeEastLongSymbol en={0}, cs={1}", en.LongitudeEastLongSymbol, cs.LongitudeEastLongSymbol))
            b.AppendLine(String.Format("LongitudeEastLongSymbol en-US={0}, cs-CZ={1}", enUS.LongitudeEastLongSymbol, csCZ.LongitudeEastLongSymbol))

            b.AppendLine(String.Format("LongitudeWestShortSymbol en={0}, cs={1}", en.LongitudeWestShortSymbol, cs.LongitudeWestShortSymbol))
            b.AppendLine(String.Format("LongitudeWestShortSymbol en-US={0}, cs-CZ={1}", enUS.LongitudeWestShortSymbol, csCZ.LongitudeWestShortSymbol))
        End Sub
    End Module

End Namespace