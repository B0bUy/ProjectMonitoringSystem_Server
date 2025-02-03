using Microsoft.AspNetCore.Mvc;
using PMS_Serverv1.Entities.Models;
using PMS_Serverv1.Services;
using PMSv1_Shared.Entities.Contracts;

namespace PMS_Serverv1.Controllers
{
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _package;
        public PackageController(IPackageService packageService)
        {
            _package = packageService;
        }

        [HttpGet("get-package")]
        public async Task<IActionResult> Get(string guid)
        {
            try
            {
                if(string.IsNullOrEmpty(guid))
                    return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Package Id is required." });
                var package = await _package.GetPackage(guid);
                return Ok(package);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpGet("get-packages")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var package = await _package.GetPackages();
                if (package is null)
                    return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Package not found." });
                return Ok(new ApiResponse<List<Package>> { StatusCode = 200, IsSuccess = true, Message = "Package found.", Result = package.Result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPost("manage-package")]
        public async Task<IActionResult> ManagePackage(Package package)
        {
            try
            {
                var result = await _package.ManagePackage(package);
                if (result is null)
                    return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Package not found." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpDelete("delete-package")]
        public async Task<IActionResult> DeletePackage(string packageId)
        {
            try
            {
                var result = await _package.DeletePackage(packageId);
                if (result is null)
                    return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Package not found." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
