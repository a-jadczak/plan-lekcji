using ObjectToJSON.Classes.Columns;
using ObjectToJSON.Classes.Cells;
using ObjectToJSON.Extensions;
using ObjectToJSON.Classes.Anchors;

namespace ObjectToJSON.HTMLReader
{
    public static class HTMLReader
    {
        /// <summary>
        /// UWAGA: Każda linia czy wiersz kodu HTML musi być odjęta o 1 bo numerowanie zaczyna się od zera
        /// </summary>


        /// <summary>
        /// Co 9 linia w HTML odpowiada poniżeszej kratce w tabeli
        /// Ex:
        ///     13 <td class="nr">12</td>
        ///     22 <td class="nr">13</td>
        /// </summary>
        private const int ITERATOR = 9;

        /// <summary>
        /// Maksymalna ilość jaką może pętla zczytująca lekcje może się wykonać
        /// Ta zmienna jest zamiast sprawdzania breaklineów bo dla każdego dnia tygodnia breakline ma inną składnie stringa.
        /// 
        /// Iterator jest przypisywany w metodzie GetScheduleNumbers raz dla każdego pliku ponieważ dla kolumny numbers ma najkrótszy i najbardziej rzetelny breakpoint. "</td></tr>"
        /// </summary>
        private static int maxIterator = 0;
        public static string GetTitle(this List<string> lines)
        {
            const int LINE_NUMBER = 13;
            const int BEGINING_INDEX = 25;
            const char ENDING_CHAR = '<';

            string cuttedTitle = lines.GetCuttedTextInHTMLLine(LINE_NUMBER, BEGINING_INDEX, ENDING_CHAR);

            return cuttedTitle;
        }
        public static string GetInitial(this List<string> lines)
        {
            const int LINE_NUMBER = 13;
            const int BEGINING_INDEX = 34;

            // Wartość Iniciałów jest zawsze 2 cyfrowa czyli stała więc nie potrzeba dodawać logiki obliczającej długość
            string initial = lines[LINE_NUMBER].Substring(
                BEGINING_INDEX,
                2
            );


            return initial;
        }
       
        public static ScheduleColumn GetScheduleNumbers(this List<string> lines)
        {
            const int LINE_NUMBER = 28;
            const int BEGINING_INDEX = 15;
            const char ENDING_CHAR = '<';

            const string breakLine = "</td></tr>";

            var scheduleCells = new List<ScheduleCell>(); 

            // Loop co 9 linie
            for (int i = LINE_NUMBER; i < lines.Count; i += ITERATOR)
            {
                // Sprawdzanie czy już nie wyjechał poza plan lekcji
                if (lines[i] == breakLine)
                {
                    // Przypisanie Maksymalnego iteratora dla pliku
                    maxIterator = i;
                    break;
                }
                
                string text = lines[i].GetCutted(BEGINING_INDEX, ENDING_CHAR);

                scheduleCells.Add(new ScheduleCell(text));
            }
            
            var scheduleColumn = new ScheduleColumn(scheduleCells);

            return scheduleColumn;
        } 

        public static ScheduleColumn GetScheduleHours(this List<string> lines)
        {
            const int LINE_NUMBER = 29;
            const int BEGINING_INDEX = 14;
            const char ENDING_CHAR = '<';

            var scheduleCells = new List<ScheduleCell>();

            // Loop co 9 linie
            for (int i = LINE_NUMBER; i < maxIterator; i += ITERATOR)
            {
                string text = lines[i].GetCutted(BEGINING_INDEX, ENDING_CHAR);

                string formattedText = text.ModifyHoursFormat();

                scheduleCells.Add(new ScheduleCell(formattedText));
            }

            var scheduleColumn = new ScheduleColumn(scheduleCells);

            return scheduleColumn;
        }

        public static LessonsColumn[] GetLessonColumn(this List<string> lines, string id)
        {
            // Dni roboczych jest 5
            const int SIZE = 5;
            LessonsColumn[] lessonColumns = new LessonsColumn[SIZE];

            // Wypełnianie kolumny dni
            // Po każdym wykonaniu pętli zmienia się kolumna wypełniania np. z poniedziałku na wtorek
            for (int i = 0; i < SIZE; i++)
            {
                lessonColumns[i] = lines.GetLessons(i, id);
            }

            return lessonColumns;
        }

