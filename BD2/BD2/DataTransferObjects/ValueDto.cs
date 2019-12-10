using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class ValueDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public long AtributeId { get; set; }
        public long LocalItemId { get; set; }
    }
}
