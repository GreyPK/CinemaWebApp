using System.Linq;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class SearchSessionController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        public ActionResult Index(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return View();
            }
            date = date.ToLower().Trim();

            var sessionsDate = db.session.ToList().Where(c => c.date_session.ToString().Equals(date.ToString()));
            var sessionsTime = db.session.ToList().Where(c => c.time_session.ToString().Equals(date.ToString()));
            var sessionsBasePrice = db.session.ToList().Where(c => c.base_price.ToString().Equals(date.ToString()));
            var sessionsFilmName = db.session.ToList().Where(c => c.film.film_name.ToLower().Contains(date.ToString()));
            var sessionsRoomNumber =
                db.session.ToList().Where(c => c.room.number_room.ToString().Equals(date.ToString()));
            var sessionsRoomType =
                db.session.ToList().Where(c => c.room.room_type.name_type_room.ToLower().Contains(date.ToString()));

            ViewBag.sessionsDate = sessionsDate.ToList();
            ViewBag.sessionsTime = sessionsTime.ToList();
            ViewBag.sessionsBasePrice = sessionsBasePrice.ToList();
            ViewBag.sessionsFilmName = sessionsFilmName.ToList();
            ViewBag.sessionsRoomNumber = sessionsRoomNumber.ToList();
            ViewBag.sessionsRoomType = sessionsRoomType.ToList();
            return View();
        }
    }
}