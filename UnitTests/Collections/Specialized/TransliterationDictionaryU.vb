Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Collections.Generic
Imports Tools.CollectionsT.SpecializedT

Namespace CollectionsUT.GenericUT


    ''' <summary>
    '''This is a test class for TransliterationDictionaryTest and is intended
    '''to contain all TransliterationDictionaryTest Unit Tests
    '''</summary>
    <TestClass()>
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
        <TestMethod()>
        <DeploymentItem("RFERL.Core.dll")>
        Public Sub IsNameCharacterTest()
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter(":"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("A"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("Z"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("a"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("z"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("_"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("Ø"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("‌"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("⁰"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("�"c))
            Assert.IsFalse(TransliterationDictionary.IsNameCharacter("\"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("ˇ"c))
            Assert.IsFalse(TransliterationDictionary.IsNameCharacter("*"c))
            Assert.IsFalse(TransliterationDictionary.IsNameCharacter("["c))

            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("-"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("."c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("0"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("5"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("·"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("́"c))
            Assert.IsTrue(TransliterationDictionary.IsNameCharacter("⁀"c))
        End Sub

        ''' <summary>
        '''A test for IsNameStartCharacter
        '''</summary>
        <TestMethod()>
        <DeploymentItem("RFERL.Core.dll")>
        Public Sub IsNameStartCharacterTest()
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter(":"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("A"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("Z"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("a"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("z"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("_"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("Ø"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("‌"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("⁰"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("�"c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("\"c))
            Assert.IsTrue(TransliterationDictionary.IsNameStartCharacter("ˇ"c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("*"c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("["c))

            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("-"c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("."c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("0"c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("5"c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("·"c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("́"c))
            Assert.IsFalse(TransliterationDictionary.IsNameStartCharacter("⁀"c))
        End Sub

        Private sampleText As String = "Lorem ipsum dolor sit amet, consectetur adipisicing elit" & vbCr & vbLf & "<a href='http://www.lipsum.com/'>Lorem ipsum</a>, <a--a> <a__a> <_ffFфгοδ:Γσdň /> <ĀՏՋేమ갿/> <a attr='aaa'/> <a attr=""bbb""/> <abc attr='adfad' >" & vbCr & vbLf & "<?pi:pi?> <?pi pi?> <!--comment--> <!----> <?pi a?a??> <!-----> <!--a-a--a--a----b---------> <![CDATA[]]> <![CDATA <abra**> <abra a=*> <<<a> <abra c=78> <button disabled />" & vbCr & vbLf & "<![CDATA[text in CData]]> <![CDATA[ aa ]] aa ]] aa ]]]]]]> život chleba ščot qido hugo jugoslavie <!-"
        Private sampleTextTranslit As String = "Лорэм ипсум долор сит амэт, цонсэцтэтур адиписицинг элит" & vbCr & vbLf & "<a href='http://www.lipsum.com/'>Лорэм ипсум</a>, <a--a> <a__a> <_ffFфгοδ:Γσdň /> <ĀՏՋేమ갿/> <a attr='aaa'/> <a attr=""bbb""/> <abc attr='adfad' >" & vbCr & vbLf & "<?pi:pi?> <?pi pi?> <!--comment--> <!----> <?pi a?a??> <!-----> <!--a-a--a--a----b---------> <![CDATA[]]> <![ЦДАТА <абра**> <абра а=*> <<<a> <абра ц=78> <буттон дисаблэд />" & vbCr & vbLf & "<![CDATA[тэкст ин ЦДата]]> <![CDATA[ аа ]] аа ]] аа ]]]]]]> живот хлэба щот квидо гуго югославиэ <!-"

        <TestMethod()>
        Public Sub Transliterate_NoChangeNoXml()
            Dim dic = New TransliterationDictionary(New KeyValuePair(Of String, String)() {})
            Assert.AreEqual(sampleText, dic.Transliterate(sampleText, False))
        End Sub

        <TestMethod()>
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

        <TestMethod()>
        Public Sub Transliterate_SimpleCyrillicXml()
            Dim dic = New TransliterationDictionary(New Dictionary(Of String, String)() From {
                {"a", "а"},
                {"A", "А"},
                {"b", "б"},
                {"B", "Б"},
                {"c", "ц"},
                {"C", "Ц"},
                {"č", "ч"},
                {"Č", "Ч"},
                {"d", "д"},
                {"D", "Д"},
                {"e", "э"},
                {"E", "Э"},
                {"ě", "е"},
                {"Ě", "Е"},
                {"f", "ф"},
                {"F", "Ф"},
                {"g", "г"},
                {"G", "Г"},
                {"h", "г"},
                {"H", "Г"},
                {"i", "и"},
                {"I", "И"},
                {"j", "й"},
                {"J", "Й"},
                {"k", "к"},
                {"K", "К"},
                {"l", "л"},
                {"L", "Л"},
                {"ľ", "љ"},
                {"Ľ", "Љ"},
                {"lj", "љ"},
                {"Lj", "Љ"},
                {"LJ", "Љ"},
                {"m", "м"},
                {"M", "М"},
                {"n", "н"},
                {"N", "Н"},
                {"ň", "њ"},
                {"Ň", "Њ"},
                {"nj", "њ"},
                {"Nj", "Њ"},
                {"NJ", "Њ"},
                {"o", "о"},
                {"O", "О"},
                {"p", "п"},
                {"P", "П"},
                {"q", "кв"},
                {"Q", "Кв"},
                {"r", "р"},
                {"R", "Р"},
                {"s", "с"},
                {"S", "С"},
                {"š", "ш"},
                {"Š", "Ш"},
                {"šč", "щ"},
                {"Šč", "Щ"},
                {"ŠČ", "Щ"},
                {"t", "т"},
                {"T", "Т"},
                {"u", "у"},
                {"U", "У"},
                {"v", "в"},
                {"V", "В"},
                {"w", "в"},
                {"W", "В"},
                {"x", "кс"},
                {"X", "Кс"},
                {"y", "ы"},
                {"Y", "Ы"},
                {"z", "з"},
                {"Z", "З"},
                {"ž", "ж"},
                {"Ž", "Ж"},
                {"ch", "х"},
                {"Ch", "Х"},
                {"CH", "Х"},
                {"ju", "ю"},
                {"Ju", "Ю"},
                {"JU", "Ю"},
                {"ja", "я"},
                {"Ja", "Я"},
                {"JA", "Я"}
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
        <TestMethod()>
        Public Sub GetAllStartingWithTest()
            Dim dic As New TransliterationDictionary(New Dictionary(Of String, String)() From {
                {"j", "й"},
                {"ja", "я"},
                {"ju", "ю"},
                {"je", "е"},
                {"jukos", "юкос"},
                {"k", "K"},
                {"kus", "KUS"},
                {"kl", "KL"},
                {"krk", "KRK"},
                {"krkovice", "KRKOVICE"},
                {"kretén", "KRETÉN"},
                {"kráva", "KRÁVA"},
                {"ž", "ж"},
                {"a", "а"}
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
        <TestMethod()>
        Public Sub FirstIndexOfPrefixTest()
            Dim dic As New TransliterationDictionary(New Dictionary(Of String, String)() From {
                {"j", "й"},
                {"ja", "я"},
                {"ju", "ю"},
                {"je", "е"},
                {"jukos", "юкос"},
                {"k", "K"},
                {"kus", "KUS"},
                {"kl", "KL"},
                {"krk", "KRK"},
                {"krkovice", "KRKOVICE"},
                {"kretén", "KRETÉN"},
                {"kráva", "KRÁVA"},
                {"ž", "ж"},
                {"a", "а"}
            })
            Assert.AreEqual(dic.FirstIndexOf("j"), dic.FirstIndexOfPrefix("j"))
            Assert.AreEqual(dic.FirstIndexOf("k"), dic.FirstIndexOfPrefix("k"))
            Assert.AreEqual(dic.FirstIndexOf("kretén"), dic.FirstIndexOfPrefix("kr"))
            Assert.AreEqual(dic.FirstIndexOf("ja"), dic.FirstIndexOfPrefix("ja"))
        End Sub

        ''' <summary>A test for <see cref="TransliterationDictionary.LastIndexOfPrefix"/><summary>
        <TestMethod()>
        Public Sub LastIndexOfPrefixTest()
            Dim dic As New TransliterationDictionary(New Dictionary(Of String, String)() From {
                {"j", "й"},
                {"ja", "я"},
                {"ju", "ю"},
                {"je", "е"},
                {"jukos", "юкос"},
                {"k", "K"},
                {"kus", "KUS"},
                {"kl", "KL"},
                {"krk", "KRK"},
                {"krkovice", "KRKOVICE"},
                {"kretén", "KRETÉN"},
                {"kráva", "KRÁVA"},
                {"ž", "ж"},
                {"a", "а"}
            })
            Assert.AreEqual(dic.FirstIndexOf("jukos"), dic.LastIndexOfPrefix("j"))
            Assert.AreEqual(dic.FirstIndexOf("kus"), dic.LastIndexOfPrefix("k"))
            Assert.AreEqual(dic.FirstIndexOf("kráva"), dic.LastIndexOfPrefix("kr"))
            Assert.AreEqual(dic.FirstIndexOf("ja"), dic.LastIndexOfPrefix("ja"))
        End Sub
    End Class
End Namespace
