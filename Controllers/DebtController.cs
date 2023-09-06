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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return _db.Debt != null ?
            View(await _db.Debt.ToListAsync()) :

            Problem("Entity set 'skintIdentityDbcontext.Debt'  is null.");
    }
    
    //Create new item
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<IEnumerable<Debt>>> Create([Bind("Description, AmountOwed, Due")] Debt debt)
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
    public async Task<IActionResult> PostEdit(int id, [Bind("Description, AmountOwed, Due")] Debt debt)
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
            return RedirectToAction("Index");
        }
        return View(debt);
    }

    private bool DebtExists(int debtID)
    {
        throw new NotImplementedException();
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
