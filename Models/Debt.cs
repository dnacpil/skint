using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace skint.Models;
public class Debt
{
    [Key]
    public int DebtID { get; set; }

    public required string Description { get; set; } // see if you wanna make this nullable
    public double AmountOwed { get; set; }

    public DateTime Due { get; set; }

    public string? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual IdentityUser? User { get; set; }

}