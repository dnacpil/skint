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

    [HttpGet]
    public async Task<IActionResult> Index(string? sortOrder, string searchString)
    {
        if (_db.Debt == null)
        {
            return NotFound();
        }

        ViewData["SourceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "source_desc" : "";
        ViewData["AmountOwedSortParam"] = sortOrder == "AmountOwed" ? "amount_owed_desc" : "AmountOwed";
        ViewData["CurrentFilter"] = searchString;

        var income = from i in _db.Income
                     select i;
        if (!String.IsNullOrEmpty(searchString))
        {
            income = income.Where(i => i.Source.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "source_desc":
                income = income.OrderByDescending(i => i.Source);
                break;
            case "amount_owed_desc":
                income = income.OrderByDescending(i => i.Amount);
                break;
            default:
                income = income.OrderBy(i => i.Amount);
                break;
        }
        return View(await income.AsNoTracking().ToListAsync());
    }

    // Create an item
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<IEnumerable<Income>>> Create([Bind("IncomeID, Source, Amount")] Income income)
    {
        if (ModelState.IsValid)
        {
            _db.Add(income);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    public async Task<IActionResult> PostEdit(int id, [Bind("IncomeID, Source, Amount")] Income income)
    {

        if (id != income.IncomeID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(income);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(income.IncomeID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(income);
    }

    private bool IncomeExists(int id)
    {
        return (_db.Income?.Any(e => e.IncomeID == id)).GetValueOrDefault();
    }

    // Delete an item
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
        return RedirectToAction(nameof(Index));

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
