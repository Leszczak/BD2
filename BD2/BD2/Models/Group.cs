using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Group
    {
        public Group()
        {
            ItemGroups = new List<ItemGroup>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<ItemGroup> ItemGroups { get; set; }
        public GroupDto GetDto()
        {
            return new GroupDto
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                ItemIds = this.ItemGroups.Select(ig => ig.ItemId).ToList()
            };
        }
    }
}
