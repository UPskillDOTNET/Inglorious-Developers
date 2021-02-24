using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;

namespace WebApp.Services.IServices
{
    public interface IWebApp_UserService
    {
        Task<ActionResult<IEnumerable<WebApp_UserDTO>>> GetAllUsers();

        Task<ActionResult<WebApp_UserDTO>> GetUserById(string id);

        //Task<ActionResult<WebApp_UserDTO>> UpdateUserById(string id, WebApp_UserDTO userDTO);

        //Task<ActionResult<WebApp_UserDTO>> CreateUser(WebApp_UserDTO userDTO, string currency);

        //Task<ActionResult<WebApp_UserDTO>> DeleteUserProfile(string id);
    }
}
