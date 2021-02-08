using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();
            var UserDTO = _mapper.Map<List<User>, List<UserDTO>>(users.ToList());
            return UserDTO;
        }

        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var user = await _userRepository.GetUsersById(id);
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<ActionResult<UserDTO>> UpdateUserById(string id, UserDTO userDTO)
        {
            var user = _mapper.Map<UserDTO, User>(userDTO);
            await _userRepository.UpdateUserById(id, user);
            return userDTO;
        }
    }
}
