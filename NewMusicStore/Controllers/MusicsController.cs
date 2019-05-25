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
    public class MusicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Musics
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Music.Include(m => m.Genre).Include(m => m.Singer);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var musics = from m in _context.Music select m;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        musics = musics.Where(s => s.Title.Contains(searchString));

        //    }
        //    return View(await musics.ToListAsync());
        //}
        public async Task<IActionResult> Index(string musicGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Music orderby m.Genre.Title select m.Genre.Title;
            var musics = from m in _context.Music select m;
            
            if (!string.IsNullOrEmpty(searchString))
            {
                musics = musics.Where(s => s.Title.Contains(searchString));

            }
            if (!string.IsNullOrEmpty(musicGenre))
            {
                musics = musics.Where(s => s.Genre.Title==musicGenre);

            }
            var musicGenreVM = new MusicGenreViewModel
            {
                Genre = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Musics = await musics.Include(m => m.Genre)
                .Include(m => m.Singer).ToListAsync()
            };
            
            return View(musicGenreVM);
        }

        // GET: Musics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music
                .Include(m => m.Genre)
                .Include(m => m.Singer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        // GET: Musics/Create
        public IActionResult Create()
        {
            ViewBag.GenreId = new SelectList(_context.Set<Genre>(), "Id", nameof(Genre.Title));
            ViewBag.SingerId = new SelectList(_context.Set<Singer>(), "Id", nameof(Singer.Name));
            return View();
        }

        // POST: Musics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,GenreId,SingerId")] Music music)
        {
            if (ModelState.IsValid)
            {
                _context.Add(music);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GenreId = new SelectList(_context.Set<Genre>(), "Id", nameof(Genre.Title));
            ViewBag.SingerId = new SelectList(_context.Set<Singer>(), "Id", nameof(Singer.Name));
            return View(music);
        }

        // GET: Musics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music.FindAsync(id);
            if (music == null)
            {
                return NotFound();
            }
            ViewBag.GenreId = new SelectList(_context.Set<Genre>(), "Id", nameof(Genre.Title));
            ViewBag.SingerId = new SelectList(_context.Set<Singer>(), "Id", nameof(Singer.Name));
            return View(music);
        }

        // POST: Musics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,GenreId,SingerId")] Music music)
        {
            if (id != music.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(music);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicExists(music.Id))
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
            ViewBag.GenreId = new SelectList(_context.Set<Genre>(), "Id", nameof(Genre.Title));
            ViewBag.SingerId = new SelectList(_context.Set<Singer>(), "Id", nameof(Singer.Name));
            return View(music);
        }

        // GET: Musics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music
                .Include(m => m.Genre)
                .Include(m => m.Singer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var music = await _context.Music.FindAsync(id);
            _context.Music.Remove(music);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicExists(int id)
        {
            return _context.Music.Any(e => e.Id == id);
        }
    }
}
