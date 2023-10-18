using chatbot_application.Data;
using Moq;
using StorageLib;

namespace chatbot_application.Tests
{
    public class BotEngineTests
    {
        private readonly Mock<IStorage> mockStorage;
        private readonly Mock<WeatherResponse> mockWeatherResponse;
        private readonly BotEngine botEngine;

        public BotEngineTests()
        {
            mockStorage = new Mock<IStorage>();
            mockWeatherResponse = new Mock<WeatherResponse>("fakeAPIKey");

            botEngine = new BotEngine(mockStorage.Object, mockWeatherResponse.Object);
        }

        [Fact]
        public void UserName_WhenSetToEmpty_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<System.ArgumentException>(() => botEngine.UserName = "");
        }

        [Fact]
        public void UserName_WhenSetToValidString_ShouldSetCorrectly()
        {
            // Arrange
            botEngine.UserName = "John";

            // Act
            var actualUserName = botEngine.UserName;

            // Assert
            Assert.Equal("John", actualUserName);
        }

        [Fact]
        public async Task ProcessInput_WhenUserNameNotSet_ShouldPromptForUserName()
        {
            // Arrange
            var input = "John";

            // Act
            var response = await botEngine.ProcessInput(input);

            // Assert
            Assert.Equal("Hallo John, wie kann ich Ihnen heute helfen?", response);
        }

        [Fact]
        public async Task ProcessInput_WhenAskedForWeather_ShouldPromptForLocation()
        {
            // Arrange
            botEngine.UserName = "John";
            var input = "wetter";

            // Act
            var response = await botEngine.ProcessInput(input);

            // Assert
            Assert.Equal("Für welchen Ort möchten Sie das Wetter wissen?", response);
        }

        [Fact]
        public void GetWeather_WhenCalled_ShouldReturnWeatherForLocation()
        {
            // Arrange
            mockWeatherResponse.Setup(m => m.GetWeatherAsync(It.IsAny<string>())).Returns(Task.FromResult("Sunny"));

            // Act
            var weather = botEngine.GetWeather("Berlin");

            // Assert
            Assert.Equal("Sunny", weather);
        }
    }
}
