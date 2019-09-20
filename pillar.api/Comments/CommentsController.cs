using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pillar.Comment
{
    [ApiController]
    [Route("comments")]
    public class CommentsController : Controller
    {
        [HttpPost(""), Authorize]
        [Produces("application/json", Type = typeof(Comment))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateTicket(Comment comment)
        {
            if (comment is null) return null;
            var response = Repository.Comments.Add(comment);
            return Ok(response);
        }
        
        [HttpGet("{commentId}")]
        [Produces("application/json", Type = typeof(Comment))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetComment(int commentId)
        {
            if (commentId == 0) return null;
            var response = Repository.Comments.Retrieve(commentId);
            return Ok(response);
        }
        
        [HttpGet("ticket/{ticketId}")]
        [Produces("application/json", Type = typeof(Comment))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllCommentForTicket(int ticketId)
        {
            if (ticketId == 0) return null;
            var response = Repository.Comments.RetrieveAll(ticketId);
            return Ok(response);
        }
    }
}