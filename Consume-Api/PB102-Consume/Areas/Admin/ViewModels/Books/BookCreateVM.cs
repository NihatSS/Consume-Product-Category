using System.ComponentModel.DataAnnotations;

namespace PB102_Consume.Areas.Admin.ViewModels.Books
{
    public class BookCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int PageCount { get; set; }
    }
}
