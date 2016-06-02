using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class roomsController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        // GET: rooms
        public ActionResult Index()
        {
            var room = db.room.Include(r => r.room_type);
            return View(room.ToList());
        }

        // GET: rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = db.room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: rooms/Create
        public ActionResult Create()
        {
            ViewBag.id_type_room = new SelectList(db.room_type, "id_type_room", "name_type_room");
            return View();
        }

        // POST: rooms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "number_room,id_room,id_type_room")] room room)
        {
            if (ModelState.IsValid)
            {
                db.room.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_type_room = new SelectList(db.room_type, "id_type_room", "name_type_room", room.id_type_room);
            return View(room);
        }

        // GET: rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = db.room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_type_room = new SelectList(db.room_type, "id_type_room", "name_type_room", room.id_type_room);
            return View(room);
        }

        // POST: rooms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "number_room,id_room,id_type_room")] room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_type_room = new SelectList(db.room_type, "id_type_room", "name_type_room", room.id_type_room);
            return View(room);
        }

        // GET: rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = db.room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var room = db.room.Find(id);
            db.room.Remove(room);
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