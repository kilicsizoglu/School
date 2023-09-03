using Microsoft.EntityFrameworkCore;
using School.API.Models;

namespace School.API.Data
{
    public class SchoolDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SchoolDB;Trusted_Certificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Admin> admins { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<ClassModel> classModels  { get; set; }
        public DbSet<APIKey> apiKeys  { get; set; }


    }
}
