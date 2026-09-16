using WebApplication1.Models;

namespace WebApplication1.DTOS
{
    public class TaskDTO
    {

        public int Id { get; set; }
        public String Name { get; set; } = String.Empty;
        TaskType tye { get; set; } = TaskType.Ice;

        DateTime dueDate;

    }
}
