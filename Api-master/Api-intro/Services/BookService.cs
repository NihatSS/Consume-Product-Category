using Api_intro.Data;
using Api_intro.DTOs.Books;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookService(AppDbContext ccontext,
                           IMapper mapper)
        {
            _context = ccontext;
            _mapper = mapper;
        }

        public async Task AddAuthorToBookAsync(int bookId, int authorId)
        {
            var existBook = await _context.Books.AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == bookId)
                                                    ?? throw new NotFoundException("Book not found");
            var existAuthor = await _context.Authors.AsNoTracking()
                                                    .FirstOrDefaultAsync(x => x.Id == authorId)
                                                         ?? throw new NotFoundException("Author not found"); 
            await _context.BookAuthors.AddAsync(new BookAuthor { BookId = bookId, AuthorId = authorId });
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(BookCreateDto book)
        {
            await _context.Books.AddAsync(_mapper.Map<Book>(book));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.Id == id)
                                                 ?? throw new NotFoundException("Book not found");
            
            foreach (var item in _context.BookAuthors.Where(x => x.Id == book.Id))
            {
                _context.BookAuthors.Remove(item);
            }

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, BookEditDto book)
        {
            var existBook = await _context.Books.AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == id)
                                                    ?? throw new NotFoundException("Book not found");
            _mapper.Map(book, existBook);
            _context.Update(existBook);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _context.Books.Include(x=>x.BookAuthors)
                                                                         .ThenInclude(x=>x.Author)
                                                                         .AsNoTracking()
                                                                         .ToListAsync());
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            return _mapper.Map<BookDto>(await _context.Books.Include(x => x.BookAuthors)
                                                            .ThenInclude(x => x.Author)
                                                            .AsNoTracking()
                                                            .FirstOrDefaultAsync(x=>x.Id == id))
                                                                   ?? throw new NotFoundException("Book not found");
        }

        public async Task RemoveAuthorFromBookAsync(int bookId, int authorId)
        {
            var data = await _context.BookAuthors.AsNoTracking()
                                                  .FirstOrDefaultAsync(x=>x.BookId == bookId && x.AuthorId == authorId);
            _context.BookAuthors.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
