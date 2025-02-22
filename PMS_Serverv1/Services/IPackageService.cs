using Microsoft.EntityFrameworkCore;
using PMS_Serverv1.Data;
using PMSv1_Shared.Entities.Models;
using PMSv1_Shared.Entities.Contracts;
using PMSv1_Shared.Entities.DTOs;

namespace PMS_Serverv1.Services
{
    public interface IPackageService
    {
        Task<ApiResponse> ManagePackage(PackageDto package);
        Task<ApiResponse> DeletePackage(string packageId);
        Task<ApiResponse<List<PackageDto>>> GetPackages();
        Task<ApiResponse<PackageDto>> GetPackage(string guid);
    }

    public class PackageService : IPackageService
    {
        private readonly ApplicationDbContext _db;
        public PackageService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse> ManagePackage(PackageDto package)
        {
            try
            {
                if (package.PackageId == Guid.Empty)
                    return await AddPackage(package);
                return await UpdatePackage(package);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message };
            }
        }

        async Task<ApiResponse> AddPackage(PackageDto package)
        {
            try
            {
                var getPackage = await _db.Packages.AsNoTracking().FirstOrDefaultAsync(p => p.Name.ToLower() == package.Name.ToLower());
                if (getPackage is not null)
                    return new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Package already exists." };
                var newpackage = new Package
                {
                    Name = package.Name,
                    Description = package.Description,
                    Color = string.IsNullOrEmpty(package.Color) ? "#000000" : package.Color,
                };
                await _db.Packages.AddAsync(newpackage);
                await _db.SaveChangesAsync();
                return new ApiResponse { StatusCode = 200, IsSuccess = true, Message = "Package created successfully." };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message };
            }
        }
        async Task<ApiResponse> UpdatePackage(PackageDto package)
        {
            try
            {
                var getPackage = await _db.Packages.AsNoTracking().FirstOrDefaultAsync(p => p.PackageId == package.PackageId);
                if (getPackage is null)
                    return new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Package does not exists." };
                var packageExists = await _db.Packages.AsNoTracking().FirstOrDefaultAsync(p => p.Name.ToLower() == package.Name.ToLower());
                if (packageExists is not null)
                    return new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Package already exists." };
                getPackage.Name = package.Name;
                getPackage.Description = package.Description;
                getPackage.Color = string.IsNullOrEmpty(package.Color) ? "#000000" : package.Color;
                _db.Packages.Update(getPackage);
                await _db.SaveChangesAsync();
                return new ApiResponse { StatusCode = 200, IsSuccess = true, Message = "Package Updated successfully." };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse> DeletePackage(string packageId)
        {
            try
            {
                var getPackage = await _db.Packages.AsNoTracking().FirstOrDefaultAsync(p => p.PackageId == Guid.Parse(packageId));
                if (getPackage is null)
                    return new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Package does not exists." };
                _db.Packages.Remove(getPackage);
                await _db.SaveChangesAsync();
                return new ApiResponse { StatusCode = 200, IsSuccess = true, Message = "Package deleted successfully." };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message };
            }
        }
        public async Task<ApiResponse<List<PackageDto>>> GetPackages()
        {
            try
            {
                var packages = await _db.Packages.AsNoTracking().ToListAsync();
                if (packages is null || packages.Count() == 0)
                    return new ApiResponse<List<PackageDto>> { StatusCode = 500, IsSuccess = false, Message = "No packages found." };
                return new ApiResponse<List<PackageDto>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Packages retrieved successfully.",
                    Result = (from package in packages
                              select new PackageDto
                              {
                                  PackageId = package.PackageId,
                                  Name = package.Name,
                                  Description = package.Description,
                                  Color = package.Color,
                              }).ToList(),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse<List<PackageDto>> { StatusCode = 500, IsSuccess = false, Message = ex.Message };
            }
        }
        public async Task<ApiResponse<PackageDto>> GetPackage(string guid)
        {
            try
            {
                var parseGuid = Guid.TryParse(guid, out Guid id);
                if (!parseGuid)
                    return new ApiResponse<PackageDto> { StatusCode = 500, IsSuccess = false, Message = "Invalid package id." };
                var packages = await _db.Packages.AsNoTracking().FirstOrDefaultAsync(c => c.PackageId == id);
                if (packages is null)
                    return new ApiResponse<PackageDto> { StatusCode = 500, IsSuccess = false, Message = "No packages found." };
                return new ApiResponse<PackageDto>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Packages retrieved successfully.",
                    Result = new PackageDto
                    {
                        PackageId = packages.PackageId,
                        Name = packages.Name,
                        Description = packages.Description,
                        Color = packages.Color,
                    },
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse<PackageDto> { StatusCode = 500, IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
