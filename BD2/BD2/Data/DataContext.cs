using BD2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Atribute> Atributes { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<GlobalAtribute> GlobalAtributes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<LocalItem> LocalItems { get; set; }
        public DbSet<Outpost> Outposts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Value> Values { get; set; }

    }
}
