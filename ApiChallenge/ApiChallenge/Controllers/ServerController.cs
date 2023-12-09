using ApiChallenge.Data;
using ApiChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiChallenge.Controllers
{
    [ApiController]
    [Route("api/servers")]
    public class ServerController : ControllerBase
    {
        private ServerContext _context;


        public ServerController(ServerContext context) 
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult createServer([FromBody]Server server) 
        {
            _context.Servers.Add(server);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getServersId), new { id = server.Id}, server);
        }

        [HttpGet]
        public IEnumerable<Server> getServers([FromQuery]int skip = 0, [FromQuery] int take = 50)
        {
            return _context.Servers.Skip(skip).Take(take);
        }


        [HttpGet("{id}")]
        public IActionResult getServersId(Guid id)
        {
            
            var server = _context.Servers.FirstOrDefault(servers => servers.Id == id);
            if(server == null) return NotFound();
            return Ok(server);
        }
    }
}
