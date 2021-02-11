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
    [Authorize(Roles = "admin")]
    public class NotesDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public NotesDetailsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        // GET: NotesDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.NotesDetail.ToListAsync());
        }

        // GET: NotesDetails/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(notesDetail);
        }

        // GET: NotesDetails/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: NotesDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotesID,NameName,Author,NotesDescription,File,CategoryID")] NotesDetail notesDetail)
        {
            using (var memoryStream = new MemoryStream())
            {
                await notesDetail.File.FormFile.CopyToAsync(memoryStream);

                string photoname = notesDetail.File.FormFile.FileName;
                notesDetail.Extension = Path.GetExtension(photoname);
                if (!".jpg.jpeg.png.pdf".Contains(notesDetail.Extension.ToLower()))
                {
                    ModelState.AddModelError("File.FormFile", "Only JPG, PNG, PDF Format is Allowed.");
                }
                else
                {
                    ModelState.Remove("Extension");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(notesDetail);
                await _context.SaveChangesAsync();

                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "notes");
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                string filename = notesDetail.NotesID + notesDetail.Extension;
                var filePath = Path.Combine(uploadsRootFolder, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await notesDetail.File.FormFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(notesDetail);
        }

        // GET: NotesDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notesDetail = await _context.NotesDetail.FindAsync(id);
            if (notesDetail == null)
            {
                return NotFound();
            }
            return View(notesDetail);
        }

        // POST: NotesDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotesID,NameName,Author,NotesDescription,Extension,CategoryID")] NotesDetail notesDetail)
        {
            if (id != notesDetail.NotesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notesDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesDetailExists(notesDetail.NotesID))
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
            return View(notesDetail);
        }

        // GET: NotesDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(notesDetail);
        }

        // POST: NotesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notesDetail = await _context.NotesDetail.FindAsync(id);
            _context.NotesDetail.Remove(notesDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesDetailExists(int id)
        {
            return _context.NotesDetail.Any(e => e.NotesID == id);
        }
    }
}
