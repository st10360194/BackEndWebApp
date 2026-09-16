using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly IFirebaseService _firebaseService;

        public TasksController(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _firebaseService.GetAllTasks();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(string id)
        {
            var task = await _firebaseService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask(Tasks task)
        {
            var createdTask = await _firebaseService.CreateTask(task);

            return CreatedAtAction(
                nameof(GetTaskById),
                new { id = createdTask.Id },
                createdTask);
        }
    }
}
