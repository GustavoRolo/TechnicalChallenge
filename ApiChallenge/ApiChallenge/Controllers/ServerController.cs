﻿using ApiChallenge.Data;
using ApiChallenge.Data.Dtos;
using ApiChallenge.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

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
        /// <summary>
        /// Cria um novo servidor
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateServer([FromBody] CreateServerDto serverDto)
        {
            Server server = _mapper.Map<Server>(serverDto);
            _context.Servers.Add(server);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetServersId), new { id = server.id }, server);
        }
        /// <summary>
        /// Retornar uma lista de servidores
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<ReadServerDto> GetServers([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadServerDto>>(_context.Servers.Skip(skip).Take(take));
        }

        /// <summary>
        /// Retorna um servidor específico pelo ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public IActionResult GetServersId(Guid id)
        {

            var server = _context.Servers.FirstOrDefault(servers => servers.id == id);
            if (server == null) return NotFound();
            var serverDto = _mapper.Map<ReadServerDto>(server);
            return Ok(serverDto);
        }

        /// <summary>
        /// Atualiza um servidor pelo ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public IActionResult UpdateServer(Guid id, [FromBody] UpdateServerDto serverDto)
        {
            var server = _context.Servers.FirstOrDefault(server => server.id == id);
            if (server == null) return NotFound();
            _mapper.Map(serverDto, server);
            _context.SaveChanges();
            return GetServersId(id);
        }

        /// <summary>
        /// Atualiza parcialmente um servidor pelo ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch("{id}")]
        public IActionResult UpdateServerPatch(Guid id, JsonPatchDocument<UpdateServerDto> patch)
        {

            var server = _context.Servers.FirstOrDefault(server => server.id == id);
            if (server == null) return NotFound();

            var serverUpdate = _mapper.Map<UpdateServerDto>(server);
            patch.ApplyTo(serverUpdate, ModelState);

            if (!TryValidateModel(serverUpdate))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(serverUpdate, server);
            _context.SaveChanges();
            return GetServersId(id);
        }

        /// <summary>
        /// Deleta um servidor pelo ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult DeleteServer(Guid id)
        {
            var server = _context.Servers.FirstOrDefault(server => server.id == id);
            if (server == null) return NotFound();
            _context.Remove(server);
            _context.SaveChanges(true);
            return NoContent();
        }

        /// <summary>
        ///  Verifica a disponibilidade de um servidor pelo ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("available/{id}​")]
        public IActionResult GetServerAvailable(Guid id)
        {
            var server = _context.Servers.FirstOrDefault(server => server.id == id);
            if (server == null) return NotFound();
            var isAvailable = IsServerAvailable(server.ip, server.port);
            if (isAvailable) return Ok(new { available = true, message = "O servidor está disponível." });
            else return StatusCode(503, new { available = false, message = "O servidor não está disponível." });
        }

        [NonAction]
        public bool IsServerAvailable(string ipAddress, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    // Tente conectar ao servidor
                    client.Connect(ipAddress, port);
                    return true;
                }
            }
            catch (SocketException)
            {
                // Tratar exceções, se necessário
                return false;
            }
        }
    }
}
