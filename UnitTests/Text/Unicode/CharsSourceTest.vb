Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.TextT.UnicodeT

Namespace TextUT.UnicodeUT

    <TestClass()>
    Public Class CharsSourceTest

        <TestMethod()>
        Public Sub VirtualFirstLastIndexTest()

            Dim array = {
                New With {.i = 1, .First = 0UI, .Last = 15UI, .FVI = 0L, .LVI = 15L, .Columns = 16, .Lines = 1},
                New With {.i = 2, .First = 0UI, .Last = 31UI, .FVI = 0L, .LVI = 31L, .Columns = 16, .Lines = 2},
                New With {.i = 3, .First = 1UI, .Last = 31UI, .FVI = -1L, .LVI = 30L, .Columns = 16, .Lines = 2},
                New With {.i = 4, .First = 1UI, .Last = 30UI, .FVI = -1L, .LVI = 30L, .Columns = 16, .Lines = 2},
                New With {.i = 5, .First = 1UI, .Last = 15UI, .FVI = -1L, .LVI = 14L, .Columns = 16, .Lines = 1},
                New With {.i = 6, .First = 1UI, .Last = 14UI, .FVI = -1L, .LVI = 14L, .Columns = 16, .Lines = 1},
                New With {.i = 7, .First = 31UI, .Last = 128UI, .FVI = -15L, .LVI = 112L, .Columns = 16, .Lines = 8},
                New With {.i = 8, .First = &H10FF80UI, .Last = &H10FFFFUI, .FVI = 0L, .LVI = 127L, .Columns = 16, .Lines = 8},
                New With {.i = 9, .First = 0UI, .Last = &H10FFFFUI, .FVI = 0L, .LVI = &H10FFFFL, .Columns = 16, .Lines = 69632},
                New With {.i = 10, .First = 1UI, .Last = &H10FFFDUI, .FVI = -1L, .LVI = &H10FFFFL - 1L, .Columns = 16, .Lines = 69632},
                New With {.i = 11, .First = 71UI, .last = 71UI, .FVI = -7L, .LVI = 8L, .Columns = 16, .Lines = 1},
                New With {.i = 12, .first = 0UI, .last = UnicodeCharacterDatabase.MaxCodePoint, .FVI = 0L, .lvi = CLng(UnicodeCharacterDatabase.MaxCodePoint), .columns = 16, .lines = CInt((UnicodeCharacterDatabase.MaxCodePoint + 1) / 16)}
            }

            For Each item In array
                With item
                    Dim range As New CharsRange(.First, .Last)
                    Assert.AreEqual(.First, range(0))
                    Assert.AreEqual(.Last, range(range.Count - 1))
                    Assert.AreEqual(CInt(.Last - .First + 1), range.Count)
                    Assert.AreEqual(True, range.Continuous)

                    Dim source As New CharsSource(range)
                    If .Columns <> 16 Then source.Columns = .Columns
                    Assert.AreEqual(.Columns, source.Columns)
                    Assert.AreEqual(True, source.Continuous)
                    Assert.AreEqual(.First, source.FirstChar)
                    Assert.AreEqual(.Last, source.LastChar)
                    Assert.AreEqual(.FVI, source.VirtualFirstIndex)
                    Assert.AreEqual(.LVI, source.VirtualLastIndex)
                    Assert.AreEqual(.Lines, source.Count)
                End With
            Next

        End Sub
    End Class
    <TestClass()>
    Public Class CharsLineTest

        <TestMethod()>
        Public Sub GetEnumeratorTest()

            'In this configuration index of char = codepoint value
            Dim range As New CharsRange(0, UnicodeCharacterDatabase.MaxCodePoint)
            Dim source As New CharsSource(range)
            Assert.AreEqual(16, source.Columns)
            Assert.IsTrue(source.Continuous)
            Dim chi As UInteger = 0
            Dim li As Integer = 0

            For Each l In source
                Assert.AreEqual(li, l.Index)
                Assert.AreEqual(CLng(chi), l.FirstIndex)

                For Each ch In l
                    Assert.AreEqual(chi, ch.Value)
                    chi += 1
                    If chi > 1000 Then Exit For
                Next
                If chi > 1000 Then Exit For
                li += 1
            Next

        End Sub
    End Class
End Namespace
