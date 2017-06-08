namespace TestsCollector.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public string TeacherId { get; set; }
        public string StudentId { get; set; }
    }
}