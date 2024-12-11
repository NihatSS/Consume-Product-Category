namespace PB102_Consume.Areas.Admin.ViewModels.Categories
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Products { get; set; }
    }
}
