using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using skint.Data;
using skint.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
        //Total Income
        List<Income> Income = await _db.Income.ToListAsync();

        double TotalIncome = Income.Sum(j => j.Amount);
        ViewBag.TotalIncome = TotalIncome.ToString("C0");

        //Total Expense
        List<Expenses> Expenses = await _db.Expenses.ToListAsync();

        double TotalExpenses = Expenses.Sum(e => e.Cost);
        ViewBag.TotalExpenses = TotalExpenses.ToString("C0");

        //Total Debt
        List<Debt> Debt = await _db.Debt.ToListAsync();

        double TotalDebt = Debt.Sum(d => d.AmountOwed);
        ViewBag.TotalDebt = TotalExpenses.ToString("C0");

        double TotalCost = TotalDebt + TotalExpenses;

        //Balance
        double Balance = TotalIncome - TotalCost;
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-IE");
        culture.NumberFormat.CurrencyNegativePattern = 1;
        ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

        return View();


    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

