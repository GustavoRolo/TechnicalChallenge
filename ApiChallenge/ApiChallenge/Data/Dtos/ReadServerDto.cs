

namespace ApiChallenge.Data.Dtos
{
    public class ReadServerDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
    }
}
