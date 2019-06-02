using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class PointOfInterestCreationDTO
    {
        [Required(ErrorMessage ="The field name is not optional")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field description is not optional")]
        [MaxLength(200)]
        public string Description { get; set; }

    }
}
