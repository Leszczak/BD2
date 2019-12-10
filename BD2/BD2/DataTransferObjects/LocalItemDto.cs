using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class LocalItemDto
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long OutpostId { get; set; }
    }
}
