using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS_Serverv1.Data;
using PMS_Serverv1.Entities.Models;
using PMSv1_Shared.Entities.Contracts;
using PMSv1_Shared.Entities.Filters.FilterModel;
using System.Linq.Expressions;

namespace PMS_Serverv1.Services
{
    public interface ITaskService
    {
        Task<ApiResponse<Entities.Models.Task>> GetTask(Guid taskId);
        Task<ApiResponse<List<Entities.Models.Task>>> GetTasks(List<FilterRequest> filter);
        Task<ApiResponse> TaskManage(Entities.Models.Task task);
        Task<ApiResponse> DeleteTask(Guid id);
    }

    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _db;
        public TaskService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<Entities.Models.Task>> GetTask(Guid taskId)
        {
            try
            {
                var get_task = await _db.Tasks.FindAsync(taskId);
                return new ApiResponse<Entities.Models.Task>()
                {
                    StatusCode = get_task != null ? 200 : 404,
                    IsSuccess = get_task != null,
                    Message = get_task != null ? "Task found." : "Task not found.",
                    Result = get_task ?? new(),
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ApiResponse<Entities.Models.Task>()
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = e.Message,
                    Result = new(),
                };
            }
        }
        public async Task<ApiResponse<List<Entities.Models.Task>>> GetTasks(List<FilterRequest> filter)
        {
            try
            {
                var tasks = await FilterTasks(filter);
                return new ApiResponse<List<Entities.Models.Task>>()
                {
                    StatusCode = tasks.Count > 0 ? 200 : 404,
                    IsSuccess = tasks.Count > 0,
                    Message = tasks.Count > 0 ? "Tasks found." : "Tasks not found.",
                    Result = tasks,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ApiResponse<List<Entities.Models.Task>>()
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = e.Message,
                    Result = new(),
                };
            }
        }
        public async Task<ApiResponse> TaskManage(Entities.Models.Task task)
        {
            try
            {
                if (task.TaskId == Guid.Empty)
                    return await AddTask(task);
                return await UpdateTask(task);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = e.Message };
            }
        }

        private async Task<ApiResponse> AddTask(Entities.Models.Task task)
        {
            try
            {
                var add_task = await _db.Tasks.AddAsync(task);
                await _db.SaveChangesAsync();
                return new ApiResponse() { StatusCode = 200, IsSuccess = true, Message = "Task added successfully." };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = e.Message };
            }
        }
        private async Task<ApiResponse> UpdateTask(Entities.Models.Task task)
        {
            try
            {
                var get_task = await _db.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.TaskId == task.TaskId);
                if (get_task is null)
                    return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = "Task does not exists." };
                get_task.Name = task.Name;
                get_task.Description = task.Description;
                get_task.Deadline = task.Deadline;
                get_task.ParentTaskId = task.ParentTaskId;
                _db.Tasks.Update(get_task);
                await _db.SaveChangesAsync();
                return new ApiResponse() { StatusCode = 200, IsSuccess = true, Message = "Task updated successfully." };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = e.Message };
            }
        }
        public async Task<ApiResponse> DeleteTask(Guid id)
        {
            try
            {
                var get_task = await _db.Tasks.FindAsync(id);
                if (get_task is null)
                    return new ApiResponse() { StatusCode = 404, IsSuccess = false, Message = "Task not found." };
                _db.Tasks.Remove(get_task);
                await _db.SaveChangesAsync();
                return new ApiResponse() { StatusCode = 200, IsSuccess = true, Message = "Task deleted successfully." };
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = e.Message };
            }
        }
        

        [NonAction]
        private async Task<List<Entities.Models.Task>> FilterTasks(List<FilterRequest> filter)
        {
            try
            {
                var projects = await _db.Projects.AsNoTracking().ToListAsync();
                var filters = new List<Expression<Func<Entities.Models.Task, bool>>>()
                {
                    x => filter.Any(c => c.FilterType == 0) ?  x.CreatedAt == DateTime.Parse(filter.FirstOrDefault(x => x.FilterType == 0)!.FilterValue) : true,
                    x => filter.Any(c => c.FilterType == 1) ?  x.Deadline == DateTime.Parse(filter.FirstOrDefault(x => x.FilterType == 1)!.FilterValue) : true,
                    x => filter.Any(c => c.FilterType == 3) ? x.DepartmentId == Guid.Parse(filter.FirstOrDefault(x => x.FilterType == 3)!.FilterValue) : true,
                    x => filter.Any(c => c.FilterType == 4) ? x.Project == projects.FirstOrDefault(x => x.ProjectId == Guid.Parse(filter.FirstOrDefault(x => x.FilterType == 4)!.FilterValue)) : true,
                    x => filter.Any(c => c.FilterType == 6) ? x.Name == filter.FirstOrDefault(x => x.FilterType == 6)!.FilterValue : true,
                };
                var combinedExpressions = ExpressionCombiner.CombineAnd(filters);
                var get_tasks = await _db.Tasks.AsNoTracking().Where(combinedExpressions).ToListAsync();
                return get_tasks ?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new();
            }
        }
    }
}
