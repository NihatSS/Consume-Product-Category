using System.ComponentModel.DataAnnotations;

namespace PB102_Consume.Areas.Admin.ViewModels.Countries
{
    public class CountryEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Population { get; set; }
    }
}
