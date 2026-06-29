using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotCybersecurityPart2.Models
{
    internal class QuizQuestions
    {
        public string Question { get; set; }

        public List<string> Options { get; set; }

        public int CorrectAnswer { get; set; }

        public string Explanation { get; set; }
    }
}
