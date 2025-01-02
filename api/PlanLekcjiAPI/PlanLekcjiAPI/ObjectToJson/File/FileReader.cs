using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectToJSON.FileReader
{
    public class FileReader
    {
        public static IEnumerable<string> FileToList(string path)
        {
            StreamReader streamReader = new StreamReader(path);

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line != null)
                {
                    yield return line.Trim();
                }
                else
                {
                    Console.WriteLine($"Null {line}");
                }
            }

            streamReader.Close();
        }
    }
}
