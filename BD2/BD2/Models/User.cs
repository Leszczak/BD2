using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Authorization Authorization { get; set; }
        public Outpost Outpost { get; set; }
        public UserDto GetDto()
        {
            return new UserDto
            {
                Id = this.Id,
                Email = this.Email,
                Name = this.Name,
                Surname = this.Surname,
                AuthorizationId = this.Authorization.Id,
                OutpostId = this.Outpost.Id
            };
        }
    }
}
