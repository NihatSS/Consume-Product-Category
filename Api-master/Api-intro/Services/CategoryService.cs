using Api_intro.Data;
using Api_intro.DTOs.Categories;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CategoryService(AppDbContext ccontext,
                           IMapper mapper,
                           IFileService fileService)
        {
            _context = ccontext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task CreateAsync(CategoryCreateDto category)
        {
            await _context.Categories.AddAsync(_mapper.Map<Category>(category));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.AsNoTracking()
                                                    .FirstOrDefaultAsync(c => c.Id == id)
                                                         ?? throw new NotFoundException("Category not found");
            _context.Categories.Remove(category);
            foreach (var item in _context.ProductCategories.Where(x=>x.CategoryId == id))
            {
                _context.ProductCategories.Remove(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, CategoryEditDto category)
        {
            var existCategory = await _context.Categories.AsNoTracking()
                                                         .FirstOrDefaultAsync(c => c.Id == id)
                                                              ?? throw new NotFoundException("Category not found");

            _mapper.Map(category, existCategory);
            _context.Update(existCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _context.Categories.Include(x=>x.ProductCategories)
                                                                                  .ThenInclude(x=>x.Product)
                                                                                  .AsNoTracking()
                                                                                  .ToListAsync());
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CategoryDto>(await _context.Categories.Include(x => x.ProductCategories)
                                                                     .ThenInclude(x => x.Product)
                                                                     .AsNoTracking()
                                                                     .FirstOrDefaultAsync(x=>x.Id ==id))
                                                                             ?? throw new NotFoundException("Not found");
        }


    }
}
