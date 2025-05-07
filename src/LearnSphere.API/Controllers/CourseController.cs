using System;
using System.Collections.Generic;
using System.Linq;
using LearnSphere.Core.Entities;
using LearnSphere.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace LearnSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly LearningDbContext _db;
        public CourseController(LearningDbContext db) 
            => _db = db;

        [HttpGet]
        public ActionResult<List<Course>> GetAll()
        {
            var list = _db.Courses.ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetById(int id)
        {
            var c = _db.Courses.Find(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpPost]
        public ActionResult<Course> Post(Course course)
        {
            course.CreatedAt = DateTime.UtcNow;
            _db.Courses.Add(course);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Course input)
        {
            var c = _db.Courses.Find(id);
            if (c == null) return NotFound();
            c.Title = input.Title;
            c.Description = input.Description;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var c = _db.Courses.Find(id);
            if (c == null) return NotFound();
            _db.Courses.Remove(c);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
