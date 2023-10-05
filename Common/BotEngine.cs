using chatbot_application.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace chatbot_application
{
    public class BotEngine
    {
        private bool isWaitingForLocation = false;
        private string _username;
        private readonly Storage storage;
        private readonly WeatherResponse weatherAPI;

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

            return storage.GetResponse(userInput, _username);
        }

        public string GetWeather(string location)
        {
            // Hier rufen wir die `GetWeatherAsync` Methode synchron auf
            return weatherAPI.GetWeatherAsync(location).Result;
        }
    }
}
