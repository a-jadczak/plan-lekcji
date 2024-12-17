using ObjectToJSON.Classes.Cells;
using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes.Columns
{
    public class LessonsColumn
    {
        private readonly int _lessonCount = 0;
        private readonly List<LessonCell> _lessons;
        public LessonsColumn(List<LessonCell> lessons)
        {
            _lessons = lessons;
            _lessonCount = CalcLessonCount();
        }
        [JsonInclude]
        public int LessonCount => _lessonCount;

        [JsonInclude]
        public List<LessonCell> Lessons => _lessons;

        public override string ToString()
        {
            string full = string.Empty;
            foreach (var item in _lessons)
            {
                full += item.ToString();
            }

            return full;
        }

        private int CalcLessonCount()
        {
            int sum = 0;
            foreach (var item in _lessons)
            {
                if (!item.ScheduleGap)
                {
                    sum++;
                }
            }

            return sum;
        }

    }
}