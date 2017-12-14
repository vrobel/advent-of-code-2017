using System;
using System.IO;
using NUnit.Framework;

namespace AdventOfCode2017
{
    [TestFixture]
    public class Day5
    {
        private int[] _input;

        [SetUp]
        public void LoadInput()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory,
                "AdventOfCode2017/Resources/day5_input.txt");
            var lines = File.ReadAllLines(path);
            _input = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                _input[i] = Int32.Parse(lines[i]);
            }
        }

        [Test]
        public void TestMaze()
        {
            var exampleInput = new[] {0, 3, 0, 1, -3};
            Assert.AreEqual(5, CountMazeSteps(exampleInput));

            Console.WriteLine(CountMazeSteps(_input));
        }

        [Test]
        public void TestMaze2()
        {
            var exampleInput = new[] {0, 3, 0, 1, -3};
            Assert.AreEqual(10, CountMaze2Steps(exampleInput));

            Console.WriteLine(CountMaze2Steps(_input));
        }

        private int CountMaze2Steps(int[] input)
        {
            var inputSize = input.Length;
            var i = 0;
            var iterations = 0;
            while (i >= 0 && i < inputSize)
            {
                var value = input[i];
                input[i] += value >= 3 ? -1 : 1;
                i += value;
                iterations++;
            }
            return iterations;
        }

        private int CountMazeSteps(int[] input)
        {
            var inputSize = input.Length;
            var i = 0;
            var iterations = 0;
            while (i >= 0 && i < inputSize)
            {
                var value = input[i];
                input[i]++;
                i += value;
                iterations++;
            }
            return iterations;
        }
    }
}