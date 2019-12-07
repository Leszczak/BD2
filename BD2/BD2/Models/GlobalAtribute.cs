using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class GlobalAtribute
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<Item> Items { get; set; }
    }
}
