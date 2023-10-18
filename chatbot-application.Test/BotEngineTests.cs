using Xunit;
using System.Threading.Tasks;
using StorageLib;
using Microsoft.Extensions.Configuration;
using chatbot_application;

public class BotTests
{
    private BotEngine bot;
    private readonly IConfiguration configuration;

    public BotEngineTests()
    {
        configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        bot = new BotEngine(configuration["OpenWeatherApiKey"]);
    }

    [Fact]
    public async Task ProcessInput_NoUsername_SetsUsernameAndGreets()
    {
        // Arrange
        var input = "John";

        // Act
        string result = await bot.ProcessInput(input);

        // Assert
        Assert.Equal("Hallo John, wie kann ich Ihnen heute helfen?", result);
    }

    [Fact]
    public async Task ProcessInput_InvalidUsername_PromptsForValidName()
    {
        // Arrange
        bot.UserName = ""; // This forces the ArgumentException in the test context.
        var input = "invalid";

        // Act
        string result = await bot.ProcessInput(input);

        // Assert
        Assert.Equal("Bitte geben Sie einen g¸ltigen Namen ein.", result);
    }

    [Fact]
    public async Task ProcessInput_IsWaitingForLocation_GetsWeather()
    {
        // Arrange
        bot.isWaitingForLocation = true;
        mockWeatherAPI.Setup(api => api.GetWeatherAsync("Berlin")).ReturnsAsync("Weather for Berlin: Sunny");
        var input = "Berlin";

        // Act
        string result = await bot.ProcessInput(input);

        // Assert
        Assert.Equal("Weather for Berlin: Sunny", result);
    }

    [Fact]
    public async Task ProcessInput_UserAsksWeather_PromptsForLocation()
    {
        // Arrange
        var input = "Wetter?";

        // Act
        string result = await bot.ProcessInput(input);

        // Assert
        Assert.Equal("F¸r welchen Ort mˆchten Sie das Wetter wissen?", result);
    }

    [Fact]
    public async Task ProcessInput_HasStoredResponse_ReturnsStoredResponse()
    {
        // Arrange
        mockStorage.Setup(s => s.GetResponse("how are you?", "John")).Returns("I'm good, John!");
        var input = "how are you?";

        // Act
        string result = await bot.ProcessInput(input);

        // Assert
        Assert.Equal("I'm good, John!", result);
    }

    [Fact]
    public async Task ProcessInput_NoStoredResponse_ReturnsRandomSuggestion()
    {
        // Arrange
        mockStorage.Setup(s => s.GetResponse("unknown", "John")).Returns(bot.DefaultResponse);
        var input = "unknown";

        // Act
        string result = await bot.ProcessInput(input);

        // Assert
        Assert.True(result.StartsWith("Entschuldige John, ich weiﬂ nicht, was du meinst. Versuche es mit: "));
    }
}
