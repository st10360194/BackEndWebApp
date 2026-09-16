
using WebApplication1.Models;
namespace WebApplication1.Services
{
    public interface IFirebaseService
    {
        Task<List<Tasks>> GetAllTasks();
        Task<Tasks?> GetTaskById(string id);

        Task<Tasks> CreateTask(Tasks task);
    }
}
