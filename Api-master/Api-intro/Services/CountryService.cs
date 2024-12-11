using Api_intro.Data;
using Api_intro.DTOs.Countries;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CountryService(AppDbContext context,
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CountryCreateDto country)
        {
            await _context.Countries.AddAsync(_mapper.Map<Country>(country));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id) ?? throw new NotFoundException("Data notfound");
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, CountryEditDto country)
        {
            var existCountry = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(m=>m.Id == id) ?? throw new NotFoundException("Data notfound");

            _mapper.Map(country, existCountry);

            _context.Countries.Update(existCountry);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            return _mapper.Map<List<CountryDto>>(await _context.Countries.AsNoTracking().ToListAsync());
        }

        public async Task<CountryDto> GetByIdAsync(int id)
        {
            var result = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (result is null) return null;

            return _mapper.Map<CountryDto>(result);
        }

        public async Task<IEnumerable<CountryDto>> SearchAsync(string str)
        {
            return _mapper.Map<IEnumerable<CountryDto>>(await _context.Countries.Where(m => m.Name.ToLower().Trim().Contains(str.ToLower().Trim())).ToListAsync());
        }
    }
}
