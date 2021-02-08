using CentralAPI.DTO;
using CentralAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers();

        Task<ActionResult<UserDTO>> GetUserById(string id);

        Task<ActionResult<UserDTO>> UpdateUserById(string id, UserDTO userDTO);

        Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO, string currency);

    }
}
