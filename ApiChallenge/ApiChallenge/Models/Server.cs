using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Models
{
    public class Server
    {
        //Dataa Notation
        [Key]
        [Required(ErrorMessage ="O ID do servidor é obrigatório")]
        public Guid Id { get; set; }
        [Required(ErrorMessage =" O nome do servidor é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = " O IP do servidor é obrigatório")]
        public string IP { get; set; }
        [Required(ErrorMessage = " A porta do servidor é obrigatória")]
        [Range(80, 9999, ErrorMessage = "A porta deve estar entre 0 e 9999")]
        public int Port { get; set; }

    }
}
