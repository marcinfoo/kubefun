using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using Cube;

namespace Cube.Tests
{
    [TestClass]
    public class TestCube
    {
        [TestMethod]
        [Ignore]
        public void TestRandomDataGen()
        {
            var rows = TestUtils.GenerateSampleRows(10);
            Assert.IsTrue(rows.ToList().Count() == 10);
        }

        [TestMethod]
        public void TestStaticDataRollup()
        {
            var rows = TestUtils.GetStaticSampleRows();

            Func<IEnumerable<SampleRow>, SampleRow> aggregator = (rs) =>
            {


                var fact1 = rs.Select(x => x.Fact1).Sum();
                var fact2 = rs.Select(x => x.Fact2).Sum();
                var fact3 = rs.Select(x => x.Fact3).Sum();

                var row = new SampleRow()
                {
                    Fact1 = fact1,
                    Fact2 = fact2,
                    Fact3 = fact3
                };

                return row;

            };

            var cubeRollupExecutor = new CubeRollupExecutor<SampleRow>(null, new string[] { "Dim1", "Dim2" }, aggregator);

            var result = cubeRollupExecutor.Rollup(rows);

            Assert.IsTrue(result.RollupItem.Fact1 == 7);
            Assert.IsTrue(result.RollupItem.Fact2 == 14);
            Assert.IsTrue(result.RollupItem.Fact3 == 21);
            Assert.IsTrue(result.Count == 7);

            // A
            var dim1Result = result.SubGroups.First(g=>g.Key == "A");
            Assert.AreEqual(dim1Result.Key, "A");
            Assert.IsTrue(dim1Result.RollupItem.Fact1 == 4);
            Assert.IsTrue(dim1Result.RollupItem.Fact2 == 8);
            Assert.IsTrue(dim1Result.RollupItem.Fact3 == 12);
            Assert.IsTrue(dim1Result.Count == 4);

            // A B
            var dim2Result = dim1Result.SubGroups.First(g=>g.Key == "B");
            Assert.AreEqual(dim2Result.Key, "B");
            Assert.IsTrue(dim2Result.RollupItem.Fact1 == 1);
            Assert.IsTrue(dim2Result.RollupItem.Fact2 == 2);
            Assert.IsTrue(dim2Result.RollupItem.Fact3 == 3);
            Assert.IsTrue(dim2Result.Count == 1);

        }
    }
}
