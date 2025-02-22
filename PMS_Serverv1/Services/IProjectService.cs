using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS_Serverv1.Data;
using PMSv1_Shared.Entities.Models;
using PMSv1_Shared.Entities.Contracts;
using PMSv1_Shared.Entities.Filters.FilterModel;
using System.Linq.Expressions;
using PMSv1_Shared.Helpers;

namespace PMS_Serverv1.Services
{
    public interface IProjectService
    {
        Task<ApiResponse<Project>> GetProject(Expression<Func<Project, bool>> filter);
        Task<ApiResponse<List<Project>>> GetProjects(List<FilterRequest> filter);
        Task<ApiResponse> ManageProject(Project project);
        Task<ApiResponse> DeleteProject(Guid projectId);
    }

    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _db;
        public ProjectService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<Project>> GetProject(Expression<Func<Project, bool>> filter)
        {
            try
            {
                var project = await _db.Projects.AsNoTracking().FirstOrDefaultAsync(filter);
                return new ApiResponse<Project>()
                {
                    StatusCode = project != null ? 200 : 404,
                    IsSuccess = project != null,
                    Message = project != null ? "Project found." : "Project not found.",
                    Result = project ?? new()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ExceptionHandler.Handle<Project>(ex);
            }
        }
        public async Task<ApiResponse<List<Project>>> GetProjects(List<FilterRequest> filter)
        {
            try
            {
                var projects = await FilterProjects(filter);
                return new ApiResponse<List<Project>>()
                {
                    StatusCode = projects.Count > 0 ? 200 : 404,
                    IsSuccess = projects.Count > 0,
                    Message = projects.Count > 0 ? "Projects found." : "Projects not found.",
                    Result = projects ?? new()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ExceptionHandler.Handle<List<Project>>(ex);
            }
        }
        public async Task<ApiResponse> ManageProject(Project project)
        {
            try
            {
                if (project.ProjectId == Guid.Empty)
                    return await AddProject(project);
                return await UpdateProject(project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ExceptionHandler.Handle(e);
            }
        }
        private async Task<ApiResponse> AddProject(Project project)
        {
            try
            {
                var isExist = await _db.Projects.AnyAsync(p => p.Name.ToLower() == project.Name.ToLower());
                if (isExist)
                    return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = "Project already exists." };
                await _db.Projects.AddAsync(project);
                await _db.SaveChangesAsync();
                return new ApiResponse() { StatusCode = 200, IsSuccess = true, Message = "Project added successfully." };       
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ExceptionHandler.Handle(e);
            }
        }
        private async Task<ApiResponse> UpdateProject(Project project)
        {
            try
            {
                var isExist = await _db.Projects.FirstOrDefaultAsync(p => p.Name.ToLower() == project.Name.ToLower());
                if (isExist == null)
                    return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = "Project does not exists." };
                isExist.Name = project.Name;
                _db.Projects.Update(isExist);
                await _db.SaveChangesAsync();
                return new ApiResponse() { StatusCode = 200, IsSuccess = true, Message = "Project added successfully." };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ExceptionHandler.Handle(e);
            }
        }
        public async Task<ApiResponse> DeleteProject(Guid projectId)
        {
            try
            {
                var project = await _db.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectId);
                if (project == null)
                    return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = "Project not found." };
                _db.Projects.Remove(project);
                await _db.SaveChangesAsync();
                return new ApiResponse() { StatusCode = 200, IsSuccess = true, Message = "Project deleted successfully." };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ExceptionHandler.Handle(e);
            }
        }


        [NonAction]
        private async Task<List<Project>> FilterProjects(List<FilterRequest> filter)
        {
            try
            {
                var project_status = await _db.ProjectStatus.AsNoTracking().ToListAsync();
                var filters = new List<Expression<Func<Project, bool>>>
                {
                    //Created At
                    x => filter.Any(c => c.FilterType == 0) ?
                         x.CreatedAt == DateTime.Parse(filter.FirstOrDefault(condition => condition.FilterType == 0)!.FilterValue)
                         : true,
                    //Deadline
                    x => filter.Any(c => c.FilterType == 1) ?
                         x.Deadline.HasValue ? x.Deadline.Value == DateTime.Parse(filter.FirstOrDefault(condition => condition.FilterType == 2)!.FilterValue)
                         : true : true,
                    //Name
                    x => filter.Any(c => c.FilterType == 5) ?
                         x.Name == filter.FirstOrDefault(condition => condition.FilterType == 6)!.FilterValue
                         : true,
                };
                // Combine all expressions using AND
                var combinedExpression = ExpressionCombiner.CombineAnd(filters);
                var get_projects = await _db.Projects.AsNoTracking().Where(combinedExpression).ToListAsync();
                return get_projects?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new();
            }
        }
    }
}
