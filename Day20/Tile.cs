using System.Linq;

namespace AdventOfCode.Day20
{
    internal class Tile
    {
        public long Id { get; set; }
        public int Size { get; set; }

        private readonly string[] _image;
        private int _orientation;

        public Tile(long id, string[] image)
        {
            Id = id;
            _image = image;
            Size = image.Length;
        }

        public char this[int row, int col]
        {
            get
            {
                for (var i = 0; i < _orientation % 4; i++)
                    (row, col) = (col, Size - 1 - row); // rotate
                if (_orientation % 8 >= 4)
                    col = Size - 1 - col; // flip vertical axis
                return _image[row][col];
            }
        }

        public string Row(int row) => GetSlice(row, 0, 0, 1);
        public string Col(int col) => GetSlice(0, col, 1, 0);
        public string Top() => Row(0);
        public string Left() => Col(0);
        public string Bottom() => Row(Size - 1);
        public string Right() => Col(Size - 1);
        public void ChangeOrientation() => _orientation++;
        public override string ToString() => string.Join("\n", Enumerable.Range(0, Size).Select(Row));

        private string GetSlice(int row, int col, int rowAdd, int colAdd)
        {
            var slice = "";
            for (var i = 0; i < Size; i++, row += rowAdd, col += colAdd)
                slice += this[row, col];
            return slice;
        }
    }
}