using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes
{
    /// <summary>
    /// Entity reprezentuje jakąś jednostke czyli:
    /// - Nauczyciela
    /// - Klase
    /// - Sale
    /// </summary>
    public class Entity
    {
        private readonly string _id;
        // Tytuł jednostki: nazwa nauczyciela, klasy ...
        private readonly string _title;

        private readonly Week _week;

        public Entity(string title, string id, Week week)
        {
            _title = title;
            _id = id;
            _week = week;
        }

        [JsonInclude]
        public string Id => _id;

        [JsonInclude]
        public string Title => _title;

        [JsonInclude]
        public Week Week => _week;
    }
}
