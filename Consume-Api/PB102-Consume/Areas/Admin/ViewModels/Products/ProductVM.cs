namespace PB102_Consume.Areas.Admin.ViewModels.Products
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
