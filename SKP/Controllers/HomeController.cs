using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SKP.Data;
using SKP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SKP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment env, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
            _environment = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.OrderBy(x => Guid.NewGuid()).ToListAsync());
        }

        public async Task<IActionResult> CategoryForBook()
        {
            return View(await _context.Categories.OrderBy(x => Guid.NewGuid()).ToListAsync());
        }
        public async Task<IActionResult> CategoryForNotes()
        {
            return View(await _context.Categories.OrderBy(x => Guid.NewGuid()).ToListAsync());
        }
        public async Task<IActionResult> CategoryForVideos()
        {
            return View(await _context.Categories.OrderBy(x => Guid.NewGuid()).ToListAsync());
        }

        public async Task<IActionResult> ViewBooksByCategory(int? id)
        {
            var applicationDbContext = _context.BooksDetails
            .Include(b => b.BooksCategories).Where(m => m.CategoryID == id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ViewNotesByCategory(int? id)
        {
            var applicationDbContext = _context.NotesDetail
            .Include(b => b.NotesCategories).Where(m => m.CategoryID == id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ViewVideoByCategory(int? id)
        {
            var applicationDbContext = _context.VideoLecDetail
            .Include(b => b.VideoCategories).Where(m => m.CategoryID == id);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DownloadBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksDetail = await _context.BooksDetails
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (booksDetail == null)
            {
                return NotFound();
            }
            else
            {
                var path = Path.Combine(_environment.WebRootPath, "bookpdfs/" + booksDetail.BookID + booksDetail.Extension);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "application/pdf", Path.GetFileName(path));

            }
            
        }

        [Authorize]
        public async Task<IActionResult> DownloadNote(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notesDetail = await _context.NotesDetail
                .FirstOrDefaultAsync(m => m.NotesID == id);
            if (notesDetail == null)
            {
                return NotFound();
            }
            else
            {
                var path = Path.Combine(_environment.WebRootPath, "notes/" + notesDetail.NotesID + notesDetail.Extension);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "application/pdf", Path.GetFileName(path));

            }

        }

        [Authorize]
        public async Task<IActionResult> DownloadVideo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoLecDetail = await _context.VideoLecDetail
                .FirstOrDefaultAsync(m => m.VidID == id);
            if (videoLecDetail == null)
            {
                return NotFound();
            }
            else
            {
                var path = Path.Combine(_environment.WebRootPath, "videos/" + videoLecDetail.VidID + videoLecDetail.Extension);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "video/mp4", Path.GetFileName(path));

            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
