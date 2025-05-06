using LearnSphere.Core.Services;

namespace LearnSphere.Services.Services
{
    public class HelloService : IHelloService
    {
        public string GetMessage()
            => "Hello from LearnSphere Service Layer!";
    }
}
