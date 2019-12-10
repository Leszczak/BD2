using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Authorization
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool CanEdit { get; set; }
        public bool IsGlobal { get; set; }
        public AuthorizationDto GetDto()
        {
            return new AuthorizationDto
            {
                Id = this.Id,
                Name = this.Name,
                CanEdit = this.CanEdit,
                IsGlobal = this.IsGlobal
            };
        }
    }
}
