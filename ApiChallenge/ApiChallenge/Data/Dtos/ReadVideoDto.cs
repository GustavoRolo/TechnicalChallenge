using ApiChallenge.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Dtos
{
    public class ReadVideoDto
    {

        public Guid Id { get; set; }
        public string Description { get; set; }
        public long Size { get; set; }
        public Guid ServerId { get; set; }

       
    }
}
