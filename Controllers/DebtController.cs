using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using skint.Data;
using skint.Models;

namespace skint.Controllers;

[Route("api/[controller]/[action]")]
public class DebtController : Controller

{
    private readonly skintIdentityDbContext _db;

    public DebtController(skintIdentityDbContext context)
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
        ViewData["DescriptionSortParm"] = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
        ViewData["AmountOwedSortParam"] = sortOrder == "AmountOwed" ? "amount_owed_desc" : "AmountOwed";
        ViewData["DueSortParm"] = sortOrder == "Due" ? "date_desc" : "Due";
        ViewData["CurrentFilter"] = searchString;
        var debt = from d in _db.Debt
                          select d;
        if (!String.IsNullOrEmpty(searchString))
        {
            debt = debt.Where(i => i.Description.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "description_desc":
                debt = debt.OrderByDescending(d => d.Description);
                break;
            case "AmountOwed":
                debt = debt.OrderBy(d => d.AmountOwed);
                break;
            case "amount_owed_desc":
                debt = debt.OrderByDescending(d => d.AmountOwed);
                break;
            case "date_desc":
                debt = debt.OrderByDescending(d => d.Due);
                break;
            default:
                debt = debt.OrderBy(d => d.Due);
                break;
        }
        return View(await debt.AsNoTracking().ToListAsync());
    }

    //Create new item
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<IEnumerable<Debt>>> Create([Bind("DebtID, Description, AmountOwed, Due, UserId")] Debt debt)
    {
        if (ModelState.IsValid)
        {
            _db.Add(debt);
            await _db.SaveChangesAsync();
            RedirectToAction(nameof(Index));

        }
        return View(new Debt());
    }
    //Edit an item
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Debt>>> Edit(int? id)
    {
        if (id == null || _db.Debt == null)
        {
            return NotFound();
        }

        var debt = await _db.Debt.FindAsync(id);
        if (debt == null)
        {
            return NotFound();
        }
        return View(debt);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostEdit(int id, [Bind("DebtID, Description, AmountOwed, Due, UserId")] Debt debt)
    {

        if (id != debt.DebtID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(debt);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DebtExists(debt.DebtID))
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
        return View(debt);
    }

    private bool DebtExists(int id)
    {
        return (_db.Debt?.Any(e => e.DebtID == id)).GetValueOrDefault();
    }

    // Delete an item
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.Debt == null)
        {
            return NotFound();
        }

        var debt = await _db.Debt
            .FirstOrDefaultAsync(m => m.DebtID == id);
        if (debt == null)
        {
            return NotFound();
        }

        return View(debt);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PostDelete(int? id)
    {
        var debt = _db.Debt.Find(id);

        if (debt == null)
        {
            return NotFound();
        }
        _db.Debt.Remove(debt);
        _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
