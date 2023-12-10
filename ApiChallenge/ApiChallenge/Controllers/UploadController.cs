namespace ApiChallenge.Controllers
{
    using ApiChallenge.Data;
    using ApiChallenge.Data.Dtos;
    using ApiChallenge.Models;
    using AutoMapper;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.IO;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/servers/file")]
    public class UploadController : ControllerBase
    {
        private ServerContext _context;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public UploadController(ServerContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        // Rota para obter o conteúdo do vídeo pelo caminho
        [HttpGet("path")]
        public IActionResult DownloadVideoByPath([FromQuery] string path)
        {
            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
            {
                return NotFound($"Vídeo no caminho '{path}' não encontrado.");
            }

            try
            {
                // Leia o conteúdo do arquivo
                var videoContent = System.IO.File.ReadAllBytes(path);

                // Determine o tipo de mídia com base na extensão do arquivo (ajuste conforme necessário)
                var contentType = "video/mp4"; // Exemplo para arquivo de vídeo MP4

                // Configurar o header Content-Disposition para forçar o download
                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    FileName = Path.GetFileName(path),
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
