using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes.Anchors
{
    /// <summary>
    /// Aby w UI lepiej ułożyć elementy
    /// </summary>
    public enum AnchorType
    {
        SubjectName = 0,
        Class = 1,
        Teacher = 2,
        Classroom = 3,
    }
    /// <summary>
    /// Przechowuje Id oraz Text
    /// Id - dla linku
    /// Text - Nazwa przedmiotu, sali, czy iniciał nauczyciela
    /// </summary>
    public class Anchor
    {
        private static Dictionary<char, AnchorType> keyValuePairs = new Dictionary<char, AnchorType>()
        {
            { 'n', AnchorType.Teacher },
            { 'o', AnchorType.Class },
            { 's', AnchorType.Classroom }
        };

        private readonly string _anchorId;
        private readonly string _anchorText;

        private readonly AnchorType _anchorType;

        public Anchor(string anchorId, string anchorText)
        {
            _anchorId = anchorId;
            _anchorText = anchorText;

            _anchorType = IdentifyCategory();
        }

        [JsonInclude]
        public string AnchorId => _anchorId;
        [JsonInclude]
        public string AnchorText => _anchorText;
        [JsonInclude]
        public AnchorType AnchorType => _anchorType;

        public override string ToString() => $"{_anchorText} id: {_anchorId}";

        private AnchorType IdentifyCategory()
        {
            if (_anchorId.Length == 0)
                return AnchorType.SubjectName;

            return keyValuePairs[_anchorId[0]];
        }
    }
}
