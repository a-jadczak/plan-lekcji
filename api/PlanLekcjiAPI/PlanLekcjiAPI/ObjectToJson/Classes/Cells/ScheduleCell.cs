using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes.Cells
{
    public class ScheduleCell
    {
        private readonly string _text;
        public ScheduleCell(string text)
        {
            _text = text;
        }

        [JsonInclude]
        public string Text => _text;

        public override string ToString() => _text;
    }
}
