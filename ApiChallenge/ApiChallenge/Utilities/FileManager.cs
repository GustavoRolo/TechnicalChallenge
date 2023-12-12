using ApiChallenge.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiChallenge.Utilities
{
    public class FileManager
    {
        private readonly IWebHostEnvironment _environment;

        public FileManager(IWebHostEnvironment environment)
        {
            _environment = environment;
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

            var filePath = Path.Combine(uploadDirectory, $"{videoDto.id}{Path.GetExtension(file.FileName)}");

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
