using ApiChallenge.Data;
using ApiChallenge.Data.Dtos;
using ApiChallenge.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiChallenge.Controllers
{
    [ApiController]
    [Route("api/servers")]
    public class ServerController : ControllerBase
    {
        private ServerContext _context;
        private IMapper _mapper;

        public ServerController(ServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult createServer([FromBody] CreateServerDto serverDto)
        {
            Server server = _mapper.Map<Server>(serverDto);
            _context.Servers.Add(server);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getServersId), new { id = server.Id }, server);
        }

        [HttpGet]
        public IEnumerable<ReadServerDto> getServers([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadServerDto>>(_context.Servers.Skip(skip).Take(take));
        }


        [HttpGet("{id}")]
        public IActionResult getServersId(Guid id)
        {

            var server = _context.Servers.FirstOrDefault(servers => servers.Id == id);
            if (server == null) return NotFound();
            var serverDto = _mapper.Map<ReadServerDto>(server); 
            return Ok(serverDto);
        }

        [HttpPut("{id}")]
        public IActionResult updateServer(Guid id, [FromBody] UpdateServerDto serverDto) 
        { 
            var server =  _context.Servers.FirstOrDefault(server => server.Id == id);
            if (server == null) return NotFound();
            _mapper.Map(serverDto, server);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult updateServerPatch(Guid id, JsonPatchDocument<UpdateServerDto> patch) 
        {

            var server = _context.Servers.FirstOrDefault(server => server.Id == id);
            if (server == null) return NotFound();

            var serverupdate = _mapper.Map<UpdateServerDto>(server);
            patch.ApplyTo(serverupdate, ModelState);

            if (!TryValidateModel(serverupdate)) 
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(serverupdate, server);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteServer(Guid id) 
        {
            var server = _context.Servers.FirstOrDefault(server => server.Id == id);
            if (server == null) return NotFound();
            _context.Remove(server);
            _context.SaveChanges(true);
            return NoContent();
        }


    }
}
