using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatbot_application.Data
{
    public class Storage
    {
        private Dictionary<string, string> responses;

        public Storage()
        {
            LoadResponsesFromCSV();
        }

        private void LoadResponsesFromCSV()
        {
            responses = new Dictionary<string, string>();
            string path = "./CsvImport/keywords.csv";
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    line = RemoveQuotes(line);  // Zeile bereinigen
                    var values = line.Split(';');

                    if (values.Length == 2)
                    {
                        string userInput = values[0].Trim().ToLower();
                        string botResponse = values[1].Trim();
                        responses[userInput] = botResponse;
                    }
                }
            }
        }

        private string RemoveQuotes(string input)
        {
            return input.Replace("\"", "");
        }

        public string GetResponse(string userInput, string userName)
        {
            userInput = userInput.ToLowerInvariant();

            string matchedKey = responses.Keys
                .OrderByDescending(key => key.Length)
                .FirstOrDefault(key => userInput.Contains(key));

            if (matchedKey != null)
            {
                return responses[matchedKey].Replace("{userName}", userName);
            }

            return "Entschuldigung, ich verstehe das nicht. Könnten Sie das bitte klären?";
        }
    }
}
