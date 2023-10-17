using chatbot_application.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace chatbot_application.Data
{

    /// <summary>
    /// Provides functionality to load and query message-response pairs from a CSV file.
    /// </summary>
    public class Storage
    {
        /// <summary>
        /// Gets the list of loaded messages from the CSV file.
        /// </summary>
        public List<Message> Messages { get; private set; } = new List<Message>();

        /// <summary>
        /// Initializes a new instance of the Storage class and loads messages from the CSV file.
        /// </summary>
        public Storage()
        {
            Load();
        }

        /// <summary>
        /// Loads messages from a specified CSV file into the Messages list.
        /// </summary>
        private void Load()
        {
            string path = "./CsvImport/keywords.csv";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Die CSV-Datei {path} wurde nicht gefunden.");
            }

            using (StreamReader reader = new StreamReader(path))
            {
                int lineNumber = 0;

                try
                {
                    while (!reader.EndOfStream)
                    {
                        lineNumber++;
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        if (values.Length != 2)
                        {
                            throw new InvalidDataException($"Ungültiges Datenformat in der Csv-Datei. Zeile: {lineNumber}. \nErwartet 2 Werte, aber gefunden {values.Length}.");
                        }

                        Messages.Add(new Message
                        {
                            Keyword = values[0].Trim('"').ToLower(),
                            Answer = values[1].Trim('"')
                        });
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Fehler", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    throw new Exception($"Fehler beim Lesen der CSV-Datei in Zeile {lineNumber}.", ex);
                }
            }
        }

        /// <summary>
        /// Returns an appropriate response based on the user's input, using the loaded messages.
        /// </summary>
        /// <param name="userInput">The user's input to find a matching response.</param>
        /// <param name="userName">The user's name to personalize the response, if necessary.</param>
        /// <returns>The bot's response based on the user's input.</returns>
        public string GetResponse(string userInput, string userName)
        {
            userInput = userInput.ToLowerInvariant();

            var matchedMessage = Messages
                .OrderByDescending(m => m.Keyword.Length)
                .FirstOrDefault(m => userInput.Contains(m.Keyword));

            if (matchedMessage != null)
            {
                return matchedMessage.GetProcessedAnswer(userName);
            }

            return "Entschuldigung, ich verstehe das nicht. Könnten Sie das bitte klären?";
        }
    }
}