using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Api_Andrew.Entities;

namespace Api_Andrew.Controllers
{
    public class RatingsController : ApiController
    {
        private Practice_EmploymentEntities db = new Practice_EmploymentEntities();

        // GET: api/Ratings
        public IQueryable<object> GetRatings()
        {
            return from a in db.Ratings
                   join p in db.Ratings on a.id_Rating equals p.id_Rating into Ratings
                   join p in db.Students on a.id_Student equals p.id_Student into Student
                   join p in db.Practices on a.id_Practice equals p.id_Practice into Practices
                   select new
                   {
                       id_rating = a.id_Rating,
                       final_assessment = a.Final_Assessment,
                       surname = Student.Select(ap => ap.Surname).FirstOrDefault(),
                       name = Student.Select(ap => ap.Name).FirstOrDefault(),
                       patronymic = Student.Select(ap => ap.Patronymic).FirstOrDefault(),
                       name_of_the_practice = Practices.Select(ap => ap.Name_Of_The_Practice).FirstOrDefault(),
                   };
        }

        // GET: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.id_Rating)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ratings.Add(rating);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rating.id_Rating }, rating);
        }

        // DELETE: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int id)
        {
            return db.Ratings.Count(e => e.id_Rating == id) > 0;
        }
    }
}