using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Photo
    {
        public long Id { get; set; }
        public string SaveLocation { get; set; }
        public string Comment { get; set; }
        public PhotoDto GetDto()
        {
            return new PhotoDto
            {
                Id = this.Id,
                Comment = this.Comment
            };
        }
    }
}
