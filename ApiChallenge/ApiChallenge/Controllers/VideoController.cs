using ApiChallenge.Data.Dtos;
using ApiChallenge.Data;
using ApiChallenge.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApiChallenge.Controllers
{
    [ApiController]
    [Route("api/servers/{idServer}/videos")]
    public class VideoController : ControllerBase
    {
        private ServerContext _context;
        private IMapper _mapper;

        public VideoController(ServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult createVideo(Guid idServer,[FromBody] CreateVideoDto videoDto)
        {
            var server = _context.Servers.FirstOrDefault(servers => servers.Id == idServer);
            if (server == null)
            {
                return NotFound("Servidor não encontrado");
            }
            Video video = _mapper.Map<Video>(videoDto);

            video.ServerId = idServer;

            _context.Videos.Add(video);
            _context.SaveChanges();
            //return Ok(video);
            //return CreatedAtAction(nameof(getVideosId), new { idServer,id = video.Id },video);
            return getVideosId(idServer, video.Id);
        }

        [HttpGet]
        public IActionResult getVideos(Guid idServer)
        {
            var video = _context.Videos.Where(videos => videos.ServerId == idServer).ToList();
            if (video == null) return NotFound();
            var videoDto = _mapper.Map<ReadVideoDto>(video);
            return Ok(video);
        }


        [HttpGet("{idVideo}")]
        public IActionResult getVideosId(Guid idServer, Guid idVideo)
        {

            var video = _context.Videos.FirstOrDefault(videos => videos.ServerId == idServer && videos.Id == idVideo);
            if (video == null) return NotFound();

            var videoDto = _mapper.Map<ReadVideoDto>(video);
            return Ok(videoDto);
        }
    }
}
