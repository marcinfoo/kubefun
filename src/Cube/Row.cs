using System;
namespace Cube
{

    public interface IRow
    {



    }

    public class Row
    {
        public Row()
        {
        }

        public int Fact1 { get; set; }
        public int Fact2 { get; set; }
        public int Fact3 { get; set; }
        public int Fact4 { get; set; }

        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }


        public override string ToString()
        {
            return string.Format("[Row: Fact1={0}, Fact2={1}, Fact3={2}, Fact4={3}, Dim1={4}, Dim2={5}, Dim3={6}, Dim4={7}]", Fact1, Fact2, Fact3, Fact4, Dim1, Dim2, Dim3, Dim4);
        }
    }
}
