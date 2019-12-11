using System;
using decision_making;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTests
{
    [TestClass]
    public class UnitKopland
    {
        [TestMethod]
        public void TestKopland()
        {
            Data.alts = 4;
            Data.experts = 5;
            int[,] a = { { 1, 2, 3, 4 }, { 1, 3, 2, 4 }, { 2, 3, 4, 1 },
                { 4, 2, 3, 1 }, { 3, 4, 2, 1 }, };
            int[] experted = { -3, 3, 3, -3 };

            models koplandtest = new models();
          int[]  actual = koplandtest.kopland(a);
            CollectionAssert.AreEqual(experted,actual);
        }
    }
}
