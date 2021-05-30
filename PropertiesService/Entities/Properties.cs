using System;
using System.Collections.Generic;
using PropertyService.Entities;

namespace PropertyService.Entities
{
    public class Properties
    {
        public IEnumerable<Property> properties { get; set; }
    }
}
