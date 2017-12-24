﻿using System;
using NUnit.Framework;

namespace AdventOfCode2017
{
    [TestFixture]
    public class Day15
    {
        private const int EndingBitMask = 0xFFFF;
        private const int Iterations = 40_000_000;
        private const int PerkyIterations = 5_000_000;

        private const int GenAMultiplier = 16807;
        private const int GenBMultiplier = 48271;
        
        private const int ExampleGenA = 65;
        private const int ExampleGenB = 8921;
        
        private const int InputGenA = 277;
        private const int InputGenB = 349;

        [Test]
        public void TestExampleGenerator()
        {
            var generator = new Generator(ExampleGenA, GenAMultiplier);
            Assert.AreEqual(1092455, generator.NextValue());
            Assert.AreEqual(1181022009, generator.NextValue());
            Assert.AreEqual(245556042, generator.NextValue());
            Assert.AreEqual(1744312007, generator.NextValue());
            Assert.AreEqual(1352636452, generator.NextValue());
            
            generator = new Generator(ExampleGenB, GenBMultiplier);
            Assert.AreEqual(430625591, generator.NextValue());
            Assert.AreEqual(1233683848, generator.NextValue());
            Assert.AreEqual(1431495498, generator.NextValue());
            Assert.AreEqual(137874439, generator.NextValue());
            Assert.AreEqual(285222916, generator.NextValue());
        }

        [Test]
        public void TestExamplePerkyGenerator()
        {
            var generator = new PerkyGenerator(ExampleGenA, GenAMultiplier, 4);
            Assert.AreEqual(1352636452, generator.NextValue());
            Assert.AreEqual(1992081072, generator.NextValue());
            Assert.AreEqual(530830436, generator.NextValue());
            Assert.AreEqual(1980017072, generator.NextValue());
            Assert.AreEqual(740335192, generator.NextValue());
            
            generator = new PerkyGenerator(ExampleGenB, GenBMultiplier, 8);
            Assert.AreEqual(1233683848, generator.NextValue());
            Assert.AreEqual(862516352, generator.NextValue());
            Assert.AreEqual(1159784568, generator.NextValue());
            Assert.AreEqual(1616057672, generator.NextValue());
            Assert.AreEqual(412269392, generator.NextValue());
        }

        [Test]
        public void TestGeneratorComparison()
        {
            var exampleGenA = new Generator(ExampleGenA, GenAMultiplier);
            var exampleGenB = new Generator(ExampleGenB, GenBMultiplier);

            Assert.IsFalse(CompareGeneratorResults(exampleGenA.NextValue(), exampleGenB.NextValue()));
            Assert.IsFalse(CompareGeneratorResults(exampleGenA.NextValue(), exampleGenB.NextValue()));
            Assert.IsTrue(CompareGeneratorResults(exampleGenA.NextValue(), exampleGenB.NextValue()));
            Assert.IsFalse(CompareGeneratorResults(exampleGenA.NextValue(), exampleGenB.NextValue()));
        }

        [Test]
        public void TestExample40MillionIterations()
        {
            var exampleGenA = new Generator(ExampleGenA, GenAMultiplier);
            var exampleGenB = new Generator(ExampleGenB, GenBMultiplier);
            Assert.AreEqual(588, CountMatchesOverIteration(exampleGenA, exampleGenB, Iterations));
        }

        [Test]
        public void Test40MillionIterations()
        {
            var genA = new Generator(InputGenA, GenAMultiplier);
            var genB = new Generator(InputGenB, GenBMultiplier);
            var count = CountMatchesOverIteration(genA, genB, Iterations);

            Console.WriteLine($"Generator results matched {count} times");
        }

        [Test]
        public void Test40MillionPerkyIterations()
        {
            var genA = new PerkyGenerator(InputGenA, GenAMultiplier, 4);
            var genB = new PerkyGenerator(InputGenB, GenBMultiplier, 8);
            var count = CountMatchesOverIteration(genA, genB, PerkyIterations);

            Console.WriteLine($"Generator results matched {count} times");
        }
        
        private bool CompareGeneratorResults(int a, int b)
        {
            return (a & EndingBitMask) == (b & EndingBitMask);
        }

        private int CountMatchesOverIteration(IGenerator genA, IGenerator genB, int iterations)
        {
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                if (CompareGeneratorResults(genA.NextValue(), genB.NextValue()))
                {
                    count++;
                }
            }
            return count;
        }
        
        private interface IGenerator
        {
            int NextValue();
        }

        private class Generator : IGenerator
        {
            private long _current;
            private readonly int _multiplier;

            public Generator(int init, int multiplier)
            {
                _current = init;
                _multiplier = multiplier;
            }

            public int NextValue()
            {
                _current = _current * _multiplier % int.MaxValue;
                return (int) _current;
            }
        }

        private class PerkyGenerator : IGenerator
        {
            private long _current;
            private readonly int _multiplier;
            private readonly int _acceptedDivider;

            public PerkyGenerator(int init, int multiplier, int acceptedDivider)
            {
                _current = init;
                _multiplier = multiplier;
                _acceptedDivider = acceptedDivider;
            }

            public int NextValue()
            {
                do
                {
                    _current = _current * _multiplier % int.MaxValue;
                } while (_current % _acceptedDivider != 0);
                return (int) _current;
            }
        }
    }
}