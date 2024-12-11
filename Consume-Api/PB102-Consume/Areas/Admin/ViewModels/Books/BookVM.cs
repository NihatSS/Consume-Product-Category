namespace PB102_Consume.Areas.Admin.ViewModels.Books
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<string> AuthorsNames { get; set; }
    }
}
