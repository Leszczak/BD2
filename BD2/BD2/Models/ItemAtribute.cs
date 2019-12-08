using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class ItemAtribute
    {
        public long AtributeId { get; set; }
        public Atribute Atribute { get; set; }
        public long ItemId { get; set; }
        public Item Item { get; set; }
    }
}
