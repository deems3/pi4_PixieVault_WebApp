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
    public class UserItemsController : Controller
    {
        private readonly DatabaseContext _context;

        public UserItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: UserItems
        public async Task<IActionResult> Index()
        {
             //DEZE IS GOED var databaseContext = _context.UserItems.Include(u => u.CollectionItem).Include(u => u.CollectionItemPicture).Include(u => u.User);
            var databaseContext = _context.UserItems.Include(u => u.CollectionItem).Include(u => u.User);
            return View(await databaseContext.ToListAsync());
        }

        // GET: UserItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userItem = await _context.UserItems
                .Include(u => u.CollectionItem)
                //.Include(u => u.CollectionItemPicture)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userItem == null)
            {
                return NotFound();
            }

            return View(userItem);
        }

        // GET: UserItems/Create
        public IActionResult Create()
        {
            ViewData["CollectionItemId"] = new SelectList(_context.CollectionItems, "Id", "Name");
            //ViewData["CollectionItemPictureId"] = new SelectList(_context.Set<Picture>(), "Id", "FileName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: UserItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //DEZE IS GOED public async Task<IActionResult> Create([Bind("Id,UserId,CollectionItemId,CollectionItemPictureId,IsForSale,ReleaseDate,State,ForSalePrice")] UserItem userItem)
        public async Task<IActionResult> Create([Bind("Id,UserId,CollectionItemId,IsForSale,ReleaseDate,State,ForSalePrice")] UserItem userItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollectionItemId"] = new SelectList(_context.CollectionItems, "Id", "Name", userItem.CollectionItemId);
            //ViewData["CollectionItemPictureId"] = new SelectList(_context.Set<Picture>(), "Id", "FileName", userItem.CollectionItemPictureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userItem.UserId);
            return View(userItem);
        }

        // GET: UserItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userItem = await _context.UserItems.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }
            ViewData["CollectionItemId"] = new SelectList(_context.CollectionItems, "Id", "Color", userItem.CollectionItemId);
            //ViewData["CollectionItemPictureId"] = new SelectList(_context.Set<Picture>(), "Id", "FileName", userItem.CollectionItemPictureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userItem.UserId);
            return View(userItem);
        }

        // POST: UserItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //DEZE IS GOED public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CollectionItemId,CollectionItemPictureId,IsForSale,ReleaseDate,State,ForSalePrice")] UserItem userItem)
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CollectionItemId,IsForSale,ReleaseDate,State,ForSalePrice")] UserItem userItem)
        {
            if (id != userItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserItemExists(userItem.Id))
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
            ViewData["CollectionItemId"] = new SelectList(_context.CollectionItems, "Id", "Color", userItem.CollectionItemId);
            //ViewData["CollectionItemPictureId"] = new SelectList(_context.Set<Picture>(), "Id", "FileName", userItem.CollectionItemPictureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userItem.UserId);
            return View(userItem);
        }

        // GET: UserItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userItem = await _context.UserItems
                .Include(u => u.CollectionItem)
                //.Include(u => u.CollectionItemPicture)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userItem == null)
            {
                return NotFound();
            }

            return View(userItem);
        }

        // POST: UserItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userItem = await _context.UserItems.FindAsync(id);
            if (userItem != null)
            {
                _context.UserItems.Remove(userItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserItemExists(int id)
        {
            return _context.UserItems.Any(e => e.Id == id);
        }
    }
}
