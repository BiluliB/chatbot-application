using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace chatbot_application.Data
{
    public class WeatherResponse
    {
        private readonly string apiKey;

        public WeatherResponse(string apiKey)
        {
            this.apiKey = apiKey;
        }

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
