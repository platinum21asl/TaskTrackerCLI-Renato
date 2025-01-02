using System;

namespace TaskTrackerCLI
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "todo";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
