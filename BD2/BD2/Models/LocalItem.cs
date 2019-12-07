using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class LocalItem
    {
        public long Id { get; set; }
        public Item Item { get; set; }
        public Outpost Outpost { get; set; }
    }
}
