using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TaskTrackerCLI_Renato.Models;

namespace TaskTrackerCLI_Renato.Repositories
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;

        public JsonTaskRepository(string filePath = "tasks.json")
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<TaskItem> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        public void SaveAll(List<TaskItem> tasks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(_filePath, json);
        }
    }
}