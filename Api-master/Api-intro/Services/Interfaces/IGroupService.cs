using Api_intro.DTOs.Group;
using Api_intro.DTOs.Student;

namespace Api_intro.Services.Interfaces
{
    public interface IGroupService
    {
        Task Create(GroupCreateDto group);
    }
}
