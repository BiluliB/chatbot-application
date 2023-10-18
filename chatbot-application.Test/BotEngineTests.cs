using chatbot_application;
using Moq;
using System.IO;
using Xunit;
using System.Threading.Tasks;
using chatbot_application.Data;
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
            Assert.Throws<System.ArgumentException>(() => botEngine.UserName = "");
        }

        [Fact]
        public void UserName_WhenSetToValidString_ShouldSetCorrectly()
        {
            botEngine.UserName = "John";
            Assert.Equal("John", botEngine.UserName);
        }

        [Fact]
        public async Task ProcessInput_WhenUserNameNotSet_ShouldPromptForUserName()
        {
            var response = await botEngine.ProcessInput("John");
            Assert.Equal("Hallo John, wie kann ich Ihnen heute helfen?", response);
        }

        [Fact]
        public async Task ProcessInput_WhenAskedForWeather_ShouldPromptForLocation()
        {
            botEngine.UserName = "John"; // Set the username first
            var response = await botEngine.ProcessInput("wetter");
            Assert.Equal("Für welchen Ort möchten Sie das Wetter wissen?", response);
        }

        // ... More tests covering other branches of logic ...

        [Fact]
        public void GetWeather_WhenCalled_ShouldReturnWeatherForLocation()
        {
            mockWeatherResponse.Setup(m => m.GetWeatherAsync(It.IsAny<string>())).Returns(Task.FromResult("Sunny"));
            var weather = botEngine.GetWeather("Berlin");
            Assert.Equal("Sunny", weather);
        }
    }
}
