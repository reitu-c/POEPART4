using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace CyberSecurityChatBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Set console colour for better UI
            ForegroundColor = ConsoleColor.Cyan;

            //Display ASCII Art Logo
            AsciiArt.DisplayLogo();

            //Play voice greeting (WAV file)
            VoiceHelper.PlayGreeting();

            //Create ChatBot object
            ChatBot bot = new ChatBot();

            //Start ChatBot interaction
            bot.StartChat();

            //Keep console open
            ReadLine();
        }
    }
}
