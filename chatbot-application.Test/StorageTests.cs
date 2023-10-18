using StorageLib;
using Xunit;
using System.IO;

namespace StorageLib.Tests
{
    public class StorageTests
    {
        private readonly CsvStorage csvStorage;

        public StorageTests()
        {
            csvStorage = new CsvStorage();
        }

        [Fact]
        public void GetResponse_WhenKeywordExists_ShouldReturnCorrectResponse()
        {
            // Arrange
            var userInput = "wie gehts";
            var userName = "John";

            // Act
            var response = csvStorage.GetResponse(userInput, userName);

            // Assert
            Assert.Equal("Gut, vielen Dank und selbst?", response);
        }

        [Fact]
        public void GetResponse_WhenKeywordDoesNotExist_ShouldReturnDefaultResponse()
        {
            // Arrange
            var userInput = "nichtexistentesKeyword";
            var userName = "John";

            // Act
            var response = csvStorage.GetResponse(userInput, userName);

            // Assert
            Assert.Equal("Entschuldigung, ich verstehe das nicht. Könnten Sie das bitte klären?", response);
        }
    }
}
