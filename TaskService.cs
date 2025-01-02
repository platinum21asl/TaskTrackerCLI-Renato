using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace TaskTrackerCLI
{
	public class TaskService
	{
		private const string FilePath = "tasks.json";
		private List<TaskItem> Tasks = new();

		public TaskService()
		{
			LoadTasks();
		}

		private void LoadTasks()
		{
			if (File.Exists(FilePath))
			{
				string json = File.ReadAllText(FilePath);
				Tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
			}
		}

		private void SaveTasks()
		{
			string json = JsonSerializer.Serialize(Tasks, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(FilePath, json);
		}

		public void AddTask(string description)
		{
			int newId = Tasks.Count > 0 ? Tasks[^1].Id + 1 : 1;
			var task = new TaskItem { Id = newId, Description = description };
			Tasks.Add(task);
			SaveTasks();
			Console.WriteLine($"Task added successfully (ID: {task.Id})");
		}

		public void UpdateTask(int id, string description)
		{
			var task = Tasks.Find(t => t.Id == id);
			if (task == null)
			{
				Console.WriteLine("Task not found.");
				return;
			}

			task.Description = description;
			task.UpdatedAt = DateTime.Now;
			SaveTasks();
			Console.WriteLine("Task updated successfully.");
		}

		public void DeleteTask(int id)
		{
			var task = Tasks.Find(t => t.Id == id);
			if (task == null)
			{
				Console.WriteLine("Task not found.");
				return;
			}

			Tasks.Remove(task);
			SaveTasks();
			Console.WriteLine("Task deleted successfully.");
		}

		public void MarkTask(int id, string status)
		{
			var task = Tasks.Find(t => t.Id == id);
			if (task == null)
			{
				Console.WriteLine("Task not found.");
				return;
			}

			task.Status = status;
			task.UpdatedAt = DateTime.Now;
			SaveTasks();
			Console.WriteLine($"Task marked as {status}.");
		}

		public void ListTasks(string? status = null)
		{
			var filteredTasks = status == null
				? Tasks
				: Tasks.FindAll(t => t.Status == status);

			if (filteredTasks.Count == 0)
			{
				Console.WriteLine("No tasks found.");
				return;
			}

			foreach (var task in filteredTasks)
			{
				Console.WriteLine($"[{task.Id}] {task.Description} - {task.Status} (Created: {task.CreatedAt}, Updated: {task.UpdatedAt})");
			}
		}
	}
}
