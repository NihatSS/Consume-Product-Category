using Api_intro.DTOs.City;
using FluentValidation;

namespace Api_intro.DTOs.Group
{
    public class GroupCreateDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }


    }
    public class GroupCreateDtoValidator : AbstractValidator<GroupCreateDto>
    {
        public GroupCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Capacity).InclusiveBetween(5, 20).WithMessage("Capacity length must be between 5 - 20");
        }
    }
}
