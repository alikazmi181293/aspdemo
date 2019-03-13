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
using AutoMapper;
using demo.Models;
using demo.Models.Data_Transfer_Objects;

namespace demo.Controllers.ApiControllers
{
    public class CoursesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Courses
        public List<CourseDto> GetCourses()
        {
            var coursesList = db.Courses.ToList();
            return Mapper.Map<List<Courses>,List<CourseDto>>(coursesList);
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Courses))]
        public IHttpActionResult GetCourses(int id)
        {
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourses(int id, Courses courses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courses.Id)
            {
                return BadRequest();
            }

            db.Entry(courses).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
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

        // POST: api/Courses
        [ResponseType(typeof(Courses))]
        public IHttpActionResult PostCourses(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Courses courses = Mapper.Map<CourseDto, Courses>(courseDto);

            db.Courses.Add(courses);
            db.SaveChanges();

            CourseDto newCourse= Mapper.Map<Courses, CourseDto>(courses);

            return CreatedAtRoute("DefaultApi", new { id = newCourse.Id }, newCourse);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Courses))]
        public IHttpActionResult DeleteCourses(int id)
        {
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return NotFound();
            }

            db.Courses.Remove(courses);
            db.SaveChanges();

            CourseDto deletedCourse = Mapper.Map<Courses, CourseDto>(courses);

            return Ok(deletedCourse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoursesExists(int id)
        {
            return db.Courses.Count(e => e.Id == id) > 0;
        }
    }
}