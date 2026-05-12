using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerCLI_Renato.Models;

namespace TaskTrackerCLI_Renato.Repositories
{
    public interface ITaskRepository
    {
        List<TaskItem> GetAll();
        void SaveAll(List<TaskItem> tasks);
    }
}
