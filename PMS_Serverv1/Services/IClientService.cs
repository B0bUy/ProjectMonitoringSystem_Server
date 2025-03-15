using Microsoft.EntityFrameworkCore;
using PMS_Serverv1.Data;
using PMSv1_Shared.Entities.Contracts;
using PMSv1_Shared.Entities.DTOs;
using PMSv1_Shared.Entities.Models;
using PMSv1_Shared.Helpers;

namespace PMS_Serverv1.Services
{
    public interface IClientService
    {
        Task<ApiResponse<ClientDto>> GetClient(Guid id);
        Task<ApiResponse> DeleteClient(Guid id);
        Task<ApiResponse<List<ClientDto>>> GetClients();
        Task<ApiResponse> ManageClient(ClientDto client);
    }
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _db;
        public ClientService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<ClientDto>> GetClient(Guid id)
        {
            try
            {
                var get_client = await _db.Clients.FindAsync(id);
                return new ApiResponse<ClientDto>()
                {
                    StatusCode = get_client == null ? 404 : 200,
                    IsSuccess = get_client != null,
                    Message = get_client == null ? "Client not found." : "Client found.",
                    Result = get_client == null ? new() :
                            new ClientDto
                            {
                                ClientId = get_client.ClientId,
                                Name = get_client.Name,
                                Description = get_client.Description,
                                Logo = get_client.Logo,
                                Color = get_client.Color,
                            },  
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ExceptionHandler.Handle<ClientDto>(e);
            }
        }
        public async Task<ApiResponse> DeleteClient(Guid id)
        {
            try
            {
                var get_client = await _db.Clients.FindAsync(id);
                if (get_client == null)
                    return new ApiResponse { StatusCode = 404, IsSuccess = false, Message = "Client not found." };
                _db.Clients.Remove(get_client);
                await _db.SaveChangesAsync();
                return new ApiResponse { StatusCode = 200, IsSuccess = true, Message = "Client deleted successfully." };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ExceptionHandler.Handle(e);
            }
        }
        public async Task<ApiResponse<List<ClientDto>>> GetClients()
        {
            try
            {
                var get_clients = await _db.Clients.AsNoTracking().ToListAsync();
                return new ApiResponse<List<ClientDto>>()
                {
                    StatusCode = get_clients.Count == 0 ? 404 : 200,
                    IsSuccess = get_clients.Count != 0,
                    Message = get_clients.Count == 0 ? "No clients found." : "Clients found.",
                    Result = get_clients.Count() == 0 ? new() :
                            (from client in get_clients
                             select new ClientDto
                             {
                                 ClientId = client.ClientId,
                                 Name = client.Name,
                                 Description = client.Description,
                                 Logo = client.Logo,
                                 Color = client.Color,
                                 CreatedAt = client.CreatedAt,
                             }).OrderBy(c => c.UpdatedAt.HasValue ? c.UpdatedAt.Value : c.CreatedAt).ToList(),
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionHandler.Handle<List<ClientDto>>(e);
            }
        }
        public async Task<ApiResponse> ManageClient(ClientDto client)
        {
            try
            {
                if(client.ClientId == Guid.Empty)
                    return await AddClient(client);
                return await UpdateClient(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionHandler.Handle(e);
            }
        }
        private async Task<ApiResponse> AddClient(ClientDto client)
        {
            try
            {
                var is_existing = await _db.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Name == client.Name);
                if (is_existing != null)
                    return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = "Client already exists." };
                await _db.Clients.AddAsync(new Client
                {
                    Name = client.Name,
                    Description = client.Description,
                    Logo = client.Logo,
                    Color = client.Color,
                });
                await _db.SaveChangesAsync();
                return new ApiResponse() { StatusCode = 200, IsSuccess = true, Message = "Client added successfully." };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionHandler.Handle(e);
            }
        }
        private async Task<ApiResponse> UpdateClient(ClientDto client)
        {
            try
            {
                var get_client = await _db.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.ClientId == client.ClientId);
                if (get_client == null)
                    return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = "Client does not exists." };
                var is_existing = await _db.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Name == client.Name);
                if (is_existing != null && get_client.Name != is_existing.Name)
                    return new ApiResponse() { StatusCode = 500, IsSuccess = false, Message = "Client already exists." };
                get_client.Name = client.Name;
                get_client.Description = client.Description;
                get_client.Logo = client.Logo;
                get_client.Color = client.Color;
                _db.Clients.Update(get_client);
                await _db.SaveChangesAsync();
                return new ApiResponse() { StatusCode = 200, IsSuccess = true, Message = "Client updated successfully." };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionHandler.Handle(e);
            }
        }
    }
}
