using Moq;
using Xunit;
using TaskTrackerCLI_Renato.Models;
using TaskTrackerCLI_Renato.Repositories;
using TaskTrackerCLI_Renato.Services;

namespace TaskTracker.Tests
{
    public class TaskServiceTests
    {
        [Fact]
        public void GetTasks_ShouldReturnAllTasks()
        {
            var mockRepo = new Mock<ITaskRepository>();

            mockRepo.Setup(repo => repo.GetAll()).Returns(new List<TaskItem>
            {
                new TaskItem { Id = 1, Description = "Belajar xUnit", Status = "todo" },
                new TaskItem { Id = 2, Description = "Belajar Moq", Status = "done" }
            });

            var taskService = new TaskService(mockRepo.Object);

            var result = taskService.GetTasks();

            Assert.Equal(2, result.Count); 
            Assert.Equal("Belajar xUnit", result[0].Description);
        }

        [Fact]
        public void AddTask_ShouldSaveTaskWithCorrectId()
        {
            var mockRepo = new Mock<ITaskRepository>();

            var existingTasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Description = "Task Lama", Status = "todo" }
            };
            mockRepo.Setup(repo => repo.GetAll()).Returns(existingTasks);

            var taskService = new TaskService(mockRepo.Object);

            taskService.AddTask("Task Baru");
            mockRepo.Verify(repo => repo.SaveAll(It.Is<List<TaskItem>>(list =>
                list.Count == 2 &&
                list[1].Id == 2 &&
                list[1].Description == "Task Baru" &&
                list[1].Status == "todo"
            )), Times.Once);
        }
    }
}