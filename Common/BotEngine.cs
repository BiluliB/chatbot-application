using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace chatbot_application
{
    public class BotEngine
    {
        private readonly string apiKey;
        private bool isWaitingForLocation = false;

        public BotEngine(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public bool IsWaitingForLocation
        {
            get { return isWaitingForLocation; }
            set { isWaitingForLocation = value; }
        }

        /// <summary>
        /// Generates a bot response based on user input.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <returns>The bot response.</returns>
        public string GetBotResponse(string userInput)
        {
            userInput = userInput.ToLowerInvariant();

            if (userInput.Contains("hallo") || userInput.Contains("hi") || userInput.Contains("hey"))
                return "Hallo! Wie kann ich Ihnen heute helfen?";

            if (userInput.Contains("tschüss") || userInput.Contains("ciao") || userInput.Contains("auf wiedersehen"))
                return "Auf Wiedersehen! Wenn Sie weitere Fragen haben, stehe ich Ihnen gerne zur Verfügung.";

            if (userInput.Contains("wetter"))
            {
                isWaitingForLocation = true;
                return "Für welchen Ort möchten Sie das Wetter wissen?";
            }

            if (userInput.Contains("uhrzeit") || userInput.Contains("zeit"))
                return $"Die aktuelle Uhrzeit ist {DateTime.Now:HH:mm}.";

            if (userInput.Contains("datum") || userInput.Contains("tag"))
                return $"Heute ist der {DateTime.Now:dddd, dd. MMMM yyyy}.";

            return "Entschuldigung, ich verstehe das nicht. Könnten Sie das bitte klären?";
        }

        /// <summary>
        /// Retrieves the weather for the specified location asynchronously.
        /// </summary>
        /// <param name="location">The location for which to retrieve the weather.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the weather information.</returns>
        public async Task<string> GetWeatherAsync(string location)
        {
            string requestUrl = $"http://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}&units=metric&lang=de";

            using HttpClient httpClient = new HttpClient();
            try
            {
                string jsonResponse = await httpClient.GetStringAsync(requestUrl);
                JObject weatherResponse = JObject.Parse(jsonResponse);

                string weatherDescription = (string)weatherResponse.SelectToken("weather[0].description");
                double temperature = (double)weatherResponse.SelectToken("main.temp");

                return $"Das Wetter in {location} ist aktuell {weatherDescription} mit einer Temperatur von {temperature}°C.";
            }
            catch (Exception)
            {
                return "Es tut mir leid, ich konnte das Wetter für diesen Ort nicht abrufen. Bitte versuchen Sie es später noch einmal oder überprüfen Sie den Ort.";
            }
        }
    }
}
