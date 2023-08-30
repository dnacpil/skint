using System.ComponentModel.DataAnnotations;

namespace skint.Models;
public class Debt
{
    [Key]
    public int DebtID { get; set; }

    public required string Description { get; set; } // see if you wanna make this nullable
    public double AmountOwed { get; set; }

    public DateTime Due { get; set; }
}