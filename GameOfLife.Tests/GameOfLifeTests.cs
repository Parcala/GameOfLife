using NUnit.Framework;
using System.Net;

namespace GameOfLife.Tests
{
    public class GameOfLifeTests
    {
        public static bool[,] testGridRule1 =
        {
            { false, false, true, false, false },
            { false, false, true, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false }
        };

        public static bool[,] ExpectedGridRule1 =
        {
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false }
        };

        public static bool[,] testGridRule2 =
        {
            { true, true, true, true, true },
            { true, true, true, true, true },
            { true, true, true, true, true },
            { true, true, true, true, true },
            { true, true, true, true, true }
        };

        public static bool[,] ExpectedGridRule2 =
        {
            { true, false, false, false, true },
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false },
            { true, false, false, false, true }
        };

        public static bool[,] testGridRule3 =
        {
            { false, false, true, false, false },
            { false, true, false, true, false },
            { false, true, false, true, false },
            { false, false, true, false, false },
            { false, false, false, false, false }
        };

        public static bool[,] ExpectedGridRule3 =
        {
            { false, false, true, false, false },
            { false, true, false, true, false },
            { false, true, false, true, false },
            { false, false, true, false, false },
            { false, false, false, false, false }
        };

        public static bool[,] testGridRule4 =
        {
            { false, true, true, false, false },
            { false, true, false, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false }
        };

        public static bool[,] ExpectedGridRule4 =
        {
            { false, true, true, false, false },
            { false, true, true, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, false, false, false, false }
        };

        [Test]
        [Category("Rules")]
        public void TestRule1()
        {
            GameOfLife game = new GameOfLife();
            Assert.AreEqual(ExpectedGridRule1, game.LifeGenerator(testGridRule1));
        }

        [Test]
        [Category("Rules")]
        public void TestRule2()
        {
            GameOfLife game = new GameOfLife();
            Assert.AreEqual(ExpectedGridRule2, game.LifeGenerator(testGridRule2));
        }

        [Test]
        [Category("Rules")]
        public void TestRule3()
        {
            GameOfLife game = new GameOfLife();
            Assert.AreEqual(ExpectedGridRule3, game.LifeGenerator(testGridRule3));
        }

        [Test]
        [Category("Rules")]
        public void TestRule4()
        {
            GameOfLife game = new GameOfLife();
            Assert.AreEqual(ExpectedGridRule4, game.LifeGenerator(testGridRule4));
        }
    }
}