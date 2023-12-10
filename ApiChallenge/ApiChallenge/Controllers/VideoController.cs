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
        public IActionResult createVideo(Guid idServer, [FromBody] CreateVideoDto videoDto)
        {
            var server = _context.Servers.FirstOrDefault(servers => servers.Id == idServer);
            if (server == null) return NotFound();
            Video video = _mapper.Map<Video>(videoDto);

            video.ServerId = idServer;
            Console.WriteLine(video.Id);
            Console.WriteLine(video.ServerId);
            _context.Videos.Add(video);
            _context.SaveChanges();
            //return CreatedAtAction(nameof(getVideosId),new { idVideo = video.Id },video);
            return getVideosId(idServer, video.Id);
        }

        [HttpGet]
        public IEnumerable<ReadVideoDto> getVideos(Guid idServer)
        {
            var video = _context.Videos.Where(videos => videos.ServerId == idServer).ToList();

            return _mapper.Map<List<ReadVideoDto>>(_context.Videos.Where(videos => videos.ServerId == idServer).ToList());
        }


        [HttpGet("{idVideo}")]
        public IActionResult getVideosId(Guid idServer, Guid idVideo)
        {

            var video = _context.Videos.FirstOrDefault(video => video.ServerId == idServer && video.Id == idVideo);
            if (video == null) return NotFound();

            var videoDto = _mapper.Map<ReadVideoDto>(video);
            return Ok(videoDto);
        }


        [HttpDelete("{idVideo}")]
        public IActionResult deleteVideo(Guid idVideo, Guid idServer)
        {
            var server = _context.Servers.FirstOrDefault(server => server.Id == idServer);
            if (server == null) return NotFound();
            var video = _context.Videos.FirstOrDefault(videos => videos.Id == idVideo);
            if (video == null) return NotFound();
            _context.Remove(video);
            _context.SaveChanges(true);
            return NoContent();
        }

        
    }
}
