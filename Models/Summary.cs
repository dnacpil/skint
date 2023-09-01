using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;

namespace skint.Models
{
    public class Summary
    {
        [Key]
        public int SummaryID { get; set; }
        public double TotalIncome { get; set; }

        public double TotalDebt { get; set; }

        public double DisposableIncome { get; set; }

        public DateTime Date { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }
    }
}