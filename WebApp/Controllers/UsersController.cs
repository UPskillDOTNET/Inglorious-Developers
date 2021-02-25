using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
                    return View(await _webUserService.GetAllUsers());
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
                    return View(await _webUserService.GetUserById(id));
                }
                catch
                {
                    return NotFound();
                }
            }
        }
    }

