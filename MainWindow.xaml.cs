using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using ChatbotCybersecurityPart2.Models;
using ChatbotCybersecurityPart2.Services;
using System.Collections.Generic;

namespace ChatbotCybersecurityPart2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int logDisplayCount = 5;

        private QuizService quizService = new QuizService();

        private List<QuizQuestions> questions;

        private int currentQuestion = 0;

        private int score = 0;

        private NLPService nlp = new NLPService();

        private DatabaseService databaseService = new DatabaseService();

        private List<ActivityLog> activityLogs = new List<ActivityLog>();

        delegate void MessageDelegate(string text);

        private ObservableCollection<ChatMessage> messages;

        private ChatbotService chatbot;

        private VoiceService voice;

        private UserMemory memory;

        private MessageDelegate botDelegate;

        private MediaPlayer player;

        public MainWindow()
        {
            InitializeComponent();

            questions = quizService.GetQuestions();

            AddActivity("Quiz loaded.");

            DisplayQuestion();

            LoadTasks();

            CheckReminders();

            MessageDelegate botDelegate;

            messages = new ObservableCollection<ChatMessage>();

            ChatList.ItemsSource = messages;

            chatbot = new ChatbotService();

            voice = new VoiceService();

            memory = new UserMemory();

            botDelegate = AddBotMessage;

            player = new MediaPlayer();

            Greeting();
        }

        private void Greeting()
        {
            string greeting =
                "Hello. Welcome to the Cybersecurity Awareness Chatbot";

            AddBotMessage(greeting);

            player.Open(new Uri(
                @"C:\Users\Student\source\repos\ChatbotCybersecurityPart2\ChatbotCybersecurityPart2\Greeting Part 2.wav"));

            player.Play();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInput.Text;

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                return;
            }

            AddUserMessage(userMessage);

            bool memoryDetected = DetectMemory(userMessage);

            if (memoryDetected == true)
            {
                SaveConversation();
                UserInput.Clear();
                return;
            }

            // ================= NLP INTEGRATION =================
            string intent = nlp.DetectIntent(userMessage);

            if (intent == "TASK")
            {
                MainTabControl.SelectedIndex = 1;   // Tasks tab

                AddBotMessage("I've opened the Tasks tab for you.");
                voice.Speak("Opening task assistant.");

                AddActivity("📋 Opened Tasks tab using NLP.");

                UserInput.Clear();
                return;
            }

            else if (intent == "QUIZ")
            {
                MainTabControl.SelectedIndex = 2;   // Quiz tab

                AddBotMessage("I've opened the Cybersecurity Quiz for you.");
                voice.Speak("Opening quiz.");

                AddActivity("🧠 Opened Quiz using NLP.");

                UserInput.Clear();
                return;
            }

            else if (intent == "LOG")
            {
                MainTabControl.SelectedIndex = 3;   // Activity Log tab

                AddBotMessage("I've opened your Activity Log.");
                voice.Speak("Showing activity log.");

                AddActivity("📜 Opened Activity Log using NLP.");

                UserInput.Clear();
                return;
            }

            // ===================================================

            string response = chatbot.GetResponse(userMessage);

            if (string.IsNullOrWhiteSpace(response))
            {
                response = "I'm not sure I understand. Can you try rephrasing?";
            }

            AddBotMessage(response);
            voice.Speak(response);

            if (userMessage.ToLower() == "show conversation")
            {
                string history = File.ReadAllText("ConversationHistory.txt");

                AddBotMessage(history);
                voice.Speak("Showing conversation history");

                UserInput.Clear();
                return;
            }

            SaveConversation();
            UserInput.Clear();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskItem task = new TaskItem
            {
                Title = TaskTitle.Text,
                Description = TaskDescription.Text,
                ReminderDate = ReminderDatePicker.SelectedDate,
                IsCompleted = false
            };

            databaseService.AddTask(task);

            AddActivity($"📋 Task added: '{task.Title}'.");

            MessageBox.Show("Task added successfully!");

            LoadTasks();
            AddActivity($"📋 Task added: '{task.Title}'.");

            TaskTitle.Clear();
            TaskDescription.Clear();
            ReminderDatePicker.SelectedDate = null;
        }

        private void LoadTasks()
        {
            TaskList.ItemsSource = null;
            TaskList.ItemsSource = databaseService.GetAllTasks();

            UpdateDashboard();
        }

        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem task)
            {
                databaseService.MarkTaskComplete(task.Id);

                AddActivity($"✅ Task completed: '{task.Title}'.");

                MessageBox.Show("Task marked as completed.");

                LoadTasks();
            }
            else
            {
                MessageBox.Show("Please select a task.");
            }
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem task)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to delete '{task.Title}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    databaseService.DeleteTask(task.Id);

                    AddActivity($"🗑️ Task deleted: '{task.Title}'.");

                    MessageBox.Show("Task deleted successfully!");

                    LoadTasks();
                    AddActivity($"🗑️ Task deleted: '{task.Title}'.");
                }
            }
            else
            {
                MessageBox.Show("Please select a task first.");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTasks();
        }
        private void AddUserMessage(string text)
        {
            messages.Add(new ChatMessage
            {
                Message = text,
                IsBot = false,
                TimeStamp = DateTime.Now
            });

            Dispatcher.BeginInvoke(new Action(() =>
            {
                ScrollChatToBottom();
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void AddBotMessage(string text)
        {
            messages.Add(new ChatMessage
            {
                Message = text,
                IsBot = true,
                TimeStamp = DateTime.Now
            });

            Dispatcher.BeginInvoke(new Action(() =>
            {
                ScrollChatToBottom();
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void ScrollChatToBottom()
        {
            ChatScrollViewer.ScrollToEnd();
        }

        private bool DetectMemory(string input)
        {
            string lower = input.ToLower();

            //Remember the user's name
            if (lower.Contains("name"))
            {
                string[] words = input.Split(' ');

                memory.UserName = words[words.Length - 1];

                string reply =
                    $"Nice to meet you {memory.UserName}. I'll remember your name";

                AddBotMessage(reply);

                voice.Speak(reply);

                return true;
            }

            //Remember the user's favourite topic
            string[] topics =
            {
                "privacy",
                "password",
                "phishing",
                "scam"
            };

            foreach (string topic in topics)
            {
                if ((lower.Contains("i like") ||
                    lower.Contains("interested in"))
                    &&
                    lower.Contains("topic"))
                {
                    memory.FavouriteTopic = topic;

                    string reply =
                        $"Great! I'll remeber that you're interested in {topic}";

                    AddBotMessage(reply);

                    voice.Speak(reply);

                    return true;
                }
            }

            return false;
        }

        private void SaveConversation()
        {
            using (StreamWriter writer =
                new StreamWriter("ConversationHistory.txt"))
            {
                foreach (var message in messages)
                {
                    writer.WriteLine(
                        $"{message.TimeStamp} - {message.Message}");
                }
            }
        }
        private void AddActivity(string action)
        {
            activityLogs.Insert(0, new ActivityLog
            {
                TimeStamp = DateTime.Now,
                Action = action
            });

            // ❌ REMOVE limiting logic (this was breaking Show More)
            // if (activityLogs.Count > 10)
            //     activityLogs.RemoveAt(10);

            ActivityLogList.ItemsSource = null;
            ActivityLogList.ItemsSource = activityLogs.Take(logDisplayCount).ToList();
        }

        private void DisplayQuestion()
        {
            // Quiz finished
            if (currentQuestion >= questions.Count)
            {
                QuestionText.Text = "🎉 Quiz Complete!";

                OptionsList.Visibility = Visibility.Collapsed;

                string feedback;

                if (score >= 9)
                {
                    feedback = "🏆 Excellent! You're a cybersecurity pro!";
                }
                else if (score >= 7)
                {
                    feedback = "🎉 Great job! You have strong cybersecurity knowledge.";
                }
                else if (score >= 5)
                {
                    feedback = "🙂 Good effort! Keep learning to stay safe online.";
                }
                else
                {
                    feedback = "📚 Keep practising. Cybersecurity is an important skill!";
                }

                ScoreText.Text =
                    $"Final Score: {score}/{questions.Count}\n\n{feedback}";

                AddActivity($"🏆 Quiz completed. Final score: {score}/{questions.Count}.");

                return;
            }

            // Display current question
            QuestionText.Text =
                $"Question {currentQuestion + 1} of {questions.Count}\n\n" +
                questions[currentQuestion].Question;

            OptionsList.ItemsSource = questions[currentQuestion].Options;

            OptionsList.SelectedIndex = -1;

            ScoreText.Text = $"Current Score: {score}";
        }

        private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (OptionsList.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            if (OptionsList.SelectedIndex == questions[currentQuestion].CorrectAnswer)
            {
                score++;

                MessageBox.Show(
                    "✅ Correct!\n\n" +
                    questions[currentQuestion].Explanation);
            }
            else
            {
                MessageBox.Show(
                    "❌ Incorrect.\n\n" +
                    questions[currentQuestion].Explanation);
            }

            currentQuestion++;

            DisplayQuestion();
        }

        private void RestartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            currentQuestion = 0;
            score = 0;

            questions = quizService.GetQuestions();

            OptionsList.Visibility = Visibility.Visible;

            DisplayQuestion();

            AddActivity("Quiz restarted.");

            MessageBox.Show("Quiz restarted successfully!");
        }

        private void CheckReminders()
        {
            var reminders = databaseService.GetDueReminders();

            if (reminders.Count > 0)
            {
                string message = "🔔 You have the following reminders:\n\n";

                foreach (var task in reminders)
                {
                    message += $"• {task.Title} (Due: {task.Reminder:dd MMM yyyy})\n";
                }

                MessageBox.Show(message, "Cybersecurity Reminders");

                AddActivity("🔔 Reminder notification displayed.");
            }
        }

        private void UpdateDashboard()
        {
            var tasks = databaseService.GetAllTasks();

            if (tasks == null)
                return;

            int total = tasks.Count;
            int completed = tasks.Count(t => t.IsCompleted);
            int pending = total - completed;

            TotalTasksText.Text = $"📋 Total: {total}";
            CompletedTasksText.Text = $"✅ Completed: {completed}";
            PendingTasksText.Text = $"🟠 Pending: {pending}";
        }

        private void ShowMoreLog_Click(object sender, RoutedEventArgs e)
        {
            logDisplayCount += 5;

            ActivityLogList.ItemsSource = activityLogs
                .Take(logDisplayCount)
                .ToList();

            AddActivity("📜 Activity log expanded.");
        }
    }

}