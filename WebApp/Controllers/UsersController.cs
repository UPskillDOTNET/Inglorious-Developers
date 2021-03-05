using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserService _webUserService;

        public UsersController(IUserService userService)
        {
            _webUserService = userService;
        }
        public async Task<IActionResult> Index()
        {
            await _webUserService.GetAllUsers();
            try
            {
                return View(_webUserService.GetAllUsers().Result.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                return View(_webUserService.GetUserById(id).Result.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        //public async Task<IActionResult> Edit(string id)
        //{
        //    try
        //    {
        //        return View(_webUserService.UpdateUserById(id).Result.Value);
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //}

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDTO, string currency)
        {
            try
            {
                await _webUserService.CreateUser(userDTO, currency);
                return RedirectToAction("Index", "Users");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

