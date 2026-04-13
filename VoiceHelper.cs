using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media; //For WAV playback
using System.Speech.Synthesis; //For bot voice
using static System.Console;

namespace CyberSecurityChatBot
{
    internal class VoiceHelper
    {
        //Method to play WAV greeting
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("C:\\Users\\Student\\source\\repos\\CyberSecurityChatBot\\CyberSecurityChatBot\\Welcoming Voice Message.wav");
                player.PlaySync(); //Wait until finished
            }
            catch (Exception)
            {
                WriteLine("Could not play greeting audio!");
            }
        }

        //Method for Bot to speak
        public static void Speak(string message)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak(message);
        }
    }
}
