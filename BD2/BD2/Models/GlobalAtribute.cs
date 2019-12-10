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
        public virtual List<ItemGlobalAtribute> ItemGlobalAtributes { get; set; }
        public GlobalAtributeDto GetDto()
        {
            return new GlobalAtributeDto
            {
                Id = this.Id,
                Name = this.Name,
                Content = this.Content,
                ItemIds = this.ItemGlobalAtributes.Select(iga => iga.ItemId).ToList()
            };
        }
    }
}
