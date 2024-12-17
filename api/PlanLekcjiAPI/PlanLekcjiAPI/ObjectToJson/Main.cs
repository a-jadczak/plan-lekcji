using ObjectToJSON.Classes.Cells;
using ObjectToJSON.Classes.Columns;
using ObjectToJSON.Classes;
using ObjectToJSON.FileReader;
using System.Text.Json;
using ObjectToJSON.HTMLReader;

namespace PlanLekcjiAPI.ObjectToJson
{
    public class Main
    {
        public static void StartConvertingToJson(string filesPath, string targetPath)
        {
            string[] files = Directory.GetFiles(filesPath);

            var entities = new List<Entity>();

            foreach (string path in files)
            {
                List<string> fileLines = FileReader.FileToList(path).ToList();

                string title = fileLines.GetTitle().Trim();
                string id = Path.GetFileNameWithoutExtension(path);

                var scheduleNumbers = fileLines.GetScheduleNumbers();
                var scheduleHours = fileLines.GetScheduleHours();

                ScheduleColumn[] scheduleColumns = new ScheduleColumn[2]
                {
                    scheduleNumbers,
                    scheduleHours
                };

                //// Kolumny dni tygodnia: pon, wt, śr ...
                LessonsColumn[] lessonColumns = fileLines.GetLessonColumn(id);

                var week = new Week(scheduleColumns, lessonColumns);
                var entity = new Entity(title, id, week);

                entities.Add(entity);
            }

            LessonPlan lessonPlan = new LessonPlan(DateTime.Now, entities);

            // Przypisywanie prawidłowych anchorow do nieprawidłowych anchorow nauczycieli
            foreach (var item in LessonCell.toFixAnchorsData)
            {
                var fixedAnchor = lessonPlan.GetToFixTeacherAnchor(item);

                //lessonPlan.OverrideBrokenTeacherAnchor(fixedAnchor, item);
                lessonPlan.FixBrokenTeacherAnchor(item);
            }


            string jsonString = JsonSerializer.Serialize(lessonPlan);

            string jsonPath = $@"{targetPath}\data.json";

            File.WriteAllText(jsonPath, jsonString);

            // Czyszczenie obiektów i zmiennych
            files = [];
            entities.Clear();
            lessonPlan = null;
            LessonPlan.toFixDictionary.Clear();
        }
    }
}
