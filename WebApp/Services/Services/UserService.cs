using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;
using WebApp.Services.Services.Utils;

namespace WebApp.Services.Services
{
    public class UserService : IUserService
    {

        private readonly APIHelper _helper;

        public UserService(APIHelper helper)
        {
            _helper = helper;
        }

        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var response = await _helper.GetClientAsync("central/users");
            return await response.Content.ReadAsAsync<List<UserDTO>>();
        }

        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var response = await _helper.GetClientAsync("api/users/" + id);
            return await response.Content.ReadAsAsync<UserDTO>();
        }
    }
}

