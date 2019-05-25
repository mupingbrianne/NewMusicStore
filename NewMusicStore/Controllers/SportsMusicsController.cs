using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB;
using MongoDB.Driver;
using NewMusicStore.Models;
using NewMusicStore.Data;

namespace NewMusicStore.Controllers
{
    public class SportsMusicsController : Controller
    {
        private MongoDBContext _mongoDBContext;
        private IMongoCollection<SportsMusic> mongoCollection;
        public SportsMusicsController(MongoDBContext mongoDataBaseInitilizer)
        {
            _mongoDBContext = mongoDataBaseInitilizer;
            mongoCollection = _mongoDBContext.mongoCollection;


            //_mongoDBContext.mongoDatabase.CreateCollection("SportsErikFirst");
            //mongoCollection = _mongoDBContext.mongoDatabase.GetCollection<SportsMusic>("SportsMusic");

        }
        // GET: SportsMusics
        public ActionResult Index()
        {
            //string id = DateTime.Now.ToString();
            //mongoCollection.InsertOne(new SportsMusic() { Title = "Mrs Xiyuan Refresh Time"+id, SingerName = "Brianne1 Refresh Time"+id});
            var list = mongoCollection.Find<SportsMusic>(m => ModelState.IsValid).ToList();



            //var list = mongoCollection.Find<SportsMusic>(m => m.Title=="Mrs Xiyuan").ToList();
            return View(list);
        }

        // GET: SportsMusics/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportsMusic = await mongoCollection
                .FindAsync(m => m.Id == id);
            if (sportsMusic == null)
            {
                return NotFound();
            }

            return View(sportsMusic.Single());
        }

        // GET: SportsMusic/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SportsMusic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,SingerName,PublishDate,LoginDate")] SportsMusic sportsMusic)
        {
            if (ModelState.IsValid)
            {
                sportsMusic.LoginDate = DateTime.Now.ToLocalTime();
                await mongoCollection.InsertOneAsync(sportsMusic);
                return RedirectToAction(nameof(Index));
            }
            return View(sportsMusic);
        }

        // GET: SportsMusic/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportsMusic = await mongoCollection.FindAsync(m => m.Id == id);
            if (sportsMusic == null)
            {
                return NotFound();
            }
            return View(sportsMusic.Single());
        }

        // POST: Singers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,SingerName,PublishDate,LoginDate")] SportsMusic sportsMusic)
        {
            if (id != sportsMusic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await mongoCollection.FindOneAndReplaceAsync(m => m.Id == id, sportsMusic);
                }
                catch (MongoException)
                {
                    if (sportsMusic.Id == null)
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
            return View(sportsMusic);
        }

        // GET: SportsMusic/Delete/
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportsMusic = await mongoCollection
                .FindAsync<SportsMusic>(m => m.Id == id);
            if (sportsMusic == null)
            {
                return NotFound();
            }

            return View(sportsMusic.Single());
        }

        // POST: SportsMusic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await mongoCollection.DeleteOneAsync<SportsMusic>(m => m.Id == id);
            return RedirectToAction(nameof(Index));
        }

    }
}