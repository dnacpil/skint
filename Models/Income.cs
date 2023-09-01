using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace skint.Models;

public class Income
{

    [Key]
    public int IncomeID { get; set; }

    public string? Source { get; set; }
    public double Amount { get; set; }
    public string? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual IdentityUser? User { get; set; }

}