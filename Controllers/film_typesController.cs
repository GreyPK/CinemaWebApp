using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class film_typesController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        // GET: film_types
        public ActionResult Index()
        {
            return View(db.film_type.ToList());
        }

        // GET: film_types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var film_type = db.film_type.Find(id);
            if (film_type == null)
            {
                return HttpNotFound();
            }
            return View(film_type);
        }

        // GET: film_types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: film_types/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "film_type_name,id_film_type")] film_type film_type)
        {
            if (ModelState.IsValid)
            {
                db.film_type.Add(film_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film_type);
        }

        // GET: film_types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var film_type = db.film_type.Find(id);
            if (film_type == null)
            {
                return HttpNotFound();
            }
            return View(film_type);
        }

        // POST: film_types/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "film_type_name,id_film_type")] film_type film_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film_type);
        }

        // GET: film_types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var film_type = db.film_type.Find(id);
            if (film_type == null)
            {
                return HttpNotFound();
            }
            return View(film_type);
        }

        // POST: film_types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var film_type = db.film_type.Find(id);
            db.film_type.Remove(film_type);
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