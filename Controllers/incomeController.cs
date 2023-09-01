using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using skint.Data;
using skint.Models;

namespace skint.Controllers;
[Route("api/[controller]/[action]")]
//[Authorize]
public class IncomeController : Controller

{
    private readonly skintIdentityDbContext _db;

    public IncomeController(skintIdentityDbContext context)
    {
        _db = context;
    }

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
            //return RedirectToAction(nameof(Index));
        }
        return View(new Income());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
