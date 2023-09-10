using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace skint.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

    }
}





    /* [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    } */
