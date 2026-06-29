using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotCybersecurityPart2.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReminderDate { get; set; }

        public bool IsCompleted { get; set; }

        public string Status
        {
            get
            {
                return IsCompleted ? "✅ Completed" : "🟠 Pending";
            }
        }

        public string Reminder
        {
            get
            {
                return ReminderDate?.ToString("dd MMM yyy") ?? "No Reminder";
            }
        }
    }
}
