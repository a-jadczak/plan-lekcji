using ObjectToJSON.Classes.Cells;
using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes.Columns
{
    public class ScheduleColumn
    {
        private readonly List<ScheduleCell> _cells;
        public ScheduleColumn(List<ScheduleCell> cells)
        {
            _cells = cells;
        }

        [JsonInclude]
        public List<ScheduleCell> Cells => _cells;

        public void Write()
        {
            foreach (ScheduleCell cell in _cells)
            {
                Console.WriteLine($"El: *{cell.ToString()}*");
            }
        }
    }
}
