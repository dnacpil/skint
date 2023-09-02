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
public class ExpensesController : Controller

{
    private readonly skintIdentityDbContext _db;

    public ExpensesController(skintIdentityDbContext context)
    {
        _db = context;
    }

    //Create new item to Expenses
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return _db.Expenses != null ?
            View(await _db.Expenses.ToListAsync()) :

            Problem("Entity set 'skintIdentityDbcontext.Expenses'  is null.");
    }
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<IEnumerable<Expenses>>> Create([Bind("Description, Cost, Due")] Expenses expense)
    {
        if (ModelState.IsValid)
        {
            _db.Add(expense);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }
        return View(new Expenses());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
