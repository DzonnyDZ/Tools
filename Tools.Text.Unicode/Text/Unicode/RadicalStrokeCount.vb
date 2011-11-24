Imports Tools.ComponentModelT, Tools.ExtensionsT
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Globalization.CultureInfo
Imports System.Xml.Serialization
Imports System.Xml.Schema
Imports System.Xml.Linq

Namespace TextT.UnicodeT

    ''' <summary>Represents CJK Radical/Stroke count</summary>
    ''' <seelaso cref="CjkRadical"/>
    ''' <version version="1.5.4">This structure is new in version 1.5.4</version>
    <Serializable()>
    Public Structure RadicalStrokeCount
        Implements IXmlSerializable
        ''' <summary>CTor - creates a new instance of the <see cref="RadicalStrokeCount"/> structure</summary>
        ''' <param name="radical">The radical to counts strokes additional to</param>
        ''' <param name="additionalStrokes">Number of additional strokes</param>
        ''' <param name="simplifiedRadical">Indicates that simplified radical was used</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="additionalStrokes"/> is less than zero.</exception>
        Public Sub New(radical As CjkRadical, additionalStrokes%, Optional simplifiedRadical As Boolean = False)
            If additionalStrokes < 0 Then Throw New ArgumentOutOfRangeException("additionalStrokes")
            _radical = radical
            _additionalStrokes = additionalStrokes
            _simplifiedRadical = simplifiedRadical
        End Sub
        Private _radical As CjkRadical
        Private _additionalStrokes%
        Private _simplifiedRadical As Boolean
        ''' <summary>Gets tha CJK radical this instance counts additional strokes for</summary>
        Public ReadOnly Property Radical() As CjkRadical
            Get
                Return _radical
            End Get
        End Property

        ''' <summary>Gets number of additional strokes (drawn in addition to <see cref="Radical"/>) that are necessray to draw a character</summary>
        Public ReadOnly Property AdditionalStrokes%
            Get
                Return _additionalStrokes
            End Get
        End Property

        ''' <summary>Gets value indicating that simplified radical was used.</summary>
        ''' <remarks>Only used for simplified radicals</remarks>
        Public ReadOnly Property SimplifiedRadical As Boolean
            Get
                Return _simplifiedRadical
            End Get
        End Property

        ''' <summary>Parses string to <see cref="RadicalStrokeCount"/> object</summary>
        ''' <param name="value">
        ''' A string to parse. Ir should be in format {<see cref="Radical"/>}.{<see cref="AdditionalStrokes"/>}.
        ''' The Radical part allows trailing apostrophe (') to indicate simplified radical.
        ''' </param>
        ''' <returns>A <see cref="RadicalStrokeCount"/> object initialized from <paramref name="value"/>.</returns>
        ''' <remarks>The radical part of <paramref name="value"/> can be represented as enumerated value, integer or CJK character</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="value"/> does not contain 2 dot(.)-separated parts -or- AdditionalStrokes part of <paramref name="value"/> cannot be parsed as <see cref="Integer"/></exception>
        ''' <exception cref="ArgumentException">Radical part of <paramref name="value"/> does not represent a value that can be converted to <see cref="CjkRadical"/> value using the <see cref="LookupRadical"/> function.</exception>
        ''' <exception cref="ArgumentOutOfRangeException">AdditionalStrokes part of <paramref name="value"/> represents negative number</exception>
        ''' <exception cref="OverflowException">AdditionalStrokes part of <paramref name="value"/> is too big or too small for datatype <see cref="Integer"/>.</exception>
        Public Shared Function Parse(value As String) As RadicalStrokeCount
            If value Is Nothing Then Throw New ArgumentNullException("value")
            Dim parts = value.Split("."c)
            If parts.Length <> 2 Then Throw New FormatException("Two dot-separated parts expected.")

            Dim radicalString = parts(0)
            Dim simplified As Boolean = False
            If radicalString.EndsWith("'") Then
                simplified = True
                radicalString = radicalString.Substring(0, radicalString.Length - 1)
            End If

            Dim radical? As CjkRadical = LookupRadical(radicalString)
            If radical Is Nothing Then Throw New ArgumentException(String.Format("{0} is not known CJK radical.", radicalString, "value"))

            Return New RadicalStrokeCount(radical, Integer.Parse(parts(1), InvariantCulture), simplified)
        End Function

        ''' <summary>Attempts to converts string value to CJK Radical</summary>
        ''' <param name="value">A value that represents a CJK radical. It can be name from the <see cref="CjkRadical"/> enumeration, integer value or a Unicode character representing Kang Xi or Unicode supplemental radical.</param>
        ''' <returns>A <see cref="CjkRadical"/> enumeration value representing a CJK radical detected from <paramref name="value"/>. Null if radical cannot be detected.</returns>
        ''' <remarks>
        ''' If <paramref name="value"/> is name of <see cref="CjkRadical"/> enumeration member or it represents iteger number it's converted to <see cref="CjkRadical"/> enumeration value using <see cref="[Enum].TryParse"/>.
        ''' If <paramref name="value"/> is single character and the character represents either Kang Xi radical (including variants) or supplemental Unicode radical corresponding <see cref="CjkRadical"/> value is returned.
        ''' <para>This function is only meaningfull if you use Kang Xi and/or Unicdoe supplement radicals.</para>
        ''' </remarks>
        ''' <seelaso cref="RadicalToChar"/>
        Public Shared Function LookupRadical(value As String) As CjkRadical?
            Dim radical As CjkRadical
            If [Enum].TryParse(value, True, radical) Then Return radical

            If value.Length = 1 Then
                Select Case value(0)
                    'Kang Xi
                    Case "一"c : Return 1
                    Case "丨"c : Return 2
                    Case "丶"c : Return 3
                    Case "丿"c, "乀"c, "乁"c : Return 4
                    Case "乙"c, "乚"c, "乛"c : Return 5
                    Case "亅"c : Return 6
                    Case "二"c : Return 7
                    Case "亠"c : Return 8
                    Case "人"c, "亻"c : Return 9
                    Case "儿"c : Return 10
                    Case "入"c : Return 11
                    Case "八"c, "丷"c : Return 12
                    Case "冂"c : Return 13
                    Case "冖"c : Return 14
                    Case "冫"c : Return 15
                    Case "几"c : Return 16
                    Case "凵"c : Return 17
                    Case "刀"c, "刂"c : Return 18
                    Case "力"c : Return 19
                    Case "勹"c : Return 20
                    Case "匕"c : Return 21
                    Case "匚"c : Return 22
                    Case "匸"c : Return 23
                    Case "十"c : Return 24
                    Case "卜"c : Return 25
                    Case "卩"c : Return 26
                    Case "厂"c : Return 27
                    Case "厶"c : Return 28
                    Case "又"c : Return 29
                    Case "口"c : Return 30
                    Case "囗"c : Return 31
                    Case "土"c : Return 32
                    Case "士"c : Return 33
                    Case "夂"c : Return 34
                    Case "夊"c : Return 35
                    Case "夕"c : Return 36
                    Case "大"c : Return 37
                    Case "女"c : Return 38
                    Case "子"c : Return 39
                    Case "宀"c : Return 40
                    Case "寸"c : Return 41
                    Case "小"c : Return 42
                    Case "尢"c, "尣"c : Return 43
                    Case "尸"c : Return 44
                    Case "屮"c : Return 45
                    Case "山"c : Return 46
                    Case "川"c, "巛"c, "巜"c : Return 47
                    Case "工"c : Return 48
                    Case "己"c : Return 49
                    Case "巾"c : Return 50
                    Case "干"c : Return 51
                    Case "幺"c : Return 52
                    Case "广"c : Return 53
                    Case "廴"c : Return 54
                    Case "廾"c : Return 55
                    Case "弋"c : Return 56
                    Case "弓"c : Return 57
                    Case "彐"c, "彑"c : Return 58
                    Case "彡"c : Return 59
                    Case "彳"c : Return 60
                    Case "心"c, "忄"c : Return 61
                    Case "戈"c : Return 62
                    Case "户"c : Return 63
                    Case "手"c, "扌"c : Return 64
                    Case "支"c : Return 65
                    Case "攴"c : Return 66
                    Case "攵"c : Return 67
                    Case "斗"c : Return 68
                    Case "斤"c : Return 69
                    Case "方"c : Return 70
                    Case "无"c : Return 71
                    Case "日"c : Return 72
                    Case "曰"c : Return 73
                    Case "月"c : Return 74
                    Case "木"c : Return 75
                    Case "欠"c : Return 76
                    Case "止"c : Return 77
                    Case "歹"c : Return 78
                    Case "殳"c : Return 79
                    Case "毋"c : Return 80
                    Case "比"c : Return 81
                    Case "毛"c : Return 82
                    Case "氏"c : Return 83
                    Case "气"c : Return 84
                    Case "水"c, "氵"c : Return 85
                    Case "火"c, "灬"c : Return 86
                    Case "爪"c, "爫"c : Return 87
                    Case "父"c : Return 88
                    Case "爻"c : Return 89
                    Case "爿"c : Return 90
                    Case "片"c : Return 91
                    Case "牙"c : Return 92
                    Case "牛"c, "牜"c : Return 93
                    Case "犭"c, "犬"c : Return 94
                    Case "玄"c : Return 95
                    Case "玉"c, "王"c : Return 96
                    Case "瓜"c : Return 97
                    Case "瓦"c : Return 98
                    Case "甘"c : Return 99
                    Case "生"c : Return 100
                    Case "用"c : Return 101
                    Case "田"c : Return 102
                    Case "疋"c : Return 103
                    Case "疒"c : Return 104
                    Case "癶"c : Return 105
                    Case "白"c : Return 106
                    Case "皮"c : Return 107
                    Case "皿"c : Return 108
                    Case "目"c : Return 109
                    Case "矛"c : Return 110
                    Case "矢"c : Return 111
                    Case "石"c : Return 112
                    Case "示"c, "礻"c : Return 113
                    Case "禸"c : Return 114
                    Case "禾"c : Return 115
                    Case "穴"c : Return 116
                    Case "立"c : Return 117
                    Case "竹"c : Return 118
                    Case "米"c : Return 119
                    Case "纟"c : Return 120
                    Case "缶"c : Return 121
                    Case "网"c, "罒"c : Return 122
                    Case "羊"c : Return 123
                    Case "羽"c : Return 124
                    Case "老"c : Return 125
                    Case "而"c : Return 126
                    Case "耒"c : Return 127
                    Case "耳"c : Return 128
                    Case "聿"c : Return 129
                    Case "肉"c : Return 130
                    Case "臣"c : Return 131
                    Case "自"c : Return 132
                    Case "至"c : Return 133
                    Case "臼"c : Return 134
                    Case "舌"c : Return 135
                    Case "舛"c : Return 136
                    Case "舟"c : Return 137
                    Case "艮"c : Return 138
                    Case "色"c : Return 139
                    Case "艹"c : Return 140
                    Case "虍"c : Return 141
                    Case "虫"c : Return 142
                    Case "血"c : Return 143
                    Case "行"c : Return 144
                    Case "衣"c, "衤"c : Return 145
                    Case "西"c, "覀"c : Return 146
                    Case "见"c : Return 147
                    Case "角"c : Return 148
                    Case "讠"c : Return 149
                    Case "谷"c : Return 150
                    Case "豆"c : Return 151
                    Case "豖"c : Return 152
                    Case "豸"c : Return 153
                    Case "贝"c : Return 154
                    Case "赤"c : Return 155
                    Case "走"c : Return 156
                    Case "足"c : Return 157
                    Case "身"c : Return 158
                    Case "车"c : Return 159
                    Case "辛"c : Return 160
                    Case "辰"c : Return 161
                    Case "辶"c : Return 162
                    Case "邑"c, "阝"c : Return 163
                    Case "酉"c : Return 164
                    Case "釆"c : Return 165
                    Case "里"c : Return 166
                    Case "钅"c, "金"c : Return 167
                    Case "长"c : Return 168
                    Case "门"c : Return 169
                    Case "阜"c, "阝"c : Return 170
                    Case "隶"c : Return 171
                    Case "隹"c : Return 172
                    Case "雨"c : Return 173
                    Case "青"c : Return 174
                    Case "非"c : Return 175
                    Case "面"c : Return 176
                    Case "革"c : Return 177
                    Case "韦"c : Return 178
                    Case "韭"c : Return 179
                    Case "音"c : Return 180
                    Case "页"c : Return 181
                    Case "风"c : Return 182
                    Case "飞"c : Return 183
                    Case "饣"c, "飠"c, "食"c : Return 184
                    Case "首"c : Return 185
                    Case "香"c : Return 186
                    Case "马"c : Return 187
                    Case "骨"c : Return 188
                    Case "高"c : Return 189
                    Case "髟"c : Return 190
                    Case "鬥"c : Return 191
                    Case "鬯"c : Return 192
                    Case "鬲"c : Return 193
                    Case "鬼"c : Return 194
                    Case "鱼"c : Return 195
                    Case "鸟"c : Return 196
                    Case "卤"c : Return 197
                    Case "鹿"c : Return 198
                    Case "麦"c : Return 199
                    Case "麻"c : Return 200
                    Case "黃"c : Return 201
                    Case "黍"c : Return 202
                    Case "黑"c : Return 203
                    Case "黹"c : Return 204
                    Case "黾"c : Return 205
                    Case "鼎"c : Return 206
                    Case "鼓"c : Return 207
                    Case "鼠"c : Return 208
                    Case "鼻"c : Return 209
                    Case "齐"c : Return 210
                    Case "齿"c : Return 211
                    Case "龙"c : Return 212
                    Case "龟"c : Return 213
                    Case "龠"c : Return 214

                        'Unicode Supplement:
                    Case "⻋"c : Return 289
                    Case "⻌"c : Return 290
                    Case "⻍"c : Return 291
                    Case "⻎"c : Return 292
                    Case "⻏"c : Return 293
                    Case "⻐"c : Return 294
                    Case "⻑"c : Return 295
                    Case "⻒"c : Return 296
                    Case "⻓"c : Return 297
                    Case "⻔"c : Return 298
                    Case "⻕"c : Return 299
                    Case "⻖"c : Return 300
                    Case "⻗"c : Return 301
                    Case "⻘"c : Return 302
                    Case "⻙"c : Return 303
                    Case "⻚"c : Return 304
                    Case "⻛"c : Return 305
                    Case "⻜"c : Return 306
                    Case "⻝"c : Return 307
                    Case "⻞"c : Return 308
                    Case "⻟"c : Return 309
                    Case "⻠"c : Return 310
                    Case "⻡"c : Return 311
                    Case "⻢"c : Return 312
                    Case "⻣"c : Return 313
                    Case "⻤"c : Return 314
                    Case "⻥"c : Return 315
                    Case "⻦"c : Return 316
                    Case "⻧"c : Return 317
                    Case "⻨"c : Return 318
                    Case "⻩"c : Return 319
                    Case "⻪"c : Return 320
                    Case "⻫"c : Return 321
                    Case "⻬"c : Return 322
                    Case "⻭"c : Return 323
                    Case "⻮"c : Return 324
                    Case "⻯"c : Return 325
                    Case "⻰"c : Return 326
                    Case "⻱"c : Return 327
                    Case "⻲"c : Return 328
                    Case "⻳"c : Return 329
                End Select
            End If
            Return Nothing
        End Function

        ''' <summary>Gets a character that represents given CJK (Kang Xi or Unicode supplemental) radical</summary>
        ''' <param name="radical">A radical</param>
        ''' <returns>A character that represents the radical</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="radical"/> is not one of known radicals.</exception>
        ''' <remarks>This function is only meaningfull if you use Kang Xi and/or Unicdoe supplement radicals.</remarks>
        ''' <seelaso cref="LookupRadical"/>
        Public Shared Function RadicalToChar(radical As CjkRadical) As Char
            Select Case radical
                'Kang Xi
                Case 1 : Return "一"c
                Case 2 : Return "丨"c
                Case 3 : Return "丶"c
                Case 4 : Return "丿"c
                Case 5 : Return "乙"c
                Case 6 : Return "亅"c
                Case 7 : Return "二"c
                Case 8 : Return "亠"c
                Case 9 : Return "人"c
                Case 10 : Return "儿"c
                Case 11 : Return "入"c
                Case 12 : Return "八"c
                Case 13 : Return "冂"c
                Case 14 : Return "冖"c
                Case 15 : Return "冫"c
                Case 16 : Return "几"c
                Case 17 : Return "凵"c
                Case 18 : Return "刀"c
                Case 19 : Return "力"c
                Case 20 : Return "勹"c
                Case 21 : Return "匕"c
                Case 22 : Return "匚"c
                Case 23 : Return "匸"c
                Case 24 : Return "十"c
                Case 25 : Return "卜"c
                Case 26 : Return "卩"c
                Case 27 : Return "厂"c
                Case 28 : Return "厶"c
                Case 29 : Return "又"c
                Case 30 : Return "口"c
                Case 31 : Return "囗"c
                Case 32 : Return "土"c
                Case 33 : Return "士"c
                Case 34 : Return "夂"c
                Case 35 : Return "夊"c
                Case 36 : Return "夕"c
                Case 37 : Return "大"c
                Case 38 : Return "女"c
                Case 39 : Return "子"c
                Case 40 : Return "宀"c
                Case 41 : Return "寸"c
                Case 42 : Return "小"c
                Case 43 : Return "尢"c
                Case 44 : Return "尸"c
                Case 45 : Return "屮"c
                Case 46 : Return "山"c
                Case 47 : Return "川"c
                Case 48 : Return "工"c
                Case 49 : Return "己"c
                Case 50 : Return "巾"c
                Case 51 : Return "干"c
                Case 52 : Return "幺"c
                Case 53 : Return "广"c
                Case 54 : Return "廴"c
                Case 55 : Return "廾"c
                Case 56 : Return "弋"c
                Case 57 : Return "弓"c
                Case 58 : Return "彐"c
                Case 59 : Return "彡"c
                Case 60 : Return "彳"c
                Case 61 : Return "心"c
                Case 62 : Return "戈"c
                Case 63 : Return "户"c
                Case 64 : Return "手"c
                Case 65 : Return "支"c
                Case 66 : Return "攴"c
                Case 67 : Return "攵"c
                Case 68 : Return "斗"c
                Case 69 : Return "斤"c
                Case 70 : Return "方"c
                Case 71 : Return "无"c
                Case 72 : Return "日"c
                Case 73 : Return "曰"c
                Case 74 : Return "月"c
                Case 75 : Return "木"c
                Case 76 : Return "欠"c
                Case 77 : Return "止"c
                Case 78 : Return "歹"c
                Case 79 : Return "殳"c
                Case 80 : Return "毋"c
                Case 81 : Return "比"c
                Case 82 : Return "毛"c
                Case 83 : Return "氏"c
                Case 84 : Return "气"c
                Case 85 : Return "水"c
                Case 86 : Return "火"c
                Case 87 : Return "爪"c
                Case 88 : Return "父"c
                Case 89 : Return "爻"c
                Case 90 : Return "爿"c
                Case 91 : Return "片"c
                Case 92 : Return "牙"c
                Case 93 : Return "牛"c
                Case 94 : Return "犭"c
                Case 95 : Return "玄"c
                Case 96 : Return "玉"c
                Case 97 : Return "瓜"c
                Case 98 : Return "瓦"c
                Case 99 : Return "甘"c
                Case 100 : Return "生"c
                Case 101 : Return "用"c
                Case 102 : Return "田"c
                Case 103 : Return "疋"c
                Case 104 : Return "疒"c
                Case 105 : Return "癶"c
                Case 106 : Return "白"c
                Case 107 : Return "皮"c
                Case 108 : Return "皿"c
                Case 109 : Return "目"c
                Case 110 : Return "矛"c
                Case 111 : Return "矢"c
                Case 112 : Return "石"c
                Case 113 : Return "示"c
                Case 114 : Return "禸"c
                Case 115 : Return "禾"c
                Case 116 : Return "穴"c
                Case 117 : Return "立"c
                Case 118 : Return "竹"c
                Case 119 : Return "米"c
                Case 120 : Return "纟"c
                Case 121 : Return "缶"c
                Case 122 : Return "网"c
                Case 123 : Return "羊"c
                Case 124 : Return "羽"c
                Case 125 : Return "老"c
                Case 126 : Return "而"c
                Case 127 : Return "耒"c
                Case 128 : Return "耳"c
                Case 129 : Return "聿"c
                Case 130 : Return "肉"c
                Case 131 : Return "臣"c
                Case 132 : Return "自"c
                Case 133 : Return "至"c
                Case 134 : Return "臼"c
                Case 135 : Return "舌"c
                Case 136 : Return "舛"c
                Case 137 : Return "舟"c
                Case 138 : Return "艮"c
                Case 139 : Return "色"c
                Case 140 : Return "艹"c
                Case 141 : Return "虍"c
                Case 142 : Return "虫"c
                Case 143 : Return "血"c
                Case 144 : Return "行"c
                Case 145 : Return "衣"c
                Case 146 : Return "西"c
                Case 147 : Return "见"c
                Case 148 : Return "角"c
                Case 149 : Return "讠"c
                Case 150 : Return "谷"c
                Case 151 : Return "豆"c
                Case 152 : Return "豖"c
                Case 153 : Return "豸"c
                Case 154 : Return "贝"c
                Case 155 : Return "赤"c
                Case 156 : Return "走"c
                Case 157 : Return "足"c
                Case 158 : Return "身"c
                Case 159 : Return "车"c
                Case 160 : Return "辛"c
                Case 161 : Return "辰"c
                Case 162 : Return "辶"c
                Case 163 : Return "邑"c
                Case 164 : Return "酉"c
                Case 165 : Return "釆"c
                Case 166 : Return "里"c
                Case 167 : Return "钅"c
                Case 168 : Return "长"c
                Case 169 : Return "门"c
                Case 170 : Return "阜"c
                Case 171 : Return "隶"c
                Case 172 : Return "隹"c
                Case 173 : Return "雨"c
                Case 174 : Return "青"c
                Case 175 : Return "非"c
                Case 176 : Return "面"c
                Case 177 : Return "革"c
                Case 178 : Return "韦"c
                Case 179 : Return "韭"c
                Case 180 : Return "音"c
                Case 181 : Return "页"c
                Case 182 : Return "风"c
                Case 183 : Return "飞"c
                Case 184 : Return "饣"c
                Case 185 : Return "首"c
                Case 186 : Return "香"c
                Case 187 : Return "马"c
                Case 188 : Return "骨"c
                Case 189 : Return "高"c
                Case 190 : Return "髟"c
                Case 191 : Return "鬥"c
                Case 192 : Return "鬯"c
                Case 193 : Return "鬲"c
                Case 194 : Return "鬼"c
                Case 195 : Return "鱼"c
                Case 196 : Return "鸟"c
                Case 197 : Return "卤"c
                Case 198 : Return "鹿"c
                Case 199 : Return "麦"c
                Case 200 : Return "麻"c
                Case 201 : Return "黃"c
                Case 202 : Return "黍"c
                Case 203 : Return "黑"c
                Case 204 : Return "黹"c
                Case 205 : Return "黾"c
                Case 206 : Return "鼎"c
                Case 207 : Return "鼓"c
                Case 208 : Return "鼠"c
                Case 209 : Return "鼻"c
                Case 210 : Return "齐"c
                Case 211 : Return "齿"c
                Case 212 : Return "龙"c
                Case 213 : Return "龟"c
                Case 214 : Return "龠"c

                    'Unicode supplement
                Case 215 : Return "⺀"c
                Case 216 : Return "⺁"c
                Case 217 : Return "⺂"c
                Case 218 : Return "⺃"c
                Case 219 : Return "⺄"c
                Case 220 : Return "⺅"c
                Case 221 : Return "⺆"c
                Case 222 : Return "⺇"c
                Case 223 : Return "⺈"c
                Case 224 : Return "⺉"c
                Case 225 : Return "⺊"c
                Case 226 : Return "⺋"c
                Case 227 : Return "⺌"c
                Case 228 : Return "⺍"c
                Case 229 : Return "⺎"c
                Case 230 : Return "⺏"c
                Case 231 : Return "⺐"c
                Case 232 : Return "⺑"c
                Case 233 : Return "⺒"c
                Case 234 : Return "⺓"c
                Case 235 : Return "⺔"c
                Case 236 : Return "⺕"c
                Case 237 : Return "⺖"c
                Case 238 : Return "⺗"c
                Case 239 : Return "⺘"c
                Case 240 : Return "⺙"c
                Case 241 : Return "⺛"c
                Case 242 : Return "⺜"c
                Case 243 : Return "⺝"c
                Case 244 : Return "⺞"c
                Case 245 : Return "⺟"c
                Case 246 : Return "⺠"c
                Case 247 : Return "⺡"c
                Case 248 : Return "⺢"c
                Case 249 : Return "⺣"c
                Case 250 : Return "⺤"c
                Case 251 : Return "⺥"c
                Case 252 : Return "⺦"c
                Case 253 : Return "⺧"c
                Case 254 : Return "⺨"c
                Case 255 : Return "⺩"c
                Case 256 : Return "⺪"c
                Case 257 : Return "⺫"c
                Case 258 : Return "⺬"c
                Case 259 : Return "⺭"c
                Case 260 : Return "⺮"c
                Case 261 : Return "⺯"c
                Case 262 : Return "⺰"c
                Case 263 : Return "⺱"c
                Case 264 : Return "⺲"c
                Case 265 : Return "⺳"c
                Case 266 : Return "⺴"c
                Case 267 : Return "⺵"c
                Case 268 : Return "⺶"c
                Case 269 : Return "⺷"c
                Case 270 : Return "⺸"c
                Case 271 : Return "⺹"c
                Case 272 : Return "⺺"c
                Case 273 : Return "⺻"c
                Case 274 : Return "⺼"c
                Case 275 : Return "⺽"c
                Case 276 : Return "⺾"c
                Case 277 : Return "⺿"c
                Case 278 : Return "⻀"c
                Case 279 : Return "⻁"c
                Case 280 : Return "⻂"c
                Case 281 : Return "⻃"c
                Case 282 : Return "⻄"c
                Case 283 : Return "⻅"c
                Case 284 : Return "⻆"c
                Case 285 : Return "⻇"c
                Case 286 : Return "⻈"c
                Case 287 : Return "⻉"c
                Case 288 : Return "⻊"c
                Case 289 : Return "⻋"c
                Case 290 : Return "⻌"c
                Case 291 : Return "⻍"c
                Case 292 : Return "⻎"c
                Case 293 : Return "⻏"c
                Case 294 : Return "⻐"c
                Case 295 : Return "⻑"c
                Case 296 : Return "⻒"c
                Case 297 : Return "⻓"c
                Case 298 : Return "⻔"c
                Case 299 : Return "⻕"c
                Case 300 : Return "⻖"c
                Case 301 : Return "⻗"c
                Case 302 : Return "⻘"c
                Case 303 : Return "⻙"c
                Case 304 : Return "⻚"c
                Case 305 : Return "⻛"c
                Case 306 : Return "⻜"c
                Case 307 : Return "⻝"c
                Case 308 : Return "⻞"c
                Case 309 : Return "⻟"c
                Case 310 : Return "⻠"c
                Case 311 : Return "⻡"c
                Case 312 : Return "⻢"c
                Case 313 : Return "⻣"c
                Case 314 : Return "⻤"c
                Case 315 : Return "⻥"c
                Case 316 : Return "⻦"c
                Case 317 : Return "⻧"c
                Case 318 : Return "⻨"c
                Case 319 : Return "⻩"c
                Case 320 : Return "⻪"c
                Case 321 : Return "⻫"c
                Case 322 : Return "⻬"c
                Case 323 : Return "⻭"c
                Case 324 : Return "⻮"c
                Case 325 : Return "⻯"c
                Case 326 : Return "⻰"c
                Case 327 : Return "⻱"c
                Case 328 : Return "⻲"c
                Case 329 : Return "⻳"c

                Case Else : Throw New InvalidEnumArgumentException("radical", radical, radical.GetType)
            End Select
        End Function

        ''' <summary>Returns null</summary>
        ''' <returns>Null</returns>
        Private Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
            Return Nothing
        End Function

        ''' <summary>Generates an object from its XML representation.</summary>
        ''' <param name="reader">The <see cref="System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        Private Sub ReadXml(reader As System.Xml.XmlReader) Implements IXmlSerializable.ReadXml
            Dim value = Parse(reader.Value)
            _radical = value.Radical
            _additionalStrokes = value.AdditionalStrokes
            _simplifiedRadical = value.SimplifiedRadical
            reader.Read()
        End Sub

        ''' <summary>Converts an object into its XML representation.</summary>
        ''' <param name="writer">The <see cref="System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        Private Sub WriteXml(writer As System.Xml.XmlWriter) Implements IXmlSerializable.WriteXml
            writer.WriteValue(String.Format("{0:d}{1}.{2}", Radical, If(SimplifiedRadical, "'", ""), AdditionalStrokes))
        End Sub

        ''' <summary>Gets string representation of this instance</summary>
        ''' <returns>String representation of this instance in format {<see cref="Radical"/>}.{<see cref="AdditionalStrokes"/>}. <see cref="Radical"/> is rendered as integer.</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0:d}{1}.{2}", Radical, If(SimplifiedRadical, "'", ""), AdditionalStrokes)
        End Function
    End Structure

    ''' <summary>Enumeration of CJK Ideographs' radicals</summary>
    ''' <remarks>
    ''' A radical is part (logical or graphical) of a CJK ideograph that's used to define it and sort it in dictionary.
    ''' <note>
    ''' This enumeration is based on Kang Xi (康熙字典) dictionary with additional Unicode radicals.
    ''' If actual implementation uses different dictionary names of enumeration members are irelevant. Only what matters is numerical value.
    ''' </note>
    ''' <note>
    ''' You should not rely on names and display names of members of this enumeration as they are subject to change.
    ''' Only thing that  really matters is numerical value.
    ''' To get normative data regarding CJK Radicals see <see cref="UnicodeCharacterDatabase.CjkRadicals"/>.
    ''' </note>
    ''' To convert between <see cref="CjkRadical"/> values and actual characters representing the radicals look for <see cref="RadicalStrokeCount.LookupRadical"/> and <see cref="RadicalStrokeCount.RadicalToChar"/> functions.
    ''' </remarks>
    ''' <seelaso cref="RadicalStrokeCount"/>
    ''' <seelaso cref="RadicalStrokeCount.LookupRadical"/>
    ''' <seelaso cref="RadicalStrokeCount.RadicalToChar"/>
    ''' <seelaso cref="UnicodeCharacterDatabase.CjkRadicals"/>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum CjkRadical
#Region "Kang Xi"
        ''' <summary>Kang Xi Radical № 1 一 (yī; one)</summary>
        <FieldDisplayName("yī")>
        [One] = 1
        ''' <summary>Kang Xi Radical № 2 丨 (shù; line)</summary>
        <FieldDisplayName("shù")>
        [Line] = 2
        ''' <summary>Kang Xi Radical № 3 丶 (diǎn; dot)</summary>
        <FieldDisplayName("diǎn")>
        [Dot] = 3
        ''' <summary>Kang Xi Radical № 4 丿 (piě; slash)</summary>
        ''' <remarks>Variants are: 乀 乁</remarks>
        <FieldDisplayName("piě")>
        [Slash] = 4
        ''' <summary>Kang Xi Radical № 5 乙 (yǐ; second)</summary>
        ''' <remarks>Variants are: 乚 乛</remarks>
        <FieldDisplayName("yǐ")>
        [Second] = 5
        ''' <summary>Kang Xi Radical № 6 亅 (gōu; hook)</summary>
        <FieldDisplayName("gōu")>
        [Hook] = 6
        ''' <summary>Kang Xi Radical № 7 二 (èr; two)</summary>
        <FieldDisplayName("èr")>
        [Two] = 7
        ''' <summary>Kang Xi Radical № 8 亠 (tóu; lid)</summary>
        <FieldDisplayName("tóu")>
        [Lid] = 8
        ''' <summary>Kang Xi Radical № 9 人 (rén; person)</summary>
        ''' <remarks>Variants are: 亻</remarks>
        <FieldDisplayName("rén")>
        [Person] = 9
        ''' <summary>Kang Xi Radical № 10 儿 (ér; legs)</summary>
        <FieldDisplayName("ér")>
        [Legs] = 10
        ''' <summary>Kang Xi Radical № 11 入 (rù; enter)</summary>
        <FieldDisplayName("rù")>
        [Enter] = 11
        ''' <summary>Kang Xi Radical № 12 八 (bā; eight)</summary>
        ''' <remarks>Variants are: 丷</remarks>
        <FieldDisplayName("bā")>
        [Eight] = 12
        ''' <summary>Kang Xi Radical № 13 冂 (jiǒng; down box)</summary>
        <FieldDisplayName("jiǒng")>
        [DownBox] = 13
        ''' <summary>Kang Xi Radical № 14 冖 (mì; cover)</summary>
        <FieldDisplayName("mì")>
        [Cover] = 14
        ''' <summary>Kang Xi Radical № 15 冫 (bīng; ice)</summary>
        <FieldDisplayName("bīng")>
        [Ice] = 15
        ''' <summary>Kang Xi Radical № 16 几 (jī; table)</summary>
        <FieldDisplayName("jī")>
        [Table] = 16
        ''' <summary>Kang Xi Radical № 17 凵 (qǔ; open box)</summary>
        <FieldDisplayName("qǔ")>
        [OpenBox] = 17
        ''' <summary>Kang Xi Radical № 18 刀 (dāo; knife)</summary>
        ''' <remarks>Variants are: 刂</remarks>
        <FieldDisplayName("dāo")>
        [Knife] = 18
        ''' <summary>Kang Xi Radical № 19 力 (lì; power)</summary>
        <FieldDisplayName("lì")>
        [Power] = 19
        ''' <summary>Kang Xi Radical № 20 勹 (bāo; wrap)</summary>
        <FieldDisplayName("bāo")>
        [Wrap] = 20
        ''' <summary>Kang Xi Radical № 21 匕 (bǐ; ladle)</summary>
        <FieldDisplayName("bǐ")>
        [Ladle] = 21
        ''' <summary>Kang Xi Radical № 22 匚 (fāng; right open box)</summary>
        <FieldDisplayName("fāng")>
        [RightOpenBox] = 22
        ''' <summary>Kang Xi Radical № 23 匸 (xǐ; hiding enclosure)</summary>
        <FieldDisplayName("xǐ")>
        [HidingEnclosure] = 23
        ''' <summary>Kang Xi Radical № 24 十 (shí; ten)</summary>
        <FieldDisplayName("shí")>
        [Ten] = 24
        ''' <summary>Kang Xi Radical № 25 卜 (bǔ; divination)</summary>
        <FieldDisplayName("bǔ")>
        [Divination] = 25
        ''' <summary>Kang Xi Radical № 26 卩 (jié; seal)</summary>
        <FieldDisplayName("jié")>
        [Seal] = 26
        ''' <summary>Kang Xi Radical № 27 厂 (hàn; cliff)</summary>
        <FieldDisplayName("hàn")>
        [Cliff] = 27
        ''' <summary>Kang Xi Radical № 28 厶 (sī; private)</summary>
        <FieldDisplayName("sī")>
        [Private] = 28
        ''' <summary>Kang Xi Radical № 29 又 (yòu; again)</summary>
        <FieldDisplayName("yòu")>
        [Again] = 29
        ''' <summary>Kang Xi Radical № 30 口 (kǒu; mouth)</summary>
        <FieldDisplayName("kǒu")>
        [Mouth] = 30
        ''' <summary>Kang Xi Radical № 31 囗 (wéi; enclosure)</summary>
        <FieldDisplayName("wéi")>
        [Enclosure] = 31
        ''' <summary>Kang Xi Radical № 32 土 (tǔ; earth)</summary>
        <FieldDisplayName("tǔ")>
        [Earth] = 32
        ''' <summary>Kang Xi Radical № 33 士 (shì; scholar)</summary>
        <FieldDisplayName("shì")>
        [Scholar] = 33
        ''' <summary>Kang Xi Radical № 34 夂 (suī; go)</summary>
        <FieldDisplayName("suī")>
        [Go] = 34
        ''' <summary>Kang Xi Radical № 35 夊 (zhī; go slowly)</summary>
        <FieldDisplayName("zhī")>
        [GoSlowly] = 35
        ''' <summary>Kang Xi Radical № 36 夕 (xī; night)</summary>
        <FieldDisplayName("xī")>
        [Night] = 36
        ''' <summary>Kang Xi Radical № 37 大 (dà; big)</summary>
        <FieldDisplayName("dà")>
        [Big] = 37
        ''' <summary>Kang Xi Radical № 38 女 (nǚ; woman)</summary>
        <FieldDisplayName("nǚ")>
        [Woman] = 38
        ''' <summary>Kang Xi Radical № 39 子 (zǐ; child)</summary>
        <FieldDisplayName("zǐ")>
        [Child] = 39
        ''' <summary>Kang Xi Radical № 40 宀 (gài; roof)</summary>
        <FieldDisplayName("gài")>
        [Roof] = 40
        ''' <summary>Kang Xi Radical № 41 寸 (cùn; inch)</summary>
        <FieldDisplayName("cùn")>
        [Inch] = 41
        ''' <summary>Kang Xi Radical № 42 小 (xiǎo; small)</summary>
        <FieldDisplayName("xiǎo")>
        [Small] = 42
        ''' <summary>Kang Xi Radical № 43 尢 (yóu; lame)</summary>
        ''' <remarks>Variants are: 尣</remarks>
        <FieldDisplayName("yóu")>
        [Lame] = 43
        ''' <summary>Kang Xi Radical № 44 尸 (shī; corpse)</summary>
        <FieldDisplayName("shī")>
        [Corpse] = 44
        ''' <summary>Kang Xi Radical № 45 屮 (chè; sprout)</summary>
        <FieldDisplayName("chè")>
        [Sprout] = 45
        ''' <summary>Kang Xi Radical № 46 山 (shān; mountain)</summary>
        <FieldDisplayName("shān")>
        [Mountain_Shān] = 46
        ''' <summary>Kang Xi Radical № 47 川 (chuān; river)</summary>
        ''' <remarks>Variants are: 巛 巜</remarks>
        <FieldDisplayName("chuān")>
        [River] = 47
        ''' <summary>Kang Xi Radical № 48 工 (gōng; work)</summary>
        <FieldDisplayName("gōng")>
        [Work] = 48
        ''' <summary>Kang Xi Radical № 49 己 (jǐ; oneself)</summary>
        <FieldDisplayName("jǐ")>
        [Oneself_Jǐ] = 49
        ''' <summary>Kang Xi Radical № 50 巾 (jīn; towel)</summary>
        <FieldDisplayName("jīn")>
        [Towel] = 50
        ''' <summary>Kang Xi Radical № 51 干 (gān; dry)</summary>
        <FieldDisplayName("gān")>
        [Dry] = 51
        ''' <summary>Kang Xi Radical № 52 幺 (yāo; thread)</summary>
        <FieldDisplayName("yāo")>
        [Thread] = 52
        ''' <summary>Kang Xi Radical № 53 广 (guǎng; shelter)</summary>
        <FieldDisplayName("guǎng")>
        [Shelter] = 53
        ''' <summary>Kang Xi Radical № 54 廴 (yǐn; stride)</summary>
        <FieldDisplayName("yǐn")>
        [Stride] = 54
        ''' <summary>Kang Xi Radical № 55 廾 (gǒng; hands joined)</summary>
        <FieldDisplayName("gǒng")>
        [HandsJoined] = 55
        ''' <summary>Kang Xi Radical № 56 弋 (yì; shoot with a bow)</summary>
        <FieldDisplayName("yì")>
        [ShootWithABow] = 56
        ''' <summary>Kang Xi Radical № 57 弓 (gōng; bow)</summary>
        <FieldDisplayName("gōng")>
        [Bow] = 57
        ''' <summary>Kang Xi Radical № 58 彐 (jì; snout)</summary>
        ''' <remarks>Variants are: 彑</remarks>
        <FieldDisplayName("jì")>
        [Snout] = 58
        ''' <summary>Kang Xi Radical № 59 彡 (shān; hair)</summary>
        <FieldDisplayName("shān")>
        [Hair] = 59
        ''' <summary>Kang Xi Radical № 60 彳 (chì; step)</summary>
        <FieldDisplayName("chì")>
        [Step] = 60
        ''' <summary>Kang Xi Radical № 61 心 (xīn; heart)</summary>
        ''' <remarks>Variants are: 忄</remarks>
        <FieldDisplayName("xīn")>
        [Heart] = 61
        ''' <summary>Kang Xi Radical № 62 戈 (gē; spear)</summary>
        <FieldDisplayName("gē")>
        [Spear_Gē] = 62
        ''' <summary>Kang Xi Radical № 63 户 (hù; door)</summary>
        <FieldDisplayName("hù")>
        [Door] = 63
        ''' <summary>Kang Xi Radical № 64 手 (shǒu; hand)</summary>
        ''' <remarks>Variants are: 扌</remarks>
        <FieldDisplayName("shǒu")>
        [Hand] = 64
        ''' <summary>Kang Xi Radical № 65 支 (zhī; branch)</summary>
        <FieldDisplayName("zhī")>
        [Branch] = 65
        ''' <summary>Kang Xi Radical № 66 攴 (pū; rap)</summary>
        <FieldDisplayName("pū")>
        [Rap] = 66
        ''' <summary>Kang Xi Radical № 67 攵 (wén; script)</summary>
        <FieldDisplayName("wén")>
        [Script] = 67
        ''' <summary>Kang Xi Radical № 68 斗 (dǒu; dipper)</summary>
        <FieldDisplayName("dǒu")>
        [Dipper] = 68
        ''' <summary>Kang Xi Radical № 69 斤 (jīn; axe)</summary>
        <FieldDisplayName("jīn")>
        [Axe] = 69
        ''' <summary>Kang Xi Radical № 70 方 (fāng; square)</summary>
        <FieldDisplayName("fāng")>
        [Square] = 70
        ''' <summary>Kang Xi Radical № 71 无 (wú; not)</summary>
        <FieldDisplayName("wú")>
        [Not] = 71
        ''' <summary>Kang Xi Radical № 72 日 (rì; sun)</summary>
        <FieldDisplayName("rì")>
        [Sun] = 72
        ''' <summary>Kang Xi Radical № 73 曰 (yuē; say)</summary>
        <FieldDisplayName("yuē")>
        [Say] = 73
        ''' <summary>Kang Xi Radical № 74 月 (yuè; moon)</summary>
        <FieldDisplayName("yuè")>
        [Moon] = 74
        ''' <summary>Kang Xi Radical № 75 木 (mù; tree)</summary>
        <FieldDisplayName("mù")>
        [Tree] = 75
        ''' <summary>Kang Xi Radical № 76 欠 (qiàn; lack)</summary>
        <FieldDisplayName("qiàn")>
        [Lack] = 76
        ''' <summary>Kang Xi Radical № 77 止 (zhǐ; stop)</summary>
        <FieldDisplayName("zhǐ")>
        [Stop] = 77
        ''' <summary>Kang Xi Radical № 78 歹 (dǎi; death)</summary>
        <FieldDisplayName("dǎi")>
        [Death] = 78
        ''' <summary>Kang Xi Radical № 79 殳 (shū; weapon)</summary>
        <FieldDisplayName("shū")>
        [Weapon] = 79
        ''' <summary>Kang Xi Radical № 80 毋 (mǔ; mother)</summary>
        <FieldDisplayName("mǔ")>
        [Mother] = 80
        ''' <summary>Kang Xi Radical № 81 比 (bǐ; compare)</summary>
        <FieldDisplayName("bǐ")>
        [Compare] = 81
        ''' <summary>Kang Xi Radical № 82 毛 (máo; fur)</summary>
        <FieldDisplayName("máo")>
        [Fur] = 82
        ''' <summary>Kang Xi Radical № 83 氏 (shì; clan)</summary>
        <FieldDisplayName("shì")>
        [Clan] = 83
        ''' <summary>Kang Xi Radical № 84 气 (qì; steam)</summary>
        <FieldDisplayName("qì")>
        [Steam] = 84
        ''' <summary>Kang Xi Radical № 85 水 (shuì; water)</summary>
        ''' <remarks>Variants are: 氵</remarks>
        <FieldDisplayName("shuì")>
        [Water] = 85
        ''' <summary>Kang Xi Radical № 86 火 (huǒ; fire)</summary>
        ''' <remarks>Variants are: 灬</remarks>
        <FieldDisplayName("huǒ")>
        [Fire] = 86
        ''' <summary>Kang Xi Radical № 87 爪 (zhǎo; claw)</summary>
        ''' <remarks>Variants are: 爫</remarks>
        <FieldDisplayName("zhǎo")>
        [Claw] = 87
        ''' <summary>Kang Xi Radical № 88 父 (fù; father)</summary>
        <FieldDisplayName("fù")>
        [Father] = 88
        ''' <summary>Kang Xi Radical № 89 爻 (yáo; lines on a trigram)</summary>
        <FieldDisplayName("yáo")>
        [LinesOnATrigram] = 89
        ''' <summary>Kang Xi Radical № 90 爿 (qiáng; half of a tree trunk)</summary>
        <FieldDisplayName("qiáng")>
        [HalfOfATreeTrunk] = 90
        ''' <summary>Kang Xi Radical № 91 片 (piàn; slice)</summary>
        <FieldDisplayName("piàn")>
        [Slice] = 91
        ''' <summary>Kang Xi Radical № 92 牙 (yá; fang)</summary>
        <FieldDisplayName("yá")>
        [Fang] = 92
        ''' <summary>Kang Xi Radical № 93 牛 (niú; cow)</summary>
        ''' <remarks>Variants are: 牜</remarks>
        <FieldDisplayName("niú")>
        [Cow] = 93
        ''' <summary>Kang Xi Radical № 94 犭 (quǎn; dog)</summary>
        ''' <remarks>Variants are: 犬</remarks>
        <FieldDisplayName("quǎn")>
        [Dog] = 94
        ''' <summary>Kang Xi Radical № 95 玄 (xuán; profound)</summary>
        <FieldDisplayName("xuán")>
        [Profound] = 95
        ''' <summary>Kang Xi Radical № 96 玉 (yù; jade)</summary>
        ''' <remarks>Variants are: 王</remarks>
        <FieldDisplayName("yù")>
        [Jade] = 96
        ''' <summary>Kang Xi Radical № 97 瓜 (guā; melon)</summary>
        <FieldDisplayName("guā")>
        [Melon] = 97
        ''' <summary>Kang Xi Radical № 98 瓦 (wǎ; tile)</summary>
        <FieldDisplayName("wǎ")>
        [Tile] = 98
        ''' <summary>Kang Xi Radical № 99 甘 (gān; sweet)</summary>
        <FieldDisplayName("gān")>
        [Sweet] = 99
        ''' <summary>Kang Xi Radical № 100 生 (shēng; life)</summary>
        <FieldDisplayName("shēng")>
        [Life] = 100
        ''' <summary>Kang Xi Radical № 101 用 (yòng; use)</summary>
        <FieldDisplayName("yòng")>
        [Use] = 101
        ''' <summary>Kang Xi Radical № 102 田 (tián; field)</summary>
        <FieldDisplayName("tián")>
        [Field] = 102
        ''' <summary>Kang Xi Radical № 103 疋 (pǐ; cloth)</summary>
        <FieldDisplayName("pǐ")>
        [Cloth] = 103
        ''' <summary>Kang Xi Radical № 104 疒 (bìng; ill)</summary>
        <FieldDisplayName("bìng")>
        [Ill] = 104
        ''' <summary>Kang Xi Radical № 105 癶 (bō; foot steps)</summary>
        <FieldDisplayName("bō")>
        [FootSteps] = 105
        ''' <summary>Kang Xi Radical № 106 白 (bái; white)</summary>
        <FieldDisplayName("bái")>
        [White] = 106
        ''' <summary>Kang Xi Radical № 107 皮 (pí; skin)</summary>
        <FieldDisplayName("pí")>
        [Skin] = 107
        ''' <summary>Kang Xi Radical № 108 皿 (mǐn; dish)</summary>
        <FieldDisplayName("mǐn")>
        [Dish] = 108
        ''' <summary>Kang Xi Radical № 109 目 (mù; eye)</summary>
        <FieldDisplayName("mù")>
        [Eye] = 109
        ''' <summary>Kang Xi Radical № 110 矛 (máo; spear)</summary>
        <FieldDisplayName("máo")>
        [Spear_Máo] = 110
        ''' <summary>Kang Xi Radical № 111 矢 (shǐ; arrow)</summary>
        <FieldDisplayName("shǐ")>
        [Arrow] = 111
        ''' <summary>Kang Xi Radical № 112 石 (shí; stone)</summary>
        <FieldDisplayName("shí")>
        [Stone] = 112
        ''' <summary>Kang Xi Radical № 113 示 (shì; spirit)</summary>
        ''' <remarks>Variants are: 礻</remarks>
        <FieldDisplayName("shì")>
        [Spirit] = 113
        ''' <summary>Kang Xi Radical № 114 禸 (róu; track)</summary>
        <FieldDisplayName("róu")>
        [Track] = 114
        ''' <summary>Kang Xi Radical № 115 禾 (hé; grain)</summary>
        <FieldDisplayName("hé")>
        [Grain] = 115
        ''' <summary>Kang Xi Radical № 116 穴 (xuè; cave)</summary>
        <FieldDisplayName("xuè")>
        [Cave] = 116
        ''' <summary>Kang Xi Radical № 117 立 (lì; stand)</summary>
        <FieldDisplayName("lì")>
        [Stand] = 117
        ''' <summary>Kang Xi Radical № 118 竹 (zhú; bamboo)</summary>
        <FieldDisplayName("zhú")>
        [Bamboo] = 118
        ''' <summary>Kang Xi Radical № 119 米 (mǐ; rice)</summary>
        <FieldDisplayName("mǐ")>
        [Rice] = 119
        ''' <summary>Kang Xi Radical № 120 纟 (sī; silk)</summary>
        <FieldDisplayName("sī")>
        [Silk] = 120
        ''' <summary>Kang Xi Radical № 121 缶 (fǒu; jar)</summary>
        <FieldDisplayName("fǒu")>
        [Jar] = 121
        ''' <summary>Kang Xi Radical № 122 网 (wǎng; net)</summary>
        ''' <remarks>Variants are: 罒</remarks>
        <FieldDisplayName("wǎng")>
        [Net] = 122
        ''' <summary>Kang Xi Radical № 123 羊 (yáng; sheep)</summary>
        <FieldDisplayName("yáng")>
        [Sheep] = 123
        ''' <summary>Kang Xi Radical № 124 羽 (yǔ; feather)</summary>
        <FieldDisplayName("yǔ")>
        [Feather] = 124
        ''' <summary>Kang Xi Radical № 125 老 (lǎo; old)</summary>
        <FieldDisplayName("lǎo")>
        [Old] = 125
        ''' <summary>Kang Xi Radical № 126 而 (ér; and)</summary>
        <FieldDisplayName("ér")>
        [And] = 126
        ''' <summary>Kang Xi Radical № 127 耒 (lěi; plow)</summary>
        <FieldDisplayName("lěi")>
        [Plow] = 127
        ''' <summary>Kang Xi Radical № 128 耳 (ěr; ear)</summary>
        <FieldDisplayName("ěr")>
        [Ear] = 128
        ''' <summary>Kang Xi Radical № 129 聿 (yù; brush)</summary>
        <FieldDisplayName("yù")>
        [Brush] = 129
        ''' <summary>Kang Xi Radical № 130 肉 (ròu; meat)</summary>
        <FieldDisplayName("ròu")>
        [Meat] = 130
        ''' <summary>Kang Xi Radical № 131 臣 (chén; minister)</summary>
        <FieldDisplayName("chén")>
        [Minister] = 131
        ''' <summary>Kang Xi Radical № 132 自 (zì; oneself)</summary>
        <FieldDisplayName("zì")>
        [Oneself_Zì] = 132
        ''' <summary>Kang Xi Radical № 133 至 (zhì; arrive)</summary>
        <FieldDisplayName("zhì")>
        [Arrive] = 133
        ''' <summary>Kang Xi Radical № 134 臼 (jiù; mortar)</summary>
        <FieldDisplayName("jiù")>
        [Mortar] = 134
        ''' <summary>Kang Xi Radical № 135 舌 (shé; tongue)</summary>
        <FieldDisplayName("shé")>
        [Tongue] = 135
        ''' <summary>Kang Xi Radical № 136 舛 (chuǎn; contrary)</summary>
        <FieldDisplayName("chuǎn")>
        [Contrary] = 136
        ''' <summary>Kang Xi Radical № 137 舟 (zhōu; boat)</summary>
        <FieldDisplayName("zhōu")>
        [Boat] = 137
        ''' <summary>Kang Xi Radical № 138 艮 (gèn; mountain)</summary>
        <FieldDisplayName("gèn")>
        [Mountain_Gèn] = 138
        ''' <summary>Kang Xi Radical № 139 色 (sè; color)</summary>
        <FieldDisplayName("sè")>
        [Color] = 139
        ''' <summary>Kang Xi Radical № 140 艹 (cǎo; grass)</summary>
        <FieldDisplayName("cǎo")>
        [Grass] = 140
        ''' <summary>Kang Xi Radical № 141 虍 (hǔ; tiger)</summary>
        <FieldDisplayName("hǔ")>
        [Tiger] = 141
        ''' <summary>Kang Xi Radical № 142 虫 (chóng; insect)</summary>
        <FieldDisplayName("chóng")>
        [Insect] = 142
        ''' <summary>Kang Xi Radical № 143 血 (xuě; blood)</summary>
        <FieldDisplayName("xuě")>
        [Blood] = 143
        ''' <summary>Kang Xi Radical № 144 行 (xíng; walk)</summary>
        <FieldDisplayName("xíng")>
        [Walk_Xíng] = 144
        ''' <summary>Kang Xi Radical № 145 衣 (yī; clothes)</summary>
        ''' <remarks>Variants are: 衤</remarks>
        <FieldDisplayName("yī")>
        [Clothes] = 145
        ''' <summary>Kang Xi Radical № 146 西 (xī; west)</summary>
        ''' <remarks>Variants are: 覀</remarks>
        <FieldDisplayName("xī")>
        [West] = 146
        ''' <summary>Kang Xi Radical № 147 见 (jiàn; see)</summary>
        <FieldDisplayName("jiàn")>
        [See] = 147
        ''' <summary>Kang Xi Radical № 148 角 (jiǎo; horn)</summary>
        <FieldDisplayName("jiǎo")>
        [Horn] = 148
        ''' <summary>Kang Xi Radical № 149 讠 (yán; speech)</summary>
        <FieldDisplayName("yán")>
        [Speech] = 149
        ''' <summary>Kang Xi Radical № 150 谷 (gǔ; valley)</summary>
        <FieldDisplayName("gǔ")>
        [Valley] = 150
        ''' <summary>Kang Xi Radical № 151 豆 (dòu; bean)</summary>
        <FieldDisplayName("dòu")>
        [Bean] = 151
        ''' <summary>Kang Xi Radical № 152 豖 (shǐ; pig)</summary>
        <FieldDisplayName("shǐ")>
        [Pig] = 152
        ''' <summary>Kang Xi Radical № 153 豸 (zhì; badger)</summary>
        <FieldDisplayName("zhì")>
        [Badger] = 153
        ''' <summary>Kang Xi Radical № 154 贝 (bèi; shell)</summary>
        <FieldDisplayName("bèi")>
        [Shell] = 154
        ''' <summary>Kang Xi Radical № 155 赤 (chì; red)</summary>
        <FieldDisplayName("chì")>
        [Red] = 155
        ''' <summary>Kang Xi Radical № 156 走 (zǒu; walk)</summary>
        <FieldDisplayName("zǒu")>
        [Walk_Zǒu] = 156
        ''' <summary>Kang Xi Radical № 157 足 (zú; foot)</summary>
        <FieldDisplayName("zú")>
        [Foot] = 157
        ''' <summary>Kang Xi Radical № 158 身 (shēn; body)</summary>
        <FieldDisplayName("shēn")>
        [Body] = 158
        ''' <summary>Kang Xi Radical № 159 车 (chē; cart)</summary>
        <FieldDisplayName("chē")>
        [Cart] = 159
        ''' <summary>Kang Xi Radical № 160 辛 (xīn; bitter)</summary>
        <FieldDisplayName("xīn")>
        [Bitter] = 160
        ''' <summary>Kang Xi Radical № 161 辰 (chén; morning)</summary>
        <FieldDisplayName("chén")>
        [Morning] = 161
        ''' <summary>Kang Xi Radical № 162 辶 (chuò; walk)</summary>
        <FieldDisplayName("chuò")>
        [Walk_Chuò] = 162
        ''' <summary>Kang Xi Radical № 163 邑 (yì; city)</summary>
        ''' <remarks>Variants are: 阝</remarks>
        <FieldDisplayName("yì")>
        [City] = 163
        ''' <summary>Kang Xi Radical № 164 酉 (yǒu; wine)</summary>
        <FieldDisplayName("yǒu")>
        [Wine] = 164
        ''' <summary>Kang Xi Radical № 165 釆 (biàn; distinguish)</summary>
        <FieldDisplayName("biàn")>
        [Distinguish] = 165
        ''' <summary>Kang Xi Radical № 166 里 (lǐ; village)</summary>
        <FieldDisplayName("lǐ")>
        [Village] = 166
        ''' <summary>Kang Xi Radical № 167 钅 (jīn; metal)</summary>
        ''' <remarks>Variants are: 金</remarks>
        <FieldDisplayName("jīn")>
        [Metal] = 167
        ''' <summary>Kang Xi Radical № 168 长 (cháng; long)</summary>
        <FieldDisplayName("cháng")>
        [Long] = 168
        ''' <summary>Kang Xi Radical № 169 门 (mén; gate)</summary>
        <FieldDisplayName("mén")>
        [Gate] = 169
        ''' <summary>Kang Xi Radical № 170 阜 (fù; mound)</summary>
        ''' <remarks>Variants are: 阝</remarks>
        <FieldDisplayName("fù")>
        [Mound] = 170
        ''' <summary>Kang Xi Radical № 171 隶 (lì; slave)</summary>
        <FieldDisplayName("lì")>
        [Slave] = 171
        ''' <summary>Kang Xi Radical № 172 隹 (zhuī; short-tailed bird)</summary>
        <FieldDisplayName("zhuī")>
        [ShortTailedBird] = 172
        ''' <summary>Kang Xi Radical № 173 雨 (yǔ; rain)</summary>
        <FieldDisplayName("yǔ")>
        [Rain] = 173
        ''' <summary>Kang Xi Radical № 174 青 (qīng; blue)</summary>
        <FieldDisplayName("qīng")>
        [Blue] = 174
        ''' <summary>Kang Xi Radical № 175 非 (fēi; wrong)</summary>
        <FieldDisplayName("fēi")>
        [Wrong] = 175
        ''' <summary>Kang Xi Radical № 176 面 (miàn; face)</summary>
        <FieldDisplayName("miàn")>
        [Face] = 176
        ''' <summary>Kang Xi Radical № 177 革 (gé; leather)</summary>
        <FieldDisplayName("gé")>
        [Leather] = 177
        ''' <summary>Kang Xi Radical № 178 韦 (wěi; soft leather)</summary>
        <FieldDisplayName("wěi")>
        [SoftLeather] = 178
        ''' <summary>Kang Xi Radical № 179 韭 (jiǔ; leek)</summary>
        <FieldDisplayName("jiǔ")>
        [Leek] = 179
        ''' <summary>Kang Xi Radical № 180 音 (yīn; sound)</summary>
        <FieldDisplayName("yīn")>
        [Sound] = 180
        ''' <summary>Kang Xi Radical № 181 页 (yè; page)</summary>
        <FieldDisplayName("yè")>
        [Page] = 181
        ''' <summary>Kang Xi Radical № 182 风 (fēng; wind)</summary>
        <FieldDisplayName("fēng")>
        [Wind] = 182
        ''' <summary>Kang Xi Radical № 183 飞 (fēi; fly)</summary>
        <FieldDisplayName("fēi")>
        [Fly] = 183
        ''' <summary>Kang Xi Radical № 184 饣 (shí; eat)</summary>
        ''' <remarks>Variants are: 飠 食</remarks>
        <FieldDisplayName("shí")>
        [Eat] = 184
        ''' <summary>Kang Xi Radical № 185 首 (shǒu; head)</summary>
        <FieldDisplayName("shǒu")>
        [Head] = 185
        ''' <summary>Kang Xi Radical № 186 香 (xiāng; fragrant)</summary>
        <FieldDisplayName("xiāng")>
        [Fragrant] = 186
        ''' <summary>Kang Xi Radical № 187 马 (mǎ; horse)</summary>
        <FieldDisplayName("mǎ")>
        [Horse] = 187
        ''' <summary>Kang Xi Radical № 188 骨 (gǔ; bone)</summary>
        <FieldDisplayName("gǔ")>
        [Bone] = 188
        ''' <summary>Kang Xi Radical № 189 高 (gāo; high)</summary>
        <FieldDisplayName("gāo")>
        [High] = 189
        ''' <summary>Kang Xi Radical № 190 髟 (biāo; long hair)</summary>
        <FieldDisplayName("biāo")>
        [LongHair] = 190
        ''' <summary>Kang Xi Radical № 191 鬥 (dòu; fight)</summary>
        <FieldDisplayName("dòu")>
        [Fight] = 191
        ''' <summary>Kang Xi Radical № 192 鬯 (chàng; sacrificial wine)</summary>
        <FieldDisplayName("chàng")>
        [SacrificialWine] = 192
        ''' <summary>Kang Xi Radical № 193 鬲 (lì; cauldron)</summary>
        <FieldDisplayName("lì")>
        [Cauldron] = 193
        ''' <summary>Kang Xi Radical № 194 鬼 (guǐ; ghost)</summary>
        <FieldDisplayName("guǐ")>
        [Ghost] = 194
        ''' <summary>Kang Xi Radical № 195 鱼 (yú; fish)</summary>
        <FieldDisplayName("yú")>
        [Fish] = 195
        ''' <summary>Kang Xi Radical № 196 鸟 (niǎo; bird)</summary>
        <FieldDisplayName("niǎo")>
        [Bird] = 196
        ''' <summary>Kang Xi Radical № 197 卤 (lǔ; salty)</summary>
        <FieldDisplayName("lǔ")>
        [Salty] = 197
        ''' <summary>Kang Xi Radical № 198 鹿 (lù; deer)</summary>
        <FieldDisplayName("lù")>
        [Deer] = 198
        ''' <summary>Kang Xi Radical № 199 麦 (mài; wheat)</summary>
        <FieldDisplayName("mài")>
        [Wheat] = 199
        ''' <summary>Kang Xi Radical № 200 麻 (má; hemp)</summary>
        <FieldDisplayName("má")>
        [Hemp] = 200
        ''' <summary>Kang Xi Radical № 201 黃 (huáng; yellow)</summary>
        <FieldDisplayName("huáng")>
        [Yellow] = 201
        ''' <summary>Kang Xi Radical № 202 黍 (shǔ; millet)</summary>
        <FieldDisplayName("shǔ")>
        [Millet] = 202
        ''' <summary>Kang Xi Radical № 203 黑 (hēi; black)</summary>
        <FieldDisplayName("hēi")>
        [Black] = 203
        ''' <summary>Kang Xi Radical № 204 黹 (zhǐ; embroidery)</summary>
        <FieldDisplayName("zhǐ")>
        [Embroidery] = 204
        ''' <summary>Kang Xi Radical № 205 黾 (mǐn; frog)</summary>
        <FieldDisplayName("mǐn")>
        [Frog] = 205
        ''' <summary>Kang Xi Radical № 206 鼎 (dǐng; tripod)</summary>
        <FieldDisplayName("dǐng")>
        [Tripod] = 206
        ''' <summary>Kang Xi Radical № 207 鼓 (gǔ; drum)</summary>
        <FieldDisplayName("gǔ")>
        [Drum] = 207
        ''' <summary>Kang Xi Radical № 208 鼠 (shǔ; rat)</summary>
        <FieldDisplayName("shǔ")>
        [Rat] = 208
        ''' <summary>Kang Xi Radical № 209 鼻 (bí; nose)</summary>
        <FieldDisplayName("bí")>
        [Nose] = 209
        ''' <summary>Kang Xi Radical № 210 齐 (qí; even)</summary>
        <FieldDisplayName("qí")>
        [Even] = 210
        ''' <summary>Kang Xi Radical № 211 齿 (chǐ; tooth)</summary>
        <FieldDisplayName("chǐ")>
        [Tooth] = 211
        ''' <summary>Kang Xi Radical № 212 龙 (lóng; dragon)</summary>
        <FieldDisplayName("lóng")>
        [Dragon] = 212
        ''' <summary>Kang Xi Radical № 213 龟 (guī; turtle)</summary>
        <FieldDisplayName("guī")>
        [Turtle] = 213
        ''' <summary>Kang Xi Radical № 214 龠 (yuè; flute)</summary>
        <FieldDisplayName("yuè")>
        [Flute] = 214

#End Region
#Region "Unicode supplement"
        '''<summary>CJK Supplemental Radical ⺀ (repeat)</summary>
        <FieldDisplayName("⺀")>
        [SupRepeat] = 215
        '''<summary>CJK Supplemental Radical ⺁ (cliff)</summary>
        <FieldDisplayName("⺁")>
        [SupCliff] = 216
        '''<summary>CJK Supplemental Radical ⺂ (second one)</summary>
        <FieldDisplayName("⺂")>
        [SupSecondOne] = 217
        '''<summary>CJK Supplemental Radical ⺃ (second two)</summary>
        <FieldDisplayName("⺃")>
        [SupSecondTwo] = 218
        '''<summary>CJK Supplemental Radical ⺄ (second three)</summary>
        <FieldDisplayName("⺄")>
        [SupSecondThree] = 219
        '''<summary>CJK Supplemental Radical ⺅ (person)</summary>
        <FieldDisplayName("⺅")>
        [SupPerson] = 220
        '''<summary>CJK Supplemental Radical ⺆ (box)</summary>
        <FieldDisplayName("⺆")>
        [SupBox] = 221
        '''<summary>CJK Supplemental Radical ⺇ (table)</summary>
        <FieldDisplayName("⺇")>
        [SupTable] = 222
        '''<summary>CJK Supplemental Radical ⺈ (knife one)</summary>
        <FieldDisplayName("⺈")>
        [SupKnifeOne] = 223
        '''<summary>CJK Supplemental Radical ⺉ (knife two)</summary>
        <FieldDisplayName("⺉")>
        [SupKnifeTwo] = 224
        '''<summary>CJK Supplemental Radical ⺊ (divination)</summary>
        <FieldDisplayName("⺊")>
        [SupDivination] = 225
        '''<summary>CJK Supplemental Radical ⺋ (seal)</summary>
        <FieldDisplayName("⺋")>
        [SupSeal] = 226
        '''<summary>CJK Supplemental Radical ⺌ (small one)</summary>
        <FieldDisplayName("⺌")>
        [SupSmallOne] = 227
        '''<summary>CJK Supplemental Radical ⺍ (small two)</summary>
        <FieldDisplayName("⺍")>
        [SupSmallTwo] = 228
        '''<summary>CJK Supplemental Radical ⺎ (lame one)</summary>
        <FieldDisplayName("⺎")>
        [SupLameOne] = 229
        '''<summary>CJK Supplemental Radical ⺏ (lame two)</summary>
        <FieldDisplayName("⺏")>
        [SupLameTwo] = 230
        '''<summary>CJK Supplemental Radical ⺐ (lame three)</summary>
        <FieldDisplayName("⺐")>
        [SupLameThree] = 231
        '''<summary>CJK Supplemental Radical ⺑ (lame four)</summary>
        <FieldDisplayName("⺑")>
        [SupLameFour] = 232
        '''<summary>CJK Supplemental Radical ⺒ (snake)</summary>
        <FieldDisplayName("⺒")>
        [SupSnake] = 233
        '''<summary>CJK Supplemental Radical ⺓ (thread)</summary>
        <FieldDisplayName("⺓")>
        [SupThread] = 234
        '''<summary>CJK Supplemental Radical ⺔ (snout one)</summary>
        <FieldDisplayName("⺔")>
        [SupSnoutOne] = 235
        '''<summary>CJK Supplemental Radical ⺕ (snout two)</summary>
        <FieldDisplayName("⺕")>
        [SupSnoutTwo] = 236
        '''<summary>CJK Supplemental Radical ⺖ (heart one)</summary>
        <FieldDisplayName("⺖")>
        [SupHeartOne] = 237
        '''<summary>CJK Supplemental Radical ⺗ (heart two)</summary>
        <FieldDisplayName("⺗")>
        [SupHeartTwo] = 238
        '''<summary>CJK Supplemental Radical ⺘ (hand)</summary>
        <FieldDisplayName("⺘")>
        [SupHand] = 239
        '''<summary>CJK Supplemental Radical ⺙ (rap)</summary>
        <FieldDisplayName("⺙")>
        [SupRap] = 240
        '''<summary>CJK Supplemental Radical ⺛ (choke)</summary>
        <FieldDisplayName("⺛")>
        [SupChoke] = 241
        '''<summary>CJK Supplemental Radical ⺜ (sun)</summary>
        <FieldDisplayName("⺜")>
        [SupSun] = 242
        '''<summary>CJK Supplemental Radical ⺝ (moon)</summary>
        <FieldDisplayName("⺝")>
        [SupMoon] = 243
        '''<summary>CJK Supplemental Radical ⺞ (death)</summary>
        <FieldDisplayName("⺞")>
        [SupDeath] = 244
        '''<summary>CJK Supplemental Radical ⺟ (mother)</summary>
        <FieldDisplayName("⺟")>
        [SupMother] = 245
        '''<summary>CJK Supplemental Radical ⺠ (civilian)</summary>
        <FieldDisplayName("⺠")>
        [SupCivilian] = 246
        '''<summary>CJK Supplemental Radical ⺡ (water one)</summary>
        <FieldDisplayName("⺡")>
        [SupWaterOne] = 247
        '''<summary>CJK Supplemental Radical ⺢ (water two)</summary>
        <FieldDisplayName("⺢")>
        [SupWaterTwo] = 248
        '''<summary>CJK Supplemental Radical ⺣ (fire)</summary>
        <FieldDisplayName("⺣")>
        [SupFire] = 249
        '''<summary>CJK Supplemental Radical ⺤ (paw one)</summary>
        <FieldDisplayName("⺤")>
        [SupPawOne] = 250
        '''<summary>CJK Supplemental Radical ⺥ (paw two)</summary>
        <FieldDisplayName("⺥")>
        [SupPawTwo] = 251
        '''<summary>CJK Supplemental Radical ⺦ (simplified half tree trunk)</summary>
        <FieldDisplayName("⺦")>
        [SupSimplifiedHalfTreeTrunk] = 252
        '''<summary>CJK Supplemental Radical ⺧ (cow)</summary>
        <FieldDisplayName("⺧")>
        [SupCow] = 253
        '''<summary>CJK Supplemental Radical ⺨ (dog)</summary>
        <FieldDisplayName("⺨")>
        [SupDog] = 254
        '''<summary>CJK Supplemental Radical ⺩ (jade)</summary>
        <FieldDisplayName("⺩")>
        [SupJade] = 255
        '''<summary>CJK Supplemental Radical ⺪ (bolt of cloth)</summary>
        <FieldDisplayName("⺪")>
        [SupBoltOfCloth] = 256
        '''<summary>CJK Supplemental Radical ⺫ (eye)</summary>
        <FieldDisplayName("⺫")>
        [SupEye] = 257
        '''<summary>CJK Supplemental Radical ⺬ (spirit one)</summary>
        <FieldDisplayName("⺬")>
        [SupSpiritOne] = 258
        '''<summary>CJK Supplemental Radical ⺭ (spirit two)</summary>
        <FieldDisplayName("⺭")>
        [SupSpiritTwo] = 259
        '''<summary>CJK Supplemental Radical ⺮ (bamboo)</summary>
        <FieldDisplayName("⺮")>
        [SupBamboo] = 260
        '''<summary>CJK Supplemental Radical ⺯ (silk)</summary>
        <FieldDisplayName("⺯")>
        [SupSilk] = 261
        '''<summary>CJK Supplemental Radical ⺰ (c-simplified silk)</summary>
        <FieldDisplayName("⺰")>
        [SupCSimplifiedSilk] = 262
        '''<summary>CJK Supplemental Radical ⺱ (net one)</summary>
        <FieldDisplayName("⺱")>
        [SupNetOne] = 263
        '''<summary>CJK Supplemental Radical ⺲ (net two)</summary>
        <FieldDisplayName("⺲")>
        [SupNetTwo] = 264
        '''<summary>CJK Supplemental Radical ⺳ (net three)</summary>
        <FieldDisplayName("⺳")>
        [SupNetThree] = 265
        '''<summary>CJK Supplemental Radical ⺴ (net four)</summary>
        <FieldDisplayName("⺴")>
        [SupNetFour] = 266
        '''<summary>CJK Supplemental Radical ⺵ (mesh)</summary>
        <FieldDisplayName("⺵")>
        [SupMesh] = 267
        '''<summary>CJK Supplemental Radical ⺶ (sheep)</summary>
        <FieldDisplayName("⺶")>
        [SupSheep] = 268
        '''<summary>CJK Supplemental Radical ⺷ (ram)</summary>
        <FieldDisplayName("⺷")>
        [SupRam] = 269
        '''<summary>CJK Supplemental Radical ⺸ (ewe)</summary>
        <FieldDisplayName("⺸")>
        [SupEwe] = 270
        '''<summary>CJK Supplemental Radical ⺹ (old)</summary>
        <FieldDisplayName("⺹")>
        [SupOld] = 271
        '''<summary>CJK Supplemental Radical ⺺ (brush one)</summary>
        <FieldDisplayName("⺺")>
        [SupBrushOne] = 272
        '''<summary>CJK Supplemental Radical ⺻ (brush two)</summary>
        <FieldDisplayName("⺻")>
        [SupBrushTwo] = 273
        '''<summary>CJK Supplemental Radical ⺼ (meat)</summary>
        <FieldDisplayName("⺼")>
        [SupMeat] = 274
        '''<summary>CJK Supplemental Radical ⺽ (mortar)</summary>
        <FieldDisplayName("⺽")>
        [SupMortar] = 275
        '''<summary>CJK Supplemental Radical ⺾ (grass one)</summary>
        <FieldDisplayName("⺾")>
        [SupGrassOne] = 276
        '''<summary>CJK Supplemental Radical ⺿ (grass two)</summary>
        <FieldDisplayName("⺿")>
        [SupGrassTwo] = 277
        '''<summary>CJK Supplemental Radical ⻀ (grass three)</summary>
        <FieldDisplayName("⻀")>
        [SupGrassThree] = 278
        '''<summary>CJK Supplemental Radical ⻁ (tiger)</summary>
        <FieldDisplayName("⻁")>
        [SupTiger] = 279
        '''<summary>CJK Supplemental Radical ⻂ (clothes)</summary>
        <FieldDisplayName("⻂")>
        [SupClothes] = 280
        '''<summary>CJK Supplemental Radical ⻃ (west one)</summary>
        <FieldDisplayName("⻃")>
        [SupWestOne] = 281
        '''<summary>CJK Supplemental Radical ⻄ (west two)</summary>
        <FieldDisplayName("⻄")>
        [SupWestTwo] = 282
        '''<summary>CJK Supplemental Radical ⻅ (c-simplified see)</summary>
        <FieldDisplayName("⻅")>
        [SupCSimplifiedSee] = 283
        '''<summary>CJK Supplemental Radical ⻆ (simplified horn)</summary>
        <FieldDisplayName("⻆")>
        [SupSimplifiedHorn] = 284
        '''<summary>CJK Supplemental Radical ⻇ (horn)</summary>
        <FieldDisplayName("⻇")>
        [SupHorn] = 285
        '''<summary>CJK Supplemental Radical ⻈ (c-simplified speech)</summary>
        <FieldDisplayName("⻈")>
        [SupCSimplifiedSpeech] = 286
        '''<summary>CJK Supplemental Radical ⻉ (c-simplified shell)</summary>
        <FieldDisplayName("⻉")>
        [SupCSimplifiedShell] = 287
        '''<summary>CJK Supplemental Radical ⻊ (foot)</summary>
        <FieldDisplayName("⻊")>
        [SupFoot] = 288
        '''<summary>CJK Supplemental Radical ⻋ (c-simplified cart)</summary>
        <FieldDisplayName("⻋")>
        [SupCSimplifiedCart] = 289
        '''<summary>CJK Supplemental Radical ⻌ (simplified walk)</summary>
        <FieldDisplayName("⻌")>
        [SupSimplifiedWalk] = 290
        '''<summary>CJK Supplemental Radical ⻍ (walk one)</summary>
        <FieldDisplayName("⻍")>
        [SupWalkOne] = 291
        '''<summary>CJK Supplemental Radical ⻎ (walk two)</summary>
        <FieldDisplayName("⻎")>
        [SupWalkTwo] = 292
        '''<summary>CJK Supplemental Radical ⻏ (city)</summary>
        <FieldDisplayName("⻏")>
        [SupCity] = 293
        '''<summary>CJK Supplemental Radical ⻐ (c-simplified gold)</summary>
        <FieldDisplayName("⻐")>
        [SupCSimplifiedGold] = 294
        '''<summary>CJK Supplemental Radical ⻑ (long one)</summary>
        <FieldDisplayName("⻑")>
        [SupLongOne] = 295
        '''<summary>CJK Supplemental Radical ⻒ (long two)</summary>
        <FieldDisplayName("⻒")>
        [SupLongTwo] = 296
        '''<summary>CJK Supplemental Radical ⻓ (c-simplified long)</summary>
        <FieldDisplayName("⻓")>
        [SupCSimplifiedLong] = 297
        '''<summary>CJK Supplemental Radical ⻔ (c-simplified gate)</summary>
        <FieldDisplayName("⻔")>
        [SupCSimplifiedGate] = 298
        '''<summary>CJK Supplemental Radical ⻕ (mound one)</summary>
        <FieldDisplayName("⻕")>
        [SupMoundOne] = 299
        '''<summary>CJK Supplemental Radical ⻖ (mound two)</summary>
        <FieldDisplayName("⻖")>
        [SupMoundTwo] = 300
        '''<summary>CJK Supplemental Radical ⻗ (rain)</summary>
        <FieldDisplayName("⻗")>
        [SupRain] = 301
        '''<summary>CJK Supplemental Radical ⻘ (blue)</summary>
        <FieldDisplayName("⻘")>
        [SupBlue] = 302
        '''<summary>CJK Supplemental Radical ⻙ (c-simplified tanned leather)</summary>
        <FieldDisplayName("⻙")>
        [SupCSimplifiedTannedLeather] = 303
        '''<summary>CJK Supplemental Radical ⻚ (c-simplified leaf)</summary>
        <FieldDisplayName("⻚")>
        [SupCSimplifiedLeaf] = 304
        '''<summary>CJK Supplemental Radical ⻛ (c-simplified wind)</summary>
        <FieldDisplayName("⻛")>
        [SupCSimplifiedWind] = 305
        '''<summary>CJK Supplemental Radical ⻜ (c-simplified fly)</summary>
        <FieldDisplayName("⻜")>
        [SupCSimplifiedFly] = 306
        '''<summary>CJK Supplemental Radical ⻝ (eat one)</summary>
        <FieldDisplayName("⻝")>
        [SupEatOne] = 307
        '''<summary>CJK Supplemental Radical ⻞ (eat two)</summary>
        <FieldDisplayName("⻞")>
        [SupEatTwo] = 308
        '''<summary>CJK Supplemental Radical ⻟ (eat three)</summary>
        <FieldDisplayName("⻟")>
        [SupEatThree] = 309
        '''<summary>CJK Supplemental Radical ⻠ (c-simplified eat)</summary>
        <FieldDisplayName("⻠")>
        [SupCSimplifiedEat] = 310
        '''<summary>CJK Supplemental Radical ⻡ (head)</summary>
        <FieldDisplayName("⻡")>
        [SupHead] = 311
        '''<summary>CJK Supplemental Radical ⻢ (c-simplified horse)</summary>
        <FieldDisplayName("⻢")>
        [SupCSimplifiedHorse] = 312
        '''<summary>CJK Supplemental Radical ⻣ (bone)</summary>
        <FieldDisplayName("⻣")>
        [SupBone] = 313
        '''<summary>CJK Supplemental Radical ⻤ (ghost)</summary>
        <FieldDisplayName("⻤")>
        [SupGhost] = 314
        '''<summary>CJK Supplemental Radical ⻥ (c-simplified fish)</summary>
        <FieldDisplayName("⻥")>
        [SupCSimplifiedFish] = 315
        '''<summary>CJK Supplemental Radical ⻦ (c-simplified bird)</summary>
        <FieldDisplayName("⻦")>
        [SupCSimplifiedBird] = 316
        '''<summary>CJK Supplemental Radical ⻧ (c-simplified salt)</summary>
        <FieldDisplayName("⻧")>
        [SupCSimplifiedSalt] = 317
        '''<summary>CJK Supplemental Radical ⻨ (simplified wheat)</summary>
        <FieldDisplayName("⻨")>
        [SupSimplifiedWheat] = 318
        '''<summary>CJK Supplemental Radical ⻩ (simplified yellow)</summary>
        <FieldDisplayName("⻩")>
        [SupSimplifiedYellow] = 319
        '''<summary>CJK Supplemental Radical ⻪ (c-simplified frog)</summary>
        <FieldDisplayName("⻪")>
        [SupCSimplifiedFrog] = 320
        '''<summary>CJK Supplemental Radical ⻫ (j-simplified even)</summary>
        <FieldDisplayName("⻫")>
        [SupJSimplifiedEven] = 321
        '''<summary>CJK Supplemental Radical ⻬ (c-simplified even)</summary>
        <FieldDisplayName("⻬")>
        [SupCSimplifiedEven] = 322
        '''<summary>CJK Supplemental Radical ⻭ (j-simplified tooth)</summary>
        <FieldDisplayName("⻭")>
        [SupJSimplifiedTooth] = 323
        '''<summary>CJK Supplemental Radical ⻮ (c-simplified tooth)</summary>
        <FieldDisplayName("⻮")>
        [SupCSimplifiedTooth] = 324
        '''<summary>CJK Supplemental Radical ⻯ (j-simplified dragon)</summary>
        <FieldDisplayName("⻯")>
        [SupJSimplifiedDragon] = 325
        '''<summary>CJK Supplemental Radical ⻰ (c-simplified dragon)</summary>
        <FieldDisplayName("⻰")>
        [SupCSimplifiedDragon] = 326
        '''<summary>CJK Supplemental Radical ⻱ (turtle)</summary>
        <FieldDisplayName("⻱")>
        [SupTurtle] = 327
        '''<summary>CJK Supplemental Radical ⻲ (j-simplified turtle)</summary>
        <FieldDisplayName("⻲")>
        [SupJSimplifiedTurtle] = 328
        '''<summary>CJK Supplemental Radical ⻳ (c-simplified turtle)</summary>
        <FieldDisplayName("⻳")>
        [SupCSimplifiedTurtle] = 329

