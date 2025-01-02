using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes.Anchors
{
    /// <summary>
    /// Może się zdarzyć że jest podział na grupy i w jednej kolumnie mogą być kilka zajęć
    /// </summary>
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
