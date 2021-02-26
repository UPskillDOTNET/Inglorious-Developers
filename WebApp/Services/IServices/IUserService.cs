﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;

namespace WebApp.Services.IServices
{
    public interface IUserService
    {
        public Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers();

        public Task<ActionResult<UserDTO>> GetUserById(string id);

        //public Task<ActionResult<UserDTO>> UpdateUserById(string id);

        //Task<ActionResult<WebApp_UserDTO>> CreateUser(WebApp_UserDTO userDTO, string currency);

        //Task<ActionResult<WebApp_UserDTO>> DeleteUserProfile(string id);
    }
}
