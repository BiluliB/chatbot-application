using chatbot_application.Data.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace chatbot_application.Data
{
    public class Storage
    {
        public List<Message> Messages { get; private set; } = new List<Message>();

        public Storage()
        {
            Load();
        }

        private void Load()
        {
            string path = "./CsvImport/keywords.csv";
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (values.Length == 2)
                    {
                        Messages.Add(new Message
                        {
                            Keyword = values[0].Trim('"').ToLower(),
                            Answer = values[1].Trim('"')
                        });
                    }
                }
            }
        }

        public string GetResponse(string userInput, string userName)
        {
            userInput = userInput.ToLowerInvariant();

            // Suche nach einem Schlüsselwort im userInput, wobei längere Schlüsselwörter zuerst kommen
            var matchedMessage = Messages
                .OrderByDescending(m => m.Keyword.Length)
                .FirstOrDefault(m => userInput.Contains(m.Keyword));

            if (matchedMessage != null)
            {
                return matchedMessage.Answer.Replace("{userName}", userName);
            }

            return "Entschuldigung, ich verstehe das nicht. Könnten Sie das bitte klären?";
        }
    }
}