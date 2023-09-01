using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using skint.Models;

namespace skint.Data

{
    public class skintIdentityDbContext : IdentityDbContext
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        internal static IEnumerable<object> ToList()
        {
            throw new NotImplementedException();
        }


        public skintIdentityDbContext(DbContextOptions<skintIdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<Debt> Debt { get; set; } = null!;
        public DbSet<Expenses> Expenses { get; set; } = null!;
        public DbSet<Income> Income { get; set; } = null!;
        public DbSet<Summary> Summary { get; set; } = null!;
    }

}