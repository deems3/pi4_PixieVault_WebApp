using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using pi4_PixieVault_DemiBruls.Database;
using pi4_PixieVault_DemiBruls.Models;
using System.Security.Claims;

namespace pi4_PixieVault_DemiBruls.Controllers
{
    [Authorize]
    public class UserItemsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public UserItemsController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserItems
        public async Task<IActionResult> Index()
        {
            //DEZE IS GOED var databaseContext = _context.UserItems.Include(u => u.CollectionItem).Include(u => u.CollectionItemPicture).Include(u => u.User);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var databaseContext = _context.UserItems.Include(u => u.CollectionItem).Include(u => u.User).Include(u => u.Picture);
            ViewData["currentUserId"] = user!.Id;
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
                .Include(u => u.Picture)
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
            // TODO: collectionid gives back collectioncolor.
            ViewData["CollectionItemId"] = new SelectList(_context.CollectionItems, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: UserItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CollectionItemId,IsForSale,ReleaseDate,State,ForSalePrice,Value")] UserItem userItem, IFormFile? file)
        {
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
                    userItem.Picture = picture;
                }
                _context.Add(userItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollectionItemId"] = new SelectList(_context.CollectionItems, "Id", "Name", userItem.CollectionItemId);
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

            // Fetch current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            // Also fetch the user that owns the UserItem
            var userItem = await _context.UserItems.Include(userItem => userItem.User).Include(u => u.Picture).FirstOrDefaultAsync(ui => ui.Id == id);
            if (userItem == null)
            {
                return NotFound();
            }

            // If the user.Id is not the same as the UserId from the UserItem, redirect the user back to the index because the item is not theirs
            if (user!.Id != userItem.User!.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["CollectionItemId"] = new SelectList(_context.CollectionItems, "Id", "Color", userItem.CollectionItemId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userItem.UserId);
            return View(userItem);
        }

        // POST: UserItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CollectionItemId,IsForSale,ReleaseDate,State,ForSalePrice,Value")] UserItem userItem, IFormFile? file)
        {
            if (id != userItem.Id)
            {
                return NotFound();
            }

            var existingEntry = _context.UserItems.AsNoTracking().Include(ci => ci.Picture).FirstOrDefault(ci => ci.Id == userItem.Id);

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
                        userItem.Picture = picture;
                    }
                    else
                    {
                        if (existingEntry != null && existingEntry.Picture is not null)
                        {
                            userItem.Picture = existingEntry.Picture;
                        }
                    }

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
                .Include(u => u.Picture)
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

        public async Task<IActionResult> Value()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userItems = _context.UserItems.Include(ui => ui.User).Include(ui => ui.CollectionItem).Where(ui => ui.User!.Id == user!.Id);
            var itemValues = new Dictionary<string, decimal>();
            itemValues.AddRange(
                userItems.Select(ui => new KeyValuePair<string, decimal>(ui.CollectionItem!.Name, ui.Value))
                );
            var userItemsValue = await userItems.SumAsync(ui => ui.Value);
            return View(new ValueViewModel { Email = user!.Email!, itemValues = itemValues, TotalValue = userItemsValue });
        }
    }
}
