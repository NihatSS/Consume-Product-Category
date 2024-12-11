using Api_intro.DTOs.Group;
using Api_intro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto request)
        {
           await _groupService.Create(request);
           return CreatedAtAction(nameof(Create), "Successfully Created");
        }
    }
}
