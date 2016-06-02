using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class sessionsController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        // GET: sessions
        public ActionResult Index()
        {
            var session = db.session.Include(s => s.film).Include(s => s.room);
            return View(session.ToList());
        }

        public ActionResult SpecialForTickets()
        {
            var session = db.session.Include(s => s.film).Include(s => s.room);
            return View(session.ToList());
        }

        // GET: sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var session = db.session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: sessions/Create
        public ActionResult Create()
        {
            ViewBag.id_film = new SelectList(db.film, "id_film", "film_name");
            ViewBag.id_room = db.room.Select(n => new SelectListItem
            {
                Value = n.id_room.ToString(),
                Text = "Номер кинозала: " + n.number_room + "; Тип кинозала: " + n.room_type.name_type_room
            });

            return View();
        }

        // POST: sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "date_session,time_session,base_price,id_session,id_film,id_room")] session session)
        {
            if (ModelState.IsValid)
            {
                db.session.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_film = new SelectList(db.film, "id_film", "film_name", session.id_film);
            ViewBag.id_room = new SelectList(db.room, "id_room", "id_room", session.id_room);

            return View(session);
        }

        // GET: sessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var session = db.session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_film = new SelectList(db.film, "id_film", "film_name", session.id_film);
            ViewBag.id_room = db.room.Select(n => new SelectListItem
            {
                Value = n.id_room.ToString(),
                Text = "Номер кинозала: " + n.number_room + "; Тип кинозала: " + n.room_type.name_type_room,
                Selected = n.id_room == session.room.id_room
            });

            return View(session);
        }

        // POST: sessions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "date_session,time_session,base_price,id_session,id_film,id_room")] session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_film = new SelectList(db.film, "id_film", "film_name", session.id_film);
            ViewBag.id_room = new SelectList(db.room, "id_room", "id_room", session.id_room);
            return View(session);
        }

        // GET: sessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var session = db.session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var session = db.session.Find(id);
            db.session.Remove(session);
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