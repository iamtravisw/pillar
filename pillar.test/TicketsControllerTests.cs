using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pillar.Ticket;
using Xunit;

namespace pillar.test
{
    public class TicketsControllerTests
    {
        [Fact]
        public async Task GetTicketByStatus_ReturnsOk_WhenQuery()
        {
            // Arrange
            var controller = new TicketsController();
            
            // Act
            var response = controller.GetTicketByStatus("New");

            // Assert
            var result = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}