using System.ComponentModel.DataAnnotations.Schema;

namespace Api_intro.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
