using System;
using System.IO;
using NUnit.Framework;

namespace AdventOfCode2017
{
    [TestFixture]
    public class Day16
    {
        private string _inputInstructionChain;
        private string _input;

        [SetUp]
        public void LoadInput()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory,
                "AdventOfCode2017/Resources/day16_input.txt");
            _inputInstructionChain = File.ReadAllLines(path)[0];
            _input = "abcdefghijklmnop";
        }

        [Test]
        public void TestSpinInstruction()
        {
            var input = "abcde".ToCharArray();
            var temp = new char[input.Length];
            ExecuteInstruction(input, "s1", temp);
            Assert.AreEqual("eabcd", new string(input));
        }

        [Test]
        public void TestExchangeInstruction()
        {
            var input = "eabcd".ToCharArray();
            var temp = new char[input.Length];
            ExecuteInstruction(input, "x3/4", temp);
            Assert.AreEqual("eabdc", new string(input));
        }

        [Test]
        public void TestPartnerInstruction()
        {
            var input = "eabdc".ToCharArray();
            var temp = new char[input.Length];
            ExecuteInstruction(input, "pe/b", temp);
            Assert.AreEqual("baedc", new string(input));
        }

        [Test]
        public void TestExampleInstructionChain()
        {
            var input = "abcde".ToCharArray();
            var instructions = "s1,x3/4,pe/b";
            ExecuteInstructionChain(input, instructions);
            Assert.AreEqual("baedc", new string(input));
        }

        [Test]
        public void ProcessInput()
        {
            var input = _input.ToCharArray();
            ExecuteInstructionChain(input, _inputInstructionChain);
            Console.WriteLine($"Result: {new string(input)}");
        }

        private void ExecuteInstructionChain(char[] input, string instructions)
        {
            var temp = new char[input.Length];
            var instructionArray = instructions.Split(',');
            foreach (var instruction in instructionArray)
            {
                ExecuteInstruction(input, instruction, temp);
            }
        }


        private void ExecuteInstruction(char[] input, string instruction, char[] temp)
        {
            var parameters = instruction.Substring(1);
            switch (instruction[0])
            {
                case 's':
                    Spin(input, int.Parse(parameters), temp);
                    return;
                case 'x':
                    var numbers = parameters.Split('/');
                    Exchange(input, int.Parse(numbers[0]), int.Parse(numbers[1]));
                    return;
                case 'p':
                    var ab = parameters.Split('/');
                    Partner(input, char.Parse(ab[0]), char.Parse(ab[1]));
                    return;
            }
        }

        [Test]
        public void TestSpinMove()
        {
            var chars = "abcde".ToCharArray();
            Spin(chars, 3, new char[chars.Length]);
            Assert.AreEqual("cdeab", new string(chars));
        }

        [Test]
        public void TestSpinMove2()
        {
            var chars = "abcde".ToCharArray();
            Spin(chars, 1, new char[chars.Length]);
            Assert.AreEqual("eabcd", new string(chars));
        }

        [Test]
        public void TestExchange()
        {
            var chars = "eabcd".ToCharArray();
            Exchange(chars, 3, 4);
            Assert.AreEqual("eabdc", new string(chars));
        }

        [Test]
        public void TestPartner()
        {
            var chars = "eabdc".ToCharArray();
            Partner(chars, 'e', 'b');
            Assert.AreEqual("baedc", new string(chars));
        }

        private void Partner(char[] chars, char a, char b)
        {
            Exchange(chars, Array.IndexOf(chars, a), Array.IndexOf(chars, b));
        }

        private void Exchange(char[] chars, int a, int b)
        {
            var ca = chars[a];
            chars[a] = chars[b];
            chars[b] = ca;
        }

        private void Spin(char[] chars, int count, char[] temp)
        {
            Array.Copy(chars, 0, temp, count, chars.Length - count);
            Array.Copy(chars, chars.Length - count, temp, 0, count);
            Array.Copy(temp, chars, chars.Length);
        }
    }
}