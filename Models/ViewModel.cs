using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace skint.Models;
public class ModelsCombined{

    public List<Debt>? Debts { get; set; }
    public List<Expenses>? Expenses { get; set; }
    public List<Income>? Incomes { get; set; }
}
