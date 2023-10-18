namespace StorageLib
{
    /// <summary>
    /// Defines the contract for a storage mechanism that provides access to message-response pairs.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Gets a list of messages stored in the storage.
        /// </summary>
        List<Message> Messages { get; }

        /// <summary>
        /// Retrieves an appropriate response based on the user's input and user name.
        /// </summary>
        /// <param name="userInput">The user's input to find a matching response.</param>
        /// <param name="userName">The user's name to personalize the response, if necessary.</param>
        /// <returns>The bot's response based on the user's input.</returns>
        string GetResponse(string userInput, string userName);
    }
}