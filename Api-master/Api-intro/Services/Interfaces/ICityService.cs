using Api_intro.DTOs.City;

namespace Api_intro.Services.Interfaces
{
    public interface ICityService
    {
        Task CreateAsync(CityCreateDto city);
        Task <CityDto> GetByIdAsync(int id);
        Task <IEnumerable<CityDto>> GetAllAsync();
        Task DeleteAsync(int id);
        Task EditAsync(int id, CityEditDto city);
    }
}
