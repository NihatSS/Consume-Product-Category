namespace Api_intro.Models
{
    public class GroupStudent
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
