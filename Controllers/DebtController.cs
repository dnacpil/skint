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
public class DebtController : Controller

{
    private readonly skintIdentityDbContext _db;

    public DebtController(skintIdentityDbContext context)
    {
        _db = context;
    }

    //Create new item to Expenses
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return _db.Debt != null ?
            View(await _db.Debt.ToListAsync()) :

            Problem("Entity set 'skintIdentityDbcontext.Debt'  is null.");
    }
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<IEnumerable<Income>>> Create([Bind("Description, Cost, Due")] Expenses expense)
    {
        if (ModelState.IsValid)
        {
            _db.Add(expense);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
