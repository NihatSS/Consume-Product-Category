using Api_intro.DTOs.Countries;

namespace Api_intro.Services.Interfaces
{
    public interface ICountryService
    {
        Task CreateAsync(CountryCreateDto country);
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task<CountryDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, CountryEditDto country);
        Task<IEnumerable<CountryDto>> SearchAsync(string str);
    }
}
