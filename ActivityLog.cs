using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotCybersecurityPart2.Models
{
    internal class ActivityLog
    {
        public DateTime TimeStamp { get; set; }

        public string Action {  get; set; }

        public override string ToString()
        {
            return $"{TimeStamp:dd MMM yyyy HH:mm} - {Action}";
        }
    }
}
