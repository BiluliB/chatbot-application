using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace chatbot_application.Data
{
    /// <summary>
    /// Provides functionality to fetch and process weather data from the OpenWeatherMap API.
    /// </summary>
    public class WeatherResponse
    {
        /// <summary>
        /// Gets the API stored in BotEngine during runtime
        /// </summary>
        private readonly string apiKey;

        /// <summary>
        /// Initializes a new instance of the WeatherResponse class with a specific API key.
        /// </summary>
        /// <param name="apiKey">The API key for the OpenWeatherMap service.</param>
        public WeatherResponse(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Asynchronously fetches and returns weather data for a specific location.
        /// </summary>
        /// <param name="location">The location for which to fetch the weather data.</param>
        /// <returns>A string representation of the weather in the given location.</returns>
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
