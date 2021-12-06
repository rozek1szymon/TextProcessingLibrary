using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessingLibrary
{
    public class WordFrequencyAnalyzer : IWordFrequencyAnalyzer
    {
        public int CalculateHighestFrequency(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            var wordsDictionary = CalculateNumberOfWordsInText(text);

            return wordsDictionary.Max(x => x.Value);
        }

        public int CalculateFrequencyForWord(string text, string word)
        {
            var wordsLower = RemoveSpecialCharacters(text.ToLower()).Split(" ");

            return wordsLower.Count(textWord => word == textWord);
        }

        public IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<IWordFrequency>();

            var wordsDictionary = CalculateNumberOfWordsInText(text);

            var mostFrequentWords = wordsDictionary.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Select(x => 
                (IWordFrequency)new WordFrequency
            {
                Word = x.Key,
                Frequency = x.Value
            }).Take(n).ToList();

            return mostFrequentWords;
        }

        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_]+", " ", RegexOptions.Compiled);
        }

        private static IDictionary<string, int> CalculateNumberOfWordsInText(string text)
        {
            var wordsDictionary = new Dictionary<string, int>();

            var words = RemoveSpecialCharacters(text.ToLowerInvariant().Trim())
                .Split(" ");

            foreach (var word in words)
            {
                if (wordsDictionary.ContainsKey(word))
                {
                    wordsDictionary[word]++;
                }
                else
                {
                    wordsDictionary.Add(word, 1);
                }
            }

            return wordsDictionary;
        }
    }
}
