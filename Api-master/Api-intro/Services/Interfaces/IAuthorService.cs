using Api_intro.DTOs.Author;
using Api_intro.DTOs.Books;

namespace Api_intro.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task CreateAsync(AuthorCreateDto book);
        Task EditAsync(int id, AuthorEditDto book);
    }
}
