using ObjectToJSON.Classes;
using ObjectToJSON.Classes.Anchors;
using ObjectToJSON.Classes.Cells;
using PlanLekcjiAPI.Debug;

namespace PlanLekcjiAPI.ObjectToJson
{
    public class TeacherAnchorFixing
    {
        public static Dictionary<string, Anchor> toFixDictionary = new Dictionary<string, Anchor>();
        
        /// <summary>
        /// Loopuje
        /// </summary>
        public static void FixingProcess(LessonPlan lessonPlan)
        {
            MyDebug.Log(LessonCell.toFixAnchorsData.Count.ToString(), ConsoleColor.Magenta);
            
            // Przypisywanie prawidłowych anchorow do nieprawidłowych anchorow nauczycieli
            foreach (var item in LessonCell.toFixAnchorsData)
            {
                GetToFixTeacherAnchor(item, lessonPlan.Entities);

                FixBrokenTeacherAnchor(item, lessonPlan.Entities);
                MyDebug.Log($"{item.BrokenId}", ConsoleColor.Magenta);
            }
        }
        private static void GetToFixTeacherAnchor(ToFixAnchorData toFixAnchorData, List<Entity> entities)
        {
            var foundEntity = entities.FirstOrDefault(entity => entity.Id == toFixAnchorData.Id);


            var teacherAnchor = foundEntity?.Week.LessonColumns[toFixAnchorData.X]
                .Lessons[toFixAnchorData.Y]
                .AnchorsRows[0]
                .Anchors[0];

            Console.WriteLine($"Obecny: {toFixAnchorData.Id}");

            if (teacherAnchor != null)
            {
                Console.WriteLine($"Nadpisujące: {teacherAnchor.AnchorText} {teacherAnchor.AnchorId}");
            }
            else
            {
                Console.WriteLine($"Trafił się null");
            }

            if (!toFixDictionary.ContainsKey(toFixAnchorData.BrokenId) && teacherAnchor != null)
            {
                var anchor = new Anchor(teacherAnchor.AnchorId, teacherAnchor.AnchorText);
                toFixDictionary.Add(toFixAnchorData.BrokenId, anchor);
            }
        }
        private static void FixBrokenTeacherAnchor(ToFixAnchorData anchorData, List<Entity> entities)
        {
            var foundEntity = entities.FirstOrDefault(entity => entity.Id == anchorData.OddzialId);

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
                    Console.WriteLine($"Klucz ({fixedAnchor?.AnchorText}) nie istnieje.");
                }

            }
        }
    }
}
