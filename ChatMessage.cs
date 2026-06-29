using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotCybersecurityPart2.Models
{
    internal class ChatMessage
    {
        public string Message { get; set; }

        public bool IsBot { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
