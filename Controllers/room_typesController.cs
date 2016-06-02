using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class room_typesController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        // GET: room_types
        public ActionResult Index()
        {
            return View(db.room_type.ToList());
        }

        // GET: room_types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room_type = db.room_type.Find(id);
            if (room_type == null)
            {
                return HttpNotFound();
            }
            return View(room_type);
        }

        // GET: room_types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: room_types/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "name_type_room,count_rows,count_seats,id_type_room")] room_type room_type)
        {
            if (ModelState.IsValid)
            {
                db.room_type.Add(room_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room_type);
        }

        // GET: room_types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room_type = db.room_type.Find(id);
            if (room_type == null)
            {
                return HttpNotFound();
            }
            return View(room_type);
        }

        // POST: room_types/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "name_type_room,count_rows,count_seats,id_type_room")] room_type room_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room_type);
        }

        // GET: room_types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room_type = db.room_type.Find(id);
            if (room_type == null)
            {
                return HttpNotFound();
            }
            return View(room_type);
        }

        // POST: room_types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var room_type = db.room_type.Find(id);
            db.room_type.Remove(room_type);
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