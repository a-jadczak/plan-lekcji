using ObjectToJSON.Classes.Columns;
using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes
{
    /// <summary>
    /// Przechowuje tablice z dniami i harmonogramem
    /// </summary>
    public class Week
    {
        // Ilość godzin lekcyjnych entity w ciągu tygodnia
        // Czyli ile dany nauczyciel, klasa, czy sala ma godzin zajętych w ciągu tygodnia
        private readonly int _lessonCount;

        // Tablica przechowująca kolumny z numerem oraz godziną
        private readonly ScheduleColumn[] _scheduleColumns;

        // Tablica przechowująca kolumny dni tygodnia
        private readonly LessonsColumn[] _lessonColumns;
        public Week(ScheduleColumn[] scheduleColumns, LessonsColumn[] dayColumns)
        {
            _lessonColumns = dayColumns;
            _scheduleColumns = scheduleColumns;

            _lessonCount = _lessonColumns.Sum(x => x.LessonCount);
        }

        [JsonInclude]
        public int LessonCount => _lessonCount;
        [JsonInclude]
        public ScheduleColumn[] ScheduleColumns => _scheduleColumns;
        [JsonInclude]
        public LessonsColumn[] LessonColumns => _lessonColumns;
    }
}
