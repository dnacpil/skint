using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using skint.Data;
using skint.Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Index()
    {
        // Retrieve expenses from the database
        var expenses = await _db.Expenses.ToListAsync();
        //Total Income

        //Total Debt

        //Total Expenses
        return View(expenses);

        
    }
//Combining the models into one view:
/* public IActionResult MyView()
{
    var combinedViewModel = new CombinedViewModel
    {
        Debts = GetDebtsFromDatabase(),
        Expenses = GetExpensesFromDatabase(),
        Incomes = GetIncomesFromDatabase()
    };

    return View(combinedViewModel);
} */

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

