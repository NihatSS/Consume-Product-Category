using Api_intro.DTOs.City;
using Api_intro.Helpers.Exceptions;
using Api_intro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityCreateDto request)
        {
            await _cityService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Successfully created");
        }


        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
           
            try 
            {
               var city =  await _cityService.GetByIdAsync(id);
                return Ok(city);
            }
            catch (NotFoundException)
            {
                return NotFound();
            } 


        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _cityService.GetAllAsync();  
            return Ok(cities);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            await _cityService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CityEditDto request)
        {
            try
            {
                await _cityService.EditAsync(id, request);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
