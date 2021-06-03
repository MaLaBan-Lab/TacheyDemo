using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Tachey001.Models
{
    public partial class TacheyDBContext : DbContext
    {
        public TacheyDBContext()
            : base("name=TacheyDBContext")
        {
        }

        public virtual DbSet<Category_Details> Category_Details { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Course_Category> Course_Category { get; set; }
        public virtual DbSet<Course_Chapter> Course_Chapter { get; set; }
        public virtual DbSet<Course_HomeWork> Course_HomeWork { get; set; }
        public virtual DbSet<Course_Score> Course_Score { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Owner_Type> Owner_Type { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Course_Unit> Course_Unit { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category_Details>()
                .Property(e => e.CategoryDetailsID)
                .IsUnicode(false);

            modelBuilder.Entity<Category_Details>()
                .HasMany(e => e.Course)
                .WithRequired(e => e.Category_Details)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.OriginalPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Course>()
                .Property(e => e.PreOrderPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Course>()
                .Property(e => e.CategoryDetailsID)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Course_Chapter)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Course_Score)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course_Category>()
                .HasMany(e => e.Category_Details)
                .WithRequired(e => e.Course_Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course_Chapter>()
                .Property(e => e.ChapterID)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Chapter>()
                .HasMany(e => e.Course_HomeWork)
                .WithRequired(e => e.Course_Chapter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course_Chapter>()
                .HasMany(e => e.Course_Unit)
                .WithRequired(e => e.Course_Chapter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course_HomeWork>()
                .Property(e => e.ChapterID)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Course_HomeWork)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Course_Score)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Answer)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Owner)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Owner_Type>()
                .HasMany(e => e.Owner)
                .WithRequired(e => e.Owner_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questions>()
                .Property(e => e.ChapterID)
                .IsUnicode(false);

            modelBuilder.Entity<Questions>()
                .Property(e => e.UnitID)
                .IsUnicode(false);

            modelBuilder.Entity<Questions>()
                .HasMany(e => e.Answer)
                .WithRequired(e => e.Questions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course_Unit>()
                .Property(e => e.ChapterID)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Unit>()
                .Property(e => e.UnitID)
                .IsUnicode(false);
        }
    }
}
