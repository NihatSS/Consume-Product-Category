using Api_intro.Data;
using Api_intro.DTOs.Author;
using Api_intro.DTOs.Books;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api_intro.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public AuthorService(AppDbContext ccontext,
                           IMapper mapper,
                           IFileService fileService)
        {
            _context = ccontext;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task CreateAsync(AuthorCreateDto author)
        {
            var response = await _fileService.UploadAsync(author.Image);


            var mappedData = _mapper.Map<Author>(author);
            mappedData.Image = $"https://localhost:7001/uploads/{response.Response}";

            await _context.Authors.AddAsync(mappedData);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.AsNoTracking()
                                               .FirstOrDefaultAsync(x => x.Id == id)
                                                   ?? throw new NotFoundException("Author not found");
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, AuthorEditDto author)
        {
            var existAuthor = await _context.Authors.AsNoTracking()
                                                    .FirstOrDefaultAsync(x => x.Id == id)
                                                         ?? throw new NotFoundException("Author not found");
            _mapper.Map(author, existAuthor);
            _context.Update(existAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AuthorDto>>(await _context.Authors.AsNoTracking()
                                                                             .ToListAsync());
        }

        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            return _mapper.Map<AuthorDto>(await _context.Authors.AsNoTracking()
                                                                .FirstOrDefaultAsync(x => x.Id == id))
                                                                    ?? throw new NotFoundException("Author not found");
        }
    }
}
