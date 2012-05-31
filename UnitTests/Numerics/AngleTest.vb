Imports Tools, System.Globalization.CultureInfo
Imports Tools.ExtensionsT
Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.NumericsT
Imports System.Numerics
Imports System.Resources
Imports Tools.GlobalizationT

Namespace NumericsUT

    '''<summary>
    '''This is a test class for AngleTest and is intended
    '''to contain all AngleTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class AngleTest
        '''<summary>
        '''Gets or sets the test context which provides
        '''information about and functionality for the current test run.
        '''</summary>
        Public Property TestContext() As TestContext

        Private Const delta = 0.00000000000014999999999999999#

        <TestMethod>
        Public Sub ToDms()
            Dim d As UInteger, m As Byte, s As Decimal, sg As SByte
            Angle.ToDms(10@, d, m, s, sg)
            Assert.AreEqual(10UI, d)
            Assert.AreEqual(CByte(0), m)
            Assert.AreEqual(0@, s)
            Assert.AreEqual(CSByte(1), sg)

            For Each v In {
                New With {.d = 0UI, .m = CByte(14), .s = 0@, .sg = CSByte(1)},
                New With {.d = 0UI, .m = CByte(0), .s = 0@, .sg = CSByte(0)},
                New With {.d = 20UI, .m = CByte(0), .s = 0@, .sg = CSByte(-1)},
                New With {.d = 20UI, .m = CByte(0), .s = 3@, .sg = CSByte(1)},
                New With {.d = 0UI, .m = CByte(0), .s = 1@, .sg = CSByte(-1)}
            }
                Dim valueD = v.d + (v.m + v.s / 60@) / 60@ * v.sg
                Angle.ToDms(valueD, d, m, s, sg)

                Assert.AreEqual(valueD, (d + (m + s / 60@) / 60@) * sg, delta, String.Format("v.d = {0}, v.m = {1}, v.s = {2}, v.sg = {3}", v.d, v.m, v.s, v.sg))

                Dim valueF = v.d + (v.m + v.s / 60.0#) / 60.0# * v.sg
                Angle.ToDms(valueF, d, m, s, sg)

                Assert.AreEqual(valueF, (d + (m + s / 60.0#) / 60.0#) * sg, delta, String.Format("v.d = {0}, v.m = {1}, v.s = {2}, v.sg = {3}", v.d, v.m, v.s, v.sg))
            Next
        End Sub

        <TestMethod>
        Public Sub NormalizeDms()
            Dim i% = 0
            For Each v In {
                New With {.d1 = 0I, .m1 = 0I, .s1 = 0@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(0)},
 _
                New With {.d1 = 1I, .m1 = 0I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = 1I, .s1 = 0@, .d2 = 0UI, .m2 = CByte(1), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = 0I, .s1 = 1@, .d2 = 0UI, .m2 = CByte(0), .s2 = 1@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = 0I, .s1 = 0.1@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0.1@, .sg2 = CSByte(1)},
                New With {.d1 = 1I, .m1 = 0I, .s1 = 1@, .d2 = 1UI, .m2 = CByte(0), .s2 = 1@, .sg2 = CSByte(1)},
                New With {.d1 = 1I, .m1 = 1I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(1), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = 1I, .m1 = 1I, .s1 = 1@, .d2 = 1UI, .m2 = CByte(1), .s2 = 1@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = 1I, .s1 = 1@, .d2 = 0UI, .m2 = CByte(1), .s2 = 1@, .sg2 = CSByte(1)},
 _
                New With {.d1 = -1I, .m1 = 0I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = -1I, .s1 = 0@, .d2 = 0UI, .m2 = CByte(1), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = 0I, .s1 = -1@, .d2 = 0UI, .m2 = CByte(0), .s2 = 1@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = 0I, .s1 = -0.1@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0.1@, .sg2 = CSByte(-1)},
                New With {.d1 = -1I, .m1 = 0I, .s1 = 1@, .d2 = 1UI, .m2 = CByte(0), .s2 = 1@, .sg2 = CSByte(-1)},
                New With {.d1 = -1I, .m1 = 1I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(1), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = -1I, .m1 = 1I, .s1 = 1@, .d2 = 1UI, .m2 = CByte(1), .s2 = 1@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = -1I, .s1 = 1@, .d2 = 0UI, .m2 = CByte(1), .s2 = 1@, .sg2 = CSByte(-1)},
 _
                New With {.d1 = 0I, .m1 = 0I, .s1 = 62@, .d2 = 0UI, .m2 = CByte(1), .s2 = 2@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = 0I, .s1 = -62@, .d2 = 0UI, .m2 = CByte(1), .s2 = 2@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = 62I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(2), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = -62I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(2), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 1I, .m1 = 62I, .s1 = 0@, .d2 = 2UI, .m2 = CByte(2), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = -1I, .m1 = 62I, .s1 = 0@, .d2 = 2UI, .m2 = CByte(2), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 1I, .m1 = 30I, .s1 = 128@, .d2 = 1UI, .m2 = CByte(32), .s2 = 8@, .sg2 = CSByte(1)},
                New With {.d1 = -1I, .m1 = 30I, .s1 = 128@, .d2 = 1UI, .m2 = CByte(32), .s2 = 8@, .sg2 = CSByte(-1)},
 _
                New With {.d1 = 5I, .m1 = -1I, .s1 = 0@, .d2 = 4UI, .m2 = CByte(59), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = 5I, .s1 = -30@, .d2 = 0UI, .m2 = CByte(4), .s2 = 30@, .sg2 = CSByte(1)},
                New With {.d1 = 5I, .m1 = -125I, .s1 = 0@, .d2 = 2UI, .m2 = CByte(55), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = 5I, .s1 = -125.1@, .d2 = 0UI, .m2 = CByte(2), .s2 = 54.9@, .sg2 = CSByte(1)},
 _
                New With {.d1 = -5I, .m1 = -1I, .s1 = 0@, .d2 = 4UI, .m2 = CByte(59), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = -5I, .s1 = -30@, .d2 = 0UI, .m2 = CByte(4), .s2 = 30@, .sg2 = CSByte(-1)},
                New With {.d1 = -5I, .m1 = -125I, .s1 = 0@, .d2 = 2UI, .m2 = CByte(55), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = -5I, .s1 = -125.1@, .d2 = 0UI, .m2 = CByte(2), .s2 = 54.9@, .sg2 = CSByte(-1)},
 _
                New With {.d1 = 1I, .m1 = -60I, .s1 = 0@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(0)},
                New With {.d1 = 1I, .m1 = -70I, .s1 = 0@, .d2 = 0UI, .m2 = CByte(10), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 1I, .m1 = -120I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 1I, .m1 = -130I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(10), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = 1I, .s1 = -60@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(0)},
                New With {.d1 = 0I, .m1 = 1I, .s1 = -70@, .d2 = 0UI, .m2 = CByte(0), .s2 = 10@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = 1I, .s1 = -120@, .d2 = 0UI, .m2 = CByte(1), .s2 = 0@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = 1I, .s1 = -130@, .d2 = 0UI, .m2 = CByte(1), .s2 = 10@, .sg2 = CSByte(-1)},
                New With {.d1 = 0I, .m1 = 1I, .s1 = -60.1@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0.1@, .sg2 = CSByte(-1)},
 _
                New With {.d1 = -1I, .m1 = -60I, .s1 = 0@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(0)},
                New With {.d1 = -1I, .m1 = -70I, .s1 = 0@, .d2 = 0UI, .m2 = CByte(10), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = -1I, .m1 = -120I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = -1I, .m1 = -130I, .s1 = 0@, .d2 = 1UI, .m2 = CByte(10), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = -1I, .s1 = -60@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0@, .sg2 = CSByte(0)},
                New With {.d1 = 0I, .m1 = -1I, .s1 = -70@, .d2 = 0UI, .m2 = CByte(0), .s2 = 10@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = -1I, .s1 = -120@, .d2 = 0UI, .m2 = CByte(1), .s2 = 0@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = -1I, .s1 = -130@, .d2 = 0UI, .m2 = CByte(1), .s2 = 10@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = -1I, .s1 = -60.1@, .d2 = 0UI, .m2 = CByte(0), .s2 = 0.1@, .sg2 = CSByte(1)},
 _
                New With {.d1 = 0I, .m1 = 0I, .s1 = 10933@, .d2 = 3UI, .m2 = CByte(2), .s2 = 13@, .sg2 = CSByte(1)},
                New With {.d1 = 10I, .m1 = 33I, .s1 = 10933@, .d2 = 13UI, .m2 = CByte(35), .s2 = 13@, .sg2 = CSByte(1)},
                New With {.d1 = 0I, .m1 = 0I, .s1 = -10933@, .d2 = 3UI, .m2 = CByte(2), .s2 = 13@, .sg2 = CSByte(-1)},
                New With {.d1 = -10I, .m1 = 33I, .s1 = 10933@, .d2 = 13UI, .m2 = CByte(35), .s2 = 13@, .sg2 = CSByte(-1)},
 _
                New With {.d1 = 10I, .m1 = 0I, .s1 = -4001.3@, .d2 = 8UI, .m2 = CByte(53), .s2 = 18.7@, .sg2 = CSByte(1)},
                New With {.d1 = -10I, .m1 = 0I, .s1 = -4001.3@, .d2 = 8UI, .m2 = CByte(53), .s2 = 18.7@, .sg2 = CSByte(-1)},
                New With {.d1 = 1I, .m1 = 0I, .s1 = -4001.3@, .d2 = 0UI, .m2 = CByte(6), .s2 = 41.3@, .sg2 = CSByte(-1)},
                New With {.d1 = -1I, .m1 = 0I, .s1 = -4001.3@, .d2 = 0UI, .m2 = CByte(6), .s2 = 41.3@, .sg2 = CSByte(1)},
 _
                New With {.d1 = 1I, .m1 = 1I, .s1 = 10003.3@, .d2 = 3UI, .m2 = CByte(47), .s2 = 43.3@, .sg2 = CSByte(1)},
                New With {.d1 = 1I, .m1 = 32I, .s1 = 10003.3@, .d2 = 4UI, .m2 = CByte(18), .s2 = 43.3@, .sg2 = CSByte(1)},
                New With {.d1 = -1I, .m1 = 1I, .s1 = 10003.3@, .d2 = 3UI, .m2 = CByte(47), .s2 = 43.3@, .sg2 = CSByte(-1)},
                New With {.d1 = -1I, .m1 = 32I, .s1 = 10003.3@, .d2 = 4UI, .m2 = CByte(18), .s2 = 43.3@, .sg2 = CSByte(-1)}
            }
                Dim d2 As UInteger, m2 As Byte, s2 As Decimal, sg2 As SByte
                Angle.NormalizeDms(v.d1, v.m1, v.s1, d2, m2, s2, sg2)
                Dim msg = String.Format("{0}: {1}°{2}′{3}″ → {4:+}{5}°{6}′{7}″✓ {8:+}{9}°{10}′{11}″?", i + 1, v.d1, v.m1, v.s1, v.sg2, v.d2, v.m2, v.s2, sg2, d2, m2, s2)
                Assert.IsTrue(m2 < CByte(60), msg)
                Assert.IsTrue(s2 >= 0@, msg)
                Assert.IsTrue(s2 < 60@, msg)
                Assert.IsTrue(sg2 = CSByte(-1) OrElse sg2 = CSByte(0) OrElse sg2 = CSByte(1), msg)
                Assert.AreEqual(v.d2, d2, msg)
                Assert.AreEqual(v.m2, m2, msg)
                Assert.AreEqual(v.s2, s2, msg)
                Assert.AreEqual(v.sg2, sg2, msg)
                i += 1
            Next
        End Sub

        <TestMethod()>
        Public Sub BasicProperties()
            Dim values = {0@, 1@, -1@, 180@, 360@, 90@, -90@, 3600@, 14.134971345645519@}
            For Each v In values
                Dim a As New Angle(v)
                Assert.AreEqual(CDbl(v), a.TotalDegrees, String.Format("v={0}", v))
                Assert.AreEqual(Math.Abs(CInt(Math.Floor(v))), a.Degrees, String.Format("v={0}", v))
                Assert.AreEqual(v * 60.0#, a.TotalMinutes, delta, String.Format("v={0}", v))
                Assert.AreEqual(CInt(Math.Floor(v * 60@ - Math.Floor(v) * 60@)), a.Minutes, String.Format("v={0}", v))
                Assert.AreEqual(v * 60.0# * 60.0#, a.TotalSeconds, String.Format("v={0}", v))
                Assert.AreEqual(v * 60@ * 60@ - Math.Floor(v * 60) * 60@, a.Seconds, delta, String.Format("v={0}", v))
                Assert.AreEqual(v / 360.0#, a.Rotations, String.Format("v={0}", v))
            Next
        End Sub

        <TestMethod()>
        Public Sub ConversionTo()
            Assert.AreEqual(1.0#, New Angle(1).ToDegrees)
            Assert.AreEqual(100.0#, New Angle(90).ToGradians)
            Assert.AreEqual(-400.0#, New Angle(-360).ToGradians)
            Assert.AreEqual(2.0# * Math.PI, New Angle(360).ToRadians)
            Assert.AreEqual(100.0#, New Angle(45).ToSlope, delta)
            Assert.AreEqual(-100.0#, New Angle(-45).ToSlope, delta)
            Assert.AreEqual(0.0#, New Angle(0).ToSlope)

            Dim values = {0@, 1@, -1@, 180@, 360@, 90@, -90@, 3600@, 14.134971345645519@}
            For Each v In values
                Dim a As New Angle(v)
                Assert.AreEqual(CDbl(v), a.ToDegrees, String.Format("v={0}", v))
                Assert.AreEqual(v * (Math.PI / 180.0#), a.ToRadians, String.Format("v={0}", v))
                Assert.AreEqual(v / 90.0# * 100.0#, a.ToGradians, String.Format("v={0}", v))
                Assert.AreEqual(v / 360.0#, a.ToRotations, String.Format("v={0}", v))
            Next
        End Sub

        <TestMethod()>
        Public Sub ConversionFrom()
            Assert.AreEqual(1.0#, Angle.FromDegrees(1).TotalDegrees)
            Assert.AreEqual(180.0#, Angle.FromDegrees(180).TotalDegrees)
            Assert.AreEqual(360.0#, Angle.FromRotations(1).TotalDegrees)
            Assert.AreEqual(180.0#, Angle.FromRotations(0.5).TotalDegrees)
            Assert.AreEqual(-360.0#, Angle.FromRotations(-1).TotalDegrees)
            Assert.AreEqual(360.0#, Angle.FromRadians(2 * Math.PI).TotalDegrees)
            Assert.AreEqual(90.0#, Angle.FromGradians(100).TotalDegrees)
            Assert.AreEqual(360.0#, Angle.FromGradians(400).TotalDegrees)
            Assert.AreEqual(1.0# / 60.0#, Angle.FromMinutes(1).TotalDegrees, delta)
            Assert.AreEqual(1.0#, Angle.FromMinutes(60).TotalDegrees)
            Assert.AreEqual(1.0# / 3600@, Angle.FromSeconds(1).TotalDegrees)
            Assert.AreEqual(1.0#, Angle.FromSeconds(3600).TotalDegrees)
            Assert.AreEqual(-1.0#, Angle.FromSeconds(-3600).TotalDegrees)
            Assert.AreEqual(New Angle(45).TotalDegrees, Angle.FromSlope(100).TotalDegrees)
            Assert.AreEqual(New Angle(-45).TotalDegrees, Angle.FromSlope(-100).TotalDegrees)
            Assert.AreEqual(New Angle(0).TotalDegrees, Angle.FromSlope(0).TotalDegrees)

            Dim values = {0@, 1@, -1@, 180@, 360@, 90@, -90@, 3600@, 14.134971345645519@}
            For Each v In values
                Assert.AreEqual(CDbl(v), Angle.FromDegrees(v).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.AreEqual(CDbl(v), Angle.FromRadians(v * (Math.PI / 180.0#)).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.AreEqual(CDbl(v), Angle.FromGradians(v / 90.0# * 100.0#).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.AreEqual(CDbl(v), Angle.FromRotations(v / 360.0#).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.AreEqual(CDbl(v), Angle.FromMinutes(v * 60).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.AreEqual(CDbl(v), Angle.FromSeconds(v * 60 * 60).TotalDegrees, delta, String.Format("v={0}", v))
            Next
        End Sub

        <TestMethod()>
        Public Sub Values()
            Assert.AreEqual(0.0#, Angle.Zero.TotalDegrees)
            Assert.AreEqual(90.0#, Angle.Right.TotalDegrees)
            Assert.AreEqual(180.0#, Angle.Straing.TotalDegrees)
            Assert.AreEqual(360.0#, Angle.Full.TotalDegrees)
            'Assert.AreEqual(Decimal.MinValue, Angle.MinValue.TotalDegrees)
            'Assert.AreEqual(Decimal.MaxValue, Angle.MaxValue.TotalDegrees)
            Assert.AreEqual(1.0#, Angle.Degree.TotalDegrees)
            Assert.AreEqual(180.0# / Math.PI, Angle.Radian.TotalDegrees)
            Assert.AreEqual(9.0# / 10.0#, Angle.Gradian.TotalDegrees)
            Assert.AreEqual(180.0#, Angle.ΠRadians.TotalDegrees)
            Assert.AreEqual(1.0#, Angle.Minute.TotalMinutes)
            Assert.AreEqual(1.0#, Angle.Second.TotalSeconds)
        End Sub

        <TestMethod()>
        Public Sub CTor_Single()
            Dim values() As Single = {0.0!, 1.0!, -1.0!, 180.0!, 360.0!, 90.0!, -90.0!, 3600.0!, 14.1349716!}
            For Each v In values
                Assert.AreEqual(CDbl(v), New Angle(v).TotalDegrees)
            Next
        End Sub

        <TestMethod()>
        Public Sub CTor_Double()
            Dim values() As Double = {0.0#, 1.0#, -1.0#, 180.0#, 360.0#, 90.0#, -90.0#, 3600.0#, 14.134971345645519#}
            For Each v In values
                Assert.AreEqual(CDbl(v), New Angle(v).TotalDegrees, String.Format("v={0}", v))
            Next
        End Sub

        <TestMethod()>
        Public Sub CTor_Decimal()
            Dim values() As Decimal = {0@, 1@, -1@, 180@, 360@, 90@, -90@, 3600@, 14.134971345645519@}
            For Each v In values
                Assert.AreEqual(CDbl(v), New Angle(v).TotalDegrees, String.Format("v={0}", v))
            Next
        End Sub

        <TestMethod()>
        Public Sub CTor_Int()
            Dim values() As Integer = {0%, 1%, -1%, 180%, 360%, 90%, -90%, 3600%}
            For Each v In values
                Assert.AreEqual(CDbl(v), New Angle(v).TotalDegrees, String.Format("v={0}", v))
            Next
        End Sub

        <TestMethod()>
        Public Sub CTor_dms_simple_single()
            Dim a = New Angle(0, 0, 0.0#)
            Assert.IsTrue(a = 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(10, 10, 10.1#)
            Assert.IsTrue(a > 0)
            Assert.AreEqual(10, a.Degrees)
            Assert.AreEqual(10, a.Minutes)
            Assert.AreEqual(10.1@, a.Seconds)

            a = New Angle(-1, 0, 0.0#)
            Assert.IsTrue(a < 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, -1, 0.0#)
            Assert.IsTrue(a < 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(1, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 0, -1.0#)
            Assert.IsTrue(a < 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(1@, a.Seconds)

            Dim values() = {
                New With {.d = 0, .m = 0, .s = 0.0#},
                New With {.d = 0, .m = 0, .s = 0.10000000000000001#},
                New With {.d = 0, .m = 0, .s = 1.0#},
                New With {.d = 0, .m = 1, .s = 0.0#},
                New With {.d = 0, .m = 59, .s = 0.0#},
                New With {.d = 0, .m = 0, .s = 59.0#},
                New With {.d = 0, .m = 0, .s = 59.990000000000002#},
                New With {.d = 1, .m = 0, .s = 0.0#},
                New With {.d = 360, .m = 0, .s = 0.0#},
                New With {.d = 1, .m = 59, .s = 59.990000000000002#},
                New With {.d = 0, .m = 0, .s = -0.01#},
                New With {.d = 0, .m = 0, .s = -1.0#},
                New With {.d = 0, .m = -1, .s = 0.0#},
                New With {.d = -1, .m = 0, .s = 0.0#},
                New With {.d = -1, .m = 1, .s = 0.0#},
                New With {.d = -1, .m = 0, .s = 0.10000000000000001#},
                New With {.d = -1, .m = 1, .s = 1.0#}
            }
            For Each v In values
                a = New Angle(v.d, v.m, v.s)
                Assert.AreEqual(v.d < 0 OrElse v.m < 0 OrElse v.s < 0.0#, a < 0, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
                Assert.AreEqual(Math.Abs(v.d), a.Degrees, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
                Assert.AreEqual(Math.Abs(v.m), a.Minutes, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
                Assert.AreEqual(Math.Abs(CDec(v.s)), a.Seconds, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
                Assert.AreEqual(v.d + If(v.d < 0, -1, 1) * v.m / 60.0# + If(v.d < 0 OrElse (v.d = 0 AndAlso v.m < 0), -1, 1) * v.s / 60.0# / 60.0#, a.TotalDegrees, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
            Next
        End Sub

        <TestMethod()>
        Public Sub CTor_dms_simple_decimal()
            Dim a = New Angle(0, 0, 0@)
            Assert.IsTrue(a = 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(10, 10, 10.1@)
            Assert.IsTrue(a > 0)
            Assert.AreEqual(10, a.Degrees)
            Assert.AreEqual(10, a.Minutes)
            Assert.AreEqual(10.1@, a.Seconds)

            a = New Angle(-1, 0, 0@)
            Assert.IsTrue(a < 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, -1, 0@)
            Assert.IsTrue(a < 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(1, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 0, -1@)
            Assert.IsTrue(a < 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(1@, a.Seconds)

            Dim values() = {
                New With {.d = 0, .m = 0, .s = 0@},
                New With {.d = 0, .m = 0, .s = 0.10000000000000001@},
                New With {.d = 0, .m = 0, .s = 1@},
                New With {.d = 0, .m = 1, .s = 0@},
                New With {.d = 0, .m = 59, .s = 0@},
                New With {.d = 0, .m = 0, .s = 59@},
                New With {.d = 0, .m = 0, .s = 59.990000000000002@},
                New With {.d = 1, .m = 0, .s = 0@},
                New With {.d = 360, .m = 0, .s = 0@},
                New With {.d = 1, .m = 59, .s = 59.990000000000002@},
                New With {.d = 0, .m = 0, .s = -0.01@},
                New With {.d = 0, .m = 0, .s = -1@},
                New With {.d = 0, .m = -1, .s = 0@},
                New With {.d = -1, .m = 0, .s = 0@},
                New With {.d = -1, .m = 1, .s = 0@},
                New With {.d = -1, .m = 0, .s = 0.10000000000000001@},
                New With {.d = -1, .m = 1, .s = 1@}
            }
            For Each v In values
                a = New Angle(v.d, v.m, v.s)
                Assert.AreEqual(v.d < 0 OrElse v.m < 0 OrElse v.s < 0@, a < 0, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
                Assert.AreEqual(Math.Abs(v.d), a.Degrees, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
                Assert.AreEqual(Math.Abs(v.m), a.Minutes, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
                Assert.AreEqual(Math.Abs(v.s), a.Seconds, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
                Assert.AreEqual(v.d + If(v.d < 0, -1, 1) * v.m / 60.0# + If(v.d < 0 OrElse (v.d = 0 AndAlso v.m < 0), -1, 1) * v.s / 60.0# / 60.0#, a.TotalDegrees, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
            Next
        End Sub

        <TestMethod()>
        Public Sub CTor_dms_ex_double()

            Dim a As New Angle(0, 60, 0.0#)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 0, 60.0#)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(1, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 1, 60.0#)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(2, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(1, 61, 1.0#)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(2, a.Degrees)
            Assert.AreEqual(1, a.Minutes)
            Assert.AreEqual(1@, a.Seconds, delta)

            a = New Angle(1, -1, 0.0#)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(59, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 1, -1.0#)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(59@, a.Seconds)

            a = New Angle(1, -60, 0.0#)
            Assert.IsTrue(a.TotalDegrees = 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 1, -60.0#)
            Assert.IsTrue(a.TotalDegrees = 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(-1, 0, 0.0#)
            Assert.IsTrue(a.TotalDegrees < 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(-1, -1, 60.0#)
            Assert.IsTrue(a.TotalDegrees < 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(1, 0, 60.0# * 60.0#)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(2, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(1, -120, 0.0#)
            Assert.IsTrue(a.TotalDegrees < 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(-1, -120, 60.0#)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(59, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            Dim values() = {
               New With {.d = 0, .m = 1, .s = 66.0#},
               New With {.d = 1, .m = 66, .s = 0.10000000000000001#},
               New With {.d = 0, .m = 66, .s = 66.099999999999994#},
               New With {.d = 1, .m = -1, .s = 0.0#},
               New With {.d = 60, .m = -30, .s = -30.0# * 60.0#},
               New With {.d = -360, .m = -10, .s = 0.0#},
               New With {.d = 0, .m = -30, .s = -8.0#},
               New With {.d = -33, .m = 0, .s = -1.0#},
               New With {.d = 33, .m = 0, .s = -1.0#},
               New With {.d = 0, .m = 0, .s = 3600.0#},
               New With {.d = 0, .m = 3600, .s = 0.0#},
               New With {.d = 0, .m = -3600, .s = -3600 * 60.0#}
           }
            For Each v In values
                a = New Angle(v.d, v.m, v.s)
                Dim expeted = v.d + If(v.d < 0, -1, 1) * v.m / 60.0# + If(v.d < 0 OrElse (v.d = 0 AndAlso v.m < 0), -1, 1) * v.s / 60.0# / 60.0#
                Assert.AreEqual(expeted, a.TotalDegrees, delta, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
            Next
        End Sub

        <TestMethod()>
        Public Sub CTor_dms_ex_decimal()

            Dim a As New Angle(0, 60, 0@)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 0, 60@)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(1, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 1, 60@)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(2, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(1, 61, 1@)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(2, a.Degrees)
            Assert.AreEqual(1, a.Minutes)
            Assert.AreEqual(1@, a.Seconds, delta)

            a = New Angle(1, -1, 0@)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(59, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 1, -1@)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(59@, a.Seconds)

            a = New Angle(1, -60, 0@)
            Assert.IsTrue(a.TotalDegrees = 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(0, 1, -60@)
            Assert.IsTrue(a.TotalDegrees = 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(-1, 0, 0@)
            Assert.IsTrue(a.TotalDegrees < 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(-1, -1, 60@)
            Assert.IsTrue(a.TotalDegrees < 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(1, 0, 60@ * 60@)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(2, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(1, -120, 0@)
            Assert.IsTrue(a.TotalDegrees < 0)
            Assert.AreEqual(1, a.Degrees)
            Assert.AreEqual(0, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            a = New Angle(-1, -120, 60@)
            Assert.IsTrue(a.TotalDegrees > 0)
            Assert.AreEqual(0, a.Degrees)
            Assert.AreEqual(59, a.Minutes)
            Assert.AreEqual(0@, a.Seconds)

            Dim values() = {
               New With {.d = 0, .m = 1, .s = 66@},
               New With {.d = 1, .m = 66, .s = 0.10000000000000001@},
               New With {.d = 0, .m = 66, .s = 66.099999999999994@},
               New With {.d = 1, .m = -1, .s = 0@},
               New With {.d = 60, .m = -30, .s = -30@ * 60@},
               New With {.d = -360, .m = -10, .s = 0@},
               New With {.d = 0, .m = -30, .s = -8@},
               New With {.d = -33, .m = 0, .s = -1@},
               New With {.d = 33, .m = 0, .s = -1@},
               New With {.d = 0, .m = 0, .s = 3600@},
               New With {.d = 0, .m = 3600, .s = 0@},
               New With {.d = 0, .m = -3600, .s = -3600 * 60@}
           }
            For Each v In values
                a = New Angle(v.d, v.m, v.s)
                Dim expeted = v.d + If(v.d < 0, -1, 1) * v.m / 60.0# + If(v.d < 0 OrElse (v.d = 0 AndAlso v.m < 0), -1, 1) * v.s / 60.0# / 60.0#
                Assert.AreEqual(expeted, a.TotalDegrees, delta, String.Format("v.d={0}, v.m={1}, v.s={2}", v.d, v.m, v.s))
            Next
        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_Single_NaN()
            Dim a = New Angle(Single.NaN)
        End Sub
        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_Single_Inf()
            Dim a = New Angle(Single.PositiveInfinity)
        End Sub
        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_Single_MinusInf()
            Dim a = New Angle(Single.NegativeInfinity)
        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_Double_NaN()
            Dim a = New Angle(Double.NaN)
        End Sub
        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_Double_Inf()
            Dim a = New Angle(Double.PositiveInfinity)
        End Sub
        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_Double_MinusInf()
            Dim a = New Angle(Double.NegativeInfinity)
        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_dms_NaN()
            Dim a As New Angle(0, 0, Double.NaN)
        End Sub
        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_dms_Inf()
            Dim a As New Angle(0, 0, Double.PositiveInfinity)
        End Sub
        <TestMethod(), ExpectedException(GetType(ArgumentException), allowderivedtypes:=True)>
        Public Sub CTor_dms_MinusInf()
            Dim a As New Angle(0, 0, Double.NegativeInfinity)
        End Sub

        <TestMethod()>
        Public Sub Normalize360()
            Assert.AreEqual(0.0#, New Angle(0.0#).Normalize.TotalDegrees)
            Assert.AreEqual(1.0#, New Angle(1.0#).Normalize.TotalDegrees)
            Assert.AreEqual(90.0#, New Angle(90.0#).Normalize.TotalDegrees)
            Assert.AreEqual(100.0#, New Angle(100.0#).Normalize.TotalDegrees)
            Assert.AreEqual(180.0#, New Angle(180.0#).Normalize.TotalDegrees)
            Assert.AreEqual(270.0#, New Angle(270.0#).Normalize.TotalDegrees)
            Assert.AreEqual(359.0#, New Angle(359.0#).Normalize.TotalDegrees)
            Assert.AreEqual(359.99900000000002#, New Angle(359.99900000000002#).Normalize.TotalDegrees)
            Assert.AreEqual(0.0#, New Angle(360.0#).Normalize.TotalDegrees)
            Assert.AreEqual(0.10000000000000001#, New Angle(360.10000000000002#).Normalize.TotalDegrees, delta)
            Assert.AreEqual(0.0#, New Angle(3600.0#).Normalize.TotalDegrees)
            Assert.AreEqual(359.0#, New Angle(-1.0#).Normalize.TotalDegrees)
            Assert.AreEqual(180.0#, New Angle(-180.0#).Normalize.TotalDegrees)
            Assert.AreEqual(0.0#, New Angle(-360.0#).Normalize.TotalDegrees)
            Assert.AreEqual(New Angle(0, 1, 0), New Angle(360, 1, 0).Normalize)
            Assert.AreEqual(New Angle(0, 1, 1), New Angle(360, 1, 1).Normalize)
            Assert.AreEqual(New Angle(0, 0, 1), New Angle(360, 0, 1).Normalize)
            Assert.AreEqual(New Angle(0, 0, 0.1@), New Angle(360, 0, 0.1@).Normalize)
            Assert.AreEqual(New Angle(0, 1, 0), New Angle(720, 1, 0).Normalize)
            Assert.AreEqual(New Angle(0, 1, 1), New Angle(720, 1, 1).Normalize)
            Assert.AreEqual(New Angle(0, 0, 1), New Angle(720, 0, 1).Normalize)
            Assert.AreEqual(New Angle(0, 0, 0.1@), New Angle(720, 0, 0.1@).Normalize)
            Assert.AreEqual(New Angle(359, 59, 59), New Angle(0, 0, -1).Normalize)
            Assert.AreEqual(New Angle(359, 59, 59), New Angle(-360, 0, 1).Normalize)
            Assert.AreEqual(New Angle(359, 59, 0), New Angle(0, -1, 0).Normalize)
            Assert.AreEqual(New Angle(359, 59, 0), New Angle(-360, 1, 0).Normalize)
            Assert.AreEqual(New Angle(359, 58, 59), New Angle(0, -1, 1).Normalize)
            Assert.AreEqual(New Angle(359, 58, 59), New Angle(-360, 1, 1).Normalize)
        End Sub

        <TestMethod()>
        Public Sub Normalize180()
            Assert.AreEqual(0.0#, New Angle(0.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(1.0#, New Angle(1.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(90.0#, New Angle(90.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(100.0#, New Angle(100.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(-180.0#, New Angle(180.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(-90.0#, New Angle(270.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(-90.0#, New Angle(630.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(-90.0#, New Angle(990.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(-1.0#, New Angle(359.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(0.0#, New Angle(360.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(0.10000000000000001#, New Angle(360.10000000000002#).Normalize(180).TotalDegrees, delta)
            Assert.AreEqual(0.0#, New Angle(3600.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(-1.0#, New Angle(-1.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(-180.0#, New Angle(-180.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(0.0#, New Angle(-360.0#).Normalize(180).TotalDegrees)
            Assert.AreEqual(-5.0#, New Angle(-365.0#).Normalize(180).TotalDegrees)
        End Sub

        <TestMethod>
        Public Sub [Mod]()
            Assert.AreEqual(New Angle(0), New Angle(0) Mod 3)
            Assert.AreEqual(New Angle(0), New Angle(0) Mod 1)
            Assert.AreEqual(New Angle(0), New Angle(0) Mod 360)
            Assert.AreEqual(New Angle(0), New Angle(0) Mod -10)

            Assert.AreEqual(New Angle(1), New Angle(1) Mod 3)
            Assert.AreEqual(New Angle(0), New Angle(1) Mod 1)
            Assert.AreEqual(New Angle(1), New Angle(1) Mod 360)
            Assert.AreEqual(New Angle(-1), New Angle(1) Mod -10)

            Assert.AreEqual(New Angle(0), New Angle(3) Mod 3)
            Assert.AreEqual(New Angle(0), New Angle(3) Mod 1)
            Assert.AreEqual(New Angle(3), New Angle(3) Mod 360)
            Assert.AreEqual(New Angle(-3), New Angle(3) Mod -10)

            Assert.AreEqual(New Angle(90), New Angle(90) Mod 360)
            Assert.AreEqual(New Angle(0), New Angle(360) Mod 360)
            Assert.AreEqual(New Angle(5), New Angle(365) Mod 360)
            Assert.AreEqual(New Angle(40), New Angle(400) Mod 360)

            Assert.AreEqual(New Angle(-90), New Angle(90) Mod -360)
            Assert.AreEqual(New Angle(0), New Angle(360) Mod -360)
            Assert.AreEqual(New Angle(-5), New Angle(365) Mod -360)
            Assert.AreEqual(New Angle(-40), New Angle(400) Mod -360)
            Assert.AreEqual(New Angle(0), New Angle(0) Mod -360)

            Assert.AreEqual(New Angle(-90), New Angle(-90) Mod 360)
            Assert.AreEqual(New Angle(0), New Angle(-360) Mod 360)
            Assert.AreEqual(New Angle(-5), New Angle(-365) Mod 360)
            Assert.AreEqual(New Angle(-40), New Angle(-400) Mod 360)

            Assert.AreEqual(New Angle(90), New Angle(-90) Mod -360)
            Assert.AreEqual(New Angle(0), New Angle(-360) Mod -360)
            Assert.AreEqual(New Angle(5), New Angle(-365) Mod -360)
            Assert.AreEqual(New Angle(40), New Angle(-400) Mod -360)

            Assert.AreEqual(New Angle(0, 6, 0), New Angle(360, 6, 0) Mod 360)
            Assert.AreEqual(New Angle(0, 6, 7), New Angle(360, 6, 7) Mod 360)
            Assert.AreEqual(New Angle(0, 0, 11), New Angle(360, 0, 11) Mod 360)
            Assert.AreEqual(New Angle(0, 6, 0), New Angle(0, 6, 0) Mod 360)
            Assert.AreEqual(New Angle(0, 6, 7), New Angle(0, 6, 7) Mod 360)
            Assert.AreEqual(New Angle(0, 0, 11), New Angle(0, 0, 11) Mod 360)
            Assert.AreEqual(New Angle(20, 0, 11), New Angle(380, 0, 11) Mod 360)
            Assert.AreEqual(New Angle(20, 59, 11), New Angle(380, 59, 11) Mod 360)
            Assert.AreEqual(New Angle(20, 33, 0), New Angle(380, 33, 0) Mod 360)
            Assert.AreEqual(New Angle(20, 0, 0.03@), New Angle(380, 0, 0.03@) Mod 360)

            Dim ex As Exception = Nothing
            Try : Dim x = New Angle(0) Mod 0
            Catch ex : End Try
            Assert.IsInstanceOfType(ex, GetType(DivideByZeroException))

            ex = Nothing
            Try : Dim x = New Angle(360) Mod 0
            Catch ex : End Try
            Assert.IsInstanceOfType(ex, GetType(DivideByZeroException))

            ex = Nothing
            Try : Dim x = New Angle(-50) Mod 0
            Catch ex : End Try
            Assert.IsInstanceOfType(ex, GetType(DivideByZeroException))
        End Sub

        <TestMethod()>
        Public Sub Normalize()
            Dim angels = {-3600.0#, -3599.0#, -620.0#, -600.0#, -370.0#, -365.0#, -360.10000000000002#, -360.0#, -359.89999999999998#, -270.0#, -269.0#, -200.0#, -181.0#, -180.0#, -179.0#, -91.0#, -90.0#, -89.0#, -46.0#, -45.0#, -44.0#, -33.0#, -30.0#, -10.0#, -2.0#, -1.0#, -0.5#, -0.10000000000000001#, 0.0#}
            Dim normalize = {-1440, -720, -700, -360, -180, 0, 1, 35, 45, 90, 180, 360, 720, 3600, 362, 700, 719, 720, 721, 1439, 1440, 1441, 1500}
            For Each n In normalize
                For Each m In {1.0#, -1.0#}
                    For Each v In angels
                        Dim value = m * v
                        Dim angle = New Angle(value)
                        Dim normalized = angle.Normalize(n)
                        Dim message = String.Format("Normalize {0} to {1}", angle, n)
                        Assert.IsTrue(normalized.TotalDegrees >= n - 360.0#, message)
                        Assert.IsTrue(normalized.TotalDegrees < n, message)
                        Assert.AreEqual(0.10000000000000001#, 0.10000000000000001# Mod 360)
                        Dim valueMod360 = value Mod 360
                        If valueMod360 < 0 Then valueMod360 += 360
                        Dim normalizedMod360 = normalized.TotalDegrees Mod 360
                        If normalizedMod360 < 0 Then normalizedMod360 += 360
                        Assert.AreEqual(valueMod360, normalizedMod360, delta, message)
                    Next
                Next
            Next
        End Sub

        <TestMethod()>
        Public Sub Abs()
            Assert.AreEqual(New Angle(100.0#), New Angle(100.0#).Abs)
            Assert.AreEqual(New Angle(100.0#), New Angle(-100.0#).Abs)
            Assert.AreEqual(New Angle(0), New Angle(0.0#).Abs)
        End Sub

        <TestMethod()>
        Public Sub Sign()
            Assert.AreEqual(-1, New Angle(-1).Sign)
            Assert.AreEqual(-1, New Angle(-360).Sign)
            Assert.AreEqual(0, New Angle(0).Sign)
            Assert.AreEqual(1, New Angle(1).Sign)
            Assert.AreEqual(1, New Angle(360).Sign)
            Assert.AreEqual(-1, New Angle(-0.01#).Sign)
            Assert.AreEqual(1, New Angle(0.01#).Sign)
        End Sub

        <TestMethod()>
        Public Sub BasicTrigonometry()
            Dim angles = {-360.0#, -180.0#, -33.0#, 0.0#, 19.0#, 45.0#, 100.0#, 180.0#, 359.0#, 360.0#}
            For Each v In angles
                Assert.AreEqual(Math.Cos(v * (Math.PI / 180.0#)), New Angle(v).Cos, String.Format("v={0}", v))
                Assert.AreEqual(Math.Sin(v * (Math.PI / 180.0#)), New Angle(v).Sin, String.Format("v={0}", v))
                Assert.AreEqual(Math.Tan(v * (Math.PI / 180.0#)), New Angle(v).Tan, String.Format("v={0}", v))
            Next
        End Sub

        <TestMethod()>
        Public Sub ExtendedTrigonometry()
            Dim angles = {-360.0#, -180.0#, -33.0#, 0.0#, 19.0#, 45.0#, 100.0#, 180.0#, 359.0#, 360.0#}
            For Each v In angles
                Assert.AreEqual(1.0# / Math.Tan(v * (Math.PI / 180.0#)), New Angle(v).Cot, String.Format("v={0}", v))
                Assert.AreEqual(1.0# / Math.Cos(v * (Math.PI / 180.0#)), New Angle(v).Sec, String.Format("v={0}", v))
                Assert.AreEqual(1.0# / Math.Sin(v * (Math.PI / 180.0#)), New Angle(v).Csc, String.Format("v={0}", v))
            Next
        End Sub
#Region "InvertTrigonometry"
        <TestMethod()>
        Public Sub Asin()
            'An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2 
            Dim angles = {-360.0#, -180.0#, -33.0#, 0.0#, 19.0#, 45.0#, 100.0#, 180.0#, 359.0#, 360.0#}
            For Each v In angles
                Dim a = New Angle(v)
                Assert.AreEqual(Angle.FromRadians(Math.Asin(Math.Sin(a.ToRadians))).TotalDegrees, Angle.Asin(a.Sin).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Asin(a.Sin).TotalDegrees >= -90, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Asin(a.Sin).TotalDegrees <= 90, String.Format("v={0}", v))
            Next

            For Each v In {-1.0#, -0.5#, -0.25#, -0.125#, 0.0#, 0.125#, 0.25#, 0.5#, 1.0#}
                Assert.AreEqual(Angle.FromRadians(Math.Asin(v)).TotalDegrees, Angle.Asin(v).TotalDegrees, String.Format("v={0}", v))
            Next

            For Each v In {Double.NegativeInfinity, -1000.0#, -1.0#, 1.0#, 1000.0#, Double.PositiveInfinity}
                Assert.AreEqual(Angle.FromRadians(Math.Acos(1.0# / v)).TotalDegrees, Angle.Asec(v).TotalDegrees, String.Format("v={0}", v))
            Next
        End Sub
        <TestMethod()>
        Public Sub Acos()
            'An angle, θ, measured in radians, such that 0 ≤ θ ≤ π 
            Dim angles = {-360.0#, -180.0#, -33.0#, 0.0#, 19.0#, 45.0#, 100.0#, 180.0#, 359.0#, 360.0#}
            For Each v In angles
                Dim a = New Angle(v)
                Assert.AreEqual(Angle.FromRadians(Math.Acos(Math.Cos(a.ToRadians))).TotalDegrees, Angle.Acos(a.Cos).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Acos(a.Cos).TotalDegrees >= 0, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Acos(a.Cos).TotalDegrees <= 180, String.Format("v={0}", v))
            Next

            For Each v In {-1.0#, -0.5#, -0.25#, -0.125#, 0.0#, 0.125#, 0.25#, 0.5#, 1.0#}
                Assert.AreEqual(Angle.FromRadians(Math.Acos(v)).TotalDegrees, Angle.Acos(v).TotalDegrees, String.Format("v={0}", v))
            Next

            For Each v In {Double.NegativeInfinity, -1000.0#, -1.0#, 1.0#, 1000.0#, Double.PositiveInfinity}
                Assert.AreEqual(Angle.FromRadians(Math.Asin(1.0# / v)).TotalDegrees, Angle.Acsc(v).TotalDegrees, String.Format("v={0}", v))
            Next
        End Sub
        <TestMethod()>
        Public Sub Atan()
            'An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
            Dim angles = {-360.0#, -180.0#, -33.0#, 0.0#, 19.0#, 45.0#, 100.0#, 180.0#, 359.0#, 360.0#}
            For Each v In angles
                Dim a = New Angle(v)
                Assert.AreEqual(Angle.FromRadians(Math.Atan(Math.Tan(a.ToRadians))).TotalDegrees, Angle.Atan(a.Tan).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Atan(a.Tan).TotalDegrees >= -90, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Atan(a.Tan).TotalDegrees <= 90, String.Format("v={0}", v))
            Next

            For Each v In {Double.NegativeInfinity, -1000.0#, -1.0#, -0.5#, -0.25#, -0.125#, 0.0#, 0.125#, 0.25#, 0.5#, 1.0#, 1000.0#, Double.PositiveInfinity}
                Assert.AreEqual(Angle.FromRadians(Math.Atan(v)).TotalDegrees, Angle.Atan(v).TotalDegrees, String.Format("v={0}", v))
            Next

        End Sub

        <TestMethod()>
        Public Sub Acot()
            Dim fCot = Function(x#) 1.0# / Math.Tan(x)
            Dim fACot = Function(x#) Math.Atan(1.0# / x)
            Dim angles = {-360.0#, -180.0#, -33.0#, 0.0#, 19.0#, 45.0#, 100.0#, 180.0#, 359.0#, 360.0#}
            For Each v In angles
                Dim a = New Angle(v)
                Assert.AreEqual(Angle.FromRadians(fACot(fCot(a.ToRadians))), Angle.Acot(a.Cot), delta, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Acot(a.Cot).TotalDegrees >= -90, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Acot(a.Cot).TotalDegrees <= 90, String.Format("v={0}", v))
            Next

            For Each v In {Double.NegativeInfinity, -1000.0#, -1.0#, -0.5#, -0.25#, -0.125#, 0.0#, 0.125#, 0.25#, 0.5#, 1.0#, 1000.0#, Double.PositiveInfinity}
                Assert.AreEqual(Angle.FromRadians(Math.Atan(1.0# / v)).TotalDegrees, Angle.Acot(v).TotalDegrees, String.Format("v={0}", v))
            Next

        End Sub
        <TestMethod()>
        Public Sub Asec()
            Dim fSec = Function(x#) 1.0# / Math.Cos(x)
            Dim fASec = Function(x#) Math.Acos(1.0# / x)
            Dim angles = {-360.0#, -180.0#, -33.0#, 0.0#, 19.0#, 45.0#, 100.0#, 180.0#, 359.0#, 360.0#}
            For Each v In angles
                Dim a = New Angle(v)
                Assert.AreEqual(Angle.FromRadians(fASec(fSec(a.ToRadians))).TotalDegrees, Angle.Asec(a.Sec).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Acot(a.Cot).TotalDegrees >= -90, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Acot(a.Cot).TotalDegrees <= 90, String.Format("v={0}", v))
            Next
        End Sub
        <TestMethod()>
        Public Sub Acsc()
            Dim fCsc = Function(x#) 1.0# / Math.Sin(x)
            Dim fACsc = Function(x#) Math.Asin(1.0# / x)
            Dim angles = {-360.0#, -180.0#, -33.0#, 0.0#, 19.0#, 45.0#, 100.0#, 180.0#, 359.0#, 360.0#}
            For Each v In angles
                Dim a = New Angle(v)
                Assert.AreEqual(Angle.FromRadians(fACsc(fCsc(a.ToRadians))).TotalDegrees, Angle.Acsc(a.Csc).TotalDegrees, delta, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Acot(a.Cot).TotalDegrees >= -90, String.Format("v={0}", v))
                Assert.IsTrue(Angle.Acot(a.Cot).TotalDegrees <= 90, String.Format("v={0}", v))
            Next

        End Sub
#End Region
        <TestMethod()>
        Public Sub RoundToDegrees()
            Assert.AreEqual(New Angle(1), New Angle(1).RoundToDegrees)
            Assert.AreEqual(New Angle(0), New Angle(0).RoundToDegrees)
            Assert.AreEqual(New Angle(-1), New Angle(-1).RoundToDegrees)

            Assert.AreEqual(New Angle(1), New Angle(1.1000000000000001#).RoundToDegrees)
            Assert.AreEqual(New Angle(0), New Angle(0.10000000000000001#).RoundToDegrees)
            Assert.AreEqual(New Angle(0), New Angle(-0.10000000000000001#).RoundToDegrees)
            Assert.AreEqual(New Angle(-1), New Angle(-1.1000000000000001#).RoundToDegrees)

            Assert.AreEqual(New Angle(2), New Angle(1.8#).RoundToDegrees)
            Assert.AreEqual(New Angle(1), New Angle(0.80000000000000004#).RoundToDegrees)
            Assert.AreEqual(New Angle(-1), New Angle(-0.80000000000000004#).RoundToDegrees)
            Assert.AreEqual(New Angle(-2), New Angle(-1.8#).RoundToDegrees)

            Assert.AreEqual(New Angle(2), New Angle(1.5#).RoundToDegrees)
            Assert.AreEqual(New Angle(1), New Angle(0.5#).RoundToDegrees)
            Assert.AreEqual(New Angle(-1), New Angle(-0.5#).RoundToDegrees)
            Assert.AreEqual(New Angle(-2), New Angle(-1.5#).RoundToDegrees)

            Assert.AreEqual(New Angle(2), New Angle(1.5#).RoundToDegrees(MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(1), New Angle(0.5#).RoundToDegrees(MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(-1), New Angle(-0.5#).RoundToDegrees(MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(-2), New Angle(-1.5#).RoundToDegrees(MidpointRounding.AwayFromZero))

            Assert.AreEqual(New Angle(2), New Angle(1.5#).RoundToDegrees(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(0), New Angle(0.5#).RoundToDegrees(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(0), New Angle(-0.5#).RoundToDegrees(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(-2), New Angle(-1.5#).RoundToDegrees(MidpointRounding.ToEven))

        End Sub

        <TestMethod()>
        Public Sub RoundToMinutes()
            Assert.AreEqual(New Angle(0, 1, 0.0#), New Angle(0, 1, 0).RoundToMinutes)
            Assert.AreEqual(New Angle(0, 0, 0.0#), New Angle(0, 0, 0).RoundToMinutes)
            Assert.AreEqual(New Angle(0, -1, 0.0#), New Angle(0, -1, 0).RoundToMinutes)

            Assert.AreEqual(New Angle(0, 1, 0.0#), New Angle(0, 1, 1).RoundToMinutes)
            Assert.AreEqual(New Angle(0, 0, 0.0#), New Angle(0, 0, 1).RoundToMinutes)
            Assert.AreEqual(New Angle(0, 0, 0.0#), New Angle(0, 0, -1).RoundToMinutes)
            Assert.AreEqual(New Angle(0, -1, 0.0#), New Angle(0, -1, 1).RoundToMinutes)

            Assert.AreEqual(New Angle(0, 2, 0.0#), New Angle(0, 1, 50).RoundToMinutes)
            Assert.AreEqual(New Angle(0, 1, 0.0#), New Angle(0, 0, 50).RoundToMinutes)
            Assert.AreEqual(New Angle(0, -1, 0.0#), New Angle(0, 0, -50).RoundToMinutes)
            Assert.AreEqual(New Angle(0, -2, 0.0#), New Angle(0, -1, 50).RoundToMinutes)

            Assert.AreEqual(New Angle(0, 2, 0.0#), New Angle(0, 1, 30).RoundToMinutes)
            Assert.AreEqual(New Angle(0, 1, 0.0#), New Angle(0, 0, 30).RoundToMinutes)
            Assert.AreEqual(New Angle(0, -1, 0.0#), New Angle(0, 0, -30).RoundToMinutes)
            Assert.AreEqual(New Angle(0, -2, 0.0#), New Angle(0, -1, 30).RoundToMinutes)

            Assert.AreEqual(New Angle(0, 2, 0.0#), New Angle(0, 1, 30).RoundToMinutes(MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(0, 1, 0.0#), New Angle(0, 0, 30).RoundToMinutes(MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(0, -1, 0.0#), New Angle(0, 0, -30).RoundToMinutes(MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(0, -2, 0.0#), New Angle(0, -1, 30).RoundToMinutes(MidpointRounding.AwayFromZero))

            Assert.AreEqual(New Angle(0, 2, 0.0#), New Angle(0, 1, 30).RoundToMinutes(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(0, 0, 0.0#), New Angle(0, 0, 30).RoundToMinutes(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(0, 0, 0.0#), New Angle(0, 0, -30).RoundToMinutes(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(0, -2, 0.0#), New Angle(0, -1, 30).RoundToMinutes(MidpointRounding.ToEven))

            Assert.AreEqual(New Angle(1, 2, 0.0#), New Angle(1, 1, 30).RoundToMinutes(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(1, 0, 0.0#), New Angle(1, 0, 30).RoundToMinutes(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(-1, 0, 0.0#), New Angle(-1, 0, 30).RoundToMinutes(MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(-1, 2, 0.0#), New Angle(-1, 1, 30).RoundToMinutes(MidpointRounding.ToEven))

            Assert.AreEqual(New Angle(1, 2, 0.0#), New Angle(1, 1, 30).RoundToMinutes)
            Assert.AreEqual(New Angle(1, 1, 0.0#), New Angle(1, 0, 30).RoundToMinutes)
            Assert.AreEqual(New Angle(-1, 1, 0.0#), New Angle(-1, 0, 30).RoundToMinutes)
            Assert.AreEqual(New Angle(-1, 2, 0.0#), New Angle(-1, 1, 30).RoundToMinutes)
        End Sub

        <TestMethod()>
        Public Sub RoundToSeconds()
            Assert.AreEqual(New Angle(0, 0, 1.0#), New Angle(0, 0, 1).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, 0.0#), New Angle(0, 0, 0).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, -1), New Angle(0, 0, -1).RoundToSeconds)

            Assert.AreEqual(New Angle(0, 0, 1), New Angle(0, 0, 1.1000000000000001#).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, 0), New Angle(0, 0, 0.10000000000000001).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, -1), New Angle(0, 0, -1.1000000000000001).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, 0), New Angle(0, 0, -0.10000000000000001).RoundToSeconds)

            Assert.AreEqual(New Angle(0, 0, 2), New Angle(0, 0, 1.8999999999999999).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, 1), New Angle(0, 0, 0.90000000000000002).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, -1), New Angle(0, 0, -0.90000000000000002).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, -2), New Angle(0, 0, -1.8999999999999999).RoundToSeconds)

            Assert.AreEqual(New Angle(0, 0, 2), New Angle(0, 0, 1.5).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, 1), New Angle(0, 0, 0.5).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, -1), New Angle(0, 0, -0.5).RoundToSeconds)
            Assert.AreEqual(New Angle(0, 0, -2), New Angle(0, 0, -1.5).RoundToSeconds)

            Assert.AreEqual(New Angle(0, 0, 2), New Angle(0, 0, 1.5).RoundToSeconds(, MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(0, 0, 1), New Angle(0, 0, 0.5).RoundToSeconds(, MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(0, 0, -1), New Angle(0, 0, -0.5).RoundToSeconds(, MidpointRounding.AwayFromZero))
            Assert.AreEqual(New Angle(0, 0, -2), New Angle(0, 0, -1.5).RoundToSeconds(, MidpointRounding.AwayFromZero))

            Assert.AreEqual(New Angle(0, 0, 2), New Angle(0, 0, 1.5).RoundToSeconds(, MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(0, 0, 0), New Angle(0, 0, 0.5).RoundToSeconds(, MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(0, 0, 0), New Angle(0, 0, -0.5).RoundToSeconds(, MidpointRounding.ToEven))
            Assert.AreEqual(New Angle(0, 0, -2), New Angle(0, 0, -1.5).RoundToSeconds(, MidpointRounding.ToEven))

            Assert.AreEqual(New Angle(1, 0, 2), New Angle(1, 0, 1.5).RoundToSeconds)
            Assert.AreEqual(New Angle(1, 0, 1), New Angle(1, 0, 0.5).RoundToSeconds)
            Assert.AreEqual(New Angle(-1, 0, 1), New Angle(-1, 0, 0.5).RoundToSeconds)
            Assert.AreEqual(New Angle(-1, 0, 2), New Angle(-1, 0, 1.5).RoundToSeconds)

            Assert.AreEqual(New Angle(0, 0, 1.22), New Angle(0, 0, 1.222#).RoundToSeconds(2))
            Assert.AreEqual(New Angle(0, 0, -0.22), New Angle(0, 0, -0.222#).RoundToSeconds(2))

            Assert.AreEqual(New Angle(1, 0, 0), New Angle(0, 59, 59.9@).RoundToSeconds)
            Assert.AreEqual(New Angle(1, 0, 0), New Angle(0, 59, 59.5@).RoundToSeconds)
            Assert.AreEqual(New Angle(-1, 0, 0), New Angle(0, -59, 59.9@).RoundToSeconds)
            Assert.AreEqual(New Angle(-1, 0, 0), New Angle(0, -59, 59.5@).RoundToSeconds)

            ' Assert.AreEqual(New Angle(0, 1, 0), New Angle(0, 59, 59.5@).RoundToSeconds)
            Assert.AreEqual(New Angle(0, -1, 0), New Angle(0, 0, -59.9@).RoundToSeconds)
            Assert.AreEqual(New Angle(0, -1, 0), New Angle(0, 0, -59.5@).RoundToSeconds)

            Assert.AreEqual(New Angle(1, 0, 0), New Angle(0, 59, 59.997@).RoundToSeconds(2))
            Assert.AreEqual(New Angle(1, 0, 0), New Angle(0, 59, 59.995@).RoundToSeconds(2))
            Assert.AreEqual(New Angle(-1, 0, 0), New Angle(0, -59, 59.997@).RoundToSeconds(2))
            Assert.AreEqual(New Angle(-1, 0, 0), New Angle(0, -59, 59.995@).RoundToSeconds(2))

            Assert.AreEqual(New Angle(1, 0, 0), New Angle(0, 59, 59.997@).RoundToSeconds(2))
            Assert.AreEqual(New Angle(1, 0, 0), New Angle(0, 59, 59.995@).RoundToSeconds(2))
            Assert.AreEqual(New Angle(0, -1, 0), New Angle(0, 0, -59.997@).RoundToSeconds(2))
            Assert.AreEqual(New Angle(0, -1, 0), New Angle(0, 0, -59.995@).RoundToSeconds(2))

        End Sub

        <TestMethod()>
        Public Sub Truncate()
            Assert.AreEqual(New Angle(0), New Angle(0).TruncateToDegrees)
            Assert.AreEqual(New Angle(0), New Angle(0).TruncateToMinutes)
            Assert.AreEqual(New Angle(0), New Angle(0).TruncateToSeconds)

            Assert.AreEqual(New Angle(0), New Angle(0, 1, 0).TruncateToDegrees)
            Assert.AreEqual(New Angle(0), New Angle(0, 0, 1).TruncateToMinutes)
            Assert.AreEqual(New Angle(0), New Angle(0, 0, 0.10000000000000001).TruncateToSeconds)

            Assert.AreEqual(New Angle(0), New Angle(0, -1, 0).TruncateToDegrees)
            Assert.AreEqual(New Angle(0), New Angle(0, 0, -1).TruncateToMinutes)
            Assert.AreEqual(New Angle(0), New Angle(0, 0, -0.10000000000000001).TruncateToSeconds)

            Assert.AreEqual(New Angle(1), New Angle(1, 1, 0).TruncateToDegrees)
            Assert.AreEqual(New Angle(0, 1, 0), New Angle(0, 1, 1).TruncateToMinutes)
            Assert.AreEqual(New Angle(0, 0, 1), New Angle(0, 0, 1.1000000000000001).TruncateToSeconds)

            Assert.AreEqual(New Angle(-1), New Angle(-1, 1, 0).TruncateToDegrees)
            Assert.AreEqual(New Angle(0, -1, 0), New Angle(0, -1, 1).TruncateToMinutes)
            Assert.AreEqual(New Angle(0, 0, -1), New Angle(0, 0, -1.1000000000000001).TruncateToSeconds)
        End Sub

        <TestMethod()>
        Public Sub Ceiling()
            Assert.AreEqual(New Angle(0), New Angle(0).CeilingDegrees)
            Assert.AreEqual(New Angle(0), New Angle(0).CeilingMinutes)
            Assert.AreEqual(New Angle(0), New Angle(0).CeilingSeconds)

            Assert.AreEqual(New Angle(1), New Angle(0, 1, 0).CeilingDegrees)
            Assert.AreEqual(New Angle(0, 1, 0), New Angle(0, 0, 1).CeilingMinutes)
            Assert.AreEqual(New Angle(0, 0, 1), New Angle(0, 0, 0.10000000000000001).CeilingSeconds)

            Assert.AreEqual(New Angle(-1), New Angle(0, -1, 0).CeilingDegrees)
            Assert.AreEqual(New Angle(0, -1, 0), New Angle(0, 0, -1).CeilingMinutes, delta)
            Assert.AreEqual(New Angle(0, 0, -1), New Angle(0, 0, -0.10000000000000001).CeilingSeconds)

            Assert.AreEqual(New Angle(2), New Angle(1, 1, 0).CeilingDegrees)
            Assert.AreEqual(New Angle(0, 2, 0), New Angle(0, 1, 1).CeilingMinutes, delta)
            Assert.AreEqual(New Angle(0, 0, 2), New Angle(0, 0, 1.1000000000000001).CeilingSeconds)

            Assert.AreEqual(New Angle(-2), New Angle(-1, 1, 0).CeilingDegrees)
            Assert.AreEqual(New Angle(0, -2, 0), New Angle(0, -1, 1).CeilingMinutes, delta)
            Assert.AreEqual(New Angle(0, 0, -2), New Angle(0, 0, -1.1000000000000001).CeilingSeconds, delta)

            Assert.AreEqual(New Angle(1, 0, 0), New Angle(0, 59, 59.3@).CeilingSeconds)
            Assert.AreEqual(New Angle(0, 1, 0), New Angle(0, 0, 59.3@).CeilingSeconds)
            Assert.AreEqual(New Angle(0, 1, 2), New Angle(0, 1, 1.3@).CeilingSeconds)

            Assert.AreEqual(New Angle(-1, 0, 0), New Angle(0, -59, 59.3@).CeilingSeconds)
            Assert.AreEqual(New Angle(0, -1, 0), New Angle(0, 0, -59.3@).CeilingSeconds)
            Assert.AreEqual(New Angle(0, -1, 2), New Angle(0, -1, 1.3@).CeilingSeconds)

            Assert.AreEqual(New Angle(3, 0, 0), New Angle(2, 59, 1).CeilingMinutes)
            Assert.AreEqual(New Angle(-3, 0, 0), New Angle(-2, 59, 1).CeilingMinutes)
        End Sub

        <TestMethod()>
        Public Sub CTypeFromNetTypes()
            Assert.AreEqual(New Angle(5.2000000000000002#), CType(5.2000000000000002#, Angle))
            Assert.AreEqual(New Angle(5.19999981!), CType(5.19999981!, Angle))
            Assert.AreEqual(New Angle(5.2@), CType(5.2@, Angle))
            Assert.AreEqual(New Angle(5%), CType(5%, Angle))
            Assert.AreEqual(New Angle(66.5#), CType(TimeSpan.FromHours(66.5#), Angle))
            Assert.AreEqual(New Angle(66.5#), CType(TimeSpanFormattable.FromHours(66.5#), Angle))
        End Sub

        <TestMethod()>
        Public Sub CTypeToNetTypes()
            Dim a As Angle = New Angle(55.200000000000003#)
            Assert.AreEqual(55.200000000000003#, CDbl(a))
            Assert.AreEqual(55.2000008!, CSng(a))
            Assert.AreEqual(55.2@, CDec(a))
            Assert.AreEqual(55%, CInt(a))
            Assert.AreEqual(TimeSpan.FromHours(55.200000000000003#), CType(a, TimeSpan))
            Assert.AreEqual(TimeSpanFormattable.FromHours(55.200000000000003#), CType(a, TimeSpanFormattable))
        End Sub

        <TestMethod()>
        Public Sub CTypeFromRational()
            Assert.AreEqual(New Angle(0), CType(CType(Nothing, URational()), Angle))
            Assert.AreEqual(New Angle(0), CType(New URational() {}, Angle))
            Assert.AreEqual(New Angle(14), CType({New URational(14, 1)}, Angle))
            Assert.AreEqual(New Angle(14, 5, 0), CType({New URational(14, 1), New URational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(14, 5, 59), CType({New URational(14, 1), New URational(5, 1), New URational(59, 1)}, Angle))
            Assert.AreEqual(New Angle(14, 5, 59.0# + 3.0# / 60.0#), CType({New URational(14, 1), New URational(5, 1), New URational(59, 1), New URational(3, 1)}, Angle))

            Assert.AreEqual(New Angle(0), CType(CType(Nothing, SRational()), Angle))
            Assert.AreEqual(New Angle(0), CType(New SRational() {}, Angle))
            Assert.AreEqual(New Angle(14), CType({New SRational(14, 1)}, Angle))
            Assert.AreEqual(New Angle(14, 5, 0), CType({New SRational(14, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(14, 5, 59), CType({New SRational(14, 1), New SRational(5, 1), New SRational(59, 1)}, Angle))
            Assert.AreEqual(New Angle(14, 5, 59.0# + 3.0# / 60.0#), CType({New SRational(14, 1), New SRational(5, 1), New SRational(59, 1), New SRational(3, 1)}, Angle))

            Assert.AreEqual(New Angle(-14, 5, 59), CType({New SRational(-14, 1), New SRational(5, 1), New SRational(59, 1)}, Angle))

            Assert.AreEqual(New Angle(14, 80, 62), CType({New URational(14, 1), New URational(80, 1), New URational(62, 1)}, Angle))
            Assert.AreEqual(New Angle(14, 80, 62), CType({New SRational(14, 1), New SRational(80, 1), New SRational(62, 1)}, Angle))
            Assert.AreEqual(New Angle(14, -80, 62), CType({New SRational(14, 1), New SRational(-80, 1), New SRational(62, 1)}, Angle))
            Assert.AreEqual(New Angle(14, 80, -62), CType({New SRational(14, 1), New SRational(80, 1), New SRational(-62, 1)}, Angle))
            Assert.AreEqual(New Angle(-14, 80, -62), CType({New SRational(-14, 1), New SRational(80, 1), New SRational(-62, 1)}, Angle))

            Assert.AreEqual(New Angle(14, 5, 1.0# / 3.0#), CType({New SRational(14, 1), New SRational(5, 1), New SRational(1, 3)}, Angle))
            Assert.AreEqual(New Angle(14, 5, 1.0# / 3.0#), CType({New URational(14, 1), New URational(5, 1), New URational(1, 3)}, Angle))

            Assert.AreEqual(New Angle(0, 0, 1.0# - 3.0# / 60.0#), CType({New SRational(0, 1), New SRational(0, 1), New SRational(1, 1), -New SRational(3, 1)}, Angle))
            Assert.AreEqual(New Angle(0, 0, -1.0# + 3.0# / 60.0#), CType({New SRational(0, 1), New SRational(0, 1), New SRational(-1, 1), -New SRational(3, 1)}, Angle))

            Assert.AreEqual(New Angle(1, 0, 5 / 3600), CType({New SRational(1, 1), New SRational(0, 1), New SRational(0, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(1, 0, -5 / 3600), CType({New SRational(1, 1), New SRational(0, 1), New SRational(0, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))
            Assert.AreEqual(New Angle(-1, 0, 5 / 3600), CType({New SRational(1, -1), New SRational(0, 1), New SRational(0, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(-1, 0, -5 / 3600), CType({New SRational(-1, 1), New SRational(0, 1), New SRational(0, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))

            Assert.AreEqual(New Angle(1, 0, 1 + 5 / 3600), CType({New SRational(1, 1), New SRational(0, 1), New SRational(1, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(1, 0, 1 - 5 / 3600), CType({New SRational(1, 1), New SRational(0, 1), New SRational(1, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))
            Assert.AreEqual(New Angle(-1, 0, 1 + 5 / 3600), CType({New SRational(1, -1), New SRational(0, 1), New SRational(1, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(-1, 0, 1 - 5 / 3600), CType({New SRational(-1, 1), New SRational(0, 1), New SRational(1, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))

            Assert.AreEqual(New Angle(1, 0, -1 + 5 / 3600), CType({New SRational(1, 1), New SRational(0, 1), New SRational(-1, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(1, 0, -1 - 5 / 3600), CType({New SRational(1, 1), New SRational(0, 1), New SRational(-1, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))
            Assert.AreEqual(New Angle(-1, 0, -1 + 5 / 3600), CType({New SRational(1, -1), New SRational(0, 1), New SRational(-1, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(-1, 0, -1 - 5 / 3600), CType({New SRational(-1, 1), New SRational(0, 1), New SRational(-1, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))

            Assert.AreEqual(New Angle(0, 0, 5 / 3600), CType({New SRational(0, 1), New SRational(0, 1), New SRational(0, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(0, 0, -5 / 3600), CType({New SRational(0, 1), New SRational(0, 1), New SRational(0, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))
            Assert.AreEqual(New Angle(0, 0, 1 + 5 / 3600), CType({New SRational(0, 1), New SRational(0, 1), New SRational(1, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(0, 0, 1 - 5 / 3600), CType({New SRational(0, 1), New SRational(0, 1), New SRational(1, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))
            Assert.AreEqual(New Angle(0, 0, -1 - 5 / 3600), CType({New SRational(0, 1), New SRational(0, 1), New SRational(-1, 1), New SRational(0, 1), New SRational(5, 1)}, Angle))
            Assert.AreEqual(New Angle(0, 0, -1 + 5 / 3600), CType({New SRational(0, 1), New SRational(0, 1), New SRational(-1, 1), New SRational(0, 1), New SRational(-5, 1)}, Angle))
        End Sub

        <TestMethod()>
        Public Sub AngleComparison()
            Dim a10 As Angle = 10
            Dim a15 As Angle = 15
            Dim a360 As Angle = 360
            Dim am360 As Angle = -360
            Dim a0 As Angle = 0
            Dim am180 As Angle = -180
            Dim a180 As Angle = 180

            Assert.IsTrue(a10 < a15)
            Assert.IsTrue(a15 > a10)
            Assert.IsTrue(a10 = a10)

            Assert.IsFalse(a10 > a15)
            Assert.IsFalse(a15 < a10)
            Assert.IsFalse(a10 <> a10)

            Assert.IsTrue(a10 <= a15)
            Assert.IsTrue(a15 >= a10)
            Assert.IsTrue(a10 <= a10)
            Assert.IsTrue(a10 >= a10)

            Assert.IsFalse(am360 = a360)
            Assert.IsFalse(am180 = a180)
            Assert.IsTrue(am180 > am360)
            Assert.IsFalse(am180 > a0)

            'Assert.AreEqual(am180, a180)
            'Assert.AreEqual(a0, a360)
            'Assert.AreEqual(a0, am360)
        End Sub

        <TestMethod()>
        Public Sub DoubleComparrison()
            Dim a10 As Angle = 10
            Dim a15 As Angle = 15
            Dim a360 As Angle = 360
            Dim am360 As Angle = 360
            Dim a0 As Angle = 0
            Dim am180 As Angle = -180
            Dim a180 As Angle = 180

            Assert.IsTrue(a10 < 15.0#)
            Assert.IsTrue(a15 > 10.0#)
            Assert.IsTrue(a10 = 10.0#)

            Assert.IsFalse(a10 > 15.0#)
            Assert.IsFalse(a15 < 10.0#)
            Assert.IsFalse(a10 <> 10.0#)

            Assert.IsTrue(a10 <= 15.0#)
            Assert.IsTrue(a15 >= 10.0#)
            Assert.IsTrue(a10 <= 10.0#)
            Assert.IsTrue(a10 >= 10.0#)

            Assert.IsTrue(am360 = 360.0#)
            Assert.IsTrue(am180 < 180.0#)
            Assert.IsFalse(am180 > 360.0#)
            Assert.IsFalse(am180 > 0.0#)

            'Assert.AreEqual(am180, 180.0#)
            'Assert.AreEqual(a0, 360.0#)
            'Assert.AreEqual(a0, 360.0#)
        End Sub

        <TestMethod()>
        Public Sub IntegerComparison()
            Dim a10 As Angle = 10
            Dim a15 As Angle = 15
            Dim a360 As Angle = 360
            Dim am360 As Angle = 360
            Dim a0 As Angle = 0
            Dim am180 As Angle = -180
            Dim a180 As Angle = 180

            Assert.IsTrue(a10 < 15%)
            Assert.IsTrue(a15 > 10%)
            Assert.IsTrue(a10 = 10%)

            Assert.IsFalse(a10 > 15%)
            Assert.IsFalse(a15 < 10%)
            Assert.IsFalse(a10 <> 10%)

            Assert.IsTrue(a10 <= 15%)
            Assert.IsTrue(a15 >= 10%)
            Assert.IsTrue(a10 <= 10%)
            Assert.IsTrue(a10 >= 10%)

            Assert.IsTrue(am360 = 360%)
            Assert.IsFalse(am180 = 180%)
            Assert.IsFalse(am180 > 360%)
            Assert.IsTrue(am180 < 0%)


            Assert.IsTrue(New Angle(0, 0, 1) > 0)
            Assert.IsTrue(-New Angle(0, 0, 1) < 0)
            Assert.IsTrue(New Angle(1, 0, 1) > 1)
            Assert.IsTrue(-New Angle(1, 0, 1) < -1)
            Assert.IsTrue(-New Angle(0, 0, 1) > -1)

            Assert.IsTrue(New Angle(0, 1, 0) > 0)
            Assert.IsTrue(-New Angle(0, 1, 0) < 0)
            Assert.IsTrue(New Angle(1, 1, 0) > 1)
            Assert.IsTrue(-New Angle(1, 1, 0) < -1)
            Assert.IsTrue(-New Angle(0, 1, 0) > -1)

            Assert.IsTrue(New Angle(0, 0, 1) >= 0)
            Assert.IsTrue(-New Angle(0, 0, 1) <= 0)
            Assert.IsTrue(New Angle(1, 0, 1) >= 1)
            Assert.IsTrue(-New Angle(1, 0, 1) <= -1)
            Assert.IsTrue(-New Angle(0, 0, 1) >= -1)

            Assert.IsTrue(New Angle(0, 1, 0) >= 0)
            Assert.IsTrue(-New Angle(0, 1, 0) <= 0)
            Assert.IsTrue(New Angle(1, 1, 0) >= 1)
            Assert.IsTrue(-New Angle(1, 1, 0) <= -1)
            Assert.IsTrue(-New Angle(0, 1, 0) >= -1)

            Assert.IsFalse(New Angle(1, 1, 1) = 1)
            Assert.IsFalse(New Angle(1, 0, 1) = 1)
            Assert.IsFalse(New Angle(1, 1, 0) = 1)
            Assert.IsTrue(New Angle(1, 0, 0) = 1)
            Assert.IsFalse(New Angle(-1, 1, 1) = -1)
            Assert.IsFalse(New Angle(-1, 0, 1) = -1)
            Assert.IsFalse(New Angle(-1, 1, 0) = -1)
            Assert.IsTrue(New Angle(-1, 0, 0) = -1)
            Assert.IsFalse(New Angle(-1) = 1)
            Assert.IsFalse(New Angle(1) = -1)

            Assert.IsTrue(New Angle(-300) < 150)
            Assert.IsTrue(New Angle(150) > -10)
        End Sub

        <TestMethod()>
        Public Sub Multiply()
            Assert.AreEqual(New Angle(30), (New Angle(30) * 1.0#))
            Assert.AreEqual(New Angle(30), (New Angle(30) * 1.0!))
            Assert.AreEqual(New Angle(30), (New Angle(30) * 1%))

            Assert.AreEqual(New Angle(30), (New Angle(15) * 2.0#))
            Assert.AreEqual(New Angle(30), (New Angle(15) * 2.0!))
            Assert.AreEqual(New Angle(30), (New Angle(15) * 2%))

            Assert.AreEqual(New Angle(30), (New Angle(10) * 3.0#))
            Assert.AreEqual(New Angle(30), (New Angle(10) * 3.0!))
            Assert.AreEqual(New Angle(30), (New Angle(10) * 3%))

            Assert.AreEqual(New Angle(30), (New Angle(-30) * -1.0#))
            Assert.AreEqual(New Angle(30), (New Angle(-30) * -1.0!))
            Assert.AreEqual(New Angle(30), (New Angle(-30) * -1%))

            Assert.AreEqual(New Angle(-30), (New Angle(30) * -1.0#))
            Assert.AreEqual(New Angle(-30), (New Angle(30) * -1.0!))
            Assert.AreEqual(New Angle(-30), (New Angle(30) * -1%))

            Assert.AreEqual(New Angle(-63), (New Angle(30) * -2.1000000000000001#))
            Assert.AreEqual(New Angle(-63).TotalDegrees, (New Angle(30) * -2.0999999!).TotalDegrees, 0.0000030000000000000001)
            Assert.AreEqual(New Angle(-60), (New Angle(30) * -2%))

            Assert.AreEqual(New Angle(30), (New Angle(60) * 0.5#))
            Assert.AreEqual(New Angle(30), (New Angle(60) * 0.5!))
            Assert.AreEqual(New Angle(30), (New Angle(90) * 0.33333333333333331#))
            Assert.AreEqual(New Angle(30).TotalDegrees, (New Angle(90) * 0.333333343!).TotalDegrees, 0.0000030000000000000001)

            Assert.AreEqual(New Angle(30000), (New Angle(30) * 1000.0#))
            Assert.AreEqual(New Angle(30000), (New Angle(30) * 1000.0!))
            Assert.AreEqual(New Angle(30000), (New Angle(30) * 1000%))

        End Sub

        <TestMethod()>
        Public Sub Divide()
            Assert.AreEqual(New Angle(30).TotalDegrees, (New Angle(30) / 1.0#).TotalDegrees)
            Assert.AreEqual(New Angle(30).TotalDegrees, (New Angle(30) / 1.0!).TotalDegrees)
            Assert.AreEqual(New Angle(30).TotalDegrees, (New Angle(30) / 1%).TotalDegrees)

            Assert.AreEqual(New Angle(7.5).TotalDegrees, (New Angle(15) / 2.0#).TotalDegrees)
            Assert.AreEqual(New Angle(7.5).TotalDegrees, (New Angle(15) / 2.0!).TotalDegrees)
            Assert.AreEqual(New Angle(7.5).TotalDegrees, (New Angle(15) / 2%).TotalDegrees)

            Assert.AreEqual(New Angle(10).TotalDegrees, (New Angle(30) / 3.0#).TotalDegrees)
            Assert.AreEqual(New Angle(10).TotalDegrees, (New Angle(30) / 3.0!).TotalDegrees)
            Assert.AreEqual(New Angle(10).TotalDegrees, (New Angle(30) / 3%).TotalDegrees)

            Assert.AreEqual(New Angle(30).TotalDegrees, (New Angle(-30) / -1.0#).TotalDegrees)
            Assert.AreEqual(New Angle(30).TotalDegrees, (New Angle(-30) / -1.0!).TotalDegrees)
            Assert.AreEqual(New Angle(30).TotalDegrees, (New Angle(-30) / -1%).TotalDegrees)

            Assert.AreEqual(New Angle(-30).TotalDegrees, (New Angle(30) / -1.0#).TotalDegrees)
            Assert.AreEqual(New Angle(-30).TotalDegrees, (New Angle(30) / -1.0!).TotalDegrees)
            Assert.AreEqual(New Angle(-30).TotalDegrees, (New Angle(30) / -1%).TotalDegrees)

            Assert.AreEqual(New Angle(-30).TotalDegrees, (New Angle(63) / -2.1000000000000001#).TotalDegrees)
            Assert.AreEqual(New Angle(63.0# / -2.0999999!).TotalDegrees, (New Angle(63) / -2.0999999!).TotalDegrees, delta)
            Assert.AreEqual(New Angle(-30).TotalDegrees, (New Angle(60) / -2%).TotalDegrees)

            Assert.AreEqual(New Angle(120).TotalDegrees, (New Angle(60) / 0.5#).TotalDegrees)
            Assert.AreEqual(New Angle(120).TotalDegrees, (New Angle(60) / 0.5!).TotalDegrees)
            Assert.AreEqual(New Angle(300).TotalDegrees, (New Angle(90) / 0.29999999999999999#).TotalDegrees)
            Assert.AreEqual(New Angle(90.0# / 0.300000012!).TotalDegrees, (New Angle(90) / 0.300000012!).TotalDegrees, delta)

            Assert.AreEqual(New Angle(30000).TotalDegrees, (New Angle(30) / 0.001#).TotalDegrees)
            Assert.AreEqual(New Angle(30.0# / 0.00100000005!).TotalDegrees, (New Angle(30) / 0.00100000005!).TotalDegrees, delta)
        End Sub

        <TestMethod()>
        Public Sub IntegerDivide()
            Assert.AreEqual(360, New Angle(360) \ 1)
            Assert.AreEqual(720, New Angle(720) \ 1)
            Assert.AreEqual(359, New Angle(359) \ 1)
            Assert.AreEqual(100, New Angle(100) \ 1)
            Assert.AreEqual(0, New Angle(0) \ 1)
            Assert.AreEqual(-360, New Angle(-360) \ 1)
            Assert.AreEqual(-360, New Angle(-720) \ 2)
            Assert.AreEqual(-360, New Angle(720) \ -2)
            Assert.AreEqual(360, New Angle(-720) \ -2)
            Assert.AreEqual(9, New Angle(90) \ 10)
            Assert.AreEqual(9, New Angle(99) \ 10)
            Assert.AreEqual(9, New Angle(91) \ 10)
        End Sub

        <TestMethod()>
        Public Sub UnaryMinus()
            Assert.AreEqual(New Angle(-100), -New Angle(100))
            Assert.AreEqual(New Angle(0), -New Angle(0))
            Assert.AreEqual(New Angle(100), -New Angle(-100))
        End Sub

        <TestMethod()>
        Public Sub UnaryPlus()
            Assert.AreEqual(New Angle(100), +New Angle(100))
            Assert.AreEqual(New Angle(0), +New Angle(0))
            Assert.AreEqual(New Angle(-100), +New Angle(-100))
        End Sub

        <TestMethod()>
        Public Sub Plus()
            TestPlusMinus(Function(a, b) a + b, Function(a, b) a + b, Function(a, b) a + b)
        End Sub

        Private Sub TestPlusMinus(angle As Func(Of Angle, Angle, Angle), [integer] As Func(Of Integer, Integer, Integer), [decimal] As Func(Of Decimal, Decimal, Decimal))
            Dim array = {-360, -180, -270, -90, 0, 33, 45, 180, 360, 720}
            For Each a In array
                For Each b In array
                    Assert.AreEqual(New Angle(a - b), (New Angle(a) - New Angle(b)), String.Format("a={0}, b={1}", a, b))
                Next
            Next

            Dim arr2 = {{-30, 15, 0, 0}, {0, -15, 0, 0}, {0, 0, -15, 0}, {-15, 0, 30, 0}, {-15, 15, 15, 0}, {0, -15, 59, 0},
                        {30, 59, 0, 0}, {0, 59, 0, 0}, {0, 0, 59, 0}, {15, 0, 59, 0}, {15, 59, 59, 9}, {0, 59, 15, 9},
                        {30, 15, 0, 3}, {0, 15, 0, 3}, {0, 0, 15, 3}, {15, 0, 30, 3}, {15, 15, 15, 9}, {0, 15, 59, 9},
                        {-30, 15, 0, 3}, {0, -15, 0, 3}, {0, 0, -15, 3}, {-15, 0, 30, 3}, {-15, 15, 15, 9}, {0, -15, 59, 9}
                       }
            For i = 0 To arr2.GetLength(0) - 1
                Dim d1 = arr2(i, 0)
                Dim m1 = arr2(i, 1)
                Dim s1 = arr2(i, 2) + CDec(arr2(i, 3)) * Math.Sign(arr2(i, 2)) / 10
                Dim a = New Angle(d1, m1, s1)
                Try
                    Assert.AreEqual(Math.Abs(d1), a.Degrees)
                    Assert.AreEqual(Math.Abs(m1), a.Minutes)
                    Assert.AreEqual(Math.Abs(s1), a.Seconds)
                    Assert.AreEqual(SgDetect(d1, m1, s1), a.Sign)
                Catch ex As AssertFailedException
                    Throw New AssertInconclusiveException(ex.Message, ex)
                End Try
                For j = 0 To arr2.GetLength(0) - 1
                    Dim d2 = arr2(j, 0)
                    Dim m2 = arr2(j, 1)
                    Dim s2 = arr2(j, 2) + CDec(arr2(j, 3)) * Math.Sign(arr2(j, 2)) / 10
                    Dim b = New Angle(d2, m2, s2)
                    Try
                        Assert.AreEqual(Math.Abs(d2), b.Degrees)
                        Assert.AreEqual(Math.Abs(m2), b.Minutes)
                        Assert.AreEqual(Math.Abs(s2), b.Seconds)
                        Assert.AreEqual(SgDetect(d2, m2, s2), b.Sign)
                    Catch ex As AssertFailedException
                        Throw New AssertInconclusiveException(ex.Message, ex)
                    End Try

                    Dim message = String.Format(vbCrLf & "{0}°{1}′{2}″ + {3}°{4}′{5}″" & vbCrLf & "{6} + {7}", d1, m1, s2, d2, m2, s2, a, b)
                    Dim ed = [integer](Math.Abs(d1) * a.Sign, Math.Abs(d2) * b.Sign)
                    Dim em = [integer](Math.Abs(m1) * a.Sign, Math.Abs(m2) * b.Sign)
                    If ed < 0 Then em = -em
                    Dim es = [decimal](Math.Abs((s1)) * a.Sign, Math.Abs(s2) * b.Sign)
                    If ed < 0 OrElse (ed = 0 AndAlso em < 0) Then es = -es
                    Assert.AreEqual(New Angle(ed, em, es), angle(a, b), message)
                Next
            Next
        End Sub

        Private Function SgDetect(d%, m%, s%) As Integer
            If d <> 0 Then Return Math.Sign(d)
            If m <> 0 Then Return Math.Sign(m)
            Return Math.Sign(s)
        End Function

        <TestMethod()>
        Public Sub Minus()
            TestPlusMinus(Function(a, b) a - b, Function(a, b) a - b, Function(a, b) a - b)
        End Sub

        '<TestMethod()>
        'Public Shadows Sub GetHashCode()
        '    Assert.AreEqual(New Angle(0).GetHashCode, New Angle(360).GetHashCode)
        '    Assert.AreEqual(New Angle(0).GetHashCode, New Angle(720).GetHashCode)
        '    Assert.AreEqual(New Angle(0).GetHashCode, New Angle(-360).GetHashCode)
        '    Assert.AreEqual(New Angle(180).GetHashCode, New Angle(-180).GetHashCode)
        '    Assert.AreEqual(New Angle(-90).GetHashCode, New Angle(270).GetHashCode)
        'End Sub

        <TestMethod()>
        Public Overloads Sub Equals()
            Assert.IsTrue(New Angle(180).Equals(180.0#))
            Assert.IsFalse(New Angle(180).Equals(-180.0#))
            Assert.IsTrue(New Angle(180).Equals(180.0!))
            Assert.IsFalse(New Angle(180).Equals(-180.0!))
            Assert.IsTrue(New Angle(180).Equals(180@))
            Assert.IsFalse(New Angle(180).Equals(-180@))
            Assert.IsTrue(New Angle(180).Equals(180%))
            Assert.IsFalse(New Angle(180).Equals(-180%))
            Assert.IsTrue(New Angle(180).Equals(180UI))
            Assert.IsFalse(New Angle(-180).Equals(180UI))
            Assert.IsTrue(New Angle(180).Equals(180S))
            Assert.IsFalse(New Angle(180).Equals(-180S))
            Assert.IsTrue(New Angle(180).Equals(180US))
            Assert.IsFalse(New Angle(-180).Equals(180US))
            Assert.IsTrue(New Angle(180).Equals(180L))
            Assert.IsFalse(New Angle(180).Equals(-180L))
            Assert.IsTrue(New Angle(180).Equals(180UL))
            Assert.IsFalse(New Angle(-180).Equals(180UL))
            Assert.IsTrue(New Angle(180).Equals(CByte(180)))
            Assert.IsFalse(New Angle(-180).Equals(CByte(180)))
            Assert.IsTrue(New Angle(90).Equals(CSByte(90)))
            Assert.IsFalse(New Angle(270).Equals(CSByte(-90)))
            Assert.IsTrue(New Angle(180).Equals(CType(180, BigInteger)))
            Assert.IsFalse(New Angle(180).Equals(CType(-180, BigInteger)))
            Assert.IsTrue(New Angle(180).Equals(New Angle(180)))
            Assert.IsFalse(New Angle(180).Equals(New Angle(-180)))
            Assert.IsTrue(New Angle(180).Equals(TimeSpan.FromHours(180)))
            Assert.IsFalse(New Angle(180).Equals(TimeSpan.FromHours(-180)))
            Assert.IsTrue(New Angle(180).Equals(TimeSpanFormattable.FromHours(180)))
            Assert.IsFalse(New Angle(180).Equals(TimeSpanFormattable.FromHours(-180)))
        End Sub

        <TestMethod()>
        Public Sub CompareTo()
            Assert.AreEqual(0, New Angle(180).CompareTo(New Angle(180)))
            Assert.AreEqual(1, New Angle(180).CompareTo(New Angle(-180)))
            Assert.AreEqual(-1, New Angle(180).CompareTo(New Angle(181)))
            Assert.AreEqual(1, New Angle(180).CompareTo(New Angle(-179)))
            Assert.AreEqual(1, New Angle(180).CompareTo(New Angle(179)))
            Assert.AreEqual(1, New Angle(180).CompareTo(New Angle(-181)))
        End Sub

        <TestMethod()>
        Public Sub ToString_Symbols()
            Dim a1 As New Angle(10) 'South, East
            Dim a2 As New Angle(-10) 'North, West

            For Each culture In {CurrentCulture, InvariantCulture, GetCultureInfo("ar"), GetCultureInfo("cs"), GetCultureInfo("en-US")}
                Dim ni = culture.NumberFormat
                Dim ai = culture.GetAngleFormatInfo
                Assert.AreEqual(ai.LatitudeSouthShortSymbol, a1.ToString("%a", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LatitudeSouthShortSymbol, a1.ToString("%φ", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LatitudeNorthShortSymbol, a2.ToString("%a", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LatitudeNorthShortSymbol, a2.ToString("%φ", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LongitudeEastShortSymbol, a1.ToString("%o", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LongitudeEastShortSymbol, a1.ToString("%λ", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LongitudeWestShortSymbol, a2.ToString("%o", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LongitudeWestShortSymbol, a2.ToString("%λ", culture), String.Format("culture={0}", culture.Name))

                Assert.AreEqual(ai.LatitudeSouthLongSymbol, a1.ToString("%A", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LatitudeSouthLongSymbol, a1.ToString("%Φ", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LatitudeNorthLongSymbol, a2.ToString("%A", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LatitudeNorthLongSymbol, a2.ToString("%Φ", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LongitudeEastLongSymbol, a1.ToString("%O", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LongitudeEastLongSymbol, a1.ToString("%Λ", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LongitudeWestLongSymbol, a2.ToString("%O", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.LongitudeWestLongSymbol, a2.ToString("%Λ", culture), String.Format("culture={0}", culture.Name))

                Assert.AreEqual("", a1.ToString("%-", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ni.NegativeSign, a2.ToString("%-", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ni.NegativeSign, a2.ToString("%+", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ni.PositiveSign, a1.ToString("%+", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ni.NumberDecimalSeparator, a1.ToString("%.", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ni.NumberGroupSeparator, a1.ToString("%,", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.DegreeSign, a1.ToString("%°", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.SecondSign, a1.ToString("%'", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.MinuteSign, a1.ToString("%""", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.SecondSign, a1.ToString("%′", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.MinuteSign, a1.ToString("%″", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.CompatibilityDegreeSign, a1.ToString("c°", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.CompatibilitySecondSign, a1.ToString("c'", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.CompatibilityMinuteSign, a1.ToString("c""", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.CompatibilitySecondSign, a1.ToString("c′", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ai.CompatibilityMinuteSign, a1.ToString("c″", culture), String.Format("culture={0}", culture.Name))

                Assert.AreEqual("\", a1.ToString("\\", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(".", a1.ToString("\.", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(",", a1.ToString("\,", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("""", a1.ToString("\""", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("\%", a1.ToString("\%", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("D", a1.ToString("\D", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("(", a1.ToString("\(", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("[", a1.ToString("\[", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(")", a1.ToString("%)", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("]]", a1.ToString("]]", culture), String.Format("culture={0}", culture.Name))

                Assert.AreEqual(ni.PercentSymbol, a1.ToString("%%", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual(ni.PerMilleSymbol, a1.ToString("%‰", culture), String.Format("culture={0}", culture.Name))

                Assert.AreEqual("", a1.ToString("%|", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("", a1.ToString("||", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("", a1.ToString("||||||", culture), String.Format("culture={0}", culture.Name))
                Assert.AreEqual("|", a1.ToString("\|", culture), String.Format("culture={0}", culture.Name))
            Next
        End Sub

        <TestMethod()>
        Public Sub ToString_CustomSimple()
            Dim a1 As New Angle(60, 30, 50.5#)
            Dim a2 As New Angle(-60, 30, 50.5#)
            Dim a3 As New Angle(225)
            Dim culture = InvariantCulture
            Assert.AreEqual(a1.TotalDegrees.ToString(culture), a1.ToString("%D", culture))
            Assert.AreEqual(a1.TotalDegrees.ToString(culture), a1.ToString("%H", culture))
            Assert.AreEqual((-a2.TotalDegrees).ToString(culture), a2.ToString("%D", culture))
            Assert.AreEqual("-" & (-a2.TotalDegrees).ToString(culture), a2.ToString("-D", culture))
            Assert.AreEqual("60", a1.ToString("%d", culture))
            Assert.AreEqual("-60", a1.ToString("-d", culture))
            Assert.AreEqual("60.5", a1.ToString("%D1", culture))
            Assert.AreEqual("60", a1.ToString("%D0", culture))
            Assert.AreEqual("60.51", a1.ToString("DD2", culture))
            Assert.AreEqual("060.5", a1.ToString("DDD1", culture))
            Assert.AreEqual("000,060.5", a1.ToString("DDD,DDD1", culture))
            Assert.AreEqual("000,060.5", a1.ToString(",DDDDDD1", culture))
            Assert.AreEqual("000,060.5", a1.ToString("DDDDDD,1", culture))
            Assert.AreEqual("000,060.5", a1.ToString("DDDDDD1,", culture))

            Assert.AreEqual((a1.ToSlope / 100.0#).ToString(culture), a1.ToString("%e", culture))
            Assert.AreEqual(a1.Rotations.ToString(culture), a1.ToString("%E", culture))

            Assert.AreEqual("5", a1.ToString("%f", culture))

            Assert.AreEqual("12", a1.ToString("%h", culture))

            Assert.AreEqual((a1.ToSlope * 10.0#).ToString(culture), a1.ToString("%l", culture))
            Assert.AreEqual(a1.ToSlope.ToString(culture), a1.ToString("%L", culture))

            Assert.AreEqual("30", a1.ToString("%m", culture))
            Assert.AreEqual(a1.TotalMinutes.ToString(culture), a1.ToString("%M", culture))

            Assert.AreEqual((a1.ToRadians / Math.PI).ToString(culture), a1.ToString("%p", culture))
            Assert.AreEqual((a1.ToRadians / Math.PI).ToString(culture), a1.ToString("%π", culture))
            Assert.AreEqual((a1.ToRadians / Math.PI).ToString(culture), a2.ToString("-π", culture))
            Assert.AreEqual((a1.ToRadians / Math.PI).ToString(culture), a2.ToString("-p", culture))
            Assert.AreEqual(a1.ToRadians.ToString(culture), a1.ToString("%R", culture))
            Assert.AreEqual(a1.ToRadians.ToString(culture), a2.ToString("-R", culture))

            Assert.AreEqual("50", a1.ToString("%s", culture))
            Assert.AreEqual(a1.TotalSeconds.ToString(culture), a1.ToString("%S", culture))

            Assert.AreEqual("2", a1.ToString("%y", culture))
            Assert.AreEqual((a1.TotalDegrees / 24).ToString(culture), a1.ToString("%Y", culture))

            Assert.AreEqual(a1.ToGradians.ToString(culture), a1.ToString("%Z", culture))
            Assert.AreEqual(a2.ToGradians.ToString(culture), a2.ToString("-Z", culture))

            Assert.AreEqual(a1.ToSlope.ToString(culture), a1.ToString("-L", culture))
            Assert.AreEqual(a2.ToSlope.ToString(culture), a2.ToString("-L", culture))

            Assert.AreEqual("-100", a3.ToString("-L", culture))
            Assert.AreEqual("- 100", a3.ToString("- L", culture))
            Assert.AreEqual("-" & vbCrLf & "100", a3.ToString("-" & vbCrLf & "L", culture))
            Assert.AreEqual("100-", a3.ToString("L-", culture))
            Assert.AreEqual("100  -", a3.ToString("L  -", culture))

            Assert.AreEqual("45100", a3.ToString("d-L", culture))
            Assert.AreEqual("100-45", a3.ToString("L-d", culture))
            Assert.AreEqual("45+100", a3.ToString("d+L", culture))
            Assert.AreEqual("100-45", a3.ToString("L+d", culture))

            Assert.AreEqual("45-100", a3.ToString("d|-L", culture))
            Assert.AreEqual("45100", a3.ToString("d-|L", culture))
            Assert.AreEqual("100-45", a3.ToString("L-|d", culture))
            Assert.AreEqual("10045", a3.ToString("L|-d", culture))
            Assert.AreEqual("45+100", a3.ToString("d+|L", culture))
            Assert.AreEqual("45-100", a3.ToString("d|+L", culture))
            Assert.AreEqual("100-45", a3.ToString("L+|d", culture))
            Assert.AreEqual("100-45", a3.ToString("L|+d", culture))

            Assert.AreEqual("1", a3.ToString("e", culture))
            Assert.AreEqual("-1000", a3.ToString("-e", culture))
        End Sub

        <TestMethod()>
        Public Sub ToString_CustomProperties()
            For Each culture In {InvariantCulture, CurrentCulture, GetCultureInfo("en-US"), GetCultureInfo("cs"), GetCultureInfo("ar"), Nothing}
                For Each a In {New Angle(60, 30, 50.5#), New Angle(-60, 30, 50.5#), New Angle(100), New Angle(2000), New Angle(0), New Angle(0, 0, 0.0001#)}
                    Assert.AreEqual((a.TotalDegrees \ 24).ToString("000.000", culture), a.ToString("(Days,000.000)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.Degrees.ToString("000", culture), a.ToString("(Degrees,000)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.ToGradians.ToString("000.###", culture), a.ToString("(Gradians,000.###)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.Degrees - (a.Degrees \ 24) * 24).ToString("0", culture), a.ToString("(Hours,0)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.Minutes.ToString("", culture), a.ToString("(Minutes,)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.ToRadians / Math.PI).ToString("###,###.0#", culture), a.ToString("(PiRadians,###,###.0#)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.ToRadians.ToString("###,###.0#", culture), a.ToString("(Radians,###\,###.0#)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.TotalDegrees - (a.TotalDegrees \ 24) * 24).ToString("f2", culture), a.ToString("(RestHours,f2)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.TotalMinutes - a.Degrees * 60.0#).ToString("e", culture), a.ToString("(RestMinutes,e)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.TotalSeconds - a.TotalMinutes \ 1 * 60.0#).ToString("N", culture), a.ToString("(RestSeconds,N)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.ToRotations.ToString("g2", culture), a.ToString("(Rotations,g2)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.Seconds.ToString(CStr(Nothing), culture), a.ToString("(Seconds)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.ToSlope.ToString(" 0\%", culture), a.ToString("(Slope, 0\\%)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.ToSlope.ToString(" 0\%", culture), a.ToString("(Slope100 , 0\\%)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.ToSlope / 100.0#).ToString("p", culture), a.ToString("(Slope1,p)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.ToSlope / 100.0#).ToString("0.00%", culture), a.ToString("(Slop\e1,0.00%)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.ToSlope / 100.0#).ToString("0.0‰", culture), a.ToString("(Slope1,0.0‰)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.ToSlope * 10.0#).ToString("0.0\‰", culture), a.ToString("(Slope1000, 0.0\\‰)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(CType(a, TimeSpan).ToString("", culture), a.ToString("(Time,)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(CType(a, TimeSpanFormattable).ToString("[h]:mm:ss", culture), a.ToString("(TimeFormattable,[h]:mm:ss)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual((a.TotalDegrees / 24.0#).ToString("g", culture), a.ToString("(TotalDays,g)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.TotalDegrees.ToString("0.00", culture), a.ToString("(TotalDegrees,0.00)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.TotalDegrees.ToString("0,00", culture), a.ToString("(TotalDegrees,0,00)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.TotalMinutes.ToString("0.00m", culture), a.ToString("(TotalMinutes,0.00)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.TotalSeconds.ToString("0.00m", culture), a.ToString("(TotalSeconds,0.00)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                    Assert.AreEqual(a.Degrees.ToString("x", culture), a.ToString("(Degrees,x)", culture), String.Format("culture={0}, a={1}", culture.Name, a))
                Next
            Next
        End Sub

        <TestMethod>
        Public Sub ToString_Standard()
            Assert.Inconclusive("TODO")
        End Sub

        <TestMethod>
        Public Sub AngleFormatInfoResources()
            Dim rm As New ResourceManager("Tools.GlobalizationT.AngleFormatInfo", GetType(AngleFormatInfo).Assembly)
            Dim cs = GetCultureInfo("cs")
            Dim en = GetCultureInfo("en")
            Dim csCZ = GetCultureInfo("cs-CZ")
            Dim enUS = GetCultureInfo("en-US")
            Assert.AreNotEqual(rm.GetString("LatitudeNorthLongSymbol", en), rm.GetString("LatitudeNorthLongSymbol", cs))
            Assert.AreNotEqual(rm.GetString("LatitudeNorthLongSymbol", enUS), rm.GetString("LatitudeNorthLongSymbol", csCZ))
            Assert.AreNotEqual(rm.GetString("LatitudeNorthLongSymbol", en), rm.GetString("LatitudeNorthLongSymbol", csCZ))
            Assert.AreNotEqual(rm.GetString("LatitudeNorthLongSymbol", enUS), rm.GetString("LatitudeNorthLongSymbol", cs))
            Assert.AreEqual(rm.GetString("LatitudeNorthLongSymbol", cs), rm.GetString("LatitudeNorthLongSymbol", csCZ))

            Assert.AreNotEqual(rm.GetString("LongitudeEastShortSymbol", en), rm.GetString("LongitudeEastShortSymbol", cs))
            Assert.AreNotEqual(rm.GetString("LongitudeEastShortSymbol", enUS), rm.GetString("LongitudeEastShortSymbol", csCZ))
            Assert.AreNotEqual(rm.GetString("LongitudeEastShortSymbol", en), rm.GetString("LongitudeEastShortSymbol", csCZ))
            Assert.AreNotEqual(rm.GetString("LongitudeEastShortSymbol", enUS), rm.GetString("LongitudeEastShortSymbol", cs))
            Assert.AreEqual(rm.GetString("LongitudeEastShortSymbol", cs), rm.GetString("LongitudeEastShortSymbol", csCZ))
        End Sub

        <TestMethod>
        Public Sub AngleFormatInfoLocalizedValues()
            Dim ccs = GetCultureInfo("cs")
            Dim cen = GetCultureInfo("en")
            Dim ccsCZ = GetCultureInfo("cs-CZ")
            Dim cenUS = GetCultureInfo("en-US")
            Dim cs = AngleFormatInfo.Get(ccs)
            Dim csCZ = AngleFormatInfo.Get(ccsCZ)
            Dim en = AngleFormatInfo.Get(cen)
            Dim enUS = AngleFormatInfo.Get(cenUS)

            Assert.AreNotEqual(en.LongitudeEastLongSymbol, cs.LongitudeEastLongSymbol)
            Assert.AreNotEqual(enUS.LongitudeEastLongSymbol, csCZ.LongitudeEastLongSymbol)
            Assert.AreNotEqual(en.LongitudeEastLongSymbol, csCZ.LongitudeEastLongSymbol)
            Assert.AreNotEqual(enUS.LongitudeEastLongSymbol, cs.LongitudeEastLongSymbol)
            Assert.AreEqual(cs.LongitudeEastLongSymbol, csCZ.LongitudeEastLongSymbol)


            Assert.AreNotEqual(en.LongitudeWestShortSymbol, cs.LongitudeWestShortSymbol)
            Assert.AreNotEqual(enUS.LongitudeWestShortSymbol, csCZ.LongitudeWestShortSymbol)
            Assert.AreNotEqual(en.LongitudeWestShortSymbol, csCZ.LongitudeWestShortSymbol)
            Assert.AreNotEqual(enUS.LongitudeWestShortSymbol, cs.LongitudeWestShortSymbol)
            Assert.AreEqual(cs.LongitudeWestShortSymbol, csCZ.LongitudeWestShortSymbol)
        End Sub
    End Class
End Namespace