#End Region
    End Enum

    ''' <summary>Provides information and points to CJK Radical</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <DebuggerDisplay("{Number} {RadicalCharacter}")>
    Public Class CjkRadicalInfo
        Implements IXElementWrapper

        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <cjk-radical/>.Name

        Private ReadOnly _element As Xml.Linq.XElement

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeStandardizedVariant"/> class</summary>
        ''' <param name="element">A <see cref="XElement"/> that represents this named sequence</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;standardized-variant> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Sub New(element As Xml.Linq.XElement)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            If element.Name <> elementName Then Throw New ArgumentException(UnicodeResources.ex_InvalidXmlElement.f(elementName))
            _element = element
        End Sub

        ''' <summary>Gets XML element this instance wraps</summary>
        Public ReadOnly Property Element As System.Xml.Linq.XElement Implements IXElementWrapper.Element
            Get
                Return _element
            End Get
        End Property

        ''' <summary>Gets node this instance wraps</summary>
        Private ReadOnly Property Node As System.Xml.Linq.XNode Implements IXNodeWrapper.Node
            Get
                Return Element
            End Get
        End Property

        ''' <summary>Gets radical number as enumerated value</summary>
        ''' <remarks>Names of members of the <see cref="CjkRadical"/> enumeration are just informative. Only what really matters is numeric value.</remarks>
        Public ReadOnly Property Radical As CjkRadical
            Get
                Return Number
            End Get
        End Property

        ''' <summary>Gets radical number as <see cref="Integer"/></summary>
        Public ReadOnly Property Number As Integer
            Get
                Return Integer.Parse(Element.@number.TrimEnd("'"c), InvariantCulture)
            End Get
        End Property

        ''' <summary>Gets value indicating if this radical represents simplified version</summary>
        Public ReadOnly Property IsSimplified As Boolean
            Get
                Return Element.@number.EndsWith("'")
            End Get
        End Property

        ''' <summary>Gets a Code Point that represents this radical</summary>
        Public ReadOnly Property RadicalCodePoint As CodePointInfo
            Get
                Return New CodePointInfo(Element.Document, UInt32.Parse(Element.@radical, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property

        ''' <summary>Gets string that represents this radical</summary>
        ''' <remarks>For non-BMP characters returns 2 characters (surrogate pair)</remarks>
        Public ReadOnly Property RadicalCharacter As String
            Get
                Return Char.ConvertFromUtf32(UInt32.Parse(Element.@radical, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property

        ''' <summary>Gets a Code Point that represents radical associated ideograph</summary>
        Public ReadOnly Property IdeographCodePoint As CodePointInfo
            Get
                Return New CodePointInfo(Element.Document, UInt32.Parse(Element.@ideograph, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property

        ''' <summary>Gets string that represents this radical associated ideograph</summary>
        ''' <remarks>For non-BMP characters returns 2 characters (surrogate pair)</remarks>
        Public ReadOnly Property IdeographCharacter As String
            Get
                Return Char.ConvertFromUtf32(UInt32.Parse(Element.@ideograph, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property
    End Class
End Namespace