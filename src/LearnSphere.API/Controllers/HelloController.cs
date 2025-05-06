using LearnSphere.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearnSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly IHelloService _helloService;

        public HelloController(IHelloService helloService)
        {
            _helloService = helloService;
        }

        [HttpGet]
        public IActionResult Get()
            => Ok(new { Message = _helloService.GetMessage() });
    }
}
