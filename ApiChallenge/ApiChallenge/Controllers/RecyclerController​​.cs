using ApiChallenge.Data;
using ApiChallenge.Data.Dtos;
using ApiChallenge.Utilities;
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
        FileManager fileManager;

        public RecyclerController(ServerContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
            fileManager = new FileManager(_environment);

        }

        [HttpPut("process/{days}​")]
        public IActionResult GetRecyclerDays(int days)
        {
            var recycler = _context.Recyclers.FirstOrDefault();
            recycler.days = days;
            _context.SaveChanges();
            return GetRecyclerStatus();
        }

        [HttpGet("status​​")]
        public IActionResult GetRecyclerStatus()
        {
            var recycler = _context.Recyclers.FirstOrDefault();
            var recyclerDto = _mapper.Map<ReadRecyclerDto>(recycler);
            return Ok(recyclerDto);
        }

        [HttpPut("status​​")]
        public IActionResult SetRecyclerStatus([FromBody] UpdateRecyclerDto status)
        {
            var recycler = _context.Recyclers.FirstOrDefault();
            recycler.run = status.run;
            _context.SaveChanges();
            return GetRecyclerStatus();

        }

    }
}
