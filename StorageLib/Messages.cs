namespace StorageLib
{
    /// <summary>
    /// Represents a message with keyword-response pairs and provides functionality 
    /// to process the response with dynamic content.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the keyword associated with the message.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the response or answer associated with the keyword.
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Returns a processed answer, replacing dynamic content placeholders 
        /// with actual content.
        /// </summary>
        /// <param name="userName">The user's name to replace any {userName} placeholders.</param>
        /// <returns>The processed answer with dynamic content filled in.</returns>
        public string GetProcessedAnswer(string userName)
        {
            string processedAnswer = this.Answer;

            // Replace {time} placeholder with current time.
            if (processedAnswer.Contains("{time}"))
            {
                processedAnswer = processedAnswer.Replace("{time}", DateTime.Now.ToString("HH:mm"));
            }

            // Replace {time} placeholder with current time.
            if (processedAnswer.Contains("{date}"))
            {
                processedAnswer = processedAnswer.Replace("{date}", DateTime.Now.ToString("dd.MM.yyyy"));
            }

            // Replace {userName} placeholder with the provided userName.
            if (processedAnswer.Contains("{userName}"))
            {
                processedAnswer = processedAnswer.Replace("{userName}", userName);
            }

            return processedAnswer;
        }
    }
}