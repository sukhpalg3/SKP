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
    public class VideoLecDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;


        public VideoLecDetailsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        // GET: VideoLecDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.VideoLecDetail.ToListAsync());
        }

        // GET: VideoLecDetails/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(videoLecDetail);
        }

        // GET: VideoLecDetails/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: VideoLecDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VidID,VidName,Author,VidDescription,File,CategoryID")] VideoLecDetail videoLecDetail)
        {
            using (var memoryStream = new MemoryStream())
            {
                await videoLecDetail.File.FormFile.CopyToAsync(memoryStream);

                string photoname = videoLecDetail.File.FormFile.FileName;
                videoLecDetail.Extension = Path.GetExtension(photoname);
                if (!".mp4".Contains(videoLecDetail.Extension.ToLower()))
                {
                    ModelState.AddModelError("File.FormFile", "Only MP4 Format is Allowed.");
                }
                else
                {
                    ModelState.Remove("Extension");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(videoLecDetail);
                await _context.SaveChangesAsync();
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "videos");
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                string filename = videoLecDetail.VidID + videoLecDetail.Extension;
                var filePath = Path.Combine(uploadsRootFolder, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await videoLecDetail.File.FormFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(videoLecDetail);
        }

        // GET: VideoLecDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoLecDetail = await _context.VideoLecDetail.FindAsync(id);
            if (videoLecDetail == null)
            {
                return NotFound();
            }
            return View(videoLecDetail);
        }

        // POST: VideoLecDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VidID,VidName,Author,VidDescription,Extension,CategoryID")] VideoLecDetail videoLecDetail)
        {
            if (id != videoLecDetail.VidID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoLecDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoLecDetailExists(videoLecDetail.VidID))
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
            return View(videoLecDetail);
        }

        // GET: VideoLecDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(videoLecDetail);
        }

        // POST: VideoLecDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoLecDetail = await _context.VideoLecDetail.FindAsync(id);
            _context.VideoLecDetail.Remove(videoLecDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoLecDetailExists(int id)
        {
            return _context.VideoLecDetail.Any(e => e.VidID == id);
        }
    }
}
