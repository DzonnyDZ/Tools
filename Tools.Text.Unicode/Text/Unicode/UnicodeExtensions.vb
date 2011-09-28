Imports System.Runtime.CompilerServices
Imports System.Globalization
Imports System.Xml.Serialization

Namespace TextT.UnicodeT
    ''' <summary>Contains Unicode-related extension methods</summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module UnicodeExtensions
        ''' <summary>Gets generalized Unicode category given general category belongs to</summary>
        ''' <param name="category">A Unicode general category to gete generalized category for</param>
        ''' <returns>A <see cref="UnicodeGeneralCategoryClass"/> indicating type of <paramref name="category"/>.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of <see cref="UnicodeCategory"/> values</exception>
        <Extension()>
        Public Function GetClass(category As UnicodeCategory) As UnicodeGeneralCategoryClass
            Select Case category
                Case UnicodeCategory.LowercaseLetter, UnicodeCategory.UppercaseLetter, UnicodeCategory.TitlecaseLetter, UnicodeCategory.ModifierLetter, UnicodeCategory.OtherLetter
                    Return UnicodeGeneralCategoryClass.Letter
                Case UnicodeCategory.NonSpacingMark, UnicodeCategory.SpacingCombiningMark, UnicodeCategory.EnclosingMark
                    Return UnicodeGeneralCategoryClass.Mark
                Case UnicodeCategory.DecimalDigitNumber, UnicodeCategory.LetterNumber, UnicodeCategory.OtherNumber
                    Return UnicodeGeneralCategoryClass.Number
                Case UnicodeCategory.ConnectorPunctuation, UnicodeCategory.DashPunctuation, UnicodeCategory.OpenPunctuation, UnicodeCategory.ClosePunctuation, UnicodeCategory.InitialQuotePunctuation, UnicodeCategory.FinalQuotePunctuation, UnicodeCategory.OtherPunctuation
                    Return UnicodeGeneralCategoryClass.Punctuation
                Case UnicodeCategory.MathSymbol, UnicodeCategory.CurrencySymbol, UnicodeCategory.ModifierSymbol, UnicodeCategory.OtherSymbol
                    Return UnicodeGeneralCategoryClass.Symbol
                Case UnicodeCategory.SpaceSeparator, UnicodeCategory.LineSeparator, UnicodeCategory.ParagraphSeparator
                    Return UnicodeGeneralCategoryClass.Separator
                Case UnicodeCategory.Control, UnicodeCategory.Format, UnicodeCategory.Surrogate, UnicodeCategory.PrivateUse, UnicodeCategory.OtherNotAssigned
                    Return UnicodeGeneralCategoryClass.Other
                Case Else
                    Throw New InvalidEnumArgumentException("category", category, category.GetType)
            End Select
        End Function

        ''' <summary>Gets strength of bidirectional class</summary>
        ''' <param name="bidClass">A bidirectional class to get strength of</param>
        ''' <returns>Strength of bidirectional class <paramref name="bidClass"/> as indicated by 2nd-least significant byte (2nd LSB) of that number.</returns>
        <Extension()>
        Public Function GetStrength(bidClass As UnicodeBidiCategory) As UnicodeBidiCategoryStrenght
            Return bidClass And (UnicodeBidiCategoryStrenght.Neutral Or UnicodeBidiCategoryStrenght.Strong Or UnicodeBidiCategoryStrenght.Weak)
        End Function

        ''' <summary>Gets origin of Unicode joining group</summary>
        ''' <param name="group">A Unicode joining group to get origin of</param>
        ''' <returns>Value indicating origin of Unicode joining group <paramref name="group"/></returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="group"/> is not one of <see cref="UnicodeJoiningGroup"/> values</exception>
        <Extension()>
        Public Function Origin(group As UnicodeJoiningGroup) As UnicodeJoiningGroupOrigin
            Select Case group
                Case UnicodeJoiningGroup.none : Return UnicodeJoiningGroupOrigin.none
                Case UnicodeJoiningGroup.Beh To UnicodeJoiningGroup.Lam : Return UnicodeJoiningGroupOrigin.ArabicDual
                Case UnicodeJoiningGroup.Alef To UnicodeJoiningGroup.YehBarree : Return UnicodeJoiningGroupOrigin.ArabicRight
                Case UnicodeJoiningGroup.Beth To UnicodeJoiningGroup.Shin : Return UnicodeJoiningGroupOrigin.SyriacDual
                Case UnicodeJoiningGroup.Dalath To UnicodeJoiningGroup.Taw : Return UnicodeJoiningGroupOrigin.SyriacRight
                Case UnicodeJoiningGroup.Alaph : Return UnicodeJoiningGroupOrigin.SyriacOther
                Case Else : Throw New InvalidEnumArgumentException("group", group, group.GetType)
            End Select
        End Function
    End Module

End Namespace