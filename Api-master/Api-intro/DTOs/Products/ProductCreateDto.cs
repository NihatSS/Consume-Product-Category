namespace Api_intro.DTOs.Products
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
