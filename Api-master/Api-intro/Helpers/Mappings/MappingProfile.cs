using Api_intro.DTOs.Author;
using Api_intro.DTOs.Books;
using Api_intro.DTOs.Categories;
using Api_intro.DTOs.City;
using Api_intro.DTOs.Countries;
using Api_intro.DTOs.Group;
using Api_intro.DTOs.Products;
using Api_intro.Models;
using AutoMapper;

namespace Api_intro.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<CountryEditDto, Country>()
            .ForAllMembers(opts =>
            {
                opts.AllowNull();
                opts.Condition((src, dest, srcMember) => srcMember != null );
            });

            CreateMap<CityCreateDto, City>();
            CreateMap<CityEditDto, City>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));

            CreateMap<GroupCreateDto, Group>();


            #region Book
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorsNames, opt => opt.MapFrom(src => src.BookAuthors.Select(x=>x.Author.Name)));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookEditDto, Book>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            #endregion

            #region Author
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorCreateDto, Author>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.FileName));
            CreateMap<AuthorEditDto, Author>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            #endregion

            #region Product
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages.Select(x=>x.Path)))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(x=>x.Category.Name)));
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductEditDto, Product>();
            #endregion

            #region Category
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductCategories.Select(x=>x.Product.Name)));
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryEditDto, Category>();
            #endregion
        }
    }
}
