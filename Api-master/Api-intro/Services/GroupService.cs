using Api_intro.Data;
using Api_intro.DTOs.Group;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;

namespace Api_intro.Services
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GroupService(AppDbContext context,
                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(GroupCreateDto group)
        {
            await _context.Groups.AddAsync(_mapper.Map<Group>(group));
            await _context.SaveChangesAsync();
        }
    }
}
