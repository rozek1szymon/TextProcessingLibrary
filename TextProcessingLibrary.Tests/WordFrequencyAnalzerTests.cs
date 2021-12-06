using System;
using System.Linq;
using Xunit;

namespace TextProcessingLibrary.Tests
{
    public class WordFrequencyAnalzerTests
    {
        [Theory]
        [InlineData("The sun is sunny and the water", "the", 2)]
        [InlineData("The the, the the, sun, water. Money then", "the", 4)]
        [InlineData(" The the, the the, sun, water.  Money then ", "the", 4)]
        [InlineData("the and, the and, sun, water. Money", "the", 2)]
        [InlineData(" ", "the", 0)]
        [InlineData("Sun is everywhere ", "the", 0)]
        [InlineData("Sun is sun is everywhere ", "the", 0)]
        public void Analyzer_should_return_proper_number_of_occurrences_for_word_within_text(string text, string word, int expectedCount)
        {
            var analyzer = new WordFrequencyAnalyzer();

            var result = analyzer.CalculateFrequencyForWord(text, word);

            Assert.Equal(expectedCount, result);
        }

        [Theory]
        [InlineData("The sun is sunny and the water", 2)]
        [InlineData("The the, the the, sun, water. Money then", 4)]
        [InlineData(" The the, the the, sun, water.  Money then ", 4)]
        [InlineData("the and, the and, sun, water. Money", 2)]
        [InlineData(" ", 0)]
        [InlineData("Sun is everywhere ", 1)]
        [InlineData("Sun is sun is everywhere ", 2)]
        public void Analyzer_should_return_count_of_highest_occurrence_of_some_word_within_text(string text, int expectedCount)
        {
            var analyzer = new WordFrequencyAnalyzer();

            var result = analyzer.CalculateHighestFrequency(text);

            Assert.Equal(expectedCount, result);
        }

        [Theory]
        [InlineData("and sun is sunny and the water and sun", 2, 3, 2)]
        [InlineData("The the, the the, sun, water. Money then", 4, 4, 1, 1, 1)]
        [InlineData(" The the, the the, sun, water.  Money then ", 1, 4)]
        [InlineData("the and, the and, sun, water. Money", 2, 2, 2)]
        [InlineData(" ", 0)]
        [InlineData("Sun is everywhere ", 2, 1, 1)]
        [InlineData("Sun is sun is everywhere ", 2, 2, 2)]
        public void Analyzer_should_return_n_highest_occurrences_of_some_word_within_text_with_proper_frequency(string text, 
            int count, params int[] numberOfWords)
        {
            var analyzer = new WordFrequencyAnalyzer();

            var result = analyzer.CalculateMostFrequentNWords(text, count);

            Assert.Equal(result.Count, count);

            for (var i = 0; i < count; i++)
            {
                var word = result[i];
                Assert.Equal(numberOfWords[i], word.Frequency);
            }
        }
    }
}
