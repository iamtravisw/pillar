using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pillar.Comment;
using Xunit;

namespace pillar.test
{
    public class CommentsControllerTest
    {
        [Fact]
        public async Task CreateTicket_ReturnsOk_WhenPost()
        {
            // Arrange
            var controller = new CommentsController();
            var comment = new Comment.Comment()
            {
                UserId = -1,
                TicketId = -1,
                Message = "Test",
                PostedDate = DateTime.Now,
                AddDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                User = new User.User()
                {
                    UserId = -1,
                    UserName = "Test",
                }
            };
            
            // Act
            var response = controller.CreateTicket(comment);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}