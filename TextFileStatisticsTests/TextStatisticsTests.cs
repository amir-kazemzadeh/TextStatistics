using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileStatisticsTests
{
    public class TextStatisticsTests
    {

        [Fact]
        public async void GetStreamAsync_ThrowsArgumentExceptionWhenPathIsNull()
        {
            // Arrange
            var textStatistics = new TextStatistics("");

            await Assert.ThrowsAsync<NullReferenceException>(() => textStatistics.ProcessTextAsync());
        }

        [Fact]
         public async Task ProcessTextAsync_ZeroWordCount()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFiles/EmptyText.txt");
            
            // Arrange
            var textStatistics = new TextStatistics(path);

            // Act
            await textStatistics.ProcessTextAsync();

            // Assert
            Assert.Equal(0, textStatistics.numberOfWords());
        }

        [Fact]
        public async Task ProcessTextAsync_OneWordCount()
        {
            // Arrange
            var path =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFiles/OneWordText.txt");
            var textStatistics = new TextStatistics(path);
            
            // Act
            await textStatistics.ProcessTextAsync();

            // Assert
            Assert.Equal(1, textStatistics.numberOfWords());
        }

        [Fact]
        public async Task ProcessTextAsync_TenWordCount()
        {
            // Arrange
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFiles/TenWordsNineLinesText.txt"); 
            var textStatistics = new TextStatistics(path);

            // Act
            await textStatistics.ProcessTextAsync();

            // Assert
            Assert.Equal(10, textStatistics.numberOfWords());
        }

        
        [Fact]
        public async Task ProcessTextAsync_MalmoRepeatedFiveTimes()
        {
            // Arrange
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFiles/WordsRepeatedFiveAndFourTimes.txt");
            var textStatistics = new TextStatistics(path);

            // Act
            await textStatistics.ProcessTextAsync();

            // Assert
            Assert.Equal(5, textStatistics.topWords(1)[0].frequency);
        }

        [Fact]
        public async Task ProcessTextAsync_LongestWordIsLundMalmoStockholm()
        {
            // Arrange
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFiles/FourWordsInOrderOfLength.txt");
            var textStatistics = new TextStatistics(path);

            // Act
            await textStatistics.ProcessTextAsync();

            // Assert
            Assert.Equal("lundmalmostockholm", textStatistics.longestWords(1)[0]);
        }
    }

}
