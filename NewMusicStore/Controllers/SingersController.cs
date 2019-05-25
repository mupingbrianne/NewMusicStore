using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewMusicStore.Data;
using NewMusicStore.Models;

namespace NewMusicStore.Controllers
{
    public class SingersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SingersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Singers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Singer.ToListAsync());
        }

        // GET: Singers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singer = await _context.Singer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (singer == null)
            {
                return NotFound();
            }

            return View(singer);
        }

        // GET: Singers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Singers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Singer singer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(singer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(singer);
        }

        // GET: Singers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singer = await _context.Singer.FindAsync(id);
            if (singer == null)
            {
                return NotFound();
            }
            return View(singer);
        }

        // POST: Singers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Singer singer)
        {
            if (id != singer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(singer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SingerExists(singer.Id))
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
            return View(singer);
        }

        // GET: Singers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singer = await _context.Singer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (singer == null)
            {
                return NotFound();
            }

            return View(singer);
        }

        // POST: Singers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var singer = await _context.Singer.FindAsync(id);
            _context.Singer.Remove(singer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SingerExists(int id)
        {
            return _context.Singer.Any(e => e.Id == id);
        }
    }
}
