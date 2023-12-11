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

        public RecyclerController(ServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        [HttpPost("process/{days}​")]
        public IActionResult setRecyclerDays(int days)
        {

            return Ok();
        }

        [HttpGet("status​​")]
        public IActionResult getRecyclerStatus()
        {

            return Ok();
        }
    }
}
