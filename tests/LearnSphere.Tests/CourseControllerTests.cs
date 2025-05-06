using LearnSphere.API.Controllers;
using LearnSphere.Core.Entities;
using LearnSphere.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LearnSphere.Tests
{
    public class CourseControllerTests
    {
        private LearningDbContext GetDb()
        {
            var opts = new DbContextOptionsBuilder<LearningDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            return new LearningDbContext(opts);
        }

        [Fact]
        public void Post_Then_Get_ReturnsSameCourse()
        {
            // Arrange
            var db = GetDb();
            var ctrl = new CourseController(db);
            var course = new Course { Title = "Test", Description = "Desc" };

            // Act
            var actionResult = ctrl.Post(course);
            // ActionResult<Course> içinden CreatedAtActionResult'ı al:
            var postResult = actionResult.Result as CreatedAtActionResult;
            Assert.NotNull(postResult);  

            // CreatedAtActionResult.Value özelliği Course objesini içerir:
            var created = postResult.Value as Course;
            Assert.NotNull(created);

            // Şimdi GET metodu:
            var getAction = ctrl.Get(created.Id);
            // ActionResult<Course> içinden OkObjectResult'ı al:
            var getResult = getAction.Result as OkObjectResult;
            Assert.NotNull(getResult);

            var fetched = getResult.Value as Course;
            Assert.NotNull(fetched);

            // Assert
            Assert.Equal("Test", fetched.Title);
            Assert.Equal("Desc", fetched.Description);
        }
    }
}
