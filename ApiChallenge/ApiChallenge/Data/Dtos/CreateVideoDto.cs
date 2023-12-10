using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Dtos
{
    public class CreateVideoDto
    {
        [Key]
        [Required(ErrorMessage = "O ID do servidor é obrigatório")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = " A descrição do servidor é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamanho da descrição não pode exceder 50 caracteres.")]
        public string Description { get; set; }
        [Required(ErrorMessage = " O tamanho do video é obrigatória")]
        public long Size { get; set; }

    }
}
