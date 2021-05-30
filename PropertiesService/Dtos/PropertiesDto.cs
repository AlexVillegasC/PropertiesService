using System.Collections.Generic;
using PropertyService.Entities;

namespace PropertyService.Dtos
{
    public class PropertiesDto
    {
        public IEnumerable<PropertyDto> properties { get; set; }
    }
}
