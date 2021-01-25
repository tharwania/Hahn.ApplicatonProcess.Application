using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.December2020.Data.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        public DbSet<Applicant> Applicants { get; set; }
    }
}
