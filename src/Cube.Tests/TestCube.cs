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
        }
    }
}
