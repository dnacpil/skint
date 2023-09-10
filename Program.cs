using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using skint.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("skintIdentityDbContextConnection") ??
    throw new InvalidOperationException("Connection string'skintIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<skintIdentityDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<skintIdentityDbContext>();

var app = builder.Build();

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBPh8sVXJ2S0R+WVpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jQH5Wd0BnXH9fdX1cRw==;Mgo+DSMBMAY9C3t2V1hiQlRPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXdTc0RmXXpecXJUT2E=");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
