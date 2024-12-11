namespace Api_intro.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
