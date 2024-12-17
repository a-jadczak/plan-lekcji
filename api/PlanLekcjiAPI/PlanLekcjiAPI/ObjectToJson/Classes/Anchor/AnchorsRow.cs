using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes.Anchors
{
    public class AnchorsRow
    {
        private Anchor[] _anchors;

        public AnchorsRow(Anchor[] anchors)
        {
            _anchors = anchors;
        }

        [JsonInclude]
        public Anchor[] Anchors { get => _anchors; set => _anchors = value; }
    }
}
