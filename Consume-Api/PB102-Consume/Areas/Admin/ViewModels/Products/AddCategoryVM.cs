using PB102_Consume.Areas.Admin.ViewModels.Categories;
using System.ComponentModel.DataAnnotations;

namespace PB102_Consume.Areas.Admin.ViewModels.Products
{
    public class AddCategoryVM
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<ProductVM> Products { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
