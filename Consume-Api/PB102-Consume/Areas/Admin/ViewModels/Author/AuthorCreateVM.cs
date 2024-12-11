using System.ComponentModel.DataAnnotations;

namespace PB102_Consume.Areas.Admin.ViewModels.Author
{
    public class AuthorCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        public IFormFile Image { get; set; }
    }

}
