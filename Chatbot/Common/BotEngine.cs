using chatbot_application.Data;
using StorageLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace chatbot_application
{
    /// <summary>
    /// The engine that powers the bot's logic and processing.
    /// </summary>
    public class BotEngine
    {
        private bool isWaitingForLocation = false;
        private string _username;
        private readonly IStorage storage;
        private readonly WeatherResponse weatherAPI;
        private readonly string csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CsvImport", "keywords.csv");

        /// <summary>
        /// Initializes a new instance of the BotEngine class with a specific API key.
        /// </summary>
        public BotEngine(string apiKey) : this(new CsvStorage(), new WeatherResponse(apiKey))
        {
        }

        public BotEngine(IStorage storage, WeatherResponse weatherAPI)
        {
            this.storage = storage;
            this.weatherAPI = weatherAPI;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Der Name darf nicht leer sein.");
                }
                _username = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the bot is waiting for a location input from the user.
        /// </summary>
        public bool IsWaitingForLocation
        {
            get { return isWaitingForLocation; }
            set { isWaitingForLocation = value; }
        }

        /// <summary>
        /// Default Response
        /// </summary>
        private const string DefaultResponse = "Entschuldigung, ich verstehe das nicht. Könnten Sie das bitte klären?";

        /// <summary>
        /// Processes the user's input and returns the bot's response.
        /// </summary>
        public async Task<string> ProcessInput(string userInput)
        {
            if (string.IsNullOrEmpty(_username))
            {
                try
                {
                    UserName = userInput;
                    return $"Hallo {UserName}, wie kann ich Ihnen heute helfen?";
                }
                catch (ArgumentException)
                {
                    return "Bitte geben Sie einen gültigen Namen ein.";
                }
            }

            if (isWaitingForLocation)
            {
                isWaitingForLocation = false;
                return await weatherAPI.GetWeatherAsync(userInput);
            }

            if (userInput.ToLowerInvariant().Contains("wetter"))
            {
                isWaitingForLocation = true;
                return "Für welchen Ort möchten Sie das Wetter wissen?";
            }

            string response = storage.GetResponse(userInput, _username);
            if (!string.IsNullOrEmpty(response) && response != DefaultResponse)
            {
                return response;
            }

            // If no appropriate response is found or the response is the default text:
            string[] questions = GetQuestionsFromCsv();
            var random = new Random();
            string randomSuggestion = questions[random.Next(questions.Length)];
            return $"Entschuldige {_username}, ich weiß nicht, was du meinst. Versuche es mit: {randomSuggestion}";
        }

        /// <summary>
        /// Retrieves questions from a CSV file.
        /// </summary>
        private string[] GetQuestionsFromCsv()
        {
            var lines = File.ReadAllLines(csvFilePath);
            var questions = new List<string>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length > 0)
                {
                    questions.Add(parts[0]);
                }
            }

            return questions.ToArray();
        }

        /// <summary>
        /// Synchronously retrieves the weather for a specific location.
        /// </summary>
        public string GetWeather(string location)
        {
            // Here we're calling the `GetWeatherAsync` method synchronously
            return weatherAPI.GetWeatherAsync(location).Result;
        }
    }
}
