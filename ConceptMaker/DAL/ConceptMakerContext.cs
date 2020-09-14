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
            Database.SetInitializer<ConceptMakerContext>(new ConceptMakerIntializer());
        }



        public virtual DbSet<Instance> Instances { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Concept> Concepts { get; set; }
        public virtual DbSet<Law> Laws { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //do errora
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            

            //
            /*modelBuilder.Entity<Rule>()
             .HasMany(c => c.Instructors).WithMany(i => i.Courses)
             .Map(t => t.MapLeftKey("CourseID")
              .MapRightKey("InstructorID")
              .ToTable("CourseInstructor"));*/
        }
    }
}