using Microsoft.AspNetCore.Mvc;
using PMSv1_Shared.Entities.Models;
using PMS_Serverv1.Services;
using PMSv1_Shared.Entities.Contracts;
using PMSv1_Shared.Entities.Filters.FilterModel;
using System.Linq.Expressions;

namespace PMS_Serverv1.Controllers
{
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _project;
        public ProjectController(IProjectService projectService)
        {
            _project = projectService;
        }

        public ProjectFilter ProjectFilter { get; set; } = new ProjectFilter();

        [HttpGet("get-projects")]
        public async Task<IActionResult> GetProjects(List<FilterRequest> filter)
        {
            List<Project> project = new();
            try
            {
                var get_projects = await _project.GetProjects(filter);
                if (get_projects.IsSuccess)
                    return Ok(get_projects);
                return BadRequest(get_projects);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new ApiResponse<List<Project>>()
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = e.Message,
                    Result = new()
                });
            }
        }
        [HttpGet("get-project")]
        public async Task<IActionResult> GetProject(string id)
        {
            try
            {
                Expression<Func<Project, bool>> filter = x => x.ProjectId == Guid.Parse(id);
                var get_project = await _project.GetProject(filter);
                if (get_project.IsSuccess)
                    return Ok(get_project);
                return BadRequest(get_project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new ApiResponse<Project>()
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = e.Message,
                    Result = new()
                });
            }
        }
        [HttpPost("manage-project")]
        public async Task<IActionResult> ManageProject(Project project)
        {
            try
            {
                var manage_project = await _project.ManageProject(project);
                if (manage_project.IsSuccess)
                    return Ok(manage_project);
                return BadRequest(manage_project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new ApiResponse()
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = e.Message,
                });
            }
        }
        [HttpDelete("delete-project")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            try
            {
                var delete_project = await _project.DeleteProject(Guid.Parse(id));
                if(delete_project.IsSuccess)
                    return Ok(delete_project);
                return BadRequest(delete_project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new ApiResponse()
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = e.Message,
                });
            }
        }
    }
}
