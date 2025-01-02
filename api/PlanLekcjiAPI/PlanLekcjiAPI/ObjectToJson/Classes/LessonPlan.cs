using ObjectToJSON.Classes.Anchors;
using ObjectToJSON.Classes.Cells;
using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes
{
    /// <summary>
    /// Klasa reprezentuje wszystkie plany leckji jednostek
    /// </summary>
    public class LessonPlan
    {
        private readonly string _lastModified;
        private readonly List<Entity> _entities;

        public LessonPlan(DateTime lastModified, List<Entity> entities)
        {
            _lastModified = lastModified.ToString("dd-MM-yyyy");
            _entities = entities;
        }

        [JsonInclude]
        public string LastModified => _lastModified;
        [JsonInclude]
        public List<Entity> Entities => _entities;
    }
}
