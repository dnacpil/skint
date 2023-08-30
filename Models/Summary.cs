using System.ComponentModel.DataAnnotations;
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
    }
}