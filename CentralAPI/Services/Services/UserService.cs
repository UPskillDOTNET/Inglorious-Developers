using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace CentralAPI.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletService _walletService;
        private readonly UserManager<User> userManager;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository,IWalletService walletService , IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _walletService = walletService;
            _mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();
            var UserDTO = _mapper.Map<List<User>, List<UserDTO>>(users.ToList());
            return UserDTO;
        }

        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {

            if (await find(id) == false)
            {
                throw new ArgumentNullException(nameof(id), "Not Found");
            }

            var user = await _userRepository.GetUsersById(id);
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<ActionResult<UserDTO>> UpdateUserById(string id, UserDTO userDTO)
        {

            // validação para não ser possivel alterar o nif desta pessoa?

            var user = _mapper.Map<UserDTO, User>(userDTO);
            await _userRepository.UpdateUserById(id, user);
            return userDTO;
        }

        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO)
        {    
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                try
                {
                    var userExists = await userManager.FindByNameAsync(userDTO.UserName);
                    if (userExists != null)
                        throw new Exception("User " + userDTO.UserName + " already exists!");

                    User user = new User()
                    {
                        Email = userDTO.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = userDTO.UserName,
                        name = userDTO.name,
                        nif = userDTO.nif,
                    };

                    var result = await userManager.CreateAsync(user, userDTO.Password);
                    if (!result.Succeeded)
                        throw new AggregateException("User creation failed! Please check user details and try again.");
                   
                    await _walletService.CreateWallet(user.Id);
                }
                catch (Exception ex)
                {
                    Transaction.Current.Rollback(ex);

                }
                scope.Complete();

            }
            return userDTO;
        }

        public async Task<bool> find(string id)
        {
            var user = await _userRepository.GetUsersById(id);


            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<ActionResult<UserDTO>> DeleteUserProfile(string id)
        {
            var user = await _userRepository.DeleteUserProfile(id);
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }
    }
}
