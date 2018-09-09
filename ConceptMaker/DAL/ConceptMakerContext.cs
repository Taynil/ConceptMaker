using ConceptMaker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ConceptMaker.DAL
{
    public class ConceptMakerContext : DbContext
    {
        public ConceptMakerContext() : base("DefaultConnection")
        {

        }


        public DbSet<Instance> Instances { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Concept> Concepts { get; set; }
        public DbSet<Law> Laws { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}