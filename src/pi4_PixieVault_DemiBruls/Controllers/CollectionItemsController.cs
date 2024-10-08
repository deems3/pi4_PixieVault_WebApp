using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pi4_PixieVault_DemiBruls.Database;
using pi4_PixieVault_DemiBruls.Models;

namespace pi4_PixieVault_DemiBruls.Controllers
{
    public class CollectionItemsController : Controller
    {
        private readonly DatabaseContext _context;

        public CollectionItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: CollectionItems
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.CollectionItems.Include(c => c.Category)
                .Include(c => c.Picture);
            return View(await databaseContext.ToListAsync());
        }

        // GET: CollectionItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionItem = await _context.CollectionItems
                .Include(c => c.Category)
                .Include(c => c.Picture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectionItem == null)
            {
                return NotFound();
            }

            return View(collectionItem);
        }

        // GET: CollectionItems/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["CollectionItemPictureId"] = new SelectList(_context.Pictures, "Id", "FileName");
            return View();
        }

        // POST: CollectionItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,CategoryId,Name,Description,Color,Material,Price")] CollectionItem collectionItem, IFormFile? file)
        {
            // Valideer alleen het GIF-bestand zonder het in ModelState te zetten
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("FileName", "De File is verplicht.");
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var uploadsDirName = "uploads";
                    var filePath = Path.Combine($"wwwroot/{uploadsDirName}", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Sla het pad van het bestand op in het productmodel
                    var picture = new Picture
                    {
                        FileName = fileName,
                        FilePath = $"/{uploadsDirName}/{fileName}"
                    };
                    collectionItem.Picture = picture;
                }

                // Sla het product op in de database
                _context.CollectionItems.Add(collectionItem);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", collectionItem.CategoryId);
            return View(collectionItem);
        }

        // GET: CollectionItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionItem = await _context.CollectionItems.Include(i => i.Picture).FirstOrDefaultAsync(i => i.Id == id);
            if (collectionItem == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", collectionItem.CategoryId);
            return View(collectionItem);
        }

        // POST: CollectionItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Name,Description,Color,Material,Price")] CollectionItem collectionItem, IFormFile? file)
        {
            if (id != collectionItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    if (file != null)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var uploadsDirName = "uploads";
                        var filePath = Path.Combine($"wwwroot/{uploadsDirName}", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Sla het pad van het bestand op in het productmodel
                        var picture = new Picture
                        {
                            FileName = fileName,
                            FilePath = $"/{uploadsDirName}/{fileName}"
                        };
                        collectionItem.Picture = picture;
                    }

                    _context.Update(collectionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionItemExists(collectionItem.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", collectionItem.CategoryId);
            ViewData["Picture"] = new SelectList(_context.Pictures, "Id", "FileName", collectionItem.PictureId);
            return View(collectionItem);
        }

        // GET: CollectionItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionItem = await _context.CollectionItems
                .Include(c => c.Category)
                .Include(c => c.Picture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectionItem == null)
            {
                return NotFound();
            }

            return View(collectionItem);
        }

        // POST: CollectionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collectionItem = await _context.CollectionItems.FindAsync(id);
            if (collectionItem != null)
            {
                _context.CollectionItems.Remove(collectionItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionItemExists(int id)
        {
            return _context.CollectionItems.Any(e => e.Id == id);
        }
    }
}
