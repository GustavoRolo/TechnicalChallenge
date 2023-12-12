using ApiChallenge.Data;
using ApiChallenge.Data.Dtos;
using ApiChallenge.Models;
using ApiChallenge.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiChallenge.Controllers
{
    [ApiController]
    [Route("api/servers/{idServer}/videos")]
    public class VideoController : ControllerBase
    {
        private ServerContext _context;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        FileManager fileManager;

        public VideoController(ServerContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
            fileManager = new FileManager(_environment);
        }
        /// <summary>
        ///  Cria e armazena um vídeo no sistema de arquivos.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadVideo(IFormFile file, Guid idServer, [FromForm] CreateVideoDto videoDto)
        {
            if (file == null || file.Length == 0) return BadRequest("Nenhum arquivo enviado.");

            var server = _context.Servers.FirstOrDefault(servers => servers.id == idServer);
            if (server == null) return NotFound();

            var filePath = await fileManager.SaveFile(idServer, file, videoDto);

            Video video = _mapper.Map<Video>(videoDto);

            video.serverId = idServer;
            video.path = filePath;
            _context.Videos.Add(video);
            _context.SaveChanges();

            return GetVideosId(idServer, video.id);

        }

        /// <summary>
        ///  Retorna uma lista das informações de cadastro dos vídeos vinculados a um servidor
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<ReadVideoDto> GetVideos(Guid idServer)
        {
            var video = _context.Videos.Where(videos => videos.serverId == idServer).ToList();

            return _mapper.Map<List<ReadVideoDto>>(_context.Videos.Where(videos => videos.serverId == idServer).ToList());
        }

        /// <summary>
        ///  Retorna um vídeo pelo ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{idVideo}")]
        public IActionResult GetVideosId(Guid idServer, Guid idVideo)
        {

            var video = _context.Videos.FirstOrDefault(video => video.serverId == idServer && video.id == idVideo);
            if (video == null) return NotFound();

            var videoDto = _mapper.Map<ReadVideoDto>(video);
            return Ok(videoDto);
        }

        /// <summary>
        ///  Deleta um vídeo pelo ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{idVideo}")]
        public IActionResult DeleteVideo(Guid idVideo, Guid idServer)
        {
            var server = _context.Servers.FirstOrDefault(server => server.id == idServer);
            if (server == null) return NotFound();
            var video = _context.Videos.FirstOrDefault(videos => videos.id == idVideo);
            if (video == null) return NotFound();
            _context.Remove(video);
            _context.SaveChanges(true);
            fileManager.DeleteFile(video.path);
            return NoContent();
        }
        /// <summary>
        /// Retorna o arquivo binário do vídeo para download
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{idVideo}/binary​")]
        public IActionResult GetBinatyVideo(Guid idVideo, Guid idServer)
        {
            var server = _context.Servers.FirstOrDefault(server => server.id == idServer);
            if (server == null) return NotFound();
            var video = _context.Videos.FirstOrDefault(videos => videos.id == idVideo);
            if (video == null) return NotFound();
            try
            {
                // Leia o conteúdo do arquivo
                var videoContent = System.IO.File.ReadAllBytes(video.path);

                // Determine o tipo de mídia com base na extensão do arquivo (ajuste conforme necessário)
                var contentType = "video/mp4"; // Exemplo para arquivo de vídeo MP4

                // Configurar o header Content-Disposition para forçar o download
                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    FileName = Path.GetFileName(video.path),
                    Inline = false,
                };

                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

                // Retorne o FileContentResult para o cliente
                return new FileContentResult(videoContent, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o vídeo: {ex.Message}");
            }
        }

    }
}
