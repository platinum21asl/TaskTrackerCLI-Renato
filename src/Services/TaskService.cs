using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackerCLI_Renato.Models;
using TaskTrackerCLI_Renato.Repositories;

namespace TaskTrackerCLI_Renato.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public void AddTask(string description)
        {
            var tasks = _repository.GetAll();
            var newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;

            var newTask = new TaskItem
            {
                Id = newId,
                Description = description,
                Status = "todo",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            tasks.Add(newTask);
            _repository.SaveAll(tasks);
            Console.WriteLine($"Task added successfully (ID: {newId})");
        }

        public void UpdateTask(int id, string description)
        {
            var tasks = _repository.GetAll();
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            task.Description = description;
            task.UpdatedAt = DateTime.Now;

            _repository.SaveAll(tasks);
            Console.WriteLine("Task updated successfully.");
        }

        public void DeleteTask(int id)
        {
            var tasks = _repository.GetAll();
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            tasks.Remove(task);
            _repository.SaveAll(tasks);
            Console.WriteLine("Task deleted successfully.");
        }
        public void UpdateStatus(int id, string status)
        {
            var tasks = _repository.GetAll();
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            task.Status = status;
            task.UpdatedAt = DateTime.Now;

            _repository.SaveAll(tasks);
            Console.WriteLine($"Task marked as {status}.");
        }
        public List<TaskItem> GetTasks(string? statusFilter = null)
        {
            var tasks = _repository.GetAll();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                tasks = tasks.Where(t => t.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return tasks;
        }
    }
}