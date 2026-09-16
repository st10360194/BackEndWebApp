namespace WebApplication1.Models
{
    public class Tasks
    {

        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }


    }

    public enum TaskType { 
    
    Assignment,
    Test,
    Ice
    
    }
}
