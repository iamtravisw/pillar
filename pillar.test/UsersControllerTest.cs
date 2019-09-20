using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pillar.User;
using Xunit; 

namespace pillar.test
{

public class UsersControllerTest
    {
        [Fact]
        public async Task AllUsers_ReturnsOk_WhenConnecting()
        {
            // Arrange
            var controller = new UsersController();

            // Act
            var response = controller.AllUsers();

            // Assert
            var result = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}