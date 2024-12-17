namespace ObjectToJSON.Extensions
{
    public static class StringFormatter
    {
        /// <summary>
        /// Poprawia format stringa na:
        ///  9:00 - 9:45
        ///  9:55 - 10:40
        /// 10:55 - 11:40
        /// </summary>
        public static string ModifyHoursFormat(this string text)
        {
            int index = text.IndexOf("-");
            string formatted = text.Insert(index, " ");

            if (formatted[7] != ' ')
            {
                formatted = formatted.Insert(7, " ");
            }

            return formatted;
        }
        /// <summary>
        /// Wydobywa z listy ucięt string elementu zaczynając od indexu aż do pierwszego napotkanego char'a
        /// </summary>
        public static string GetCuttedTextInHTMLLine(this List<string> lines, int lineNumber, int beginingIndex, char endingChar)
        {
            string text = lines[lineNumber];
            string cutted = text.GetCutted(beginingIndex, endingChar);

            return cutted;
        }
        /// <summary>
        /// Wydobywa ze stringa ucięty tekst zaczynając od indexu aż do pierwszego napotkanego char'a
        /// </summary>
        public static string GetCutted(this string text, int beginingIndex, char endingCharacter)
        {
            string cuttedBegining = text.Substring(
                beginingIndex,
                text.Length - beginingIndex
            );

            string cuttedEnding = cuttedBegining.Substring(0,
                cuttedBegining.IndexOf(endingCharacter));

            return cuttedEnding;
        }

        /// <summary>
        /// Wydobywa ze stringa tekst na podstawie podanych indexów
        /// </summary>
        public static string GetByIndexes(this string text, int start, int end)
        {
            string @string = text.Substring(start, end - start);

            if (@string.Length < 0)
                throw new ArgumentOutOfRangeException("Indeksy są nieprawidłowe");

            return @string;
        }
    }
}
