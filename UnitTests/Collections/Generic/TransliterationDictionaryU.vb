Imports RFERL.Core.Data
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Collections.Generic

Namespace CollectionsUT.GenericUT


    ''' <summary>
    '''This is a test class for TransliterationDictionaryTest and is intended
    '''to contain all TransliterationDictionaryTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class TransliterationDictionaryTest


        Private testContextInstance As TestContext

        ''' <summary>
        '''Gets or sets the test context which provides
        '''information about and functionality for the current test run.
        '''</summary>
        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = value
            End Set
        End Property


        ''' <summary>
        '''A test for IsNameCharacter
        '''</summary>
        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub IsNameCharacterTest()
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter(":"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("A"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("Z"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("a"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("z"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("_"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("Ø"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("‌"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("⁰"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("�"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameCharacter("\"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("ˇ"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameCharacter("*"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameCharacter("["c))

            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("-"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("."c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("0"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("5"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("·"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("́"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameCharacter("⁀"c))
        End Sub

        ''' <summary>
        '''A test for IsNameStartCharacter
        '''</summary>
        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub IsNameStartCharacterTest()
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter(":"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("A"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("Z"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("a"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("z"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("_"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("Ø"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("‌"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("⁰"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("�"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("\"c))
            Assert.IsTrue(TransliterationDictionary_Accessor.IsNameStartCharacter("ˇ"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("*"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("["c))

            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("-"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("."c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("0"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("5"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("·"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("́"c))
            Assert.IsFalse(TransliterationDictionary_Accessor.IsNameStartCharacter("⁀"c))
        End Sub

        Private sampleText As String = "Lorem ipsum dolor sit amet, consectetur adipisicing elit" & vbCr & vbLf & "<a href='http://www.lipsum.com/'>Lorem ipsum</a>, <a--a> <a__a> <_ffFфгοδ:Γσdň /> <ĀՏՋేమ갿/> <a attr='aaa'/> <a attr=""bbb""/> <abc attr='adfad' >" & vbCr & vbLf & "<?pi:pi?> <?pi pi?> <!--comment--> <!----> <?pi a?a??> <!-----> <!--a-a--a--a----b---------> <![CDATA[]]> <![CDATA <abra**> <abra a=*> <<<a> <abra c=78> <button disabled />" & vbCr & vbLf & "<![CDATA[text in CData]]> <![CDATA[ aa ]] aa ]] aa ]]]]]]> život chleba ščot qido hugo jugoslavie <!-"
        Private sampleTextTranslit As String = "Лорэм ипсум долор сит амэт, цонсэцтэтур адиписицинг элит" & vbCr & vbLf & "<a href='http://www.lipsum.com/'>Лорэм ипсум</a>, <a--a> <a__a> <_ffFфгοδ:Γσdň /> <ĀՏՋేమ갿/> <a attr='aaa'/> <a attr=""bbb""/> <abc attr='adfad' >" & vbCr & vbLf & "<?pi:pi?> <?pi pi?> <!--comment--> <!----> <?pi a?a??> <!-----> <!--a-a--a--a----b---------> <![CDATA[]]> <![ЦДАТА <абра**> <абра а=*> <<<a> <абра ц=78> <буттон дисаблэд />" & vbCr & vbLf & "<![CDATA[тэкст ин ЦДата]]> <![CDATA[ аа ]] аа ]] аа ]]]]]]> живот хлэба щот квидо гуго югославиэ <!-"

        <TestMethod()> _
        Public Sub Transliterate_NoChangeNoXml()
            Dim dic = New TransliterationDictionary(New KeyValuePair(Of String, String)() {})
            Assert.AreEqual(sampleText, dic.Transliterate(sampleText, False))
        End Sub

        <TestMethod()> _
        Public Sub Transliterate_NoChageXml()
            Dim dic = New TransliterationDictionary(New KeyValuePair(Of String, String)() {})

            Assert.AreEqual(sampleText, dic.Transliterate(sampleText, True))
            Assert.AreEqual("<tag> text", dic.Transliterate("<tag> text", True))
            Assert.AreEqual("<?pi?> text", dic.Transliterate("<?pi?> text", True))
            Assert.AreEqual("&amp; text", dic.Transliterate("&amp; text", True))
            Assert.AreEqual("a <tag attr=""", dic.Transliterate("a <tag attr=""", True))
            Assert.AreEqual("a <?pi ? ? ?>", dic.Transliterate("a <?pi ? ? ?>", True))
            Assert.AreEqual("a <?pi ? ???>", dic.Transliterate("a <?pi ? ???>", True))
            Assert.AreEqual("a <? pi ?", dic.Transliterate("a <? pi ?", True))
            Assert.AreEqual("a <? pi", dic.Transliterate("a <? pi", True))
            Assert.AreEqual("a <!-- - -->", dic.Transliterate("a <!-- - -->", True))
            Assert.AreEqual("a <!-- -- -->", dic.Transliterate("a <!-- -- -->", True))
            Assert.AreEqual("a <!-- ---->", dic.Transliterate("a <!-- ---->", True))
            Assert.AreEqual("a <!-- a", dic.Transliterate("a <!-- a", True))
            Assert.AreEqual("a <!-- a-", dic.Transliterate("a <!-- a-", True))
            Assert.AreEqual("a <!-- a--", dic.Transliterate("a <!-- a--", True))
            Assert.AreEqual("a <![CDAT", dic.Transliterate("a <![CDAT", True))
            Assert.AreEqual("a <", dic.Transliterate("a <", True))
            Assert.AreEqual("a <!", dic.Transliterate("a <!", True))
            Assert.AreEqual("a <?", dic.Transliterate("a <?", True))
            Assert.AreEqual("a <![", dic.Transliterate("a <![", True))
            Assert.AreEqual("a <![CDATA[aaa", dic.Transliterate("a <![CDATA[aaa", True))
            Assert.AreEqual("a <![CDATA[aaa]", dic.Transliterate("a <![CDATA[aaa]", True))
            Assert.AreEqual("a <![CDATA[aaa]]", dic.Transliterate("a <![CDATA[aaa]]", True))
            Assert.AreEqual("a <![CDATA[aaa]xxx]]>", dic.Transliterate("a <![CDATA[aaa]xxx]]>", True))
            Assert.AreEqual("a <![CDATA[aaa]]xxx]]>", dic.Transliterate("a <![CDATA[aaa]]xxx]]>", True))
            Assert.AreEqual("a <![CDATA[aaa]]]>", dic.Transliterate("a <![CDATA[aaa]]]>", True))
            Assert.AreEqual("a &", dic.Transliterate("a &", True))
            Assert.AreEqual("a &a", dic.Transliterate("a &a", True))
            Assert.AreEqual("a &#", dic.Transliterate("a &#", True))
            Assert.AreEqual("a &#04", dic.Transliterate("a &#04", True))
            Assert.AreEqual("a &#x", dic.Transliterate("a &#x", True))
            Assert.AreEqual("a &#xD", dic.Transliterate("a &#xD", True))
        End Sub

        <TestMethod()> _
        Public Sub Transliterate_SimpleCyrillicXml()
			Dim dic = New TransliterationDictionary(New Dictionary(Of String, String)() With { _
				{"a", "а"}, _
				{"A", "А"}, _
				{"b", "б"}, _
				{"B", "Б"}, _
				{"c", "ц"}, _
				{"C", "Ц"}, _
				{"č", "ч"}, _
				{"Č", "Ч"}, _
				{"d", "д"}, _
				{"D", "Д"}, _
				{"e", "э"}, _
				{"E", "Э"}, _
				{"ě", "е"}, _
				{"Ě", "Е"}, _
				{"f", "ф"}, _
				{"F", "Ф"}, _
				{"g", "г"}, _
				{"G", "Г"}, _
				{"h", "г"}, _
				{"H", "Г"}, _
				{"i", "и"}, _
				{"I", "И"}, _
				{"j", "й"}, _
				{"J", "Й"}, _
				{"k", "к"}, _
				{"K", "К"}, _
				{"l", "л"}, _
				{"L", "Л"}, _
				{"ľ", "љ"}, _
				{"Ľ", "Љ"}, _
				{"lj", "љ"}, _
				{"Lj", "Љ"}, _
				{"LJ", "Љ"}, _
				{"m", "м"}, _
				{"M", "М"}, _
				{"n", "н"}, _
				{"N", "Н"}, _
				{"ň", "њ"}, _
				{"Ň", "Њ"}, _
				{"nj", "њ"}, _
				{"Nj", "Њ"}, _
				{"NJ", "Њ"}, _
				{"o", "о"}, _
				{"O", "О"}, _
				{"p", "п"}, _
				{"P", "П"}, _
				{"q", "кв"}, _
				{"Q", "Кв"}, _
				{"r", "р"}, _
				{"R", "Р"}, _
				{"s", "с"}, _
				{"S", "С"}, _
				{"š", "ш"}, _
				{"Š", "Ш"}, _
				{"šč", "щ"}, _
				{"Šč", "Щ"}, _
				{"ŠČ", "Щ"}, _
				{"t", "т"}, _
				{"T", "Т"}, _
				{"u", "у"}, _
				{"U", "У"}, _
				{"v", "в"}, _
				{"V", "В"}, _
				{"w", "в"}, _
				{"W", "В"}, _
				{"x", "кс"}, _
				{"X", "Кс"}, _
				{"y", "ы"}, _
				{"Y", "Ы"}, _
				{"z", "з"}, _
				{"Z", "З"}, _
				{"ž", "ж"}, _
				{"Ž", "Ж"}, _
				{"ch", "х"}, _
				{"Ch", "Х"}, _
				{"CH", "Х"}, _
				{"ju", "ю"}, _
				{"Ju", "Ю"}, _
				{"JU", "Ю"}, _
				{"ja", "я"}, _
				{"Ja", "Я"}, _
				{"JA", "Я"} _
			})
            Assert.AreEqual("абракадабрака", dic.Transliterate("abrakadabraka", True))
            Assert.AreEqual("АБРАКАДАБРАКА", dic.Transliterate("ABRAKADABRAKA", True))
            Assert.AreEqual(sampleTextTranslit, dic.Transliterate(sampleText, True))

            Assert.AreEqual("<tag> тэкст", dic.Transliterate("<tag> text", True))
            Assert.AreEqual("<?pi?> тэкст", dic.Transliterate("<?pi?> text", True))
            Assert.AreEqual("&amp; тэкст", dic.Transliterate("&amp; text", True))
            Assert.AreEqual("а <таг аттр=""", dic.Transliterate("a <tag attr=""", True))
            Assert.AreEqual("а <?pi ? ? ?>", dic.Transliterate("a <?pi ? ? ?>", True))
            Assert.AreEqual("а <?pi ? ???>", dic.Transliterate("a <?pi ? ???>", True))
            Assert.AreEqual("а <? пи ?", dic.Transliterate("a <? pi ?", True))
            Assert.AreEqual("а <? пи", dic.Transliterate("a <? pi", True))
            Assert.AreEqual("а <!-- - -->", dic.Transliterate("a <!-- - -->", True))
            Assert.AreEqual("а <!-- -- -->", dic.Transliterate("a <!-- -- -->", True))
            Assert.AreEqual("а <!-- ---->", dic.Transliterate("a <!-- ---->", True))
            Assert.AreEqual("а <!-- а", dic.Transliterate("a <!-- a", True))
            Assert.AreEqual("а <!-- а-", dic.Transliterate("a <!-- a-", True))
            Assert.AreEqual("а <!-- а--", dic.Transliterate("a <!-- a--", True))
            Assert.AreEqual("а <![ЦДАТ", dic.Transliterate("a <![CDAT", True))
            Assert.AreEqual("а <", dic.Transliterate("a <", True))
            Assert.AreEqual("а <!", dic.Transliterate("a <!", True))
            Assert.AreEqual("а <?", dic.Transliterate("a <?", True))
            Assert.AreEqual("а <![", dic.Transliterate("a <![", True))
            Assert.AreEqual("а <![CDATA[ааа", dic.Transliterate("a <![CDATA[aaa", True))
            Assert.AreEqual("а <![CDATA[ааа]", dic.Transliterate("a <![CDATA[aaa]", True))
            Assert.AreEqual("а <![CDATA[ааа]]", dic.Transliterate("a <![CDATA[aaa]]", True))
            Assert.AreEqual("а <![CDATA[ааа]кскскс]]>", dic.Transliterate("a <![CDATA[aaa]xxx]]>", True))
            Assert.AreEqual("а <![CDATA[ааа]]кскскс]]>", dic.Transliterate("a <![CDATA[aaa]]xxx]]>", True))
            Assert.AreEqual("а <![CDATA[ааа]]]>", dic.Transliterate("a <![CDATA[aaa]]]>", True))
            Assert.AreEqual("а &", dic.Transliterate("a &", True))
            Assert.AreEqual("а &а", dic.Transliterate("a &a", True))
            Assert.AreEqual("а &#", dic.Transliterate("a &#", True))
            Assert.AreEqual("а &#04", dic.Transliterate("a &#04", True))
            Assert.AreEqual("а &#кс", dic.Transliterate("a &#x", True))
            Assert.AreEqual("а &#ксД", dic.Transliterate("a &#xD", True))
            Assert.AreEqual("эндс витг й", dic.Transliterate("ends with j", True))
        End Sub

        ''' <summary>A test for <see cref="TransliterationDictionary.GetAllStartingWith"/><summary>
        <TestMethod()> _
        Public Sub GetAllStartingWithTest()
			Dim dic As New TransliterationDictionary(New Dictionary(Of String, String)() With { _
				{"j", "й"}, _
				{"ja", "я"}, _
				{"ju", "ю"}, _
				{"je", "е"}, _
				{"jukos", "юкос"}, _
				{"k", "K"}, _
				{"kus", "KUS"}, _
				{"kl", "KL"}, _
				{"krk", "KRK"}, _
				{"krkovice", "KRKOVICE"}, _
				{"kretén", "KRETÉN"}, _
				{"kráva", "KRÁVA"}, _
				{"ž", "ж"}, _
				{"a", "а"} _
			})
            Dim js = dic.GetAllStartingWith("j")
            Assert.AreEqual(5, js.Length)
            Assert.IsTrue(Array.IndexOf(js, New KeyValuePair(Of String, String)("j", "й")) >= 0)
            Assert.IsTrue(Array.IndexOf(js, New KeyValuePair(Of String, String)("ja", "я")) >= 0)
            Assert.IsTrue(Array.IndexOf(js, New KeyValuePair(Of String, String)("ju", "ю")) >= 0)
            Assert.IsTrue(Array.IndexOf(js, New KeyValuePair(Of String, String)("je", "е")) >= 0)
            Assert.IsTrue(Array.IndexOf(js, New KeyValuePair(Of String, String)("jukos", "юкос")) >= 0)
            Dim ks = dic.GetAllStartingWith("k")
            Assert.AreEqual(7, ks.Length)
            Assert.IsTrue(Array.IndexOf(ks, New KeyValuePair(Of String, String)("k", "K")) >= 0)
            Assert.IsTrue(Array.IndexOf(ks, New KeyValuePair(Of String, String)("kus", "KUS")) >= 0)
            Assert.IsTrue(Array.IndexOf(ks, New KeyValuePair(Of String, String)("kl", "KL")) >= 0)
            Assert.IsTrue(Array.IndexOf(ks, New KeyValuePair(Of String, String)("krk", "KRK")) >= 0)
            Assert.IsTrue(Array.IndexOf(ks, New KeyValuePair(Of String, String)("krkovice", "KRKOVICE")) >= 0)
            Assert.IsTrue(Array.IndexOf(ks, New KeyValuePair(Of String, String)("kretén", "KRETÉN")) >= 0)
            Assert.IsTrue(Array.IndexOf(ks, New KeyValuePair(Of String, String)("kráva", "KRÁVA")) >= 0)
            Dim krs = dic.GetAllStartingWith("kr")
            Assert.AreEqual(4, krs.Length)
            Assert.IsTrue(Array.IndexOf(krs, New KeyValuePair(Of String, String)("krk", "KRK")) >= 0)
            Assert.IsTrue(Array.IndexOf(krs, New KeyValuePair(Of String, String)("krkovice", "KRKOVICE")) >= 0)
            Assert.IsTrue(Array.IndexOf(krs, New KeyValuePair(Of String, String)("kretén", "KRETÉN")) >= 0)
            Assert.IsTrue(Array.IndexOf(krs, New KeyValuePair(Of String, String)("kráva", "KRÁVA")) >= 0)
        End Sub

        ''' <summary>A test for <see cref="TransliterationDictionary.FirstIndexOfPrefix"/></summary>
        <TestMethod()> _
        Public Sub FirstIndexOfPrefixTest()
			Dim dic As New TransliterationDictionary(New Dictionary(Of String, String)() With { _
				{"j", "й"}, _
				{"ja", "я"}, _
				{"ju", "ю"}, _
				{"je", "е"}, _
				{"jukos", "юкос"}, _
				{"k", "K"}, _
				{"kus", "KUS"}, _
				{"kl", "KL"}, _
				{"krk", "KRK"}, _
				{"krkovice", "KRKOVICE"}, _
				{"kretén", "KRETÉN"}, _
				{"kráva", "KRÁVA"}, _
				{"ž", "ж"}, _
				{"a", "а"} _
			})
            Assert.AreEqual(dic.FirstIndexOf("j"), dic.FirstIndexOfPrefix("j"))
            Assert.AreEqual(dic.FirstIndexOf("k"), dic.FirstIndexOfPrefix("k"))
            Assert.AreEqual(dic.FirstIndexOf("kretén"), dic.FirstIndexOfPrefix("kr"))
            Assert.AreEqual(dic.FirstIndexOf("ja"), dic.FirstIndexOfPrefix("ja"))
        End Sub

        ''' <summary>A test for <see cref="TransliterationDictionary.LastIndexOfPrefix"/><summary>
        <TestMethod()> _
        Public Sub LastIndexOfPrefixTest()
			Dim dic As New TransliterationDictionary(New Dictionary(Of String, String)() With { _
				{"j", "й"}, _
				{"ja", "я"}, _
				{"ju", "ю"}, _
				{"je", "е"}, _
				{"jukos", "юкос"}, _
				{"k", "K"}, _
				{"kus", "KUS"}, _
				{"kl", "KL"}, _
				{"krk", "KRK"}, _
				{"krkovice", "KRKOVICE"}, _
				{"kretén", "KRETÉN"}, _
				{"kráva", "KRÁVA"}, _
				{"ž", "ж"}, _
				{"a", "а"} _
			})
            Assert.AreEqual(dic.FirstIndexOf("jukos"), dic.LastIndexOfPrefix("j"))
            Assert.AreEqual(dic.FirstIndexOf("kus"), dic.LastIndexOfPrefix("k"))
            Assert.AreEqual(dic.FirstIndexOf("kráva"), dic.LastIndexOfPrefix("kr"))
            Assert.AreEqual(dic.FirstIndexOf("ja"), dic.LastIndexOfPrefix("ja"))
        End Sub
    End Class
End Namespace
