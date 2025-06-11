using Microsoft.AspNetCore.Mvc;
using SSR_Agile.Data;
using Microsoft.EntityFrameworkCore;

namespace SSR_Agile.Controllers
{
        public class DevToolsController : Controller
    {
        private readonly AppDbContext _context;

        public DevToolsController(AppDbContext context)
        {
            _context = context;
        }

        // 顯示所有使用者
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}
