using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.User.FindFirst("sub")?.Value;
           
            try
            {
                var vm = await _webUserService.GetUserById(id);
                ViewData["user"] = vm.Value;
                return View(vm.Value);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
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

