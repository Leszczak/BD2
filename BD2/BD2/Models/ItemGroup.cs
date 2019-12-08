using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class ItemGroup
    {
        public long GroupId { get; set; }
        public Group Group { get; set; }
        public long ItemId { get; set; }
        public Item Item { get; set; }
    }
}
