
namespace ApiChallenge.Data.Dtos
{
    public class ReadVideoDto
    {

        public Guid id { get; set; }
        public string description { get; set; }
        public long size { get; set; }
        public Guid serverId { get; set; }
        public string path { get; set; }


    }
}
