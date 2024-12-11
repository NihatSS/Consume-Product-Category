using System.ComponentModel.DataAnnotations;

namespace PB102_Consume.Areas.Admin.ViewModels.Products
{
    public class ProductEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Desc { get; set; }
        public decimal Price { get; set; }

        public List<IFormFile> Photos { get; set; }
    }
}
