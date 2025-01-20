using BankApi;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OnlineCasino.Areas.Identity.Data;
using OnlineCasino.Repository;
using OnlineCasino.Repository.IRepository;
using OnlineCasino.Service;
using OnlineCasino.Service.IService;
using OnlineCasino.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<IdentityDbContext>()
				.AddDefaultTokenProviders();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IDepositWithdrawRepository, DepositWithdrawRepository>();
builder.Services.AddScoped<ICallbackRepository, CallbackRepository>();
builder.Services.AddScoped<IBankingService, BankingService>();

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	await DbInitializer.SeedRolesAndAdmin(services);
}

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
