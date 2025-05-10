using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace NET_Core_Web_API_HiringTest.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpPost("CreateTask")]
        [Authorize]
        public async Task<IActionResult> CreateTask([FromBody] TaskItems task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        
        [HttpGet("{id}")]
       [Authorize]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.TaskItems
                .Include(t => t.AssignedUser)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

       
        [HttpGet("user/{userId}")]
       [Authorize]
        public async Task<IActionResult> GetTasksForUser(int userId)
        {
            var tasks = await _context.TaskItems
                .Where(t => t.AssignedUserId == userId)
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("GetAllTaskItmes")]
        public async Task<ActionResult<List<TaskItems>>> GetTaskItems()
        {
            var tasks = await _context.TaskItems.ToListAsync();
            return Ok(tasks);
        }

    }
}
