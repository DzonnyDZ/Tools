using System;
using System.Collections.Generic;

namespace DMKSoftware.CodeGenerators.Tools
{
    public static class FormatValidator
    {
        private const string FormatInvalidString = "Input string was not in a correct format.";
        private const string FormatIndexOutOfRange = "Index (zero based) must be greater than or equal to zero and less than the size of the argument list.";

        public static int Parse(string format)
        {
            if (null == format)
                throw new ArgumentNullException("format");

            char[] formatChars = format.ToCharArray(0, format.Length);
            int cursor = 0;
            char currChar = '\0';
            List<int> argumentIndexes = new List<int>();

            while (true)
            {
                int supplementaryCursor = cursor;
                while (cursor < formatChars.Length)
                {
                    currChar = formatChars[cursor];
                    cursor++;
                    if (currChar == '}')
                    {
                        if ((cursor < formatChars.Length) && (formatChars[cursor] == '}'))
                            cursor++;
                        else
                        {
                            FormatError();
                        }
                    }
                    if (currChar == '{')
                    {
                        if ((cursor < formatChars.Length) && (formatChars[cursor] == '{'))
                            cursor++;
                        else
                        {
                            cursor--;
                            break;
                        }
                    }
                    formatChars[supplementaryCursor++] = currChar;
                }

                if (cursor == formatChars.Length)
                {
                    // Checking the argument list
                    argumentIndexes.Sort();

                    for (int argumentIndex = 0; argumentIndex < argumentIndexes.Count;
                        ++argumentIndex)
                        if (argumentIndex != argumentIndexes[argumentIndex])
                            throw new FormatException(FormatIndexOutOfRange);

                    return argumentIndexes.Count;
                }

                // Checking the first index letter
                cursor++;
                if (((cursor == formatChars.Length) || ((currChar = formatChars[cursor]) < '0')) || (currChar > '9'))
                    FormatError();

                // Getting the index
                int index = 0;
                do
                {
                    index = ((index * 10) + currChar) - 0x30;
                    cursor++;
                    if (cursor == formatChars.Length)
                        FormatError();

                    currChar = formatChars[cursor];
                }
                while (((currChar >= '0') && (currChar <= '9')) && (index < 0xf4240));

                // Storing the index
                if (!argumentIndexes.Contains(index))
                    argumentIndexes.Add(index);

                while ((cursor < formatChars.Length) && ((currChar = formatChars[cursor]) == ' '))
                    cursor++;

                // Getting the alignment
                int alignment = 0;
                if (currChar == ',')
                {
                    cursor++;
                    while ((cursor < formatChars.Length) && (formatChars[cursor] == ' '))
                        cursor++;

                    if (cursor == formatChars.Length)
                        FormatError();

                    currChar = formatChars[cursor];
                    if (currChar == '-')
                    {
                        cursor++;
                        if (cursor == formatChars.Length)
                            FormatError();

                        currChar = formatChars[cursor];
                    }

                    if ((currChar < '0') || (currChar > '9'))
                        FormatError();

                    do
                    {
                        alignment = ((alignment * 10) + currChar) - 0x30;
                        cursor++;
                        if (cursor == formatChars.Length)
                            FormatError();

                        currChar = formatChars[cursor];
                    }
                    while (((currChar >= '0') && (currChar <= '9')) && (alignment < 0xf4240));
                }

                while ((cursor < formatChars.Length) && ((currChar = formatChars[cursor]) == ' '))
                    cursor++;

                // object obj1 = args[index];
                string formatString = null;
                if (currChar == ':')
                {
                    cursor++;
                    int formatStringBeginning = cursor;
                    supplementaryCursor = cursor;
                    while (true)
                    {
                        if (cursor == formatChars.Length)
                            FormatError();

                        currChar = formatChars[cursor];
                        cursor++;
                        if (currChar == '{')
                        {
                            if ((cursor < formatChars.Length) && (formatChars[cursor] == '{'))
                                cursor++;
                            else
                                FormatError();
                        }
                        else if (currChar == '}')
                        {
                            if ((cursor < formatChars.Length) && (formatChars[cursor] == '}'))
                                cursor++;
                            else
                            {
                                cursor--;
                                break;
                            }
                        }

                        formatChars[supplementaryCursor++] = currChar;
                    }
                    if (supplementaryCursor > formatStringBeginning)
                        formatString = new string(formatChars, formatStringBeginning, supplementaryCursor - formatStringBeginning);
                }

                if (currChar != '}')
                    FormatError();

                cursor++;
            }
        }

        private static void FormatError()
        {
            throw new FormatException(FormatInvalidString);
        }
    }
}
