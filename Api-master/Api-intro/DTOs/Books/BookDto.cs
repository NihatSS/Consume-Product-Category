namespace Api_intro.DTOs.Books
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<string> AuthorsNames { get; set; }
    }
}
