using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class filmsController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        // GET: films
        public ActionResult Index()
        {
            var film = db.film.Include(f => f.film_type);
            return View(film.ToList());
        }

        // GET: films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var film = db.film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: films/Create
        public ActionResult Create()
        {
            ViewBag.id_film_type = new SelectList(db.film_type, "id_film_type", "film_type_name");
            return View();
        }

        // POST: films/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "film_name,producer,genre,roles,duration,review,id_film,id_film_type")] film film)
        {
            if (ModelState.IsValid)
            {
                db.film.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_film_type = new SelectList(db.film_type, "id_film_type", "film_type_name", film.id_film_type);
            return View(film);
        }

        // GET: films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var film = db.film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_film_type = new SelectList(db.film_type, "id_film_type", "film_type_name", film.id_film_type);
            return View(film);
        }

        // POST: films/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "film_name,producer,genre,roles,duration,review,id_film,id_film_type")] film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_film_type = new SelectList(db.film_type, "id_film_type", "film_type_name", film.id_film_type);
            return View(film);
        }

        // GET: films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var film = db.film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var film = db.film.Find(id);
            db.film.Remove(film);
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