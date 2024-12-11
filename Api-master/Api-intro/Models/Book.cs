namespace Api_intro.Models
{
    public class Book
    {
        public  int  Id { get; set; }
        public string Name { get; set; }
        public int? PageCount { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
