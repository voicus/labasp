using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Phone_Market;
using Phone_Market.Code;
using Phone_Market.Models;
using Phone_Market.Services.EmailImplementation;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddDbContext<Phone_MarketContext>(o =>
{
    o.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCurrentUser();
builder.Services.AddBusinessLogic();
builder.Services.AddPolicy();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
builder.Services.AddTransient<IEmailSender, EmailSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
