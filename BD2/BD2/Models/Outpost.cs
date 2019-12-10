using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Outpost
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public OutpostDto GetDto()
        {
            return new OutpostDto
            {
                Id = this.Id,
                Name = this.Name,
                Location = this.Location
            };
        }
    }
}
