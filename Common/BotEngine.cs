using chatbot_application.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace chatbot_application
{
    public class BotEngine
    {
        private bool isWaitingForLocation = false;
        private string _username;
        private readonly Storage storage;
        private readonly WeatherResponse weatherAPI;
        private readonly string csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CsvImport", "keywords.csv");

        public BotEngine(string apiKey)
        {
            this.storage = new Storage();
            this.weatherAPI = new WeatherResponse(apiKey);
        }

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

        public bool IsWaitingForLocation
        {
            get { return isWaitingForLocation; }
            set { isWaitingForLocation = value; }
        }

        private const string DefaultResponse = "Entschuldigung, ich verstehe das nicht. Könnten Sie das bitte klären?";

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

            // Wenn keine passende Antwort gefunden wird oder die Antwort der Standardtext ist:
            string[] questions = GetQuestionsFromCsv();
            var random = new Random();
            string randomSuggestion = questions[random.Next(questions.Length)];
            return $"Entschuldige {_username}, ich weiß nicht, was du meinst. Versuche es mit: {randomSuggestion}";
        }



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

        public string GetWeather(string location)
        {
            // Hier rufen wir die `GetWeatherAsync` Methode synchron auf
            return weatherAPI.GetWeatherAsync(location).Result;
        }
    }
}
