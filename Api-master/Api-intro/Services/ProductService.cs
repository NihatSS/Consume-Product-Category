using Api_intro.Data;
using Api_intro.DTOs.Products;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ProductService(AppDbContext ccontext,
                           IMapper mapper,
                           IFileService fileService)
        {
            _context = ccontext;
            _mapper = mapper;
            _fileService = fileService;
        }


        public async Task CreateAsync(ProductCreateDto product)
        {
            var mappedData = _mapper.Map<Product>(product);

            await _context.Products.AddAsync(mappedData);
            await _context.SaveChangesAsync();

            foreach (var item in product.Photos)
            {
                var response = await _fileService.UploadAsync(item);
                if (response.HasError)
                {
                    throw new BadHttpRequestException(response.Response);
                }

                await _context.ProductImages.AddAsync(new ProductImage
                {
                    ProductId = mappedData.Id,
                    Image = $"{response.Response}",
                    Path = $"https://localhost:7001/uploads/{response.Response}"
                });
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _context.Products.Include(x=>x.ProductImages)
                                                                               .Include(x=>x.ProductCategories)
                                                                               .ThenInclude(x=>x.Category)
                                                                               .AsNoTracking()
                                                                               .ToListAsync());        
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return _mapper.Map<ProductDto>(await _context.Products.Include(x => x.ProductImages)
                                                                  .Include(x => x.ProductCategories)
                                                                  .ThenInclude(x => x.Category)
                                                                  .AsNoTracking()
                                                                  .FirstOrDefaultAsync(x=>x.Id == id))
                                                                          ?? throw new NotFoundException("Product not found");
        }


        public async Task EditAsync(int id, ProductEditDto product)
        {

            var existingProduct = await _context.Products.FindAsync(id)
                ?? throw new NotFoundException("Product not found");

            _mapper.Map(product, existingProduct);

            if (product.Photos != null && product.Photos.Any())
            {
                var existingImages = await _context.ProductImages.Where(pi => pi.ProductId == id).ToListAsync();
                foreach (var image in existingImages)
                {
                    var fileName = Path.GetFileName(image.Image);
                    _fileService.Delete(fileName);
                }
                _context.ProductImages.RemoveRange(existingImages);

                foreach (var photo in product.Photos)
                {
                    var response = await _fileService.UploadAsync(photo);
                    if (response.HasError)
                    {
                        throw new BadHttpRequestException(response.Response);
                    }

                    await _context.ProductImages.AddAsync(new ProductImage
                    {
                        ProductId = id,
                        Image = $"{response.Response}",
                        Path = $"https://localhost:7001/uploads/{response.Response}"
                        
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var exisProduct = await _context.Products.FindAsync(id);
            _context.Products.Remove(exisProduct);
            foreach (var item in _context.ProductImages.Where(x=>x.ProductId == exisProduct.Id))
            {
                _fileService.Delete(item.Image);
                _context.ProductImages.Remove(item);
            }

            foreach (var item in _context.ProductCategories.Where(x => x.ProductId == exisProduct.Id))
            {
                _context.ProductCategories.Remove(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddCategory(int productId, int categoryId)
        {
            var existProduct = await _context.Products.FindAsync(productId)
                                                        ?? throw new NotFoundException("Product not found");
            var existCategory = await _context.Categories.FindAsync(categoryId)
                                                            ?? throw new NotFoundException("Category not found");
            await _context.ProductCategories.AddAsync(new ProductCategory { CategoryId = categoryId, ProductId = productId });
            await _context.SaveChangesAsync();
        }
    }
}
