using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectToJSON.File
{
    public class StartEndIndexes
    {
        private readonly int _start;
        private readonly int _end;

        public StartEndIndexes(int start, int end)
        {
            _start = start;
            _end = end;
        }

        //  "<x, y)"
        public int Start { get => _start; }
        public int End { get => _end; }

        public static StartEndIndexes GetIndexesBy(string line, int begining, char endingCharacter)
        {
            int ending = line.IndexOf(endingCharacter);

            var xd = line[ending];

            return new StartEndIndexes(begining, ending);
        }

    }
}
