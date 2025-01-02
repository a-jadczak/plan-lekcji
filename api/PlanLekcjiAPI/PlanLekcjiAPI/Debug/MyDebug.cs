using ObjectToJSON.Classes.Anchors;
using ObjectToJSON.Classes.Cells;
using System.Drawing;

namespace PlanLekcjiAPI.Debug
{
    public class MyDebug
    {
        public static void Log(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Log(Dictionary<string, Anchor> dict, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Dictionary: ");
            foreach (var item in dict)
            {
                Console.WriteLine($"\t{item.Key} - ({item.Value.AnchorId} = {item.Value.AnchorText})");
            }
            Console.ResetColor();
        }

        public static void Log(List<ToFixAnchorData> list, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("List: ");
            foreach (var item in list)
            {
                Console.WriteLine($"\t{item.Id}");
            }
            Console.ResetColor();
        }
    }
}
