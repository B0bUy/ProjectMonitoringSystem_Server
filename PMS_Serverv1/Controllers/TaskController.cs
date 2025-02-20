using Microsoft.AspNetCore.Mvc;
using PMS_Serverv1.Services;
using PMSv1_Shared.Entities.Filters.FilterModel;

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
    }
}
