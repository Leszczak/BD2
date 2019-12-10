using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class AtributeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual List<long> ItemIds { get; set; }
    }
}
