using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CinemaWebApp.Controllers
{
    public class QuerysController : Controller
    {
        private readonly EntityConnection _db = new EntityConnection("name=normCinemaEntities");

        public ActionResult Query1(string type)
        {
            if (type == null)
                return View();

            _db.Open();
            var query = @"select VALUE film from normCinemaEntities.film
                               join normCinemaEntities.film_type on film.id_film_type = film_type.id_film_type
                               where film_type.film_type_name = @t
                               ORDER BY film.film_name";

            var data = new ObjectQuery<film>(query, new ObjectContext(_db));
            data.Parameters.Add(new ObjectParameter("t", type));
            ViewBag.data = data;
            return View();
        }

        public ActionResult Query2()
        {
            _db.Open();

            var query = @"select film.id_film, film.genre, COUNT(film.id_film) as count from normCinemaEntities.film
                                join normCinemaEntities.session on session.id_film = film.id_film
                                where session.base_price >= 70 && session.base_price <= 100 
                                GROUP BY film.genre, film.id_film";

            var data = new ObjectQuery<DbDataRecord>(query, new ObjectContext(_db));
            ViewBag.data = data;
            return View();
        }

        public ActionResult Query3(string genre, int? roomNumber)
        {
            if (genre == null || roomNumber == null)
                return View();

            _db.Open();
            var query = @"select session.id_session, film.film_name from normCinemaEntities.session
                            join normCinemaEntities.film on session.id_film = film.id_film
                            join normCinemaEntities.room on session.id_room = room.id_room
                            where film.genre = @genre and room.number_room = @roomNumber";
            var data = new ObjectQuery<DbDataRecord>(query, new ObjectContext(_db));
            data.Parameters.Add(new ObjectParameter("genre", genre));
            data.Parameters.Add(new ObjectParameter("roomNumber", roomNumber));
            ViewBag.data = data;
            return View();
        }

        public ActionResult Query4(string type)
        {
            if (type == null)
                return View();

            _db.Open();
            var query = @"select VALUE room  from normCinemaEntities.room
                                join normCinemaEntities.room_type on room.id_type_room = room_type.id_type_room
                                where room_type.name_type_room = @t
                                ORDER BY room.number_room";

            var data = new ObjectQuery<room>(query, new ObjectContext(_db));
            data.Parameters.Add(new ObjectParameter("t", type));
            ViewBag.data = data;
            return View();
        }

        public ActionResult Query5(string date)
        {
            if (date == null)
                return View();

            var regex = new Regex("^[0-9]{4}\\.[0-9]{2}\\.[0-9]{2}$");

            if (!regex.IsMatch(date))
            {
                ViewBag.errors = new List<string>();
                ViewBag.errors.Add("Введен неправильный формат даты");

                return View();
            }
            _db.Open();
            var query =
                @"select ticket.id_ticket, session.date_session, session.time_session, film.film_name, ticket.nomber_row, ticket.nomber_seat  from normCinemaEntities.ticket                                  
                                join normCinemaEntities.session on ticket.id_session = session.id_session
                                join normCinemaEntities.film on film.id_film = session.id_film
                                where session.date_session = @date";

            var data = new ObjectQuery<DbDataRecord>(query, new ObjectContext(_db));
            data.Parameters.Add(new ObjectParameter("date", DateTime.Parse(date)));
            ViewBag.data = data;
            return View();
        }

        public ActionResult Query6()
        {
            _db.Open();
            var query =
                @"select margin.id_margin, ticket.id_ticket, session.date_session, session.time_session, film.film_name, film_type.film_type_name, ticket.nomber_row, ticket.nomber_seat  from normCinemaEntities.ticket
                                join normCinemaEntities.session on ticket.id_session = session.id_session
                                join normCinemaEntities.film on film.id_film = session.id_film
                                join normCinemaEntities.film_type on film.id_film_type = film_type.id_film_type
                                join normCinemaEntities.margin on ticket.id_margin = margin.id_margin
                                where margin.id_film_type = 1 OR margin.id_film_type = 2 ";
            var data = new ObjectQuery<DbDataRecord>(query, new ObjectContext(_db));
            ViewBag.data = data;
            return View();
        }
    }
}