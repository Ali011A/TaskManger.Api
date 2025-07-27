using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManger.Api.Entities;
using TaskManger.Api.Interfaces;

namespace TaskManger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;   

        public TasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet] //GET /api/task: Retrieve a list of all tasks.
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();
            if (tasks == null)
            {
                return NotFound("No tasks found.");
            }

            return Ok(tasks);
        }
        [HttpGet("{id}")] //GET /api/task/{id}: Retrieve a specific task by ID.
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(task);
        }
        [HttpPost("Add-Task")] //POST /api/task: Add a new task.
        public async Task<IActionResult> AddTask([FromBody] Tasks task)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Tasks.AddAsync(task);
                await _unitOfWork.CommitAsync();
                return Ok("Task added successfully.");

            }
            return BadRequest("Invalid task data.");
        }
        [HttpPut("Update-Task/{id}")] //PUT /api/task/{id}: Update an existing task by ID.
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Tasks task)
        {
            if (id != task.Id)
            {
                return BadRequest("Task ID mismatch.");
            }
            if (ModelState.IsValid)
            {

                 _unitOfWork.Tasks.Update(task);
                await _unitOfWork.CommitAsync();
                return Ok("Task updated successfully.");
            
            }
            return BadRequest("Invalid task data.");
        }

        [HttpDelete("Delete-Task/{id}")] //DELETE /api/task/{id}: Delete a task by ID.
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            _unitOfWork.Tasks.Delete(id);
            await _unitOfWork.CommitAsync();
            return Ok("Task deleted successfully.");
        }
    }
}
