using Microsoft.AspNetCore.Mvc;
using PMS_Serverv1.Services;
using PMSv1_Shared.Entities.Contracts;
using PMSv1_Shared.Entities.DTOs;

namespace PMS_Serverv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _client;
        public ClientController(IClientService client)
        {
            _client = client;
        }

        [HttpPost("manage-client")]
        public async Task<IActionResult> ManageClient(ClientDto client)
        {
            try
            {
                var result = await _client.ManageClient(client);
                if(result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = e.Message });
            }
        }

        [HttpGet("get-clients")]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var result = await _client.GetClients();
                if(result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = e.Message });
            }
        }

        [HttpGet("get-client")]
        public async Task<IActionResult> GetClient(string guid)
        {
            try
            {
                if(string.IsNullOrEmpty(guid))
                    return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Client Id is required." });
                var client = await _client.GetClient(Guid.Parse(guid));
                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = e.Message });
            }
        }

        [HttpDelete("delete-client")]
        public async Task<IActionResult> DeleteClient(string guid)
        {
            try
            {
                if (string.IsNullOrEmpty(guid))
                    return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = "Client Id is required." });
                var client = await _client.DeleteClient(Guid.Parse(guid));
                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = e.Message });
            }
        }
    }
}
