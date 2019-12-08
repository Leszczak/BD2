using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class ItemGlobalAtribute
    {
        public long GlobalAtributeId { get; set; }
        public GlobalAtribute GlobalAtribute { get; set; }
        public long ItemId { get; set; }
        public Item Item { get; set; }
    }
}
