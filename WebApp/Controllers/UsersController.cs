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
        private readonly IWalletService _walletService;

        public UsersController(IUserService userService, IWalletService walletService)
        {
            _webUserService = userService;
            _walletService = walletService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.User.FindFirst("sub")?.Value;
           
            try
            {
                var vm = await _webUserService.GetUserById(id);
                ViewData["user"] = vm.Value;
                var wallet = await Wallet();
                vm.Value.walletDTO = wallet.Value;
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

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
            try
            {
                await _webUserService.CreateUser(userDTO);
                return RedirectToAction("Index", "Users");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<ActionResult<WalletDTO>> Wallet()
        {
            var id = HttpContext.User.FindFirst("sub")?.Value;
            var vm = await _walletService.GetUserWalletById(id);
            return vm;
        }

        public IActionResult Deposit()
        {
            
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(WalletDTO walletDTO)
        {
            var id = HttpContext.User.FindFirst("sub")?.Value;
            var value = walletDTO.totalAmount;
            await _walletService.Deposit(id, value);
            return RedirectToAction("Index", "Users");
           
        }
    }
}

