using FluentValidation;

namespace Api_intro.DTOs.City
{
    public class CityEditDto
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
    }

    public class CityEditDtoValidator : AbstractValidator<CityEditDto>
    {
        public CityEditDtoValidator()
        {
            RuleFor(x=>x.Name).NotNull().WithMessage("Name can't be null");
        }
    }
}
