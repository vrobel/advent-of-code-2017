using System;
using NUnit.Framework;

namespace AdventOfCode2017
{
    [TestFixture]
    public class Day2
    {
        private static readonly int[][] Input =
        {
            new[] {798, 1976, 1866, 1862, 559, 1797, 1129, 747, 85, 1108, 104, 2000, 248, 131, 87, 95},
            new[] {201, 419, 336, 65, 208, 57, 74, 433, 68, 360, 390, 412, 355, 209, 330, 135},
            new[] {967, 84, 492, 1425, 1502, 1324, 1268, 1113, 1259, 81, 310, 1360, 773, 69, 68, 290},
            new[] {169, 264, 107, 298, 38, 149, 56, 126, 276, 45, 305, 403, 89, 179, 394, 172},
            new[] {3069, 387, 2914, 2748, 1294, 1143, 3099, 152, 2867, 3082, 113, 145, 2827, 2545, 134, 469},
            new[] {3885, 1098, 2638, 5806, 4655, 4787, 186, 4024, 2286, 5585, 5590, 215, 5336, 2738, 218, 266},
            new[] {661, 789, 393, 159, 172, 355, 820, 891, 196, 831, 345, 784, 65, 971, 396, 234},
            new[] {4095, 191, 4333, 161, 3184, 193, 4830, 4153, 2070, 3759, 1207, 3222, 185, 176, 2914, 4152},
            new[] {131, 298, 279, 304, 118, 135, 300, 74, 269, 96, 366, 341, 139, 159, 17, 149},
            new[] {1155, 5131, 373, 136, 103, 5168, 3424, 5126, 122, 5046, 4315, 126, 236, 4668, 4595, 4959},
            new[] {664, 635, 588, 673, 354, 656, 70, 86, 211, 139, 95, 40, 84, 413, 618, 31},
            new[] {2163, 127, 957, 2500, 2370, 2344, 2224, 1432, 125, 1984, 2392, 379, 2292, 98, 456, 154},
            new[] {271, 4026, 2960, 6444, 2896, 228, 819, 676, 6612, 6987, 265, 2231, 2565, 6603, 207, 6236},
            new[] {91, 683, 1736, 1998, 1960, 1727, 84, 1992, 1072, 1588, 1768, 74, 58, 1956, 1627, 893},
            new[] {3591, 1843, 3448, 1775, 3564, 2632, 1002, 3065, 77, 3579, 78, 99, 1668, 98, 2963, 3553},
            new[] {2155, 225, 2856, 3061, 105, 204, 1269, 171, 2505, 2852, 977, 1377, 181, 1856, 2952, 2262}
        };

        [Test]
        public void TestCorruptionCheckSum()
        {
            var exampleMatrix = new[]
            {
                new[] {5, 1, 9, 5},
                new[] {7, 5, 3},
                new[] {2, 4, 6, 8}
            };

            Assert.AreEqual(1, GetSmallest(exampleMatrix[0]));
            Assert.AreEqual(9, GetLargest(exampleMatrix[0]));
            Assert.AreEqual(8, GetSmallestToLargestDifference(exampleMatrix[0]));
            Assert.AreEqual(18, GetCorruptionCheckSum(exampleMatrix));
            
            Console.WriteLine(GetCorruptionCheckSum(Input));
        }

        private int GetCorruptionCheckSum(int[][] input)
        {
            var sum = 0;
            foreach (var row in input)
            {
                sum += GetSmallestToLargestDifference(row);
            }
            return sum;
        }

        private int GetSmallestToLargestDifference(int[] input)
        {
            return GetLargest(input) - GetSmallest(input);
        }

        private int GetSmallest(int[] input)
        {
            var min = input[0];
            for (int i = 0; i < input.Length; i++)
            {
                min = Math.Min(min, input[i]);
            }
            return min;
        }

        private int GetLargest(int[] input)
        {
            var max = input[0];
            for (int i = 0; i < input.Length; i++)
            {
                max = Math.Max(max, input[i]);
            }
            return max;
        }

        [Test]
        public void TestCorruptionCheckSumEvenDividers()
        {
            var exampleMatrix = new[]
            {
                new[] {5, 9, 2, 8},
                new[] {9, 4, 7, 3},
                new[] {3, 8, 6, 5}
            };
            
            Assert.IsTrue(AreEquallyDivisible(2, 8));
            Assert.AreEqual(4, EqualDivision(2, 8));
            Assert.AreEqual(4, FindEqualDivision(exampleMatrix[0]));
            Assert.AreEqual(9, GetCorruptionCheckSumEvenDividers(exampleMatrix));

            Console.WriteLine(GetCorruptionCheckSumEvenDividers(Input));
        }

        private int GetCorruptionCheckSumEvenDividers(int[][] input)
        {
            var sum = 0;
            foreach (var row in input)
            {
                sum += FindEqualDivision(row);
            }
            return sum;
        }

        private int FindEqualDivision(int[] input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (AreEquallyDivisible(input[i], input[j]))
                    {
                        return EqualDivision(input[i], input[j]);
                    }
                }
            }
            return -1;
        }

        private int EqualDivision(int a, int b)
        {
            return a > b ? a / b : b / a;
        }

        private bool AreEquallyDivisible(int a, int b)
        {
            return a > b ? a % b == 0 : b % a == 0;
        }
    }
}