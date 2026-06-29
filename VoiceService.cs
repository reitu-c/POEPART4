using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace ChatbotCybersecurityPart2.Services
{
    internal class VoiceService
    {
        private SpeechSynthesizer synthesizer;

        public VoiceService()
        {
            synthesizer = new SpeechSynthesizer();

            synthesizer.Rate = 0;
            synthesizer.Volume = 100;
        }

        public void Speak(string text)
        {
            synthesizer.SpeakAsync(text);
        }
    }
}
