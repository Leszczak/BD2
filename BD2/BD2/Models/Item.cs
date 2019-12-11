using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Item
    {
        public Item()
        {
            ItemAtributes = new List<ItemAtribute>();
            ItemGlobalAtributes = new List<ItemGlobalAtribute>();
            ItemGroups = new List<ItemGroup>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Photo Photo { get; set; }
        public virtual List<ItemAtribute> ItemAtributes { get; set; }
        public virtual List<ItemGlobalAtribute> ItemGlobalAtributes { get; set; }
        public virtual List<ItemGroup> ItemGroups { get; set; }
        public ItemDto GetDto()
        {
            return new ItemDto
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                PhotoId = this.Photo.Id,
                AtributeIds = this.ItemAtributes.Select(ia => ia.AtributeId).ToList(),
                GlobalAtributeIds = this.ItemGlobalAtributes.Select(iga => iga.GlobalAtributeId).ToList(),
                GroupIds = this.ItemGroups.Select(ig => ig.GroupId).ToList()
            };
        }
    }
}
