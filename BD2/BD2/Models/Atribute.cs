using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Atribute
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual List<ItemAtribute> ItemAtributes { get; set; }
        public AtributeDto ToDto()
        {
            return new AtributeDto 
            { 
                Id = this.Id, 
                Name = this.Name, 
                ItemIds = ItemAtributes
                            .Select(ia => ia.ItemId)
                            .ToList()
            };
        }
    }
}
