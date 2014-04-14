using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc_race_v3;

namespace mvc_race_v3.Controllers
{
    public class RaceController : Controller
    {
        private mvcRallyRaceEntities db = new mvcRallyRaceEntities();

        // GET: /Race/
        public ActionResult Index()
        {
            var tblraces = db.tblRaces.Include(t => t.tblRallyRacer);
            return View(tblraces.ToList());
        }

        // GET: /Race/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRace tblrace = db.tblRaces.Find(id);
            if (tblrace == null)
            {
                return HttpNotFound();
            }
            return View(tblrace);
        }

        // GET: /Race/Create
        public ActionResult Create()
        {
            ViewBag.Winner = new SelectList(db.tblRallyRacers, "RacerID", "LastName");
            return View();
        }

        // POST: /Race/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RaceID,RaceLocation,RaceLengthMiles,RaceWinnings,Winner")] tblRace tblrace)
        {
            if (ModelState.IsValid)
            {
                db.tblRaces.Add(tblrace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Winner = new SelectList(db.tblRallyRacers, "RacerID", "LastName", tblrace.Winner);
            return View(tblrace);
        }

        // GET: /Race/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRace tblrace = db.tblRaces.Find(id);
            if (tblrace == null)
            {
                return HttpNotFound();
            }
            ViewBag.Winner = new SelectList(db.tblRallyRacers, "RacerID", "LastName", tblrace.Winner);
            return View(tblrace);
        }

        // POST: /Race/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RaceID,RaceLocation,RaceLengthMiles,RaceWinnings,Winner")] tblRace tblrace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblrace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Winner = new SelectList(db.tblRallyRacers, "RacerID", "LastName", tblrace.Winner);
            return View(tblrace);
        }

        // GET: /Race/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRace tblrace = db.tblRaces.Find(id);
            if (tblrace == null)
            {
                return HttpNotFound();
            }
            return View(tblrace);
        }

        // POST: /Race/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRace tblrace = db.tblRaces.Find(id);
            db.tblRaces.Remove(tblrace);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
