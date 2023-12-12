using ApiChallenge.Data;
using ApiChallenge.Data.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiChallenge.Controllers
{
    [ApiController]
    [Route("/api/recycler")]
    public class RecyclerController​​ : ControllerBase
    {
        private ServerContext _context;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public RecyclerController(ServerContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;

        }

        /// <summary>
        /// Altera a quantidade de dias em que a rotina de limpeza é acionada [ROTINA NÃO IMPLEMENTADA]
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("process/{days}​")]
        public IActionResult GetRecyclerDays(int days)
        {
            var recycler = _context.Recyclers.FirstOrDefault();
            recycler.days = days;
            _context.SaveChanges();
            return GetRecyclerStatus();
        }
        /// <summary>
        /// Retorna a configuração da rotina de limpeza [ROTINA NÃO IMPLEMENTADA]
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("status​​")]
        public IActionResult GetRecyclerStatus()
        {
            var recycler = _context.Recyclers.FirstOrDefault();
            var recyclerDto = _mapper.Map<ReadRecyclerDto>(recycler);
            return Ok(recyclerDto);
        }

    }
}
