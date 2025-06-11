using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SSR_Agile.Data;
using SSR_Agile.Models;
using SSR_Agile.Utility;
using SSR_Agile.ViewModels;
using System.Security.Claims;

namespace SSR_Agile.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Register() => View();
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View();
        if (_context.Users.Any(u => u.Username == model.Username))
        {
            ModelState.AddModelError("Username", "使用者名稱已存在");
            return View(model);
        }

        User user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = HashHelper.ComputeSha256Hash(model.Password)
        };
        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login() => View();


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    
    {
        if (!ModelState.IsValid)
            return View(model);

        string hash = HashHelper.ComputeSha256Hash(model.Password);
        var user = _context.Users.FirstOrDefault(
            u => u.Username == model.Username &&
            u.PasswordHash == hash);

        if (user != null)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError("", "帳號或密碼錯誤");
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]  
      public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("Login");
    }
}