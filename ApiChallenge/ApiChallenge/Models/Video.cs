using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Models
{
    public class Video
    {
        [Key]
        [Required(ErrorMessage = "O ID do servidor é obrigatório")]
        public Guid id { get; set; }
        [Required(ErrorMessage = " A descrição do servidor é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamanho da descrição não pode exceder 50 caracteres.")]
        public string description { get; set; }
        [Required(ErrorMessage = "O caminho do video é obrigatório")]
        public string path { get; set; }
        [Required(ErrorMessage = " O tamanho do video é obrigatória")]
        public long size { get; set; }
        [Required(ErrorMessage = " O id do servidor é obrigatório")]
        public Guid serverId { get; set; }
        [Required]
        public DateTime dateTime { get; set; } = DateTime.Now;
    }
}
