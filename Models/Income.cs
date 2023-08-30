using System.ComponentModel.DataAnnotations;

namespace skint.Models;

public class Income
{

    [Key]
    public int IncomeID { get; set; }

    public string? Source { get; set; }
    public double Amount { get; set; }

}