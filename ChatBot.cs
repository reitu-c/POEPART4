using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace CyberSecurityChatBot
{
    internal class ChatBot
    {
        //Automatic property
        public string UserName { get; set; }

        //Start chat method
        public void StartChat()
        {
            ForegroundColor = ConsoleColor.Yellow;

            //Prompt user to enter their name
            VoiceHelper.Speak("Please enter your name");
            Write("Enter your name: ");
            UserName = ReadLine();

            //Input validation
            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserName = "User";
            }

            //Welcome the user with their name
            string welcome = $"Hello {UserName}! Welcome to the Cybersecurity Awareness Bot";
            WriteLine(welcome);
            VoiceHelper.Speak(welcome);

            ShowHelp();

            ShowMenu();
        }

        //Method to show what the user can ask
        private void ShowHelp()
        {

            string helpMessage = (@"
You can ask me about the following:
How am I doing
My purpose
What you can ask");

            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(helpMessage);

            VoiceHelper.Speak(helpMessage);
        }

        //Chat loop
        private void ShowMenu()
        {
            while (true)
            {
                //Divider line before each question
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine("------------------------------------------------------------------------------------------------------------------------");

                ForegroundColor = ConsoleColor.Blue;
                string prompt = "Ask me something, type 'exit' to quit: ";

                //Display text
                WriteLine("\n" + prompt);

                //Make bot speak the prompt
                VoiceHelper.Speak(prompt);

                string input = ReadLine();

                //Display what the user typed with their name
                ForegroundColor = ConsoleColor.Green;
                Write(UserName + ": ");

                //Message in another colour
                ForegroundColor = ConsoleColor.White;
                WriteLine(input);

                //Convert to lowercase after displaying
                input = input.ToLower();

                //Input validation
                if (string.IsNullOrWhiteSpace(input))
                {
                    Respond("I didn't quite understand that. Could you rephrase?");

                    //Show help again
                    ShowHelp();

                    continue;
                }

                if (input == "exit")
                {
                    Respond("Goodbye " + UserName + "! Stay safe online");
                    break;
                }

                HandleResponse(input);
            }
        }

        //Handles chatbot responses
        private void HandleResponse(string input)
        {
            if (input.Contains("how are you?"))
            {
                Respond("I'm just a bot, but I'm here to help you stay safe online!");
            }
            else if (input.Contains("purpose"))
            {
                Respond("My purpose is to educate you about cybersecurity and keep you safe online");
            }
            else if (input.Contains("what can i ask you about?"))
            {
                Respond("You can ask me about passwords, phishing and, safe browsing");
            }
            else if (input.Contains("password"))
            {
                Respond("Use strong passwords with uppercase, lowercase, numbers and symbols");
            }
            else if (input.Contains("phishing"))
            {
                Respond("Phishing is when attackers trick you into giving them your personal information. Be careful of suspicious emails or links. Always verify the sender");
            }
            else if (input.Contains("browsing"))
            {
                Respond("Always user secure websites (https) and avoid public Wi-Fi for sensitive data");
            }
            else
            {
                Respond("I didn't quite understand that. Could you rephrase?");

                ShowHelp();
            }
        }

        //Method to respond with voice + text
        public void Respond(string message)
        {
            //Bot name in one colour
            ForegroundColor = ConsoleColor.Magenta;
            Write("CyberBot: ");

            //Message in another colour
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine(message);

            //Bot speaks
            VoiceHelper.Speak(message);
        }
    }
}
