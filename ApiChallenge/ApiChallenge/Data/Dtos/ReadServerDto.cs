using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Dtos
{
    public class ReadServerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
