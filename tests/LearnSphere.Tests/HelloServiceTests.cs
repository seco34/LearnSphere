using LearnSphere.Core.Services;       // IHelloService burada
using LearnSphere.Services.Services;   // HelloService burada
using Xunit;

namespace LearnSphere.Tests
{
    public class HelloServiceTests
    {
        [Fact]
        public void GetMessage_ReturnsExpectedString()
        {
            // Arrange
            IHelloService service = new HelloService();

            // Act
            var result = service.GetMessage();

            // Assert
            Assert.Equal("Hello from LearnSphere Service Layer!", result);
        }
    }
}
