using Api_intro.DTOs.Books;

namespace Api_intro.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task CreateAsync(BookCreateDto book);
        Task EditAsync(int id, BookEditDto book);
        Task AddAuthorToBookAsync(int bookId, int  authorId);
        Task RemoveAuthorFromBookAsync(int bookId, int  authorId);
    }
}