        /// <summary>
        /// lines - lista zczytanego plika
        /// lineIterator - przesuwa zaczynającą linijke 30 o X lini. UWAGA TYLKO W ZAKRESIE (0, 4)
        /// 
        /// </summary>
        private static LessonsColumn GetLessons(this List<string> lines, int lineIterator, string id)
        {
            // 30 - to startowa pozycja od poniedzialku
            int LINE_NUMBER = 30 + lineIterator;

            var lessonCells = new List<LessonCell>();

            int y = 0;

            // Loop co 9 linie
            for (int i = LINE_NUMBER; i < maxIterator; i += ITERATOR)
            {
                string line = lines[i];
                int x = lineIterator;
                LessonCell lessonCell = GetLessonsCell(line, x, y, id);

                lessonCells.Add(lessonCell);
                y++;
            }

            LessonsColumn lessonsColumn = new LessonsColumn(lessonCells);

            return lessonsColumn;
        }

        /// <summary>
        /// x - przypisuje współrzędną X kratki
        /// </summary>
        private static LessonCell GetLessonsCell(string line, int x, int y, string id)
        {
            LessonCell lessonCell;

            // Spradza czy jest okienko
            if (line.Contains("&nbsp;"))
            {
                lessonCell = new LessonCell(true, null, x, y, id);
                return lessonCell;
            }

            List<Anchor> anchors = new List<Anchor>();

            // Wycięcie <td> </td>
            string cuttedTdLine = SeparateFromTd(line);
            // Separaca " <"
            string[] separatedLine = cuttedTdLine.Split(" <");

            //
            List<string> finalSeparated = new List<string>();

            // Jeszcze trzeba zseparatować <br><span aby zapobiec nie wyświetlaniu się kolejnych nazw przedmiotu
            foreach (var item in separatedLine)
            {
                string[] newSeparated = item.Split("<br><span ");

                for (int i = 0; i < newSeparated.Length; i++)
                {
                    finalSeparated.Add(newSeparated[i]);
                }
            }

            // Dla niektórych lekcji jest tylko sama nazwa lekcji np. edb; Więc dlatego nie potrzeba szukac linkow
            if (separatedLine.Length == 1)
            {
                anchors.Add(new Anchor("", cuttedTdLine));
            }
            else
            {
                // Wydobycie linków i nazwy dla każdego separatowanego elementu (jeśli to możliwe)
                foreach (var item in finalSeparated)
                {
                    string name = item.GetName();
                    string hrefId = item.GetHrefId();

                    anchors.Add(new Anchor(hrefId, name));
                }
            }

            lessonCell = new LessonCell(false, anchors, x, y, id);

            return lessonCell;
            
        }

        private static string SeparateFromTd(string line)
        {
            string newLine = line.Replace(@"<td class=""l"">", "").Replace(@"</td>", "");
            
            return newLine;
        }

        /// <summary>
        /// Wydobywa z separatowanej lini nazwe nauczyciela, pracowni, klasy
        /// </summary>
        private static string GetName(this string line)
        {
            int index = line.IndexOf("class=");
            string beginingCuttedLine = line.Substring(index, line.Length - index);
            int indexOfEndMark = beginingCuttedLine.IndexOf("</");

            string name = beginingCuttedLine.GetByIndexes(10, indexOfEndMark);

            // dla np. zaj. wych.
            string[] separated = name.Split(" ");
            string fullName = String.Concat(separated);
            
            return fullName;
        }

        /// <summary>
        /// Wydobywa z separatowanej lini id nauczyciela, pracowni, klasy
        /// </summary>
        private static string GetHrefId(this string line)
        {
            int index = line.IndexOf("class=");

            string id = line.GetPreviousHrefId(index);

            return id;
        }

        /// <summary>
        /// Szuka i zwraca najbliższego hrefa z tyłu w zależności od zaczętego indexu
        /// </summary>
        private static string GetPreviousHrefId(this string line, int startIndex)
        {
            int hrefIndex = -1;
            int i = 0;
            string id = "";
            while (startIndex - i > -1)
            {
                string href = line.Substring(startIndex - i, 2);
                if (href.Equals("f="))
                {
                    // Tutaj jest wycinanie id ze stringa (nie polecam)
                    hrefIndex = startIndex - i;

                    string fullPart = line.Substring(hrefIndex + 3, line.Length - startIndex);
                    int indexOfDot = fullPart.IndexOf("."); // for .html

                    id = fullPart.Substring(0, indexOfDot);

                    break;
                }
                i++;
            }

           
            return id;
        }

    }
}
