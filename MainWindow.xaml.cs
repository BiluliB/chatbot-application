using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.Windows.Controls;
using System.Windows.Media;

namespace chatbot_application
{
    /// <summary>
    /// Represents the main window of the application.
    /// </summary>
    public partial class MainWindow : Window
    {
        private BotEngine botEngine;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            botEngine = new BotEngine(configuration["OpenWeatherMapApiKey"]);
        }

        /// <summary>
        /// Handles the KeyDown event of the UserInput control.
        /// </summary>
        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        /// <summary>
        /// Handles the Click event of the SendButton control.
        /// </summary>
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        /// <summary>
        /// Sends the user message and receives a response from the bot.
        /// </summary>
        private async void SendMessage()
        {
            string userInput = UserInput.Text;
            UserInput.Clear();
            ChatHistory.Items.Add(new UserMessage { Text = userInput });

            string botResponse;
            if (botEngine.IsWaitingForLocation)
            {
                botResponse = await botEngine.GetWeatherAsync(userInput);
                botEngine.IsWaitingForLocation = false;
            }
            else
            {
                botResponse = botEngine.GetBotResponse(userInput);
            }

            ChatHistory.Items.Add(new BotMessage { Text = botResponse });

            // Scroll to latest message
            if (VisualTreeHelper.GetChildrenCount(ChatHistory) > 0)
            {
                var border = (Border)VisualTreeHelper.GetChild(ChatHistory, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToEnd();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    /// <summary>
    /// Represents a user message.
    /// </summary>
    public class UserMessage
    {
        /// <summary>
        /// Gets or sets the text of the user message.
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Represents a bot message.
    /// </summary>
    public class BotMessage
    {
        /// <summary>
        /// Gets or sets the text of the bot message.
        /// </summary>
        public string Text { get; set; }
    }
}
