using System.Diagnostics;
using System.Security.Claims;
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


    public async Task<IActionResult> Index(string? sortOrder, string searchString)
    {
        if (_db.Debt == null)
        {
            return NotFound();
        }

        ViewData["DescriptionSortParm"] = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
        ViewData["CostSortParam"] = sortOrder == "Cost" ? "cost_desc" : "Cost";
        ViewData["DueSortParm"] = sortOrder == "Due" ? "date_desc" : "Due";
        var expense = from e in _db.Expenses
                      select e;
        if (!String.IsNullOrEmpty(searchString))
        {
            expense = expense.Where(i => i.Description.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "description_desc":
                expense = expense.OrderByDescending(e => e.Description);
                break;
            case "Cost":
                expense = expense.OrderBy(e => e.Cost);
                break;
            case "cost_desc":
                expense = expense.OrderByDescending(e => e.Cost);
                break;
            case "date_desc":
                expense = expense.OrderByDescending(e => e.Due);
                break;
            default:
                expense = expense.OrderBy(e => e.Due);
                break;
        }
        return View(await expense.AsNoTracking().ToListAsync());
    }

    //Create new item 
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<IEnumerable<Expenses>>> Create([Bind("ExpenseID, Description, Cost, Due, UserId")] Expenses expense)
    {
        if (ModelState.IsValid)
        {
            _db.Add(expense);
            await _db.SaveChangesAsync();
            RedirectToAction(nameof(Index));
        }
        return View(new Expenses());
    }

    //Edit an item
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expenses>>> Edit(int? id)
    {
        if (id == null || _db.Expenses == null)
        {
            return NotFound();
        }

        var expense = await _db.Expenses.FindAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        return View(expense);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostEdit(int id, [Bind("ExpenseID, Description, Cost, Due, UserId")] Expenses expense)
    {

        if (id != expense.ExpenseID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(expense);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpensesExists(expense.ExpenseID))
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
        return View(expense);
    }

    private bool ExpensesExists(int id)
    {
        return (_db.Expenses?.Any(e => e.ExpenseID == id)).GetValueOrDefault();
    }

    // Delete an item
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.Expenses == null)
        {
            return NotFound();
        }

        var expense = await _db.Expenses
            .FirstOrDefaultAsync(m => m.ExpenseID == id);
        if (expense == null)
        {
            return NotFound();
        }

        return View(expense);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PostDelete(int? id)
    {
        var expense = _db.Expenses.Find(id);

        if (expense == null)
        {
            return NotFound();
        }
        _db.Expenses.Remove(expense);
        _db.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
