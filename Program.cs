using Microsoft.AspNetCore.Authentication.Cookies;
using SSR_Agile.Models;
using SSR_Agile.Utility;
using SSR_Agile.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Add SQLite Database
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuth")
.AddCookie("CookieAuth", option =>
{
    option.LoginPath = "/Account/Login";
    option.LogoutPath = "/Account/Logout";
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.ExpireTimeSpan = TimeSpan.FromDays(7);
    option.SlidingExpiration = true;
});

builder.Services.AddAuthorization();

//test log in account
/*
List<User> users = new List<User>
{
 new User {Id=1,Username="admin",PasswordHash=HashHelper.ComputeSha256Hash("123456")}
};

builder.Services.AddSingleton(users);*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();//加入身分驗證
app.UseAuthorization();//加入授權機制

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapDefaultControllerRoute();
app.Run();
