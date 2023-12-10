using ApiChallenge.Data.Dtos;
using ApiChallenge.Data;
using ApiChallenge.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting.Server;
using System.IO;

namespace ApiChallenge.Controllers
{
    [ApiController]
    [Route("api/servers/{idServer}/videos")]
    public class VideoController : ControllerBase
    {
        private ServerContext _context;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public VideoController(ServerContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadVideo(IFormFile file, Guid idServer, [FromForm] CreateVideoDto videoDto)
        {
            if (file == null || file.Length == 0) return BadRequest("Nenhum arquivo enviado.");

            var server = _context.Servers.FirstOrDefault(servers => servers.Id == idServer);
            if (server == null) return NotFound();

            var filePath = await SaveFile(idServer,file,videoDto);

            Video video = _mapper.Map<Video>(videoDto);

            video.ServerId = idServer;
            video.Path = filePath;
            _context.Videos.Add(video);
            _context.SaveChanges();

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
            DeleteFile(video.Path);
            return NoContent();
        }

        [HttpGet("{idVideo}/binary​")]
        public IActionResult getBinatyVideo(Guid idVideo, Guid idServer) 
        {
            var server = _context.Servers.FirstOrDefault(server => server.Id == idServer);
            if (server == null) return NotFound();
            var video = _context.Videos.FirstOrDefault(videos => videos.Id == idVideo);
            if (video == null) return NotFound();
            try
            {
                // Leia o conteúdo do arquivo
                var videoContent = System.IO.File.ReadAllBytes(video.Path);

                // Determine o tipo de mídia com base na extensão do arquivo (ajuste conforme necessário)
                var contentType = "video/mp4"; // Exemplo para arquivo de vídeo MP4

                // Configurar o header Content-Disposition para forçar o download
                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    FileName = Path.GetFileName(video.Path),
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
        [NonAction]
        public async Task<string> SaveFile(Guid idServer, IFormFile file, CreateVideoDto videoDto)
        {
            // Crie um caminho único para salvar o arquivo
            var uploadDirectory = Path.Combine(_environment.ContentRootPath, $"Uploads\\{idServer}");


            // Certifique-se de que o diretório de upload existe; se não, crie-o
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var filePath = Path.Combine(uploadDirectory, $"{videoDto.Id}{Path.GetExtension(file.FileName)}");

            // Salve o arquivo no caminho especificado
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }
        [NonAction]
        public bool DeleteFile(String filePath) 
        {
            try
            {
                // Verifique se o arquivo existe antes de excluir
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return true; // Exclusão bem-sucedida
                }
                else
                {
                    Console.WriteLine($"O arquivo {filePath} não existe.");
                    return false; // Arquivo não existe
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir o arquivo {filePath}: {ex.Message}");
                return false; // Erro ao excluir o arquivo
            }

          
        }
    }
}
