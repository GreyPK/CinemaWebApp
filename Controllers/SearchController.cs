using System.Linq;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly normCinemaEntities db = new normCinemaEntities();

        public ActionResult Index(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return View();
            }
            date = date.ToLower().Trim();

            var sessions = db.session.ToList().Where(c => c.date_session.ToString().Contains(date.ToString()) ||
                                                          c.time_session.ToString().Contains(date.ToString()) ||
                                                          c.film.film_name.ToLower().Contains(date.ToString()) ||
                                                          c.base_price.ToString().Contains(date.ToString()) ||
                                                          c.room.number_room.ToString().Contains(date.ToString()) ||
                                                          c.room.room_type.name_type_room.ToLower()
                                                              .Contains(date.ToString())
                );

            var films = db.film.ToList().Where(c => c.film_name.ToLower().Contains(date.ToString()) ||
                                                    c.producer.ToLower().Contains(date.ToString()) ||
                                                    c.genre.ToLower().Contains(date.ToString()) ||
                                                    c.roles.ToLower().Contains(date.ToString()) ||
                                                    c.duration.ToLower().Contains(date.ToString()) ||
                                                    c.review.ToLower().Contains(date.ToString()) ||
                                                    c.film_type.film_type_name.ToString().Contains(date.ToString())
                );

            var rooms = db.room.ToList().Where(c => c.room_type.name_type_room.ToLower().Contains(date.ToString()) ||
                                                    c.number_room.ToString().Contains(date.ToString())
                );

            var tickets = db.ticket.ToList().Where(c => c.session.date_session.ToString().Contains(date.ToString()) ||
                                                        c.session.time_session.ToString().Contains(date.ToString()) ||
                                                        c.session.film.film_name.ToLower().Contains(date.ToString()) ||
                                                        c.session.base_price.ToString().Contains(date.ToString()) ||
                                                        c.session.room.number_room.ToString().Contains(date.ToString()) ||
                                                        c.session.room.room_type.name_type_room.ToLower()
                                                            .Contains(date.ToString())
                );

            ViewBag.tickets = tickets.ToList();
            ViewBag.films = films.ToList();
            ViewBag.rooms = rooms.ToList();
            ViewBag.sessions = sessions.ToList();
            return View();
        }
    }
}