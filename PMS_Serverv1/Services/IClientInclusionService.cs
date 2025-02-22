using Microsoft.EntityFrameworkCore;
using PMS_Serverv1.Data;
using PMSv1_Shared.Entities.Contracts;
using PMSv1_Shared.Entities.DTOs;
using PMSv1_Shared.Entities.Models;
using PMSv1_Shared.Helpers;

namespace PMS_Serverv1.Services
{
    public interface IClientInclusionService
    {
        Task<ApiResponse<List<ClientInclusionDto>>> GetAll(Guid id);
        Task<ApiResponse> Manage(List<ClientInclusionDto> inclusions);
    }
    public class ClientInclusionService : IClientInclusionService
    {
        private readonly ApplicationDbContext _db;
        public ClientInclusionService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<List<ClientInclusionDto>>> GetAll(Guid id)
        {
            try
            {
                var get_inclusions = await _db.ClientInclusions
                                              .Where(inclusion => inclusion.ClientId == id)
                                              .AsNoTracking()
                                              .ToListAsync();
                if(get_inclusions == null || get_inclusions.Count() == 0)
                    return new ApiResponse<List<ClientInclusionDto>>()
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "No inclusions found.",
                        Result = new()
                    };
                var get_dto = (from inclusion in get_inclusions
                               join client in _db.Clients.AsNoTracking().ToList() on inclusion.ClientId equals client.ClientId
                               join package in _db.Packages.AsNoTracking().ToList() on inclusion.PackageId equals package.PackageId into _package
                               from package in _package.DefaultIfEmpty()
                               join project in _db.Projects.AsNoTracking().ToList() on inclusion.ProjectId equals project.ProjectId into _project
                               from project in _project.DefaultIfEmpty()
                               select new ClientInclusionDto
                               {
                                   ClientInclusionId = inclusion.ClientInclusionId,
                                   ClientId = inclusion.ClientId,
                                   ClientName = client.Name,
                                   PackageId = package == null ? Guid.Empty : package.PackageId,
                                   PackageName = package == null ? string.Empty : package.Name,
                                   ProjectId = project == null ? Guid.Empty : project.ProjectId,
                                   ProjectName = project == null ? string.Empty : project.Name,
                               }).ToList();
                return new ApiResponse<List<ClientInclusionDto>>()
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Inclusions found",
                    Result = get_dto,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ApiResponse<List<ClientInclusionDto>>()
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Result = new()
                };
            }
        }
        public async Task<ApiResponse> Manage(List<ClientInclusionDto> inclusions)
        {
            try
            {
                var get_existing_inclusions = (from inclusion in _db.ClientInclusions.AsNoTracking().ToList()
                                               select new ClientInclusionDto
                                               {
                                                   ClientId = inclusion.ClientId,
                                                   PackageId = inclusion.PackageId,
                                                   ProjectId = inclusion.ProjectId,
                                               }).ToList();
                var new_inclusions = inclusions.Except(get_existing_inclusions).ToList();
                if (new_inclusions != null && new_inclusions.Count() > 0)
                {
                    var add_inclusions = await AddAll(new_inclusions);
                    if (!add_inclusions.IsSuccess)
                        return add_inclusions;
                }
                var remove_inclusions = get_existing_inclusions.Except(inclusions).ToList();
                if (remove_inclusions != null && remove_inclusions.Count() > 0)
                {
                    var remove_inclusion = await RemoveAll(remove_inclusions);
                    if (!remove_inclusion.IsSuccess)
                        return remove_inclusion;
                }
                return new ApiResponse()
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Inclusions managed successfully.",
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionHandler.Handle(e);
            }
        }
        private async Task<ApiResponse> AddAll(List<ClientInclusionDto> inclusions)
        {
            try
            {
                var inclusion_dto = (from inclusion in inclusions
                                     select new ClientInclusion
                                     {
                                         ClientInclusionId = inclusion.ClientInclusionId,
                                         ClientId = inclusion.ClientId,
                                         PackageId = inclusion.PackageId,
                                         ProjectId = inclusion.ProjectId,
                                     }).ToList();
                await _db.ClientInclusions.AddRangeAsync(inclusion_dto);
                await _db.SaveChangesAsync();   
                return new ApiResponse()
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Inclusions added successfully.",
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionHandler.Handle(e);
            }
        }
        private async Task<ApiResponse> RemoveAll(List<ClientInclusionDto> inclusions)
        {
            try
            {
                var inclusion_dto = (from inclusion in inclusions
                                     select new ClientInclusion
                                     {
                                         ClientInclusionId = inclusion.ClientInclusionId,
                                         ClientId = inclusion.ClientId,
                                         PackageId = inclusion.PackageId,
                                         ProjectId = inclusion.ProjectId,
                                     }).ToList();
                _db.ClientInclusions.RemoveRange(inclusion_dto);
                await _db.SaveChangesAsync();   
                return new ApiResponse()
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Inclusions added removed.",
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionHandler.Handle(e);
            }
        }
    }
}
