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
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-IE");
        culture.NumberFormat.CurrencyNegativePattern = 1;

        //Total Income
        List<Income> Income = await _db.Income.ToListAsync();

        double TotalIncome = Income.Sum(j => j.Amount);
        ViewBag.TotalIncome = String.Format(culture, "{0:C0}", TotalIncome);

        //Total Expense
        List<Expenses> Expenses = await _db.Expenses.ToListAsync();

        double TotalExpenses = Expenses.Sum(e => e.Cost);
        ViewBag.TotalExpenses = String.Format(culture, "{0:C0}", TotalExpenses);

        //Total Debt
        List<Debt> Debt = await _db.Debt.ToListAsync();

        double TotalDebt = Debt.Sum(d => d.AmountOwed);
        ViewBag.TotalDebt = String.Format(culture, "{0:C0}", TotalDebt);

        double TotalCost = TotalDebt + TotalExpenses;

        //Balance
        double Balance = TotalIncome - TotalCost;
        ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

        List<PieChartData> SummaryData = new List<PieChartData>
            {
 
                new PieChartData { xValue = "Income", yValue = TotalIncome },
                new PieChartData { xValue = "Debt", yValue = TotalDebt },
                new PieChartData { xValue = "Expenses", yValue = TotalExpenses },
            };
            ViewBag.dataSource = SummaryData;

        return View();

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
public class PieChartData
        {
            public string xValue;
            public double yValue;
        }

