using Microsoft.AspNetCore.Mvc;
using PMS_Serverv1.Services;
using PMSv1_Shared.Entities.Filters.FilterModel;
using PMSv1_Shared.Entities.Models;

namespace PMS_Serverv1.Controllers
{
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _task;
        public TaskController(ITaskService taskService)
        {
            _task = taskService;
        }

        [HttpGet("get-task")]
        public async Task<IActionResult> GetTask(string id)
        {
            try
            {
                var task_id = Guid.TryParse(id, out Guid result);
                var get_task = await _task.GetTask(result);
                if (get_task.IsSuccess)
                    return Ok(get_task);
                return BadRequest(get_task);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e);
            }
        }

        [HttpGet("get-tasks")]
        public async Task<IActionResult> GetTasks(List<FilterRequest> filter)
        {
            try
            {
                var get_tasks = await _task.GetTasks(filter);
                if (get_tasks.IsSuccess)
                    return Ok(get_tasks);
                return BadRequest(get_tasks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpPost("manage-task")]
        public async Task<IActionResult> ManageTask(Tasks task)
        {
            try
            {
                var manage_tasks = await _task.ManageTask(task);
                if (manage_tasks.IsSuccess)
                    return Ok(manage_tasks);
                return BadRequest(manage_tasks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpDelete("delete-task")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            try
            {
                var delete_task = await _task.DeleteTask(Guid.Parse(id));
                if (delete_task.IsSuccess)
                    return Ok(delete_task);
                return BadRequest(delete_task);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
    }
}
