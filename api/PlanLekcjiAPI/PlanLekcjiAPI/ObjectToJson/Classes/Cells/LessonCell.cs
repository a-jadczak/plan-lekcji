using ObjectToJSON.Classes.Anchors;
using PlanLekcjiAPI.Debug;
using System;
using System.Text.Json.Serialization;

namespace ObjectToJSON.Classes.Cells
{
    public class LessonCell
    {
        public static List<ToFixAnchorData> toFixAnchorsData = new List<ToFixAnchorData>();
        /// <summary>
        /// Reprezentuje wspołrzędne w planie lekcji
        /// x - poziomo
        /// y - pionowo
        /// </summary>
        private readonly int _x, _y;

        private readonly bool _scheduleGap;

        // W jednej kratce może być podział na grupy więc dlatego lista
        private readonly List<AnchorsRow> _anchorsRows;
        public LessonCell(bool scheduleGap, List<Anchor> anchors, int x, int y, string id)
        {
            _x = x;
            _y = y;
            _scheduleGap = scheduleGap;

            if (anchors == null)
            {
                _anchorsRows = new List<AnchorsRow>();
            }
            else
            {
                _anchorsRows = ToAnchorRows(anchors, id).ToList();
            }
        }

        /// <summary>
        /// To rozdziela anchory na anchorsRows
        /// (troche przekombinowałem)
        /// </summary>
        private IEnumerable<AnchorsRow> ToAnchorRows(List<Anchor> anchors, string id)
        {
            if (anchors.Count == 1)
            {
                Anchor[] t =
                {
                    new Anchor(anchors[0].AnchorId, anchors[0].AnchorText)
                };

                yield return new AnchorsRow(t);
            }
            else
            {
                const int anchorRowLength = 3;
                int temp = 0;
                for (int i = 0; i < anchors.Count; i += anchorRowLength)
                {
                    Anchor[] anchorArr = new Anchor[anchorRowLength];
                    for (int j = 0; j < anchorRowLength; j++)
                    {
                        var anchor = new Anchor(anchors[i + j].AnchorId, anchors[i + j].AnchorText);
                        // Tutaj będzie naprawa niedziałających linków
                        if (anchors[i+j].AnchorText.Contains('#'))
                        {
                            // i + j + 1, bo zawsze następnym elementem będzie link z salą
                            // a _x i _y przekazuje po to by potem na podstawie tych współrzędnych odnaleść nauczyciela za pomocą linku do sali
                            // i oraz j dla indexów aby potem nadpisać te istniejące już anchory
                            int indexY = temp; // Tu dzieli przez 3 bo inkrementuje się co 3
                            int indexX = j;
                            var toFixAnchor = new 
                                ToFixAnchorData(
                                    anchors[i + j+1].AnchorId,
                                    anchors[i + j].AnchorText,
                                    id,
                                    _x, _y, indexX, indexY);

                            toFixAnchorsData.Add(toFixAnchor);
                        }

                        anchorArr[j] = anchor;
                    }
                    temp++;

                    yield return new AnchorsRow(anchorArr);
                }
            }
        }

        [JsonInclude]
        public bool ScheduleGap { get => _scheduleGap; }

        [JsonInclude]
        public List<AnchorsRow> AnchorsRows => _anchorsRows;
    }
}