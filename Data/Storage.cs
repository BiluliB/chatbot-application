using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatbot_application.Data
{
    public class Storage
    {
        /// <summary>
        /// Generates a bot response based on user input.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <returns>The bot response.</returns>
        public string GetResponse(string userInput, string userName)
        {
            userInput = userInput.ToLowerInvariant();

            if (userInput.Contains("hallo") || userInput.Contains("hi") || userInput.Contains("hey"))
                return $"Hallo {userName}, wie kann ich Ihnen heute helfen?";

            if (userInput.Contains("tschüss") || userInput.Contains("ciao") || userInput.Contains("auf wiedersehen"))
                return "Auf Wiedersehen! Wenn Sie weitere Fragen haben, stehe ich Ihnen gerne zur Verfügung.";

            if (userInput.Contains("uhrzeit") || userInput.Contains("zeit"))
                return $"Die aktuelle Uhrzeit ist {DateTime.Now:HH:mm}.";

            if (userInput.Contains("datum") || userInput.Contains("tag"))
                return $"Heute ist der {DateTime.Now:dddd, dd. MMMM yyyy}.";

            return "Entschuldigung, ich verstehe das nicht. Könnten Sie das bitte klären?";
        }
    }
}
