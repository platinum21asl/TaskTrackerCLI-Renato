using System.Collections.Generic;
using TaskTrackerCLI_Renato.Models;

namespace TaskTrackerCLI_Renato.Services
{
    public interface ITaskService
    {
        void AddTask(string description);
        void UpdateTask(int id, string description);
        void DeleteTask(int id);
        void UpdateStatus(int id, string status);
        List<TaskItem> GetTasks(string? statusFilter = null);
    }
}