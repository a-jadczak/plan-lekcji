namespace ObjectToJSON.Classes.Cells
{
    /// <summary>
    /// Troche skomplikowane ale:
    /// x, y - odpowiadają za współrzędne danej kolumny aby potem naprawić link. Działa to na zasadzie że przy zepsutym linku jest zawsze link do sali w której ma dany nauczyciel. Program przechodzi do linku z sali i za pomocą współrzędnych szuka tego nauczyciela który ma tam obecnie.
    /// </summary>
    public class ToFixAnchorData
    {
        private readonly string _id, _oddzialId, _brokenId;
        private readonly int _x, _y, _indexX, _indexY;
        public ToFixAnchorData(string id, string brokenId, string oddzialId, int x, int y, int indexX, int indexY)
        {
            _brokenId = brokenId;
            _oddzialId = oddzialId;
            _x = x;
            _y = y;
            _id = id;
            _indexX = indexX;
            _indexY = indexY;
        }
        public string Id => _id; // Id sali z której zczyta kto o danej godzinie ma jakiego nauczyciuela
        public string OddzialId => _oddzialId; // Id odzialu z której napdpisze zepsute linki
        public string BrokenId => _brokenId;
        public int X => _x;
        public int Y => _y;
        public int IndexX => _indexX;
        public int IndexY => _indexY;
    }
}