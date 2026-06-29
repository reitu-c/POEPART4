using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChatbotCybersecurityPart2.Models;

namespace ChatbotCybersecurityPart2.Services
{
    internal class QuizService
    {
        public List<QuizQuestions> GetQuestions()
        {
            return new List<QuizQuestions>
    {
        new QuizQuestions
        {
            Question = "What should you do if you receive an email asking for your password?",
            Options = new List<string>
            {
                "Reply with your password",
                "Delete the email",
                "Report it as phishing",
                "Ignore it"
            },
            CorrectAnswer = 2,
            Explanation = "Reporting phishing emails helps prevent scams."
        },

        new QuizQuestions
        {
            Question = "True or False: Using the same password for every account is safe.",
            Options = new List<string>{ "True", "False" },
            CorrectAnswer = 1,
            Explanation = "Each account should have a unique password."
        },

        new QuizQuestions
        {
            Question = "Which password is the strongest?",
            Options = new List<string>
            {
                "password123",
                "John2005",
                "P@55w0rd!",
                "12345678"
            },
            CorrectAnswer = 2,
            Explanation = "Strong passwords use uppercase, lowercase, numbers and symbols."
        },

        new QuizQuestions
        {
            Question = "True or False: Two-factor authentication improves account security.",
            Options = new List<string>{ "True", "False" },
            CorrectAnswer = 0,
            Explanation = "2FA adds an extra layer of protection."
        },

        new QuizQuestions
        {
            Question = "What is phishing?",
            Options = new List<string>
            {
                "A fishing hobby",
                "A scam to steal information",
                "A computer game",
                "A firewall"
            },
            CorrectAnswer = 1,
            Explanation = "Phishing tricks users into giving away sensitive information."
        },

        new QuizQuestions
        {
            Question = "Which website is generally more secure?",
            Options = new List<string>
            {
                "http://example.com",
                "https://example.com"
            },
            CorrectAnswer = 1,
            Explanation = "HTTPS encrypts communication."
        },

        new QuizQuestions
        {
            Question = "True or False: Public Wi-Fi is always safe for banking.",
            Options = new List<string>{ "True", "False" },
            CorrectAnswer = 1,
            Explanation = "Avoid sensitive activities on public Wi-Fi unless using a VPN."
        },

        new QuizQuestions
        {
            Question = "What should you do before downloading software?",
            Options = new List<string>
            {
                "Download from any website",
                "Check if the source is trusted",
                "Disable antivirus",
                "Ignore reviews"
            },
            CorrectAnswer = 1,
            Explanation = "Always download software from trusted sources."
        },

        new QuizQuestions
        {
            Question = "True or False: Antivirus software should be kept updated.",
            Options = new List<string>{ "True", "False" },
            CorrectAnswer = 0,
            Explanation = "Updates protect against the latest threats."
        },

        new QuizQuestions
        {
            Question = "Which action helps protect your online accounts?",
            Options = new List<string>
            {
                "Sharing passwords",
                "Using weak passwords",
                "Enabling two-factor authentication",
                "Ignoring software updates"
            },
            CorrectAnswer = 2,
            Explanation = "Two-factor authentication greatly improves account security."
        }
    };
        }
    }
}
