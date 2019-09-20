using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pillar.Ticket
{
    [ApiController]
    [Route("tickets")]
    public class TicketsController : Controller
    {
        [HttpPost(""), Authorize]
        [Produces("application/json", Type = typeof(Ticket))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket is null) return null;
            var response = Repository.Tickets.Add(ticket);
            return Ok(response);
        }
        
        [HttpGet("{ticketId}")]
        [Produces("application/json", Type = typeof(Ticket))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetTicket(int ticketId)
        {
            if (ticketId == 0) return null;
            var response = Repository.Tickets.Retrieve(ticketId);
            return Ok(response);
        }
        
        [HttpGet("status/{status}")]
        [Produces("application/json", Type = typeof(Ticket))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetTicketByStatus(string status)
        {
            if (status == null) return null;
            var response = Repository.Tickets.RetrieveByStatus(status);
            return Ok(response);
        }
        
        [HttpGet("status/count/{status}")]
        [Produces("application/json", Type = typeof(Ticket))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetTicketCountByStatus(string status)
        {
            if (status == null) return null;
            var response = Repository.Tickets.RetrieveCountByStatus(status);
            return Ok(response);
        }
        
        [HttpGet("status/{status}/user/{userId}")]
        [Produces("application/json", Type = typeof(Ticket))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllTicketsForUser(
            [FromRoute] string status,
            [FromRoute] int userId)
        {
            if (userId == 0) return null;
            var response = Repository.Tickets.RetrieveAllByUserId(userId, status);
            return Ok(response);
        }
        
        [HttpGet("")]
        [Produces("application/json", Type = typeof(Ticket))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllTickets()
        {
            var response = Repository.Tickets.RetrieveAll();
            return Ok(response);
        }
        
        [HttpPut("update"), Authorize]
        [Produces("application/json", Type = typeof(Ticket))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateTicket(Ticket ticket)
        {
            if (ticket is null) return null;
            var response = Repository.Tickets.UpdateTicket(ticket);
            return Ok(response);
        }
    }
}