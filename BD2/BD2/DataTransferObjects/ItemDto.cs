using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class ItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PhotoId { get; set; }
        public virtual List<long> AtributeIds { get; set; }
        public virtual List<long> GlobalAtributeIds { get; set; }
        public virtual List<long> GroupIds { get; set; }
    }
}
