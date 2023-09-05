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

    //Edit an item
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Income>>> Edit(int? id)
    {
        if (id == null || _db.Income == null)
        {
            return NotFound();
        }

        var income = await _db.Income.FindAsync(id);
        if (income == null)
        {
            return NotFound();
        }
        return View(income);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostEdit(int id, [Bind("Source, Amount")] Income income)
    {

        if (ModelState.IsValid)
        {
            _db.Update(income);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }
        return View(new Income());
    }

    private bool IncomeExists(int IncomeID)
    {
        throw new NotImplementedException();
    }

    // Delete item
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.Income == null)
        {
            return NotFound();
        }

        var income = await _db.Income
            .FirstOrDefaultAsync(m => m.IncomeID == id);
        if (income == null)
        {
            return NotFound();
        }

        return View(income);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PostDelete(int? id)
    {
        var income = _db.Income.Find(id);

        if (income == null)
        {
            return NotFound();
        }
        _db.Income.Remove(income);
        _db.SaveChangesAsync();
        //return RedirectToAction(nameof(Delete));
        return RedirectToRoute("../Home");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
