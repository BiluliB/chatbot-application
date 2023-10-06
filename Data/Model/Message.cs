using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatbot_application.Data.Model
{
    public class Message
    {
        public string Keyword { get; set; }
        public string Answer { get; set; }

        public string GetProcessedAnswer(string userName)
        {
            string processedAnswer = this.Answer;

            if (processedAnswer.Contains("{time}"))
            {
                processedAnswer = processedAnswer.Replace("{time}", DateTime.Now.ToString("HH:mm"));
            }

            if (processedAnswer.Contains("{userName}"))
            {
                processedAnswer = processedAnswer.Replace("{userName}", userName);
            }

            return processedAnswer;
        }
    }
}
