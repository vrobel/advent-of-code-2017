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
                "AdventOfCode2017/Resources/day3_input.txt");
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
                var word = strings[i];
                var charArray = word.ToCharArray();
                Array.Sort(charArray);
                var sortedWord = new string(charArray);
                strings[i] = sortedWord;
            }
            
            for (int i = 0; i < strings.Length - 1; i++)
            {
                var w1 = strings[i];
                for (int j = i + 1; j < strings.Length; j++)
                {
                    var w2 = strings[j];
                    if (string.Equals(w1, w2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsPassphraseValid(string passphrase)
        {
            var strings = passphrase.Split(' ');
            for (int i = 0; i < strings.Length - 1; i++)
            {
                var w1 = strings[i];
                for (int j = i + 1; j < strings.Length; j++)
                {
                    var w2 = strings[j];
                    if (string.Equals(w1, w2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}