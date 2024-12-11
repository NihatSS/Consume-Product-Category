namespace Api_intro.DTOs.Author
{
    public class AuthorCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public IFormFile Image { get; set; }
    }
}
