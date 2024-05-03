using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentCRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCRUD.Insfrastructure
{
    public class ApplicationDBContext : IdentityDbContext<StudentTask>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =desktop-6qdoe18; Database=StudentData;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }
        public DbSet<Student> Students { get; set; }
    }
}
