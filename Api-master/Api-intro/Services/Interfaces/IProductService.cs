using Api_intro.DTOs.Products;

namespace Api_intro.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateDto product);
        Task EditAsync(int id, ProductEditDto updatedProduct);
        Task DeleteAsync(int id);
        Task AddCategory(int productId, int categoryId);
    }
}
