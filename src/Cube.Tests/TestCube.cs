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

            var cubeRollupExecutor = new CubeRollupExecutor<SampleRow>(null, new string[] { "Dim1", "Dim2" }, null);

            var result = cubeRollupExecutor.Rollup(rows);
        }
    }
}
