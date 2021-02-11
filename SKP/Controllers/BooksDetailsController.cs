using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SKP.Data;
using SKP.Models;

namespace SKP.Controllers
{
    [Authorize(Roles ="admin")]
    public class BooksDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public BooksDetailsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        // GET: BooksDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.BooksDetails.ToListAsync());
        }

        // GET: BooksDetails/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(booksDetail);
        }

        // GET: BooksDetails/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: BooksDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> Create([Bind("BookID,BookName,Author,Publisher,BookDescription,File,CategoryID")] BooksDetail booksDetail)
        {
            using (var memoryStream = new MemoryStream())
            {
                await booksDetail.File.FormFile.CopyToAsync(memoryStream);

                string photoname = booksDetail.File.FormFile.FileName;
                booksDetail.Extension = Path.GetExtension(photoname);
                if (!".pdf".Contains(booksDetail.Extension.ToLower()))
                {
                    ModelState.AddModelError("File.FormFile", "Only PDF Format is Allowed.");
                }
                else
                {
                    ModelState.Remove("Extension");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(booksDetail);
                await _context.SaveChangesAsync();
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "bookpdfs");
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                string filename = booksDetail.BookID + booksDetail.Extension;
                var filePath = Path.Combine(uploadsRootFolder, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await booksDetail.File.FormFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(booksDetail);
        }

        // GET: BooksDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksDetail = await _context.BooksDetails.FindAsync(id);
            if (booksDetail == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View(booksDetail);
        }

        // POST: BooksDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,BookName,Author,Publisher,BookDescription,Extension,CategoryID")] BooksDetail booksDetail)
        {
            if (id != booksDetail.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booksDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksDetailExists(booksDetail.BookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(booksDetail);
        }

        // GET: BooksDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(booksDetail);
        }

        // POST: BooksDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booksDetail = await _context.BooksDetails.FindAsync(id);
            _context.BooksDetails.Remove(booksDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksDetailExists(int id)
        {
            return _context.BooksDetails.Any(e => e.BookID == id);
        }
    }
}
