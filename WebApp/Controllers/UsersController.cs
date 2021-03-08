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
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NifSortParm = String.IsNullOrEmpty(sortOrder) ? "nif_desc" : "";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";


            var users = from u in _webUserService.GetAllUsers().Result.Value
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.name.Contains(char.ToUpper(searchString[0]) + searchString.Substring(1))
                                || u.nif.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.name);
                    break;
                case "nif_desc":
                    users = users.OrderByDescending(u => u.nif);
                    break;
                case "email_desc":
                    users = users.OrderByDescending(u => u.email);
                    break;
                default:
                    users = users.OrderBy(u => u.name);
                    break;
            }

            return View(users.ToList());
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

