using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VIMS.Data;

namespace VIMS.Controllers
{
    public class MasterController : Controller
    {
        private readonly BRTAContext _context;

        public MasterController(BRTAContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Notice()
        {
            return View(await _context.Notices.OrderByDescending(n => n).ToListAsync());
        }
        //citizen data detail
        public async Task<IActionResult> NoticeView(int? id)
        {
            if (id == null || _context.Notices == null)
            {
                return NotFound();
            }

            var user = await _context.Notices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}