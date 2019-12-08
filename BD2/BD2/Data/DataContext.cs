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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemAtribute>().HasKey(ia => new { ia.ItemId, ia.AtributeId });
            modelBuilder.Entity<ItemGlobalAtribute>().HasKey(iga => new { iga.ItemId, iga.GlobalAtributeId });
            modelBuilder.Entity<ItemGroup>().HasKey(ig => new { ig.ItemId, ig.GroupId });
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
        public DbSet<ItemAtribute> ItemAtributes { get; set; }
        public DbSet<ItemGlobalAtribute> ItemGlobalAtributes { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }

    }
}
