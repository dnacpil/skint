using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace skint.Models;

public class Expenses{

    [Key]
    public int ExpenseID {get; set;}
    public string? Description { get; set; }
    public double Cost { get; set; }
    public DateTime? Due { get; set; } = DateTime.Now;

    public string? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual IdentityUser? User { get; set; }
}