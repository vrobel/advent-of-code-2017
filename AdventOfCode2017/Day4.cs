using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2017
{
    [TestFixture]
    public class Day4
    {
        private string[] _input;

        [SetUp]
        public void LoadInput()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory,
                "AdventOfCode2017/Resources/day4_input.txt");
            _input = File.ReadAllLines(path);
            Console.Write(_input);
        }
        
        [Test]
        public void TestHighEntropyPassphrases()
        {
            Assert.IsTrue(IsPassphraseValid("aa bb cc dd ee"));
            Assert.IsFalse(IsPassphraseValid("aa bb cc dd aa"));
            Assert.IsTrue(IsPassphraseValid("aa bb cc dd aaa"));

            Console.WriteLine("Amount of valid passphrases in the input: " + _input.Count(IsPassphraseValid));
        }
        
        [Test]
        public void TestHighEntropyPassphrasesIncludingAnagrams()
        {
            Assert.IsTrue(IsAnagramlessPassphraseValid("abcde fghij"));
            Assert.IsFalse(IsAnagramlessPassphraseValid("abcde xyz ecdab"));
            Assert.IsTrue(IsAnagramlessPassphraseValid("a ab abc abd abf abj"));
            Assert.IsTrue(IsAnagramlessPassphraseValid("iiii oiii ooii oooi oooo"));
            Assert.IsFalse(IsAnagramlessPassphraseValid("oiii ioii iioi iiio"));

            Console.WriteLine("Amount of valid anagramless passphrases in the input: " + _input.Count(IsAnagramlessPassphraseValid));
        }

        private bool IsAnagramlessPassphraseValid(string passphrase)
        {
            var strings = passphrase.Split(' ');
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = string.Concat(strings[i].OrderBy(c => c));
            }

            return IsPassphraseValid(strings);
        }

        private bool IsPassphraseValid(string passphrase)
        {
            return IsPassphraseValid(passphrase.Split(' '));
        }

        private static bool IsPassphraseValid(string[] strings)
        {
            return strings.Distinct().Count() == strings.Length;
        }
    }
}