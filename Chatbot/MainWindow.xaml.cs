using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Text;
using StorageLib;

namespace chatbot_application
{
    /// <summary>
    /// The main window for the chatbot application.
    /// </summary>
    public partial class MainWindow : Window
    {
        private BotEngine botEngine;

        /// <summary>
        /// Initializes the main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;

            botEngine = new BotEngine(configuration["OpenWeatherMapApiKey"]);

            UserInput_TextChanged(null, null);
        }

        /// <summary>
        /// Handles the window closed event to save the chat history.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var chatLogPath = Path.Combine(appDataPath, "chatbot_application", "chat_log.txt");

            // Creating the directory, if not exist
            Directory.CreateDirectory(Path.GetDirectoryName(chatLogPath));

            var chatHistory = new StringBuilder();
            foreach (var item in ChatHistory.Items)
            {
                chatHistory.AppendLine(item.ToString());
            }

            File.WriteAllText(chatLogPath, chatHistory.ToString());
        }

        /// <summary>
        /// Handles the window loaded event to load previous chat history or display an initial message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var chatLogPath = Path.Combine(appDataPath, "chatbot_application", "chat_log.txt");

            if (File.Exists(chatLogPath))
            {
                var result = MessageBox.Show("Möchten Sie die letzte Sitzung fortsetzen?", "Fortsetzen?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var chatHistory = File.ReadAllLines(chatLogPath);
              
                    foreach (var line in chatHistory)
                    {
                        ChatHistory.Items.Add(line);
                    }
                }
            }
            ChatHistory.Items.Add(new BotMessage { Text = "Hallo! Wie darf ich Sie nennen?" });
        }

        private void UserInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Deaktivieren Sie den Button, wenn das Textfeld leer ist oder nur Leerzeichen enthält.
            SendButton.IsEnabled = !string.IsNullOrWhiteSpace(UserInput.Text);
        }

        /// <summary>
        /// Handles the KeyDown event of the UserInput control.
        /// </summary>
        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SendButton.IsEnabled)
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
            try
            {
                string userInput = UserInput.Text;
                UserInput.Clear();
                ChatHistory.Items.Add(new UserMessage { Text = userInput });

                string botResponse = await botEngine.ProcessInput(userInput);

                ChatHistory.Items.Add(new BotMessage { Text = botResponse });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // Scroll to latest message
            if (VisualTreeHelper.GetChildrenCount(ChatHistory) > 0)
            {
                var border = (Border)VisualTreeHelper.GetChild(ChatHistory, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToEnd();
            }
        }
    }

    public class UserMessage
    {
        /// <summary>
        /// Gets or sets the text of the user message.
        /// </summary>
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class BotMessage
    {
        /// <summary>
        /// Gets or sets the text of the bot message.
        /// </summary>
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
