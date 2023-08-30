using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace skint.Models;

public class Expenses{

    [Key]
    public int ExpenseID {get; set;}
    public required string Description { get; set; }
    public double Cost { get; set; }
    public DateTime Due { get; set; }
}