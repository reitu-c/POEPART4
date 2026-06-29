using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotCybersecurityPart2.Services
{
    internal class NLPService
    {
        public string DetectIntent(string input)
        {
            input = input.ToLower();

            // Task-related keywords
            if (input.Contains("task") ||
                input.Contains("remind") ||
                input.Contains("reminder") ||
                input.Contains("2fa") ||
                input.Contains("two-factor") ||
                input.Contains("password"))
            {
                return "TASK";
            }

            // Quiz-related keywords
            if (input.Contains("quiz") ||
                input.Contains("game") ||
                input.Contains("question") ||
                input.Contains("test"))
            {
                return "QUIZ";
            }

            // Activity log keywords
            if (input.Contains("activity") ||
                input.Contains("log") ||
                input.Contains("what have you done") ||
                input.Contains("history"))
            {
                return "LOG";
            }

            return "UNKNOWN";
        }
    }
}
