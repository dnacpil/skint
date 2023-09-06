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


    public async Task<IActionResult> Index()
    {
        return _db.Expenses != null ?
            View(await _db.Expenses.ToListAsync()) :

            Problem("Entity set 'skintIdentityDbcontext.Expenses'  is null.");
    }

    //Create new item 
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
    public async Task<IActionResult> PostEdit(int id, [Bind("Description, Cost, Due")] Expenses expense)
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
            return RedirectToAction("Index");
        }
        return View(expense);
    }

    private bool ExpensesExists(int expenseID)
    {
        throw new NotImplementedException();
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
