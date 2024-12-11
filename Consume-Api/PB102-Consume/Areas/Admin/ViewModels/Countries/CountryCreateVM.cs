using System.ComponentModel.DataAnnotations;

namespace PB102_Consume.Areas.Admin.ViewModels.Countries
{
    public class CountryCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Population { get; set; }
    }
}
