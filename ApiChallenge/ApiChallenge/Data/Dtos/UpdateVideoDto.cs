using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Dtos
{
    public class UpdateVideoDto
    {
        [Required(ErrorMessage = " A descrição do servidor é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamanho da descrição não pode exceder 50 caracteres.")]
        public string description { get; set; }
        [Required(ErrorMessage = " O tamanho do video é obrigatória")]
        public long size { get; set; }
    }
}
