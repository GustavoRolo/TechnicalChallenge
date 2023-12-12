using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Dtos
{
    public class UpdateRecyclerDto
    {
        [Required(ErrorMessage = "O camo run é obrigatório")]
        public bool run { get; set; }
    }
}
