using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Value
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public Atribute Atribute { get; set; }
        public LocalItem LocalItem { get; set; }
    }
}
