using Api_intro.Data;
using Api_intro.DTOs.City;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class CityService : ICityService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CityService(AppDbContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task CreateAsync(CityCreateDto city)
        {
            await _context.Cities.AddAsync(_mapper.Map<City>(city));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if ( city is null)
            {
                throw new NotFoundException("Not Found");
            }
            _context.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, CityEditDto city)
        {
            City existCity = await _context.Cities.AsNoTracking()
                                             .FirstOrDefaultAsync(x => x.Id == id)
                                                ?? throw new NotFoundException("Data not found!");
            _mapper.Map(city, existCity);
            _context.Update(existCity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            var cities = await _context.Cities.AsNoTracking().Include(x => x.Country).ToListAsync();
           return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto> GetByIdAsync(int id)
        {
          var city =  await _context.Cities.AsNoTracking().Include(x => x.Country).FirstOrDefaultAsync(c => c.Id == id);
            if (city is null)
            {
                throw new NotFoundException("Not found");  
            }

            return _mapper.Map<CityDto>(city);

        }
    }
}
