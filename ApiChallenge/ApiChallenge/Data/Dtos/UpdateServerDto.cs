using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Dtos
{
    public class UpdateServerDto
    {

        [Required(ErrorMessage = " O nome do servidor é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamanho do nome não pode exceder 50 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = " O IP do servidor é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamanho do ip não pode exceder 50 caracteres.")]
        public string IP { get; set; }
        [Required(ErrorMessage = " A porta do servidor é obrigatória")]
        [Range(80, 9999, ErrorMessage = "A porta deve estar entre 0 e 9999")]
        public int Port { get; set; }
    }
}
