using System;
namespace Cube.Tests
{
    public class SampleRow
    {
        public int Fact1 { get; set; }
        public int Fact2 { get; set; }
        public int Fact3 { get; set; }

        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }

        public SampleRow()
        {
       
        }

        public override string ToString()
        {
            return string.Format("[SampleRow: Fact1={0}, Fact2={1}, Fact3={2}, Dim1={3}, Dim2={4}, Dim3={5}]", Fact1, Fact2, Fact3, Dim1, Dim2, Dim3);
        }
    }
}
