using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Photo Photo { get; set; }
        public virtual List<ItemAtribute> ItemAtributes { get; set; }
        public virtual List<ItemGlobalAtribute> ItemGlobalAtributes { get; set; }
        public virtual List<ItemGroup> ItemGroups { get; set; }
    }
}
