using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using skint.Data;
using skint.Models;

namespace skint.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly skintIdentityDbContext _db;

    public HomeController(ILogger<HomeController> logger, skintIdentityDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

/* 
    // GET 
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return _db.Income != null ?
            View(await _db.Income.ToListAsync()) :

            Problem("Entity set 'skintIdentityDbcontext.Income'  is null.");
    }
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<IEnumerable<Income>>> Create([Bind("Source, Amount")] Income income)
    {
        if (ModelState.IsValid)
        {
            _db.Add(income);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(new Income());
    } */


