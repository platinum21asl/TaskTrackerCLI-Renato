using System;

namespace TaskTrackerCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskService = new TaskService();

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: task-cli [command] [options]");
                return;
            }

            string command = args[0].ToLower();

            switch (command)
            {
                case "add":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Usage: task-cli add [description]");
                    }
                    else
                    {
                        string description = string.Join(" ", args[1..]);
                        taskService.AddTask(description);
                    }
                    break;

                case "update":
                    if (args.Length < 3 || !int.TryParse(args[1], out int updateId))
                    {
                        Console.WriteLine("Usage: task-cli update [id] [description]");
                    }
                    else
                    {
                        string description = string.Join(" ", args[2..]);
                        taskService.UpdateTask(updateId, description);
                    }
                    break;

                case "delete":
                    if (args.Length < 2 || !int.TryParse(args[1], out int deleteId))
                    {
                        Console.WriteLine("Usage: task-cli delete [id]");
                    }
                    else
                    {
                        taskService.DeleteTask(deleteId);
                    }
                    break;

                case "mark-in-progress":
                case "mark-done":
                    if (args.Length < 2 || !int.TryParse(args[1], out int markId))
                    {
                        Console.WriteLine($"Usage: task-cli {command} [id]");
                    }
                    else
                    {
                        string status = command == "mark-in-progress" ? "in-progress" : "done";
                        taskService.MarkTask(markId, status);
                    }
                    break;

                case "list":
                    string? statusFilter = args.Length > 1 ? args[1].ToLower() : null;
                    taskService.ListTasks(statusFilter);
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}
