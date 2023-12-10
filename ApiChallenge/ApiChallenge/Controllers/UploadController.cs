namespace ApiChallenge.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/servers/file")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("file")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            // Crie um caminho único para salvar o arquivo
            var filePath = Path.Combine(_environment.ContentRootPath, "Uploads", file.FileName);

            // Salve o arquivo no caminho especificado
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok($"Arquivo {file.FileName} enviado com sucesso.");
        }
    }

}
