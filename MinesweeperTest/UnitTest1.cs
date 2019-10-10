using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinesweeperLib;

namespace MinesweeperTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountNeighborsTest()
        {
            MinesweeperModel m = new MinesweeperModel(8);

            var neighbors = m.CountNeighbors(3,4);

            Assert.AreEqual(2, neighbors);
        }

        [TestMethod]
        public void InspectTest()
        {
            MinesweeperModel m = new MinesweeperModel(8);

            var isBomb = m.Inspect(2, 4);

            Assert.AreEqual(true, isBomb);
        }
    }
}