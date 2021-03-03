using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CentralAPI.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository,IWalletService walletService , IMapper mapper)
        {
            _userRepository = userRepository;
            _walletService = walletService;
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

            if (await find(id) == false)
            {
                throw new ArgumentNullException(nameof(id), "Not Found");
            }

            var user = await _userRepository.GetUsersById(id);
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }


        public async Task<ActionResult<UserDTO>> GetUserEmail(string Email)
        {

            var user = await _userRepository.GetUsersByEmail(Email);
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

        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO, string currency)
        {
            var user = _mapper.Map<UserDTO, User>(userDTO);
            user.password = GetMD5(userDTO.password);
            user.paymentMethodID = "1";
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _userRepository.CreateUser(user);
                    await _walletService.CreateWallet(user.userID, currency);
                }
                catch (Exception ex)
                {
                    Transaction.Current.Rollback(ex);

                }
                scope.Complete();
            }



            userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }


        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
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
