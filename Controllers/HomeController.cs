using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mheportal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace mheportal.Controllers
{
    public class HomeController : Controller
    {
        private readonly MvcPortalLinkContext _context;

        public HomeController(MvcPortalLinkContext context)
        {
            _context = context;
        }

        // GET: PortalLink
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var linkItems = from l in _context.PortalLink select l;

            if (!String.IsNullOrEmpty(searchString))
            {
            linkItems = linkItems.Where(s => s.Title.Contains(searchString));                               
            }            

            return View(await linkItems.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Admin(string searchString)
        {  
            ViewData["CurrentFilter"] = searchString;

            var linkItems = from l in _context.PortalLink select l;

            if (!String.IsNullOrEmpty(searchString))
            {
            linkItems = linkItems.Where(s => s.Title.Contains(searchString));                               
            }            

            return View(await linkItems.AsNoTracking().ToListAsync());
        }

        
        // GET: PortalLink/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalLink = await _context.PortalLink
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portalLink == null)
            {
                return NotFound();
            }

            return View(portalLink);
        }

        // GET: PortalLink/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortalLink/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,LinkUrl")] PortalLinkDataModel portalLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portalLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portalLink);
        }

        // GET: PortalLink/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalLink = await _context.PortalLink.FindAsync(id);
            if (portalLink == null)
            {
                return NotFound();
            }
            return View(portalLink);
        }

        // POST: PortalLink/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,LinkUrl")] PortalLinkDataModel portalLink)
        {
            if (id != portalLink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portalLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortalLinkExists(portalLink.Id))
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
            return View(portalLink);
        }

        // GET: PortalLink/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalLink = await _context.PortalLink
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portalLink == null)
            {
                return NotFound();
            }

            return View(portalLink);
        }

        // POST: PortalLink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portalLink = await _context.PortalLink.FindAsync(id);
            _context.PortalLink.Remove(portalLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortalLinkExists(int id)
        {
            return _context.PortalLink.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
