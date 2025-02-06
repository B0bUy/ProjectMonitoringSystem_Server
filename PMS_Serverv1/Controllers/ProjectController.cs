using Microsoft.AspNetCore.Mvc;
using PMS_Serverv1.Entities.Models;
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
    }
}
