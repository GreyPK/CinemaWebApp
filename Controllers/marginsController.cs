using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class marginsController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        // GET: margins
        public ActionResult Index()
        {
            var margin = db.margin.Include(m => m.film_type);
            return View(margin.ToList());
        }

        // GET: margins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var margin = db.margin.Find(id);
            if (margin == null)
            {
                return HttpNotFound();
            }
            return View(margin);
        }

        // GET: margins/Create
        public ActionResult Create()
        {
            ViewBag.id_film_type = new SelectList(db.film_type, "id_film_type", "film_type_name");
            return View();
        }

        // POST: margins/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "coef_start,coef_mid,coef_end,id_margin,id_film_type")] margin margin)
        {
            if (ModelState.IsValid)
            {
                db.margin.Add(margin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_film_type = new SelectList(db.film_type, "id_film_type", "film_type_name", margin.id_film_type);
            return View(margin);
        }

        // GET: margins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var margin = db.margin.Find(id);
            if (margin == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_film_type = new SelectList(db.film_type, "id_film_type", "film_type_name", margin.id_film_type);
            return View(margin);
        }

        // POST: margins/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "coef_start,coef_mid,coef_end,id_margin,id_film_type")] margin margin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(margin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_film_type = new SelectList(db.film_type, "id_film_type", "film_type_name", margin.id_film_type);
            return View(margin);
        }

        // GET: margins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var margin = db.margin.Find(id);
            if (margin == null)
            {
                return HttpNotFound();
            }
            return View(margin);
        }

        // POST: margins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var margin = db.margin.Find(id);
            db.margin.Remove(margin);
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