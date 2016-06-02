using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class ticketsController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        // GET: tickets
        public ActionResult Index()
        {
            var ticket = db.ticket.Include(t => t.margin).Include(t => t.session);
            return View(ticket.ToList());
        }

        // GET: tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: tickets/Create
        public ActionResult Create()
        {
            ViewBag.id_margin = new SelectList(db.margin, "id_margin", "coef_start");


            ViewBag.id_session = db.session.Select(n => new SelectListItem
            {
                Value = n.id_session.ToString(),
                Text =
                    "Дата сеанса: " + n.date_session + "; Время сеанса: " + n.time_session + "; Базовая цена: " +
                    n.base_price + "; Фильм: " + n.film.film_name + "; Номер кинозала: " + n.room.number_room +
                    "; Тип кинозала: " + n.room.room_type.name_type_room
            });

            return View();
        }

        // POST: tickets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "nomber_row,nomber_seat,id_ticket,id_margin,id_session")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_margin = new SelectList(db.margin, "id_margin", "id_margin", ticket.id_margin);
            ViewBag.id_session = new SelectList(db.session, "id_session", "id_session", ticket.id_session);
            return View(ticket);
        }

        // GET: tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_margin = new SelectList(db.margin, "id_margin", "id_margin", ticket.id_margin);
            ViewBag.id_session = new SelectList(db.session, "id_session", "id_session", ticket.id_session);
            return View(ticket);
        }

        // POST: tickets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nomber_row,nomber_seat,id_ticket,id_margin,id_session")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_margin = new SelectList(db.margin, "id_margin", "id_margin", ticket.id_margin);
            ViewBag.id_session = new SelectList(db.session, "id_session", "id_session", ticket.id_session);
            return View(ticket);
        }

        // GET: tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ticket = db.ticket.Find(id);
            db.ticket.Remove(ticket);
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