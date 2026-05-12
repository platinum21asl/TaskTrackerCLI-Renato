using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using TaskTrackerCLI_Renato.Repositories;
using TaskTrackerCLI_Renato.Services;

namespace TaskTrackerCLI_Renato
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITaskRepository>(new JsonTaskRepository("tasks.json"))
                .AddSingleton<ITaskService, TaskService>()
                .BuildServiceProvider();

            var taskService = serviceProvider.GetRequiredService<ITaskService>();
            var rootCommand = new RootCommand("Task Tracker CLI - Enterprise Edition by Daniel");

            // ==========================================
            // COMMAND: ADD
            // ==========================================
            var addCommand = new Command("add", "Add a new task to your list");
            var descArgument = new Argument<string>("The description of the task");
            addCommand.AddArgument(descArgument);
            addCommand.SetHandler((string description) =>
            {
                taskService.AddTask(description);
                AnsiConsole.MarkupLine($"[green]✓ Success:[/] Task added successfully.");
            }, descArgument);

            // ==========================================
            // COMMAND: LIST
            // ==========================================
            var listCommand = new Command("list", "Display all tasks");
            var statusOption = new Option<string?>("--status", "Filter by status (todo, in-progress, done)");
            listCommand.AddOption(statusOption);
            listCommand.SetHandler((status) =>
            {
                var tasks = taskService.GetTasks(status);
                if (tasks.Count == 0)
                {
                    AnsiConsole.MarkupLine("[yellow]ℹ No tasks found.[/]");
                    return;
                }

                var table = new Table();
                table.Border(TableBorder.Rounded);
                table.AddColumn(new TableColumn("[bold cyan]ID[/]").Centered());
                table.AddColumn(new TableColumn("[bold white]Description[/]"));
                table.AddColumn(new TableColumn("[bold yellow]Status[/]").Centered());
                table.AddColumn(new TableColumn("[bold grey]Last Updated[/]"));

                foreach (var t in tasks)
                {
                    string statusColor = t.Status switch
                    {
                        "todo" => "red",
                        "in-progress" => "yellow",
                        "done" => "green",
                        _ => "white"
                    };

                    table.AddRow(
                        t.Id.ToString(),
                        t.Description,
                        $"[{statusColor}]{t.Status}[/]",
                        $"[grey]{t.UpdatedAt:yyyy-MM-dd HH:mm}[/]"
                    );
                }
                AnsiConsole.Write(table);

            }, statusOption);

            // ==========================================
            // COMMAND: UPDATE
            // ==========================================
            var updateCommand = new Command("update", "Update the description of an existing task");
            var updateIdArg = new Argument<int>("id", "The ID of the task");
            var updateDescArg = new Argument<string>("description", "The new description");
            updateCommand.AddArgument(updateIdArg);
            updateCommand.AddArgument(updateDescArg);
            updateCommand.SetHandler((id, desc) =>
            {
                taskService.UpdateTask(id, desc);
                AnsiConsole.MarkupLine("[green]✓ Task updated successfully.[/]");
            }, updateIdArg, updateDescArg);

            // ==========================================
            // COMMAND: DELETE
            // ==========================================
            var deleteCommand = new Command("delete", "Delete a task by ID");
            var deleteIdArg = new Argument<int>("id", "The ID of the task");
            deleteCommand.AddArgument(deleteIdArg);
            deleteCommand.SetHandler((id) =>
            {
                taskService.DeleteTask(id);
                AnsiConsole.MarkupLine("[green]✓ Task deleted successfully.[/]");
            }, deleteIdArg);

            // ==========================================
            // COMMAND: MARK (In-Progress / Done)
            // ==========================================
            var markProgressCommand = new Command("mark-in-progress", "Mark a task as in-progress");
            var progressIdArg = new Argument<int>("id", "Task ID");
            markProgressCommand.AddArgument(progressIdArg);
            markProgressCommand.SetHandler((id) => {
                taskService.UpdateStatus(id, "in-progress");
                AnsiConsole.MarkupLine("[yellow]▶ Task marked as in-progress.[/]");
            }, progressIdArg);

            var markDoneCommand = new Command("mark-done", "Mark a task as done");
            var doneIdArg = new Argument<int>("id", "Task ID");
            markDoneCommand.AddArgument(doneIdArg);
            markDoneCommand.SetHandler((id) => {
                taskService.UpdateStatus(id, "done");
                AnsiConsole.MarkupLine("[green]✓ Task marked as done.[/]");
            }, doneIdArg);

            rootCommand.AddCommand(addCommand);
            rootCommand.AddCommand(listCommand);
            rootCommand.AddCommand(updateCommand);
            rootCommand.AddCommand(deleteCommand);
            rootCommand.AddCommand(markProgressCommand);
            rootCommand.AddCommand(markDoneCommand);

            return await rootCommand.InvokeAsync(args);
        }
    }
}