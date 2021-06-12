using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Tachey001.Models
{
    public partial class TacheyContext : DbContext
    {
        public TacheyContext()
            : base("name=TacheyContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseBuyed> CourseBuyed { get; set; }
        public virtual DbSet<CourseUnit> CourseUnit { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<Point> Point { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<CategoryDetail> CategoryDetail { get; set; }
        public virtual DbSet<CourseCategory> CourseCategory { get; set; }
        public virtual DbSet<CourseChapter> CourseChapter { get; set; }
        public virtual DbSet<PersonalUrl> PersonalUrl { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Course>()
                .Property(e => e.OriginalPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Course>()
                .Property(e => e.PreOrderPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Course>()
                .Property(e => e.CategoryDetailsID)
                .IsUnicode(false);

            modelBuilder.Entity<CourseUnit>()
                .Property(e => e.ChapterID)
                .IsUnicode(false);

            modelBuilder.Entity<CourseUnit>()
                .Property(e => e.UnutID)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.ChapterID)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.UnitID)
                .IsUnicode(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.Discount)
                .HasPrecision(1, 1);

            modelBuilder.Entity<CourseChapter>()
                .Property(e => e.ChapterID)
                .IsUnicode(false);
        }
    }
}
