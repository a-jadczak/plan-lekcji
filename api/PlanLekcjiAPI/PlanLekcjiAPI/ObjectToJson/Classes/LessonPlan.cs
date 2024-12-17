using ObjectToJSON.Classes.Anchors;
using ObjectToJSON.Classes.Cells;
using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes
{
    public class LessonPlan
    {
        public static Dictionary<string, Anchor> toFixDictionary = new Dictionary<string, Anchor>();

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

        public Anchor? GetToFixTeacherAnchor(ToFixAnchorData toFixAnchorData)
        {
            var foundEntity = _entities.FirstOrDefault(entity => entity.Id == toFixAnchorData.Id);


            var teacherAnchor = foundEntity?.Week.LessonColumns[toFixAnchorData.X]
                .Lessons[toFixAnchorData.Y]
                .AnchorsRows[0]
                .Anchors[0];
            //.FirstOrDefault(anchor => 
            //     anchor.AnchorType == AnchorType.Teacher);

            Console.WriteLine(toFixAnchorData.Id);
            if (teacherAnchor != null ) 
            Console.WriteLine($"{teacherAnchor.AnchorText} {teacherAnchor.AnchorId}");

            if (!toFixDictionary.ContainsKey(toFixAnchorData.BrokenId) && teacherAnchor != null)
            {
                var anchor = new Anchor(teacherAnchor.AnchorId, teacherAnchor.AnchorText);
                toFixDictionary.Add(toFixAnchorData.BrokenId, anchor);
            }

            return teacherAnchor;
        }
        public void FixBrokenTeacherAnchor(ToFixAnchorData anchorData)
        {
            var foundEntity = _entities.FirstOrDefault(entity => entity.Id == anchorData.OddzialId);

            if (foundEntity != null)
            {
                var brokenAnchor = foundEntity.Week
                    .LessonColumns[anchorData.X]
                    .Lessons[anchorData.Y]
                    .AnchorsRows[anchorData.IndexY]
                    .Anchors[anchorData.IndexX];


                if (toFixDictionary.TryGetValue(brokenAnchor.AnchorText, out var fixedAnchor))
                {
                    foundEntity.Week
                        .LessonColumns[anchorData.X]
                        .Lessons[anchorData.Y]
                        .AnchorsRows[anchorData.IndexY]
                        .Anchors[anchorData.IndexX] = fixedAnchor;
                }
                else
                {
                    Console.WriteLine($"Klucz {fixedAnchor?.AnchorText} nie istnieje.");
                }

            }

        }

        public void OverrideBrokenTeacherAnchor(Anchor newAnchor, ToFixAnchorData anchorData)
        {
            var foundEntity = _entities.FirstOrDefault(entity => entity.Id == anchorData.Id);

            Console.WriteLine($"{anchorData.IndexX} - {anchorData.IndexY}");
            if (foundEntity != null)
            {
                var xd = foundEntity.Week
                    .LessonColumns[anchorData.X]
                    .Lessons[anchorData.Y];

                var xd2 = xd
                    .AnchorsRows[anchorData.IndexY]
                    .Anchors[anchorData.IndexX];

                //Console.WriteLine($"{xd.AnchorText} {xd.AnchorId}");
                //xd = newAnchor;
            }
        }
    }
}
