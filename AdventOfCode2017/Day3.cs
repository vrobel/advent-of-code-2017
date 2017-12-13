using System;
using NUnit.Framework;

namespace AdventOfCode2017
{
    [TestFixture]
    public class Day3
    {
        private const int Input = 289326;

        [Test]
        public void TestSpiralMemory()
        {
            Assert.AreEqual(0, GetMemoryPathDistance(1));
            Assert.AreEqual(3, GetMemoryPathDistance(12));
            Assert.AreEqual(2, GetMemoryPathDistance(23));
            Assert.AreEqual(3, GetMemoryPathDistance(14));
            Assert.AreEqual(31, GetMemoryPathDistance(1024));

            Console.WriteLine(GetMemoryPathDistance(Input));
        }

        [Test]
        public void TestDetermineSquare()
        {
            Assert.AreEqual(0, DetermineSquareNumber(1));
            Assert.AreEqual(1, DetermineSquareNumber(2));
            Assert.AreEqual(1, DetermineSquareNumber(5));
            Assert.AreEqual(2, DetermineSquareNumber(10));
            Assert.AreEqual(2, DetermineSquareNumber(23));

            Console.WriteLine(DetermineSquareNumber(Input));
        }

        [Test]
        public void TestLastFieldNumber()
        {
            Assert.AreEqual(1, LastFieldNumber(0));
            Assert.AreEqual(9, LastFieldNumber(1));
            Assert.AreEqual(25, LastFieldNumber(2));
        }

        private int DetermineSquareNumber(int input)
        {
            return (int) (Math.Ceiling(Math.Sqrt(input)) / 2);
        }

        private int LastFieldNumber(int squareNumber)
        {
            return (int) Math.Pow(2 * squareNumber + 1, 2);
        }

        private int GetMemoryPathDistance(int input)
        {
            if (input == 1)
            {
                return 0;
            }

            var squareNumber = DetermineSquareNumber(input);
            var previousSquareLastNumber = LastFieldNumber(squareNumber - 1);
            
            //squareNumber is a minimum number of moves to get to the center.
            var distanceToNextCenterPosition = 2 * squareNumber;
            var lastCenterPosition = (input + squareNumber - previousSquareLastNumber) % distanceToNextCenterPosition;
            var centerPositionOffset = Math.Min(lastCenterPosition, distanceToNextCenterPosition - lastCenterPosition);
            return squareNumber + centerPositionOffset;
        }
    }
}