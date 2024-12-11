using Api_intro.Data;
using Api_intro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student request)
        {
            await _context.Students.AddAsync(request);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Student student = await _context.Students.FindAsync(id);

            if (student is null) return NotFound();

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Student request)
        {
            var student = await _context.Students.FindAsync(id);
            if (student is null) return NotFound();
            student.FullName = request.FullName ?? student.FullName;
            student.Age = request.Age ?? student.Age;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
