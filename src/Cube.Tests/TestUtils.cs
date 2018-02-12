using System;
using System.Collections.Generic;

namespace Cube.Tests
{
    public static class TestUtils
    {
        public static IEnumerable<SampleRow> GenerateSampleRows(int numOfRows){
            
            for (int i = 0; i < numOfRows; i++){

                yield return new SampleRow()
                {
                    Fact1 = 1,
                    Fact2 = 2,
                    Fact3 = 3,
                    Dim1 = "A",
                    Dim2 = "B",
                    Dim3 = "C"
                };
            }
        }

        public static IEnumerable<SampleRow> GetStaticSampleRows(){

            IList<SampleRow> rows = new List<SampleRow>(){
                new SampleRow(){ Fact1=1, Fact2=2, Fact3=3, Dim1="A", Dim2="BB", Dim3="CC" },
                new SampleRow(){ Fact1=1, Fact2=2, Fact3=3, Dim1="A", Dim2="BB", Dim3="C" },
                new SampleRow(){ Fact1=1, Fact2=2, Fact3=3, Dim1="AA", Dim2="B", Dim3="CCC" },
                new SampleRow(){ Fact1=1, Fact2=2, Fact3=3, Dim1="AA", Dim2="BB", Dim3="C" },
                new SampleRow(){ Fact1=1, Fact2=2, Fact3=3, Dim1="A", Dim2="BB", Dim3="C" },
                new SampleRow(){ Fact1=1, Fact2=2, Fact3=3, Dim1="AA", Dim2="B", Dim3="CC" },
                new SampleRow(){ Fact1=1, Fact2=2, Fact3=3, Dim1="A", Dim2="B", Dim3="CCC" }
            };


            return rows;


        }

    }
}
