using PB102_Consume.Areas.Admin.ViewModels.Author;

namespace PB102_Consume.Areas.Admin.ViewModels.Books
{
    public class AddAuthorVM
    {
        public IEnumerable<AuthorVM> Authors { get; set; }
        public IEnumerable<BookVM> Books { get; set; }
    }
}
