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
        public CourseController(LearningDbContext db) => _db = db;

        
        [HttpGet]
        public IEnumerable<Course> Get() 
            => _db.Courses;

        
        [HttpGet("{id}")]
        public ActionResult<Course> Get(int id)
        {
            var course = _db.Courses.Find(id);
            if (course == null) return NotFound();
            return course;
        }

       
        [HttpPost]
        public ActionResult<Course> Post(Course course)
        {
            course.CreatedAt = DateTime.UtcNow;
            _db.Courses.Add(course);
            _db.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }
    }
}
