namespace Api_intro.DTOs.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Products { get; set; }
    }
}
