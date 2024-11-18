using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNewApiProject.Data;
using MyNewApiProject.Models;

namespace MyNewApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTask>>> GetUserTasks()
        {
            try
            {
                var tasks = await _context.UserTasks.Include(ut => ut.User).Include(ut => ut.Category).ToListAsync();
                return Ok(tasks); // Return 200 OK with list of tasks
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/UserTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTask>> GetUserTask(int id)
        {
            try
            {
                var userTask = await _context.UserTasks.Include(ut => ut.User).Include(ut => ut.Category).FirstOrDefaultAsync(ut => ut.Id == id);

                if (userTask == null)
                {
                    return NotFound(); // Return 404 if user task not found
                }

                return Ok(userTask); // Return 200 OK with the user task
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/UserTasks
        [HttpPost]
        public async Task<ActionResult<UserTask>> PostUserTask(UserTask userTask)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Return 400 if validation fails
                }

                _context.UserTasks.Add(userTask);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserTask), new { id = userTask.Id }, userTask); // Return 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/UserTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTask(int id, UserTask userTask)
        {
            if (id != userTask.Id)
            {
                return BadRequest("Task ID mismatch");
            }

            try
            {
                _context.Entry(userTask).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.UserTasks.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/UserTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTask(int id)
        {
            try
            {
                var userTask = await _context.UserTasks.FindAsync(id);
                if (userTask == null)
                {
                    return NotFound();
                }

                _context.UserTasks.Remove(userTask);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
