using Microsoft.AspNetCore.Mvc.Rendering;
using PB102_Consume.Areas.Admin.ViewModels.Countries;
using System.ComponentModel.DataAnnotations;

namespace PB102_Consume.Areas.Admin.ViewModels.Cities
{
    public class CityCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }

    }
}
