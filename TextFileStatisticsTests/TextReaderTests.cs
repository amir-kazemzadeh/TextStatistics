using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using TextReader = TextFileStatistics.TextReader;

namespace TextFileStatisticsTests
{
    public class TextReaderTests
    {
        [Fact]
        public async void GetStreamAsync_ThrowsArgumentExceptionWhenPathIsNull()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => TextReader.GetStreamAsync(null));
        }

        [Fact]
        public async void GetStreamAsync_ThrowsArgumentExceptionWhenPathIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => TextReader.GetStreamAsync(string.Empty));
        }

        [Fact]
        public async void GetStreamAsync_ThrowsExceptionWhenFileDoesNotExist()
        {
            // Arrange
            string path = "IDontExist.txt";

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => TextReader.GetStreamAsync(path));
        }

        [Fact]
        public async void GetStreamAsync_ReturnsStreamWhenPathIsURL()
        {
            // Arrange
            string url = "https://www.gutenberg.org/files/45839/45839.txt";

            // Act
            Stream stream = await TextReader.GetStreamAsync(url);

            // Assert
            Assert.NotNull(stream);
        }

        [Fact]
        public async void GetStreamAsync_ReturnsStreamWhenPathIsLocalFile()
        {
            // Arrange
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFiles/test.txt");

            // Act
            Stream stream = await TextReader.GetStreamAsync(path);

            // Assert
            Assert.NotNull(stream);
        }
    }
}
