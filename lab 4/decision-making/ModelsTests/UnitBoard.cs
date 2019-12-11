using System;
using decision_making;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTests
{
    [TestClass]
    public class UnitBoard
    {
        [TestMethod]
        public void TestBoard()
        {
            Data.alts = 4;
            Data.experts = 5;
            int[,] a = { { 1, 2, 3, 4 }, { 1, 3, 2, 4 }, { 2, 3, 4, 1 }, 
                { 4, 2, 3, 1 }, { 3, 4, 2, 1 }, };
            int[] experted = {11,14,14,11};

            models boardtest = new models();
            CollectionAssert.AreEqual(experted, boardtest.board(a));
        }
    }
}
