using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Luisapangilinan.Midterm.Infrastructure.Domain.Models;

namespace Luisapangilinan.Midterm.Infrastructure.Domain
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
          : base(options)
        {
        }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Course> courses = new List<Course>();
            List<Class> classes = new List<Class>();

            courses.Add(new Course()
            {
                Id = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96"),
                Name = "Joy",
                Description = "People who work in school but not teaching",
                Abbreviation = "Stf"
            });

            courses.Add(new Course()
            {
                Id = Guid.Parse("7ce68d5c-5b65-495a-8a63-14aeb48da87d"),
                Name = "Reniel",
                Description = "People who teach in school",
                Abbreviation = "Fct"
            });


            courses.Add(new Course()
            {
                Id = Guid.Parse("1fb7085a-762f-440c-87de-59f75f85e935"),
                Name = "Luisa",
                Description = "People who learn in school",
                Abbreviation = "Std"
            });

            modelBuilder.Entity<Course>().HasData(courses);


            classes.Add(new Class()
            {
                Id = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac0"),
                Code = "1234",
                YearLevel="123",
                Startdate = DateTime.Now,
                Meeting = Meeting.TTH,
                CourseId = Guid.Parse("7ce68d5c-5b65-495a-8a63-14aeb48da87d")
            });


            classes.Add(new Class()
            {
                Id = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                Code = "456",
                YearLevel = "123",
                Startdate = DateTime.Now,
                Meeting = Meeting.TTH,
                CourseId = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96")
            });


            classes.Add(new Class()
            {
                Id = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac2"),
                Code = "789",
                YearLevel = "123",
                Startdate = DateTime.Now,
                Meeting = Meeting.TTH,
                CourseId = Guid.Parse("1fb7085a-762f-440c-87de-59f75f85e935")
            });

            modelBuilder.Entity<Class>().HasData(classes);
        }

    }
}
