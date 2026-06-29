using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotCybersecurityPart2.Services
{
    internal class ChatbotService
    {
        private Random random = new Random();

        public string LastTopic = "";

        private Dictionary<string, List<string>> keywordResponses =
            new Dictionary<string, List<string>>()
            {
                {
                    "password",
                    new List<string>()
                    {
                        "Use strong passwords with symbols and numbers",
                        "Avoid using your birthday in passwords",
                        "Use a different password for each account"
                    }
                },
                {
                    "phishing",
                    new List<string>()
                    {
                        "Never click on suspicious links",
                        "Scammers pretend to be trusted organisations",
                        "Always verify the sender's email address"
                    }
                },
                {
                    "privacy",
                    new List<string>()
                    {
                        "Review your privacy settings regularly",
                        "Avoid sharing personal information publicly",
                        "Use two-factor authentication for better privacy"
                    }
                },
                {
                    "scam",
                    new List<string>()
                    {
                        "Be careful of offers that seem too good to be true",
                        "Scammers often create urgency to pressure victims",
                        "Never share banking OTPs online"
                    }
                }
            };
        public string GetResponse(string input)
        {
            input = input.ToLower();

            //Sentiment detection
            if (input.Contains("worried"))
            {
                return "It's understandable to feel worried. Online scams are common, but staying informed helps protect you. Never click suspicious links";
            }

            if (input.Contains("frustrated"))
            {
                return "I understand that cybersecurity can feel frustrating sometimes. Take it step by step and always stay cautious online";
            }

            if (input.Contains("curious"))
            {
                return "Curiosity is great when learning Cybersecurity. Staying informed helps you stay protected online";
            }

            //Follow-up responses
            if (input.Contains("tell me more") ||
                input.Contains("another tip") ||
                input.Contains("explain more"))
            {
                if (LastTopic != "" && keywordResponses.ContainsKey(LastTopic))
                {
                    List<string> responses = keywordResponses[LastTopic];

                    int index = random.Next(responses.Count);

                    return responses[index];
                }
            }

            //Keyword recognition
            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {

                    //Ignore memory sentences
                    
                    LastTopic = keyword;

                    List<string> responses = 
                        keywordResponses[keyword];

                    int index = 
                        random.Next(responses.Count);

                    return responses[index];
                }
            }

            //Default response
            return "I'm not sure I understand. Can you rephrase that?";
        }
    }
}
