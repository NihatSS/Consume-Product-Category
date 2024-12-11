using Api_intro.DTOs.Categories;

namespace Api_intro.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateDto category);
        Task DeleteAsync(int id);
        Task EditAsync(int id, CategoryEditDto category);
    }
}
